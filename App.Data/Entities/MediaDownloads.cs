using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace App.Data.Entities
{
    public class MediaDownloads : BaseEntity
    {
        public string FileName { get; set; }
        public bool HasDownoaded { get; set; }
        public bool IsProcessed { get; set; }
        public int MediaType { get; set; }
        public int RecordId { get; set; }
        public int TotalBeneficiary { get; set; }
        public int TotalValidated { get; set; }
        public int TotalNotvalidated { get; set; }
        public string Remark { get; set; }
    }
}
