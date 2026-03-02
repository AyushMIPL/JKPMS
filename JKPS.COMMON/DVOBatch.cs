using System;
using System.Collections.Generic;
using System.Text;
namespace JKPS.COMMON
{
    public class DVOBatch : DVOBase
    {
        #region Private Variables
        /// <summary>
        ///
        /// </summary>
        private int _batch_id;
        /// <summary>
        ///
        /// </summary>
        private string _batch_type;
        /// <summary>
        ///
        /// </summary>
        private string _batch_status;
        /// <summary>
        ///
        /// </summary>
        private string _owner;
        /// <summary>
        ///
        /// </summary>
        private string _created_by;
        /// <summary>
        ///
        /// </summary>
        private string _create_date;
        /// <summary>
        ///
        /// </summary>
        private string _create_time;
        /// <summary>
        ///
        /// </summary>
        private string _approved_by;
        /// <summary>
        ///
        /// </summary>
        private string _approve_date;
        /// <summary>
        ///
        /// </summary>
        private string _approve_time;
        /// <summary>
        ///
        /// </summary>
        private string _posted_by;
        /// <summary>
        ///
        /// </summary>
        private string _post_date;
        /// <summary>
        ///
        /// </summary>
        private string _post_time;
        /// <summary>
        ///
        /// </summary>
        private int _post_seq;
        /// <summary>
        ///
        /// </summary>
        private int _total_trx;
        /// <summary>
        /// 
        /// </summary>
        private string _createdby_name;
        /// <summary>
        /// 
        /// </summary>
        private string _approved_by_name;
        /// <summary>
        /// 
        /// </summary>
        private string _posted_by_name;

        private string _batch_user;

        private int _batch_err;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;

        private DateTime _dateFrom = Convert.ToDateTime("01/01/1900");
        private DateTime _dateto = Convert.ToDateTime("01/01/1900");

        #endregion

        #region Constructor
        public DVOBatch()
        {
            batch_id = 0;
            batch_type = string.Empty;
            batch_status = string.Empty;
            owner = string.Empty;
            created_by = string.Empty;
            create_date = string.Empty;
            create_time = string.Empty;
            approved_by = string.Empty;
            approve_date = string.Empty;
            approve_time = string.Empty;
            posted_by = string.Empty;
            post_date = string.Empty;
            post_time = string.Empty;
            post_seq = 0;
            total_trx = 0;
            _createdby_name = string.Empty;
            _approved_by_name = string.Empty;
            _posted_by_name = string.Empty;
            batch_user = string.Empty;
            batch_err = 0;

            _dateFrom = Convert.ToDateTime("01/01/1900");
            _dateto = Convert.ToDateTime("01/01/1900");
        }
        
        #endregion Constructor

        #region Public Properties

        public DateTime dateto
        {
            get { return _dateto; }
            set { _dateto = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }


        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public string batch_type
        {
            get { return _batch_type; }
            set { _batch_type = value; }
        }
        public string batch_status
        {
            get { return _batch_status; }
            set { _batch_status = value; }
        }
        public string owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        public string created_by
        {
            get { return _created_by; }
            set { _created_by = value; }
        }
        public string create_date
        {
            get { return _create_date; }
            set { _create_date = value; }
        }
        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }
        public string approved_by
        {
            get { return _approved_by; }
            set { _approved_by = value; }
        }
        public string approve_date
        {
            get { return _approve_date; }
            set { _approve_date = value; }
        }
        public string approve_time
        {
            get { return _approve_time; }
            set { _approve_time = value; }
        }
        public string posted_by
        {
            get { return _posted_by; }
            set { _posted_by = value; }
        }
        public string post_date
        {
            get { return _post_date; }
            set { _post_date = value; }
        }
        public string post_time
        {
            get { return _post_time; }
            set { _post_time = value; }
        }
        public int post_seq
        {
            get { return _post_seq; }
            set { _post_seq = value; }
        }

        public string createdby_name
        {
            get { return _createdby_name; }
            set { _createdby_name = value; }
        }
        public string approved_by_name
        {
            get { return _approved_by_name; }
            set { _approved_by_name = value; }
        }
        public string posted_by_name
        {
            get { return _posted_by_name; }
            set { _posted_by_name = value; }
        }


        public int total_trx
        {
            get { return _total_trx; }
            set { _total_trx = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public string batch_user
        {
            get { return _batch_user; }
            set { _batch_user = value; }
        }
        public int batch_err
        {
            get { return _batch_err; }
            set { _batch_err = value; }
        }
        #endregion Public Properties

        #region Stored-Procedures

        //********************************Added by Bharat Dhall****************************
        public string GET_BATCH_APPROVAL_CODE
        {
            get { return "uspBtchCodeGet"; }
        }
        public string SET_AS_CURRENT_BATCH
        {
            get { return "uspSetAsCurBtch"; }
        }
        public string GET_BATCH_CANCEL_STATUS
        {
            get { return "uspBtchCnclSttsGet"; }
        }
        public string BATCH_CANCEL
        {
            get { return "uspBtchCncl"; }
        }
        public string GET_BATCH_INFO
        {
            get { return "uspBtchInfoGet"; }
        }
        //*********************************************Added By ROHIT WADHWA(06/11/2008)************
        public string GET_BATCH_INFORMATION
        {
            get { return "uspBatchInfoGet"; }
        }

        //********************************************************************************************
        //***********************************************************************************
        //**********************************added by Rohit Wadhwa **********************
        //*********************************11/10/2008*******************************
        public string POST_BATCH
        {
            get { return "uspBatchPost"; }
        }
        //*******************************************************************
        public override string INSERT_SPNAME
        {
            get { return "uspBatInfins"; }//uspbatinfins
        }

        public override string UPDATE_SPNAME
        {  // This Procedure is written by Sarvjeet on 27 Nov. 2008
           // to update total trx after posting the documents.  
            get { return "uspupdstxbtchh"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspBatInfget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspBatInfgetAll"; }
        }

        public string FIND_BATCH_LISTING
        {
            get { return "uspbatchlistingget"; }
        }

        public string GET_BATCH_DETAILS_INFO
        {
            get { return "uspprtbchcshinfget"; }
        }
        public string GET_BATCH_DETAILS_INFO_BY_DATE
        {
            get { return "uspbchcshbydateget"; }
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
        public string GET_NSS_TRX_COUNT
        {
            get { return "uspnsstrxcnount"; }
        }
        public string GET_SB_TRX_COUNT
        {
            get { return "uspsbtrxcnount"; }
        }
        public string GET_TB_TRX_COUNT
        {
            get { return "usptbtrxcnount"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT batch_id,batch_type,batch_status,owner,created_by,EXTEND(create_date,YEAR TO DAY) create_date,create_time,approved_by,approve_date,approve_time,");
            sql.Append(" posted_by,post_date,post_time,post_seq,total_trx FROM  stxbtchh where 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)//batch_id
                sql.Append(" AND batch_id = " + parameters[0].ToString());
            if (parameters[1] != null)//batch_type
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(batch_type) like  '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)//batch_status
                    sql.Append(" AND Rtrim(batch_status) like  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)//owner
                    sql.Append(" AND Rtrim(owner) like  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)//created_by
                    sql.Append(" AND Rtrim(created_by) like  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)//create_date
                    sql.Append(" AND create_date='" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)//create_time
            //    sql.Append(" AND Rtrim(create_time)=TRIM('" + parameters[6].ToString() + "')");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)//approved_by
                    sql.Append(" AND Rtrim(approved_by) like '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)//approve_date
                    sql.Append(" AND approve_date ='" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[9].ToString() != string.Empty && parameters[9].ToString() != null)//approve_time
            //    sql.Append(" AND Rtrim(approve_time) =TRIM('" + parameters[9].ToString() + "')");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)//posted_by
                    sql.Append(" AND Rtrim(posted_by) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)//post_date
                    sql.Append(" AND post_date ='" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[12].ToString() != string.Empty && parameters[12].ToString() != null)//post_time
            //   sql.Append(" AND Rtrim(post_time) =TRIM('" + parameters[12].ToString() + "')");
            if (Convert.ToInt32(parameters[10]) > 0)//post_seq
                sql.Append(" AND post_seq = " + parameters[10].ToString());
            if (Convert.ToInt32(parameters[11]) > 0)//total_trx
                sql.Append(" AND total_trx = " + parameters[11].ToString());
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
