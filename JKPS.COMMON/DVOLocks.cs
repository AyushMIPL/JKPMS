using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOLocks :Object,IDisposable
    {
        public void Dispose() { }
        private string _TableName;
        private string _KeyFieldName;
        private object _KeyFieldValue;
        private string _DummyField;
        private string _DummyValue;
        public DVOLocks()
        {
            _TableName=string.Empty;
            _KeyFieldName=string.Empty;
            _KeyFieldValue=null;
            _DummyField = string.Empty;
            _DummyValue = string.Empty;
        }
        public DVOLocks(string pTableName,string pKeyFieldName,object pKeyFieldValue,string pDummyField, string pDummyValue)
        {
             _TableName =pTableName;
            _KeyFieldName = pKeyFieldName;
            _KeyFieldValue = pKeyFieldValue;
            _DummyField = pDummyField;
            _DummyValue = pDummyValue;
        }
        
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }
        public string KeyFieldName
        {
            get
            {
                return _KeyFieldName;
            }
            set
            {
                _KeyFieldName = value;
            }
        }
        public object KeyFieldValue
        {
            get
            {
                return _KeyFieldValue;
            }
            set
            {
                _KeyFieldValue = value;
            }
        }
        public string Dummyfield
        {
            get
            {
                return _DummyField;
            }
            set
            {
                _DummyField = value;
            }


        }
        public string DummyValue
        {
            get
            {
                return _DummyValue;
            }
            set
            {
                _DummyValue = value;
            }
        }
         public  string LOCK_SPName
        {
            get { return "UspLockTables"; }
        }
        public string FIND_SPNAME
        {
            get { return "sp_lock2"; }
        }
}
}
