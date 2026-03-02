using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLMasterEmployeeObligations
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="pobjDVOMasterEmployeeObligations">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmployeeObligations> GetData(ref DVOMasterEmployeeObligations pobjDVOMasterEmployeeObligations, string FlexDepartmentType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();

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
                if (pobjDVOMasterEmployeeObligations.lastPay != null)
                    if (pobjDVOMasterEmployeeObligations.lastPay.Trim().Length > 0)
                        pobjDVOMasterEmployeeObligations.lastPay = Convert.ToDateTime(pobjDVOMasterEmployeeObligations.lastPay).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[23] = pobjDVOMasterEmployeeObligations.lastPay;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
                        objDVOMasterEmployeeObligations.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.obl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeObligations.obl_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeObligations.obl_limit = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null); ;
                        objDVOMasterEmployeeObligations.acct_no = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);
                        objDVOMasterEmployeeObligations.department = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.bal_acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOMasterEmployeeObligations.bal_dept = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.obl_qtd1 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd2 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd3 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd4 = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeObligations.obl_ytd = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                        objDVOMasterEmployeeObligations.pay_limit = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeObligations.acct_no_kv = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.bal_acct_no_kv = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.acct_no_typeid = (dr[17] != DBNull.Value ? Convert.ToInt32(dr[17]) : 0);
                        objDVOMasterEmployeeObligations.bal_acct_no_typeid = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                        objDVOMasterEmployeeObligations.acct_no_type = (dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.bal_acct_no_type = (dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty);

                        if (objDVOMasterEmployeeObligations.acct_no <= 0)
                        {
                            int _AccountNumber = 0, _AccountTypeId = 0;
                            string _AccountType = string.Empty, _AccountDescription = string.Empty;
                            objDVOMasterEmployeeObligations.acct_no_type = FlexDepartmentType;
                            objDVOMasterEmployeeObligations.acct_no_kv = GetFlexExpAccountKeyvalue(ref objDVOMasterEmployeeObligations);
                            if (objDVOMasterEmployeeObligations.acct_no_kv.Trim().Length > 0)
                            {
                                BLLCommonUtilities.GetAccountInformation(objDVOMasterEmployeeObligations.acct_no_kv, out _AccountNumber, out _AccountType, out _AccountTypeId, out _AccountDescription);
                                if (_AccountNumber > 0)
                                {
                                    objDVOMasterEmployeeObligations.acct_no = _AccountNumber;
                                    objDVOMasterEmployeeObligations.acct_no_typeid = _AccountTypeId;
                                }
                                else
                                {
                                    objDVOMasterEmployeeObligations.acct_no_type = string.Empty;
                                    objDVOMasterEmployeeObligations.acct_no_kv = string.Empty;
                                    objDVOMasterEmployeeObligations.acct_no = 0;
                                    objDVOMasterEmployeeObligations.acct_no_typeid = 0;
                                }
                            }
                        }

                        if (objDVOMasterEmployeeObligations.bal_acct_no <= 0)
                        {
                            int _AccountNumber = 0, _AccountTypeId = 0;
                            string _AccountType = string.Empty, _AccountDescription = string.Empty;
                            objDVOMasterEmployeeObligations.bal_acct_no_type = FlexDepartmentType;
                            objDVOMasterEmployeeObligations.bal_acct_no_kv = GetFlexLiabAccountKeyvalue(ref objDVOMasterEmployeeObligations);
                            if (objDVOMasterEmployeeObligations.bal_acct_no_kv.Trim().Length > 0)
                            {
                                BLLCommonUtilities.GetAccountInformation(objDVOMasterEmployeeObligations.bal_acct_no_kv, out _AccountNumber, out _AccountType, out _AccountTypeId, out _AccountDescription);
                                if (_AccountNumber > 0)
                                {
                                    objDVOMasterEmployeeObligations.bal_acct_no = _AccountNumber;
                                    objDVOMasterEmployeeObligations.bal_acct_no_typeid = _AccountTypeId;
                                }
                                else
                                {
                                    objDVOMasterEmployeeObligations.bal_acct_no_type = string.Empty;
                                    objDVOMasterEmployeeObligations.bal_acct_no_kv = string.Empty;
                                    objDVOMasterEmployeeObligations.bal_acct_no = 0;
                                    objDVOMasterEmployeeObligations.bal_acct_no_typeid = 0;
                                }
                            }
                        }
                        
                        listDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeObligations;
            }
            return listDVOMasterEmployeeObligations;
        }

        public static List<DVOMasterEmployeeObligations> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmployeeObligations)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
                        objDVOMasterEmployeeObligations.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.obl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmployeeObligations.obl_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        objDVOMasterEmployeeObligations.obl_limit = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null); ;
                        objDVOMasterEmployeeObligations.acct_no = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);
                        objDVOMasterEmployeeObligations.department = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.bal_acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOMasterEmployeeObligations.bal_dept = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.obl_qtd1 = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd2 = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd3 = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                        objDVOMasterEmployeeObligations.obl_qtd4 = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                        objDVOMasterEmployeeObligations.obl_ytd = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                        objDVOMasterEmployeeObligations.pay_limit = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                        objDVOMasterEmployeeObligations.acct_no_kv = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);
                        objDVOMasterEmployeeObligations.bal_acct_no_kv = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);

                        listDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmployeeObligations;
            }
            return listDVOMasterEmployeeObligations;
        }

        /// <summary>
        /// To Insert New list of Employee Obligation Code
        /// </summary>
        /// <param name="listDVOMasterEmployeeObligations">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations)
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
                if(listDVOMasterEmployeeObligations.Count>0)
                    foreach (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations in listDVOMasterEmployeeObligations)
                    {
                        DVOMasterEmployeeObligations obj = objDVOMasterEmployeeObligations;
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
        /// To Insert New Employee Obligation Code
        /// </summary>
        /// <param name="objDVOMasterEmployeeObligations">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOMasterEmployeeObligations objDVOMasterEmployeeObligations, bool MaintainLog)
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
                parameters[0] = objDVOMasterEmployeeObligations.empl_code;

                parameters[1] = objDVOMasterEmployeeObligations.obl_code;
                if(MaintainLog)
                using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                {
                    objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                    objtmp.field_name = "OBLIGATION - obl_code";
                    objtmp.old_value = "[NEWLY ADDED]";
                    objtmp.new_value = objDVOMasterEmployeeObligations.obl_code;
                    objtmp.update_by = objDVOMasterEmployeeObligations.InsertBy;
                    objtmp.update_machine = objDVOMasterEmployeeObligations.InsertMachineInfo;
                    objtmp.update_date = objDVOMasterEmployeeObligations.InsertDate;
                    listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                }
                parameters[2] = objDVOMasterEmployeeObligations.obl_rate;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                {
                    objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                    objtmp.field_name = "OBLIGATION - obl_rate";
                    objtmp.old_value = "[NEWLY ADDED]";
                    objtmp.new_value = objDVOMasterEmployeeObligations.obl_rate.ToString();
                    objtmp.update_by = objDVOMasterEmployeeObligations.InsertBy;
                    objtmp.update_machine = objDVOMasterEmployeeObligations.InsertMachineInfo;
                    objtmp.update_date = objDVOMasterEmployeeObligations.InsertDate;
                    listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                }
                parameters[3] = objDVOMasterEmployeeObligations.obl_limit;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                {
                    objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                    objtmp.field_name = "OBLIGATION - obl_limit";
                    objtmp.old_value = "[NEWLY ADDED]";
                    objtmp.new_value = objDVOMasterEmployeeObligations.obl_limit.ToString();
                    objtmp.update_by = objDVOMasterEmployeeObligations.InsertBy;
                    objtmp.update_machine = objDVOMasterEmployeeObligations.InsertMachineInfo;
                    objtmp.update_date = objDVOMasterEmployeeObligations.InsertDate;
                    listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                }

                parameters[4] = objDVOMasterEmployeeObligations.acct_no;
                parameters[5] = objDVOMasterEmployeeObligations.department;
                parameters[6] = objDVOMasterEmployeeObligations.bal_acct_no;
                parameters[7] = objDVOMasterEmployeeObligations.bal_dept;
                parameters[8] = objDVOMasterEmployeeObligations.obl_qtd1;
                parameters[9] = objDVOMasterEmployeeObligations.obl_qtd2;
                parameters[10] = objDVOMasterEmployeeObligations.obl_qtd3;
                parameters[11] = objDVOMasterEmployeeObligations.obl_qtd4;
                parameters[12] = objDVOMasterEmployeeObligations.obl_ytd;

                parameters[13] = objDVOMasterEmployeeObligations.pay_limit;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                {
                    objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                    objtmp.field_name = "OBLIGATION - pay_limit";
                    objtmp.old_value = "[NEWLY ADDED]";
                    objtmp.new_value = objDVOMasterEmployeeObligations.pay_limit.ToString();
                    objtmp.update_by = objDVOMasterEmployeeObligations.InsertBy;
                    objtmp.update_machine = objDVOMasterEmployeeObligations.InsertMachineInfo;
                    objtmp.update_date = objDVOMasterEmployeeObligations.InsertDate;
                    listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                }

                parameters[14] = objDVOMasterEmployeeObligations.InsertMachineInfo;
                if (objDVOMasterEmployeeObligations.InsertDate == null || objDVOMasterEmployeeObligations.InsertDate.Trim().Length <= 0)
                    objDVOMasterEmployeeObligations.InsertDate = "01/01/1900";
                parameters[15] = objDVOMasterEmployeeObligations.InsertDate;
                parameters[16] = objDVOMasterEmployeeObligations.InsertBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeObligations.INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeObligations.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeObligations.acct_no_kv;
                BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

                objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeObligations.bal_acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeObligations.bal_acct_no_kv;
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
        /// To Update list of Employee Obligation Code
        /// </summary>
        /// <param name="listDVOMasterEmployeeObligations">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations, ref List<DVOMasterEmployeeObligations> listPreDVOMasterEmployeeObligations)
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
                if(listDVOMasterEmployeeObligations.Count>0)
                    foreach (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations in listDVOMasterEmployeeObligations)
                    {
                        DVOMasterEmployeeObligations obj = objDVOMasterEmployeeObligations;
                        if (obj.line_no > 0)
                        {
                            DVOMasterEmployeeObligations objPre =
                                listPreDVOMasterEmployeeObligations.Find(delegate(DVOMasterEmployeeObligations objDel)
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
        /// To Update Employee Obligation Code
        /// </summary>
        /// <param name="objDVOMasterEmployeeObligations">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOMasterEmployeeObligations objDVOMasterEmployeeObligations, ref DVOMasterEmployeeObligations objPreDVOMasterEmployeeObligations)
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
                parameters[0] = objDVOMasterEmployeeObligations.line_no;
                parameters[1] = objDVOMasterEmployeeObligations.empl_code;

                parameters[2] = objDVOMasterEmployeeObligations.obl_code;
                if (objDVOMasterEmployeeObligations.obl_code.Trim() != objPreDVOMasterEmployeeObligations.obl_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                        objtmp.field_name = "OBLIGATION - obl_code";
                        objtmp.old_value = objPreDVOMasterEmployeeObligations.obl_code;
                        objtmp.new_value = objDVOMasterEmployeeObligations.obl_code;
                        objtmp.update_by = objDVOMasterEmployeeObligations.Updateby;
                        objtmp.update_machine = objDVOMasterEmployeeObligations.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeObligations.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[3] = objDVOMasterEmployeeObligations.obl_rate;
                if (objDVOMasterEmployeeObligations.obl_rate != objPreDVOMasterEmployeeObligations.obl_rate)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                        objtmp.field_name = "OBLIGATION - obl_rate";
                        objtmp.old_value = objPreDVOMasterEmployeeObligations.obl_rate.ToString();
                        objtmp.new_value = objDVOMasterEmployeeObligations.obl_rate.ToString();
                        objtmp.update_by = objDVOMasterEmployeeObligations.Updateby;
                        objtmp.update_machine = objDVOMasterEmployeeObligations.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeObligations.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[4] = objDVOMasterEmployeeObligations.obl_limit;
                if (objDVOMasterEmployeeObligations.obl_limit != objPreDVOMasterEmployeeObligations.obl_limit)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                        objtmp.field_name = "OBLIGATION - obl_limit";
                        objtmp.old_value = objPreDVOMasterEmployeeObligations.obl_limit.ToString();
                        objtmp.new_value = objDVOMasterEmployeeObligations.obl_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeObligations.Updateby;
                        objtmp.update_machine = objDVOMasterEmployeeObligations.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeObligations.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[5] = objDVOMasterEmployeeObligations.acct_no;
                parameters[6] = objDVOMasterEmployeeObligations.department;
                parameters[7] = objDVOMasterEmployeeObligations.bal_acct_no;
                parameters[8] = objDVOMasterEmployeeObligations.bal_dept;
                parameters[9] = objDVOMasterEmployeeObligations.obl_qtd1;
                parameters[10] = objDVOMasterEmployeeObligations.obl_qtd2;
                parameters[11] = objDVOMasterEmployeeObligations.obl_qtd3;
                parameters[12] = objDVOMasterEmployeeObligations.obl_qtd4;
                parameters[13] = objDVOMasterEmployeeObligations.obl_ytd;

                parameters[14] = objDVOMasterEmployeeObligations.pay_limit;
                if (objDVOMasterEmployeeObligations.pay_limit != objPreDVOMasterEmployeeObligations.pay_limit)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmployeeObligations.empl_code;
                        objtmp.field_name = "OBLIGATION - pay_limit";
                        objtmp.old_value = objPreDVOMasterEmployeeObligations.pay_limit.ToString();
                        objtmp.new_value = objDVOMasterEmployeeObligations.pay_limit.ToString();
                        objtmp.update_by = objDVOMasterEmployeeObligations.Updateby;
                        objtmp.update_machine = objDVOMasterEmployeeObligations.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmployeeObligations.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[15] = objDVOMasterEmployeeObligations.UpdateMachineInfo;
                if (objDVOMasterEmployeeObligations.UpdateDate == null || objDVOMasterEmployeeObligations.UpdateDate.Trim().Length <= 0)
                    objDVOMasterEmployeeObligations.UpdateDate = "01/01/1900";
                parameters[16] = objDVOMasterEmployeeObligations.UpdateDate;
                parameters[17] = objDVOMasterEmployeeObligations.Updateby;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeObligations.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeObligations.acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeObligations.acct_no_kv;
                BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

                objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = objDVOMasterEmployeeObligations.bal_acct_no_type;
                objDVOFlexSegCommon.keyvalue = objDVOMasterEmployeeObligations.bal_acct_no_kv;
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
        /// To Delete list of Employee Obligation Code
        /// </summary>
        /// <param name="listDVOMasterEmployeeObligations">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations)
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
                if (listDVOMasterEmployeeObligations.Count > 0)
                    foreach (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations in listDVOMasterEmployeeObligations)
                    {
                        DVOMasterEmployeeObligations obj = objDVOMasterEmployeeObligations;
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
        /// To Update Employee Obligation Code
        /// </summary>
        /// <param name="objDVOMasterEmployeeObligations">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmployeeObligations objDVOMasterEmployeeObligations)
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
                parameters[0] = objDVOMasterEmployeeObligations.line_no;
                parameters[1] = objDVOMasterEmployeeObligations.empl_code;

                //object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeObligations()).DELETE_SPNAME);
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterEmployeeObligations.DELETE_LINE);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = objDVOMasterEmployeeObligations.obl_code;
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
        /// Get Keyvalue for flex-expense-account-type of employee
        /// </summary>
        /// <param name="pobjDVOMasterEmployee"></param>
        /// <returns></returns>
        public static string GetFlexExpAccountKeyvalue(ref DVOMasterEmployeeObligations pobjDVOMasterEmployeeObligations)
        {
            string _FlexExpAccountKeyvalue = string.Empty;
            try
            {
                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = pobjDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployeeObligations.acct_no_type;
                _FlexExpAccountKeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
                objDVOFlexSegCommon = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _FlexExpAccountKeyvalue;
        }

        /// <summary>
        /// Get Keyvalue for flex-liability-account-type of employee
        /// </summary>
        /// <param name="pobjDVOMasterEmployee"></param>
        /// <returns></returns>
        public static string GetFlexLiabAccountKeyvalue(ref DVOMasterEmployeeObligations pobjDVOMasterEmployeeObligations)
        {
            string _FlexLiabAccountKeyvalue = string.Empty;
            try
            {
                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterEmployeeObligations";
                objDVOFlexSegCommon.Code = pobjDVOMasterEmployeeObligations.obl_code;
                objDVOFlexSegCommon.AccountType = pobjDVOMasterEmployeeObligations.bal_acct_no_type;
                _FlexLiabAccountKeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
                objDVOFlexSegCommon = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _FlexLiabAccountKeyvalue;
        }
    }
}
