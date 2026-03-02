using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOASUpdPurchaseClass : DVOBase
    {
        private int _acd_id;
        private string _module;
        private string _dept;
       private int _rowid;
       
       
        #region Constructor

        public DVOASUpdPurchaseClass()
        {
         _acd_id = 0;
         _module = string.Empty;
         _dept = string.Empty;
         _rowid = 0; 
        }

        #endregion Constructor

        #region Public Properties

        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public string module
        {
            get { return _module; }
            set { _module = value; }
        }

        public string dept
        {
            get { return _dept; }
            set { _dept = value; }
        }


       public int rowid
       {
           get { return _rowid; }
           set { _rowid = value; }
       }


        #endregion Public Properties

        #region Stored-Procedures

      
        public string GET_PURCHASE_LEVEL
        {
            get { return "uspaspurclsgget"; }
        }

       public string DEL_PURCHASE_LEVEL
        {
            get { return "uspaspurclsdel"; }
        }


        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspasappurclsupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspasappurclsdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspasappurclsget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspasappurclsall"; }
        }


        public override string TABLE_NAME
        {
            get { return "inappcls"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select *,rowid from inappcls where 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)//acd_id
                sql.Append(" and acd_id = " + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//dept
                sql.Append(" and dept = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//module
                sql.Append(" and module = '" + parameters[2].ToString().Replace("'", "''") + "'");

            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" and rowid = " + parameters[3].ToString());

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
