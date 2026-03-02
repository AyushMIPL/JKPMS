using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLBudgetAllocationDistributionInballod : DVOBase
    {
        private int _EstId;
        private string _StartingPeriod;
        private string _EndingPeriod;
        private decimal _AllocPercent;
        private decimal _AllocAmount;
        private string _Used;

        #region Constructor

        public DVOGLBudgetAllocationDistributionInballod()
        {
            _EstId = 0;
            _StartingPeriod = string.Empty;
            _EndingPeriod = string.Empty;
            _AllocPercent = 0;
            _AllocAmount = 0;
            _Used = string.Empty;
        }

        #endregion Constructor

        #region Properties

        public int EstId
        {
            get { return _EstId; }
            set { _EstId = value; }
        }
        public string StartingPeriod
        {
            get { return _StartingPeriod; }
            set { _StartingPeriod = value; }
        }
        public string EndingPeriod
        {
            get { return _EndingPeriod; }
            set { _EndingPeriod = value; }
        }
        public decimal AllocPercent
        {
            get { return _AllocPercent; }
            set { _AllocPercent = value; }
        }
        public decimal AllocAmount
        {
            get { return _AllocAmount; }
            set { _AllocAmount = value; }
        }
        public string Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        #endregion Properties

        #region Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGLBgtDtrbIns"; }//uspglbgtdtrbins
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLBgtDtrbDel"; }//uspglbgtdtrbdel
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLBgtDtrbGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "inballod"; }
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
            sql.Append("SELECT DISTINCT od.estid p_estid, od.startingperiod p_startingperiod, ");
            sql.Append(" od.endingperiod p_endingperiod, od.allocpercent p_allocpercent, ");
            sql.Append(" od.allocamount p_allocamount, od.used p_used");
            sql.Append(" FROM inballod od, inbestid ibi");
            sql.Append(" WHERE od.estid = ibi.id ");

            if (Convert.ToInt32(parameters[0]) > 0)//EstId
                sql.Append(" AND od.estid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)//StartingPeriod
                    sql.Append(" AND od.startingperiod = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)//EndingPeriod
                    sql.Append(" AND od.endingperiod = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[3]) > 0)//AllocPercent
                sql.Append(" AND od.allocpercent = " + parameters[3].ToString());
            if (Convert.ToDouble(parameters[4]) > 0)//AllocAmount
                sql.Append(" AND od.allocamount = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)//Used
                    sql.Append(" AND od.used = '" + parameters[5].ToString().Replace("'", "''") + "'");

            return sql.ToString();
        }

        #endregion Procedures
    }
}
