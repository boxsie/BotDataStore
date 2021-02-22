using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BotData.Data.Models
{
    public class GuessGameModel
    {
        [Required]
        public string GameName { get; set; }

        [Required]
        public long DiscordId { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }
    }
}
