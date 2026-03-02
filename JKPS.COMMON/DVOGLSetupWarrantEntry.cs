using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLSetupWarrantEntry:DVOBase
    {
        private int _rowid;
        private string _type;
        private string _desc;
        private string _mustbalance;
        private string _budapprov;
        private string _fullkeyreqd;
        private string _startendreqd;
        private string _dollarsorpercnt;       
       

        #region Constructor

        public DVOGLSetupWarrantEntry()
        {
            _rowid = 0;
            _type=string.Empty;
            _desc=string.Empty;
            _mustbalance=string.Empty;
            _budapprov=string.Empty;
            _fullkeyreqd=string.Empty;
            _startendreqd=string.Empty;
            _dollarsorpercnt=string.Empty;    
        }

        #endregion Constructor

        #region Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        public string mustbalance
        {
            get { return _mustbalance; }
            set { _mustbalance = value; }
        }
        public string budapprov
        {
            get { return _budapprov; }
            set { _budapprov = value; }
        }
        public string fullkeyreqd
        {
            get { return _fullkeyreqd; }
            set { _fullkeyreqd = value; }
        }
        public string startendreqd
        {
            get { return _startendreqd; }
            set { _startendreqd = value; }
        }
        public string dollarsorpercnt
        {
            get { return _dollarsorpercnt; }
            set { _dollarsorpercnt = value; }
        }
        

        #endregion Properties

        #region Stored-Procedures

      

        public override string INSERT_SPNAME
        {
            get { return "uspGLSupWrntEntIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLSupWrntEntUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLSupWrntEntDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLSupWrntEntGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspGLSupWrntGelAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "inbbdocr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,type,desc,mustbalance,budapprov,fullkeyreqd,startendreqd,dollarsorpercnt from inbbdocr where 1=1");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != string.Empty)//type
                sql.Append(" and type = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != string.Empty)//desc
                sql.Append(" and desc LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != string.Empty)//mustbalance
                sql.Append(" and mustbalance = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != string.Empty)//budapprov
                sql.Append(" and budapprov = '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != string.Empty)//fullkeyreqd
                sql.Append(" and fullkeyreqd = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != string.Empty)//startendreqd
                sql.Append(" and startendreqd = '" + parameters[5].ToString().Replace("'", "''") + "'");
            if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != string.Empty)//dollarsorpercnt
                sql.Append(" and dollarsorpercnt = '" + parameters[6].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)//rowid
                sql.Append("and rowid =" + parameters[7].ToString());           

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}

