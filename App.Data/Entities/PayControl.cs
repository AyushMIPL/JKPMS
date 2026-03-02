using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PayControl : BaseEntity
  {
    //public int PayControl_ID { get; set; }
    public string post_gl { get; set; }
    public string ein_number { get; set; }
    public string state_number { get; set; }
    public string fedtax_code { get; set; }
    public string fica_code { get; set; }
    public string medicare_code { get; set; }
    public string statax_code { get; set; }
    public string loctax_code { get; set; }
    public string futa_code { get; set; }
    public string fica_ob_code { get; set; }
    public string medicare_ob_code { get; set; }
    public string eic_code { get; set; }
    public string exp_acct { get; set; }
    public string liab_acct { get; set; }
    public string cash_acct { get; set; }
    public string mmedia_file { get; set; }
    public string mmedia_command { get; set; }
    public Nullable<long> py_doc_no { get; set; }
    public Nullable<long> py_post_no { get; set; }
    public Nullable<long> immed_dest_dfi { get; set; }
    public Nullable<short> immed_chk_digit { get; set; }
    public string immed_dest_name { get; set; }
    public string co_bank_acct_no { get; set; }
    public string offset_debit { get; set; }
    public string set_up { get; set; }
    public string suta_code { get; set; }

  }
}
