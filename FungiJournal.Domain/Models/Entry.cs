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
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }

        [ForeignKey("FungiId")]
        public int? FungiId { get; set; }
        public Fungi? Fungi { get; set; }
        public string? Description { get; set; }
    }
}
