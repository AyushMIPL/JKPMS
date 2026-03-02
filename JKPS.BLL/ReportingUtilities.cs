using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
using System.Collections;
using App.Data.ViewModels;
using System.Data.SqlClient;

namespace JKPS.BLL
{
  public class ReportingUtilities
  {

    public static DataSet GetUsersData(ref DVOSecUsers objSecUsers)
    {
      object[] parameters = new object[11];
      parameters[0] = objSecUsers.UserId;
      parameters[1] = objSecUsers.FirstName;
      parameters[2] = objSecUsers.LastName;
      parameters[3] = objSecUsers.LoginId;
      parameters[4] = objSecUsers.EmployeeId;
      parameters[5] = objSecUsers.Department;
      parameters[6] = objSecUsers.RoleId;
      parameters[7] = objSecUsers.Active;
      parameters[8] = "";
      parameters[9] = "";
      parameters[10] = objSecUsers.EmailId;



      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsUsersData = objDalBaseClass.GetData(ref parameters, typeof(DVOSecUsers));

      return dsUsersData;
    }
    public static DataSet GetUserModulesData(ref DVOSecUsers objSecUsers)
    {
      object[] parameters = new object[8];

      parameters[0] = 0;
      parameters[1] = objSecUsers.UserId;
      if (objSecUsers.RoleId == 0)
        parameters[2] = "";
      else
        parameters[2] = objSecUsers.RoleId.ToString();
      parameters[3] = objSecUsers.ModuleID;
      parameters[4] = objSecUsers.LoginId;
      parameters[5] = objSecUsers.EmployeeId;
      parameters[6] = objSecUsers.FirstName;
      parameters[7] = objSecUsers.LastName;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsUsersData = objDalBaseClass.GetData(ref parameters, typeof(DVOSecUserModule));
      DataSet dsUsersData = objDalBaseClass.GetData((new DVOSecUserModule()).FIND_USER_PERMISSION(ref parameters));
      return dsUsersData;
    }
    public static DataSet GetCompanyInfo()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCompanyInfo = objDalBaseClass.GetAllData(typeof(DVOSecCompany));
      return dsCompanyInfo;

    }
    public static DataSet GetLedgerAccunt(ref DVOGeneralLedger objGeneralLedger)
    {

      object[] parameters = new object[7];
      parameters[0] = objGeneralLedger.acct_type;
      parameters[1] = objGeneralLedger.acct_desc;
      parameters[2] = objGeneralLedger.incr_with_crdt;
      parameters[3] = objGeneralLedger.subtotal_group;
      parameters[4] = objGeneralLedger.keyvalue;
      parameters[5] = objGeneralLedger.acct_no;
      parameters[6] = objGeneralLedger.acct_cat;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet dsGenrelLedgerAccunt = objDalBaseClass.GetData(ref parameters, typeof(DVOGeneralLedger));
      return dsGenrelLedgerAccunt;


    }
    public static DataSet GetAppHistory(ref DVOAppHistory objAppHistory)
    {
      Object[] parameters = new object[4];
      parameters[0] = objAppHistory.doc_no;
      parameters[1] = objAppHistory.dateFrom;
      parameters[2] = objAppHistory.dateto;
      parameters[3] = objAppHistory.user_id;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsAppHistory = objDalBaseClass.GetData(ref parameters, typeof(DVOAppHistory));
      return dsAppHistory;
    }
    public static DataSet GetGenJourView(ref DVOGenJourView objDVOGenJourView)
    {
      object[] parameters = new object[1];
      parameters[0] = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsGenJour = objDalBaseClass.GetData(ref parameters, typeof(DVOGenJourView), objDVOGenJourView.FIND_SPNAME);
      return dsGenJour;
    }
    public static DataSet GetOrgStrucChart()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsOrgStrucChart = objDalBaseClass.GetAllData(typeof(DVOOrganiztionalStructureChart));
      return dsOrgStrucChart;
    }
    public static DataSet GetBatchInfo(ref DVOBatchInfo objbatchinfo)
    {
      object[] parameters = new object[13];
      parameters[0] = objbatchinfo.batch_id;
      parameters[1] = objbatchinfo.batchtype;
      parameters[2] = objbatchinfo.status;
      parameters[3] = objbatchinfo.Owner;
      parameters[4] = objbatchinfo.createdby;
      parameters[5] = objbatchinfo.CreateDateFrom;
      parameters[6] = objbatchinfo.CreateDateTo;
      parameters[7] = objbatchinfo.ApprovedBy;
      parameters[8] = objbatchinfo.approvdate_from;
      parameters[9] = objbatchinfo.approvdate_to;
      parameters[10] = objbatchinfo.postedBy;
      parameters[11] = objbatchinfo.postdate_from;
      parameters[12] = objbatchinfo.postdate_to;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsBatchInfo = objDalBaseClass.GetData(ref parameters, typeof(DVOBatchInfo));
      return dsBatchInfo;

    }
    public static List<DVODistinctMonth> GetDistinctMonth()
    {
      List<DVODistinctMonth> objDistMList = new List<DVODistinctMonth>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVODistinctMonth)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVODistinctMonth objDistM = new DVODistinctMonth();
          if (dr[0].ToString() != "")
          {
            objDistM.month = dr[0].ToString();
            objDistMList.Add(objDistM);
          }


        }
        return objDistMList;
      }
    }
    public static string GetCurr_periodstgcntrc()
    {
      List<DVOPrintVoteBook> objDistMList = new List<DVOPrintVoteBook>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetAllData(typeof(DVOPrintVoteBook));
      string str = ds.Tables[0].Rows[0][0].ToString();



      return str;

    }
    public static string GetCurr_yearstgcntrc()
    {
      List<DVOPrintVoteBook> objDistMList = new List<DVOPrintVoteBook>();
      DVOPrintVoteBook objPrintVBdetails = new DVOPrintVoteBook();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(typeof(DVOPrintVoteBook), objPrintVBdetails.GetCurrentYear);
      string str = ds.Tables[0].Rows[0][0].ToString();



      return str;

    }
    public static List<DVODistinctYear> GetDistinctYear()
    {
      List<DVODistinctYear> objDistYList = new List<DVODistinctYear>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVODistinctYear)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVODistinctYear objDistY = new DVODistinctYear();
          if (dr[0].ToString() != "")
          {
            objDistY.year = dr[0].ToString();
            objDistYList.Add(objDistY);
          }
        }
        return objDistYList;
      }
    }
    public static DataSet GetRecRevByobjectCode(ref DVORecRevByObj objRecrev)
    {
      Object[] parameters = new object[2];
      parameters[0] = objRecrev._month;
      parameters[1] = objRecrev._Year;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(ref parameters, typeof(DVORecRevByObj));
      return dsRecRev;
    }
    public static DataSet GetRecExpByobjectCode(ref DVORecExpByOBj objRecExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objRecExp._month;
      parameters[1] = objRecExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecExp = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj));
      return dsRecExp;
    }
    //public static DataSet GetCapRevByobjectCode(ref DVOCapRevByObj objCapRev)
    //{
    //    Object[] parameters = new object[2];
    //    parameters[0] = objCapRev._month;
    //    parameters[1] = objCapRev._Year;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsCapRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByObj));
    //    return dsCapRev;
    //}
    //public static DataSet GetCapRevByMinstryAndObjectCode(ref DVOCapRevByObj objCapExp)
    //{
    //    Object[] parameters = new object[3];
    //    parameters[0] = objCapExp._month;
    //    parameters[1] = objCapExp._Year;
    //    parameters[2] = objCapExp.Ministry;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsCapRev = objDalBaseClass.GetData(ref parameters, typeof(DVODistinctMonth));
    //    return dsCapRev;
    //}
    public static DataSet GetCapExpByobjectCode(ref DVOCapExpByObj objCapExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objCapExp._month;
      parameters[1] = objCapExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapExp = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByObj));
      return dsCapExp;
    }
    public static DataSet GetExpAndRevSummary(ref DVOCapExpByObj objCapExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objCapExp._month;
      parameters[1] = objCapExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByObj), objCapExp.GET_EXP_REV_ACT);
      dsExpRev.Tables[0].Columns[0].ColumnName = "v_acct_type ";
      dsExpRev.Tables[0].Columns[1].ColumnName = "v_this_month ";
      dsExpRev.Tables[0].Columns[2].ColumnName = "v_activity ";
      dsExpRev.Tables[0].Columns[3].ColumnName = "v_balance ";

      return dsExpRev;
    }
    public static DataSet GetExpAndRevEstamtSummary(ref DVOCapExpByObj objCapExp)
    {
      Object[] parameters = new object[1];
      parameters[0] = objCapExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByObj), objCapExp.GET_Est_Amt);
      dsExpRev.Tables[0].Columns[0].ColumnName = "acct_type";
      dsExpRev.Tables[0].Columns[1].ColumnName = "balance";

      return dsExpRev;
    }
    public static DataSet GetBankAccountBalance(ref DVOCapExpByObj objCapExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objCapExp._month;
      parameters[1] = objCapExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByObj), objCapExp.GET_BANKACCTBAL);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "this_month";
      ds.Tables[0].Columns[3].ColumnName = "activity";
      ds.Tables[0].Columns[4].ColumnName = "balance";
      return ds;

    }
    public static DataSet GetCapExpByMinistry(ref DVOCapExpByMinistry objCapExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objCapExp._month;
      parameters[1] = objCapExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapExp = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistry));
      return dsCapExp;
    }
    public static DataSet GetCapRevByMinistry(ref DVOCapRevByMinistry objCapRev)
    {
      Object[] parameters = new object[2];
      parameters[0] = objCapRev._month;
      parameters[1] = objCapRev._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByMinistry));
      return dsCapRev;
    }
    public static DataSet GetRecRevByMinistry(ref DVORecRevByMinistry objRecRev)
    {
      Object[] parameters = new object[2];
      parameters[0] = objRecRev._month;
      parameters[1] = objRecRev._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(ref parameters, typeof(DVORecRevByMinistry));
      return dsRecRev;
    }
    public static DataSet GetRecExpByMinistry(ref DVORecExpByMinistry objRecExp)
    {
      Object[] parameters = new object[2];
      parameters[0] = objRecExp._month;
      parameters[1] = objRecExp._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecExp = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByMinistry));
      return dsRecExp;
    }
    public static DataSet GetCapExpByMinistryFromW2(ref DVOCapExpByMinistryFormW2 objCapExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objCapExp._month;
      parameters[1] = objCapExp._Year;
      parameters[2] = objCapExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapExp = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistryFormW2));
      return dsCapExp;
    }
    public static List<DVOMinstry> GetAllMinistry()
    {
      List<DVOMinstry> objMinistry = new List<DVOMinstry>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMinstry)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOMinstry objMins = new DVOMinstry();
          if (dr[0].ToString() != "")
          {
            objMins.Keyvalue = dr[0].ToString();
            objMins.Desc = dr[1].ToString();
            objMinistry.Add(objMins);

          }


        }
        return objMinistry;
      }
    }
    public static DataSet GetAllMinistry(bool returnds)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMinstry));
      ds.Tables[0].TableName = "Allmin";
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "desc";
      return ds;

    }
    //public static DataSet GetRecExpByProgramFromW2A(ref DVORecExpByProgram_FromW2A_ objRecExp)
    //{
    //    Object[] parameters = new object[2];
    //    parameters[0] = objRecExp._month;
    //    parameters[1] = objRecExp._Year;

    //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
    //    DataSet dsRecExp = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByProgram_FromW2A_), objRecExp.FIND_SPNAME);
    //    return dsRecExp;
    //} 
    public static DataSet GetCapRevByDetailedObjcet(ref DVOCapRevByDetailedObjcet objRecRev)
    {
      Object[] parameters = new object[3];
      parameters[0] = objRecRev._month;
      parameters[1] = objRecRev._Year;
      parameters[2] = objRecRev.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByDetailedObjcet));
      return dsRecRev;

    }
    public static DataSet GetRecRevByDetailedObjcet(ref DVOCapRevByDetailedObjcet objRecRev)
    {
      Object[] parameters = new object[3];
      parameters[0] = objRecRev._month;
      parameters[1] = objRecRev._Year;
      parameters[2] = objRecRev.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(objRecRev.FIND_RECREV(ref parameters));
      return dsRecRev;

    }
    public static DataSet GetRecRevByDetailedObjcetCode(ref DVOCapRevByDetailedObjcet objRecRev)
    {
      Object[] parameters = new object[3];
      parameters[0] = objRecRev._month;
      parameters[1] = objRecRev._Year;
      parameters[2] = objRecRev.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByDetailedObjcet), objRecRev.GET_RECREVDTLOBJ);
      dsRecRev.Tables[0].Columns[0].ColumnName = "v_keyvalue";
      dsRecRev.Tables[0].Columns[1].ColumnName = "v_actualamt";
      dsRecRev.Tables[0].Columns[2].ColumnName = "v_desc";
      dsRecRev.Tables[0].Columns[3].ColumnName = "v_budgetedamt";
      return dsRecRev;

    }
    public static DataSet GetCapExpByMInistryAndObject(ref DVOCapExpByMinistryAndObject objCapExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objCapExp.month;
      parameters[1] = objCapExp.Year;
      parameters[2] = objCapExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapExp = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistryAndObject));
      return dsCapExp;
    }
    public static DataSet GetCapRevByMInistryAndObject(ref DVOCapExpByMinistryAndObject objCapExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objCapExp.month;
      parameters[1] = objCapExp.Year;
      parameters[2] = objCapExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCapRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistryAndObject), objCapExp.GET_CAPREV);
      dsCapRev.Tables[0].Columns[0].ColumnName = "ministry";
      dsCapRev.Tables[0].Columns[1].ColumnName = "objCode";
      dsCapRev.Tables[0].Columns[2].ColumnName = "p_desc";
      dsCapRev.Tables[0].Columns[3].ColumnName = "p_yeartodate";
      dsCapRev.Tables[0].Columns[4].ColumnName = "budgetedamt";
      dsCapRev.Tables[0].Columns[5].ColumnName = "year";
      return dsCapRev;
    }
    public static DataSet GetRecExpByMInistryAndObject(ref DVOCapExpByMinistryAndObject objCapExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objCapExp.month;
      parameters[1] = objCapExp.Year;
      parameters[2] = objCapExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecExp = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistryAndObject), objCapExp.GET_RECEXP);
      dsRecExp.Tables[0].Columns[0].ColumnName = "ministry";
      dsRecExp.Tables[0].Columns[1].ColumnName = "objCode";
      dsRecExp.Tables[0].Columns[2].ColumnName = "p_desc";
      dsRecExp.Tables[0].Columns[3].ColumnName = "p_yeartodate";
      dsRecExp.Tables[0].Columns[4].ColumnName = "budgetedamt";
      dsRecExp.Tables[0].Columns[5].ColumnName = "year";
      return dsRecExp;
    }
    public static DataSet GetRecRevByMInistryAndObject(ref DVOCapExpByMinistryAndObject objCapExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objCapExp.month;
      parameters[1] = objCapExp.Year;
      parameters[2] = objCapExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistryAndObject), objCapExp.GET_RECREV);
      dsRecRev.Tables[0].Columns[0].ColumnName = "ministry";
      dsRecRev.Tables[0].Columns[1].ColumnName = "objCode";
      dsRecRev.Tables[0].Columns[2].ColumnName = "p_desc";
      dsRecRev.Tables[0].Columns[3].ColumnName = "p_yeartodate";
      dsRecRev.Tables[0].Columns[4].ColumnName = "budgetedamt";
      dsRecRev.Tables[0].Columns[5].ColumnName = "year";
      return dsRecRev;
    }
    public static DataView GetVendorLedger_ByDocDate(ref DVOAPVendorInformation objDVOAPVendorInformation)
    {
      DataView dv = new DataView();
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOAPVendorInformation.vend_code;
        parameters[1] = objDVOAPVendorInformation.bus_name;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPVendorInformation), objDVOAPVendorInformation.GET_VENDOR_LEDGER_DOC);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "p_vend_code";
          ds.Tables[0].Columns[1].ColumnName = "v_doc_date";
          ds.Tables[0].Columns[2].ColumnName = "v_doc_type";
          ds.Tables[0].Columns[3].ColumnName = "v_inv_chk_no";
          ds.Tables[0].Columns[4].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[5].ColumnName = "v_amount";
          ds.Tables[0].Columns[6].ColumnName = "v_bus_name";

          dv = ds.Tables[0].DefaultView;
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dv;
    }
    public static DataView GetVendorLedger_ByInvDate(ref DVOAPVendorInformation objDVOAPVendorInformation)
    {
      DataView dv = new DataView();
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOAPVendorInformation.vend_code;
        parameters[1] = objDVOAPVendorInformation.bus_name;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPVendorInformation), objDVOAPVendorInformation.GET_VENDOR_LEDGER_INV);
        if (ds.Tables.Count > 0)
        {
          if (ds.Tables[0].Rows.Count > 0)
            foreach (DataRow dr in ds.Tables[0].Rows)
              if (dr[7] == DBNull.Value || dr[7] == null || dr[7].ToString() == string.Empty)
                dr[7] = dr[1];

          ds.Tables[0].Columns[0].ColumnName = "p_vend_code";
          ds.Tables[0].Columns[1].ColumnName = "v_doc_date";
          ds.Tables[0].Columns[2].ColumnName = "v_doc_type";
          ds.Tables[0].Columns[3].ColumnName = "v_inv_chk_no";
          ds.Tables[0].Columns[4].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[5].ColumnName = "v_amount";
          ds.Tables[0].Columns[6].ColumnName = "v_bus_name";
          ds.Tables[0].Columns[7].ColumnName = "v_inv_date";
          ds.Tables[0].Columns[8].ColumnName = "v_doc_no";

          dv = ds.Tables[0].DefaultView;
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dv;
    }
    public static DataSet GetRecExpByProgramFromW2A(ref DVORecExpByProgram_FromW2A_ objRecExp)
    {
      Object[] parameters = new object[3];
      parameters[0] = objRecExp._month;
      parameters[1] = objRecExp._Year;
      parameters[2] = objRecExp.Ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRecExp = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByProgram_FromW2A_));
      return dsRecExp;

    }
    public static DataSet GetAllMaster_Segment()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetAllData(typeof(DVOMaster_Segment));
      return dsSegvd;
    }
    public static DataView GetVendorAging_ByCode(ref DVOAPVendorInformation objDVOAPVendorInformation, string DateType, string Date)
    {
      DataView dv = new DataView();
      try
      {
        Object[] parameters = new object[4];
        parameters[0] = DateType;
        parameters[1] = Date;
        parameters[2] = objDVOAPVendorInformation.vend_code;
        parameters[3] = objDVOAPVendorInformation.bus_name;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPVendorInformation), objDVOAPVendorInformation.GET_VENDOR_AGING_CODE);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns.Add("v_srchDateType");
          ds.Tables[0].Columns.Add("v_srchAgeDate", typeof(DateTime));
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            if (DateType.ToUpper() == "D")
              dr["v_srchDateType"] = "Due Date";
            else if (DateType.ToUpper() == "I")
              dr["v_srchDateType"] = "Inv Date";

            if (Date != null)
              if (Date != string.Empty)
                dr["v_srchAgeDate"] = Convert.ToDateTime(Date);
          }

          ds.Tables[0].Columns[0].ColumnName = "p_vend_code";
          ds.Tables[0].Columns[1].ColumnName = "v_doc_date";
          ds.Tables[0].Columns[2].ColumnName = "v_doc_type";
          ds.Tables[0].Columns[3].ColumnName = "v_inv_chk_no";
          ds.Tables[0].Columns[4].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[5].ColumnName = "v_amount";
          ds.Tables[0].Columns[6].ColumnName = "v_bus_name";
          ds.Tables[0].Columns[7].ColumnName = "v_inv_date";
          ds.Tables[0].Columns[8].ColumnName = "v_doc_no";
          ds.Tables[0].Columns[9].ColumnName = "v_due_date";
          //ds.Tables[0].Columns[10].ColumnName = "v_crntdays";
          //ds.Tables[0].Columns[11].ColumnName = "v_1to30days";
          //ds.Tables[0].Columns[12].ColumnName = "v_31to60days";
          //ds.Tables[0].Columns[13].ColumnName = "v_over60days";

          dv = ds.Tables[0].DefaultView;
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dv;
    }
    public static DataView GetVendorCashRequirement(ref DVOAPVendorInformation objDVOAPVendorInformation, string DateType, string Date)
    {
      DataView dv = new DataView();
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOAPVendorInformation.vend_code;
        parameters[1] = objDVOAPVendorInformation.bus_name;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPVendorInformation), objDVOAPVendorInformation.GET_VENDOR_CASH_REQUIRE_CODE);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns.Add("v_srchDateType");
          ds.Tables[0].Columns.Add("v_srchAgeDate", typeof(DateTime));
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            if (DateType.ToUpper() == "D")
              dr["v_srchDateType"] = "Due Date";
            else if (DateType.ToUpper() == "I")
              dr["v_srchDateType"] = "Inv Date";

            if (Date != null)
              if (Date != string.Empty)
                dr["v_srchAgeDate"] = DateTime.ParseExact(Date, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


          }


          ds.Tables[0].Columns[0].ColumnName = "p_vend_code";
          ds.Tables[0].Columns[1].ColumnName = "v_doc_date";
          ds.Tables[0].Columns[2].ColumnName = "v_doc_type";
          ds.Tables[0].Columns[3].ColumnName = "v_inv_chk_no";
          ds.Tables[0].Columns[4].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[5].ColumnName = "v_amount";
          ds.Tables[0].Columns[6].ColumnName = "v_bus_name";
          ds.Tables[0].Columns[7].ColumnName = "v_inv_date";
          ds.Tables[0].Columns[8].ColumnName = "v_doc_no";
          ds.Tables[0].Columns[9].ColumnName = "v_due_date";
          //ds.Tables[0].Columns[10].ColumnName = "v_crntdays";
          //ds.Tables[0].Columns[11].ColumnName = "v_1to30days";
          //ds.Tables[0].Columns[12].ColumnName = "v_31to60days";
          //ds.Tables[0].Columns[13].ColumnName = "v_over60days";

          dv = ds.Tables[0].DefaultView;
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dv;
    }
    public static DataSet GetVendorInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsVenderInfo = objDalBaseClass.GetAllData(typeof(DVOAPVendorInformation));
      return dsVenderInfo;


    }
    public static DataSet GetVendorInfo(ref DvoVendorInfo objvendorInfo)
    {
      object[] vendorParameter = new object[3];
      vendorParameter[0] = objvendorInfo.vend_code;
      vendorParameter[1] = objvendorInfo.bus_name;
      vendorParameter[2] = objvendorInfo.zip;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsvendorInfo = objDalBaseClass.GetData(ref vendorParameter, typeof(DvoVendorInfo));
      return dsvendorInfo;

    }
    public static DataSet GetExpnRecPreviousYearComp(ref DVOExpnRevPreviousYearComp objExpRec)
    {
      Object[] parameters = new object[1];
      parameters[0] = objExpRec.CurrentMonth;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref parameters, typeof(DVOExpnRevPreviousYearComp));
      return dsExpExp;
    }
    public static DataSet GetVendorTerms(ref DvoVendorTerms objdvoverterms)
    {
      Object[] TermsParameter = new object[1];
      TermsParameter[0] = objdvoverterms.terms_desc;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DvoVendorTerms));
      return dsExpExp;
    }
    public static DataSet GetPayableListing(ref DVOPayableListingStpinvce objPostAP)
    {
      Object[] TermsParameter = new object[1];
      TermsParameter[0] = objPostAP.batch_id;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DVOPayableListingStpinvce), objPostAP.GET_PAY_LISTING);
      return dsExpExp;
    }
    public static DataSet GetTaxTable(ref DVODeductionTaxTablesRpt objPRUpdateTaxTables)
    {
      Object[] TermsParameter = new object[2];
      TermsParameter[0] = objPRUpdateTaxTables.tax_year;
      TermsParameter[1] = objPRUpdateTaxTables.ded_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DVODeductionTaxTablesRpt));//, objPRUpdateTaxTables.GET_TAX_TABLE_CODE
      return dsExpExp;
    }
    public static DataSet GetCheckPrinting(ref DVOAPCheckProcessingStpcashe objCheckProcessing)
    {
      Object[] TermsParameter = new object[3];
      TermsParameter[0] = objCheckProcessing.batch_id;
      TermsParameter[1] = objCheckProcessing.cash_acct;
      TermsParameter[2] = objCheckProcessing.ap_type.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DVOAPCheckProcessingStpcashe), objCheckProcessing.GET_CHECK_PRINTING);
      return dsExpExp;
    }
    public static DataSet GetEmpPayDetail(ref DVOPayEmpPayDetail objEmpPayDetail)
    {
      Object[] TermsParameter = new object[2];
      //TermsParameter[0] = objEmpPayDetail.month;
      TermsParameter[0] = objEmpPayDetail.startdate;
      TermsParameter[1] = objEmpPayDetail.enddate;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DVOPayEmpPayDetail), objEmpPayDetail.GET_PAY_DETAIL);
      return dsExpExp;
    }
    public static object UpdateCheckPrintingStatus(ref DVOAPCheckProcessingStpcashe objCheckProcessing)
    {
      Object[] TermsParameter = new object[1];
      TermsParameter[0] = objCheckProcessing.doc_no;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object status = objDalBaseClass.ExecuteScalar(ref TermsParameter, objCheckProcessing.UPD_CHECK_PRINT_STS);
      return status;
    }
    public static DataSet GetCashDisJournal(ref DVOAccountsPayableJournal objCashJournal)
    {
      Object[] TermsParameter = new object[2];
      TermsParameter[0] = objCashJournal.startDate;
      TermsParameter[1] = objCashJournal.EndDate;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsExpExp = objDalBaseClass.GetData(ref TermsParameter, typeof(DVOAccountsPayableJournal), objCashJournal.GET_CASH_DIS_JOURNAL);
      return dsExpExp;
    }
    public static DataSet GetMonthlyBudgetAllocations(ref DVOMonthlyBudgetAlloca objMBA)
    {
      object[] parameters = new object[3];
      parameters[0] = objMBA.AccountType;
      parameters[1] = objMBA.Year;
      parameters[2] = objMBA.Keyvalue;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsMBA = objDalBaseClass.GetData(ref parameters, typeof(DVOMonthlyBudgetAlloca));
      return dsMBA;

    }
    public static DataSet GetPayableDefaultInfo()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetAllData(typeof(DvoUpdatePayableDefDetails));
      return dsSegvd;
    }
    public static DataSet GetCheckListingInfo(int BatchID, string CHECK_POST)
    {
      object[] parameter = new object[2];

      parameter[0] = BatchID;
      parameter[1] = CHECK_POST.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetData(ref parameter, typeof(DvoCheckListing), (new DvoCheckListing()).FIND_CHECKLISTINFO);
      return dsSegvd;

    }
    public static DataSet GetCheckListingInfoByBatch(int BatchID, string CHECK_POST)
    {
      object[] parameter = new object[2];

      parameter[0] = BatchID;
      parameter[1] = CHECK_POST.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetData(ref parameter, typeof(DvoCheckListing), (new DvoCheckListing()).FIND_CHECKLISTINFO_BY_BATCH);
      return dsSegvd;

    }
    public static DataSet GetCheckListingdetailsInfo(int BatchID, string CHECK_POST)
    {
      object[] parameter = new object[2];

      parameter[0] = BatchID;
      parameter[1] = CHECK_POST.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetData(ref parameter, typeof(DvoCheckListing), (new DvoCheckListing()).FIND_CHECKLISTDTLINFO);
      return dsSegvd;

    }
    public static DataTable GetGLActivityDetail(ref DVOGLActivityDetail objDVOGLActivityDetail)
    {
      object[] parameters = new object[9];
      parameters[0] = objDVOGLActivityDetail.AccountType.Trim();
      parameters[1] = objDVOGLActivityDetail.Keyvalue.Trim();
      parameters[2] = objDVOGLActivityDetail.StartingMonth;
      parameters[3] = objDVOGLActivityDetail.StartingYear;
      parameters[4] = objDVOGLActivityDetail.EndingMonth;
      parameters[5] = objDVOGLActivityDetail.EndingYear;
      parameters[6] = objDVOGLActivityDetail.DocDateFrom;
      parameters[7] = objDVOGLActivityDetail.DocDateTo;
      parameters[8] = objDVOGLActivityDetail.orig_journal;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLActivityDetail));

      return ds.Tables[0];
    }

    public static DataTable GetGLActByDoc(ref DVOGLActivityDetail objDVOGLActivityDetail)
    {
      try
      {
        object[] parameters = new object[6];
        parameters[0] = objDVOGLActivityDetail.AccountType.Trim();
        parameters[1] = objDVOGLActivityDetail.Keyvalue.Trim().Replace("*", "");
        parameters[2] = objDVOGLActivityDetail.StartingMonth;
        parameters[3] = objDVOGLActivityDetail.StartingYear;
        parameters[4] = objDVOGLActivityDetail.EndingMonth;
        parameters[5] = objDVOGLActivityDetail.EndingYear;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLActivityDetail), objDVOGLActivityDetail.GETACTDOC);

        ds.Tables[0].Columns[0].ColumnName = "keyvalue";
        ds.Tables[0].Columns[1].ColumnName = "acct_desc";
        ds.Tables[0].Columns[2].ColumnName = "amount";
        ds.Tables[0].Columns[3].ColumnName = "debit_credit";
        ds.Tables[0].Columns[4].ColumnName = "orig_journal";
        ds.Tables[0].Columns[5].ColumnName = "doc_no";
        ds.Tables[0].Columns[6].ColumnName = "doc_date";
        ds.Tables[0].Columns[7].ColumnName = "inv_chk_no";
        ds.Tables[0].Columns[8].ColumnName = "ref_code";
        ds.Tables[0].Columns[9].ColumnName = "doc_desc";
        ds.Tables[0].Columns[10].ColumnName = "acct_period";
        ds.Tables[0].Columns[11].ColumnName = "acct_year";
        ds.Tables[0].Columns[12].ColumnName = "v_acct_type";

        return ds.Tables[0];
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }

    public static DataTable GetExpenditures(ref DVOGLActivityDetail objDVOGLActivityDetail)
    {
      object[] parameters = new object[5];
      parameters[0] = objDVOGLActivityDetail.AccountType.Trim();
      parameters[1] = objDVOGLActivityDetail.Keyvalue.Trim();
      parameters[2] = objDVOGLActivityDetail.StartingMonth;
      parameters[3] = objDVOGLActivityDetail.StartingYear;
      parameters[4] = objDVOGLActivityDetail.DocDateTo;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLActivityDetail), objDVOGLActivityDetail.GET_EXP);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "amount";
      ds.Tables[0].Columns[3].ColumnName = "debit_credit";
      ds.Tables[0].Columns[4].ColumnName = "orig_journal";
      ds.Tables[0].Columns[5].ColumnName = "doc_no";
      ds.Tables[0].Columns[6].ColumnName = "doc_date";
      ds.Tables[0].Columns[7].ColumnName = "inv_chk_no";
      ds.Tables[0].Columns[8].ColumnName = "ref_code";
      ds.Tables[0].Columns[9].ColumnName = "doc_desc";
      ds.Tables[0].Columns[10].ColumnName = "acct_period";
      ds.Tables[0].Columns[11].ColumnName = "acct_year";
      ds.Tables[0].Columns[12].ColumnName = "v_acct_type";
      ds.Tables[0].Columns[13].ColumnName = "incr_with_crdt";

      return ds.Tables[0];
    }
    public static DataSet GetGLActivitySummary(ref DVOGLActivitySummary objDVOGLActivitySummary)
    {
      object[] parameters = new object[7];
      parameters[0] = objDVOGLActivitySummary.AccountType;
      parameters[1] = objDVOGLActivitySummary.Keyvalue.Trim();
      parameters[2] = objDVOGLActivitySummary.StartingMonth;
      parameters[3] = objDVOGLActivitySummary.StartingYear;
      parameters[4] = objDVOGLActivitySummary.EndingMonth;
      parameters[5] = objDVOGLActivitySummary.EndingYear;
      parameters[6] = objDVOGLActivitySummary.orig_journal;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsDVOGLActivityDetail = objDalBaseClass.GetData(ref parameters, typeof(DVOGLActivitySummary));
      return dsDVOGLActivityDetail;
    }
    public static DataSet GetPaymentsDue(ref DVOPaymentDue objDVOPaymentDue)
    {
      object[] Parameters = new object[4];
      Parameters[0] = objDVOPaymentDue.vend_code;
      Parameters[1] = objDVOPaymentDue.bus_name;
      Parameters[2] = objDVOPaymentDue.TopaydateFrom;
      Parameters[3] = objDVOPaymentDue.TopaydateTo;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dspaymentsdue = objDalBaseClass.GetData(ref Parameters, typeof(DVOPaymentDue));
      return dspaymentsdue;
    }
    public static DataTable GetCapExpMonthlyReport(ref DVOCapExpMntRpt objSearch)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataTable objDataTable = null;
      try
      {
        Object[] parameters = new object[3];
        parameters[0] = objSearch.Month;
        parameters[1] = objSearch.Year;
        parameters[2] = objSearch.Ministry;

        IDataReader DRD = objDalBaseClass.GetDataByReader(ref parameters, typeof(DVOCapExpMntRpt));
        objDataTable = new DataTable();
        objDataTable.Columns.Add("inbacct_no", typeof(Int32));
        objDataTable.Columns.Add("inbkeyvalue");
        objDataTable.Columns.Add("inbacct_desc");
        objDataTable.Columns.Add("approved", typeof(decimal));
        objDataTable.Columns.Add("revised", typeof(decimal));
        objDataTable.Columns.Add("acct_no", typeof(Int32));
        objDataTable.Columns.Add("keyvalue");
        objDataTable.Columns.Add("acct_desc");
        objDataTable.Columns.Add("activity", typeof(decimal));
        objDataTable.Columns.Add("this_month", typeof(decimal));
        objDataTable.Columns.Add("balance", typeof(decimal));
        objDataTable.Columns.Add("Level1");
        objDataTable.Columns.Add("Level2");
        objDataTable.Columns.Add("Level3");
        objDataTable.Columns.Add("Level4");
        objDataTable.Columns.Add("Level5");
        objDataTable.Columns.Add("Level6");
        objDataTable.Columns.Add("Level7");
        objDataTable.Columns.Add("Level8");
        objDataTable.Columns.Add("LevelDesc1");
        objDataTable.Columns.Add("LevelDesc2");
        objDataTable.Columns.Add("LevelDesc3");
        objDataTable.Columns.Add("LevelDesc4");
        objDataTable.Columns.Add("LevelDesc5");
        objDataTable.Columns.Add("LevelDesc6");
        objDataTable.Columns.Add("LevelDesc7");
        objDataTable.Columns.Add("LevelDesc8");

        DataSet DSMaster_Segment = null;
        List<DVOFlexSegCommon> objListSegDesc = null;
        DataTable dtlSegments = null;
        DataSet dsTd = objDalBaseClass.GetData(objSearch.FIND_STXCHRTD(ref parameters));
        while (DRD.Read())
        {
          DataRow dr = objDataTable.NewRow();
          dr["inbacct_no"] = DRD["acct_no"];
          dr["inbkeyvalue"] = DRD["keyvalue"];
          dr["inbacct_desc"] = DRD["acct_desc"];
          dr["approved"] = DRD["approved"];
          dr["revised"] = DRD["revised"];
          string bkeyvalue = Convert.ToString(dr["inbkeyvalue"]).Trim();
          dtlSegments = BLLCommonUtilities.MakeSegments("CAPEXP", "CAPEXP", bkeyvalue, ref objListSegDesc, ref DSMaster_Segment);
          if (dtlSegments.Rows.Count > 0)
          {
            DataRow drSegments = dtlSegments.Rows[0];
            dr["Level1"] = drSegments["Level1"];
            dr["Level2"] = drSegments["Level2"];
            dr["Level3"] = drSegments["Level3"];
            dr["Level4"] = drSegments["Level4"];
            //dr["Level5"] = drSegments["Level5"];
            //dr["Level6"] = drSegments["Level6"];
            //dr["Level7"] = drSegments["Level7"];
            //dr["Level8"] = drSegments["Level8"];
            dr["LevelDesc1"] = drSegments["LevelDesc1"];
            dr["LevelDesc2"] = drSegments["LevelDesc2"];
            dr["LevelDesc3"] = drSegments["LevelDesc3"];
            dr["LevelDesc4"] = drSegments["LevelDesc4"];
            //dr["LevelDesc5"] = drSegments["LevelDesc5"];
            //dr["LevelDesc6"] = drSegments["LevelDesc6"];
            //dr["LevelDesc7"] = drSegments["LevelDesc7"];
            //dr["LevelDesc8"] = drSegments["LevelDesc8"];
          }
          objDataTable.Rows.Add(dr);
          bool IsFirst = true;
          //string key = DRD["keyvalue"] != DBNull.Value ? Convert.ToString(DRD["keyvalue"]).Trim().Replace("#", "?") : string.Empty;

          foreach (DataRow drTd in dsTd.Tables[0].Rows)
          {

            string _kv = drTd["keyvalue"].ToString().Trim();
            for (int i = 0; i < bkeyvalue.Length; i++)
              if (bkeyvalue[i] == '#')
              {
                _kv = _kv.Remove(i, 1);
                _kv = _kv.Insert(i, "#");
              }
            if (bkeyvalue == _kv)
            {
              string keyvalue = drTd["keyvalue"].ToString().Trim();
              string objCode = string.Empty;
              //string dtlObjCode = string.Empty;
              string objCodeDesc = string.Empty;
              //string dtlObjCodeDesc = string.Empty;
              if (keyvalue.Length >= 15)
                objCode = keyvalue.Substring(13, 2);
              //if (keyvalue.Length >= 18)
              // dtlObjCode = keyvalue.Substring(15, 2);
              if (objCode.Length > 0)
                if (!objCode.Contains("#"))
                {
                  DataRow[] dra = DSMaster_Segment.Tables[0].Select("segmentid = " + 32 + " AND keyvalue= '" + objCode + "'");
                  if (dra.Length > 0)
                  {
                    dr["Level5"] = objCode;
                    dr["LevelDesc5"] = Convert.ToString(dra[0]["desc"]).Trim();
                  }
                  //if(dtlObjCode.Length>0)
                  //    if (!dtlObjCode.Contains("#"))
                  //    {
                  //        dra = DSMaster_Segment.Tables[0].Select("segmentid = " + 37 + " AND keyvalue= '" + dtlObjCode + "'");
                  //        if (dra.Length > 0)
                  //        {
                  //            dr["Level6"] = dtlObjCode;
                  //            dr["LevelDesc6"] = Convert.ToString(dra[0]["desc"]).Trim();
                  //        }
                  //    }
                }
              if (IsFirst)
              {

                dr["activity"] = drTd["activity"];
                dr["this_month"] = drTd["this_month"];
                dr["balance"] = drTd["balance"];
                IsFirst = false;
              }
              else
              {
                DataRow drn = objDataTable.NewRow();
                drn.ItemArray = dr.ItemArray;
                drn["approved"] = 0;
                drn["revised"] = 0;
                drn["activity"] = drTd["activity"];
                drn["this_month"] = drTd["this_month"];
                drn["balance"] = drTd["balance"];

                objDataTable.Rows.Add(drn.ItemArray);
              }
            }
          }

        }
        DSMaster_Segment = null;
        objListSegDesc = null;
        dsTd = null;
        dtlSegments = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return objDataTable;

    }

    public static DataSet GetAccountPayableJournalInfo(DVOAccountsPayableJournal objAccountPayJournal)
    {
      Object[] AccPayParameter = new object[2];
      AccPayParameter[0] = objAccountPayJournal.startDate;
      AccPayParameter[1] = objAccountPayJournal.EndDate;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref AccPayParameter, typeof(DVOAccountsPayableJournal));
      return ds;

    }
    public static DataSet GetCapExpMaster_Segment()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsMaster_Segment = objDalBaseClass.GetAllData(typeof(DVOCapExpByMinistryFormW2));
      dsMaster_Segment.Tables[0].Columns[0].ColumnName = "ministry";
      dsMaster_Segment.Tables[0].Columns[1].ColumnName = "mindesc";
      dsMaster_Segment.Tables[0].Columns[2].ColumnName = "keyvalue";
      dsMaster_Segment.Tables[0].Columns[3].ColumnName = "keydesc";
      return dsMaster_Segment;
    }
    public static DataSet GetOutstandingCheckInfo(ref DVOOutstandingChecks DVOOutstandingChecks)
    {
      object[] CheckParameter = new object[2];
      CheckParameter[0] = DVOOutstandingChecks.acct_no;
      CheckParameter[1] = DVOOutstandingChecks.department;
      //CheckParameter[1] = DVOOutstandingChecks.AccountType;
      //  CheckParameter[1] = DVOOutstandingChecks.Keyvalue;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsOutstandingChecks = objDalBaseClass.GetData(ref CheckParameter, typeof(DVOOutstandingChecks));
      return dsOutstandingChecks;
    }
    public static DataSet GetCapRevbyDetailedobjMaster_Segment()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsMaster_Segment = objDalBaseClass.GetAllData(typeof(DVOCapRevByDetailedObjcet));
      dsMaster_Segment.Tables[0].Columns[0].ColumnName = "ministry";
      dsMaster_Segment.Tables[0].Columns[1].ColumnName = "mindesc";
      dsMaster_Segment.Tables[0].Columns[2].ColumnName = "keyvalue";
      dsMaster_Segment.Tables[0].Columns[3].ColumnName = "keydesc";
      return dsMaster_Segment;
    }
    public static DataSet GetRecExpbyProgramMaster_Segment()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsMaster_Segment = objDalBaseClass.GetAllData(typeof(DVORecExpByProgram_FromW2A_));
      dsMaster_Segment.Tables[0].Columns[0].ColumnName = "ministry";
      dsMaster_Segment.Tables[0].Columns[1].ColumnName = "mindesc";
      dsMaster_Segment.Tables[0].Columns[2].ColumnName = "keyvalue";
      dsMaster_Segment.Tables[0].Columns[3].ColumnName = "keydesc";
      return dsMaster_Segment;
    }
    public static DataSet GetCapRevbyMinistrynObjectMaster_Segment()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsMaster_Segment = objDalBaseClass.GetAllData(typeof(DVOGLAccountBalance));
      dsMaster_Segment.Tables[0].Columns[0].ColumnName = "ministry";
      dsMaster_Segment.Tables[0].Columns[1].ColumnName = "mindesc";
      dsMaster_Segment.Tables[0].Columns[2].ColumnName = "keyvalue";
      dsMaster_Segment.Tables[0].Columns[3].ColumnName = "keydesc";
      return dsMaster_Segment;
    }
    //public static DataSet GetCapRevbyMinistryandObject(ref DVOCapRevByObj objCapRev)
    //{
    //    Object[] parameters = new object[3];
    //    parameters[0] = objCapRev._month;
    //    parameters[1] = objCapRev._Year;
    //    parameters[2] = objCapRev.Ministry;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsCapRev = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByObj));
    //    return dsCapRev;
    //}
    public static List<DVOOutstandingChecks> GetAllCashAccountInfo()
    {
      List<DVOOutstandingChecks> objOChks = new List<DVOOutstandingChecks>();
      //List<DVOFlxview> objFlxseglst = new List<DVOFlxview>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOOutstandingChecks)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOOutstandingChecks Obj = new DVOOutstandingChecks();

          if (!Convert.IsDBNull(dr[0])) Obj.acct_no = Convert.ToInt32(dr[0]);
          if (!Convert.IsDBNull(dr[3])) Obj.acct_desc = dr[3].ToString().Trim();
          if (!Convert.IsDBNull(dr[7])) Obj.keyvalu = dr[7].ToString().Trim();
          objOChks.Add(Obj);
        }
        return objOChks;
      }
    }
    public static DataSet Getstxchrtdvotebook(ref DVOPrintVoteBook objDVOPrintVoteBook1)
    {
      object[] parameters = new object[7];
      parameters[0] = objDVOPrintVoteBook1.ReportingYear;
      parameters[1] = objDVOPrintVoteBook1.StartingMonth;
      parameters[2] = objDVOPrintVoteBook1.EndingMonth;
      parameters[3] = objDVOPrintVoteBook1.PreviousPeriod;
      parameters[4] = objDVOPrintVoteBook1.AccountType.Trim();
      parameters[5] = objDVOPrintVoteBook1.Keyvalue.Trim();
      parameters[6] = objDVOPrintVoteBook1.PreviousYear.Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet dsstxcrtd = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintVoteBook), objDVOPrintVoteBook1.FIND_SPNAME);
      dsstxcrtd.Tables[0].Columns[0].ColumnName = "keyvalue";
      dsstxcrtd.Tables[0].Columns[1].ColumnName = "acct_desc";
      dsstxcrtd.Tables[0].Columns[2].ColumnName = "openinigbalance";
      dsstxcrtd.Tables[0].Columns[3].ColumnName = "this_month";
      dsstxcrtd.Tables[0].Columns[4].ColumnName = "activity";
      dsstxcrtd.Tables[0].Columns[5].ColumnName = "acct_type";



      return dsstxcrtd;

    }
    public static DataTable GetBudAllocVb(ref DVOPrintVoteBook objDVOPrintVoteBook1)
    {
      DataSet ds = new DataSet();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = objDVOPrintVoteBook1.Keyvalue.Trim();
        parameters[1] = objDVOPrintVoteBook1.AccountType.Trim();
        parameters[2] = objDVOPrintVoteBook1.ReportingYear.Trim();
        parameters[3] = objDVOPrintVoteBook1.Set.Trim();
        parameters[4] = DVOApplicationUserInfo.LoginId.Trim();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintVoteBook), objDVOPrintVoteBook1.GET_Bud_AllocVb);
        ds.Tables[0].Columns[0].ColumnName = "v_keyvalue";
        ds.Tables[0].Columns[1].ColumnName = "v_acct_desc";
        ds.Tables[0].Columns[2].ColumnName = "v_acct_type";
        ds.Tables[0].Columns[3].ColumnName = "v_amount";

      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds.Tables[0];
    }
    public static DataTable GetPayrollGLAccountsvotebook(ref DVOPrintVoteBook objDVOPrintVoteBook)
    {
      object[] parameters = new object[4];
      parameters[0] = objDVOPrintVoteBook.ReportingYear;
      parameters[1] = objDVOPrintVoteBook.AccountType.Trim();
      parameters[2] = objDVOPrintVoteBook.Keyvalue.Trim();
      parameters[3] = DVOApplicationUserInfo.LoginId.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintVoteBook), objDVOPrintVoteBook.GET_votebkookstxtr);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "diff_in_alloc";
      ds.Tables[0].Columns[2].ColumnName = "desc";
      ds.Tables[0].Columns[3].ColumnName = "effectivedate";
      ds.Tables[0].Columns[4].ColumnName = "warrant_num";
      ds.Tables[0].Columns[5].ColumnName = "acct_desc";
      ds.Tables[0].Columns[6].ColumnName = "acct_type";
      return ds.Tables[0];

    }
    public static DataTable Getaccmask()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameter = new object[0];

      DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintVoteBook1), (new DVOPrintVoteBook1()).ALL_SPNAME);
      ds.Tables[0].Columns[0].ColumnName = "acc_mask";
      ds.Tables[0].Columns[1].ColumnName = "AcctType";
      ds.Tables[0].Columns[2].ColumnName = "LoginId";
      return ds.Tables[0];
    }
    public static DataTable Getaccmask1(string Loginid, string Acct_type)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameter = new object[2];
      parameter[0] = Acct_type.Trim();
      parameter[1] = Loginid.Trim();
      DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintVoteBook1), (new DVOPrintVoteBook1()).FIND_ACCT_MASK);
      ds.Tables[0].Columns[0].ColumnName = "acc_mask";
      ds.Tables[0].Columns[1].ColumnName = "AcctType";
      ds.Tables[0].Columns[2].ColumnName = "LoginId";
      return ds.Tables[0];
    }
    public static DataSet GetBudgetEstimateList(ref DVOBudgetEstimateList objBudgetEstimate)
    {
      object[] BudgetEstParameter = new object[3];
      BudgetEstParameter[0] = objBudgetEstimate.acct_type;
      BudgetEstParameter[1] = objBudgetEstimate.year;
      BudgetEstParameter[2] = objBudgetEstimate.set;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DsBudgetEstimateList = objDalBaseClass.GetData(ref BudgetEstParameter, typeof(DVOBudgetEstimateList));
      return DsBudgetEstimateList;
    }
    public static List<DVOBudgetEstimateList> GetEstimateBudgetInfo()
    {
      List<DVOBudgetEstimateList> objBgtList = new List<DVOBudgetEstimateList>();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOBudgetEstimateList)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOBudgetEstimateList obj = new DVOBudgetEstimateList();

          if (!Convert.IsDBNull(dr[0])) obj.year = Convert.ToString(dr[0]);
          if (!Convert.IsDBNull(dr[1])) obj.set = Convert.ToString(dr[1]);

          objBgtList.Add(obj);

        }
        return objBgtList;
      }
    }
    public static DataSet GetGLAccountBalance(ref DVOGLAccountBalance pobjGLTrialBal)
    {

      object[] parameters = new object[4];
      parameters[0] = pobjGLTrialBal.acct_type;
      parameters[1] = pobjGLTrialBal.period_month;
      parameters[2] = pobjGLTrialBal.period_year;
      parameters[3] = pobjGLTrialBal.keyvalue;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountBalance));

      return ds;
    }
    public static DataSet GetGLAccountBalance(ref DVOGLAccountBalance pobjGLTrialBal, bool _useacctmask)
    {
      DataSet ds = new DataSet();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = pobjGLTrialBal.acct_type;
        parameters[1] = pobjGLTrialBal.keyvalue;
        parameters[2] = pobjGLTrialBal.period_year;
        parameters[3] = pobjGLTrialBal.period_month;
        parameters[4] = DVOApplicationUserInfo.LoginId.Trim();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountBalance), pobjGLTrialBal.GET_ACCOUNT_BAL);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "acct_no";
          ds.Tables[0].Columns[1].ColumnName = "acct_type";
          ds.Tables[0].Columns[2].ColumnName = "acct_desc";
          ds.Tables[0].Columns[3].ColumnName = "acct_cat";
          ds.Tables[0].Columns[4].ColumnName = "processing_seq";
          ds.Tables[0].Columns[5].ColumnName = "incr_with_crdt";
          ds.Tables[0].Columns[6].ColumnName = "subtotal_group";
          ds.Tables[0].Columns[7].ColumnName = "keyvalue";
          ds.Tables[0].Columns[8].ColumnName = "id";
          ds.Tables[0].Columns[9].ColumnName = "desc";
          ds.Tables[0].Columns[10].ColumnName = "keylength";
          ds.Tables[0].Columns[11].ColumnName = "printsafter";
          ds.Tables[0].Columns[12].ColumnName = "department";
          ds.Tables[0].Columns[13].ColumnName = "period_month";
          ds.Tables[0].Columns[14].ColumnName = "period_year";
          ds.Tables[0].Columns[15].ColumnName = "activity";
          ds.Tables[0].Columns[16].ColumnName = "balance";
          ds.Tables[0].Columns[17].ColumnName = "this_month";
          ds.Tables[0].Columns[18].ColumnName = "budget";
        }


      }
      catch (Exception ex)
      {

        throw ex;
      }
      return ds;
    }
    //********* Modified by Sarvjeet Verma **********On 11 December 2008 ********
    public static DataSet GetEditBgtInfo(string Check_Post)
    {
      DataSet DSBgtEditListing = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOBudgetEditListing objDVOBudgetEditListing = new DVOBudgetEditListing();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = Check_Post;
        DSBgtEditListing = objDalBaseClass.GetData(ref parameters, typeof(DVOBudgetEditListing), objDVOBudgetEditListing.GET_BUDGET_EDITLISTING_INFO);
        if (DSBgtEditListing.Tables.Count > 0)
        {
          DSBgtEditListing.Tables[0].Columns[0].ColumnName = "p_budapprov";
          DSBgtEditListing.Tables[0].Columns[1].ColumnName = "p_dollarsorpercnt";
          DSBgtEditListing.Tables[0].Columns[2].ColumnName = "p_mustbalance";
          DSBgtEditListing.Tables[0].Columns[3].ColumnName = "p_startendreqd";
          DSBgtEditListing.Tables[0].Columns[4].ColumnName = "p_typ";
          DSBgtEditListing.Tables[0].Columns[5].ColumnName = "p_desc";
          DSBgtEditListing.Tables[0].Columns[6].ColumnName = "p_doc_no";
          DSBgtEditListing.Tables[0].Columns[7].ColumnName = "p_account";
          DSBgtEditListing.Tables[0].Columns[8].ColumnName = "p_acct_type";
          DSBgtEditListing.Tables[0].Columns[9].ColumnName = "p_amount";
          DSBgtEditListing.Tables[0].Columns[10].ColumnName = "p_key_desc";
          DSBgtEditListing.Tables[0].Columns[11].ColumnName = "p_keyvalue";
          DSBgtEditListing.Tables[0].Columns[12].ColumnName = "p_allocfullamt";
          DSBgtEditListing.Tables[0].Columns[13].ColumnName = "p_dateentered";
          DSBgtEditListing.Tables[0].Columns[14].ColumnName = "p_doc_desc";
          DSBgtEditListing.Tables[0].Columns[15].ColumnName = "p_effectivedate";
          DSBgtEditListing.Tables[0].Columns[16].ColumnName = "p_endprodforaloc";
          DSBgtEditListing.Tables[0].Columns[17].ColumnName = "p_enteredby";
          DSBgtEditListing.Tables[0].Columns[18].ColumnName = "p_requestedby";
          DSBgtEditListing.Tables[0].Columns[19].ColumnName = "p_set";
          DSBgtEditListing.Tables[0].Columns[20].ColumnName = "p_strtprod4alloc";
          DSBgtEditListing.Tables[0].Columns[21].ColumnName = "p_type";
          DSBgtEditListing.Tables[0].Columns[22].ColumnName = "p_year";
          DSBgtEditListing.Tables[0].Columns[23].ColumnName = "p_docc_no";
          DSBgtEditListing.Tables[0].Columns[24].ColumnName = "p_rowid";
          DSBgtEditListing.Tables[0].Columns[25].ColumnName = "p_line_no";
          DSBgtEditListing.Tables[0].Columns[26].ColumnName = "p_first_name";
          DSBgtEditListing.Tables[0].Columns[27].ColumnName = "p_middle_name";
          DSBgtEditListing.Tables[0].Columns[28].ColumnName = "p_last_name";
          DSBgtEditListing.Tables[0].Columns[29].ColumnName = "p_first_name1";
          DSBgtEditListing.Tables[0].Columns[30].ColumnName = "p_middle_name1";
          DSBgtEditListing.Tables[0].Columns[31].ColumnName = "p_last_name1";

        }

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return DSBgtEditListing;

    }
    public static DataTable GetBudgetsExpMain(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[5];
      parameters[0] = objBudExp.AccountType;
      parameters[1] = objBudExp.Keyvalue;
      parameters[2] = objBudExp.Year;
      parameters[3] = objBudExp.Set;
      parameters[4] = DVOApplicationUserInfo.LoginId.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_budgetexpm);
      ds.Tables[0].Columns[0].ColumnName = "ingflxkhdesc";
      ds.Tables[0].Columns[1].ColumnName = "acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      ds.Tables[0].Columns[3].ColumnName = "keyvalue";
      ds.Tables[0].Columns[4].ColumnName = "available";
      ds.Tables[0].Columns[5].ColumnName = "allocatedtodate";
      return ds.Tables[0];


    }



    public static DataSet GetBudgetsExptdSub(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[3];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Year.Trim();
      parameters[2] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_budexptd);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;

    }
    public static DataSet GetBudgetsExpunPostedGLStmt(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_unPostedGLStmt);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;

    }
    public static DataSet GetBudgetsExpunPostInv(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_unPostInv);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;
    }
    public static DataSet GetBudgetsExpunPostedChq(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_unPostedChq);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;

    }
    public static DataSet GetBudgetsExpunPostedOrd(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_punPostedOrd);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;

    }
    public static DataSet GetBudgetsExpunPostedUInv(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.AccountType.Trim();
      parameters[1] = objBudExp.Keyvalue.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_unPostedUInv);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      return ds;

    }
    public static DataTable GetRecExpBudnCommittdamt(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[1];
      parameters[0] = objBudExp.Keyvalue;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_rebctdamt);
      ds.Tables[0].Columns[0].ColumnName = "amount";
      ds.Tables[0].Columns[1].ColumnName = "keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "Desc";
      return ds.Tables[0];

    }
    public static DataTable GetRecExpBudnCommitAvlamt(ref DVOPrintBudgetsExpenditures objBudExp)
    {
      object[] parameters = new object[2];
      parameters[0] = objBudExp.Keyvalue;
      parameters[1] = DVOApplicationUserInfo.LoginId.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintBudgetsExpenditures), objBudExp.FIND_rebandcavilamt);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "available";
      ds.Tables[0].Columns[2].ColumnName = "Description";
      return ds.Tables[0];

    }
    //Added By Rahul Jain On 24/11/2008 for getting Login log Report
    public static DataSet GetLoginLog(ref DVOLoginLog objLoginLog)
    {
      Object[] parameters = new object[5];

      parameters[0] = objLoginLog.LoginId;
      parameters[1] = objLoginLog.FirstName;
      parameters[2] = objLoginLog.LastName;
      parameters[3] = objLoginLog.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[4] = objLoginLog.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLoginLog = objDalBaseClass.GetData(ref parameters, typeof(DVOLoginLog));
      return dsLoginLog;
    }
    //*************************************************************
    //Added By Rahul Jain On 27/11/2008 for getting FeedBack Report
    public static DataSet GetFeedBack(ref DVOFeedBack objFeedBack)
    {
      Object[] parameters = new object[5];

      parameters[0] = objFeedBack.LoginID;
      parameters[1] = objFeedBack.P_Module;
      parameters[2] = objFeedBack.P_Form;
      parameters[3] = objFeedBack.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[4] = objFeedBack.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsFeedBack = objDalBaseClass.GetData(ref parameters, typeof(DVOFeedBack));
      return dsFeedBack;
    }
    //*************************************************************
    //**********  Added By Bharat Dhall [27 November, 2008] **********
    /// <summary>
    /// to get dataset for report - 'Print Cash Receipts Listing'
    /// </summary>
    /// <returns></returns>
    public static DataSet GetCashReceiptsListing(int BatchId)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = BatchId;
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOARCashProcessingStrcashe), (new DVOARCashProcessingStrcashe()).CASH_RECEIPT_LISTING);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "v_tb_tendcode";
          ds.Tables[0].Columns[1].ColumnName = "v_disc_acct";
          ds.Tables[0].Columns[2].ColumnName = "v_disc_amt";
          ds.Tables[0].Columns[3].ColumnName = "v_disc_deb_cred";
          ds.Tables[0].Columns[4].ColumnName = "v_disc_department";
          ds.Tables[0].Columns[5].ColumnName = "v_dist_acct";
          ds.Tables[0].Columns[6].ColumnName = "v_dist_amt";
          ds.Tables[0].Columns[7].ColumnName = "v_dist_deb_cred";
          ds.Tables[0].Columns[8].ColumnName = "v_dist_department";
          ds.Tables[0].Columns[9].ColumnName = "v_due_date";
          ds.Tables[0].Columns[10].ColumnName = "v_goods_amt";
          ds.Tables[0].Columns[11].ColumnName = "v_inv_doc_no";
          ds.Tables[0].Columns[12].ColumnName = "v_inv_no";
          ds.Tables[0].Columns[13].ColumnName = "v_mtax_code";
          ds.Tables[0].Columns[14].ColumnName = "v_cash_acct";
          ds.Tables[0].Columns[15].ColumnName = "v_cash_amt";
          ds.Tables[0].Columns[16].ColumnName = "v_cash_deb_cred";
          ds.Tables[0].Columns[17].ColumnName = "v_cash_department";
          ds.Tables[0].Columns[18].ColumnName = "v_check_no";
          ds.Tables[0].Columns[19].ColumnName = "v_cust_code";
          ds.Tables[0].Columns[20].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[21].ColumnName = "v_doc_no";
          ds.Tables[0].Columns[22].ColumnName = "v_oa_acct";
          ds.Tables[0].Columns[23].ColumnName = "v_oa_amt";
          ds.Tables[0].Columns[24].ColumnName = "v_oa_deb_cred";
          ds.Tables[0].Columns[25].ColumnName = "v_oa_department";
          ds.Tables[0].Columns[26].ColumnName = "v_ok_to_post";
          ds.Tables[0].Columns[27].ColumnName = "v_rcpt_date";
          ds.Tables[0].Columns[28].ColumnName = "v_dist_acct_desc";
          ds.Tables[0].Columns[29].ColumnName = "v_cash_acct_desc";
          ds.Tables[0].Columns[30].ColumnName = "v_dist_keyvalue";
          ds.Tables[0].Columns[31].ColumnName = "v_cash_keyvalue";
          //foreach (DataRow dr in ds.Tables[0].Rows)
          //{
          //    if (dr["v_due_date"] != DBNull.Value)
          //        if (dr["v_due_date"].ToString() != string.Empty)
          //            dr["v_due_date"] = Convert.ToDateTime(Date);
          //}
        }
        //DataTable dt = new DataTable();
        //dt.TableName = "GLSummary";
        //dt.Columns.Add("keyvalue");
        //dt.Columns.Add("acct_desc");
        //dt.Columns.Add("amount", typeof(String));
        //dt.Columns.Add("debit_credit");
        //dt.Columns.Add("orig_journal");
        //dt.Columns.Add("doc_no");
        //dt.Columns.Add("doc_date");
        //dt.Columns.Add("inv_chk_no");
        //dt.Columns.Add("ref_code");
        //dt.Columns.Add("doc_desc");
        //dt.Columns.Add("acct_period", typeof(Int32));
        //dt.Columns.Add("acct_year", typeof(Int32));
        //dt.Columns.Add("SearchCriteria");

        DataTable dt2 = new DataTable();
        dt2.TableName = "GLSummary";
        dt2.Columns.Add("keyvalue");
        dt2.Columns.Add("acct_desc");
        dt2.Columns.Add("amount", typeof(decimal));
        dt2.Columns.Add("debit_credit");
        dt2.Columns.Add("orig_journal");
        dt2.Columns.Add("doc_no", typeof(Int32));
        dt2.Columns.Add("doc_date");
        dt2.Columns.Add("inv_chk_no");
        dt2.Columns.Add("ref_code");
        dt2.Columns.Add("doc_desc");
        dt2.Columns.Add("acct_period");
        dt2.Columns.Add("acct_year");
        dt2.Columns.Add("SearchCriteria");

        int nDocNo = 0;
        int cDocNo = 0;
        if (ds.Tables.Count > 0)
        {
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          //foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DataRow dr = ds.Tables[0].Rows[i];
            nDocNo = (dr["v_doc_no"] != DBNull.Value) ? Convert.ToInt32(dr["v_doc_no"]) : 0;

            DataRow dr2 = dt2.NewRow();
            if (nDocNo != cDocNo)
            {
              dr2["keyvalue"] = dr["v_cash_keyvalue"];
              dr2["acct_desc"] = dr["v_cash_acct_desc"];
              dr2["amount"] = dr["v_cash_amt"];
              dr2["debit_credit"] = dr["v_cash_deb_cred"];
              cDocNo = nDocNo;
              i--;
            }
            else if (nDocNo == cDocNo)
            {
              dr2["keyvalue"] = dr["v_dist_keyvalue"];
              dr2["acct_desc"] = dr["v_dist_acct_desc"];
              dr2["amount"] = dr["v_dist_amt"];
              dr2["debit_credit"] = dr["v_dist_deb_cred"];
            }
            dr2["orig_journal"] = string.Empty;
            dr2["doc_no"] = nDocNo;
            dr2["doc_date"] = dr["v_rcpt_date"];
            dr2["inv_chk_no"] = string.Empty;
            dr2["ref_code"] = string.Empty;
            dr2["doc_desc"] = string.Empty;
            if (dr["v_rcpt_date"].ToString() != string.Empty)
            {
              DateTime dtt = Convert.ToDateTime(dr["v_rcpt_date"]);
              dr2["acct_period"] = dtt.Month.ToString();
              dr2["acct_year"] = dtt.Year.ToString();
            }
            dr2["SearchCriteria"] = string.Empty;
            dt2.Rows.Add(dr2);



            //DataRow dr2 = dt2.NewRow();
            //dr2["keyvalue"] = dr["v_dist_keyvalue"];
            //dr2["acct_desc"] = dr["v_dist_acct_desc"];
            //dr2["amount"] = dr["v_dist_amt"];
            //dr2["debit_credit"] = dr["v_dist_deb_cred"];
            //dr2["orig_journal"] = string.Empty;
            //dr2["doc_no"] = 0;
            //dr2["doc_date"] = dr["v_rcpt_date"];
            //dr2["inv_chk_no"] = string.Empty;
            //dr2["ref_code"] = string.Empty;
            //dr2["doc_desc"] = string.Empty;
            //if (dr["v_rcpt_date"].ToString() != string.Empty)
            //{
            //    DateTime dtt = Convert.ToDateTime(dr["v_rcpt_date"]);
            //    dr2["acct_period"] = dtt.Month.ToString();
            //    dr2["acct_year"] = dtt.Year.ToString();
            //}
            //dr2["SearchCriteria"] = string.Empty;
            //dt2.Rows.Add(dr2);
          }
        }
        ds.Tables.Add(dt2);

        //using (DataSet ds2 = objDalBaseClass.GetData(ref parameters, typeof(DVOARCashProcessingStrcashe), (new DVOARCashProcessingStrcashe()).CASH_RECEIPT_LISTING_GL))
        //{
        //    if (ds2.Tables.Count > 0)
        //    {
        //        ds2.Tables[0].TableName = "GLSummary";
        //        //dt = ds2.Tables[0].Copy();

        //        ds2.Tables[0].Columns[0].ColumnName = "keyvalue";
        //        ds2.Tables[0].Columns[1].ColumnName = "acct_desc";
        //        ds2.Tables[0].Columns[2].ColumnName = "amount";
        //        ds2.Tables[0].Columns[3].ColumnName = "debit_credit";
        //        ds2.Tables[0].Columns[4].ColumnName = "orig_journal";
        //        ds2.Tables[0].Columns[5].ColumnName = "doc_no";
        //        ds2.Tables[0].Columns[6].ColumnName = "doc_date";
        //        ds2.Tables[0].Columns[7].ColumnName = "inv_chk_no";
        //        ds2.Tables[0].Columns[8].ColumnName = "ref_code";
        //        ds2.Tables[0].Columns[9].ColumnName = "doc_desc";
        //        ds2.Tables[0].Columns[10].ColumnName = "acct_period";
        //        ds2.Tables[0].Columns[11].ColumnName = "acct_year";
        //        ds2.Tables[0].Columns[12].ColumnName = "SearchCriteria";
        //    }

        //    ds.Tables.Add(ds2.Tables[0].Copy());
        //    //ds.Tables.Add(dt);
        //}
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    //***********************************************************************

    //Added By Rahul Jain On 1/12/2008 for getting NSS Details  Report
    public static DataSet GetNSSDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[4];

      parameters[0] = objNSSdetail.account_no;
      parameters[1] = objNSSdetail.nss_status;
      parameters[2] = objNSSdetail.first_name;
      parameters[3] = objNSSdetail.last_name;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail));
      return dsNSSDetail;
    }
    //*************************************************************
    //Added By Rahul Jain On 3/12/2008 for getting NSS Repayments Details  Report
    public static DataSet GetNSSRepaymentsDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[3];

      parameters[0] = objNSSdetail.PaidYear;
      parameters[1] = objNSSdetail.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[2] = objNSSdetail.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), objNSSdetail.FIND_NSS_REPAYEMNTS_BY_YEAR);
      if (dsNSSDetail.Tables.Count > 0)
      {
        dsNSSDetail.Tables[0].TableName = "NSSClients";
        //dt = ds2.Tables[0].Copy();

        dsNSSDetail.Tables[0].Columns[0].ColumnName = "p_account_no";
        dsNSSDetail.Tables[0].Columns[1].ColumnName = "p_contract_no";
        dsNSSDetail.Tables[0].Columns[2].ColumnName = "p_last_name";
        dsNSSDetail.Tables[0].Columns[3].ColumnName = "p_first_name";
        dsNSSDetail.Tables[0].Columns[4].ColumnName = "p_title";
        dsNSSDetail.Tables[0].Columns[5].ColumnName = "p_nss_status";
        dsNSSDetail.Tables[0].Columns[6].ColumnName = "p_interest_paid";
        dsNSSDetail.Tables[0].Columns[7].ColumnName = "p_date_paid";
        dsNSSDetail.Tables[0].Columns[8].ColumnName = "p_monthly_contrib";
        dsNSSDetail.Tables[0].Columns[9].ColumnName = "p_bonus_paid";
        dsNSSDetail.Tables[0].Columns[10].ColumnName = "p_current_bal";

      }



      return dsNSSDetail;
    }
    //*************************************************************
    public static int GetIncome()
    {
      int i;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOPostGLDetail)))
      {
        i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
      }
      return i;

    }
    //Added By Rahul Jain On 4/12/2008 for getting NSS Contribution Details  Report
    public static DataSet GetNSSContributionDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[3];

      parameters[0] = objNSSdetail.PaidYear;
      parameters[1] = objNSSdetail.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[2] = objNSSdetail.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), objNSSdetail.FIND_NSS_CONTRIBUTION_BY_YEAR);
      if (dsNSSDetail.Tables.Count > 0)
      {
        dsNSSDetail.Tables[0].TableName = "NSSClients";
        dsNSSDetail.Tables[0].Columns[0].ColumnName = "p_account_no";
        dsNSSDetail.Tables[0].Columns[1].ColumnName = "p_contract_no";
        dsNSSDetail.Tables[0].Columns[2].ColumnName = "p_last_name";
        dsNSSDetail.Tables[0].Columns[3].ColumnName = "p_first_name";
        dsNSSDetail.Tables[0].Columns[4].ColumnName = "p_title";
        dsNSSDetail.Tables[0].Columns[5].ColumnName = "p_nss_status";
        dsNSSDetail.Tables[0].Columns[6].ColumnName = "p_date_paid";
        dsNSSDetail.Tables[0].Columns[7].ColumnName = "p_payement_date";
        dsNSSDetail.Tables[0].Columns[8].ColumnName = "p_amount";


      }



      return dsNSSDetail;
    }
    //*************************************************************
    //Added By Rahul Jain On 4/12/2008 for getting NSS Monthly Payments Details  Report
    public static DataSet GetNSSMonthlyDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[2];


      parameters[0] = objNSSdetail.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[1] = objNSSdetail.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), objNSSdetail.FIND_NSS_MONTHLY_DETAIL);
      if (dsNSSDetail.Tables.Count > 0)
      {
        dsNSSDetail.Tables[0].TableName = "NSSClients";
        dsNSSDetail.Tables[0].Columns[0].ColumnName = "p_account_no";
        dsNSSDetail.Tables[0].Columns[1].ColumnName = "p_last_name";
        dsNSSDetail.Tables[0].Columns[2].ColumnName = "p_first_name";
        dsNSSDetail.Tables[0].Columns[3].ColumnName = "p_title";
        dsNSSDetail.Tables[0].Columns[4].ColumnName = "p_nss_status";
        dsNSSDetail.Tables[0].Columns[5].ColumnName = "p_notes";
        dsNSSDetail.Tables[0].Columns[6].ColumnName = "p_last_period";
        dsNSSDetail.Tables[0].Columns[7].ColumnName = "p_payment_date";
        dsNSSDetail.Tables[0].Columns[8].ColumnName = "p_for_period";
        dsNSSDetail.Tables[0].Columns[9].ColumnName = "p_voucher_no";
        dsNSSDetail.Tables[0].Columns[10].ColumnName = "p_amount";
        dsNSSDetail.Tables[0].Columns[11].ColumnName = "p_trans_flag";

      }
      return dsNSSDetail;
    }
    //*************************************************************
    //Added By Rahul Jain On 5/12/2008 for getting NSS Transaction Edit List Details  Report
    public static DataSet GetNSSTransEditDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[3];


      parameters[0] = objNSSdetail.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[1] = objNSSdetail.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[2] = objNSSdetail.cash_received;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), objNSSdetail.FIND_NSS_TRANSEDIT_DETAIL);
      if (dsNSSDetail.Tables.Count > 0)
      {
        dsNSSDetail.Tables[0].TableName = "NSSClients";
        dsNSSDetail.Tables[0].Columns[0].ColumnName = "p_account_no";
        dsNSSDetail.Tables[0].Columns[1].ColumnName = "p_last_name";
        dsNSSDetail.Tables[0].Columns[2].ColumnName = "p_first_name";
        dsNSSDetail.Tables[0].Columns[3].ColumnName = "p_title";
        dsNSSDetail.Tables[0].Columns[4].ColumnName = "p_nss_status";
        dsNSSDetail.Tables[0].Columns[5].ColumnName = "p_last_period";
        dsNSSDetail.Tables[0].Columns[6].ColumnName = "p_contract_no";
        dsNSSDetail.Tables[0].Columns[7].ColumnName = "p_current_bal";
        dsNSSDetail.Tables[0].Columns[8].ColumnName = "p_no_of_payments";
        dsNSSDetail.Tables[0].Columns[9].ColumnName = "p_for_period";
        dsNSSDetail.Tables[0].Columns[10].ColumnName = "p_amount";
        dsNSSDetail.Tables[0].Columns[11].ColumnName = "p_trans_flag";
        dsNSSDetail.Tables[0].Columns[12].ColumnName = "p_voucher_no";
        dsNSSDetail.Tables[0].Columns[13].ColumnName = "p_payment_date";
        dsNSSDetail.Tables[0].Columns[14].ColumnName = "p_paid_by_operator";
        dsNSSDetail.Tables[0].Columns[15].ColumnName = "p_date_stopped";
        dsNSSDetail.Tables[0].Columns[16].ColumnName = "p_stop_count";
        dsNSSDetail.Tables[0].Columns[17].ColumnName = "p_seq_no";

      }
      return dsNSSDetail;
    }
    //*************************************************************
    //Added by Sunil Pahwa on 8/12/2008 for getting Employee information by name
    public static DataSet GetEmployeeInformation(ref DVOEmployeeSummary ObjDvoEmpSummary)
    {
      object[] Parameter = new object[10];
      Parameter[0] = ObjDvoEmpSummary.Emp1_code;
      Parameter[1] = ObjDvoEmpSummary.soc_sec_num;
      Parameter[2] = ObjDvoEmpSummary.type_code;
      Parameter[3] = ObjDvoEmpSummary.last_name;
      Parameter[4] = ObjDvoEmpSummary.first_name;
      //Parameter[5] = ObjDvoEmpSummary.type_code;
      Parameter[5] = ObjDvoEmpSummary.job_code;
      Parameter[6] = ObjDvoEmpSummary.job_title;
      Parameter[7] = ObjDvoEmpSummary.empl_status;
      Parameter[8] = ObjDvoEmpSummary.pay_period;
      if (ObjDvoEmpSummary.last_pay == string.Empty)
        ObjDvoEmpSummary.last_pay = null;
      Parameter[9] = ObjDvoEmpSummary.last_pay;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSEmployeeInformation = objDalBaseClass.GetData(ref Parameter, typeof(DVOEmployeeSummary));
      if (DSEmployeeInformation.Tables.Count >= 1)
      {
        DSEmployeeInformation.Tables[0].Columns[0].ColumnName = "p_empl_code";
        DSEmployeeInformation.Tables[0].Columns[1].ColumnName = "p_first_name";
        DSEmployeeInformation.Tables[0].Columns[2].ColumnName = "p_middle_name";
        DSEmployeeInformation.Tables[0].Columns[3].ColumnName = "p_last_name";
        DSEmployeeInformation.Tables[0].Columns[4].ColumnName = "p_address1";
        DSEmployeeInformation.Tables[0].Columns[5].ColumnName = "p_job_title";
        DSEmployeeInformation.Tables[0].Columns[6].ColumnName = "p_date_hired";
        DSEmployeeInformation.Tables[0].Columns[7].ColumnName = "p_soc_sec_num";
        DSEmployeeInformation.Tables[0].Columns[8].ColumnName = "p_birthdate";


      }
      return DSEmployeeInformation;
    }
    //**************************************************************


    public static DataSet GetEmployeeInformationWithoutSocialSecurity(ref DVOEmployeeSummary ObjDvoEmpSummary)
    {
      object[] Parameter = new object[10];
      Parameter[0] = ObjDvoEmpSummary.Emp1_code;
      Parameter[1] = ObjDvoEmpSummary.soc_sec_num;
      Parameter[2] = ObjDvoEmpSummary.type_code;
      Parameter[3] = ObjDvoEmpSummary.last_name;
      Parameter[4] = ObjDvoEmpSummary.first_name;
      //Parameter[5] = ObjDvoEmpSummary.type_code;
      Parameter[5] = ObjDvoEmpSummary.job_code;
      Parameter[6] = ObjDvoEmpSummary.job_title;
      Parameter[7] = ObjDvoEmpSummary.empl_status;
      Parameter[8] = ObjDvoEmpSummary.pay_period;
      if (ObjDvoEmpSummary.last_pay == string.Empty)
        ObjDvoEmpSummary.last_pay = null;
      Parameter[9] = ObjDvoEmpSummary.last_pay;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSEmployeeInformation = objDalBaseClass.GetData(ref Parameter, typeof(DVOEmployeeSummary));
      if (DSEmployeeInformation.Tables.Count >= 1)
      {
        DSEmployeeInformation.Tables[0].Columns[0].ColumnName = "p_empl_code";
        DSEmployeeInformation.Tables[0].Columns[1].ColumnName = "p_first_name";
        DSEmployeeInformation.Tables[0].Columns[2].ColumnName = "p_middle_name";
        DSEmployeeInformation.Tables[0].Columns[3].ColumnName = "p_last_name";
        DSEmployeeInformation.Tables[0].Columns[4].ColumnName = "p_address1";
        DSEmployeeInformation.Tables[0].Columns[5].ColumnName = "p_job_title";
        DSEmployeeInformation.Tables[0].Columns[6].ColumnName = "p_date_hired";
        DSEmployeeInformation.Tables[0].Columns[7].ColumnName = "p_soc_sec_num";
        DSEmployeeInformation.Tables[0].Columns[8].ColumnName = "p_birthdate";


      }
      return DSEmployeeInformation;
    }
    //**************************************************************

    //Added By Rahul Jain On 19/12/2008 for getting Time cards Details  Report
    public static DataSet GetTimeCardsDetails(ref DVOTimeCard objTimeCard)
    {
      Object[] parameters = new object[8];

      parameters[0] = objTimeCard.empl_code;
      parameters[1] = objTimeCard.first_name;
      parameters[2] = objTimeCard.last_name;
      parameters[3] = objTimeCard.start_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[4] = objTimeCard.end_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[5] = objTimeCard.used_flag;
      parameters[6] = objTimeCard.start_range;
      parameters[7] = objTimeCard.end_range;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOTimeCard));
      return ds;
    }
    //*************************************************************

    //Added By Rahul Jain On 21/12/2008 for getting  Payroll Employee type  Details  Report
    public static DataSet GetEmployeeType(ref DVOMasterEmpTypes objEmpType)
    {
      Object[] parameters = new object[1];
      parameters[0] = objEmpType.type_code + '%';
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypes), objEmpType.FIND_EMPTYPE);
      if (ds != null)
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].TableName = "EmpType";
          ds.Tables[0].Columns[0].ColumnName = "p_keyvalue";
          ds.Tables[0].Columns[1].ColumnName = "p_acct_desc";
          ds.Tables[0].Columns[2].ColumnName = "p_cash_acct";
          ds.Tables[0].Columns[3].ColumnName = "p_department";
          ds.Tables[0].Columns[4].ColumnName = "p_description";
          ds.Tables[0].Columns[5].ColumnName = "p_empl_status";
          ds.Tables[0].Columns[6].ColumnName = "p_hold_pymnt";
          ds.Tables[0].Columns[7].ColumnName = "p_loctax_code";
          ds.Tables[0].Columns[8].ColumnName = "p_pay_period";
          ds.Tables[0].Columns[9].ColumnName = "p_sick_accr_code";
          ds.Tables[0].Columns[10].ColumnName = "p_sick_allowed";
          ds.Tables[0].Columns[11].ColumnName = "p_sick_code";
          ds.Tables[0].Columns[12].ColumnName = "p_statax_code";
          ds.Tables[0].Columns[13].ColumnName = "p_type_code";
          ds.Tables[0].Columns[14].ColumnName = "p_vac_accr_code";
          ds.Tables[0].Columns[15].ColumnName = "p_vac_allowed";
          ds.Tables[0].Columns[16].ColumnName = "p_vac_code";
        }
      return ds;
    }
    //*************************************************************

    //***********************************************************************

    //Added By Sunil Pahwa On 21/12/2008 for getting Obligation Codes
    public static DataSet GetObligationCodesInformation(ref DVOMasterOblCodes1 objDvoOblCodes)
    {
      object[] Parameter = new object[1];
      Parameter[0] = objDvoOblCodes.obl_codes;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSObligationCodes = objDalBaseClass.GetData(ref Parameter, typeof(DVOMasterOblCodes1));
      //if (DSObligationCodes.Tables.Count >= 1)
      //{
      //    //DSObligationCodes.Tables[0].Columns[0].ColumnName = "obl_code";
      //    //DSObligationCodes.Tables[0].Columns[1].ColumnName = "description";
      //    //DSObligationCodes.Tables[0].Columns[2].ColumnName = "dflt_rate";
      //    //DSObligationCodes.Tables[0].Columns[3].ColumnName = "dflt_limit";
      //    //DSObligationCodes.Tables[0].Columns[4].ColumnName = "dflt_pay_limit";
      //    //DSObligationCodes.Tables[0].Columns[5].ColumnName = "obl_type";
      //    //DSObligationCodes.Tables[0].Columns[6].ColumnName = "acct_desc";
      //    //DSObligationCodes.Tables[0].Columns[7].ColumnName = "keyvalue";
      //    //DSObligationCodes.Tables[0].Columns[8].ColumnName = "dfltaccounttype";
      //    //DSObligationCodes.Tables[0].Columns[9].ColumnName = "dflt_acct";
      //}
      return DSObligationCodes;
    }
    //******************************************************************************
    //Added By Rahul Jain On 22/12/2008 for getting  Payroll Employee type Income  Details  Report
    public static DataSet GetEmployeeIncomeDetail(ref DVOMasterEmpTypes objEmpType)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[1];
      parameters[0] = objEmpType.type_code.Trim();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypes), objEmpType.FIND_EMPINCOME);
      if (ds != null)
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].TableName = "stytypid";
          ds.Tables[0].Columns[0].ColumnName = "p_keyvalue";
          ds.Tables[0].Columns[1].ColumnName = "p_type_code";
          ds.Tables[0].Columns[2].ColumnName = "p_inc_code";
          ds.Tables[0].Columns[3].ColumnName = "p_line_no";
          ds.Tables[0].Columns[4].ColumnName = "p_inc_rate";
          ds.Tables[0].Columns[5].ColumnName = "p_inc_number";
          ds.Tables[0].Columns[6].ColumnName = "p_inc_hours";
          ds.Tables[0].Columns[7].ColumnName = "p_acct_no";
          ds.Tables[0].Columns[8].ColumnName = "p_department";
          ds.Tables[0].Columns[9].ColumnName = "p_lo_inc_amt";
          ds.Tables[0].Columns[10].ColumnName = "p_hi_inc_amt";
        }
      return ds;
    }
    //*************************************************************
    //Added By Rahul Jain On 22/12/2008 for getting  Payroll Employee type deduction  Details  Report
    public static DataSet GetEmployeeDeductionDetail(ref DVOMasterEmpTypes objEmpType)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(typeof(DVOMasterEmpTypes), objEmpType.FIND_EMPDEDUCTION);
      if (ds != null)
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].TableName = "stytypdd";
          ds.Tables[0].Columns[0].ColumnName = "p_keyvalue";
          ds.Tables[0].Columns[1].ColumnName = "p_type_code";
          ds.Tables[0].Columns[2].ColumnName = "p_ded_code";
          ds.Tables[0].Columns[3].ColumnName = "p_line_no";
          ds.Tables[0].Columns[4].ColumnName = "p_ded_rate";
          ds.Tables[0].Columns[5].ColumnName = "p_ded_limit";
          ds.Tables[0].Columns[6].ColumnName = "p_ded_apply";
          ds.Tables[0].Columns[7].ColumnName = "p_acct_no";
          ds.Tables[0].Columns[8].ColumnName = "p_department";
          ds.Tables[0].Columns[9].ColumnName = "p_lo_ded_amt";
          ds.Tables[0].Columns[10].ColumnName = "p_hi_ded_amt";
          ds.Tables[0].Columns[11].ColumnName = "p_pay_limit";
        }
      return ds;
    }
    //*************************************************************
    //Added By Rahul Jain On 22/12/2008 for getting  Payroll Employee type deduction  Details  Report
    public static DataSet GetEmployeeObligationDetail(ref DVOMasterEmpTypes objEmpType)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(typeof(DVOMasterEmpTypes), objEmpType.FIND_EMPOBLIGATION);
      if (ds != null)
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].TableName = "stytypod";
          ds.Tables[0].Columns[0].ColumnName = "p_keyvalue";
          ds.Tables[0].Columns[1].ColumnName = "p_keyvalue_bal";
          ds.Tables[0].Columns[2].ColumnName = "p_type_code";
          ds.Tables[0].Columns[3].ColumnName = "p_obl_code";
          ds.Tables[0].Columns[4].ColumnName = "p_line_no";
          ds.Tables[0].Columns[5].ColumnName = "p_obl_rate";
          ds.Tables[0].Columns[6].ColumnName = "p_obl_limit";
          ds.Tables[0].Columns[7].ColumnName = "p_acct_no";
          ds.Tables[0].Columns[8].ColumnName = "p_department";
          ds.Tables[0].Columns[9].ColumnName = "p_bal_acct_no";
          ds.Tables[0].Columns[10].ColumnName = "p_bal_dept";
          ds.Tables[0].Columns[11].ColumnName = "p_pay_limit";
        }
      return ds;
    }
    //*************************************************************

    public static DataSet GetIncomeCodesInformation(ref DVOMasterIncCodes objDvoIncCodes)
    {
      object[] Parameter = new object[1];
      Parameter[0] = objDvoIncCodes.inc_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMasterIncCodes));
      return ds;
    }
    //******************************************************************************
    //Added By Rahul Jain On 23/12/2008 for getting Deduction Codes Details
    public static DataSet GetDeductionCodesInformation(ref DVODeductionsGet objDvoDedCodes)
    {
      object[] Parameter = new object[1];
      Parameter[0] = objDvoDedCodes.ded_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVODeductionsGet));
      return ds;
    }
    //******************************************************************************
    //Added By Rahul Jain On 23/12/2008 for getting Accural Codes Details
    public static DataSet GetAccuralCodesInformation(ref DVOUpdAccrualCode objDvoaccCodes)
    {
      object[] Parameter = new object[7];
      Parameter[0] = objDvoaccCodes.accr_code;
      Parameter[1] = objDvoaccCodes.accr_desc;
      Parameter[2] = objDvoaccCodes.accr_method;
      Parameter[3] = objDvoaccCodes.accr_rate;
      Parameter[4] = objDvoaccCodes.accr_freq;
      Parameter[5] = objDvoaccCodes.accr_lapse;
      Parameter[6] = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOUpdAccrualCode));
      return ds;
    }
    //******************************************************************************

    //*************************************************************
    //**********  Added By Bharat Dhall [26 December, 2008] **********

    /// <summary>
    /// to get dataset for report - 'Print Employee Summary/Detail Information'
    /// </summary>
    /// <returns></returns>
    public static DataSet GetEmployeeInformation(ref DVOMasterEmployee pobjDVOEmployeeStyemplr)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[26];
        parameters[0] = pobjDVOEmployeeStyemplr.RowID;
        parameters[1] = pobjDVOEmployeeStyemplr.EmplCode;
        parameters[2] = pobjDVOEmployeeStyemplr.SocSecNum;
        parameters[3] = pobjDVOEmployeeStyemplr.TypeCode;
        parameters[4] = pobjDVOEmployeeStyemplr.Birthdate;
        parameters[5] = pobjDVOEmployeeStyemplr.FirstName;
        parameters[6] = pobjDVOEmployeeStyemplr.MiddleName;
        parameters[7] = pobjDVOEmployeeStyemplr.LastName;
        parameters[8] = pobjDVOEmployeeStyemplr.Address1;
        parameters[9] = pobjDVOEmployeeStyemplr.Address2;
        parameters[10] = pobjDVOEmployeeStyemplr.City;
        parameters[11] = pobjDVOEmployeeStyemplr.State;
        parameters[12] = pobjDVOEmployeeStyemplr.Zip;
        parameters[13] = pobjDVOEmployeeStyemplr.Phone;
        parameters[14] = pobjDVOEmployeeStyemplr.CashAcct;
        parameters[15] = pobjDVOEmployeeStyemplr.Terminated;
        parameters[16] = pobjDVOEmployeeStyemplr.HoldPayment;
        parameters[17] = pobjDVOEmployeeStyemplr.FlexDeptAcctType;
        parameters[18] = pobjDVOEmployeeStyemplr.LastIncDate;
        parameters[19] = pobjDVOEmployeeStyemplr.AppointDate;
        parameters[20] = pobjDVOEmployeeStyemplr.Gender;
        parameters[21] = pobjDVOEmployeeStyemplr.EmplStatus;
        parameters[22] = pobjDVOEmployeeStyemplr.JobCode;
        parameters[23] = pobjDVOEmployeeStyemplr.JobTitle;
        parameters[24] = pobjDVOEmployeeStyemplr.PayPeriod;
        parameters[25] = pobjDVOEmployeeStyemplr.LastPay;

        ds = objDalBaseClass.GetData(ref parameters, pobjDVOEmployeeStyemplr.GetType());
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
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
          ds.Tables[0].Columns[49].ColumnName = "rowid";
          ds.Tables[0].Columns[50].ColumnName = "CashAcctKeyvalue";
          ds.Tables[0].Columns[51].ColumnName = "TypeDesc";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    /// <summary>
    /// to get dataset for report - 'Print Employee Income-Codes Information'
    /// </summary>
    /// <returns></returns>
    /// 

    //public static DataSet GetEmployeeInformationwithoutsocialsecurity(ref DVOMasterEmployee pobjDVOEmployeeStyemplr)
    //{
    //    DataSet ds = new DataSet();
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    try
    //    {
    //        object[] parameters = new object[26];
    //        parameters[0] = pobjDVOEmployeeStyemplr.RowID;
    //        parameters[1] = pobjDVOEmployeeStyemplr.EmplCode;
    //        parameters[2] = pobjDVOEmployeeStyemplr.SocSecNum;
    //        parameters[3] = pobjDVOEmployeeStyemplr.TypeCode;
    //        parameters[4] = pobjDVOEmployeeStyemplr.Birthdate;
    //        parameters[5] = pobjDVOEmployeeStyemplr.FirstName;
    //        parameters[6] = pobjDVOEmployeeStyemplr.MiddleName;
    //        parameters[7] = pobjDVOEmployeeStyemplr.LastName;
    //        parameters[8] = pobjDVOEmployeeStyemplr.Address1;
    //        parameters[9] = pobjDVOEmployeeStyemplr.Address2;
    //        parameters[10] = pobjDVOEmployeeStyemplr.City;
    //        parameters[11] = pobjDVOEmployeeStyemplr.State;
    //        parameters[12] = pobjDVOEmployeeStyemplr.Zip;
    //        parameters[13] = pobjDVOEmployeeStyemplr.Phone;
    //        parameters[14] = pobjDVOEmployeeStyemplr.CashAcct;
    //        parameters[15] = pobjDVOEmployeeStyemplr.Terminated;
    //        parameters[16] = pobjDVOEmployeeStyemplr.HoldPayment;
    //        parameters[17] = pobjDVOEmployeeStyemplr.FlexDeptAcctType;
    //        parameters[18] = pobjDVOEmployeeStyemplr.LastIncDate;
    //        parameters[19] = pobjDVOEmployeeStyemplr.AppointDate;
    //        parameters[20] = pobjDVOEmployeeStyemplr.Gender;
    //        parameters[21] = pobjDVOEmployeeStyemplr.EmplStatus;
    //        parameters[22] = pobjDVOEmployeeStyemplr.JobCode;
    //        parameters[23] = pobjDVOEmployeeStyemplr.JobTitle;
    //        parameters[24] = pobjDVOEmployeeStyemplr.PayPeriod;
    //        parameters[25] = pobjDVOEmployeeStyemplr.LastPay;

    //        ds = objDalBaseClass.GetData(ref parameters, pobjDVOEmployeeStyemplr.GET_Employee_List_Without_SocialSecurity);
    //        if (ds.Tables.Count > 0)
    //        {
    //            ds.Tables[0].Columns[0].ColumnName = "empl_code";
    //            ds.Tables[0].Columns[1].ColumnName = "soc_sec_num";
    //            ds.Tables[0].Columns[2].ColumnName = "type_code";
    //            ds.Tables[0].Columns[3].ColumnName = "birthdate";
    //            ds.Tables[0].Columns[4].ColumnName = "first_name";
    //            ds.Tables[0].Columns[5].ColumnName = "middle_name";
    //            ds.Tables[0].Columns[6].ColumnName = "last_name";
    //            ds.Tables[0].Columns[7].ColumnName = "address1";
    //            ds.Tables[0].Columns[8].ColumnName = "address2";
    //            ds.Tables[0].Columns[9].ColumnName = "city";
    //            ds.Tables[0].Columns[10].ColumnName = "state";
    //            ds.Tables[0].Columns[11].ColumnName = "zip";
    //            ds.Tables[0].Columns[12].ColumnName = "phone";
    //            ds.Tables[0].Columns[13].ColumnName = "cash_acct";
    //            ds.Tables[0].Columns[14].ColumnName = "department";
    //            ds.Tables[0].Columns[15].ColumnName = "job_code";
    //            ds.Tables[0].Columns[16].ColumnName = "job_title";
    //            ds.Tables[0].Columns[17].ColumnName = "date_hired";
    //            ds.Tables[0].Columns[18].ColumnName = "terminated";
    //            ds.Tables[0].Columns[19].ColumnName = "empl_status";
    //            ds.Tables[0].Columns[20].ColumnName = "pay_period";
    //            ds.Tables[0].Columns[21].ColumnName = "allowances";
    //            ds.Tables[0].Columns[22].ColumnName = "state_allow";
    //            ds.Tables[0].Columns[23].ColumnName = "marital_stat";
    //            ds.Tables[0].Columns[24].ColumnName = "vac_code";
    //            ds.Tables[0].Columns[25].ColumnName = "vac_allowed";
    //            ds.Tables[0].Columns[26].ColumnName = "vac_used";
    //            ds.Tables[0].Columns[27].ColumnName = "sick_code";
    //            ds.Tables[0].Columns[28].ColumnName = "sick_allowed";
    //            ds.Tables[0].Columns[29].ColumnName = "sick_used";
    //            ds.Tables[0].Columns[30].ColumnName = "last_pay";
    //            ds.Tables[0].Columns[31].ColumnName = "hold_pymnt";
    //            ds.Tables[0].Columns[32].ColumnName = "statax_code";
    //            ds.Tables[0].Columns[33].ColumnName = "loctax_code";
    //            ds.Tables[0].Columns[34].ColumnName = "sick_accr_code";
    //            ds.Tables[0].Columns[35].ColumnName = "sick_accr_ctr";
    //            ds.Tables[0].Columns[36].ColumnName = "sick_lapse_date";
    //            ds.Tables[0].Columns[37].ColumnName = "vac_accr_code";
    //            ds.Tables[0].Columns[38].ColumnName = "vac_accr_ctr";
    //            ds.Tables[0].Columns[39].ColumnName = "vac_lapse_date";
    //            ds.Tables[0].Columns[40].ColumnName = "dir_dept";
    //            ds.Tables[0].Columns[41].ColumnName = "dfi_dest";
    //            ds.Tables[0].Columns[42].ColumnName = "chk_digit";
    //            ds.Tables[0].Columns[43].ColumnName = "bank_acct_no";
    //            ds.Tables[0].Columns[44].ColumnName = "state_udf";
    //            ds.Tables[0].Columns[45].ColumnName = "flexdeptaccttype";
    //            ds.Tables[0].Columns[46].ColumnName = "last_inc_date";
    //            ds.Tables[0].Columns[47].ColumnName = "appoint_date";
    //            ds.Tables[0].Columns[48].ColumnName = "gender";
    //            ds.Tables[0].Columns[49].ColumnName = "rowid";
    //            ds.Tables[0].Columns[50].ColumnName = "CashAcctKeyvalue";
    //            ds.Tables[0].Columns[51].ColumnName = "TypeDesc";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionManagement.ExceptionManager.Publish(ex);
    //    }
    //    return ds;
    //}
    public static DataSet GetEmployeeIncomeCodes(ref DVOMasterEmployeeIncomes pobjDVOMasterEmployeeIncomes)//, string employeeCodes)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[24];
        parameters[0] = pobjDVOMasterEmployeeIncomes.line_no;
        parameters[1] = pobjDVOMasterEmployeeIncomes.empl_code;
        parameters[2] = pobjDVOMasterEmployeeIncomes.inc_code;
        parameters[3] = pobjDVOMasterEmployeeIncomes.inc_rate;
        parameters[4] = pobjDVOMasterEmployeeIncomes.inc_number;
        parameters[5] = pobjDVOMasterEmployeeIncomes.inc_hours;
        parameters[6] = pobjDVOMasterEmployeeIncomes.acct_no;
        parameters[7] = pobjDVOMasterEmployeeIncomes.department;
        parameters[8] = pobjDVOMasterEmployeeIncomes.inc_qtd1;
        parameters[9] = pobjDVOMasterEmployeeIncomes.inc_qtd2;
        parameters[10] = pobjDVOMasterEmployeeIncomes.inc_qtd3;
        parameters[11] = pobjDVOMasterEmployeeIncomes.inc_qtd4;
        parameters[12] = pobjDVOMasterEmployeeIncomes.inc_ytd;
        parameters[13] = pobjDVOMasterEmployeeIncomes.lo_inc_amt;
        parameters[14] = pobjDVOMasterEmployeeIncomes.hi_inc_amt;
        parameters[15] = pobjDVOMasterEmployeeIncomes.SSN;
        parameters[16] = pobjDVOMasterEmployeeIncomes.typeCode;
        parameters[17] = pobjDVOMasterEmployeeIncomes.lastName;
        parameters[18] = pobjDVOMasterEmployeeIncomes.firstName;
        parameters[19] = pobjDVOMasterEmployeeIncomes.empl_status;
        parameters[20] = pobjDVOMasterEmployeeIncomes.jobCode;
        parameters[21] = pobjDVOMasterEmployeeIncomes.jobTitle;
        parameters[22] = pobjDVOMasterEmployeeIncomes.pay_period;
        parameters[23] = pobjDVOMasterEmployeeIncomes.lastPay;
        //parameters[24] = employeeCodes;

        ds = objDalBaseClass.GetData(ref parameters, pobjDVOMasterEmployeeIncomes.GetType());
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
          ds.Tables[0].Columns[1].ColumnName = "inc_code";
          ds.Tables[0].Columns[2].ColumnName = "line_no";
          ds.Tables[0].Columns[3].ColumnName = "inc_rate";
          ds.Tables[0].Columns[4].ColumnName = "inc_number";
          ds.Tables[0].Columns[5].ColumnName = "inc_hours";
          ds.Tables[0].Columns[6].ColumnName = "acct_no";
          ds.Tables[0].Columns[7].ColumnName = "department";
          ds.Tables[0].Columns[8].ColumnName = "inc_qtd1";
          ds.Tables[0].Columns[9].ColumnName = "inc_qtd2";
          ds.Tables[0].Columns[10].ColumnName = "inc_qtd3";
          ds.Tables[0].Columns[11].ColumnName = "inc_qtd4";
          ds.Tables[0].Columns[12].ColumnName = "inc_ytd";
          ds.Tables[0].Columns[13].ColumnName = "lo_inc_amt";
          ds.Tables[0].Columns[14].ColumnName = "hi_inc_amt";
          ds.Tables[0].Columns[15].ColumnName = "acct_no_kv";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    /// <summary>
    /// to get dataset for report - 'Print Employee Deduction-Codes Information'
    /// </summary>
    /// <returns></returns>
    public static DataSet GetEmployeeDeductionCodes(ref DVOMasterEmployeeDeductions pobjDVOMasterEmployeeDeductions)//, string employeeCodes)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[27];
        parameters[0] = pobjDVOMasterEmployeeDeductions.line_no;
        parameters[1] = pobjDVOMasterEmployeeDeductions.empl_code;
        parameters[2] = pobjDVOMasterEmployeeDeductions.ded_code;
        parameters[3] = pobjDVOMasterEmployeeDeductions.ded_rate;
        parameters[4] = pobjDVOMasterEmployeeDeductions.ded_limit;
        parameters[5] = pobjDVOMasterEmployeeDeductions.ded_apply;
        parameters[6] = pobjDVOMasterEmployeeDeductions.acct_no;
        parameters[7] = pobjDVOMasterEmployeeDeductions.department;
        parameters[8] = pobjDVOMasterEmployeeDeductions.ded_qtd1;
        parameters[9] = pobjDVOMasterEmployeeDeductions.ded_qtd2;
        parameters[10] = pobjDVOMasterEmployeeDeductions.ded_qtd3;
        parameters[11] = pobjDVOMasterEmployeeDeductions.ded_qtd4;
        parameters[12] = pobjDVOMasterEmployeeDeductions.ded_ytd;
        parameters[13] = pobjDVOMasterEmployeeDeductions.ded_date;
        parameters[14] = pobjDVOMasterEmployeeDeductions.lo_ded_amt;
        parameters[15] = pobjDVOMasterEmployeeDeductions.hi_ded_amt;
        parameters[16] = pobjDVOMasterEmployeeDeductions.pay_limit;
        parameters[17] = pobjDVOMasterEmployeeDeductions.balanceamt;

        parameters[18] = pobjDVOMasterEmployeeDeductions.SSN;
        parameters[19] = pobjDVOMasterEmployeeDeductions.typeCode;
        parameters[20] = pobjDVOMasterEmployeeDeductions.lastName;
        parameters[21] = pobjDVOMasterEmployeeDeductions.firstName;
        parameters[22] = pobjDVOMasterEmployeeDeductions.empl_status;
        parameters[23] = pobjDVOMasterEmployeeDeductions.jobCode;
        parameters[24] = pobjDVOMasterEmployeeDeductions.jobTitle;
        parameters[25] = pobjDVOMasterEmployeeDeductions.pay_period;
        parameters[26] = pobjDVOMasterEmployeeDeductions.lastPay;
        //parameters[27] = employeeCodes;

        ds = objDalBaseClass.GetData(ref parameters, pobjDVOMasterEmployeeDeductions.GetType());
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
          ds.Tables[0].Columns[1].ColumnName = "ded_code";
          ds.Tables[0].Columns[2].ColumnName = "line_no";
          ds.Tables[0].Columns[3].ColumnName = "ded_rate";
          ds.Tables[0].Columns[4].ColumnName = "ded_limit";
          ds.Tables[0].Columns[5].ColumnName = "ded_apply";
          ds.Tables[0].Columns[6].ColumnName = "acct_no";
          ds.Tables[0].Columns[7].ColumnName = "department";
          ds.Tables[0].Columns[8].ColumnName = "ded_qtd1";
          ds.Tables[0].Columns[9].ColumnName = "ded_qtd2";
          ds.Tables[0].Columns[10].ColumnName = "ded_qtd3";
          ds.Tables[0].Columns[11].ColumnName = "ded_qtd4";
          ds.Tables[0].Columns[12].ColumnName = "ded_ytd";
          ds.Tables[0].Columns[13].ColumnName = "ded_date";
          ds.Tables[0].Columns[14].ColumnName = "lo_ded_amt";
          ds.Tables[0].Columns[15].ColumnName = "hi_ded_amt";
          ds.Tables[0].Columns[16].ColumnName = "pay_limit";
          ds.Tables[0].Columns[17].ColumnName = "balanceamt";
          ds.Tables[0].Columns[18].ColumnName = "acct_no_kv";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    /// <summary>
    /// to get dataset for report - 'Print Employee Obligation-Codes Information'
    /// </summary>
    /// <returns></returns>
    public static DataSet GetEmployeeObligationCodes(ref DVOMasterEmployeeObligations pobjDVOMasterEmployeeObligations)//, string employeeCodes)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[24];
        parameters[0] = pobjDVOMasterEmployeeObligations.line_no;
        parameters[1] = pobjDVOMasterEmployeeObligations.empl_code;
        parameters[2] = pobjDVOMasterEmployeeObligations.obl_code;
        parameters[3] = pobjDVOMasterEmployeeObligations.obl_rate;
        parameters[4] = pobjDVOMasterEmployeeObligations.obl_limit;
        parameters[5] = pobjDVOMasterEmployeeObligations.acct_no;
        parameters[6] = pobjDVOMasterEmployeeObligations.department;
        parameters[7] = pobjDVOMasterEmployeeObligations.bal_acct_no;
        parameters[8] = pobjDVOMasterEmployeeObligations.bal_dept;
        parameters[9] = pobjDVOMasterEmployeeObligations.obl_qtd1;
        parameters[10] = pobjDVOMasterEmployeeObligations.obl_qtd2;
        parameters[11] = pobjDVOMasterEmployeeObligations.obl_qtd3;
        parameters[12] = pobjDVOMasterEmployeeObligations.obl_qtd4;
        parameters[13] = pobjDVOMasterEmployeeObligations.obl_ytd;
        parameters[14] = pobjDVOMasterEmployeeObligations.pay_limit;

        parameters[15] = pobjDVOMasterEmployeeObligations.SSN;
        parameters[16] = pobjDVOMasterEmployeeObligations.typeCode;
        parameters[17] = pobjDVOMasterEmployeeObligations.lastName;
        parameters[18] = pobjDVOMasterEmployeeObligations.firstName;
        parameters[19] = pobjDVOMasterEmployeeObligations.empl_status;
        parameters[20] = pobjDVOMasterEmployeeObligations.jobCode;
        parameters[21] = pobjDVOMasterEmployeeObligations.jobTitle;
        parameters[22] = pobjDVOMasterEmployeeObligations.pay_period;
        parameters[23] = pobjDVOMasterEmployeeObligations.lastPay;


        ds = objDalBaseClass.GetData(ref parameters, pobjDVOMasterEmployeeObligations.GetType());
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
          ds.Tables[0].Columns[1].ColumnName = "obl_code";
          ds.Tables[0].Columns[2].ColumnName = "line_no";
          ds.Tables[0].Columns[3].ColumnName = "obl_rate";
          ds.Tables[0].Columns[4].ColumnName = "obl_limit";
          ds.Tables[0].Columns[5].ColumnName = "acct_no";
          ds.Tables[0].Columns[6].ColumnName = "department";
          ds.Tables[0].Columns[7].ColumnName = "bal_acct_no";
          ds.Tables[0].Columns[8].ColumnName = "bal_dept";
          ds.Tables[0].Columns[9].ColumnName = "obl_qtd1";
          ds.Tables[0].Columns[10].ColumnName = "obl_qtd2";
          ds.Tables[0].Columns[11].ColumnName = "obl_qtd3";
          ds.Tables[0].Columns[12].ColumnName = "obl_qtd4";
          ds.Tables[0].Columns[13].ColumnName = "obl_ytd";
          ds.Tables[0].Columns[14].ColumnName = "pay_limit";
          ds.Tables[0].Columns[15].ColumnName = "acct_no_kv";
          ds.Tables[0].Columns[16].ColumnName = "bal_acct_no_kv";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    public static int starting_check_no()
    {
      int check_no = 0;
      object[] Parameter = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object checkno = objDalBaseClass.ExecuteScalar(ref Parameter, (new DVOPostAP()).FIND_LASTCHECKNO);
      if (checkno != null)
      {
        check_no = Convert.ToInt32(checkno);
      }
      return check_no;
    }
    //***********************************************************************

    public static DataSet GetEmployeeInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSLedgerDefaults = objDalBaseClass.GetAllData(typeof(DVOUpdateLedgerDefaults));
      if (DSLedgerDefaults.Tables.Count >= 1)
      {
        DSLedgerDefaults.Tables[0].Columns[0].ColumnName = "rowid";
        DSLedgerDefaults.Tables[0].Columns[1].ColumnName = "curr_period";
        DSLedgerDefaults.Tables[0].Columns[2].ColumnName = "curr_year";
        DSLedgerDefaults.Tables[0].Columns[3].ColumnName = "gl_balanced";
        DSLedgerDefaults.Tables[0].Columns[4].ColumnName = "use_batch_gen";
        DSLedgerDefaults.Tables[0].Columns[5].ColumnName = "use_approv_post";
        DSLedgerDefaults.Tables[0].Columns[6].ColumnName = "start_date";
        DSLedgerDefaults.Tables[0].Columns[7].ColumnName = "end_date";
        DSLedgerDefaults.Tables[0].Columns[8].ColumnName = "co_name";
        DSLedgerDefaults.Tables[0].Columns[9].ColumnName = "retain_earnings";

      }
      return DSLedgerDefaults;

    }
    /// <summary>
    /// Method to get Treasury class description
    /// </summary>
    /// <param name="objTreasuryBillClasses">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetTreasuryClasses(ref DVOTreasuryBillClasses objTreasuryBillClasses)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[1];
      parameters[0] = objTreasuryBillClasses.Class_desc;
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOTreasuryBillClasses), objTreasuryBillClasses.GET_Treasury_Classes);
      if (ds.Tables.Count >= 1)
      {
        ds.Tables[0].Columns[0].ColumnName = "rowid";
        ds.Tables[0].Columns[1].ColumnName = "class_code";
        ds.Tables[0].Columns[2].ColumnName = "class_desc";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Daily Sales Register details
    /// </summary>
    /// <param name="objTreasuryBillClasses">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetDailtSalesReports(ref DVOSalesRegister objSalesRegister)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[2];
      parameters[0] = objSalesRegister.start_date;
      parameters[1] = objSalesRegister.end_date;
      DataSet ds = objDalBaseClass.GetData(objSalesRegister.FIND_QUERY(ref parameters));
      if (ds.Tables.Count >= 1)
      {
        ds.Tables[0].Columns[0].ColumnName = "currency_code";
        ds.Tables[0].Columns[1].ColumnName = "currency_rate";
        ds.Tables[0].Columns[2].ColumnName = "frght_amount";
        ds.Tables[0].Columns[3].ColumnName = "inv_date";
        ds.Tables[0].Columns[4].ColumnName = "inv_doc_no";
        ds.Tables[0].Columns[5].ColumnName = "inv_no";
        ds.Tables[0].Columns[6].ColumnName = "tax_amount";
        ds.Tables[0].Columns[7].ColumnName = "doc_no";
        ds.Tables[0].Columns[8].ColumnName = "gross_margin";
        ds.Tables[0].Columns[9].ColumnName = "item_cost";
        ds.Tables[0].Columns[10].ColumnName = "net_amount";
        ds.Tables[0].Columns[11].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[12].ColumnName = "ship_qty";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Product Summary
    /// </summary>
    /// <param name="objSalesRegister">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetProductSummary(ref DVOSalesRegister objSalesRegister)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objSalesRegister.start_date;
      parameters[1] = objSalesRegister.end_date;
      parameters[2] = objSalesRegister.item_class;
      DataSet ds = objDalBaseClass.GetData(objSalesRegister.FIND_QUERY_2(ref parameters));
      if (ds.Tables.Count >= 1)
      {
        //ds.Tables[0].Columns[0].ColumnName = "rowid";
        ds.Tables[0].Columns[0].ColumnName = "item_class";
        ds.Tables[0].Columns[1].ColumnName = "currency_code";
        ds.Tables[0].Columns[2].ColumnName = "currency_rate";
        ds.Tables[0].Columns[3].ColumnName = "doc_no";
        ds.Tables[0].Columns[4].ColumnName = "gross_margin";
        ds.Tables[0].Columns[5].ColumnName = "net_amount";
        ds.Tables[0].Columns[6].ColumnName = "tax_amount";
        ds.Tables[0].Columns[7].ColumnName = "src_desc";
        ds.Tables[0].Columns[8].ColumnName = "start_date";
        ds.Tables[0].Columns[9].ColumnName = "end_date";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Product By Date Summary
    /// </summary>
    /// <param name="objSalesRegister">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetProductByDateSummary(ref DVOSalesRegister objSalesRegister)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objSalesRegister.start_date;
      parameters[1] = objSalesRegister.end_date;
      parameters[2] = objSalesRegister.item_class;
      DataSet ds = objDalBaseClass.GetData(objSalesRegister.FIND_QUERY_3(ref parameters));
      if (ds.Tables.Count >= 1)
      {
        //ds.Tables[0].Columns[0].ColumnName = "rowid";
        ds.Tables[0].Columns[0].ColumnName = "item_class";
        ds.Tables[0].Columns[1].ColumnName = "currency_code";
        ds.Tables[0].Columns[2].ColumnName = "currency_rate";
        ds.Tables[0].Columns[3].ColumnName = "doc_no";
        ds.Tables[0].Columns[4].ColumnName = "gross_margin";
        ds.Tables[0].Columns[5].ColumnName = "inv_date";
        ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
        ds.Tables[0].Columns[7].ColumnName = "item_code";
        ds.Tables[0].Columns[8].ColumnName = "item_cost";
        ds.Tables[0].Columns[9].ColumnName = "net_amount";
        ds.Tables[0].Columns[10].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[11].ColumnName = "src_desc";
        ds.Tables[0].Columns[12].ColumnName = "start_date";
        ds.Tables[0].Columns[13].ColumnName = "end_date";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Product Detail
    /// </summary>
    /// <param name="objSalesRegister">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetProductDetail(ref DVOSalesRegister objSalesRegister)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[4];
      parameters[0] = objSalesRegister.start_date;
      parameters[1] = objSalesRegister.end_date;
      parameters[2] = objSalesRegister.item_class;
      parameters[3] = objSalesRegister.item_code;
      DataSet ds = objDalBaseClass.GetData(objSalesRegister.FIND_QUERY_4(ref parameters));
      if (ds.Tables.Count >= 1)
      {
        //ds.Tables[0].Columns[0].ColumnName = "rowid";
        ds.Tables[0].Columns[0].ColumnName = "item_class";
        ds.Tables[0].Columns[1].ColumnName = "currency_code";
        ds.Tables[0].Columns[2].ColumnName = "currency_rate";
        ds.Tables[0].Columns[3].ColumnName = "line_type";
        ds.Tables[0].Columns[4].ColumnName = "doc_no";
        ds.Tables[0].Columns[5].ColumnName = "gross_margin";
        ds.Tables[0].Columns[6].ColumnName = "inv_date";
        ds.Tables[0].Columns[7].ColumnName = "inv_doc_no";
        ds.Tables[0].Columns[8].ColumnName = "item_code";
        ds.Tables[0].Columns[9].ColumnName = "item_cost";
        ds.Tables[0].Columns[10].ColumnName = "net_amount";
        ds.Tables[0].Columns[11].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[12].ColumnName = "start_date";
        ds.Tables[0].Columns[13].ColumnName = "end_date";
        ds.Tables[0].Columns[14].ColumnName = "class_desc";
        ds.Tables[0].Columns[15].ColumnName = "line_type_desc";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Customer Summary
    /// </summary>
    /// <param name="objSalesRegister">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetCustomerSummary(ref DVOOrderCustomerInfostrcustr objSummary)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objSummary.stDate;
      parameters[1] = objSummary.endDate;
      parameters[2] = objSummary.cust_code;
      DataSet ds = objDalBaseClass.GetData(objSummary.FINDQUERY_CUSTOMER_SUMMARY(ref parameters));
      if (ds.Tables.Count >= 1)
      {
        //ds.Tables[0].Columns[0].ColumnName = "rowid";
        ds.Tables[0].Columns[0].ColumnName = "currency_code";
        ds.Tables[0].Columns[1].ColumnName = "currency_rate";
        ds.Tables[0].Columns[2].ColumnName = "doc_no";
        ds.Tables[0].Columns[3].ColumnName = "gross_margin";
        ds.Tables[0].Columns[4].ColumnName = "inv_date";
        ds.Tables[0].Columns[5].ColumnName = "net_amount";
        ds.Tables[0].Columns[6].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[7].ColumnName = "bus_name";
        ds.Tables[0].Columns[8].ColumnName = "start_date";
        ds.Tables[0].Columns[9].ColumnName = "end_date";

      }
      return ds;

    }
    /// <summary>
    /// Method to get Tender List description
    /// </summary>
    /// <param name="objTreasuryBillClasses">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetTenderList(ref DVOTreasuryBillTenders objTreasuryBillTenders)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = new DataSet();
      try
      {

        object[] parameters = new object[5];
        parameters[0] = objTreasuryBillTenders.tend_code;
        parameters[1] = objTreasuryBillTenders.tend_class;
        parameters[2] = objTreasuryBillTenders.tend_name;
        parameters[3] = objTreasuryBillTenders.address1;
        parameters[4] = objTreasuryBillTenders.Option;
        ds = objDalBaseClass.GetData(objTreasuryBillTenders.GET_Tender_List(ref parameters));

        #region set column name
        ds.Tables[0].Columns[0].ColumnName = "tend_code";
        ds.Tables[0].Columns[2].ColumnName = "tend_name";
        ds.Tables[0].Columns[3].ColumnName = "address1";
        ds.Tables[0].Columns[4].ColumnName = "address2";
        //Added by Sunil Pahwa 
        ds.Tables[0].Columns[5].ColumnName = "city";
        ds.Tables[0].Columns[6].ColumnName = "contact";
        ds.Tables[0].Columns[7].ColumnName = "phone";
        ds.Tables[0].Columns[8].ColumnName = "email";
        //*********************
        ds.Tables[0].Columns.Add("Last_Issue_No", typeof(string));
        #endregion set column name

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //DVOTreasuryBillMaintanance objIssue_num = new DVOTreasuryBillMaintanance();
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //Get value of last_issue_num and last Bill No.(line_no)
        //string tend_code = dr["tend_code"].ToString().Trim();
        //parameters = new object[1];
        //parameters[0] = tend_code;
        //DataSet DSIssue_no = objDalBaseClass.GetData(ref parameters, typeof(DVOTreasuryBillMaintanance), objIssue_num.GET_ISSUE_NUM);
        //DSIssue_no.Tables[0].Columns[0].ColumnName = "issue_num";
        //if (DSIssue_no.Tables[0].Rows.Count > 0 && DSIssue_no != null)
        //{
        //    if (DSIssue_no.Tables[0].Rows[0]["issue_num"] != DBNull.Value)
        //        dr["Last_Issue_No"] = DSIssue_no.Tables[0].Rows[0]["issue_num"].ToString();
        //    else
        //        dr["Last_Issue_No"] = "New Tender";
        //}
        //else
        //{
        //    dr["Last_Issue_No"] = "New Tender";
        //}
        //}
        //}
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }

    /// <summary>
    /// Method to get Print Allotment Letter information
    /// </summary>
    /// <param name="objTreasuryBillmaintanance">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetTreasuryBill(ref DVOTreasuryBillMaintanance objTreasuryBillmaintanance)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objTreasuryBillmaintanance.tend_code;
      parameters[1] = objTreasuryBillmaintanance.tend_code_to;
      parameters[2] = objTreasuryBillmaintanance.issue_no;
      DataSet ds = objDalBaseClass.GetData(objTreasuryBillmaintanance.FIND_QUERY_4(ref parameters));
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "issue_num";
        ds.Tables[0].Columns[1].ColumnName = "tend_code";
        ds.Tables[0].Columns[2].ColumnName = "amt_applied_for";
        ds.Tables[0].Columns[3].ColumnName = "amt_issued";
        ds.Tables[0].Columns[4].ColumnName = "amt_per_100";
        ds.Tables[0].Columns[5].ColumnName = "issue_date";
        ds.Tables[0].Columns[6].ColumnName = "redeem_date";
        ds.Tables[0].Columns[7].ColumnName = "bill_status";
        ds.Tables[0].Columns[8].ColumnName = "tend_name";
        ds.Tables[0].Columns[9].ColumnName = "address1";
        ds.Tables[0].Columns[10].ColumnName = "address2";
        ds.Tables[0].Columns[11].ColumnName = "status";
        ds.Tables[0].Columns[12].ColumnName = "contact";
      }
      return ds;

    }
    /// <summary>
    /// Method to get Treasuty bill receipt information
    /// </summary>
    /// <param name="objTreasuryBillmaintanance">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetTreasuryBillReceipt(ref DVOTreasuryBillMaintanance objTreasuryBillmaintanance)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objTreasuryBillmaintanance.tend_code;
      parameters[1] = objTreasuryBillmaintanance.issue_no;
      parameters[2] = objTreasuryBillmaintanance.schemeId;
      DataSet ds = objDalBaseClass.GetData(objTreasuryBillmaintanance.FIND_QUERY_3(ref parameters));
      if (ds != null && ds.Tables.Count > 0)
        if (ds.Tables[0].Rows.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "issue_num";
          ds.Tables[0].Columns[1].ColumnName = "amt_applied_for";
          ds.Tables[0].Columns[2].ColumnName = "tend_code";
          ds.Tables[0].Columns[3].ColumnName = "tend_name";
          ds.Tables[0].Columns[4].ColumnName = "cash_amt";
          ds.Tables[0].Columns[5].ColumnName = "rcpt_date";
          ds.Tables[0].Columns[6].ColumnName = "tre_voucher_no";
          ds.Tables[0].Columns[7].ColumnName = "acct_desc";
          ds.Tables[0].Columns[8].ColumnName = "keyvalue";
          ds.Tables[0].Columns[9].ColumnName = "tbschid";
          ds.Tables[0].Columns[10].ColumnName = "tbschname";
        }
      return ds;
    }

    /// <summary>
    /// Method to get Treasuty bill withdraw information
    /// </summary>
    /// <param name="objTreasuryBillmaintanance">an object as a collection of parameter</param>
    /// <returns>return dataset</returns>
    public static DataSet GetTreasuryBillWithDraw(ref DVOTreasuryBillMaintanance objTreasuryBillmaintanance)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[2];
      parameters[0] = objTreasuryBillmaintanance.tend_code;
      parameters[1] = objTreasuryBillmaintanance.issue_no;
      DataSet ds = objDalBaseClass.GetData(objTreasuryBillmaintanance.FIND_QUERY_5(ref parameters));
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "issue_num";
        ds.Tables[0].Columns[1].ColumnName = "tend_code";
        ds.Tables[0].Columns[2].ColumnName = "issue_date";
        ds.Tables[0].Columns[3].ColumnName = "redeem_date";
        ds.Tables[0].Columns[4].ColumnName = "amt_issued";
        ds.Tables[0].Columns[5].ColumnName = "amt_per_100";
        ds.Tables[0].Columns[6].ColumnName = "withdraw_date";
        ds.Tables[0].Columns[7].ColumnName = "withdraw_reason";

      }
      return ds;

    }
    public static DataSet GetPayrollAnalysis(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objPayEmployeeTypes.InsertDate;
      parameters[1] = objPayEmployeeTypes.UpdateDate;
      parameters[2] = objPayEmployeeTypes.type_code;
      // DataSet DSEmployeePayrollList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_PayrollAnalysis(ref parameters));
      DataSet DSEmployeePayrollList = objDalBaseClass.GetData(ref parameters, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.PayrollAnalysis);
      if (DSEmployeePayrollList.Tables.Count >= 1)
      {
        DSEmployeePayrollList.Tables[0].Columns[0].ColumnName = "PayProcess_id";
        DSEmployeePayrollList.Tables[0].Columns[1].ColumnName = "doc_no";
        DSEmployeePayrollList.Tables[0].Columns[2].ColumnName = "doc_date";
        DSEmployeePayrollList.Tables[0].Columns[3].ColumnName = "pay_date";
        DSEmployeePayrollList.Tables[0].Columns[4].ColumnName = "eop_date";
        DSEmployeePayrollList.Tables[0].Columns[5].ColumnName = "cash_amount";
        DSEmployeePayrollList.Tables[0].Columns[6].ColumnName = "inc_gross";
        DSEmployeePayrollList.Tables[0].Columns[7].ColumnName = "position";
        DSEmployeePayrollList.Tables[0].Columns[8].ColumnName = "SegmentDesc";
        DSEmployeePayrollList.Tables[0].Columns[9].ColumnName = "SegDesc";
        DSEmployeePayrollList.Tables[0].Columns[10].ColumnName = "keyvalue";
        DSEmployeePayrollList.Tables[0].Columns[11].ColumnName = "empl_code";
        DSEmployeePayrollList.Tables[0].Columns[12].ColumnName = "first_name";
        DSEmployeePayrollList.Tables[0].Columns[13].ColumnName = "flexdeptaccttype";
        DSEmployeePayrollList.Tables[0].Columns[14].ColumnName = "last_name";
        DSEmployeePayrollList.Tables[0].Columns[15].ColumnName = "middle_name";
        DSEmployeePayrollList.Tables[0].Columns[16].ColumnName = "terminated";
        DSEmployeePayrollList.Tables[0].Columns[17].ColumnName = "Socialsecurity";
        DSEmployeePayrollList.Tables[0].Columns[18].ColumnName = "Levyee";
        DSEmployeePayrollList.Tables[0].Columns[19].ColumnName = "Empcontribution";
        DSEmployeePayrollList.Tables[0].Columns[20].ColumnName = "EIB";
      }
      return DSEmployeePayrollList;

    }

    public static DataSet GetPayrollACAnalysis(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      parameters[0] = objPayEmployeeTypes.InsertDate;
      parameters[1] = objPayEmployeeTypes.UpdateDate;
      parameters[2] = objPayEmployeeTypes.type_code;
      // DataSet DSEmployeePayrollList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_PayrollAnalysis(ref parameters));
      DataSet DSEmployeePayrollList = objDalBaseClass.GetData(ref parameters, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.PayrollAccountAnalysis);
      if (DSEmployeePayrollList.Tables.Count >= 1)
      {
        DSEmployeePayrollList.Tables[0].Columns[0].ColumnName = "PayProcess_id";
        DSEmployeePayrollList.Tables[0].Columns[1].ColumnName = "doc_no";
        DSEmployeePayrollList.Tables[0].Columns[2].ColumnName = "doc_date";
        DSEmployeePayrollList.Tables[0].Columns[3].ColumnName = "pay_date";
        DSEmployeePayrollList.Tables[0].Columns[4].ColumnName = "eop_date";
        DSEmployeePayrollList.Tables[0].Columns[5].ColumnName = "cash_amount";
        DSEmployeePayrollList.Tables[0].Columns[6].ColumnName = "inc_gross";
        DSEmployeePayrollList.Tables[0].Columns[7].ColumnName = "position";
        DSEmployeePayrollList.Tables[0].Columns[8].ColumnName = "SegmentDesc";
        DSEmployeePayrollList.Tables[0].Columns[9].ColumnName = "SegDesc";
        DSEmployeePayrollList.Tables[0].Columns[10].ColumnName = "keyvalue";
        DSEmployeePayrollList.Tables[0].Columns[11].ColumnName = "empl_code";
        DSEmployeePayrollList.Tables[0].Columns[12].ColumnName = "first_name";
        DSEmployeePayrollList.Tables[0].Columns[13].ColumnName = "flexdeptaccttype";
        DSEmployeePayrollList.Tables[0].Columns[14].ColumnName = "last_name";
        DSEmployeePayrollList.Tables[0].Columns[15].ColumnName = "middle_name";
        DSEmployeePayrollList.Tables[0].Columns[16].ColumnName = "terminated";
        DSEmployeePayrollList.Tables[0].Columns[17].ColumnName = "Socialsecurity";
        DSEmployeePayrollList.Tables[0].Columns[18].ColumnName = "Levyee";
        DSEmployeePayrollList.Tables[0].Columns[19].ColumnName = "Empcontribution";
        DSEmployeePayrollList.Tables[0].Columns[20].ColumnName = "EIB";
        DSEmployeePayrollList.Tables[0].Columns[21].ColumnName = "BankName";
      }
      return DSEmployeePayrollList;

    }

    public static DataSet GetEmployeeListByMinistry(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";

      }
      return DSEmployeeList;

    }

    //Added By Neeraj date 07/04/2015
    public static DataSet GetEmployeeHolidayPayCalculation(string objEmpl_Code, string start_date, string end_date, string lrd, string expectedresumeduty)
    {
      string SQL = "SELECT sum(isnull(Process_PayIncomes.hours,0))hours,sum(isnull(Process_PayEmployee.total_hours,0))total_hours,";
      SQL = SQL + "sum(isnull(Process_PayEmployee.inc_net,0))inc_net,Process_PayEmployee.empl_code,";
      SQL = SQL + "isnull(first_name,'')+' '+isnull(middle_name,'')+' '+isnull(last_name,'')empl_name,";
      SQL = SQL + "isnull(address1,'')+' '+isnull(address2,'')+' '+isnull(city,'')+' '+isnull(state,'')+' '+isnull(zip,'')empl_address,";
      SQL = SQL + "[desc] department,'" + lrd + "' lrd,sum(isnull(Process_PayEmployee.total_hours,0))/8 totaldays,'" + expectedresumeduty + "' expectedresumeduty ";
      SQL = SQL + " FROM Process_PayIncomes ,Process_PayEmployee,MasterEmployee LEFT OUTER JOIN";
      SQL = SQL + " Flex_Segment_Reference  WITH(NOLOCK) ON MasterEmployee.empl_code = Flex_Segment_Reference.code ";
      SQL = SQL + " LEFT OUTER JOIN  Flex_Segment_Value_Details WITH(NOLOCK) ON Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id";
      SQL = SQL + " LEFT OUTER JOIN Master_Segment WITH(NOLOCK) ON Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid";
      SQL = SQL + " WHERE Process_PayIncomes.doc_no=Process_PayEmployee.doc_no ";
      SQL = SQL + "AND Process_PayEmployee.empl_code=MasterEmployee.empl_code ";
      SQL = SQL + "AND Process_PayEmployee.ok_to_post = 'P' ";
      SQL = SQL + "AND ISNULL(Process_PayEmployee.empl_code,'')=ISNULL('" + objEmpl_Code + "',ISNULL(Process_PayEmployee.empl_code,'')) and pay_date >='" + start_date + "' and pay_date <='" + end_date + "' ";
      SQL = SQL + " AND Flex_Segment_Reference.entity_type = 'styemplr' and Flex_Segment_Value_Details.segmentid=2 ";
      SQL = SQL + "group by Process_PayEmployee.empl_code,isnull(first_name,'')+' '+isnull(middle_name,'')+' '+isnull(last_name,''),";
      SQL = SQL + "isnull(address1,'')+' '+isnull(address2,'')+' '+isnull(city,'')+' '+isnull(state,'')+' '+isnull(zip,''),[desc]";


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(SQL);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "hours";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "total_hours";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "inc_net";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "empl_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_address";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "department";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "lrd";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "totaldays";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "expectedresumeduty";

      }
      return DSEmployeeList;

    }


    public static DataSet GetEmployeeListByMinistrywithcountry(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_List_By_Ministrywithcountry);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "currentAge";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "birthdate";
        DSEmployeeList.Tables[0].Columns[13].ColumnName = "country";
        DSEmployeeList.Tables[0].Columns[14].ColumnName = "gender";
        DSEmployeeList.Tables[0].Columns[15].ColumnName = "GrossAmount";
        DSEmployeeList.Tables[0].Columns[16].ColumnName = "NetAmount";
        DSEmployeeList.Tables[0].Columns[17].ColumnName = "State";
      }
      return DSEmployeeList;

    }
    public static DataSet GetEmployeeListByMinistrywithdegree(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_List_By_Ministrywithdegree);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "currentAge";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "birthdate";
        DSEmployeeList.Tables[0].Columns[13].ColumnName = "country";
        DSEmployeeList.Tables[0].Columns[14].ColumnName = "gender";
        DSEmployeeList.Tables[0].Columns[15].ColumnName = "GrossAmount";
        DSEmployeeList.Tables[0].Columns[16].ColumnName = "NetAmount";
        DSEmployeeList.Tables[0].Columns[17].ColumnName = "State";
        DSEmployeeList.Tables[0].Columns[18].ColumnName = "Paydate";
        DSEmployeeList.Tables[0].Columns[19].ColumnName = "HourlyRate";
      }
      return DSEmployeeList;

    }
    public static DataSet GetEmployeeListByEmployer(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_List_By_Employer);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";

      }
      return DSEmployeeList;

    }
    public static DataSet GetEmployeeListByMinistrywithoutBankacct(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_ListwithoutBankAcct);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "Birthdate";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "Phone";

      }
      return DSEmployeeList;

    }


    public static DataSet GetEmployeeListByMinistrywithoutsocsec(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_Listwithoutsocsec);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "Birthdate";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "Phone";
        DSEmployeeList.Tables[0].Columns[13].ColumnName = "Soc_sec_num";

      }
      return DSEmployeeList;

    }


    public static DataSet GetEmployeeListforVerification(ref DVOMasterEmployee objPayEmployeeTypes)
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_Employee_Forverification);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "Birthdate";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "Phone";
        DSEmployeeList.Tables[0].Columns[13].ColumnName = "Soc_sec_num";
        DSEmployeeList.Tables[0].Columns[14].ColumnName = "dir_dept";
        DSEmployeeList.Tables[0].Columns[15].ColumnName = "typeofacct";
        DSEmployeeList.Tables[0].Columns[16].ColumnName = "bank_acct_no";
        DSEmployeeList.Tables[0].Columns[17].ColumnName = "bank_desc";

      }
      return DSEmployeeList;

    }

    public static DataSet GetEmployerList(ref DVOMasterEmployee objPayEmployeeTypes)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSEmployeeList = objDalBaseClass.GetData(objPayEmployeeTypes.GET_EmployerList);
      //DataSet DSEmployeeList = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee), objPayEmployeeTypes.GET_Employee_List_By_Ministry);
      if (DSEmployeeList.Tables.Count >= 1)
      {
        DSEmployeeList.Tables[0].Columns[0].ColumnName = "position";
        DSEmployeeList.Tables[0].Columns[1].ColumnName = "desc";
        DSEmployeeList.Tables[0].Columns[2].ColumnName = "SegDesc";
        DSEmployeeList.Tables[0].Columns[3].ColumnName = "keyvalue";
        DSEmployeeList.Tables[0].Columns[4].ColumnName = "co_name";
        DSEmployeeList.Tables[0].Columns[5].ColumnName = "empl_code";
        DSEmployeeList.Tables[0].Columns[6].ColumnName = "first_name";
        DSEmployeeList.Tables[0].Columns[7].ColumnName = "flexdeptaccttype";
        DSEmployeeList.Tables[0].Columns[8].ColumnName = "last_name";
        DSEmployeeList.Tables[0].Columns[9].ColumnName = "middle_name";
        DSEmployeeList.Tables[0].Columns[10].ColumnName = "terminated";
        DSEmployeeList.Tables[0].Columns[11].ColumnName = "Birthdate";
        DSEmployeeList.Tables[0].Columns[12].ColumnName = "Phone";
        DSEmployeeList.Tables[0].Columns[13].ColumnName = "Soc_sec_num";
        DSEmployeeList.Tables[0].Columns[14].ColumnName = "abbreviation";
        DSEmployeeList.Tables[0].Columns[15].ColumnName = "AppReferenceNo";
      }
      return DSEmployeeList;

    }
    public static DataSet GetPayrollDefaultInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSPayrollDefaults = objDalBaseClass.GetAllData(typeof(DVOPayrollDefaults));
      if (DSPayrollDefaults.Tables[0].Rows.Count >= 1)
      {
        DSPayrollDefaults.Tables[0].Columns[0].ColumnName = "ein_number";
        DSPayrollDefaults.Tables[0].Columns[1].ColumnName = "state_number";
        DSPayrollDefaults.Tables[0].Columns[2].ColumnName = "post_gl";
        DSPayrollDefaults.Tables[0].Columns[3].ColumnName = "fedtax_code";
        DSPayrollDefaults.Tables[0].Columns[4].ColumnName = "futa_code";
        DSPayrollDefaults.Tables[0].Columns[5].ColumnName = "fica_code";
        DSPayrollDefaults.Tables[0].Columns[6].ColumnName = "fica_ob_code";
        DSPayrollDefaults.Tables[0].Columns[7].ColumnName = "medicare_code";
        DSPayrollDefaults.Tables[0].Columns[8].ColumnName = "medicare_ob_code";
        DSPayrollDefaults.Tables[0].Columns[9].ColumnName = "statax_code";
        DSPayrollDefaults.Tables[0].Columns[10].ColumnName = "eic_code";
        DSPayrollDefaults.Tables[0].Columns[11].ColumnName = "loctax_code";
        DSPayrollDefaults.Tables[0].Columns[12].ColumnName = "exp_acct";
        DSPayrollDefaults.Tables[0].Columns[13].ColumnName = "liab_acct";
        DSPayrollDefaults.Tables[0].Columns[14].ColumnName = "cash_acct";
        DSPayrollDefaults.Tables[0].Columns[15].ColumnName = "immed_dest_dfi";
        DSPayrollDefaults.Tables[0].Columns[16].ColumnName = "immed_chk_digit";
        DSPayrollDefaults.Tables[0].Columns[17].ColumnName = "offset_debit";
        DSPayrollDefaults.Tables[0].Columns[18].ColumnName = "immed_dest_name";
        DSPayrollDefaults.Tables[0].Columns[19].ColumnName = "co_bank_acct_no";
        DSPayrollDefaults.Tables[0].Columns[20].ColumnName = "mmedia_file";
        DSPayrollDefaults.Tables[0].Columns[21].ColumnName = "mmedia_command";
        DSPayrollDefaults.Tables[0].Columns[22].ColumnName = "keyvalue";
      }
      return DSPayrollDefaults;
    }
    //**********************Added by Sunil Pahwa  on 16/01/2009********************
    public static DataSet GetPaymentToEmployee(ref DVOPaymentToEmployee objDvoPaytoEmp)
    {

      object[] Parameter = new object[2];
      Parameter[0] = objDvoPaytoEmp.startdate;
      Parameter[1] = objDvoPaytoEmp.enddate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSPaymentsToEmployee = objDalBaseClass.GetData(ref Parameter, typeof(DVOPaymentToEmployee));
      if (DSPaymentsToEmployee.Tables.Count >= 1)
      {

        DSPaymentsToEmployee.Tables[0].Columns[0].ColumnName = "rowid";
        DSPaymentsToEmployee.Tables[0].Columns[1].ColumnName = "empl_code";
        DSPaymentsToEmployee.Tables[0].Columns[2].ColumnName = "soc_sec_num";
        DSPaymentsToEmployee.Tables[0].Columns[3].ColumnName = "type_code";
        DSPaymentsToEmployee.Tables[0].Columns[4].ColumnName = "first_name";
        DSPaymentsToEmployee.Tables[0].Columns[5].ColumnName = "last_name";
        DSPaymentsToEmployee.Tables[0].Columns[6].ColumnName = "address1";
        DSPaymentsToEmployee.Tables[0].Columns[7].ColumnName = "cash_acct";
        DSPaymentsToEmployee.Tables[0].Columns[8].ColumnName = "department";
        DSPaymentsToEmployee.Tables[0].Columns[9].ColumnName = "job_title";

        DSPaymentsToEmployee.Tables[0].Columns[10].ColumnName = "date_hired";
        DSPaymentsToEmployee.Tables[0].Columns[11].ColumnName = "empl_status";
        DSPaymentsToEmployee.Tables[0].Columns[12].ColumnName = "bank_acct_no";
        DSPaymentsToEmployee.Tables[0].Columns[13].ColumnName = "gender";
        DSPaymentsToEmployee.Tables[0].Columns[14].ColumnName = "doc_no";
        DSPaymentsToEmployee.Tables[0].Columns[15].ColumnName = "doc_date";
        DSPaymentsToEmployee.Tables[0].Columns[16].ColumnName = "pay_date";
        DSPaymentsToEmployee.Tables[0].Columns[17].ColumnName = "print_check";
        DSPaymentsToEmployee.Tables[0].Columns[18].ColumnName = "cash_acct_no";
        DSPaymentsToEmployee.Tables[0].Columns[19].ColumnName = "department1";

        DSPaymentsToEmployee.Tables[0].Columns[20].ColumnName = "cash_amount";
        DSPaymentsToEmployee.Tables[0].Columns[21].ColumnName = "inc_gross";
        DSPaymentsToEmployee.Tables[0].Columns[22].ColumnName = "inc_taxable";
        DSPaymentsToEmployee.Tables[0].Columns[23].ColumnName = "ded_medicare";
        DSPaymentsToEmployee.Tables[0].Columns[24].ColumnName = "ded_fedtax";
        DSPaymentsToEmployee.Tables[0].Columns[25].ColumnName = "ded_loctax";
        DSPaymentsToEmployee.Tables[0].Columns[26].ColumnName = "ded_other";
        DSPaymentsToEmployee.Tables[0].Columns[27].ColumnName = "total_hours";

      }
      return DSPaymentsToEmployee;

    }
    //****************************************************************************************
    //****************Added By Sunil Pahwa on 17/01/2009**************************************
    public static DataSet GetPaymentHistoryOfStypayid(ref DVOPaymentToEmployee objDvoPaytoEmp)
    {
      object[] parameter = new object[2];
      parameter[0] = objDvoPaytoEmp.startdate;
      parameter[1] = objDvoPaytoEmp.enddate;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSstypayid = objDalBaseClass.GetData(ref parameter, typeof(DVOPaymentToEmployee), objDvoPaytoEmp.GET_EMPLOYEE_PAY_INFORMATION);

      if (DSstypayid.Tables.Count >= 1)
      {

        DSstypayid.Tables[0].Columns[0].ColumnName = "doc_no";
        DSstypayid.Tables[0].Columns[1].ColumnName = "line_no";
        DSstypayid.Tables[0].Columns[2].ColumnName = "inc_code";
        DSstypayid.Tables[0].Columns[3].ColumnName = "inc_rate";
        DSstypayid.Tables[0].Columns[4].ColumnName = "number";
        DSstypayid.Tables[0].Columns[5].ColumnName = "hours";
        DSstypayid.Tables[0].Columns[6].ColumnName = "amount";
        DSstypayid.Tables[0].Columns[7].ColumnName = "acct_no";
        DSstypayid.Tables[0].Columns[8].ColumnName = "department";
        DSstypayid.Tables[0].Columns[9].ColumnName = "mod_flag";
        DSstypayid.Tables[0].Columns[10].ColumnName = "add_code";
        DSstypayid.Tables[0].Columns[11].ColumnName = "lo_inc_amt";
        DSstypayid.Tables[0].Columns[12].ColumnName = "hi_inc_amt";
      }
      return DSstypayid;
    }

    public static DataSet GetPaymentHistoryOfStypayod(ref DVOPaymentToEmployee objDvoPaytoEmp)
    {
      object[] parameter = new object[2];
      parameter[0] = objDvoPaytoEmp.startdate;
      parameter[1] = objDvoPaytoEmp.enddate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSstypayod = objDalBaseClass.GetData(ref parameter, typeof(DVOPaymentToEmployee), objDvoPaytoEmp.GET_EMPLOYEE_PAY_OBLIGATION);

      if (DSstypayod.Tables.Count >= 1)
      {

        DSstypayod.Tables[0].Columns[0].ColumnName = "doc_no";
        DSstypayod.Tables[0].Columns[1].ColumnName = "line_no";
        DSstypayod.Tables[0].Columns[2].ColumnName = "obl_code";
        DSstypayod.Tables[0].Columns[3].ColumnName = "obl_rate";
        DSstypayod.Tables[0].Columns[4].ColumnName = "amount";
        DSstypayod.Tables[0].Columns[5].ColumnName = "acct_no";
        DSstypayod.Tables[0].Columns[6].ColumnName = "department";
        DSstypayod.Tables[0].Columns[7].ColumnName = "bal_acct_no";
        DSstypayod.Tables[0].Columns[8].ColumnName = "bal_dept";
        DSstypayod.Tables[0].Columns[9].ColumnName = "mod_flag";
        DSstypayod.Tables[0].Columns[10].ColumnName = "add_code";

      }
      return DSstypayod;

    }

    public static DataSet GetPaymentHistoryOfStypaydd(ref DVOPaymentToEmployee objDvoPaytoEmp)
    {

      object[] parameter = new object[2];
      parameter[0] = objDvoPaytoEmp.startdate;
      parameter[1] = objDvoPaytoEmp.enddate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSstypaydd = objDalBaseClass.GetData(ref parameter, typeof(DVOPaymentToEmployee), objDvoPaytoEmp.GET_EMPLOYEE_PAY_DEDUCTION);

      if (DSstypaydd.Tables.Count >= 1)
      {

        DSstypaydd.Tables[0].Columns[0].ColumnName = "doc_no";
        DSstypaydd.Tables[0].Columns[1].ColumnName = "line_no";
        DSstypaydd.Tables[0].Columns[2].ColumnName = "ded_code";
        DSstypaydd.Tables[0].Columns[3].ColumnName = "ded_rate";
        DSstypaydd.Tables[0].Columns[4].ColumnName = "amount";
        DSstypaydd.Tables[0].Columns[5].ColumnName = "acct_no";
        DSstypaydd.Tables[0].Columns[6].ColumnName = "department";
        DSstypaydd.Tables[0].Columns[7].ColumnName = "mod_flag";
        DSstypaydd.Tables[0].Columns[8].ColumnName = "add_code";
        DSstypaydd.Tables[0].Columns[9].ColumnName = "lo_ded_amount";
        DSstypaydd.Tables[0].Columns[10].ColumnName = "hi_ded_amount";

      }
      return DSstypaydd;

    }
    public static object GetPayToName(string vend_code, string paytocode)
    {
      object[] parameter = new object[2];
      parameter[0] = vend_code;
      parameter[1] = paytocode;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object PayToName = objDalBaseClass.ExecuteScalar(ref parameter, (new DVOPaymentDue()).GETPAYTONAME);
      return PayToName;
    }
    //*************************************************************************************************************

    //Developed by rajeev
    //Development date: 9 Feb 2009
    //Aim: To generate Tresury bill certificate

    public static DataSet GetTreasuryBillCertificate(ref DVOTreasuryBillCertificate objTreasuryBillCertificate)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      object[] parameter = new object[4];
      parameter[0] = objTreasuryBillCertificate.issue_num;
      parameter[1] = objTreasuryBillCertificate.tend_code;
      parameter[2] = objTreasuryBillCertificate.tend_codemax;
      parameter[3] = objTreasuryBillCertificate.tbschid;
      DataSet DSTreasuryCertificate = objDalBaseClass.GetData(ref parameter, typeof(DVOTreasuryBillCertificate));//, objTreasuryBillCertificate.GET_TREASURY_BILL_CERTIFICATE);
      if (DSTreasuryCertificate != null && DSTreasuryCertificate.Tables.Count > 0)
      {
        //DSTreasuryCertificate.Tables[0].Columns[0].ColumnName = "doc_no";
        DSTreasuryCertificate.Tables[0].Columns[0].ColumnName = "issue_date";
        DSTreasuryCertificate.Tables[0].Columns[1].ColumnName = "issue_num";
        DSTreasuryCertificate.Tables[0].Columns[2].ColumnName = "amt_issued";
        DSTreasuryCertificate.Tables[0].Columns[3].ColumnName = "tend_code";
        DSTreasuryCertificate.Tables[0].Columns[4].ColumnName = "tbschid";
        DSTreasuryCertificate.Tables[0].Columns[5].ColumnName = "tbschname";
        //DSTreasuryCertificate.Tables[0].Columns[4].ColumnName = "line_no";
      }
      return DSTreasuryCertificate;
    }
    public static DataSet GET_Revenue_Receipts(int doc_no)
    {
      object[] parameter = new object[1];
      parameter[0] = doc_no;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOARCashProcessingStrcashe), (new DVOARCashProcessingStrcashe()).GET_REV_RECPT);
      if (ds.Tables.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "v_keyvalue";
        ds.Tables[0].Columns[1].ColumnName = "v_acct_desc";
        ds.Tables[0].Columns[2].ColumnName = "v_chkeyvalue";
        ds.Tables[0].Columns[3].ColumnName = "v_chacct_desc";
        ds.Tables[0].Columns[4].ColumnName = "v_dist_amt";
        ds.Tables[0].Columns[5].ColumnName = "v_dist_deb_cred";
        ds.Tables[0].Columns[6].ColumnName = "v_cash_amt";
        ds.Tables[0].Columns[7].ColumnName = "v_check_no";
        ds.Tables[0].Columns[8].ColumnName = "v_doc_desc";
        ds.Tables[0].Columns[9].ColumnName = "v_doc_no";
        ds.Tables[0].Columns[10].ColumnName = "v_rcpt_date";
        ds.Tables[0].Columns[11].ColumnName = "v_createdby";
      }
      return ds;

    }

    public static DataSet GetPayrollAnalysis(ref DVOPrintSummaryAnalysis ObjDvoSummaryAnalysis)
    {

      object[] parameter = new object[12];
      parameter[0] = ObjDvoSummaryAnalysis.startdate;
      parameter[1] = ObjDvoSummaryAnalysis.Enddate;
      parameter[2] = ObjDvoSummaryAnalysis.act_code;
      parameter[3] = ObjDvoSummaryAnalysis.act_type;
      parameter[4] = ObjDvoSummaryAnalysis.empl_code;
      parameter[5] = ObjDvoSummaryAnalysis.last_name;
      parameter[6] = ObjDvoSummaryAnalysis.first_name;
      parameter[7] = ObjDvoSummaryAnalysis.type_code;
      parameter[8] = ObjDvoSummaryAnalysis.job_code;
      parameter[9] = ObjDvoSummaryAnalysis.job_title;
      parameter[10] = ObjDvoSummaryAnalysis.pay_period;
      parameter[11] = ObjDvoSummaryAnalysis.emp_status;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSSummaryAnalysisByEmpCode = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintSummaryAnalysis));

      if (DSSummaryAnalysisByEmpCode.Tables.Count > 0)
      {
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[0].ColumnName = "ref_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[1].ColumnName = "act_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[2].ColumnName = "act_type";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[3].ColumnName = "amount";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[4].ColumnName = "doc_no";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[5].ColumnName = "first_name";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns[6].ColumnName = "last_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[7].ColumnName = "middle_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[8].ColumnName = "pay_date";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("act_type_description");
        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("EmpYTD");

        DSSummaryAnalysisByEmpCode.Tables[0].DefaultView.Sort = "act_type, act_code, ref_code, pay_date";

        decimal _EmpYTD = 0;
        string _Act_type_desc = string.Empty;

        for (int i = 0; i < DSSummaryAnalysisByEmpCode.Tables[0].Rows.Count; i++)
        //foreach (DataRow dr in DSSummaryAnalysisByEmpCode.Tables[0].Rows)
        {
          DataRow dr = DSSummaryAnalysisByEmpCode.Tables[0].Rows[i];
          if (i == 0 || (dr["ref_code"].ToString().Trim() != DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim())
              || (dr["ref_code"].ToString().Trim() == DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() != DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["act_type"].ToString().Trim())
              || (dr["ref_code"].ToString().Trim() == DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() == DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["act_type"].ToString().Trim() && dr["act_code"].ToString().Trim() != DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["act_code"].ToString().Trim())
              )
          {
            _Act_type_desc = string.Empty;
            _EmpYTD = 0;
            DVOPrintSummaryAnalysis obj = new DVOPrintSummaryAnalysis();

            obj.ref_code = dr[0].ToString();
            obj.act_code = dr[1].ToString();
            obj.act_type = dr[2].ToString();
            obj.doc_no = Convert.ToInt32(dr[4]);
            obj.pay_period = dr[8].ToString();

            object[] Parameterss = new object[5];
            Parameterss[0] = obj.ref_code;
            Parameterss[1] = obj.act_code;
            Parameterss[2] = obj.act_type;
            Parameterss[3] = obj.doc_no;
            Parameterss[4] = Convert.ToDateTime(obj.pay_period.ToString());

            using (DataSet DSSummaryAnalysisByEmpCodeDtl = objDalBaseClass.GetData(ref Parameterss, typeof(DVOPrintSummaryAnalysis), obj.GET_DESCRIPTION))
            {
              if (DSSummaryAnalysisByEmpCodeDtl != null)
                if (DSSummaryAnalysisByEmpCodeDtl.Tables.Count > 0)
                  if (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows.Count > 0)
                  {
                    _Act_type_desc = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0] != DBNull.Value) ? DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;
                    _EmpYTD = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToDecimal(DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1]) : 0;
                  }
            }
            dr["act_type_description"] = _Act_type_desc;
            dr["EmpYTD"] = _EmpYTD;
          }
          else
          {
            DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["EmpYTD"] = 0;
            dr["EmpYTD"] = _EmpYTD;
          }
        }
      }
      return DSSummaryAnalysisByEmpCode;
    }

    public static DataSet GetSummaryAnalysis(ref DVOPrintSummaryAnalysis ObjDvoSummaryAnalysis)
    {
      DataTable objDataTable = new DataTable();
      object[] parameter = new object[12];
      parameter[0] = ObjDvoSummaryAnalysis.startdate;
      parameter[1] = ObjDvoSummaryAnalysis.Enddate;
      parameter[2] = ObjDvoSummaryAnalysis.act_code;
      parameter[3] = ObjDvoSummaryAnalysis.act_type;
      parameter[4] = ObjDvoSummaryAnalysis.empl_code;
      parameter[5] = ObjDvoSummaryAnalysis.last_name;
      parameter[6] = ObjDvoSummaryAnalysis.first_name;
      parameter[7] = ObjDvoSummaryAnalysis.type_code;
      parameter[8] = ObjDvoSummaryAnalysis.job_code;
      parameter[9] = ObjDvoSummaryAnalysis.job_title;
      parameter[10] = ObjDvoSummaryAnalysis.pay_period;
      parameter[11] = ObjDvoSummaryAnalysis.emp_status;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSSummaryAnalysisByEmpCode = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintSummaryAnalysis));

      if (DSSummaryAnalysisByEmpCode.Tables.Count > 0)
      {
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[0].ColumnName = "ref_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[1].ColumnName = "act_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[2].ColumnName = "act_type";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[3].ColumnName = "amount";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[4].ColumnName = "doc_no";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[5].ColumnName = "first_name";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns[6].ColumnName = "last_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[7].ColumnName = "middle_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[8].ColumnName = "pay_date";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("act_type_description");
        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("EmpYTD");
        if (DSSummaryAnalysisByEmpCode.Tables[0].Rows.Count <= 0)
        {
          return DSSummaryAnalysisByEmpCode;
        }
        try
        {
          //DSSummaryAnalysisByEmpCode.Tables[0].DefaultView.Sort = "act_type, act_code, ref_code, pay_date";
          DataRow[] dra = DSSummaryAnalysisByEmpCode.Tables[0].Select("", "ref_code,act_type,act_code");
          decimal _EmpYTD = 0;
          object[] paramet = new object[2];
          paramet[0] = ObjDvoSummaryAnalysis.startdate;
          paramet[1] = ObjDvoSummaryAnalysis.empl_code;
          DataSet DsYtoD = objDalBaseClass.GetData(ref paramet, typeof(DVOPrintSummaryAnalysis), ObjDvoSummaryAnalysis.GET_YtoD);
          DsYtoD.Tables[0].Columns[0].ColumnName = "amount";
          DsYtoD.Tables[0].Columns[1].ColumnName = "ref_code";
          DsYtoD.Tables[0].Columns[2].ColumnName = "act_code";
          DsYtoD.Tables[0].Columns[3].ColumnName = "act_type";
          DsYtoD.Tables[0].Columns[4].ColumnName = "pay_date";
          DataSet dsInc = objDalBaseClass.GetData((new DVOUpdateIncCode()).FIND_DESC());
          DataSet dsDed = objDalBaseClass.GetData((new DVOPRDeductionCodesMasterDedcodes()).FIND_DESC());
          DataSet dsObl = objDalBaseClass.GetData((new DVOMasterOblCodes()).FIND_DESC());
          string actType = string.Empty;
          string actCode = string.Empty;
          string empl_code = string.Empty;
          DataRow[] draInc = null;
          DataRow[] draDed = null;
          DataRow[] draObl = null;
          //string curCode=string.Empty;
          //string pevCode = string.Empty;
          //List<string> strList = new List<string>();

          for (int i = 0; i < dra.Length; i++)
          //foreach (DataRow dr in DSSummaryAnalysisByEmpCode.Tables[0].Rows)
          {
            DataRow dr = dra[i];
            actType = dr["act_type"].ToString().Trim();
            actCode = dr["act_code"].ToString().Trim();
            empl_code = dr["ref_code"].ToString().Trim();

            //curCode = actType + actCode + empl_code;
            //if (i == 0 || (!strList.Contains(curCode)))
            if (i == 0 || (dr["ref_code"].ToString().Trim() != dra[i - 1]["ref_code"].ToString().Trim())
                || (dr["ref_code"].ToString().Trim() == dra[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() != dra[i - 1]["act_type"].ToString().Trim())
                || (dr["ref_code"].ToString().Trim() == dra[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() == dra[i - 1]["act_type"].ToString().Trim() && dr["act_code"].ToString().Trim() != dra[i - 1]["act_code"].ToString().Trim())
                )
            {

              //strList.Add(curCode);
              _EmpYTD = 0;
              decimal locAmount = 0;
              DataRow[] draYD = null;
              if (ObjDvoSummaryAnalysis.startdate.Trim().Length > 0)
              {
                int year = dr["pay_date"] != DBNull.Value ? Convert.ToDateTime(dr["pay_date"]).Year : 0;
                draYD = DsYtoD.Tables[0].Select("ref_code='" + empl_code + "'" + " and act_code='" + actCode + "'" + " and act_type='" + actType + "'" + " and pay_date=" + year);
              }
              else
              {
                draYD = DsYtoD.Tables[0].Select("ref_code='" + empl_code + "'" + " and act_code='" + actCode + "'" + " and act_type='" + actType + "'");
              }
              if (draYD.Length > 0)
              {
                foreach (DataRow drYD in draYD)
                {
                  locAmount += drYD["amount"] != DBNull.Value ? Convert.ToDecimal(drYD["amount"]) : 0;
                }
              }
              _EmpYTD = locAmount;
              dr["EmpYTD"] = _EmpYTD;

            }
            else
            {
              dra[i - 1]["EmpYTD"] = 0;
              dr["EmpYTD"] = _EmpYTD;
            }

            if (actType == "B")
            {
              draInc = dsInc.Tables[0].Select("inc_code ='" + actCode + "'");
              if (draInc.Length > 0)
                dr["Act_type_description"] = draInc[0]["description"];
            }
            else if (actType == "C")
            {
              draDed = dsDed.Tables[0].Select("ded_code ='" + actCode + "'");
              if (draDed.Length > 0)
                dr["Act_type_description"] = draDed[0]["description"];
            }
            else if (actType == "D")
            {
              draObl = dsObl.Tables[0].Select("obl_code ='" + actCode + "'");
              if (draObl.Length > 0)
                dr["Act_type_description"] = draObl[0]["description"];
            }
            actType = string.Empty;
            actCode = string.Empty;
            draInc = null;
            draDed = null;
            draObl = null;
            //strList = null;
          }
          //DSSummaryAnalysisByEmpCode.Tables[0].Rows.Clear();
          //DSSummaryAnalysisByEmpCode.Merge(dra);
        }
        catch (Exception ex)
        {
          ExceptionManagement.ExceptionManager.Publish(ex);
          throw ex;
        }
      }
      return DSSummaryAnalysisByEmpCode;
    }


    public static DataTable GetDetailAnalysis(ref DVOPrintSummaryAnalysis objSearch)
    {
      DataTable objDataTable = new DataTable();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameter = new object[12];
        parameter[0] = objSearch.startdate;
        parameter[1] = objSearch.Enddate;
        parameter[2] = objSearch.act_code;
        parameter[3] = objSearch.act_type;
        parameter[4] = objSearch.empl_code;
        parameter[5] = objSearch.last_name;
        parameter[6] = objSearch.first_name;
        parameter[7] = objSearch.type_code;
        parameter[8] = objSearch.job_code;
        parameter[9] = objSearch.job_title;
        parameter[10] = objSearch.pay_period;
        parameter[11] = objSearch.emp_status;
        //DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintSummaryAnalysis));
        IDataReader DRD = objDalBaseClass.GetDataByReader(ref parameter, typeof(DVOPrintSummaryAnalysis));
        objDataTable.Columns.Add("ref_code");
        objDataTable.Columns.Add("act_code");
        objDataTable.Columns.Add("act_type");
        objDataTable.Columns.Add("amount", typeof(decimal));
        objDataTable.Columns.Add("doc_no", typeof(Int32));
        objDataTable.Columns.Add("first_name");
        objDataTable.Columns.Add("last_name");
        objDataTable.Columns.Add("middle_name");
        objDataTable.Columns.Add("pay_date", typeof(DateTime));
        objDataTable.Columns.Add("Act_type_description");
        objDataTable.Columns.Add("EmpYTD");

        //ds.Tables[0].DefaultView.Sort = "act_type, act_code, last_name, first_name, ref_code, pay_date";
        DataSet dsInc = objDalBaseClass.GetData((new DVOUpdateIncCode()).FIND_DESC());
        DataSet dsDed = objDalBaseClass.GetData((new DVOPRDeductionCodesMasterDedcodes()).FIND_DESC());
        DataSet dsObl = objDalBaseClass.GetData((new DVOMasterOblCodes()).FIND_DESC());
        //decimal _EmpYTD = 0;
        //string _Act_type_desc = string.Empty;
        string actType = string.Empty;
        string actCode = string.Empty;
        DataRow[] draInc = null;
        DataRow[] draDed = null;
        DataRow[] draObl = null;

        DataRow dr = objDataTable.NewRow();
        while (DRD.Read())
        {

          actType = Convert.ToString(DRD[2]).Trim();
          actCode = Convert.ToString(DRD[1]).Trim();
          if (actType == "B")
          {
            draInc = dsInc.Tables[0].Select("inc_code ='" + actCode + "'");
            if (draInc.Length > 0)
              dr["Act_type_description"] = draInc[0]["description"];
          }
          else if (actType == "C")
          {
            draDed = dsDed.Tables[0].Select("ded_code ='" + actCode + "'");
            if (draDed.Length > 0)
              dr["Act_type_description"] = draDed[0]["description"];
          }
          else if (actType == "D")
          {
            draObl = dsObl.Tables[0].Select("obl_code ='" + actCode + "'");
            if (draObl.Length > 0)
              dr["Act_type_description"] = draObl[0]["description"];
          }
          actType = string.Empty;
          actCode = string.Empty;
          draInc = null;
          draDed = null;
          draObl = null;
          dr[0] = DRD[0];
          dr[1] = DRD[1];
          dr[2] = DRD[2];
          dr[3] = DRD[3];
          dr[4] = DRD[4];
          dr[5] = DRD[5];
          dr[6] = DRD[6];
          dr[7] = DRD[7];
          dr[8] = DRD[8];
          objDataTable.Rows.Add(dr.ItemArray);


          //if (i == 0 || (dr["ref_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim())
          //    || (dr["ref_code"].ToString().Trim() == ds.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["act_type"].ToString().Trim())
          //    || (dr["ref_code"].ToString().Trim() == ds.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim() && dr["act_type"].ToString().Trim() == ds.Tables[0].Rows[i - 1]["act_type"].ToString().Trim() && dr["act_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["act_code"].ToString().Trim())
          //    )
          //{
          //    _Act_type_desc = string.Empty;
          //    _EmpYTD = 0;
          //    DVOPrintSummaryAnalysis obj = new DVOPrintSummaryAnalysis();

          //    obj.ref_code = dr[0].ToString();
          //    obj.act_code = dr[1].ToString();
          //    obj.act_type = dr[2].ToString();
          //    obj.doc_no = Convert.ToInt32(dr[4]);
          //    obj.pay_period = dr[8].ToString();

          //    object[] Parameterss = new object[5];
          //    Parameterss[0] = obj.ref_code;
          //    Parameterss[1] = obj.act_code;
          //    Parameterss[2] = obj.act_type;
          //    Parameterss[3] = obj.doc_no;
          //    Parameterss[4] = Convert.ToDateTime(obj.pay_period.ToString());

          //    using (DataSet DSSummaryAnalysisByEmpCodeDtl = objDalBaseClass.GetData(ref Parameterss, typeof(DVOPrintSummaryAnalysis), obj.GET_DESCRIPTION))
          //    {
          //        if (DSSummaryAnalysisByEmpCodeDtl != null)
          //            if (DSSummaryAnalysisByEmpCodeDtl.Tables.Count > 0)
          //                if (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows.Count > 0)
          //                {
          //                    _Act_type_desc = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0] != DBNull.Value) ? DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;
          //                    _EmpYTD = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToDecimal(DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1]) : 0;
          //                }
          //    }
          //    dr["act_type_description"] = _Act_type_desc;
          //    dr["EmpYTD"] = _EmpYTD;
          //}
          //else
          //{
          //    ds.Tables[0].Rows[i - 1]["EmpYTD"] = 0;
          //    dr["EmpYTD"] = _EmpYTD;
          //}
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return objDataTable;
    }



    public static DataSet GetEmployeeInfo(ref DVOPrintSummaryAnalysis ObjDvoDetailAnalysis)
    {

      object[] parameter = new object[12];
      parameter[0] = ObjDvoDetailAnalysis.startdate;
      parameter[1] = ObjDvoDetailAnalysis.Enddate;
      parameter[2] = ObjDvoDetailAnalysis.act_code;
      parameter[3] = ObjDvoDetailAnalysis.act_type;
      parameter[4] = ObjDvoDetailAnalysis.empl_code;
      parameter[5] = ObjDvoDetailAnalysis.last_name;
      parameter[6] = ObjDvoDetailAnalysis.first_name;
      parameter[7] = ObjDvoDetailAnalysis.type_code;
      parameter[8] = ObjDvoDetailAnalysis.job_code;
      parameter[9] = ObjDvoDetailAnalysis.job_title;
      parameter[10] = ObjDvoDetailAnalysis.pay_period;
      parameter[11] = ObjDvoDetailAnalysis.emp_status;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSSummaryAnalysisByEmpCode = objDalBaseClass.GetData(ref parameter, typeof(DVOPrintSummaryAnalysis));

      if (DSSummaryAnalysisByEmpCode.Tables.Count > 0)
      {
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[0].ColumnName = "ref_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[1].ColumnName = "act_code";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[2].ColumnName = "act_type";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[3].ColumnName = "amount";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[4].ColumnName = "doc_no";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[5].ColumnName = "first_name";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns[6].ColumnName = "last_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[7].ColumnName = "middle_name";
        DSSummaryAnalysisByEmpCode.Tables[0].Columns[8].ColumnName = "pay_date";

        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("act_type_description");
        DSSummaryAnalysisByEmpCode.Tables[0].Columns.Add("EmpYTD");

        DSSummaryAnalysisByEmpCode.Tables[0].DefaultView.Sort = "act_type, act_code, last_name, first_name, ref_code, pay_date";
        //List<DVOPrintSummaryAnalysis> list = new List<DVOPrintSummaryAnalysis>();
        //decimal _EmpYTD = 0;
        //string _Act_type_desc = string.Empty;

        //for (int i = 0; i < DSSummaryAnalysisByEmpCode.Tables[0].Rows.Count; i++)
        ////foreach (DataRow dr in DSSummaryAnalysisByEmpCode.Tables[0].Rows)
        //{
        //    DataRow dr = DSSummaryAnalysisByEmpCode.Tables[0].Rows[i];
        //    if (i == 0 || dr["ref_code"].ToString().Trim() != DSSummaryAnalysisByEmpCode.Tables[0].Rows[i - 1]["ref_code"].ToString().Trim())
        //    {
        //        DVOPrintSummaryAnalysis obj = new DVOPrintSummaryAnalysis();

        //        obj.ref_code = dr[0].ToString();
        //        obj.act_code = dr[1].ToString();
        //        obj.act_type = dr[2].ToString();
        //        obj.doc_no = Convert.ToInt32(dr[4]);
        //        obj.pay_period = dr[8].ToString();

        //        object[] Parameterss = new object[5];
        //        Parameterss[0] = obj.ref_code;
        //        Parameterss[1] = obj.act_code;
        //        Parameterss[2] = obj.act_type;
        //        Parameterss[3] = obj.doc_no;
        //        Parameterss[4] = Convert.ToDateTime(obj.pay_period.ToString());

        //        using (DataSet DSSummaryAnalysisByEmpCodeDtl = objDalBaseClass.GetData(ref Parameterss, typeof(DVOPrintSummaryAnalysis), obj.GET_DESCRIPTION))
        //        {
        //            if (DSSummaryAnalysisByEmpCodeDtl != null)
        //                if (DSSummaryAnalysisByEmpCodeDtl.Tables.Count > 0)
        //                    if (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows.Count > 0)
        //                    {
        //                        _Act_type_desc = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0] != DBNull.Value) ? DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;
        //                        _EmpYTD = (DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToDecimal(DSSummaryAnalysisByEmpCodeDtl.Tables[0].Rows[0][1]) : 0;
        //                    }
        //        }
        //    }
        //    dr["act_type_description"] = _Act_type_desc;
        //    dr["EmpYTD"] = _EmpYTD;
        //}
      }
      return DSSummaryAnalysisByEmpCode;
    }
    public static DataTable GetDirectDepositeInfo(ref DvoDirectDepositEntries ObjDvoDirectDepositEntries, ref DVOPYBatchProcessStybatchr pObjBatch)
    {

      /*
      Added by Sarvjeet on 22/10/2010
      To implemented Payroll batch process into Create Direct Deposit Enties. Nedd to add
      A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'GetDirectDepositeInfo' function
      and remove comment from the code written for batch process logic.   

     */
      DataSet DsDirectDepositEntries = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataView dv = null;
      DataTable dttemp = new DataTable();
      //Added by Sarvjeet on 22/01/2010..
      #region Declare Variables for Batch Process
      StringBuilder errorMassage = new StringBuilder();
      DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      int recordsSearched = 0;
      int recordsProcessed = 0;
      bool IsProessIns = false;
      #endregion
      try
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Insert Process Start Info..
        object objTrx = null;
        objProcessDtl.pybatchid = pObjBatch.pybatchid;
        objProcessDtl.processname = "Create Direct Deposit Enties";
        objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
        BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
        IsProessIns = true;
        #endregion
        object[] Parameter = new object[2];
        Parameter[0] = ObjDvoDirectDepositEntries.type_code;
        Parameter[1] = ObjDvoDirectDepositEntries.District;

        DsDirectDepositEntries = objDalBaseClass.GetData(ref Parameter, typeof(DvoDirectDepositEntries));


        if (DsDirectDepositEntries.Tables[0].Rows.Count >= 1)
        {
          DsDirectDepositEntries.Tables[0].Columns[0].ColumnName = "amount";
          DsDirectDepositEntries.Tables[0].Columns[1].ColumnName = "bank_acct_no";
          DsDirectDepositEntries.Tables[0].Columns[2].ColumnName = "bank_code";
          DsDirectDepositEntries.Tables[0].Columns[3].ColumnName = "line_no";
          DsDirectDepositEntries.Tables[0].Columns[4].ColumnName = "type";
          DsDirectDepositEntries.Tables[0].Columns[5].ColumnName = "acct_desc";
          DsDirectDepositEntries.Tables[0].Columns[6].ColumnName = "keyvalue";
          DsDirectDepositEntries.Tables[0].Columns[7].ColumnName = "chk_digit";
          DsDirectDepositEntries.Tables[0].Columns[8].ColumnName = "dfi_digit";
          DsDirectDepositEntries.Tables[0].Columns[9].ColumnName = "empl_code";
          DsDirectDepositEntries.Tables[0].Columns[10].ColumnName = "cash_acct_no";
          DsDirectDepositEntries.Tables[0].Columns[11].ColumnName = "cash_amount";
          DsDirectDepositEntries.Tables[0].Columns[12].ColumnName = "check_no";
          DsDirectDepositEntries.Tables[0].Columns[13].ColumnName = "department";
          DsDirectDepositEntries.Tables[0].Columns[14].ColumnName = "doc_no";
          DsDirectDepositEntries.Tables[0].Columns[15].ColumnName = "pay_date";
          DsDirectDepositEntries.Tables[0].Columns[16].ColumnName = "last_name";
          DsDirectDepositEntries.Tables[0].Columns[17].ColumnName = "first_name";
          DsDirectDepositEntries.Tables[0].Columns[18].ColumnName = "middle_name";
          DsDirectDepositEntries.Tables[0].Columns[19].ColumnName = "typeofacct";
          DsDirectDepositEntries.Tables[0].Columns[20].ColumnName = "ApplicationReferenceNo";
          DsDirectDepositEntries.Tables[0].Columns[21].ColumnName = "Applicant_Bank_Ifsc_code";
          DsDirectDepositEntries.Tables[0].Columns.Add("BatchNumber", typeof(int));
          DsDirectDepositEntries.Tables[0].Columns.Add("DepositAmt", typeof(Decimal));


          dv = DsDirectDepositEntries.Tables[0].DefaultView;
          dv.Sort = "cash_acct_no,empl_code,type";
          recordsSearched = DsDirectDepositEntries.Tables[0].Rows.Count;
          Decimal cashAmount = 0;
          Decimal CashAmountRemaining = 0;
          string prev_check = "N";
          int batchNo = 0;
          int svc_class = 0;
          DataTable dt_stxcntrc = Get_stxcntrc();
          string comp_name = Convert.ToString(dt_stxcntrc.Rows[0][0]);
          DataTable dt_stycntrc = Get_Stycntrc();

          int dfi_immed = (dt_stycntrc.Rows[0][0] != DBNull.Value ? Convert.ToInt32(dt_stycntrc.Rows[0][0]) : 0);
          string offset_debit = (dt_stycntrc.Rows[0][4] != DBNull.Value ? dt_stycntrc.Rows[0][4].ToString().Trim() : string.Empty);
          if (offset_debit == "Y")
          {
            svc_class = 200;
          }
          else
          {
            svc_class = 220;
          }

          DataTable dt_stypddre = Get_stypddre();

          batchNo = Convert.ToInt32(dt_stypddre.Rows[0][0] != DBNull.Value ? Convert.ToInt32(dt_stypddre.Rows[0][0]) : 0);

          if (batchNo == 0)
          {
            batchNo = 1;
          }
          else
          {
            batchNo = batchNo + 1;
          }

          int NewDocNo = 0;
          string batch_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); //BLLCommonUtilities.GetServerDate();
                                                                                                                                                                 //string batch_date = Convert.ToDateTime(batch_date).ToString("MM/dd/yyyy");
          string used = "N";
          int doc_Next = 0;
          //***********Modified by Sarvjeet Verma***************
          // find all of the unique bank_codes and create the stypddre entries.
          Hashtable bank_doc = new Hashtable();
          //for (int i = 0; i < dv.Table.Rows.Count; i++)
          //foreach (DataRow dr in DsDirectDepositEntries.Tables[0].Rows)
          dttemp = dv.ToTable();
          for (int i = 0; i < dttemp.Rows.Count; i++)
          {
            DataRow dr = dttemp.Rows[i];
            decimal amount = dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0;
            string type = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
            decimal cash_amount = dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0;
            string empl_code = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;
            int empl_doc_no = dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0;
            string empl_previous;
            string typeofacct = dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty;
            dr["BatchNumber"] = batchNo;

            if (i == 0)
              empl_previous = "";
            else
              //to get the previous index values of the dataset row
              empl_previous = dttemp.Rows[i - 1]["empl_code"] != DBNull.Value ? dttemp.Rows[i - 1]["empl_code"].ToString().Trim() : string.Empty;

            if (i < DsDirectDepositEntries.Tables[0].Rows.Count - 1)
              //to get the next value of the dataset row 
              doc_Next = dttemp.Rows[i + 1]["doc_no"] != DBNull.Value ? Convert.ToInt32(dttemp.Rows[i + 1]["doc_no"]) : 0;
            else
              doc_Next = 0;


            string LastName = dr["last_name"] != DBNull.Value ? Convert.ToString(dr["last_name"]).Trim() : string.Empty;
            if (LastName.Trim() != string.Empty)
              LastName = LastName + ", ";
            string FirstName = dr["first_name"] != DBNull.Value ? Convert.ToString(dr["first_name"]).Trim() : string.Empty;

            string MiddleName = dr["middle_name"] != DBNull.Value ? Convert.ToString(dr["middle_name"]).Trim() : string.Empty;
            if (MiddleName.Trim() != string.Empty)
              MiddleName = ", " + MiddleName;
            string EmpName = LastName + FirstName + MiddleName;
            dr["last_name"] = LastName;
            dr["first_name"] = FirstName;
            dr["middle_name"] = MiddleName;
            if (EmpName.Trim().Length > 22)
              EmpName = EmpName.Substring(0, 21);
            string BankAccountNo = dr[1].ToString().Trim();
            string bank_code = dr[2].ToString().Trim();
            int chk_digit = 0;
            if (!Convert.IsDBNull(dr["chk_digit"])) chk_digit = Convert.ToInt32(dr[7]);
            int dfi_dest = 0;
            if (!Convert.IsDBNull(dr["dfi_digit"])) dfi_dest = Convert.ToInt32(dr[8]);
            int doc_no = 0;
            if (!Convert.IsDBNull(dr["doc_no"])) doc_no = Convert.ToInt32(dr[14]);

            string pay_date = dr[15] != DBNull.Value ? (Convert.ToDateTime(dr[15]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture)) : string.Empty;
            UpdatePreviousCashAmounts(empl_code, empl_previous, ref cashAmount, ref CashAmountRemaining, cash_amount);
            bool IsUnique = true;
            //***********Modified by Sarvjeet Verma***************
            // find all of the unique bank_codes and create the stypddre entries.
            foreach (string str in bank_doc.Keys)
            {
              if (str == bank_code)
              {
                IsUnique = false;
                break;
              }
            }
            if (IsUnique)
            {
              if (!InsertDirectDepositHeader(ref objTransaction, empl_code, batchNo, svc_class, comp_name, dfi_immed, bank_code, batch_date, used, out NewDocNo))
              {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                dttemp.Rows.Clear();
                DsDirectDepositEntries.Tables[0].Rows.Clear();
                Exception ex = new Exception("Cannot run Direct Deposite Entries,An SQL Error has occurred");
                throw ex;
              }
              bank_doc.Add(bank_code, NewDocNo);
            }
            else
            {
              NewDocNo = Convert.ToInt32(bank_doc[bank_code]);
            }
            decimal Deposit_Amount = 0.0M;
            if (!InsertDirectDepositDetail(ref objTransaction, pay_date, amount, dfi_immed, NewDocNo, prev_check, type, cash_amount, cashAmount, ref CashAmountRemaining, empl_code, EmpName, BankAccountNo, chk_digit, dfi_dest, doc_no, typeofacct, out Deposit_Amount))
            {
              objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
              dttemp.Rows.Clear();
              DsDirectDepositEntries.Tables[0].Rows.Clear();
              Exception ex = new Exception("Cannot run Direct Deposite Entries,An SQL Error has occurred");
              throw ex;
            }
            dr["DepositAmt"] = Deposit_Amount;
            if (!UpdatePayrollDepositStatus(ref objTransaction, empl_code, empl_doc_no, doc_Next))
            {
              objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
              dttemp.Rows.Clear();
              DsDirectDepositEntries.Tables[0].Rows.Clear();
              Exception ex = new Exception("Cannot run Direct Deposite Entries,An SQL Error has occurred");
              throw ex;
            }

          }
          if (objTransaction != null)
            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
          recordsProcessed = dttemp.Rows.Count;
        }
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        errorMassage.Append("[" + ex.Message + "]");
        ExceptionManager.Publish(ex);
        throw ex;
      }
      finally
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Record process detail..
        if (IsProessIns)
        {
          List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
          object objTrx = null;
          objProcessDtl.recordssearched = recordsSearched;
          objProcessDtl.recordsprocessed = recordsProcessed;
          objProcessDtl.status = 1;
          objProcessDtl.errormessage = errorMassage.ToString();
          objList.Add(objProcessDtl);
          BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
        }
        else
        {
          object objTrx = null;
          List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
          DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
          obj.pybatchid = pObjBatch.pybatchid;
          obj.processname = "Create Direct Deposit Enties";
          //obj.processstartedon = pObjBatch.startedon;
          //obj.processendedon = pObjBatch.endedon;
          obj.recordssearched = recordsSearched;
          obj.recordsprocessed = recordsProcessed;
          obj.status = 1;
          obj.searchcriteria = pObjBatch.searchcriteria;
          obj.errormessage = errorMassage.ToString();
          objList.Add(obj);
          BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
        }
        #endregion
      }

      //dttemp.WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DSDirectDepositEntries.xsd");
      //dttemp.WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DataTable1.xsd");

      return dttemp;
    }

    private static void UpdatePreviousCashAmounts(string empl_code, string empl_previous, ref decimal cashAmount, ref decimal CashAmountRemaining, decimal cash_amount)
    {
      if (empl_code != empl_previous || (empl_code == null && empl_previous != null) || (empl_code != null && empl_previous == null))
      {
        cashAmount = cash_amount;
        CashAmountRemaining = cashAmount;
      }
    }

        private static bool InsertDirectDepositDetail(ref object objTransaction, string pay_date, decimal amount, int dfi_immed, int NewDocNo, string prev_check, string type, decimal cash_amount, decimal cashAmount, ref decimal CashAmountRemaining, string empl_code, string EmpName, string BankAccountNo, int chk_digit, int dfi_dest, int doc_no, string typeofacct, out decimal Deposit_Amount)
        {
            Deposit_Amount = 0.0M;
            try
            {
                decimal trace_number = 0.0M;

                if (prev_check == "Y")
                {
                    trace_number = trace_number + 1;
                }
                else
                {
                    prev_check = "Y";
                    DataTable dt_stypddrd = Get_stypddrd();
                    if (dt_stypddrd.Rows.Count > 0)
                        trace_number = dt_stypddrd.Rows[0][0] != DBNull.Value ? Convert.ToDecimal(dt_stypddrd.Rows[0][0]) : 0;
                    if (trace_number == 0)
                    {
                        trace_number = (dfi_immed * 10000000);
                    }
                    trace_number = trace_number + 1;
                }

                if (type.Trim() == "A")
                {
                    Deposit_Amount = amount;
                }
                else if (type.Trim() == "P")
                {
                    Deposit_Amount = cashAmount * (amount / 100);
                }

                else if (type.Trim() == "R")
                {
                    Deposit_Amount = CashAmountRemaining;
                }

                if (Deposit_Amount > CashAmountRemaining)
                {
                    Deposit_Amount = CashAmountRemaining;
                }

                CashAmountRemaining = CashAmountRemaining - Deposit_Amount;

                int trans_code = 0;
                if (cash_amount == 0)
                {
                    trans_code = 23;
                }
                else
                {
                    trans_code = 22;
                }

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                DvoDirectDepositEntries objDvoDir = new DvoDirectDepositEntries();

                object[] insParameter = new object[12];
                insParameter[0] = NewDocNo;
                insParameter[1] = empl_code;
                insParameter[2] = dfi_dest;
                insParameter[3] = chk_digit;
                insParameter[4] = pay_date;
                insParameter[5] = trans_code;
                insParameter[6] = BankAccountNo;
                insParameter[7] = Deposit_Amount;
                insParameter[8] = EmpName.Trim();
                insParameter[9] = trace_number;
                insParameter[10] = doc_no;
                insParameter[11] = typeofacct;

                object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref insParameter, typeof(DvoDirectDepositEntries), objDvoDir.INSERT_STYPDDRD);
                if (obj == null || obj.ToString().Trim() == string.Empty || Convert.ToInt32(obj) != 1)
                    return false;


            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);

                return false;
            }
            return true;

        }

    private static DataTable Get_stypddrd()
    {
      DvoDirectDepositEntries obj = new DvoDirectDepositEntries();
      object[] Parameter = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DvoDirectDepositEntries), obj.GET_TRACE_NUMBER);
      return ds.Tables[0];
    }

    private static bool InsertDirectDepositHeader(ref object objTransaction, string empl_code, int batchNo, int svc_class, string comp_name, int dfi_immed, string bank_code, string batch_date, string used, out int NewDocNo)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      NewDocNo = 0;
      bool transObjectStatus = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        transObjectStatus = false;
      }
      try
      {
        object[] insParameter = new object[7];
        insParameter[0] = comp_name;
        insParameter[1] = dfi_immed;
        insParameter[2] = svc_class;
        insParameter[3] = batchNo;
        insParameter[4] = batch_date;
        insParameter[5] = used;
        insParameter[6] = bank_code;


        DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref insParameter, typeof(DvoDirectDepositEntries));
        if (ds.Tables.Count > 0)
        {
          if (ds.Tables[0].Rows.Count > 0)
          {
            NewDocNo = ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
            return true;
          }
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return false;
    }

    private static bool UpdatePayrollDepositStatus(ref object objTransaction, string empl_code, int NewDocNo, int doc_Next)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      if (NewDocNo != doc_Next || (NewDocNo == 0 && doc_Next != 0) || (NewDocNo != 0 && doc_Next == 0))
      {
        try
        {
          object[] UpdParameter = new object[2];
          UpdParameter[0] = NewDocNo;
          UpdParameter[1] = empl_code;
          object c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameter, typeof(DvoDirectDepositEntries), true);
          if (Convert.ToInt32(c) == 1)
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (Exception ex)
        {
          throw ex;
          return false;
        }

      }
      return true;
    }

    private static DataTable Get_stxcntrc()
    {
      DvoDirectDepositEntries obj = new DvoDirectDepositEntries();
      object[] Parameter = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DvoDirectDepositEntries), obj.GET_COMP_NAME);
      return ds.Tables[0];
    }

    private static DataTable Get_stypddre()
    {
      DvoDirectDepositEntries obj = new DvoDirectDepositEntries();
      object[] Parameter = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DvoDirectDepositEntries), obj.GET_INFO_FROM_STYPDDREGET);
      return ds.Tables[0];
    }

    private static DataTable Get_Stycntrc()
    {
      DvoDirectDepositEntries obj = new DvoDirectDepositEntries();
      object[] Parameter = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DvoDirectDepositEntries), obj.GET_INFO_FROM_PayControl);
      return ds.Tables[0];
    }

    public static DataSet GET_UnpostedJournalGeneralAdjustments(ref DVOPrintVoteBook objsearch)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objsearch.Keyvalue.Trim();
      Parameter[1] = objsearch.AccountType.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOPrintVoteBook), objsearch.GET_UNPOSTEDJGADJ);
      ds.Tables[0].Columns[0].ColumnName = "v_keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "v_acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "v_acct_type";
      ds.Tables[0].Columns[3].ColumnName = "v_amount";
      ds.Tables[0].Columns[4].ColumnName = "v_debit_credit";
      ds.Tables[0].Columns[5].ColumnName = "incr_with_crdt";
      return ds;
    }
    public static DataSet GET_UnpostedInvoices(ref DVOPrintVoteBook objsearch)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objsearch.Keyvalue.Trim();
      Parameter[1] = objsearch.AccountType.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOPrintVoteBook), objsearch.GET_UNPOSTED_INVOICE);
      ds.Tables[0].Columns[0].ColumnName = "v_keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "v_acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "v_acct_type";
      ds.Tables[0].Columns[3].ColumnName = "v_amount";
      ds.Tables[0].Columns[4].ColumnName = "v_debit_credit";
      ds.Tables[0].Columns[5].ColumnName = "incr_with_crdt";
      return ds;
    }
    public static DataSet GET_UnpostedAPChecks(ref DVOPrintVoteBook objsearch)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objsearch.Keyvalue.Trim();
      Parameter[1] = objsearch.AccountType.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOPrintVoteBook), objsearch.GET_UNPOSTED_APCHECK);
      ds.Tables[0].Columns[0].ColumnName = "v_keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "v_acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "v_acct_type";
      ds.Tables[0].Columns[3].ColumnName = "v_amount";
      ds.Tables[0].Columns[4].ColumnName = "v_debit_credit";
      ds.Tables[0].Columns[5].ColumnName = "incr_with_crdt";
      return ds;
    }
    public static DataSet GET_UnpostedPurchaseOrders(ref DVOPrintVoteBook objsearch)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objsearch.Keyvalue.Trim();
      Parameter[1] = objsearch.AccountType.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOPrintVoteBook), objsearch.GET_UNPOSTED_PURCHORD);

      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "acct_desc";
      ds.Tables[0].Columns[2].ColumnName = "acct_type";
      ds.Tables[0].Columns[3].ColumnName = "doc_no";
      ds.Tables[0].Columns[4].ColumnName = "orig_doc_no";
      ds.Tables[0].Columns[5].ColumnName = "po_no";
      ds.Tables[0].Columns[6].ColumnName = "buyer_code";
      ds.Tables[0].Columns[7].ColumnName = "po_type";
      ds.Tables[0].Columns[8].ColumnName = "po_date";
      ds.Tables[0].Columns[9].ColumnName = "po_status";
      ds.Tables[0].Columns[10].ColumnName = "vend_code";
      ds.Tables[0].Columns[11].ColumnName = "pay_to_code";
      ds.Tables[0].Columns[12].ColumnName = "bus_name";
      ds.Tables[0].Columns[13].ColumnName = "tax_amount";
      ds.Tables[0].Columns[14].ColumnName = "frght_amount";
      ds.Tables[0].Columns[15].ColumnName = "misc_amount";
      ds.Tables[0].Columns[16].ColumnName = "goods_amount";
      ds.Tables[0].Columns[17].ColumnName = "total_amount";
      ds.Tables[0].Columns[18].ColumnName = "prepay_amount";
      ds.Tables[0].Columns[19].ColumnName = "create_date";
      ds.Tables[0].Columns[20].ColumnName = "create_time";
      ds.Tables[0].Columns[21].ColumnName = "item_code";
      ds.Tables[0].Columns[22].ColumnName = "desc1";
      ds.Tables[0].Columns[23].ColumnName = "desc2";
      ds.Tables[0].Columns[24].ColumnName = "ordr_qty";
      ds.Tables[0].Columns[25].ColumnName = "rlse_qty";
      ds.Tables[0].Columns[26].ColumnName = "rjct_qty";
      ds.Tables[0].Columns[27].ColumnName = "recv_qty";
      ds.Tables[0].Columns[28].ColumnName = "cost_qty";
      ds.Tables[0].Columns[29].ColumnName = "acpt_qty";
      ds.Tables[0].Columns[30].ColumnName = "exp_rec_qty";
      ds.Tables[0].Columns[31].ColumnName = "exp_inv_qty";
      ds.Tables[0].Columns[32].ColumnName = "sell_unit";
      ds.Tables[0].Columns[33].ColumnName = "purch_unit";
      ds.Tables[0].Columns[34].ColumnName = "cost";
      ds.Tables[0].Columns[35].ColumnName = "gl_acct_no";
      ds.Tables[0].Columns[36].ColumnName = "net_price";

      return ds;
    }
    public static DataTable GetBudgetAdjustment(ref DVOPrintVoteBook objDVOPrintVoteBook)
    {
      object[] parameters = new object[4];
      parameters[0] = objDVOPrintVoteBook.Year.Trim();
      parameters[1] = objDVOPrintVoteBook.AccountType.Trim();
      parameters[2] = objDVOPrintVoteBook.Keyvalue.Trim();
      parameters[3] = objDVOPrintVoteBook.Date;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintVoteBook), objDVOPrintVoteBook.GET_BUD_ADJ);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "diff_in_alloc";
      ds.Tables[0].Columns[2].ColumnName = "desc";
      ds.Tables[0].Columns[3].ColumnName = "effectivedate";
      ds.Tables[0].Columns[4].ColumnName = "warrant_num";
      ds.Tables[0].Columns[5].ColumnName = "acct_desc";
      ds.Tables[0].Columns[6].ColumnName = "acct_type";
      ds.Tables[0].Columns[7].ColumnName = "dateentered";
      ds.Tables[0].Columns[8].ColumnName = "enteredby";
      ds.Tables[0].Columns[9].ColumnName = "first_name";
      ds.Tables[0].Columns[10].ColumnName = "middle_name";
      ds.Tables[0].Columns[11].ColumnName = "last_name";


      return ds.Tables[0];

    }
    public static DataSet GETEstRecExpByObjCode(ref DVORecExpByOBj objSearch)
    {
      Object[] parameters = new object[2];
      parameters[0] = objSearch._month;
      parameters[1] = objSearch._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj), objSearch.GET_EstRecExpbyObjCode);
      ds.Tables[0].Columns[0].ColumnName = "objcode";
      ds.Tables[0].Columns[1].ColumnName = "desc";
      ds.Tables[0].Columns[2].ColumnName = "actualexp";
      ds.Tables[0].Columns[3].ColumnName = "estexp";
      return ds;
    }

    //public static DataSet GETEstRecnCapRevByObjCode(ref DVOCapRevByObj objSearch)
    //{
    //    Object[] parameters = new object[2];
    //    parameters[0] = objSearch._month;
    //    parameters[1] = objSearch._Year;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj), objSearch.GET_ESTREV);
    //    ds.Tables[0].Columns[0].ColumnName = "objcode";
    //    ds.Tables[0].Columns[1].ColumnName = "desc";
    //    ds.Tables[0].Columns[2].ColumnName = "actualexp";
    //    ds.Tables[0].Columns[3].ColumnName = "estexp";
    //    return ds;
    //}

    public static DataSet GetEmployeeInfoOver55(string curDate, string date, string empType)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[3];
        parameters[0] = curDate;
        parameters[1] = date;
        parameters[2] = empType;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPrintSummaryAnalysis), (new DVOPrintSummaryAnalysis()).GET_EMPL_INFO_55);
        ds.Tables[0].Columns[0].ColumnName = "empl_code";
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
        ds.Tables[0].Columns[13].ColumnName = "job_code";
        ds.Tables[0].Columns[14].ColumnName = "job_title";
        ds.Tables[0].Columns[15].ColumnName = "date_hired";
        ds.Tables[0].Columns[16].ColumnName = "terminated";
        ds.Tables[0].Columns[17].ColumnName = "empl_status";
        ds.Tables[0].Columns[18].ColumnName = "pay_period";
        ds.Tables[0].Columns[19].ColumnName = "marital_stat";
        ds.Tables[0].Columns[20].ColumnName = "last_pay";
        ds.Tables[0].Columns[21].ColumnName = "hold_pymnt";
        ds.Tables[0].Columns[22].ColumnName = "flexdeptaccttype";
        ds.Tables[0].Columns[23].ColumnName = "appoint_date";
        ds.Tables[0].Columns[24].ColumnName = "gender";
        ds.Tables[0].Columns[25].ColumnName = "inc_net";

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    //public static DataSet GETEstRecnCapRevByObjCode1(ref DVOCapRevByObj objSearch)
    //{
    //    Object[] parameters = new object[2];
    //    parameters[0] = objSearch._month;
    //    parameters[1] = objSearch._Year;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj), objSearch.GET_ESTREV1);
    //    ds.Tables[0].Columns[0].ColumnName = "objcode";
    //    ds.Tables[0].Columns[1].ColumnName = "desc";
    //    ds.Tables[0].Columns[2].ColumnName = "actualexp";
    //    ds.Tables[0].Columns[3].ColumnName = "estexp";
    //    return ds;
    //}
    public static DataSet GETAnnualAbstrcAccount(ref DVORecExpByOBj objSearch)
    {
      Object[] parameters = new object[1];
      parameters[0] = objSearch._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj), objSearch.AbstrcAccount);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "segkeyvalue";
      ds.Tables[0].Columns[2].ColumnName = "amount";
      return ds;
    }
    public static DataSet GETAnnualAbstrcActAccount(ref DVORecExpByOBj objSearch)
    {
      Object[] parameters = new object[2];
      parameters[0] = objSearch._month;
      parameters[1] = objSearch._Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByOBj), objSearch.AbstrcActAccount);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "segkeyvalue";
      ds.Tables[0].Columns[2].ColumnName = "amount";
      return ds;
    }

    public static DataSet GetEstCapExpWithSourceFund(string Year, string Month, string Ministry, string Bud_Act)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[4];
        parameters[0] = Year.Trim();
        parameters[1] = Month.Trim();
        parameters[2] = Ministry.Trim();
        parameters[3] = Bud_Act.Trim();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistry), (new DVOCapExpByMinistry()).GET_CAPEXP_SRCFUND);
        ds.Tables[0].Columns[0].ColumnName = "ministry";
        ds.Tables[0].Columns[1].ColumnName = "project";
        ds.Tables[0].Columns[2].ColumnName = "projdesc";
        ds.Tables[0].Columns[3].ColumnName = "key";
        ds.Tables[0].Columns[4].ColumnName = "keydesc";
        ds.Tables[0].Columns[5].ColumnName = "amount";
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }
        public static DataSet GetDirectDepositeListing(ref DVOddmStypddreAndStypddrd objSearch, PensionProcessViewModel Paysearch, bool callStoredProcedure = false)
        {
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            if (callStoredProcedure)
            {
                object[] parameters = new object[16];
                parameters[0] = Paysearch.pybatchid;
                parameters[1] = Paysearch.searchcriteria;
                parameters[2] = objProcessDtl.insertby;
                parameters[3] = objProcessDtl.insertmachineinfo;
                parameters[4] = 0;//RowID;
                parameters[5] = Paysearch.EmployeeCode;
                parameters[6] = "";//SocSecNum;
                parameters[7] = Paysearch.FirstName;
                parameters[8] = Paysearch.LastName;
                parameters[9] = Paysearch.EmpType;
                parameters[10] = Paysearch.JobCode;
                parameters[11] = "N";
                parameters[12] = Paysearch.EOPDate;
                parameters[13] = objSearch.District.Trim();
                parameters[14] = Paysearch.PayrollDate;
                parameters[15] = "";

                return BLLPYBatchProcessDetailStybatchd.ExecuteAutoPay_V6(parameters);
            }
            else
            {
                Object[] parameters = new object[4];
                parameters[0] = objSearch.type_code.Trim();
                parameters[1] = objSearch.BanckCode.Trim();
                parameters[2] = objSearch.Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objSearch.District.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOddmStypddreAndStypddrd), objSearch.GET_DDL);
                ds.Tables[0].Columns[0].ColumnName = "bank_desc";
                ds.Tables[0].Columns[1].ColumnName = "amount";
                ds.Tables[0].Columns[2].ColumnName = "bank_acct_no";
                //ds.Tables[0].Columns[3].ColumnName = "empl_code";
                ds.Tables[0].Columns[4].ColumnName = "empl_name";
                ds.Tables[0].Columns[5].ColumnName = "bank_code";
                ds.Tables[0].Columns[6].ColumnName = "batch_date";
                ds.Tables[0].Columns[7].ColumnName = "Account_Type";
                ds.Tables[0].Columns[8].ColumnName = "ApplicationReferenceNo";
                return ds;
            }
        }

    //Added by sunil Pahwa on 7/4/2009
    public static DataSet GetDeductionCommissionInfo(ref DVODeductionCommision objDvoDedComm)
    {
      Object[] parameters = new object[3];
      parameters[0] = objDvoDedComm.ded_code;
      parameters[1] = objDvoDedComm.Month;
      parameters[2] = objDvoDedComm.Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionCommision));
      ds.Tables[0].Columns[0].ColumnName = "co_name";
      ds.Tables[0].Columns[1].ColumnName = "first_name";
      ds.Tables[0].Columns[2].ColumnName = "last_name";
      ds.Tables[0].Columns[3].ColumnName = "middle_name";
      ds.Tables[0].Columns[4].ColumnName = "amount";
      ds.Tables[0].Columns[5].ColumnName = "empl_code";
      //ds.Tables[0].Columns[6].ColumnName = "SearchCriteria";

      return ds;


    }
    //Added by Sunil Pahwa
    public static DataSet GetClaimEarningInfo(DVOClaimEarnings objDvoClaimEarnings)
    {
      Object[] parameters = new object[4];
      parameters[0] = objDvoClaimEarnings.empl_code.Trim();
      parameters[1] = objDvoClaimEarnings.act_code;
      parameters[2] = objDvoClaimEarnings.start_date;
      parameters[3] = objDvoClaimEarnings.End_date;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOClaimEarnings));
      if (ds != null)
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "amount";
          ds.Tables[0].Columns[1].ColumnName = "first_name";
          ds.Tables[0].Columns[2].ColumnName = "last_name";
          ds.Tables[0].Columns[3].ColumnName = "middle_name";
          ds.Tables[0].Columns[4].ColumnName = "act_code";
          ds.Tables[0].Columns[5].ColumnName = "empl_code";
        }
      return ds;
    }
    //Added by Sunil Pahwa
    public static DataSet GetQtrlyHourlyWagesInfo(ref DVOQtrlyHourlyWages objDvoQtrlyHourlyWages)
    {
      Object[] parameters = new object[2];
      parameters[0] = objDvoQtrlyHourlyWages.start_date;
      parameters[1] = objDvoQtrlyHourlyWages.End_date;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOQtrlyHourlyWages));
      ds.Tables[0].Columns[0].ColumnName = "ref_code";
      ds.Tables[0].Columns[1].ColumnName = "act_code";
      ds.Tables[0].Columns[2].ColumnName = "act_type";
      ds.Tables[0].Columns[3].ColumnName = "amount";
      ds.Tables[0].Columns[4].ColumnName = "hours";
      ds.Tables[0].Columns[5].ColumnName = "first_name";

      ds.Tables[0].Columns[6].ColumnName = "last_name";
      ds.Tables[0].Columns[7].ColumnName = "middle_name";
      ds.Tables[0].Columns[8].ColumnName = "soc_sec_num";

      ds.Tables[0].Columns[9].ColumnName = "pay_date";
      ds.Tables[0].DefaultView.Sort = "soc_sec_num, ref_code, pay_date, act_code";

      return ds;
    }

    //public static DataSet get_Saving_Bank_Deposit_BalanceInfo(DVOSBclients objDvoSavingBank)
    //{
    //    Object[] parameters = new object[3];
    //    parameters[0] = objDvoSavingBank.acct_status;
    //    parameters[1] = objDvoSavingBank.start_date;
    //    parameters[2] = objDvoSavingBank.end_date;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients), objDvoSavingBank.GET_SAVING_BANK_ACCOUNT_BALANCE);
    //    ds.Tables[0].Columns[0].ColumnName = "v_doc_no";
    //    ds.Tables[0].Columns[1].ColumnName = "v_doc_date";
    //    ds.Tables[0].Columns[2].ColumnName = "v_acct_id";
    //    ds.Tables[0].Columns[3].ColumnName = "v_acct_cat";
    //    ds.Tables[0].Columns[4].ColumnName = "v_acct_no";
    //    ds.Tables[0].Columns[5].ColumnName = "v_acct_status";

    //    ds.Tables[0].Columns[6].ColumnName = "v_tran_type";
    //    ds.Tables[0].Columns[7].ColumnName = "v_tarn_no";
    //    ds.Tables[0].Columns[8].ColumnName = "v_deposit_amt";

    //    ds.Tables[0].Columns[9].ColumnName = "v_withdrawn_amt";

    //    ds.Tables[0].Columns[10].ColumnName = "v_acct_balance";
    //    ds.Tables[0].Columns[11].ColumnName = "v_active_balance";
    //    ds.Tables[0].Columns[12].ColumnName = "v_operator";

    //    ds.Tables[0].Columns[13].ColumnName = "v_postedby";
    //    ds.Tables[0].Columns[14].ColumnName = "v_posteddate";

    //    return ds;
    //}
    //Added by Sunil Pahwa
    public static DataSet GET_SB_Client_Detail(DVOSBclients objDvoSBClient)
    {

      Object[] parameters = new object[8];
      parameters[0] = objDvoSBClient.acct_cat;
      parameters[1] = objDvoSBClient.acct_no;
      parameters[2] = objDvoSBClient.last_name;
      parameters[3] = objDvoSBClient.first_name;
      parameters[4] = objDvoSBClient.acct_status;
      parameters[5] = objDvoSBClient.start_date;
      parameters[6] = objDvoSBClient.end_date;
      parameters[7] = "SAVINGBANKCLIENTREPORT";


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients));
      //DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients), objDvoSBClient.GET_SB_ClIENT_DETAIL);
      if (ds.Tables.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "acct_id";
        ds.Tables[0].Columns[1].ColumnName = "acct_cat";
        ds.Tables[0].Columns[2].ColumnName = "acct_no";
        ds.Tables[0].Columns[3].ColumnName = "acct_status";
        ds.Tables[0].Columns[4].ColumnName = "acct_closed";
        ds.Tables[0].Columns[5].ColumnName = "last_name";

        ds.Tables[0].Columns[6].ColumnName = "first_name";
        ds.Tables[0].Columns[7].ColumnName = "n_join";
        ds.Tables[0].Columns[8].ColumnName = "first_name1";

        ds.Tables[0].Columns[9].ColumnName = "last_name1";

        ds.Tables[0].Columns[10].ColumnName = "remarks";
        ds.Tables[0].Columns[11].ColumnName = "address_1";
        ds.Tables[0].Columns[12].ColumnName = "address_2";

        ds.Tables[0].Columns[13].ColumnName = "acct_date";
      }

      return ds;

    }
    //Added by Sunil Pahwa
    public static DataSet Get_SavingBank_AccountInfo(DVOSBclients objDvoSavingBank)
    {
      Object[] parameters = new object[8];
      parameters[0] = objDvoSavingBank.acct_cat;
      parameters[1] = objDvoSavingBank.acct_no;
      parameters[2] = string.Empty;
      parameters[3] = string.Empty;
      parameters[4] = objDvoSavingBank.acct_status;
      parameters[5] = objDvoSavingBank.start_date;
      parameters[6] = objDvoSavingBank.end_date;
      parameters[7] = "SAVINGBANKCLIENTREPORT";

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients));
      //DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients), objDvoSBClient.GET_SB_ClIENT_DETAIL);
      //ds.Tables[0].Columns[0].ColumnName = "acct_id";
      //ds.Tables[0].Columns[1].ColumnName = "acct_cat";
      //ds.Tables[0].Columns[2].ColumnName = "acct_no";
      //ds.Tables[0].Columns[3].ColumnName = "acct_status";
      //ds.Tables[0].Columns[4].ColumnName = "acct_closed";
      //ds.Tables[0].Columns[5].ColumnName = "last_name";

      //ds.Tables[0].Columns[6].ColumnName = "first_name";
      //ds.Tables[0].Columns[7].ColumnName = "n_join";
      //ds.Tables[0].Columns[8].ColumnName = "first_name1";

      //ds.Tables[0].Columns[9].ColumnName = "last_name1";

      //ds.Tables[0].Columns[10].ColumnName = "remarks";
      //ds.Tables[0].Columns[11].ColumnName = "address_1";
      //ds.Tables[0].Columns[12].ColumnName = "address_2";

      //ds.Tables[0].Columns[13].ColumnName = "acct_date";


      return ds;
    }
    public static DataSet GetCheckJournalData(ref DVOAPCheckProcessingStpcashe objSearch)
    {
      Object[] parameters = new object[4];
      parameters[0] = objSearch.start_date;
      parameters[1] = objSearch.end_date;
      parameters[2] = objSearch.cash_acct;
      parameters[3] = objSearch.check_no.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_CHECK_JRNL);

      ds.Tables[0].Columns[0].ColumnName = "v_bus_name";
      ds.Tables[0].Columns[1].ColumnName = "v_keyvalue";
      ds.Tables[0].Columns[2].ColumnName = "v_amount";
      ds.Tables[0].Columns[3].ColumnName = "v_chk_voided";
      ds.Tables[0].Columns[4].ColumnName = "v_debit_credit";
      ds.Tables[0].Columns[5].ColumnName = "v_inv_chk_no";
      ds.Tables[0].Columns[6].ColumnName = "v_doc_date";
      ds.Tables[0].Columns[7].ColumnName = "v_doc_desc";
      ds.Tables[0].Columns[8].ColumnName = "v_doc_no";
      ds.Tables[0].Columns[9].ColumnName = "v_ref_code";
      return ds;
    }
    public static object GetBus_Name(string vend_code)
    {
      Object[] parameters = new object[1];
      parameters[0] = vend_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOAPCheckProcessingStpcashe objSearch = new DVOAPCheckProcessingStpcashe();
      object bus_name = objDalBaseClass.ExecuteScalar(ref parameters, objSearch.GET_Bus_Name);

      return bus_name;
    }

    public static DataSet GetRecExpEstamtByProgram(string ministry)
    {
      Object[] parameters = new object[1];
      parameters[0] = ministry;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DVORecExpByProgram_FromW2A_ objRecExp = new DVORecExpByProgram_FromW2A_();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecExpByProgram_FromW2A_), objRecExp.GET_EST_AMT);
      ds.Tables[0].Columns[0].ColumnName = "keyvalue";
      ds.Tables[0].Columns[1].ColumnName = "amount";
      ds.Tables[0].Columns[2].ColumnName = "year";
      return ds;

    }

    //Added by Sunil Pahwa
    public static DataSet Close_SavingBank_AccountInfo_ToShow(DVOPostTranr objDvoDtl)
    {
      // decimal end_balance = 0;
      // decimal interest = 0;
      DataSet ds = new DataSet();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objDvoDtl.acct_cat;
        parameters[1] = objDvoDtl.acct_no;
        //parameters[2] = objDvoDtl.acct_status;

        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPostTranr));
        if (ds.Tables.Count > 0)
        {
          if (ds.Tables[0].Rows.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "acct_id";
            ds.Tables[0].Columns[1].ColumnName = "acct_cat";
            ds.Tables[0].Columns[2].ColumnName = "acct_no";
            ds.Tables[0].Columns[3].ColumnName = "acct_status";
            ds.Tables[0].Columns[4].ColumnName = "doc_date";
            ds.Tables[0].Columns[5].ColumnName = "withdrawn_amt";
            ds.Tables[0].Columns[6].ColumnName = "deposit_amt";
            ds.Tables[0].Columns[7].ColumnName = "acct_balance";
            ds.Tables[0].Columns[8].ColumnName = "operator";
            ds.Tables[0].Columns[9].ColumnName = "tran_type";
            ds.Tables[0].Columns[10].ColumnName = "tran_no";
            ds.Tables[0].Columns[11].ColumnName = "doc_no";
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }


    public static DataSet Saving_Bank_Account_ToShow(DVOSBDefaults objdvoAccdtl, DVOPostTranr objDvoDtl)
    {
      DataSet ds = new DataSet();
      DataSet ds1 = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objdvoAccdtl.acct_cat;
        parameters[1] = objdvoAccdtl.acct_no;
        //parameters[2] = objdvoAccdtl.acct_status;

        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBDefaults));
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "acct_id";
          ds.Tables[0].Columns[1].ColumnName = "acct_cat";
          ds.Tables[0].Columns[2].ColumnName = "acct_no";
          ds.Tables[0].Columns[3].ColumnName = "acct_status";
          ds.Tables[0].Columns[4].ColumnName = "doc_date";
          ds.Tables[0].Columns[5].ColumnName = "withdrawn_amt";
          ds.Tables[0].Columns[6].ColumnName = "deposit_amt";
          ds.Tables[0].Columns[7].ColumnName = "acct_balance";
          ds.Tables[0].Columns[8].ColumnName = "operator";
          ds.Tables[0].Columns[9].ColumnName = "tran_type";
          ds.Tables[0].Columns[10].ColumnName = "tran_no";
          ds.Tables[0].Columns[11].ColumnName = "doc_no";
          ds1 = Close_SavingBank_AccountInfo_ToShow(objDvoDtl);
          ds1.Merge(ds);
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds1;

    }

    public static DataTable Get_Interest_Calculation_For_Close_Account(ref DVOSBclients objDvoIntCal)
    {
      DataTable dsdata = new DataTable();
      dsdata = null;//= BLLSavingBankInterestCalculation.PostSavingBankInterest(ref objDvoIntCal);
      return dsdata;

    }

    public static DataSet Close_SavingBank_AccountInfo(ref DVOPostTranr objDvoDtl, DataTable dtClientInfo, int BatchID, int AccountPayableBatchID, ref DVOBatch objDVOPostBatch, decimal CalculatedInterest, decimal Endbalance)
    {
      #region Commented Code..................
      // DVOSBclients ObjSBClients = new DVOSBclients();
      // DataSet dsDataset = new DataSet();
      //Get_SavingBank_AccountInfo(objDvoSavingBank);
      // //dsDataset = GET_SB_Client_Detail(objDvoSBClient);
      // //Get_SavingBank_AccountInfo(DVOSBclients objDvoSavingBank)
      //ObjSBClients.acct_cat = dtClientInfo.Rows[0]["acct_cat"].ToString();
      //ObjSBClients.acct_no =Convert.ToInt32 ( dtClientInfo.Rows[0]["acct_no"]);
      //ObjSBClients.acct_status = dtClientInfo.Rows[0]["acct_status"].ToString();
      //ObjSBClients.acct_closed = dtClientInfo.Rows[0]["acct_closed"].ToString();

      //ObjSBClients.last_name = dtClientInfo.Rows[0]["last_name"].ToString().Trim();
      //ObjSBClients.first_name = dtClientInfo.Rows[0]["first_name"].ToString().Trim();
      //ObjSBClients.n_join = dtClientInfo.Rows[0]["n_join"].ToString().Trim();
      //ObjSBClients.first_name1 = dtClientInfo.Rows[0]["first_name1"].ToString();
      //ObjSBClients.last_name1 = dtClientInfo.Rows[0]["last_name1"].ToString();
      //DataTable dsdata = BLLSavingBankInterestCalculation.PostSavingBankInterest(ref ObjSBClients);
      //    // BLLSavingBankInterestCalculation.GetSavingBankInterest(ref  ObjSBClients);




      //DVOSBclients ObjSBClients = new DVOSBclients();
      //ObjSBClients.acct_id = objDvoDtl.acct_cat;
      //ObjSBClients.acct_no = objDvoDtl.acct_no;
      //ObjSBClients.acct_status = objDvoDtl.acct_status;
      //ObjSBClients.


      //    ObjSBClients.acct_id = dr["acct_cat"].ToString();


      //    DataSet dsInterest = BLLSavingBankInterestCalculation.PostSavingBankInterest(ref  ObjSBClients);

      //}
      #endregion

      //decimal end_balance = 0; // now replace with Endbalance
      //decimal interest = 0; // now replace with CalculatedInterest
      DataSet ds = new DataSet();
      DataSet DSCloseSBAccount = new DataSet();
      bool Success = false;
      DVOSBDefaults objDVOSBDefaults;
      List<DVOSBDefaults> list = null;//= BLLSBDefaults.GetAllDetailInfo();
      if (list.Count > 0)
        objDVOSBDefaults = list[0];
      else
        objDVOSBDefaults = new DVOSBDefaults();
      list = null;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        if (dtClientInfo.Rows.Count > 0)
        {
          DataRow dr = dtClientInfo.Rows[0];

          //make an entry of transaction to pay interest
          DVOSBhdr objDVOSBhdr = new DVOSBhdr();
          objDVOSBhdr.acct_cat = dr["acct_cat"].ToString();
          objDVOSBhdr.acct_no = Convert.ToInt32(dr["acct_no"]);
          objDVOSBhdr.acct_id = dr["acct_id"].ToString().Trim();
          objDVOSBhdr.acct_status = dr["acct_status"].ToString().Trim();
          objDVOSBhdr.first_name = dr["first_name"] != DBNull.Value ? dr["first_name"].ToString().Trim() : string.Empty;
          objDVOSBhdr.last_name = dr["last_name"] != DBNull.Value ? dr["last_name"].ToString().Trim() : string.Empty;
          objDVOSBhdr.n_join = dr["n_join"] != DBNull.Value ? dr["n_join"].ToString().Trim() : string.Empty;
          objDVOSBhdr.first_name1 = dr["first_name1"] != DBNull.Value ? dr["first_name1"].ToString().Trim() : string.Empty;
          objDVOSBhdr.last_name1 = dr["last_name1"] != DBNull.Value ? dr["last_name1"].ToString().Trim() : string.Empty;
          objDVOSBhdr.doc_date = DateTime.ParseExact(DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);// Convert.ToDateTime(DateTime.Now.ToString());//Convert.ToDateTime ( dtClientInfo.Rows[i]["doc_date"]);
          objDVOSBhdr.tran_code = "I";
          // objDVOSBhdr.active_balance = Convert.ToDecimal(txtActiveBal.Text);
          objDVOSBhdr.withdrawn_amt = 0;//Convert.ToDecimal(dtClientInfo.Rows[i][5]);
          objDVOSBhdr.deposit_amt = CalculatedInterest;//Convert.ToDecimal(dtClientInfo.Rows[i][6]);
                                                       //after deposit interest calculated
          Endbalance += CalculatedInterest;
          objDVOSBhdr.acct_balance = Endbalance;//Convert.ToDecimal(dtClientInfo.Rows[i][7]);
          objDVOSBhdr.Operator = DVOApplicationUserInfo.LoginId;                  //Program.LoginId.Trim();
                                                                                  //objDVOSBhdr.trans_id = 0; // Convert.ToInt32(txtTranNo.Text); 
          objDVOSBhdr.Ok_to_Post = "N";
          objDVOSBhdr.InsertBy = DVOApplicationUserInfo.UserId; //Program.UserId;
          objDVOSBhdr.InsertMachineInfo = DVOApplicationUserInfo.MachineInfo; //Program.MachineInfo;

          //Added By Rahul Jain on 13-08-09 ****
          objDVOSBhdr.cash_received = 1;
          objDVOSBhdr.batch_id = BatchID;
          //************************************
          DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
          List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();

          int NewDocumentNo_Interest = 0, NewTransactionNo_Interest = 0;

          int status = 0;// = BLLSBTransaction.InsertNewSBTransaction(ref objTransaction, ref objDVOSBhdr, ref objDVOAPCheckProcessingStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocumentNo_Interest, out NewTransactionNo_Interest);
          if (status <= 0)
          {
            throw new Exception();
          }

          //make an entry of transaction to withdraw all amount from account to close
          objDVOSBhdr = new DVOSBhdr();
          objDVOSBhdr.acct_cat = dr["acct_cat"].ToString();
          objDVOSBhdr.acct_no = Convert.ToInt32(dr["acct_no"]);
          objDVOSBhdr.acct_id = dr["acct_id"].ToString().Trim();
          objDVOSBhdr.acct_status = dr["acct_status"].ToString().Trim();
          objDVOSBhdr.first_name = dr["first_name"] != DBNull.Value ? dr["first_name"].ToString().Trim() : string.Empty;
          objDVOSBhdr.last_name = dr["last_name"] != DBNull.Value ? dr["last_name"].ToString().Trim() : string.Empty;
          objDVOSBhdr.n_join = dr["n_join"] != DBNull.Value ? dr["n_join"].ToString().Trim() : string.Empty;
          objDVOSBhdr.first_name1 = dr["first_name1"] != DBNull.Value ? dr["first_name1"].ToString().Trim() : string.Empty;
          objDVOSBhdr.last_name1 = dr["last_name1"] != DBNull.Value ? dr["last_name1"].ToString().Trim() : string.Empty;
          objDVOSBhdr.doc_date = DVOApplicationUserInfo.CurrentDate; //Convert.ToDateTime(dtClientInfo.Rows[i][4]);
                                                                     //objDVOSBhdr.tran_code = "I";//txtTrancode.Text.ToString().Trim();
          objDVOSBhdr.active_balance = 0;// Convert.ToDecimal(txtActiveBal.Text);
          objDVOSBhdr.tran_code = "W";
          objDVOSBhdr.withdrawn_amt = Endbalance;//Convert.ToDecimal(dtClientInfo.Rows[i][5]);
          objDVOSBhdr.deposit_amt = 0;//Convert.ToDecimal(dtClientInfo.Rows[i][6]);
                                      //after withdrawn all acct_balance
          Endbalance = 0;
          objDVOSBhdr.acct_balance = Endbalance; //Convert.ToDecimal(dtClientInfo.Rows[i][7]);
          objDVOSBhdr.Operator = DVOApplicationUserInfo.LoginId; ;
          //objDVOSBhdr.trans_id = 1;// Convert.ToInt32(txtTranNo.Text); 
          objDVOSBhdr.Ok_to_Post = "N";
          objDVOSBhdr.InsertBy = DVOApplicationUserInfo.UserId;
          objDVOSBhdr.InsertMachineInfo = DVOApplicationUserInfo.MachineInfo;

          //Added By Rahul Jain on 13-08-09 ****
          objDVOSBhdr.cash_received = 1;
          objDVOSBhdr.batch_id = BatchID;
          //************************************

          CreateAPforWithdraw(AccountPayableBatchID, CalculatedInterest, ref objDVOSBDefaults, ref objDVOSBhdr, out objDVOAPCheckProcessingStpcashe, out listDVOAPCheckProcessingDetailStpcashd);
          int NewDocumentNo_Withdraw = 0, NewTransactionNo_Withdraw = 0;
          //  status = BLLSBTransaction.InsertNewSBTransaction(ref objTransaction, ref objDVOSBhdr, ref objDVOAPCheckProcessingStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocumentNo_Withdraw, out NewTransactionNo_Withdraw);
          if (status <= 0)
          {
            throw new Exception();
          }

          //Get all Unposted Transactions
          DataSet dsNew = ReportingUtilities.Saving_Bank_Unposted_Transaction(objDVOSBhdr);
          if (dsNew.Tables[0].Rows.Count > 0)
          {
            //Post transaction of withdraw amount
            objDVOSBhdr.postorcheck = "POST";
            int post_no = 0;
            // ds = PostingBLL.BLLPostSavingBankTransaction.PostSavingBankTransactions(ref objDVOSBhdr, ref objDVOPostBatch, AccountPayableBatchID, ref objDVOSBDefaults, objDVOSBDefaults.cash_acctno, dsNew, ref objTransaction, out post_no);
          }

          //Post transaction of withdraw amount
          //objDVOSBhdr.doc_no = NewDocumentNo_Withdraw;
          //ds = TransactionToPost(ref objDVOSBhdr);
          //DataTable objDataTable = null;
          //objDVOSBhdr.postorcheck = "POST";
          //ds = PostingBLL.BLLPostSavingBankTransaction.PostSavingBankTransactions(ref objDVOSBhdr, ref objDVOPostBatch, AccountPayableBatchID, ref objDVOSBDefaults, objDVOSBDefaults.cash_acctno, ds, ref objTransaction);

          //PostingBLL.BLLPostSavingBankTransaction.PostTransaction(ref objDVOSBhdr, AccountPayableBatchID, ref objDVOSBDefaults, objDVOSBDefaults.cash_acctno, dt.Rows[0], ref objDALBaseClassHelper, ref objDalBaseClass, ref objTransaction, ref objDataTable);
          //update client's status to account-close
          Update_Client_Status_In_Close_Account(ref objTransaction, ref objDvoDtl);
          objDVOSBhdr.acct_status = "CLOSED";

          //returning the final dataset after updating acct closed status
          DVOSBclients objDVOSBclients = new DVOSBclients();
          objDVOSBclients.acct_cat = objDVOSBhdr.acct_cat;
          objDVOSBclients.acct_no = objDVOSBhdr.acct_no;
          objDVOSBclients.acct_status = objDVOSBhdr.acct_status;
          DSCloseSBAccount = ReportingUtilities.Get_Info_For_Close_Account(objDVOSBclients);
          if (objTransaction != null)
            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
          //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        }
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return DSCloseSBAccount;
    }

    public static DataSet Saving_Bank_Unposted_Transaction(DVOSBhdr objDVOSBhdr)
    {
      DataSet ds = new DataSet();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objDVOSBhdr.acct_cat;
        parameters[1] = objDVOSBhdr.acct_no;
        parameters[2] = objDVOSBhdr.acct_status;
        ds = objDalBaseClass.GetData(objDVOSBhdr.FINDQUERY_UNPOSTEDTRAN_GET(ref parameters));
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
      }
      return ds;

    }

    private static DataSet TransactionToPost(ref DVOSBhdr objDVOSBhdr)
    {
      DataSet ds = new DataSet();
      ds.Tables.Add();
      ds.Tables[0].Columns.Add("lname", typeof(string));
      ds.Tables[0].Columns.Add("fname", typeof(string));
      ds.Tables[0].Columns.Add("doc_no", typeof(int));
      ds.Tables[0].Columns.Add("doc_date", typeof(DateTime));
      ds.Tables[0].Columns.Add("acct_id", typeof(string));
      ds.Tables[0].Columns.Add("acct_cat", typeof(string));
      ds.Tables[0].Columns.Add("acct_no", typeof(int));
      ds.Tables[0].Columns.Add("acct_status", typeof(string));

      ds.Tables[0].Columns.Add("tran_type", typeof(string));
      ds.Tables[0].Columns.Add("tran_no", typeof(int));
      ds.Tables[0].Columns.Add("deposit_amt", typeof(decimal));
      ds.Tables[0].Columns.Add("withdrawn_amt", typeof(decimal));
      ds.Tables[0].Columns.Add("acct_balance", typeof(decimal));
      ds.Tables[0].Columns.Add("active_balance", typeof(decimal));
      ds.Tables[0].Columns.Add("operator", typeof(string));

      ds.Tables[0].Columns.Add("ok_to_post", typeof(string));
      ds.Tables[0].Columns.Add("apdoc_no", typeof(string));
      ds.Tables[0].Columns.Add("cash_received", typeof(int));
      ds.Tables[0].Columns.Add("batch_id", typeof(int));

      DataRow dr = ds.Tables[0].NewRow();
      dr[0] = objDVOSBhdr.last_name;
      dr[1] = objDVOSBhdr.first_name;
      dr[2] = objDVOSBhdr.doc_no;
      dr[3] = objDVOSBhdr.doc_date;
      dr[4] = objDVOSBhdr.acct_id;
      dr[5] = objDVOSBhdr.acct_cat;
      dr[6] = objDVOSBhdr.acct_no;
      dr[7] = objDVOSBhdr.acct_status;
      dr[8] = objDVOSBhdr.tran_code;
      dr[9] = objDVOSBhdr.trans_id;

      dr[10] = objDVOSBhdr.deposit_amt;
      dr[11] = objDVOSBhdr.withdrawn_amt;
      dr[12] = objDVOSBhdr.acct_balance;
      dr[13] = objDVOSBhdr.active_balance;
      dr[14] = objDVOSBhdr.Operator;
      dr[15] = objDVOSBhdr.Ok_to_Post;
      dr[16] = objDVOSBhdr.apdoc_no;
      dr[17] = objDVOSBhdr.cash_received;
      dr[18] = objDVOSBhdr.batch_id;

      ds.Tables[0].Rows.Add(dr.ItemArray);

      return ds;
    }

    private static void Update_Client_Status_In_Close_Account(ref object objTransaction, ref DVOPostTranr objDvoDtl)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      try
      {
        object[] updParameter = new object[3];
        updParameter[0] = objDvoDtl.acct_cat;
        updParameter[1] = objDvoDtl.acct_no;
        updParameter[2] = objDvoDtl.acct_status;

        //DVOSBclients  objupd = new DVOAPCheckProcessingStpcashe();
        object obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref updParameter, objDvoDtl.UPDATE_CLIENT_STATUS_IN_CLOSE_ACCOUNT);
        if (obj == null)
          throw new Exception("Error occured during update client's status to close.");
        else if (Convert.ToInt32(obj) < 1)
          throw new Exception("Error occured during update client's status to close.");
        updParameter = null;

        if (objTransaction != null && !(statusObjTransaction))
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      }
      catch (Exception ex)
      {
        if (objTransaction != null && !(statusObjTransaction))
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }

    private static void CreateAPforWithdraw(int BatchID, decimal CalculatedInterest, ref DVOSBDefaults objDVOSBDefaults, ref DVOSBhdr objDVOSBhdr, out DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, out List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
    {
      objDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
      //objDVOAPCheckProcessingStpcashe.doc_no = DocumentNo;
      objDVOAPCheckProcessingStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);// Program.CurrentDate.ToString("MM/dd/yyyy");
      objDVOAPCheckProcessingStpcashe.vend_code = "MISC";
      objDVOAPCheckProcessingStpcashe.bus_name = objDVOSBhdr.last_name + ", " + objDVOSBhdr.first_name;
      objDVOAPCheckProcessingStpcashe.pay_to_code = "PAYTO";
      objDVOAPCheckProcessingStpcashe.doc_desc = "SAVING BANK CLOSE ACT#" + objDVOSBhdr.acct_cat.Trim() + objDVOSBhdr.acct_no.ToString().Trim();
      objDVOAPCheckProcessingStpcashe.tre_voucher_no = "99999";
      objDVOAPCheckProcessingStpcashe.cash_acct = objDVOSBDefaults.cash_acctno;
      objDVOAPCheckProcessingStpcashe.cash_amt = objDVOSBhdr.withdrawn_amt;
      objDVOAPCheckProcessingStpcashe.cash_deb_cred = "CR";
      objDVOAPCheckProcessingStpcashe.cash_department = "000";
      objDVOAPCheckProcessingStpcashe.oa_amt = 0;
      objDVOAPCheckProcessingStpcashe.print_chk = "Y";
      objDVOAPCheckProcessingStpcashe.ok_to_post = "N";
      objDVOAPCheckProcessingStpcashe.chk_printed = "N";
      objDVOAPCheckProcessingStpcashe.ap_type = "N";
      objDVOAPCheckProcessingStpcashe.required_approval = 100;
      objDVOAPCheckProcessingStpcashe.batch_id = BatchID;
      objDVOAPCheckProcessingStpcashe.current_approval = -1;
      objDVOAPCheckProcessingStpcashe.acd_id = 3;

      listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
      using (DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd())
      {
        //assign appropriate values to detail-object
        //objDVOAPCheckProcessingDetailStpcashd.doc_no = DocumentNo;
        objDVOAPCheckProcessingDetailStpcashd.inv_doc_no = -99;
        objDVOAPCheckProcessingDetailStpcashd.dist_acct = objDVOSBDefaults.with_acctno;
        objDVOAPCheckProcessingDetailStpcashd.dist_department = "000";
        objDVOAPCheckProcessingDetailStpcashd.dist_amt = objDVOSBhdr.withdrawn_amt - CalculatedInterest;
        objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = "DB";

        //add into list
        listDVOAPCheckProcessingDetailStpcashd.Add(objDVOAPCheckProcessingDetailStpcashd);
      }
      using (DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd())
      {
        //assign appropriate values to detail-object
        //objDVOAPCheckProcessingDetailStpcashd.doc_no = DocumentNo;
        objDVOAPCheckProcessingDetailStpcashd.inv_doc_no = -99;
        objDVOAPCheckProcessingDetailStpcashd.dist_acct = objDVOSBDefaults.int_acctno;
        objDVOAPCheckProcessingDetailStpcashd.dist_department = "000";
        objDVOAPCheckProcessingDetailStpcashd.dist_amt = CalculatedInterest;
        objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = "DB";

        //add into list
        listDVOAPCheckProcessingDetailStpcashd.Add(objDVOAPCheckProcessingDetailStpcashd);
      }
    }

    public static DataSet GetSavingBankInterest(ref DVOSBclients objSearch)
    {
      DataSet ds = null;
      try
      {
        Object[] parameters = new object[5];
        parameters[0] = objSearch.Year;
        parameters[1] = objSearch.acct_status;
        parameters[2] = objSearch.acct_closed;
        parameters[3] = objSearch.acct_cat;
        parameters[4] = objSearch.acct_no;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objSearch.FIND_SBInterest(ref parameters));
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "acct_id";
          ds.Tables[0].Columns[1].ColumnName = "acct_cat";
          ds.Tables[0].Columns[2].ColumnName = "acct_no";
          ds.Tables[0].Columns[3].ColumnName = "last_name";
          ds.Tables[0].Columns[4].ColumnName = "first_name";
          ds.Tables[0].Columns[5].ColumnName = "n_join";
          ds.Tables[0].Columns[6].ColumnName = "first_name1";
          ds.Tables[0].Columns[7].ColumnName = "last_name1";
          ds.Tables[0].Columns[8].ColumnName = "acct_balance";
          ds.Tables[0].Columns[9].ColumnName = "deposit_amt";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }
    public static DataSet GetSavingBankUnpostedTrans(DateTime start_date, DateTime end_date)
    {
      DataSet ds = null;
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = start_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = end_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DVOSBTransactionDetails objTransDlt = new DVOSBTransactionDetails();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBTransactionDetails), objTransDlt.GET_UNP_TRAN_DTL);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "doc_no";
          ds.Tables[0].Columns[1].ColumnName = "doc_date";
          ds.Tables[0].Columns[2].ColumnName = "acct_id";
          ds.Tables[0].Columns[3].ColumnName = "tran_type";
          ds.Tables[0].Columns[4].ColumnName = "deposit_amt";
          ds.Tables[0].Columns[5].ColumnName = "withdrawn_amt";
          ds.Tables[0].Columns[6].ColumnName = "last_name";
          ds.Tables[0].Columns[7].ColumnName = "first_name";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }
    public static DataSet GetSavingBankPostedTrans(DateTime start_date, DateTime end_date)
    {
      DataSet ds = null;
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = start_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = end_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DVOSBTransactionDetails objTransDlt = new DVOSBTransactionDetails();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBTransactionDetails), objTransDlt.GET_P_TRAN_DTL);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "doc_no";
          ds.Tables[0].Columns[1].ColumnName = "doc_date";
          ds.Tables[0].Columns[2].ColumnName = "acct_id";
          ds.Tables[0].Columns[3].ColumnName = "tran_type";
          ds.Tables[0].Columns[4].ColumnName = "deposit_amt";
          ds.Tables[0].Columns[5].ColumnName = "withdrawn_amt";
          ds.Tables[0].Columns[6].ColumnName = "last_name";
          ds.Tables[0].Columns[7].ColumnName = "first_name";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    public static DataSet Get_Info_For_Close_Account(DVOSBclients objDvoSavingBnk)
    {
      Object[] parameters = new object[8];
      parameters[0] = objDvoSavingBnk.acct_cat;
      parameters[1] = objDvoSavingBnk.acct_no;
      parameters[2] = string.Empty;
      parameters[3] = string.Empty;
      parameters[4] = objDvoSavingBnk.acct_status;
      parameters[5] = string.Empty;
      parameters[6] = string.Empty;
      parameters[7] = "SAVINGBANKCLIENTREPORT";

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients));
      //DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSBclients), objDvoSBClient.GET_SB_ClIENT_DETAIL);
      ds.Tables[0].Columns[0].ColumnName = "acct_id";
      ds.Tables[0].Columns[1].ColumnName = "acct_cat";
      ds.Tables[0].Columns[2].ColumnName = "acct_no";
      ds.Tables[0].Columns[3].ColumnName = "acct_status";
      ds.Tables[0].Columns[4].ColumnName = "acct_closed";
      ds.Tables[0].Columns[5].ColumnName = "last_name";
      ds.Tables[0].Columns[6].ColumnName = "first_name";
      ds.Tables[0].Columns[7].ColumnName = "n_join";
      ds.Tables[0].Columns[8].ColumnName = "first_name1";
      ds.Tables[0].Columns[9].ColumnName = "last_name1";
      ds.Tables[0].Columns[10].ColumnName = "remarks";
      ds.Tables[0].Columns[11].ColumnName = "address_1";
      ds.Tables[0].Columns[12].ColumnName = "address_2";
      ds.Tables[0].Columns[13].ColumnName = "acct_date";
      ds.Tables[0].Columns[14].ColumnName = "birth_date";
      ds.Tables[0].Columns[15].ColumnName = "occ_emp";
      ds.Tables[0].Columns[16].ColumnName = "active_balance";
      ds.Tables[0].Columns[17].ColumnName = "acct_balance";
      ds.Tables[0].Columns[18].ColumnName = "close_date";
      return ds;

    }

    public static int Delete_Info_For_Close_Account(DVOSBclients objDtldel)
    {
      object[] Delparameter = new object[3];
      Delparameter[0] = objDtldel.acct_cat;
      Delparameter[1] = objDtldel.acct_no;
      Delparameter[2] = objDtldel.acct_status;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object s = objDalBaseClass.DeleteData(ref Delparameter, typeof(DVOSBclients), objDtldel.DELETE_FOR_CLOSE_SAVING_BANK_ACCOUNT);
      int y = Convert.ToInt32(s);
      if (y == 1)
      {
        return y;
      }
      return y;
    }

    public static DataSet Get_p_month_Info_For_Close_Account()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSBclients));
      return ds;
    }

    //Added By Rahul Jain on 06-05-2009 get All Purchasing default data from stucntrc table for  

    public static DataSet GetPurchasingDefaultInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSPurchaisngDefaults = objDalBaseClass.GetAllData(typeof(DVOPurchasingDefault));
      if (DSPurchaisngDefaults.Tables[0].Rows.Count >= 1)
      {
        DSPurchaisngDefaults.Tables[0].Columns[0].ColumnName = "RowID";
        DSPurchaisngDefaults.Tables[0].Columns[1].ColumnName = "buyer_code";
        DSPurchaisngDefaults.Tables[0].Columns[2].ColumnName = "price_tolerance";
        DSPurchaisngDefaults.Tables[0].Columns[3].ColumnName = "po_type";
        DSPurchaisngDefaults.Tables[0].Columns[4].ColumnName = "line_type";
        DSPurchaisngDefaults.Tables[0].Columns[5].ColumnName = "whse_shipto";
        DSPurchaisngDefaults.Tables[0].Columns[6].ColumnName = "ship_via";
        DSPurchaisngDefaults.Tables[0].Columns[7].ColumnName = "fob_point";
        DSPurchaisngDefaults.Tables[0].Columns[8].ColumnName = "print_notes";
        DSPurchaisngDefaults.Tables[0].Columns[9].ColumnName = "mtaxg_code";
        DSPurchaisngDefaults.Tables[0].Columns[10].ColumnName = "exempt_tax_code";
        DSPurchaisngDefaults.Tables[0].Columns[11].ColumnName = "misc_tax_code";
        DSPurchaisngDefaults.Tables[0].Columns[12].ColumnName = "frght_tax_code";
        DSPurchaisngDefaults.Tables[0].Columns[13].ColumnName = "ap_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[14].ColumnName = "diff_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[15].ColumnName = "inv_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[16].ColumnName = "misc_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[17].ColumnName = "disc_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[18].ColumnName = "supp_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[19].ColumnName = "frght_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[20].ColumnName = "adj_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[21].ColumnName = "non_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[22].ColumnName = "cap_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[23].ColumnName = "cash_acct_no";
        //Keyvalues
        DSPurchaisngDefaults.Tables[0].Columns[24].ColumnName = "ap_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[25].ColumnName = "diff_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[26].ColumnName = "inv_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[27].ColumnName = "misc_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[28].ColumnName = "disc_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[29].ColumnName = "supp_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[30].ColumnName = "frght_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[31].ColumnName = "adj_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[32].ColumnName = "non_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[33].ColumnName = "cap_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[34].ColumnName = "cash_keyvalue";
        //Account Type
        DSPurchaisngDefaults.Tables[0].Columns[35].ColumnName = "ap_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[36].ColumnName = "diff_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[37].ColumnName = "inv_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[38].ColumnName = "misc_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[39].ColumnName = "disc_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[40].ColumnName = "supp_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[41].ColumnName = "frght_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[42].ColumnName = "adj_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[43].ColumnName = "non_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[44].ColumnName = "cap_acct_type";
        DSPurchaisngDefaults.Tables[0].Columns[45].ColumnName = "cash_acct_type";
        //
        DSPurchaisngDefaults.Tables[0].Columns[46].ColumnName = "req_doc_no";
        DSPurchaisngDefaults.Tables[0].Columns[47].ColumnName = "req_post_no";
        DSPurchaisngDefaults.Tables[0].Columns[48].ColumnName = "po_doc_no";
        DSPurchaisngDefaults.Tables[0].Columns[49].ColumnName = "rec_doc_no";
        DSPurchaisngDefaults.Tables[0].Columns[50].ColumnName = "rec_post_no";
        DSPurchaisngDefaults.Tables[0].Columns[51].ColumnName = "use_batch_rec";
        DSPurchaisngDefaults.Tables[0].Columns[52].ColumnName = "inv_post_no";
        DSPurchaisngDefaults.Tables[0].Columns[53].ColumnName = "inv_doc_no";
        DSPurchaisngDefaults.Tables[0].Columns[54].ColumnName = "use_batch_inv";
        DSPurchaisngDefaults.Tables[0].Columns[55].ColumnName = "use_approv_post";
        DSPurchaisngDefaults.Tables[0].Columns[56].ColumnName = "approval_code";
        DSPurchaisngDefaults.Tables[0].Columns[57].ColumnName = "cpu_acct_no";
        DSPurchaisngDefaults.Tables[0].Columns[58].ColumnName = "cpu_keyvalue";
        DSPurchaisngDefaults.Tables[0].Columns[59].ColumnName = "cpu_acct_type";

      }
      return DSPurchaisngDefaults;
    }

    //Added By Rahul Jain On 09/05/2009 for getting Warehouse Type Details for Report
    public static DataSet GetWarehouseDetails(ref DVOWarehousestiwhser objDVOWarehousestiwhser)
    {
      object[] parameters = new object[10];

      parameters[0] = objDVOWarehousestiwhser.whse_code;
      parameters[1] = objDVOWarehousestiwhser.description;
      parameters[2] = objDVOWarehousestiwhser.address1;
      parameters[3] = objDVOWarehousestiwhser.address2;
      parameters[4] = objDVOWarehousestiwhser.city;
      parameters[5] = objDVOWarehousestiwhser.state;
      parameters[6] = objDVOWarehousestiwhser.zip;
      parameters[7] = objDVOWarehousestiwhser.country;
      parameters[8] = objDVOWarehousestiwhser.phone;
      parameters[9] = objDVOWarehousestiwhser.RowID;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet DSWarehouse = objDalBaseClass.GetData(ref parameters, typeof(DVOWarehousestiwhser));
      return DSWarehouse;
    }

    //*************************************************************





    //Added by Sunil Pahwa for Print Line Type Def Report on 9/5/09
    public static DataSet Get_Line_Type_Definition(DVOLineTypestultypr objDvoLineTypeget)
    {
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = objDvoLineTypeget.line_type;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOLineTypestultypr), objDvoLineTypeget.GET_LINE_TYPE_DEFINITION_DTL);
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "line_type";
          ds.Tables[0].Columns[1].ColumnName = "line_desc";
          ds.Tables[0].Columns[2].ColumnName = "gl_acct_no";
          ds.Tables[0].Columns[3].ColumnName = "line_item_type";
          ds.Tables[0].Columns[4].ColumnName = "update_desc";
          ds.Tables[0].Columns[5].ColumnName = "update_price";

          ds.Tables[0].Columns[6].ColumnName = "acct_type";
          ds.Tables[0].Columns[7].ColumnName = "keyvalue";
          ds.Tables[0].Columns[8].ColumnName = "acct_desc";
          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();
    }

    //Added by Sunil Pahwa for Print Requestor Information
    public static DataSet Get_Requestor_Info(DVORequestorInfoSturqsor objDvoReqInfoSturqsor)
    {
      Object[] parameters = new object[5];
      parameters[0] = objDvoReqInfoSturqsor.requestor_code;
      parameters[1] = objDvoReqInfoSturqsor.request_desc;
      parameters[2] = objDvoReqInfoSturqsor.approval_level;
      parameters[3] = string.Empty;
      parameters[4] = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORequestorInfoSturqsor));
      return ds;

    }
    //Added by Sunil Pahwa
    public static DataSet Get_Non_Inventory_Items(DVOItemCatalogstiinvtr objDvoNonInventoryItems)
    {
      Object[] parameters = new object[9];
      parameters[0] = objDvoNonInventoryItems.item_code;
      parameters[8] = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDvoNonInventoryItems.FIND_QUERY1(ref parameters));
      return ds;
    }

    //Added By Rahul Jain On 15/05/2009 for getting Item Catalog Report
    public static DataSet GetItemCatalogDetail(ref DVOItemCatalogstiinvtr objDVOItemCatalogstiinvtr)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOItemCatalogstiinvtr.item_code;
        parameters[1] = objDVOItemCatalogstiinvtr.item_class;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet DSItemCatalog = objDalBaseClass.GetData(objDVOItemCatalogstiinvtr.FINDQUERY_ITEM_CATALOG_INFO(ref parameters));//, typeof(DVOItemCatalogstiinvtr), objDVOItemCatalogstiinvtr.ITEM_CATALOG_GET);
        if (DSItemCatalog != null && DSItemCatalog.Tables.Count > 0)
        {
          DSItemCatalog.Tables[0].TableName = "DSItemCatalog";
          DSItemCatalog.Tables[0].Columns[0].ColumnName = "desc1";
          DSItemCatalog.Tables[0].Columns[1].ColumnName = "desc2";
          DSItemCatalog.Tables[0].Columns[2].ColumnName = "item_code";
          DSItemCatalog.Tables[0].Columns[3].ColumnName = "item_class";
          DSItemCatalog.Tables[0].Columns[4].ColumnName = "item_type";
          DSItemCatalog.Tables[0].Columns[5].ColumnName = "bus_name";
          DSItemCatalog.Tables[0].Columns[6].ColumnName = "cost";
          DSItemCatalog.Tables[0].Columns[7].ColumnName = "vend_item_code";
          DSItemCatalog.Tables[0].Columns[8].ColumnName = "vendor_code";
          DSItemCatalog.Tables[0].Columns[9].ColumnName = "contact";
          DSItemCatalog.Tables[0].Columns[10].ColumnName = "phone";
          return DSItemCatalog;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();


    }
    //*************************************************************
    //Added By Rahul Jain On 15/05/2009 for getting Vendor Catalog Report
    public static DataSet GetVednorCatalogDetail(ref DVOCatalogDetailstuctlgd objDVOCatalogDetailstuctlgd)
    {
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = objDVOCatalogDetailstuctlgd.vendor_code;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet DSItemCatalog = objDalBaseClass.GetData(ref parameters, typeof(DVOCatalogDetailstuctlgd), objDVOCatalogDetailstuctlgd.VENDOR_CATALOG_GET);
        if (DSItemCatalog != null && DSItemCatalog.Tables.Count > 0)
        {
          DSItemCatalog.Tables[0].TableName = "DSItemCatalog";
          DSItemCatalog.Tables[0].Columns[0].ColumnName = "desc1";
          DSItemCatalog.Tables[0].Columns[1].ColumnName = "desc2";
          DSItemCatalog.Tables[0].Columns[2].ColumnName = "item_code";
          DSItemCatalog.Tables[0].Columns[3].ColumnName = "item_class";
          DSItemCatalog.Tables[0].Columns[4].ColumnName = "item_type";
          DSItemCatalog.Tables[0].Columns[5].ColumnName = "bus_name";
          DSItemCatalog.Tables[0].Columns[6].ColumnName = "cost";
          DSItemCatalog.Tables[0].Columns[7].ColumnName = "vend_item_code";
          DSItemCatalog.Tables[0].Columns[8].ColumnName = "vendor_code";
          DSItemCatalog.Tables[0].Columns[9].ColumnName = "contact";
          DSItemCatalog.Tables[0].Columns[10].ColumnName = "phone";
          return DSItemCatalog;
        }
        else
          throw new Exception();

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();

    }
    //*************************************************************
    //Added by Sunil Pahwa
    public static DataSet Get_Delivery_Slip_info(DVOPOPurchaseOrdersDetailStuordrd objDVOPOPurchaseOrdersDetailStuordrd)
    {
      Object[] parameters = new object[1];
      parameters[0] = objDVOPOPurchaseOrdersDetailStuordrd.po_no;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSDeliverySlip = objDalBaseClass.GetData(ref parameters, typeof(DVOPOPurchaseOrdersDetailStuordrd), objDVOPOPurchaseOrdersDetailStuordrd.GET_DELIVERY_SLIP_DETAIL);
      if (DSDeliverySlip.Tables.Count > 0)
      {

        DSDeliverySlip.Tables[0].Columns[0].ColumnName = "cost";
        DSDeliverySlip.Tables[0].Columns[1].ColumnName = "desc1";
        DSDeliverySlip.Tables[0].Columns[2].ColumnName = "item_code";
        DSDeliverySlip.Tables[0].Columns[3].ColumnName = "vend_code";
        DSDeliverySlip.Tables[0].Columns[4].ColumnName = "whse_shipto";
        DSDeliverySlip.Tables[0].Columns[5].ColumnName = "rec_line_no";
        DSDeliverySlip.Tables[0].Columns[6].ColumnName = "recv_qty";
        DSDeliverySlip.Tables[0].Columns[7].ColumnName = "po_no";
        DSDeliverySlip.Tables[0].Columns[8].ColumnName = "rec_doc_no";
        DSDeliverySlip.Tables[0].Columns[9].ColumnName = "receipt_date";
        DSDeliverySlip.Tables[0].Columns[10].ColumnName = "receipt_ref";
        // DSDeliverySlip.Tables[0].Columns[11].ColumnName = "SearchCriteria";
      }
      return DSDeliverySlip;
    }
    //Added by Sunil Pahwa
    //public static DataSet Get_Order_Summary_By_Po_No_Info(DVOPOPurchaseOrdersStuordre objDVOPOPurchaseOrdrsStuordre)
    //{
    //    Object[] parameters = new object[10];
    //    parameters[0] = objDVOPOPurchaseOrdrsStuordre.vend_code;
    //    parameters[1] = objDVOPOPurchaseOrdrsStuordre.bus_name;
    //    parameters[2] = objDVOPOPurchaseOrdrsStuordre.buyer_code;
    //    parameters[3] = objDVOPOPurchaseOrdrsStuordre.whse_shipto;
    //    parameters[4] = objDVOPOPurchaseOrdrsStuordre.po_no;
    //    parameters[5] = objDVOPOPurchaseOrdrsStuordre.doc_no;
    //    if (!objDVOPOPurchaseOrdrsStuordre.required_date.Contains("1900"))
    //    {
    //        parameters[6] = objDVOPOPurchaseOrdrsStuordre.required_date;
    //    }
    //    else
    //    {
    //        parameters[6] = string.Empty;
    //    }

    //    parameters[7] = objDVOPOPurchaseOrdrsStuordre.po_type;
    //    parameters[8] = objDVOPOPurchaseOrdrsStuordre.po_status;
    //    parameters[9] = objDVOPOPurchaseOrdrsStuordre.po_stage;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet DSOrderSummaryByPoNo = objDalBaseClass.GetData(objDVOPOPurchaseOrdrsStuordre.FIND_ORDER_SUMMARY_BY_PO_NO(ref parameters));
    //    if (DSOrderSummaryByPoNo.Tables.Count > 0)
    //    {

    //        DSOrderSummaryByPoNo.Tables[0].Columns[0].ColumnName = "cost";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[1].ColumnName = "netprice";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[2].ColumnName = "ordr_qty";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[3].ColumnName = "recv_qty";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[4].ColumnName = "bus_name";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[5].ColumnName = "buyer_code";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[6].ColumnName = "currency_code";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[7].ColumnName = "currency_rate";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[8].ColumnName = "po_date";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[9].ColumnName = "po_no";
    //        DSOrderSummaryByPoNo.Tables[0].Columns[10].ColumnName = "vend_code";

    //    }
    //    return DSOrderSummaryByPoNo;
    //}
    /// <summary>
    /// This fuction returns DS to report. 
    /// Form: Print Requisition  Added: Shrishanshu
    /// </summary>
    /// <param name="objdvo">DVO's Properties</param>
    /// <returns>Dataset</returns>
    public static DataSet GetRequitionReport(DVORequestorInfoSturqsor objdvo)
    {
      Object[] param = new Object[7];
      param[0] = objdvo.request_no;
      param[1] = objdvo.request_date;
      param[2] = objdvo.requestor_code;
      param[3] = objdvo.request_status;
      param[4] = objdvo.authorization_code;
      param[5] = objdvo.whse_billto;
      param[6] = objdvo.whse_shipto;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsRequitionReport = objDalBaseClass.GetData(objdvo.FIND_RptData(ref param));
      return dsRequitionReport;

    }

    //public static DataSet Get_Order_Detail_Info(DVOPOPurchaseOrdersStuordre objDVOPOPurchaseOrdrsStuordr)
    //{
    //    Object[] parameters = new object[14];
    //    parameters[0] = objDVOPOPurchaseOrdrsStuordr.vend_code;
    //    parameters[1] = objDVOPOPurchaseOrdrsStuordr.bus_name;
    //    parameters[2] = objDVOPOPurchaseOrdrsStuordr.buyer_code;
    //    parameters[3] = objDVOPOPurchaseOrdrsStuordr.whse_shipto;
    //    parameters[4] = objDVOPOPurchaseOrdrsStuordr.po_no;
    //    parameters[5] = objDVOPOPurchaseOrdrsStuordr.doc_no;
    //    if (!objDVOPOPurchaseOrdrsStuordr.required_date.Contains("1900"))
    //    {
    //        parameters[6] = objDVOPOPurchaseOrdrsStuordr.required_date;
    //    }
    //    else
    //    {
    //        parameters[6] = string.Empty;
    //    }

    //    parameters[7] = objDVOPOPurchaseOrdrsStuordr.po_type;
    //    parameters[8] = objDVOPOPurchaseOrdrsStuordr.po_status;
    //    parameters[9] = objDVOPOPurchaseOrdrsStuordr.po_stage;
    //    parameters[10] = objDVOPOPurchaseOrdrsStuordr.gl_acct_no;
    //    parameters[11] = objDVOPOPurchaseOrdrsStuordr.item_code;
    //    parameters[12] = objDVOPOPurchaseOrdrsStuordr.acct_type;
    //    parameters[13] = objDVOPOPurchaseOrdrsStuordr.keyvalue;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet DSOrderDetail = objDalBaseClass.GetData(objDVOPOPurchaseOrdrsStuordr.FIND_ORDER_DETAIL_INFO(ref parameters));
    //    if (DSOrderDetail.Tables.Count > 0)
    //    {

    //        DSOrderDetail.Tables[0].Columns[0].ColumnName = "cost";
    //        DSOrderDetail.Tables[0].Columns[1].ColumnName = "desc1";
    //        DSOrderDetail.Tables[0].Columns[2].ColumnName = "item_code";
    //        DSOrderDetail.Tables[0].Columns[3].ColumnName = "line_stage";
    //        DSOrderDetail.Tables[0].Columns[4].ColumnName = "line_type";
    //        DSOrderDetail.Tables[0].Columns[5].ColumnName = "net_price";
    //        DSOrderDetail.Tables[0].Columns[6].ColumnName = "ordr_qty";
    //        DSOrderDetail.Tables[0].Columns[7].ColumnName = "purch_unit";
    //        DSOrderDetail.Tables[0].Columns[8].ColumnName = "recv_qty";
    //        DSOrderDetail.Tables[0].Columns[9].ColumnName = "bus_name";
    //        DSOrderDetail.Tables[0].Columns[10].ColumnName = "buyer_code";

    //        DSOrderDetail.Tables[0].Columns[11].ColumnName = "currency_code";
    //        DSOrderDetail.Tables[0].Columns[12].ColumnName = "currency_rate";
    //        DSOrderDetail.Tables[0].Columns[13].ColumnName = "doc_no";
    //        DSOrderDetail.Tables[0].Columns[14].ColumnName = "order_reference";

    //        DSOrderDetail.Tables[0].Columns[15].ColumnName = "po_date";
    //        DSOrderDetail.Tables[0].Columns[16].ColumnName = "po_no";
    //        DSOrderDetail.Tables[0].Columns[17].ColumnName = "po_stage";
    //        DSOrderDetail.Tables[0].Columns[18].ColumnName = "po_status";
    //        DSOrderDetail.Tables[0].Columns[19].ColumnName = "po_type";

    //        DSOrderDetail.Tables[0].Columns[20].ColumnName = "required_date";
    //        DSOrderDetail.Tables[0].Columns[21].ColumnName = "vend_code";
    //        DSOrderDetail.Tables[0].Columns[22].ColumnName = "whse_shipto";
    //        DSOrderDetail.Tables[0].Columns[23].ColumnName = "exp_rec_qty";
    //        DSOrderDetail.Tables[0].Columns[24].ColumnName = "gl_acct_no";
    //        DSOrderDetail.Tables[0].Columns[25].ColumnName = "keyvalue";
    //        DSOrderDetail.Tables[0].Columns[26].ColumnName = "acct_desc";
    //    }

    //    return DSOrderDetail;

    //}

    //public static DataSet Get_UnPaid_Order_Detail_Info(ref DVOPOPurchaseOrdersStuordre objDVOPOPurchaseOrdersStuordre)
    //{
    //    object[] Parameter = new object[2];
    //    Parameter[0] = objDVOPOPurchaseOrdersStuordre.TopaydateFrom;
    //    Parameter[1] = objDVOPOPurchaseOrdersStuordre.TopaydateTo;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    //DataSet DSOrderDetail = objDalBaseClass.GetData((new DVOPOPurchaseOrdersStuordre()).FIND_UNPAID_ORDER_DETAIL_INFO());
    //    DataSet DSOrderDetail = objDalBaseClass.GetData(objDVOPOPurchaseOrdersStuordre.FIND_UNPAID_ORDER_DETAIL_INFO(ref Parameter));//ref Parameter, typeof(DVOPOPurchaseOrdersStuordre), objDVOPOPurchaseOrdersStuordre.FIND_UNPAID_ORDER_DETAIL_INFO);
    //    if (DSOrderDetail != null && DSOrderDetail.Tables.Count > 0 && DSOrderDetail.Tables[0].Columns.Count >= 27)
    //    {
    //        DSOrderDetail.Tables[0].Columns[0].ColumnName = "cost";
    //        DSOrderDetail.Tables[0].Columns[1].ColumnName = "desc1";
    //        DSOrderDetail.Tables[0].Columns[2].ColumnName = "item_code";
    //        DSOrderDetail.Tables[0].Columns[3].ColumnName = "line_stage";
    //        DSOrderDetail.Tables[0].Columns[4].ColumnName = "line_type";
    //        DSOrderDetail.Tables[0].Columns[5].ColumnName = "net_price";
    //        DSOrderDetail.Tables[0].Columns[6].ColumnName = "ordr_qty";
    //        DSOrderDetail.Tables[0].Columns[7].ColumnName = "purch_unit";
    //        DSOrderDetail.Tables[0].Columns[8].ColumnName = "recv_qty";
    //        DSOrderDetail.Tables[0].Columns[9].ColumnName = "bus_name";
    //        DSOrderDetail.Tables[0].Columns[10].ColumnName = "buyer_code";

    //        DSOrderDetail.Tables[0].Columns[11].ColumnName = "currency_code";
    //        DSOrderDetail.Tables[0].Columns[12].ColumnName = "currency_rate";
    //        DSOrderDetail.Tables[0].Columns[13].ColumnName = "doc_no";
    //        DSOrderDetail.Tables[0].Columns[14].ColumnName = "order_reference";

    //        DSOrderDetail.Tables[0].Columns[15].ColumnName = "po_date";
    //        DSOrderDetail.Tables[0].Columns[16].ColumnName = "po_no";
    //        DSOrderDetail.Tables[0].Columns[17].ColumnName = "po_stage";
    //        DSOrderDetail.Tables[0].Columns[18].ColumnName = "po_status";
    //        DSOrderDetail.Tables[0].Columns[19].ColumnName = "po_type";

    //        DSOrderDetail.Tables[0].Columns[20].ColumnName = "required_date";
    //        DSOrderDetail.Tables[0].Columns[21].ColumnName = "vend_code";
    //        DSOrderDetail.Tables[0].Columns[22].ColumnName = "whse_shipto";
    //        DSOrderDetail.Tables[0].Columns[23].ColumnName = "exp_rec_qty";
    //        DSOrderDetail.Tables[0].Columns[24].ColumnName = "gl_acct_no";
    //        DSOrderDetail.Tables[0].Columns[25].ColumnName = "keyvalue";
    //        DSOrderDetail.Tables[0].Columns[26].ColumnName = "acct_desc";
    //    }
    //    else
    //        return new DataSet();
    //    return DSOrderDetail;
    //}

    //public static DataSet Get_Goods_Rec_By_GL_Code_Info(DVOPOPurchaseOrdersStuordre obDVOPOPurchaseOrdrsStuordr)
    //{

    //    Object[] parameters = new object[6];

    //    parameters[0] = obDVOPOPurchaseOrdrsStuordr.po_no;
    //    parameters[1] = obDVOPOPurchaseOrdrsStuordr.gl_acct_no;
    //    parameters[2] = obDVOPOPurchaseOrdrsStuordr.item_code;
    //    if (!obDVOPOPurchaseOrdrsStuordr.receipt_date.Contains("1900"))
    //        parameters[3] = obDVOPOPurchaseOrdrsStuordr.receipt_date; 
    //    else
    //        parameters[3] = string.Empty;
    //    parameters[4] = obDVOPOPurchaseOrdrsStuordr.acct_type;
    //    parameters[5] = obDVOPOPurchaseOrdrsStuordr.keyvalue;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet DSGoodsRecByGLCode = objDalBaseClass.GetData((new DVOPOPurchaseOrdersDetailStuordrd()).FIND_GOODS_REC_BY_GL_CODE(ref parameters));
    //    if (DSGoodsRecByGLCode.Tables.Count > 0)
    //    {

    //        DSGoodsRecByGLCode.Tables[0].Columns[0].ColumnName = "gl_acct_no";
    //        DSGoodsRecByGLCode.Tables[0].Columns[1].ColumnName = "department";
    //        DSGoodsRecByGLCode.Tables[0].Columns[2].ColumnName = "po_no";
    //        DSGoodsRecByGLCode.Tables[0].Columns[3].ColumnName = "receipt_date";
    //        DSGoodsRecByGLCode.Tables[0].Columns[4].ColumnName = "item_code";
    //        DSGoodsRecByGLCode.Tables[0].Columns[5].ColumnName = "recv_qty";
    //        DSGoodsRecByGLCode.Tables[0].Columns[6].ColumnName = "cost";
    //        DSGoodsRecByGLCode.Tables[0].Columns[7].ColumnName = "currrency_code";
    //        DSGoodsRecByGLCode.Tables[0].Columns[8].ColumnName = "currency_rate";
    //        DSGoodsRecByGLCode.Tables[0].Columns[9].ColumnName = "keyvalue";
    //        DSGoodsRecByGLCode.Tables[0].Columns[10].ColumnName = "acct_desc";
    //    }

    //    return DSGoodsRecByGLCode;
    //}

    //*****************************************************************************************
    //******************************* Added by Bharat Dhall [06/04/2009] **********************
    //*****************************************************************************************
    /// <summary>
    /// to get data for PaySlipA4 
    /// </summary>
    /// <param name="Keyvalue"></param>
    /// <param name="EmployeeType"></param>
    /// <returns>Dataset with 3 tables for 1.Header, 2.Incomes & 3.Deductions</returns>
    public static DataSet GetPaySlipsA4Info(string Keyvalue, string EmployeeType, string PayDate, ref DVOPYBatchProcessStybatchr pObjBatch)
    {

      /*
       Added by Sarvjeet on 22/10/2010
       To implemented Payroll batch process into Show Pay SlipsA4. Nedd to add
       A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'GetPaySlipsA4Info' function
       and remove comment from the code written for batch process logic.   
     */

      Object[] parameters = new object[4];
      parameters[0] = Keyvalue.Length >= 3 ? Keyvalue.Substring(0, 3) : Keyvalue;
      parameters[1] = 0;
      parameters[2] = EmployeeType;
      parameters[3] = PayDate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSPaySlipA4 = new DataSet();
      //Added by Sarvjeet on 22/01/2010..
      #region Declare Variables for Batch Process
      StringBuilder errorMassage = new StringBuilder();
      DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      int recordsSearched = 0;
      int recordsProcessed = 0;
      bool IsProessIns = false;
      #endregion
      try
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Insert Process Start Info..
        object objTrx = null;
        objProcessDtl.pybatchid = pObjBatch.pybatchid;
        objProcessDtl.processname = "Show Pay SlipsA4";
        objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
        BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
        IsProessIns = true;
        #endregion

        DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
        DataSet ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_HEADER);
        if (ds != null)
          if (ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[9].ColumnName = "doc_no";
            ds.Tables[0].Columns[10].ColumnName = "empl_code";

            ds.Tables[0].DefaultView.Sort = "empl_code,doc_no";
            DataTable dttemp = ds.Tables[0].DefaultView.ToTable();
            DataTable dt = new DataTable();
            dt.TableName = "Header";
            dt.Columns.Add("first_name");
            dt.Columns.Add("last_name");
            dt.Columns.Add("middle_name");
            dt.Columns.Add("empl_code");
            dt.Columns.Add("eop_date", typeof(DateTime));
            dt.Columns.Add("keyvalue");
            dt.Columns.Add("bank1");
            dt.Columns.Add("deposit1", typeof(Decimal));
            dt.Columns.Add("bank2");
            dt.Columns.Add("deposit2", typeof(Decimal));
            dt.Columns.Add("bank3");
            dt.Columns.Add("deposit3", typeof(Decimal));
            dt.Columns.Add("bank4");
            dt.Columns.Add("deposit4", typeof(Decimal));
            dt.Columns.Add("bank5");
            dt.Columns.Add("deposit5");
            dt.Columns.Add("doc_no", typeof(Int32));
            dt.Columns.Add("dd_create");
            dt.Columns.Add("pay_date", typeof(DateTime));
            dt.Columns.Add("check_no");
            dt.Columns.Add("mailid");
            recordsSearched = ds.Tables[0].Rows.Count;
            int er = 1;
            DataRow drh = dt.NewRow();
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
              DataRow dr = dttemp.Rows[i];

              if (i == 0 || dttemp.Rows[i - 1]["empl_code"].ToString().Trim() != dr["empl_code"].ToString().Trim() || dttemp.Rows[i - 1]["doc_no"].ToString().Trim() != dr["doc_no"].ToString().Trim())
              {
                er = 1;
                drh["first_name"] = dr[2];
                drh["last_name"] = dr[4];
                drh["middle_name"] = dr[5];
                drh["empl_code"] = dr["empl_code"];
                drh["eop_date"] = dr[11];

                DVOMasterEmployee objtmp = new DVOMasterEmployee();
                objtmp.EmplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                objtmp.FlexDeptAcctType = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                drh["keyvalue"] = BLLMasterEmployee.GetFlexDeptKeyvalue(ref objtmp);
                objtmp.Dispose();

                drh["doc_no"] = dr["doc_no"];
                drh["bank1"] = dr[0];
                drh["deposit1"] = dr[13];
                drh["dd_create"] = dr[1];
                drh["pay_date"] = dr[12];
                drh["check_no"] = dr[7];
                drh["mailid"] = dr[14];
              }
              else
              {
                er++;
                if (er <= 5)
                {
                  drh["bank" + er.ToString()] = dr[0];
                  drh["deposit" + er.ToString()] = dr[13];
                  drh["mailid"] = dr[14];
                }
              }

              if (i == dttemp.Rows.Count - 1 || dttemp.Rows[i + 1]["empl_code"].ToString().Trim() != dr["empl_code"].ToString().Trim() || dttemp.Rows[i + 1]["doc_no"].ToString().Trim() != dr["doc_no"].ToString().Trim())
              {
                dt.Rows.Add(drh);
                drh = dt.NewRow();
              }
              recordsProcessed++;
            }
            dttemp.Dispose();
            DataTable dtnew = dt.Clone();
            if (Keyvalue.Trim() != string.Empty)
            {
              DataRow[] dtrows = dt.Select("keyvalue LIKE '" + Keyvalue + "%'");
              foreach (DataRow dtr in dtrows)
                dtnew.ImportRow(dtr);
              dt.Dispose();

              DSPaySlipA4.Tables.Add(dtnew);
            }
            else
              DSPaySlipA4.Tables.Add(dt);
          }
        if (DSPaySlipA4.Tables["Header"].Rows.Count > 0)
        {
          ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_INCOMES);
          if (ds != null)
            if (ds.Tables.Count > 0)
            {
              ds.Tables[0].Columns[0].ColumnName = "line_no";
              ds.Tables[0].Columns[1].ColumnName = "inc_code";
              ds.Tables[0].Columns[2].ColumnName = "number";
              ds.Tables[0].Columns[3].ColumnName = "hours";
              ds.Tables[0].Columns[4].ColumnName = "inc_prd";
              ds.Tables[0].Columns[5].ColumnName = "inc_ytd";
              ds.Tables[0].Columns[6].ColumnName = "inc_type";
              ds.Tables[0].Columns[7].ColumnName = "inc_rate";
              ds.Tables[0].Columns[8].ColumnName = "empl_code";
              ds.Tables[0].Columns[9].ColumnName = "doc_no";

              foreach (DataRow dr in DSPaySlipA4.Tables["Header"].Rows)
              {
                string _emplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                string _docNo = dr["doc_no"] != DBNull.Value ? dr["doc_no"].ToString() : "0";

                DataRow[] drs = ds.Tables[0].Select("empl_code='" + _emplCode + "' AND doc_no=" + _docNo);
                DataTable dts = ds.Tables[0].Clone();
                foreach (DataRow drs1 in drs)
                  dts.ImportRow(drs1);
                DataTable dts2 = dts.DefaultView.ToTable(true, "inc_code");

                if (dts2.Rows.Count < 8)
                {
                  for (int i = 0; i < 8 - dts2.Rows.Count; i++)
                  {
                    DataRow drn = ds.Tables[0].NewRow();
                    drn["doc_no"] = dr["doc_no"];
                    drn["inc_code"] = (new String('.', i));
                    //drn["inc_prd"] = 0;
                    //drn["inc_ytd"] = 0;
                    ds.Tables[0].Rows.Add(drn);
                  }
                }
                else if (dts2.Rows.Count > 8)
                {
                  string _exIncCodes = string.Empty;
                  for (int k = 7; k < dts2.Rows.Count; k++)
                    _exIncCodes += dts2.Rows[k]["inc_code"].ToString().Trim() + "||";

                  foreach (DataRow drDed in ds.Tables[0].Rows)
                  {
                    if (_exIncCodes.IndexOf(drDed["inc_code"].ToString().Trim()) >= 0 && drDed["empl_code"].ToString().Trim() == _emplCode && drDed["doc_no"].ToString().Trim() == _docNo)
                      drDed["inc_code"] = " OTHERS";
                  }
                }
                dts2.Dispose();
                dts.Dispose();
                drs = null;
              }

              ds.Tables[0].DefaultView.Sort = "empl_code,doc_no,inc_code desc";

              DataTable dt = ds.Tables[0].DefaultView.ToTable();
              dt.TableName = "Incomes";
              DSPaySlipA4.Tables.Add(dt);
            }

          ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_DEDUCTIONS);
          if (ds != null)
            if (ds.Tables.Count > 0)
            {
              ds.Tables[0].Columns[0].ColumnName = "line_no";
              ds.Tables[0].Columns[1].ColumnName = "ded_code";
              ds.Tables[0].Columns[2].ColumnName = "ded_prd";
              ds.Tables[0].Columns[3].ColumnName = "ded_ytd";
              ds.Tables[0].Columns[4].ColumnName = "empl_code";
              ds.Tables[0].Columns[5].ColumnName = "doc_no";

              foreach (DataRow dr in DSPaySlipA4.Tables["Header"].Rows)
              {
                string _emplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                string _docNo = dr["doc_no"] != DBNull.Value ? dr["doc_no"].ToString() : "0";

                DataRow[] drs = ds.Tables[0].Select("empl_code='" + _emplCode + "' AND doc_no=" + _docNo);
                DataTable dts = ds.Tables[0].Clone();

                foreach (DataRow drs1 in drs)
                  dts.ImportRow(drs1);
                DataTable dts2 = dts.DefaultView.ToTable(true, "ded_code", "empl_code", "doc_no");

                if (dts2.Rows.Count < 8)
                {
                  for (int i = 0; i < 8 - dts2.Rows.Count; i++)
                  {
                    DataRow drn = ds.Tables[0].NewRow();
                    drn["doc_no"] = dr["doc_no"];
                    drn["ded_code"] = (new String('.', i));
                    //drn["ded_prd"] = 0;
                    //drn["ded_ytd"] = 0;
                    ds.Tables[0].Rows.Add(drn);
                  }
                }
                else if (dts2.Rows.Count > 8)
                {
                  string _exDedCodes = string.Empty;
                  for (int k = 7; k < dts2.Rows.Count; k++)
                    _exDedCodes += dts2.Rows[k]["ded_code"].ToString().Trim() + "||";

                  foreach (DataRow drDed in ds.Tables[0].Rows)
                  {
                    if (_exDedCodes.IndexOf(drDed["ded_code"].ToString().Trim()) >= 0 && drDed["empl_code"].ToString().Trim() == _emplCode && drDed["doc_no"].ToString().Trim() == _docNo)
                      drDed["ded_code"] = " OTHERS";
                  }
                }
                dts2.Dispose();
                dts.Dispose();
                drs = null;
              }

              ds.Tables[0].DefaultView.Sort = "empl_code,doc_no,ded_code desc";

              DataTable dt = ds.Tables[0].DefaultView.ToTable();
              dt.TableName = "Deductions";
              DSPaySlipA4.Tables.Add(dt);
            }
          ds.Dispose();
        }
        else
        {
          return DSPaySlipA4;
        }
      }
      catch (Exception ex)
      {
        errorMassage.Append("[" + ex.Message + "]");
        ExceptionManager.Publish(ex);
        DSPaySlipA4 = new DataSet();
      }
      finally
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Record process detail..
        if (IsProessIns)
        {
          List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
          object objTrx = null;
          objProcessDtl.recordssearched = recordsSearched;
          objProcessDtl.recordsprocessed = recordsProcessed;
          objProcessDtl.status = 1;
          objProcessDtl.errormessage = errorMassage.ToString();
          objList.Add(objProcessDtl);
          BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
        }
        else
        {
          object objTrx = null;
          List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
          DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
          obj.pybatchid = pObjBatch.pybatchid;
          obj.processname = "Show Pay SlipsA4";
          //obj.processstartedon = pObjBatch.startedon;
          //obj.processendedon = pObjBatch.endedon;
          obj.recordssearched = recordsSearched;
          obj.recordsprocessed = recordsProcessed;
          obj.status = 1;
          obj.searchcriteria = pObjBatch.searchcriteria;
          obj.errormessage = errorMassage.ToString();
          objList.Add(obj);
          BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
        }
        #endregion
      }
      return DSPaySlipA4;
    }

    /// <summary>
    /// to get data for PaySlipA4 
    /// </summary>
    /// <param name="Keyvalue"></param>
    /// <param name="EmployeeType"></param>
    /// <returns>Dataset with 3 tables for 1.Header, 2.Incomes & 3.Deductions</returns>
    public static DataSet GetPaySlipsA4DuplicateInfo(string Keyvalue, string EmployeeType, string EmployeeCode, string StartPayDate, string EndPayDate)
    {

      /*
      Added by Sarvjeet on 22/10/2010
      To implemented Payroll batch process into Show Duplicate PaySlipsA4. Nedd to add
      A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'GetPaySlipsA4DuplicateInfo' function
      and remove comment from the code written for batch process logic.   

    */

      Object[] parameters = new object[6];
      parameters[0] = Keyvalue.Length >= 3 ? Keyvalue.Substring(0, 3) : Keyvalue;
      parameters[1] = 0;
      parameters[2] = EmployeeType;
      parameters[3] = EmployeeCode;
      parameters[4] = StartPayDate;
      parameters[5] = EndPayDate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet DSPaySlipA4 = new DataSet();
      //Added by Sarvjeet on 22/01/2010..
      #region Declare Variables for Batch Process
      //StringBuilder errorMassage = new StringBuilder();
      //DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      //int recordsSearched = 0;
      //int recordsProcessed = 0;
      //bool IsProessIns = false;
      #endregion
      try
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Insert Process Start Info..
        //object objTrx = null;
        //objProcessDtl.pybatchid = pObjBatch.pybatchid;
        //objProcessDtl.processname = "Show Duplicate PaySlipsA4";
        //objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
        //BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
        //IsProessIns = true;
        #endregion
        DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
        DataSet ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_DUPLICATE);
        if (ds != null)
          if (ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[9].ColumnName = "doc_no";
            ds.Tables[0].Columns[10].ColumnName = "empl_code";

            ds.Tables[0].DefaultView.Sort = "empl_code,doc_no";
            DataTable dttemp = ds.Tables[0].DefaultView.ToTable();
            DataTable dt = new DataTable();
            dt.TableName = "Header";
            dt.Columns.Add("first_name");
            dt.Columns.Add("last_name");
            dt.Columns.Add("middle_name");
            dt.Columns.Add("empl_code");
            dt.Columns.Add("eop_date", typeof(DateTime));
            dt.Columns.Add("keyvalue");
            dt.Columns.Add("bank1");
            dt.Columns.Add("deposit1", typeof(Decimal));
            dt.Columns.Add("bank2");
            dt.Columns.Add("deposit2", typeof(Decimal));
            dt.Columns.Add("bank3");
            dt.Columns.Add("deposit3", typeof(Decimal));
            dt.Columns.Add("bank4");
            dt.Columns.Add("deposit4", typeof(Decimal));
            dt.Columns.Add("bank5");
            dt.Columns.Add("deposit5");
            dt.Columns.Add("doc_no", typeof(Int32));
            dt.Columns.Add("dd_create");
            dt.Columns.Add("pay_date", typeof(DateTime));
            dt.Columns.Add("check_no");
            dt.Columns.Add("ok_to_post", typeof(Char));
            //recordsSearched = dttemp.Rows.Count;
            int er = 1;
            DataRow drh = dt.NewRow();
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
              DataRow dr = dttemp.Rows[i];

              if (i == 0 || dttemp.Rows[i - 1]["empl_code"].ToString().Trim() != dr["empl_code"].ToString().Trim() || dttemp.Rows[i - 1]["doc_no"].ToString().Trim() != dr["doc_no"].ToString().Trim())
              {
                er = 1;
                drh["first_name"] = dr[2];
                drh["last_name"] = dr[4];
                drh["middle_name"] = dr[5];
                drh["empl_code"] = dr["empl_code"];
                drh["eop_date"] = dr[11];
                drh["ok_to_post"] = dr[14];
                DVOMasterEmployee objtmp = new DVOMasterEmployee();
                objtmp.EmplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                objtmp.FlexDeptAcctType = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                drh["keyvalue"] = BLLMasterEmployee.GetFlexDeptKeyvalue(ref objtmp);
                objtmp.Dispose();

                drh["doc_no"] = dr["doc_no"];
                drh["bank1"] = dr[0];
                drh["deposit1"] = dr[13];
                drh["dd_create"] = dr[1];
                drh["pay_date"] = dr[12];
                drh["check_no"] = dr[7];
              }
              else
              {
                er++;
                if (er <= 5)
                {
                  drh["bank" + er.ToString()] = dr[0];
                  drh["deposit" + er.ToString()] = dr[13];
                }
              }

              if (i == dttemp.Rows.Count - 1 || dttemp.Rows[i + 1]["empl_code"].ToString().Trim() != dr["empl_code"].ToString().Trim() || dttemp.Rows[i + 1]["doc_no"].ToString().Trim() != dr["doc_no"].ToString().Trim())
              {
                dt.Rows.Add(drh);
                drh = dt.NewRow();
              }
              //recordsProcessed++;
            }
            dttemp.Dispose();
            DataTable dtnew = dt.Clone();
            if (Keyvalue.Trim() != string.Empty)
            {
              DataRow[] dtrows = dt.Select("keyvalue LIKE '" + Keyvalue + "%'");
              foreach (DataRow dtr in dtrows)
                dtnew.ImportRow(dtr);
              dt.Dispose();

              DSPaySlipA4.Tables.Add(dtnew);
            }
            else
              DSPaySlipA4.Tables.Add(dt);
          }
        if (DSPaySlipA4.Tables["Header"].Rows.Count > 0)
        {
          ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_INCOMES_DUPLICATE);
          if (ds != null)
            if (ds.Tables.Count > 0)
            {
              ds.Tables[0].Columns[0].ColumnName = "line_no";
              ds.Tables[0].Columns[1].ColumnName = "inc_code";
              ds.Tables[0].Columns[2].ColumnName = "number";
              ds.Tables[0].Columns[3].ColumnName = "hours";
              ds.Tables[0].Columns[4].ColumnName = "inc_prd";
              ds.Tables[0].Columns[5].ColumnName = "inc_ytd";
              ds.Tables[0].Columns[6].ColumnName = "inc_type";
              ds.Tables[0].Columns[7].ColumnName = "inc_rate";
              ds.Tables[0].Columns[8].ColumnName = "empl_code";
              ds.Tables[0].Columns[9].ColumnName = "doc_no";

              foreach (DataRow dr in DSPaySlipA4.Tables["Header"].Rows)
              {
                string _emplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                string _docNo = dr["doc_no"] != DBNull.Value ? dr["doc_no"].ToString() : "0";

                DataRow[] drs = ds.Tables[0].Select("empl_code='" + _emplCode + "' AND doc_no=" + _docNo);
                DataTable dts = ds.Tables[0].Clone();
                foreach (DataRow drs1 in drs)
                  dts.ImportRow(drs1);
                DataTable dts2 = dts.DefaultView.ToTable(true, "inc_code");

                if (dts2.Rows.Count < 8)
                {
                  for (int i = 0; i < 8 - dts2.Rows.Count; i++)
                  {
                    DataRow drn = ds.Tables[0].NewRow();
                    drn["doc_no"] = dr["doc_no"];
                    drn["inc_code"] = (new String('.', i));
                    //drn["inc_prd"] = 0;
                    //drn["inc_ytd"] = 0;
                    ds.Tables[0].Rows.Add(drn);
                  }
                }
                else if (dts2.Rows.Count > 8)
                {
                  string _exIncCodes = string.Empty;
                  for (int k = 7; k < dts2.Rows.Count; k++)
                    _exIncCodes += dts2.Rows[k]["inc_code"].ToString().Trim() + "||";

                  foreach (DataRow drDed in ds.Tables[0].Rows)
                  {
                    if (_exIncCodes.IndexOf(drDed["inc_code"].ToString().Trim()) >= 0 && drDed["empl_code"].ToString().Trim() == _emplCode && drDed["doc_no"].ToString().Trim() == _docNo)
                      drDed["inc_code"] = " OTHERS";
                  }
                }
                dts2.Dispose();
                dts.Dispose();
                drs = null;
              }

              ds.Tables[0].DefaultView.Sort = "empl_code,doc_no,inc_code desc";

              DataTable dt = ds.Tables[0].DefaultView.ToTable();
              dt.TableName = "Incomes";
              DSPaySlipA4.Tables.Add(dt);
            }

          ds = objDalBaseClass.GetData(ref parameters, objDVOPayrollProcess_PayEmployee.GetType(), objDVOPayrollProcess_PayEmployee.GET_PAYSLIP_A4_DEDUCTIONS_DUPLICATE);
          if (ds != null)
            if (ds.Tables.Count > 0)
            {
              ds.Tables[0].Columns[0].ColumnName = "line_no";
              ds.Tables[0].Columns[1].ColumnName = "ded_code";
              ds.Tables[0].Columns[2].ColumnName = "ded_prd";
              ds.Tables[0].Columns[3].ColumnName = "ded_ytd";
              ds.Tables[0].Columns[4].ColumnName = "empl_code";
              ds.Tables[0].Columns[5].ColumnName = "doc_no";

              foreach (DataRow dr in DSPaySlipA4.Tables["Header"].Rows)
              {
                string _emplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty;
                string _docNo = dr["doc_no"] != DBNull.Value ? dr["doc_no"].ToString() : "0";

                DataRow[] drs = ds.Tables[0].Select("empl_code='" + _emplCode + "' AND doc_no=" + _docNo);
                DataTable dts = ds.Tables[0].Clone();

                foreach (DataRow drs1 in drs)
                  dts.ImportRow(drs1);
                DataTable dts2 = dts.DefaultView.ToTable(true, "ded_code", "empl_code", "doc_no");

                if (dts2.Rows.Count < 8)
                {
                  for (int i = 0; i < 8 - dts2.Rows.Count; i++)
                  {
                    DataRow drn = ds.Tables[0].NewRow();
                    drn["doc_no"] = dr["doc_no"];
                    drn["ded_code"] = (new String('.', i));
                    //drn["ded_prd"] = 0;
                    //drn["ded_ytd"] = 0;
                    ds.Tables[0].Rows.Add(drn);
                  }
                }
                else if (dts2.Rows.Count > 8)
                {
                  string _exDedCodes = string.Empty;
                  for (int k = 7; k < dts2.Rows.Count; k++)
                    _exDedCodes += dts2.Rows[k]["ded_code"].ToString().Trim() + "||";

                  foreach (DataRow drDed in ds.Tables[0].Rows)
                  {
                    if (_exDedCodes.IndexOf(drDed["ded_code"].ToString().Trim()) >= 0 && drDed["empl_code"].ToString().Trim() == _emplCode && drDed["doc_no"].ToString().Trim() == _docNo)
                      drDed["ded_code"] = " OTHERS";
                  }
                }
                dts2.Dispose();
                dts.Dispose();
                drs = null;
              }

              ds.Tables[0].DefaultView.Sort = "empl_code,doc_no,ded_code desc";

              DataTable dt = ds.Tables[0].DefaultView.ToTable();
              dt.TableName = "Deductions";
              DSPaySlipA4.Tables.Add(dt);
            }
          ds.Dispose();
        }
        else
        {
          return DSPaySlipA4;
        }
      }
      catch (Exception ex)
      {
        //errorMassage.Append("[" + ex.Message + "]");
        ExceptionManager.Publish(ex);
        DSPaySlipA4 = new DataSet();
      }
      finally
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Record process detail..
        //if (IsProessIns)
        //{
        //    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
        //    object objTrx = null;
        //    objProcessDtl.recordssearched = recordsSearched;
        //    objProcessDtl.recordsprocessed = recordsProcessed;
        //    objProcessDtl.status = 1;
        //    objProcessDtl.errormessage = errorMassage.ToString();
        //    objList.Add(objProcessDtl);
        //    BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
        //}
        //else
        //{
        //    object objTrx = null;
        //    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
        //    DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
        //    obj.pybatchid = pObjBatch.pybatchid;
        //    obj.processname = "Show Duplicate PaySlipA4";
        //    //obj.processstartedon = pObjBatch.startedon;
        //    //obj.processendedon = pObjBatch.endedon;
        //    obj.recordssearched = recordsSearched;
        //    obj.recordsprocessed = recordsProcessed;
        //    obj.status = 1;
        //    obj.searchcriteria = pObjBatch.searchcriteria;
        //    obj.errormessage = errorMassage.ToString();
        //    objList.Add(obj);
        //    BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
        //}
        #endregion
      }
      return DSPaySlipA4;
    }
    //*****************************************************************************************
    //*****************************************************************************************       
    //Added by Sunil Pahwa on 15/06/09 
    public static DataSet Get_Inventory_Items(DVOInventoryItemsStilocar objDvoInventoryItemStilocar)
    {

      object[] Parameters = new object[7];
      Parameters[0] = objDvoInventoryItemStilocar.item_code;
      Parameters[1] = objDvoInventoryItemStilocar.item_type;
      Parameters[2] = objDvoInventoryItemStilocar.desc1;
      Parameters[3] = objDvoInventoryItemStilocar.desc2;
      Parameters[4] = objDvoInventoryItemStilocar.item_class;
      Parameters[5] = objDvoInventoryItemStilocar.warehouse_code;
      Parameters[6] = objDvoInventoryItemStilocar.stock_location;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDvoInventoryItemStilocar.FIND_INVENTORY_INFORMATION(ref Parameters));



      if (ds.Tables[0].Rows.Count > 0)
      {

        ds.Tables[0].Columns[0].ColumnName = "item_code";
        ds.Tables[0].Columns[1].ColumnName = "item_type";
        ds.Tables[0].Columns[2].ColumnName = "line_no";
        ds.Tables[0].Columns[3].ColumnName = "desc1";
        ds.Tables[0].Columns[4].ColumnName = "desc2";
        ds.Tables[0].Columns[5].ColumnName = "item_class";
        ds.Tables[0].Columns[6].ColumnName = "price";
        ds.Tables[0].Columns[7].ColumnName = "purch_unit_cost";
        ds.Tables[0].Columns[8].ColumnName = "qty_on_hand";
        ds.Tables[0].Columns[9].ColumnName = "stock_location";
        ds.Tables[0].Columns[10].ColumnName = "vend_code";
        ds.Tables[0].Columns[11].ColumnName = "warehouse_code";

      }

      return ds;



    }

    public static DataTable GetInventoryStatus(ref DVOItemCatalogstiinvtr ObjSearch)
    {
      DataSet ds = null;
      DataTable objDataTable = new DataTable();
      try
      {
        object[] Parameters = new object[6];
        Parameters[0] = ObjSearch.item_code;
        Parameters[1] = ObjSearch.item_type;
        Parameters[2] = ObjSearch.desc1;
        Parameters[3] = ObjSearch.item_class;
        Parameters[4] = ObjSearch.warehouse_code;
        Parameters[5] = ObjSearch.stock_location;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ObjSearch.FINDQUERY_ITMSTARPT(ref Parameters));
        //ds.Tables[0].Columns.Add("qty_on_hand",typeof(decimal));
        ds.Tables[0].Columns.Add("qty_commit", typeof(decimal));
        ds.Tables[0].Columns.Add("qty_on_bko", typeof(decimal));
        ds.Tables[0].Columns.Add("qty_on_req", typeof(decimal));
        ds.Tables[0].Columns.Add("qty_on_po", typeof(decimal));
        objDataTable = ds.Tables[0].Clone();
        DataView Dv = ds.Tables[0].DefaultView;
        Dv.Sort = "item_code, warehouse_code";
        objDataTable = Dv.ToTable();
        foreach (DataRow dr in objDataTable.Rows)
        {
          string item_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
          string Warehouse_code = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
          decimal sell_factor = dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0;
          string[] str = get_item_avail(item_code, Warehouse_code, sell_factor);
          dr["qty_on_hand"] = Convert.ToDecimal(str[0]);
          dr["qty_commit"] = Convert.ToDecimal(str[1]);
          dr["qty_on_bko"] = Convert.ToDecimal(str[2]);
          dr["qty_on_req"] = Convert.ToDecimal(str[3]);
          dr["qty_on_po"] = Convert.ToDecimal(str[4]);
        }


      }
      catch (Exception ex)
      {
        ds.Dispose();
        throw ex;
      }
      ds.Dispose();
      return objDataTable;
    }
    public static string[] get_item_avail(string Item_code, string Warehouse_code, decimal sell_factor)
    {
      decimal qty_on_hand = 0;
      decimal qty_commit = 0;
      decimal qty_on_bko = 0;
      decimal qty_on_req = 0;
      decimal qty_on_po = 0;
      decimal p_trans = 0;
      string[] returnparam = new string[5];
      DVOItemCatalogstiinvtr objstiinvtr = new DVOItemCatalogstiinvtr();
      BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();
      object[] parameters = new object[2];
      parameters[0] = Item_code.Trim();
      parameters[1] = Warehouse_code.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //// Get the sell factor
      //object o_sell_factor = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_sell_factor);
      //sell_factor = Convert.ToDecimal(o_sell_factor);
      //  Get the qty_on_hand from stilocar
      object o_qty_on_hand = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_qty_on_hand);
      qty_on_hand = Convert.ToDecimal(o_qty_on_hand);
      qty_on_hand = Convert.ToDecimal(BPfunctions.Al_Round("c", qty_on_hand / sell_factor));

      object o_qty_commit = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_commit_qty);
      if (o_qty_commit != null)
        if (o_qty_commit.ToString().Trim() != string.Empty)
          qty_commit = Convert.ToDecimal(o_qty_commit);
      qty_commit = Convert.ToDecimal(BPfunctions.Al_Round("c", qty_commit / sell_factor));

      object o_p_trans = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_trancommit_qty);
      if (o_p_trans != null)
        if (o_p_trans.ToString().Trim() != string.Empty)
          p_trans = Convert.ToDecimal(o_p_trans);
      p_trans = Convert.ToDecimal(BPfunctions.Al_Round("c", p_trans / sell_factor));
      qty_commit = qty_commit + p_trans;

      object o_qty_on_bko = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_ship_qty);
      if (o_qty_on_bko != null)
        if (o_qty_on_bko.ToString().Trim() != string.Empty)
          qty_on_bko = Convert.ToDecimal(o_qty_on_bko);
      qty_on_bko = Convert.ToDecimal(BPfunctions.Al_Round("c", qty_on_bko / sell_factor));

      object o_qty_on_req = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_ordr_qty);
      if (o_qty_on_req != null)
        if (o_qty_on_req.ToString().Trim() != string.Empty)
          qty_on_req = Convert.ToDecimal(o_qty_on_req);
      qty_on_req = Convert.ToDecimal(BPfunctions.Al_Round("c", qty_on_req / sell_factor));

      object o_qty_on_po = objDalBaseClass.ExecuteScalar(ref parameters, objstiinvtr.GET_exp_rec_qty);
      if (o_qty_on_po != null)
        if (o_qty_on_po.ToString().Trim() != string.Empty)
          qty_on_po = Convert.ToDecimal(o_qty_on_po);
      qty_on_po = Convert.ToDecimal(BPfunctions.Al_Round("c", qty_on_po));

      returnparam[0] = qty_on_hand.ToString();
      returnparam[1] = qty_commit.ToString();
      returnparam[2] = qty_on_bko.ToString();
      returnparam[3] = qty_on_req.ToString();
      returnparam[4] = qty_on_po.ToString();
      return returnparam;
    }
    // Added by sunil Pahwa to get Detail in Inventory on 16/06/2009
    public static DataSet Get_Inv_Detail(DVOInventoryItemsStilocar objDvoInvItemStilocar)
    {


      object[] Parameters = new object[7];
      Parameters[0] = objDvoInvItemStilocar.item_code;
      Parameters[1] = objDvoInvItemStilocar.item_type;
      Parameters[2] = objDvoInvItemStilocar.desc1;
      Parameters[3] = objDvoInvItemStilocar.desc2;
      Parameters[4] = objDvoInvItemStilocar.item_class;
      Parameters[5] = objDvoInvItemStilocar.warehouse_code;
      Parameters[6] = objDvoInvItemStilocar.stock_location;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDvoInvItemStilocar.FIND_INVENTORY_DETAIL_INFORMATION(ref Parameters));



      if (ds.Tables[0].Rows.Count > 0)
      {

        ds.Tables[0].Columns[0].ColumnName = "cog_acct_no";
        ds.Tables[0].Columns[1].ColumnName = "commodity_code";
        ds.Tables[0].Columns[2].ColumnName = "desc1";
        ds.Tables[0].Columns[3].ColumnName = "desc2";
        ds.Tables[0].Columns[4].ColumnName = "inv_acct_no";
        ds.Tables[0].Columns[5].ColumnName = "item_class";
        ds.Tables[0].Columns[6].ColumnName = "item_code";
        ds.Tables[0].Columns[7].ColumnName = "market_price";
        ds.Tables[0].Columns[8].ColumnName = "price_group";
        ds.Tables[0].Columns[9].ColumnName = "purch_factor";
        ds.Tables[0].Columns[10].ColumnName = "purch_unit";
        ds.Tables[0].Columns[11].ColumnName = "sales_acct_no";
        ds.Tables[0].Columns[12].ColumnName = "sell_factor";
        ds.Tables[0].Columns[13].ColumnName = "sell_unit";
        ds.Tables[0].Columns[14].ColumnName = "serialized";
        ds.Tables[0].Columns[15].ColumnName = "stock_unit";
        ds.Tables[0].Columns[16].ColumnName = "volume";
        ds.Tables[0].Columns[17].ColumnName = "weight";
        ds.Tables[0].Columns[18].ColumnName = "weight_unit";
        ds.Tables[0].Columns[19].ColumnName = "abc_code";
        ds.Tables[0].Columns[20].ColumnName = "allow_bo";
        ds.Tables[0].Columns[21].ColumnName = "avg_ld_tm";
        ds.Tables[0].Columns[22].ColumnName = "avg_unit_cost";

        ds.Tables[0].Columns[23].ColumnName = "comm_code";
        ds.Tables[0].Columns[24].ColumnName = "count_cycle";
        ds.Tables[0].Columns[25].ColumnName = "count_date";
        ds.Tables[0].Columns[26].ColumnName = "freez_date";
        ds.Tables[0].Columns[27].ColumnName = "freez_expir";
        ds.Tables[0].Columns[28].ColumnName = "freez_flag";
        ds.Tables[0].Columns[29].ColumnName = "last_cost";
        ds.Tables[0].Columns[30].ColumnName = "last_qty";
        ds.Tables[0].Columns[31].ColumnName = "loc_aisle";
        ds.Tables[0].Columns[32].ColumnName = "loc_bin";
        ds.Tables[0].Columns[33].ColumnName = "loc_row";
        ds.Tables[0].Columns[34].ColumnName = "lst_act_date";
        ds.Tables[0].Columns[35].ColumnName = "lst_ld_tm";
        ds.Tables[0].Columns[36].ColumnName = "min_sell_qty";
        ds.Tables[0].Columns[37].ColumnName = "obsolete";
        ds.Tables[0].Columns[38].ColumnName = "pri_ld_tm";
        ds.Tables[0].Columns[39].ColumnName = "price";
        ds.Tables[0].Columns[40].ColumnName = "purch_unit_cost";
        ds.Tables[0].Columns[41].ColumnName = "purchase_date";
        ds.Tables[0].Columns[42].ColumnName = "qty_on_hand";
        ds.Tables[0].Columns[43].ColumnName = "qty_reorder";
        ds.Tables[0].Columns[44].ColumnName = "reorder_point";
        ds.Tables[0].Columns[45].ColumnName = "safety_factor";
        ds.Tables[0].Columns[46].ColumnName = "safety_stock";
        ds.Tables[0].Columns[47].ColumnName = "seasonal";
        ds.Tables[0].Columns[48].ColumnName = "sold_date";
        ds.Tables[0].Columns[49].ColumnName = "stk_out_date";
        ds.Tables[0].Columns[50].ColumnName = "taxable";
        ds.Tables[0].Columns[51].ColumnName = "terms_disc";
        ds.Tables[0].Columns[52].ColumnName = "trade_disc";
        ds.Tables[0].Columns[53].ColumnName = "vend_code";
        ds.Tables[0].Columns[54].ColumnName = "vend_prod_no";
        ds.Tables[0].Columns[55].ColumnName = "warehouse_code";
        ds.Tables[0].Columns[56].ColumnName = "cost";
        ds.Tables[0].Columns[57].ColumnName = "cqty";
        ds.Tables[0].Columns[58].ColumnName = "period";
        ds.Tables[0].Columns[59].ColumnName = "sale";
        ds.Tables[0].Columns[60].ColumnName = "sqty";

        ds.Tables[0].Columns[61].ColumnName = "usage_rate";
        ds.Tables[0].Columns[62].ColumnName = "yr";

        ds.Tables[0].Columns[63].ColumnName = "cog_acct_kv";
        ds.Tables[0].Columns[64].ColumnName = "cog_acct_desc";
        ds.Tables[0].Columns[65].ColumnName = "inv_acct_kv";
        ds.Tables[0].Columns[66].ColumnName = "inv_acct_desc";
        ds.Tables[0].Columns[67].ColumnName = "sales_acct_kv";
        ds.Tables[0].Columns[68].ColumnName = "sales_acct_desc";
        ds.Tables[0].DefaultView.Sort = "item_code, warehouse_code, yr desc, period desc";


      }

      return ds;


    }

    public static DataTable GetEmployeeSocialSecurityInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataTable dtres = new DataTable();
      try
      {
        DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
        using (DataSet ds = objDalBaseClass.GetData(objDVOMasterEmployee.GetType(), objDVOMasterEmployee.GET_EMP_SOCIAL_SECURITY_INFO))
        {
          if (ds != null)
            if (ds.Tables.Count > 0)
              if (ds.Tables[0].Rows.Count > 0)
              {
                ds.Tables[0].Columns[0].ColumnName = "empl_code";
                ds.Tables[0].Columns[1].ColumnName = "soc_sec_num";
                ds.Tables[0].Columns[2].ColumnName = "birthdate";
                ds.Tables[0].Columns[3].ColumnName = "first_name";
                ds.Tables[0].Columns[4].ColumnName = "middle_name";
                ds.Tables[0].Columns[5].ColumnName = "last_name";
                ds.Tables[0].Columns[6].ColumnName = "address1";
                ds.Tables[0].Columns[7].ColumnName = "address2";
                ds.Tables[0].Columns[8].ColumnName = "city";
                ds.Tables[0].Columns[9].ColumnName = "job_title";
                ds.Tables[0].Columns[10].ColumnName = "date_hired";
                ds.Tables[0].Columns[11].ColumnName = "marital_stat";
                ds.Tables[0].Columns[12].ColumnName = "gender";
                ds.Tables[0].Columns[13].ColumnName = "keyvalue";
                ds.Tables[0].Columns[14].ColumnName = "department";
                ds.Tables[0].Columns[15].ColumnName = "flexdeptaccttype";
                ds.Tables[0].Columns[16].ColumnName = "position";

                dtres = ds.Tables[0].Clone();
                dtres.Columns["birthdate"].DataType = typeof(DateTime);
                dtres.Columns["date_hired"].DataType = typeof(DateTime);

                string _prevEmpCode = null, _department = null;
                DataRow drres = dtres.NewRow();
                dtres.Clear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                  if (_prevEmpCode != null && _prevEmpCode.Trim() != string.Empty)
                  {
                    if (i < ds.Tables[0].Rows.Count - 1)
                      if (_prevEmpCode != ds.Tables[0].Rows[i]["empl_code"].ToString().Trim())
                        drres["empl_code"] = _prevEmpCode;

                    if (drres["empl_code"] != null && drres["empl_code"].ToString().Trim() != string.Empty)
                      dtres.Rows.Add(drres);
                    drres = dtres.NewRow();
                  }

                  for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    drres[j] = ds.Tables[0].Rows[i][j];
                  drres["empl_code"] = string.Empty;

                  if ((_prevEmpCode == null || _prevEmpCode.Trim() == string.Empty) || (_prevEmpCode != null && _prevEmpCode != ds.Tables[0].Rows[i]["empl_code"].ToString().Trim()))
                  {
                    _department = string.Empty;
                    _prevEmpCode = ds.Tables[0].Rows[i]["empl_code"].ToString().Trim();
                  }

                  _department += ds.Tables[0].Rows[i]["keyvalue"].ToString().Trim();
                  drres["department"] = _department;
                }
                drres["empl_code"] = _prevEmpCode;
                if (drres["empl_code"] != null && drres["empl_code"].ToString().Trim() != string.Empty)
                  dtres.Rows.Add(drres);
              }
        }
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        dtres = new DataTable();
        return dtres;
      }
      return dtres;
    }


    public static DataSet GetOverShortReportInfo(DVOInventoryCountSheetDtl objDvoInvCountSheetShortRpt)
    {

      object[] Parameters = new object[3];
      Parameters[0] = objDvoInvCountSheetShortRpt.first_value;
      Parameters[1] = objDvoInvCountSheetShortRpt.second_value;
      Parameters[2] = objDvoInvCountSheetShortRpt.oprator;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOInventoryCountSheetDtl), objDvoInvCountSheetShortRpt.GET_OVER_SHORT_REPORT_INFO1);



      if (ds.Tables[0].Rows.Count > 0)
      {

        ds.Tables[0].Columns[0].ColumnName = "page_no";
        ds.Tables[0].Columns[1].ColumnName = "doc_no";
        ds.Tables[0].Columns[2].ColumnName = "line_no";
        ds.Tables[0].Columns[3].ColumnName = "item_code";
        ds.Tables[0].Columns[4].ColumnName = "warehouse_code";
        ds.Tables[0].Columns[5].ColumnName = "qty_on_hand";
        ds.Tables[0].Columns[6].ColumnName = "count_qty";
        ds.Tables[0].Columns[7].ColumnName = "adj_qty";

      }

      return ds;


    }

    public static bool GenerateCheckNo(ref DataTable dttemp)
    {
      /*
      Added by Sarvjeet on 22/10/2010
      To implemented Payroll batch process into Print Pay SlipsA4. Nedd to add
      two parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' and 'string processName'
      in 'ShowPayrollChecks' function
      and remove comment from the code written for batch process logic.   
    */
      bool IsCheckNoNull = false;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      //Added by Sarvjeet on 22/01/2010..
      #region Declare Variables for Batch Process
      StringBuilder errorMassage = new StringBuilder();
      DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      int recordsSearched = 0;
      int recordsProcessed = 0;
      bool IsProessIns = false;
      #endregion

      //Added by Sarvjeet on 22/01/2010..
      #region Insert Process Start Info..
      //object objTrx = null;
      //objProcessDtl.pybatchid = pObjBatch.pybatchid;
      //objProcessDtl.processname = processName;
      //objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
      //BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
      //IsProessIns = true;
      #endregion
      recordsSearched = dttemp.Rows.Count;
      try
      {
        if (dttemp != null && dttemp.Rows.Count > 0)
        {
          Int32 last_check_no = BLLAccountingLiberary.CurrentValue_With_LockAndSelect("stpcntrc", "last_chkno", dttemp.Rows.Count, ref objTransaction);
          if (last_check_no == 0)
            throw new Exception("Error has occurred while generating check_no.");

          for (int i = 0; i < dttemp.Rows.Count; i++)
          {
            DataRow dr = dttemp.Rows[i];

            if (i == 0 || dttemp.Rows[i - 1]["empl_code"].ToString().Trim() != dr["empl_code"].ToString().Trim() || dttemp.Rows[i - 1]["doc_no"].ToString().Trim() != dr["doc_no"].ToString().Trim())
            {
              if (dr["check_no"] == DBNull.Value)
                IsCheckNoNull = true;
              else if (dr["check_no"].ToString().Trim() == string.Empty)
                IsCheckNoNull = true;
              if (IsCheckNoNull)
              {
                //************* Update By Bharat Dhall[09/29/2009] ****************
                int check_no = last_check_no + i + 1;
                //Int32 check_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "last_chkno", ref objTransaction);
                //if (check_no == 0)
                //    throw new Exception("Error has occurred while generating check_no");


                Object[] Parameters = new object[2];
                Parameters[0] = Convert.ToInt32(dr["doc_no"]);
                Parameters[1] = check_no;
                object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref Parameters, (new DVOPayrollProcess_PayEmployee()).UPDATE_CHECK_NO, true);
                if (o != null)
                  if (o.ToString().Trim() != string.Empty)
                    if (Convert.ToInt32(o) != 1)
                      throw new Exception("Error has occurred while updating Process_PayEmployee");


              }
              IsCheckNoNull = false;
            }
            recordsProcessed++;
          }
          if (objTransaction != null)
            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        }
      }
      catch (Exception ex)
      {
        errorMassage.Append("[" + ex.Message + "]");
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        throw ex;
      }
      finally
      {
        //Added by Sarvjeet on 22/01/2010..
        #region Record process detail..
        //if (IsProessIns)
        //{
        //    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
        //    objTrx = null;
        //    objProcessDtl.recordssearched = recordsSearched;
        //    objProcessDtl.recordsprocessed = recordsProcessed;
        //    objProcessDtl.status = 1;
        //    objProcessDtl.errormessage = errorMassage.ToString();
        //    objList.Add(objProcessDtl);
        //    BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
        //}
        //else
        //{
        //    objTrx = null;
        //    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
        //    DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
        //    obj.pybatchid = pObjBatch.pybatchid;
        //    obj.processname = processName;
        //    //obj.processstartedon = pObjBatch.startedon;
        //    //obj.processendedon = pObjBatch.endedon;
        //    obj.recordssearched = recordsSearched;
        //    obj.recordsprocessed = recordsProcessed;
        //    obj.status = 1;
        //    obj.searchcriteria = pObjBatch.searchcriteria;
        //    obj.errormessage = errorMassage.ToString();
        //    objList.Add(obj);
        //    BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
        //}
        #endregion
      }
      return true;
    }

    public static DataSet GetEstCapExpByobjectCode(string Period, string Year)
    {
      Object[] parameters = new object[2];
      parameters[0] = Period;
      parameters[1] = Year;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByObj), (new DVOCapExpByObj()).GET_EST_CAPEXP_BY_OBJECT_CODE);
      ds.Tables[0].Columns[0].ColumnName = "objcode";
      ds.Tables[0].Columns[1].ColumnName = "desc";
      ds.Tables[0].Columns[2].ColumnName = "actualexp";
      ds.Tables[0].Columns[3].ColumnName = "estexp";
      return ds;
    }
    //public static DataSet GetEstCapRevByobjectCode(string Period, string Year)
    //{
    //    Object[] parameters = new object[2];
    //    parameters[0] = Period;
    //    parameters[1] = Year;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCapRevByObj), (new DVOCapRevByObj()).GET_CAP_REV_EST);
    //    ds.Tables[0].Columns[0].ColumnName = "objcode";
    //    ds.Tables[0].Columns[1].ColumnName = "desc";
    //    ds.Tables[0].Columns[2].ColumnName = "actualexp";
    //    ds.Tables[0].Columns[3].ColumnName = "estexp";
    //    return ds;
    //}
    public static DataSet GetEstRecRevByobjectCode(string Period, string Year)
    {
      Object[] parameters = new object[2];
      parameters[0] = Period;
      parameters[1] = Year;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORecRevByObj), (new DVORecRevByObj()).GET_RECREV_EST);
      ds.Tables[0].Columns[0].ColumnName = "objcode";
      ds.Tables[0].Columns[1].ColumnName = "desc";
      ds.Tables[0].Columns[2].ColumnName = "actualexp";
      ds.Tables[0].Columns[3].ColumnName = "estexp";
      return ds;

    }
    public static DataSet GetXinforData(string src_type)
    {

      Object[] parameters = new object[1];
      parameters[0] = src_type.Trim();
      DVOInventoryDefaultsticntrc obj = new DVOInventoryDefaultsticntrc();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOInventoryDefaultsticntrc), obj.GET_XINFOR);
      ds.Tables[0].Columns[0].ColumnName = "src_acct_no";
      ds.Tables[0].Columns[1].ColumnName = "src_char_desc";
      ds.Tables[0].Columns[2].ColumnName = "src_desc";
      ds.Tables[0].Columns[3].ColumnName = "src_key";
      ds.Tables[0].Columns[4].ColumnName = "src_num_desc";
      ds.Tables[0].Columns[5].ColumnName = "src_type";
      return ds;

    }
    public static DataSet GetAllInvDefData()
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetAllData(typeof(DVOInventoryDefaultsticntrc));
      ds.Tables[0].Columns[0].ColumnName = "RowId";
      ds.Tables[0].Columns[1].ColumnName = "item_type";
      ds.Tables[0].Columns[2].ColumnName = "item_class";
      ds.Tables[0].Columns[3].ColumnName = "inacct_no";
      ds.Tables[0].Columns[4].ColumnName = "sales_acct_no";
      ds.Tables[0].Columns[5].ColumnName = "cog_acct_no";
      ds.Tables[0].Columns[6].ColumnName = "adj_acct_no";
      ds.Tables[0].Columns[7].ColumnName = "count_acct_no";
      ds.Tables[0].Columns[8].ColumnName = "count_cycle";
      ds.Tables[0].Columns[9].ColumnName = "comm_code";
      ds.Tables[0].Columns[10].ColumnName = "allow_bo";
      ds.Tables[0].Columns[11].ColumnName = "taxable";
      ds.Tables[0].Columns[12].ColumnName = "terms_disc";
      ds.Tables[0].Columns[13].ColumnName = "trade_disc";
      ds.Tables[0].Columns[14].ColumnName = "ic_setup_done";
      ds.Tables[0].Columns[15].ColumnName = "doc_no";
      ds.Tables[0].Columns[16].ColumnName = "post_no";
      ds.Tables[0].Columns[17].ColumnName = "use_department";
      ds.Tables[0].Columns[18].ColumnName = "cost_method";
      ds.Tables[0].Columns[19].ColumnName = "ilocar_ina_days";
      ds.Tables[0].Columns[20].ColumnName = "ilocar_ret_days";
      ds.Tables[0].Columns[21].ColumnName = "class1";
      ds.Tables[0].Columns[22].ColumnName = "class2";
      ds.Tables[0].Columns[23].ColumnName = "class3";
      ds.Tables[0].Columns[24].ColumnName = "class4";
      ds.Tables[0].Columns[25].ColumnName = "class5";
      ds.Tables[0].Columns[26].ColumnName = "class6";
      ds.Tables[0].Columns[27].ColumnName = "class7";
      ds.Tables[0].Columns[28].ColumnName = "class8";
      ds.Tables[0].Columns[29].ColumnName = "class9";
      ds.Tables[0].Columns[30].ColumnName = "class10";
      ds.Tables[0].Columns[31].ColumnName = "class11";
      ds.Tables[0].Columns[32].ColumnName = "class12";
      ds.Tables[0].Columns[33].ColumnName = "class13";
      ds.Tables[0].Columns[34].ColumnName = "inkeyvalue";
      ds.Tables[0].Columns[35].ColumnName = "salse_keyvalue";
      ds.Tables[0].Columns[36].ColumnName = "cog_keyvalue";
      ds.Tables[0].Columns[37].ColumnName = "adj_keyvalue";
      ds.Tables[0].Columns[38].ColumnName = "count_keyvalue";
      ds.Tables[0].Columns[39].ColumnName = "inaccttype";
      ds.Tables[0].Columns[40].ColumnName = "salse_accttype";
      ds.Tables[0].Columns[41].ColumnName = "cog_accttype";
      ds.Tables[0].Columns[42].ColumnName = "adj_accttype";
      ds.Tables[0].Columns[43].ColumnName = "count_accttype";
      return ds;
    }
    public static DataSet GetOrderEntryDefaults()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsOrderEntryDefault = objDalBaseClass.GetAllData(typeof(DVOstocntrc));
      dsOrderEntryDefault.Tables[0].Columns[0].ColumnName = "disc_frght";
      dsOrderEntryDefault.Tables[0].Columns[1].ColumnName = "tax_frght";
      dsOrderEntryDefault.Tables[0].Columns[2].ColumnName = "warehouse_code";
      dsOrderEntryDefault.Tables[0].Columns[3].ColumnName = "retention_days";
      dsOrderEntryDefault.Tables[0].Columns[4].ColumnName = "due_days";
      //dsOrderEntryDefault.Tables[0].Columns[5].ColumnName = "cog_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[5].ColumnName = "cm_reason";
      dsOrderEntryDefault.Tables[0].Columns[6].ColumnName = "dm_reason";
      dsOrderEntryDefault.Tables[0].Columns[7].ColumnName = "ar_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[8].ColumnName = "cash_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[9].ColumnName = "visa_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[10].ColumnName = "sales_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[11].ColumnName = "disc_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[12].ColumnName = "frght_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[13].ColumnName = "inv_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[14].ColumnName = "cog_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[15].ColumnName = "scrap_acct_no";
      dsOrderEntryDefault.Tables[0].Columns[16].ColumnName = "use_department";
      dsOrderEntryDefault.Tables[0].Columns[17].ColumnName = "oe_doc_no";
      dsOrderEntryDefault.Tables[0].Columns[18].ColumnName = "oe_inv_doc_no";
      dsOrderEntryDefault.Tables[0].Columns[19].ColumnName = "oe_post_no";
      dsOrderEntryDefault.Tables[0].Columns[20].ColumnName = "order_type";
      dsOrderEntryDefault.Tables[0].Columns[21].ColumnName = "line_type";
      dsOrderEntryDefault.Tables[0].Columns[22].ColumnName = "terms_code";
      dsOrderEntryDefault.Tables[0].Columns[23].ColumnName = "ack_kit_exp";
      dsOrderEntryDefault.Tables[0].Columns[24].ColumnName = "pic_kit_exp";
      dsOrderEntryDefault.Tables[0].Columns[25].ColumnName = "mfs_kit_exp";
      dsOrderEntryDefault.Tables[0].Columns[26].ColumnName = "inv_kit_exp";
      dsOrderEntryDefault.Tables[0].Columns[27].ColumnName = "ack_note";
      dsOrderEntryDefault.Tables[0].Columns[28].ColumnName = "pic_note";
      dsOrderEntryDefault.Tables[0].Columns[29].ColumnName = "shp_note";
      dsOrderEntryDefault.Tables[0].Columns[30].ColumnName = "inv_note";
      dsOrderEntryDefault.Tables[0].Columns[31].ColumnName = "pay_method";
      dsOrderEntryDefault.Tables[0].Columns[32].ColumnName = "fob_point";
      dsOrderEntryDefault.Tables[0].Columns[33].ColumnName = "ship_via";
      dsOrderEntryDefault.Tables[0].Columns[34].ColumnName = "mtaxg_code";
      dsOrderEntryDefault.Tables[0].Columns[35].ColumnName = "use_batch_inv";
      dsOrderEntryDefault.Tables[0].Columns[36].ColumnName = "use_approv_post";
      dsOrderEntryDefault.Tables[0].Columns[37].ColumnName = "approval_code";
      dsOrderEntryDefault.Tables[0].Columns[38].ColumnName = "taxCodeDesc";
      return dsOrderEntryDefault;

    }


    //*************[Added by Sunil Pahwa For Print Picking Documents]***********************************
    public static DataSet Get_Picking_Document_info(DVOOrderEntrystoshipd objDvoDVOOrderEntrystoshipd)
    {
      //try
      //{
      Object[] parameters = new object[6];
      parameters[0] = objDvoDVOOrderEntrystoshipd.cust_code;
      parameters[1] = objDvoDVOOrderEntrystoshipd.ord_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[2] = objDvoDVOOrderEntrystoshipd.order_no;
      parameters[3] = objDvoDVOOrderEntrystoshipd.doc_no;
      parameters[4] = objDvoDVOOrderEntrystoshipd.warehouse_code;
      parameters[5] = objDvoDVOOrderEntrystoshipd.pic_ticket_no;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOOrderEntrystoshipd));
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "alias_code";
        ds.Tables[0].Columns[1].ColumnName = "desc1";
        ds.Tables[0].Columns[2].ColumnName = "desc2";
        ds.Tables[0].Columns[3].ColumnName = "interchanged";
        ds.Tables[0].Columns[4].ColumnName = "item_code";
        ds.Tables[0].Columns[5].ColumnName = "kit_group";
        ds.Tables[0].Columns[6].ColumnName = "ordr_qty";
        ds.Tables[0].Columns[7].ColumnName = "sell_unit";
        ds.Tables[0].Columns[8].ColumnName = "serialized";
        ds.Tables[0].Columns[9].ColumnName = "address1";
        ds.Tables[0].Columns[10].ColumnName = "address2";
        ds.Tables[0].Columns[11].ColumnName = "bus_name";
        ds.Tables[0].Columns[12].ColumnName = "city";
        ds.Tables[0].Columns[13].ColumnName = "contact";
        ds.Tables[0].Columns[14].ColumnName = "country";
        ds.Tables[0].Columns[15].ColumnName = "doc_no";
        ds.Tables[0].Columns[16].ColumnName = "fob_point";
        ds.Tables[0].Columns[17].ColumnName = "l_mod_time";
        ds.Tables[0].Columns[18].ColumnName = "order_date";
        ds.Tables[0].Columns[19].ColumnName = "order_no";
        ds.Tables[0].Columns[20].ColumnName = "order_status";
        ds.Tables[0].Columns[21].ColumnName = "pic_ticket_no";
        ds.Tables[0].Columns[22].ColumnName = "po_no";
        ds.Tables[0].Columns[23].ColumnName = "ship_via";
        ds.Tables[0].Columns[24].ColumnName = "sls_psn_code";
        ds.Tables[0].Columns[25].ColumnName = "staging_area";
        ds.Tables[0].Columns[26].ColumnName = "state";
        ds.Tables[0].Columns[27].ColumnName = "terms_code";
        ds.Tables[0].Columns[28].ColumnName = "zip";
        ds.Tables[0].Columns[29].ColumnName = "line_no";
        ds.Tables[0].Columns[30].ColumnName = "d_pic_ticket_no";
        ds.Tables[0].Columns[31].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[32].ColumnName = "ship_no";
        ds.Tables[0].Columns[33].ColumnName = "ship_qty";
        ds.Tables[0].Columns[34].ColumnName = "ship_to_code";
        ds.Tables[0].Columns[35].ColumnName = "stock_location";
        ds.Tables[0].Columns[36].ColumnName = "warehouse_code";
        ds.Tables[0].Columns[37].ColumnName = "detail_row";
        ds.Tables[0].Columns[38].ColumnName = "cust_code";
        ds.Tables[0].Columns[39].ColumnName = "description";


        //Added Later As per the requirement
        ds.Tables[0].Columns.Add("sbus_name");
        ds.Tables[0].Columns.Add("saddress1");
        ds.Tables[0].Columns.Add("saddress2");
        ds.Tables[0].Columns.Add("scity");
        ds.Tables[0].Columns.Add("sstate");
        ds.Tables[0].Columns.Add("szip");
        ds.Tables[0].Columns.Add("scountry");
        ds.Tables[0].Columns.Add("sphone");

        bool kit_compression = false;
        bool print_notes = false;
        //Getting Few Fiels from Control Table
        DataSet dsStocntrc = objDalBaseClass.GetAllData(typeof(DVOOrderEntrystoshipd));
        dsStocntrc.Tables[0].Columns[0].ColumnName = "v_one_time_cust";
        dsStocntrc.Tables[0].Columns[1].ColumnName = "v_kit_exp";
        dsStocntrc.Tables[0].Columns[2].ColumnName = "v_pic_note";

        string kit_exp = string.Empty;
        string pic_note = string.Empty;

        string one_time_cust = (dsStocntrc.Tables[0].Rows[0]["v_one_time_cust"] != DBNull.Value ? dsStocntrc.Tables[0].Rows[0]["v_one_time_cust"].ToString() : string.Empty);
        kit_exp = (dsStocntrc.Tables[0].Rows[0]["v_kit_exp"] != DBNull.Value ? dsStocntrc.Tables[0].Rows[0]["v_kit_exp"].ToString() : string.Empty);
        if (kit_exp == "N")
        {
          kit_compression = true;
        }

        pic_note = (dsStocntrc.Tables[0].Rows[0]["v_pic_note"] != DBNull.Value ? dsStocntrc.Tables[0].Rows[0]["v_pic_note"].ToString() : string.Empty);
        if (pic_note == "Y")
        {
          print_notes = true;
        }
        //get the customer address
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
          DataRow drow = ds.Tables[0].Rows[i];
          // if this is a one time customer, put the order header address in
          // both places.  otherwise, get the addresses from the customer and
          // ship-to tables.
          if (Convert.ToString(drow["sell_to_code"]) == one_time_cust)
          {
            drow["sbus_name"] = drow["bus_name"];
            drow["saddress1"] = drow["address1"];
            drow["saddress2"] = drow["address2"];
            drow["scity"] = drow["city"];
            drow["sstate"] = drow["state"];
            drow["szip"] = drow["zip"];
            drow["scountry"] = drow["country"];
          }
          else
          {

            object[] Getparameter = new object[1];
            Getparameter[0] = drow["cust_code"];

            DVOCustomerDetailstrcustr objDVOCustomerDetailstrcustr = new DVOCustomerDetailstrcustr();
            DataSet Dsstrcustr = objDalBaseClass.GetData(ref Getparameter, typeof(DVOCustomerDetailstrcustr), objDVOCustomerDetailstrcustr.GETCUSTADDRESS_FOR_PICKING_DOC);
            Dsstrcustr.Tables[0].Columns[0].ColumnName = "v_bus_name";
            Dsstrcustr.Tables[0].Columns[1].ColumnName = "v_address1";
            Dsstrcustr.Tables[0].Columns[2].ColumnName = "v_address2";
            Dsstrcustr.Tables[0].Columns[3].ColumnName = "v_city";
            Dsstrcustr.Tables[0].Columns[4].ColumnName = "v_state";
            Dsstrcustr.Tables[0].Columns[5].ColumnName = "v_zip";
            Dsstrcustr.Tables[0].Columns[6].ColumnName = "v_country";
            Dsstrcustr.Tables[0].Columns[7].ColumnName = "v_contact";
            Dsstrcustr.Tables[0].Columns[8].ColumnName = "v_phone";


            if (dsStocntrc.Tables[0].Rows.Count > 0)
            {
              string bus_name = Dsstrcustr.Tables[0].Rows[0]["v_bus_name"].ToString();
              string address1 = Dsstrcustr.Tables[0].Rows[0]["v_address1"].ToString();
              string address2 = Dsstrcustr.Tables[0].Rows[0]["v_address2"].ToString();
              string city = Dsstrcustr.Tables[0].Rows[0]["v_city"].ToString();
              string state = Dsstrcustr.Tables[0].Rows[0]["v_state"].ToString();
              string zip = Dsstrcustr.Tables[0].Rows[0]["v_zip"].ToString();
              string country = Dsstrcustr.Tables[0].Rows[0]["v_country"].ToString();
              string contact = Dsstrcustr.Tables[0].Rows[0]["v_contact"].ToString();
              string phone = Dsstrcustr.Tables[0].Rows[0]["v_phone"].ToString();

              //get the customer Address
              drow["bus_name"] = bus_name;
              drow["address1"] = address1;
              drow["address2"] = address2;
              drow["city"] = city;
              drow["state"] = state;
              drow["zip"] = zip;
              drow["country"] = country;
              drow["contact"] = contact;
              drow["sphone"] = phone;
            }
            //get the shipping address
            DVOShipDetailstrshipr objDVOShipDetailstrshipr = new DVOShipDetailstrshipr();
            object[] paramstrshipr = new object[2];
            paramstrshipr[0] = drow["cust_code"];
            paramstrshipr[1] = drow["ship_to_code"];
            DataSet Dsstrshipr = objDalBaseClass.GetData(ref paramstrshipr, typeof(DVOShipDetailstrshipr), objDVOShipDetailstrshipr.GET_PICKING_DOC_STRSHIPR);


            if (Dsstrshipr.Tables[0].Rows.Count > 0)
            {

              string b_name = (Dsstrshipr.Tables[0].Rows[0]["v_bus_name"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_bus_name"].ToString() : string.Empty);
              string add1 = (Dsstrshipr.Tables[0].Rows[0]["v_address1"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_address1"].ToString() : string.Empty);
              string add2 = (Dsstrshipr.Tables[0].Rows[0]["v_address2"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_address2"].ToString() : string.Empty); //Dsstrshipr.Tables[0].Rows[0]["v_address2"].ToString();
              string cty = (Dsstrshipr.Tables[0].Rows[0]["v_city"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_city"].ToString() : string.Empty); //Dsstrshipr.Tables[0].Rows[0]["v_city"].ToString();
              string stat = (Dsstrshipr.Tables[0].Rows[0]["v_state"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_state"].ToString() : string.Empty);// Dsstrshipr.Tables[0].Rows[0]["v_state"].ToString();
              string zp = (Dsstrshipr.Tables[0].Rows[0]["v_zip"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_zip"].ToString() : string.Empty);// Dsstrshipr.Tables[0].Rows[0]["v_zip"].ToString();
              string county = (Dsstrshipr.Tables[0].Rows[0]["v_country"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_country"].ToString() : string.Empty); //Dsstrshipr.Tables[0].Rows[0]["v_country"].ToString();
              string contat = (Dsstrshipr.Tables[0].Rows[0]["v_contact"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_contace"].ToString() : string.Empty); //Dsstrshipr.Tables[0].Rows[0]["v_contact"].ToString();
              string ph = (Dsstrshipr.Tables[0].Rows[0]["v_phone"] != DBNull.Value ? Dsstrshipr.Tables[0].Rows[0]["v_phone"].ToString() : string.Empty); //Dsstrshipr.Tables[0].Rows[0]["phone"].ToString();


              drow["sbus_name"] = b_name;
              drow["saddress1"] = add1;
              drow["saddress2"] = add2;
              drow["scity"] = cty;
              drow["sstate"] = stat;
              drow["szip"] = zp;
              drow["scountry"] = county;
              drow["scountry"] = contat;
              drow["sphone"] = ph;

            }
            else
            {

              drow["sbus_name"] = drow["bus_name"];
              drow["saddress1"] = drow["address1"];
              drow["saddress2"] = drow["address2"];
              drow["scity"] = drow["city"];
              drow["sstate"] = drow["state"];
              drow["szip"] = drow["zip"];
              drow["scountry"] = drow["country"];
              //drow["scountry"];
              //drow["sphone"];
            }

            //# if this is not a reprint, then update the picking ticket number
            string reprint = objDvoDVOOrderEntrystoshipd.reprint;
            if (reprint != "Y")
            {

              int pic_ticket_no = 0;
              drow["pic_ticket_no"] = pic_ticket_no + 1;
            }

            // # if this is an order to be staged, then change the 
            //shipping address to the staging area
            if (Convert.ToString(drow["order_status"]) == "STH")
            {
              drow["bus_name"] = string.Empty;
              drow["address1"] = string.Empty;
              drow["address2"] = string.Empty;
              drow["city"] = string.Empty;
              drow["state"] = string.Empty;
              drow["zip"] = string.Empty;
              drow["country"] = string.Empty;
              drow["contact"] = string.Empty;
              drow["sphone"] = string.Empty;
              drow["bus_name"] = "Do not ship.";

            }
            if (drow["staging_area"] == null)
            {
              drow["address1"] = "Staged in area _______________";
            }
            else
            {
              drow["address1"] = "Stage in area " + drow["staging_area"] + ".";

            }

            bool print_ok = true;
            bool pic_mod_flag = false;
            bool pic_chg_flag = false;
            DVOOrderstoordre objDVOOrderstoordre = new DVOOrderstoordre();
            object[] paramstoordre = new object[1];
            paramstoordre[0] = drow["doc_no"];

            DataSet Dsstoordre = objDalBaseClass.GetData(ref paramstoordre, typeof(DVOOrderstoordre), objDVOOrderstoordre.GET_PICKING_DOC_INFO_STOORDRE);

            Dsstoordre.Tables[0].Columns[0].ColumnName = "rowid";
            Dsstoordre.Tables[0].Columns[1].ColumnName = "l_mod_into";
            string l_mode_into = Dsstoordre.Tables[0].Rows[0][1].ToString();
            int rowid = Convert.ToInt32(Dsstoordre.Tables[0].Rows[0][0].ToString());
            if (Dsstoordre.Tables[0].Rows.Count > 0)
            {
              if (Convert.ToString(drow["l_mod_time"]) != l_mode_into)
              {
                pic_chg_flag = true;
              }
            }
            //else {}
            //       // # see if the header record is locked.
            //      // let print_ok = row_lock("stoordre", doc_rowid)

            ////

            // private int LockCurrentRecord()
            // {
            // DVOOrderstoordre objDVOOrderstoordre = new DVOOrderstoordre();

            // objDVOOrderstoordre.Rowid = 
            //    int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref TransactionObject, (iDVO)objDVOOrderstoordre, Program.UserId, Program.MachineInfo);
            //    if (LockStatus != 1)
            //    {
            //        _ChangeLockStatus = 0;
            //        DialogResult d = Utilities.ShowMessage("Want to wait to release the record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (d == DialogResult.Yes)
            //        {
            //            System.Threading.Thread.Sleep(10000);
            //            LockCurrentRecord();
            //        }
            //    }
            //    else
            //    {

            //        //List<DVOOrderstoordre> listDVOOrderstoordre = BLLInventoryShippedStiselle.GetInvShippedInfo(ref objDVOOrderstoordre);
            //        //if (listDVOUpdateInvShippedStiselle.Count > 0)
            //        //    if (lstDVOUpdateInvShippedStiselle  != null)
            //        //        if (lstDVOUpdateInvShippedStiselle.Count > 0)
            //        //            lstDVOUpdateInvShippedStiselle[RecordIndex] = listDVOUpdateInvShippedStiselle[0];

            //        //listDVOUpdateInvShippedStiselle = null;
            //        objDVOOrderstoordre = null;
            //        _ChangeLockStatus = 1;
            //    }

            //    return _ChangeLockStatus;
            // }


            if (print_ok)
            {
              //GET_PICKING_DOCUMENT_SHIPDGET
              DVOOrderEntrystoshipd objDVOOrderEntrystoshipd = new DVOOrderEntrystoshipd();
              object[] par = new object[4];

              par[0] = drow["doc_no"];
              par[1] = drow["line_no"];
              par[2] = drow["ship_no"];
              par[3] = drow["pic_ticket_no"];
              DataSet DsStoshipd = objDalBaseClass.GetData(ref par, typeof(DVOOrderEntrystoshipd), objDVOOrderEntrystoshipd.GET_PICKING_DOCUMENT_STOSHIPD);

              if (Convert.ToInt32(DsStoshipd.Tables[0].Rows[0][0]) == 0)
              {
                pic_chg_flag = false;

              }

            }
            if (reprint != "Y")
              if (print_ok)
                if (pic_chg_flag == false && pic_mod_flag == false)
                {
                  DVOOrderEntrystoshipd obj = new DVOOrderEntrystoshipd();
                  object[] updParam = new object[4];
                  updParam[0] = drow["pic_ticket_no"];
                  updParam[1] = drow["doc_no"];
                  updParam[2] = drow["line_no"];
                  updParam[3] = drow["ship_no"];
                  object R = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref updParam, typeof(DVOOrderEntrystoshipd), obj.UPDATE_PICKING_DOCUMENT_STOSHIPD);
                  if (Convert.ToInt32(R) != 1)
                  {
                    if (objTransaction != null)
                      objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                  }
                }

            if ((pic_chg_flag = false) && (pic_mod_flag = false))
            {
              // # update the order header.
              DVOOrderstoordre objDVOOrderstoordreupd = new DVOOrderstoordre();
              object[] UpdPar = new object[2];
              UpdPar[0] = drow["doc_no"];
              UpdPar[1] = drow["pic_ticket_no"];
              object Result = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdPar, typeof(DVOOrderstoordre), objDVOOrderstoordreupd.UPD_PICKING_DOC_INFO_STOORDRE);


              if (Convert.ToInt32(Result) != 1)
              {

                //if (objTransaction != null)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                if (objTransaction != null)
                  objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
              }


            }
          }



        }
      }
      //}

      //catch (Exception err)
      //{
      //    if (objTransaction != null)
      //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
      //   // throw ex;
      //}

      ds.AcceptChanges();
      return ds;
    }

    public static DataSet GetOrderDefDCReasons()
    {
      return null;
    }
    public static DataSet GetODDiscDef()
    {
      return null;
    }
    public static DataSet GetODPaymentTypes()
    {
      return null;
    }
    public static DataSet GetODSPDef()
    {
      return null;
    }
    public static DataSet GET_DTL_ANLY_FOR_ALL_PAYCODE(string Empl_code, string PayrollDate)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[2];
      parameters[0] = Empl_code;
      parameters[1] = PayrollDate;
      try
      {
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployee), (new DVOMasterEmployee()).GET_DTLANALFORALLPAYCODE);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
          ds.Tables[0].Columns[1].ColumnName = "l_name";
          ds.Tables[0].Columns[2].ColumnName = "f_name";
          ds.Tables[0].Columns[3].ColumnName = "m_name";
          ds.Tables[0].Columns[4].ColumnName = "pay_code";
          ds.Tables[0].Columns[5].ColumnName = "amount";
          ds.Tables[0].Columns[6].ColumnName = "const";
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }



    //Added by Sunil Pahwa for getting the salesperson summary information
    public static DataSet get_salesPerson_SummaryInfo(DVOOrderstoordre obsDVOOrderstoordre)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[6];

      parameters[0] = obsDVOOrderstoordre.sls_psn_code;
      parameters[1] = obsDVOOrderstoordre.cust_code;
      parameters[2] = Convert.ToDateTime(obsDVOOrderstoordre.order_date).ToString("MM/dd/yyyy");
      parameters[3] = obsDVOOrderstoordre.order_no;
      parameters[4] = obsDVOOrderstoordre.order_type;
      parameters[5] = obsDVOOrderstoordre.like_type;
      try
      {
        ds = objDalBaseClass.GetData(obsDVOOrderstoordre.FINDQUERY_GET_SALESPERSON_SUMMARY(ref parameters));
        ds.Tables[0].Columns[0].ColumnName = "sls_psn_code";
        ds.Tables[0].Columns[1].ColumnName = "currency_code";
        ds.Tables[0].Columns[2].ColumnName = "currency_rate";
        ds.Tables[0].Columns[3].ColumnName = "doc_no";
        ds.Tables[0].Columns[4].ColumnName = "like_type";
        ds.Tables[0].Columns[5].ColumnName = "lo_stage";
        ds.Tables[0].Columns[6].ColumnName = "order_date";
        ds.Tables[0].Columns[7].ColumnName = "order_no";
        ds.Tables[0].Columns[8].ColumnName = "order_status";
        ds.Tables[0].Columns[9].ColumnName = "order_type";
        ds.Tables[0].Columns[10].ColumnName = "net_amount";
        ds.Tables[0].Columns[11].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[12].ColumnName = "stage";
        ds.Tables[0].Columns[13].ColumnName = "cust_code";


        //Added Later As per the requirement
        ds.Tables[0].Columns.Add("hdr_slspsn");
        ds.Tables[0].Columns.Add("hdr_type");
        ds.Tables[0].Columns.Add("customer");
        ds.Tables[0].Columns.Add("ordr_amount");
        ds.Tables[0].Columns.Add("back_amount");
        ds.Tables[0].Columns.Add("shpd_amount");



        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
          DataRow drow = ds.Tables[0].Rows[i];

          #region Before Group sls_psn_code.....

          bool _status = false;
          if (i != 0)
          {
            if (ds.Tables[0].Rows[i]["sls_psn_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["sls_psn_code"].ToString().Trim())
              _status = true;
          }
          else
            _status = true;

          if (_status)
          {
            DVOBCBudgetSets obDvoBCBudgetSets = new DVOBCBudgetSets();
            object[] Parm = new object[1];

            obDvoBCBudgetSets.src_key = Convert.ToString(drow["sls_psn_code"]);
            if (obDvoBCBudgetSets.src_key == string.Empty)
            {
              drow["hdr_slspsn"] = "UNKNOWN SALESPERSON";
            }

            else
            {
              Parm[0] = obDvoBCBudgetSets;
              //DataSet dsDesc = objDalBaseClass.GetData(ref parameters, typeof(DVOBCBudgetSets));
              object src_desc = objDalBaseClass.ExecuteScalar(ref Parm, obDvoBCBudgetSets.GET_SRC_DESC);
              string desc = Convert.ToString(src_desc);
              drow["hdr_slspsn"] = desc;

            }
          }
          #endregion

          #region Before Group like_type.....

          bool _sttus = false;
          if (i != 0)
          {
            if (ds.Tables[0].Rows[i]["like_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["like_type"].ToString().Trim())
              _status = true;
          }
          else
            _sttus = true;

          if (_sttus)
          {
            DVOOrderTypeDefinition obDVOOrderTypeDefinition = new DVOOrderTypeDefinition();

            obDVOOrderTypeDefinition.order_type = Convert.ToString(drow["order_type"]);
            if (obDVOOrderTypeDefinition.order_type != string.Empty)
            {
              object[] ordParam = new object[1];
              ordParam[0] = obDVOOrderTypeDefinition.order_type;

              object description = objDalBaseClass.ExecuteScalar(ref ordParam, obDVOOrderTypeDefinition.GET_SALES_PERSON_DESC);
              string descript = Convert.ToString(description);
              drow["hdr_type"] = "Orders of type like: " + drow["like_type"] + " - " + descript;
            }
          }

          #endregion


          DVOCustomerDetailstrcustr objBN = new DVOCustomerDetailstrcustr();
          objBN.cust_code = Convert.ToString(drow["cust_code"]);
          if (objBN.cust_code != string.Empty)
          {
            object[] CustParam = new object[1];
            CustParam[0] = objBN.cust_code;
            object Bus_Name = objDalBaseClass.ExecuteScalar(ref CustParam, objBN.GET_SALES_PERSONS_BUS_NAME);
            string BusName = Convert.ToString(Bus_Name);
            if (BusName == string.Empty)
            {
              drow["customer"] = "UNKNOWN";
            }
            else
            {
              drow["customer"] = BusName;
            }
          }
          else
          {
            drow["customer"] = "UNKNOWN";
          }

          if (Convert.ToString(drow["stage"]) != "CAN")
          {
            drow["ordr_amount"] = drow["net_amount"];
          }
          else
          {
            drow["ordr_amount"] = 0;
          }
          if ((Convert.ToString(drow["stage"]) == "SHP") || (Convert.ToString(drow["stage"]) == "INV") || (Convert.ToString(drow["stage"]) == "PST"))
          {
            drow["shpd_amount"] = drow["net_amount"];
          }
          else
          {
            drow["shpd_amount"] = 0;
          }

          if (Convert.ToString(drow["stage"]) == "BKO")
          {
            drow["back_amount"] = drow["net_amount"];
          }
          else
          {
            drow["back_amount"] = 0;
          }

        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }

    //Added by Sunil Pahwa for Getting SalesPerson Detail Information
    public static DataSet get_salesPerson_Detail(DVOOrderstoordre obtDVOOrderstoordre)
    {

      DataSet dsinfo = null;
      DVOOrderstoordre obDVOOrderstoordre = new DVOOrderstoordre();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[6];

      parameters[0] = obtDVOOrderstoordre.sls_psn_code;
      parameters[1] = obtDVOOrderstoordre.cust_code;
      parameters[2] = Convert.ToDateTime(obtDVOOrderstoordre.order_date).ToString("MM/dd/yyyy");
      parameters[3] = obtDVOOrderstoordre.order_no;
      parameters[4] = obtDVOOrderstoordre.order_type;
      parameters[5] = obtDVOOrderstoordre.like_type;
      try
      {
        dsinfo = objDalBaseClass.GetData(obtDVOOrderstoordre.FINDQUERY_GET_SALESPERSON_DETAIL(ref parameters));
        dsinfo.Tables[0].Columns[0].ColumnName = "line_no";
        dsinfo.Tables[0].Columns[1].ColumnName = "sls_psn_code";
        dsinfo.Tables[0].Columns[2].ColumnName = "currency_code";
        dsinfo.Tables[0].Columns[3].ColumnName = "currency_rate";
        dsinfo.Tables[0].Columns[4].ColumnName = "doc_no";
        dsinfo.Tables[0].Columns[5].ColumnName = "like_type";
        dsinfo.Tables[0].Columns[6].ColumnName = "lo_stage";
        dsinfo.Tables[0].Columns[7].ColumnName = "order_date";
        dsinfo.Tables[0].Columns[8].ColumnName = "order_no";
        dsinfo.Tables[0].Columns[9].ColumnName = "order_status";
        dsinfo.Tables[0].Columns[10].ColumnName = "order_type";
        dsinfo.Tables[0].Columns[11].ColumnName = "bill_to_code";
        dsinfo.Tables[0].Columns[12].ColumnName = "bko_date";
        dsinfo.Tables[0].Columns[13].ColumnName = "can_date";
        dsinfo.Tables[0].Columns[14].ColumnName = "inv_date";
        dsinfo.Tables[0].Columns[15].ColumnName = "item_code";
        dsinfo.Tables[0].Columns[16].ColumnName = "net_amount";
        dsinfo.Tables[0].Columns[17].ColumnName = "new_date";
        dsinfo.Tables[0].Columns[18].ColumnName = "ord_date";
        dsinfo.Tables[0].Columns[19].ColumnName = "pic_date";
        dsinfo.Tables[0].Columns[20].ColumnName = "pst_date";
        dsinfo.Tables[0].Columns[21].ColumnName = "sell_to_code";
        dsinfo.Tables[0].Columns[22].ColumnName = "ship_qty";
        dsinfo.Tables[0].Columns[23].ColumnName = "ship_to_code";
        dsinfo.Tables[0].Columns[24].ColumnName = "shp_date";
        dsinfo.Tables[0].Columns[25].ColumnName = "stage";
        dsinfo.Tables[0].Columns[26].ColumnName = "warehouse_code";

        //Added Later As per the requirement
        dsinfo.Tables[0].Columns.Add("hdr_slspsn");
        dsinfo.Tables[0].Columns.Add("hdr_type");
        dsinfo.Tables[0].Columns.Add("bus_name");
        dsinfo.Tables[0].Columns.Add("ordr_amount");
        dsinfo.Tables[0].Columns.Add("back_amount");
        dsinfo.Tables[0].Columns.Add("shpd_amount");
        dsinfo.Tables[0].Columns.Add("stage_date");


        for (int i = 0; i < dsinfo.Tables[0].Rows.Count; i++)
        {
          DataRow dr = dsinfo.Tables[0].Rows[i];

          DVOBCBudgetSets obDvoBCBudgetSets = new DVOBCBudgetSets();
          object[] Parm = new object[1];

          obDvoBCBudgetSets.src_key = Convert.ToString(dr["sls_psn_code"]);
          if (obDvoBCBudgetSets.src_key == string.Empty)
          {
            dr["hdr_slspsn"] = "UNKNOWN SALESPERSON";
          }

          else
          {
            Parm[0] = obDvoBCBudgetSets;
            //DataSet dsDesc = objDalBaseClass.GetData(ref parameters, typeof(DVOBCBudgetSets));
            object src_desc = objDalBaseClass.ExecuteScalar(ref Parm, obDvoBCBudgetSets.GET_SRC_DESC);
            string desc = Convert.ToString(src_desc);
            dr["hdr_slspsn"] = desc;

          }


          DVOOrderTypeDefinition obDVOOrderTypeDefinition = new DVOOrderTypeDefinition();
          obDVOOrderTypeDefinition.order_type = Convert.ToString(dr["order_type"]);
          if (obDVOOrderTypeDefinition.order_type != string.Empty)
          {
            object[] ordParam = new object[1];
            ordParam[0] = obDVOOrderTypeDefinition.order_type;

            object description = objDalBaseClass.ExecuteScalar(ref ordParam, obDVOOrderTypeDefinition.GET_SALES_PERSON_DESC);
            string descript = Convert.ToString(description);
            dr["hdr_type"] = "Orders of type like: " + dr["like_type"] + " - " + descript;
          }

          switch (dr["stage"].ToString())
          {
            case "NEW":
              dr["stage_date"] = DVOApplicationUserInfo.CurrentDate;
              break;
            case "ORD":
              dr["stage_date"] = dr["ord_date"];
              break;
            case "PIC":
              dr["stage_date"] = dr["pic_date"];
              break;
            case "SHP":
              dr["stage_date"] = dr["shp_date"];
              break;
            case "INV":
              dr["stage_date"] = dr["inv_date"];
              break;
            case "PST":
              dr["stage_date"] = dr["pst_date"];
              break;
            case "CAN":
              dr["stage_date"] = dr["can_date"];
              break;
            default:
              dr["stage_date"] = dr["ord_date"];
              break;
          }

          if (Convert.ToString(dr["stage"]) != "CAN")
          {
            dr["ordr_amount"] = dr["net_amount"];
          }
          else
          {
            dr["ordr_amount"] = 0;
          }
          if ((Convert.ToString(dr["stage"]) == "SHP") || (Convert.ToString(dr["stage"]) == "INV") || (Convert.ToString(dr["stage"]) == "PST"))
          {
            dr["shpd_amount"] = dr["net_amount"];
          }
          else
          {
            dr["shpd_amount"] = 0;
          }
          if (Convert.ToString(dr["stage"]) == "BKO")
          {
            dr["back_amount"] = dr["net_amount"];
          }
          else
          {
            dr["back_amount"] = 0;
          }
        }
      }

      catch (Exception ex)
      {
        throw ex;
      }
      return dsinfo;

    }
    //*************************************************************************************
    public static DataSet GET_CHECK_PRINTED_DATA(int cash_account, string chkdate)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[2];
      parameters[0] = cash_account;
      parameters[1] = chkdate;
      try
      {
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), (new DVOAPCheckProcessingStpcashe()).GET_CHK_PRINTED);
        ds.Tables[0].Columns[0].ColumnName = "chk_date";
        ds.Tables[0].Columns[1].ColumnName = "check_no";
        ds.Tables[0].Columns[2].ColumnName = "cash_amt";
        ds.Tables[0].Columns[3].ColumnName = "ap_type";
        ds.Tables[0].Columns[4].ColumnName = "bus_name";
        ds.Tables[0].Columns[5].ColumnName = "keyvalue";
        ds.Tables[0].Columns[6].ColumnName = "acct_desc";
        ds.Tables[0].Columns[7].ColumnName = "vend_code";
        ds.Tables[0].Columns[8].ColumnName = "printdate";
        ds.Tables[0].Columns[9].ColumnName = "chkamount";
        ds.Tables[0].Columns[10].ColumnName = "notes";

      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;

    }
    public static DataSet GET_CHECK_PRINTED_ALL(int cash_account, string chkdate)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[2];
      parameters[0] = cash_account;
      parameters[1] = chkdate;
      try
      {
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), (new DVOAPCheckProcessingStpcashe()).GET_CHK_PRINTED_ALL);
        ds.Tables[0].Columns[0].ColumnName = "chk_date";
        ds.Tables[0].Columns[1].ColumnName = "check_no";
        ds.Tables[0].Columns[2].ColumnName = "cash_amt";
        ds.Tables[0].Columns[3].ColumnName = "ap_type";
        ds.Tables[0].Columns[4].ColumnName = "bus_name";
        ds.Tables[0].Columns[5].ColumnName = "keyvalue";
        ds.Tables[0].Columns[6].ColumnName = "acct_desc";
        ds.Tables[0].Columns[7].ColumnName = "vend_code";
        ds.Tables[0].Columns[8].ColumnName = "printdate";
        ds.Tables[0].Columns[9].ColumnName = "chkamount";
        ds.Tables[0].Columns[10].ColumnName = "notes";


      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;

    }
    public static DataSet GET_CHECK_PRINTED_DETAILDATA(int cash_account, string chkdate)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[2];
      parameters[0] = cash_account;
      parameters[1] = chkdate;
      try
      {

        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), (new DVOAPCheckProcessingStpcashe()).GET_CHK_PRINTED_DTL);
        ds.Tables[0].TableName = "chk_dtl";
        ds.Tables[0].Columns[0].ColumnName = "chk_date";
        ds.Tables[0].Columns[1].ColumnName = "doc_no";
        ds.Tables[0].Columns[2].ColumnName = "pay_to_code";
        ds.Tables[0].Columns[3].ColumnName = "check_no";
        ds.Tables[0].Columns[4].ColumnName = "doc_desc";
        ds.Tables[0].Columns[5].ColumnName = "ok_to_post";
        ds.Tables[0].Columns[6].ColumnName = "ap_type";
        ds.Tables[0].Columns[7].ColumnName = "min_voucher_no";
        ds.Tables[0].Columns[8].ColumnName = "tre_voucher_no";
        ds.Tables[0].Columns[9].ColumnName = "inv_doc_no";
        ds.Tables[0].Columns[10].ColumnName = "inv_no";
        ds.Tables[0].Columns[11].ColumnName = "dist_acct";
        ds.Tables[0].Columns[12].ColumnName = "dist_amt";
        ds.Tables[0].Columns[13].ColumnName = "dist_deb_cred";
        ds.Tables[0].Columns[14].ColumnName = "disc_acct";
        ds.Tables[0].Columns[15].ColumnName = "disc_amt";
        ds.Tables[0].Columns[16].ColumnName = "disc_deb_cred";
        ds.Tables[0].Columns[17].ColumnName = "oa_acct";
        ds.Tables[0].Columns[18].ColumnName = "oa_amt";
        ds.Tables[0].Columns[19].ColumnName = "oa_deb_cred";

        ds.Tables[0].Columns[18].ColumnName = "printdate";
        ds.Tables[0].Columns[19].ColumnName = "printchkamount";

      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;

    }

    //Added By Rahul Jain On 07/23/2009 for getting Print Daily Cash Details Report
    public static DataSet GetDailyCashDetails(string DateFrom, string DateTo)
    {
      DataTable objTable = new DataTable();
      Object[] parameters = new object[2];
      parameters[0] = DateFrom;
      parameters[1] = DateTo;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCASHDetail = objDalBaseClass.GetData(ref parameters, typeof(DVOARCashProcessingStrcashe), (new DVOARCashProcessingStrcashe()).FIND_DAILY_CASH_DETAILS);
      if (dsCASHDetail.Tables.Count > 0)
      {

        dsCASHDetail.Tables[0].TableName = "dscashdetail";
        dsCASHDetail.Tables[0].Columns[0].ColumnName = "rcpt_date";
        dsCASHDetail.Tables[0].Columns[1].ColumnName = "doc_no";
        dsCASHDetail.Tables[0].Columns[2].ColumnName = "doc_desc";
        dsCASHDetail.Tables[0].Columns[3].ColumnName = "check_no";
        dsCASHDetail.Tables[0].Columns[4].ColumnName = "batch_id";
        dsCASHDetail.Tables[0].Columns[5].ColumnName = "min_voucher_no";
        dsCASHDetail.Tables[0].Columns[6].ColumnName = "tre_voucher_no";
        dsCASHDetail.Tables[0].Columns[7].ColumnName = "cust_code";

        dsCASHDetail.Tables[0].Columns[8].ColumnName = "cashamount";
        dsCASHDetail.Tables[0].Columns[9].ColumnName = "cashdebit_credit";
        dsCASHDetail.Tables[0].Columns[10].ColumnName = "cashkeyvalue";
        dsCASHDetail.Tables[0].Columns[11].ColumnName = "cashacct_desc";

        dsCASHDetail.Tables[0].Columns[12].ColumnName = "distamount";
        dsCASHDetail.Tables[0].Columns[13].ColumnName = "distdebit_credit";
        dsCASHDetail.Tables[0].Columns[14].ColumnName = "distkeyvalue";
        dsCASHDetail.Tables[0].Columns[15].ColumnName = "distacct_desc";

        dsCASHDetail.Tables[0].Columns[16].ColumnName = "discamount";
        dsCASHDetail.Tables[0].Columns[17].ColumnName = "discdebit_credit";
        dsCASHDetail.Tables[0].Columns[18].ColumnName = "disckeyvalue";
        dsCASHDetail.Tables[0].Columns[19].ColumnName = "discacct_desc";
        dsCASHDetail.Tables[0].Columns[20].ColumnName = "bus_name";
        dsCASHDetail.Tables[0].Columns[21].ColumnName = "dist_acct";
        dsCASHDetail.Tables[0].Columns.Add("ar_src");
        DataView dv = dsCASHDetail.Tables[0].DefaultView;
        dv.Sort = "doc_no";
        objTable = dv.ToTable();
        int last_doc_no = 0;
        int current_doc_no = 0;
        for (int i = 0; i < objTable.Rows.Count; i++)
        {
          DataRow dr = objTable.Rows[i];
          string distacct_desc = dr["distacct_desc"] != DBNull.Value ? Convert.ToString(dr["distacct_desc"]).Trim() : string.Empty;
          if (distacct_desc == "NATIONAL SAVINGS SCHEME")
            dr["ar_src"] = "NATIONAL SAVINGS SCHEME";
          else if (distacct_desc == "SAVINGS BANK")
            dr["ar_src"] = "SAVINGS BANK";
          else
            dr["ar_src"] = null;
          current_doc_no = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0;
          bool _status = false;
          if (i != 0)
          {
            if (current_doc_no != last_doc_no)
            {
              _status = true;

            }

          }
          else
            _status = true;
          if (_status)
          {
            last_doc_no = current_doc_no;
            _status = false;
          }
          else
          {
            dr["cashamount"] = 0;
          }
        }


      }
      dsCASHDetail.Tables.Clear();
      dsCASHDetail.Tables.Add(objTable);
      return dsCASHDetail;

    }

    public static DataSet GetDailyCashSummary(string DateFrom, string DateTo)
    {
      Object[] parameters = new object[2];
      parameters[0] = DateFrom;
      parameters[1] = DateTo;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCASHDetail = objDalBaseClass.GetData(ref parameters, typeof(DVOARCashProcessingStrcashe), (new DVOARCashProcessingStrcashe()).FIND_DAILYCASHSUM);
      if (dsCASHDetail.Tables.Count > 0)
      {
        dsCASHDetail.Tables[0].TableName = "dscashdetail";
        dsCASHDetail.Tables[0].Columns[0].ColumnName = "rcpt_date";
        dsCASHDetail.Tables[0].Columns[1].ColumnName = "doc_no";
        dsCASHDetail.Tables[0].Columns[2].ColumnName = "doc_desc";
        dsCASHDetail.Tables[0].Columns[3].ColumnName = "check_no";
        dsCASHDetail.Tables[0].Columns[4].ColumnName = "batch_id";
        dsCASHDetail.Tables[0].Columns[5].ColumnName = "amount";
        dsCASHDetail.Tables[0].Columns[6].ColumnName = "DebitCredit";
        dsCASHDetail.Tables[0].Columns[7].ColumnName = "keyvalue";
        dsCASHDetail.Tables[0].Columns[8].ColumnName = "acct_desc";
        dsCASHDetail.Tables[0].Columns[9].ColumnName = "const";
      }
      return dsCASHDetail;
    }

    //*************************************************************

    //Added by Sunil Pahwa for getting vendor balance information
    public static DataSet Get_Vendor_Balance(DvoVendorInfo obDvoVendorInfo)
    {
      DataSet ds = null;
      //DvoVendorInfo obDvoVendorInf = new DvoVendorInfo();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[2];

      parameters[0] = obDvoVendorInfo.vend_code;
      parameters[1] = obDvoVendorInfo.bus_name;

      try
      {
        ds = objDalBaseClass.GetData(obDvoVendorInfo.FIND_VENDOR_BALANCE_INFO(ref parameters));
        ds.Tables[0].Columns[0].ColumnName = "vend_code";
        ds.Tables[0].Columns[1].ColumnName = "bus_name";
        ds.Tables[0].Columns[2].ColumnName = "contact";
        ds.Tables[0].Columns[3].ColumnName = "phone";
        ds.Tables[0].Columns[4].ColumnName = "acct_bal";
        ds.Tables[0].Columns[5].ColumnName = "balance";
        ds.Tables[0].Columns[6].ColumnName = "inv_no";
        ds.Tables[0].Columns[7].ColumnName = "inv_desc";
        ds.Tables[0].Columns[8].ColumnName = "inv_date";
        ds.Tables[0].Columns[9].ColumnName = "disc_amt";
        ds.Tables[0].Columns[10].ColumnName = "disc_bal";
        ds.Tables[0].Columns[11].ColumnName = "orig_amount";
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }
    //Added By Rahul Jain On 07/24/2009 for getting Print Vendor Invoice Details Report
    public static DataSet GetVendorInvoiceDetails(string DateFrom, string DateTo)
    {
      Object[] parameters = new object[2];
      parameters[0] = DateFrom;
      parameters[1] = DateTo;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DvoVendorInfo), (new DvoVendorInfo()).FIND_VENDOR_INVOICE_DETAILS);
      if (ds.Tables.Count > 0)
      {
        ds.Tables[0].TableName = "DSVndInvDtl";
        ds.Tables[0].Columns[0].ColumnName = "v_vend_code";
        ds.Tables[0].Columns[1].ColumnName = "v_bus_name";
        ds.Tables[0].Columns[2].ColumnName = "v_inv_no";
        ds.Tables[0].Columns[3].ColumnName = "v_ap_amount";
        ds.Tables[0].Columns[4].ColumnName = "v_inv_disc_amt";
        ds.Tables[0].Columns[5].ColumnName = "v_inv_posted";
        ds.Tables[0].Columns[6].ColumnName = "v_inv_date";
        ds.Tables[0].Columns[7].ColumnName = "v_cashd_doc_no";
        ds.Tables[0].Columns[8].ColumnName = "v_cashd_dist_amt";
        ds.Tables[0].Columns[9].ColumnName = "v_cashd_disc_amt";
        ds.Tables[0].Columns[10].ColumnName = "v_cashd_ok_to_post";
      }
      return ds;
    }
    //*************************************************************

    //Added By Rajeev 
    //date :25/07/2009
    //Aim: Business logic For Customer Details Report
    #region Function Used For CustomerDetail Report
    public static DataSet GetCustomerDetails(ref DVOOrderCustomerInfostrcustr objDVOOrderCustomerInfostrcustr)
    {


      DataTable objDataTable = new DataTable();
      DataSet DsFinal = new DataSet();

      Object[] parameters = new object[3];

      parameters[0] = objDVOOrderCustomerInfostrcustr.cust_code;
      parameters[1] = Convert.ToDateTime(objDVOOrderCustomerInfostrcustr.stDate).ToString("MM/dd/yyyy");
      parameters[2] = Convert.ToDateTime(objDVOOrderCustomerInfostrcustr.endDate).ToString("MM/dd/yyyy");


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCustDtl = objDalBaseClass.GetData(objDVOOrderCustomerInfostrcustr.FINDQUERY_CUSTOMER_DETAILS(ref parameters));
      if (dsCustDtl.Tables.Count > 0)
      {
        dsCustDtl.Tables[0].TableName = "DTCustDtl";
        dsCustDtl.Tables[0].Columns[0].ColumnName = "currency_code";
        dsCustDtl.Tables[0].Columns[1].ColumnName = "currency_rate";
        dsCustDtl.Tables[0].Columns[2].ColumnName = "doc_no";
        dsCustDtl.Tables[0].Columns[3].ColumnName = "gross_margin";
        dsCustDtl.Tables[0].Columns[4].ColumnName = "inv_date";
        dsCustDtl.Tables[0].Columns[5].ColumnName = "inv_doc_no";
        dsCustDtl.Tables[0].Columns[6].ColumnName = "item_code";
        dsCustDtl.Tables[0].Columns[7].ColumnName = "net_amount";
        dsCustDtl.Tables[0].Columns[8].ColumnName = "sell_to_code";
        dsCustDtl.Tables[0].Columns[9].ColumnName = "bus_name";

        dsCustDtl.Tables[0].Columns.Add("currency", typeof(string));
        dsCustDtl.Tables[0].Columns.Add("likeType", typeof(string));

        objDataTable = dsCustDtl.Tables[0].Clone();
        if (dsCustDtl.Tables[0].Rows.Count != 0)
        {
          for (int i = 0; i < dsCustDtl.Tables[0].Rows.Count; i++)
          {
            DataRow dr = dsCustDtl.Tables[0].Rows[i];
            if (DVOApplicationUserInfo._mcurr == "Y")
            {
              #region Implement Multicurrency
              //Rest to Implement Multicurrency
              //if mcurr = "Y"
              //then
              //    #_get_curr_rate
              //    # if report is in home currency get exchange rate
              //    let rpt.currency_rate = get_exchg_rate(curs.currency_code,
              //    home_currency, rate_type, today)
              //    # if currency_rate not found, print an error on the report
              //    # and use 1 as exchange rate
              //    if rpt.currency_rate = -1
              //    then
              //        let rpt.currency_problem =
              //            "Error: Exchange Rate Not Found, Using Invoice's ",
              //            "Exchange Rate"
              //        # there was a problem finding exchange rate
              //        # use order's exchange rate
              //        let rpt.currency_rate = curs.currency_rate
              //    end if
              //    # translate amounts to home currency
              //    let curs.net_amount = calc_currency(rpt.currency_rate,
              //        curs.net_amount,"T")
              //    let curs.gross_margin= calc_currency(rpt.currency_rate,
              //        curs.gross_margin,"T")
              //end if
              #endregion Implement Multicurrency
            }

            //Find Like type to check the Credit memo
            //if like type is CRM then set curs.net_amount to negitive
            if (dr["doc_no"] != DBNull.Value)
              dr["likeType"] = Get_LikeTypeByDocNumber(Convert.ToInt32(dr["doc_no"]));

            if (dr["likeType"] != DBNull.Value)
              if (dr["likeType"].ToString() != "")
                if (dr["likeType"].ToString() != "CRM")
                {
                  if (dr["net_amount"] != DBNull.Value)
                    dr["net_amount"] = Convert.ToDecimal(dr["net_amount"]) * -1;
                  if (dr["gross_margin"] != DBNull.Value)
                    dr["gross_margin"] = Convert.ToDecimal(dr["gross_margin"]) * -1;
                }

            objDataTable.Rows.Add(dr.ItemArray);

          }
        }

      }

      DsFinal.Tables.Add(objDataTable);
      return DsFinal;
    }

    public static string Get_LikeTypeByDocNumber(int doc_num)
    {
      DVOOrderCustomerInfostrcustr objDVOOrderCustomerInfostrcustr = new DVOOrderCustomerInfostrcustr();
      object[] parameter = new object[1];
      parameter[0] = doc_num;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dslikeType = objDalBaseClass.GetData(ref parameter, typeof(DVOOrderCustomerInfostrcustr), objDVOOrderCustomerInfostrcustr.GET_LIKE_TYPE);
      if (dslikeType.Tables[0].Rows.Count > 0)
      {
        return dslikeType.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }

    }

    #endregion Function Used For CustomerDetail Report


    #region  Function Used For SalesPersonSummery Report
    //Added By Rajeev 
    //date :28/07/2009
    //Aim: Business logic For Sales Person Summery Report
    public static DataSet GetSalespersonSummery(ref DVOOrderDetailstoordrd objDVOOrderDetailstoordrd)
    {


      DataTable objDataTable = new DataTable();
      DataSet DsFinal = new DataSet();

      Object[] parameters = new object[3];

      parameters[0] = objDVOOrderDetailstoordrd.sls_psn_code;
      parameters[1] = Convert.ToDateTime(objDVOOrderDetailstoordrd.stDate).ToString("MM/dd/yyyy");
      parameters[2] = Convert.ToDateTime(objDVOOrderDetailstoordrd.endDate).ToString("MM/dd/yyyy");


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSlsSumm = objDalBaseClass.GetData(objDVOOrderDetailstoordrd.FINDQUERY_SALESPERSON_SUMMERY(ref parameters));
      if (dsSlsSumm.Tables.Count > 0)
      {

        dsSlsSumm.Tables[0].TableName = "DTSlsSumm";
        dsSlsSumm.Tables[0].Columns[0].ColumnName = "currency_code";
        dsSlsSumm.Tables[0].Columns[1].ColumnName = "currency_rate";
        dsSlsSumm.Tables[0].Columns[2].ColumnName = "inv_date";
        dsSlsSumm.Tables[0].Columns[3].ColumnName = "sls_psn_code";
        dsSlsSumm.Tables[0].Columns[4].ColumnName = "doc_no";
        dsSlsSumm.Tables[0].Columns[5].ColumnName = "gross_margin";
        dsSlsSumm.Tables[0].Columns[6].ColumnName = "net_amount";

        dsSlsSumm.Tables[0].Columns.Add("currency", typeof(string));
        dsSlsSumm.Tables[0].Columns.Add("likeType", typeof(string));
        dsSlsSumm.Tables[0].Columns.Add("sls_psn", typeof(string));

        objDataTable = dsSlsSumm.Tables[0].Clone();
        if (dsSlsSumm.Tables[0].Rows.Count != 0)
        {
          for (int i = 0; i < dsSlsSumm.Tables[0].Rows.Count; i++)
          {
            DataRow dr = dsSlsSumm.Tables[0].Rows[i];
            if (DVOApplicationUserInfo._mcurr == "Y")
            {
              #region Implement Multicurrency
              //Rest to Implement Multicurrency
              // if mcurr = "Y"
              //then
              //    #_get_curr_rate
              //    # if report is in home currency get exchange rate
              //    let rpt.currency_rate = get_exchg_rate(curs.currency_code,
              //    home_currency, rate_type, today)
              //    # if currency_rate not found, print an error on the report
              //    # and use 1 as exchange rate
              //    if rpt.currency_rate = -1
              //    then
              //        let rpt.currency_problem =
              //            "Error: Exchange Rate Not Found, Using Invoice's ",
              //            "Exchange Rate"
              //        # there was a problem finding exchange rate
              //        # use order's exchange rate
              //        let rpt.currency_rate = curs.currency_rate
              //    end if
              //    #_trans_amt # translate amounts to home currency
              //    let curs.net_amount = calc_currency(rpt.currency_rate,
              //        curs.net_amount,"T")
              //    let curs.gross_margin= calc_currency(rpt.currency_rate,
              //        curs.gross_margin,"T")
              //end if

              #endregion Implement Multicurrency
            }

            if (dr["sls_psn_code"] != DBNull.Value)
              dr["sls_psn"] = Get_SalesPersonName(dr["sls_psn_code"].ToString());
            if (dr["sls_psn"] != DBNull.Value)
              if (dr["sls_psn"].ToString() == "")
              {
                dr["sls_psn"] = "UNKNOWN SALESPERSON";
              }
              else
              {
                dr["sls_psn"] = dr["sls_psn_code"].ToString() + " " + "-" + " " + dr["sls_psn"].ToString();
              }

            //Find Like type to check the Credit memo
            //if like type is CRM then set curs.net_amount to negitive
            if (dr["doc_no"] != DBNull.Value)
              dr["likeType"] = Get_LikeTypeByDocNumber(Convert.ToInt32(dr["doc_no"]));

            if (dr["likeType"] != DBNull.Value)
              if (dr["likeType"].ToString() != "")
                if (dr["likeType"].ToString() != "CRM")
                {
                  if (dr["net_amount"] != DBNull.Value)
                    dr["net_amount"] = Convert.ToDecimal(dr["net_amount"]) * -1;
                  if (dr["gross_margin"] != DBNull.Value)
                    dr["gross_margin"] = Convert.ToDecimal(dr["gross_margin"]) * -1;
                }

            objDataTable.Rows.Add(dr.ItemArray);

          }
        }

      }

      DsFinal.Tables.Add(objDataTable);
      return DsFinal;
    }

    /// <summary>
    /// Added By Rajeev 
    /// Date: 29/07/2009
    /// Function used to Generate the SalesPerson Detials Report
    /// </summary>
    /// <param name="objDVOOrderDetailstoordrd"></param>
    /// <returns>Dataset</returns>
    public static DataSet GetSalespersonDetails(ref DVOOrderDetailstoordrd objDVOOrderDetailstoordrd)
    {


      DataTable objDataTable = new DataTable();
      DataSet DsFinal = new DataSet();

      Object[] parameters = new object[3];

      parameters[0] = objDVOOrderDetailstoordrd.sls_psn_code;
      parameters[1] = Convert.ToDateTime(objDVOOrderDetailstoordrd.stDate).ToString("MM/dd/yyyy");
      parameters[2] = Convert.ToDateTime(objDVOOrderDetailstoordrd.endDate).ToString("MM/dd/yyyy");


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSlsDtl = objDalBaseClass.GetData(objDVOOrderDetailstoordrd.FINDQUERY_SALESPERSON_DETAILS(ref parameters));
      if (dsSlsDtl.Tables.Count > 0)
      {

        dsSlsDtl.Tables[0].TableName = "DTSlsSumm";
        dsSlsDtl.Tables[0].Columns[0].ColumnName = "currency_code";
        dsSlsDtl.Tables[0].Columns[1].ColumnName = "currency_rate";
        dsSlsDtl.Tables[0].Columns[2].ColumnName = "inv_date";
        dsSlsDtl.Tables[0].Columns[3].ColumnName = "sell_to_code";
        dsSlsDtl.Tables[0].Columns[4].ColumnName = "tax_amount";
        dsSlsDtl.Tables[0].Columns[5].ColumnName = "sls_psn_code";
        dsSlsDtl.Tables[0].Columns[6].ColumnName = "doc_no";
        dsSlsDtl.Tables[0].Columns[7].ColumnName = "inv_doc_no";
        dsSlsDtl.Tables[0].Columns[8].ColumnName = "net_amount";

        dsSlsDtl.Tables[0].Columns.Add("currency", typeof(string));
        dsSlsDtl.Tables[0].Columns.Add("likeType", typeof(string));
        dsSlsDtl.Tables[0].Columns.Add("salesperson", typeof(string));

        objDataTable = dsSlsDtl.Tables[0].Clone();
        if (dsSlsDtl.Tables[0].Rows.Count != 0)
        {
          for (int i = 0; i < dsSlsDtl.Tables[0].Rows.Count; i++)
          {
            DataRow dr = dsSlsDtl.Tables[0].Rows[i];
            if (DVOApplicationUserInfo._mcurr == "Y")
            {
              #region Implement Multicurrency
              //Rest to Implement Multicurrency
              // if mcurr = "Y"
              //then
              //    #_get_curr_rate
              //    # if report is in home currency get exchange rate
              //    let rpt.currency_rate = get_exchg_rate(curs.currency_code,
              //    home_currency, rate_type, today)
              //    # if currency_rate not found, print an error on the report
              //    # and use 1 as exchange rate
              //    if rpt.currency_rate = -1
              //    then
              //        let rpt.currency_problem =
              //            "Error: Exchange Rate Not Found, Using Invoice's ",
              //            "Exchange Rate"
              //        # there was a problem finding exchange rate
              //        # use order's exchange rate
              //        let rpt.currency_rate = curs.currency_rate
              //    end if
              //    #_trans_amt # translate amounts to home currency
              //    let curs.net_amount = calc_currency(rpt.currency_rate,
              //        curs.net_amount,"T")
              //    let curs.gross_margin= calc_currency(rpt.currency_rate,
              //        curs.gross_margin,"T")
              //end if

              #endregion Implement Multicurrency
            }

            if (dr["sls_psn_code"] != DBNull.Value)
              dr["salesperson"] = Get_SalesPersonName(dr["sls_psn_code"].ToString());
            if (dr["salesperson"] != DBNull.Value)
              if (dr["salesperson"].ToString() == "")
              {
                dr["salesperson"] = "UNKNOWN SALESPERSON";
              }
              else
              {
                dr["salesperson"] = dr["sls_psn_code"].ToString() + " " + "-" + " " + dr["salesperson"].ToString();
              }



            //Find Like type to check the Credit memo
            //if like type is CRM then set curs.net_amount to negitive
            if (dr["doc_no"] != DBNull.Value)
              dr["likeType"] = Get_LikeTypeByDocNumber(Convert.ToInt32(dr["doc_no"]));

            if (dr["likeType"] != DBNull.Value)
              if (dr["likeType"].ToString() != "")
                if (dr["likeType"].ToString() != "CRM")
                {
                  if (dr["net_amount"] != DBNull.Value)
                    dr["net_amount"] = Convert.ToDecimal(dr["net_amount"]) * -1;
                  if (dr["tax_amount"] != DBNull.Value)
                    dr["tax_amount"] = Convert.ToDecimal(dr["gross_margin"]) * -1;
                }

            objDataTable.Rows.Add(dr.ItemArray);

          }
        }

      }

      DsFinal.Tables.Add(objDataTable);
      return DsFinal;
    }


    /// <summary>
    /// Added By Rajeev 
    /// Date: 29/07/2009
    /// Function used to Generate the SalesPerson By Product Report
    /// </summary>
    /// <param name="objDVOOrderDetailstoordrd"></param>
    /// <returns>Dataset</returns>
    public static DataSet GetSalespersonDetailsByProduct(ref DVOOrderDetailstoordrd objDVOOrderDetailstoordrd)
    {

      DataTable objDataTable = new DataTable();
      DataSet DsFinal = new DataSet();

      Object[] parameters = new object[3];

      parameters[0] = objDVOOrderDetailstoordrd.sls_psn_code;
      parameters[1] = Convert.ToDateTime(objDVOOrderDetailstoordrd.stDate).ToString("MM/dd/yyyy");
      parameters[2] = Convert.ToDateTime(objDVOOrderDetailstoordrd.endDate).ToString("MM/dd/yyyy");


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSlsDtl = objDalBaseClass.GetData(objDVOOrderDetailstoordrd.FINDQUERY_SALESPERSON_BYPRODUCT(ref parameters));
      if (dsSlsDtl.Tables.Count > 0)
      {

        dsSlsDtl.Tables[0].TableName = "DTSlsSumm";
        dsSlsDtl.Tables[0].Columns[0].ColumnName = "item_class";
        dsSlsDtl.Tables[0].Columns[1].ColumnName = "inv_date";
        dsSlsDtl.Tables[0].Columns[2].ColumnName = "sls_psn_code";
        dsSlsDtl.Tables[0].Columns[3].ColumnName = "doc_no";
        dsSlsDtl.Tables[0].Columns[4].ColumnName = "gross_margin";
        dsSlsDtl.Tables[0].Columns[5].ColumnName = "net_amount";


        dsSlsDtl.Tables[0].Columns.Add("item_desc", typeof(string));
        dsSlsDtl.Tables[0].Columns.Add("likeType", typeof(string));
        dsSlsDtl.Tables[0].Columns.Add("salesperson", typeof(string));

        objDataTable = dsSlsDtl.Tables[0].Clone();
        if (dsSlsDtl.Tables[0].Rows.Count != 0)
        {
          for (int i = 0; i < dsSlsDtl.Tables[0].Rows.Count; i++)
          {
            DataRow dr = dsSlsDtl.Tables[0].Rows[i];
            if (DVOApplicationUserInfo._mcurr == "Y")
            {
              #region Implement Multicurrency
              //Rest to Implement Multicurrency
              // if mcurr = "Y"
              //then
              //    #_get_curr_rate
              //    # if report is in home currency get exchange rate
              //    let rpt.currency_rate = get_exchg_rate(curs.currency_code,
              //    home_currency, rate_type, today)
              //    # if currency_rate not found, print an error on the report
              //    # and use 1 as exchange rate
              //    if rpt.currency_rate = -1
              //    then
              //        let rpt.currency_problem =
              //            "Error: Exchange Rate Not Found, Using Invoice's ",
              //            "Exchange Rate"
              //        # there was a problem finding exchange rate
              //        # use order's exchange rate
              //        let rpt.currency_rate = curs.currency_rate
              //    end if
              //    #_trans_amt # translate amounts to home currency
              //    let curs.net_amount = calc_currency(rpt.currency_rate,
              //        curs.net_amount,"T")
              //    let curs.gross_margin= calc_currency(rpt.currency_rate,
              //        curs.gross_margin,"T")
              //end if

              #endregion Implement Multicurrency
            }

            if (dr["item_class"] != DBNull.Value)
              dr["item_desc"] = Get_ProductDescription(dr["item_class"].ToString());
            if (dr["item_desc"] != DBNull.Value)
              if (dr["item_desc"].ToString() == "")
              {
                dr["item_desc"] = "UNKNOWN PRODUCT TYPE";
              }
              else
              {
                dr["item_desc"] = dr["item_class"].ToString() + " " + "-" + " " + dr["item_desc"].ToString();
              }

            if (dr["sls_psn_code"] != DBNull.Value)
              dr["salesperson"] = Get_SalesPersonName(dr["sls_psn_code"].ToString());
            if (dr["salesperson"] != DBNull.Value)
              if (dr["salesperson"].ToString() == "")
              {
                dr["salesperson"] = "UNKNOWN SALESPERSON";
              }
              else
              {
                dr["salesperson"] = dr["sls_psn_code"].ToString() + " " + "-" + " " + dr["salesperson"].ToString();
              }



            //Find Like type to check the Credit memo
            //if like type is CRM then set curs.net_amount to negitive
            if (dr["doc_no"] != DBNull.Value)
              dr["likeType"] = Get_LikeTypeByDocNumber(Convert.ToInt32(dr["doc_no"]));

            if (dr["likeType"] != DBNull.Value)
              if (dr["likeType"].ToString() != "")
                if (dr["likeType"].ToString() != "CRM")
                {
                  if (dr["net_amount"] != DBNull.Value)
                    dr["net_amount"] = Convert.ToDecimal(dr["net_amount"]) * -1;
                  if (dr["tax_amount"] != DBNull.Value)
                    dr["tax_amount"] = Convert.ToDecimal(dr["gross_margin"]) * -1;
                }

            objDataTable.Rows.Add(dr.ItemArray);

          }
        }

      }

      DsFinal.Tables.Add(objDataTable);
      return DsFinal;
    }


    public static string Get_SalesPersonName(string sls_psn_code)
    {
      DVOOrderDetailstoordrd objDVOOrderDetailstoordrd = new DVOOrderDetailstoordrd();
      object[] parameter = new object[1];
      parameter[0] = sls_psn_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsslsPsnName = objDalBaseClass.GetData(ref parameter, typeof(DVOOrderCustomerInfostrcustr), objDVOOrderDetailstoordrd.SALESPERSON_NAME);
      if (dsslsPsnName.Tables[0].Rows.Count > 0)
      {
        return dsslsPsnName.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public static string Get_ProductDescription(string itemClass)
    {
      DVOOrderDetailstoordrd objDVOOrderDetailstoordrd = new DVOOrderDetailstoordrd();
      object[] parameter = new object[1];
      parameter[0] = itemClass;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsPrDesc = objDalBaseClass.GetData(ref parameter, typeof(DVOOrderCustomerInfostrcustr), objDVOOrderDetailstoordrd.PRODUCT_DESC);
      if (dsPrDesc.Tables[0].Rows.Count > 0)
      {
        return dsPrDesc.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    #endregion Function Used For SalesPersonSummery Report

    //Added By Rahul Jain On 07/24/2009 for getting Print Vendor Invoice Details Report
    public static DataSet GetOrderStatus(ref DVOOrderstoordre objDVOOrderstoordre)
    {
      Object[] parameters = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDVOOrderstoordre.FINDQUERY_ORDER_STATUS(ref parameters));
      //if (ds.Tables.Count > 0)
      return ds;
    }
    //*************************************************************

    //Added By Rahul Jain On 07/24/2009 for getting Print Period Exchange Rates Report
    public static DataSet GetPeriodExchngeRate(ref DVOMCstxpcrtr objDVOMCstxpcrtr)
    {
      Object[] parameters = new object[5];
      parameters[0] = objDVOMCstxpcrtr.from_currency_code;
      parameters[1] = objDVOMCstxpcrtr.rate_type;
      parameters[2] = objDVOMCstxpcrtr.period;
      parameters[3] = objDVOMCstxpcrtr.period_year;
      parameters[4] = objDVOMCstxpcrtr.rate;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDVOMCstxpcrtr.FINDQUERY_PERIODEXCHENGE_RATES(ref parameters));
      //if (ds.Tables.Count > 0)
      return ds;
    }
    //*************************************************************

    //Added By Rahul Jain On 07/24/2009 for getting Print Vendor Invoice Details Report
    public static DataSet GetMultilevelTaxCodes(ref DVOMultiTaxstxmtaxr objDVOMultiTaxstxmtaxr)
    {
      Object[] parameters = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDVOMultiTaxstxmtaxr.FINDQUERY_MULTILEVELTAX_CODES(ref parameters));
      //if (ds.Tables.Count > 0)
      return ds;
    }
    //*************************************************************

    //Added By Rahul Jain On 07/24/2009 for getting Print Vendor Invoice Details Report
    public static DataSet GetMultilevelTaxGroups(ref DVOMultiTaxstxmtaxr objDVOMultiTaxstxmtaxr)
    {
      Object[] parameters = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDVOMultiTaxstxmtaxr.FINDQUERY_MULTILEVELTAX_GROUPS(ref parameters));
      //if (ds.Tables.Count > 0)
      return ds;
    }
    //*************************************************************

    public static DataSet GetNssDepositWithdrawn(string datefrom, string dateto)
    {

      object[] parameters = new object[2];
      parameters[0] = datefrom;
      parameters[1] = dateto;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), (new DVONSSDetail().GET_NSS_DEP_WED));
      ds.Tables[0].Columns[0].ColumnName = "account_no";
      ds.Tables[0].Columns[1].ColumnName = "contract_no";
      ds.Tables[0].Columns[2].ColumnName = "name";
      ds.Tables[0].Columns[3].ColumnName = "title";
      ds.Tables[0].Columns[4].ColumnName = "nss_status";
      ds.Tables[0].Columns[5].ColumnName = "payment_date";
      ds.Tables[0].Columns[6].ColumnName = "interest_paid";
      ds.Tables[0].Columns[7].ColumnName = "bonus_paid";
      ds.Tables[0].Columns[8].ColumnName = "current_bal";
      ds.Tables[0].Columns[9].ColumnName = "AmountDeposit";
      ds.Tables[0].Columns[10].ColumnName = "AmountWithdraw";
      return ds;

    }
    //*********Added by Sunil Pahwa For get Order Open Item by Summary *********************
    public static DataSet GetOrder_Open_Item_Info(DVOOrderEntrystoshipd objDvoDVOEntrystoshipd)
    {
      Object[] parameters = new object[4];
      parameters[0] = objDvoDVOEntrystoshipd.item_code;
      parameters[1] = objDvoDVOEntrystoshipd.warehouse_code;
      parameters[2] = objDvoDVOEntrystoshipd.like_type;
      parameters[3] = objDvoDVOEntrystoshipd.stage;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      DataSet ds = objDalBaseClass.GetData(objDvoDVOEntrystoshipd.FIND_ORDER_OPEN_ITEM_SUMMARY(ref parameters));

      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "desc1";
        ds.Tables[0].Columns[1].ColumnName = "desc2";
        ds.Tables[0].Columns[2].ColumnName = "currency_code";
        ds.Tables[0].Columns[3].ColumnName = "currency_rate";
        ds.Tables[0].Columns[4].ColumnName = "like_type";
        ds.Tables[0].Columns[5].ColumnName = "bko_date";
        ds.Tables[0].Columns[6].ColumnName = "can_date";
        ds.Tables[0].Columns[7].ColumnName = "inv_date";
        ds.Tables[0].Columns[8].ColumnName = "item_code";
        ds.Tables[0].Columns[9].ColumnName = "net_amount";
        ds.Tables[0].Columns[10].ColumnName = "new_date";
        ds.Tables[0].Columns[11].ColumnName = "ord_date";
        ds.Tables[0].Columns[12].ColumnName = "pic_date";
        ds.Tables[0].Columns[13].ColumnName = "pst_date";
        ds.Tables[0].Columns[14].ColumnName = "ship_qty";
        ds.Tables[0].Columns[15].ColumnName = "shp_date";
        ds.Tables[0].Columns[16].ColumnName = "stage";
        ds.Tables[0].Columns[17].ColumnName = "warehouse_code";
        ds.Tables[0].Columns[18].ColumnName = "order_type";


        //Added Later As per the requirement
        ds.Tables[0].Columns.Add("description");
        // ds.Tables[0].Columns.Add("like_type");
        ds.Tables[0].Columns.Add("hdr_type");
        ds.Tables[0].Columns.Add("t_ordr_amt");
        ds.Tables[0].Columns.Add("t_shpd_amt");

        ds.Tables[0].Columns.Add("t_back_amt");
        ds.Tables[0].Columns.Add("i_ordr_amt");
        ds.Tables[0].Columns.Add("i_shpd_amt");
        ds.Tables[0].Columns.Add("i_back_amt");
        ds.Tables[0].Columns.Add("s_ordr_qty");
        ds.Tables[0].Columns.Add("s_shpd_qty");
        ds.Tables[0].Columns.Add("s_back_qty");

        ds.Tables[0].Columns.Add("first_date");
        ds.Tables[0].Columns.Add("last_date");

        #region Before Group like_type.....
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

          DataRow dr = ds.Tables[0].Rows[i];
          bool _st = false;
          if (i != 0)
          {
            if (ds.Tables[0].Rows[i]["like_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["like_type"].ToString().Trim())
              _st = true;
          }
          else
            _st = true;

          if (_st)
          {
            DVOstootypr objDVOstootypr = new DVOstootypr();
            objDVOstootypr.order_type = Convert.ToString(dr["order_type"]);
            if (objDVOstootypr.order_type != string.Empty)
            {
              object[] orderParam = new object[1];
              orderParam[0] = objDVOstootypr.order_type;

              object desc = objDalBaseClass.ExecuteScalar(ref orderParam, objDVOstootypr.GET_ORDER_OPEN_ITEM_SUMMARY_INFO);
              string descript = Convert.ToString(desc).Trim();
              dr["description"] = "Orders of type like: " + dr["like_type"] + " - " + descript;
              ds.Tables[0].Rows[i]["t_ordr_amt"] = 0;
              ds.Tables[0].Rows[i]["t_shpd_amt"] = 0;
              ds.Tables[0].Rows[i]["t_back_amt"] = 0;

            }
          }
          #endregion
          dr["first_date"] = DateTime.MinValue;
          dr["last_date"] = DVOApplicationUserInfo.CurrentDate;


          string like_type = string.Empty;
          if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "N")
          {
            //                        # print currency_code and description
            //let rpt.currency_code = curs.currency_code
            //let rpt.hdr_type = get_curr_desc(rpt.currency_code)

          }

          //#_net_amount - On every row processing for net_amount
          if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "Y")
          {
            //if (Convert.ToDecimal(dr["currency_rate"]) = -1)
            //{
            //    //# there was a problem finding exchange rate
            //    //# use order's exchange rate
            //    //let rpt.currency_rate = curs.currency_rate
            //}

            //translate amounts to home currency
            // dr["net_amount"]=calc_currency(rpt.currency_rate,curs.net_amount,"T")
          }

          if (Convert.ToString(dr["stage"]) != "CAN")
          {
            ds.Tables[0].Rows[i]["s_ordr_qty"] = (dr["s_ordr_qty"] != DBNull.Value ? Convert.ToDecimal(dr["s_ordr_qty"]) : 0) + (dr["ship_qty"] != DBNull.Value ? Convert.ToDecimal(dr["ship_qty"]) : 0);
            ds.Tables[0].Rows[i]["i_ordr_amt"] = (dr["i_ordr_amt"] != DBNull.Value ? Convert.ToDecimal(dr["i_ordr_amt"]) : 0) + (dr["net_amount"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);
            ds.Tables[0].Rows[i]["t_ordr_amt"] = (dr["i_ordr_amt"] != DBNull.Value ? Convert.ToDecimal(dr["t_ordr_amt"]) : 0) + (dr["i_ordr_amt"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);
          }
          //shpd_amount
          if (Convert.ToString(dr["stage"]) == "SHP" || (Convert.ToString(dr["stage"]) == "INV") || (Convert.ToString(dr["stage"]) == "PST"))
          {
            ds.Tables[0].Rows[i]["s_shpd_qty"] = (dr["s_shpd_qty"] != DBNull.Value ? Convert.ToDecimal(dr["s_shpd_qty"]) : 0) + (dr["ship_qty"] != DBNull.Value ? Convert.ToDecimal(dr["ship_qty"]) : 0);
            ds.Tables[0].Rows[i]["i_shpd_amt"] = (dr["i_shpd_amt"] != DBNull.Value ? Convert.ToDecimal(dr["i_shpd_amt"]) : 0) + (dr["net_amount"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);
            ds.Tables[0].Rows[i]["t_shpd_amt"] = (dr["t_shpd_amt"] != DBNull.Value ? Convert.ToDecimal(dr["t_shpd_amt"]) : 0) + (dr["net_amount"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);
          }
          //back_amount

          if (Convert.ToString(dr["stage"]) == "BKO")
          {
            ds.Tables[0].Rows[i]["s_back_qty"] = (dr["s_back_qty"] != DBNull.Value ? Convert.ToDecimal(dr["s_back_qty"]) : 0) + (dr["ship_qty"] != DBNull.Value ? Convert.ToDecimal(dr["ship_qty"]) : 0);
            ds.Tables[0].Rows[i]["i_back_amt"] = (dr["i_back_amt"] != DBNull.Value ? Convert.ToDecimal(dr["i_back_amt"]) : 0) + (dr["net_amount"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);
            ds.Tables[0].Rows[i]["t_back_amt"] = (dr["i_back_amt"] != DBNull.Value ? Convert.ToDecimal(dr["i_back_amt"]) : 0) + (dr["net_amount"] != DBNull.Value ? Convert.ToDecimal(dr["net_amount"]) : 0);

          }
        }
      }
      return ds;
    }
    //*********Added by Sunil Pahwa For get Order Open Item by Detail *********************
    public static DataSet GetOrder_Open_Item_DetailInfo(DVOOrderEntrystoshipd objDvoDVEntrystoshipd)
    {
      Object[] parameters = new object[9];
      parameters[0] = objDvoDVEntrystoshipd.item_code;
      parameters[1] = objDvoDVEntrystoshipd.warehouse_code;
      parameters[2] = objDvoDVEntrystoshipd.lo_stage;
      parameters[3] = objDvoDVEntrystoshipd.hi_stage;
      parameters[4] = objDvoDVEntrystoshipd.cust_code;
      parameters[5] = objDvoDVEntrystoshipd.order_type;
      parameters[6] = objDvoDVEntrystoshipd.like_type;
      parameters[7] = objDvoDVEntrystoshipd.order_no;
      parameters[8] = objDvoDVEntrystoshipd.ord_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      DataSet ds = objDalBaseClass.GetData(objDvoDVEntrystoshipd.FIND_ORDER_OPEN_ITEM_DETAIL(ref parameters));

      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "desc1";
        ds.Tables[0].Columns[1].ColumnName = "desc2";
        ds.Tables[0].Columns[2].ColumnName = "currency_code";
        ds.Tables[0].Columns[3].ColumnName = "currency_rate";
        ds.Tables[0].Columns[4].ColumnName = "like_type";
        ds.Tables[0].Columns[5].ColumnName = "order_date";
        ds.Tables[0].Columns[6].ColumnName = "order_no";
        ds.Tables[0].Columns[7].ColumnName = "order_type";
        ds.Tables[0].Columns[8].ColumnName = "bko_date";
        ds.Tables[0].Columns[9].ColumnName = "can_date";
        ds.Tables[0].Columns[10].ColumnName = "inv_date";
        ds.Tables[0].Columns[11].ColumnName = "item_code";
        ds.Tables[0].Columns[12].ColumnName = "net_amount";
        ds.Tables[0].Columns[13].ColumnName = "new_date";
        ds.Tables[0].Columns[14].ColumnName = "ord_date";
        ds.Tables[0].Columns[15].ColumnName = "pic_date";
        ds.Tables[0].Columns[16].ColumnName = "pst_date";
        ds.Tables[0].Columns[17].ColumnName = "sell_to_code";
        ds.Tables[0].Columns[18].ColumnName = "ship_qty";
        ds.Tables[0].Columns[19].ColumnName = "ship_to_code";
        ds.Tables[0].Columns[20].ColumnName = "shp_date";
        ds.Tables[0].Columns[21].ColumnName = "stage";
        ds.Tables[0].Columns[22].ColumnName = "warehouse_code";


        //Added Later As per the requirement
        ds.Tables[0].Columns.Add("description");
        ds.Tables[0].Columns.Add("hdr_type");
        ds.Tables[0].Columns.Add("ordr_amount");
        ds.Tables[0].Columns.Add("shpd_amount");
        ds.Tables[0].Columns.Add("back_amount");
        ds.Tables[0].Columns.Add("stage_date");

        #region Before Group like_type.....
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

          DataRow dr = ds.Tables[0].Rows[i];
          bool _s = false;
          if (i != 0)
          {
            if (ds.Tables[0].Rows[i]["like_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["like_type"].ToString().Trim())
              _s = true;
          }
          else
            _s = true;

          if (_s)
          {
            DVOstootypr objDVOstootypr = new DVOstootypr();
            objDVOstootypr.order_type = Convert.ToString(dr["order_type"]);
            if (objDVOstootypr.order_type != string.Empty)
            {
              object[] orderParam = new object[1];
              orderParam[0] = objDVOstootypr.order_type;

              object desc = objDalBaseClass.ExecuteScalar(ref orderParam, objDVOstootypr.GET_ORDER_OPEN_ITEM_SUMMARY_INFO);
              string descript = Convert.ToString(desc).Trim();
              dr["description"] = "Orders of type like: " + dr["like_type"] + " - " + descript;
            }
            #endregion
          }
          if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "N")
          {
            // # print currency_code and description
            //let rpt.currency_code = curs.currency_code
            // #let rpt.description = get_curr_desc(rpt.currency_code)

          }

          if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "Y")
          {
            //if (Convert.ToString dr["currency_rate"] == -1)
            //{
            //    // # there was a problem finding exchange rate
            //    //# use order's exchange rate
            //    //  let rpt.currency_rate = curs.currency_rate
            //}


            //    //# translate amounts to home currency
            //    //let curs.net_amount = calc_currency(rpt.currency_rate,
            //    //    curs.net_amount,"T")

          }

          switch (dr["stage"].ToString())
          {
            case "NEW":
              ds.Tables[0].Rows[i]["stage_date"] = dr["new_date"];
              break;
            case "BKO":
              ds.Tables[0].Rows[i]["stage_date"] = dr["bko_date"];
              break;
            case "ORD":
              ds.Tables[0].Rows[i]["stage_date"] = dr["ord_date"];
              break;
            case "PIC":
              ds.Tables[0].Rows[i]["stage_date"] = dr["pic_datd"];
              break;
            case "SHP":
              ds.Tables[0].Rows[i]["stage_date"] = dr["shp_date"];
              break;
            case "INV":
              ds.Tables[0].Rows[i]["stage_date"] = dr["inv_date"];
              break;
            case "PST":
              ds.Tables[0].Rows[i]["stage_date"] = dr["pst_date"];
              break;
            case "CAN":
              ds.Tables[0].Rows[i]["stage_date"] = dr["can_date"];
              break;
            default:
              ds.Tables[0].Rows[i]["stage_date"] = dr["ord_date"];
              break;

          }

          if (dr["stage"] != "CAN")
          {
            dr["ordr_amount"] = dr["net_amount"];
          }
          else
          {
            dr["ordr_amount"] = 0;
          }
          //#_shpd_amount
          if ((Convert.ToString(dr["stage"]) == "SHP") || (Convert.ToString(dr["stage"]) == "INV") || (Convert.ToString(dr["stage"]) == "PST"))
          {
            dr["shpd_amount"] = dr["net_amount"];
          }
          else
          {
            dr["shpd_amount"] = 0;
          }
          //#_back_amount
          if (Convert.ToString(dr["stage"]) == "BKO")
          {
            dr["back_amount"] = dr["net_amount"];
          }
          else
          {
            dr["back_amount"] = 0;
          }
        }

      }
      return ds;
    }
    //Added By Rahul jain for getting data and using in Print Order Type definition reports
    public static DataSet GetOrderTypeDefinitions(ref DVOstootypr objDVOstootypr)
    {
      object[] parameters = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOstootypr), objDVOstootypr.GET_ORDER_TYPE_DEFINITION);
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "description";
        ds.Tables[0].Columns[1].ColumnName = "fob_point_req";
        ds.Tables[0].Columns[2].ColumnName = "frt_doc_no_req";
        ds.Tables[0].Columns[3].ColumnName = "like_type";
        ds.Tables[0].Columns[4].ColumnName = "master_order";
        ds.Tables[0].Columns[5].ColumnName = "order_type";
        ds.Tables[0].Columns[6].ColumnName = "pay_method_req";
        ds.Tables[0].Columns[7].ColumnName = "po_no_req";
        ds.Tables[0].Columns[8].ColumnName = "print_ack";
        ds.Tables[0].Columns[9].ColumnName = "print_mfs";
        ds.Tables[0].Columns[10].ColumnName = "print_pic";
        ds.Tables[0].Columns[11].ColumnName = "reference_order";
        ds.Tables[0].Columns[12].ColumnName = "shp_via_req";
      }
      return ds;
    }
    //************************************************************************************

    //Added By Rahul jain for getting data and using in Print Line Type definition reports
    public static DataSet GetLineTypeDefinitions(ref DVOOrderEntryLineType objDVOOrderEntryLineType)
    {
      object[] parameters = new object[0];
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOOrderEntryLineType), objDVOOrderEntryLineType.GET_LINE_TYPE_DEFINITION);
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "desc_update";
        ds.Tables[0].Columns[1].ColumnName = "description";
        ds.Tables[0].Columns[2].ColumnName = "like_type";
        ds.Tables[0].Columns[3].ColumnName = "line_type";
        ds.Tables[0].Columns[4].ColumnName = "price_update";
        ds.Tables[0].Columns[5].ColumnName = "stock_item";
      }
      return ds;
    }
    //************************************************************************************

    //Added By Rahul jain for getting data and using in Print Alias definition reports
    public static DataSet GetItemAliasesDefinition(ref DVOCustomerItemAlias objDVOCustomerItemAlias)
    {
      object[] parameters = new object[3];
      parameters[0] = objDVOCustomerItemAlias.alias;
      parameters[1] = objDVOCustomerItemAlias.item_code;
      parameters[2] = objDVOCustomerItemAlias.cust_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCustomerItemAlias));
      return ds;
    }
    //************************************************************************************

    //Added by Rahul Jain for Getting data and us in Print Customer Order summary
    public static DataSet Get_Customer_Order_Summary(ref DVOOrderstoordre obtDVOOrderstoordre)
    {
      DataSet ds = null;
      DVOCustomerDetailstrcustr objDVOCustomerDetailstrcustr = new DVOCustomerDetailstrcustr();
      DVOOrderTypeDefinition obj_stootypr = new DVOOrderTypeDefinition();
      DVOOrderstoordre obDVOOrderstoordre = new DVOOrderstoordre();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[5];
      parameters[0] = obtDVOOrderstoordre.cust_code;
      parameters[1] = obtDVOOrderstoordre.order_type;
      parameters[2] = obtDVOOrderstoordre.like_type;
      parameters[3] = obtDVOOrderstoordre.order_no;
      parameters[4] = Convert.ToDateTime(obtDVOOrderstoordre.order_date).ToString("MM/dd/yyyy");
      try
      {
        ds = objDalBaseClass.GetData(obtDVOOrderstoordre.FINDQUERY_GET_CUSTOMERORDER_SUMMARY(ref parameters));
        if (ds.Tables.Count > 0)
        {
          if (ds.Tables[0].Rows.Count > 0)
          {
            //Added Later As per the requirement
            ds.Tables[0].Columns.Add("hdr_type");
            ds.Tables[0].Columns.Add("ordr_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("back_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("shpd_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("customer");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
              DataRow dr = ds.Tables[0].Rows[i];

              #region Before Group like_type.....

              bool _status = false;
              if (i != 0)
              {
                if (ds.Tables[0].Rows[i]["like_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["like_type"].ToString().Trim())
                  _status = true;
              }
              else
                _status = true;

              if (_status)
              {
                //open c_type using rpt.like_type fetch c_type into rpt.type_desc
                obj_stootypr.order_type = dr["like_type"].ToString().Trim();
                DataSet dsDesc = null;// = BLLPrintOpnOrdSummary.Getc_type(ref obj_stootypr);
                if (dsDesc.Tables[0].Rows.Count > 0)
                  dr["hdr_type"] = "Orders of type like: " + dr["like_type"] + " - " + dsDesc.Tables[0].Rows[0][0].ToString().Trim();
              }
              #endregion Before Group like_type

              #region Before Group sell_to_code.....

              bool _statusdoc = false;
              if (i != 0)
              {
                if (ds.Tables[0].Rows[i]["sell_to_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["sell_to_code"].ToString().Trim())
                  _statusdoc = true;
              }
              else
                _statusdoc = true;

              if (_statusdoc)
              {
                objDVOCustomerDetailstrcustr.cust_code = dr["sell_to_code"].ToString().Trim();
                string bus_name = string.Empty;// = BLLPrintOpenOrdDetails.ar_custn(ref objDVOCustomerDetailstrcustr);
                dr["customer"] = bus_name.ToString().Trim();
                if (bus_name == string.Empty)
                {
                  dr["customer"] = " - UNKNOWN";
                }
                else
                {
                  dr["customer"] = " - " + dr["customer"];
                }
              }
              #endregion Before Group like_type

              #region on every row........
              ////************************Multi currency not implemented yet*************************************
              if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "N")
              {
                //# print currency_code and description
                dr["currency_code"] = dr["currency_code"];
                //dr["hdr_type"] = get_curr_desc(rpt.currency_code)
              }
              if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "Y")
              {
                // # report is in home currency
                if (Convert.ToDecimal(dr["currency_rate"]) == -1)
                {
                  //# there was a problem finding exchange rate
                  //# use order's exchange rate
                  dr["currency_rate"] = dr["currency_rate"];
                }
                //# translate amounts to home currency
                //let curs.net_amount = calc_currency(rpt.currency_rate, curs.net_amount,"T")
              }
              // #_ord_amount # summarize the amounts
              if (dr["stage"].ToString() != "CAN")
              {
                dr["ordr_amount"] = dr["net_amount"];
              }
              else
              {
                dr["ordr_amount"] = 0;
              }
              // #_shpd_amount
              if (dr["stage"].ToString() == "SHP" || dr["stage"].ToString() == "INV" || dr["stage"].ToString() == "PST")
              {
                dr["shpd_amount"] = dr["net_amount"];
              }
              else
              {
                dr["shpd_amount"] = 0;
              }

              //#_back_amount
              if (dr["stage"].ToString() == "BKO")
              {
                dr["back_amount"] = dr["net_amount"];
              }
              else
              {
                dr["back_amount"] = 0;
              }
              #endregion on every row
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }
    //*************************************************************************************
    //Added by Sunil Pahwa for UnPaid Deductions
    public static DataTable GET_EMPLOYEE_UNPAID_DEDUCTIONS(DVOMasterEmployee objDVOMasterEmployee, DateTime DateFrom, DateTime DateTo)
    {
      object[] Parameter = new object[5];
      Parameter[0] = objDVOMasterEmployee.EmplCode;
      Parameter[1] = objDVOMasterEmployee.ded_code;
      Parameter[2] = objDVOMasterEmployee.type_code;
      Parameter[3] = DateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      Parameter[4] = DateTo.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMasterEmployee), objDVOMasterEmployee.GET_EMP_UNPAID_DEDUCTIONS);
      DataTable dt = null;
      if (ds.Tables[0].Rows.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "v_empl_code";
        ds.Tables[0].Columns[1].ColumnName = "v_first_name";
        ds.Tables[0].Columns[2].ColumnName = "v_last_name";
        ds.Tables[0].Columns[3].ColumnName = "v_department";
        ds.Tables[0].Columns[4].ColumnName = "v_ded_code";
        ds.Tables[0].Columns[5].ColumnName = "v_last_pay";
        ds.Tables[0].Columns[6].ColumnName = "v_emp_age";

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
          if (ds.Tables[0].Rows[i]["v_emp_age"] != DBNull.Value || Convert.ToString(ds.Tables[0].Rows[i]["v_emp_age"]) != string.Empty)
          {
            if (Convert.ToInt32(ds.Tables[0].Rows[i]["v_emp_age"]) > 62)
            {
              ds.Tables[0].Rows[i].Delete();
              ds.AcceptChanges();
              i--;
            }

          }
        }
        //Sorted the dataset and putting in datatable
        dt = ds.Tables[0].DefaultView.ToTable("v_empl_code,v_last_name");
      }

      return dt;
    }

    //Added by Rahul Jain for Getting data and us in Print Customer Order Detail
    public static DataSet Get_Customer_Order_Detail(ref DVOOrderstoordre obtDVOOrderstoordre)
    {
      DataSet ds = null;
      DVOCustomerDetailstrcustr objDVOCustomerDetailstrcustr = new DVOCustomerDetailstrcustr();
      //DVOOrderEntrystoshipd obj_stoshipd = new DVOOrderEntrystoshipd();
      DVOOrderTypeDefinition obj_stootypr = new DVOOrderTypeDefinition();
      DVOOrderstoordre obDVOOrderstoordre = new DVOOrderstoordre();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      Object[] parameters = new object[5];
      parameters[0] = obtDVOOrderstoordre.cust_code;
      parameters[1] = obtDVOOrderstoordre.order_type;
      parameters[2] = obtDVOOrderstoordre.like_type;
      parameters[3] = obtDVOOrderstoordre.order_no;
      parameters[4] = Convert.ToDateTime(obtDVOOrderstoordre.order_date).ToString("MM/dd/yyyy");
      try
      {
        ds = objDalBaseClass.GetData(obtDVOOrderstoordre.FINDQUERY_GET_CUSTOMERORDER_DETAIL(ref parameters));
        if (ds.Tables.Count > 0)
        {
          if (ds.Tables[0].Rows.Count > 0)
          {
            //Added Later As per the requirement
            ds.Tables[0].Columns.Add("hdr_type");
            ds.Tables[0].Columns.Add("ordr_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("back_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("shpd_amount", typeof(decimal));
            ds.Tables[0].Columns.Add("customer");
            ds.Tables[0].Columns.Add("quantity", typeof(decimal));
            ds.Tables[0].Columns.Add("stage_date", typeof(DateTime));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
              DataRow dr = ds.Tables[0].Rows[i];

              #region Before Group like_type.....

              bool _status = false;
              if (i != 0)
              {
                if (ds.Tables[0].Rows[i]["like_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["like_type"].ToString().Trim())
                  _status = true;
              }
              else
                _status = true;

              if (_status)
              {
                //open c_type using rpt.like_type fetch c_type into rpt.type_desc
                obj_stootypr.order_type = dr["like_type"].ToString().Trim();
                DataSet dsDesc = null;//= BLLPrintOpnOrdSummary.Getc_type(ref obj_stootypr);
                if (dsDesc.Tables[0].Rows.Count > 0)
                  dr["hdr_type"] = "Orders of type like: " + dr["like_type"] + " - " + dsDesc.Tables[0].Rows[0][0].ToString().Trim();
              }
              #endregion Before Group like_type

              #region Before Group sell_to_code.....

              bool _statusdoc = false;
              if (i != 0)
              {
                if (ds.Tables[0].Rows[i]["sell_to_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["sell_to_code"].ToString().Trim())
                  _statusdoc = true;
              }
              else
                _statusdoc = true;

              if (_statusdoc)
              {
                objDVOCustomerDetailstrcustr.cust_code = dr["sell_to_code"].ToString().Trim();
                string bus_name = string.Empty;// = BLLPrintOpenOrdDetails.ar_custn(ref objDVOCustomerDetailstrcustr);
                dr["customer"] = bus_name.ToString().Trim();
                if (bus_name == string.Empty)
                {
                  dr["customer"] = " - UNKNOWN";
                }
                else
                {
                  dr["customer"] = " - " + dr["customer"];
                }
              }
              #endregion Before Group like_type

              #region on every row........
              ////************************Multi currency not implemented yet*************************************
              if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "N")
              {
                //# print currency_code and description
                dr["currency_code"] = dr["currency_code"];
                //dr["hdr_type"] = get_curr_desc(rpt.currency_code)
              }
              if (DVOApplicationUserInfo._mcurr == "Y" && DVOApplicationUserInfo._hcurr == "Y")
              {
                // # report is in home currency
                if (Convert.ToDecimal(dr["currency_rate"]) == -1)
                {
                  //# there was a problem finding exchange rate
                  //# use order's exchange rate
                  dr["currency_rate"] = dr["currency_rate"];
                }
                //# translate amounts to home currency
                //let curs.net_amount = calc_currency(rpt.currency_rate, curs.net_amount,"T")
              }

              switch (dr["stage"].ToString())
              {
                case "NEW":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["new_date"];
                  break;
                case "BKO":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["bko_date"];
                  break;
                case "ORD":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["ord_date"];
                  break;
                case "PIC":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["pic_datd"];
                  break;
                case "SHP":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["shp_date"];
                  break;
                case "INV":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["inv_date"];
                  break;
                case "PST":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["pst_date"];
                  break;
                case "CAN":
                  ds.Tables[0].Rows[i]["stage_date"] = dr["can_date"];
                  break;
                default:
                  ds.Tables[0].Rows[i]["stage_date"] = dr["ord_date"];
                  break;

              }
              dr["quantity"] = dr["ship_qty"];
              // #_ord_amount # summarize the amounts
              if (dr["stage"].ToString() != "CAN")
              {
                dr["ordr_amount"] = dr["net_amount"];
              }
              else
              {
                dr["ordr_amount"] = 0;
              }
              // #_shpd_amount
              if (dr["stage"].ToString() == "SHP" || dr["stage"].ToString() == "INV" || dr["stage"].ToString() == "PST")
              {
                dr["shpd_amount"] = dr["net_amount"];
              }
              else
              {
                dr["shpd_amount"] = 0;
              }

              //#_back_amount
              if (dr["stage"].ToString() == "BKO")
              {
                dr["back_amount"] = dr["net_amount"];
              }
              else
              {
                dr["back_amount"] = 0;
              }
              #endregion on every row
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }
    //*************************************************************************************

    public static DataSet GetDuplicateCheckRegisterData(string DateFrom, string DateTo)
    {
      DataSet ds = null;

      return ds;

    }
    //Added By sunil Pahwa On 22/10/2009 for getting Cleared check from Bank
    public static DataSet GetCheckClearedFromBankInfo(ref DVOCheckRegisterstpchkreconcile objstpchkreconcile)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[2];

        parameters[0] = objstpchkreconcile.FromDate;
        parameters[1] = objstpchkreconcile.ToDate;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objstpchkreconcile.FIND_CLEARED_CHECKS(ref parameters));
        return ds;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    //Added By sanjay On 29/10/2009 for getting amount deposit to Bank
    public static DataSet GetAmtDepositToBank(ref DVOCheckRegisterstpchkreconcile objstpchkreconcile)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[2];

        parameters[0] = objstpchkreconcile.FromDate;
        parameters[1] = objstpchkreconcile.ToDate;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objstpchkreconcile.FIND_DEPOSIT_AMT(ref parameters));
        return ds;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    public static DataSet GetAccountStatement(ref DVOCheckRegisterstpchkreconcile objstpchkreconcile)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[3];

        parameters[0] = objstpchkreconcile.acct_no;
        parameters[1] = objstpchkreconcile.FromDate;
        parameters[2] = objstpchkreconcile.ToDate;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objstpchkreconcile.FIND_ACCT_STATEMENT(ref parameters));
        return ds;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }
    //Added by Sunil Pahwa
    public static DataSet GET_UNCLEAR_CHECKS(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
    {
      object[] parameters = new object[2];
      parameters[0] = objDVOAPCheckProcessingStpcashe.end_date;
      parameters[1] = objDVOAPCheckProcessingStpcashe.start_date;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), objDVOAPCheckProcessingStpcashe.GET_UNCLEAR_CHECKS);
      if (ds.Tables.Count > 0)
      {
        ds.Tables[0].Columns[0].ColumnName = "v_chk_date";
        ds.Tables[0].Columns[1].ColumnName = "v_doc_no";
        ds.Tables[0].Columns[2].ColumnName = "v_check_no";
        ds.Tables[0].Columns[3].ColumnName = "v_cash_amt";
        ds.Tables[0].Columns[4].ColumnName = "v_cash_acct";
        ds.Tables[0].Columns[5].ColumnName = "v_cash_department";
        ds.Tables[0].Columns[6].ColumnName = "v_cash_deb_cred";
        ds.Tables[0].Columns[7].ColumnName = "v_vend_code";
        ds.Tables[0].Columns[8].ColumnName = "v_bus_name";
        ds.Tables[0].Columns[9].ColumnName = "v_reconciled";
        ds.Tables[0].Columns[10].ColumnName = "v_keyvalue";
        ds.Tables[0].Columns[11].ColumnName = "v_orig_journal";
        ds.Tables[0].Columns[12].ColumnName = "v_ap_type";
      }
      return ds;

    }

    //Added By Rajeev 
    //date :31/08/2009
    //Aim: Business logic For Treasury Bill With Interest Reports

    public static DataSet GetTreasuryBillWithInterest(ref DVOTreasuryBillIssueNumbersIntissur objDVOTreasuryBillIssueNumbersIntissur)
    {


      DataTable objDataTable = new DataTable();
      DataSet DsFinal = new DataSet();

      Object[] parameters = new object[3];
      parameters[0] = objDVOTreasuryBillIssueNumbersIntissur.tend_code;
      parameters[1] = objDVOTreasuryBillIssueNumbersIntissur.issue_num;
      parameters[2] = objDVOTreasuryBillIssueNumbersIntissur.schId;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTrDtl = objDalBaseClass.GetData(objDVOTreasuryBillIssueNumbersIntissur.FINDQUERY_TREASURYBILL_WITH_INTEREST(ref parameters));
      if (dsTrDtl.Tables.Count > 0)
      {

        dsTrDtl.Tables[0].TableName = "DTTrDtl";
        dsTrDtl.Tables[0].Columns[0].ColumnName = "doc_no";
        dsTrDtl.Tables[0].Columns[1].ColumnName = "status";
        dsTrDtl.Tables[0].Columns[2].ColumnName = "issue_date";
        dsTrDtl.Tables[0].Columns[3].ColumnName = "redeem_date";
        dsTrDtl.Tables[0].Columns[4].ColumnName = "stat_limit";
        dsTrDtl.Tables[0].Columns[5].ColumnName = "issue_num";
        dsTrDtl.Tables[0].Columns[6].ColumnName = "dflt_amt_per_100";
        //dsTrDtl.Tables[0].Columns[7].ColumnName = "line_no";
        dsTrDtl.Tables[0].Columns[7].ColumnName = "tend_code";
        dsTrDtl.Tables[0].Columns[8].ColumnName = "amt_applied_for";
        dsTrDtl.Tables[0].Columns[9].ColumnName = "bill_status";
        dsTrDtl.Tables[0].Columns[10].ColumnName = "amt_issued";
        dsTrDtl.Tables[0].Columns[11].ColumnName = "amt_per_100";
        dsTrDtl.Tables[0].Columns[12].ColumnName = "tend_name";
        dsTrDtl.Tables[0].Columns[13].ColumnName = "address1";
        dsTrDtl.Tables[0].Columns[14].ColumnName = "address2";
        dsTrDtl.Tables[0].Columns[15].ColumnName = "contact";
        dsTrDtl.Tables[0].Columns[16].ColumnName = "phone";
        dsTrDtl.Tables[0].Columns[17].ColumnName = "fax";
        dsTrDtl.Tables[0].Columns[18].ColumnName = "tend_class";
        dsTrDtl.Tables[0].Columns[19].ColumnName = "tbschid";
        dsTrDtl.Tables[0].Columns[20].ColumnName = "tbschname";

        dsTrDtl.Tables[0].Columns.Add("PR_Amount", typeof(decimal));
        dsTrDtl.Tables[0].Columns.Add("INT_Amount", typeof(decimal));


        objDataTable = dsTrDtl.Tables[0].Clone();
        if (dsTrDtl.Tables[0].Rows.Count != 0)
        {
          for (int i = 0; i < dsTrDtl.Tables[0].Rows.Count; i++)
          {
            DataRow dr = dsTrDtl.Tables[0].Rows[i];

            //Calculate the Principal amount                        
            if (dr["amt_applied_for"] != DBNull.Value && dr["amt_per_100"] != DBNull.Value)
              dr["PR_Amount"] = (Convert.ToDecimal(dr["amt_applied_for"]) * Convert.ToDecimal(dr["amt_per_100"])) / 100;
            else
              dr["PR_Amount"] = 0.0M;

            if (dr["amt_applied_for"] != DBNull.Value && dr["PR_Amount"] != DBNull.Value)
              dr["INT_Amount"] = Convert.ToDecimal(dr["amt_applied_for"]) - Convert.ToDecimal(dr["PR_Amount"]);
            else
              dr["INT_Amount"] = 0.0M;

            objDataTable.Rows.Add(dr.ItemArray);

          }
        }

      }

      DsFinal.Tables.Add(objDataTable);
      return DsFinal;
    }

    public static IDataReader GetDirectDepositListingByDataReader(ref DVOddmStypddreAndStypddrd objDVO)
    {
      IDataReader dr = null;
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objDVO.BanckCode.Trim();
        parameters[1] = objDVO.Date;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        dr = objDalBaseClass.GetDataByReader(ref parameters, objDVO.GET_DDL);
        //while(dr.Read())
        //{

        //}


        return dr;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dr;
    }


    public static IDataReader GetNSSDetailByDR(ref DVONSSDetail objNSSDetail)
    {
      Object[] parameters = new object[4];

      parameters[0] = objNSSDetail.account_no;
      parameters[1] = objNSSDetail.nss_status;
      parameters[2] = objNSSDetail.first_name;
      parameters[3] = objNSSDetail.last_name;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      IDataReader drNSSDetail = objDalBaseClass.GetDataByReader(ref parameters, typeof(DVONSSDetail));
      return drNSSDetail;
    }

    //Added by Sunil Pahwa on 28/10/09
    public static DataSet GetReceivedInfo(ref DVOIssueTendersTbissued objDVOIssueTendersTbissued)
    {
      DataSet ds = null;
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objDVOIssueTendersTbissued.tbschid;
        parameters[1] = objDVOIssueTendersTbissued.issue_num;
        parameters[2] = objDVOIssueTendersTbissued.tend_code;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objDVOIssueTendersTbissued.FIND_QUERY_2(ref parameters));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;


    }

    /// <summary>
    /// Created By Rajeev, Created Date :13/11/2009
    /// This function is used to set the paramerter to generate the checks
    /// when a person come to stop NSS Account
    /// </summary>
    /// <param name="BatchID"></param>
    /// <param name="CalculatedInterest"></param>
    /// <param name="objDVONssControls"></param>
    /// <param name="objStopNssContract"></param>
    /// <param name="objDVOAPCheckProcessingStpcashe"></param>
    /// <param name="listDVOAPCheckProcessingDetailStpcashd"></param>
    public static void CreateAPCheck_forCloseNSSAccount(int BatchID, decimal CalculatedInterest, ref List<DVONssControls> objDVONssControls, ref DVONssCloseAccount objStopNssContract, out DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, out List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
    {
      objDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
      //objDVOAPCheckProcessingStpcashe.doc_no = DocumentNo;
      objDVOAPCheckProcessingStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);// Program.CurrentDate.ToString("MM/dd/yyyy");
      objDVOAPCheckProcessingStpcashe.vend_code = "MISC";
      objDVOAPCheckProcessingStpcashe.bus_name = objStopNssContract.last_name + ", " + objStopNssContract.first_name;
      objDVOAPCheckProcessingStpcashe.pay_to_code = "PAYTO";
      objDVOAPCheckProcessingStpcashe.doc_desc = "NSS CLOSE ACT#" + objStopNssContract.account_no.ToString().Trim();
      objDVOAPCheckProcessingStpcashe.tre_voucher_no = "99999";
      objDVOAPCheckProcessingStpcashe.cash_acct = Convert.ToInt32(objDVONssControls[0].bank_acctno);
      objDVOAPCheckProcessingStpcashe.cash_amt = CalculatedInterest + objStopNssContract.current_bal;
      objDVOAPCheckProcessingStpcashe.cash_deb_cred = "CR";
      objDVOAPCheckProcessingStpcashe.cash_department = "000";
      objDVOAPCheckProcessingStpcashe.oa_amt = 0;
      objDVOAPCheckProcessingStpcashe.print_chk = "Y";
      objDVOAPCheckProcessingStpcashe.ok_to_post = "N";
      objDVOAPCheckProcessingStpcashe.chk_printed = "N";
      objDVOAPCheckProcessingStpcashe.ap_type = "N";
      objDVOAPCheckProcessingStpcashe.required_approval = 100;
      objDVOAPCheckProcessingStpcashe.batch_id = BatchID;
      objDVOAPCheckProcessingStpcashe.current_approval = -1;
      objDVOAPCheckProcessingStpcashe.acd_id = 3;

      listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
      using (DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd())
      {
        //assign appropriate values to detail-object
        //Assign the Principal amount
        objDVOAPCheckProcessingDetailStpcashd.inv_doc_no = -99;
        objDVOAPCheckProcessingDetailStpcashd.dist_acct = Convert.ToInt32(objDVONssControls[0].principal_acctno);
        objDVOAPCheckProcessingDetailStpcashd.dist_department = "000";
        objDVOAPCheckProcessingDetailStpcashd.dist_amt = objStopNssContract.current_bal;
        objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = "DB";

        //add into list
        listDVOAPCheckProcessingDetailStpcashd.Add(objDVOAPCheckProcessingDetailStpcashd);
      }
      using (DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd())
      {
        //assign appropriate values to detail-object
        //The Interest amount is:::: bonus + interest
        objDVOAPCheckProcessingDetailStpcashd.inv_doc_no = -99;
        objDVOAPCheckProcessingDetailStpcashd.dist_acct = Convert.ToInt32(objDVONssControls[0].interest_acctno);
        objDVOAPCheckProcessingDetailStpcashd.dist_department = "000";
        objDVOAPCheckProcessingDetailStpcashd.dist_amt = CalculatedInterest;
        objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = "DB";

        //add into list
        listDVOAPCheckProcessingDetailStpcashd.Add(objDVOAPCheckProcessingDetailStpcashd);
      }
    }
    public static DataSet GetLedgerAccount()
    {

      DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = null;
      try
      {
        object[] parameters = new object[0];
        ds = objDalBaseClass.GetData(objDVOGeneralLedger.FIND_LEDGER_ACCOUNT(ref parameters));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }

    //public static DataSet GetLedgerAccountDetail(ref DVOGLPayrollGLAccounts objSearch)
    //{
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet ds = null;
    //    try
    //    {
    //        object[] parameters = new object[9];
    //        parameters[0] = objSearch.acct_no;
    //        parameters[1] = objSearch.acct_type;
    //        parameters[2] = objSearch.acct_desc;
    //        parameters[3] = objSearch.subtotal_group;
    //        parameters[4] = objSearch.keyvalue;
    //        parameters[5] = objSearch.incr_with_crdt;
    //        parameters[6] = objSearch.gobzero;
    //        parameters[7] = objSearch.active;
    //        parameters[8] = objSearch.acct_cat;
    //        ds = objDalBaseClass.GetData(objSearch.FindLedgerAcctDtl(ref parameters));
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionManagement.ExceptionManager.Publish(ex);
    //        throw ex;
    //    }
    //    return ds;

    //}

    public static DataSet GetSelectedDocuments()
    {
      DVOstgstder objDVOstgstder = new DVOstgstder();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = null;
      try
      {
        object[] parameters = new object[0];
        ds = objDalBaseClass.GetData(objDVOstgstder.FIND_RECURRING_DOCS(ref parameters));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    public static DataSet GetWarrantDetails(ref DVOGLWarrantEntryInbwarah objSearch, string acct_type, string keyvalue)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = null;
      try
      {
        object[] parameters = new object[7];
        parameters[0] = objSearch.Type;
        parameters[1] = objSearch.EffectiveDate;
        parameters[2] = objSearch.DateEntered;
        parameters[3] = objSearch.EnteredBy;
        parameters[4] = objSearch.RequestedBy;
        parameters[5] = keyvalue;
        parameters[6] = acct_type;
        ds = objDalBaseClass.GetData(objSearch.FIND_WARRANT_DETAIL(ref parameters));

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    public static DataSet GetTotalApprovedCheckDetails(ref DVOPaymentDue objDVOPaymentDue)
    {
      object[] Parameters = new object[2];
      Parameters[0] = objDVOPaymentDue.TopaydateFrom;
      Parameters[1] = objDVOPaymentDue.TopaydateTo;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsApprovedCheck = objDalBaseClass.GetData(ref Parameters, typeof(DVOPaymentDue), objDVOPaymentDue.GET_CHECK_AMOUNT_DUE);
      if (dsApprovedCheck.Tables.Count > 0)
      {
        dsApprovedCheck.Tables[0].Columns[0].ColumnName = "chk_date";
        dsApprovedCheck.Tables[0].Columns[1].ColumnName = "doc_no";
        dsApprovedCheck.Tables[0].Columns[2].ColumnName = "vend_code";
        dsApprovedCheck.Tables[0].Columns[3].ColumnName = "pay_to_code";
        dsApprovedCheck.Tables[0].Columns[4].ColumnName = "check_no";
        dsApprovedCheck.Tables[0].Columns[5].ColumnName = "doc_desc";
        dsApprovedCheck.Tables[0].Columns[6].ColumnName = "cash_amt";
        dsApprovedCheck.Tables[0].Columns[7].ColumnName = "cash_acct";
        dsApprovedCheck.Tables[0].Columns[8].ColumnName = "cash_deb_cred";
        dsApprovedCheck.Tables[0].Columns[9].ColumnName = "oa_amt";
        dsApprovedCheck.Tables[0].Columns[10].ColumnName = "min_voucher_no";
        dsApprovedCheck.Tables[0].Columns[11].ColumnName = "tre_voucher_no";
        dsApprovedCheck.Tables[0].Columns[12].ColumnName = "bus_name";
        dsApprovedCheck.Tables[0].Columns[13].ColumnName = "dist_acct";
        dsApprovedCheck.Tables[0].Columns[14].ColumnName = "dist_amt";
        dsApprovedCheck.Tables[0].Columns[15].ColumnName = "dist_deb_cred";

      }
      return dsApprovedCheck;
    }
    //Added By Rahul Jain On 13/01/2010 for getting NSS Account Details  Report
    public static DataSet GetNSSAcctDetail(ref DVONSSDetail objNSSdetail)
    {
      Object[] parameters = new object[4];

      parameters[0] = objNSSdetail.account_no;
      parameters[1] = objNSSdetail.nss_status;
      parameters[2] = objNSSdetail.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
      parameters[3] = objNSSdetail.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNSSDetail = objDalBaseClass.GetData(objNSSdetail.FIND_ACCT_DETAIL(ref parameters));
      //DataSet dsNSSDetail = objDalBaseClass.GetData(ref parameters, typeof(DVONSSDetail), objNSSdetail.FIND_NSS_CONTRIBUTION_BY_YEAR);
      if (dsNSSDetail.Tables.Count > 0)
      {
        dsNSSDetail.Tables[0].TableName = "NSSClients";
        dsNSSDetail.Tables[0].Columns[0].ColumnName = "p_account_no";
        dsNSSDetail.Tables[0].Columns[1].ColumnName = "p_contract_no";
        dsNSSDetail.Tables[0].Columns[2].ColumnName = "p_last_name";
        dsNSSDetail.Tables[0].Columns[3].ColumnName = "p_first_name";
        dsNSSDetail.Tables[0].Columns[4].ColumnName = "p_title";
        dsNSSDetail.Tables[0].Columns[5].ColumnName = "p_nss_status";
        dsNSSDetail.Tables[0].Columns[6].ColumnName = "p_date_paid";
        dsNSSDetail.Tables[0].Columns[7].ColumnName = "p_payment_date";
        dsNSSDetail.Tables[0].Columns[8].ColumnName = "p_amount";
      }
      return dsNSSDetail;
    }
    //Added by Sunil Pahwa
    public static DataSet Get_AccountInfo(DVOSBclients objDvoSavingBank)
    {
      Object[] parameters = new object[5];
      parameters[0] = objDvoSavingBank.acct_cat;
      parameters[1] = objDvoSavingBank.acct_no;
      parameters[2] = objDvoSavingBank.acct_status;
      parameters[3] = objDvoSavingBank.start_date;
      parameters[4] = objDvoSavingBank.end_date;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(objDvoSavingBank.FIND_ACCT_DETAILS(ref parameters));
      return ds;
    }

    public static DataSet GetCheckListingForApprovalInfo(int BatchID, string CHECK_POST)
    {
      object[] parameter = new object[2];

      parameter[0] = BatchID;
      parameter[1] = CHECK_POST.Trim();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSegvd = objDalBaseClass.GetData(ref parameter, typeof(DvoCheckListing), (new DvoCheckListing()).FIND_CHECKLISTINFOAPPROVAL);
      return dsSegvd;

    }

    public static DataSet GetDeductionAnalysis(ref DVOPrintSummaryAnalysis ObjDVODeductionAnalysis)
    {
      try
      {
        object[] parameter = new object[11];
        parameter[0] = ObjDVODeductionAnalysis.startdate;
        parameter[1] = ObjDVODeductionAnalysis.Enddate;
        parameter[2] = ObjDVODeductionAnalysis.act_code;
        //parameter[3] = ObjDvoSummaryAnalysis.act_type;
        parameter[3] = ObjDVODeductionAnalysis.empl_code;
        parameter[4] = ObjDVODeductionAnalysis.last_name;
        parameter[5] = ObjDVODeductionAnalysis.first_name;
        parameter[6] = ObjDVODeductionAnalysis.type_code;
        parameter[7] = ObjDVODeductionAnalysis.job_code;
        parameter[8] = ObjDVODeductionAnalysis.job_title;
        parameter[9] = ObjDVODeductionAnalysis.pay_period;
        parameter[10] = ObjDVODeductionAnalysis.emp_status;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet DSDeductionAnalysis = objDalBaseClass.GetData(ObjDVODeductionAnalysis.GET_DEDUCTION_ANALYSIS(ref parameter));

        if (DSDeductionAnalysis != null && DSDeductionAnalysis.Tables.Count > 0)
        {
          DSDeductionAnalysis.Tables[0].Columns[0].ColumnName = "doc_no";
          DSDeductionAnalysis.Tables[0].Columns[1].ColumnName = "empl_code";
          DSDeductionAnalysis.Tables[0].Columns[2].ColumnName = "last_name";
          DSDeductionAnalysis.Tables[0].Columns[3].ColumnName = "first_name";
          DSDeductionAnalysis.Tables[0].Columns[4].ColumnName = "type_code";
          DSDeductionAnalysis.Tables[0].Columns[5].ColumnName = "ded_code";
          DSDeductionAnalysis.Tables[0].Columns[6].ColumnName = "amount";
          DSDeductionAnalysis.Tables[0].Columns[7].ColumnName = "pay_date";
          DSDeductionAnalysis.Tables[0].Columns[8].ColumnName = "cash_amount";
          DSDeductionAnalysis.Tables[0].Columns.Add("empl", typeof(String), "trim(empl_code) + ' - ' + trim(last_name) + ' ' + trim(first_name)");

          //foreach (DataRow dr in DSDeductionAnalysis.Tables[0].Rows)
          //{
          //    dr["empl"] = (dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() + " - " : string.Empty)
          //    + (dr["last_name"] != DBNull.Value ? dr["last_name"].ToString().Trim() + " " : string.Empty)
          //    + (dr["first_name"] != DBNull.Value ? dr["first_name"].ToString().Trim() : string.Empty);
          //}
          return DSDeductionAnalysis;
        }
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();
    }

    public static DataTable GetPrintDeductionAnalysis(ref DVOstydedanlyd objDVOstydedanlyd)
    {
      try
      {
        object[] parameter = new object[1];
        parameter[0] = objDVOstydedanlyd.checkNo;


        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOstydedanlyd), objDVOstydedanlyd.GET_PRINT_DEDUCTION_ANALYSIS_DETAIL);//((ref parameter, typeof(DVOstydedanlyd));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "doc_no";
          ds.Tables[0].Columns[1].ColumnName = "empl_code";
          ds.Tables[0].Columns[2].ColumnName = "last_name";
          ds.Tables[0].Columns[3].ColumnName = "first_name";
          ds.Tables[0].Columns[4].ColumnName = "ded_code";
          ds.Tables[0].Columns[5].ColumnName = "ded_amount";
          ds.Tables[0].Columns[6].ColumnName = "pay_date";
          return ds.Tables[0];
        }
        else
          throw new Exception();
        //DataTable dt = ds.Tables[0].Clone();
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //    dt.ImportRow(dr);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    dt.Columns[0].ColumnName = "doc_no";
        //    dt.Columns[0].ColumnName = "empl_code";
        //    dt.Columns[0].ColumnName = "last_name";
        //    dt.Columns[0].ColumnName = "first_name";
        //    dt.Columns[0].ColumnName = "ded_code";
        //    dt.Columns[0].ColumnName = "ded_amount";
        //    dt.Columns[0].ColumnName = "pay_date";
        //    return dt;
        //}
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataTable();

    }

    public static DataTable GetDeductionAnalysisDocInfo(ref DVOstydedanlyd objDVOstydedanlydDoc)
    {
      try
      {
        object[] parameter = new object[1];
        parameter[0] = objDVOstydedanlydDoc.doc_no; ;


        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOstydedanlyd), objDVOstydedanlydDoc.GET_PRINT_DEDUCTION_ANALYSIS_DETAIL_ON_DOC);//((ref parameter, typeof(DVOstydedanlyd));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "doc_no";
          ds.Tables[0].Columns[1].ColumnName = "empl_code";
          ds.Tables[0].Columns[2].ColumnName = "last_name";
          ds.Tables[0].Columns[3].ColumnName = "first_name";
          ds.Tables[0].Columns[4].ColumnName = "ded_code";
          ds.Tables[0].Columns[5].ColumnName = "ded_amount";
          ds.Tables[0].Columns[6].ColumnName = "pay_date";
          return ds.Tables[0];
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataTable();

    }
    //Added By Rahul jain on 05-02-2010
    public static DataSet GetDDListingBYDepositDate(ref DVOddmStypddreAndStypddrd objSearch)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objSearch.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = objSearch.dateTo.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(objSearch.GET_DIRECT_DEPOSIT_BY_DEPOSIT_DATE(ref parameters));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "bank_desc";
          ds.Tables[0].Columns[1].ColumnName = "amount";
          ds.Tables[0].Columns[2].ColumnName = "bank_acct_no";
          ds.Tables[0].Columns[3].ColumnName = "empl_code";
          ds.Tables[0].Columns[4].ColumnName = "empl_name";
          ds.Tables[0].Columns[5].ColumnName = "bank_code";
          ds.Tables[0].Columns[6].ColumnName = "batch_date";
          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();
    }
    //Added By Rahul jain on 05-02-2010
    public static DataSet GetDDListingBYReconcileDate(ref DVOddmStypddreAndStypddrd objSearch)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objSearch.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = objSearch.dateTo.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(objSearch.GET_DIRECT_DEPOSIT_BY_RECONCILE_DATE(ref parameters));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "bank_desc";
          ds.Tables[0].Columns[1].ColumnName = "amount";
          ds.Tables[0].Columns[2].ColumnName = "bank_acct_no";
          ds.Tables[0].Columns[3].ColumnName = "empl_code";
          ds.Tables[0].Columns[4].ColumnName = "empl_name";
          ds.Tables[0].Columns[5].ColumnName = "bank_code";
          ds.Tables[0].Columns[6].ColumnName = "batch_date";
          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();
    }
    //Added By Rahul jain on 05-02-2010
    public static DataSet GetDDListingBYDocumentDate(ref DVOddmStypddreAndStypddrd objSearch)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objSearch.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = objSearch.dateTo.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(objSearch.GET_DIRECT_DEPOSIT_BY_DOCUMENT_DATE(ref parameters));
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "bank_desc";
          ds.Tables[0].Columns[1].ColumnName = "amount";
          ds.Tables[0].Columns[2].ColumnName = "bank_acct_no";
          ds.Tables[0].Columns[3].ColumnName = "empl_code";
          ds.Tables[0].Columns[4].ColumnName = "empl_name";
          ds.Tables[0].Columns[5].ColumnName = "bank_code";
          ds.Tables[0].Columns[6].ColumnName = "batch_date";
          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();
    }

    //public static DataSet GetApprovedPurchaseOders(string DateFrom, string DateTo, string acct_type, string keyvalue)
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        Object[] parameters = new object[4];
    //        parameters[0] = DateFrom;
    //        parameters[1] = DateTo;
    //        parameters[2] = acct_type;
    //        parameters[3] = keyvalue;
    //        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //        DVOPOPurchaseOrdersStuordre obj = new DVOPOPurchaseOrdersStuordre();
    //        ds = objDalBaseClass.GetData(obj.FIND_APPRRVEDPU(ref parameters));

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionManagement.ExceptionManager.Publish(ex);
    //        throw ex;
    //    }
    //    return ds;
    //}

    public static DataSet GetBatchListing(ref DVOBatch objDVOBatchlisting)
    {

      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOBatchlisting.batch_id;
        parameters[1] = objDVOBatchlisting.create_date;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), objDVOBatchlisting.FIND_BATCH_LISTING);
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "v_disc_acct";
          ds.Tables[0].Columns[1].ColumnName = "v_disc_amt";
          ds.Tables[0].Columns[2].ColumnName = "v_disc_deb_cred";
          ds.Tables[0].Columns[3].ColumnName = "v_disc_department";
          ds.Tables[0].Columns[4].ColumnName = "v_dist_acct";
          ds.Tables[0].Columns[5].ColumnName = "v_dist_amt";
          ds.Tables[0].Columns[6].ColumnName = "v_dist_deb_cred";

          ds.Tables[0].Columns[7].ColumnName = "v_dist_department";
          ds.Tables[0].Columns[8].ColumnName = "v_due_date";
          ds.Tables[0].Columns[9].ColumnName = "v_goods_amt";
          ds.Tables[0].Columns[10].ColumnName = "v_inv_doc_no";
          ds.Tables[0].Columns[11].ColumnName = "v_inv_no";
          ds.Tables[0].Columns[12].ColumnName = "v_mtax_code";
          ds.Tables[0].Columns[13].ColumnName = "v_cash_acct";

          ds.Tables[0].Columns[14].ColumnName = "v_cash_amt";
          ds.Tables[0].Columns[15].ColumnName = "v_cash_deb_cred";
          ds.Tables[0].Columns[16].ColumnName = "v_cash_department";
          ds.Tables[0].Columns[17].ColumnName = "v_check_no";
          ds.Tables[0].Columns[18].ColumnName = "v_cust_code";
          ds.Tables[0].Columns[19].ColumnName = "v_doc_desc";
          ds.Tables[0].Columns[20].ColumnName = "v_doc_no";

          ds.Tables[0].Columns[21].ColumnName = "v_oa_acct";
          ds.Tables[0].Columns[22].ColumnName = "v_oa_amt";
          ds.Tables[0].Columns[23].ColumnName = "v_oa_deb_cred";
          ds.Tables[0].Columns[24].ColumnName = "v_oa_department";
          ds.Tables[0].Columns[25].ColumnName = "v_ok_to_post";
          ds.Tables[0].Columns[26].ColumnName = "v_rcpt_date";
          ds.Tables[0].Columns[27].ColumnName = "v_dist_acct_desc";
          ds.Tables[0].Columns[28].ColumnName = "v_cash_acct_desc";
          ds.Tables[0].Columns[29].ColumnName = "v_dist_keyvalue";
          ds.Tables[0].Columns[30].ColumnName = "v_cash_keyvalue";

          ds.Tables[0].Columns.Add("v_tb_tendcode");
          ds.Tables[0].Columns.Add("Message1");
          ds.Tables[0].Columns.Add("Problem1");
          ds.Tables[0].Columns.Add("Problem2");
          ds.Tables[0].Columns.Add("Prpblem3");
          ds.Tables[0].Columns.Add("TBProblem1");
          ds.Tables[0].Columns.Add("TBProblem2");
          ds.Tables[0].Columns.Add("TBProblem3");
          ds.Tables[0].Columns.Add("PostSeq");
          ds.Tables[0].Columns.Add("description");
          ds.Tables[0].Columns.Add("Problem0");
          ds.Tables[0].Columns.Add("post_period");
          ds.Tables[0].Columns.Add("Message2");

          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();


    }

    public static DataSet GetCashDetailsInfo(ref DVOBatch objDVOBatchlisting)
    {
      try
      {
        Object[] parameters = new object[1];
        parameters[0] = objDVOBatchlisting.batch_id;
        // parameters[1] = objDVOBatchlisting.create_date;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), objDVOBatchlisting.GET_BATCH_DETAILS_INFO);
        if (ds != null && ds.Tables.Count > 0)
        {
          //ds.Tables[0].Columns[0].ColumnName = "id";
          //ds.Tables[0].Columns[1].ColumnName = "batch_id";
          //ds.Tables[0].Columns[2].ColumnName = "total_cash";
          //ds.Tables[0].Columns[3].ColumnName = "coin_1";
          //ds.Tables[0].Columns[4].ColumnName = "coin_2";
          //ds.Tables[0].Columns[5].ColumnName = "coin_5";
          //ds.Tables[0].Columns[6].ColumnName = "coin_10";

          //ds.Tables[0].Columns[7].ColumnName = "coin_25";
          //ds.Tables[0].Columns[8].ColumnName = "coin_50";
          //ds.Tables[0].Columns[9].ColumnName = "notes_1";
          //ds.Tables[0].Columns[10].ColumnName = "notes_5";
          //ds.Tables[0].Columns[11].ColumnName = "notes_10";
          //ds.Tables[0].Columns[12].ColumnName = "notes_20";
          //ds.Tables[0].Columns[13].ColumnName = "notes_50";
          //ds.Tables[0].Columns[14].ColumnName = "notes_100";
          //ds.Tables[0].Columns[15].ColumnName = "checktotal";
          //ds.Tables[0].Columns[16].ColumnName = "bch_id";
          //ds.Tables[0].Columns[17].ColumnName = "check_no";
          //ds.Tables[0].Columns[18].ColumnName = "check_date";
          //ds.Tables[0].Columns[19].ColumnName = "bank_name";
          //ds.Tables[0].Columns[20].ColumnName = "checkamount";

          //ds.Tables[0].Columns[21].ColumnName = "USnotes_1";
          //ds.Tables[0].Columns[22].ColumnName = "USnotes_5";
          //ds.Tables[0].Columns[23].ColumnName = "USnotes_10";
          //ds.Tables[0].Columns[24].ColumnName = "USnotes_20";
          //ds.Tables[0].Columns[25].ColumnName = "USnotes_50";
          //ds.Tables[0].Columns[26].ColumnName = "USnotes_100";

          ds.Tables[0].Columns[0].ColumnName = "id";
          ds.Tables[0].Columns[1].ColumnName = "batch_id";
          ds.Tables[0].Columns[2].ColumnName = "total_cash";
          ds.Tables[0].Columns[3].ColumnName = "coin_1";
          ds.Tables[0].Columns[4].ColumnName = "coin_2";
          ds.Tables[0].Columns[5].ColumnName = "coin_5";
          ds.Tables[0].Columns[6].ColumnName = "coin_10";
          ds.Tables[0].Columns[7].ColumnName = "coin_25";
          ds.Tables[0].Columns[8].ColumnName = "coin_50";
          ds.Tables[0].Columns[9].ColumnName = "notes_1";
          ds.Tables[0].Columns[10].ColumnName = "notes_5";
          ds.Tables[0].Columns[11].ColumnName = "notes_10";
          ds.Tables[0].Columns[12].ColumnName = "notes_20";
          ds.Tables[0].Columns[13].ColumnName = "notes_50";
          ds.Tables[0].Columns[14].ColumnName = "notes_100";
          ds.Tables[0].Columns[15].ColumnName = "checktotal";
          ds.Tables[0].Columns[16].ColumnName = "bch_id";
          ds.Tables[0].Columns[17].ColumnName = "check_no";
          ds.Tables[0].Columns[18].ColumnName = "check_date";
          ds.Tables[0].Columns[19].ColumnName = "bank_name";
          ds.Tables[0].Columns[20].ColumnName = "checkamount";

          ds.Tables[0].Columns[21].ColumnName = "USnotes_1";
          ds.Tables[0].Columns[22].ColumnName = "USnotes_5";
          ds.Tables[0].Columns[23].ColumnName = "USnotes_10";
          ds.Tables[0].Columns[24].ColumnName = "USnotes_20";
          ds.Tables[0].Columns[25].ColumnName = "USnotes_50";
          ds.Tables[0].Columns[26].ColumnName = "USnotes_100";



          ds.Tables[0].Columns.Add("NewTotalCash");
          ds.Tables[0].Columns.Add("NewChecktotal");


          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();

    }

    public static DataSet GetRequitionDocDetail(DVORequisitionstureqste objSearch)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] param = new Object[6];
        param[0] = DVOApplicationUserInfo.Ministry;
        param[1] = DVOApplicationUserInfo.MinisDepartment;
        param[2] = objSearch.requester_id;
        param[3] = objSearch.request_date;
        param[4] = objSearch.status;
        param[5] = objSearch.dprt_doc_ref_no;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(objSearch.GET_PRINTREQ(ref param));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }

    public static DataSet GetActualCapExpSourceFundSltdMnth(string Year, string Month, string Ministry)
    {
      DataSet ds = new DataSet();
      try
      {
        Object[] parameters = new object[3];
        parameters[0] = Year.Trim();
        parameters[1] = Month.Trim();
        parameters[2] = Ministry.Trim();

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCapExpByMinistry), (new DVOCapExpByMinistry()).GET_ACTCAPEXP_SRCFUND_SLTDMNTH);
        ds.Tables[0].Columns[0].ColumnName = "ministry";
        ds.Tables[0].Columns[1].ColumnName = "project";
        ds.Tables[0].Columns[2].ColumnName = "projdesc";
        ds.Tables[0].Columns[3].ColumnName = "key";
        ds.Tables[0].Columns[4].ColumnName = "keydesc";
        ds.Tables[0].Columns[5].ColumnName = "activity";
        ds.Tables[0].Columns[6].ColumnName = "this_month";
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return ds;
    }



    public static DataSet GetCashDetailsInfoByDate(ref DVOBatch objDVOBatch)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOBatch.dateFrom.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[1] = objDVOBatch.dateto.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), objDVOBatch.GET_BATCH_DETAILS_INFO_BY_DATE);
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "id";
          ds.Tables[0].Columns[1].ColumnName = "batch_id";
          ds.Tables[0].Columns[2].ColumnName = "total_cash";
          ds.Tables[0].Columns[3].ColumnName = "coin_1";
          ds.Tables[0].Columns[4].ColumnName = "coin_2";
          ds.Tables[0].Columns[5].ColumnName = "coin_5";
          ds.Tables[0].Columns[6].ColumnName = "coin_10";
          ds.Tables[0].Columns[7].ColumnName = "coin_25";
          ds.Tables[0].Columns[8].ColumnName = "coin_50";
          ds.Tables[0].Columns[9].ColumnName = "notes_1";
          ds.Tables[0].Columns[10].ColumnName = "notes_5";
          ds.Tables[0].Columns[11].ColumnName = "notes_10";
          ds.Tables[0].Columns[12].ColumnName = "notes_20";
          ds.Tables[0].Columns[13].ColumnName = "notes_50";
          ds.Tables[0].Columns[14].ColumnName = "notes_100";
          ds.Tables[0].Columns[15].ColumnName = "checktotal";
          ds.Tables[0].Columns[16].ColumnName = "bch_id";
          ds.Tables[0].Columns[17].ColumnName = "check_no";
          ds.Tables[0].Columns[18].ColumnName = "check_date";
          ds.Tables[0].Columns[19].ColumnName = "bank_name";
          ds.Tables[0].Columns[20].ColumnName = "checkamount";

          ds.Tables[0].Columns[21].ColumnName = "USnotes_1";
          ds.Tables[0].Columns[22].ColumnName = "USnotes_5";
          ds.Tables[0].Columns[23].ColumnName = "USnotes_10";
          ds.Tables[0].Columns[24].ColumnName = "USnotes_20";
          ds.Tables[0].Columns[25].ColumnName = "USnotes_50";
          ds.Tables[0].Columns[26].ColumnName = "USnotes_100";



          ds.Tables[0].Columns.Add("NewTotalCash");
          ds.Tables[0].Columns.Add("NewChecktotal");

          decimal _TotalCash = 0;
          decimal _TotalCheck = 0;
          int _oldbatchid = 0;
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            DataRow dr = ds.Tables[0].Rows[i];
            if (_oldbatchid != (dr["batch_id"] != DBNull.Value ? Convert.ToInt32(dr["batch_id"]) : 0))
            {
              _TotalCash += dr["total_cash"] != DBNull.Value ? Convert.ToDecimal(dr["total_cash"]) : 0;
              _TotalCheck += dr["checktotal"] != DBNull.Value ? Convert.ToDecimal(dr["checktotal"]) : 0;
              _oldbatchid = dr["batch_id"] != DBNull.Value ? Convert.ToInt32(dr["batch_id"]) : 0;
              ds.Tables[0].Rows[i]["NewTotalCash"] = _TotalCash;
              ds.Tables[0].Rows[i]["NewChecktotal"] = _TotalCheck;
            }
          }

          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();

    }

    public static DataSet GetApprovalClasssesInfo(ref DVOASApprovalClassesInfo objDVOASApprovalClassesInfo)
    {
      try
      {
        Object[] parameters = new object[2];
        parameters[0] = objDVOASApprovalClassesInfo.dept;
        parameters[1] = objDVOASApprovalClassesInfo.module;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DataSet ds = objDalBaseClass.GetData(objDVOASApprovalClassesInfo.PRINT_APPROVAL_LEVELS_INFO(ref parameters)); //objDVOASApprovalClassesInfo.PRINT_APPROVAL_LEVELS_INFO(ref parameters);// objDalBaseClass.GetData(ref parameters, typeof(DVOASApprovalClassesInfo), (new DVOASApprovalClassesInfo()).PRINT_APPROVAL_LEVELS_INFO);
        if (ds != null && ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "acd_id";
          ds.Tables[0].Columns[1].ColumnName = "module";
          ds.Tables[0].Columns[2].ColumnName = "dept";
          ds.Tables[0].Columns[3].ColumnName = "line_amount";
          ds.Tables[0].Columns[4].ColumnName = "total_amount";
          ds.Tables[0].Columns[5].ColumnName = "global_skip";
          ds.Tables[0].Columns[6].ColumnName = "approval_level";
          ds.Tables[0].Columns[7].ColumnName = "amount";
          ds.Tables[0].Columns[8].ColumnName = "description";



          return ds;
        }
        else
          throw new Exception();
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
      }
      return new DataSet();



    }

    public static DataSet GetTenderListByIssueNum(ref DVOTreasuryBillTenders objtreaTenders)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = new DataSet();
      try
      {

        object[] parameters = new object[7];
        parameters[0] = objtreaTenders.tend_code;
        parameters[1] = objtreaTenders.tend_class;
        parameters[2] = objtreaTenders.tend_name;
        parameters[3] = objtreaTenders.address1;
        parameters[4] = objtreaTenders.Option;
        parameters[5] = objtreaTenders.tbschid;
        parameters[6] = objtreaTenders.issue_num;
        ds = objDalBaseClass.GetData(objtreaTenders.GET_Tender_ListByIssueNum(ref parameters));

        #region set column name
        ds.Tables[0].Columns[0].ColumnName = "tend_code";
        ds.Tables[0].Columns[2].ColumnName = "tend_name";
        ds.Tables[0].Columns[3].ColumnName = "address1";
        ds.Tables[0].Columns[4].ColumnName = "address2";
        ds.Tables[0].Columns[5].ColumnName = "city";
        ds.Tables[0].Columns[6].ColumnName = "contact";
        ds.Tables[0].Columns[7].ColumnName = "phone";
        ds.Tables[0].Columns[8].ColumnName = "email";
        ds.Tables[0].Columns[9].ColumnName = "tbschid";
        ds.Tables[0].Columns[10].ColumnName = "issue_num";
        ds.Tables[0].Columns[11].ColumnName = "tbschname";



        ds.Tables[0].Columns.Add("Last_Issue_No", typeof(string));
        #endregion set column name

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }


    public static DataSet GetEmployeeWeekWorkingHr(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = new DataSet();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objEmployeeWorkHrDetails.StartDate;
        parameters[1] = objEmployeeWorkHrDetails.EndDate;
        parameters[2] = objEmployeeWorkHrDetails.empl_code;
        ds = objDalBaseClass.GetData(objEmployeeWorkHrDetails.PRINT_QUERY(ref parameters));

        #region set column name
        /*ds.Tables[0].Columns[0].ColumnName = "EmpWorkingHrDetailsId";
        ds.Tables[0].Columns[1].ColumnName = "EmpWorkingHrId";
        ds.Tables[0].Columns[2].ColumnName = "empl_code";
        ds.Tables[0].Columns[3].ColumnName = "EmpName";
        ds.Tables[0].Columns[4].ColumnName = "WorkingDate";
        ds.Tables[0].Columns[5].ColumnName = "Remark";
        ds.Tables[0].Columns[6].ColumnName = "WorkingHr";
        ds.Tables[0].Columns[7].ColumnName = "RgDayOvtm";
        ds.Tables[0].Columns[8].ColumnName = "PhOvtm";
        ds.Tables[0].Columns[9].ColumnName = "StartDate";
        ds.Tables[0].Columns[10].ColumnName = "EndDate";*/
        #endregion set column name

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }

    public static DataSet GetEmployeeWeekWorkingHrRpt(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = new DataSet();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objEmployeeWorkHrDetails.StartDate;
        parameters[1] = objEmployeeWorkHrDetails.EndDate;
        parameters[2] = objEmployeeWorkHrDetails.empl_code;
        ds = objDalBaseClass.GetData(objEmployeeWorkHrDetails.PRINT_QUERY_RPT(ref parameters));

        #region set column name
        /*ds.Tables[0].Columns[0].ColumnName = "EmpWorkingHrDetailsId";
        ds.Tables[0].Columns[1].ColumnName = "EmpWorkingHrId";
        ds.Tables[0].Columns[2].ColumnName = "empl_code";
        ds.Tables[0].Columns[3].ColumnName = "EmpName";
        ds.Tables[0].Columns[4].ColumnName = "WorkingDate";
        ds.Tables[0].Columns[5].ColumnName = "Remark";
        ds.Tables[0].Columns[6].ColumnName = "WorkingHr";
        ds.Tables[0].Columns[7].ColumnName = "RgDayOvtm";
        ds.Tables[0].Columns[8].ColumnName = "PhOvtm";
        ds.Tables[0].Columns[9].ColumnName = "StartDate";
        ds.Tables[0].Columns[10].ColumnName = "EndDate";*/
        #endregion set column name

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }

    public static DataSet GetEmployeeWeekTotalWorkingHr(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = new DataSet();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objEmployeeWorkHrDetails.empl_code;
        parameters[1] = objEmployeeWorkHrDetails.StartDate;
        parameters[2] = objEmployeeWorkHrDetails.EndDate;
        //ds = objDalBaseClass.GetData(objEmployeeWorkHrDetails.PRINT_QUERY(ref parameters));
        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOEmployeeWorkHrDetails), objEmployeeWorkHrDetails.GET_TimCardRptGet);

        #region set column name
        /*ds.Tables[0].Columns[0].ColumnName = "EmpWorkingHrDetailsId";
        ds.Tables[0].Columns[1].ColumnName = "EmpWorkingHrId";
        ds.Tables[0].Columns[2].ColumnName = "empl_code";
        ds.Tables[0].Columns[3].ColumnName = "EmpName";
        ds.Tables[0].Columns[4].ColumnName = "WorkingDate";
        ds.Tables[0].Columns[5].ColumnName = "Remark";
        ds.Tables[0].Columns[6].ColumnName = "WorkingHr";
        ds.Tables[0].Columns[7].ColumnName = "RgDayOvtm";
        ds.Tables[0].Columns[8].ColumnName = "PhOvtm";
        ds.Tables[0].Columns[9].ColumnName = "StartDate";
        ds.Tables[0].Columns[10].ColumnName = "EndDate";*/
        #endregion set column name

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;

    }
    public static List<ArearModel> GetArearReportData(string SchemeType)
    {
      DALBaseClassHelper objDALBaseClassHeler = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHeler.GetDAL();
      List<ArearModel> LogsList = new List<ArearModel>();
      DataSet ds = new DataSet();
      try
      {
        SqlParameter[] Parameter = new SqlParameter[]
        {
          new SqlParameter("@SchemeType",SchemeType)
        };
        ds = objDALBaseClass.GetArearData(Parameter);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          ArearModel AuditLogs = new ArearModel();
          AuditLogs.ApplicationReferenceNo = (dr[0] != DBNull.Value ? (dr[0]).ToString() : "");
          AuditLogs.ApplicantName = (dr[1] != DBNull.Value ? (dr[1]).ToString() : "");
          AuditLogs.GrossAmount = (dr[2] != DBNull.Value ? (dr[2]).ToString() : "");
          AuditLogs.Jan = (dr[3] != DBNull.Value ? (dr[3]).ToString() : "");
          AuditLogs.Feb = (dr[4] != DBNull.Value ? (dr[4]).ToString() : "");
          AuditLogs.Mar = (dr[5] != DBNull.Value ? (dr[5]).ToString() : "");
          AuditLogs.Apr = (dr[6] != DBNull.Value ? (dr[6]).ToString() : "");
          AuditLogs.May = (dr[7] != DBNull.Value ? (dr[7]).ToString() : "");
          AuditLogs.Jun = (dr[8] != DBNull.Value ? (dr[8]).ToString() : "");
          AuditLogs.Jul = (dr[9] != DBNull.Value ? (dr[9]).ToString() : "");
          AuditLogs.Aug = (dr[10] != DBNull.Value ? (dr[10]).ToString() : "");
          AuditLogs.Sep = (dr[11] != DBNull.Value ? (dr[11]).ToString() : "");
          AuditLogs.Oct = (dr[12] != DBNull.Value ? (dr[12]).ToString() : "");
          AuditLogs.Nov = (dr[13] != DBNull.Value ? (dr[13]).ToString() : "");
          AuditLogs.Dec = (dr[14] != DBNull.Value ? (dr[14]).ToString() : "");
          LogsList.Add(AuditLogs);
        }
      }
      catch (Exception ex)
      {

        throw ex;
      }
      return LogsList;
    }
  }
}
