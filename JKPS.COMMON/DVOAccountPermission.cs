using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAccountPermission : Object
    {
        
        private int _acct_type_id;
        private string _accounttype;
        private string _acc_mask;
        private int _acd_id;
        private string _dept;
        private int _approval_level;
        private int _RowID;
        //private bool _Editable;
        public DVOAccountPermission()
        {
        _acct_type_id=0;
        _accounttype=string.Empty;
        _acc_mask=string.Empty;
        _acd_id=0;
        _dept=string.Empty;
        _approval_level=0;
        //_Editable = false;
        }
        
        public int acct_type_id
        {
            get
            {
                return _acct_type_id;
            }
            set
            {
                _acct_type_id = value;
            }
        }
        public string accounttype
        {
            get
            {
                return _accounttype;
            }
            set
            {
                _accounttype = value.Trim();
            }
        }
        public string acc_mask
        {
            get
            {
                return _acc_mask;
            }
            set
            {
                _acc_mask = value.Trim();
            }
        }
        public int acd_id
        {
            get
            {
                return _acd_id;
            }
            set
            {
                _acd_id = value;
            }
        }
        public string dept
        {
            get
            {
                return _dept;
            }
            set
            {
                _dept =value.Trim();
            }
        }
        public int approval_level
        {
            get
            {
                return _approval_level;
            }
            set
            {
                _approval_level = value;
            }
        }
        public int RowID
        {
            get
            {
                return _RowID;
            }
            set
            {
                _RowID = value;
            }

        }
    }
}
