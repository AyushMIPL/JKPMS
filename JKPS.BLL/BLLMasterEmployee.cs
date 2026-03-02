using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using App.Data.ViewModels;
using JKPS.COMMON;
using JKPS.DL;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using Autofac.Core;


namespace JKPS.BLL
{
  public class BLLMasterEmployee
  {
    /// <summary>
    /// This method is use to get information from database based on search options
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">A class object passed as parameter having parameter data</param>
    /// <returns>return a list having searched information</returns>
    public static List<DVOMasterEmployee> GetData(ref DVOMasterEmployee pobjDVOEmployeeStyemplr)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();

      try
      {
        object[] parameters = new object[29];
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
        parameters[26] = pobjDVOEmployeeStyemplr.mailid;
        parameters[27] = pobjDVOEmployeeStyemplr.PersonID;
        parameters[28] = pobjDVOEmployeeStyemplr.ApplicationReferenceNo;

        //parameters[28] = pobjDVOEmployeeStyemplr.CurrentStatus;
        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployee)))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//"p_empl_code"
            objDVOEmployeeStyemplr.SocSecNum = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//"p_soc_sec_num"
            objDVOEmployeeStyemplr.TypeCode = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//"p_type_code"
            if (dr[3] != null)
              objDVOEmployeeStyemplr.Birthdate = ((dr[3] != DBNull.Value && dr[3].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_birthdate"
            objDVOEmployeeStyemplr.FirstName = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//"p_last_name"
            objDVOEmployeeStyemplr.Address1 = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_address1"
            objDVOEmployeeStyemplr.Address2 = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"p_address2"
            objDVOEmployeeStyemplr.City = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//"p_city"
            objDVOEmployeeStyemplr.State = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//"p_state"
            objDVOEmployeeStyemplr.Pincode = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//"p_zip"
            objDVOEmployeeStyemplr.Phone = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);//"p_phone"
            objDVOEmployeeStyemplr.CashAcct = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//"p_cash_acct"
            objDVOEmployeeStyemplr.Department = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty);//"p_department"
            objDVOEmployeeStyemplr.JobCode = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);//"p_job_code"
            objDVOEmployeeStyemplr.JobTitle = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);//"p_job_title"
            if (dr[17] != null)
              objDVOEmployeeStyemplr.DateHired = ((dr[17] != DBNull.Value && dr[17].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[17]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_date_hired"
            if (dr[18] != null)
              objDVOEmployeeStyemplr.Terminated = ((dr[18] != DBNull.Value && dr[18].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[18]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_terminated"
            objDVOEmployeeStyemplr.EmplStatus = (dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty);//"p_empl_status"
            objDVOEmployeeStyemplr.PayPeriod = (dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty);//"p_pay_period"
            objDVOEmployeeStyemplr.Allowances = (dr[21] != DBNull.Value ? Convert.ToInt32(dr[21]) : 0);//"p_allowances"
            objDVOEmployeeStyemplr.StateAllow = (dr[22] != DBNull.Value ? Convert.ToInt32(dr[22]) : 0);//"p_state_allow"
            objDVOEmployeeStyemplr.MaritalStat = (dr[23] != DBNull.Value ? dr[23].ToString().Trim() : string.Empty);//"p_marital_stat"
            objDVOEmployeeStyemplr.VacCode = (dr[24] != DBNull.Value ? dr[24].ToString().Trim() : string.Empty);//"p_vac_code"
            objDVOEmployeeStyemplr.VacAllowed = (dr[25] != DBNull.Value ? (decimal?)(dr[25]) : null);//"p_vac_allowed"
            objDVOEmployeeStyemplr.VacUsed = (dr[26] != DBNull.Value ? (decimal?)(dr[26]) : null);//"p_vac_used"
            objDVOEmployeeStyemplr.SickCode = (dr[27] != DBNull.Value ? dr[27].ToString().Trim() : string.Empty);//"p_sick_code"
            objDVOEmployeeStyemplr.SickAllowed = (dr[28] != DBNull.Value ? (decimal?)(dr[28]) : null);//"p_sick_allowed"
            objDVOEmployeeStyemplr.SickUsed = (dr[29] != DBNull.Value ? (decimal?)(dr[29]) : null);//"p_sick_used"
            if (dr[30] != null)
              objDVOEmployeeStyemplr.LastPay = ((dr[30] != DBNull.Value && dr[30].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[30]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_last_pay"
            objDVOEmployeeStyemplr.HoldPayment = (dr[31] != DBNull.Value ? dr[31].ToString().Trim() : string.Empty);//"p_hold_pymnt"
            objDVOEmployeeStyemplr.StaTaxCode = (dr[32] != DBNull.Value ? dr[32].ToString().Trim() : string.Empty);//"p_statax_code"
            objDVOEmployeeStyemplr.LocTaxCode = (dr[33] != DBNull.Value ? dr[33].ToString().Trim() : string.Empty);//"p_loctax_code"
            objDVOEmployeeStyemplr.SickAccrCodr = (dr[34] != DBNull.Value ? dr[34].ToString().Trim() : string.Empty);//"p_sick_accr_code"
            objDVOEmployeeStyemplr.SickAccrCtr = (dr[35] != DBNull.Value ? Convert.ToInt32(dr[35]) : 0);//"p_sick_accr_ctr"
            if (dr[36] != null)
              objDVOEmployeeStyemplr.SickLapseDate = ((dr[36] != DBNull.Value && dr[36].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[36]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_sick_lapse_date"
            objDVOEmployeeStyemplr.VacAccrCode = (dr[37] != DBNull.Value ? dr[37].ToString().Trim() : string.Empty);//"p_vac_accr_code"
            objDVOEmployeeStyemplr.VacAccrCtr = (dr[38] != DBNull.Value ? Convert.ToInt32(dr[38]) : 0);//"p_vac_accr_ctr"
            if (dr[39] != null)
              objDVOEmployeeStyemplr.VacLapseDate = ((dr[39] != DBNull.Value && dr[39].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[39]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_vac_lapse_date"
            objDVOEmployeeStyemplr.DirDept = (dr[40] != DBNull.Value ? dr[40].ToString().Trim() : string.Empty);//"p_dir_dept"
            objDVOEmployeeStyemplr.DfiDest = (dr[41] != DBNull.Value ? Convert.ToInt32(dr[41]) : 0);//"p_dfi_dest"
            objDVOEmployeeStyemplr.ChkDigit = (dr[42] != DBNull.Value ? Convert.ToInt32(dr[42]) : 0);//"p_chk_digit"
            objDVOEmployeeStyemplr.BankAcctNo = (dr[43] != DBNull.Value ? dr[43].ToString().Trim() : string.Empty);//"p_bank_acct_no"
            objDVOEmployeeStyemplr.StateUdf = (dr[44] != DBNull.Value ? (decimal?)(dr[44]) : null);//"p_state_udf"
            objDVOEmployeeStyemplr.FlexDeptAcctType = (dr[45] != DBNull.Value ? dr[45].ToString().Trim() : string.Empty);//"p_flexdeptaccttype"
            if (dr[46] != null)
              objDVOEmployeeStyemplr.LastIncDate = ((dr[46] != DBNull.Value && dr[46].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[46]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_last_inc_date"
            if (dr[47] != null)
              objDVOEmployeeStyemplr.AppointDate = ((dr[47] != DBNull.Value && dr[47].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[47]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_appoint_date"
            objDVOEmployeeStyemplr.Gender = (dr[48] != DBNull.Value ? dr[48].ToString().Trim() : string.Empty);//"p_gender"
            objDVOEmployeeStyemplr.RowID = (dr[49] != DBNull.Value) ? Convert.ToInt32(dr[49]) : 0;//"p_gender"
            objDVOEmployeeStyemplr.cash_acct_kv = (dr[50] != DBNull.Value ? dr[50].ToString().Trim() : string.Empty);//"cash_acct_kv"
            objDVOEmployeeStyemplr.type_desc = (dr[51] != DBNull.Value ? dr[51].ToString().Trim() : string.Empty);//"type_desc"
            objDVOEmployeeStyemplr.mailid = (dr[52] != DBNull.Value ? dr[52].ToString().Trim() : string.Empty);//"type_desc"

            objDVOEmployeeStyemplr.Prefix = (dr[53] != DBNull.Value ? dr[53].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Suffix = (dr[54] != DBNull.Value ? dr[54].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.MaidenName = (dr[55] != DBNull.Value ? dr[55].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Nationality = (dr[56] != DBNull.Value ? dr[56].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PostalAddress = (dr[57] != DBNull.Value ? dr[57].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PhoneOffice = (dr[58] != DBNull.Value ? dr[58].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Mobile = (dr[59] != DBNull.Value ? dr[59].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.EmplrCode = (dr[60] != DBNull.Value ? Convert.ToInt32(dr[60].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.EmplrName = (dr[61] != DBNull.Value ? dr[61].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PensionerType = (dr[62] != DBNull.Value ? dr[62].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PersonID = (dr[63] != DBNull.Value ? dr[63].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.AnnualSalary = (dr[64] != DBNull.Value ? Convert.ToDecimal(dr[64].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.CurrentTask = (dr[65] != DBNull.Value ? (dr[65].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.CurrentStatus = (dr[66] != DBNull.Value ? (dr[66].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.LastTask = (dr[67] != DBNull.Value ? (dr[67].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.VersionNo = (dr[68] != DBNull.Value ? (dr[68].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.ApplicationReferenceNo = (dr[69] != DBNull.Value ? (dr[69].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PhoneOffice = (dr[70] != DBNull.Value ? (dr[70].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.EMail = (dr[71] != DBNull.Value ? (dr[71].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PercentageofDisability = (dr[72] != DBNull.Value ? (dr[72].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.JKISSS = (dr[73] != DBNull.Value ? (dr[73].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.SubmissionLocation = (dr[74] != DBNull.Value ? (dr[74].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.SubmissionDate = (dr[75] != DBNull.Value) ? Convert.ToDateTime(dr[75]) : (DateTime?)null;
            objDVOEmployeeStyemplr.TSWO = (dr[76] != DBNull.Value ? (dr[76].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.SelectDistrict = (dr[77] != DBNull.Value ? (dr[77].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.AgeInYears = (dr[78] != DBNull.Value ? Convert.ToInt32(dr[78]) : 0);
            //objDVOEmployeeStyemplr.District = (dr[79] != DBNull.Value ? (dr[79].ToString().Trim()) : string.Empty); 
            objDVOEmployeeStyemplr.DoyouhaveBPLcard = (dr[79] != DBNull.Value) ? (dr[79].ToString()) : string.Empty;
            objDVOEmployeeStyemplr.FatherOrHusbandOrGuardianName = (dr[80] != DBNull.Value ? (dr[80].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.Category = (dr[81] != DBNull.Value ? (dr[81].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PermanentAddress = (dr[82] != DBNull.Value ? (dr[82].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PermanentDistrict = (dr[83] != DBNull.Value ? (dr[83].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PermanentTehsil = (dr[84] != DBNull.Value ? (dr[84].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PermanentHalqaPanchayatMunicipalityName = (dr[85] != DBNull.Value ? (dr[85].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PermanentVillageName = (dr[86] != DBNull.Value ? (dr[86].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.ApplicationSanctionedunderSchemeName = (dr[87] != DBNull.Value ? (dr[87].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PresentVillageName = (dr[88] != DBNull.Value ? (dr[88].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PresentDistrict = (dr[89] != DBNull.Value ? (dr[89].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PresentAddress = (dr[90] != DBNull.Value ? (dr[90].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.CivilCondition = (dr[91] != DBNull.Value ? (dr[91].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PresentHalqaPanchayatMunicipalityName = (dr[92] != DBNull.Value ? (dr[92].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.PresentTehsil = (dr[93] != DBNull.Value ? (dr[93].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.ReasonForChange = (dr[94] != DBNull.Value ? (dr[94].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.Last_pay_date = (dr[95] != DBNull.Value ? (dr[95].ToString().Trim()) : string.Empty);
            objDVOEmployeeStyemplr.Application_approve_on = (dr[96] != DBNull.Value ? (dr[96].ToString().Trim()) : string.Empty);

            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        parameters = null;
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      return listDVOEmployeeStyemplr;
    }

    public static List<DVOMasterEmployee> GetData(ref DVOMasterEmployee pobjDVOEmployeeStyemplr, string employername)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();

      try
      {


        pobjDVOEmployeeStyemplr.acct_type = "EXPENS";


        pobjDVOEmployeeStyemplr.segment_code = "AGENCIES /INSTRUCTORS";


        pobjDVOEmployeeStyemplr.segment_value = employername;

        using (DataSet ds = ReportingUtilities.GetEmployeeListByEmployer(ref pobjDVOEmployeeStyemplr))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//"p_empl_code"
            objDVOEmployeeStyemplr.SocSecNum = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);//"p_soc_sec_num"
            objDVOEmployeeStyemplr.TypeCode = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);//"p_type_code"

            objDVOEmployeeStyemplr.FirstName = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"p_last_name"
            objDVOEmployeeStyemplr.HoldPayment = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//"p_last_name"
            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        // parameters = null;
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      return listDVOEmployeeStyemplr;
    }
    //searching Committed
    public static List<DVOMasterEmployee> GetAllData(int UserId, int RoleId, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", Int32 start = 0, Int32 fatch = 10, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "", string CBS_Name = "", bool IsDuplicateAccountData = false)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();

      try
      {
        SqlParameter[] Parameters = new SqlParameter[]
        {
          new SqlParameter("@UserId",UserId),
          new SqlParameter("@RoleId",RoleId),
          new SqlParameter("@Tehsil",Tehsil),
          new SqlParameter("@BeneficiariesName",BeneficiariesType),
          new SqlParameter("@Gender",Gender),
          new SqlParameter("@District",RegionNames),
          new SqlParameter("@skip",start),
          new SqlParameter("@fatch",fatch),
          new SqlParameter("@AccountStatus",AccountStatus),
          new SqlParameter("@bank_acct_no",bank_acct_no),
          new SqlParameter("@APPLICATION_REFERENCE_NO",APPLICATION_REFERENCE_NO)


        };
        using (DataSet ds = IsDuplicateAccountData ? objDalBaseClass.GetDuplicateAccountData(Parameters) : objDalBaseClass.GetAllData(typeof(DVOMasterEmployee), Parameters))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//"p_empl_code"
            objDVOEmployeeStyemplr.SocSecNum = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//"p_soc_sec_num"
            objDVOEmployeeStyemplr.TypeCode = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//"p_type_code"
            if (dr[3] != null)
              objDVOEmployeeStyemplr.Birthdate = ((dr[3] != DBNull.Value && dr[3].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_birthdate"
            objDVOEmployeeStyemplr.FirstName = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//"p_last_name"
            objDVOEmployeeStyemplr.Address1 = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_address1"
            objDVOEmployeeStyemplr.Address2 = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"p_address2"
            objDVOEmployeeStyemplr.City = (dr[9] != DBNull.Value ? dr[9].ToString().Trim().ToUpper() : string.Empty);//"p_city"
            objDVOEmployeeStyemplr.State = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//"p_state"
            objDVOEmployeeStyemplr.Zip = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//"p_zip"
            objDVOEmployeeStyemplr.Phone = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);//"p_phone"
            objDVOEmployeeStyemplr.CashAcct = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//"p_cash_acct"
            objDVOEmployeeStyemplr.Department = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty);//"p_department"
            objDVOEmployeeStyemplr.JobCode = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);//"p_job_code"
            objDVOEmployeeStyemplr.JobTitle = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);//"p_job_title"
            if (dr[17] != null)
              objDVOEmployeeStyemplr.DateHired = ((dr[17] != DBNull.Value && dr[17].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[17]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_date_hired"
            if (dr[18] != null)
              objDVOEmployeeStyemplr.Terminated = ((dr[18] != DBNull.Value && dr[18].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[18]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_terminated"
            objDVOEmployeeStyemplr.EmplStatus = (dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty);//"p_empl_status"
            objDVOEmployeeStyemplr.PayPeriod = (dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty);//"p_pay_period"
            objDVOEmployeeStyemplr.Allowances = (dr[21] != DBNull.Value ? Convert.ToInt32(dr[21]) : 0);//"p_allowances"
            objDVOEmployeeStyemplr.StateAllow = (dr[22] != DBNull.Value ? Convert.ToInt32(dr[22]) : 0);//"p_state_allow"
            objDVOEmployeeStyemplr.MaritalStat = (dr[23] != DBNull.Value ? dr[23].ToString().Trim() : string.Empty);//"p_marital_stat"
            objDVOEmployeeStyemplr.VacCode = (dr[24] != DBNull.Value ? dr[24].ToString().Trim() : string.Empty);//"p_vac_code"
            objDVOEmployeeStyemplr.VacAllowed = (dr[25] != DBNull.Value ? (decimal?)(dr[25]) : null);//"p_vac_allowed"
            objDVOEmployeeStyemplr.VacUsed = (dr[26] != DBNull.Value ? (decimal?)(dr[26]) : null);//"p_vac_used"
            objDVOEmployeeStyemplr.SickCode = (dr[27] != DBNull.Value ? dr[27].ToString().Trim() : string.Empty);//"p_sick_code"
            objDVOEmployeeStyemplr.SickAllowed = (dr[28] != DBNull.Value ? (decimal?)(dr[28]) : null);//"p_sick_allowed"
            objDVOEmployeeStyemplr.SickUsed = (dr[29] != DBNull.Value ? (decimal?)(dr[29]) : null);//"p_sick_used"
            if (dr[30] != null)
              objDVOEmployeeStyemplr.LastPay = ((dr[30] != DBNull.Value && dr[30].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[30]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_last_pay"
            objDVOEmployeeStyemplr.HoldPayment = (dr[31] != DBNull.Value ? dr[31].ToString().Trim() : string.Empty);//"p_hold_pymnt"
            objDVOEmployeeStyemplr.StaTaxCode = (dr[32] != DBNull.Value ? dr[32].ToString().Trim() : string.Empty);//"p_statax_code"
            objDVOEmployeeStyemplr.LocTaxCode = (dr[33] != DBNull.Value ? dr[33].ToString().Trim() : string.Empty);//"p_loctax_code"
            objDVOEmployeeStyemplr.SickAccrCodr = (dr[34] != DBNull.Value ? dr[34].ToString().Trim() : string.Empty);//"p_sick_accr_code"
            objDVOEmployeeStyemplr.SickAccrCtr = (dr[35] != DBNull.Value ? Convert.ToInt32(dr[35]) : 0);//"p_sick_accr_ctr"
            if (dr[36] != null)
              objDVOEmployeeStyemplr.SickLapseDate = ((dr[36] != DBNull.Value && dr[36].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[36]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_sick_lapse_date"
            objDVOEmployeeStyemplr.VacAccrCode = (dr[37] != DBNull.Value ? dr[37].ToString().Trim() : string.Empty);//"p_vac_accr_code"
            objDVOEmployeeStyemplr.VacAccrCtr = (dr[38] != DBNull.Value ? Convert.ToInt32(dr[38]) : 0);//"p_vac_accr_ctr"
            if (dr[39] != null)
              objDVOEmployeeStyemplr.VacLapseDate = ((dr[39] != DBNull.Value && dr[39].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[39]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_vac_lapse_date"
            objDVOEmployeeStyemplr.DirDept = (dr[40] != DBNull.Value ? dr[40].ToString().Trim() : string.Empty);//"p_dir_dept"
            objDVOEmployeeStyemplr.DfiDest = (dr[41] != DBNull.Value ? Convert.ToInt32(dr[41]) : 0);//"p_dfi_dest"
            objDVOEmployeeStyemplr.ChkDigit = (dr[42] != DBNull.Value ? Convert.ToInt32(dr[42]) : 0);//"p_chk_digit"
            objDVOEmployeeStyemplr.BankAcctNo = (dr[43] != DBNull.Value ? dr[43].ToString().Trim() : string.Empty);//"p_bank_acct_no"
            objDVOEmployeeStyemplr.StateUdf = (dr[44] != DBNull.Value ? (decimal?)(dr[44]) : null);//"p_state_udf"
            objDVOEmployeeStyemplr.FlexDeptAcctType = (dr[45] != DBNull.Value ? dr[45].ToString().Trim() : string.Empty);//"p_flexdeptaccttype"
            if (dr[46] != null)
              objDVOEmployeeStyemplr.LastIncDate = ((dr[46] != DBNull.Value && dr[46].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[46]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_last_inc_date"
            if (dr[47] != null)
              objDVOEmployeeStyemplr.AppointDate = ((dr[47] != DBNull.Value && dr[47].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[47]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_appoint_date"
            objDVOEmployeeStyemplr.Gender = (dr[48] != DBNull.Value ? dr[48].ToString().Trim() : string.Empty);//"p_gender"
            objDVOEmployeeStyemplr.RowID = (dr[49] != DBNull.Value) ? Convert.ToInt32(dr[49]) : 0;//"p_gender"
            objDVOEmployeeStyemplr.cash_acct_kv = (dr[50] != DBNull.Value ? dr[50].ToString().Trim() : string.Empty);//"cash_acct_kv"
            objDVOEmployeeStyemplr.type_desc = (dr[51] != DBNull.Value ? dr[51].ToString().Trim() : string.Empty);//"type_desc"

            objDVOEmployeeStyemplr.mailid = (dr[52] != DBNull.Value ? dr[52].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Prefix = (dr[53] != DBNull.Value ? dr[53].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Suffix = (dr[54] != DBNull.Value ? dr[54].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.MaidenName = (dr[55] != DBNull.Value ? dr[55].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Nationality = (dr[56] != DBNull.Value ? dr[56].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PostalAddress = (dr[57] != DBNull.Value ? dr[57].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PhoneOffice = (dr[58] != DBNull.Value ? dr[58].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Mobile = (dr[59] != DBNull.Value ? dr[59].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.EmplrCode = (dr[60] != DBNull.Value ? Convert.ToInt32(dr[60].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.EmplrName = (dr[61] != DBNull.Value ? dr[61].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PensionerType = (dr[62] != DBNull.Value ? dr[62].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.PersonID = (dr[63] != DBNull.Value ? dr[63].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.AnnualSalary = (dr[64] != DBNull.Value ? Convert.ToDecimal(dr[64].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.AgeInYears = (dr[65] != DBNull.Value ? Convert.ToInt32(dr[65].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.ApplicationReferenceNo = (dr[66] != DBNull.Value ? dr[66].ToString().Trim() : "");
            objDVOEmployeeStyemplr.mailid = (dr[67] != DBNull.Value ? dr[67].ToString().Trim() : "");
            objDVOEmployeeStyemplr.IFSCCode = (dr[68] != DBNull.Value ? dr[68].ToString().Trim() : "");
            objDVOEmployeeStyemplr.BankName = (dr[69] != DBNull.Value ? dr[69].ToString().Trim() : "");
            objDVOEmployeeStyemplr.LastVerified = ((dr[70] != DBNull.Value && dr[70].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[70]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_appoint_date"

            objDVOEmployeeStyemplr.ReasonForChange = (dr[71] != DBNull.Value ? dr[71].ToString().Trim() : "");
            objDVOEmployeeStyemplr.ACCOUNT_STATUS = (dr[72] != DBNull.Value ? dr[72].ToString().Trim() : "");
            objDVOEmployeeStyemplr.CBS_Name = (dr[73] != DBNull.Value ? dr[73].ToString().Trim() : "");
            objDVOEmployeeStyemplr.recordCount = (dr[74] != DBNull.Value ? dr[74].ToString().Trim() : "0");


            //objDVOEmployeeStyemplr.LengthOfQualifyingServiceInMonthsTo31Dec2003 = (dr[62] != DBNull.Value ? Convert.ToInt32(dr[62].ToString().Trim()) : 0);
            //objDVOEmployeeStyemplr.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = (dr[63] != DBNull.Value ? Convert.ToInt32(dr[63].ToString().Trim()) : 0);


            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      //var district = RegionNames?.ToLower().Split(',').Select(x => x.Trim()).ToArray();
      //listDVOEmployeeStyemplr = listDVOEmployeeStyemplr.Where(x => (string.IsNullOrEmpty(Gender) ? true : x.Gender.ToLower().Trim() == Gender.ToLower().Trim())
      //&& (string.IsNullOrEmpty(Tehsil) ? true : x.Address1.ToLower().Trim().Contains(Tehsil.ToLower().Trim()))
      //&& (string.IsNullOrEmpty(Tehsil) ? true : district.Contains(x.Address1.ToLower().Trim()))).ToList();
      return listDVOEmployeeStyemplr;
    }
    public static List<DVOMasterEmployee> GetAllDuplicateAccountsAcrossDatabases(int UserId, int RoleId, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", Int32 start = 0, Int32 fatch = 10, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "", string CBS_Name = "", bool IsDuplicateAccountData = false)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();

      try
      {
        SqlParameter[] Parameters = new SqlParameter[]
        {
          new SqlParameter("@UserId",UserId),
          new SqlParameter("@RoleId",RoleId),
          new SqlParameter("@Tehsil",Tehsil),
          new SqlParameter("@BeneficiariesType",BeneficiariesType),
          new SqlParameter("@Gender",Gender),
          new SqlParameter("@District",RegionNames),
          new SqlParameter("@skip",start),
          new SqlParameter("@fatch",fatch),
          new SqlParameter("@AccountStatus",AccountStatus),
          new SqlParameter("@bank_acct_no",bank_acct_no),
          new SqlParameter("@APPLICATION_REFERENCE_NO",APPLICATION_REFERENCE_NO)


        };

        using (DataSet ds = IsDuplicateAccountData ? objDalBaseClass.GetDuplicateAcrossDatabases(Parameters) : objDalBaseClass.GetAllData(typeof(DVOMasterEmployee), Parameters))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//"p_empl_code"           
            objDVOEmployeeStyemplr.TypeCode = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//"p_type_code"
            if (dr[2] != null)
            objDVOEmployeeStyemplr.Birthdate = ((dr[2] != DBNull.Value && dr[2].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//"p_birthdate"
            objDVOEmployeeStyemplr.FirstName = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//"p_last_name"
            objDVOEmployeeStyemplr.City = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//"p_city"
            objDVOEmployeeStyemplr.Gender = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_gender"
            objDVOEmployeeStyemplr.Phone = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"p_phone"            
            objDVOEmployeeStyemplr.BankAcctNo = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//"p_bank_acct_no"
            objDVOEmployeeStyemplr.Address1 = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//"p_address1"
            objDVOEmployeeStyemplr.AgeInYears = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11].ToString().Trim()) : 0);
            objDVOEmployeeStyemplr.ApplicationReferenceNo = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : "");
            objDVOEmployeeStyemplr.mailid = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.IFSCCode = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : "");
            objDVOEmployeeStyemplr.BankName = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : "");
            objDVOEmployeeStyemplr.ReasonForChange = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : "");
            objDVOEmployeeStyemplr.ACCOUNT_STATUS = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : "");
            objDVOEmployeeStyemplr.CBS_Name = (dr[18] != DBNull.Value ? dr[18].ToString().Trim() : "");            
            objDVOEmployeeStyemplr.HoldPayment = (dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty);//"p_hold_pymnt"
            objDVOEmployeeStyemplr.recordCount = (dr[21] != DBNull.Value ? dr[21].ToString().Trim() : "0");
            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      //var district = RegionNames?.ToLower().Split(',').Select(x => x.Trim()).ToArray();
      //listDVOEmployeeStyemplr = listDVOEmployeeStyemplr.Where(x => (string.IsNullOrEmpty(Gender) ? true : x.Gender.ToLower().Trim() == Gender.ToLower().Trim())
      //&& (string.IsNullOrEmpty(Tehsil) ? true : x.Address1.ToLower().Trim().Contains(Tehsil.ToLower().Trim()))
      //&& (string.IsNullOrEmpty(Tehsil) ? true : district.Contains(x.Address1.ToLower().Trim()))).ToList();
      return listDVOEmployeeStyemplr;
    }
    public static DataSet GetAllBeneficiaryStatusReportData(int UserId, int RoleId, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", Int32 start = 0, Int32 fatch = 10, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();
      DataSet ds = null;
      try
      {
        SqlParameter[] Parameters = new SqlParameter[]
        {
          new SqlParameter("@UserId",UserId),
          new SqlParameter("@RoleId",RoleId),
          new SqlParameter("@Tehsil",Tehsil),
          new SqlParameter("@BeneficiariesName",BeneficiariesType),
          new SqlParameter("@Gender",Gender),
          new SqlParameter("@District",RegionNames),
          new SqlParameter("@skip",start),
          new SqlParameter("@fatch",fatch),
          new SqlParameter("@AccountStatus",AccountStatus),
          new SqlParameter("@bank_acct_no",bank_acct_no),
          new SqlParameter("@APPLICATION_REFERENCE_NO",APPLICATION_REFERENCE_NO)


        };
        ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmployee), Parameters);



      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw;
      }
      //var district = RegionNames?.ToLower().Split(',').Select(x => x.Trim()).ToArray();
      //listDVOEmployeeStyemplr = listDVOEmployeeStyemplr.Where(x => (string.IsNullOrEmpty(Gender) ? true : x.Gender.ToLower().Trim() == Gender.ToLower().Trim())
      //&& (string.IsNullOrEmpty(Tehsil) ? true : x.Address1.ToLower().Trim().Contains(Tehsil.ToLower().Trim()))
      //&& (string.IsNullOrEmpty(Tehsil) ? true : district.Contains(x.Address1.ToLower().Trim()))).ToList();
      return ds;
    }

    public static List<TotalContributionSummaryViewModel> GelAllPaymentCount(int UserId, int RoleId, Int32 financialYear)
    {
      List<TotalContributionSummaryViewModel> listCount = new List<TotalContributionSummaryViewModel>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      SqlParameter[] Parameter = new SqlParameter[]
      {
                new SqlParameter("@RoleId",RoleId),
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@FYear",financialYear)
      };
      DataSet ds = objDalBaseClass.GetPaymentCount(Parameter);
      if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          TotalContributionSummaryViewModel ContributorCont = new TotalContributionSummaryViewModel();
          ContributorCont.EmployerId = dr[0] == DBNull.Value ? "" : dr[0].ToString();//"RegionName"
          ContributorCont.EmployerName = dr[1] == DBNull.Value ? "" : dr[1].ToString();//"District"
          ContributorCont.TotalContributorContribution = dr[2] == DBNull.Value ? 0 : Convert.ToDecimal(dr[2]);//"TotalYearAmount"
          ContributorCont.TotalMonthAmount = dr[3] == DBNull.Value ? 0 : Convert.ToDecimal(dr[3]);//"TotalMonthAmount"
          ContributorCont.PayMonthInYear = dr[4] == DBNull.Value ? "" : dr[4].ToString();//"OK"  
          ContributorCont.LastPayMonthInYear = dr[5] == DBNull.Value ? "" : dr[5].ToString();//"OK"  
          ContributorCont.TotalEmployerContribution = dr[6] == DBNull.Value ? "" : dr[6].ToString();//"OK"  
          ContributorCont.status = dr[7] == DBNull.Value ? "" : dr[7].ToString();//"Fail"
          ContributorCont.NotUpdated = dr[8] == DBNull.Value ? "" : dr[8].ToString();//"Status Not Updated"
          listCount.Add(ContributorCont);
        }
      }
      return listCount;
    }
    /// <summary>
    /// To Insert Employee Deduction Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
    /// <returns></returns>
    public static int InsertEmployeeDeductions(ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        #region parameters
        object[] parameters = new object[1];

        #endregion parameters
        //foreach (DVOMasterEmployeeDeductions obj in listDVOMasterEmployeeDeductions)
        //{
        //  obj.empl_code = o.ToString();
        //}
        BLLMasterEmployeeDeductions.InsertData(ref objTransaction, ref listDVOMasterEmployeeDeductions);

        parameters = null;
        objDALBaseClass = null;

        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    /// <summary>
    /// To Update Employee Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
    /// <returns></returns>
    public static int UpdateEmployeeDeductions(ref object objTransaction,
        ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions,
        string FlexDeptKeyvalue,
        ref List<DVOMasterEmployeeDeductions> listPreDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeDeductions> listDeleteDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeDeductions> listNewDVOMasterEmployeeDeductions)
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
        BLLMasterEmployeeDeductions.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmployeeDeductions);
        BLLMasterEmployeeDeductions.UpdateData(ref objTransaction, ref listDVOMasterEmployeeDeductions, ref listPreDVOMasterEmployeeDeductions);
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

    /// <summary>
    /// To Insert Employee Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
    /// <returns></returns>
    public static int InsertEmployeeInformation(ref DVOMasterEmployee objDVOEmployeeStyemplr,
        ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails,
        string FlexDeptKeyvalue,
        ref List<DVOstxnoted> listDVOstxnoted, out string EmpCode)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        #region parameters
        object[] parameters = new object[64];
        parameters[0] = objDVOEmployeeStyemplr.EmplCode;
        parameters[1] = objDVOEmployeeStyemplr.SocSecNum;
        parameters[2] = objDVOEmployeeStyemplr.TypeCode;
        if (objDVOEmployeeStyemplr.Birthdate == null || objDVOEmployeeStyemplr.Birthdate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.Birthdate = "01/01/1900";
        parameters[3] = objDVOEmployeeStyemplr.Birthdate;
        parameters[4] = objDVOEmployeeStyemplr.FirstName;
        parameters[5] = objDVOEmployeeStyemplr.MiddleName;
        parameters[6] = objDVOEmployeeStyemplr.LastName;
        parameters[7] = objDVOEmployeeStyemplr.Address1;
        parameters[8] = objDVOEmployeeStyemplr.Address2;
        parameters[9] = objDVOEmployeeStyemplr.City;
        parameters[10] = objDVOEmployeeStyemplr.State;
        parameters[11] = objDVOEmployeeStyemplr.Zip;
        parameters[12] = objDVOEmployeeStyemplr.Phone;
        parameters[13] = objDVOEmployeeStyemplr.CashAcct;
        parameters[14] = objDVOEmployeeStyemplr.Department;
        parameters[15] = objDVOEmployeeStyemplr.JobCode;
        parameters[16] = objDVOEmployeeStyemplr.JobTitle;
        if (objDVOEmployeeStyemplr.DateHired == null || objDVOEmployeeStyemplr.DateHired.Trim().Length <= 0)
          objDVOEmployeeStyemplr.DateHired = "01/01/1900";
        parameters[17] = objDVOEmployeeStyemplr.DateHired;
        if (objDVOEmployeeStyemplr.Terminated == null || objDVOEmployeeStyemplr.Terminated.Trim().Length <= 0)
          objDVOEmployeeStyemplr.Terminated = "01/01/1900";
        parameters[18] = objDVOEmployeeStyemplr.Terminated;
        parameters[19] = objDVOEmployeeStyemplr.EmplStatus;
        parameters[20] = objDVOEmployeeStyemplr.PayPeriod;
        parameters[21] = objDVOEmployeeStyemplr.Allowances;
        parameters[22] = objDVOEmployeeStyemplr.StateAllow;
        parameters[23] = objDVOEmployeeStyemplr.MaritalStat;
        parameters[24] = objDVOEmployeeStyemplr.VacCode;
        parameters[25] = objDVOEmployeeStyemplr.VacAllowed;
        parameters[26] = objDVOEmployeeStyemplr.VacUsed;
        parameters[27] = objDVOEmployeeStyemplr.SickCode;
        parameters[28] = objDVOEmployeeStyemplr.SickAllowed;
        parameters[29] = objDVOEmployeeStyemplr.SickUsed;
        if (objDVOEmployeeStyemplr.LastPay == null || objDVOEmployeeStyemplr.LastPay.Trim().Length <= 0)
          objDVOEmployeeStyemplr.LastPay = "01/01/1900";
        parameters[30] = objDVOEmployeeStyemplr.LastPay;
        parameters[31] = objDVOEmployeeStyemplr.HoldPayment;
        parameters[32] = objDVOEmployeeStyemplr.StaTaxCode;
        parameters[33] = objDVOEmployeeStyemplr.LocTaxCode;
        parameters[34] = objDVOEmployeeStyemplr.SickAccrCodr;
        parameters[35] = objDVOEmployeeStyemplr.SickAccrCtr;
        if (objDVOEmployeeStyemplr.SickLapseDate == null || objDVOEmployeeStyemplr.SickLapseDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.SickLapseDate = "01/01/1900";
        parameters[36] = objDVOEmployeeStyemplr.SickLapseDate;
        parameters[37] = objDVOEmployeeStyemplr.VacAccrCode;
        parameters[38] = objDVOEmployeeStyemplr.VacAccrCtr;
        if (objDVOEmployeeStyemplr.VacLapseDate == null || objDVOEmployeeStyemplr.VacLapseDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.VacLapseDate = "01/01/1900";
        parameters[39] = objDVOEmployeeStyemplr.VacLapseDate;
        parameters[40] = objDVOEmployeeStyemplr.DirDept;
        parameters[41] = objDVOEmployeeStyemplr.DfiDest;
        parameters[42] = objDVOEmployeeStyemplr.ChkDigit;
        parameters[43] = objDVOEmployeeStyemplr.BankAcctNo;
        parameters[44] = objDVOEmployeeStyemplr.StateUdf;
        parameters[45] = objDVOEmployeeStyemplr.FlexDeptAcctType;
        if (objDVOEmployeeStyemplr.LastIncDate == null || objDVOEmployeeStyemplr.LastIncDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.LastIncDate = "01/01/1900";
        parameters[46] = objDVOEmployeeStyemplr.LastIncDate;
        if (objDVOEmployeeStyemplr.AppointDate == null || objDVOEmployeeStyemplr.AppointDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.AppointDate = "01/01/1900";
        parameters[47] = objDVOEmployeeStyemplr.AppointDate;
        parameters[48] = objDVOEmployeeStyemplr.Gender;
        parameters[49] = objDVOEmployeeStyemplr.InsertMachineInfo;
        if (objDVOEmployeeStyemplr.InsertDate == null || objDVOEmployeeStyemplr.InsertDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.InsertDate = "01/01/1900";
        parameters[50] = objDVOEmployeeStyemplr.InsertDate;
        parameters[51] = objDVOEmployeeStyemplr.InsertBy;
        parameters[52] = objDVOEmployeeStyemplr.mailid;

        parameters[53] = objDVOEmployeeStyemplr.Prefix;
        parameters[54] = objDVOEmployeeStyemplr.Suffix;
        parameters[55] = objDVOEmployeeStyemplr.MaidenName;
        parameters[56] = objDVOEmployeeStyemplr.Nationality;
        parameters[57] = objDVOEmployeeStyemplr.PostalAddress;
        parameters[58] = objDVOEmployeeStyemplr.PhoneOffice;
        parameters[59] = objDVOEmployeeStyemplr.Mobile;
        parameters[60] = objDVOEmployeeStyemplr.EmplrCode;
        parameters[61] = objDVOEmployeeStyemplr.PensionerType;
        parameters[62] = objDVOEmployeeStyemplr.PersonID;
        parameters[63] = objDVOEmployeeStyemplr.AnnualSalary;

        #endregion parameters

        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmployeeStyemplr.INSERT_SPNAME);
        if (o == null)
          throw new Exception();
        else if (Convert.ToInt32(o) < 1)
          throw new Exception();
        EmpCode = o.ToString();

        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = o.ToString();
        objDVOFlexSegCommon.AccountType = objDVOEmployeeStyemplr.FlexDeptAcctType;
        objDVOFlexSegCommon.keyvalue = FlexDeptKeyvalue;
        BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);
        //throw new Exception();

        foreach (DVOMasterEmployeeIncomes obj in listDVOMasterEmployeeIncomes)
        {
          obj.empl_code = o.ToString();
        }
        foreach (DVOMasterEmployeeDeductions obj in listDVOMasterEmployeeDeductions)
        {
          obj.empl_code = o.ToString();
        }
        foreach (DVOMasterEmployeeObligations obj in listDVOMasterEmployeeObligations)
        {
          obj.empl_code = o.ToString();
        }
        foreach (DVOPREmployeePositionHistoryInyemppd obj in listDVOPREmployeePositionHistoryInyemppd)
        {
          obj.empl_code = o.ToString();
        }
        foreach (DVOMasterEmpBankDetails obj in listDVOMasterEmpBankDetails)
        {
          obj.empl_code = o.ToString();
        }


        BLLMasterEmployeeIncomes.InsertData(ref objTransaction, ref listDVOMasterEmployeeIncomes);
        BLLMasterEmployeeDeductions.InsertData(ref objTransaction, ref listDVOMasterEmployeeDeductions);
        BLLMasterEmployeeObligations.InsertData(ref objTransaction, ref listDVOMasterEmployeeObligations);
        BLLPREmployeePositionHistoryInyemppd.InsertData(ref objTransaction, ref listDVOPREmployeePositionHistoryInyemppd);
        BLLMasterEmpBankDetails.InsertData(ref objTransaction, ref listDVOMasterEmpBankDetails);
        //BLLScreenNotesStxnoted.InsertEmployeeNotes(ref objTransaction, ref listDVOstxnoted);
        BLLScreenNotesStxnoted.InsertCommonNotes(ref objTransaction, ref listDVOstxnoted);

        List<DVOEmpTypLogEmpsaltyplog> listDVOEmpTypLogEmpsaltyplog = new List<DVOEmpTypLogEmpsaltyplog>();
        using (DVOEmpTypLogEmpsaltyplog objtmp = new DVOEmpTypLogEmpsaltyplog())
        {
          objtmp.emp_code = o.ToString(); //objDVOEmployeeStyemplr.EmplCode;
          objtmp.type_code = objDVOEmployeeStyemplr.TypeCode;
          objtmp.date_assigned = objDVOEmployeeStyemplr.InsertDate;
          objtmp.update_by = objDVOEmployeeStyemplr.InsertBy;
          objtmp.update_date = objDVOEmployeeStyemplr.InsertDate;
          objtmp.update_machine = objDVOEmployeeStyemplr.InsertMachineInfo;
          listDVOEmpTypLogEmpsaltyplog.Add(objtmp);
        }
        //BLLEmpTypLogEmpsaltyplog.InsertEmployeeSalaryTypeLog(ref objTransaction, ref listDVOEmpTypLogEmpsaltyplog);

        parameters = null;
        objDALBaseClass = null;

        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return 1;
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    /// <summary>
    /// To Update Employee Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
    /// <returns></returns>
    public static int UpdateEmployeeInformation(ref object objTransaction, ref DVOMasterEmployee objDVOEmployeeStyemplr,
        ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails,
        string FlexDeptKeyvalue,
        ref DVOMasterEmployee objPreUpdDVOMasterEmployee,
        ref List<DVOMasterEmployeeIncomes> listPreDVOMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listPreDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listPreDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listPreDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listPreDVOMasterEmpBankDetails,
        ref List<DVOstxnoted> listDVOstxnoted,
        ref List<DVOMasterEmployeeIncomes> listDeleteDVOMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listDeleteDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listDeleteDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listDeleteDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listDeleteDVOMasterEmpBankDetails,
        ref List<DVOMasterEmployeeIncomes> listNewVOPREmployeeIncomeCodeAndRateDetailMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listNewDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listNewDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listNewDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listNewDVOMasterEmpBankDetails)
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
        List<DVOEmployeeInfoLogEmpUpdLog> listDVOEmployeeInfoLogEmpUpdLog = new List<DVOEmployeeInfoLogEmpUpdLog>();
        List<DVOEmpTypLogEmpsaltyplog> listDVOEmpTypLogEmpsaltyplog = new List<DVOEmpTypLogEmpsaltyplog>();

        #region Parameters
        object[] parameters = new object[94];

        parameters[0] = objDVOEmployeeStyemplr.EmplCode;
        if (objDVOEmployeeStyemplr.EmplCode.Trim() != objPreUpdDVOMasterEmployee.EmplCode.Trim())
        {
          using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
          {
            objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
            objtmp.field_name = "EmplCode";
            objtmp.old_value = objPreUpdDVOMasterEmployee.EmplCode;
            objtmp.new_value = objDVOEmployeeStyemplr.EmplCode;
            objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
            objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
            objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
            listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
          }
        }

        parameters[1] = objDVOEmployeeStyemplr.SocSecNum;
        if (objDVOEmployeeStyemplr.SocSecNum != null)
          if (objDVOEmployeeStyemplr.SocSecNum.Trim() != objPreUpdDVOMasterEmployee.SocSecNum.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SocSecNum";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SocSecNum;
              objtmp.new_value = objDVOEmployeeStyemplr.SocSecNum;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[2] = objDVOEmployeeStyemplr.TypeCode;
        if (objDVOEmployeeStyemplr.TypeCode != null)
          if (objDVOEmployeeStyemplr.TypeCode.Trim() != objPreUpdDVOMasterEmployee.TypeCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "TypeCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.TypeCode;
              objtmp.new_value = objDVOEmployeeStyemplr.TypeCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
            using (DVOEmpTypLogEmpsaltyplog objtmp = new DVOEmpTypLogEmpsaltyplog())
            {
              objtmp.emp_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.type_code = objDVOEmployeeStyemplr.TypeCode;
              objtmp.date_assigned = objDVOEmployeeStyemplr.UpdateDate;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              listDVOEmpTypLogEmpsaltyplog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.Birthdate != null)
        //    if (objDVOEmployeeStyemplr.Birthdate != string.Empty)
        //        objDVOEmployeeStyemplr.Birthdate = Convert.ToDateTime(objDVOEmployeeStyemplr.Birthdate).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        //if (objPreUpdDVOMasterEmployee.Birthdate != null)
        //    if (objPreUpdDVOMasterEmployee.Birthdate != string.Empty)
        //        objPreUpdDVOMasterEmployee.Birthdate = Convert.ToDateTime(objPreUpdDVOMasterEmployee.Birthdate).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objPreUpdDVOMasterEmployee.Birthdate == null || objPreUpdDVOMasterEmployee.Birthdate.Trim().Length <= 0)
          objPreUpdDVOMasterEmployee.Birthdate = "01/01/1900";
        if (objDVOEmployeeStyemplr.Birthdate == null || objDVOEmployeeStyemplr.Birthdate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.Birthdate = "01/01/1900";
        parameters[3] = DateTime.ParseExact(objDVOEmployeeStyemplr.Birthdate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        if (objDVOEmployeeStyemplr.Birthdate != null)
          if (objDVOEmployeeStyemplr.Birthdate != objPreUpdDVOMasterEmployee.Birthdate)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Birthdate";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Birthdate;
              objtmp.new_value = objDVOEmployeeStyemplr.Birthdate;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[4] = objDVOEmployeeStyemplr.FirstName;
        if (objDVOEmployeeStyemplr.FirstName != null)
          if (objDVOEmployeeStyemplr.FirstName.Trim() != objPreUpdDVOMasterEmployee.FirstName.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "FirstName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.FirstName;
              objtmp.new_value = objDVOEmployeeStyemplr.FirstName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[5] = objDVOEmployeeStyemplr.MiddleName;
        if (objDVOEmployeeStyemplr.MiddleName != null)
          if (objDVOEmployeeStyemplr.MiddleName.Trim() != objPreUpdDVOMasterEmployee.MiddleName.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "MiddleName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.MiddleName;
              objtmp.new_value = objDVOEmployeeStyemplr.MiddleName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[6] = objDVOEmployeeStyemplr.LastName;
        if (objDVOEmployeeStyemplr.LastName != null)
          if (objDVOEmployeeStyemplr.LastName.Trim() != objPreUpdDVOMasterEmployee.LastName.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "LastName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.LastName;
              objtmp.new_value = objDVOEmployeeStyemplr.LastName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[7] = objDVOEmployeeStyemplr.Address1;
        if (objDVOEmployeeStyemplr.Address1 != null)
          if (objDVOEmployeeStyemplr.Address1.Trim() != objPreUpdDVOMasterEmployee.Address1.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Address1";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Address1;
              objtmp.new_value = objDVOEmployeeStyemplr.Address1;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[8] = objDVOEmployeeStyemplr.Address2;
        if (objDVOEmployeeStyemplr.Address2 != null)
          if (objDVOEmployeeStyemplr.Address2.Trim() != objPreUpdDVOMasterEmployee.Address2.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Address2";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Address2;
              objtmp.new_value = objDVOEmployeeStyemplr.Address2;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[9] = objDVOEmployeeStyemplr.City;
        if (objDVOEmployeeStyemplr.City != null)
          if (objDVOEmployeeStyemplr.City.Trim() != objPreUpdDVOMasterEmployee.City.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "City";
              objtmp.old_value = objPreUpdDVOMasterEmployee.City;
              objtmp.new_value = objDVOEmployeeStyemplr.City;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[10] = objDVOEmployeeStyemplr.State;
        if (objDVOEmployeeStyemplr.State != null)
          if (objDVOEmployeeStyemplr.State.Trim() != objPreUpdDVOMasterEmployee.State.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "State";
              objtmp.old_value = objPreUpdDVOMasterEmployee.State;
              objtmp.new_value = objDVOEmployeeStyemplr.State;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[11] = objDVOEmployeeStyemplr.Zip;
        if (objDVOEmployeeStyemplr.Zip != null)
          if (objDVOEmployeeStyemplr.Zip.Trim() != objPreUpdDVOMasterEmployee.Pincode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Zip";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Pincode;
              objtmp.new_value = objDVOEmployeeStyemplr.Zip;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[12] = objDVOEmployeeStyemplr.Phone;
        if (objDVOEmployeeStyemplr.Phone != null)
          if (objDVOEmployeeStyemplr.Phone.Trim() != objPreUpdDVOMasterEmployee.Phone.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Phone";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Phone;
              objtmp.new_value = objDVOEmployeeStyemplr.Phone;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[13] = objDVOEmployeeStyemplr.CashAcct;
        if (objDVOEmployeeStyemplr.CashAcct != null)
          if (objDVOEmployeeStyemplr.CashAcct != objPreUpdDVOMasterEmployee.CashAcct)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "CashAcct";
              objtmp.old_value = objPreUpdDVOMasterEmployee.CashAcct.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.CashAcct.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[14] = objDVOEmployeeStyemplr.Department;
        if (objDVOEmployeeStyemplr.Department != null)
          if (objDVOEmployeeStyemplr.Department.Trim() != objPreUpdDVOMasterEmployee.Department.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Department";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Department;
              objtmp.new_value = objDVOEmployeeStyemplr.Department;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[15] = objDVOEmployeeStyemplr.JobCode;
        //if (objDVOEmployeeStyemplr.JobCode.Trim() != objPreUpdDVOMasterEmployee.JobCode.Trim())
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "JobCode";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.JobCode;
        //        objtmp.new_value = objDVOEmployeeStyemplr.JobCode;
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[16] = objDVOEmployeeStyemplr.JobTitle;
        //if (objDVOEmployeeStyemplr.JobTitle.Trim() != objPreUpdDVOMasterEmployee.JobTitle.Trim())
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "JobTitle";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.JobTitle;
        //        objtmp.new_value = objDVOEmployeeStyemplr.JobTitle;
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        //if (objDVOEmployeeStyemplr.DateHired != null)
        //    if (objDVOEmployeeStyemplr.DateHired != string.Empty)
        //        objDVOEmployeeStyemplr.DateHired = Convert.ToDateTime(objDVOEmployeeStyemplr.DateHired).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        //if (objPreUpdDVOMasterEmployee.DateHired != null)
        //    if (objPreUpdDVOMasterEmployee.DateHired != string.Empty)
        //        objPreUpdDVOMasterEmployee.DateHired = Convert.ToDateTime(objPreUpdDVOMasterEmployee.DateHired).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOEmployeeStyemplr.DateHired != null)
        {
          if (objPreUpdDVOMasterEmployee.DateHired == null || objPreUpdDVOMasterEmployee.DateHired.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.DateHired = "01/01/1900";
          if (objDVOEmployeeStyemplr.DateHired == null || objDVOEmployeeStyemplr.DateHired.Trim().Length <= 0)
            objDVOEmployeeStyemplr.DateHired = "01/01/1900";
        }
        parameters[17] = DateTime.ParseExact(objDVOEmployeeStyemplr.DateHired, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.DateHired != null)
          if (objDVOEmployeeStyemplr.DateHired != objPreUpdDVOMasterEmployee.DateHired)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "DateHired";
              objtmp.old_value = objPreUpdDVOMasterEmployee.DateHired;
              objtmp.new_value = objDVOEmployeeStyemplr.DateHired;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.Terminated != null)
        //    if (objDVOEmployeeStyemplr.Terminated != string.Empty)
        //        objDVOEmployeeStyemplr.Terminated = Convert.ToDateTime(objDVOEmployeeStyemplr.Terminated).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.Terminated != null)
        //    if (objPreUpdDVOMasterEmployee.Terminated != string.Empty)
        //        objPreUpdDVOMasterEmployee.Terminated = Convert.ToDateTime(objPreUpdDVOMasterEmployee.Terminated).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.DateHired != null)
        {
          if (objPreUpdDVOMasterEmployee.Terminated == null || objPreUpdDVOMasterEmployee.Terminated.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.Terminated = "01/01/1900";
          if (objDVOEmployeeStyemplr.Terminated == null || objDVOEmployeeStyemplr.Terminated.Trim().Length <= 0)
            objDVOEmployeeStyemplr.Terminated = "01/01/1900";
        }
        parameters[18] = DateTime.ParseExact(objDVOEmployeeStyemplr.Terminated, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.Terminated != null)
          if (objDVOEmployeeStyemplr.Terminated != objPreUpdDVOMasterEmployee.Terminated)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Terminated";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Terminated;
              objtmp.new_value = objDVOEmployeeStyemplr.Terminated;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[19] = objDVOEmployeeStyemplr.EmplStatus;
        if (objDVOEmployeeStyemplr.EmplStatus != null)
          if (objDVOEmployeeStyemplr.EmplStatus.Trim() != objPreUpdDVOMasterEmployee.EmplStatus.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "EmplStatus";
              objtmp.old_value = objPreUpdDVOMasterEmployee.EmplStatus;
              objtmp.new_value = objDVOEmployeeStyemplr.EmplStatus;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[20] = objDVOEmployeeStyemplr.PayPeriod;
        if (objDVOEmployeeStyemplr.PayPeriod != null)
          if (objDVOEmployeeStyemplr.PayPeriod.Trim() != objPreUpdDVOMasterEmployee.PayPeriod.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PayPeriod";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PayPeriod;
              objtmp.new_value = objDVOEmployeeStyemplr.PayPeriod;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[21] = objDVOEmployeeStyemplr.Allowances;
        if (objDVOEmployeeStyemplr.Allowances != null)
          if (objDVOEmployeeStyemplr.Allowances != objPreUpdDVOMasterEmployee.Allowances)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Allowances";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Allowances.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.Allowances.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[22] = objDVOEmployeeStyemplr.StateAllow;
        if (objDVOEmployeeStyemplr.StateAllow != null)
          if (objDVOEmployeeStyemplr.StateAllow != objPreUpdDVOMasterEmployee.StateAllow)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "StateAllow";
              objtmp.old_value = objPreUpdDVOMasterEmployee.StateAllow.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.StateAllow.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[23] = objDVOEmployeeStyemplr.MaritalStat;
        if (objDVOEmployeeStyemplr.MaritalStat != null)
          if (objDVOEmployeeStyemplr.MaritalStat.Trim() != objPreUpdDVOMasterEmployee.MaritalStat.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "MaritalStat";
              objtmp.old_value = objPreUpdDVOMasterEmployee.MaritalStat;
              objtmp.new_value = objDVOEmployeeStyemplr.MaritalStat;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[24] = objDVOEmployeeStyemplr.VacCode;
        if (objDVOEmployeeStyemplr.VacCode != null)
          if (objDVOEmployeeStyemplr.VacCode.Trim() != objPreUpdDVOMasterEmployee.VacCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VacCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VacCode;
              objtmp.new_value = objDVOEmployeeStyemplr.VacCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[25] = objDVOEmployeeStyemplr.VacAllowed;
        if (objDVOEmployeeStyemplr.VacAllowed != null)
          if (objDVOEmployeeStyemplr.VacAllowed != objPreUpdDVOMasterEmployee.VacAllowed)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VacAllowed";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VacAllowed.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.VacAllowed.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[26] = objDVOEmployeeStyemplr.VacUsed;
        if (objDVOEmployeeStyemplr.VacUsed != null)
          if (objDVOEmployeeStyemplr.VacUsed != objPreUpdDVOMasterEmployee.VacUsed)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VacUsed";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VacUsed.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.VacUsed.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[27] = objDVOEmployeeStyemplr.SickCode;
        if (objDVOEmployeeStyemplr.SickCode != null)
          if (objDVOEmployeeStyemplr.SickCode.Trim() != objPreUpdDVOMasterEmployee.SickCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickCode;
              objtmp.new_value = objDVOEmployeeStyemplr.SickCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[28] = objDVOEmployeeStyemplr.SickAllowed;
        if (objDVOEmployeeStyemplr.SickAllowed != null)
          if (objDVOEmployeeStyemplr.SickAllowed != objPreUpdDVOMasterEmployee.SickAllowed)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickAllowed";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickAllowed.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.SickAllowed.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[29] = objDVOEmployeeStyemplr.SickUsed;
        if (objDVOEmployeeStyemplr.SickUsed != null)
          if (objDVOEmployeeStyemplr.SickUsed != objPreUpdDVOMasterEmployee.SickUsed)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickUsed";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickUsed.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.SickUsed.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.LastPay != null)
        //    if (objDVOEmployeeStyemplr.LastPay != string.Empty)
        //        objDVOEmployeeStyemplr.LastPay = Convert.ToDateTime(objDVOEmployeeStyemplr.LastPay).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.LastPay != null)
        //    if (objPreUpdDVOMasterEmployee.LastPay != string.Empty)
        //        objPreUpdDVOMasterEmployee.LastPay = Convert.ToDateTime(objPreUpdDVOMasterEmployee.LastPay).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.LastPay != null)
        {
          if (objPreUpdDVOMasterEmployee.LastPay == null || objPreUpdDVOMasterEmployee.LastPay.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.LastPay = "01/01/1900";
          if (objDVOEmployeeStyemplr.LastPay == null || objDVOEmployeeStyemplr.LastPay.Trim().Length <= 0)
            objDVOEmployeeStyemplr.LastPay = "01/01/1900";
        }
        parameters[30] = DateTime.ParseExact(objDVOEmployeeStyemplr.LastPay, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.LastPay != null)
          if (objDVOEmployeeStyemplr.LastPay.Trim() != objPreUpdDVOMasterEmployee.LastPay.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "LastPay";
              objtmp.old_value = objPreUpdDVOMasterEmployee.LastPay;
              objtmp.new_value = objDVOEmployeeStyemplr.LastPay;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[31] = objDVOEmployeeStyemplr.HoldPayment;
        if (objDVOEmployeeStyemplr.HoldPayment != null)
          if (objDVOEmployeeStyemplr.HoldPayment.Trim() != objPreUpdDVOMasterEmployee.HoldPayment.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "HoldPayment";
              objtmp.old_value = objPreUpdDVOMasterEmployee.HoldPayment;
              objtmp.new_value = objDVOEmployeeStyemplr.HoldPayment;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[32] = objDVOEmployeeStyemplr.StaTaxCode;
        if (objDVOEmployeeStyemplr.StaTaxCode != null)
          if (objDVOEmployeeStyemplr.StaTaxCode.Trim() != objPreUpdDVOMasterEmployee.StaTaxCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "StaTaxCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.StaTaxCode;
              objtmp.new_value = objDVOEmployeeStyemplr.StaTaxCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[33] = objDVOEmployeeStyemplr.LocTaxCode;
        if (objDVOEmployeeStyemplr.LocTaxCode != null)
          if (objDVOEmployeeStyemplr.LocTaxCode.Trim() != objPreUpdDVOMasterEmployee.LocTaxCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "LocTaxCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.LocTaxCode;
              objtmp.new_value = objDVOEmployeeStyemplr.LocTaxCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[34] = objDVOEmployeeStyemplr.SickAccrCodr;
        if (objDVOEmployeeStyemplr.SickAccrCodr != null)
          if (objDVOEmployeeStyemplr.SickAccrCodr.Trim() != objPreUpdDVOMasterEmployee.SickAccrCodr.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickAccrCodr";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickAccrCodr;
              objtmp.new_value = objDVOEmployeeStyemplr.SickAccrCodr;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[35] = objDVOEmployeeStyemplr.SickAccrCtr;
        if (objDVOEmployeeStyemplr.SickAccrCtr != null)
          if (objDVOEmployeeStyemplr.SickAccrCtr != objPreUpdDVOMasterEmployee.SickAccrCtr)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickAccrCtr";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickAccrCtr.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.SickAccrCtr.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.SickLapseDate != null)
        //    if (objDVOEmployeeStyemplr.SickLapseDate != string.Empty)
        //        objDVOEmployeeStyemplr.SickLapseDate = Convert.ToDateTime(objDVOEmployeeStyemplr.SickLapseDate).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.SickLapseDate != null)
        //    if (objPreUpdDVOMasterEmployee.SickLapseDate != string.Empty)
        //        objPreUpdDVOMasterEmployee.SickLapseDate = Convert.ToDateTime(objPreUpdDVOMasterEmployee.SickLapseDate).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.SickLapseDate != null)
        {
          if (objPreUpdDVOMasterEmployee.SickLapseDate == null || objPreUpdDVOMasterEmployee.SickLapseDate.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.SickLapseDate = "01/01/1900";
          if (objDVOEmployeeStyemplr.SickLapseDate == null || objDVOEmployeeStyemplr.SickLapseDate.Trim().Length <= 0)
            objDVOEmployeeStyemplr.SickLapseDate = "01/01/1900";
        }
        parameters[36] = DateTime.ParseExact(objDVOEmployeeStyemplr.SickLapseDate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.SickLapseDate != null)
          if (objDVOEmployeeStyemplr.SickLapseDate.Trim() != objPreUpdDVOMasterEmployee.SickLapseDate.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SickLapseDate";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SickLapseDate;
              objtmp.new_value = objDVOEmployeeStyemplr.SickLapseDate;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[37] = objDVOEmployeeStyemplr.VacAccrCode;
        if (objDVOEmployeeStyemplr.VacAccrCode != null)
          if (objDVOEmployeeStyemplr.VacAccrCode.Trim() != objPreUpdDVOMasterEmployee.VacAccrCode.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VacAccrCode";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VacAccrCode;
              objtmp.new_value = objDVOEmployeeStyemplr.VacAccrCode;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[38] = objDVOEmployeeStyemplr.VacAccrCtr;
        if (objDVOEmployeeStyemplr.VacAccrCtr != null)
          if (objDVOEmployeeStyemplr.VacAccrCtr != objPreUpdDVOMasterEmployee.VacAccrCtr)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VacAccrCtr";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VacAccrCtr.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.VacAccrCtr.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.VacLapseDate != null)
        //    if (objDVOEmployeeStyemplr.VacLapseDate != string.Empty)
        //        objDVOEmployeeStyemplr.VacLapseDate = Convert.ToDateTime(objDVOEmployeeStyemplr.VacLapseDate).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.VacLapseDate != null)
        //    if (objPreUpdDVOMasterEmployee.VacLapseDate != string.Empty)
        //        objPreUpdDVOMasterEmployee.VacLapseDate = Convert.ToDateTime(objPreUpdDVOMasterEmployee.VacLapseDate).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.VacLapseDate != null)
        {
          if (objPreUpdDVOMasterEmployee.VacLapseDate == null || objPreUpdDVOMasterEmployee.VacLapseDate.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.VacLapseDate = "01/01/1900";
          if (objDVOEmployeeStyemplr.VacLapseDate == null || objDVOEmployeeStyemplr.VacLapseDate.Trim().Length <= 0)
            objDVOEmployeeStyemplr.VacLapseDate = "01/01/1900";
        }
        parameters[39] = DateTime.ParseExact(objDVOEmployeeStyemplr.VacLapseDate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        //if (objDVOEmployeeStyemplr.VacLapseDate.Trim() != objPreUpdDVOMasterEmployee.VacLapseDate.Trim())
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "VacLapseDate";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.VacLapseDate;
        //        objtmp.new_value = objDVOEmployeeStyemplr.VacLapseDate;
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[40] = objDVOEmployeeStyemplr.DirDept;
        if (objDVOEmployeeStyemplr.DirDept != null)
          if (objDVOEmployeeStyemplr.DirDept.Trim() != objPreUpdDVOMasterEmployee.DirDept.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "DirDept";
              objtmp.old_value = objPreUpdDVOMasterEmployee.DirDept;
              objtmp.new_value = objDVOEmployeeStyemplr.DirDept;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[41] = objDVOEmployeeStyemplr.DfiDest;
        //if (objDVOEmployeeStyemplr.DfiDest != objPreUpdDVOMasterEmployee.DfiDest)
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "DfiDest";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.DfiDest.ToString();
        //        objtmp.new_value = objDVOEmployeeStyemplr.DfiDest.ToString();
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[42] = objDVOEmployeeStyemplr.ChkDigit;
        //if (objDVOEmployeeStyemplr.ChkDigit != objPreUpdDVOMasterEmployee.ChkDigit)
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "ChkDigit";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.ChkDigit.ToString();
        //        objtmp.new_value = objDVOEmployeeStyemplr.ChkDigit.ToString();
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[43] = objDVOEmployeeStyemplr.BankAcctNo;
        //if (objDVOEmployeeStyemplr.BankAcctNo != objPreUpdDVOMasterEmployee.BankAcctNo)
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "BankAcctNo";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.BankAcctNo;
        //        objtmp.new_value = objDVOEmployeeStyemplr.BankAcctNo;
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[44] = objDVOEmployeeStyemplr.StateUdf;
        //if (objDVOEmployeeStyemplr.StateUdf != objPreUpdDVOMasterEmployee.StateUdf)
        //{
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //        objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //        objtmp.field_name = "StateUdf";
        //        objtmp.old_value = objPreUpdDVOMasterEmployee.StateUdf.ToString();
        //        objtmp.new_value = objDVOEmployeeStyemplr.StateUdf.ToString();
        //        objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //        objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //        objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //}

        parameters[45] = objDVOEmployeeStyemplr.FlexDeptAcctType;
        if (objDVOEmployeeStyemplr.FlexDeptAcctType != null)
          if (objDVOEmployeeStyemplr.FlexDeptAcctType.Trim() != objPreUpdDVOMasterEmployee.FlexDeptAcctType.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "FlexDeptAcctType";
              objtmp.old_value = objPreUpdDVOMasterEmployee.FlexDeptAcctType;
              objtmp.new_value = objDVOEmployeeStyemplr.FlexDeptAcctType;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.LastIncDate != null)
        //    if (objDVOEmployeeStyemplr.LastIncDate != string.Empty)
        //        objDVOEmployeeStyemplr.LastIncDate = Convert.ToDateTime(objDVOEmployeeStyemplr.LastIncDate).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.LastIncDate != null)
        //    if (objPreUpdDVOMasterEmployee.LastIncDate != string.Empty)
        //        objPreUpdDVOMasterEmployee.LastIncDate = Convert.ToDateTime(objPreUpdDVOMasterEmployee.LastIncDate).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.LastIncDate != null)
        {
          if (objPreUpdDVOMasterEmployee.LastIncDate == null || objPreUpdDVOMasterEmployee.LastIncDate.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.LastIncDate = "01/01/1900";
          if (objDVOEmployeeStyemplr.LastIncDate == null || objDVOEmployeeStyemplr.LastIncDate.Trim().Length <= 0)
            objDVOEmployeeStyemplr.LastIncDate = "01/01/1900";
        }
        parameters[46] = DateTime.ParseExact(objDVOEmployeeStyemplr.LastIncDate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.LastIncDate != null)
          if (objDVOEmployeeStyemplr.LastIncDate.Trim() != objPreUpdDVOMasterEmployee.LastIncDate.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "LastIncDate";
              objtmp.old_value = objPreUpdDVOMasterEmployee.LastIncDate;
              objtmp.new_value = objDVOEmployeeStyemplr.LastIncDate;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        //if (objDVOEmployeeStyemplr.AppointDate != null)
        //    if (objDVOEmployeeStyemplr.AppointDate != string.Empty)
        //        objDVOEmployeeStyemplr.AppointDate = Convert.ToDateTime(objDVOEmployeeStyemplr.AppointDate).ToString("MM/dd/yyyy");
        //if (objPreUpdDVOMasterEmployee.AppointDate != null)
        //    if (objPreUpdDVOMasterEmployee.AppointDate != string.Empty)
        //        objPreUpdDVOMasterEmployee.AppointDate = Convert.ToDateTime(objPreUpdDVOMasterEmployee.AppointDate).ToString("MM/dd/yyyy");
        if (objDVOEmployeeStyemplr.AppointDate != null)
        {
          if (objPreUpdDVOMasterEmployee.AppointDate == null || objPreUpdDVOMasterEmployee.AppointDate.Trim().Length <= 0)
            objPreUpdDVOMasterEmployee.AppointDate = "01/01/1900";
          if (objDVOEmployeeStyemplr.AppointDate == null || objDVOEmployeeStyemplr.AppointDate.Trim().Length <= 0)
            objDVOEmployeeStyemplr.AppointDate = "01/01/1900";
        }
        parameters[47] = DateTime.ParseExact(objDVOEmployeeStyemplr.AppointDate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        if (objDVOEmployeeStyemplr.AppointDate != null)
          if (objDVOEmployeeStyemplr.AppointDate.Trim() != objPreUpdDVOMasterEmployee.AppointDate.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "AppointDate";
              objtmp.old_value = objPreUpdDVOMasterEmployee.AppointDate;
              objtmp.new_value = objDVOEmployeeStyemplr.AppointDate;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[48] = objDVOEmployeeStyemplr.Gender;
        if (objDVOEmployeeStyemplr.Gender != null)
          if (objDVOEmployeeStyemplr.Gender.Trim() != objPreUpdDVOMasterEmployee.Gender.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Gender";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Gender;
              objtmp.new_value = objDVOEmployeeStyemplr.Gender;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[49] = objDVOEmployeeStyemplr.UpdateMachineInfo;
        if (objDVOEmployeeStyemplr.UpdateDate == null || objDVOEmployeeStyemplr.UpdateDate.Trim().Length <= 0)
          objDVOEmployeeStyemplr.UpdateDate = "01/01/1900";
        parameters[50] = objDVOEmployeeStyemplr.UpdateDate;
        parameters[51] = objDVOEmployeeStyemplr.UpdateBy;
        parameters[52] = objDVOEmployeeStyemplr.mailid;
        if (objDVOEmployeeStyemplr.mailid != null)
          if (objDVOEmployeeStyemplr.mailid.Trim() != objPreUpdDVOMasterEmployee.mailid.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "mailid";
              objtmp.old_value = objPreUpdDVOMasterEmployee.mailid;
              objtmp.new_value = objDVOEmployeeStyemplr.mailid;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[53] = objDVOEmployeeStyemplr.Prefix;
        if (objDVOEmployeeStyemplr.Prefix != null)
          if (objDVOEmployeeStyemplr.Prefix.Trim() != objPreUpdDVOMasterEmployee.Prefix.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Prefix";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Prefix;
              objtmp.new_value = objDVOEmployeeStyemplr.Prefix;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[54] = objDVOEmployeeStyemplr.Suffix;
        if (objDVOEmployeeStyemplr.Suffix != null)
          if (objDVOEmployeeStyemplr.Suffix.Trim() != objPreUpdDVOMasterEmployee.Suffix.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Suffix";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Suffix;
              objtmp.new_value = objDVOEmployeeStyemplr.Suffix;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[55] = objDVOEmployeeStyemplr.MaidenName;
        if (objDVOEmployeeStyemplr.MaidenName != null)
          if (objDVOEmployeeStyemplr.MaidenName.Trim() != objPreUpdDVOMasterEmployee.MaidenName.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "MaidenName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.MaidenName;
              objtmp.new_value = objDVOEmployeeStyemplr.MaidenName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[56] = objDVOEmployeeStyemplr.Nationality;
        if (objDVOEmployeeStyemplr.Nationality != null)
          if (objDVOEmployeeStyemplr.Nationality.Trim() != objPreUpdDVOMasterEmployee.Nationality.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Nationality";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Nationality;
              objtmp.new_value = objDVOEmployeeStyemplr.Nationality;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[57] = objDVOEmployeeStyemplr.PostalAddress;
        if (objDVOEmployeeStyemplr.PostalAddress != null)
          if (objDVOEmployeeStyemplr.PostalAddress.Trim() != objPreUpdDVOMasterEmployee.PostalAddress.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PostalAddress";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PostalAddress;
              objtmp.new_value = objDVOEmployeeStyemplr.PostalAddress;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[58] = objDVOEmployeeStyemplr.PhoneOffice;
        if (objDVOEmployeeStyemplr.PhoneOffice != null)
          if (objDVOEmployeeStyemplr.PhoneOffice.Trim() != objPreUpdDVOMasterEmployee.PhoneOffice.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PhoneOffice";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PhoneOffice;
              objtmp.new_value = objDVOEmployeeStyemplr.PhoneOffice;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[59] = objDVOEmployeeStyemplr.Mobile;
        if (objDVOEmployeeStyemplr.Mobile != null)
          if (objDVOEmployeeStyemplr.Mobile.Trim() != objPreUpdDVOMasterEmployee.Mobile.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Mobile";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Mobile;
              objtmp.new_value = objDVOEmployeeStyemplr.Mobile;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[60] = objDVOEmployeeStyemplr.EmplrCode;
        if (objDVOEmployeeStyemplr.EmplrCode != null)
          if (objDVOEmployeeStyemplr.EmplrCode != objPreUpdDVOMasterEmployee.EmplrCode)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "EmployerID";
              objtmp.old_value = objPreUpdDVOMasterEmployee.EmplrCode == null ? null : objPreUpdDVOMasterEmployee.EmplrCode.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.EmplrCode == null ? null : objDVOEmployeeStyemplr.EmplrCode.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[61] = objDVOEmployeeStyemplr.PensionerType;
        if (objDVOEmployeeStyemplr.PensionerType != null)
          if (objDVOEmployeeStyemplr.PensionerType.Trim() != objPreUpdDVOMasterEmployee.PensionerType.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PensionerType";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PensionerType;
              objtmp.new_value = objDVOEmployeeStyemplr.PensionerType;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[62] = objDVOEmployeeStyemplr.PersonID;
        //if (objDVOEmployeeStyemplr.PersonID != null)
        //if (objDVOEmployeeStyemplr.PersonID.Trim() != objPreUpdDVOMasterEmployee.PersonID.Trim())
        //{
        //  using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //  {
        //    objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //    objtmp.field_name = "PersonID";
        //    objtmp.old_value = objPreUpdDVOMasterEmployee.PersonID;
        //    objtmp.new_value = objDVOEmployeeStyemplr.PersonID;
        //    objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //    objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //    objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //    listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //  }
        //}
        parameters[63] = objDVOEmployeeStyemplr.AnnualSalary;
        if (objDVOEmployeeStyemplr.AnnualSalary != null)
          if (objDVOEmployeeStyemplr.AnnualSalary != objPreUpdDVOMasterEmployee.AnnualSalary)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "AnnualSalary";
              objtmp.old_value = objPreUpdDVOMasterEmployee.AnnualSalary.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.AnnualSalary.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[64] = objDVOEmployeeStyemplr.PercentageofDisability;
        if (objDVOEmployeeStyemplr.PercentageofDisability != null)
          if (objDVOEmployeeStyemplr.PercentageofDisability.Trim() != objPreUpdDVOMasterEmployee.PercentageofDisability.Trim())
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PercentageofDisability";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PercentageofDisability;
              objtmp.new_value = objDVOEmployeeStyemplr.PercentageofDisability;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[65] = objDVOEmployeeStyemplr.PresentHalqaPanchayatMunicipalityName;
        if (objDVOEmployeeStyemplr.PresentHalqaPanchayatMunicipalityName != null)
          if (objDVOEmployeeStyemplr.PresentHalqaPanchayatMunicipalityName != objPreUpdDVOMasterEmployee.PresentHalqaPanchayatMunicipalityName)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PresentHalqaPanchayatMunicipalityName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PresentHalqaPanchayatMunicipalityName;
              objtmp.new_value = objDVOEmployeeStyemplr.PresentHalqaPanchayatMunicipalityName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[66] = objDVOEmployeeStyemplr.ApplicationReferenceNo;

        if (objDVOEmployeeStyemplr.ApplicationReferenceNo != null)
          if (objDVOEmployeeStyemplr.ApplicationReferenceNo != objPreUpdDVOMasterEmployee.ApplicationReferenceNo)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "ApplicationReferenceNo";
              objtmp.old_value = objPreUpdDVOMasterEmployee.ApplicationReferenceNo;
              objtmp.new_value = objDVOEmployeeStyemplr.ApplicationReferenceNo;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[67] = objDVOEmployeeStyemplr.EMail;
        if (objDVOEmployeeStyemplr.EMail != null)
          if (objDVOEmployeeStyemplr.EMail != objPreUpdDVOMasterEmployee.EMail)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "EMail";
              objtmp.old_value = objPreUpdDVOMasterEmployee.EMail;
              objtmp.new_value = objDVOEmployeeStyemplr.EMail;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[68] = objDVOEmployeeStyemplr.PresentTehsil;
        if (objDVOEmployeeStyemplr.PresentTehsil != null)
          if (objDVOEmployeeStyemplr.PresentTehsil != objPreUpdDVOMasterEmployee.PresentTehsil)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PresentTehsil";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PresentTehsil;
              objtmp.new_value = objDVOEmployeeStyemplr.PresentTehsil;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[69] = objDVOEmployeeStyemplr.PermanentDistrict;
        if (objDVOEmployeeStyemplr.PermanentDistrict != null)
          if (objDVOEmployeeStyemplr.PermanentDistrict != objPreUpdDVOMasterEmployee.PermanentDistrict)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PermanentDistrict";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PermanentDistrict;
              objtmp.new_value = objDVOEmployeeStyemplr.PermanentDistrict;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[70] = objDVOEmployeeStyemplr.PermanentTehsil;
        if (objDVOEmployeeStyemplr.PermanentTehsil != null)
          if (objDVOEmployeeStyemplr.PermanentTehsil != objPreUpdDVOMasterEmployee.PermanentTehsil)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PermanentTehsil";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PermanentTehsil;
              objtmp.new_value = objDVOEmployeeStyemplr.PermanentTehsil;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[71] = objDVOEmployeeStyemplr.PermanentVillageName;
        if (objDVOEmployeeStyemplr.PermanentVillageName != null)
          if (objDVOEmployeeStyemplr.PermanentVillageName != objPreUpdDVOMasterEmployee.PermanentVillageName)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PermanentVillageName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PermanentVillageName;
              objtmp.new_value = objDVOEmployeeStyemplr.PermanentVillageName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[72] = objDVOEmployeeStyemplr.ApplicationSanctionedunderSchemeName;
        if (objDVOEmployeeStyemplr.ApplicationSanctionedunderSchemeName != null)
          if (objDVOEmployeeStyemplr.ApplicationSanctionedunderSchemeName != objPreUpdDVOMasterEmployee.ApplicationSanctionedunderSchemeName)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "ApplicationSanctionedunderSchemeName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.ApplicationSanctionedunderSchemeName;
              objtmp.new_value = objDVOEmployeeStyemplr.ApplicationSanctionedunderSchemeName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[73] = objDVOEmployeeStyemplr.CurrentTask;
        if (objDVOEmployeeStyemplr.CurrentTask != null)
          if (objDVOEmployeeStyemplr.CurrentTask != objPreUpdDVOMasterEmployee.CurrentTask)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "CurrentTask";
              objtmp.old_value = objPreUpdDVOMasterEmployee.CurrentTask;
              objtmp.new_value = objDVOEmployeeStyemplr.CurrentTask;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[74] = objDVOEmployeeStyemplr.CurrentStatus;
        if (objDVOEmployeeStyemplr.CurrentStatus != null)
          if (objDVOEmployeeStyemplr.CurrentStatus != objPreUpdDVOMasterEmployee.CurrentStatus)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "CurrentStatus";
              objtmp.old_value = objPreUpdDVOMasterEmployee.CurrentStatus;
              objtmp.new_value = objDVOEmployeeStyemplr.CurrentStatus;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[75] = objDVOEmployeeStyemplr.DoyouhaveBPLcard;
        if (objDVOEmployeeStyemplr.DoyouhaveBPLcard != null)
          if (objDVOEmployeeStyemplr.DoyouhaveBPLcard != objPreUpdDVOMasterEmployee.DoyouhaveBPLcard && (objPreUpdDVOMasterEmployee.DoyouhaveBPLcard != "" || objDVOEmployeeStyemplr.DoyouhaveBPLcard.ToLower() == "yes"))
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "DoyouhaveBPLcard";
              objtmp.old_value = objPreUpdDVOMasterEmployee.DoyouhaveBPLcard == null || objPreUpdDVOMasterEmployee.DoyouhaveBPLcard == "" ? "NO" : objPreUpdDVOMasterEmployee.DoyouhaveBPLcard;
              objtmp.new_value = objDVOEmployeeStyemplr.DoyouhaveBPLcard;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[76] = objDVOEmployeeStyemplr.JKISSS;
        if (objDVOEmployeeStyemplr.JKISSS != null)
          if (objDVOEmployeeStyemplr.JKISSS != objPreUpdDVOMasterEmployee.JKISSS)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "JKISSS";
              objtmp.old_value = objPreUpdDVOMasterEmployee.JKISSS;
              objtmp.new_value = objDVOEmployeeStyemplr.JKISSS;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[77] = objDVOEmployeeStyemplr.CivilCondition;
        if (objDVOEmployeeStyemplr.CivilCondition != null)
          if (objDVOEmployeeStyemplr.CivilCondition != objPreUpdDVOMasterEmployee.CivilCondition)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "CivilCondition";
              objtmp.old_value = objPreUpdDVOMasterEmployee.CivilCondition;
              objtmp.new_value = objDVOEmployeeStyemplr.CivilCondition;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[78] = objDVOEmployeeStyemplr.TSWO;
        if (objDVOEmployeeStyemplr.TSWO != null)
          if (objDVOEmployeeStyemplr.TSWO != objPreUpdDVOMasterEmployee.TSWO)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "TSWO";
              objtmp.old_value = objPreUpdDVOMasterEmployee.CivilCondition;
              objtmp.new_value = objDVOEmployeeStyemplr.CivilCondition;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[79] = objDVOEmployeeStyemplr.AgeInYears;
        if (objDVOEmployeeStyemplr.AgeInYears != 0)
          if (objDVOEmployeeStyemplr.AgeInYears != objPreUpdDVOMasterEmployee.AgeInYears)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "AgeInYears";
              objtmp.old_value = objPreUpdDVOMasterEmployee.AgeInYears.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.AgeInYears.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[80] = objDVOEmployeeStyemplr.VersionNo;
        if (objDVOEmployeeStyemplr.VersionNo != null)
          if (objDVOEmployeeStyemplr.VersionNo != objPreUpdDVOMasterEmployee.VersionNo)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "VersionNo";
              objtmp.old_value = objPreUpdDVOMasterEmployee.VersionNo;
              objtmp.new_value = objDVOEmployeeStyemplr.VersionNo;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        //  New Code Add 
        parameters[81] = objDVOEmployeeStyemplr.FatherOrHusbandOrGuardianName;
        if (objDVOEmployeeStyemplr.FatherOrHusbandOrGuardianName != null)
          if (objDVOEmployeeStyemplr.FatherOrHusbandOrGuardianName != objPreUpdDVOMasterEmployee.FatherOrHusbandOrGuardianName)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "FatherOrHusbandOrGuardianName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.FatherOrHusbandOrGuardianName;
              objtmp.new_value = objDVOEmployeeStyemplr.FatherOrHusbandOrGuardianName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[82] = objDVOEmployeeStyemplr.PresentAddress;
        if (objDVOEmployeeStyemplr.PresentAddress != null)
          if (objDVOEmployeeStyemplr.PresentAddress != objPreUpdDVOMasterEmployee.PresentAddress)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PresentAddress";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PresentAddress;
              objtmp.new_value = objDVOEmployeeStyemplr.PresentAddress;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[83] = objDVOEmployeeStyemplr.PresentDistrict;
        if (objDVOEmployeeStyemplr.PresentDistrict != null)
          if (objDVOEmployeeStyemplr.PresentDistrict != objPreUpdDVOMasterEmployee.PresentDistrict)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PresentDistrict";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PresentDistrict;
              objtmp.new_value = objDVOEmployeeStyemplr.PresentDistrict;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[84] = objDVOEmployeeStyemplr.PresentVillageName;
        if (objDVOEmployeeStyemplr.PresentVillageName != null)
          if (objDVOEmployeeStyemplr.PresentVillageName != objPreUpdDVOMasterEmployee.PresentVillageName)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PresentVillageName";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PresentVillageName;
              objtmp.new_value = objDVOEmployeeStyemplr.PresentVillageName;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[85] = objDVOEmployeeStyemplr.PermanentAddress;
        if (objDVOEmployeeStyemplr.PermanentAddress != null)
          if (objDVOEmployeeStyemplr.PermanentAddress != objPreUpdDVOMasterEmployee.PermanentAddress)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "PermanentAddress";
              objtmp.old_value = objPreUpdDVOMasterEmployee.PermanentAddress;
              objtmp.new_value = objDVOEmployeeStyemplr.PermanentAddress;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[86] = objDVOEmployeeStyemplr.SubmissionLocation;
        if (objDVOEmployeeStyemplr.SubmissionLocation != null)
          if (objDVOEmployeeStyemplr.SubmissionLocation != objPreUpdDVOMasterEmployee.SubmissionLocation)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SubmissionLocation";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SubmissionLocation;
              objtmp.new_value = objDVOEmployeeStyemplr.SubmissionLocation;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        parameters[87] = objDVOEmployeeStyemplr.Category;
        if (objDVOEmployeeStyemplr.Category != null)
          if (objDVOEmployeeStyemplr.Category != objPreUpdDVOMasterEmployee.Category)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Category";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Category;
              objtmp.new_value = objDVOEmployeeStyemplr.Category;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[88] = objDVOEmployeeStyemplr.LastTask;
        if (objDVOEmployeeStyemplr.LastTask != null)
          if (objDVOEmployeeStyemplr.LastTask != objPreUpdDVOMasterEmployee.LastTask)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "LastTask";
              objtmp.old_value = objPreUpdDVOMasterEmployee.LastTask;
              objtmp.new_value = objDVOEmployeeStyemplr.LastTask;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[89] = objDVOEmployeeStyemplr.SelectDistrict;
        if (objDVOEmployeeStyemplr.SelectDistrict != null)
          if (objDVOEmployeeStyemplr.SelectDistrict != objPreUpdDVOMasterEmployee.SelectDistrict)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SelectDistrict";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SelectDistrict;
              objtmp.new_value = objDVOEmployeeStyemplr.SelectDistrict;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[90] = objDVOEmployeeStyemplr.SubmissionDate;
        if (objDVOEmployeeStyemplr.SubmissionDate != null)
          if (objDVOEmployeeStyemplr.SubmissionDate != objPreUpdDVOMasterEmployee.SubmissionDate)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "SubmissionDate";
              objtmp.old_value = objPreUpdDVOMasterEmployee.SubmissionDate.ToString();
              objtmp.new_value = objDVOEmployeeStyemplr.SubmissionDate.ToString();
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }

        parameters[91] = objDVOEmployeeStyemplr.ReasonForChange;
        if (objDVOEmployeeStyemplr.ReasonForChange != null)
          if (objDVOEmployeeStyemplr.ReasonForChange != objPreUpdDVOMasterEmployee.ReasonForChange)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "ReasonForChange";
              objtmp.old_value = objPreUpdDVOMasterEmployee.ReasonForChange;
              objtmp.new_value = objDVOEmployeeStyemplr.ReasonForChange != null ? "• [" + DateTime.Now.ToString("dd/MM/yyyy") + "] " + objDVOEmployeeStyemplr.ReasonForChange : "";
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[92] = objDVOEmployeeStyemplr.Last_pay_date;
        if (objDVOEmployeeStyemplr.Last_pay_date != null)
          if (objDVOEmployeeStyemplr.Last_pay_date != objPreUpdDVOMasterEmployee.Last_pay_date)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Last_pay_date";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Last_pay_date;
              objtmp.new_value = objDVOEmployeeStyemplr.Last_pay_date;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }
        parameters[93] = objDVOEmployeeStyemplr.Application_approve_on;
        if (objDVOEmployeeStyemplr.Application_approve_on != null)
          if (objDVOEmployeeStyemplr.Application_approve_on != objPreUpdDVOMasterEmployee.Application_approve_on)
          {
            using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
            {
              objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
              objtmp.field_name = "Application_approve_on";
              objtmp.old_value = objPreUpdDVOMasterEmployee.Application_approve_on;
              objtmp.new_value = objDVOEmployeeStyemplr.Application_approve_on;
              objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
              objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
              objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
              listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
            }
          }


        //parameters[94] = objDVOEmployeeStyemplr.PermanentHalqaPanchayatMunicipalityName;
        //if (objDVOEmployeeStyemplr.PermanentHalqaPanchayatMunicipalityName != null)
        //  if (objDVOEmployeeStyemplr.PermanentHalqaPanchayatMunicipalityName != objPreUpdDVOMasterEmployee.PermanentHalqaPanchayatMunicipalityName)
        //  {
        //    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
        //    {
        //      objtmp.empl_code = objDVOEmployeeStyemplr.EmplCode;
        //      objtmp.field_name = "PermanentHalqaPanchayatMunicipalityName";
        //      objtmp.old_value = objPreUpdDVOMasterEmployee.PermanentHalqaPanchayatMunicipalityName;
        //      objtmp.new_value = objDVOEmployeeStyemplr.PermanentHalqaPanchayatMunicipalityName;
        //      objtmp.update_by = objDVOEmployeeStyemplr.UpdateBy;
        //      objtmp.update_machine = objDVOEmployeeStyemplr.UpdateMachineInfo;
        //      objtmp.update_date = objDVOEmployeeStyemplr.UpdateDate;
        //      listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
        //    }
        //  }
        #endregion Parameters

        if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
          BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);
        //if (listDVOEmpTypLogEmpsaltyplog.Count > 0)
        //  BLLEmpTypLogEmpsaltyplog.UpdateEmployeeSalaryTypeLog(ref objTransaction, ref listDVOEmpTypLogEmpsaltyplog);

        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmployeeStyemplr.UPDATE_SPNAME);
        if (o == null)
          throw new Exception();
        else if (Convert.ToInt32(o) < 1)
          throw new Exception();

        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = objDVOEmployeeStyemplr.EmplCode;
        objDVOFlexSegCommon.AccountType = objDVOEmployeeStyemplr.FlexDeptAcctType;
        objDVOFlexSegCommon.keyvalue = FlexDeptKeyvalue;
        BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

        BLLMasterEmployeeIncomes.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmployeeIncomes);
        BLLMasterEmployeeDeductions.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmployeeDeductions);
        BLLMasterEmployeeObligations.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmployeeObligations);
        BLLPREmployeePositionHistoryInyemppd.DeleteData(ref objTransaction, ref listDeleteDVOPREmployeePositionHistoryInyemppd);
        BLLMasterEmpBankDetails.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmpBankDetails);

        BLLMasterEmployeeIncomes.UpdateData(ref objTransaction, ref listDVOMasterEmployeeIncomes, ref listPreDVOMasterEmployeeIncomes);
        BLLMasterEmployeeDeductions.UpdateData(ref objTransaction, ref listDVOMasterEmployeeDeductions, ref listPreDVOMasterEmployeeDeductions);
        BLLMasterEmployeeObligations.UpdateData(ref objTransaction, ref listDVOMasterEmployeeObligations, ref listPreDVOMasterEmployeeObligations);
        BLLPREmployeePositionHistoryInyemppd.UpdateData(ref objTransaction, ref listDVOPREmployeePositionHistoryInyemppd, ref listPreDVOPREmployeePositionHistoryInyemppd);
        BLLMasterEmpBankDetails.UpdateData(ref objTransaction, ref listDVOMasterEmpBankDetails, ref listPreDVOMasterEmpBankDetails);
        //BLLScreenNotesStxnoted.UpdateEmployeeNotes(ref objTransaction, ref listDVOstxnoted);
        BLLScreenNotesStxnoted.UpdateCommonNotes(ref objTransaction, ref listDVOstxnoted);

        BLLMasterEmployeeIncomes.InsertData(ref objTransaction, ref listNewVOPREmployeeIncomeCodeAndRateDetailMasterEmployeeIncomes);
        BLLMasterEmployeeDeductions.InsertData(ref objTransaction, ref listNewDVOMasterEmployeeDeductions);
        BLLMasterEmployeeObligations.InsertData(ref objTransaction, ref listNewDVOMasterEmployeeObligations);
        BLLPREmployeePositionHistoryInyemppd.InsertData(ref objTransaction, ref listNewDVOPREmployeePositionHistoryInyemppd);
        BLLMasterEmpBankDetails.InsertData(ref objTransaction, ref listNewDVOMasterEmpBankDetails);

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

    /// <summary>
    /// To Delete Employee Information
    /// </summary>
    /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
    /// <returns></returns>
    public static int DeleteEmployeeInformation(ref DVOMasterEmployee objDVOEmployeeStyemplr,
        ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes,
        ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions,
        ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations,
        ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd,
        ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails,
        string FlexDeptKeyvalue)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = objDVOEmployeeStyemplr.EmplCode;

        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmployeeStyemplr.DELETE_SPNAME);
        if (o == null)
          throw new Exception();
        else if (Convert.ToInt32(o) < 1)
          throw new Exception();

        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = objDVOEmployeeStyemplr.EmplCode;
        BLLFlexSegCommon.Flexseg_delete(ref objDVOFlexSegCommon);

        BLLMasterEmployeeIncomes.DeleteData(ref objTransaction, ref listDVOMasterEmployeeIncomes);
        BLLMasterEmployeeDeductions.DeleteData(ref objTransaction, ref listDVOMasterEmployeeDeductions);
        BLLMasterEmployeeObligations.DeleteData(ref objTransaction, ref listDVOMasterEmployeeObligations);
        BLLPREmployeePositionHistoryInyemppd.DeleteData(ref objTransaction, ref listDVOPREmployeePositionHistoryInyemppd);
        BLLMasterEmpBankDetails.DeleteData(ref objTransaction, ref listDVOMasterEmpBankDetails);

        parameters = null;
        objDALBaseClass = null;

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

    /// <summary>
    /// Get Keyvalue for flex-department-account-type of employee
    /// </summary>
    /// <param name="pobjDVOMasterEmployee"></param>
    /// <returns></returns>
    public static string GetFlexDeptKeyvalue(ref DVOMasterEmployee pobjDVOMasterEmployee)
    {
      string _FlexDeptKeyvalue = string.Empty;
      try
      {
        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = pobjDVOMasterEmployee.EmplCode;
        objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployee.FlexDeptAcctType;
        _FlexDeptKeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
        objDVOFlexSegCommon = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return _FlexDeptKeyvalue;
    }
    public static string GetFlexDeptKeyvalueNew(String Employeecode, String FlexDeptAcctType)
    {
      string _FlexDeptKeyvalue = string.Empty;
      string EMployercode = "";
      String EmployerName = "";
      try
      {
        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = Employeecode;
        objDVOFlexSegCommon.AccountType = FlexDeptAcctType;
        _FlexDeptKeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
        objDVOFlexSegCommon = null;
        if (_FlexDeptKeyvalue.ToString().Length > 3)
        {
          EMployercode = _FlexDeptKeyvalue.Substring(0, 3);
          EmployerName = GetFlexDeptName(EMployercode);
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return EmployerName;
    }

    public static string GetFlexDeptName(String EmployerCode)
    {
      string _FlexDeptName = string.Empty;
      try
      {
        _FlexDeptName = BLLFlexSegCommon.Flexseg_Name(EmployerCode, 1);

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return _FlexDeptName;
    }

    public static string GetFlexProgramName(String ProgramCode)
    {
      string _FlexDeptProgramName = string.Empty;
      try
      {
        _FlexDeptProgramName = BLLFlexSegCommon.Flexseg_Name(ProgramCode, 2);
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return _FlexDeptProgramName;
    }
    //Added By Rajeev
    //Date : 02/03/10
    //To Get the requester on update Requisition form
    public static string GetFlexDeptKeyvalue_Requisition(ref DVOMasterEmployee pobjDVOMasterEmployee)
    {
      string _FlexDeptKeyvalue = string.Empty;
      try
      {
        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = pobjDVOMasterEmployee.EmplCode;
        objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployee.FlexDeptAcctType;
        _FlexDeptKeyvalue = BLLFlexSegCommon.GetKeyValueForRequisition(ref objDVOFlexSegCommon);
        objDVOFlexSegCommon = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return _FlexDeptKeyvalue;
    }


    //Added By Rahul Jain On 24/12/2008 for getting empl code from styemplr tables 
    //and using in special control payrollsearch control for validation  
    public static bool GetExistsEmplCodes(ref DVOMasterEmployee objDvoEmplCode)
    {
      bool status = false;
      object[] Parameter = new object[1];
      Parameter[0] = objDvoEmplCode.EmplCode;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object c = objDalBaseClass.ExecuteScalar(ref Parameter, objDvoEmplCode.FIND_EXISTS_EMPL_CODE);
      if (Convert.ToInt16(c) == 1)
      {
        status = true;
      }
      return status;
    }
    //******************************************************************************

    //Added by rajeev
    //Date :29/12/2008
    //Aim: To get the employee details whose termination vlaue is null

    public static List<DVOMasterEmployee> GetAllEmployeeDetails_NotTerminated()
    {
      DVOMasterEmployee objtempDVOMasterEmployee = new DVOMasterEmployee();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();

      try
      {
        using (DataSet ds = objDalBaseClass.GetData(typeof(DVOMasterEmployee), objtempDVOMasterEmployee.FIND_GET_EMP_NOT_TERMINATED))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//"p_empl_code"

            objDVOEmployeeStyemplr.FirstName = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//"p_last_name"                       
            objDVOEmployeeStyemplr.FlexDeptAcctType = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//""                       
            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      return listDVOEmployeeStyemplr;
    }




    public static DataSet GetDetails(ref DVOMasterEmployee objDVOMasterEmployee)
    {
      DataSet ds = null;
      object[] Parameter = new object[2];
      Parameter[0] = objDVOMasterEmployee.inc_code;
      Parameter[1] = objDVOMasterEmployee.type_code;

      DVOMasterEmployee objtempDVOMasterEmployee = new DVOMasterEmployee();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMasterEmployee), objtempDVOMasterEmployee.FND_INCOMES_DETAILS);
        if (ds != null)
          if (ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "empl_code";
            ds.Tables[0].Columns[1].ColumnName = "doc_no";
            ds.Tables[0].Columns[2].ColumnName = "inc_code";
            ds.Tables[0].Columns[3].ColumnName = "amount";
            ds.Tables[0].Columns[4].ColumnName = "pay_date";
          }

        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return ds;
      }
      return ds;


    }

    public static DataSet GetTypeDetails(ref DVOMasterEmployee objDVOMasterEmployee)
    {


      DataSet ds = null;
      object[] parameter = new object[1];
      parameter[0] = objDVOMasterEmployee.type_code;

      DVOMasterEmployee objtempDVOMasterEmployee = new DVOMasterEmployee();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        ds = objDalBaseClass.GetData(ref parameter, typeof(DVOMasterEmployee), objtempDVOMasterEmployee.FND_TYPE_DETAILS);
        if (ds != null)
          if (ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "empl_code";

          }
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return ds;
      }
      return ds;

    }
    public static DataSet GetYTDDetails(ref DVOMasterEmployee objDVOMasterEmployee)
    {
      DataSet ds = null;
      object[] Parameter = new object[2];
      Parameter[0] = objDVOMasterEmployee.inc_code;
      Parameter[1] = objDVOMasterEmployee.type_code;

      DVOMasterEmployee objtempDVOMasterEmployee = new DVOMasterEmployee();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMasterEmployee), objtempDVOMasterEmployee.FND_YTD_DETAILS);
        if (ds != null)
          if (ds.Tables.Count > 0)
          {
            ds.Tables[0].Columns[0].ColumnName = "empl_code";
            ds.Tables[0].Columns[1].ColumnName = "inc_code";
            ds.Tables[0].Columns[2].ColumnName = "inc_ytd";
          }

        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return ds;
      }
      return ds;

    }


    //Added By Rajeev
    //Date : 02/03/2010
    // To process all requester. 
    public static List<DVOMasterEmployee> GET_All_Employee_Not_Terminated()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();
      DVOMasterEmployee obj = new DVOMasterEmployee();

      try
      {
        object[] parameters = new object[0];

        using (DataSet ds = objDalBaseClass.GetData(typeof(DVOMasterEmployee), obj.GET_ALL_EMPLOYEE_NOT_TERMINATED))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//"p_empl_code"                        
            objDVOEmployeeStyemplr.TypeCode = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//"p_type_code"

            objDVOEmployeeStyemplr.FirstName = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//"p_first_name"
            objDVOEmployeeStyemplr.MiddleName = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//"p_middle_name"
            objDVOEmployeeStyemplr.LastName = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//"p_last_name"


            objDVOEmployeeStyemplr.FlexDeptAcctType = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//"p_flexdeptaccttype"

            objDVOEmployeeStyemplr.RowID = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"p_gender"
            objDVOEmployeeStyemplr.cash_acct_kv = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"cash_acct_kv"
            objDVOEmployeeStyemplr.type_desc = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"type_desc"

            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        parameters = null;
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      return listDVOEmployeeStyemplr;
    }


    public static DataSet GET_All_Employee_Not_Terminated_requisition()
    {
      DataSet ds = null; ;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();
      DVOMasterEmployee obj = new DVOMasterEmployee();
      try
      {
        object[] parameters = new object[0];
        ds = objDalBaseClass.GetData(typeof(DVOMasterEmployee), obj.GET_ALL_EMPLOYEE_NOT_TERMINATED);
        if (ds.Tables.Count > 0)
        {
          ds.Tables[0].Columns[0].ColumnName = "empl_code";
          ds.Tables[0].Columns[1].ColumnName = "type_code";
          ds.Tables[0].Columns[2].ColumnName = "first_name";
          ds.Tables[0].Columns[3].ColumnName = "middle_name";
          ds.Tables[0].Columns[4].ColumnName = "last_name";
          ds.Tables[0].Columns[5].ColumnName = "flexdeptaccttype";
          ds.Tables[0].Columns[6].ColumnName = "RowID";

        }

      }
      catch (Exception ex)
      { ExceptionManagement.ExceptionManager.Publish(ex); }
      finally
      {
        ds.Dispose();
      }
      return ds;
    }
    public static void UpdateEmployeeOnHold(string empl_code, string onHold)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = empl_code;
        parameters[1] = onHold;

        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmployeeStyemplr.UPD_ONHOLD);
        if (o == null || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          throw new Exception("Error has occurred while updating Employee Info.");

        //Create Log.. 
        List<DVOEmployeeInfoLogEmpUpdLog> ListObjLog = new List<DVOEmployeeInfoLogEmpUpdLog>();
        DVOEmployeeInfoLogEmpUpdLog objLog = new DVOEmployeeInfoLogEmpUpdLog();
        objLog.empl_code = empl_code;
        objLog.field_name = "hold_pymnt";
        objLog.old_value = onHold == "Y" ? "N" : "Y";
        objLog.new_value = onHold;
        objLog.update_by = DVOApplicationUserInfo.UserId;
        objLog.update_machine = DVOApplicationUserInfo.MachineInfo;
        ListObjLog.Add(objLog);
        BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref ListObjLog);

        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }

    public static List<DVOMasterEmployee> GetEmpToUpdateHoldInfo(string type_code, string firstname, string LastName)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      List<DVOMasterEmployee> listDVOEmployeeStyemplr = new List<DVOMasterEmployee>();
      try
      {
        object[] parameters = new object[4];
        parameters[0] = type_code;
        parameters[1] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[2] = firstname.ToString();
        parameters[3] = LastName.ToString();
        using (DataSet ds = objDalBaseClass.GetData((new DVOMasterEmployee()).FIND_EMP(ref parameters)))
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {

            DVOMasterEmployee objDVOEmployeeStyemplr = new DVOMasterEmployee();
            objDVOEmployeeStyemplr.EmplCode = (dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.SocSecNum = (dr["soc_sec_num"] != DBNull.Value ? dr["soc_sec_num"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.TypeCode = (dr["type_code"] != DBNull.Value ? dr["type_code"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.FirstName = (dr["first_name"] != DBNull.Value ? dr["first_name"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.MiddleName = (dr["middle_name"] != DBNull.Value ? dr["middle_name"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.LastName = (dr["last_name"] != DBNull.Value ? dr["last_name"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Address1 = (dr["address1"] != DBNull.Value ? dr["address1"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Address2 = (dr["address2"] != DBNull.Value ? dr["address2"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.City = (dr["city"] != DBNull.Value ? dr["city"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.State = (dr["state"] != DBNull.Value ? dr["state"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Zip = (dr["zip"] != DBNull.Value ? dr["zip"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.CashAcct = (dr["cash_acct"] != DBNull.Value ? Convert.ToInt32(dr["cash_acct"]) : 0);
            objDVOEmployeeStyemplr.HoldPayment = (dr["hold_pymnt"] != DBNull.Value ? dr["hold_pymnt"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.FlexDeptAcctType = (dr["flexdeptaccttype"] != DBNull.Value ? dr["flexdeptaccttype"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.Gender = (dr["gender"] != DBNull.Value ? dr["gender"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.cash_acct_kv = (dr["keyvalue"] != DBNull.Value ? dr["keyvalue"].ToString().Trim() : string.Empty);
            objDVOEmployeeStyemplr.type_code = (dr["type_code"] != DBNull.Value ? dr["type_code"].ToString().Trim() : string.Empty);
            listDVOEmployeeStyemplr.Add(objDVOEmployeeStyemplr);
          }
        }
        parameters = null;
        objDalBaseClass = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        return listDVOEmployeeStyemplr;
      }
      return listDVOEmployeeStyemplr;
    }

    //Added By Rajeev
    //Aim : Optimizing to fill Requesot on Update Requisition form.
    //Date : 03/03/2010

    //public static DataTable GetAllEmployee_As_Requestor(DVOSecUsers_FP objDVOSecUsers_FP_UserMinDept)
    //{
    //    DataTable objDataTable = new DataTable();
    //    try
    //    {
    //        List<DVOSecUsers_FP> listKeyvalue = new List<DVOSecUsers_FP>();
    //        DVOSecUsers_FP objKeyvalue = new DVOSecUsers_FP();

    //        DataSet EmpDs = GET_All_Employee_Not_Terminated_As_Requestor();
    //        objDataTable = EmpDs.Tables[0].Clone();
    //        if (EmpDs != null)
    //            if (EmpDs.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < EmpDs.Tables[0].Rows.Count - 1; i++)
    //                {
    //                }
    //            }

    //    }
    //    catch (Exception ex)
    //    { }
    //    finally
    //    { }

    //    return objDataTable;
    //}

    //public static DataSet GET_All_Employee_Not_Terminated_As_Requestor()
    //{
    //    DataSet ds = null; ;
    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();            
    //    DVOMasterEmployee obj = new DVOMasterEmployee();
    //    try
    //    {
    //        object[] parameters = new object[0];
    //        ds = objDalBaseClass.GetData(typeof(DVOMasterEmployee), obj.GET_ALL_EMPLOYEE_AS_REQUESTOR);
    //        if (ds.Tables.Count > 0)
    //        {
    //            ds.Tables[0].Columns[0].ColumnName = "keyvalue";
    //            ds.Tables[0].Columns[1].ColumnName = "position";
    //            ds.Tables[0].Columns[2].ColumnName = "length";
    //            ds.Tables[0].Columns[3].ColumnName = "abbreviation";
    //            ds.Tables[0].Columns[4].ColumnName = "empl_code";                   
    //            ds.Tables[0].Columns[5].ColumnName = "first_name";
    //            ds.Tables[0].Columns[6].ColumnName = "middle_name";
    //            ds.Tables[0].Columns[7].ColumnName = "last_name";                    
    //            ds.Tables[0].Columns[8].ColumnName = "RowID";
    //        }
    //    }
    //    catch (Exception ex)
    //    { ExceptionManagement.ExceptionManager.Publish(ex); }
    //    finally
    //    {
    //        ds.Dispose();
    //    }
    //    return ds;
    //}

    public static List<TotalContributionSummaryViewModel> GelAllPaymentTotalCount(int UserId, int RoleId, int batchid,string DistrictNames,int currentFinancialYear)
   {
      try
      {
        List<TotalContributionSummaryViewModel> listCount = new List<TotalContributionSummaryViewModel>();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //SqlParameter[] Parameter = new SqlParameter[]
        //{
        //          new SqlParameter("@RoleId",RoleId),
        //          new SqlParameter("@UserId",UserId)
        //};
        object[] parameters = new object[3];
        parameters[0] = RoleId;
        parameters[1] = UserId;
        //parameters[0] = DistrictNames;
        parameters[2] = batchid;
        DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(TotalContributionSummaryViewModel), "USP_PaymentTotalCount"); // THIS IS TIME TAKEN PROCEDURE
       // DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(TotalContributionSummaryViewModel), "USP_GENERATEPENTION_PAYMENTSUMMARY");// THIS IS OPTIMIZE PROCEDURE FOR ROHIT

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
          // Dictionary to hold row index as key and TotalContributionSummaryViewModel as value
          Dictionary<int, TotalContributionSummaryViewModel> dataDict = new Dictionary<int, TotalContributionSummaryViewModel>();

          // Iterate through the first table and populate the dictionary
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            DataRow dr = ds.Tables[0].Rows[i];
            TotalContributionSummaryViewModel ContributorCont = new TotalContributionSummaryViewModel
            {
              Region = dr[0] == DBNull.Value ? "" : dr[0].ToString(),
              District = dr[1] == DBNull.Value ? "" : dr[1].ToString(),
              TotalMonthAmount = dr[2] == DBNull.Value ? 0 : Convert.ToDecimal(dr[2]),
              pay_date = dr[3] == DBNull.Value ? (DateTime?)null : DVOApplicationUserInfo.ParseDateConvertion(dr[3].ToString()),
              pybatchid = Convert.ToInt32(dr[4] == DBNull.Value ? "0" : dr[4].ToString()),
              TotalArreaMonthAmount = dr[5] == DBNull.Value ? 0 : Convert.ToDecimal(dr[5]),
              OAPCount = Convert.ToInt32(dr[6] == DBNull.Value ? "0" : dr[6].ToString()),
              WIDCount = Convert.ToInt32(dr[7] == DBNull.Value ? "0" : dr[7].ToString()),
              PCPCount = Convert.ToInt32(dr[8] == DBNull.Value ? "0" : dr[8].ToString()),
              TGRCount = Convert.ToInt32(dr[9] == DBNull.Value ? "0" : dr[9].ToString()),
            };
            ContributorCont.BeneficiariesCount = ContributorCont.OAPCount + ContributorCont.PCPCount + ContributorCont.WIDCount + ContributorCont.TGRCount;

            // Add to dictionary
            dataDict[i] = ContributorCont;
          }

          // get isProcessed from MediaDownloads

          for (int i = 0; i < dataDict.Count; i++)
          {

            // Iterate through the second table and update the dictionary
            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
              DataRow dr = ds.Tables[1].Rows[j];

              if (dataDict[i].District.ToLower().ToString() == dr[1].ToString().ToLower())
              {
                dataDict[i].IsReUploaded = dr[0] == DBNull.Value ? "" : dr[0].ToString();
              }
            }

            // Iterate through the second table and update the dictionary
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
              DataRow dr = ds.Tables[2].Rows[k];

              string FileName = dr[0].ToString().ToLower();
              string DistrictName = string.Empty;
              if (!string.IsNullOrEmpty(FileName))
              {
                string[] FileNameArray = FileName.Split('_');
                if (FileNameArray.Length > 1)
                {
                  DistrictName = FileNameArray[0];
                }
              }

              string isProcessed = dr[1].ToString().ToLower();
              if (string.IsNullOrEmpty(isProcessed))
              {
                isProcessed = "false";
              }


              if (dataDict[i].District.ToLower().ToString() == DistrictName && isProcessed == "true")
              {
                dataDict[i].isProcessed = true;
              }
              else if (dataDict[i].District.ToLower().ToString() == DistrictName && isProcessed == "true")
              {
                dataDict[i].isProcessed = false;
              }
            }

          }

          // Convert dictionary values to list
          listCount = dataDict.Values.ToList();
        }


        return listCount;

        // old code working 

        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{



        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                TotalContributionSummaryViewModel ContributorCont = new TotalContributionSummaryViewModel();
        //                ContributorCont.Region = dr[0] == DBNull.Value ? "" : dr[0].ToString();//"RegionName"
        //                ContributorCont.District = dr[1] == DBNull.Value ? "" : dr[1].ToString();//"District"
        //                                                                                         //ContributorCont.TotalContributorContribution = dr[2] == DBNull.Value ? 0 : Convert.ToDecimal(dr[2]);//"TotalYearAmount"
        //                ContributorCont.TotalMonthAmount = dr[2] == DBNull.Value ? 0 : Convert.ToDecimal(dr[2]);//"TotalMonthAmount"
        //                                                                                                        //ContributorCont.TotalEmployerContribution = dr[3] == DBNull.Value ? "" : dr[3].ToString();//"OK"  
        //                                                                                                        //ContributorCont.status = dr[4] == DBNull.Value ? "" : dr[4].ToString();//"Fail"
        //                                                                                                        //ContributorCont.NotUpdated = dr[5] == DBNull.Value ? "" : dr[5].ToString();//"Status Not Updated"
        //                ContributorCont.pay_date = dr[3] == DBNull.Value ? (DateTime?)null : DVOApplicationUserInfo.ParseDateConvertion(dr[3].ToString());//"RegionName"
        //                ContributorCont.pybatchid = Convert.ToInt32(dr[4] == DBNull.Value ? "0" : dr[4].ToString());//"RegionName"

        //                ContributorCont.TotalArreaMonthAmount = dr[5] == DBNull.Value ? 0 : Convert.ToDecimal(dr[5]);

        //                ContributorCont.OAPCount = Convert.ToInt32(dr[6] == DBNull.Value ? "0" : dr[6].ToString());//"OAP Scheam"
        //                ContributorCont.WIDCount = Convert.ToInt32(dr[7] == DBNull.Value ? "0" : dr[7].ToString());//"WID Scheam"
        //                ContributorCont.PCPCount = Convert.ToInt32(dr[8] == DBNull.Value ? "0" : dr[8].ToString());//"PCP Scheam"
        //                ContributorCont.TGRCount = Convert.ToInt32(dr[9] == DBNull.Value ? "0" : dr[9].ToString());//"TGR Scheam"
        //                ContributorCont.BeneficiariesCount = (ContributorCont.OAPCount + ContributorCont.PCPCount + ContributorCont.WIDCount + ContributorCont.TGRCount);//"TGR Scheam"

        //                listCount.Add(ContributorCont);
        //            }
        //        }
        //         
        //return listCount;
      }
      catch (Exception ex)
      {
        throw;
      }

    }
  }
}

