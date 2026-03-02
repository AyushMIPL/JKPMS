using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPrintSummaryAnalysis : DVOBase
    {


        private string _paydate;
        private string _startdate;
        private string _Enddate;
        private string _act_code;
        private string _act_type;
        private string _empl_code;
        private string _last_name;
        private string _first_name;
        private string _type_code;
        private string _job_code;
        private string _job_title;
        private string _pay_period;
        private string _emp_status;
        private int _doc_no;
        private string _ref_code;


        public DVOPrintSummaryAnalysis()
        {
            _paydate = string.Empty;
            _startdate = string.Empty;
            _Enddate = string.Empty;
            _act_code = string.Empty;
            _act_type = string.Empty;
            _empl_code = string.Empty;
            _last_name = string.Empty;
            _first_name = string.Empty;
            _type_code = string.Empty;
            _job_code = string.Empty;
            _job_title = string.Empty;
            _pay_period = string.Empty;
            _emp_status = string.Empty;

            _doc_no = 0;
            _ref_code = string.Empty;

        }

        public string paydate
        {
            get { return _paydate ; }
            set { _paydate  = value; }

        }
        public string startdate
        {
            get { return _startdate ; }
            set { _startdate  = value; }

        }
        public string Enddate
        {
            get { return _Enddate ; }
            set { _Enddate  = value; }

        }
       public string act_code
       {
           get { return _act_code; }
           set { _act_code = value; }

       }

       public string act_type
       {
           get { return _act_type; }
           set { _act_type = value; }

       }
       public string empl_code
       {
           get { return _empl_code; }
           set { _empl_code = value; }

       }

       public string last_name
       {
           get { return _last_name; }
           set { _last_name = value; }

       }

       public string first_name
       {
           get { return _first_name; }
           set { _first_name = value; }

       }

       public string type_code
       {
           get { return _type_code; }
           set { _type_code = value; }

       }

       public string job_code
       {
           get { return _job_code; }
           set { _job_code = value; }

       }
       public string job_title
       {
           get { return _job_title ; }
           set { _job_title  = value; }

       }

       public string pay_period
       {
           get { return _pay_period; }
           set { _pay_period = value; }

       }

       public string emp_status
       {
           get { return _emp_status; }
           set { _emp_status = value; }

       }

         
         public int  doc_no
       {
           get { return _doc_no; }
           set { _doc_no = value; }

       }
      
         public string ref_code
       {
           get { return _ref_code; }
           set { _ref_code = value; }

       }

        //**************
        public string GET_DESCRIPTION
        {
            get { return "uspDtlAnsECodeGet"; }//uspdtlansecodeget
        }
        //***************
        public string GET_YtoD
        {
            get { return "uspytodget"; }//uspdtlansecodeget
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
        public string GET_EMPL_INFO_55
        {
            get { return "uspempinfoovr55"; }
        }

      //stytranr.pay_date

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select stxtranr.ref_code, styactvd.act_code, styactvd.act_type,");
            sql.Append(" styactvd.amount,styactvd.doc_no, MasterEmployee.first_name,MasterEmployee.last_name,");
            sql.Append(" MasterEmployee.middle_name, stytranr.pay_date from styactvd, stytranr, ");
            sql.Append(" stxtranr, MasterEmployee Where stxtranr.doc_no = stytranr.doc_no and");
            sql.Append(" styactvd.doc_no = stytranr.doc_no and stxtranr.ref_code = MasterEmployee.empl_code and ");
            sql.Append(" stxtranr.orig_journal = 'PY' and styactvd.act_type != 'E' and styactvd.amount != 0");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" AND stytranr.pay_date >= '" + parameters[0] + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND stytranr.pay_date <= '" + parameters[1] + "'");


            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(styactvd.act_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND styactvd.act_type= '" + parameters[3].ToString().Trim() + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND MasterEmployee.empl_code='" + parameters[4].ToString().Trim() +"'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.type_code) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.job_code) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND MasterEmployee.job_title='" + parameters[9].ToString().Trim()+"'");
            //if (parameters[8] != null)
            //    if (parameters[8].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_title) LIKE '" + parameters[8].ToString().Trim() + "%'");


            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND MasterEmployee.pay_period= '" + parameters[10].ToString().Trim()+ "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND MasterEmployee.empl_status='" + parameters[11].ToString().Trim() + "'");

            //sql.Append(" and MasterEmployee.empl_code ='32821'");

            return sql.ToString();
        }

        public string GET_DEDUCTION_ANALYSIS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT Process_PayEmployee.doc_no,MasterEmployee.empl_code,MasterEmployee.last_name,MasterEmployee.first_name,");
            sql.Append(" MasterEmployee.type_code,stypaydd.ded_code,stypaydd.amount,Process_PayEmployee.pay_date,Process_PayEmployee.cash_amount");
            sql.Append(" FROM stypaydd, Process_PayEmployee, MasterEmployee ");
            sql.Append(" Where stypaydd.doc_no = Process_PayEmployee.doc_no");
            sql.Append(" AND Process_PayEmployee.empl_code = MasterEmployee.empl_code");
            sql.Append(" AND Process_PayEmployee.ok_to_post = 'Y'");
            sql.Append(" AND stypaydd.doc_no NOT IN (Select payroll_doc_no from stydedanlyd where payroll_doc_no=stypaydd.doc_no and ded_code=stypaydd.ded_code) ");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" AND Process_PayEmployee.pay_date >= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND Process_PayEmployee.pay_date <= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stypaydd.ded_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND styactvd.act_type= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" AND MasterEmployee.empl_code = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(MasterEmployee.type_code) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(MasterEmployee.job_code) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim().Length > 0)
                    sql.Append(" AND MasterEmployee.job_title = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString().Trim().Length > 0)
                    sql.Append(" AND MasterEmployee.pay_period = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString().Trim().Length > 0)
                    sql.Append(" AND MasterEmployee.empl_status = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }








    }
}
