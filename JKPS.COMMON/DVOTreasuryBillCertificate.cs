using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTreasuryBillCertificate:DVOBase
    {
        
        private int _doc_no;
        int _tbschid;
        private DateTime _issue_date;
        private int _issue_num;
        private decimal _amt_issued;
        private int _line_no;
        private int _tend_code;
        private int _tend_codemax;


        #region Constructor
        public DVOTreasuryBillCertificate()
        {
            _doc_no = 0;
            _tbschid = 0;
            _issue_date = Convert.ToDateTime(null);
            _issue_num = 0;
            _amt_issued = 0.0M;
            _line_no = 0;
            _tend_code = 0;
            _tend_codemax = 0;
        }
        #endregion Constructor


        #region properties
        public Int32 doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }//
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public DateTime issue_date
        {
            get { return _issue_date; }
            set { _issue_date = value; }
        }

        public Int32 issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }

        public decimal amt_issued
        {
            get { return _amt_issued; }
            set { _amt_issued = value; }
        }

        public Int32 line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }

        public int tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }

        public int tend_codemax
        {
            get { return _tend_codemax; }
            set { _tend_codemax = value; }
        }



        #endregion properties


        #region StoredProcedure

        public string GET_TREASURY_BILL_CERTIFICATE
        {
            get { return "usptreasurbillget1"; }//uspPaystypayddget//usptreasurbillget1
        }


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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT tbissuer.issue_date,tbissuer.issue_num,tbissued.amt_issued,");
            sql.Append(" tbissued.tend_code,tbissuer.tbschid,tbschemes.tbschname, tbclients.tend_code ");
            sql.Append(" FROM tbclients, tbissuer, tbissued,tbschemes");
            sql.Append(" WHERE tbissuer.issue_num = tbissued.issue_num");
            sql.Append(" AND tbissued.tend_code= tbclients.tend_code and tbissuer.tbschid=tbschemes.tbschid");
            //Added by Rahul On 13/11/2009 to restrict canceled tenders *************
            sql.Append(" AND tbissued.bill_status <> 'C' ");
            //**********
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND tbissuer.issue_num = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND tbissued.tend_code >=  " + parameters[1].ToString().Trim().Replace("'", "''") + "");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND tbissued.tend_code <=  " + parameters[2].ToString().Trim().Replace("'", "''") + "");
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)
                    sql.Append(" AND tbissuer.tbschid = " + parameters[3].ToString());
            sql.Append(" order by tbclients.tend_code ");
            return sql.ToString();
        }


        #endregion StoredProcedure
    }
}
