using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
  
    /// </summary>
    public class DVODeductionsGet : DVOBase 
    {
         private string _ded_code;
       #region Constructor
        public DVODeductionsGet()
       {
           _ded_code=string.Empty;
       }
       #endregion

       #region Public properties
       public string ded_code
       {
           get  {  return  _ded_code; }
           set  {  _ded_code=value; }
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
          get { return "uspDedCodeGet"; }
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
          sql.Append("select ded_code p_ded_code,description p_description,ded_type p_ded_type,");
          sql.Append(" ded_taxred p_ded_taxred,dflt_pay_limit p_dflt_pay_limit,dflt_limit p_dflt_limit,");
          sql.Append(" dflt_rate p_dflt_rate,dflt_apply p_dflt_apply,dflt_acct p_dflt_acct,PayrollGLAccounts.keyvalue p_keyvalue,PayrollGLAccounts.acct_desc p_acct_desc ");
          sql.Append(" from MasterDedcodes LEFT Outer JOIN  PayrollGLAccounts ON PayrollGLAccounts.acct_no=MasterDedcodes.dflt_acct  WHERE 1=1 ");

          if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(ded_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

          return sql.ToString();
      }
      #endregion store-procedures

    }
}
