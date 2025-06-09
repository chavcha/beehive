using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("MessageFileRecs")]
    [PrimaryKey("Id")]
    public class MessageFileRecord
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public MessageRecord Message { get; set; } = null!;

        [Required]
        [ForeignKey("File")]
        public Guid FileId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public FileRecord File { get; set; } = null!;
    }
}
