using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
{
    public class BLLEmployeeWorkHrDetails
    {
        public static int InsertEmployeeWorkHrDetails(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {

            object[] parameters = new object[9];
            parameters[0] = objEmployeeWorkHrDetails.EmpWorkingHrDetailsId;
            parameters[1] = objEmployeeWorkHrDetails.EmpWorkingHrId;
            parameters[2] = objEmployeeWorkHrDetails.empl_code;
            parameters[3] = objEmployeeWorkHrDetails.WorkingDate;
            parameters[4] = objEmployeeWorkHrDetails.Remark;
            parameters[5] = objEmployeeWorkHrDetails.WorkingHr;
            parameters[6] = objEmployeeWorkHrDetails.RgDayOvtm;
            parameters[7] = objEmployeeWorkHrDetails.PhOvtm;
            parameters[8] = objEmployeeWorkHrDetails.Status;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.InsertData(ref parameters, typeof(DVOEmployeeWorkHrDetails), objEmployeeWorkHrDetails.INSERT_SPNAME);
            return Convert.ToInt32(c);
        }

        public static int InsertUpdateTimeCard(ref DVOUpdateTimeCard objUpdateTimeCard)
        {

            object[] parameters = new object[11];
            parameters[0] = objUpdateTimeCard.empl_code;
            parameters[1] = objUpdateTimeCard.empl_name;
            parameters[2] = objUpdateTimeCard.start_date;
            parameters[3] = objUpdateTimeCard.end_date;
            parameters[4] = objUpdateTimeCard.used_flag;
            parameters[5] = objUpdateTimeCard.InsertMachineInfo;
            parameters[6] = objUpdateTimeCard.InsertDate;
            parameters[7] = objUpdateTimeCard.InsertBy;
            parameters[8] = objUpdateTimeCard.UpdateMachineInfo;
            parameters[9] = objUpdateTimeCard.UpdateDate;
            parameters[10] = objUpdateTimeCard.UpdateBy;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.InsertData(ref parameters, typeof(DVOEmployeeWorkHrDetails), objUpdateTimeCard.INSERT_SPNAME);
            return Convert.ToInt32(c);
        }

        public static int InsertUpdateTimeCardDetails(ref DVOUpdateTimeCard objUpdateTimeCard)
        {

            object[] parameters = new object[13];
            parameters[0] = objUpdateTimeCard.card_no;
            parameters[1] = objUpdateTimeCard.inc_code_id;
            parameters[2] = objUpdateTimeCard.inc_rate_id;
            parameters[3] = objUpdateTimeCard.inc_number_id;
            parameters[4] = objUpdateTimeCard.inc_hours_id;
            parameters[5] = objUpdateTimeCard.acct_no_id;
            parameters[6] = objUpdateTimeCard.line_no_id;
            parameters[7] = objUpdateTimeCard.InsertMachineInfo;
            parameters[8] = objUpdateTimeCard.InsertDate;
            parameters[9] = objUpdateTimeCard.InsertBy;
            parameters[10] = objUpdateTimeCard.UpdateMachineInfo;
            parameters[11] = objUpdateTimeCard.UpdateDate;
            parameters[12] = objUpdateTimeCard.UpdateBy;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.InsertData(ref parameters, typeof(DVOEmployeeWorkHrDetails), objUpdateTimeCard.INSERT_SPNAME);
            return Convert.ToInt32(c);
        }

        public static List<DVOEmployeeWorkHrDetails> GetEmployeeWorkHrDetails(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[5];
            Parameter[0] = objEmployeeWorkHrDetails.EmpWorkingHrDetailsId;
            Parameter[1] = objEmployeeWorkHrDetails.EmpWorkingHrId;
            Parameter[2] = objEmployeeWorkHrDetails.empl_code;
            Parameter[3] = objEmployeeWorkHrDetails.StartDate;
            Parameter[4] = objEmployeeWorkHrDetails.EndDate;
            List<DVOEmployeeWorkHrDetails> lstEmployeeWorkHrDetails = new List<DVOEmployeeWorkHrDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOEmployeeWorkHrDetails), objEmployeeWorkHrDetails.FIND_SPNAME))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmployeeWorkHrDetails obj = new DVOEmployeeWorkHrDetails();
                    obj.EmpWorkingHrDetailsId = Convert.ToInt32(dr[0]);
                    obj.EmpWorkingHrId = Convert.ToInt32(dr[1]);
                    obj.empl_code = dr[2].ToString();
                    obj.EmpName = dr[3].ToString();
                    obj.WorkingDate = Convert.ToDateTime(dr[4].ToString().Trim());
                    obj.Remark = dr[5].ToString().Trim();
                    obj.WorkingHr = Convert.ToDecimal(dr[6]);
                    obj.RgDayOvtm = Convert.ToDecimal(dr[7]);
                    obj.PhOvtm = Convert.ToDecimal(dr[8]);
                    obj.StartDate = Convert.ToDateTime(dr[9].ToString().Trim());
                    obj.EndDate = Convert.ToDateTime(dr[10].ToString().Trim());
                    obj.Status = "";// dr[11].ToString().Trim();
                    lstEmployeeWorkHrDetails.Add(obj);
                }
                return lstEmployeeWorkHrDetails;
            }
        }
        public static int DeleteEmployeeWorkHrDetails(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[3];
            Parameter[0] = objEmployeeWorkHrDetails.EmpWorkingHrDetailsId;
            Parameter[1] = objEmployeeWorkHrDetails.EmpWorkingHrId;
            Parameter[2] = objEmployeeWorkHrDetails.WorkingDate;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOEmployeeWorkHrDetails), objEmployeeWorkHrDetails.DELETE_SPNAME);
            return c!=null? Convert.ToInt32(c):0;
        }

        public static List<DVOEmployeeWorkHrDetails> GetAllEmpTotalsInfo(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[4];
            Parameter[0] = objEmployeeWorkHrDetails.StartDate;
            Parameter[1] = objEmployeeWorkHrDetails.EndDate;
            Parameter[2] = null;
            Parameter[3] = null;
            List<DVOEmployeeWorkHrDetails> lstEmployeeWorkHrDetails = new List<DVOEmployeeWorkHrDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOEmployeeWorkHrDetails()).FIND_TOTALS_QUERY(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmployeeWorkHrDetails obj = new DVOEmployeeWorkHrDetails();
                    obj.empl_code = dr[0].ToString();
                    obj.EmpName = dr[1].ToString();
                    obj.Remark = dr[2].ToString().Trim();
                    obj.WorkingHr = Convert.ToDecimal(dr[3]);
                    obj.RgDayOvtm = Convert.ToDecimal(dr[4]);
                    obj.PhOvtm = Convert.ToDecimal(dr[5]);
                    obj.Status = dr[6].ToString().Trim();
                    obj.inc_code = dr[7].ToString().Trim();
                    obj.inc_rate = Convert.ToDecimal(dr[8]);
                    lstEmployeeWorkHrDetails.Add(obj);
                }
                return lstEmployeeWorkHrDetails;
            }
        }

        public static List<DVOEmployeeWorkHrDetails> GetEmpTotalsInfo(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[3];
            Parameter[0] = objEmployeeWorkHrDetails.StartDate;
            Parameter[1] = objEmployeeWorkHrDetails.EndDate;
            Parameter[2] = objEmployeeWorkHrDetails.empl_code;
            List<DVOEmployeeWorkHrDetails> lstEmployeeWorkHrDetails = new List<DVOEmployeeWorkHrDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOEmployeeWorkHrDetails()).FIND_TOTALS_QUERY(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmployeeWorkHrDetails obj = new DVOEmployeeWorkHrDetails();
                    obj.empl_code = dr[0].ToString();
                    obj.EmpName = dr[1].ToString();
                    obj.Remark = dr[2].ToString().Trim();
                    obj.WorkingHr = Convert.ToDecimal(dr[3]);
                    obj.RgDayOvtm = Convert.ToDecimal(dr[4]);
                    obj.PhOvtm = Convert.ToDecimal(dr[5]);
                    obj.Status = dr[6].ToString().Trim();

                    lstEmployeeWorkHrDetails.Add(obj);
                }
                return lstEmployeeWorkHrDetails;
            }
        }


        public static List<DVOUpdateTimeCard> GetEmpTcardIDInfo(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[3];
            Parameter[0] = objEmployeeWorkHrDetails.StartDate;
            Parameter[1] = objEmployeeWorkHrDetails.EndDate;
            Parameter[2] = objEmployeeWorkHrDetails.empl_code;
            List<DVOUpdateTimeCard> lstUpdateTimeCard = new List<DVOUpdateTimeCard>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOEmployeeWorkHrDetails()).FIND_EmpTcardID_QUERY(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard obj = new DVOUpdateTimeCard();
                    obj.RowID =Convert.ToInt32(dr[0]);

                    lstUpdateTimeCard.Add(obj);
                }
                return lstUpdateTimeCard;
            }
        }

        public static List<DVOIncomeCodeSettings> GetIncomeCodeSettingsInfo(ref DVOIncomeCodeSettings objIncomeCodeSettings)
        {
            object[] Parameter = new object[1];
            Parameter[0] = objIncomeCodeSettings.Applicable_for;
            List<DVOIncomeCodeSettings> lstIncomeCodeSettings = new List<DVOIncomeCodeSettings>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOEmployeeWorkHrDetails()).FIND_MasterIncomeCodeSettings_QUERY(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOIncomeCodeSettings obj = new DVOIncomeCodeSettings();
                    obj.incomecodesettingid = Convert.ToInt32(dr[0]);
                    obj.inc_code = dr[1].ToString();
                    obj.Applicable_for = dr[2].ToString();
                    lstIncomeCodeSettings.Add(obj);
                }
                return lstIncomeCodeSettings;
            }
        }


        public static int SetIncomeCodeSettingsInfo(ref DVOIncomeCodeSettings objIncomeCodeSettings)
        {


            object[] Parameter = new object[2];
            Parameter[0] = objIncomeCodeSettings.inc_code;
            Parameter[1] = objIncomeCodeSettings.Applicable_for;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.InsertData(ref Parameter, typeof(DVOIncomeCodeSettings), objIncomeCodeSettings.UPDATE_SPNAME);
            return Convert.ToInt32(c);
        }




        public static List<DVOEmployeeWorkHrDetails> GetEmployeeWorkingHrDetailsRpt(ref DVOEmployeeWorkHrDetails objEmployeeWorkHrDetails)
        {
            object[] Parameter = new object[3];
            Parameter[0] = objEmployeeWorkHrDetails.StartDate;
            Parameter[1] = objEmployeeWorkHrDetails.EndDate;
            Parameter[2] = objEmployeeWorkHrDetails.empl_code;
            List<DVOEmployeeWorkHrDetails> lstEmployeeWorkHrDetails = new List<DVOEmployeeWorkHrDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOEmployeeWorkHrDetails()).PRINT_QUERY_RPT(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmployeeWorkHrDetails obj = new DVOEmployeeWorkHrDetails();
                    obj.EmpWorkingHrDetailsId = Convert.ToInt32(dr[0]);
                    obj.EmpWorkingHrId = Convert.ToInt32(dr[1]);
                    obj.empl_code = dr[2].ToString();
                    obj.EmpName = dr[3].ToString();
                    obj.WorkingDate = Convert.ToDateTime(dr[4].ToString().Trim());
                    obj.Remark = dr[5].ToString().Trim();
                    obj.WorkingHr = Convert.ToDecimal(dr[6]);
                    obj.RgDayOvtm = Convert.ToDecimal(dr[7]);
                    obj.PhOvtm = Convert.ToDecimal(dr[8]);
                    obj.StartDate = Convert.ToDateTime(dr[9].ToString().Trim());
                    obj.EndDate = Convert.ToDateTime(dr[10].ToString().Trim());
                    obj.inc_rate = Convert.ToDecimal(dr[11]);
                    obj.Total = Convert.ToDecimal(dr[12]);
                    obj.Soc_Sec_Num = dr[13].ToString();
                    obj.Status = dr[14].ToString().Trim();
                    lstEmployeeWorkHrDetails.Add(obj);
                }
                return lstEmployeeWorkHrDetails;
            }
        }

    }
}
