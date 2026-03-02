using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLMasterEmpBankDetails
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOMasterEmpBankDetails">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmpBankDetails> GetData(ref DVOMasterEmpBankDetails pobjDVOMasterEmpBankDetails)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();

            try
            {
                object[] parameters = new object[17];
                parameters[0] = pobjDVOMasterEmpBankDetails.RowID;
                parameters[1] = pobjDVOMasterEmpBankDetails.empl_code;
                parameters[2] = pobjDVOMasterEmpBankDetails.line_no;
                parameters[3] = pobjDVOMasterEmpBankDetails.bank_code;
                parameters[4] = pobjDVOMasterEmpBankDetails.bank_acct_no;
                parameters[5] = pobjDVOMasterEmpBankDetails.type;
                parameters[6] = pobjDVOMasterEmpBankDetails.amount;
                parameters[7] = pobjDVOMasterEmpBankDetails.typeofacct;

                parameters[8] = pobjDVOMasterEmpBankDetails.SSN;
                parameters[9] = pobjDVOMasterEmpBankDetails.typeCode;
                parameters[10] = pobjDVOMasterEmpBankDetails.lastName;
                parameters[11] = pobjDVOMasterEmpBankDetails.firstName;
                parameters[12] = pobjDVOMasterEmpBankDetails.empl_status;
                parameters[13] = pobjDVOMasterEmpBankDetails.jobCode;
                parameters[14] = pobjDVOMasterEmpBankDetails.jobTitle;
                parameters[15] = pobjDVOMasterEmpBankDetails.pay_period;
                if (pobjDVOMasterEmpBankDetails.lastPay != null)
                    if (pobjDVOMasterEmpBankDetails.lastPay.Trim().Length > 0)
                        pobjDVOMasterEmpBankDetails.lastPay = Convert.ToDateTime(pobjDVOMasterEmpBankDetails.lastPay).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[16] = pobjDVOMasterEmpBankDetails.lastPay;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpBankDetails)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails();
                        objDVOMasterEmpBankDetails.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterEmpBankDetails.empl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmpBankDetails.bank_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.bank_acct_no = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.type = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.amount = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                        objDVOMasterEmpBankDetails.typeofacct = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.bank_desc = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        
                        listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmpBankDetails;
            }
            return listDVOMasterEmpBankDetails;
        }

        public static List<DVOMasterEmpBankDetails> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmpBankDetails)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails();
                        objDVOMasterEmpBankDetails.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterEmpBankDetails.empl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.line_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                        objDVOMasterEmpBankDetails.bank_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.bank_acct_no = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.type = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.amount = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                        objDVOMasterEmpBankDetails.typeofacct = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmpBankDetails.bank_desc = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);

                        listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmpBankDetails;
            }
            return listDVOMasterEmpBankDetails;
        }

        /// <summary>
        /// To Insert New list of Employee Direct Deposit Information
        /// </summary>
        /// <param name="listDVOMasterEmpBankDetails">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails)
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
                if(listDVOMasterEmpBankDetails.Count>0)
                    foreach (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails in listDVOMasterEmpBankDetails)
                    {
                        DVOMasterEmpBankDetails obj = objDVOMasterEmpBankDetails;
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
        /// To Insert New Employee Direct Deposit Information
        /// </summary>
        /// <param name="objDVOMasterEmpBankDetails">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOMasterEmpBankDetails objDVOMasterEmpBankDetails, bool MaintainLog)
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
                object[] parameters = new object[10];
                parameters[0] = objDVOMasterEmpBankDetails.empl_code;
                parameters[1] = objDVOMasterEmpBankDetails.line_no;

                parameters[2] = objDVOMasterEmpBankDetails.bank_code;
                if(MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - bank_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmpBankDetails.bank_code;
                        objtmp.update_by = objDVOMasterEmpBankDetails.InsertBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[3] = objDVOMasterEmpBankDetails.bank_acct_no;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - bank_acct_no";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmpBankDetails.bank_acct_no;
                        objtmp.update_by = objDVOMasterEmpBankDetails.InsertBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[4] = objDVOMasterEmpBankDetails.type;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - type";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmpBankDetails.type;
                        objtmp.update_by = objDVOMasterEmpBankDetails.InsertBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[5] = objDVOMasterEmpBankDetails.amount;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - amount";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmpBankDetails.amount.ToString();
                        objtmp.update_by = objDVOMasterEmpBankDetails.InsertBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[6] = objDVOMasterEmpBankDetails.typeofacct;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - typeofacct";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOMasterEmpBankDetails.typeofacct;
                        objtmp.update_by = objDVOMasterEmpBankDetails.InsertBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.InsertMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[7] = objDVOMasterEmpBankDetails.InsertMachineInfo;
                if (objDVOMasterEmpBankDetails.InsertDate == null || objDVOMasterEmpBankDetails.InsertDate.Trim().Length <= 0)
                    objDVOMasterEmpBankDetails.InsertDate = "01/01/1900";
                parameters[8] = objDVOMasterEmpBankDetails.InsertDate;
                parameters[9] = objDVOMasterEmpBankDetails.InsertBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmpBankDetails()).INSERT_SPNAME);
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

        /// <summary>
        /// To Update list of Employee Direct Deposit Information
        /// </summary>
        /// <param name="listDVOMasterEmpBankDetails">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails, ref List<DVOMasterEmpBankDetails> listPreDVOMasterEmpBankDetails)
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
                if(listDVOMasterEmpBankDetails.Count>0)
                    foreach (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails in listDVOMasterEmpBankDetails)
                    {
                        DVOMasterEmpBankDetails obj = objDVOMasterEmpBankDetails;
                        if (obj.RowID > 0)
                        {
                            DVOMasterEmpBankDetails objPre =
                                listPreDVOMasterEmpBankDetails.Find(delegate(DVOMasterEmpBankDetails objDel)
                            {
                                if (objDel.RowID == obj.RowID)
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
        /// To Update Employee Direct Deposit Information
        /// </summary>
        /// <param name="objDVOMasterEmpBankDetails">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOMasterEmpBankDetails objDVOMasterEmpBankDetails, ref DVOMasterEmpBankDetails objPreDVOMasterEmpBankDetails)
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
                object[] parameters = new object[11];
                parameters[0] = objDVOMasterEmpBankDetails.RowID;
                parameters[1] = objDVOMasterEmpBankDetails.empl_code;
                parameters[2] = objDVOMasterEmpBankDetails.line_no;

                parameters[3] = objDVOMasterEmpBankDetails.bank_code;
                if (objDVOMasterEmpBankDetails.bank_code.Trim() != objPreDVOMasterEmpBankDetails.bank_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - bank_code";
                        objtmp.old_value = objPreDVOMasterEmpBankDetails.bank_code;
                        objtmp.new_value = objDVOMasterEmpBankDetails.bank_code;
                        objtmp.update_by = objDVOMasterEmpBankDetails.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[4] = objDVOMasterEmpBankDetails.bank_acct_no;
                if (objDVOMasterEmpBankDetails.bank_acct_no.Trim() != objPreDVOMasterEmpBankDetails.bank_acct_no.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - bank_acct_no";
                        objtmp.old_value = objPreDVOMasterEmpBankDetails.bank_acct_no;
                        objtmp.new_value = objDVOMasterEmpBankDetails.bank_acct_no;
                        objtmp.update_by = objDVOMasterEmpBankDetails.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[5] = objDVOMasterEmpBankDetails.type;
                if (objDVOMasterEmpBankDetails.type.Trim() != objPreDVOMasterEmpBankDetails.type.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - type";
                        objtmp.old_value = objPreDVOMasterEmpBankDetails.type;
                        objtmp.new_value = objDVOMasterEmpBankDetails.type;
                        objtmp.update_by = objDVOMasterEmpBankDetails.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[6] = objDVOMasterEmpBankDetails.amount;
                if (objDVOMasterEmpBankDetails.amount != objPreDVOMasterEmpBankDetails.amount)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - amount";
                        objtmp.old_value = objPreDVOMasterEmpBankDetails.amount.ToString();
                        objtmp.new_value = objDVOMasterEmpBankDetails.amount.ToString();
                        objtmp.update_by = objDVOMasterEmpBankDetails.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[7] = objDVOMasterEmpBankDetails.typeofacct;
                if (objDVOMasterEmpBankDetails.typeofacct.Trim() != objPreDVOMasterEmpBankDetails.typeofacct.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOMasterEmpBankDetails.empl_code;
                        objtmp.field_name = "DIRECT-DEPOSIT - typeofacct";
                        objtmp.old_value = objPreDVOMasterEmpBankDetails.typeofacct;
                        objtmp.new_value = objDVOMasterEmpBankDetails.typeofacct;
                        objtmp.update_by = objDVOMasterEmpBankDetails.UpdateBy;
                        objtmp.update_machine = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                        objtmp.update_date = objDVOMasterEmpBankDetails.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[8] = objDVOMasterEmpBankDetails.UpdateMachineInfo;
                if (objDVOMasterEmpBankDetails.UpdateDate == null || objDVOMasterEmpBankDetails.UpdateDate.Trim().Length <= 0)
                    objDVOMasterEmpBankDetails.UpdateDate = "01/01/1900";
                parameters[9] = objDVOMasterEmpBankDetails.UpdateDate;
                parameters[10] = objDVOMasterEmpBankDetails.UpdateBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmpBankDetails()).UPDATE_SPNAME);
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

        /// <summary>
        /// To Delete list of Employee Direct Deposit Information
        /// </summary>
        /// <param name="listDVOMasterEmpBankDetails">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails)
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
                if (listDVOMasterEmpBankDetails.Count > 0)
                    foreach (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails in listDVOMasterEmpBankDetails)
                    {
                        DVOMasterEmpBankDetails obj = objDVOMasterEmpBankDetails;
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
        /// To Delete Employee Direct Deposit Information
        /// </summary>
        /// <param name="objDVOMasterEmpBankDetails">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmpBankDetails objDVOMasterEmpBankDetails)
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
                parameters[0] = objDVOMasterEmpBankDetails.RowID;
                parameters[1] = objDVOMasterEmpBankDetails.empl_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmpBankDetails()).DELETE_SPNAME);
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
