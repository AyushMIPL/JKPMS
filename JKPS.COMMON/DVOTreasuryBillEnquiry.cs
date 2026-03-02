using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 06 June 2009
    /// Description :common class for treasury bill enquiry
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTreasuryBillEnquiry:DVOBase
    {
        private int _Rowid;
        private int _issue_no;
        private DateTime _issue_date;
        private DateTime _redeem_date;
        private decimal _amt_per_100;
        private string _status;
        private decimal _amt_tender;
        private string _tend_code;
        private string _class_code;
        private string _tend_name;
        private string _tend_class;
        private string _address1;
        private string _address2;
        private string _city;
        private string _contact;
        private string _phone;
        private string _fax;
        private string _email;
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;

        private string _tbschname;
        private int _TotalRecords;
        private string _bill_status;

        #region Constructor
        public DVOTreasuryBillEnquiry()
        {
            _Rowid = 0;
            _issue_no = 0;
            _issue_date = Convert.ToDateTime("01/01/1900"); ;
            _redeem_date = Convert.ToDateTime("01/01/1900"); ;
            _amt_per_100 = 0;
            _status = string.Empty;
            _bill_status = string.Empty;
            _amt_tender = 0;
            _tend_code=string.Empty;
            _class_code = string.Empty;
            _tend_name=string.Empty;
            _tend_class=string.Empty;
            _address1=string.Empty;
            _address2=string.Empty;
            _city=string.Empty;
            _contact=string.Empty;
            _phone=string.Empty;
            _fax=string.Empty;
            _email=string.Empty;
            _Insertby=0;
            _InsertDate=Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo=string.Empty;
            _UpdateBy=0;
            _Updatedate=Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo=string.Empty;
            _tbschname =string.Empty;
            _TotalRecords = 0;
        }
        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int issue_no
        {
            get { return _issue_no; }
            set { _issue_no = value; }
        }
        public DateTime issue_date
        {
            get { return _issue_date; }
            set { _issue_date = value; }
        }
        public DateTime redeem_date
        {
            get { return _redeem_date; }
            set { _redeem_date = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string bill_status
        {
            get { return _bill_status; }
            set { _bill_status = value; }
        }
        public decimal amt_tender
        {
            get { return _amt_tender; }
            set { _amt_tender = value; }
        }
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string class_code
        {
            get { return _class_code; }
            set { _class_code = value; }
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
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public string tbschname
        {
            get { return _tbschname; }
            set { _tbschname = value; }
        }
        public int TotalRecords
        {
            get { return _TotalRecords ; }
            set { _TotalRecords = value; }
        }

        #endregion public properties

        #region Stored-Procedures
        //For check pay out  ******************
        public string CHECK_NEXT_ISSUENO
        {
            get { return "usp_next_issueno";}
        }
        public string GET_CURRENT_ISSUENO
        {
            get { return "usp_curr_issueno"; }
        }
        public string GET_MAX_ISSUENO
        {
            get { return "usp_max_issueno"; }
        }
        public string GET_MAX_ISSUENOSTATUS
        {
            get { return "usp_max_status"; }
        }
        //Added by SunilPahwa 
        public string GET_ClASS_BFR_DELETE
        {
            get { return "usptbclaschkbfrdel"; }
        }  
        
        //***********************************
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
        //public string GET_Tender_List
        //{
        //    get { return "usp_get_tend_list"; }
        //}
        public override string TABLE_NAME
        {
            get { return "tbclients"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tend_code,tend_name,tend_class,address1,address2,city ,contact,phone,fax,email ,country ,rowid from tbclients where 1=1");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null && parameters[0].ToString() != "0")//tend_code
                sql.Append(" AND tbclients.tend_code =" + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//tend_class
                sql.Append(" AND tbclients.tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null && parameters[2].ToString() != "0")//tend_name
                sql.Append(" AND tbclients.tend_name LIKE '" + parameters[2].ToString().Replace("'", "''") + "%'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//address1
                sql.Append(" AND tbclients.address1 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//address2
                sql.Append(" AND tbclients.address2 LIKE '" + parameters[4].ToString().Replace("'", "''") + "%'");
            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//contact
                sql.Append(" AND tbclients.contact LIKE '" + parameters[5].ToString().Replace("'", "''") + "%'");
            if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)//phone
                sql.Append(" AND tbclients.phone LIKE '" + parameters[6].ToString().Replace("'", "''") + "%'");
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//fax
                sql.Append(" AND tbclients.fax LIKE '" + parameters[7].ToString().Replace("'", "''") + "%'");
            if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//email
                sql.Append(" AND tbclients.email LIKE '" + parameters[8].ToString().Replace("'", "''") + "%'");
            sql.Append("  Order By rowid ");
            return sql.ToString();
        }

        public string GET_DETAIL(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissued.issue_num,tbschemes.tbschname,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append(" tbissued.amt_per_100,tbissuer.status,tbissued.amt_issued ,tbissued.bill_status");
            sql.Append(" from tbclasses,tbclients,tbissued,tbissuer,tbschemes where tbclasses.class_code=tbclients.tend_class");
            sql.Append(" and tbclients.tend_code=tbissued.tend_code and tbissued.issue_num=tbissuer.issue_num");
            sql.Append(" and tbissued.tbschid=tbschemes.tbschid ");
            sql.Append(" and tbissued.tbschid=tbissuer.tbschid ");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//tend_code
                sql.Append(" AND tbclients.tend_code =" + parameters[0].ToString());          
            return sql.ToString();
        }

      
        #endregion store-procedures
    }
}
