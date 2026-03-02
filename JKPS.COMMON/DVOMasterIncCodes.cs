using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
   
    /// </summary>
   public class DVOMasterIncCodes : DVOBase 
	{
         private string _inc_code;
       #region Constructor
        public DVOMasterIncCodes()
       {
           _inc_code=string.Empty;
       }
       #endregion

       #region Public properties
       public string inc_code
       {
           get 
           {
               return  _inc_code;
           }
           set 
           {
               _inc_code=value;
           }
       }

       #endregion 


       #region Stored-Procedures

       //public string AUTHENTICATION_SPNAME
      //{
      //    get { return "uspsecauthenticate"; }
      //}
       public string GetEmployeeType
       {
           get { return ""; }
       }
      public override string INSERT_SPNAME
      {
          get { return "";}
      }

      public override string UPDATE_SPNAME
      {
          get { return "";}
      }

      public override string DELETE_SPNAME
      {
          get { return "";}
      }

      public override string FIND_SPNAME
      {
          get { return "USP_IncCodeGet"; }
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
          sql.Append("select inc_code p_inc_code,description p_description,dflt_num p_dflt_num,");
          sql.Append(" dflt_rate p_dflt_rate,dflt_hours p_dflt_hours,dflt_acct p_dflt_acct,");
          sql.Append(" inc_type p_inc_type,dfltaccounttype p_dfltaccounttype ");
          sql.Append(" from MasterIncCodes where 1=1 ");

          if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(inc_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

          return sql.ToString();
      }
      #endregion store-procedures
	}
}
