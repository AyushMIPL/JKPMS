using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    // created By <*=======Sunil Pahwa[29/07/09]=========*>
    public class DVOstrcasr : DVOBase
    {
        private int _rowid;
        private int _id;// serial not null ,
        private int _batch_id;// integer,
        private decimal _total_cash;// decimal(18,2),
        private int _active;// smallint,
        private int _coins_1;// integer,
        private int _coins_2;// integer,
        private int _coins_5;// integer,
        private int _coins_10;// integer,
        private int _coins_25;// integer,
        private int _coins_50;// integer,
        private int _notes_1;// integer,
        private int _notes_5;// integer,
        private int _notes_10;// integer,
        private int _notes_20;// integer,
        private int _notes_50;// integer,
        private int _notes_100;// integer,
        private int _USnotes_1;// integer,
        private int _USnotes_5;// integer,
        private int _USnotes_10;// integer,
        private int _USnotes_20;// integer,
        private int _USnotes_50;// integer,
        private int _USnotes_100;// integer,
        private decimal _checktotal;// decimal(18,2),
        private int _insertby;// integer,
        private string _insertdate;// datetime year to fraction(3),
        private string _insertmachineinfo;// char(50),
        private int _updateby;// integer,
        private string _updatedate;//updatedate datetime year to fraction(3),
        private string _updatemachineinfo;// char(50)


        public DVOstrcasr()
        {
            _rowid = 0;
            _id = 0;// serial not null ,
            _batch_id = 0;// integer,
            _total_cash = 0;// decimal(18,2),
            _active = 0;// smallint,
            _coins_1 = 0;// integer,
            _coins_2 = 0;// integer,
            _coins_5 = 0;// integer,
            _coins_10 = 0;// integer,
            _coins_25 = 0;// integer,
            _coins_50 = 0;// integer,
            _notes_1 = 0;// integer,
            _notes_5 = 0;// integer,
            _notes_10 = 0;// integer,
            _notes_20 = 0;// integer,
            _notes_50 = 0;// integer,
            _notes_100 = 0;// integer,
            _USnotes_1 = 0;// integer,
            _USnotes_5 = 0;// integer,
            _USnotes_10 = 0;// integer,
            _USnotes_20 = 0;// integer,
            _USnotes_50 = 0;// integer,
            _USnotes_100 = 0;// integer,
            _checktotal = 0;// decimal(18,2),
            _insertby = 0;// integer,
            _insertdate = "01/01/1900";// datetime year to fraction(3),
            _insertmachineinfo = string.Empty;// char(50),
            _updateby = 0;// integer,
            _updatedate = "01/01/1900";//updatedate datetime year to fraction(3),
            _updatemachineinfo = string.Empty;// char(50)

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
        public decimal total_cash
        {
            get { return _total_cash; }
            set { _total_cash = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }

        public int coins_1
        {
            get { return _coins_1; }
            set { _coins_1 = value; }
        }


        public int coins_2
        {
            get { return _coins_2; }
            set { _coins_2 = value; }
        }

        public int coins_5
        {
            get { return _coins_5; }
            set { _coins_5 = value; }
        }
        public int coins_10
        {
            get { return _coins_10; }
            set { _coins_10 = value; }
        }
        public int coins_25
        {
            get { return _coins_25; }
            set { _coins_25 = value; }
        }
        public int coins_50
        {
            get { return _coins_50; }
            set { _coins_50 = value; }
        }
        public int notes_1
        {
            get { return _notes_1; }
            set { _notes_1 = value; }
        }
        public int notes_5
        {
            get { return _notes_5; }
            set { _notes_5 = value; }
        }
        public int notes_10
        {
            get { return _notes_10; }
            set { _notes_10 = value; }
        }
        public int notes_20
        {
            get { return _notes_20; }
            set { _notes_20 = value; }
        }
        public int notes_50
        {
            get { return _notes_50; }
            set { _notes_50 = value; }
        }
        public int notes_100
        {
            get { return _notes_100; }
            set { _notes_100 = value; }
        }
        public int USnotes_1
        {
            get { return _USnotes_1; }
            set { _USnotes_1 = value; }
        }
        public int USnotes_5
        {
            get { return _USnotes_5; }
            set { _USnotes_5 = value; }
        }
        public int USnotes_10
        {
            get { return _USnotes_10; }
            set { _USnotes_10 = value; }
        }
        public int USnotes_20
        {
            get { return _USnotes_20; }
            set { _USnotes_20 = value; }
        }
        public int USnotes_50
        {
            get { return _USnotes_50; }
            set { _USnotes_50 = value; }
        }
        public int USnotes_100
        {
            get { return _USnotes_100; }
            set { _USnotes_100 = value; }
        }
        public decimal checktotal
        {
            get { return _checktotal; }
            set { _checktotal = value; }
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

        #region storedProcedures


        public override string INSERT_SPNAME
        {
            get { return "uspbatchCashins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspbatchcashinfupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbatchCashdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspbatchcashinfget"; }
        }

        public string GET_BATCH_INFO
        {
            get { return "uspbchcashinfoget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "strcasr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _id; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        public string INSERT_BATCH_CASH_DETAIL
        {
            get { return "uspbatchCashdtlins"; }
        }

        public string GET_BATCH_CASH_DETAIL
        {
            get { return "uspbatchcashdtlget"; }
        }
        public string UPD_BATCH_CASH_DETAIL
        {
            get { return "uspbatchcashdtlupd"; }
        }



        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select id,batch_id,total_cash, coins_1, coins_2,coins_5,");
            sql.Append(" coins_10,coins_25 ,coins_50,notes_1,notes_5,notes_10,");
            sql.Append(" notes_20,notes_50, notes_100,USnotes_1,USnotes_5,USnotes_10,");
            sql.Append(" USnotes_20,USnotes_50, USnotes_100, checktotal,rowid from strcasr");
            sql.Append(" where active=1");

            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" and strcasr.rowid=" + parameters[0].ToString());
            if (parameters[1] != null && parameters[1].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" and strcasr.id=" + parameters[1].ToString());
            if (parameters[2] != null && parameters[2].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" and strcasr.batch_id=" + parameters[2].ToString());

            return sql.ToString();
        }
        #endregion StoredProcedures


    }
}
