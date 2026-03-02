using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim*************************************************** Developed By*** DevelopmentDate
    ///1.) DVO To get the Control Data of stucntrc table for Purchasing     Rahul Jain       05/05/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOPurchasingDefault : DVOBase
    {
        #region Private variables
        private string _exempt_tax_code;
        private string _frght_tax_code;
        private string _misc_tax_code;
        private string _mtaxg_code;
        private string _gross_entry;
        private string _entry_by_line;
        private string _whse_shipto;
        private string _whse_billto;
        private string _po_type;
        private int _retention_days;
        private int _eta_days;
        private string _cm_reason;
        private string _dm_reason;
        private string _terms_code;
        private int _ap_acct_no;
        private int _disc_acct_no;
        private int _frght_acct_no;
        private int _misc_acct_no;
        private int _inv_acct_no;
        private int _adj_acct_no;
        private int _prepaid_acct_no;
        private int _supp_acct_no;
        private int _cap_acct_no;
        private int _non_acct_no;
        private int _diff_acct_no;
        private int _cash_acct_no;
        private string _buyer_code;
        private decimal _price_tolerance;
        private string _line_type;
        private string _use_department;
        private int _req_doc_no;
        private int _po_doc_no;
        private int _rec_doc_no;
        private int _inv_doc_no;
        private int _req_post_no;
        private int _rec_post_no;
        private int _inv_post_no;
        private string _merge_requisitions;
        private string _ship_via;
        private string _fob_point;
        private string _print_notes;
        private string _use_batch_rec;
        private string _use_batch_inv;
        private string _use_approv_post;
        private string _approval_code;
        private int _cpu_acct_no;
        private string _rcttogl_stk;
        private string _rcttogl_nstk;

        // For get Keyvalue and account type
        private string _ap_keyvalue ;
        private string _diff_keyvalue;
        private string _inv_keyvalue;
        private string _misc_keyvalue;
        private string _disc_keyvalue;
        private string _supp_keyvalue;
        private string _frght_keyvalue;
        private string _adj_keyvalue;
        private string _non_keyvalue;
        private string _cap_keyvalue;
        private string _cash_keyvalue;
        private string _cpu_keyvalue;

        private string _ap_acct_type;
        private string _diff_acct_type;
        private string _inv_acct_type;
        private string _misc_acct_type;
        private string _disc_acct_type;
        private string _supp_acct_type;
        private string _frght_acct_type;
        private string _adj_acct_type;
        private string _non_acct_type;
        private string _cap_acct_type;
        private string _cash_acct_type;
        private string _cpu_acct_type;


        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary
        private int _RowID;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        #endregion Private variables

        #region Constructor
        public DVOPurchasingDefault()
        { 
            _exempt_tax_code="";
            _frght_tax_code = "";
            _misc_tax_code="";
            _mtaxg_code="";
            _gross_entry="";
            _entry_by_line="";
            _whse_shipto="";
            _whse_billto="";
            _po_type="";
            _retention_days=0;
            _eta_days=0;
            _cm_reason="";
            _dm_reason="";
            _terms_code="";
            _ap_acct_no=0;
            _disc_acct_no=0;
            _frght_acct_no=0;
            _misc_acct_no=0;
            _inv_acct_no=0;
            _adj_acct_no=0;
            _prepaid_acct_no=0;
            _supp_acct_no=0;
            _cap_acct_no=0;
            _non_acct_no=0;
            _diff_acct_no=0;
            _cash_acct_no=0;
            _buyer_code="";
            _price_tolerance=0.0M;
            _line_type="";
            _use_department="";
            _req_doc_no=0;
            _po_doc_no=0;
            _rec_doc_no=0;
            _inv_doc_no = 0;
            _req_post_no=0;
            _rec_post_no=0;
            _inv_post_no=0;
            _merge_requisitions="";
            _ship_via="";
            _fob_point="";
            _print_notes="";
            _use_batch_rec="";
            _use_batch_inv="";
            _use_approv_post="";
            _approval_code="";


             _ap_keyvalue="" ;
             _diff_keyvalue = "";
             _inv_keyvalue = "";
             _misc_keyvalue = "";
             _disc_keyvalue = "";
             _supp_keyvalue = "";
             _frght_keyvalue = "";
             _adj_keyvalue = "";
             _non_keyvalue = "";
             _cap_keyvalue = "";
             _cash_keyvalue = "";

             _ap_acct_type = "";
             _diff_acct_type = "";
             _inv_acct_type = "";
             _misc_acct_type = "";
             _disc_acct_type = "";
             _supp_acct_type = "";
             _frght_acct_type = "";
             _adj_acct_type = "";
             _non_acct_type = "";
             _cap_acct_type = "";
             _cash_acct_type = "";
             _rcttogl_nstk = string.Empty;
             _rcttogl_stk = string.Empty;
            /// <summary>
            /// Private variable applicable only for SQL-Server
            /// </summary
            _RowID=0;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

        }
        #endregion Constructor

        #region Properties
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string exempt_tax_code
        {
            get { return _exempt_tax_code; }
            set { _exempt_tax_code = value; }
        }
        public string frght_tax_code
        {
            get { return _frght_tax_code; }
            set { _frght_tax_code = value; }
        }
        public string misc_tax_code
        {
            get { return _misc_tax_code; }
            set { _misc_tax_code = value; }
        }
        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
        }
        public string entry_by_line
        {
            get { return _entry_by_line; }
            set { _entry_by_line = value; }
        }
        public string whse_shipto
        {
            get { return _whse_shipto; }
            set { _whse_shipto=value; }
        }
        public string whse_billto
        {
            get { return _whse_billto; }
            set { _whse_billto=value; }
        }
        public string po_type
        {
            get { return _po_type; }
            set { _po_type=value; }
        }
         public int retention_days
        {
            get { return _retention_days; }
            set { _retention_days=value; }
        }
         public int eta_days
        {
            get { return _eta_days; }
            set { _eta_days=value; }
        }
        public string cm_reason
        {
            get { return _cm_reason; }
            set { _cm_reason=value; }
        }
        public string dm_reason
        {
            get { return _dm_reason; }
            set { _dm_reason=value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code=value; }
        }
        public int ap_acct_no
        {
            get { return _ap_acct_no; }
            set { _ap_acct_no=value; }
        }
        public int disc_acct_no
        {
            get { return _disc_acct_no; }
            set { _disc_acct_no=value; }
        }
         public int frght_acct_no
        {
            get { return _frght_acct_no; }
            set { _frght_acct_no=value; }
        }
         public int misc_acct_no
        {
            get { return _misc_acct_no; }
            set { _misc_acct_no=value; }
        }
          public int inv_acct_no
        {
            get { return _inv_acct_no; }
            set { _inv_acct_no=value; }
        }
          public int adj_acct_no
        {
            get { return _adj_acct_no; }
            set { _adj_acct_no=value; }
        }
          public int prepaid_acct_no
        {
            get { return _prepaid_acct_no; }
            set { _prepaid_acct_no=value; }
        }
          public int supp_acct_no
        {
            get { return _supp_acct_no; }
            set { _supp_acct_no=value; }
        }
          public int cap_acct_no
        {
            get { return _cap_acct_no; }
            set { _cap_acct_no=value; }
        }
          public int non_acct_no
        {
            get { return _non_acct_no; }
            set { _non_acct_no=value; }
        }
           public int diff_acct_no
        {
            get { return _diff_acct_no; }
            set { _diff_acct_no=value; }
        }
           public int cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no=value; }
        }
        public string buyer_code
        {
            get { return _buyer_code; }
            set { _buyer_code=value; }
        }
         public decimal  price_tolerance
        {
            get { return _price_tolerance; }
            set { _price_tolerance=value; }
        }
         public string line_type
        {
            get { return _line_type; }
            set { _line_type=value; }
        }
          public string use_department
        {
            get { return _use_department; }
            set { _use_department=value; }
        }
          public int req_doc_no
        {
            get { return _req_doc_no; }
            set { _req_doc_no=value; }
        }
          public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no=value; }
        }
          public int rec_doc_no
        {
            get { return _rec_doc_no; }
            set { _rec_doc_no=value; }
        }
           public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no=value; }
        }
        public int req_post_no
        {
            get { return _req_post_no; }
            set { _req_post_no=value; }
        }
        public int rec_post_no
        {
            get { return _rec_post_no; }
            set { _rec_post_no=value; }
        }
        public int inv_post_no
        {
            get { return _inv_post_no; }
            set { _inv_post_no=value; }
        }
        public string merge_requisitions
        {
            get { return _merge_requisitions; }
            set { _merge_requisitions=value; }
        }
        public string ship_via
        {
            get { return _ship_via; }
            set { _ship_via=value; }
        }
        public string fob_point
        {
            get { return _fob_point; }
            set { _fob_point=value; }
        }
        public string print_notes
        {
            get { return _print_notes; }
            set { _print_notes=value; }
        }
        public string use_batch_rec
        {
            get { return _use_batch_rec; }
            set { _use_batch_rec=value; }
        }
        public string use_batch_inv
        {
            get { return _use_batch_inv; }
            set { _use_batch_inv=value; }
        }
        public string use_approv_post
        {
            get { return _use_approv_post; }
            set { _use_approv_post=value; }
        }
        public string approval_code
        {
            get { return _approval_code; }
            set { _approval_code=value; }
        }
        //Properties for keyvalue and Account type 
         public string ap_keyvalue
        {
            get { return _ap_keyvalue; }
            set { _ap_keyvalue=value; }
        }
         public string diff_keyvalue
        {
            get { return _diff_keyvalue; }
            set { _diff_keyvalue=value; }
        }
         public string inv_keyvalue
        {
            get { return _inv_keyvalue; }
            set { _inv_keyvalue=value; }
        }
         public string misc_keyvalue
        {
            get { return _misc_keyvalue; }
            set { _misc_keyvalue=value; }
        }
         public string disc_keyvalue
        {
            get { return _disc_keyvalue; }
            set { _disc_keyvalue=value; }
        }
         public string supp_keyvalue
        {
            get { return _supp_keyvalue; }
            set { _supp_keyvalue=value; }
        }
          public string frght_keyvalue
        {
            get { return _frght_keyvalue; }
            set { _frght_keyvalue=value; }
        }
           public string adj_keyvalue
        {
            get { return _adj_keyvalue; }
            set { _adj_keyvalue=value; }
        }
           public string non_keyvalue
        {
            get { return _non_keyvalue; }
            set { _non_keyvalue=value; }
        }
           public string cap_keyvalue
        {
            get { return _cap_keyvalue; }
            set { _cap_keyvalue=value; }
        }
        public string cash_keyvalue
        {
            get { return _cash_keyvalue; }
            set { _cash_keyvalue=value; }
        }

        public string ap_acct_type
        {
            get { return _ap_acct_type; }
            set { _ap_acct_type=value; }
        }
        public string diff_acct_type
        {
            get { return _diff_acct_type; }
            set { _diff_acct_type=value; }
        }
          public string inv_acct_type
        {
            get { return _inv_acct_type; }
            set { _inv_acct_type=value; }
        }
          public string misc_acct_type
        {
            get { return _misc_acct_type; }
            set { _misc_acct_type=value; }
        }
          public string disc_acct_type
        {
            get { return _disc_acct_type; }
            set { _disc_acct_type=value; }
        }
          public string supp_acct_type
        {
            get { return _supp_acct_type; }
            set { _supp_acct_type=value; }
        }
          public string frght_acct_type
        {
            get { return _frght_acct_type; }
            set { _frght_acct_type=value; }
        }
          public string adj_acct_type
        {
            get { return _adj_acct_type; }
            set { _adj_acct_type=value; }
        }
         public string non_acct_type
        {
            get { return _non_acct_type; }
            set { _non_acct_type=value; }
        }
         public string cap_acct_type
        {
            get { return _cap_acct_type; }
            set { _cap_acct_type=value; }
        }
         public string cash_acct_type
        {
            get { return _cash_acct_type; }
            set { _cash_acct_type=value; }
        }

        public int cpu_acct_no
        {
            get { return _cpu_acct_no; }
            set { _cpu_acct_no = value; }
        }
        public string cpu_keyvalue
        {
            get { return _cpu_keyvalue; }
            set { _cpu_keyvalue = value; }
        }
        public string cpu_acct_type
        {
            get { return _cpu_acct_type; }
            set { _cpu_acct_type = value; }
        }
        public string rcttogl_stk
        {
            get { return _rcttogl_stk; }
            set { _rcttogl_stk = value; }
        }
        public string rcttogl_nstk
        {
            get { return _rcttogl_nstk; }
            set { _rcttogl_nstk = value; }
        }
        //Properties used for only SQL Server

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate;  }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        #endregion 


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usppurchadefins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppurchadefupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "usppurdefget"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {

            return "";
        }

        public override string TABLE_NAME
        {
            get { return "stucntrc"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string DELETE_STISERLE
        {
            get { return "uspstiserledlt"; }
        }
        //Addes bY rahul Jain using in Post receipts reports
        public string GET_BATCH_REC
        {
            get { return "uspbatchrecget"; }
        }
        #endregion Stored-Procedures

    }
}
