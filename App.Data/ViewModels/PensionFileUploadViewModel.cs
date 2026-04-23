using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Entities;

namespace App.Data.ViewModels
{
    public class PensionFileUploadViewModel
    {
        [Required(ErrorMessage = "Please select a file to upload.")]
        [Display(Name = "Pension File")]
        public System.Web.HttpPostedFileBase UploadedFile { get; set; }

        [Display(Name = "District")]
        public string Region { get; set; }
        public string UploadYear { get; set; }
        public string UploadMonth { get; set; }
        public string Period { get; set; }
        public string UploadType { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public List<PensionFileUploadHistoryViewModel> UploadHistory { get; set; }
    }

    public class PensionFileUploadHistoryViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Region { get; set; }
        public string Period { get; set; }
        public string UploadType { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
