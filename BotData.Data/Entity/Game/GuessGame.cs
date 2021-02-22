using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using BotData.Data.Entity.BotUser;

namespace BotData.Data.Entity.Game
{
    [Table("GuessGame")]
    public class GuessGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] 
        public string GameName { get; set; }
        
        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public long DiscordId { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        [Required]
        public DateTime FinishedOn { get; set; }

        public List<GuessGameAttempt> Attempts { get; set; }
        public User User { get; set; }
    }
}
