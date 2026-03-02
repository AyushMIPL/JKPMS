using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLPayrollGLAccounts : DVOBase
    {
        private int _RowID;
        private int _acct_no;
        private string _acct_type;
        private string _acct_desc;
        private string _acct_cat;
        private string _processing_seq;
        private string _incr_with_crdt;
        private string _subtotal_group;
        private string _keyvalue;
        private Int32 _gobzero;
        private Int32 _active;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        #region Constructor
        public DVOGLPayrollGLAccounts()
        {
            _RowID = 0;
            _acct_no = 0;
            _acct_type = string.Empty;
            _acct_desc = string.Empty;
            _acct_cat = string.Empty;
            _processing_seq = string.Empty;
            _incr_with_crdt = string.Empty;
            _subtotal_group = string.Empty;
            _keyvalue = string.Empty;
            _gobzero = 0;
            _active = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";// date
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";// date
            _updatemachineinfo = string.Empty;

        }
        #endregion Constructor

        #region Property
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }
        public string processing_seq
        {
            get { return _processing_seq; }
            set { _processing_seq = value; }
        }
        public string incr_with_crdt
        {
            get { return _incr_with_crdt; }
            set { _incr_with_crdt = value; }
        }
        public string subtotal_group
        {
            get { return _subtotal_group; }
            set { _subtotal_group = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        public int gobzero
        {
            get { return _gobzero; }
            set { _gobzero = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

        #endregion Property

        #region Stored-Procedures

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
            get { return "uspPayrollGLAccountsGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "PayrollGLAccounts"; }
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
        public string GET_KEYVALUE
        {
            get { return "uspkeyvalueget"; }
        }
        //**********Added by Sunil*************
        public string UPDATE_ACCOUNT_INFO
        {
            get { return "uspinquireglbalupd"; }
        }
        //*************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid v_rowid, acct_no v_acct_no,acct_type v_acct_type,acct_desc v_acct_desc,acct_cat v_acct_cat,");
            sql.Append(" processing_seq v_processing_seq,incr_with_crdt v_incr_with_crdt,subtotal_group v_subtotal_group,keyvalue v_keyvalue");
            sql.Append(" FROM PayrollGLAccounts");
            sql.Append(" WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) != 0)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND acct_no = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_type) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_desc) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(subtotal_group) =  '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(keyvalue) =  '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(incr_with_crdt) =  '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //********Added By Sunil Pahwa****************************************************************
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND rowid = " + parameters[6].ToString());

            return sql.ToString();
        }

        public string GET_INQUIRE_GL_ACCOUNT_BALANCE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid v_rowid, acct_no v_acct_no,acct_type v_acct_type,acct_desc v_acct_desc,acct_cat v_acct_cat,");
            sql.Append(" processing_seq v_processing_seq,incr_with_crdt v_incr_with_crdt,subtotal_group v_subtotal_group,keyvalue v_keyvalue");
            sql.Append(" FROM PayrollGLAccounts");
            sql.Append(" WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) != 0)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND acct_no = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_type) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_desc) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND acct_cat= '" + parameters[3].ToString().Trim() + "'");


            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(subtotal_group) =  '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(keyvalue) =  '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(incr_with_crdt) =  '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND rowid = " + parameters[7].ToString());

            return sql.ToString();
        }

        //Added By Rahul Jain on 24/12/2009 using in Account verification report
        //started using in payroll -Rohit from 27/01/2010
        public string GET_GL_ACCOUNTS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.acct_no,PayrollGLAccounts.acct_type,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc ");
            //sql.Append(" ");
            sql.Append(" FROM PayrollGLAccounts");
            sql.Append(" WHERE 1=1 and acct_cat NOT IN('U')");
            sql.Append(" order by PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_no ");
            return sql.ToString();
        }

        public string FindLedgerAcctDtl(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT acct_desc,acct_type,incr_with_crdt,keyvalue,acct_no,acct_cat,");
            sql.Append(" processing_seq,subtotal_group,gobzero,active");
            sql.Append(" FROM PayrollGLAccounts");
            sql.Append(" WHERE acct_cat <> 'U' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND acct_no = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_type) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_desc) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(subtotal_group) Like  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(keyvalue) Like  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(incr_with_crdt) =  '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    if (Convert.ToInt32(parameters[6]) > 0)
                        sql.Append(" AND gobzero =  " + parameters[6].ToString().Trim());

            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND active =  " + parameters[7].ToString().Trim());

            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_cat) =  '" + parameters[8].ToString().Trim() + "'");

            return sql.ToString();
        }

        public string GET_Flex_struct_Header_curs
        {
            get { return "uspFlex_struct_Header_curs"; }
        }
        public string GET_Flex_struct_Details_curs
        {
            get { return "uspFlex_struct_Details_curs"; }
        }
        public string GET_Master_Segment_curs
        {
            get { return "uspMaster_Segment_curs"; }
        }
        //get count by acct_no
        public string GET_inxchrtd_curs
        {
            get { return "uspinxchrtd_curs"; }
        }
        //delete account from PayrollGLAccounts by acct_no
        public string DELETE_PayrollGLAccounts_ACCT
        {
            get { return "uspPayrollGLAccountsdel"; }
        }
        //delete account from ingfkvad by acct_no
        public string DELETE_ingfkvad_ACCT
        {
            get { return "uspingfkvaddel"; }
        }
        //get data from Master_Segment by keyvalue,segmentid and issubto
        public string GET_Master_Segment1_curs
        {
            get { return "uspMaster_Segment1_curs"; }
        }
        //get data from Master_Segment by id
        public string GET_Master_Segment2_stmt
        {
            get { return "uspMaster_Segment2_stmt"; }
        }
        public string GET_ingfkvad1_curs
        {
            get { return "uspingfkvad1_curs"; }
        }
        public string GET_ingfkvad2_stmt
        {
            get { return "uspingfkvad2_curs"; }
        }
        //update keyvalue
        public string UPDATE_PayrollGLAccounts
        {
            get { return "uspstxchtrupd"; }
        }
        public string INSERT_Flex_struct_Header
        {
            get { return "uspingfkvadins"; }
        }
        public string GetGLAccounts()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.acct_no,PayrollGLAccounts.acct_type,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,PayrollGLAccounts.incr_with_crdt ");
            sql.Append(" FROM PayrollGLAccounts");
            sql.Append(" WHERE acct_cat<>'U'");
            return sql.ToString();
        }
        //-----------------------------------------------------------------------
        #endregion Stored-Procedures
    }

}
