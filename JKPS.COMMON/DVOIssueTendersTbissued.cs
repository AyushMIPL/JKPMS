using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa on 24/10/09
    public class DVOIssueTendersTbissued : DVOBase
    {
        private int _rowid;
        private int _issue_num;
        private string _tend_code;
        private decimal _amt_applied_for;
        private string _bill_status;
        private decimal _amt_issued;
        private decimal _amt_per_100;
        private int _tbschid;
        private int _insertby;
        private DateTime _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private DateTime _updatedate;
        private string _updatemachineinfo;





        private string _tend_name;
        private string _tend_class;
        private string _address1;
        private string _address2;
        private string _city;
        private string _contact;
        private string _phone;
        private string _fax;
        private string _email;

        private string _tbschname;




        public DVOIssueTendersTbissued()
        {
            _rowid = 0;
            _issue_num = 0;
            _tend_code = string.Empty;
            _amt_applied_for = 0;
            _bill_status = string.Empty;
            _amt_issued = 0;
            _amt_per_100 = 0;
            _tbschid = 0;
            _insertby = 0;
            _insertdate = Convert.ToDateTime("01/01/1900");
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = Convert.ToDateTime("01/01/1900");
            _updatemachineinfo = string.Empty;


            _tend_name = string.Empty;
            _tend_class = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _fax = string.Empty;
            _email = string.Empty;

            _tbschname = string.Empty;

        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public decimal amt_applied_for
        {
            get { return _amt_applied_for; }
            set { _amt_applied_for = value; }
        }

        public string bill_status
        {
            get { return _bill_status; }
            set { _bill_status = value; }
        }
        public decimal amt_issued
        {
            get { return _amt_issued; }
            set { _amt_issued = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public DateTime insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }

        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public DateTime updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }




        public string tend_name
        {
            get { return _tend_name; }
            set { _tend_name = value; }
        }
        public string tend_class
        {
            get { return _tend_class; }
            set { _tend_class = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string tbschname
        {
            get { return _tbschname; }
            set { _tbschname = value; }
        }


        public override string INSERT_SPNAME
        {
            get { return "usptbissuedtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usptbissuedtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usptbissuedtldel"; }
        }

        public string CANCEL_TENDER
        {
            get { return "usptndrcncl"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usptbtottendget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "tbissued"; }
        }
        public string TENDER_RECEIVED_AMOUNT
        {
            get { return "usptndrcvamt"; }
        }
        public string TENDER_UNPOSTED_RECEIVED_AMOUNT
        {
            get { return "usptndunpstrcvamt"; }
        }
        public string ALL_TENDERS_IN_ISSUE
        {
            get { return "uspaltndisu"; }
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
            sql.Append(" select tbissued.rowid,tbissued.amt_applied_for,tbissued.bill_status, ");
            sql.Append(" tbissued.amt_issued,tbissued.amt_per_100,tbissued.tbschid,tbclients.tend_code, ");
            sql.Append(" tbclients.tend_name, tbclients.tend_class,tbclients.address1, tbclients.address2, ");
            sql.Append(" tbclients.city, tbclients.contact,tbclients.phone, tbclients.fax,tbclients.email, ");
            sql.Append(" tbclients.country,tbissued.issue_num from tbissued,outer tbclients");
            sql.Append(" where tbissued.tend_code=tbclients.tend_code  ");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    if (Convert.ToDecimal(parameters[0]) > 0)
                        sql.Append(" AND tbissued.amt_per_100=" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    if (Convert.ToDecimal(parameters[1]) > 0)
                        sql.Append(" AND tbissued.amt_applied_for=" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[2]) > 0)
                        sql.Append(" AND tbissued.rowid=" + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[3]) > 0)
                        sql.Append(" AND tbissued.tbschid=" + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[4]) > 0)
                        sql.Append(" AND tbissued.issue_num=" + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    sql.Append(" AND tbissued.tend_code = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        public string FIND_QUERY_2(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select tbissued.tbschid,tbissued.issue_num,tbissued.tend_code,  ");
            sql.Append(" tbissued.amt_applied_for,tbissued.amt_per_100 ,tbrecd.amt_rec, ");
            sql.Append(" tbrecd.amt_bal,tbrecd.insertdate,tbrecd.ar_cr_doc_no,tbschemes.tbschname, ");
            sql.Append(" tbclients.tend_name  from tbissued,tbrecd,outer tbschemes, ");
            sql.Append(" outer tbclients where tbissued.tbschid=tbrecd.tbschid and  ");
            sql.Append(" tbissued.issue_num=tbrecd.issue_num and tbissued.tend_code=tbrecd.tend_code and ");
            sql.Append(" tbissued.tbschid=tbschemes.tbschid and tbclients.tend_code=tbissued.tend_code ");
            sql.Append(" and tbissued.bill_Status <> 'C' and tbrecd.ok_to_post <> 'C' ");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[0]) > 0)
                        sql.Append(" AND tbissued.tbschid=" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[1]) > 0)
                        sql.Append(" AND tbissued.issue_num=" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND tbissued.tend_code = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
    }
}
