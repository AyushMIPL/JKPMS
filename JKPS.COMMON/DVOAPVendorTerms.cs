using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPVendorTerms : DVOBase
    {
        private string _terms_code;
        private string _terms_desc;
        private int _due_days;
        private int _disc_days;
        private decimal _disc_pct;
        private int _rowid;

        #region Constructor

        public DVOAPVendorTerms()
        {
            _rowid = 0;
            _terms_code = string.Empty;
            _terms_desc = string.Empty;
            _due_days = 0;
            _disc_days = 0;
            _disc_pct = 0;

        }

        #endregion Constructor

        #region Public Properties

        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string terms_desc
        {
            get { return _terms_desc; }
            set { _terms_desc = value; }
        }
        public int due_days
        {
            get { return _due_days; }
            set { _due_days = value; }
        }

        public int disc_days
        {
            get { return _disc_days; }
            set { _disc_days = value; }
        }
        public decimal disc_pct
        {
            get { return _disc_pct; }
            set { _disc_pct = value; }
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }


        #endregion Public Properties

        #region Stored-Procedures



        public override string INSERT_SPNAME
        {
            get { return "uspapventermsins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspapventermsupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspapventermsdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspapventermsget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspappaytvenall"; }
        }



        public override string TABLE_NAME
        {
            get { return "stptermr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public  string GET_TERMS_FOR_ORDER_ENTRY
        {
            get { return "uspordrenttermsget"; }
        }
        
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            //Modified by Rahul Jain on 5/02/2009 add Where 1=1  in query 

            sql.Append("select terms_code,terms_desc,due_days,disc_days,disc_pct,rowid  from stptermr where 1=1 ");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//terms_code
                sql.Append(" and terms_code = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//terms_desc
                sql.Append(" and terms_desc LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)//due_days
                //sql.Append(" and terms_desc = " + parameters[2].ToString()); 
                sql.Append(" and due_days = " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)//disc_days
                sql.Append(" and disc_days = " + parameters[3].ToString());
            if (Convert.ToInt32(parameters[4]) > 0)//disc_pct
                sql.Append(" and disc_pct = " + parameters[4].ToString());

            //Added by Sunil Pahwa on *********[28-01-09]************************
            if (Convert.ToInt32(parameters[5]) > 0)//rowid
                sql.Append(" and rowid = " + parameters[5].ToString());

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
