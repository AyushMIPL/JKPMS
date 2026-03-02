using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    
   public class DVOGenJourView:DVOBase
    {
        private int _BatchId;
       private string _postorcheck;
       private string _batchowner;
        public DVOGenJourView()
        {
            _BatchId = 0;
            _postorcheck = "CHECK";
            _batchowner = "MASTER";
        }
       public int BatchID
       {
           get { return _BatchId; }
           set { _BatchId = value; }
       
       }
       public string postorcheck
       {
           get { return _postorcheck; }
           set { _postorcheck = value; }

       }
       public string batchowner
       {
           get { return _batchowner; }
           set { _batchowner = value; }
       }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGenJuorIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGenJuoUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGenJuorDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGenJuorGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspGenJuorGetAll"; }
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
       public override string FIND_QUERY(ref object[] parameters)
       {
        
     
           return "";
       }

        #endregion Stored-Procedures
       
    }
}
