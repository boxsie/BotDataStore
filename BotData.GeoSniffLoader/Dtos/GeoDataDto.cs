using CsvHelper.Configuration.Attributes;

namespace BotData.GeoSniffLoader.Dtos
{
    public abstract class GeoDataDto
    {
        [Name("geoname_id")]
        public int? GeoNameId { get; set; }
    }
}