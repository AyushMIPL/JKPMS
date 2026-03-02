using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOASApprovalClassesInfo : DVOBase
    {
        private int _acd_id;
        private string _module;
        private string _dept;
        private decimal _line_amount;
        private decimal _total_amount;
        private string _global_skip;

        //*************************for Approval Level*******************
        private int _approval_level;
        private decimal _amount;
        private string _description;
        //**************************************************************

        #region Constructor

        public DVOASApprovalClassesInfo()
        {
            _acd_id = 0;
            _module = string.Empty;
            _dept = string.Empty;
            _line_amount = 0;
            _total_amount = 0;
            _global_skip = string.Empty;

            //*************************for Approval Level*******************
            _approval_level = 0;
            _amount = 0;
            _description = string.Empty;
            //**************************************************************

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

        public decimal line_amount
        {
            get { return _line_amount; }
            set { _line_amount = value; }
        }

        public decimal total_amount
        {
            get { return _total_amount; }
            set { _total_amount = value; }
        }

        public string global_skip
        {
            get { return _global_skip; }
            set { _global_skip = value; }
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
            get { return "uspasappclassins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspasappclassupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspasappclassdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspasappclassget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }//uspasappclassall
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
            //**************************** Modified by Bharat Dhall ***********************
            StringBuilder sql = new StringBuilder();
            sql.Append("select inappcls.acd_id,module,dept,line_amount,total_amount,global_skip");
            sql.Append(" from inappcls where 1=1");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//dept
                sql.Append(" and dept  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//module
                sql.Append(" and module  = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[2]) > 0)//line_amount
                sql.Append(" and line_amount  = " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)//total_amount
                sql.Append(" and total_amount  = " + parameters[2].ToString());
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//global_skip
                sql.Append(" and global_skip  = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)//acd_id
                sql.Append(" and inappcls.acd_id  = " + parameters[5].ToString());
            return sql.ToString();
        }

        public string GET_APPROVAL_LEVELS(ref Object[] parameters)
        {
            //**************************** Modified by Bharat Dhall ***********************
            StringBuilder sql = new StringBuilder();
            sql.Append("select inappcls.acd_id,module,dept,line_amount,total_amount,global_skip,inapplev.approval_level,inapplev.amount,inapplev.description");
            sql.Append(" from inappcls,outer inapplev where inappcls.acd_id=inapplev.acd_id");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//dept
                sql.Append(" and dept  = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//module
                sql.Append(" and module  = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[2]) > 0)//line_amount
                sql.Append(" and line_amount  = " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)//total_amount
                sql.Append(" and total_amount  = " + parameters[2].ToString());
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//global_skip
                sql.Append(" and global_skip  = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)//acd_id
                sql.Append(" and inappcls.acd_id  = " + parameters[5].ToString());
            return sql.ToString();
        }

        public string PRINT_APPROVAL_LEVELS_INFO(ref Object[] parameters)
        {
            
            StringBuilder sql = new StringBuilder();
            sql.Append("select inappcls.acd_id,inappcls.module,dept,inappcls.line_amount,");
            sql.Append(" inappcls.total_amount,inappcls.global_skip,inapplev.approval_level,");
            sql.Append(" inapplev.amount,inapplev.description ");
            sql.Append(" from inappcls,inapplev where inappcls.acd_id=inapplev.acd_id ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dept) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND  module='" +parameters[1].ToString().Trim()+"'");

            return sql.ToString();
        }




        #endregion Stored-Procedures
    }
}
