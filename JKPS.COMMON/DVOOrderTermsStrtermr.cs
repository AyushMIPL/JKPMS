using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOOrderTermsStrtermr : DVOBase
    {
        private string _terms_code;
        private string _terms_desc;
        private int _due_days;
        private int _disc_days;
        private decimal _disc_pct;
        private string _terms_type;
        private int _fix_due_day;
        private int _cut_off_day;

        private int _rowid;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;
        

        public DVOOrderTermsStrtermr()
        {
            _terms_code = string.Empty;
            _terms_desc = string.Empty;
            _due_days = 0;
            _disc_days = 0;
            _disc_pct = 0.0M;
            _terms_type = string.Empty;
            _fix_due_day = 0;
            _cut_off_day = 0;

          _rowid = 0;
          _InsertMachineInfo = "App";
          _InsertDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
          _InsertBy = -1;
          _UpdateMachineInfo = "App";
          _UpdateDate = Convert.ToDateTime("01/01/1900");// DateTime.Now;
          _UpdateBy = -1;
        }

        #region Public Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public string terms_code
            {
                get { return _terms_code; }
                set { _terms_code = value; }
        }
        public string terms_desc
            {
                get { return _terms_desc; }
                set { _terms_desc = value; }
        }
        public int due_days
            {
                get { return _due_days; }
                set { _due_days = value; }
        }
        public int disc_days
            {
                get { return _disc_days; }
                set { _disc_days = value; }
        }
        public decimal disc_pct
            {
                get { return _disc_pct; }
                set { _disc_pct = value; }
        }
        public string terms_type
            {
                get { return _terms_type; }
                set { _terms_type = value; }
        }
        public int fix_due_day
            {
                get { return _fix_due_day; }
                set { _fix_due_day = value; }
        }
        public int cut_off_day
        {
            get { return _cut_off_day; }
            set { _cut_off_day = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        
        #endregion Public Properties

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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "strtermr"; }
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
        
        //************************************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            //StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT  rowid,src_key, src_desc, src_num_desc, src_char_desc");
            //sql.Append(" FROM  stxinfor where 1=1");

            //if (parameters[1] != null)//src_type
            //    if (parameters[1].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_type) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)//src_key
            //    if (parameters[2].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_key) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)//src_desc
            //    if (parameters[3].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_desc) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[4] != null)//src_num_desc
            //    if (Convert.ToDecimal(parameters[4]) != 0)
            //        sql.Append(" and src_num_desc = " + parameters[4]);

            ////Added By Sunil Pahwa
            //if (Convert.ToInt32(parameters[0]) != 0)
            //    sql.Append(" AND rowid = " + parameters[0].ToString());
            ////********************
            //sql.Append(" order by src_key");

            //return sql.ToString();
            return "";
        }

        public string Get_Terms_Code_Desc
        {
            get { return "uspodrtermget"; }
        }

        #endregion Stored-Procedures


    }
}
