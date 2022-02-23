using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FungiJournal.Domain.Models
{
    public class Fungi
    {
        [Required] [Key]
        public int FungiId { get; set; }
        public string? Name { get; set; }
        public string? LatinName { get; set; }
        public bool? IsToxic { get; set; }
    }
}
