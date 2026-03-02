using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public  class DVOStyvoidAndEmpInfo:DVOBase
    {

        #region Private Varaibles
        //styvoide
        private int _Rowid;
        private int _pay_doc_no;
        private string _void_date ;
        private string _sick_accr;
        private string  _vac_accr;
        private int _doc_no;
        private string _ok_to_post;
        //Employee Info
        private string _ref_code;
        private string  _pay_date;
        private string  _post_date;
        private int _postno;
        private int _checkNo;

        //Process_PayEmployee
        private string _empl_code;

        //MasterEmployee
        private string _first_name;
        private string _middle_name;
        private string _last_name;
        #endregion

        #region Constructor
        public  DVOStyvoidAndEmpInfo()
        {

             _Rowid = 0;
            _pay_doc_no = 0;
            _void_date = string.Empty;// Convert.ToDateTime(null);
            _sick_accr = string.Empty;
            _vac_accr = string.Empty;
            _doc_no = 0;
            _ok_to_post = string.Empty;
            _ref_code = string.Empty;
            _pay_date = string.Empty;// Convert.ToDateTime(null);
            _post_date = string.Empty;// Convert.ToDateTime(null);
            _postno = 0;
            _checkNo = 0;
              //Process_PayEmployee
            _empl_code=string.Empty;
           //MasterEmployee
           _first_name=string.Empty;
           _middle_name=string.Empty;
           _last_name=string.Empty;
        }
        #endregion

        #region Properties

        public int Rowid
        {
            get { return _Rowid  ; }
            set { _Rowid  = value; }
        }
        public int pay_doc_no
        {
            get { return _pay_doc_no; }
            set { _pay_doc_no = value; }
        }
        public string  void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
        public string sick_accr 
        {
            get { return _sick_accr; }
            set { _sick_accr = value; }
        }
        public string vac_accr
        {
            get { return _vac_accr; }
            set { _vac_accr = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string ref_code
        {
            get { return _ref_code; }
            set { _ref_code = value; }
        }
        public string  pay_date
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }
        public string   post_date
        {
            get { return _post_date; }
            set { _post_date = value; }
        }
        public int  PostNo
        {
            get { return _postno; }
            set { _postno = value; }
        }
        public int checkNo
        {
            get { return _checkNo; }
            set { _checkNo = value; }
        }
          public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }

           public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

         public string middle_name
        {
            get { return _middle_name; }
            set { _middle_name = value; }
        }
          public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

         


        #endregion

        #region StoreProcedures
        //Added by Sunil Pahwa    
        public string FIND_POST_CHK_STXTRANR
        {
            get { return "uspPostedChkGet"; }
        }     
        public string FIND_POST_CHK_STXCKRGD
        {
            get { return "usppstxckrgdChkGet"; }//usppstxckrgdchkget
        }      
        public string FIND_POST_CHK_STYPARE_DETAIL1
        {
            get { return "uspPsdChkDtlGet"; }
        }     
        public string FIND_POST_CHK_STYTRANR_DETAIL2
        {
            get { return "uspPstytranrDtlGet"; }
        }
        public string FIND_POST_CHK_STXTRANR_DETAIL3
        {
            get { return "uspPstxtranrDtlGet"; }
        }

        //Added by Sarvjeet Verma On 04/03/2009
        public string GET_VOIDCHECKLIST
        {
            get { return "usppyvoidckget"; }
        }
        public string GET_PY_ACTI
        {
            get { return "usppyentrget"; }
        }
        public string GET_GL_ACTI
        {
            get { return "uspglentrget"; }
        }
        public string GET_LINE_NO
        {
            get { return "usppyvoidcklineno"; }
        }
        public string GET_SickAndVacCode
        {
            get { return "uspvoidcksicvacget"; }
        }
        public string UPD_EMPLR_SICK_U
        {
            get { return "uspemplrSickupd"; }
        }
        public string UPD_EMPLR_LSTPAYDATE
        {
            get { return "usplrlstpydtk"; }
        }
        public string UPD_EMPLR_VAC_U
        {
            get { return "uspemplrVackupd"; }
        }
        public string UPD_TIMME
        {
            get { return "uspcard_numupd"; }
        }
        public string UPD_CKRGD
        {
            get { return "uspckrgdupd"; }
        }
        public string UPD_VOIDE
        {
            get { return "uspvoideoktp"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspvoidpostdchkins"; }
        }
        public override string UPDATE_SPNAME
        {
            get { return "uspPostedCHkupd "; }
        }
        public override string DELETE_SPNAME
        {
            get { return "uspPostedCHkDel"; }
        }
        public override string FIND_SPNAME
        {
            get { return ""; }
        }
        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "styvoide"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Added by SUnil 
          public string GET_SVOID_GET
        {
            get { return "uspvoidpstsvoidegt"; }
        }
        //***************
        
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append("select *,rowid from styvoide where ok_to_post<>'C'");
            if (Convert.ToInt32(parameters[0])>0)
               Sql.Append("and  styvoide.pay_doc_no =" + parameters[0].ToString().Trim());
           if (parameters[1].ToString() != string.Empty)
               Sql.Append(" AND void_date = '" + (parameters[1]) + "'");
            //if(Convert.ToDateTime(parameters[1])!= Convert.ToDateTime(null))
            //   Sql.Append(" and styvoide.void_date ='" + parameters[1].ToString().Trim()+"'");
            if (parameters[2].ToString().Trim() != string.Empty)
                Sql.Append(" and styvoide.sick_accr ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3].ToString().Trim() != string.Empty)
                Sql.Append(" and styvoide.vac_accr ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4])>0)
                Sql.Append(" and  styvoide.doc_no =" + parameters[4].ToString().Trim());
            if (parameters[5].ToString().Trim() != string.Empty)
                Sql.Append(" and  styvoide.ok_to_post ='" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)
            Sql.Append(" and  styvoide.rowid =" + parameters[6].ToString().Trim());

        Sql.Append(" order by styvoide.pay_doc_no ");


            return Sql.ToString();
        }
        #endregion
    }
}
