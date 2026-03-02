using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Linq;
using System.IO;

namespace JKPS.BLL
{
    public class BLLPYBatchProcessStybatchr
    {
        public static int INSERTBatchProcessInfo(ref DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrINS)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();

            try
            {
                object[] Parameters = new object[5];
                //Parameters[0] = objDVOPYBatchProcessStybatchrINS.startedon;
                //Parameters[1] = objDVOPYBatchProcessStybatchrINS.status;
                Parameters[0] = objDVOPYBatchProcessStybatchrINS.searchcriteria;
                Parameters[1] = objDVOPYBatchProcessStybatchrINS.insertby;
                Parameters[2] = objDVOPYBatchProcessStybatchrINS.insertmachineinfo;
                Parameters[3] = objDVOPYBatchProcessStybatchrINS.startedon;
                Parameters[4] = objDVOPYBatchProcessStybatchrINS.Districts;
                //Parameters[3] = objDVOPYBatchProcessStybatchrINS.startedon;

                object objres = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref Parameters, objDVOPYBatchProcessStybatchrINS.INSERT_SPNAME);
                if (!(objres != DBNull.Value && objres != null && objres.ToString().Trim().Length > 0))// 
                    throw new Exception("Error occured during inserting of new Payroll Batch Header.");
                if (Convert.ToInt32(objres) < 1)
                    throw new Exception("Error occured during inserting of new Payroll Batch Header.");

                //success = objDALBaseClass.InsertData(ref Parameters, typeof(DVOPYBatchProcessStybatchr), true);
                //if (success == null)
                //    throw new Exception();
                //else if (Convert.ToInt16(success) < 1)
                //    throw new Exception();
                Parameters = null;
                objDALBaseClass = null;

                if (objTransaction != null)
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

        public static List<DVOPYBatchProcessStybatchr> GetActiveBatch()
        {
            StringBuilder sb = new StringBuilder();
            List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = new List<DVOPYBatchProcessStybatchr>();
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                using (DataSet ds = objDALBaseClass.GetData(typeof(DVOPYBatchProcessStybatchr), (new DVOPYBatchProcessStybatchr()).GET_ACTIVE_BATCHES))
                {//(ref parameter,  objDVOPYBatchProcessStybatchrGET.FIND_QUERY);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOPYBatchProcessStybatchr obj = new DVOPYBatchProcessStybatchr();
                            obj.pybatchid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                            sb.AppendLine(dr[0] != DBNull.Value ? dr[0].ToString() : string.Empty);
                            sb.AppendLine(dr[1] != DBNull.Value ? dr[1].ToString() : string.Empty);
                            sb.AppendLine(dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty);
                            sb.AppendLine(dr[4] != DBNull.Value ? dr[4].ToString() : string.Empty);
                            sb.AppendLine(dr[5] != DBNull.Value ? dr[5].ToString() : string.Empty);
                            sb.AppendLine(dr[6] != DBNull.Value ? dr[6].ToString() : string.Empty);
                            sb.AppendLine(dr[7] != DBNull.Value ? dr[7].ToString() : string.Empty);
                            sb.AppendLine(dr[8] != DBNull.Value ? dr[8].ToString() : string.Empty);

                            //obj.startedon = (dr[1] != DBNull.Value ? Convert.ToDateTime(dr[1]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                            obj.startedon = (dr[1] != DBNull.Value ? Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy") : string.Empty); //DVOApplicationUserInfo.ParseDateConvertionStr(dr[1].ToString()) : string.Empty);// Convert.ToDateTime(dr[1]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                            sb.AppendLine(dr[1] != DBNull.Value ? dr[1].ToString() : string.Empty);                                                                                                          //obj.endedon = (dr[2] != DBNull.Value ? Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);

                            obj.endedon = (dr[2] != DBNull.Value ? DVOApplicationUserInfo.ParseDateConvertionStr(dr[2].ToString()) : string.Empty);// Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                            sb.AppendLine(dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty);

                            //obj.insertdate = (dr[5] != DBNull.Value ? Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                            obj.insertdate = (dr[5] != DBNull.Value ? DVOApplicationUserInfo.ParseDateConvertionStr(dr[5].ToString()) : string.Empty);// Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                            sb.AppendLine(dr[5] != DBNull.Value ? dr[5].ToString() : string.Empty);

                            obj.insertmachineinfo = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]) : string.Empty);
                            obj.searchcriteria = (dr[7] != DBNull.Value ? Convert.ToString(dr[7]) : string.Empty);
                            obj.Districts = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]) : string.Empty);
                            lstDVOPYBatchProcessStybatchr.Add(obj);
                        }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);

                //WriteTextToFile(sb.ToString());
            }
            return lstDVOPYBatchProcessStybatchr;
        }

        public static void WriteTextToFile(string Text)
        {
            try
            {
                //string path = @"D:\J & K\App.Web\Log file\jkps_log.txt";// Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log file", "jkps_log.txt");
                string path = @"C:\inetpub\wwwroot\JKPS_Web\Log file\jkps_log.txt";// Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log file", "jkps_log.txt");
                File.WriteAllText(path, Text);
            }
            catch (Exception ex)
            {

            }
        }

        public static List<DVOPYBatchProcessStybatchr> GetLastActiveBatch()
        {
            //DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrGET=new DVOPYBatchProcessStybatchr();
            List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = new List<DVOPYBatchProcessStybatchr>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDALBaseClass.GetData("select * from(SELECT TOP 1 PPH.pybatchid,PPH.startedon,PPH.endedon,PPH.[status],PPH.insertby,PPH.insertdate,PPH.insertmachineinfo,(CASE WHEN PPD.searchcriteria IS NULL THEN PPH.searchcriteria ELSE PPD.searchcriteria END ) AS searchcriteria, CASE WHEN PPH.searchcriteria LIKE '%Payroll Date: %' THEN (CONVERT(DATETIME, SUBSTRING(PPH.searchcriteria, CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103)) ELSE GETDATE() END PayrollDate,PPH.Districts,MONTH(CONVERT(DATETIME, SUBSTRING(PPH.searchcriteria, CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11),103))PayrollMonth,YEAR(CONVERT(DATETIME, SUBSTRING(PPH.searchcriteria, CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103))PayrollYear from Payroll_Process_Header PPH LEFT JOIN [dbo].[Payroll_Process_Details] PPD ON PPD.pybatchid = PPH.pybatchid ORDER BY PPD.processstartedon DESC) as tt where 1 = (SELECT CASE WHEN SUM(inc_gross) > 0 THEN 1 ELSE 0 END FROM Process_Payemployee WHERE ok_to_post IN ('Y','N','P') and month(pay_date) = tt.PayrollMonth AND year(pay_date) = tt.PayrollYear) "))
            {//(ref parameter,  objDVOPYBatchProcessStybatchrGET.FIND_QUERY);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOPYBatchProcessStybatchr obj = new DVOPYBatchProcessStybatchr();
                        obj.pybatchid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        obj.startedon = (dr[1] != DBNull.Value ? Convert.ToDateTime(dr[1]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        obj.endedon = (dr[2] != DBNull.Value ? Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        obj.status = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        obj.insertby = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                        obj.insertdate = (dr[5] != DBNull.Value ? Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        obj.insertmachineinfo = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]) : string.Empty);
                        obj.searchcriteria = (dr[7] != DBNull.Value ? Convert.ToString(dr[7]) : string.Empty);
                        obj.processedStartedOn = (dr[8] != DBNull.Value ? Convert.ToDateTime(dr[8]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        lstDVOPYBatchProcessStybatchr.Add(obj);
                    }
            }
            return lstDVOPYBatchProcessStybatchr;
        }

        public static Dictionary<string, string> PensionDetailByMonthYear(DateTime? PayDate = null)
        {
            //DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrGET=new DVOPYBatchProcessStybatchr();
            Dictionary<string, string> keyValuePairs = null;
            DVOPYBatchProcessStybatchr lstDVOPYBatchProcessStybatchr = GetLastActiveBatch().OrderByDescending(x => x.insertdate).FirstOrDefault();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] Parameters = new object[2];
            if (lstDVOPYBatchProcessStybatchr != null && PayDate == null)
            {
                Parameters[0] = DVOApplicationUserInfo.ParseDateConvertion(lstDVOPYBatchProcessStybatchr.processedStartedOn).Month;//DateTime.Now.Month;
                Parameters[1] = DVOApplicationUserInfo.ParseDateConvertion(lstDVOPYBatchProcessStybatchr.processedStartedOn).Year; //DateTime.Now.Year;
            }
            else
            {
                if (PayDate != null)
                {
                    Parameters[0] = PayDate.Value.Month;
                    Parameters[1] = PayDate.Value.Year;
                }
                else
                {
                    Parameters[0] = DateTime.Now.Month;
                    Parameters[1] = DateTime.Now.Year;
                }

            }
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            using (DataSet ds = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref Parameters, "PensionDetailByMonthYear"))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    keyValuePairs = new Dictionary<string, string>
                {
                    { "UploadedFileName", ds.Tables[0].Rows[0]["UploadedFileName"].ToString() },
                    { "UploadedBy", ds.Tables[0].Rows[0]["UploadedBy"].ToString() },
                    { "UploadedOn", ds.Tables[0].Rows[0]["UploadedOn"].ToString() },
                    { "FileName", ds.Tables[0].Rows[0]["FileName"].ToString() },
                    { "UpdateResponseOn", ds.Tables[0].Rows[0]["UpdateResponseOn"].ToString() },
                    { "recordsprocessed", ds.Tables[0].Rows[0]["recordsprocessed"].ToString() },
                    { "pybatchid", ds.Tables[0].Rows[0]["pybatchid"].ToString() },
                    { "BatchCreatedBy", ds.Tables[0].Rows[0]["BatchCreatedBy"].ToString() },
                    { "insertDate", ds.Tables[0].Rows[0]["insertDate"].ToString() },
                    { "totalAmount", Convert.ToDecimal(ds.Tables[0].Rows[0]["totalAmount"].ToString()).ToString("0.00") },
                    { "totalCanceled", Convert.ToInt32(ds.Tables[0].Rows[0]["totalCanceled"].ToString()).ToString("0") },
                    { "Districts", ds.Tables[0].Rows[0]["Districts"].ToString() },
                };
                }
            }
            return keyValuePairs;
        }
        public static int UPDATEBatchProcessStatus(ref DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrUPD)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] Parameters = new object[3];
                Parameters[0] = objDVOPYBatchProcessStybatchrUPD.pybatchid;
                Parameters[1] = objDVOPYBatchProcessStybatchrUPD.updateby;
                Parameters[2] = objDVOPYBatchProcessStybatchrUPD.updatemachineinfo;

                object objres = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref Parameters, objDVOPYBatchProcessStybatchrUPD.UPDATE_SPNAME);
                if (!(objres != DBNull.Value && objres != null && objres.ToString().Trim().Length > 0))// 
                    throw new Exception("Error occured during updating of Payroll Batch Status.");
                if (Convert.ToInt32(objres) < 1)
                    throw new Exception("Error occured during updating of Payroll Batch Status.");
                Parameters = null;
                objDALBaseClass = null;
                if (objTransaction != null)
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

        public static int CANCELBatchProcessStatus(ref DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrUPD)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] Parameters = new object[3];
                Parameters[0] = objDVOPYBatchProcessStybatchrUPD.pybatchid;
                Parameters[1] = objDVOPYBatchProcessStybatchrUPD.updateby;
                Parameters[2] = objDVOPYBatchProcessStybatchrUPD.updatemachineinfo;

                object objres = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref Parameters, objDVOPYBatchProcessStybatchrUPD.DELETE_SPNAME);
                if (!(objres != DBNull.Value && objres != null && objres.ToString().Trim().Length > 0))// 
                    throw new Exception("Error occured during Cancellation of Payroll Batch Status.");
                if (Convert.ToInt32(objres) < 1)
                    throw new Exception("Error occured during Cancellation of Payroll Batch Status.");
                Parameters = null;
                objDALBaseClass = null;
                if (objTransaction != null)
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


        public static DataSet GetChecks(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                ds = objDALBaseClass.GetData((new DVOPYBatchProcessStybatchr()).FIND_CHECKS(ref parameters));
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].Columns.Add("empl_name", typeof(string), "trim(empl_code) + ' - ' + trim(last_name) + ', ' + trim(first_name)");
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
    }
}
