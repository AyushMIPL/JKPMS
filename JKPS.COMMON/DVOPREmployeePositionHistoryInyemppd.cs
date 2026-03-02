using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPREmployeePositionHistoryInyemppd : DVOBase
    {
        private int _RowId;
        private string _empl_code;
        private string _pos_code;
        private string _cat_code;
        private string _scale_code;
        private string _start_date;// smalldatetime,
        private string _end_date;// smalldatetime,
        private string _approved_by;
        private decimal? _pay_rate;
        private string _temporary;
        private string _InsertMachineInfo;
        private string _InsertDate;// datetime,
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;// datetime,
        private int _UpdateBy;

        #region Constructor

        public DVOPREmployeePositionHistoryInyemppd()
        {
            _RowId = 0;
            _empl_code = string.Empty;
            _pos_code = string.Empty;
            _cat_code = string.Empty;
            _scale_code = string.Empty;
            _start_date = "01/01/1900";// smalldatetime,
            _end_date = "01/01/1900";// smalldatetime,
            _approved_by = string.Empty;
            _pay_rate = null;
            _temporary = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";// datetime,
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";// datetime,
            _UpdateBy = 0;
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string pos_code
        {
            get { return _pos_code; }
            set { _pos_code = value; }
        }
        public string cat_code
        {
            get { return _cat_code; }
            set { _cat_code = value; }
        }
        public string scale_code
        {
            get { return _scale_code; }
            set { _scale_code = value; }
        }
        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public string approved_by
        {
            get { return _approved_by; }
            set { _approved_by = value; }
        }
        public decimal? pay_rate
        {
            get { return _pay_rate; }
            set { _pay_rate = value; }
        }
        public string temporary
        {
            get { return _temporary; }
            set { _temporary = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
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
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspEmpPosHstIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspEmpPosHstUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspEmpPosHstDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspEmpPosHstGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspEmpPosHstGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "inyemppd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT RowId p_RowId,empl_code p_empl_code,pos_code p_pos_code,cat_code p_cat_code,scale_code p_scale_code,");
            sql.Append(" start_date p_start_date,end_date p_end_date,approved_by p_approved_by,pay_rate p_pay_rate,");
            sql.Append(" temporary p_temporary");
            sql.Append(" FROM inyemppd WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND RowId = " + parameters[0].ToString().Trim());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(pos_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(cat_code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(scale_code) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim() != string.Empty && !parameters[5].ToString().Trim().Contains("1900"))
                    sql.Append(" AND start_date = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim() != string.Empty && !parameters[6].ToString().Trim().Contains("1900"))
                    sql.Append(" AND end_date = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(approved_by) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND pay_rate = " + parameters[8].ToString().Trim());
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(temporary) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
