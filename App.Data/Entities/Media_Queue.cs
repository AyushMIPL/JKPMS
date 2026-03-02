using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Media_Queue : BaseEntity
    {
        public int RecordId { get; set; }
        public string FilePath { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsUploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadErrors { get; set; }
        public int MediaType { get; set; }
        public int TotalBeneficiary { get; set; }
        public int TotalValidated { get; set; }
        public int TotalNotvalidated { get; set; }
        public bool IsReUploadedPermitted { get; set; }
        public int ReUploadedPermittedBy { get; set; }
        public DateTime? ReUploadedPermittedDate { get; set; }
        public string Notes { get; set; }
        public bool IsReUploaded { get; set; }
        public int ReUploadedBy { get; set; }
        public DateTime? ReUploadedDate { get; set; }
        public string Districts { get; set; }
    }
}
