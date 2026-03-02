using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLMasterEmployeeIncomes
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmployeeIncomes> GetData(ref DVOMasterEmployeeIncomes pobjDVOMasterEmployeeIncomes, string FlexDepartmentType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();

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
                if (pobjDVOMasterEmployeeIncomes.lastPay != null)
                    if (pobjDVOMasterEmployeeIncomes.lastPay.Trim().Length > 0)
                        pobjDVOMasterEmployeeIncomes.lastPay = Convert.ToDateTime(pobjDVOMasterEmployeeIncomes.lastPay).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[23] = pobjDVOMasterEmployeeIncomes.lastPay;
                //parameters[24] = "";

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeIncomes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
                        objDVOMasterEmployeeIncomes.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.inc_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeIncomes.inc_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeIncomes.inc_number = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                        objDVOMasterEmployeeIncomes.inc_hours = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                        objDVOMasterEmployeeIncomes.acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterEmployeeIncomes.department = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.inc_qtd1 = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd2 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd3 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd4 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeIncomes.inc_ytd = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeIncomes.lo_inc_amt = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                        objDVOMasterEmployeeIncomes.hi_inc_amt = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeIncomes.acct_no_kv = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.acct_no_typeid = (dr[16] != DBNull.Value ? Convert.ToInt32(dr[16]) : 0);
                        objDVOMasterEmployeeIncomes.acct_no_type = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);

                        if (objDVOMasterEmployeeIncomes.acct_no <= 0)
                        {
                            int _AccountNumber = 0, _AccountTypeId = 0;
                            string _AccountType = string.Empty, _AccountDescription = string.Empty;
                            objDVOMasterEmployeeIncomes.acct_no_type = FlexDepartmentType;
                            objDVOMasterEmployeeIncomes.acct_no_kv = GetFlexAccountKeyvalue(ref objDVOMasterEmployeeIncomes);
                            if (objDVOMasterEmployeeIncomes.acct_no_kv.Trim().Length > 0)
                            {
                                BLLCommonUtilities.GetAccountInformation(objDVOMasterEmployeeIncomes.acct_no_kv, out _AccountNumber, out _AccountType, out _AccountTypeId, out _AccountDescription);
                                if (_AccountNumber > 0)
                                {
                                    objDVOMasterEmployeeIncomes.acct_no = _AccountNumber;
                                    objDVOMasterEmployeeIncomes.acct_no_typeid = _AccountTypeId;
                                }
                                else
                                {
                                    objDVOMasterEmployeeIncomes.acct_no_type = string.Empty;
                                    objDVOMasterEmployeeIncomes.acct_no_kv = string.Empty;
                                    objDVOMasterEmployeeIncomes.acct_no = 0;
                                    objDVOMasterEmployeeIncomes.acct_no_typeid = 0;
                                }
                            }
                        }

                        listDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeIncomes;
            }
            return listDVOMasterEmployeeIncomes;
        }

        public static List<DVOMasterEmployeeIncomes> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmployeeIncomes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
                        objDVOMasterEmployeeIncomes.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.inc_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeIncomes.inc_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeIncomes.inc_number = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                        objDVOMasterEmployeeIncomes.inc_hours = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                        objDVOMasterEmployeeIncomes.acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterEmployeeIncomes.department = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeIncomes.inc_qtd1 = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd2 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd3 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeIncomes.inc_qtd4 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeIncomes.inc_ytd = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeIncomes.lo_inc_amt = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                        objDVOMasterEmployeeIncomes.hi_inc_amt = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeIncomes.acct_no_kv = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);

                        listDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeIncomes;
            }
            return listDVOMasterEmployeeIncomes;
        }

        /// <summary>
        /// To Insert New list of Employee Income Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeIncomes">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes)
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
                if (listDVOMasterEmployeeIncomes.Count > 0)
                    foreach (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes in listDVOMasterEmployeeIncomes)
                    {
                        DVOMasterEmployeeIncomes obj = objDVOMasterEmployeeIncomes;
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
        /// To Insert New Employee Income Code
        /// </summary>
        /// <param name="objDVOMasterEmployeeIncomes">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes, bool MaintainLog)
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
                object[] parameters = new object[17];
                parameters[0] = objDVOMasterEmployeeIncomes.empl_code;

                parameters[1] = objDVOMasterEmployeeIncomes.inc_code;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_code;
                        objtmp.update_by = objDVOMasterEmployeeIncomes.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[2] = objDVOMasterEmployeeIncomes.inc_rate;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_rate";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_rate.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[3] = objDVOMasterEmployeeIncomes.inc_number;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_number";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_number.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[4] = objDVOMasterEmployeeIncomes.inc_hours;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_hours";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_hours.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.InsertBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[5] = objDVOMasterEmployeeIncomes.acct_no;
                parameters[6] = objDVOMasterEmployeeIncomes.department;
                parameters[7] = objDVOMasterEmployeeIncomes.inc_qtd1;
                parameters[8] = objDVOMasterEmployeeIncomes.inc_qtd2;
                parameters[9] = objDVOMasterEmployeeIncomes.inc_qtd3;
                parameters[10] = objDVOMasterEmployeeIncomes.inc_qtd4;
                parameters[11] = objDVOMasterEmployeeIncomes.inc_ytd;
                parameters[12] = objDVOMasterEmployeeIncomes.lo_inc_amt;
                parameters[13] = objDVOMasterEmployeeIncomes.hi_inc_amt;
                parameters[14] = objDVOMasterEmployeeIncomes.InsertMachineInfo;
                if (objDVOMasterEmployeeIncomes.InsertDate == null || objDVOMasterEmployeeIncomes.InsertDate.Trim().Length <= 0)
                    objDVOMasterEmployeeIncomes.InsertDate = "01/01/1900";
                parameters[15] = objDVOMasterEmployeeIncomes.InsertDate;
                parameters[16] = objDVOMasterEmployeeIncomes.InsertBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);
                
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeIncomes.INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeIncomes";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeIncomes.inc_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeIncomes.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeIncomes.acct_no_kv;
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
        /// To Update list of Employee Income Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes, ref List<DVOMasterEmployeeIncomes> listPreDVOMasterEmployeeIncomes)
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
                if (listDVOMasterEmployeeIncomes.Count > 0)
                    foreach (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes in listDVOMasterEmployeeIncomes)
                    {
                        DVOMasterEmployeeIncomes obj = objDVOMasterEmployeeIncomes;
                        if (obj.line_no > 0)
                        {
                            DVOMasterEmployeeIncomes objPre =
                                listPreDVOMasterEmployeeIncomes.Find(delegate(DVOMasterEmployeeIncomes objDel)
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
        /// To Update Employee Income Codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes, ref DVOMasterEmployeeIncomes objPreDVOMasterEmployeeIncomes)
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
                object[] parameters = new object[18];
                parameters[0] = objDVOMasterEmployeeIncomes.line_no;
                parameters[1] = objDVOMasterEmployeeIncomes.empl_code;

                parameters[2] = objDVOMasterEmployeeIncomes.inc_code;
                if (objDVOMasterEmployeeIncomes.inc_code.Trim() != objPreDVOMasterEmployeeIncomes.inc_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_code";
                        objtmp.old_value = objPreDVOMasterEmployeeIncomes.inc_code;
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_code;
                        objtmp.update_by = objDVOMasterEmployeeIncomes.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[3] = objDVOMasterEmployeeIncomes.inc_rate;
                if (objDVOMasterEmployeeIncomes.inc_rate != objPreDVOMasterEmployeeIncomes.inc_rate)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_rate";
                        objtmp.old_value = objPreDVOMasterEmployeeIncomes.inc_rate.ToString();
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_rate.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[4] = objDVOMasterEmployeeIncomes.inc_number;
                if (objDVOMasterEmployeeIncomes.inc_number != objPreDVOMasterEmployeeIncomes.inc_number)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_number";
                        objtmp.old_value = objPreDVOMasterEmployeeIncomes.inc_number.ToString();
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_number.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[5] = objDVOMasterEmployeeIncomes.inc_hours;
                if (objDVOMasterEmployeeIncomes.inc_hours != objPreDVOMasterEmployeeIncomes.inc_hours)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeIncomes.empl_code;
                        objtmp.field_name = "INCOME - inc_hours";
                        objtmp.old_value = objPreDVOMasterEmployeeIncomes.inc_hours.ToString();
                        objtmp.new_value = objDVOMasterEmployeeIncomes.inc_hours.ToString();
                        objtmp.update_by = objDVOMasterEmployeeIncomes.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmployeeIncomes.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeIncomes.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[6] = objDVOMasterEmployeeIncomes.acct_no;
                parameters[7] = objDVOMasterEmployeeIncomes.department;
                parameters[8] = objDVOMasterEmployeeIncomes.inc_qtd1;
                parameters[9] = objDVOMasterEmployeeIncomes.inc_qtd2;
                parameters[10] = objDVOMasterEmployeeIncomes.inc_qtd3;
                parameters[11] = objDVOMasterEmployeeIncomes.inc_qtd4;
                parameters[12] = objDVOMasterEmployeeIncomes.inc_ytd;
                parameters[13] = objDVOMasterEmployeeIncomes.lo_inc_amt;
                parameters[14] = objDVOMasterEmployeeIncomes.hi_inc_amt;
                parameters[15] = objDVOMasterEmployeeIncomes.UpdateMachineInfo;
                if (objDVOMasterEmployeeIncomes.UpdateDate == null || objDVOMasterEmployeeIncomes.UpdateDate.Trim().Length <= 0)
                    objDVOMasterEmployeeIncomes.UpdateDate = "01/01/1900";
                parameters[16] = objDVOMasterEmployeeIncomes.UpdateDate;
                parameters[17] = objDVOMasterEmployeeIncomes.UpdateBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeIncomes.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeIncomes";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeIncomes.inc_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeIncomes.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeIncomes.acct_no_kv;
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
        /// To Delete list of Employee Income codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes)
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
                if (listDVOMasterEmployeeIncomes.Count > 0)
                    foreach (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes in listDVOMasterEmployeeIncomes)
                    {
                        DVOMasterEmployeeIncomes obj = objDVOMasterEmployeeIncomes;
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
        /// To Delete Employee Income codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes)
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
                parameters[0] = objDVOMasterEmployeeIncomes.line_no;
                parameters[1] = objDVOMasterEmployeeIncomes.empl_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeIncomes.DELETE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeIncomes";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeIncomes.inc_code;
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
        public static string GetFlexAccountKeyvalue(ref DVOMasterEmployeeIncomes pobjDVOMasterEmployeeIncomes)
        {
            string _FlexAccountKeyvalue = string.Empty;
            try
            {
                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeIncomes";
                objDVOFlexSegCommon.Code = pobjDVOMasterEmployeeIncomes.inc_code;
                objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployeeIncomes.acct_no_type;
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
