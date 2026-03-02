using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOingappcls:DVOBase
    {
        private int _acd_id;
        private string _module;
        private string _dept;
        private decimal _line_amount;
        private decimal _total_amount;
        private string _global_skip;
        //Informix to be enhanced 
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #region indexer
            //public string this [int index]
            //{
            //get
            //{
            //    switch(index)
            //    {
            //        case 0 :
            //        return data[index];
            //        case 1:
            //        case 3:
            //        case 4:
            //        case 5:
            //        case 6:
            //        case 7:
                    
            //}
            //}
            //set
            //{
            //    data[index] = value;
            //}
            //}
        #endregion
        #region Constructor

        public DVOingappcls()
        {
            _acd_id = 0;
            _module = string.Empty;
            _dept = string.Empty;
            _line_amount = 0;
            _total_amount = 0;
            _global_skip = string.Empty;
            // Informix table to be enhanced 
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
        }
         
        #endregion Constructor

        #region Public Properties

        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public string module
        {
            get { return _module; }
            set { _module = value; }
        }
        public string dept
        {
            get { return _dept; }
            set { _dept = value; }
        }
        public decimal line_amount
        {
            get { return _line_amount; }
            set { _line_amount = value; }
        }
        public decimal total_amount
        {
            get { return _total_amount; }
            set { _total_amount = value; }
        }
        public string global_skip
        {
            get { return _global_skip; }
            set { _global_skip = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value.TrimEnd(); }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value.TrimEnd(); }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGLActins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLActupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLActdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLActget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspingappclsgetall"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
        }

        #endregion Stored-Procedures
    }
}
