using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("SftpResponseHistory")]
    public class SftpResponseHistory : BaseEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; } // "Disbursement" or "Validation"
        public DateTime ProcessDate { get; set; }
        public string Status { get; set; } // "Success", "Failed", "Partial"
        public string Remarks { get; set; }
        public string FilePath { get; set; }
        public int? RecordCount { get; set; }
    }
}
