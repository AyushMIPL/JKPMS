using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOApprovals :DVOBase
    {
        private int _acd_id;
        private DVOMisc _Header;
        private List<DVOMatrix> _Matrixes;
        #region Constructor
        public DVOApprovals()
        {
            _acd_id = 0;
            _Header=new DVOMisc();
            _Matrixes = new List<DVOMatrix>();
           }

        #endregion Constructor

        #region Public Properties

        public object Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = (DVOMisc)value;
            }
        }
        public object Matrixes 
        {
            get { 
            object obj=_Matrixes;
                return obj;
            }
            set { _Matrixes.Add((DVOMatrix)value); }
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
        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }
        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspDocApprInfogetall"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspDocAppInfgetall"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
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

        #endregion Stored-Procedures

    }
}
