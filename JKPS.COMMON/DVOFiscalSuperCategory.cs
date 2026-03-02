using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOFiscalSuperCategory : DVOBase 
    {
        private int _sprcatid ;// serial 
        private int _sfcatid  ;//integer 
        private string _sprcatdesc ;//char(100) 
        private int _sprcatprintodr ;//integer  
        private int _insertby;//integer 
        private string _insertmachineinfo ;//char(50) 
        private string _insertdate ;//datetime year to fraction(3)
        private int _updateby   ;//integer 
        private string _updatemachineinfo;//char(50) 
        private string _updatedate ;//datetime year to fraction(3)
        private int _rowid;
        public DVOFiscalSuperCategory()
        {
            _sprcatid = 0;
            _sfcatid = 0;
            _sprcatdesc = string.Empty;
            _sprcatprintodr = 0;
            _insertby = 0;
            _insertmachineinfo = string.Empty;
            _insertdate = "01/01/1900";
            _updateby = 0;
            _updatemachineinfo = string.Empty;
            _updatedate = "01/01/1900";
            _rowid = 0;
        }

        public int sprcatid
        {
            get { return _sprcatid; }
            set { _sprcatid = value; }
        }

        public int sfcatid
        {
            get { return _sfcatid; }
            set { _sfcatid = value; }
        }
        public string sprcatdesc
        {
            get { return _sprcatdesc; }
            set { _sprcatdesc = value; }
        }
        public int sprcatprintodr
        {
            get { return _sprcatprintodr; }
            set { _sprcatprintodr = value; }
        }

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }

        public string insertmachineinfo
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
            set { _rowid  = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspfissupercatins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspfissupercatupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspfissupercatdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsupcatgetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "fissprcat"; }//fissfcat
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }
        public string Find_next_order
        {
            get { return " uspfissprcatodr"; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,sfcatid,sprcatid,sprcatdesc,sprcatprintodr from fissprcat Where 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append("AND rowid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append("AND sfcatid=" + Convert.ToInt32(parameters[1]));

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sprcatdesc)  LIKE '%" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) != 0)
                    sql.Append("AND sprcatprintodr=" + Convert.ToInt32(parameters[3]));

            return sql.ToString();

        }//

        public string IS_DUPLICATE_SUPER_CAT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select COUNT(*) from fissprcat where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(sprcatdesc) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion Stored-Procedures

        

    }
}
