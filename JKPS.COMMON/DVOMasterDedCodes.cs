using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPRDeductionCodesMasterDedcodes : DVOBase
    {
        private int _RowID;
        private string _ded_code;
        private string _description;
        private string _ded_type;
        private string _ded_taxred;
        private decimal? _dflt_rate;
        private decimal? _dflt_limit;
        private int    _dflt_acct;
        private string _dflt_dept;
        private string _dflt_apply;
        private decimal? _dflt_hi_ded_amt;
        private decimal? _dflt_lo_ded_amt;
        private string _state_ein;
        private string _tax_jur;
        private string _dfltaccounttype;
        //Added by sanjay
        private string _dfltaccountdesc;
        private string _dfltkeyvalue;
        private decimal? _dflt_pay_limit;
        private string _yearrollover;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;// date
        private int _UpdateBy;

        #region Constructor

        public DVOPRDeductionCodesMasterDedcodes()
        {
            _RowID = 0;
            _ded_code = string.Empty;
            _description = string.Empty;
            _ded_type = string.Empty;
            _ded_taxred = string.Empty;
            _dflt_rate = null;
            _dflt_limit = null;
            _dflt_acct = 0;
            _dflt_dept = string.Empty;
            _dflt_apply = string.Empty;
            _dflt_hi_ded_amt = null;
            _dflt_lo_ded_amt = null;
            _state_ein = string.Empty;
            _tax_jur = string.Empty;
            _dfltaccounttype = string.Empty;
            _dfltaccountdesc = string.Empty;
            _dfltkeyvalue = string.Empty;
            _dflt_pay_limit = null;
            _yearrollover = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//date
            _InsertBy = -1;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";// date
            _UpdateBy =-1;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string ded_type
        {
            get { return _ded_type; }
            set { _ded_type = value; }
        }
        public string ded_taxred
        {
            get { return _ded_taxred; }
            set { _ded_taxred = value; }
        }
        public Nullable<decimal> dflt_rate
        {
            get { return _dflt_rate; }
            set { _dflt_rate = value; }
        }
        public Nullable<decimal> dflt_limit
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
        public string dflt_apply
        {
            get { return _dflt_apply; }
            set { _dflt_apply = value; }
        }
        public Nullable<decimal> dflt_hi_ded_amt
        {
            get { return _dflt_hi_ded_amt; }
            set { _dflt_hi_ded_amt = value; }
        }
        public  Nullable<decimal> dflt_lo_ded_amt
        {
            get { return _dflt_lo_ded_amt; }
            set { _dflt_lo_ded_amt = value; }
        }
        public string state_ein
        {
            get { return _state_ein; }
            set { _state_ein = value; }
        }
        public string tax_jur
        {
            get { return _tax_jur; }
            set { _tax_jur = value; }
        }
        public string dfltaccounttype
        {
            get { return _dfltaccounttype; }
            set { _dfltaccounttype = value; }
        }
        public string dfltaccountdesc
        {
            get { return _dfltaccountdesc; }
            set { _dfltaccountdesc = value; }
        }
        public string dfltkeyvalue
        {
            get { return _dfltkeyvalue; }
            set { _dfltkeyvalue = value; }
        }
        public Nullable<decimal> dflt_pay_limit
        {
            get { return _dflt_pay_limit; }
            set { _dflt_pay_limit = value; }
        }
        public string yearrollover
        {
            get { return _yearrollover; }
            set { _yearrollover = value; }
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

        #endregion public properties

        #region Stored-Procedures       
        public string GetDeductionCodes
        {
            get { return "USP_DedcodeGet"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "USP_DedCodIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_DedCodUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_DedCodDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspDedCodGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspDedCodGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterDedcodes"; }
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

        //***********By Sanjay**************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MasterDedcodes.ded_code_id p_RowID,MasterDedcodes.ded_code p_ded_code,MasterDedcodes.description p_description,MasterDedcodes.ded_type p_ded_type,");
            sql.Append(" MasterDedcodes.ded_taxred p_ded_taxred,MasterDedcodes.dflt_rate p_dflt_rate,MasterDedcodes.dflt_limit p_dflt_limit,MasterDedcodes.dflt_acct p_dflt_acct,");
            sql.Append(" MasterDedcodes.dflt_dept p_dflt_dept,MasterDedcodes.dflt_apply p_dflt_apply,MasterDedcodes.dflt_hi_ded_amt p_dflt_hi_ded_amt,");
            sql.Append(" MasterDedcodes.dflt_lo_ded_amt p_dflt_lo_ded_amt,MasterDedcodes.state_ein p_state_ein,MasterDedcodes.tax_jur p_tax_jur,MasterDedcodes.dfltaccounttype p_dfltaccounttype,");
            sql.Append(" MasterDedcodes.dfltkeyvalue p_dfltkeyvalue,MasterDedcodes.dflt_pay_limit p_dflt_pay_limit,MasterDedcodes.yearrollover p_yearrollover,PayrollGLAccounts.acct_type p_accttype,");
            sql.Append(" PayrollGLAccounts.acct_desc p_acctdesc,PayrollGLAccounts.keyvalue p_keyvalue");
            sql.Append(" FROM MasterDedcodes LEFT Outer JOIN PayrollGLAccounts ");
            sql.Append(" ON MasterDedcodes.dflt_acct = PayrollGLAccounts.acct_no where 1=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterDedcodes.ded_code_id = " + parameters[0].ToString().Trim().Replace("'", "''") + "");
            if (parameters[1] != null && parameters[1].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.ded_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null && parameters[2].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.description  LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null && parameters[3].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.ded_type = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null && parameters[4].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.ded_taxred = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[5])>0  &&  parameters[5]!=null)
                sql.Append(" AND MasterDedcodes.dflt_rate = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[6])> 0 && parameters[6]!=null)
                sql.Append(" AND MasterDedcodes.dflt_limit = " + parameters[6].ToString().Trim().Replace("'", "''") + "");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND MasterDedcodes.dflt_acct = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null && parameters[8].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.dflt_dept = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null && parameters[9].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.dflt_apply = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[10])>0  &&  parameters[10]!=null)
                sql.Append(" AND MasterDedcodes.dflt_hi_ded_amt = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[11])>0 && parameters[11]!=null)
                sql.Append(" AND MasterDedcodes.dflt_lo_ded_amt = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null && parameters[12].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.state_ein = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null && parameters[13].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.tax_jur = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[14] != null && parameters[14].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.dfltaccounttype = '" + parameters[14].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[15] != null && parameters[15].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.dfltkeyvalue = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32 (parameters[16])> 0  && parameters[16]!=null)
                sql.Append(" AND MasterDedcodes.dflt_pay_limit = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[17] != null && parameters[17].ToString() != string.Empty)
                sql.Append(" AND MasterDedcodes.yearrollover = '" + parameters[17].ToString().Trim().Replace("'", "''") + "'");           

            return sql.ToString();
        }

        public string FIND_DESC()
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MasterDedcodes.ded_code, MasterDedcodes.description from MasterDedcodes");
            return sql.ToString();
        }

        #endregion store-procedures
    }
}
