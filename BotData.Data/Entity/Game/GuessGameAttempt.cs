using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using BotData.Data.Entity.BotUser;

namespace BotData.Data.Entity.Game
{
    public class GuessGameAttempt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Attempt { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public long DiscordId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public GuessGame Game { get; set; }
        public User User { get; set; }
    }
}
