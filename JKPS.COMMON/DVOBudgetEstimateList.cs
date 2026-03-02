using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By: Sunil Pahwa
   public  class DVOBudgetEstimateList:DVOBase 
    {
    
       
       private string _acct_type;
       private string _desc;
       private string _acct_desc;
      //private string _acct_type;
       private string _keyvalue;
       private string _co_name;

        private string _year;
        private string _set;
     

        #region Constructor
       public DVOBudgetEstimateList()
       {
           _acct_type = string.Empty;

              _desc=string.Empty ;
         _acct_desc=string.Empty ;
         _acct_type=string.Empty ;
         _keyvalue=string.Empty ;
         _co_name=string.Empty ;
          _year  = string.Empty;
           _set = string.Empty;
       
       } 
      #endregion Constructor


         #region public properties
       public string acct_type
       {
           get { return _acct_type; }
           set { _acct_type = value; }
       }

       public string desc
       {
           get { return _desc; }
           set { _desc = value; }
       }

       public string acct_desc
       {
           get { return _acct_desc ; }
           set { _acct_desc  = value; }
       }
     

           public string keyvalue
       {
           get { return _keyvalue ; }
           set { _keyvalue  = value; }
       }
           public string co_name
       {
           get { return _co_name ; }
           set { _co_name  = value; }
       }




       public string year
       {
           get { return _year ; }
           set { _year  = value; }
       }
       public string set
       {
           get { return _set ; }
           set { _set  = value; }
       }

    
      #endregion public properties

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
          get { return "uspBgtEstLstGet"; }
      }

      public override string ALL_SPNAME
      {
          get { return "uspBgtEstLstgetall"; }
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
          sql.Append("Select inbestid.year,inbestid.set,inbestid.approved,inbestid.currestimate,");
          sql.Append("inbestid.initialrequest,inbestid.revised,Flex_struct_Header.desc,");
          sql.Append("PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_type,PayrollGLAccounts.keyvalue, ");
          sql.Append("MasterCompany.company_name from  PayrollGLAccounts, inbestid, Flex_struct_Header, MasterCompany ");
          sql.Append("where  PayrollGLAccounts.acct_no=inbestid.account AND ");
          sql.Append("PayrollGLAccounts.acct_type=Flex_struct_Header.accounttype and  PayrollGLAccounts.acct_cat='U'");

          if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(acct_type) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
          if (parameters[1] != null)
              if (parameters[1].ToString() != string.Empty)
                  sql.Append(" AND year='" + parameters[1].ToString().Trim() +"'");


          if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//grp_key
              sql.Append(" AND set='" + parameters[2].ToString().Trim() + "'");
         // if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//grp_desc
             // sql.Append(" AND Rtrim(grp_desc)LIKE '" + parameters[1].ToString().Trim() + "%'");
          sql.Append(" order by PayrollGLAccounts.acct_type, PayrollGLAccounts.keyvalue");

          return sql.ToString();
      }
      #endregion store-procedures




    }
}
