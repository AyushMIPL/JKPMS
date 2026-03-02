using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOGLTRanActVD :DVOBase
    {
       public string _orig_journal;
       public int _doc_no;
       public int _acct_no;
       public string _department;
       public decimal _amount;
       public string _debit_credit;
         #region Constructor

       public DVOGLTRanActVD()
        {
            _orig_journal=string.Empty;
            _doc_no=0;
            _acct_no = 0;
            _department = "000";
            _amount = 0;
            _debit_credit = "";
        }

        #endregion Constructor

        #region Public Properties


     public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }

     public Int32 doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

       public Int32 acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }

       public string department
        {
            get { return _department; }
            set { _department = value; }
        }
       public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
       public string debit_credit
       {
           get { return _debit_credit; }
           set { _debit_credit = value; }
       }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstgactvdins"; }
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
            get { return ""; }
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
       public string INSERT_STGACTVD
       {
           get { return "uspstgactvdins"; }
       }

       public string INSERT_PYTRX
       {
           get { return "uspinspytrx"; }
       }
       public string CHECK_STGACTVD
       {
           get { return "uspstgactvdchk"; }
       }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
