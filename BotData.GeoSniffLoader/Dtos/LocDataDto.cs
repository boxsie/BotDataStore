using CsvHelper.Configuration.Attributes;

namespace BotData.GeoSniffLoader.Dtos
{
    public class LocDataDto : GeoDataDto
    {
        [Name("latitude")]
        public double? Lat { get; set; }

        [Name("longitude")]
        public double? Long { get; set; }

        [Name("accuracy_radius")]
        public double? Radius { get; set; }
    }
}