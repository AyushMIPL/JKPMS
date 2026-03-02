using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{


    public class DVOAdjustInventorystiadjme : DVOBase
    {
       private int _rowid;
       private  int     _doc_no ;
       private  string  _doc_date ;
       private  string  _adj_no;
       private  string _adj_desc;
       private  int    _adj_acct;
       private  string _ok_post;
       private string  _keyvalue;
      //  private int _acct_no;
        private string _acct_desc;
        private string _acct_type;


        private int _insertby;
        private DateTime _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private DateTime _updatedate;
        private string _updatemachineinfo;


        public DVOAdjustInventorystiadjme()
        {
            _rowid = 0;
             _doc_no=0;
             _doc_date=string.Empty;
             _adj_no=string.Empty;
             _adj_desc=string.Empty;
             _adj_acct=0;
             _ok_post=string.Empty;
             _keyvalue=string.Empty;
    
            _acct_desc=string.Empty;
            _acct_type = string.Empty;


            _insertby = 0;
            _insertdate = DateTime.Now;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = DateTime.Now;
            _updatemachineinfo = string.Empty;

        }

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

        public string  doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string adj_no
        {
            get { return _adj_no; }
            set { _adj_no = value; }
        }

        public string adj_desc
        {
            get { return _adj_desc; }
            set { _adj_desc = value; }
        }
        public int  adj_acct
        {
            get { return _adj_acct; }
            set { _adj_acct = value; }
        }
        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }
        

          public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        
          public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }


        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public DateTime insertdate
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
        public DateTime updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
    



       #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspinvadjmentins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvadjmentupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvadjmentDel"; }
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
            get { return "stiadjme"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stiadjme.rowid,stiadjme.doc_no,stiadjme.doc_date,stiadjme.adj_no,");
            sql.Append(" stiadjme.adj_desc,stiadjme.adj_acct,stiadjme.ok_post,");
            sql.Append(" PayrollGLAccounts.acct_no,PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue ");
            sql.Append(" from stiadjme,outer(PayrollGLAccounts) where stiadjme.adj_acct=PayrollGLAccounts.acct_no");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND stiadjme.doc_no = " + parameters[0].ToString());

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stiadjme.adj_no) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");



            if (parameters[2].ToString() != string.Empty && Convert.ToDateTime(parameters[2].ToString()) != Convert.ToDateTime("01/01/1900"))
                //if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
                sql.Append(" and stiadjme.doc_date >='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

         

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stiadjme.adj_desc) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            
            //if (Convert.ToInt32(parameters[4]) > 0)
            //    sql.Append(" AND stiadjme.adj_acct = " + parameters[4].ToString());


            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stiadjme.ok_post '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stiadjme.rowid = " + parameters[5].ToString());


            return sql.ToString();
        }

        #endregion Stored-Procedures


        
    }

   
}
