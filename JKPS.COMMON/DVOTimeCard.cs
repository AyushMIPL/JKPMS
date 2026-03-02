using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTimeCard : DVOBase
    {
        private string _first_name;
        private string _last_name;
        private string _department;
        private string _keyvalue;
        private int _card_no;    
        private string _inc_code; 
        private decimal _inc_hours;   
        private decimal _inc_number;  
        private int _acct_no;
        private decimal _inc_rate;    
        private int _line_no ;    
        private string _empl_code;   
        private DateTime _end_date;    
        private DateTime _start_date;
        private string _used_flag;
        //**** If user give range ****
        private string _start_range;
        private string _end_range;
        
        #region public constructor


        public DVOTimeCard()
        {
            _first_name = string.Empty;
            _last_name = string.Empty;
            _department = string.Empty;
            _keyvalue = string.Empty;
            _card_no = 0;
            _inc_code = string.Empty;
            _inc_hours = 0;
            _inc_number = 0;
            _acct_no = 0;
            _inc_rate = 0;
            _line_no = 0;
            _empl_code = string.Empty;
            _end_date = Convert.ToDateTime("01/01/1900");
            _start_date = Convert.ToDateTime("01/01/1900");
            _used_flag = "N";
            _start_range=string.Empty;
            _end_range=string.Empty;
            }
        #endregion constructor
        #region public properties
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public int card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }
        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }
        public decimal inc_hours
        {
            get { return _inc_hours; }
            set { _inc_hours = value; }
        }
        public decimal inc_number
        {
            get { return _inc_number; }
            set { _inc_number = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public decimal inc_rate
        {
            get { return _inc_rate; }
            set { _inc_rate = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public DateTime end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string used_flag
        {
            get { return _used_flag; }
            set { _used_flag = value; }
        }
        public string start_range
        {
            get { return _start_range; }
            set { _start_range = value; }
        }
        public string end_range
        {
            get { return _end_range; }
            set { _end_range = value; }
        }
        
            #endregion public properties




     #region Stored-Procedures
     /// <summary>
     /// stored procedures
     /// </summary>

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
            get { return "uspTimeCardDetailGet"; }
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
    
       /// <summary>
       /// Find the record 
       /// </summary>
       /// <param name="parameters"></param>
       /// <returns></returns>

        //public override string FIND_QUERY(ref Object[] parameters)
        //{
        //    System.Text.StringBuilder sql = new StringBuilder();
        //    sql.Append("select MasterEmployee.first_name p_first_name,MasterEmployee.last_name p_last_name,MasterEmployee.department p_department, ");
        //    sql.Append(" PayrollGLAccounts.keyvalue p_keyvalue,EmployeeTCard_Details.card_no p_card_no,EmployeeTCard_Details.inc_code p_inc_code, ");
        //    sql.Append("EmployeeTCard_Details.inc_hours p_inc_hours,EmployeeTCard_Details.inc_number p_inc_number,MasterEmployee.cash_acct p_acct_no, ");
        //    sql.Append(" EmployeeTCard_Details.inc_rate p_inc_rate,EmployeeTCard_Details.line_no p_line_no,EmployeeTCard_Header.used_flag p_used_flag ,");
        //    sql.Append(" EmployeeTCard_Header.empl_code p_empl_code,EmployeeTCard_Header.end_date p_end_date,EmployeeTCard_Header.start_date p_start_date ");
        //    sql.Append(" , Substring(ISNULL(PayrollGLAccounts.keyvalue,'000'),1,3) p_key ,Flex_Segment_Value_Details.[desc] p_description ");
        //    sql.Append("  from EmployeeTCard_Details INNER JOIN  EmployeeTCard_Header ON EmployeeTCard_Header.card_no = EmployeeTCard_Details.card_no  INNER JOIN MasterEmployee  ON  MasterEmployee.empl_code=EmployeeTCard_Header.empl_code LEFT Outer JOIN PayrollGLAccounts ");
        //    sql.Append(" ON PayrollGLAccounts.acct_no=EmployeeTCard_Details.acct_no ");
        //    sql.Append(" INNER JOIN Flex_Segment_Value_Details ON Flex_Segment_Value_Details.Keyvalue = Substring(ISNULL(PayrollGLAccounts.keyvalue,'000'),1,3) ");
        //    sql.Append("where  1=1   and Flex_Segment_Value_Details.segmentid=1 ");
        //    sql.Append("  ");
        //    sql.Append(" and EmployeeTCard_Details.inc_number >= 0 AND EmployeeTCard_Details.inc_rate >= 0 ");
        //    if (parameters[0] != null)
        //        if (parameters[0].ToString() != string.Empty)
        //            sql.Append(" AND EmployeeTCard_Header.empl_code = '" + Convert.ToString(parameters[0]).Replace("'", "''") + "'");
        //    if (parameters[1] != null)
        //        if (parameters[1].ToString() != string.Empty)
        //            sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

        //    if (parameters[2] != null)
        //        if (parameters[2].ToString() != string.Empty)
        //            sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
        //    if (parameters[3] != null)
        //        if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Contains("1900"))//dateFrom
        //            sql.Append(" AND EmployeeTCard_Header.start_date >= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
        //    if (parameters[4] != null)
        //        if (parameters[4].ToString() != string.Empty && !parameters[4].ToString().Contains("1900"))//dateto
        //            sql.Append(" AND EmployeeTCard_Header.end_date <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
        //    if (parameters[5] != null)
        //        if (parameters[5].ToString() != string.Empty )
        //            sql.Append(" AND Rtrim(EmployeeTCard_Header.used_flag) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
        //    if (parameters[6] != null && parameters[7] != null)
        //        if (parameters[6].ToString() != string.Empty && parameters[7].ToString() != string.Empty)
        //            sql.Append(" AND EmployeeTCard_Header.empl_code BETWEEN '" + parameters[6].ToString().Trim() + "' AND '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");

        //    return sql.ToString();
        //}

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select MasterEmployee.first_name p_first_name,MasterEmployee.last_name p_last_name,MasterEmployee.department p_department, ");
            sql.Append(" PayrollGLAccounts.keyvalue p_keyvalue,EmployeeTCard_Details.card_no p_card_no,EmployeeTCard_Details.inc_code p_inc_code, ");
            sql.Append("EmployeeTCard_Details.inc_hours p_inc_hours,EmployeeTCard_Details.inc_number p_inc_number,MasterEmployee.cash_acct p_acct_no, ");
            sql.Append(" EmployeeTCard_Details.inc_rate p_inc_rate,EmployeeTCard_Details.line_no p_line_no,EmployeeTCard_Header.used_flag p_used_flag ,");
            sql.Append(" EmployeeTCard_Header.empl_code p_empl_code,EmployeeTCard_Header.end_date p_end_date,EmployeeTCard_Header.start_date p_start_date ");
           sql.Append(" , 1 p_key , 2  p_description ");
            sql.Append("  from EmployeeTCard_Details INNER JOIN  EmployeeTCard_Header ON EmployeeTCard_Header.card_no = EmployeeTCard_Details.card_no  INNER JOIN MasterEmployee  ON  MasterEmployee.empl_code=EmployeeTCard_Header.empl_code LEFT Outer JOIN PayrollGLAccounts ");
            sql.Append(" ON PayrollGLAccounts.acct_no=EmployeeTCard_Details.acct_no ");
          //  sql.Append(" INNER JOIN Flex_Segment_Value_Details ON Flex_Segment_Value_Details.Keyvalue = Substring(ISNULL(PayrollGLAccounts.keyvalue,'000'),1,3) ");
            sql.Append("where  1=1  ");// and Flex_Segment_Value_Details.segmentid=1 ");
            sql.Append("  ");
            sql.Append(" and EmployeeTCard_Details.inc_number >= 0 AND EmployeeTCard_Details.inc_rate >= 0 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND EmployeeTCard_Header.empl_code = '" + Convert.ToString(parameters[0]).Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Contains("1900"))//dateFrom
                    sql.Append(" AND EmployeeTCard_Header.start_date >= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && parameters[4].ToString().Contains("1900"))//dateto
                    sql.Append(" AND EmployeeTCard_Header.end_date <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(EmployeeTCard_Header.used_flag) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null && parameters[7] != null)
                if (parameters[6].ToString() != string.Empty && parameters[7].ToString() != string.Empty)
                    sql.Append(" AND EmployeeTCard_Header.empl_code BETWEEN '" + parameters[6].ToString().Trim() + "' AND '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
