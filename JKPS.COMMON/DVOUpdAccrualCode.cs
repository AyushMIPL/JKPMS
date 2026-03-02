using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    ///      Aim*******************************Created By(Midified By)**********Created Date(Modivied Date)******
    /// 1) To Update Accural for Employee       Rajeev    (Rahul Jain)          18/11/08    (23/12/2008)
    ///    
    /// 2)
    /// </summary>
   public class DVOUpdAccrualCode :DVOBase
    {
        
        private string _accr_code;
        private string _accr_desc;
        private string _accr_method;
        private decimal? _accr_rate;
        private int _accr_freq;
        private int _accr_lapse;

        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary
        private int _RowID;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;
        
        //Constructor to Assign the initial value to the decleared variable
        public DVOUpdAccrualCode()
        {
            _RowID = 0;
            _accr_code = string.Empty;
            _accr_desc = string.Empty;
            _accr_method = string.Empty;
            _accr_rate = null;
            _accr_freq = 0;
            _accr_lapse = 0;


            _InsertMachineInfo = "App";
            _InsertDate = DVOApplicationUserInfo.CurrentDate;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DVOApplicationUserInfo.CurrentDate;
            _UpdateBy = -1;
        }

        #region StartProperties
       public int RowID
       {
           get { return _RowID; }
           set { _RowID = value; }
       }

        public string accr_code
        {
         get{return _accr_code;}
         set { _accr_code = value; }
        }

        public string accr_desc
        {
            get { return _accr_desc; }
            set { _accr_desc = value; }
        }

        public string accr_method
        {
            get { return _accr_method; }
            set { _accr_method = value; }
        }

       public Nullable<decimal> accr_rate
       {
           get { return _accr_rate; }
           set { _accr_rate = value; }
       }

       public int accr_freq
        {
            get { return _accr_freq; }
            set { _accr_freq = value; }
        }

       public int accr_lapse
        {
            get { return _accr_lapse; }
            set { _accr_lapse = value; }
        }


        //Properties used for only SQL Server
       
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
            get
            {
                return _UpdateDate;
            }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        #endregion Properties


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_AccrlIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_AccrlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_AccrDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_AccrlGetAll"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT accr_code_ID v_RowID,accr_code v_accr_code,accr_desc v_accr_desc,accr_method v_accr_method,accr_rate v_accr_rate,accr_freq v_accr_freq,accr_lapse v_accr_lapse from MasterAccuralCodes where 1=1");           
           
            if (parameters[0]!=null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  accr_code LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(accr_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND  accr_method= " + "'" + parameters[2].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[3])>0 && parameters[3] != null)
                sql.Append(" AND accr_rate = " + parameters[3].ToString());

            if (Convert.ToInt32 (parameters[4]) >0)
                sql.Append(" AND accr_freq = " + parameters[4].ToString());
            if (Convert.ToInt32 (parameters[5]) > 0)
                sql.Append(" AND accr_lapse = " + parameters[5].ToString());


            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND accr_code_ID = " + parameters[6].ToString());

            //if (Convert.ToInt32(parameters[6]) > 0)
            //    sql.Append(" AND subdivides= " + parameters[6].ToString());
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(subdivides) LIKE '" + parameters[4].ToString().Trim() + "%'");
            //return sql.ToString();

            return sql.ToString();
        }
        public override string TABLE_NAME
        {
            get { return "MasterAccuralCodes"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        #endregion Stored-Procedures


    }
}
