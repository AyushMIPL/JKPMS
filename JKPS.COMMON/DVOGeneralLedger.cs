using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGeneralLedger : DVOBase
    {
        private int _acct_no;
        private string _acct_type;
        private int _acct_type_id;
        private string _acct_desc;
        private string _acct_cat;
        private string _processing_seq;
        private string _incr_with_crdt;
        private string _subtotal_group;
        private string _keyvalue;
        // To be Enhanced in the tables
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        /// <summary>
        /// Added for Record Locking Purpose
        /// </summary>
        private int _RowId;

        private int _gobzero;
        private int _active;

        #region Constructor

        public DVOGeneralLedger()
        {
            _acct_no=0;
            _acct_type=string.Empty;
            _acct_type_id = 0;
            _acct_desc=string.Empty;
            _acct_cat=string.Empty;
            _processing_seq=string.Empty;
            _incr_with_crdt=string.Empty;
            _subtotal_group=string.Empty;
            _keyvalue=string.Empty;
            // Informix table to be enhanced 
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;

            _gobzero = 0;
            _active = 1;
        }

        #endregion Constructor

        #region Public Properties

        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }//

        public string acct_type
        {
            get { return _acct_type; }
            set { 
                _acct_type = value.TrimEnd(); 
            }
        }

        public int acct_type_id
        {
            get { return _acct_type_id; }
            set
            {
                _acct_type_id = value;
            }
        }

        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value.TrimEnd(); }
        }

        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value.TrimEnd(); }
        }

        public string processing_seq
        {
            get { return _processing_seq; }
            set { _processing_seq = value.TrimEnd(); }
        }
        public string incr_with_crdt
        {
            get { return _incr_with_crdt; }
            set { _incr_with_crdt = value.TrimEnd(); }
        }
        public string subtotal_group
        {
            get { return _subtotal_group; }
            set { _subtotal_group = value.TrimEnd(); }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value.TrimEnd(); }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value.TrimEnd(); }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value.TrimEnd(); }
        }
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int gobzero
        {
            get { return _gobzero; }
            set { _gobzero = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        public override int UNIQUE_ID
        {
            get { return _acct_no; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        #endregion Public Properties

        #region Stored-Procedures

        public string COUNT_ACCOUNTS_OF_ACCOUNTTYPEID
        {
            get { return "USP_CountActTypId"; }
        }

        public string ACCOUNT_INFO_FOR_ACCOUNT_TEXT_BOX
        {
            get { return "USP_ActtxtInfo"; }
        }

        public string COUNT_KEYVALUE
        {
            get { return "USP_CountKeytypId"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "USP_NewGlActIns"; }
        }

        public string Create_GLAccount
        {
            get { return "USP_CreateGLAcct"; }
        }
        public override string UPDATE_SPNAME
        {
            get { return "USP_GLActUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLActdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLActget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }//uspGLActgetall
        }
        public override string TABLE_NAME
        {
            get { return "PayrollGLAccounts"; }
        } 
        
        //Procedure Added By Rajeev For BELLIN Account Setup For Fiscal Reporting
        //Added Date 15/01/10
        public string GetDistinctSubTotalGroup
        {
            get { return "uspsubtotlget"; }
        }

        public string GetAllDetailedKeyValueBySubTotalGroup
        {
            get { return "uspactbysubtot"; }
        }

        public string GetAllDetailedKeyValueBySubTotalGroupFind
        {
            get { return "uspactsubtot"; }
        }
        /*////////////////////////////////////////////////*/

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT  acct_no,acct_type,acct_desc,acct_cat,processing_seq,incr_with_crdt,subtotal_group,keyvalue,Flex_struct_Header.[desc] AcctTypeDesc,Flex_struct_Header.id v_acct_type_id,");
            sql.Append (" PayrollGLAccounts.gobzero v_gobzero,PayrollGLAccounts.active ");
            sql.Append(" FROM PayrollGLAccounts,Flex_struct_Header  where 1=1 ");
            sql.Append(" and Flex_struct_Header.accounttype=PayrollGLAccounts.acct_type");
            if (parameters[0]!= null  && parameters[0].ToString().Trim().Length > 0)
                sql.Append("  AND Rtrim(acct_type)= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1]!= null &&  parameters[1].ToString().Trim().Length > 0)
                sql.Append("  AND Rtrim(acct_desc) like '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2]!= null && parameters[2].ToString().Trim().Length > 0 )
                sql.Append("  AND Rtrim(incr_with_crdt)= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3]!= null && parameters[3].ToString().Trim().Length > 0 )
                sql.Append("  AND Rtrim(subtotal_group)= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
             if (parameters[4] != null && parameters[4].ToString().Trim().Length >0)
                 sql.Append("  AND Rtrim(keyvalue) like '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
        if (parameters[5] != null && Convert.ToInt32(parameters[5]) > 0)
                sql.Append("  AND acct_no = " + parameters[5].ToString().Trim());
            
            if (parameters[6] != null && parameters[6].ToString().Trim().Length > 0)
                    sql.Append("  AND Rtrim(acct_cat) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");


            return sql.ToString();
        }

        //Added By Rahul on 04-05-2009 for get account info not use  LIKE Keyvalue in this procedure 
        public string GetAccountInfo
        { 
          get { return "uspactinfo";}
        }
        //Added By Rahul on 17-12-2009 for get account info  
        public string FIND_LEDGER_ACCOUNT(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT acct_desc,acct_type,incr_with_crdt,keyvalue");
            sql.Append(" FROM PayrollGLAccounts where 1=1 ");
            sql.Append(" and PayrollGLAccounts.acct_type NOT IN ('U')");
            sql.Append(" order by PayrollGLAccounts.acct_type ");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    
    }
}
