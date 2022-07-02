using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FungiJournal.Domain.Models
{
    public record Fungi
    {
        [Required]
        [Key]
        public int FungiId { get; set; }
        public string? CommonName { get; set; }
        public string? LatinName { get; set; }
        public bool? IsToxic { get; set; }
        public string? Occurrence { get; set; }
        public string? Season { get; set; }
        public int? FoodValue { get; set; }
    }
}
