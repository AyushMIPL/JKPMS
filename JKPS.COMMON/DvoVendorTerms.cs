using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DvoVendorTerms:DVOBase 
    {
        private  string   _terms_code ;
        private  string  _terms_desc ;
        private int  _due_days ;
        private int _disc_days;
        private decimal  _disc_pct; 
            
      
        public DvoVendorTerms()
        {
            _terms_code=string.Empty;
            _terms_desc=string.Empty;
            _due_days=0;
            _disc_days=0;
            _disc_pct=0;

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
       public int  due_days
       {
           get { return _due_days; }
           set { _due_days = value; }
       }
       public int disc_days
       {
           get { return _disc_days; }
           set { _disc_days = value; }
       }
       public decimal  disc_pct
       {
           get { return _disc_pct; }
           set { _disc_pct = value; }
       }

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
           get { return "uspVenTermsGet"; }
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
           StringBuilder sql = new StringBuilder();
           sql.Append("SELECT  terms_code p_terms_code ,terms_desc p_terms_desc ,");
           sql.Append(" due_days p_due_days,disc_days p_disc_days,disc_pct p_disc_pct ");
           sql.Append(" from stptermr ");
           sql.Append(" WHERE 1=1");

          
           if (parameters[0] != null)
               if (parameters[0].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(terms_desc) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
        
           return sql.ToString();
       }

       #endregion Stored-Procedures





    }
   
}
