using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstrcasd : DVOBase
    {
        private int _rowid;// in
        private int _id;// int not null ,
        private int _batch_id;// integer,
        private string _check_no;// char(10),
        private string   _check_date;// date,
        private string _bank_name;//  Char ,
        private decimal _checkamount;// Decimal(18,2),
        private int _insertby;// integer,
        private string _insertdate;// datetime year to fraction(3),
        private string _insertmachineinfo;// char(50),
        private int _updateby;// integer,
        private string _updatedate;// datetime year to fraction(3),
        private string _updatemachineinfo;// char(50)

        public DVOstrcasd()
        {
            _id = 0;
            _batch_id = 0;
            _check_no = string.Empty;
            _check_date = "01/01/1900";
            _bank_name = string.Empty;
            _checkamount = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";
            _updatemachineinfo = string.Empty;
            _rowid = 0;
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public string  check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public string  check_date
        {
            get { return _check_date; }
            set { _check_date = value; }
        }
        public string bank_name
        {
            get { return _bank_name; }
            set { _bank_name = value; }
        }
        public decimal  checkamount
        {
            get { return _checkamount; }
            set { _checkamount = value; }
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



        public override string INSERT_SPNAME
        {
            get { return "uspbatchcashdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspbatchcashdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbatchcashdtldel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspbatchcashdtlget"; }
        }


        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
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


        //public string INSERT_BATCH_CASH_DETAIL
        //{
        //    get { return "uspbatchCashdtlins"; }
        //}

        //public string GET_BATCH_CASH_DETAIL
        //{
        //    get { return "uspbatchcashdtlget"; }
        //}
        //public string UPD_BATCH_CASH_DETAIL
        //{
        //    get { return "uspbatchcashdtlupd"; }
        //}



        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }



    }

}


