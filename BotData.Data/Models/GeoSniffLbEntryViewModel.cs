using System;
using System.Collections.Generic;
using System.Text;

namespace BotData.Data.Models
{
    public class GeoSniffLbEntryViewModel
    {
        public long DiscordId { get; set; }
        public string Name { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Score { get; set; }
    }
}
