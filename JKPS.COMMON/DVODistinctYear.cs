using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVODistinctYear : DVOBase
    {
        private string _period_year;


        #region Properties
        public string year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public string GET_DISTICT_YEAR_XPERDR
        {
            get { return "USP_DistYRPerdr"; }
        }
        #endregion Properties

        #region Stored-Procedures

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
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { return "uspDistnYGetAll"; }
        }
        // using in Multi Tax Analysis Summary reports
        public string GET_DISTINCT_YEAR
        {
            get { return "uspdistyeargetall"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            return "";
        }
        #endregion Stored-Procedures
    }
}

 