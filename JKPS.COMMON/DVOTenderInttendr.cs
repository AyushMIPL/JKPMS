using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTenderInttendr : DVOBase
    {
        private int _RowID;
        private string _tend_code;
        private string _tend_name;
        private string _address1;
        private string _address2;
        private string _contact;
        private string _phone;
        private string _fax;
        private string _class_code;

        #region Constructor

        public DVOTenderInttendr()
        {
            _RowID = 0;
            _tend_code = string.Empty;
            _tend_name = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _fax = string.Empty;
            _class_code = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string tend_name
        {
            get { return _tend_name; }
            set { _tend_name = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string class_code
        {
            get { return _class_code; }
            set { _class_code = value; }
        }

        #endregion public properties

        #region Stored-Procedures

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
            get { return "uspTendersGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspTendersGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "inttendr"; }
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


        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
