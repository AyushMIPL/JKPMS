using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("txnHeader")]
    public class TxnHeader
    {
        [Key]
        public int HeaderId { get; set; }

        [Column("SourceTable")]
        public string SourceTable { get; set; }

        [Column("TxnDate")]
        public DateTime? TxnDate { get; set; }

        [Column("ImportedOn")]
        public DateTime? ImportedOn { get; set; }

        public virtual ICollection<TxnDetail> Details { get; set; }
    }
}
