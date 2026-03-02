using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOOSCheckingAccount:DVOBase
    {
       private int _accounttypeId;
       private string _accounttype;
       private string _desc;
       private string _keyvalue;
       private string _acct_desc;
       private int _acct_no;
       private string _department;
       private int _rowid;
       
        #region Constructor

        public DVOOSCheckingAccount()
        {
            _accounttypeId = 0;
            _accounttype=string.Empty;
            _desc=string.Empty;
            _keyvalue=string.Empty;
            _acct_desc=string.Empty;
            _acct_no=0;
            _department = string.Empty;
            _rowid = 0; 
        }

        #endregion Constructor

        #region Public Properties

       public int accounttypeId
       {
           get { return _accounttypeId; }
           set { _accounttypeId = value; }
       }
       public string accounttype
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }
       public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

       public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
       public string acct_desc
       {
           get { return _acct_desc; }
           set { _acct_desc = value; }
       }
       public int acct_no
       {
           get { return _acct_no; }
           set { _acct_no = value; }
       }

       public string department
       {
           get { return _department; }
           set { _department = value; }
       }

       public int rowid
       {
           get { return _rowid ; }
           set { _rowid  = value; }
       }


        #endregion Public Properties

        #region Stored-Procedures       

       public string DELETE_CHECK_SPNAME
       {
           get { return "usposchkdel"; }
       }

        public override string INSERT_SPNAME
        {
            get { return "USP_Cash_Accounts_Ins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usposchkaccupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_Cash_Acct_Del"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usposchkaccget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "usposchkaccall"; }
        }
        public override string TABLE_NAME
        {
            get { return "Master_Cash_Accounts"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PayrollGLAccounts.acct_type, Flex_struct_Header.[desc], PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,Master_Cash_Accounts.acct_no, Flex_struct_Header.id,Master_Cash_Accounts.Acct_No_ID FROM Master_Cash_Accounts,");
            sql.Append("PayrollGLAccounts,Flex_struct_Header where Master_Cash_Accounts.acct_no = PayrollGLAccounts.acct_no and PayrollGLAccounts.acct_type = Flex_struct_Header.accounttype ");
          
            if (Convert.ToInt32(parameters[0]) > 0)//acct_no                
                sql.Append(" and Master_Cash_Accounts.acct_no=" + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND Master_Cash_Accounts.rowid = " + parameters[1].ToString().Trim());

            //if (Convert.ToInt32(parameters[2]) > 0)
            //    sql.Append(" AND MasterEmployee.SelectDistrict IN (" + parameters[2].ToString() + ")");


            return sql.ToString();            
        }
        #endregion Stored-Procedures
    }
}
