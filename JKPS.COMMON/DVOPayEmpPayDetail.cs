using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayEmpPayDetail:DVOBase
    {
        private string _month;
        private string _startdate;
        private string _enddate;
        #region Constructor

        public DVOPayEmpPayDetail()
        {
            _month = string.Empty;
            _startdate = string.Empty;
            _enddate = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public string month
        {
            get { return _month; }
            set { _month = value; }
        }
        public string startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        public string enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }
      

        #endregion Public Properties

        #region Stored-Procedures       

       
        public string GET_PAY_DETAIL
        {
            get { return "UspgetEmpPayDetail";}
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
            get { return "MasterEmployee"; }
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
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterEmployee.empl_code,Process_PayIncomes.inc_code,Process_PayIncomes.amount ,MasterEmployee.soc_sec_num,");
            sql.Append("MasterEmployee.first_name,MasterEmployee.last_name,MasterEmployee.birthdate,Process_PayIncomes.inc_rate,Process_PayIncomes.number,Process_PayEmployee.inc_gross,");
            sql.Append("where Process_PayEmployee.doc_no =Process_PayIncomes.doc_no and Process_PayEmployee.empl_code=MasterEmployee.empl_code");
            sql.Append("and pay_date BETWEEN Date('01/01/2008') and date('31/01/2008') and MasterEmployee.terminated is null");
            sql.Append("order by MasterEmployee.empl_code");
                
            
            return sql.ToString();
            
        }

        #endregion Stored-Procedures
    }
}
