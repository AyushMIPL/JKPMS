using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
///<Development and modification Details>
/// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
///1.) Make property for Income Code Settings                   NP                                  10/16/2014(DD)
///2.) 
///<summery>
{
   public class DVOIncomeCodeSettings : DVOBase
    {
       private int _incomecodesettingid;
       private string _inc_code;
       private string _applicable_for;

        #region Constructor

       public DVOIncomeCodeSettings()
          {
              _incomecodesettingid = 0;
              _inc_code = string.Empty;
              _applicable_for = string.Empty;
          }

        #endregion

          #region Public Properties

        public int incomecodesettingid
          {
              get { return _incomecodesettingid; }
              set { _incomecodesettingid = value; }
          }

          public string inc_code
          {
              get { return _inc_code; }
              set { _inc_code = value; }
          }

          public string Applicable_for
          {
              get { return _applicable_for; }
              set { _applicable_for = value; }
          }
          #endregion Properties

          #region Stored-Procedures

          
          public override string INSERT_SPNAME
          {
              get { return ""; }
          }

          public override string UPDATE_SPNAME
          {
              get { return "USP_IncCodeSettingsUpd"; }
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
              get { return "MasterIncomeCodeSettings"; }
          }

          public override int UNIQUE_ID
          {
              get { return incomecodesettingid; }
          }

          public override string NOTES_TABLE_RECORD_ID
          {
              get { return string.Empty; }
              set { throw new Exception("The method or operation is not implemented."); }
          }

          public string SET_QUERY(ref Object[] parameters)
          {
              StringBuilder sql = new StringBuilder();
              sql.Append("UPDATE MasterIncomeCodeSettings SET inc_code='" + parameters[0].ToString().Trim().Replace("'", "''") + "' ");
              sql.Append(" Where Applicable_for ='" + parameters[1].ToString().Trim().Replace("'", "''") + "' ");
              return sql.ToString();

          }


       public override string FIND_QUERY(ref Object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append("SELECT incomecodesettingid,inc_code,applicable_for");
           sql.Append(" From  MasterIncomeCodeSettings  ");
           sql.Append(" Where 1 =1 ");

           if (parameters[0] != null)
               if (parameters[0].ToString() != string.Empty)
                   sql.Append(" AND applicable_for =  '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

           return sql.ToString();

          }

          #endregion Stored-Procedures
      }
  }
