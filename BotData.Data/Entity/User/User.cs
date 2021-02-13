using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BotData.Data.Entity.User
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public long DiscordId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string EntranceSound { get; set; }
    }
}
