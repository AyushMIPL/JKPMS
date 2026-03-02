using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public abstract class DVOBase : Object, iDVO, IDisposable
    {
        public void Dispose() { }

        public abstract string INSERT_SPNAME
        {
            get;
        }

        public abstract string UPDATE_SPNAME
        {
            get;
        }

        public abstract string DELETE_SPNAME
        {
            get;
        }

        public abstract string FIND_SPNAME
        {
            get;
        }

        public abstract string ALL_SPNAME
        {
            get;
        }

        public abstract string TABLE_NAME
        {
            get;
        }

        public abstract int UNIQUE_ID
        {
            get;
        }

        public abstract string NOTES_TABLE_RECORD_ID
        {
            get;
            set;
        }

        public abstract string FIND_QUERY(ref Object[] parameters);
        
    }
}
