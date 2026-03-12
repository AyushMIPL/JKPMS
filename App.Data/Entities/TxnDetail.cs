using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("txnDetail")]
    public class TxnDetail
    {
        [Key]
        public int DetailId { get; set; }

        [ForeignKey("Header")]
        public int HeaderId { get; set; }

        [Column("Application Reference No#")]
        public string ApplicationReferenceNo { get; set; }

        [Column("Application Reference No#1")]
        public string ApplicationReferenceNo1 { get; set; }

        [Column("Department")]
        public string Department { get; set; }

        [Column("Department Account No#")]
        public string DepartmentAccountNo { get; set; }

        [Column("Amount")]
        public double? Amount { get; set; }

        [Column("DateText")]
        public string DateText { get; set; }

        [Column("Department Bank Name")]
        public string DepartmentBankName { get; set; }

        [Column("Department Bank IFSC")]
        public string DepartmentBankIFSC { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("IFSC")]
        public string IFSC { get; set; }

        [Column("Account No#")]
        public string AccountNo { get; set; }

        [Column("Scheme")]
        public string Scheme { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("TransactionReference")]
        public string TransactionReference { get; set; }

        [Column("TransactionDate")]
        public string TransactionDate { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        public virtual TxnHeader Header { get; set; }
    }
}
