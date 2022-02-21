using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.Domain.Models
{
    public class Entry
    {
        public int EntryId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? FungiId { get; set; }
        public string? Description { get; set; }
    }
}
