using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOCapExpByMinistry:DVOBase
    {

        private string _period_month;
        private string _period_year;
        #region Constructure

        public DVOCapExpByMinistry()
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
            get { return "[uspCapExpByMinis]"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
        }
        public string GET_CAPEXP_SRCFUND
        {
            get { return "uspcapexpsrcfnd"; }
        }
        public string GET_ACTCAPEXP_SRCFUND_SLTDMNTH
        {
            get { return "uspactcapexpsrcfnd"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select capexpbymins1.ministry ministry1,capexpbymins1.budgetedamt, allministry.ministry,");
            sql.Append(" allministry.desc description ,capexpbymins1.budgetedyear,capexpbymins2.ministry ministry2,");
            sql.Append(" capexpbymins2.description desc,capexpbymins2.p_yeartodate,");
            sql.Append(" capexpbymins2.period_month,capexpbymins2.period_year from allministry,");
            sql.Append(" outer (capexpbymins2, outer capexpbymins1)");
            sql.Append(" where allministry.ministry = capexpbymins2.ministry");
            //sql.Append(" and allministry.ministry = capexpbymins1.ministry");
            sql.Append(" and capexpbymins1.ministry=capexpbymins2.ministry");
            sql.Append(" and capexpbymins1.budgetedyear='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and capexpbymins2.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and capexpbymins2.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
          
            sql.Append(" ORDER By  allministry.ministry");

            return sql.ToString(); 
        }
        #endregion StoreProcedures
    }
}
