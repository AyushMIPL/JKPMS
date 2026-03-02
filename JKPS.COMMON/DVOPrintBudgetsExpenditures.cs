using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPrintBudgetsExpenditures:DVOBase
   {
       private string _AccountType;
       private string _keyvalue;
       private string _year;
       private string _set;

       public DVOPrintBudgetsExpenditures()
       {
           _AccountType = string.Empty;
           _keyvalue = string.Empty;
           _set = string.Empty;
           _year = string.Empty;
       
       }
       public string AccountType
       {
           get { return _AccountType; }
           set { _AccountType = value; }
       }
       public string Keyvalue
       {
           get { return _keyvalue; }
           set { _keyvalue = value; }

       }
       public string Year
       {
           get { return _year; }
           set { _year = value; }

       }
       public string Set
       {
           get { return _set; }
           set { _set = value; }
       }

       public override string INSERT_SPNAME
       {
           get { throw new Exception("The method or operation is not implemented."); }
       }

       public override string UPDATE_SPNAME
       {
           get { throw new Exception("The method or operation is not implemented."); }
       }

       public override string DELETE_SPNAME
       {
           get { throw new Exception("The method or operation is not implemented."); }
       }

       public override string FIND_SPNAME
       {
           get { throw new Exception("The method or operation is not implemented."); }
       }

       public override string ALL_SPNAME
       {
           get { throw new Exception("The method or operation is not implemented."); }
       }

       public override string FIND_QUERY(ref object[] parameters)
       {
           throw new Exception("The method or operation is not implemented.");
       }
       public string FIND_budgetexpm
       {
           get { return "uspbudgetexpm"; }
       }
       public string FIND_BUDvEXP
       {
           get { return "uspbudvexpget"; }
       }
       public string FIND_budexptd
       {
           get { return "uspbudexptd"; }
       }
       public string FIND_unPostedGLStmt
       {
           get { return "uspunpostedglstmt"; }
       }
       public string FIND_unPostInv
       {
           get { return "uspunpostinv"; }
       }
       public string FIND_unPostedChq
       {
           get { return "uspunpostedchq"; }
       }
       public string FIND_punPostedOrd
       {
           get { return "uspunpostedord"; }
       }
       public string FIND_unPostedUInv
       {
           get { return "uspunposteduinv"; }
       }
       public string FIND_rebctdamt
       {
           get { return "usprebctdamt"; }
       }
       public string FIND_rebandcavilamt
       {
           get { return "usprebandcavilamt"; }
       }
       public string FIND_ADJUSTMENTS
       {
           get { return "uspadjget"; }
       }
       public override string TABLE_NAME
       {
           get { return ""; }
       }
       public string GET_ACTUALS
       {
           get { return "uspcalactual"; }
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

   }
}
