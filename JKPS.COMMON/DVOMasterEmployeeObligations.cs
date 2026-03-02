using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmployeeObligations : DVOBase
    {
        private string _empl_code;
        private string _obl_code;
        private string _obl_type;
        private int _line_no;
        private decimal? _obl_rate;
        private decimal? _obl_limit;
        private int _acct_no;
        private string _department;
        private int _bal_acct_no;
        private string _bal_dept;
        private decimal? _obl_qtd1;
        private decimal? _obl_qtd2;
        private decimal? _obl_qtd3;
        private decimal? _obl_qtd4;
        private decimal? _obl_ytd;
        private decimal? _pay_limit;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _Updateby;
        private int _rowid;
        private string _acct_no_kv;
        private string _bal_acct_no_kv;
        int _acct_no_typeid;
        int _bal_acct_no_typeid;
        string _acct_no_type;
        string _bal_acct_no_type;

        private string _SSN;
        private string _typeCode;
        private string _lastName;
        private string _firstName;
        private string _empl_status;
        private string _jobCode;
        private string _jobTitle;
        private string _pay_period;
        private string _lastPay;//date
         //**************29/12/2008******************
        //Added by   :Rohit Wadhwa
        //Aim :    To get Default Obligation values from MasterOblCodes for each obligation code in employee with join
        //******************************************
       
        private string _description;
       // private string _obl_type;
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
       
        //******************************************

        #region Constructor

        public DVOMasterEmployeeObligations()
        {
            _empl_code = string.Empty;
            _obl_code = string.Empty;
            _obl_type = string.Empty;
            _line_no = 0;
            _obl_rate = null;
            _obl_limit = null;
            _acct_no = 0;
            _department = string.Empty;
            _bal_acct_no = 0;
            _bal_dept = string.Empty;
            _obl_qtd1 = null;
            _obl_qtd2 = null;
            _obl_qtd3 = null;
            _obl_qtd4 = null;
            _obl_ytd = null;
            _pay_limit = null;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//date
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";//date
            _Updateby = 0;
            _rowid = 0;
            _acct_no_kv = string.Empty;
            _bal_acct_no_kv = string.Empty;
            _acct_no_typeid = 0;
            _bal_acct_no_typeid = 0;
            _acct_no_type = string.Empty;
            _bal_acct_no_type = string.Empty;

            _SSN = string.Empty;
            _typeCode = string.Empty;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _empl_status = string.Empty;
            _jobCode = string.Empty;
            _jobTitle = string.Empty;
            _pay_period = string.Empty;
            _lastPay = "01/01/1900";//date
          //**************29/12/2008******************
        //Added by   :Rohit Wadhwa
        //Aim :    To get Default Obligation values from MasterOblCodes for each obligation code in employee with join
        //******************************************
            _description = string.Empty;
           // _obl_type = string.Empty;
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
           //*******************************************
        }

        #endregion Constructor

        #region public properties

        public int Rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string obl_code
        {
            get { return _obl_code; }
            set { _obl_code = value; }
        }
        public string obl_type
        {
            get { return _obl_type; }
            set { _obl_type = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public decimal? obl_rate
        {
            get { return _obl_rate; }
            set { _obl_rate = value; }
        }
        public decimal? obl_limit
        {
            get { return _obl_limit; }
            set { _obl_limit = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public int bal_acct_no
        {
            get { return _bal_acct_no; }
            set { _bal_acct_no = value; }
        }
        public string bal_dept
        {
            get { return _bal_dept; }
            set { _bal_dept = value; }
        }
        public decimal? obl_qtd1
        {
            get { return _obl_qtd1; }
            set { _obl_qtd1 = value; }
        }
        public decimal? obl_qtd2
        {
            get { return _obl_qtd2; }
            set { _obl_qtd2 = value; }
        }
        public decimal? obl_qtd3
        {
            get { return _obl_qtd3; }
            set { _obl_qtd3 = value; }
        }
        public decimal? obl_qtd4
        {
            get { return _obl_qtd4; }
            set { _obl_qtd4 = value; }
        }
        public decimal? obl_ytd
        {
            get { return _obl_ytd; }
            set { _obl_ytd = value; }
        }
        public decimal? pay_limit
        {
            get { return _pay_limit; }
            set { _pay_limit = value; }
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
        public int Updateby
        {
            get { return _Updateby; }
            set { _Updateby = value; }
        }

        public string acct_no_kv
        {
            get { return _acct_no_kv; }
            set { _acct_no_kv = value; }
        }
        public string bal_acct_no_kv
        {
            get { return _bal_acct_no_kv; }
            set { _bal_acct_no_kv = value; }
        }
        public int acct_no_typeid
        {
            get { return _acct_no_typeid; }
            set { _acct_no_typeid = value; }
        }
        public int bal_acct_no_typeid
        {
            get { return _bal_acct_no_typeid; }
            set { _bal_acct_no_typeid = value; }
        }
        public string acct_no_type
        {
            get { return _acct_no_type; }
            set { _acct_no_type = value; }
        }
        public string bal_acct_no_type
        {
            get { return _bal_acct_no_type; }
            set { _bal_acct_no_type = value; }
        }

        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }
        public string typeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string empl_status
        {
            get { return _empl_status; }
            set { _empl_status = value; }
        }
        public string jobCode
        {
            get { return _jobCode; }
            set { _jobCode = value; }
        }
        public string jobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }
        public string pay_period
        {
            get { return _pay_period; }
            set { _pay_period = value; }
        }
        public string lastPay
        {
            get { return _lastPay; }
            set { _lastPay = value; }
        }
          //**************29/12/2008******************
        //Added by   :Rohit Wadhwa
        //Aim :    To get Default Obligation values from MasterOblCodes for each obligation code in employee with join
        //******************************************
        
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        //public string obl_type
        //{
        //    get { return _obl_type; }
        //    set { _obl_type = value; }
        //}
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
      
      
        
        //*******************************************
        #endregion public properties

        #region Stored-Procedures
        
        public  string Emplobldefaultsget
        {
            get { return "USP_EmpOblRef"; }
        }
        public string FIND_EMPLOBLDEFAULTSGET_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,");
            sql.Append(" MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,");
            sql.Append(" MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,");
            sql.Append(" MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,");
            sql.Append(" MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,");
            sql.Append(" MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,");
            sql.Append(" MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept,MasterEmployeeObligations.empl_code,");
            sql.Append(" MasterEmployee.soc_sec_num");
            sql.Append(" from MasterEmployeeObligations, MasterOblCodes,MasterEmployee");
            sql.Append(" where MasterEmployeeObligations.obl_code = MasterOblCodes.obl_code");
            sql.Append(" and MasterEmployeeObligations.empl_code = MasterEmployee.empl_code");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and MasterEmployeeObligations.empl_code in (" + parameters[0].ToString().Trim() + ")");
            //sql.Append(" and MasterEmployeeObligations.empl_code in (SELECT MasterEmployee.empl_code FROM MasterEmployee WHERE 1=1");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.soc_sec_num) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[3].ToString().Trim() != string.Empty)
            //    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.type_code) matches '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_title) = '" + parameters[6].ToString().Replace("'", "''") + "'");
            //if (parameters[7] != null)
            //    if (parameters[7].ToString().Trim() != string.Empty)
            //        sql.Append(" AND MasterEmployee.pay_period = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and not exists (select empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
            //// add check for termination date
            //if (parameters[8] != null)
            //    if (parameters[8].ToString().Trim() != string.Empty && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
            //    {
            //        sql.Append(" and (MasterEmployee.terminated is null ");
            //        sql.Append(" or MasterEmployee.terminated >='" + parameters[8].ToString().Trim() + "')");
            //    }
            //sql.Append(" )");
            sql.Append(" order by MasterEmployeeObligations.line_no");

            return sql.ToString();
        }
        //*******************************************
        public override string INSERT_SPNAME
        {
            get { return "USP_EmpOblCodIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpOblUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpOblDel"; }
        }

        public string DELETE_LINE
        {
            get { return "USP_EmpOblLnDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspEmpOblCodGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_EmpOblGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterEmployeeObligations"; }
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
        public string GET_ROWID
        {
            get { return "USP_PayOd_rowid"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT empl_code p_empl_code,obl_code p_obl_code,line_no p_line_no,obl_rate p_obl_rate,obl_limit p_obl_limit,");
            sql.Append(" MasterEmployeeObligations.acct_no p_acct_no,department p_department,bal_acct_no p_bal_acct_no,bal_dept p_bal_dept,");
            sql.Append(" obl_qtd1 p_obl_qtd1,obl_qtd2 p_obl_qtd2,obl_qtd3 p_obl_qtd3,obl_qtd4 p_obl_qtd4,obl_ytd p_obl_ytd,");
            sql.Append(" pay_limit p_pay_limit,tr1.keyvalue acct_no_kv,tr2.keyvalue bal_acct_no_kv,");
            sql.Append(" kh1.id acct_no_typeid,kh2.id bal_acct_no_typeid,tr1.acct_type,tr2.acct_type");
            sql.Append(" FROM (MasterEmployeeObligations LEFT Outer JOIN (PayrollGLAccounts tr1 INNER JOIN Flex_struct_Header kh1 ON tr1.acct_type=kh1.accounttype ) ON MasterEmployeeObligations.acct_no=tr1.acct_no) LEFT OUTER JOIN(PayrollGLAccounts tr2 INNER JOIN Flex_struct_Header kh2 ON tr2.acct_type=kh2.accounttype) ON MasterEmployeeObligations.bal_acct_no=tr2.acct_no");
            sql.Append(" WHERE  1=1  ");
            sql.Append(" ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND line_no = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(obl_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND obl_rate = " + parameters[3].ToString().Trim());
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND obl_limit = " + parameters[4].ToString().Trim());
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND MasterEmployeeObligations.acct_no = " + parameters[5].ToString().Trim());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(department) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND bal_acct_no = " + parameters[7].ToString().Trim());
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bal_dept) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND obl_qtd1 = " + parameters[9].ToString().Trim());
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND obl_qtd2 = " + parameters[10].ToString().Trim());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND obl_qtd3 = " + parameters[11].ToString().Trim());
            if (Convert.ToInt32(parameters[12]) > 0)
                sql.Append(" AND obl_qtd4 = " + parameters[12].ToString().Trim());
            if (Convert.ToInt32(parameters[13]) > 0)
                sql.Append(" AND obl_ytd = " + parameters[13].ToString().Trim());
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND pay_limit = " + parameters[14].ToString().Trim());

            if ((parameters[15] == null || parameters[15].ToString() == string.Empty) &&//_SSN
                (parameters[16] == null || parameters[16].ToString() == string.Empty) &&//_typeCode
                (parameters[17] == null || parameters[17].ToString() == string.Empty) &&//_lastName
                (parameters[18] == null || parameters[18].ToString() == string.Empty) &&//_firstName
                (parameters[19] == null || parameters[19].ToString() == string.Empty) &&//_empl_status
                (parameters[20] == null || parameters[20].ToString() == string.Empty) &&//_jobCode
                (parameters[21] == null || parameters[21].ToString() == string.Empty) &&//_jobTitle
                (parameters[22] == null || parameters[22].ToString() == string.Empty) &&//_pay_period
                (parameters[23] == null || parameters[23].ToString() == string.Empty || parameters[23].ToString().Trim() == "01/01/1900"))//_lastPay
            {
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            }
            else
            {
                sql.Append(" AND RTRIM(empl_code) IN (");
                sql.Append(" SELECT empl_code FROM MasterEmployee WHERE 1=1 ");
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[15] != null)
                    if (parameters[15].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[16] != null)
                    if (parameters[16].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[17] != null)
                    if (parameters[17].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[17].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[18] != null)
                    if (parameters[18].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[18].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[19] != null)
                    if (parameters[19].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[20] != null)
                    if (parameters[20].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Code) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[21] != null)
                    if (parameters[21].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Title) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[22] != null)
                    if (parameters[22].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[23] != null)
                    if (parameters[23].ToString().Trim() != string.Empty && !parameters[23].ToString().Trim().Contains("1900"))
                        sql.Append(" AND last_pay = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" )");
            }
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
