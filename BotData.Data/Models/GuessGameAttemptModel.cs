using System.ComponentModel.DataAnnotations;

namespace BotData.Data.Models
{
    public class GuessGameAttemptModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public long DiscordId { get; set; }

        [Required]
        public string Attempt { get; set; }

        [Required]
        public bool Correct { get; set; }
    }
}