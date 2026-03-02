using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
///<Development and modification Details>
//Update By Bandra
{
    public class DVOActivityLog : DVOBase
    {
        private int _UserId;
        private int _ActivityId;
        private string _ActivityName;
        private string _LoginId;
        private string _FormName;
        private string _Module;
        private DateTime _InsertDate;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private DateTime _dateFrom = Convert.ToDateTime("01/01/1900");
        private DateTime _dateto = Convert.ToDateTime("01/01/1900");

        #region Constructor

        public DVOActivityLog()
          { 
            _UserId = 0;
            _ActivityId = 0;
            _ActivityName = string.Empty;
            _LoginId = string.Empty;
            _FormName = string.Empty;
            _Module = string.Empty;
            _InsertDate = Convert.ToDateTime("01/01/1900");
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
          }

        #endregion

          #region Public Properties
         
          public int UserId
          {
              get { return _UserId; }
              set { _UserId = value; }
          }
          public int ActivityId
          { 
            get {return _ActivityId;}
            set { _ActivityId = value; }
          }

          public string ActivityName
          {
              get { return _ActivityName; }
              set { _ActivityName = value; }
          }

          public string LoginId
          {
              get { return _LoginId; }
              set { _LoginId = value; }
          }

          public string FormName
          {
              get { return _FormName; }
              set { _FormName = value; }
          }

          public string Module
          {
              get { return _Module; }
              set { _Module = value; }
          }

          public DateTime InsertDate
          {
              get { return _InsertDate; }
              set { _InsertDate = value; }

          }
         public string InsertMachineInfo
         {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
         }

         public int InsertBy
         {
           get { return _InsertBy; }
           set { _InsertBy = value; }

         }
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
          #endregion Properties

          #region Stored-Procedures

          public string AUTHENTICATION_SPNAME
          {
              get { return "uspsecauthenticate"; }
          }

          public override string INSERT_SPNAME
          {
              get { return "USP_ActivityLogIns"; }
          }

          public override string UPDATE_SPNAME
          {
              get { return ""; }
          }

          public override string DELETE_SPNAME
          {
              get { return "USP_ActLogdel"; }
          }

          public override string FIND_SPNAME
          {
              get { return "USP_ActLogGet"; }
          }

          public override string ALL_SPNAME
          {
              get { return ""; }
          }
          public override string TABLE_NAME
          {
              get { return "ActivityLog"; }
          }

          public override int UNIQUE_ID
          {
              get { return ActivityId; }
          }

          public override string NOTES_TABLE_RECORD_ID//
          {
              get { return string.Empty; }
              set { throw new Exception("The method or operation is not implemented."); }
          }

          public override string FIND_QUERY(ref Object[] parameters)
          {
                  System.Text.StringBuilder sql = new StringBuilder();
                  sql.Append("select ActivityId p_ActivityId,ActivityName p_ActivityName,LoginId p_LoginId,");
                  sql.Append("FormName p_FormName,Module p_Module,InsertDate p_InsertDate");
                  sql.Append(" from activitylog ");
                  sql.Append(" where 1=1 ");
               
                  if (parameters[0] != null)
                      if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null && Convert.ToDateTime(parameters[0]) != Convert.ToDateTime("01/01/1900"))//dateFrom
                          sql.Append(" AND InsertDate >= '" + Convert.ToDateTime(parameters[0]).ToShortDateString()+ "'");
                  if (parameters[1] != null)
                      if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null && Convert.ToDateTime(parameters[1]) != Convert.ToDateTime("01/01/1900"))//dateTo
                          sql.Append(" AND InsertDate <= '" + Convert.ToDateTime(parameters[1]).ToShortDateString() + "'");
                  sql.Append(" ORDER BY  ActivityId,InsertDate ");
                  return sql.ToString();
           }

          #endregion Stored-Procedures
      }
    
}
