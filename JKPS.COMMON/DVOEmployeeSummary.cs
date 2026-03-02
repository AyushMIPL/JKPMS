using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// declare the variables with type and name
    /// </summary>
 public  class DVOEmployeeSummary: DVOBase 
    {
        private string _Empl_code;
        private string _soc_sec_num;
        private string _last_name;
        private string _first_name;
        private string _type_code;
        private string _job_code;
        private string _job_title;
        private string _pay_period;
        //date type  in informix
        private string _last_pay;
     //---------------------------
        private string _empl_status;

        #region public constructor
      

        public DVOEmployeeSummary()
        {
                _Empl_code=string.Empty;
                _soc_sec_num = string.Empty;
                _last_name = string.Empty;
                _first_name = string.Empty;
                _type_code = string.Empty;
                _job_code = string.Empty;
                _job_title = string.Empty;
                _pay_period = string.Empty;
                //date type in informix
                _last_pay = string.Empty;
            //---------------------------
                _empl_status = string.Empty;
            }
        #endregion constructor
        #region public properties
     
     public string Emp1_code
     {
         get { return  _Empl_code; }
         set { _Empl_code = value; }
     }

     public string soc_sec_num
     {
         get { return _soc_sec_num; }
         set { _soc_sec_num = value; }
     }

  public string last_name
     {
         get { return  _last_name; }
         set { _last_name = value; }
     }


     public string first_name
     {
         get { return  _first_name; }
         set { _first_name = value; }
     }

       public string type_code
     {
         get { return  _type_code; }
         set { _type_code = value; }
     }
       public string job_code
     {
         get { return  _job_code; }
         set { _job_code = value; }
     }
        public string job_title
     {
         get { return  _job_title; }
         set { _job_title = value; }
     }

          public string pay_period
     {
         get { return  _pay_period; }
         set { _pay_period = value; }
     }

           public string last_pay
     {
         get { return  _last_pay; }
         set { _last_pay = value; }
     }

     public string empl_status
     {
         get { return _empl_status ; }
         set { _empl_status  = value; }
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
            get { return "uspEmpSummByNGet"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select empl_code p_empl_code,first_name p_first_name,middle_name p_middle_name,last_name p_last_name, ");
            sql.Append("address1 p_address1,job_title p_job_title,date_hired p_date_hired,soc_sec_num p_soc_sec_num,birthdate p_birthdate from MasterEmployee where 1=1 ");
    
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND empl_code = '" + Convert.ToString(parameters[0]).Replace("'", "''") + "'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND soc_sec_num = '" + Convert.ToString(parameters[1]).Replace("'", "''") + "'");

            
          
            
              if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(last_name) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(first_name) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(job_code) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(job_title) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(empl_status) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)
                sql.Append(" and pay_period='" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[9] != null)
            //    if (parameters[9].ToString() != string.Empty)
            //        sql.Append(" AND last_pay = '" + Convert.ToDateTime(parameters[9]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                sql.Append(" AND last_pay = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");

            sql.Append("order by last_name ");
            return sql.ToString();
        }

        #endregion Stored-Procedures


    }
}
