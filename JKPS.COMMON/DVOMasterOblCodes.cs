using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterOblCodes : DVOBase
    {
        private int _RowID;
        private string _obl_code;
        private string _description;
        private string _obl_type;
        private decimal? _dflt_rate;//make nullable decimal By Rahul 
        private decimal? _dflt_limit;//make nullable decimal By Rahul 
        private int _dflt_acct;
        private string _dflt_dept;
        private int _dflt_bacct;
        private string _dflt_bdept;
        private string _dfltaccounttype;
        private string _dfltbaccounttype;
        private string _dfltbkeyvalue;
        private string _dfltbacct_desc;
        private decimal? _dflt_pay_limit;//make nullable decimal By Rahul 
        private string _InsertMachineInfo;
        private string _InsertDate;//datetime,
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;// datetime,
        private int _UpdateBy;

        private string _dflt_keyvalue;



        #region Constructor

        public DVOMasterOblCodes()
        {
            _RowID = 0;
            _obl_code = string.Empty;
            _description = string.Empty;
            _obl_type = string.Empty;
            _dflt_rate = null;
            _dflt_limit = null;
            _dflt_acct = 0;
            _dflt_dept = string.Empty;
            _dflt_bacct = 0;
            _dflt_bdept = string.Empty;
            _dfltaccounttype = string.Empty;
            _dfltbaccounttype = string.Empty;
            _dfltbkeyvalue = string.Empty;
            _dfltbacct_desc = string.Empty;
            _dflt_pay_limit = null;
            _InsertMachineInfo = null;
            _InsertDate = "01/01/1900";//datetime,
            _InsertBy = 0;
            _UpdateMachineInfo = null;
            _UpdateDate = "01/01/1900";// datetime,
            _UpdateBy = 0;

            _dflt_keyvalue = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string obl_code
        {
            get { return _obl_code; }
            set { _obl_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string obl_type
        {
            get { return _obl_type; }
            set { _obl_type = value; }
        }
        public decimal? dflt_rate//make nullable decimal By Rahul 
        {
            get { return _dflt_rate; }
            set { _dflt_rate = value; }
        }
        public decimal? dflt_limit//make nullable decimal By Rahul 
        {
            get { return _dflt_limit; }
            set { _dflt_limit = value; }
        }
        public int dflt_acct
        {
            get { return _dflt_acct; }
            set { _dflt_acct = value; }
        }
        public string dflt_dept
        {
            get { return _dflt_dept; }
            set { _dflt_dept = value; }
        }
        public int dflt_bacct
        {
            get { return _dflt_bacct; }
            set { _dflt_bacct = value; }
        }
        public string dflt_bdept
        {
            get { return _dflt_bdept; }
            set { _dflt_bdept = value; }
        }
        public string dfltaccounttype
        {
            get { return _dfltaccounttype; }
            set { _dfltaccounttype = value; }
        }
        public string dfltbaccounttype
        {
            get { return _dfltbaccounttype; }
            set { _dfltbaccounttype = value; }
        }
        public string dfltbkeyvalue
        {
            get { return _dfltbkeyvalue; }
            set { _dfltbkeyvalue = value; }
        }
        public string dfltbacct_desc
        {
            get { return _dfltbacct_desc; }
            set { _dfltbacct_desc = value; }
        }
        public decimal? dflt_pay_limit//make nullable decimal By Rahul 
        {
            get { return _dflt_pay_limit; }
            set { _dflt_pay_limit = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }


        public string dflt_keyvalue
        {
            get { return _dflt_keyvalue; }
            set { _dflt_keyvalue = value; }
        }


        #endregion public properties

        #region Stored-Procedures
        
        public string GetActivityCode
        {
            get { return "USP_ActCodGet"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "USP_OblCodIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_OblCodUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_OblCodDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspOblCodGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_OblCodGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterOblCodes"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT MasterOblCodes.Obl_Code_ID p_RowID,MasterOblCodes.obl_code p_obl_code,MasterOblCodes.description p_description,MasterOblCodes.obl_type p_obl_type,MasterOblCodes.dflt_rate p_dflt_rate,");
            sql.Append(" MasterOblCodes.dflt_limit p_dflt_limit,MasterOblCodes.dflt_acct p_dflt_acct,MasterOblCodes.dflt_dept p_dflt_dept,MasterOblCodes.dflt_bacct p_dflt_bacct,");
            sql.Append(" MasterOblCodes.dflt_bdept dflt_bdept,MasterOblCodes.dfltaccounttype p_dfltaccounttype,MasterOblCodes.dfltbaccounttype p_dfltbaccounttype,");
            sql.Append(" MasterOblCodes.dfltbkeyvalue p_dfltbkeyvalue,MasterOblCodes.dflt_pay_limit p_dflt_pay_limit,PayrollGLAccounts.acct_type,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc");
            sql.Append(" FROM MasterOblCodes LEFT Outer JOIN PayrollGLAccounts ON MasterOblCodes.dflt_bacct=PayrollGLAccounts.acct_no WHERE 1=1 ");//Modify by sanjay--join with table PayrollGLAccounts

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterOblCodes.Obl_Code_ID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.obl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.description) Like '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.obl_type) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[4]) > 0 && parameters[4] != null)
                sql.Append(" AND MasterOblCodes.dflt_rate = " + parameters[4].ToString());
            if (Convert.ToDecimal(parameters[5]) > 0 && parameters[5] != null)
                sql.Append(" AND MasterOblCodes.dflt_limit = " + parameters[5].ToString());
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND MasterOblCodes.dflt_acct = " + parameters[6].ToString());
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.dflt_dept) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND MasterOblCodes.dflt_bacct = " + parameters[8].ToString());
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.dflt_bdept) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.dfltaccounttype) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.dfltbaccounttype) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterOblCodes.dfltbkeyvalue) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[13]) > 0 && parameters[13] != null)
                sql.Append(" AND MasterOblCodes.dflt_pay_limit = " + parameters[13].ToString());

            return sql.ToString();
        }
        public string FIND_DESC()
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT obl_code,description from MasterOblCodes");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
