using CsvHelper.Configuration.Attributes;

namespace BotData.GeoSniffLoader.Dtos
{
    public class CityDataDto : GeoDataDto
    {
        [Name("country_name")]
        public string CountryName { get; set; }

        [Name("subdivision_1_name")]
        public string AreaName { get; set; }

        [Name("city_name")]
        public string SubAreaName { get; set; }
    }
}