using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BotData.Data.Entity.Game;

namespace BotData.Data.Entity.BotUser
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DiscordId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string EntranceSound { get; set; }

        public List<GuessGame> GuessGames { get; set; }
        public List<GuessGameAttempt> GuessGameAttempts { get; set; }
    }
}
