using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class PensionFileUploadHistory : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; } // Pending, Sent, Failed
        public string Remarks { get; set; }
        public string Region { get; set; }
        public string Period { get; set; }
        public string UploadType { get; set; } // Disbursement, Validation
        public int? DistrictId { get; set; }
    }
}
