using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstydedanlyd : DVOBase
    {
        private int _rowid;
        private int _doc_no;//int
        private string _ded_code;//char(6)
        private decimal _ded_amount;//decimal(12)
        private string _pay_date;//date
        private int _insertby;// int,
        private string _insertdate;// date,
        private string _insertmachineinfo;// char(50),
        private int _updateby;// int,
        private string _updatedate;// date,
        private string _updatemachineinfo;//
        private string _checkNo;

        //Added By Rahul jain on 20/01/2010
        private string _empl_code;
        private string _last_name;
        private string _first_name;
        private int _payroll_doc_no;


        #region Constructor
        public DVOstydedanlyd()
        {
            _rowid = 0;
            _doc_no = 0;
            _ded_code = string.Empty;
            _ded_amount = 0;
            _pay_date = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;
            _checkNo = string.Empty;

            _empl_code = string.Empty;
            _last_name = string.Empty;
            _first_name = string.Empty;
            _payroll_doc_no = 0;

        }
        #endregion Constructor

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public decimal ded_amount
        {
            get { return _ded_amount; }
            set { _ded_amount = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public string pay_date
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }

        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
        public string checkNo
        {
            get { return _checkNo; }
            set { _checkNo  = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public int payroll_doc_no
        {
            get { return _payroll_doc_no; }
            set { _payroll_doc_no = value; }
        }

        #endregion Properties

        #region Store Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspdedanalydins"; }//
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }//
        }

        public override string DELETE_SPNAME
        {
            get { return ""; } //
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
            get { return "stydedanlyd"; }
        }

        public override int UNIQUE_ID
        {
            get { return rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public  string GET_PRINT_DEDUCTION_ANALYSIS_DETAIL
        {
            get { return "uspprntdedanlysget"; }
        }
         public  string GET_PRINT_DEDUCTION_ANALYSIS_DETAIL_ON_DOC
        {
            get { return "uspprntdedalyisget"; }
        }

        

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT sbvoidr.doc_no,sbvoidr.post_doc_no,sbvoidr.tran_doc_no,sbvoidr.ok_to_post,sbvoidr.void_date,sbvoidr.rowid");
            //sql.Append(" FROM sbvoidr where 1=1 ");//AND sbvoidr.ok_to_post NOT IN ('P') ");
            //if (parameters[0] != null)
            //    if (Convert.ToInt32(parameters[0]) > 0)
            //        sql.Append(" AND  sbvoidr.post_doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            //if (parameters[1] != null)
            //    if (Convert.ToInt32(parameters[1]) > 0)
            //        sql.Append(" AND  sbvoidr.doc_no=" + Convert.ToInt32(parameters[1].ToString()));
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sbvoidr.ok_to_post) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND void_date(sbvoidr.doc_date) = date('" + parameters[3].ToString().Trim().Replace("'", "''") + "')");

            //sql.Append(" order by sbvoidr.doc_no");
            return sql.ToString();
        }

        #endregion Store Procedures
    }
}
