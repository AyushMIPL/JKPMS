using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("MasterProcessKeys")]
    public class MasterProcessKey : BaseEntity
    {
        [StringLength(200)]
        [Display(Name = "Process Key")]
        public string ProcessKeyName { get; set; }
    }
}
