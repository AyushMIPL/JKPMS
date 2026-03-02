using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOMinstry:DVOBase
   {
       private string _keyvalue;
       private string _desc;
     public DVOMinstry()
       {
           _keyvalue =string.Empty;
           _desc = string.Empty;
       
       }
       public string Keyvalue
       {
           get { return _keyvalue; }
           set { _keyvalue = value; }                  
       }
       public string Desc
       {
           get { return _desc; }
           set { _desc = value; }
       }
       #region Store Procedure
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
            get { return "uspMinistryGetAll"; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            throw new Exception("The method or operation is not implemented.");
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
        #endregion Store Procedure
    }
}
