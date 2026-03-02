using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOARCashProcessingDetailStrcashd : DVOBase
    {
        private int _doc_no;
        private int _inv_doc_no;
        private string _inv_no;
        private string _due_date;
        private int _dist_acct;
        private string _dist_department;
        private decimal _dist_amt;
        private string _dist_deb_cred;
        private string _mtax_code;
        private decimal _goods_amt;
        private int _disc_acct;
        private string _disc_department;
        private decimal _disc_amt;
        private string _disc_deb_cred;
        private int _RowId;

        #region Constructor

        public DVOARCashProcessingDetailStrcashd()
        {
            _doc_no = 0;
            _inv_doc_no = 0;
            _inv_no = string.Empty;
            _due_date = string.Empty;
            _dist_acct = 0;
            _dist_department = string.Empty;
            _dist_amt = 0;
            _dist_deb_cred = string.Empty;
            _mtax_code = string.Empty;
            _goods_amt = 0;
            _disc_acct = 0;
            _disc_department = string.Empty;
            _disc_amt = 0;
            _disc_deb_cred = string.Empty;
            _RowId = 0;
        }

        #endregion Constructor

        #region public properties

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public string due_date
        {
            get { return _due_date; }
            set { _due_date = value; }
        }
        public int dist_acct
        {
            get { return _dist_acct; }
            set { _dist_acct = value; }
        }
        public string dist_department
        {
            get { return _dist_department; }
            set { _dist_department = value; }
        }
        public decimal dist_amt
        {
            get { return _dist_amt; }
            set { _dist_amt = value; }
        }
        public string dist_deb_cred
        {
            get { return _dist_deb_cred; }
            set { _dist_deb_cred = value; }
        }
        public string mtax_code
        {
            get { return _mtax_code; }
            set { _mtax_code = value; }
        }
        public decimal goods_amt
        {
            get { return _goods_amt; }
            set { _goods_amt = value; }
        }
        public int disc_acct
        {
            get { return _disc_acct; }
            set { _disc_acct = value; }
        }
        public string disc_department
        {
            get { return _disc_department; }
            set { _disc_department = value; }
        }
        public decimal disc_amt
        {
            get { return _disc_amt; }
            set { _disc_amt = value; }
        }
        public string disc_deb_cred
        {
            get { return _disc_deb_cred; }
            set { _disc_deb_cred = value; }
        }
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspArCashDtlIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usparcashdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usparcashdtldel"; }
        }

        public string DELETEALL_SPNAME
        {
            get { return "usparcashdtlalldel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspArCashDtlGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "strcashd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string INS_NSSTOAR_CASHD
        {
            get { return "uspnssarcashdins"; }
        }
        
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT strcashd.doc_no p_doc_no,inv_doc_no p_inv_doc_no,inv_no p_inv_no,due_date p_due_date,dist_acct p_dist_acct,");
            sql.Append(" dist_department p_dist_department,dist_amt p_dist_amt,dist_deb_cred p_dist_deb_cred,");
            sql.Append(" mtax_code p_mtax_code,goods_amt p_goods_amt,disc_acct p_disc_acct,disc_department v_disc_department,");
            sql.Append(" disc_amt p_disc_amt,disc_deb_cred p_disc_deb_cred,strcashd.RowId v_RowId");
            sql.Append(" FROM strcashd,strcashe WHERE strcashd.doc_no=strcashe.doc_no");

            if (Convert.ToInt32(parameters[0]) != 0)
                sql.Append(" AND strcashd.doc_no = " + parameters[0].ToString().Trim());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND inv_doc_no = " + parameters[1].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(inv_no) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && Convert.ToDateTime(parameters[3].ToString())!=Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND due_date = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND dist_acct = " + parameters[4].ToString().Trim());
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND dist_amt = " + parameters[5].ToString().Trim());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dist_deb_cred) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND disc_acct = " + parameters[7].ToString().Trim());
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND disc_amt = " + parameters[8].ToString().Trim());
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(disc_deb_cred) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mtax_code) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND goods_amt = " + parameters[11].ToString().Trim());
            if (Convert.ToInt32(parameters[12]) > 0)
                sql.Append(" AND strcashd.rowId = " + parameters[12].ToString().Trim());

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
