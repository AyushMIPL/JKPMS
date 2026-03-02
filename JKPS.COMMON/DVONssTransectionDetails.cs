using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.) DVo For New Transection Details(NSS)               Rajeev(D)                                 04/12/2008(DD)
    ///2.) 
    ///<summery>
    public class DVONssTransectionDetails : DVOBase
    {
        //Class Member Declearation for Record Table
        #region DataMembernss_dtlTable

        private int _dtlRowID;
        private int _dtlseq_no;
        private string _account_no;
        private int _for_period;
        private string _trans_flag;
        private decimal _amount;

        #endregion DataMembernss_dtlTable   
        private DateTime _PaymentDate;
        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //Added By Rahul jain on 21-07-09
        private  string _ok_post ;
        private int _cash_received;
        private int _batch_id;

        public DVONssTransectionDetails()
        {
            _dtlRowID = 0;
            _dtlseq_no = 0;
            _account_no = string.Empty;
            _for_period = 0;
            _trans_flag = string.Empty;
            _amount = 0.0M;

            _PaymentDate = DateTime.Now;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

            _ok_post = "N";
            _cash_received = -1;
            _batch_id = 0;
        }

        //Properties For Details Table
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public int dtlRowID
        {
            get { return _dtlRowID; }
            set { _dtlRowID = value; }
        }

        public int dtlseq_no
        {
            get { return _dtlseq_no; }
            set { _dtlseq_no = value; }
        }

        public string account_no
        {
            get { return _account_no; }
            set { _account_no = value; }
        }

        public int for_period
        {
            get { return _for_period; }
            set { _for_period = value; }
        }

        public string trans_flag
        {
            get { return _trans_flag; }
            set { _trans_flag = value; }
        }

        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        //Properties used for only SQL Server

        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

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


        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }
        public int cash_received
        {
            get { return _cash_received; }
            set { _cash_received = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspnsstrdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspnssTrDtlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspnsstrdtldel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspnssTrDtlGet"; }
        }
        public string FIND_WHENUPDATE
        {
            get { return "uspwhnupdnssdtlget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
            //System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("select rowid v_rowid,voucher_no v_voucher_no,payment_date v_payment_date,operator v_operator,seq_no v_seq_no from nss_hdr where 1=1");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND  voucher_no=" + "'" + parameters[0].ToString() + "'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != "1/1/1900")
            //        sql.Append(" AND  payment_date=" + "'" + parameters[1].ToString() + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(operator) LIKE '" + parameters[2].ToString().Trim() + "%'");

            //return sql.ToString();
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

        public string FIND_DUPLICATE_PERIOD(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) dup_count from nss_dtl ");
            sql.Append( " where ok_post<>'C'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  account_no='" + parameters[0].ToString() + "'");
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND for_period = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND seq_no <> " + parameters[2].ToString());
            return sql.ToString();
        }
        #endregion Stored-Procedures

    }
}
