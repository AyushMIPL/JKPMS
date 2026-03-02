using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class ProcessHolidayPayCal
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOProcessHolidayPayCal">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOProcessHolidayPayCal> GetData(ref DVOProcessHolidayPayCal pobjDVOProcessHolidayPayCal)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOProcessHolidayPayCal> listDVOProcessHolidayPayCal = new List<DVOProcessHolidayPayCal>();

            try
            {
                object[] parameters = new object[3];
                parameters[0] = pobjDVOProcessHolidayPayCal.ProcessHolidayPayCalId;
                parameters[1] = pobjDVOProcessHolidayPayCal.empl_code;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOProcessHolidayPayCal)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOProcessHolidayPayCal objDVOProcessHolidayPayCal = new DVOProcessHolidayPayCal();
                        objDVOProcessHolidayPayCal.RowId = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOProcessHolidayPayCal.ProcessHolidayPayCalId = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOProcessHolidayPayCal.empl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOProcessHolidayPayCal.LRD = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOProcessHolidayPayCal.start_date = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOProcessHolidayPayCal.end_date = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOProcessHolidayPayCal.TotalEarning = (dr[5] != DBNull.Value ? (decimal)dr[5] : 0);
                        objDVOProcessHolidayPayCal.Proportionate = (dr[6] != DBNull.Value ? (int)dr[6] : 0);
                        objDVOProcessHolidayPayCal.Calculation = (dr[7] != DBNull.Value ? (decimal)dr[7] : 0);
                        objDVOProcessHolidayPayCal.LessSocialSecurity = (dr[8] != DBNull.Value ? (decimal)dr[8] : 0);
                        objDVOProcessHolidayPayCal.NetHolidayPay = (dr[9] != DBNull.Value ? (decimal)dr[9] : 0);
                        objDVOProcessHolidayPayCal.WeekDays = (dr[10] != DBNull.Value ? (int)dr[9] : 0);
                        objDVOProcessHolidayPayCal.ExpectedToResumeDuty = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);

                        listDVOProcessHolidayPayCal.Add(objDVOProcessHolidayPayCal);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOProcessHolidayPayCal;
            }
            return listDVOProcessHolidayPayCal;
        }

        //Added by- neeraj* 
        //form -updateEmployeeTypes        
        public static int InsertData(ref object objTransaction, ref DVOProcessHolidayPayCal objProcessHolidayPayCal)
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
                object[] InsParameter = new object[12];
                InsParameter[0] = objProcessHolidayPayCal.ProcessHolidayPayCalId;
                InsParameter[1] = objProcessHolidayPayCal.empl_code;
                InsParameter[2] = objProcessHolidayPayCal.LRD;
                InsParameter[3] = objProcessHolidayPayCal.start_date;
                InsParameter[4] = objProcessHolidayPayCal.end_date;
                InsParameter[5] = objProcessHolidayPayCal.TotalEarning;
                InsParameter[6] = objProcessHolidayPayCal.Proportionate;
                InsParameter[7] = objProcessHolidayPayCal.Calculation;
                InsParameter[8] = objProcessHolidayPayCal.LessSocialSecurity;
                InsParameter[9] = objProcessHolidayPayCal.NetHolidayPay;
                InsParameter[10] = objProcessHolidayPayCal.WeekDays;
                InsParameter[11] = objProcessHolidayPayCal.ExpectedToResumeDuty;

                object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref InsParameter, (new DVOProcessHolidayPayCal()).INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                InsParameter = null;

                objDalBaseClass = null;
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
        //Added by- neeraj* 
        //form -FrmHolidayPay        
        public  static object   updateData(ref object objTransaction, ref DVOProcessHolidayPayCal objDVOProcessHolidayPayCal)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;

            try
            {
                object[] parameters = new object[12];
                parameters[0] = objDVOProcessHolidayPayCal.ProcessHolidayPayCalId;
                parameters[1] = objDVOProcessHolidayPayCal.empl_code;
                parameters[2] = objDVOProcessHolidayPayCal.LRD;
                parameters[3] = objDVOProcessHolidayPayCal.start_date;
                parameters[4] = objDVOProcessHolidayPayCal.end_date;
                parameters[5] = objDVOProcessHolidayPayCal.TotalEarning;
                parameters[6] = objDVOProcessHolidayPayCal.Proportionate;
                parameters[7] = objDVOProcessHolidayPayCal.Calculation;
                parameters[8] = objDVOProcessHolidayPayCal.LessSocialSecurity;
                parameters[9] = objDVOProcessHolidayPayCal.NetHolidayPay;
                parameters[10] = objDVOProcessHolidayPayCal.WeekDays;
                parameters[11] = objDVOProcessHolidayPayCal.ExpectedToResumeDuty;


                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOProcessHolidayPayCal), objDVOProcessHolidayPayCal.UPDATE_SPNAME);
                parameters = null;

                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                else
                    throw ex;

                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;


        }
        //Added by- neeraj* 
        //form -FrmHolidayPay        
        /// <summary>
        /// To Delete list of Employee Income codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public  static int  DeleteData(ref object objTransaction, ref List<DVOProcessHolidayPayCal> listDeleteDVOProcessHolidayPayCal)
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
                if (listDeleteDVOProcessHolidayPayCal.Count > 0)
                    foreach (DVOProcessHolidayPayCal objDVOPRDVOProcessHolidayPayCal in listDeleteDVOProcessHolidayPayCal)
                    {
                        DVOProcessHolidayPayCal obj = objDVOPRDVOProcessHolidayPayCal;
                        DeleteData(ref objTransaction, ref obj);
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

        //Added by- neeraj* 
        //form -FrmHolidayPay        
        /// <summary>
        /// To Delete process holiday pay
        /// </summary>
        /// <param name="objDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOProcessHolidayPayCal obDVOPRDVOProcessHolidayPayCal)
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
                parameters[0] = obDVOPRDVOProcessHolidayPayCal.ProcessHolidayPayCalId;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOProcessHolidayPayCal()).DELETE_SPNAME);
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

    

