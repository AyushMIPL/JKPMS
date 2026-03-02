using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 28-07-2009 get or set property of stxmtaxr table.
    //Use in Multilevel Tax Module
    public class DVOMultiTaxstxmtaxr : DVOBase
    {
        private int _Rowid;
        private string _mtax_code;
        private decimal _mtax_rate;
        private int _ar_acct_no;
        private int _ar_disc_acct_no;
        private int _ap_acct_no;
        private int _ap_disc_acct_no;
        private string _mtax_desc;
        private string _mtax_country;
        private string _mtax_st_prvc;
        private string _department;
        private string _include_tax;

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;


        public DVOMultiTaxstxmtaxr()
        {
            _Rowid = 0;
            _mtax_code = string.Empty;
            _mtax_rate = 0.0M;
            _ar_acct_no = 0;
            _ar_disc_acct_no = 0;
            _ap_acct_no = 0;
            _ap_disc_acct_no = 0;
            _mtax_desc = string.Empty;
            _mtax_country = string.Empty;
            _mtax_st_prvc = string.Empty;
            _department = string.Empty;
            _include_tax = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }


        #region Public Property

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string mtax_code
        {
            get { return _mtax_code; }
            set { _mtax_code = value; }
        }
        public decimal mtax_rate
        {
            get { return _mtax_rate; }
            set { _mtax_rate = value; }
        }
        public int ar_acct_no
        {
            get { return _ar_acct_no; }
            set { _ar_acct_no = value; }
        }
        public int ar_disc_acct_no
        {
            get { return _ar_disc_acct_no; }
            set { _ar_disc_acct_no = value; }
        }
        public int ap_acct_no
        {
            get { return _ap_acct_no; }
            set { _ap_acct_no = value; }
        }
        public int ap_disc_acct_no
        {
            get { return _ap_disc_acct_no; }
            set { _ap_disc_acct_no = value; }
        }
        public string mtax_desc
        {
            get { return _mtax_desc; }
            set { _mtax_desc = value; }
        }
         public string mtax_country
        {
            get { return _mtax_country; }
            set { _mtax_country = value; }
        }
         public string mtax_st_prvc
        {
            get { return _mtax_st_prvc; }
            set { _mtax_st_prvc = value; }
        }
         public string department
        {
            get { return _department; }
            set { _department = value; }
        }
         public string include_tax
        {
            get { return _include_tax; }
            set { _include_tax = value; }
        }
        
        ////Properties used for only SQL Server

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
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        #endregion

        #region Stored Procedure

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
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
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stxmtaxr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print MultiLevel Tax Codes
        /// Created By : Rahul
        /// Created Date : 28/07/09
        /// </summary>
        public string FINDQUERY_MULTILEVELTAX_CODES(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT stxmtaxr.ap_acct_no,stxmtaxr.ap_disc_acct_no,stxmtaxr.ar_acct_no, ");
            sql.Append(" stxmtaxr.ar_disc_acct_no,stxmtaxr.mtax_code,stxmtaxr.mtax_country, ");
            sql.Append(" stxmtaxr.mtax_desc,stxmtaxr.mtax_rate,stxmtaxr.mtax_st_prvc, ");
            sql.Append(" s1.acct_desc ap_acct_desc,s1.keyvalue ap_keyvalue,s1.acct_type ap_acct_type,s2.acct_desc ap_dist_acct_desc,s2.keyvalue ap_dist_keyvalue,s2.acct_type ap_dist_acct_type, ");
            sql.Append(" s3.acct_desc ar_acct_desc,s3.keyvalue ar_keyvalue,s3.acct_type ar_acct_type,s4.acct_desc ar_dist_acct_desc,s4.keyvalue ar_dist_keyvalue,s4.acct_type ar_dist_acct_type ");
            sql.Append(" FROM  stxmtaxr ,outer PayrollGLAccounts s1,outer PayrollGLAccounts s2,outer PayrollGLAccounts s3 ,outer PayrollGLAccounts s4 ");
            sql.Append(" WHERE stxmtaxr.ap_acct_no=s1.acct_no  ");
            sql.Append(" and stxmtaxr.ap_disc_acct_no=s2.acct_no  ");
            sql.Append(" and stxmtaxr.ar_acct_no=s3.acct_no  ");
            sql.Append(" and stxmtaxr.ar_disc_acct_no=s4.acct_no  ");
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print MultiLevel Tax Groups
        /// Created By : Rahul
        /// Created Date : 28/07/09
        /// </summary>
        public string FINDQUERY_MULTILEVELTAX_GROUPS(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT stxmtaxr.mtax_desc, stxmtaxr.mtax_rate, stxmtxgd.cumulative, ");
            sql.Append(" stxmtxgd.line_no, stxmtxgd.mtax_code, stxmtxgr.mtaxg_code,  ");
            sql.Append(" stxmtxgr.mtaxg_desc ");
            sql.Append(" FROM stxmtxgr, stxmtxgd, stxmtaxr ");
            sql.Append(" WHERE stxmtxgr.mtaxg_code = stxmtxgd.mtaxg_code ");
            sql.Append(" and stxmtaxr.mtax_code = stxmtxgd.mtax_code ");
            sql.Append(" ORDER BY stxmtxgr.mtaxg_code, stxmtxgd.line_no ");
            return sql.ToString();
        }
        #endregion Stored Procedures
    }
}
