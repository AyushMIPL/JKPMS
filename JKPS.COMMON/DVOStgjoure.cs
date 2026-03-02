using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOStgjoure:DVOBase
    {
        private int _doc_no;
        private string  _doc_desc;
        private DateTime _doc_date;
        private string  _doc_src;
        private string  _auto_rev;
        private string  _file_type;
        private string  _posted;
        private string  _ok_to_post;
        private int  _batch_id;
        private string  _user_id;

        public DVOStgjoure()
        {
            _doc_no = 0;
            _doc_desc = string.Empty;
            _doc_date = Convert.ToDateTime(null);
            _auto_rev = string.Empty;
            _batch_id = 0;
            _file_type = string.Empty;
            _ok_to_post = string.Empty;
            _posted = string.Empty;
            _doc_src = string.Empty;
            _user_id = string.Empty;

        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string doc_desc
        {

            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string auto_rev
        {
            get { return _auto_rev; }
            set { _auto_rev = value; }
        }
        public string file_type
        {
            get { return _file_type; }
            set { _file_type = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string posted
        {
            get { return _posted; }
            set { _posted = value; }
        }
        public string doc_src
        {
            get { return _doc_src; }
            set { _doc_src = value; }
        }
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
         
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value;}
        } 


        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get {return  "uspstgjouredel"; }
        }

        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int UNIQUE_ID
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string INSERT_STGJOURE
        {
            get { return "uspstgjoureIns"; }
        
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
