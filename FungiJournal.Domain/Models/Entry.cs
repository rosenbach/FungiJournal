using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FungiJournal.Domain.Models
{
    public class Entry
    {
        [Key] [Required]
        public int EntryId { get; set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? LastModified { get; private set; }

        [ForeignKey("Fungi")]
        public int? FungiId { get; set; }

        public virtual Fungi? Fungi { get; set; }
        public string? Description { get; set; }

        public void UpdateTimestamp()
        {
            LastModified = DateTime.Now;
        }

        public void GenerateCreationDate()
        {
            CreatedAt = DateTime.Now;
            LastModified = CreatedAt;
        }
    }
}
