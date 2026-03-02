using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)     DVO For New (NSS)Transection                    Rajeev(D)                                    03/12/2008(DD)
    ///2.) 
    ///<summery>
    public class DVONssTransection : DVOBase
    {
        //Class Member Declearation for Details Table
        #region DataMembernss_hdrTable

        private string _voucher_no;
        private DateTime _payment_date;
        private string _operator;
        private int _seq_no;
        private string _Ok_To_Post;
        private string _Posted;
        private int _BatchNumber;

        private int _Rowid;

        #endregion DataMembernss_hdrTable

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //Added By Rahul Jain 21-07-09
        private string _status;
        private int _cash_received;
        private int _batch_id;


        public DVONssTransection()
        {

            _voucher_no = string.Empty;
            _payment_date = Convert.ToDateTime("1/1/1900");
            _operator = string.Empty;
            _seq_no = 0;
            _Ok_To_Post = string.Empty;
            _Posted = string.Empty;
            _BatchNumber = 0;
            _Rowid = 0;


            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

            _status = string.Empty;
            _cash_received = -1;
            _batch_id = 0;
        }

        #region StartProperties

        //Properties For Record Table

        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }

        public DateTime payment_date
        {
            get { return _payment_date; }
            set { _payment_date = value; }
        }

        public string operatorHdr
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public int seq_no
        {
            get { return _seq_no; }
            set { _seq_no = value; }
        }

        public string Ok_To_Post
        {
            get { return _Ok_To_Post; }
            set { _Ok_To_Post = value; }
        }

        public string Posted
        {
            get { return _Posted; }
            set { _Posted = value; }
        }

        public int BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }


        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }



        //Properties used for only SQL Server

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }

        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public int cash_received
        {
            get { return _cash_received; }
            set { _cash_received = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspnsstrrecins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspnssTrRecUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspnssTrRecDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspnssTrRecGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select distinct nss_hdr.seq_no v_seq_no,nss_hdr.voucher_no v_voucher_no,nss_hdr.payment_date v_payment_date,nss_hdr.operator v_operator,nss_hdr.rowid v_rowid ,nss_hdr.status v_status ");
            sql.Append(" from nss_hdr ,nss_dtl where nss_hdr.seq_no=nss_dtl.seq_no  and nss_hdr.status not in ('0') ");//and nss_dtl.ok_post not in ('P','C') ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  nss_hdr.voucher_no=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (!parameters[1].ToString().Contains("1900"))
                    sql.Append(" AND  nss_hdr.payment_date=" + "'" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(nss_hdr.operator) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND nss_hdr.rowid = " + parameters[3].ToString());
            if (Convert.ToInt32(parameters[4]) > -1)
                sql.Append(" AND nss_dtl.cash_received  = " + parameters[4].ToString());
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND nss_dtl.batch_id = " + parameters[5].ToString());
            return sql.ToString();
        }

        public override string TABLE_NAME
        {
            get { return "nss_hdr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion Stored-Procedures
    }
}
