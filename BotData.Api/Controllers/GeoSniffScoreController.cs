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

        private const int MaxScore = 20;
        private const int MinScore = 5;
        private const int IncorrectPenalty = 5;

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

                var winningGames = att
                    .GroupBy(x => x.GameId);

                var total = winningGames.Select(x => {
                    if (x.Any(y => y.Correct))
                    {
                        var score = MaxScore - ((x.Count() - 1) * IncorrectPenalty);

                        if (score < MinScore)
                            score = MinScore;

                        return score;
                    }

                    return 0;                    
                }).Sum();

                leaderboard.Add(new GeoSniffLbEntryViewModel
                {
                    DiscordId = att.Key.DiscordId,
                    Name = att.Key.Name,
                    Played = played,
                    Won = won,
                    Score = total
                });
            }

            return Ok(leaderboard.OrderByDescending(x => x.Score));
        }
    }    
}