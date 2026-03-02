using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOsbacctstatus : DVOBase
    {
        //Written By Rahul Jain for getting account Status and Join Type 
        private int _status_id;
        private string _status_name;
        private int _join_id;
        private string _join_name;

        public DVOsbacctstatus()
        {
            _status_id =0;
            _status_name = string.Empty;
            _join_id = 0;
            _join_name = string.Empty;
        }

        public int status_id
        {
            get { return _status_id; }
            set { _status_id = value; }
        }
        public string status_name
        {
            get { return _status_name; }
            set { _status_name = value; }
        }
        public int join_id
        {
            get { return _join_id; }
            set { _join_id = value; }
        }
        public string join_name
        {
            get { return _join_name; }
            set { _join_name = value; }
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
            get { return ""; }
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

          public string GET_JOIN_TYPE
        {
            get { return "uspsbjoinget"; }
        }
        public override string ALL_SPNAME
        {
            get { return "uspsbjoinget"; }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        { 
        System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT status_id,status_name from sbacctstatus ");
            sql.Append("where 1=1 ");
            return sql.ToString();
        }
    }
}
