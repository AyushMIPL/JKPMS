using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)DVO For Fiscal BTL  Linker Detail line               Rajeev(D)                                 31/12/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOFiscalLinkerFisbtlkeyvaladd:DVOBase
    {
 
        private int _btlid;
        private string _acct_type;
        private string _keyvalue;
        private int _AddSubStatus;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;

        //added by rajeev to setup BRLLIN account
        //Added Date : 17/01/2010
        private string _description;

        private string _period_month;
        private string _period_year;
        private string _CrDrNet;

        public DVOFiscalLinkerFisbtlkeyvaladd()
        {
            _btlid = 0;
            _acct_type = string.Empty;
            _keyvalue = string.Empty;
            _AddSubStatus = 0;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;

            _description = string.Empty;

            _period_month = string.Empty;
            _period_year = string.Empty;
            _CrDrNet = string.Empty;
        }

        #region StartProperties

        public string CrDrNet
        {
            get { return _CrDrNet; }
            set { _CrDrNet = value; }
        }
        public int btlid
        {
            get { return _btlid; }
            set { _btlid = value; }
        }

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }

        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }

        public int AddSubStatus
        {
            get { return _AddSubStatus; }
            set { _AddSubStatus = value; }
        }

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
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


        //added by Rajeev To setup Bellin Account
        //Added Date : 17/01/10
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string period_month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME    
        {
            get { return "uspfisnbtldins"; }
        }

        public override string UPDATE_SPNAME   
        {
            get { return "uspfisnbtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbtlndtldel"; } 
        }

        public override string FIND_SPNAME            
        {
            get { return "uspfisnbtldget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "fisbtldtl"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {          

            return "";
        }       

        #endregion Stored-Procedures


        //public override string INSERT_SPNAME
        //{
        //    get { return "uspfisbtldtlins"; }
        //}

        //public override string UPDATE_SPNAME
        //{
        //    get { return "uspfisbtlupd"; }
        //}

        //public override string DELETE_SPNAME
        //{
        //    get { return "uspbtldtldel"; }
        //}

        //public override string TABLE_NAME
        //{
        //    get { return "fisbtlkeyvaladd"; }
        //}
    }
}
