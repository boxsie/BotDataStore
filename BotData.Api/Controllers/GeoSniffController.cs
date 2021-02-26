using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotData.Data.Context;
using BotData.Data.Entity.BotUser;
using BotData.Data.Entity.Game;
using BotData.Data.Entity.GeoSniff;
using BotData.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotData.Api.Controllers
{
    [Route("api/geoscore")]
    [ApiController]
    public class GeoSniffScoresController : ControllerBase
    {
        private readonly BotDataContext _context;

        public GeoSniffScoresController(BotDataContext context)
        {
            _context = context;
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<List<GeoSniffLbEntryViewModel>>> GetLeaderboard()
        {
            var attempts = await _context
                .GuessGameAttempts
                    .Include(x => x.User)
                .ToListAsync();                

            var leaderboard = new List<GeoSniffLbEntryViewModel>();

            foreach (var att in attempts.GroupBy(x => new { x.DiscordId, x.User.Name }))
            {
                var played = att.Select(y => y.GameId).Distinct().Count();
                var won = att.Where(y => y.Correct).Count();

                leaderboard.Add(new GeoSniffLbEntryViewModel
                {
                    DiscordId = att.Key.DiscordId,
                    Name = att.Key.Name,
                    Played = played,
                    GuessPerGame = att.Count() / played,
                    Won = won,
                    WinRate = won > 0 ? (float)won / played : 0,
                    Accuracy = won > 0 ? (float)won / att.Count() : 0
                });
            }
           
            return Ok(leaderboard);
        }
    }

    [Route("api/geo")]
    [ApiController]
    public class GeoSniffController : ControllerBase
    {
        private readonly BotDataContext _context;

        private const int MinimumCountryLocations = 100;

        public GeoSniffController(BotDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await GetRandomLocation());
        }

        [HttpGet("{geoId:int}")]
        public async Task<ActionResult<Location>> GetById(int geoId)
        {
            var loc = await _context
                .GeoLocations
                .FirstOrDefaultAsync(x => x.Id == geoId);

            if (loc == null)
                return NotFound();

            return Ok(loc);
        }

        [HttpGet("{country}")]
        public async Task<ActionResult<Location>> GetByCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return NotFound();

            return Ok(await GetRandomLocation(country));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<User>> PostNewGame(GuessGameModel model)
        {
            var user = await GetUser(model.DiscordId);

            if (user == null)
                return BadRequest("User does not exist");

            var game = new GuessGame
            {
                User = user,
                GameName = model.GameName,
                CorrectAnswer = model.CorrectAnswer,
                StartedOn = DateTime.Now
            };

            await _context.GuessGames.AddAsync(game);
            await _context.SaveChangesAsync();

            return Ok(game.Id);
        }

        [HttpPut("{gameId:int}")]
        public async Task<ActionResult<User>> FinishGame(int gameId)
        {
            var game = await _context
                .GuessGames
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
                return BadRequest("Game does not exist");

            game.FinishedOn = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("guess")]
        [ValidateModel]
        public async Task<ActionResult<User>> PostAttempt(GuessGameAttemptModel model)
        {
            var game = await _context
                .GuessGames
                .Include(x => x.Attempts)
                .FirstOrDefaultAsync(x => x.Id == model.GameId);

            if (game == null)
                return BadRequest("Game does not exist");

            var user = await GetUser(model.DiscordId);

            if (user == null)
                return BadRequest("User does not exist");

            var attempt = new GuessGameAttempt
            {
                Attempt = model.Attempt,
                Game = game,
                User = user,
                CreatedOn = DateTime.Now
            };

            game.Attempts.Add(attempt);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<Location> GetRandomLocation(string countryName = null)
        {
            var rnd = new Random();

            if (string.IsNullOrWhiteSpace(countryName))
            {
                var countryNames = await GetAvailableCountries();

                while (string.IsNullOrWhiteSpace(countryName))
                {
                    countryName = countryNames[rnd.Next(countryNames.Length)];
                    var countryCount = await GetCountryCount(countryName);

                    if (countryCount < MinimumCountryLocations)
                        countryName = null;
                    else
                    {
                        return await _context.GeoLocations
                            .Where(x => x.Country.ToLower() == countryName.ToLower())
                            .Skip(rnd.Next(countryCount))
                            .Take(1)
                            .FirstAsync();
                    }
                }
            }
            else
            {
                var countryCount = await GetCountryCount(countryName);

                return await _context.GeoLocations
                    .Where(x => x.Country.ToLower() == countryName.ToLower())
                    .Skip(rnd.Next(countryCount))
                    .Take(1)
                    .FirstAsync();
            }

            throw new Exception($"Not able to find a country with less than {MinimumCountryLocations} locations");
        }

        private Task<string[]> GetAvailableCountries()
        {
            return _context.GeoLocations
                .Select(x => x.Country.ToLower())
                .Distinct()
                .ToArrayAsync();
        }

        private Task<int> GetCountryCount(string country)
        {
            return _context.GeoLocations
                .Where(x => x.Country.ToLower() == country.ToLower())
                .CountAsync();
        }

        private Task<User> GetUser(long discordId)
        {
            return _context
                .Users
                .FirstOrDefaultAsync(x => x.DiscordId == discordId);
        }
    }
}