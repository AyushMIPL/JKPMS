using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public  class DVOPaymentToEmployee:DVOBase 
{
      private  string _startdate;
      private string _enddate;
      private int _doc_no;
      private string _ok_to_post;
      private string _period;
      private string _year;
      private string _type_code;
      private string _empl_code;
      private string _empl_name;
      public DVOPaymentToEmployee()
      {
          _startdate  = "1/1/1900";
          _enddate  = "1/1/2100";
          _doc_no = 0;
          _ok_to_post = "A";
          _period = string.Empty;
          _year = string.Empty;
          _type_code = string.Empty;
          _empl_code = string.Empty;
          _empl_name = string.Empty;
      }
      public string startdate
      {
          get{return _startdate;}
          set {_startdate=value;}
        
      }
       public string enddate
      {
          get{return _enddate ;}
          set { _enddate =value;}
        
      }
      public string ok_to_post
      {
          get { return _ok_to_post; }
          set { _ok_to_post = value; }
      }
      public string period
      {
          get { return _period; }
          set { _period = value; }
      }
      public string year
      {
          get { return _year; }
          set { _year = value; }
      }
      public string  type_code
      {
          get { return _type_code; }
          set { _type_code = value; }
      }
      public int  doc_no
      {
          get { return _doc_no ; }
          set { _doc_no  = value; }
      }
      public string empl_code
      {
        get { return _empl_code; }
        set { _empl_code = value; }
      }
      public string empl_name
      {
        get { return _empl_name; }
        set { _empl_name = value; }
      }

      public string  GET_EMPLOYEE_PAY_INFORMATION
      {
          get { return "uspPayProcess_PayIncomesget"; }
      }
      public string GET_EMPLOYEE_PAY_OBLIGATION
      {
          get { return "uspPayProcess_Payobligationsget"; }
      }

      public string GET_EMPLOYEE_PAY_DEDUCTION
      {
          get { return "usppaydeccodget"; }//uspPayProcess_PayDeductionsget
       }

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


      public string GET_SOC_SEC_RPT
      {
          get { return "USP_SocSecRptGet"; }
      }
      public string GET_OblAmtSSR
      {
          get { return "USP_SumOblAmtSsr"; }
      }
      public string GET_DedAmtSSR
      {
          get { return "USP_SumDedAmtSsr"; }
      }
      public string GET_AGDedAmtSSR
      {
          get { return "USP_AGDedAmtSsr"; }
      }
      public string GET_AGOblAmtSSR
      {
          get { return "USP_AGOblAmtSsr"; }
      }
      public string GET_AmtSumSSR
      {
          get { return "USP_SumAmtSsr"; }
      }


      public override string FIND_QUERY(ref Object[] parameters)
      {
          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("  select r.EmployeeID,r.empl_code,r. soc_sec_num ,r.type_code,r.first_name,");
          sql.Append(" r.last_name,r.address1,r.cash_acct,r.department,r.job_title,r.date_hired,");
          sql.Append(" r.empl_status,r.bank_acct_no,r.gender,");
          sql.Append(" e.doc_no,e.doc_date,e.pay_date,e.print_check,e.cash_acct_no,e.department,");
          sql.Append(" e.cash_amount,e.inc_gross,e.inc_taxable,e.ded_medicare,e.ded_fedtax,");
          sql.Append(" e.ded_loctax,e.ded_other,e.total_hours");
          sql.Append(" from MasterEmployee r,Process_PayEmployee e where r.empl_code=e.empl_code");

          if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//
              sql.Append(" and pay_date>='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
              sql.Append(" and pay_date<='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

  
          return sql.ToString();
      }

      //Added by Sarvjeet Verma On 01/05/2009...........................................
      public string FIND_SOC_SEC_RPT(ref Object[] parameters,ref string[] GlobalCode)
      {
          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append(" select MasterCompany.Address1,MasterCompany.Address2,MasterCompany.city,MasterCompany.company_name,MasterCompany.country,MasterCompany.state,MasterCompany.zip,");
          sql.Append(" Master_periods.end_date,Master_periods.start_date, PayControl.ein_number,MasterEmployee.appoint_date,MasterEmployee.empl_code,");
          sql.Append(" MasterEmployee.first_name,MasterEmployee.last_name,MasterEmployee.middle_name,MasterEmployee.pay_period,MasterEmployee.soc_sec_num,");
          sql.Append(" MasterEmployee.terminated,Process_PayEmployee.doc_no,Process_PayEmployee.pay_start_date");
          sql.Append(" from MasterEmployee, Process_PayEmployee, MasterCompany, PayControl, Master_periods where ");
          sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code");
          //sql.Append(" and MasterEmployee.type_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
          }
          sql.Append(" and Process_PayEmployee.ok_to_post IN ('P','Y') " );
          if (parameters[1].ToString().Trim()=="P")
          sql.Append(" and Process_PayEmployee.ok_to_post='P'");
          if (parameters[1].ToString().Trim() == "C")
          sql.Append(" and Process_PayEmployee.ok_to_post!='P'");
         //GlobalCode[0] gloAllDed
         //GlobalCode[1] gloLevy
         //GlobalCode[2] gloDed
         //GlobalCode[3] gloObl
         //GlobalCode[4] gloAllObl
         //GlobalCode[5] gloSevPay
          //Commented by ROhit *************************************
          //****************************Commented By Rohit 
          //sql.Append(" AND ( EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + ")");
          //sql.Append(" AND Process_PayDeductions.amount <> 0)");
          //sql.Append(" OR EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")");
          //sql.Append(" AND Process_Payobligations.amount <> 0))");

          //**********************
          if (parameters[2].ToString().Trim() == string.Empty)
          {
              sql.Append(" AND Process_PayEmployee.pay_start_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_start_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_start_date <= Master_periods.end_date");
              //sql.Append(" AND Process_PayEmployee.empl_code='12156'");
          }
          else
          {
              sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_start_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_start_date <= Master_periods.end_date ");
              //sql.Append(" AND Process_PayEmployee.empl_code='11875'");

          }
   

        
         //sql.Append("AND MasterEmployee.soc_sec_num ='175263' ");
         sql.Append(" ORDER BY MasterEmployee.last_name,MasterEmployee.first_name,MasterEmployee.soc_sec_num,Process_PayEmployee.pay_start_date");
         return sql.ToString();
      }

      public string FIND_Monthly_Deductions_RPT(ref Object[] parameters, ref string[] GlobalCode)
      {
          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append(" select masteremployee.empl_code , first_name,last_name,address1,address2,appoint_date,pay_date,ded_code,ded_rate,amount from MasterEmployee ,Process_PayEmployee ,Process_PayDeductions");
          sql.Append(" where MasterEmployee.Empl_Code=Process_PayEmployee.empl_code and Process_PayEmployee.doc_no=Process_PayDeductions.doc_no and Process_PayEmployee.ok_to_post NOT IN ('C','N') ");
       
          if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'PENS%'");
          }
          if(parameters[2].ToString() != string.Empty)
          {
          sql.Append("and month(Process_PayEmployee.pay_date) ='"+parameters[2].ToString()+"'");
          }
          if (parameters[3].ToString() != string.Empty)
          {
          sql.Append(" and YEAR(Process_PayEmployee.pay_date)='"+parameters[3].ToString()+"'");
          }
          if( GlobalCode[0]!=null )
          {
              sql.Append(" and ded_code="+ GlobalCode[0].ToString().Trim());
          }
       
        
          //sql.Append(" AND ( EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + ")");
          //sql.Append(" AND Process_PayDeductions.amount <> 0)");
          //sql.Append(" OR EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")");
          //sql.Append(" AND Process_Payobligations.amount <> 0))");
          //if (parameters[2].ToString().Trim() == string.Empty)
          //{
          //    sql.Append(" AND Process_PayEmployee.pay_start_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
          //    sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
          //    sql.Append(" AND Process_PayEmployee.pay_start_date >= Master_periods.start_date");
          //    sql.Append(" AND Process_PayEmployee.pay_start_date <= Master_periods.end_date");
          //    //sql.Append(" AND Process_PayEmployee.empl_code='12156'");
          //}
          //else
          //{
          //    sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
          //    sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
          //    sql.Append(" AND Process_PayEmployee.pay_start_date >= Master_periods.start_date");
          //    sql.Append(" AND Process_PayEmployee.pay_start_date <= Master_periods.end_date ");
          //    //sql.Append(" AND Process_PayEmployee.empl_code='12156'");

          //}
          ////sql.Append("AND MasterEmployee.soc_sec_num ='175263' ");
          //sql.Append(" ORDER BY MasterEmployee.last_name,MasterEmployee.first_name,MasterEmployee.soc_sec_num,Process_PayEmployee.pay_start_date");
          return sql.ToString();
      }

      public string FIND_OBL_AMT_SSR(ref Object[] parameters, ref string[] GlobalCode)
      {
          //GlobalCode[0] gloAllDed
          //GlobalCode[1] gloLevy
          //GlobalCode[2] gloDed
          //GlobalCode[3] gloObl
          //GlobalCode[4] gloAllObl
          //GlobalCode[5] gloSevPay

          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("SELECT  amount  FROM Process_Payobligations");
          sql.Append(" WHERE obl_code IN (" + GlobalCode[4] + ")"); 
          sql.Append(" AND doc_no IN (SELECT doc_no FROM Process_PayEmployee, MasterEmployee, Master_periods WHERE");
          sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code");
         // sql.Append(" and MasterEmployee.type_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
           if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
          }
          if (parameters[1].ToString().Trim() == "P")
              sql.Append(" and Process_PayEmployee.ok_to_post='P'");
          if (parameters[1].ToString().Trim() == "C")
              sql.Append(" and Process_PayEmployee.ok_to_post!='P'");

          sql.Append(" AND ( EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + "))");
        //  sql.Append(" AND Process_PayDeductions.amount <> 0)");
          sql.Append(" AND  EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")))");
       //   sql.Append(" AND Process_Payobligations.amount <> 0)");
          if (parameters[2].ToString().Trim() == string.Empty)
          {
              sql.Append(" AND Process_PayEmployee.pay_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          else
          {
              sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          
          return sql.ToString();
      }

      public string FIND_DED_AMT_SSR(ref Object[] parameters, ref string[] GlobalCode)
      {
          //GlobalCode[0] gloAllDed
          //GlobalCode[1] gloLevy
          //GlobalCode[2] gloDed
          //GlobalCode[3] gloObl
          //GlobalCode[4] gloAllObl
          //GlobalCode[5] gloSevPay

          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("SELECT  amount  FROM Process_PayDeductions");
          sql.Append(" WHERE ded_code IN (" + GlobalCode[2] + ")");
          sql.Append(" AND doc_no IN (SELECT doc_no FROM Process_PayEmployee, MasterEmployee, Master_periods WHERE");
          sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code");
         // sql.Append(" and MasterEmployee.type_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
          }
          if (parameters[1].ToString().Trim() == "P")
              sql.Append(" and Process_PayEmployee.ok_to_post='P'");
          if (parameters[1].ToString().Trim() == "C")
              sql.Append(" and Process_PayEmployee.ok_to_post!='P'");

          sql.Append(" AND  EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + ")");
         // sql.Append(" AND Process_PayDeductions.amount <> 0)");
          sql.Append(" AND EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")");
         // sql.Append(" AND Process_Payobligations.amount <> 0)");
          if (parameters[2].ToString().Trim() == string.Empty)
          {
              sql.Append(" AND Process_PayEmployee.pay_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          else
          {
              sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          return sql.ToString();
      }

      public string FIND_AG_DED_AMT_SSR(ref Object[] parameters, ref string[] GlobalCode)
      {
          //GlobalCode[0] gloAllDed
          //GlobalCode[1] gloLevy
          //GlobalCode[2] gloDed
          //GlobalCode[3] gloObl
          //GlobalCode[4] gloAllObl
          //GlobalCode[5] gloSevPay

          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("SELECT  amount  FROM Process_PayDeductions");
          sql.Append(" WHERE ded_code IN (" + GlobalCode[1] + ")");
          sql.Append(" AND doc_no IN (SELECT doc_no FROM Process_PayEmployee, MasterEmployee, Master_periods WHERE");
          sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code");
      //    sql.Append(" and MasterEmployee.type_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
          }
          if (parameters[1].ToString().Trim() == "P")
              sql.Append(" and Process_PayEmployee.ok_to_post='P'");
          if (parameters[1].ToString().Trim() == "C")
              sql.Append(" and Process_PayEmployee.ok_to_post!='P'");

          sql.Append(" AND  EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + ")");
         // sql.Append(" AND Process_PayDeductions.amount <> 0)");
          sql.Append(" AND EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")");
         // sql.Append(" AND Process_Payobligations.amount <> 0)");
          if (parameters[2].ToString().Trim() == string.Empty)
          {
              sql.Append(" AND Process_PayEmployee.pay_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          else
          {
              sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          return sql.ToString();
      }

      public string FIND_AG_OBL_AMT_SSR(ref Object[] parameters, ref string[] GlobalCode)
      {
          //GlobalCode[0] gloAllDed
          //GlobalCode[1] gloLevy
          //GlobalCode[2] gloDed
          //GlobalCode[3] gloObl
          //GlobalCode[4] gloAllObl
          //GlobalCode[5] gloSevPay

          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("SELECT  amount FROM Process_Payobligations");
          sql.Append(" WHERE obl_code IN (" + GlobalCode[5] + ")");
          sql.Append(" AND doc_no IN (SELECT doc_no FROM Process_PayEmployee, MasterEmployee, Master_periods WHERE");
          sql.Append(" MasterEmployee.empl_code = Process_PayEmployee.empl_code");
          //sql.Append(" and MasterEmployee.type_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[0].ToString().Trim() == "SALARY")
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
          }
          else
          {
              sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
          }
          if (parameters[1].ToString().Trim() == "P")
              sql.Append(" and Process_PayEmployee.ok_to_post='P'");
          if (parameters[1].ToString().Trim() == "C")
              sql.Append(" and Process_PayEmployee.ok_to_post!='P'");

          sql.Append(" AND  EXISTS (SELECT * FROM Process_PayDeductions WHERE Process_PayEmployee.doc_no =Process_PayDeductions.doc_no AND Process_PayDeductions.ded_code IN (" + GlobalCode[0] + ")");
          sql.Append(" AND Process_PayDeductions.amount <> 0)");
          sql.Append(" AND EXISTS (SELECT * FROM Process_Payobligations WHERE Process_PayEmployee.doc_no = Process_Payobligations.doc_no AND Process_Payobligations.obl_code IN (" + GlobalCode[4] + ")");
          sql.Append(" AND Process_Payobligations.amount <> 0)");
          if (parameters[2].ToString().Trim() == string.Empty)
          {
              sql.Append(" AND Process_PayEmployee.pay_date between '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          else
          {
              sql.Append(" AND Master_periods.period  ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Master_periods.period_year  ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
              sql.Append(" AND Process_PayEmployee.pay_date >= Master_periods.start_date");
              sql.Append(" AND Process_PayEmployee.pay_date <= Master_periods.end_date)");
          }
          return sql.ToString();
      }

      public string FIND_AMT_SUM_SSR(ref Object[] parameters, ref string[] GlobalCode)
      {
          //GlobalCode[0] gloAllDed
          //GlobalCode[1] gloLevy
          //GlobalCode[2] gloDed
          //GlobalCode[3] gloObl
          //GlobalCode[4] gloAllObl
          //GlobalCode[5] gloSevPay
          System.Text.StringBuilder sql = new StringBuilder();

          // get the amount for the Levy column
          sql.Append(" SELECT ISNULL(SUM(amount),0),0  FROM Process_PayDeductions");
          if (Convert.ToInt32( parameters[0])>0)
              sql.Append(" WHERE doc_no ="+Convert.ToInt32( parameters[0]));
          sql.Append(" AND Process_PayDeductions.ded_code in (" + GlobalCode[1] + ") UNION ");

          // get the amount for the Contrib column
          sql.Append(" SELECT ISNULL(SUM(amount),0),1  FROM Process_Payobligations");
          if (Convert.ToInt32(parameters[0]) > 0)
              sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_Payobligations.obl_code in (" + GlobalCode[4] + ") UNION ");
           
          sql.Append(" SELECT ISNULL(SUM(amount),0),2  FROM Process_PayDeductions");
          if (Convert.ToInt32(parameters[0]) > 0)
              sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_PayDeductions.ded_code in (" + GlobalCode[2] + ") UNION ");

          //get the amount for the Total Wages column
          sql.Append(" SELECT ISNULL(SUM(amount),0),3  FROM Process_PayIncomes, MasterIncCodes WHERE Process_PayIncomes.inc_code = MasterIncCodes.inc_code AND MasterIncCodes.inc_type <> 'F'");
          sql.Append(" and Process_PayIncomes.doc_no =" + Convert.ToInt32(parameters[0]) + " UNION ");

          //get the amount for the Total sevpay column
          sql.Append(" SELECT ISNULL(SUM(amount),0),4  FROM Process_Payobligations");
          if (Convert.ToInt32(parameters[0]) > 0)
              sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_Payobligations.obl_code in (" + GlobalCode[5] + ");");

          return sql.ToString();
      }

      //Added by Sarvjeet On 26/11/2009 , 
      //Used to get amounts for (LEVY ,SOCSEC ,CTRLTTL,TTTPE,TotalWages,BONUS,HOLPAY). 
      //in Create SocSecInfo. Text File..
      public string FIND_AMT_FOR_SSTF(ref Object[] parameters, ref string[] GlobalCode)
      {
          
          System.Text.StringBuilder sql = new StringBuilder();

          //0- LEVY
          sql.Append(" SELECT SUM(amount),0 FROM Process_PayDeductions");
          sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_PayDeductions.ded_code in (" + GlobalCode[1] + ") UNION ");

          //1- SOCSEC1
          sql.Append(" SELECT SUM(amount),1  FROM Process_Payobligations");
          sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_Payobligations.obl_code in (" + GlobalCode[4] + ") UNION ");
          //2- SOCSEC2
          sql.Append(" SELECT SUM(amount),2  FROM Process_PayDeductions");
          sql.Append(" WHERE doc_no =" + Convert.ToInt32(parameters[0]));
          sql.Append(" AND Process_PayDeductions.ded_code in (" + GlobalCode[2] + ") UNION ");

          //3- TTTPE
          //Total sevpay 
          sql.Append(" SELECT SUM(amount),3  FROM Process_Payobligations");
          sql.Append(" WHERE Process_Payobligations.obl_code in (" + GlobalCode[5] + ")");
          sql.Append(" AND doc_no =" + Convert.ToInt32(parameters[0]) + " UNION ");
          

          //4- TotalWages
          sql.Append(" SELECT SUM(amount),4  FROM Process_PayIncomes, MasterIncCodes WHERE Process_PayIncomes.inc_code = MasterIncCodes.inc_code AND MasterIncCodes.inc_type <> 'F'");
          sql.Append(" and Process_PayIncomes.doc_no =" + Convert.ToInt32(parameters[0]) + " UNION ");

          //5- BONUS
          sql.Append(" SELECT SUM(amount),5  FROM Process_PayIncomes where Process_PayIncomes.inc_code='BONUS' ");
          sql.Append(" and Process_PayIncomes.doc_no =" + Convert.ToInt32(parameters[0]) + " UNION ");
          //6- HOLPAY
          sql.Append(" SELECT SUM(amount),6  FROM Process_PayIncomes where Process_PayIncomes.inc_code='HOLPAY' ");
          sql.Append(" and Process_PayIncomes.doc_no =" + Convert.ToInt32(parameters[0]));

          return sql.ToString();
      }

      public string FIND_Deductions_RPT(ref Object[] parameters, ref string[] GlobalCode)
      {
        System.Text.StringBuilder sql = new StringBuilder();
        sql.Append(" select masteremployee.empl_code , first_name,last_name,address1,address2,appoint_date,pay_date,ded_code,ded_rate,amount from MasterEmployee ,Process_PayEmployee ,Process_PayDeductions");
        sql.Append(" where MasterEmployee.Empl_Code=Process_PayEmployee.empl_code and Process_PayEmployee.doc_no=Process_PayDeductions.doc_no and Process_PayEmployee.ok_to_post IN ('Y') And amount > 0 ");

        /*if (parameters[0].ToString().Trim() == "SALARY")
        {
            sql.Append(" and MasterEmployee.type_code LIKE 'SAL%'");
        }
        else if (parameters[0].ToString().Trim() == "WAGES")
        {
            sql.Append(" and MasterEmployee.type_code LIKE 'WAG%'");
        }*/

        if (parameters[0].ToString() != string.Empty)
        {
          sql.Append("and MasterEmployee.type_code LIKE '" + parameters[0].ToString().Trim() + "'");
        }
        if (parameters[1].ToString() != string.Empty)
        {
          sql.Append("and MasterEmployee.empl_code ='" + parameters[1].ToString() + "'");
        }
        if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != "1/1/1900")
        {
          sql.Append("and Process_PayEmployee.pay_date >='" + parameters[2].ToString() + "'");
        }
        if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != "1/1/2100")
        {
          sql.Append(" and Process_PayEmployee.pay_date <='" + parameters[3].ToString() + "'");
        }
        if (GlobalCode[0] != null)
        {
          sql.Append(" and ded_code='" + GlobalCode[0].ToString().Trim() + "'");
        }

        return sql.ToString();
      }

     
    }
}
