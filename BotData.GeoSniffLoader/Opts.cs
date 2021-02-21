using CommandLine;

namespace BotData.GeoSniffLoader
{
    public class Opts
    {
        [Option('c', "citydatapath", Required = true, HelpText = "Path to the geo cities data csv")]
        public string CityDataPath { get; set; }

        [Option('l', "locdatapath", Required = true, HelpText = "Path to the geo location data csv")]
        public string LocationDataPath { get; set; }
    }
}