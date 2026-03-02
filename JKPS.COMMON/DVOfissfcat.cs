using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOfissfcat:DVOBase 
    {
        private int _sfcatid;
        private string _sfcatdesc;// char(100),
        private int _sfcatprintodr;// intl
        private int _insertby;// int,
        private string _insertmachineinfo;// char(50),
        private string _insertdate;// datetime year to fraction(3),
        private int _updateby; //int,
        private string _updatemachineinfo;// char(50),
        private string _updatedate; //datetime year to fraction(3)
        private int _rowid;

        public DVOfissfcat()
        {
            _sfcatid = 0;
            _sfcatdesc = string.Empty;// char(100),
            _sfcatprintodr = 0;// intl
            _insertby = 0;// int,
            _insertmachineinfo = string.Empty;// char(50),
            _insertdate = "01/01/1900";// string.Empty;// datetime year to fraction(3),
            _updateby = 0; //int,
            _updatemachineinfo = string.Empty;// char(50),
            _updatedate = "01/01/1900";// string.Empty; //datetime year to fraction(3)
            _rowid = 0;

        }
        public int sfcatid
        {
            get  {   return _sfcatid;  }
            set {  _sfcatid=value;   }

        }
        public string sfcatdesc
        {
            get { return _sfcatdesc; }
            set { _sfcatdesc = value; }
        }
        public int sfcatprintodr
        {
            get { return _sfcatprintodr; }
            set { _sfcatprintodr = value; }
        }

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }

        public string  insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
       public int rowid
       {
           get { return _rowid; }
           set { _rowid = value; }

       }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspfissupfnecatins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspfissupfnecatupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspfissupfnecatdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsupfnegetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "fissfcat"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

       public string Find_next_order
       {
           get { return "uspfissprfcatodr"; }
       }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,sfcatid,sfcatdesc,sfcatprintodr from fissfcat where 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append("AND rowid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sfcatdesc)  LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append("AND sfcatprintodr=" + Convert.ToInt32(parameters[2]));
        
            return sql.ToString();

        }

        public string IS_DUPLICATE_SUPERFINE_CAT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select COUNT(*) from fissfcat where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(sfcatdesc) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }

        #endregion Stored-Procedures

    }
   
}
