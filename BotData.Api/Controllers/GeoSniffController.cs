using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotData.Data.Context;
using BotData.Data.Entity.GeoSniff;
using BotData.Data.Entity.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotData.Api.Controllers
{
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
    }
}