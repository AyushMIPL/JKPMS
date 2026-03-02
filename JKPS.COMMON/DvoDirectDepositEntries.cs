using System;
using System.Collections.Generic;
using System.Text;



namespace JKPS.COMMON
{
   public  class DvoDirectDepositEntries:DVOBase 
    {//uspstypddrdget
       private string _type_code;

       private string  _comp_name;


        private string _entry_desc;          
        private int _dfi_immed  ;
        private int _svc_class;
        private int _batch_no;
        private string _batch_date;
        private string _create_date;
        private string _file_id;
        private string _used;
        private string _bank_code;
        private string _District;



      private int _ddocument;
       private string _empl_code;
       private int _dfi_dest;
       private int _chk_digit;
        private string _pay_date;
        private int _trans_code;
       private string _bank_acct_no;
        private Decimal _deposit_amount;
       private string _empl_name;
       private Decimal _trace_number;
        private int _doc_no;







       public DvoDirectDepositEntries()
       {
           _type_code = string.Empty;
        _comp_name=string.Empty;


          _entry_desc=string.Empty ;
          _dfi_immed = 0;
          _svc_class = 0;
           _batch_no=0;
          _batch_date=string.Empty ;
          _create_date=string.Empty ;
          _file_id=string.Empty ;
          _used=string.Empty ;
          _bank_code=string.Empty ;   
  




           
        _ddocument=0;
         _empl_code=string.Empty;
         _dfi_dest=0;
         _chk_digit=0;
          _pay_date=string.Empty;
          _trans_code=0;
         _bank_acct_no=string.Empty;
          _deposit_amount=0.0M;
         _empl_name=string.Empty;
         _trace_number=0.0M;
          _doc_no=0;
       }

        public string District
        {
          get { return _District; }
          set { _District = value; }
        }
      public string type_code
         {
             get { return _type_code; }
             set { _type_code = value; }
         }
       public  string comp_name
       {
           get { return _comp_name; }
           set { _comp_name = value; }
       }

       public  string entry_desc
       {
           get { return _entry_desc; }
           set { _entry_desc = value; }
       }

       public  int dfi_immed
       {
           get { return _dfi_immed; }
           set { _dfi_immed = value; }
       }

       public  int svc_class
       {
           get { return _svc_class; }
           set { _svc_class = value; }
       }
       public  int batch_no
       {
           get { return _batch_no; }
           set { _batch_no = value; }
       }


       public string batch_date
       {
           get { return _batch_date; }
           set { _batch_date = value; }
       }
       public string create_date
       {
           get { return _create_date; }
           set { _create_date = value; }
       }



       public string file_id
       {
           get { return _file_id; }
           set { _file_id = value; }
       }
       public string used
       {
           get { return _used; }
           set { _used = value; }
       }
       public string bank_code
       {
           get { return _bank_code; }
           set { _bank_code = value; }
       }



     

        public int  ddocument
       {
           get { return _ddocument; }
           set { _ddocument = value; }
       }
          public string empl_code
       {
           get { return _empl_code; }
           set { _empl_code = value; }
       }

       public int  dfi_dest
       {
           get { return _dfi_dest; }
           set { _dfi_dest = value; }
       }

        public int  chk_digit
       {
           get { return _chk_digit; }
           set { _chk_digit = value; }
       }

      public string pay_date
       {
           get { return _pay_date; }
           set { _pay_date = value; }
       }

      public int  trans_code
       {
           get { return _trans_code; }
           set { _trans_code = value; }
       }

       public string   bank_acct_no
       {
           get { return _bank_acct_no; }
           set { _bank_acct_no = value; }
       }
    public Decimal   deposit_amount
       {
           get { return _deposit_amount; }
           set { _deposit_amount = value; }
       }
    
        public string empl_name
       {
           get { return _empl_name; }
           set { _empl_name = value; }
       }
       
       
       public Decimal   trace_number
       {
           get { return _trace_number; }
           set { _trace_number = value; }
       }

        public int  doc_no
       {
           get { return _doc_no ; }
           set { _doc_no  = value; }
       }




       public string GET_INFO_FROM_PayControl
       {
           get { return "USP_PayControlGet"; }
       }
      public string GET_INFO_FROM_STYPDDREGET
       {
           get { return "USP_PayDDHeader_Get"; }
       }
       public string GET_COMP_NAME
       {
           get { return "USP_CompInfo"; }
       }

       public string GET_TRACE_NUMBER
       {
           get { return "USP_MaxTraceNo_Get"; }
       }

       public string INSERT_STYPDDRD
       {
           get { return "USP_DDDetailsins"; }
       }

     

        public override string INSERT_SPNAME
        {
            get { return "USP_DDHeaderIns"; }
        }



        public override string UPDATE_SPNAME
        {
            get { return "USP_PPEmplDDStatusUpd"; }
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

      
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select MasterEmpBankDetails.amount, MasterEmpBankDetails.bank_acct_no, MasterEmpBankDetails.bank_code,MasterEmpBankDetails.line_no,");
            sql.Append(" MasterEmpBankDetails.type, PayrollGLAccounts.acct_desc, PayrollGLAccounts.keyvalue, MasterEmployee.chk_digit, MasterBanks.dfi_dest,");
            sql.Append(" MasterEmployee.empl_code, Process_PayEmployee.cash_acct_no, Process_PayEmployee.cash_amount, Process_PayEmployee.check_no,");
            sql.Append(" Process_PayEmployee.department, Process_PayEmployee.doc_no, Process_PayEmployee.pay_date, MasterEmployee.last_name,MasterEmployee.first_name,MasterEmployee.middle_name,MasterEmpBankDetails.typeofacct,MasterEmployee.ApplicationReferenceNo,MasterEmpBankDetails.Applicant_bank_Ifsc_code from MasterEmployee, Process_PayEmployee, MasterEmpBankDetails,");
            sql.Append(" MasterBanks,PayrollGLAccounts where MasterEmployee.empl_code = Process_PayEmployee.empl_code ");
            sql.Append(" and MasterEmpBankDetails.empl_code = MasterEmployee.empl_code ");
            sql.Append(" and MasterBanks.bank_code = MasterEmpBankDetails.bank_code ");
            sql.Append(" and PayrollGLAccounts.acct_no = Process_PayEmployee.cash_acct_no");
            sql.Append(" and (Process_PayEmployee.deposit = 'Y' and Process_PayEmployee.print_check = 'N'");
            sql.Append(" and Process_PayEmployee.check_no is not null and Process_PayEmployee.check_no!=0 and Process_PayEmployee.ok_to_post = 'Y' )");
            
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters.Length > 1 && parameters[1] != null && parameters[1].ToString().Length > 0)
                sql.Append(" AND MasterEmployee.SelectDistrict IN (" + parameters[1].ToString() + ")");


            return sql.ToString();
        }
    }
}
