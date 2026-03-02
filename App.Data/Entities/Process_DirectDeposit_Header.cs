using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class Process_DirectDeposit_Header
  {
    [System.ComponentModel.DataAnnotations.Key]
    public int doc_no { get; set; }
    public string company_name { get; set; }
    public string entry_desc { get; set; }
    public int dfi_immed { get; set; }
    public int svc_class { get; set; }
    public int batch_no { get; set; }
    public DateTime batch_date { get; set; }
    public DateTime create_date { get; set; }
    public string file_id { get; set; }
    public string used { get; set; }
    public string bank_code { get; set; }
    public bool IsUpload { get; set; }

    public string UploadedBy { get; set; }
    public DateTime? UploadedOn { get; set; }
    public bool IsDownload { get; set; }
    public string DownloadedBy { get; set; }
    public DateTime? DownloadedOn { get; set; }

  }
}
