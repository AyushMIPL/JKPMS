using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{

   public  class DVOQtrlyHourlyWages:DVOBase 
    {
       private string _start_date;
       private string _End_date;


        public DVOQtrlyHourlyWages()
        {
           _start_date=string.Empty;
          _End_date=string.Empty;

        }

       public string  start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }

       public string  End_date
       {
           get { return _End_date; }
           set { _End_date = value; }
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
            sql.Append(" select stxtranr.ref_code, styactvd.act_code, styactvd.act_type, ");
            sql.Append(" styactvd.amount,styactvd.hours,MasterEmployee.first_name, ");
            sql.Append(" MasterEmployee.last_name,MasterEmployee.middle_name, ");
            sql.Append(" MasterEmployee.soc_sec_num, stytranr.pay_date ");
            sql.Append(" from  styactvd, stytranr, stxtranr, MasterEmployee, MasterCompany ");
            sql.Append(" Where stytranr.doc_no = styactvd.doc_no ");
            sql.Append(" and stxtranr.doc_no = styactvd.doc_no ");
            sql.Append(" and MasterEmployee.empl_code = stxtranr.ref_code and  ");
            sql.Append(" styactvd.act_type ='B'");
            sql.Append(" and MasterEmployee.soc_sec_num is not null ");


            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//YearTo
                sql.Append(" and stytranr.pay_date  >= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//YearTo
                sql.Append(" and stytranr.pay_date  <= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 




            //sql.Append(" select styactvv.amount, MasterEmployee.first_name,MasterEmployee.last_name,MasterEmployee.middle_name,styactvv.act_code, ");
            //sql.Append(" MasterEmployee.empl_code from styactvv, MasterEmployee where styactvv.ref_code = MasterEmployee.empl_code ");
            //sql.Append(" and styactvv.act_type = 'B'");


            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(MasterEmployee.empl_code) LIKE '" + parameters[0].ToString().Trim() + "%'");


            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(styactvv.act_code) LIKE '" + parameters[1].ToString().Trim() + "%'");

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
