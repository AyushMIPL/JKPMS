using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOASApprovalLevelInfo:DVOBase
    {
        private int _acd_id;
        private int _approval_level;
        private decimal _amount;
        private string _description;        
       
        #region Constructor

        public DVOASApprovalLevelInfo()
        {
         _acd_id = 0;
         _approval_level=0;
         _amount=0;
         _description=string.Empty;                    
        }

        #endregion Constructor

        #region Public Properties

        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }        

        public int approval_level
        {
            get { return _approval_level; }
            set { _approval_level = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
 
        
      
     


        #endregion Public Properties

        #region Stored-Procedures


        public string GET_APPROVED_LEVEL
        {
            get { return "uspasapplevelgget"; }
        }

        public string DEL_APPROVED_LEVEL
        {
            get { return "uspasappleveldel"; }
        }


        public override string INSERT_SPNAME
        {
            get { return "uspasapplevelins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspasapplevelupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspasappleveldel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspasapplevelgget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspasapplevelall"; }
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
            return "";
            
        }

        #endregion Stored-Procedures
    }
}
