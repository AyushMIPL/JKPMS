using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOFiscalMainCategory : DVOBase
    {
        private int  _mcatid ;
        private string _mcatdesc;
        private int _mcatprintodr;
        private string _insertmachineinfo;
        private string _insertdate;
        private int _insertby;
        private string _updatemachineinfo;
        private string _updatedate ;
        private int _updateby;
        private int _sfcatid ;
        private int _sprcatid;
        private int _rowid;
        public DVOFiscalMainCategory()
        {

            _mcatid = 0;
            _mcatdesc = string.Empty;
            _mcatprintodr = 0;
            _insertmachineinfo = string.Empty;
            _insertdate = "01/01/1900";
            _insertby = 0;
            _updatemachineinfo = string.Empty;
            _updatedate = "01/01/1900";
            _updateby = 0;
            _sfcatid = 0;
            _sprcatid = 0;
            _rowid = 0;
        }

        public int mcatid
        {
            get { return _mcatid; }
            set { _mcatid = value; }
        }
        public string  mcatdesc
        {
            get { return _mcatdesc; }
            set { _mcatdesc = value; }
        }
        public int mcatprintodr
        {
            get { return _mcatprintodr; }
            set { _mcatprintodr = value; }
        }
        public string  insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public string  insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
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
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
       public int sfcatid
        {
            get { return _sfcatid; }
            set { _sfcatid = value; }
        }
        public int sprcatid
        {
            get { return _sprcatid; }
            set { _sprcatid = value; }
        }
        public int rowid
        {
            get { return _rowid ; }
            set { _rowid  = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "upsfismaincatins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "upsfismaincatupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "upsfismaincatdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "fismcat"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public string Find_next_order
        {
            get { return "uspfismaincatodr"; }
        }
        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,sfcatid,sprcatid,mcatid,mcatdesc,mcatprintodr from fismcat Where 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append("AND rowid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append("AND sfcatid=" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append("AND sprcatid=" + Convert.ToInt32(parameters[2]));

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mcatdesc)  LIKE '%" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) != 0)
                    sql.Append("AND mcatprintodr=" + Convert.ToInt32(parameters[4]));

            return sql.ToString();

        }

        public string IS_DUPLICATE_MAIN_CAT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select COUNT(*) from fismcat where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(mcatdesc) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion Stored-Procedures

 


    }
}
