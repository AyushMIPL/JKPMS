using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVORecExpByMinistry:DVOBase
    {
       private string _period_month;
        private string _period_year;
        #region Constructure

        public DVORecExpByMinistry()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
        }
        #endregion Constructure

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
        # endregion Properties
      

        
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
            get { return "uspRecExpByMinis"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref object[] parameters) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select recexpbymins1.ministry ministry1 ,recexpbymins1.budgetedamt,allministry.ministry,");
            sql.Append("allministry.desc description, recexpbymins1.budgetedyear,recexpbymins2.ministry ministry2,");
            sql.Append("recexpbymins2.description desc,recexpbymins2.p_yeartodate,");
            sql.Append("recexpbymins2.period_month,recexpbymins2.period_year");
            sql.Append(" FROM allministry, outer (recexpbymins2,outer recexpbymins1)");
            sql.Append(" where allministry.ministry= recexpbymins2.ministry");
            sql.Append(" and recexpbymins1.ministry=recexpbymins2.ministry");
            sql.Append(" and recexpbymins1.budgetedyear='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and recexpbymins2.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and recexpbymins2.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
           
            sql.Append(" ORDER By allministry.ministry");

            return sql.ToString();
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
        #endregion StoreProcedures
    }
}
