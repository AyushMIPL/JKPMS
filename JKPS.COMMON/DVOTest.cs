using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTest : DVOBase
    {
        int _rowid;
       
        string _Text1;// char(8)
        string _Text2;// date
        string _Text3;// char(200),
        


        #region Constructor

        public DVOTest()
        {
            _rowid = 0;
       
            _Text1 = string.Empty;// char(8)
            _Text2 = "01/01/1900";// date
            _Text3 = string.Empty;// char(200),
          
           
        }

        #endregion Constructor

        #region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public string text1
        {
            get { return _Text1; }
            set { _Text1 = value; }
        }
        public string text2
        {
            get { return _Text2; }
            set { _Text2 = value; }
        }
        public string text3
        {
            get { return _Text3; }
            set { _Text3 = value; }
        }


        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspInsertTestTable"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }//usptbwithdrawupd
        }

        public override string DELETE_SPNAME
        {
            get { return "usptbwithdrawdel"; }
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
            get { return "tbwithdraw"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT issue_num,tend_code,withdraw_date,withdraw_reason,tbschid,amt_per_100,amt_issued,");
            sql.Append(" rowid,ok_to_post,amt_withdraw,ap_cd_doc_no,wdrcncl");
            sql.Append(" FROM tbwithdraw WHERE 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND issue_num = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND tend_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND tbschid = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Trim().Contains("1900") && !parameters[3].ToString().Trim().Contains("0001"))
                    sql.Append(" AND withdraw_date = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND ok_to_post = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND ap_cd_doc_no = " + parameters[5].ToString());

            return sql.ToString();
        }

        //Get data for TB withdraw Posting
        public string FIND_TBwithdrawDETAILS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct tbwithdraw.rowid,tbwithdraw.doc_no,tbwithdraw.issue_num,tbwithdraw.tend_code, ");
            sql.Append(" tbwithdraw.tbschid,tbwithdraw.withdraw_date,tbwithdraw.ok_to_post, ");
            sql.Append(" tbwithdraw.amt_issued,tbwithdraw.amt_per_100, ");
            sql.Append(" tbschemes.tbschname,tbclients.tend_name, tbwithdraw.amt_withdraw,tbwithdraw.ap_cd_doc_no ap_doc_no ");
            sql.Append(" from tbwithdraw,tbschemes,tbclients where ");
            sql.Append("  tbclients.tend_code = tbwithdraw.tend_code ");
            sql.Append(" AND tbwithdraw.tbschid =tbschemes.tbschid ");
            sql.Append(" AND tbwithdraw.ok_to_post in('N','Y')");
            sql.Append(" AND tbwithdraw.wdrcncl in('W')");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbwithdraw.tbschid =" + parameters[0]);
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                    sql.Append(" AND tbwithdraw.issue_num =" + parameters[1]);
            //if (parameters[2] != null)
            //    if (Convert.ToInt32(parameters[2]) > 0) //batch_id
            //        sql.Append(" AND tbwithdraw.batch_id = " + parameters[2]);

            sql.Append(" order by tbwithdraw.doc_no,tbwithdraw.withdraw_date ");
            return sql.ToString();
        }
        ////Using in TB Withdraw Posting
        public string UPDATE_TBWITHDRAW
        {
            get { return "usptbwithdrawupd"; }
        }
        //Using in TB tbissued Posting update tender status
        public string UPDATE_TBISSUED
        {
            get { return "usptbisudstupd"; }
        }

        public string FIND_DATAFROMTBWITHDRAW(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT issue_num,tend_code,withdraw_date,withdraw_reason,tbschid,amt_per_100,amt_issued,");
            sql.Append(" rowid,ok_to_post,amt_withdraw,ap_cd_doc_no,wdrcncl");
            sql.Append(" FROM tbwithdraw WHERE ok_to_post <> 'C' AND wdrcncl <> 'C' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND issue_num = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND tend_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND tbschid = " + parameters[2].ToString());
            return sql.ToString();
        }


        #endregion store-procedures
    }

    ///// <summary>
    ///// Comparer to make DVOTBWithdraw class to comparable for dist_amt
    ///// </summary>
    //public class DVOTBWithdraw_DistAmt_Comparer : IComparer<DVOTBWithdraw>
    //{
    //    #region IComparer<Student> Members

    //    public int Compare(DVOTBWithdraw obj1, DVOTBWithdraw obj2)
    //    {
    //        int returnValue = 1;
    //        if (obj1 != null && obj2 != null)
    //        {
    //            returnValue = obj2.dist_amt.CompareTo(obj1.dist_amt);
    //        }

    //        return returnValue;
    //    }

    //    #endregion
    //}
}
