using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.) DVO For FeedBack                             Rajeev(D)                                    25/11/2008(DD)
    ///2.) 
    ///<summery>
    public class DVOFeedBack : DVOBase
    {

         
      private DateTime _P_Date;
      private string _P_Desc;
      private string _PRec_Situation;
      private string _P_Module;
      private string _P_Form;
      private string _LoginID;
      private string _Email;
      private string _Phone;
      private string _P_File;
        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary
        private int _RowID;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;
        //Added By Rahul on 26/11/2008
        private DateTime _dateFrom = Convert.ToDateTime("01/01/1900");
        private DateTime _dateto = Convert.ToDateTime("01/01/1900");
        //*******************************


        //Constructor to Assign the initial value to the decleared variable
        public DVOFeedBack()
        {
            _P_Date = DateTime.Now;
            _P_Desc="";
            _PRec_Situation = "";
            _P_Module = "";
            _P_Form = "";
            _LoginID = "";
            _Email = "";
            _Phone = "";
            _P_File = "";

            _RowID = 0;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
        }

        #region StartProperties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }

        public DateTime P_Date
        {
            get { return _P_Date; }
            set { _P_Date = value; }
        }

        public string P_Desc
        {
            get { return _P_Desc; }
            set { _P_Desc = value; }
        }
        public string PRec_Situation
        {
            get { return _PRec_Situation; }
            set { _PRec_Situation = value; }
        }
        public string P_Module
        {
            get { return _P_Module; }
            set { _P_Module = value; }
        }
        public string P_Form
        {
            get { return _P_Form; }
            set { _P_Form = value; }
        }
        public string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string P_File
        {
            get { return _P_File; }
            set { _P_File = value; }
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
        //Added By Rahul Jain on 26/11/2008
        public DateTime dateto
        {
            get { return _dateto; }
            set { _dateto = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
        //**********************************

        #endregion Properties


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspFeedBkIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspFeedBkUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspFeedBkGet"; }
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
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT RowID p_RowID, P_Date, P_Desc, PRec_Situation p_Situation,");
            sql.Append(" P_Module, P_Form, LoginID p_LoginId, Email p_Email, Phone p_Phone,");
            sql.Append(" P_File, InsertMachineInfo p_MachineInfo,InsertDate p_InsertDate FROM   tblfeedback ");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)//LoginId
                    sql.Append(" AND Rtrim(LoginId) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)//P_Module
                if (parameters[1].ToString() != string.Empty)//P_Module
                    sql.Append(" AND Rtrim(P_Module)  LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)//P_Form
                if (parameters[2].ToString() != string.Empty)//P_Form
                    sql.Append(" AND Rtrim(P_Form)  LIKE '%" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null && Convert.ToDateTime(parameters[3]) != Convert.ToDateTime("01/01/1900"))//dateFrom
                    sql.Append(" AND InsertDate >= '" + Convert.ToDateTime(parameters[3]).ToShortDateString().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null && Convert.ToDateTime(parameters[4]) != Convert.ToDateTime("01/01/1900"))//dateto
                    sql.Append(" AND InsertDate <= '" + Convert.ToDateTime(parameters[4]).ToShortDateString().Replace("'", "''") + "'");

            //************************************************************

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
