using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPaybleListingstpopend:DVOBase
    {
        private int _doc_no;
        public DVOPaybleListingstpopend()
        {
            _doc_no = 0;
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

        public override string INSERT_SPNAME
        {
            get {return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get {return ""; }
        }

        public override string DELETE_SPNAME
        {
            get {return ""; }
        }

        public override string FIND_SPNAME
        {
            get {return ""; }
        }

        public override string ALL_SPNAME
        {
            get {return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stpopend"; }
        }

        public override int UNIQUE_ID
        {
            get { return _doc_no; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
           return "";
        }
    }
}
