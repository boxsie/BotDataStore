using System.ComponentModel.DataAnnotations;

namespace BotData.Data.Models
{
    public class UserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long DiscordId { get; set; }
    }
}