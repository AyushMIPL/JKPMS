using JKPS.BLL;
using JKPS.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Data;
namespace App.Web.Repository
{
    public class UpdateEmployeeDetailsRepo
    {
        public static int UpdateEmployeeInformation(DVOMasterEmployee objDVOMasterEmployeeModel)
        {
            try
            {
                ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();

                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                //make an object with values of appropriate properties to insert into database
                //DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();

                #region "Employee Information"
                //make object to pass as parameter of search function
                DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
                //call getDate function of BLL
                List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
                objDVOMasterEmpTypes = null;
                var marriage = from x in db.MasterContributorMarriageDetails where x.PersonId == objDVOMasterEmployeeModel.PersonID select new { x };
                var city = from x in db.MasterCity where x.Name == objDVOMasterEmployeeModel.City select new { x.Name };

                //objDVOMasterEmployee.EmplrCode = objDVOMasterEmployeeModel.EmplrCode == null ? 0 : (int)objDVOMasterEmployeeModel.EmplrCode;
                //objDVOMasterEmployee.EmplCode = objDVOMasterEmployeeModel.EmplCode;
                //objDVOMasterEmployee.HoldPayment = string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.HoldPayment) ? "N" : objDVOMasterEmployeeModel.HoldPayment;// "N";// need to discuss about permission for hold payment//txtOnHold0.Text;
                //objDVOMasterEmployee.SocSecNum = objDVOMasterEmployeeModel.SocSecNum;
                //objDVOMasterEmployee.FlexDeptAcctType = objDVOMasterEmployeeModel.FlexDeptAcctType == null ? "EXPENS" : objDVOMasterEmployeeModel.FlexDeptAcctType;
                //if (listDVOMasterEmpTypes.Any())
                //{
                //  objDVOMasterEmployee.TypeCode = listDVOMasterEmpTypes.FirstOrDefault().type_code;
                //  objDVOMasterEmployee.CashAcct = listDVOMasterEmpTypes.FirstOrDefault().cash_acct == 0 ? 1000 : listDVOMasterEmpTypes.FirstOrDefault().cash_acct;
                //}
                //objDVOMasterEmployee.LastName = objDVOMasterEmployeeModel.LastName;
                //objDVOMasterEmployee.FirstName = objDVOMasterEmployeeModel.FirstName;
                //objDVOMasterEmployee.MiddleName = objDVOMasterEmployeeModel.MiddleName;
                //objDVOMasterEmployee.Address1 = objDVOMasterEmployeeModel.Address1;
                ////objDVOMasterEmployee.Address2 = objDVOMasterEmployeeModel.Address2;
                //objDVOMasterEmployee.City = city.Any() ? city.FirstOrDefault().Name : null;
                ////objDVOMasterEmployee.State = objDVOMasterEmployeeModel.State;
                ////objDVOMasterEmployee.Zip = objDVOMasterEmployeeModel.Zip;
                //objDVOMasterEmployee.Phone = objDVOMasterEmployeeModel.Phone;
                //objDVOMasterEmployee.mailid = objDVOMasterEmployeeModel.mailid;
                //if (objDVOMasterEmployee.Birthdate != null)
                //  objDVOMasterEmployee.Birthdate = objDVOMasterEmployeeModel.Birthdate.ToString();
                //objDVOMasterEmployee.Gender = objDVOMasterEmployeeModel.Gender;

                //if (objDVOMasterEmployeeModel.LastPay != null)
                //  objDVOMasterEmployee.LastIncDate = objDVOMasterEmployeeModel.LastPay.ToString();// dtpLastIncrementDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //if (objDVOMasterEmployeeModel.Terminated != "")
                //  objDVOMasterEmployee.Terminated = objDVOMasterEmployeeModel.Terminated;// dtpTerminationDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //if (objDVOMasterEmployeeModel.AppointDate != null)
                //  objDVOMasterEmployee.AppointDate = objDVOMasterEmployeeModel.AppointDate.ToString();// dtpAppointmentDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                //objDVOMasterEmployee.Department = "000";
                //objDVOMasterEmployee.Prefix = objDVOMasterEmployeeModel.Prefix != null ? objDVOMasterEmployeeModel.Prefix : string.Empty;
                //objDVOMasterEmployee.PostalAddress = objDVOMasterEmployeeModel.PostalAddress;
                //objDVOMasterEmployee.Suffix = objDVOMasterEmployeeModel.Suffix != null ? objDVOMasterEmployeeModel.Suffix : string.Empty;
                //objDVOMasterEmployee.MaidenName = objDVOMasterEmployeeModel.MaidenName;
                //objDVOMasterEmployee.Nationality = objDVOMasterEmployeeModel.Nationality != null ? objDVOMasterEmployeeModel.Nationality : string.Empty;
                //objDVOMasterEmployee.PhoneOffice = objDVOMasterEmployeeModel.PhoneOffice;
                //objDVOMasterEmployee.Mobile = objDVOMasterEmployeeModel.Mobile;
                //objDVOMasterEmployee.PensionerType = objDVOMasterEmployeeModel.PensionerType != null ? objDVOMasterEmployeeModel.PensionerType : "1";
                //objDVOMasterEmployee.PersonID = objDVOMasterEmployeeModel.PersonID != null ? objDVOMasterEmployeeModel.PersonID : string.Empty;


                #endregion "Employee Information"

                #region "Extended Information"
                //objDVOMasterEmployee.Allowances = objDVOMasterEmployeeModel.Allowances;// txtFedAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtFedAllwncs6.Text);
                //objDVOMasterEmployee.PayPeriod = listDVOMasterEmpTypes.Any() ? listDVOMasterEmpTypes.FirstOrDefault().pay_period : "M";
                //objDVOMasterEmployee.EmplStatus = string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.EmplStatus) ? "Y" : objDVOMasterEmployeeModel.EmplStatus;// txtFullTime6.Text;
                //if (objDVOMasterEmployeeModel.DateHired != null)
                //  objDVOMasterEmployee.DateHired = objDVOMasterEmployeeModel.DateHired.ToString();// dtpHired6.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                ////objDVOMasterEmployee.StateAllow = objDVOMasterEmployeeModel.StateAllow;// txtStateAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtStateAllwncs6.Text);
                //objDVOMasterEmployee.MaritalStat = marriage == null ? "S" : "M";

                //if (objDVOMasterEmployeeModel.SickCode != null)
                //  objDVOMasterEmployee.SickCode = objDVOMasterEmployeeModel.SickCode;// mcgSickLeaveIncomeCode6.SelectedValue.ToString();
                //objDVOMasterEmployee.SickAllowed = objDVOMasterEmployeeModel.SickAllowed;// txtSickAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickAccrued6.Text);
                //objDVOMasterEmployee.SickUsed = objDVOMasterEmployeeModel.SickUsed;// txtSickUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickUsed6.Text);
                //if (objDVOMasterEmployeeModel.VacCode != null)
                //  objDVOMasterEmployee.VacCode = objDVOMasterEmployeeModel.VacCode;// mcgVacationIncomeCode6.SelectedValue.ToString();
                //objDVOMasterEmployee.VacAllowed = objDVOMasterEmployeeModel.VacAllowed;// txtVacAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacAccrued6.Text);
                //objDVOMasterEmployee.VacUsed = objDVOMasterEmployeeModel.VacUsed;// txtVacUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacUsed6.Text);

                //objDVOMasterEmployee.SickAccrCtr = objDVOMasterEmployeeModel.SickAccrCtr;// txtSickCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSickCntr.Text);
                //if (objDVOMasterEmployeeModel.VacAccrCode != null)
                //  objDVOMasterEmployee.VacAccrCode = objDVOMasterEmployeeModel.VacAccrCode;// mcgVacAccrual6.SelectedValue.ToString();
                //objDVOMasterEmployee.VacAccrCtr = objDVOMasterEmployeeModel.VacAccrCtr;// txtVacCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtVacCntr.Text);
                //objDVOMasterEmployee.DirDept = "N";// txtDirectDeposit6.Text;

                //objDVOMasterEmployee.InsertMachineInfo = System.Environment.MachineName;
                //objDVOMasterEmployee.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
                //objDVOMasterEmployee.InsertDate = System.DateTime.Now.ToString();
                #endregion "Extended Information"

                #region "Employee Income"

                //make list of detail-objects of main object
                List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
                List<DVOMasterEmployeeIncomes> listNewDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
                List<DVOMasterEmployeeIncomes> listDeleteDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();




                #endregion "Employee Income"

                #region "Employee Deduction"

                //make list of detail-objects of main object
                List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
                List<DVOMasterEmployeeDeductions> listDeleteDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
                List<DVOMasterEmployeeDeductions> listNewDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();



                #endregion "Employee Deduction"

                #region "Employee Obligation"

                //make list of detail-objects of main object
                List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
                List<DVOMasterEmployeeObligations> listDeleteDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
                List<DVOMasterEmployeeObligations> listNewDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();



                #endregion "Employee Obligation"

                #region "Position History"

                //make list of detail-objects of main object
                List<DVOPREmployeePositionHistoryInyemppd> listDeleteDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
                List<DVOPREmployeePositionHistoryInyemppd> listNewDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
                List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();



                #endregion "Position History"

                #region "Direct Deposit"

                //make list of detail-objects of main object
                List<DVOMasterEmpBankDetails> listNewDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
                List<DVOMasterEmpBankDetails> listDeleteDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
                List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();



                #endregion "Direct Deposit"

                #region "Employee Notes"



                #endregion "Employee Notes"

                //call update function of BLL
                //DVOMasterEmployee objPreUpdDVOMasterEmployee = SearchEmployeeInformation(objDVOMasterEmployee.EmplCode); ;
                object TransactionObject = null;
                List<DVOMasterEmployeeIncomes> listSearchResultDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
                List<DVOMasterEmployeeDeductions> listSearchResultDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
                List<DVOMasterEmployeeObligations> listSearchResultDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
                List<DVOMasterEmpBankDetails> listSearchResultDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
                List<DVOPREmployeePositionHistoryInyemppd> listSearchResultDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
                List<DVOstxnoted> listCommonNotes = new List<DVOstxnoted>();
                int i = BLLMasterEmployee.UpdateEmployeeInformation(ref TransactionObject, ref objDVOMasterEmployeeModel,
                    ref listDVOMasterEmployeeIncomes,
                    ref listDVOMasterEmployeeDeductions,
                    ref listDVOMasterEmployeeObligations,
                    ref listDVOPREmployeePositionHistoryInyemppd,
                    ref listDVOMasterEmpBankDetails, "01",
                    ref objDVOMasterEmployeeModel,
                    ref listSearchResultDVOMasterEmployeeIncomes,
                    ref listSearchResultDVOMasterEmployeeDeductions,
                    ref listSearchResultDVOMasterEmployeeObligations,
                    ref listSearchResultDVOPREmployeePositionHistoryInyemppd,
                    ref listSearchResultDVOMasterEmpBankDetails,
                    //ref listEmployeeNotes,
                    ref listCommonNotes,
                    ref listDeleteDVOMasterEmployeeIncomes,
                    ref listDeleteDVOMasterEmployeeDeductions,
                    ref listDeleteDVOMasterEmployeeObligations,
                    ref listDeleteDVOPREmployeePositionHistoryInyemppd,
                    ref listDeleteDVOMasterEmpBankDetails,
                    ref listNewDVOMasterEmployeeIncomes,
                    ref listNewDVOMasterEmployeeDeductions,
                    ref listNewDVOMasterEmployeeObligations,
                    ref listNewDVOPREmployeePositionHistoryInyemppd,
                    ref listNewDVOMasterEmpBankDetails);

                //objDVOMasterEmployee = null;
                listDVOMasterEmployeeIncomes = null;
                listDVOMasterEmployeeDeductions = null;
                listDVOMasterEmployeeObligations = null;
                listDVOPREmployeePositionHistoryInyemppd = null;
                listDVOMasterEmpBankDetails = null;
                //objPreUpdDVOMasterEmployee = null;
                //if process is successfully completed, then set Success flag to true.
                return i;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}