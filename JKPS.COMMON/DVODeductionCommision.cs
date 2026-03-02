using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVODeductionCommision:DVOBase 
    {

       private string _ded_code;
       private string _Month;
       private string _Year;
       private int _ded_percent;

       public DVODeductionCommision()
       {
              _ded_code=string.Empty ;
         _Month=string.Empty ;
         _Year=string.Empty ;
       }
       public string ded_code
       {
           get { return _ded_code; }
           set { _ded_code = value; }
       }
       public string Month
       {
           get { return _Month; }
           set { _Month = value; }

       }

       public string Year
       {
           get { return _Year ; }
           set { _Year  = value; }

       }

       public int ded_percent
       {
           get { return _ded_percent; }
           set { _ded_percent = value; }
       }


#region Stored-Procedures

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

            sql.Append(" Select MasterCompany.company_name, MasterEmployee.first_name, MasterEmployee.last_name, ");
            sql.Append(" MasterEmployee.middle_name, stypaydd.amount, Process_PayEmployee.empl_code ");
            sql.Append(" from MasterCompany, MasterEmployee, stypaydd, Process_PayEmployee ");
            sql.Append("  where MasterEmployee.empl_code = Process_PayEmployee.empl_code");
            sql.Append("  and Process_PayEmployee.doc_no = stypaydd.doc_no");
            sql.Append("  and Process_PayEmployee.ok_to_post <> 'C' and stypaydd.amount > 0");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stypaydd.ded_code) LIKE '" + parameters[0].ToString().Trim() + "%'");


            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND Month(Process_PayEmployee.pay_date) = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND year(Process_PayEmployee.pay_date) = " + parameters[2].ToString());



            //if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//YearTo
            //    sql.Append(" and styactvv.doc_date >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
            //if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//YearTo
            //    sql.Append(" and styactvv.doc_date <= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 





            //sql.Append(" Order by MasterEmployee.last_name, MasterEmployee.first_name, MasterEmployee.middle_name ");



            return sql.ToString();
        }
        #endregion store-procedures
    }
}
