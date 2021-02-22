using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BotData.Data.Entity.GeoSniff
{
    [Table("GeoSniffLocation")]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [NotNull]
        public string Country { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }
        
        public double Lat { get; set; }

        public double Long { get; set; }

        public double Radius { get; set; }
    }
}
