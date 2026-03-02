using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //***********Implemented By: Sunil Pahwa****************
    public class DVOFlxview:DVOBase 
    {
        private int      _rowid;
        private string   _src_type;
        private string   _src_key;
        private string   _src_desc;
        private decimal  _src_num_desc;
        private string   _src_char_desc;
        private int      _src_acct_no;

        private string _item_class;
         
        public DVOFlxview()
        {
            _rowid = 0;
            _src_type = string.Empty;
            _src_key = string.Empty;
            _src_desc = string.Empty;
           _src_num_desc = 0;
            _src_char_desc = string.Empty;
            _src_acct_no = 0;
            _item_class = string.Empty;


        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string src_type
        {
            get { return _src_type; }
            set { _src_type = value; }
        }

        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }
        public string src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }
        public decimal  src_num_desc
        {
            get { return _src_num_desc; }
            set { _src_num_desc = value; }
        }

        public string   src_char_desc
        {
            get { return _src_char_desc ; }
            set {_src_char_desc= value; }
        }

        public int src_acct_no
        {
            get { return _src_acct_no; }
            set { _src_acct_no = value; }
        }


        public string item_class
        {
            get {return  _item_class; }
            set { _item_class = value; }
        }
       
       
         #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return ""; }
        }
        
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
            get { return "USP_FlxViewGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_FlxViewGetAll"; }
        }


        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid p_rowid,src_type p_src_type,src_key p_src_key,src_desc p_src_desc,");
            sql.Append(" src_num_desc p_src_num_desc, src_char_desc p_src_char_desc,src_acct_no p_src_acct_no");
            sql.Append(" FROM stxinfor s ");
            sql.Append(" WHERE 1=1");
            //src_num_desc p_src_num_desc,
            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND rowid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(src_type) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(src_key) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");//src_key
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(src_desc) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");//src_desc
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    if (Convert.ToInt32(parameters[4].ToString()) > 0)
                        sql.Append(" AND  src_num_desc=" + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(src_char_desc) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");//src_char_desc
            if (Convert.ToInt32(parameters[6]) > 0)//rowid
                sql.Append(" AND src_acct_no = " + parameters[6].ToString());
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }

    
}
