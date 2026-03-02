using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLMasterEmployeeDeductions
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmployeeDeductions> GetData(ref DVOMasterEmployeeDeductions pobjDVOMasterEmployeeDeductions, string FlexDepartmentType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();

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
                if (pobjDVOMasterEmployeeDeductions.ded_date != null)
                    if (pobjDVOMasterEmployeeDeductions.ded_date.Trim().Length > 0)
                        parameters[13] = Convert.ToDateTime(pobjDVOMasterEmployeeDeductions.ded_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
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
                if (pobjDVOMasterEmployeeDeductions.lastPay != null)
                    if (pobjDVOMasterEmployeeDeductions.lastPay.Trim().Length > 0)
                        pobjDVOMasterEmployeeDeductions.lastPay = Convert.ToDateTime(pobjDVOMasterEmployeeDeductions.lastPay).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[26] = pobjDVOMasterEmployeeDeductions.lastPay;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
                        objDVOMasterEmployeeDeductions.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.ded_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeDeductions.ded_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeDeductions.ded_limit = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                         objDVOMasterEmployeeDeductions.ded_apply = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterEmployeeDeductions.department = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.ded_qtd1 = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd2 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd3 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd4 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeDeductions.ded_ytd = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeDeductions.ded_date = ((dr[13] != DBNull.Value && dr[13].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[13]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        objDVOMasterEmployeeDeductions.lo_ded_amt = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeDeductions.hi_ded_amt = (dr[15] != DBNull.Value ? (decimal?)(dr[15]) : null);
                        objDVOMasterEmployeeDeductions.pay_limit = (dr[16] != DBNull.Value ? (decimal?)(dr[16]) : null);
                        objDVOMasterEmployeeDeductions.balanceamt = (dr[17] != DBNull.Value ? (decimal?)(dr[17]) : null);
                        objDVOMasterEmployeeDeductions.acct_no_kv = (dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.acct_no_typeid = (dr[19] != DBNull.Value ? Convert.ToInt32(dr[19]) : 0);
                        objDVOMasterEmployeeDeductions.acct_no_type = (dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty);

                        if (objDVOMasterEmployeeDeductions.acct_no <= 0)
                        {
                            int _AccountNumber = 0, _AccountTypeId = 0;
                            string _AccountType = string.Empty, _AccountDescription = string.Empty;
                            objDVOMasterEmployeeDeductions.acct_no_type = FlexDepartmentType;
                            objDVOMasterEmployeeDeductions.acct_no_kv = GetFlexAccountKeyvalue(ref objDVOMasterEmployeeDeductions);
                            if (objDVOMasterEmployeeDeductions.acct_no_kv.Trim().Length > 0)
                            {
                                BLLCommonUtilities.GetAccountInformation(objDVOMasterEmployeeDeductions.acct_no_kv, out _AccountNumber, out _AccountType, out _AccountTypeId, out _AccountDescription);
                                if (_AccountNumber > 0)
                                {
                                    objDVOMasterEmployeeDeductions.acct_no = _AccountNumber;
                                    objDVOMasterEmployeeDeductions.acct_no_typeid = _AccountTypeId;
                                }
                                else
                                {
                                    objDVOMasterEmployeeDeductions.acct_no_type = string.Empty;
                                    objDVOMasterEmployeeDeductions.acct_no_kv = string.Empty;
                                    objDVOMasterEmployeeDeductions.acct_no = 0;
                                    objDVOMasterEmployeeDeductions.acct_no_typeid = 0;
                                }
                            }
                        }
                        listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeDeductions;
            }
            return listDVOMasterEmployeeDeductions;
        }

        public static List<DVOMasterEmployeeDeductions> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmployeeDeductions)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
                        objDVOMasterEmployeeDeductions.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.ded_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeDeductions.ded_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeDeductions.ded_limit = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                        objDVOMasterEmployeeDeductions.ded_apply = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterEmployeeDeductions.department = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeDeductions.ded_qtd1 = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd2 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd3 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeDeductions.ded_qtd4 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeDeductions.ded_ytd = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeDeductions.ded_date = ((dr[13] != DBNull.Value && dr[13].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[13]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        objDVOMasterEmployeeDeductions.lo_ded_amt = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeDeductions.hi_ded_amt = (dr[15] != DBNull.Value ? (decimal?)(dr[15]) : null);
                        objDVOMasterEmployeeDeductions.pay_limit = (dr[16] != DBNull.Value ? (decimal?)(dr[16]) : null);
                        objDVOMasterEmployeeDeductions.balanceamt = (dr[17] != DBNull.Value ? (decimal?)(dr[17]) : null);
                        objDVOMasterEmployeeDeductions.acct_no_kv = (dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty);

                        listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeDeductions;
            }
            return listDVOMasterEmployeeDeductions;
        }

        /// <summary>
        /// To Insert New list of Employee Deduction Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeDeductions">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions)
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
                if(listDVOMasterEmployeeDeductions.Count>0)
                    foreach (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions in listDVOMasterEmployeeDeductions)
                    {
                        DVOMasterEmployeeDeductions obj = objDVOMasterEmployeeDeductions;
                        InsertData(ref objTransaction, ref obj, false);
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

        /// <summary>
        /// To Insert New Employee Deduction Codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeDeductions">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions, bool MaintainLog)
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

                #region Parameters
                object[] parameters = new object[20];
                parameters[0] = objDVOMasterEmployeeDeductions.empl_code;

                parameters[1] = objDVOMasterEmployeeDeductions.ded_code;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_code;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[2] = objDVOMasterEmployeeDeductions.ded_rate;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_rate";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_rate.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[3] = objDVOMasterEmployeeDeductions.ded_limit;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_limit";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[4] = objDVOMasterEmployeeDeductions.ded_apply;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_apply";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_apply;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[5] = objDVOMasterEmployeeDeductions.acct_no;
                parameters[6] = objDVOMasterEmployeeDeductions.department;
                parameters[7] = objDVOMasterEmployeeDeductions.ded_qtd1;
                parameters[8] = objDVOMasterEmployeeDeductions.ded_qtd2;
                parameters[9] = objDVOMasterEmployeeDeductions.ded_qtd3;
                parameters[10] = objDVOMasterEmployeeDeductions.ded_qtd4;
                parameters[11] = objDVOMasterEmployeeDeductions.ded_ytd;

                if (objDVOMasterEmployeeDeductions.ded_date == null || objDVOMasterEmployeeDeductions.ded_date.Trim().Length <= 0)
                    objDVOMasterEmployeeDeductions.ded_date = "01/01/1900";
                parameters[12] = objDVOMasterEmployeeDeductions.ded_date;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_date";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_date;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[13] = objDVOMasterEmployeeDeductions.lo_ded_amt;
                parameters[14] = objDVOMasterEmployeeDeductions.hi_ded_amt;

                parameters[15] = objDVOMasterEmployeeDeductions.pay_limit;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - pay_limit";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeDeductions.pay_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[16] = objDVOMasterEmployeeDeductions.balanceamt;
                parameters[17] = objDVOMasterEmployeeDeductions.InsertMachineInfo;
                if (objDVOMasterEmployeeDeductions.InsertDate == null || objDVOMasterEmployeeDeductions.InsertDate.Trim().Length <= 0)
                    objDVOMasterEmployeeDeductions.InsertDate = "01/01/1900";
                parameters[18] = objDVOMasterEmployeeDeductions.InsertBy;
                parameters[19] = objDVOMasterEmployeeDeductions.InsertDate;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeDeductions.INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeDeductions";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeDeductions.ded_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeDeductions.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeDeductions.acct_no_kv;
                BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

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
        /// To Update list of Employee Deduction Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions, ref List<DVOMasterEmployeeDeductions> listPreDVOMasterEmployeeDeductions)
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
                if(listDVOMasterEmployeeDeductions.Count>0)
                    foreach (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions in listDVOMasterEmployeeDeductions)
                    {
                        DVOMasterEmployeeDeductions obj = objDVOMasterEmployeeDeductions;
                        if (obj.line_no > 0)
                        {
                            DVOMasterEmployeeDeductions objPre =
                                listPreDVOMasterEmployeeDeductions.Find(delegate(DVOMasterEmployeeDeductions objDel)
                            {
                                if (objDel.line_no == obj.line_no)
                                    return true;
                                return false;
                            });
                            UpdateData(ref objTransaction, ref obj, ref objPre);
                        }
                        else
                            InsertData(ref objTransaction, ref obj, true);
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

        /// <summary>
        /// To Update Employee Deduction Codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions, ref DVOMasterEmployeeDeductions objPreDVOMasterEmployeeDeductions)
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

                #region Parameters
                object[] parameters = new object[21];
                parameters[0] = objDVOMasterEmployeeDeductions.line_no;
                parameters[1] = objDVOMasterEmployeeDeductions.empl_code;

                parameters[2] = objDVOMasterEmployeeDeductions.ded_code;
                if (objDVOMasterEmployeeDeductions.ded_code.Trim() != objPreDVOMasterEmployeeDeductions.ded_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_code";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.ded_code;
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_code;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[3] = objDVOMasterEmployeeDeductions.ded_rate;
                if (objDVOMasterEmployeeDeductions.ded_rate != objPreDVOMasterEmployeeDeductions.ded_rate)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_rate";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.ded_rate.ToString();
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_rate.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[4] = objDVOMasterEmployeeDeductions.ded_limit;
                if (objDVOMasterEmployeeDeductions.ded_limit != objPreDVOMasterEmployeeDeductions.ded_limit)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_limit";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.ded_limit.ToString();
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[5] = objDVOMasterEmployeeDeductions.ded_apply;
                if (objDVOMasterEmployeeDeductions.ded_apply != objPreDVOMasterEmployeeDeductions.ded_apply)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_apply";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.ded_apply;
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_apply;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[6] = objDVOMasterEmployeeDeductions.acct_no;
                parameters[7] = objDVOMasterEmployeeDeductions.department;
                parameters[8] = objDVOMasterEmployeeDeductions.ded_qtd1;
                parameters[9] = objDVOMasterEmployeeDeductions.ded_qtd2;
                parameters[10] = objDVOMasterEmployeeDeductions.ded_qtd3;
                parameters[11] = objDVOMasterEmployeeDeductions.ded_qtd4;
                parameters[12] = objDVOMasterEmployeeDeductions.ded_ytd;

                if (objDVOMasterEmployeeDeductions.ded_date == null || objDVOMasterEmployeeDeductions.ded_date.Trim().Length <= 0)
                    objDVOMasterEmployeeDeductions.ded_date = "01/01/1900";
                if (objPreDVOMasterEmployeeDeductions.ded_date == null || objPreDVOMasterEmployeeDeductions.ded_date.Trim().Length <= 0)
                    objPreDVOMasterEmployeeDeductions.ded_date = "01/01/1900";
                parameters[13] = objDVOMasterEmployeeDeductions.ded_date;
                if (objDVOMasterEmployeeDeductions.ded_date != objPreDVOMasterEmployeeDeductions.ded_date)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - ded_date";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.ded_date;
                        objtmp.new_value = objDVOMasterEmployeeDeductions.ded_date;
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[14] = objDVOMasterEmployeeDeductions.lo_ded_amt;
                parameters[15] = objDVOMasterEmployeeDeductions.hi_ded_amt;

                parameters[16] = objDVOMasterEmployeeDeductions.pay_limit;
                if (objDVOMasterEmployeeDeductions.pay_limit != objPreDVOMasterEmployeeDeductions.pay_limit)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeDeductions.empl_code;
                        objtmp.field_name = "DEDUCTION - pay_limit";
                        objtmp.old_value = objPreDVOMasterEmployeeDeductions.pay_limit.ToString();
                        objtmp.new_value = objDVOMasterEmployeeDeductions.pay_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeDeductions.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeDeductions.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[17] = objDVOMasterEmployeeDeductions.balanceamt;
                parameters[18] = objDVOMasterEmployeeDeductions.UpdateMachineInfo;
                if (objDVOMasterEmployeeDeductions.UpdateDate == null || objDVOMasterEmployeeDeductions.UpdateDate.Trim().Length <= 0)
                    objDVOMasterEmployeeDeductions.UpdateDate = "01/01/1900";
                parameters[19] = objDVOMasterEmployeeDeductions.UpdateDate;
                parameters[20] = objDVOMasterEmployeeDeductions.UpdateBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeDeductions.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeDeductions";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeDeductions.ded_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeDeductions.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeDeductions.acct_no_kv;
                BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

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
        /// To Delete list of Employee Deduction Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions)
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
                if (listDVOMasterEmployeeDeductions.Count > 0)
                    foreach (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions in listDVOMasterEmployeeDeductions)
                    {
                        DVOMasterEmployeeDeductions obj = objDVOMasterEmployeeDeductions;
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

        /// <summary>
        /// To Delete Employee Deduction Codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions)
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
                parameters[0] = objDVOMasterEmployeeDeductions.line_no;
                parameters[1] = objDVOMasterEmployeeDeductions.empl_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeDeductions.DELETE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeDeductions";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeDeductions.ded_code;
                BLLFlexSegCommon.Flexseg_delete(ref objDVOFlexSegCommon);

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
        /// Get Keyvalue for flex-account-type of employee
        /// </summary>
        /// <param name="pobjDVOMasterEmployee"></param>
        /// <returns></returns>
        public static string GetFlexAccountKeyvalue(ref DVOMasterEmployeeDeductions pobjDVOMasterEmployeeDeductions)
        {
            string _FlexAccountKeyvalue = string.Empty;
            try
            {
                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeDeductions";
                objDVOFlexSegCommon.Code = pobjDVOMasterEmployeeDeductions.ded_code;
                objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployeeDeductions.acct_no_type;
                _FlexAccountKeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
                objDVOFlexSegCommon = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _FlexAccountKeyvalue;
        }

    }
}
