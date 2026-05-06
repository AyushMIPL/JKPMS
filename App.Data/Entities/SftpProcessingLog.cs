using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("SftpProcessingLogs")]
    public class SftpProcessingLog : BaseEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int? RowNumber { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
