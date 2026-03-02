using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVONssVoidr:DVOBase
    {
        #region Private Variable
        private int _rowId;
        private int _doc_no;
        private string _void_date;//date
        private string _ok_to_post;
        private string _account_no;
        private int _for_period;
        private string _voucher_no;
        private int _trx_rowid;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
        #endregion

        #region Constructor
        public DVONssVoidr()
        {
            _rowId = 0;
            _doc_no = 0;
            _void_date = "01/01/1900";//date
            _ok_to_post = string.Empty;
            _account_no = string.Empty;
            _for_period = 0;
            _voucher_no = string.Empty;
            _trx_rowid = 0;
            _InsertMachineInfo = DVOApplicationUserInfo.MachineInfo;
            _InsertDate = "01/01/1900";
            _InsertBy = DVOApplicationUserInfo.UserId;
            _UpdateMachineInfo = DVOApplicationUserInfo.MachineInfo;
            _UpdateDate = "01/01/1900";
            _UpdateBy = DVOApplicationUserInfo.UserId;
        }
        #endregion Constructor

        #region Properties

        public int RowId
        {
            get { return _rowId; }
            set { _rowId = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string account_no
        {
            get { return _account_no; }
            set { _account_no = value; }
        }
        public int for_preiod
        {
            get { return _for_period; }
            set { _for_period = value; }
        }
        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }
        public int trx_rowid
        {
            get { return _trx_rowid; }
            set { _trx_rowid = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
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
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        #endregion Properties

        #region Store Procedure
        public override string INSERT_SPNAME
        {
            get { return "uspnssvoidrins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspnssvoidrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspnssvoidrdel"; }
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
            get { return "nss_voidr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select rowid, doc_no,void_date,ok_to_post,account_no,for_period,voucher_no,trx_rowid ");
            sql.Append(" from nss_voidr where 1=1 and ok_to_post <> 'C'");
            
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    if(Convert.ToInt32(parameters[0])>0)
                    sql.Append(" and doc_no =" + parameters[0].ToString().Trim());
            
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0 && parameters[1].ToString().Trim()!="01/01/1900")
                    sql.Append(" and void_date ='" + parameters[1].ToString().Trim()+"'");
            
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0 )
                    sql.Append(" and ok_to_post ='" + parameters[2].ToString().Trim() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" and account_no ='" + parameters[3].ToString().Trim()+"'");
            
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    if(Convert.ToInt32(parameters[4])>0)
                    sql.Append(" and for_period =" + parameters[4].ToString().Trim());

            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    sql.Append(" and voucher_no ='" + parameters[5].ToString().Trim()+"'");

            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[6])>0)
                    sql.Append(" and rowid =" + parameters[6].ToString().Trim());

            return sql.ToString();
        }
        public string CHK_NSS_DETAIL
        {
            get { return "uspnssdetailchk"; }
        }
        public string CHK_VOIDR
        {
            get { return "uspnssvoidrchk"; }
        }
        public string GET_DETAILS
        {
            get { return "uspnssdtlget"; }
        }
        public string GET_NSSVOIDR
        {
            get { return "uspnss_voidrget"; }
        }
        public string GET_ARDOCNO
        {
            get { return "uspardocchk"; }
        }
        public string UPDARBAL
        {
            get { return "usprcashebalupd"; }
        }
        public string VDNSSTRX
        {
            get { return "uspnsstrxvd"; }
        }
        public string UPD_STS
        {
            get { return "uspnssvdrstsupd"; }
         }
        #endregion 
    }
}
