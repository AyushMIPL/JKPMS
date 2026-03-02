using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMatrix : DVOBase
    {
        #region Private Variables

        int _RowId;
        /// <summary>
        /// 
        /// </summary>
        private int _orig_doc_no;
        /// <summary>
        ///
        /// </summary>
        private string _type;
        /// <summary>
        ///
        /// </summary>
        private string _doc_no;
        /// <summary>
        ///
        /// </summary>
        private string _vendor_buyer;
        /// <summary>
        ///
        /// </summary>
        private DateTime _doc_date;
        /// <summary>
        ///
        /// </summary>
        private decimal _required_approval;
        /// <summary>
        ///
        /// </summary>
        private decimal _current_approval;
        /// <summary>
        ///
        /// </summary>
        private string _keyvalue;
        /// <summary>
        ///
        /// </summary>
        private int _acd_id;
        /// <summary>
        ///
        /// </summary>
        private decimal _amount;
        /// <summary>
        ///
        /// </summary>
        private int _batch_id;
        /// <summary>
        /// 
        /// </summary>
        private int _Approve;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        private List<DVOAccountPermission> _DVOAccountPermissions;
        #endregion

        #region Constructor
        public DVOMatrix()
        {
            _RowId=0;
            _orig_doc_no = 0; ;
            _type = string.Empty;
            _doc_no = string.Empty;
            _vendor_buyer = string.Empty;
            _doc_date = DateTime.Now;
            _required_approval =0;
            _current_approval = 0;
            _keyvalue = string.Empty;
            _acd_id = 0;
            _amount = 0;
            _batch_id = 0;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
            _Approve = 0;
        }
        #endregion Constructor

        #region Public Properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int orig_doc_no
        {
            get { return _orig_doc_no; }
            set { _orig_doc_no = value; }
        }
        public string type
        {
            get { return _type; }
            set { _type = value.Trim(); }
        }
        public string doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value.Trim(); }
        }
        public string vendor_buyer
        {
            get { return _vendor_buyer; }
            set { _vendor_buyer = value.Trim(); }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public decimal required_approval
        {
            get { return _required_approval; }
            set { _required_approval = value; }
        }


        public decimal current_approval
        {
            get { return _current_approval; }
            set { _current_approval = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value.Trim(); }
        }
        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public int Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value.Trim(); }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value.Trim(); }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            // Insert not applicable for this  form
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspDocApprInfoupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspDocAppInfodel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspDocApprInfoget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspDocApprInfogetall"; }
        }
        public override string TABLE_NAME
        {
            get { return "matrix"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
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