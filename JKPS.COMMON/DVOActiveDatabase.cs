using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   //Implemented By: Rahul Jain on 20/01/2009
   public class DVOActiveDatabase: DVOBase
   {
       private int _RowId;
       private string _DBName;
       private string _DBDescription;
       private string _DBType;
       private string _ConString;
       private string _Password;
       private string _UserId;
       private int _Active;

       public DVOActiveDatabase()
       {
           _RowId = 0;
           _DBName = string.Empty;
           _DBDescription = string.Empty;
           _DBType = string.Empty;
           _ConString = string.Empty;
           _Password = string.Empty;
           _Password = string.Empty;
           _UserId = string.Empty;
           _Active = 0;
       }

       public int RowId
       {
           get { return _RowId; }
           set { _RowId = value; }
       }

       public string DBName
       {
           get { return _DBName; }
           set { _DBName = value; }
       }
       public string DBDescription
       {
           get { return _DBDescription; }
           set { _DBDescription = value; }
       }

       public string DBType
       {
           get { return _DBType; }
           set { _DBType = value; }
       }
       public string ConString
       {
           get { return _ConString; }
           set { _ConString = value; }
       }

       public string UserId
       {
           get { return _UserId; }
           set { _UserId = value; }
       }

       public string Password
       {
           get { return _Password; }
           set { _Password = value; }
       }
       public int Active
       {
           get { return _Active; }
           set { _Active = value; }
       }

       #region Stored-Procedures

       public string AUTHENTICATION_SPNAME
       {
           get { return ""; }
       }

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
           get { return "activedatabase"; }
       }
       public string GET_ALL_DATABASES
       {
           get { return "uspalldatabasesget"; }
       }
       //public string GETConnectionString
       //{
       //    get { return "uspgetconstring"; }
       //}
       public override int UNIQUE_ID
       {
           get { return _RowId; }
       }

       public override string NOTES_TABLE_RECORD_ID
       {
           get { return string.Empty; }
           set { throw new Exception("The method or operation is not implemented."); }
       }

       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           
           return sql.ToString();
       }

       #endregion Stored-Procedures
   }
}
