using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTBWithdraw : DVOBase
    {
        int _rowid;
        int _issue_num;
        string _tend_code;// char(8)
        string _withdraw_date;// date
        string _withdraw_reason;// char(200),
        int _tbschid;
        decimal _amt_per_100;// decimal(12,4),
        decimal _amt_issued;// decimal(12,2),
        decimal _amt_withdraw;// decimal(12,2)
        string _ok_to_post;
        int _ap_cd_doc_no;
        string _wdrcncl;
        int _insertby;// 
        string _insertdate;// date,
        string _insertmachineinfo;// char(50),
        int _updateby;// 
        string _updatedate;// date,
        string _updatemachineinfo;// char(50)


        #region Constructor

        public DVOTBWithdraw()
        {
            _rowid = 0;
            _issue_num = 0;
            _tend_code = string.Empty;// char(8)
            _withdraw_date = "01/01/1900";// date
            _withdraw_reason = string.Empty;// char(200),
            _tbschid = 0;
            _amt_per_100 = 0;// decimal(12,4),
            _amt_issued = 0;// decimal(12,2),
            _amt_withdraw = 0;// decimal(12,2),
            _ok_to_post = string.Empty;
            _ap_cd_doc_no = 0;
            _wdrcncl = string.Empty;
            _insertby = DVOApplicationUserInfo.UserId;// 
            _insertdate = "01/01/1900";// date,
            _insertmachineinfo = DVOApplicationUserInfo.MachineInfo;// char(50),
            _updateby = DVOApplicationUserInfo.UserId;// 
            _updatedate = "01/01/1900";// date,
            _updatemachineinfo = DVOApplicationUserInfo.MachineInfo;// char(50)
        }

        #endregion Constructor

        #region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string withdraw_date
        {
            get { return _withdraw_date; }
            set { _withdraw_date = value; }
        }
        public string withdraw_reason
        {
            get { return _withdraw_reason; }
            set { _withdraw_reason = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public decimal amt_issued
        {
            get { return _amt_issued; }
            set { _amt_issued = value; }
        }
        public decimal amt_withdraw
        {
            get { return _amt_withdraw; }
            set { _amt_withdraw = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public int ap_cd_doc_no
        {
            get { return _ap_cd_doc_no; }
            set { _ap_cd_doc_no = value; }
        }
        public string wdrcncl
        {
            get { return _wdrcncl; }
            set { _wdrcncl = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usptbwithdrawins"; }
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
