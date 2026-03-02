using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public  class DVODeleteOldActivity:DVOBase
    {
        private string _ref_code;
        private string _act_code;
        private string _act_type;
        private int _doc_no;
        private DateTime _pay_date;

        #region Constructor
        public DVODeleteOldActivity()
        {
            _act_type = string.Empty;
            _act_code = string.Empty;
            _doc_no = 0;
            _ref_code = string.Empty;
        }
        #endregion

        #region Properties
        public string ref_code
        {
            get { return _ref_code; }
            set { _ref_code = value; }
        }
        public string act_type
        {
            get { return _act_type; }
            set { _act_type = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string act_code
        {
            get { return _act_code; }
            set { _act_code = value; }
        }
        #endregion

        #region StoreProcedure

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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "usp_st_tranr_get"; }
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

        #region Payrooll

        public string GET_PY_DELOLDACT
        {
            get { return "usppydeleteoact"; }
        }

        public string Get_Act_desc
        {
            get { return "usp_act_descget"; }
        }

        public string DeletePyOldAct
        {
            get { return "usppyold_act_del"; }
        }

        public string DeletePyOldTimecard
        {
            get { return "usppyodtimcrddel"; }
        }

        public string Gettranr
        {
            get { return "usp_xtranrget"; }
        }

        public string DeleteStxtranr
        {
            get { return "uspxtranrdel"; }
        }
        public string DeleteNotes
        {
            get { return "uspnotesdel"; }
        }
        #endregion

        public override string FIND_QUERY(ref object[] parameters)
        {
            return "";
        }
        #endregion
    }
}
