using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVORecExpByProgram_FromW2A_:DVOBase
    {
        private string _period_month;
        private string _period_year;
        private string _ministry;
       
        # region Properties

        public string _month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string _Year
        {
            get { return _period_year; }
            set { _period_year = value; }

        }
        public string Ministry
        {
            get { return _ministry; }
            set { _ministry = value; }
        }
        # endregion Properties
        #region Constructure
        public DVORecExpByProgram_FromW2A_ ()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
            _ministry = string.Empty;
        }

         #endregion Constructure
        #region StoreProcedures
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspRecExpByProgFormW2A"; }
        }

        public override string ALL_SPNAME
        {
            get { return "usprecexpMaster_Segment"; }
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
       public string GET_EST_AMT
       {
           get { return "uspestamtexpget"; }
       }
        public override string FIND_QUERY(ref object[] parameters) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select x0.keyvalue,x1.balance p_yeartodateamt");
            sql.Append(" from PayrollGLAccounts x0,outer stxchrtd x1");
            sql.Append(" where  x0.acct_no = x1.acct_no  AND x0.acct_type = 'RECEXP'");
            sql.Append(" and x1.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and x1.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if(parameters[2].ToString().Trim()!="" && parameters[2]!=DBNull.Value)
                sql.Append(" and x0.keyvalue[1,2]='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
          
            return sql.ToString();






            
        }
   #endregion   StoreProcedures
    }
}
