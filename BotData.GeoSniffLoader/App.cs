using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotData.Data.Context;
using BotData.Data.Entity.GeoSniff;
using BotData.GeoSniffLoader.Dtos;
using CsvHelper;

namespace BotData.GeoSniffLoader
{
    public class App
    {
        private readonly BotDataContext _ctx;
        
        public App(BotDataContext ctx)
        {
            _ctx = ctx;
            _ctx.Database.EnsureCreated();
        }

        public async Task RunAsync(Opts options)
        {
            Console.WriteLine("Loading geo city data...");
            var cityData = LoadFromCsv<CityDataDto>(options.CityDataPath);
            Console.WriteLine($"{cityData.Count} rows have been loaded");

            Console.WriteLine("Loading geo location data...");
            var locData = LoadFromCsv<LocDataDto>(options.LocationDataPath);
            Console.WriteLine($"{locData.Count} rows have been loaded");

            await InsertNewLocations(cityData, locData);
        }

        private static List<T> LoadFromCsv<T>(string path) where T : GeoDataDto
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

        private async Task InsertNewLocations(IReadOnlyCollection<CityDataDto> cityData, IReadOnlyCollection<LocDataDto> locData)
        {
            const int bs = 1000;
            var len = Math.Ceiling((double) cityData.Count / bs);
            var insertCnt = 0;

            for (var i = 0; i < len; i++)
            {
                var cityBatch = cityData.Skip(i * bs).Take(bs);

                foreach (var cityDataDto in cityBatch)
                {
                    if (!cityDataDto.GeoNameId.HasValue || string.IsNullOrWhiteSpace(cityDataDto.CountryName))
                        continue;

                    var locDataDto = locData.FirstOrDefault(x => x.GeoNameId == cityDataDto.GeoNameId);

                    if (locDataDto?.Lat == null || !locDataDto.Long.HasValue || !locDataDto.Radius.HasValue)
                        continue;

                    var newGeoLoc = new Location
                    {
                        Id = cityDataDto.GeoNameId.Value,
                        Country = cityDataDto.CountryName,
                        Area = cityDataDto.AreaName,
                        SubArea = cityDataDto.SubAreaName,
                        Lat = locDataDto.Lat.Value,
                        Long = locDataDto.Long.Value,
                        Radius = locDataDto.Radius.Value
                    };

                    Console.WriteLine($"Adding {cityDataDto.GeoNameId.Value} - {cityDataDto.CountryName}...");
                    await _ctx.GeoLocations.AddAsync(newGeoLoc);
                    insertCnt++;
                }

                Console.WriteLine("Saving changes...");
                await _ctx.SaveChangesAsync();
                Console.WriteLine($"{insertCnt} locations added to database");
            }

            Console.WriteLine("Geo location update complete!");
        }
    }
}