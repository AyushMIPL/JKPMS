using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class ReportText
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public string Field { get; set; }
        [Required]
        public string Signature { get; set; }
    }
}
