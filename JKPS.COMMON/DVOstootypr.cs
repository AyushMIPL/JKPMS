using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa[01/08/09]
   public class DVOstootypr:DVOBase
    {
      private  string   _order_type;
      private  string _description;
      
       public DVOstootypr()
       {
            _order_type=string.Empty;
            _description=string.Empty;
       }

       public string order_type
       {
           get { return _order_type; }
           set { _order_type = value; }
       }
       public string description
       {
           get { return _description; }
           set { _description = value; }
       }


       #region storedProcedures


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
           get { return ""; }
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
       //Added by Sunil Pahwa For getting Open Order Item Summary Info
       public  string GET_ORDER_OPEN_ITEM_SUMMARY_INFO
       {
           get { return "uspoordsumdescget"; }
       }
       //Added By Rahul Jain Using in Print Order Type definition report
       public string GET_ORDER_TYPE_DEFINITION
       {
           get { return "uspordtypedefget"; }
       }



     

       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();

           return sql.ToString();
       }
       #endregion StoredProcedures





    }
}
