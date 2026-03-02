using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOASPurchaseClass:DVOBase
    {
       private int _acd_id;
       private int _purchase_class_id;
       private string _description;
        
       
        #region Constructor

        public DVOASPurchaseClass()
        {
            _acd_id = 0;
            _purchase_class_id = 0;
            _description = string.Empty;
         
             
        }

        #endregion Constructor

        #region Public Properties

       public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
       
       public string description
        {
            get { return _description; }
            set { _description = value; }
        }
       public int purchase_class_id
       {
           get { return _purchase_class_id; }
           set { _purchase_class_id = value; }
       }

       


        #endregion Public Properties

        #region Stored-Procedures


        public override string INSERT_SPNAME
        {
            get { return "uspaspurclsins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspaspurclsdel"; }
        }
        public override string FIND_SPNAME
        {
            get { return "uspaspurclsget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspaspurclsall"; }
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
            sql.Append("select * from inpcmast where 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)//purchase_class_id
                sql.Append(" and purchase_class_id = " + parameters[0].ToString());
           

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
