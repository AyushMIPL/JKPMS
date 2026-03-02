using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{//****Implemented BY****
    //-------Sunil Pahwa------
    public class DVOUpdateBudgetControlDefaults : DVOBase
    {//inbcntrc
        private string _activebudgetyear;
        private string _activebudgetset;
        private int _defaultinitialaloc;
        private int _post_seq_no;
        private string _def_alloc_warrant;
        private string _use_def_alloc_war;
        private int _alloc_periods;
        private int _warrant_no;
        //stxinfor
        private string _src_key;
        private string _src_desc;
        private decimal _src_num_desc;

        private string _src_char_desc;
        private int _Rowid;



        #region Constructor

        public DVOUpdateBudgetControlDefaults()
        {
            _activebudgetyear = string.Empty;
            _activebudgetset = string.Empty;
            _defaultinitialaloc = 0;
            _post_seq_no = 0;
            _def_alloc_warrant = string.Empty;
            _use_def_alloc_war = string.Empty;
            _alloc_periods = 0;
            _warrant_no = 0;

            _src_key = string.Empty;
            _src_desc = string.Empty;
            _src_num_desc = 0;
            _src_char_desc = string.Empty;
            _Rowid = 0;
        }

        #endregion Constructor

        #region Public Properties
        public string activebudgetyear
        {
            get { return _activebudgetyear; }
            set { _activebudgetyear = value; }
        }
        public string activebudgetset
        {
            get { return _activebudgetset; }
            set { _activebudgetset = value; }
        }
        public int defaultinitialaloc
        {
            get { return _defaultinitialaloc; }
            set { _defaultinitialaloc = value; }
        }

        public int post_seq_no
        {
            get { return _post_seq_no; }
            set { _post_seq_no = value; }
        }
        public string def_alloc_warrant
        {
            get { return _def_alloc_warrant; }
            set { _def_alloc_warrant = value; }
        }
        public string use_def_alloc_war
        {
            get { return _use_def_alloc_war; }
            set { _use_def_alloc_war = value; }
        }
        public int alloc_periods
        {
            get { return _alloc_periods; }
            set { _alloc_periods = value; }
        }
        public int warrant_no
        {
            get { return _warrant_no; }
            set { _warrant_no = value; }
        }



        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }
        public string src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }
        public decimal src_num_desc
        {
            get { return _src_num_desc; }
            set { _src_num_desc = value; }
        }
        public string src_char_desc
        {
            get { return _src_char_desc; }
            set { _src_char_desc = value; }
        }

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        #endregion Properties



        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspUpdBgtCtDfIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUpdBgtCtDfUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspUpdBgtCtDfDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspUpdBgtCtlDefGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspUpdBgtCtDfGetAl"; }
        }

        public override string TABLE_NAME
        {
            get { return "inbcntrc"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  u.activebudgetyear P_actbudgetyear, u.activebudgetset P_actbudgetset,");
            sql.Append(" u.defaultinitialaloc P_definitaloc,");
            sql.Append(" u.def_alloc_warrant P_def_alloc_warnt, u.alloc_periods p_alloc_periods,");
            sql.Append(" u.warrant_no p_warrant_no,   ");
            sql.Append(" r.src_desc z_src_desc,r.src_num_desc P_src_num_desc,u.rowid P_rowid,u.use_def_alloc_war p_def_al_war FROM ");
            sql.Append(" inbcntrc u ,outer stxinfor r WHERE src_type='s' and r.src_key = u.activebudgetset  ");
            //Added Later
            sql.Append(" and r.src_char_desc= u.activebudgetyear ");
            //After bhart sir told
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND activebudgetyear = '" + parameters[0].ToString().Trim() + "'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(activebudgetset) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND  defaultinitialaloc=" + parameters[2].ToString());

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(def_alloc_warrant) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    if (Convert.ToInt32(parameters[4].ToString()) > 0)
                        sql.Append(" AND  src_num_desc=" + parameters[4].ToString());


            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND  u.alloc_periods=" + parameters[5].ToString());

            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND  warrant_no=" + parameters[6].ToString());

            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND  r.RowId=" + parameters[7].ToString());




            return sql.ToString();
        }

        public string GET_ROWID_BY_ACTIVE_SET(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid FROM inbcntrc ");
            sql.Append(" WHERE 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND activebudgetset = '" + parameters[0].ToString().Trim() + "'");
            return sql.ToString();
        }

        #endregion Stored-Procedures

    }


}


