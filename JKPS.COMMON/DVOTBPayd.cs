using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa
    public class DVOTBPayd:DVOBase
    {
        private int _rowid;
        private int _tbp_doc_no;
        private int _issue_num;
        private string _tend_code;
        private int _ap_cd_doc_no;
        private int _tbschid;
        private string _ok_to_post;
        private decimal _pay_amt;
        private int _insertby;
        private DateTime _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private DateTime _updatedate;
        private string _updatemachineinfo;


        public DVOTBPayd()
        {
            _rowid = 0;
            _tbp_doc_no = 0;
            _issue_num = 0;
            _tend_code = string.Empty;
            _ap_cd_doc_no = 0;
            _tbschid = 0;
            _ok_to_post = string.Empty;
            _pay_amt = 0;
            _insertby = DVOApplicationUserInfo.UserId;
            _insertdate = DVOApplicationUserInfo.CurrentDate;
            _insertmachineinfo = DVOApplicationUserInfo.MachineInfo;
            _updateby = DVOApplicationUserInfo.UserId;
            _updatedate = DVOApplicationUserInfo.CurrentDate;
            _updatemachineinfo = DVOApplicationUserInfo.MachineInfo;


        }

        #region Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public int tbp_doc_no
        {
            get { return _tbp_doc_no; }
            set { _tbp_doc_no = value; }
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
        public int ap_cd_doc_no
        {
            get { return _ap_cd_doc_no; }
            set { _ap_cd_doc_no = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public decimal pay_amt
        {
            get { return _pay_amt; }
            set { _pay_amt = value; }
        }
        public string InsertMachineInfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public int InsertBy
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public int UpdateBy
        {
            get { return _updateby; }
            set { _updateby = value; }
        }



        #endregion 

        #region Store Procedures
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usptbpaydupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usptbpayddel"; }
        }
        public string UPD_STS
        {
            get { return "usppaydstsupd"; }
        }
        public string GET_SUSP_ACCT
        {
            get { return "uspsusacctget"; }
        }
        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { return "tbpayd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select rowid,tbp_doc_no,issue_num,tend_code,ap_cd_doc_no,tbschid,ok_to_post, pay_amt from TBPAYD ");
            sql.Append(" where tbpayd.ok_to_post<> 'C' ");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbpayd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbpayd.issue_num =" + parameters[1].ToString());

                if (parameters[2] != null)
                    if (parameters[2].ToString().Trim().Length > 0)
                        sql.Append(" and tbpayd.tend_code ='" + parameters[2].ToString().Trim() + "'");

                if (parameters[3] != null)
                    if (parameters[3].ToString().Trim().Length > 0)
                        if (Convert.ToInt32(parameters[3]) > 0)
                            sql.Append(" and tbpayd.tbp_doc_no =" + parameters[3].ToString().Trim() + "");

            if(parameters[4]!=null)
                if(parameters[4].ToString().Trim().Length>0)
                    sql.Append(" and tbpayd.ok_to_post ='" + parameters[4].ToString().Trim() + "'");

            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[5]) > 0)
                        sql.Append(" and tbpayd.rowid =" + parameters[5].ToString().Trim() + "");

            return sql.ToString();
        }

        public  string FIND_POSTING_DATA(ref object[] parameters)
        {
           
            StringBuilder sql = new StringBuilder();
            sql.Append(" select tbpayd.tbp_doc_no,tbpayd.issue_num,tbpayd.tend_code,tbpayd.ap_cd_doc_no, ");
            sql.Append(" tbpayd.tbschid,tbpayd.ok_to_post, tbpayd.pay_amt,tbschemes.tbschname,tbclients.tend_name,tbpayd.insertdate");
            //sql.Append(" tbschemes.tbschname,tbschemes.bank_acct_no, tbschemes.deposit_acct_no,tbschemes.interest_acct_no,tbschemes.pay_acct_no");
            sql.Append(" from tbpayd ,tbschemes,tbclients  ");
            sql.Append(" where tbpayd.tbschid=tbschemes.tbschid ");
            sql.Append(" and tbpayd.tend_code=tbclients.tend_code ");
            sql.Append(" and  tbpayd.ok_to_post in ('N','Y')");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbpayd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbpayd.issue_num =" + parameters[1].ToString());

            return sql.ToString();
        }

        public string FIND_UNPOSTED(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select count(*) from TBPAYD ");
            sql.Append(" where tbpayd.ok_to_post not in ('P','C') ");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbpayd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbpayd.issue_num =" + parameters[1].ToString());

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" and tbpayd.tend_code ='" + parameters[2].ToString().Trim() + "'");


            return sql.ToString();
        }


        #endregion 
    }
}
