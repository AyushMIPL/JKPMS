using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPaySlipsA4:DVOBase 
    {
        private string _Account_Type_For_Department;
        private string _Department;
        private string _EmployeeType;

        public DVOPaySlipsA4()
        {
            _Account_Type_For_Department = string.Empty;
            _Department = string.Empty;
            _EmployeeType = string.Empty;
        }
        public  string Account_Type_For_Department
        {
            get { return _Account_Type_For_Department; }
            set { _Account_Type_For_Department = value; }
        }

        public  string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        public  string EmployeeType
        {
            get { return _EmployeeType; }
            set { _EmployeeType = value; }
        }




        #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return ""; }
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
        public string FIND_DETAIL
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
            sql.Append(" select stypddrd.doc_no,MasterBanks.bank_desc, MasterBanks.dd_create,");
            sql.Append(" MasterEmployee.first_name,MasterEmployee.flexdeptaccttype, MasterEmployee.last_name, MasterEmployee.middle_name,");
            sql.Append(" Process_PayEmployee.cash_amount, Process_PayEmployee.check_no,Process_PayEmployee.deposit,Process_PayEmployee.doc_no,");
            sql.Append(" Process_PayEmployee.empl_code,Process_PayEmployee.eop_date,Process_PayEmployee.pay_date,stypddrd.amount");
            sql.Append(" from stypddrd, stypddre, MasterBanks, Process_PayEmployee,  MasterEmployee where");
            sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code AND");
            sql.Append(" Process_PayEmployee.doc_no = stypddrd.pay_doc_no AND stypddrd.doc_no= stypddre.doc_no");
            sql.Append(" AND stypddre.bank_code = MasterBanks.bank_code and Process_PayEmployee.ok_to_post = 'Y'");
            sql.Append(" AND Process_PayEmployee.print_check = 'N'");
            sql.Append(" order by MasterBanks.dd_create, Process_PayEmployee.empl_code, stypddrd.doc_no, Process_PayEmployee.pay_date");

            

            //sql.Append("SELECT code p_code,per_annum p_per_annum, ");
            //sql.Append("rowid p_rowid ");
            //sql.Append(" from inyscale  where 1=1 ");
            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND code LIKE '" + parameters[0].ToString() + "%'");
            //if (Convert.ToDecimal(parameters[1]) != 0)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND per_annum =" + parameters[1].ToString().Trim());

            //if (Convert.ToInt32(parameters[2]) > 0)
            //    sql.Append(" AND rowid=" + Convert.ToInt32(parameters[2]));

            //sql.Append(" order by code");


            return sql.ToString();
        }

        #endregion Stored-Procedures



    }
}
