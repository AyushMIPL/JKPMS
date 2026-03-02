using System;
using System.Collections.Generic;
using System.Text;
//Shrishanshu
namespace JKPS.COMMON
{
   public class DVOstxinfor:DVOBase
    {
         private int _rowid;
         private string _src_type;
         private string _src_key;
         private string _src_desc;
         private decimal _src_num_desc;
         private string _src_char_desc;
         private string _src_acct_no;
       

       public DVOstxinfor()
       {
           _rowid = 0;
           _src_type = string.Empty;
           _src_key = string.Empty;
           _src_desc = string.Empty;
           _src_num_desc = 0;
           _src_char_desc = string.Empty;
           _src_acct_no = string.Empty;
 
       }

       public int rowid
       {
           get { return _rowid; }
           set { _rowid = value; }
       }
       public string src_type
       {
           get { return _src_type ; }
           set { _src_type = value; }
       }
       public string src_key
       {
           get { return _src_key; }
           set { _src_key = value; }
       }
       public string src_desc
       {
           get { return _src_desc; }
           set { _src_desc = value; }
       }
       public decimal src_num_desc
       {
           get { return _src_num_desc; }
           set { _src_num_desc = value; }
       }
       public string src_char_desc
       {
           get { return _src_char_desc; }
           set { _src_char_desc = value; }
       }
       public string src_acct_no
       {
           get { return _src_acct_no; }
           set { _src_acct_no = value; }
       }
       public override string INSERT_SPNAME
       {
           get { return "uspdiscdefiins"; }
       }
       public override string UPDATE_SPNAME
       {
           get { return "uspdiscdefiupd"; }
       }
       public override string DELETE_SPNAME
       {
           get { return "uspdiscdefidel"; }
       }
       public override string FIND_SPNAME
       {
           get { return "uspdiscdefiget"; }
       }
       public override string TABLE_NAME
       {
           get { return "stxinfor"; }
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
       public string GET_JOIN_TYPE
       {
           get { return "uspsbjoinget"; }
       }
       public override string ALL_SPNAME
       {
           get { return "uspsbjoinget"; }
       }
       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           sql.Append("SELECT status_id,status_name from sbacctstatus ");
           sql.Append("where 1=1 ");
           return sql.ToString();
       }
    }
}
