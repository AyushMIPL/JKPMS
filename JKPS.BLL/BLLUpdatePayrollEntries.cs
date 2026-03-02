using System;
using System.Collections.Generic;
using System.Text;

using JKPS.COMMON;
using JKPS.DL;
using System.Data;
namespace JKPS.BLL
{
  public class BLLUpdatePayrollEntries
  {
    public static decimal fica_wages = 0.0M;
    public static decimal futa_wages = 0.0M;
    public static List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();
    public static List<DVOPayrollProcess_PayEmployee> GetPayrollEntriesData(ref DVOPayrollProcess_PayEmployee objSearch)
    {
      List<DVOPayrollProcess_PayEmployee> ListDVOPayrollProcess_PayEmployee = new List<DVOPayrollProcess_PayEmployee>();
      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        //Object[] parameters = new object[25];
        //parameters[0] = objSearch.EmplCode;
        //parameters[1] = objSearch.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        //parameters[2] = objSearch.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        //parameters[3] = objSearch.print_check;
        //parameters[4] = objSearch.check_no;
        //parameters[5] = objSearch.ok_to_post;
        //parameters[6] = objSearch.Doc_no;
        //parameters[7] = objSearch.cash_amount;
        //parameters[8] = objSearch.deposit;
        //parameters[9] = objSearch.inc_gross;
        //parameters[10] = objSearch.inc_taxable;
        //parameters[11] = objSearch.ded_fica;
        //parameters[12] = objSearch.obl_futa;
        //parameters[13] = objSearch.ded_medicare;
        //parameters[14] = objSearch.obl_fica;
        //parameters[15] = objSearch.ded_fedtax;
        //parameters[16] = objSearch.obl_medicare;
        //parameters[17] = objSearch.ded_statax;
        //parameters[18] = objSearch.obl_other;
        //parameters[19] = objSearch.ded_loctax;
        //parameters[20] = objSearch.ded_other;
        //parameters[21] = objSearch.obl_total;
        //parameters[22] = objSearch.inc_net;
        //parameters[23] = objSearch.inc_expense;
        //parameters[24] = objSearch.total_hours;

        //using (DataSet ds = objDalBaseClass.GetData(objSearch.FIND_UPE(ref parameters)))
        //{
        DataSet ds = GetPayrollEntries_DataSet(ref objSearch);
        if (ds != null && ds.Tables.Count > 0)
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOPayrollProcess_PayEmployee obj = new DVOPayrollProcess_PayEmployee();
            obj.FirstName = dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty;
            obj.MiddleName = dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty;
            obj.LastName = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
            obj.LastPay = (dr[3] != DBNull.Value && dr[3].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : "01/01/1900";
            obj.PayPeriod = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
            obj.EmplCode = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
            obj.pay_date = (dr[6] != DBNull.Value && dr[6].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[6]) : Convert.ToDateTime("01/01/1900");
            obj.eop_date = (dr[7] != DBNull.Value && dr[7].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[7]) : Convert.ToDateTime("01/01/1900");
            obj.keyvalue = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
            obj.print_check = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;
            obj.check_no = dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0;
            obj.ok_to_post = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
            obj.Doc_no = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
            obj.cash_amount = dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0;
            obj.deposit = dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty;
            obj.inc_gross = dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0;
            obj.inc_taxable = dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0;
            obj.ded_fica = dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0;
            obj.ded_medicare = dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0;
            obj.ded_fedtax = dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0;
            obj.ded_statax = dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0;
            obj.ded_loctax = dr[21] != DBNull.Value ? Convert.ToDecimal(dr[21]) : 0;
            obj.ded_other = dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0;
            obj.obl_futa = dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0;
            obj.obl_fica = dr[24] != DBNull.Value ? Convert.ToDecimal(dr[24]) : 0;
            obj.obl_medicare = dr[25] != DBNull.Value ? Convert.ToDecimal(dr[25]) : 0;
            obj.obl_other = dr[26] != DBNull.Value ? Convert.ToDecimal(dr[26]) : 0;
            obj.obl_total = dr[27] != DBNull.Value ? Convert.ToDecimal(dr[27]) : 0;
            obj.inc_net = dr[28] != DBNull.Value ? Convert.ToDecimal(dr[28]) : 0;
            obj.inc_expense = dr[29] != DBNull.Value ? Convert.ToDecimal(dr[29]) : 0;
            obj.total_hours = dr[30] != DBNull.Value ? Convert.ToDecimal(dr[30]) : 0;
            obj.accrue_sick = dr[31] != DBNull.Value ? dr[31].ToString().Trim() : string.Empty;
            obj.accrue_vac = dr[32] != DBNull.Value ? dr[32].ToString().Trim() : string.Empty;
            obj.bonus = dr[33] != DBNull.Value ? dr[33].ToString().Trim() : string.Empty;
            obj.RowID = dr[34] != DBNull.Value ? Convert.ToInt32(dr[34]) : 0;
            obj.doc_date = dr[35] != DBNull.Value ? DateTime.ParseExact(Convert.ToDateTime(dr[35]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault) : Convert.ToDateTime("01/01/1900");//dr[35] != DBNull.Value ? Convert.ToDateTime(dr[35]) : Convert.ToDateTime(null);
            obj.EmplSSN = dr[36] != DBNull.Value ? dr[36].ToString().Trim() : string.Empty;
            obj.stataxcode = dr[37] != DBNull.Value ? dr[37].ToString().Trim() : string.Empty;
            obj.empflexdeptaccttype = dr[38] != DBNull.Value ? dr[38].ToString().Trim() : string.Empty;
            ListDVOPayrollProcess_PayEmployee.Add(obj);
          }
        //}
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ListDVOPayrollProcess_PayEmployee;
    }
    //Added by Sunil Pahwa for getting the docno 
    public static List<DVOPayrollProcess_PayEmployee> GetPayrollDocNo(ref DVOPayrollProcess_PayEmployee objSearchDoc)
    {
      List<DVOPayrollProcess_PayEmployee> ListDVOPayrollProcess_PayEmployee = new List<DVOPayrollProcess_PayEmployee>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = objSearchDoc.EmplCode;
        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollProcess_PayEmployee), objSearchDoc.GET_STYPAYRE_DOC_NO))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOPayrollProcess_PayEmployee obj = new DVOPayrollProcess_PayEmployee();
            obj.Doc_no = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
            ListDVOPayrollProcess_PayEmployee.Add(obj);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ListDVOPayrollProcess_PayEmployee;
    }

    public static DataSet GetPayrollEntries_DataSet(ref DVOPayrollProcess_PayEmployee objSearch)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] parameters = new object[25];
        parameters[0] = objSearch.EmplCode;
        parameters[1] = objSearch.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[2] = objSearch.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[3] = objSearch.print_check;
        parameters[4] = objSearch.check_no;
        parameters[5] = objSearch.ok_to_post;
        parameters[6] = objSearch.Doc_no;
        parameters[7] = objSearch.cash_amount;
        parameters[8] = objSearch.deposit;
        parameters[9] = objSearch.inc_gross;
        parameters[10] = objSearch.inc_taxable;
        parameters[11] = objSearch.ded_fica;
        parameters[12] = objSearch.obl_futa;
        parameters[13] = objSearch.ded_medicare;
        parameters[14] = objSearch.obl_fica;
        parameters[15] = objSearch.ded_fedtax;
        parameters[16] = objSearch.obl_medicare;
        parameters[17] = objSearch.ded_statax;
        parameters[18] = objSearch.obl_other;
        parameters[19] = objSearch.ded_loctax;
        parameters[20] = objSearch.ded_other;
        parameters[21] = objSearch.obl_total;
        parameters[22] = objSearch.inc_net;
        parameters[23] = objSearch.inc_expense;
        parameters[24] = objSearch.total_hours;

        DataSet ds = objDalBaseClass.GetData(objSearch.FIND_UPE(ref parameters));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "FirstName";
          ds.Tables[0].Columns[1].ColumnName = "MiddleName";
          ds.Tables[0].Columns[2].ColumnName = "LastName";
          ds.Tables[0].Columns[3].ColumnName = "LastPay";
          ds.Tables[0].Columns[4].ColumnName = "PayPeriod";
          ds.Tables[0].Columns[5].ColumnName = "EmplCode";
          ds.Tables[0].Columns[6].ColumnName = "pay_date";
          ds.Tables[0].Columns[7].ColumnName = "eop_date";
          ds.Tables[0].Columns[8].ColumnName = "keyvalue";
          ds.Tables[0].Columns[9].ColumnName = "print_check";
          ds.Tables[0].Columns[10].ColumnName = "check_no";
          ds.Tables[0].Columns[11].ColumnName = "ok_to_post";
          ds.Tables[0].Columns[12].ColumnName = "Doc_no";
          ds.Tables[0].Columns[13].ColumnName = "cash_amount";
          ds.Tables[0].Columns[14].ColumnName = "deposit";
          ds.Tables[0].Columns[15].ColumnName = "inc_gross";
          ds.Tables[0].Columns[16].ColumnName = "inc_taxable";
          ds.Tables[0].Columns[17].ColumnName = "ded_fica";
          ds.Tables[0].Columns[18].ColumnName = "ded_medicare";
          ds.Tables[0].Columns[19].ColumnName = "ded_fedtax";
          ds.Tables[0].Columns[20].ColumnName = "ded_statax";
          ds.Tables[0].Columns[21].ColumnName = "ded_loctax";
          ds.Tables[0].Columns[22].ColumnName = "ded_other";
          ds.Tables[0].Columns[23].ColumnName = "obl_futa";
          ds.Tables[0].Columns[24].ColumnName = "obl_fica";
          ds.Tables[0].Columns[25].ColumnName = "obl_medicare";
          ds.Tables[0].Columns[26].ColumnName = "obl_other";
          ds.Tables[0].Columns[27].ColumnName = "obl_total";
          ds.Tables[0].Columns[28].ColumnName = "inc_net";
          ds.Tables[0].Columns[29].ColumnName = "inc_expense";
          ds.Tables[0].Columns[30].ColumnName = "total_hours";
          ds.Tables[0].Columns[31].ColumnName = "accrue_sick";
          ds.Tables[0].Columns[32].ColumnName = "accrue_vac";
          ds.Tables[0].Columns[33].ColumnName = "bonus";
          ds.Tables[0].Columns[34].ColumnName = "RowID";
          ds.Tables[0].Columns[35].ColumnName = "doc_date";
          ds.Tables[0].Columns[36].ColumnName = "EmplSSN";
          ds.Tables[0].Columns[37].ColumnName = "stataxcode";
          ds.Tables[0].Columns[38].ColumnName = "empflexdeptaccttype";

          return ds;
        }
      }
      catch (Exception ex)
      {
        return new DataSet();
      }
      return new DataSet();
    }

    public static List<DVOPayrollstypayid> GetPayrollIncomesData(int doc_no)
    {
      List<DVOPayrollstypayid> ListDVOPayrollstypayid = new List<DVOPayrollstypayid>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = doc_no;
        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_PYINCOMES))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOPayrollstypayid obj = new DVOPayrollstypayid();
            obj.inc_code = dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty;
            obj.inc_rate = dr[1] != DBNull.Value ? (decimal?)(dr[1]) : null;
            obj.number = dr[2] != DBNull.Value ? (decimal?)(dr[2]) : null;
            obj.hours = dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null;
            obj.amount = dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null;
            obj.acct_no = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
            obj.lo_inc_amt = dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null;
            obj.hi_inc_amt = dr[7] != DBNull.Value ? (decimal?)(dr[7]) : null;
            obj.line_no = dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0;
            obj.RowID = dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0;
            obj.acctKeyvalue = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
            obj.acctAccountType = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
            obj.acctAccountTypeId = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
            obj.inc_type = dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty;
            ListDVOPayrollstypayid.Add(obj);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ListDVOPayrollstypayid;

    }

    public static List<DVOPayrollstypaydd> GetPayrollDeductionData(int doc_no)
    {
      List<DVOPayrollstypaydd> ListDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = doc_no;
        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_PYDEDUCTIONS))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOPayrollstypaydd obj = new DVOPayrollstypaydd();
            obj.ded_code = dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty;
            obj.ded_rate = dr[1] != DBNull.Value ? (decimal?)dr[1] : null;
            obj.amount = dr[2] != DBNull.Value ? (decimal?)dr[2] : null;
            obj.lo_ded_amt = dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null;
            obj.hi_ded_amt = dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null;
            obj.acct_no = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
            obj.line_no = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
            obj.RowID = dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0;
            obj.pay_limit = dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null;
            obj.yearrollover = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;
            obj.acctKeyvalue = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
            obj.acctAccoutType = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
            obj.accountTypeId = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
            obj.ded_type = dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty;
            obj.ded_taxred = dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty;
            obj.ded_limit = dr[15] != DBNull.Value ? (decimal?)(dr[15]) : null;
            obj.ded_ytd = dr[16] != DBNull.Value ? (decimal?)dr[16] : null;
            ListDVOPayrollstypaydd.Add(obj);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ListDVOPayrollstypaydd;

    }

    public static List<DVOPayrollStypayod> GetPayrollObligationData(int doc_no)
    {
      List<DVOPayrollStypayod> ListDVOPayrollStypayod = new List<DVOPayrollStypayod>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = doc_no;
        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollStypayod), (new DVOPayrollStypayod()).FIND_PYOBLICATIONS))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOPayrollStypayod obj = new DVOPayrollStypayod();
            obj.obl_code = dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty;
            obj.obl_rate = dr[1] != DBNull.Value ? (decimal?)dr[1] : null;
            obj.amount = dr[2] != DBNull.Value ? (decimal?)dr[2] : null;
            obj.acct_no = dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0;
            obj.bal_acct_no = dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0;
            obj.line_no = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
            obj.RowID = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
            obj.pay_limit = dr[7] != DBNull.Value ? (decimal?)(dr[7]) : null;
            obj.acctKeyvalue = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
            obj.acctAccountType = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;
            obj.balacctKeyvalue = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
            obj.balacctAccountType = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
            obj.acctAccountTypeId = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
            obj.balacctAccountTypeId = dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0;
            obj.obl_limit = dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null;
            obj.obl_ytd = dr[15] != DBNull.Value ? (decimal?)dr[15] : null;
            ListDVOPayrollStypayod.Add(obj);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ListDVOPayrollStypayod;
    }

    public static string GetKeyvalue(int acct_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[1];
      parameters[0] = acct_no;
      Object O = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOGLPayrollGLAccounts()).GET_KEYVALUE);
      if (O != null)
        return O.ToString().Trim();
      else
        return string.Empty;
    }

    public static int UpdatePayrollEntries_Old(ref object objTransaction,
        ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee,
        ref List<DVOPayrollstypayid> objlistDVOPayrollstypayid,
        ref List<DVOPayrollstypaydd> objlistDVOPayrollstypaydd,
        ref List<DVOPayrollStypayod> objlistDVOPayrollStypayod
      //,ref List<DVOPayrollstypayid> objlistDeleteDVOPayrollstypayid,
      //ref List<DVOPayrollstypaydd> objlistDeleteDVOPayrollstypaydd,
      //ref List<DVOPayrollStypayod> objlistDeleteDVOPayrollStypayod
        )
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        UpdateStypayre(ref objTransaction, ref objDVOPayrollProcess_PayEmployee);
        DeleteAllPayIncome(ref objTransaction, objDVOPayrollProcess_PayEmployee.Doc_no);
        UpdateStypayid(ref objTransaction, ref objlistDVOPayrollstypayid);
        DeleteAllPayDeduction(ref objTransaction, objDVOPayrollProcess_PayEmployee.Doc_no);
        UpdateStypaydd(ref objTransaction, ref objlistDVOPayrollstypaydd);
        DeleteAllPayObligation(ref objTransaction, objDVOPayrollProcess_PayEmployee.Doc_no);
        UpdateStypayod(ref objTransaction, ref objlistDVOPayrollStypayod);
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      if (!statusObjTransaction)
        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      return 1;

    }

    public static int UpdatePayrollEntries(ref object objTransaction,
        ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee,
        ref List<DVOPayrollstypayid> objNewlistDVOPayrollstypayid,
        ref List<DVOPayrollstypayid> objUpdatelistDVOPayrollstypayid,
        ref List<DVOPayrollstypayid> objDeletelistDVOPayrollstypayid,
        ref List<DVOPayrollstypaydd> objNewlistDVOPayrollstypaydd,
        ref List<DVOPayrollstypaydd> objUpdatelistDVOPayrollstypaydd,
        ref List<DVOPayrollstypaydd> objDeletelistDVOPayrollstypaydd,
        ref List<DVOPayrollStypayod> objNewlistDVOPayrollStypayod,
        ref List<DVOPayrollStypayod> objUpdatelistDVOPayrollStypayod,
        ref List<DVOPayrollStypayod> objDeletelistDVOPayrollStypayod
        )
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        UpdateStypayre(ref objTransaction, ref objDVOPayrollProcess_PayEmployee);

        InsertPayrollIncomes(ref objTransaction, ref objNewlistDVOPayrollstypayid);
        UpdateStypayid(ref objTransaction, ref objUpdatelistDVOPayrollstypayid);
        DeletePayrollIncomes(ref objTransaction, ref objDeletelistDVOPayrollstypayid);

        InsertPayrollDeductions(ref objTransaction, ref objNewlistDVOPayrollstypaydd);
        UpdateStypaydd(ref objTransaction, ref objUpdatelistDVOPayrollstypaydd);
        DeletePayrollDeductions(ref objTransaction, ref objDeletelistDVOPayrollstypaydd);

        InsertPayrollObligations(ref objTransaction, ref objNewlistDVOPayrollStypayod);
        UpdateStypayod(ref objTransaction, ref objUpdatelistDVOPayrollStypayod);
        DeletePayrollObligations(ref objTransaction, ref objDeletelistDVOPayrollStypayod);
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      if (!statusObjTransaction)
        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      return 1;

    }


    public static int InsertPayrollEntries(ref object objTransaction,
        ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee,
        ref List<DVOPayrollstypayid> objNewlistDVOPayrollstypayid,
        ref List<DVOPayrollstypaydd> objNewlistDVOPayrollstypaydd,
        ref List<DVOPayrollStypayod> objNewlistDVOPayrollStypayod,
        bool TimeCardUsedForPayroll, int UsedTimeCardNo,
        out int newDocNo)
    {
      newDocNo = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        if (objDVOPayrollProcess_PayEmployee.bonus.Trim().Length <= 0)
          objDVOPayrollProcess_PayEmployee.bonus = "N";

        object[] parameters = new object[32];
        parameters[0] = objDVOPayrollProcess_PayEmployee.EmplCode;
        parameters[1] = objDVOPayrollProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[2] = objDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[3] = objDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[4] = objDVOPayrollProcess_PayEmployee.print_check;
        parameters[5] = objDVOPayrollProcess_PayEmployee.Cash_acct_no;
        parameters[6] = "000";// objDVOPayrollProcess_PayEmployee.Department;
        parameters[7] = objDVOPayrollProcess_PayEmployee.cash_amount;
        parameters[8] = objDVOPayrollProcess_PayEmployee.check_no;
        parameters[9] = objDVOPayrollProcess_PayEmployee.inc_gross;
        parameters[10] = objDVOPayrollProcess_PayEmployee.ded_fica;
        parameters[11] = objDVOPayrollProcess_PayEmployee.inc_taxable;
        parameters[12] = objDVOPayrollProcess_PayEmployee.ded_medicare;
        parameters[13] = objDVOPayrollProcess_PayEmployee.ded_fedtax;
        parameters[14] = objDVOPayrollProcess_PayEmployee.ded_statax;
        parameters[15] = objDVOPayrollProcess_PayEmployee.ded_loctax;
        parameters[16] = objDVOPayrollProcess_PayEmployee.ded_other;
        parameters[17] = objDVOPayrollProcess_PayEmployee.obl_futa;
        parameters[18] = objDVOPayrollProcess_PayEmployee.obl_fica;
        parameters[19] = objDVOPayrollProcess_PayEmployee.obl_medicare;
        parameters[20] = objDVOPayrollProcess_PayEmployee.obl_other;
        parameters[21] = objDVOPayrollProcess_PayEmployee.obl_total;
        parameters[22] = objDVOPayrollProcess_PayEmployee.inc_net;
        parameters[23] = objDVOPayrollProcess_PayEmployee.inc_expense;
        parameters[24] = objDVOPayrollProcess_PayEmployee.total_hours;
        parameters[25] = objDVOPayrollProcess_PayEmployee.ok_to_post;
        parameters[26] = objDVOPayrollProcess_PayEmployee.accrue_sick;
        parameters[27] = objDVOPayrollProcess_PayEmployee.accrue_vac;
        parameters[28] = objDVOPayrollProcess_PayEmployee.bonus;
        parameters[29] = objDVOPayrollProcess_PayEmployee.deposit;
        parameters[30] = TimeCardUsedForPayroll ? 1 : 0;
        parameters[31] = UsedTimeCardNo;

        object InsResult = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollProcess_PayEmployee.INSERT_STYPAYRE_PY_SCREEN);
        if (InsResult != DBNull.Value && InsResult.ToString().Trim().Length > 0 && Convert.ToInt32(InsResult) > 0)
        {
          newDocNo = Convert.ToInt32(InsResult);
          // post income detail                  
          for (int j = 0; j < objNewlistDVOPayrollstypayid.Count; j++)
          {
            objNewlistDVOPayrollstypayid[j].Doc_no = newDocNo;
            bool id_ststus = JKPS.BLL.BLLPayrollAutopayNew.InsertIntoStypayid(objNewlistDVOPayrollstypayid[j], ref objTransaction);
            if (!id_ststus)
              throw new Exception("Error occured during insertion of Payroll-Income.");
          }
          //post deduction detail
          for (int j = 0; j < objNewlistDVOPayrollstypaydd.Count; j++)
          {
            objNewlistDVOPayrollstypaydd[j].Doc_no = newDocNo;
            bool dd_status = JKPS.BLL.BLLPayrollAutopayNew.InsertIntoStypaydd(objNewlistDVOPayrollstypaydd[j], ref objTransaction);
            if (!dd_status)
              throw new Exception("Error occured during insertion of Payroll-Deduction.");
          }
          //post obligation detail
          for (int j = 0; j < objNewlistDVOPayrollStypayod.Count; j++)
          {
            objNewlistDVOPayrollStypayod[j].Doc_no = newDocNo;
            bool od_ststus = JKPS.BLL.BLLPayrollAutopayNew.InsertIntoStypayod(objNewlistDVOPayrollStypayod[j], ref objTransaction);
            if (!od_ststus)
              throw new Exception("Error occured during insertion of Payroll-Obligation.");
          }
        }
        else
        {
          throw new Exception("Error occured during insertion of new Payroll entry.");
        }
        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UpdateStypayre(ref object objTransaction, ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {

        Object[] Parameters = new object[35];

        Parameters[0] = objDVOPayrollProcess_PayEmployee.Doc_no;
        Parameters[1] = objDVOPayrollProcess_PayEmployee.EmplCode;
        Parameters[2] = objDVOPayrollProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        Parameters[3] = objDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        Parameters[4] = objDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        Parameters[5] = objDVOPayrollProcess_PayEmployee.print_check;
        Parameters[6] = objDVOPayrollProcess_PayEmployee.Cash_acct_no;
        Parameters[7] = objDVOPayrollProcess_PayEmployee.Department;
        Parameters[8] = objDVOPayrollProcess_PayEmployee.cash_amount;
        Parameters[9] = objDVOPayrollProcess_PayEmployee.check_no;
        Parameters[10] = objDVOPayrollProcess_PayEmployee.inc_gross;
        Parameters[11] = objDVOPayrollProcess_PayEmployee.ded_fica;
        Parameters[12] = objDVOPayrollProcess_PayEmployee.inc_taxable;
        Parameters[13] = objDVOPayrollProcess_PayEmployee.ded_medicare;
        Parameters[14] = objDVOPayrollProcess_PayEmployee.ded_fedtax;
        Parameters[15] = objDVOPayrollProcess_PayEmployee.ded_statax;
        Parameters[16] = objDVOPayrollProcess_PayEmployee.ded_loctax;
        Parameters[17] = objDVOPayrollProcess_PayEmployee.ded_other;
        Parameters[18] = objDVOPayrollProcess_PayEmployee.obl_futa;
        Parameters[19] = objDVOPayrollProcess_PayEmployee.obl_fica;
        Parameters[20] = objDVOPayrollProcess_PayEmployee.obl_medicare;
        Parameters[21] = objDVOPayrollProcess_PayEmployee.obl_other;
        Parameters[22] = objDVOPayrollProcess_PayEmployee.obl_total;
        Parameters[23] = objDVOPayrollProcess_PayEmployee.inc_net;
        Parameters[24] = objDVOPayrollProcess_PayEmployee.inc_expense;
        Parameters[25] = objDVOPayrollProcess_PayEmployee.total_hours;
        Parameters[26] = objDVOPayrollProcess_PayEmployee.ok_to_post;
        Parameters[27] = objDVOPayrollProcess_PayEmployee.accrue_sick;
        Parameters[28] = objDVOPayrollProcess_PayEmployee.accrue_vac;
        Parameters[29] = objDVOPayrollProcess_PayEmployee.bonus;
        Parameters[30] = objDVOPayrollProcess_PayEmployee.deposit;
        Parameters[31] = objDVOPayrollProcess_PayEmployee.RowID;
        Parameters[32] = objDVOPayrollProcess_PayEmployee.UpdateMachineInfo;
        Parameters[33] = objDVOPayrollProcess_PayEmployee.UpdateDate;
        Parameters[34] = objDVOPayrollProcess_PayEmployee.UpdateBy;

        object o = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollProcess_PayEmployee), true);
        if (Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    #region Payroll Incomes

    public static int InsertPayrollIncomes(ref object objTransaction, ref List<DVOPayrollstypayid> listDVOPayrollstypayid)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[13];
        foreach (DVOPayrollstypayid objDVOPayrollstypayid in listDVOPayrollstypayid)
        {
          parameters[0] = objDVOPayrollstypayid.Doc_no;
          parameters[1] = objDVOPayrollstypayid.line_no;
          parameters[2] = objDVOPayrollstypayid.inc_code == null ? null : objDVOPayrollstypayid.inc_code.Trim();
          parameters[3] = objDVOPayrollstypayid.inc_rate;
          parameters[4] = objDVOPayrollstypayid.number;
          parameters[5] = objDVOPayrollstypayid.hours;
          parameters[6] = objDVOPayrollstypayid.amount;
          parameters[7] = objDVOPayrollstypayid.acct_no;
          parameters[8] = "000";// objDVOPayrollstypayid.Department.Trim();
          parameters[9] = objDVOPayrollstypayid.mod_flag;
          parameters[10] = objDVOPayrollstypayid.add_code == null ? null : objDVOPayrollstypayid.add_code.Trim();
          parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
          parameters[12] = objDVOPayrollstypayid.hi_inc_amt;

          object InsResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);

          if (InsResult == DBNull.Value || InsResult == null || InsResult.ToString().Trim().Length <= 0 || Convert.ToInt32(InsResult) != 1)
            throw new Exception("Error occured during inserting income in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int UpdateStypayid(ref object objTransaction, ref List<DVOPayrollstypayid> objListDVOPayrollstypayid)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      try
      {
        if (objListDVOPayrollstypayid.Count > 0)
          foreach (DVOPayrollstypayid objDVOPayrollstypayid in objListDVOPayrollstypayid)
          {
            DVOPayrollstypayid obj = objDVOPayrollstypayid;
            UpdateData(ref objTransaction, ref obj);
          }

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int UpdateData(ref object objTransaction, ref DVOPayrollstypayid objDVOPayrollstypayid)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[14];
        Parameters[0] = objDVOPayrollstypayid.Doc_no;
        Parameters[1] = objDVOPayrollstypayid.inc_code;
        Parameters[2] = objDVOPayrollstypayid.inc_rate;
        Parameters[3] = objDVOPayrollstypayid.number;
        Parameters[4] = objDVOPayrollstypayid.hours;
        Parameters[5] = objDVOPayrollstypayid.amount;
        Parameters[6] = objDVOPayrollstypayid.acct_no;
        Parameters[7] = objDVOPayrollstypayid.lo_inc_amt;
        Parameters[8] = objDVOPayrollstypayid.hi_inc_amt;
        Parameters[9] = objDVOPayrollstypayid.RowID;
        Parameters[10] = objDVOPayrollstypayid.RowID;
        Parameters[11] = objDVOPayrollstypayid.UpdateMachineInfo;
        Parameters[12] = objDVOPayrollstypayid.UpdateDate;
        Parameters[13] = objDVOPayrollstypayid.UpdateBy;
        object o = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollstypayid), true);
        if (Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;

      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int DeletePayrollIncomes(ref object objTransaction, ref List<DVOPayrollstypayid> listDVOPayrollstypayid)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        foreach (DVOPayrollstypayid objDVOPayrollstypayid in listDVOPayrollstypayid)
        {
          parameters[0] = objDVOPayrollstypayid.Doc_no;
          parameters[1] = objDVOPayrollstypayid.RowID;

          object delResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollstypayid.DELETE_STYPAYID, true);

          if (delResult == DBNull.Value || delResult == null || delResult.ToString().Trim().Length <= 0 || Convert.ToInt32(delResult) != 1)
            throw new Exception("Error occured during deleting income in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int DeleteAllPayIncome(ref object objTransaction, int PayIncomeDocumentNo)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[1];
        Parameters[0] = PayIncomeDocumentNo;
        object o = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollstypayid), true);
        if (o == null || Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    #endregion Payroll Incomes

    #region Payroll Deductions

    public static int InsertPayrollDeductions(ref object objTransaction, ref List<DVOPayrollstypaydd> listDVOPayrollstypaydd)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[11];
        foreach (DVOPayrollstypaydd objDVOPayrollstypaydd in listDVOPayrollstypaydd)
        {
          parameters[0] = objDVOPayrollstypaydd.Doc_no;
          parameters[1] = objDVOPayrollstypaydd.line_no;
          parameters[2] = objDVOPayrollstypaydd.ded_code == null ? null : objDVOPayrollstypaydd.ded_code.Trim();
          parameters[3] = objDVOPayrollstypaydd.ded_rate;
          parameters[4] = objDVOPayrollstypaydd.amount;
          parameters[5] = objDVOPayrollstypaydd.acct_no;
          parameters[6] = "000";// objDVOPayrollstypaydd.Department.Trim();
          parameters[7] = objDVOPayrollstypaydd.mod_flag;
          parameters[8] = objDVOPayrollstypaydd.add_code == null ? null : objDVOPayrollstypaydd.add_code.Trim();
          parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
          parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;

          object InsResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
          if (InsResult == DBNull.Value || InsResult == null || InsResult.ToString().Trim().Length <= 0 || Convert.ToInt32(InsResult) != 1)
            throw new Exception("Error occured during inserting deduction in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int UpdateStypaydd(ref object objTransaction, ref List<DVOPayrollstypaydd> objListDVOPayrollstypaydd)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        if (objListDVOPayrollstypaydd.Count > 0)
          foreach (DVOPayrollstypaydd objDVOPayrollstypaydd in objListDVOPayrollstypaydd)
          {
            DVOPayrollstypaydd obj = objDVOPayrollstypaydd;
            UpdateData(ref objTransaction, ref obj);
          }
        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int UpdateData(ref object objTransaction, ref  DVOPayrollstypaydd objDVOPayrollstypaydd)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[12];
        Parameters[0] = objDVOPayrollstypaydd.Doc_no;
        Parameters[1] = objDVOPayrollstypaydd.ded_code;
        Parameters[2] = objDVOPayrollstypaydd.ded_rate;
        Parameters[3] = objDVOPayrollstypaydd.amount;
        Parameters[4] = objDVOPayrollstypaydd.acct_no;
        Parameters[5] = objDVOPayrollstypaydd.lo_ded_amt;
        Parameters[6] = objDVOPayrollstypaydd.hi_ded_amt;
        Parameters[7] = objDVOPayrollstypaydd.RowID;
        Parameters[8] = objDVOPayrollstypaydd.line_no;
        Parameters[9] = objDVOPayrollstypaydd.UpdateMachineInfo;
        Parameters[10] = objDVOPayrollstypaydd.UpdateDate;
        Parameters[11] = objDVOPayrollstypaydd.UpdateBy;
        object o = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollstypaydd), true);
        if (Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int DeletePayrollDeductions(ref object objTransaction, ref List<DVOPayrollstypaydd> listDVOPayrollstypaydd)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        foreach (DVOPayrollstypaydd objDVOPayrollstypaydd in listDVOPayrollstypaydd)
        {
          parameters[0] = objDVOPayrollstypaydd.Doc_no;
          parameters[1] = objDVOPayrollstypaydd.RowID;

          object delResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollstypaydd.DELETE_STYPARDD, true);
          if (delResult == DBNull.Value || delResult == null || delResult.ToString().Trim().Length <= 0 || Convert.ToInt32(delResult) != 1)
            throw new Exception("Error occured during deleting deduction in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int DeleteAllPayDeduction(ref object objTransaction, int PayDeductionDocumentNo)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[1];
        Parameters[0] = PayDeductionDocumentNo;
        object o = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollstypaydd), true);
        if (o == null || Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    #endregion Payroll Deductions

    #region Payroll Obligations

    public static int InsertPayrollObligations(ref object objTransaction, ref List<DVOPayrollStypayod> listDVOPayrollStypayod)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[11];
        foreach (DVOPayrollStypayod objDVOPayrollStypayod in listDVOPayrollStypayod)
        {
          parameters[0] = objDVOPayrollStypayod.Doc_no;
          parameters[1] = objDVOPayrollStypayod.line_no;
          parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
          parameters[3] = objDVOPayrollStypayod.obl_rate;
          parameters[4] = objDVOPayrollStypayod.amount;
          parameters[5] = objDVOPayrollStypayod.acct_no;
          parameters[6] = "000";// objDVOPayrollStypayod.Department.Trim();
          parameters[7] = objDVOPayrollStypayod.bal_acct_no;
          parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
          parameters[9] = objDVOPayrollStypayod.mod_flag;
          parameters[10] = objDVOPayrollStypayod.add_code.Trim();

          object InsResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
          if (InsResult == DBNull.Value || InsResult == null || InsResult.ToString().Trim().Length <= 0 || Convert.ToInt32(InsResult) != 1)
            throw new Exception("Error occured during inserting obligation in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int UpdateStypayod(ref object objTransaction, ref List<DVOPayrollStypayod> objListDVOPayrollStypayod)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {

        if (objListDVOPayrollStypayod.Count > 0)
          foreach (DVOPayrollStypayod objDVOPayrollStypayod in objListDVOPayrollStypayod)
          {
            DVOPayrollStypayod obj = objDVOPayrollStypayod;
            UpdateData(ref objTransaction, ref obj);
          }


        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int UpdateData(ref object objTransaction, ref  DVOPayrollStypayod objDVOPayrollStypayod)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[11];
        Parameters[0] = objDVOPayrollStypayod.Doc_no;
        Parameters[1] = objDVOPayrollStypayod.obl_code;
        Parameters[2] = objDVOPayrollStypayod.obl_rate;
        Parameters[3] = objDVOPayrollStypayod.amount;
        Parameters[4] = objDVOPayrollStypayod.acct_no;
        Parameters[5] = objDVOPayrollStypayod.bal_acct_no;
        Parameters[6] = objDVOPayrollStypayod.RowID;
        Parameters[7] = objDVOPayrollStypayod.RowID;
        Parameters[8] = objDVOPayrollStypayod.UpdateMachineInfo;
        Parameters[9] = objDVOPayrollStypayod.UpdateDate;
        Parameters[10] = objDVOPayrollStypayod.UpdateBy;
        object o = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollStypayod), true);
        if (Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;

    }

    public static int DeletePayrollObligations(ref object objTransaction, ref List<DVOPayrollStypayod> listDVOPayrollStypayod)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        foreach (DVOPayrollStypayod objDVOPayrollStypayod in listDVOPayrollStypayod)
        {
          parameters[0] = objDVOPayrollStypayod.Doc_no;
          parameters[1] = objDVOPayrollStypayod.RowID;

          object delResult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollStypayod.DELETE_STYPAYOD, true);
          if (delResult == DBNull.Value || delResult == null || delResult.ToString().Trim().Length <= 0 || Convert.ToInt32(delResult) != 1)
            throw new Exception("Error occured during deleting deduction in Payroll.");
        }
        parameters = null;
        objDALBaseClass = null;
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    public static int DeleteAllPayObligation(ref object objTransaction, int PayObligationDocumentNo)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        Object[] Parameters = new object[1];
        Parameters[0] = PayObligationDocumentNo;
        object o = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOPayrollStypayod), true);
        if (o == null || Convert.ToInt32(o) != 1)
          throw new Exception();

        Parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    #endregion Payroll Obligations


    public static string getyearrollover(string ded_code)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[1];
      parameters[0] = ded_code;
      Object O = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollstypaydd()).GET_YEARROLLOVER);
      if (O != null)
        return O.ToString().Trim();
      else
        return string.Empty;

    }
    public static string getbalanceamt(string ded_code, string empl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[3];
      parameters[0] = ded_code;
      parameters[1] = empl_code;
      parameters[2] = line_no;
      Object O = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollstypaydd()).GET_BALANCEAMT);
      if (O != null)
        return O.ToString().Trim();
      else
        return string.Empty;

    }

    //public void CalulatePayroll(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee, ref List<DVOPayrollstypayid> objListDVOPayrollstypayid, 
    //    ref List<DVOPayrollstypaydd> objListDVOPayrollstypaydd, ref List<DVOPayrollStypayod> objListDVOPayrollStypayod)
    //{  
    //    //
    //    objstypayre.cash_amount = 0.0M;
    //    objstypayre.inc_gross = 0.0M;
    //    objstypayre.inc_taxable = 0.0M;
    //    objstypayre.ded_fica = 0.0M;
    //    objstypayre.ded_medicare = 0.0M;
    //    objstypayre.ded_fedtax = 0.0M;
    //    objstypayre.ded_statax = 0.0M;
    //    objstypayre.ded_loctax = 0.0M;
    //    objstypayre.ded_other = 0.0M;
    //    objstypayre.obl_futa = 0.0M;
    //    objstypayre.obl_fica = 0.0M;
    //    objstypayre.obl_medicare = 0.0M;
    //    objstypayre.obl_other = 0.0M;
    //    objstypayre.obl_total = 0.0M;
    //    objstypayre.inc_net = 0.0M;
    //    objstypayre.inc_expense = 0.0M;
    //    objstypayre.total_hours = 0.0M;

    //    //Calulate Incomes
    //    foreach (DVOPayrollstypayid objDVOPayrollstypayid in objListDVOPayrollstypayid)
    //    {
    //         DVOPayrollstypayid obj=objDVOPayrollstypayid;
    //         CalculateIncomes(ref obj, ref objDVOPayrollProcess_PayEmployee);
    //    }
    //    //Calculate deductions that reduce taxable income
    //    //Get default data from stycntrc
    //    DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
    //    if (objstycntrcList==null)
    //    objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
    //    foreach (DVOPayrollstypaydd objDVOPayrollstypaydd in objListDVOPayrollstypaydd)
    //    {
    //        DVOPayrollstypaydd obj = objDVOPayrollstypaydd;
    //        //Get data from Stydedcr for current ded_code
    //        DVOPRDeductionCodesStydedcr objDVOPRDeductionCodesStydedcr = new DVOPRDeductionCodesStydedcr();
    //        objDVOPRDeductionCodesStydedcr.ded_code = obj.ded_code;
    //        List<DVOPRDeductionCodesStydedcr> listDVOPRDeductionCodesStydedcr = BLLPRDeductionCodesStydedcr.GetData(ref objDVOPRDeductionCodesStydedcr);



    //        if (listDVOPRDeductionCodesStydedcr[0].ded_taxred != "N"
    //            && listDVOPRDeductionCodesStydedcr[0].ded_taxred.Trim() != string.Empty)
    //        {
    //            objDVOPayrollstypaydd.dedflag = true;


    //        }
    //    }

    //}

    //public static void CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollstypayre, ref DVOPayrollstypayid objDVOPayrollstypayid)
    //{
    //    //Get default data from styinccr
    //    DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
    //    objDVOUpdateIncCode.inc_code = objDVOPayrollstypayid.inc_code;
    //    List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);

    //    if (objDVOPayrollstypayid.amount < 0)
    //    {
    //        objDVOPayrollstypayid.amount = 0.0M;
    //    }
    //    if (objDVOPayrollstypayid.hi_inc_amt == 0)
    //        objDVOPayrollstypayid.hi_inc_amt = listDVOUpdateIncCode[0].dflt_hi_inc_amt;
    //    if (objDVOPayrollstypayid.lo_inc_amt == 0)
    //        objDVOPayrollstypayid.lo_inc_amt = listDVOUpdateIncCode[0].dflt_lo_inc_amt;
    //    if (objDVOPayrollstypayid.acct_no == 0)
    //        objDVOPayrollstypayid.acct_no = listDVOUpdateIncCode[0].acct_no;

    //    //add to the gross wages or expenses/advances
    //    switch (listDVOUpdateIncCode[0].inc_type)
    //    {
    //        case "H":
    //            {
    //                objpayrollstypayre.inc_gross = objpayrollstypayre.inc_gross + objDVOPayrollstypayid.amount;
    //                fica_wages = fica_wages + objDVOPayrollstypayid.amount;
    //                futa_wages = futa_wages + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        case "E":
    //            {
    //                objpayrollstypayre.inc_expense = objpayrollstypayre.inc_expense + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        case "A":
    //            {
    //                objpayrollstypayre.inc_expense = objpayrollstypayre.inc_expense + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        case "F":
    //            {
    //                objpayrollstypayre.inc_gross =
    //      objpayrollstypayre.inc_gross + objDVOPayrollstypayid.amount;
    //                futa_wages = futa_wages + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        case "U":
    //            {
    //                objpayrollstypayre.inc_gross =
    //      objpayrollstypayre.inc_gross + objDVOPayrollstypayid.amount;
    //                fica_wages = fica_wages + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        case "B":
    //            {
    //                objpayrollstypayre.inc_gross =
    //      objpayrollstypayre.inc_gross + objDVOPayrollstypayid.amount;
    //                break;
    //            }
    //        default:
    //            {
    //                objpayrollstypayre.inc_gross =
    //                objpayrollstypayre.inc_gross + objDVOPayrollstypayid.amount;
    //                fica_wages = fica_wages + objDVOPayrollstypayid.amount;
    //                futa_wages = futa_wages + objDVOPayrollstypayid.amount;
    //                break;
    //            }

    //            if (listDVOUpdateIncCode[0].inc_type.Trim() != "F")
    //            {
    //                objstypayre.inc_taxable = objstypayre.inc_taxable + objPayrollIncomesglobal[i].amount;
    //            }
    //            objpayrollstypayre.total_hours = objpayrollstypayre.total_hours +
    //            objDVOPayrollstypayid.hours;
    //            objpayrollstypayre.inc_net = objpayrollstypayre.inc_gross;
    //    }
    //}

    //public static void CalcuteDeductions(ref DVOPayrollProcess_PayEmployee objpayrollstypayre, ref  DVOPayrollstypaydd objDVOPayrollstypaydd)
    //{ 


    //}

    //public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollstypayre,
    //    ref  DVOPayrollstypaydd objDVOPayrollstypaydd, ref DVOPRDeductionCodesStydedcr objStydedcr)
    //{
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

    //    if (objStydedcr.ded_type.Trim() ==string.Empty)
    //    {
    //        objStydedcr.ded_type = "T";
    //    }
    //    //calc the amount of the deduction relative to type
    //    if (objPayrolldeductionsglobal[i].ded_code.Trim() == objstycntrcList[0].statax_code.Trim())
    //    {
    //        //call the state tax calculation logic
    //        objPayrolldeductionsglobal[i].amount = state_calc(i);
    //    }
    //    switch (objPayrolldeductionsglobal[i].ded_type)
    //    {
    //        case "G":
    //            {
    //                //calculate amount using gross wage base
    //                // check for a tax table and retrieve the amount
    //                if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
    //                {
    //                    if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
    //                    {
    //                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
    //                    }
    //                    else
    //                    {
    //                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollstypayre.inc_gross));
    //                    }
    //                }
    //                else
    //                {
    //                    objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollstypayre.inc_taxable, objListemplforprocess[currentempnoid].PayPeriod);
    //                }
    //                break;
    //            }

    //        case "T":
    //            {
    //                //calculate amount using taxable wage base
    //                // check for a tax table and retrieve the amount
    //                if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != Convert.ToDecimal(null)))
    //                {
    //                    if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
    //                    {
    //                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
    //                    }
    //                    else
    //                    {
    //                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollstypayre.inc_taxable));
    //                    }
    //                }
    //                else
    //                {
    //                    objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollstypayre.inc_taxable, objListemplforprocess[currentempnoid].PayPeriod);
    //                }
    //                break;
    //            }
    //        case "U":
    //            {
    //                //calculate amount using futa wage base
    //                if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
    //                {
    //                    objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
    //                }
    //                else
    //                {
    //                    objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
    //                }
    //                break;
    //            }
    //        case "F":
    //            {
    //                //calculate amount using fica wage base
    //                if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
    //                {
    //                    objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
    //                }
    //                else
    //                {
    //                    objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
    //                }
    //                break;
    //            }
    //        case "H":
    //            {
    //                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollstypayre.total_hours));
    //                break;
    //            }
    //        case "N":
    //            {
    //                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
    //                break;
    //            }
    //        default:
    //            {
    //                objPayrolldeductionsglobal[i].amount = 0.0M;
    //                break;
    //            }

    //    }


    //    //make sure deduction is not greater than net or zero if net < 0
    //    if (objPayrolldeductionsglobal[i].amount > 0)
    //    {
    //        if (objpayrollstypayre.inc_net < 0)
    //        {
    //            objPayrolldeductionsglobal[i].amount = 0;
    //        }
    //        else
    //        {
    //            if (objPayrolldeductionsglobal[i].amount > objpayrollstypayre.inc_net)
    //            {
    //                objPayrolldeductionsglobal[i].amount = objpayrollstypayre.inc_net;
    //            }
    //        }
    //    }

    //    if (objPayrolldeductionsglobal[i].pay_limit != 0.0M)
    //    {
    //        if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
    //        {
    //            objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
    //        }
    //    }
    //    // check for limit
    //    if (objPayrolldeductionsglobal[i].amount > Max_deductions)
    //    {
    //        objPayrolldeductionsglobal[i].amount = Max_deductions;
    //    }

    //    // post amount to correct total
    //    if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
    //    {
    //        objpayrollstypayre.ded_fedtax = objpayrollstypayre.ded_fedtax + objPayrolldeductionsglobal[i].amount;
    //    }
    //    else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
    //    {
    //        objpayrollstypayre.ded_statax = objpayrollstypayre.ded_statax + objPayrolldeductionsglobal[i].amount;
    //    }
    //    else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
    //    {
    //        objpayrollstypayre.ded_loctax = objpayrollstypayre.ded_loctax + objPayrolldeductionsglobal[i].amount;
    //    }
    //    else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
    //    {
    //        objpayrollstypayre.ded_fica = objpayrollstypayre.ded_fica + objPayrolldeductionsglobal[i].amount;
    //    }
    //    else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
    //    {
    //        objpayrollstypayre.ded_medicare = objpayrollstypayre.ded_medicare + objPayrolldeductionsglobal[i].amount;
    //    }
    //    else
    //    {
    //        objpayrollstypayre.ded_other = objpayrollstypayre.ded_other + objPayrolldeductionsglobal[i].amount;
    //    }



    //    return objpayrollstypayre;
    //}
    //public decimal state_calc(string ded_type,)
    //{
    //    // define
    //    decimal wage_amount = 0;
    //    decimal statax_amount = 0;
    //    // set wage_amount appropriately
    //    if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
    //    {
    //        wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross;
    //    }
    //    else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
    //    {
    //        wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable;
    //    }
    //    else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
    //    {
    //        wage_amount = fica_wages;

    //    }
    //    else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
    //    {
    //        wage_amount = futa_wages;
    //    }
    //    else
    //    {
    //        wage_amount = 0;
    //    }
    //    if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
    //    {
    //        if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
    //        {
    //            statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

    //        }
    //        else
    //        {
    //            statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
    //        }
    //    }
    //    else
    //    {
    //        if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
    //        {
    //            statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
    //        }
    //        else
    //        {
    //            statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod);
    //        }

    //    }
    //    return statax_amount;
    //}
    //ref List<DVOPayrollStypayod> objListDVOPayrollStypayod
    public static int InsertBonusDetails(ref List<DVOPayrollstypayid> listDVOPayrollstypayid)
    {
      DataSet dsinfo = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      object objTransaction = null;
      objTransaction = objDALBaseClassHelper.GetTransactionObject();
      List<DVOPayrollstypayid> listDVOPayrolstypayid = new List<DVOPayrollstypayid>();

      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        if (listDVOPayrollstypayid.Count > 0)
          foreach (DVOPayrollstypayid obDVOPayrollstypayid in listDVOPayrollstypayid)
          {
            object[] parameters = new object[4];
            parameters[0] = obDVOPayrollstypayid.empl_code;
            parameters[1] = obDVOPayrollstypayid.inc_code;
            parameters[2] = obDVOPayrollstypayid.inc_type;
            parameters[3] = obDVOPayrollstypayid.inc_rate;

            //using (
            dsinfo = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, obDVOPayrollstypayid.INS_STYPAYID_INFO);
            if (dsinfo == null)
              throw new Exception();
            //{
            //foreach (DataRow dr in dsinfo.Tables[0].Rows)
            //{
            //    //DVOPayrollstypayid obj = new DVOPayrollstypayid();
            //    obDVOPayrollstypayid.error = Convert.ToInt32(dr[0]);
            //    obDVOPayrollstypayid.Doc_no = Convert.ToInt32(dr[1]);
            //    //listDVOPayrolstypayid.Add(obj);
            //}

            // }
          }

        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;





    }



    public static string GetDeductionTaxCode(ref DVOPayrollstypaydd objDVOPayrollstypaydd, ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objDVOPayrollstypaydd.ded_code;
        parameters[1] = objDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        object taxcode = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollautopay()).DeductionTaxCodeGet);
        if (taxcode != DBNull.Value && taxcode != null)
          return taxcode.ToString().Trim();
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return string.Empty;
    }

    public static decimal? GetDeductionYtd(ref DVOPayrollstypaydd objDVOPayrollstypaydd, ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameter1 = new object[2];
        parameter1[0] = objDVOPayrollProcess_PayEmployee.EmplCode;
        parameter1[1] = objDVOPayrollstypaydd.ded_code;
        object ded_ytd = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollstypaydd.FIND_stypayddytd);
        if (ded_ytd == DBNull.Value || ded_ytd == null || ded_ytd.ToString().Trim().Length <= 0)
          return null;
        else
          return Convert.ToDecimal(ded_ytd);
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return null;
    }

    public static bool GetDeductionAllowedAmountForPayPeriod(ref DVOPayrollstypaydd objDVOPayrollstypaydd, string payrePayrollDate, out decimal AllowanceAmount)
    {
      AllowanceAmount = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objDVOPayrollstypaydd.ded_code;
        parameters[1] = payrePayrollDate;
        object week_allow = objDALBaseClass.ExecuteScalar(ref parameters, objDVOPayrollstypaydd.FIND_week_allow);
        if (week_allow == DBNull.Value || week_allow == null || week_allow.ToString().Trim().Length <= 0)
          return true;
        else
          AllowanceAmount = Convert.ToDecimal(week_allow);
        return false;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return false;
    }

    public static DataSet GetDeductionTaxTableForPayPeriod(ref DVOPayrollstypaydd objDVOPayrollstypaydd,
        string payrePayrollDate, string pay_period, decimal t_total, string empMaritalStat)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] taxparameter = new object[5];
        taxparameter[0] = objDVOPayrollstypaydd.ded_code;
        taxparameter[1] = payrePayrollDate;
        taxparameter[2] = pay_period;
        taxparameter[3] = t_total;
        taxparameter[4] = empMaritalStat;
        ds = objDALBaseClass.GetData(ref taxparameter, objDVOPayrollstypaydd.GetType(), objDVOPayrollstypaydd.FIND_TaxValueGet);
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    public static bool IsDeductionTaxTableExist(ref DVOPayrollstypaydd objDVOPayrollstypaydd, string payrePayrollDate)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameter = new object[2];
        parameter[0] = objDVOPayrollstypaydd.ded_code;
        parameter[1] = payrePayrollDate;
        object obj = objDALBaseClass.ExecuteScalar(ref parameter, objDVOPayrollstypaydd.FIND_usp_tbl_check);
        if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
          return false;
        return true;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return false;
    }

    public static decimal? GetObligationYtd(ref DVOPayrollStypayod objDVOPayrollStypayod, ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameter1 = new object[2];
        parameter1[0] = objDVOPayrollProcess_PayEmployee.EmplCode;
        parameter1[1] = objDVOPayrollStypayod.obl_code;
        object Current_obl = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollStypayod.FIND_stypayodytd);
        if (Current_obl != DBNull.Value && Current_obl != null && Current_obl.ToString().Trim().Length > 0)
          return Convert.ToDecimal(Current_obl);
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    //Added by sunil Pahwa for Print Bonus of EMployee
    public static DataTable Get_EmpBonus_Details(string EmployeeType, string IncomeCodeAsBonus)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        DVOPayrollstypayid obDVOPayrollstypayid = new DVOPayrollstypayid();


        object[] Inc_parameter = new object[2];
        Inc_parameter[0] = IncomeCodeAsBonus;
        Inc_parameter[1] = EmployeeType;
        using (DataSet ds = objDALBaseClass.GetData(ref Inc_parameter, typeof(DVOPayrollstypayid), obDVOPayrollstypayid.GET_BONUS_INFO))
        {
          if (ds != null && ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "empl_code";
            ds.Tables[0].Columns[1].ColumnName = "pay_date";
            ds.Tables[0].Columns[2].ColumnName = "cash_amount";
            ds.Tables[0].Columns[3].ColumnName = "inc_gross";
            ds.Tables[0].Columns[4].ColumnName = "inc_taxable";
            ds.Tables[0].Columns[5].ColumnName = "inc_net";
            ds.Tables[0].Columns[6].ColumnName = "inc_expense";
            ds.Tables[0].Columns[7].ColumnName = "total_hours";
            ds.Tables[0].Columns[8].ColumnName = "inc_code";
            ds.Tables[0].Columns[9].ColumnName = "inc_rate";
            ds.Tables[0].Columns[10].ColumnName = "number";
            ds.Tables[0].Columns[11].ColumnName = "hours";
            ds.Tables[0].Columns[12].ColumnName = "amount";

            return ds.Tables[0];
          }

        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return new DataTable();
    }

    public static DataTable GetEmployeeDeductionTaxCalc(string deductioncode, DateTime paydate)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = deductioncode;
        parameters[1] = paydate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        using (DataSet ds = objDALBaseClass.GetData((new DVOMasterEmployeeDeductions()).GET_EMPDED_TAXCALC(ref parameters)))
        {
          if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "ded_code";
            ds.Tables[0].Columns[1].ColumnName = "week_allow";
            ds.Tables[0].Columns[2].ColumnName = "biweek_allow";
            ds.Tables[0].Columns[3].ColumnName = "smonth_allow";
            ds.Tables[0].Columns[4].ColumnName = "month_allow";
            ds.Tables[0].Columns[5].ColumnName = "quarter_allow";
            ds.Tables[0].Columns[6].ColumnName = "year_allow";
            ds.Tables[0].Columns[7].ColumnName = "misc_allow";
            ds.Tables[0].Columns[8].ColumnName = "tax_year";
            ds.Tables[0].Columns[9].ColumnName = "syear_allow";
            ds.Tables[0].Columns[10].ColumnName = "hrs_week_allow";
            ds.Tables[0].Columns[11].ColumnName = "hrs_biweek_allow";
            ds.Tables[0].Columns[12].ColumnName = "hrs_smonth_allow";
            ds.Tables[0].Columns[13].ColumnName = "hrs_month_allow";
            ds.Tables[0].Columns[14].ColumnName = "hrs_quarter_allow";
            ds.Tables[0].Columns[15].ColumnName = "hrs_syear_allow";
            ds.Tables[0].Columns[16].ColumnName = "hrs_year_allow";
            ds.Tables[0].Columns[17].ColumnName = "hrs_misc_allow";
            ds.Tables[0].Columns[18].ColumnName = "allow_or_limit";

            return ds.Tables[0];
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return new DataTable();
      }
      return new DataTable();
    }

    public static DataTable GetEmployeeDeductionTaxCalcDetail(string deductioncode, DateTime paydate,
        string pay_period, decimal t_total, string empMaritalStatus)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = deductioncode;
        parameters[1] = paydate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[2] = pay_period == null ? null : pay_period.Trim();
        parameters[3] = t_total;
        parameters[4] = empMaritalStatus == null ? null : empMaritalStatus.Trim();
        using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_TaxValueGet))
        {
          if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0];
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return new DataTable();
      }
      return new DataTable();
    }

    public static bool tbl_check(string dedcode, DateTime payrolldate)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        // this function is called to verify that if a table exists, that
        // the Tax Year matches the payroll date year.
        object[] parameter = new object[2];
        parameter[0] = dedcode;
        parameter[1] = payrolldate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_usp_tbl_check))
        {
          if (ds.Tables[0].Rows[0][0] == DBNull.Value || ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty)
          {
            //ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
            //ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return false;
      }
      return true;
    }

    public static DVOMasterEmployee GetEmployeePayrollStatus_Unposted(string EmoloyeeCode)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
      try
      {
        object[] parameter = new object[1];
        parameter[0] = EmoloyeeCode;

        using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).GET_EMP_PY_STATUS))
        {
          if (ds != null && ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "empl_code";//
            ds.Tables[0].Columns[1].ColumnName = "soc_sec_num";
            ds.Tables[0].Columns[2].ColumnName = "type_code";
            ds.Tables[0].Columns[3].ColumnName = "birthdate";
            ds.Tables[0].Columns[4].ColumnName = "first_name";
            ds.Tables[0].Columns[5].ColumnName = "middle_name";
            ds.Tables[0].Columns[6].ColumnName = "last_name";
            ds.Tables[0].Columns[7].ColumnName = "address1";
            ds.Tables[0].Columns[8].ColumnName = "address2";
            ds.Tables[0].Columns[9].ColumnName = "city";
            ds.Tables[0].Columns[10].ColumnName = "state";
            ds.Tables[0].Columns[11].ColumnName = "zip";
            ds.Tables[0].Columns[12].ColumnName = "phone";
            ds.Tables[0].Columns[13].ColumnName = "cash_acct";
            ds.Tables[0].Columns[14].ColumnName = "department";
            ds.Tables[0].Columns[15].ColumnName = "job_code";
            ds.Tables[0].Columns[16].ColumnName = "job_title";
            ds.Tables[0].Columns[17].ColumnName = "date_hired";
            ds.Tables[0].Columns[18].ColumnName = "terminated";
            ds.Tables[0].Columns[19].ColumnName = "empl_status";
            ds.Tables[0].Columns[20].ColumnName = "pay_period";
            ds.Tables[0].Columns[21].ColumnName = "allowances";
            ds.Tables[0].Columns[22].ColumnName = "state_allow";
            ds.Tables[0].Columns[23].ColumnName = "marital_stat";
            ds.Tables[0].Columns[24].ColumnName = "vac_code";
            ds.Tables[0].Columns[25].ColumnName = "vac_allowed";
            ds.Tables[0].Columns[26].ColumnName = "vac_used";
            ds.Tables[0].Columns[27].ColumnName = "sick_code";
            ds.Tables[0].Columns[28].ColumnName = "sick_allowed";
            ds.Tables[0].Columns[29].ColumnName = "sick_used";
            ds.Tables[0].Columns[30].ColumnName = "last_pay";
            ds.Tables[0].Columns[31].ColumnName = "hold_pymnt";
            ds.Tables[0].Columns[32].ColumnName = "statax_code";
            ds.Tables[0].Columns[33].ColumnName = "loctax_code";
            ds.Tables[0].Columns[34].ColumnName = "sick_accr_code";
            ds.Tables[0].Columns[35].ColumnName = "sick_accr_ctr";
            ds.Tables[0].Columns[36].ColumnName = "sick_lapse_date";
            ds.Tables[0].Columns[37].ColumnName = "vac_accr_code";
            ds.Tables[0].Columns[38].ColumnName = "vac_accr_ctr";
            ds.Tables[0].Columns[39].ColumnName = "vac_lapse_date";
            ds.Tables[0].Columns[40].ColumnName = "dir_dept";
            ds.Tables[0].Columns[41].ColumnName = "dfi_dest";
            ds.Tables[0].Columns[42].ColumnName = "chk_digit";
            ds.Tables[0].Columns[43].ColumnName = "bank_acct_no";
            ds.Tables[0].Columns[44].ColumnName = "state_udf";
            ds.Tables[0].Columns[45].ColumnName = "flexdeptaccttype";
            ds.Tables[0].Columns[46].ColumnName = "last_inc_date";
            ds.Tables[0].Columns[47].ColumnName = "appoint_date";
            ds.Tables[0].Columns[48].ColumnName = "gender";
            ds.Tables[0].Columns[49].ColumnName = "pystatus";

            if (ds.Tables[0].Rows.Count > 0)
            //if (ds.Tables[0].Rows[0]["pystatus"] != DBNull.Value
            //    && ds.Tables[0].Rows[0]["pystatus"].ToString().Length > 0
            //    && &Convert.ToInt32(ds.Tables[0].Rows[0]["pystatus"]) == 1)
            {
              //objDVOPayrollProcess_PayEmployee.EmplCode=ds.Tables[0].Rows[0][""]
            }
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return objDVOMasterEmployee;
    }
    /// To Delete Payroll Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of payroll</param>
    /// <returns></returns>
    public static int DeletePayrollEntries(ref object objTransaction, ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = objDVOPayrollProcess_PayEmployee.RowID;

        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayrollProcess_PayEmployee.DELETE_SPNAME);
        if (o == null)
          throw new Exception();
        else if (Convert.ToInt32(o) < 1)
          throw new Exception();

        parameters = null;
        objDALBaseClass = null;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }
  }
}