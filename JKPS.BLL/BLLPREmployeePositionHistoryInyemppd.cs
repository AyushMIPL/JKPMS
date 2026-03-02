using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPREmployeePositionHistoryInyemppd
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOPREmployeePositionHistoryInyemppd">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOPREmployeePositionHistoryInyemppd> GetData(ref DVOPREmployeePositionHistoryInyemppd pobjDVOPREmployeePositionHistoryInyemppd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();

            try
            {
                object[] parameters = new object[10];
                parameters[0] = pobjDVOPREmployeePositionHistoryInyemppd.RowId;
                parameters[1] = pobjDVOPREmployeePositionHistoryInyemppd.empl_code;
                parameters[2] = pobjDVOPREmployeePositionHistoryInyemppd.pos_code;
                parameters[3] = pobjDVOPREmployeePositionHistoryInyemppd.cat_code;
                parameters[4] = pobjDVOPREmployeePositionHistoryInyemppd.scale_code;
                if (pobjDVOPREmployeePositionHistoryInyemppd.start_date != null)
                    if (pobjDVOPREmployeePositionHistoryInyemppd.start_date.Trim().Length > 0)
                        pobjDVOPREmployeePositionHistoryInyemppd.start_date = Convert.ToDateTime(pobjDVOPREmployeePositionHistoryInyemppd.start_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = pobjDVOPREmployeePositionHistoryInyemppd.start_date;
                if (pobjDVOPREmployeePositionHistoryInyemppd.end_date != null)
                    if (pobjDVOPREmployeePositionHistoryInyemppd.end_date.Trim().Length > 0)
                        pobjDVOPREmployeePositionHistoryInyemppd.end_date = Convert.ToDateTime(pobjDVOPREmployeePositionHistoryInyemppd.end_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[6] = pobjDVOPREmployeePositionHistoryInyemppd.end_date;
                parameters[7] = pobjDVOPREmployeePositionHistoryInyemppd.approved_by;
                parameters[8] = pobjDVOPREmployeePositionHistoryInyemppd.pay_rate;
                parameters[9] = pobjDVOPREmployeePositionHistoryInyemppd.temporary;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPREmployeePositionHistoryInyemppd)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd();
                        objDVOPREmployeePositionHistoryInyemppd.RowId = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOPREmployeePositionHistoryInyemppd.empl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.pos_code = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.cat_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.scale_code = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.start_date = ((dr[5] != DBNull.Value && dr[5].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.end_date = ((dr[6] != DBNull.Value && dr[6].ToString().Trim().Length > 0) ? Convert.ToDateTime(dr[6]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.approved_by = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.pay_rate = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOPREmployeePositionHistoryInyemppd.temporary = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);

                        listDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOPREmployeePositionHistoryInyemppd;
            }
            return listDVOPREmployeePositionHistoryInyemppd;
        }

        public static List<DVOPREmployeePositionHistoryInyemppd> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOPREmployeePositionHistoryInyemppd)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd();
                        objDVOPREmployeePositionHistoryInyemppd.RowId = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOPREmployeePositionHistoryInyemppd.empl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.pos_code = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.cat_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.scale_code = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.start_date = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.end_date = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.approved_by = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOPREmployeePositionHistoryInyemppd.pay_rate = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                        objDVOPREmployeePositionHistoryInyemppd.temporary = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);

                        listDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOPREmployeePositionHistoryInyemppd;
            }
            return listDVOPREmployeePositionHistoryInyemppd;
        }

        /// <summary>
        /// To Insert New list of Employee Position History
        /// </summary>
        /// <param name="listDVOPREmployeePositionHistoryInyemppd">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd)
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
                if(listDVOPREmployeePositionHistoryInyemppd.Count>0)
                    foreach (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd in listDVOPREmployeePositionHistoryInyemppd)
                    {
                        DVOPREmployeePositionHistoryInyemppd obj = objDVOPREmployeePositionHistoryInyemppd;
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
        /// To Insert New Employee Position History
        /// </summary>
        /// <param name="objDVOPREmployeePositionHistoryInyemppd">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd, bool MaintainLog)
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
                object[] parameters = new object[12];
                parameters[0] = objDVOPREmployeePositionHistoryInyemppd.empl_code;

                parameters[1] = objDVOPREmployeePositionHistoryInyemppd.pos_code;
                if(MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - pos_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.pos_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[2] = objDVOPREmployeePositionHistoryInyemppd.cat_code;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - cat_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.cat_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[3] = objDVOPREmployeePositionHistoryInyemppd.scale_code;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - scale_code";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.scale_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                if (objDVOPREmployeePositionHistoryInyemppd.start_date == null || objDVOPREmployeePositionHistoryInyemppd.start_date.Trim().Length <= 0)
                    objDVOPREmployeePositionHistoryInyemppd.start_date = "01/01/1900";
                parameters[4] = objDVOPREmployeePositionHistoryInyemppd.start_date;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - start_date";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.start_date;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                if (objDVOPREmployeePositionHistoryInyemppd.end_date == null || objDVOPREmployeePositionHistoryInyemppd.end_date.Trim().Length <= 0)
                    objDVOPREmployeePositionHistoryInyemppd.end_date = "01/01/1900";
                parameters[5] = objDVOPREmployeePositionHistoryInyemppd.end_date;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - end_date";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.end_date;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                parameters[6] = objDVOPREmployeePositionHistoryInyemppd.approved_by;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - approved_by";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.approved_by;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[7] = objDVOPREmployeePositionHistoryInyemppd.pay_rate;

                parameters[8] = objDVOPREmployeePositionHistoryInyemppd.temporary;
                if (MaintainLog)
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - temporary";
                        objtmp.old_value = "[NEWLY ADDED]";
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.temporary;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }

                parameters[9] = objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo;
                if (objDVOPREmployeePositionHistoryInyemppd.InsertDate == null || objDVOPREmployeePositionHistoryInyemppd.InsertDate.Trim().Length <= 0)
                    objDVOPREmployeePositionHistoryInyemppd.InsertDate = "01/01/1900";
                parameters[10] = objDVOPREmployeePositionHistoryInyemppd.InsertDate;
                parameters[11] = objDVOPREmployeePositionHistoryInyemppd.InsertBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPREmployeePositionHistoryInyemppd()).INSERT_SPNAME);
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
        /// To Update list of Employee Position History
        /// </summary>
        /// <param name="listDVOPREmployeePositionHistoryInyemppd">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd, ref List<DVOPREmployeePositionHistoryInyemppd> listPreDVOPREmployeePositionHistoryInyemppd)
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
                if(listDVOPREmployeePositionHistoryInyemppd.Count>0)
                    foreach (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd in listDVOPREmployeePositionHistoryInyemppd)
                    {
                        DVOPREmployeePositionHistoryInyemppd obj = objDVOPREmployeePositionHistoryInyemppd;
                        if (obj.RowId > 0)
                        {
                            DVOPREmployeePositionHistoryInyemppd objPre =
                                listPreDVOPREmployeePositionHistoryInyemppd.Find(delegate(DVOPREmployeePositionHistoryInyemppd objDel)
                            {
                                if (objDel.RowId == obj.RowId)
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
        /// To Update Employee Position History
        /// </summary>
        /// <param name="objDVOPREmployeePositionHistoryInyemppd">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd, ref DVOPREmployeePositionHistoryInyemppd objPreDVOPREmployeePositionHistoryInyemppd)
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
                object[] parameters = new object[13];
                parameters[0] = objDVOPREmployeePositionHistoryInyemppd.RowId;
                parameters[1] = objDVOPREmployeePositionHistoryInyemppd.empl_code;

                parameters[2] = objDVOPREmployeePositionHistoryInyemppd.pos_code;
                if (objDVOPREmployeePositionHistoryInyemppd.pos_code.Trim() != objPreDVOPREmployeePositionHistoryInyemppd.pos_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - pos_code";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.pos_code;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.pos_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[3] = objDVOPREmployeePositionHistoryInyemppd.cat_code;
                if (objDVOPREmployeePositionHistoryInyemppd.cat_code.Trim() != objPreDVOPREmployeePositionHistoryInyemppd.cat_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - cat_code";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.cat_code;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.cat_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[4] = objDVOPREmployeePositionHistoryInyemppd.scale_code;
                if (objDVOPREmployeePositionHistoryInyemppd.scale_code.Trim() != objPreDVOPREmployeePositionHistoryInyemppd.scale_code.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - scale_code";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.scale_code;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.scale_code;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                if (objDVOPREmployeePositionHistoryInyemppd.start_date == null || objDVOPREmployeePositionHistoryInyemppd.start_date.Trim().Length <= 0)
                    objDVOPREmployeePositionHistoryInyemppd.start_date = "01/01/1900";
                if (objPreDVOPREmployeePositionHistoryInyemppd.start_date == null || objPreDVOPREmployeePositionHistoryInyemppd.start_date.Trim().Length <= 0)
                    objPreDVOPREmployeePositionHistoryInyemppd.start_date = "01/01/1900";
                parameters[5] = objDVOPREmployeePositionHistoryInyemppd.start_date;
                if (objDVOPREmployeePositionHistoryInyemppd.start_date != objPreDVOPREmployeePositionHistoryInyemppd.start_date)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - start_date";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.start_date;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.start_date;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                if (objDVOPREmployeePositionHistoryInyemppd.end_date == null || objDVOPREmployeePositionHistoryInyemppd.end_date.Trim().Length <= 0)
                    objDVOPREmployeePositionHistoryInyemppd.end_date = "01/01/1900";
                if (objPreDVOPREmployeePositionHistoryInyemppd.end_date == null || objPreDVOPREmployeePositionHistoryInyemppd.end_date.Trim().Length <= 0)
                    objPreDVOPREmployeePositionHistoryInyemppd.end_date = "01/01/1900";
                parameters[6] = objDVOPREmployeePositionHistoryInyemppd.end_date;
                if (objDVOPREmployeePositionHistoryInyemppd.end_date != objPreDVOPREmployeePositionHistoryInyemppd.end_date)
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - end_date";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.end_date;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.end_date;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }
                parameters[7] = objDVOPREmployeePositionHistoryInyemppd.approved_by;
                if (objDVOPREmployeePositionHistoryInyemppd.approved_by.Trim() != objPreDVOPREmployeePositionHistoryInyemppd.approved_by.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - approved_by";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.approved_by;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.approved_by;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[8] = objDVOPREmployeePositionHistoryInyemppd.pay_rate;

                parameters[9] = objDVOPREmployeePositionHistoryInyemppd.temporary;
                if (objDVOPREmployeePositionHistoryInyemppd.temporary.Trim() != objPreDVOPREmployeePositionHistoryInyemppd.temporary.Trim())
                {
                    using (DVOEmployeeInfoLogEmpUpdLog objtmp = new DVOEmployeeInfoLogEmpUpdLog())
                    {
                        objtmp.empl_code = objDVOPREmployeePositionHistoryInyemppd.empl_code;
                        objtmp.field_name = "POSITION-HISTORY - temporary";
                        objtmp.old_value = objPreDVOPREmployeePositionHistoryInyemppd.temporary;
                        objtmp.new_value = objDVOPREmployeePositionHistoryInyemppd.temporary;
                        objtmp.update_by = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                        objtmp.update_machine = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                        objtmp.update_date = objDVOPREmployeePositionHistoryInyemppd.UpdateDate;
                        listDVOEmployeeInfoLogEmpUpdLog.Add(objtmp);
                    }
                }

                parameters[10] = objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo;
                if (objDVOPREmployeePositionHistoryInyemppd.UpdateDate == null || objDVOPREmployeePositionHistoryInyemppd.UpdateDate.Trim().Length <= 0)
                        objDVOPREmployeePositionHistoryInyemppd.UpdateDate = "01/01/1900";
                parameters[11] = objDVOPREmployeePositionHistoryInyemppd.UpdateDate.Trim();
                parameters[12] = objDVOPREmployeePositionHistoryInyemppd.UpdateBy;
                #endregion Parameters

                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPREmployeePositionHistoryInyemppd()).UPDATE_SPNAME);
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
        /// To Delete list of Employee Position History
        /// </summary>
        /// <param name="listDVOPREmployeePositionHistoryInyemppd">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd)
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
                if (listDVOPREmployeePositionHistoryInyemppd.Count > 0)
                    foreach (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd in listDVOPREmployeePositionHistoryInyemppd)
                    {
                        DVOPREmployeePositionHistoryInyemppd obj = objDVOPREmployeePositionHistoryInyemppd;
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
        /// To Delete Employee Position History
        /// </summary>
        /// <param name="objDVOPREmployeePositionHistoryInyemppd">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd)
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
                parameters[0] = objDVOPREmployeePositionHistoryInyemppd.RowId;
                parameters[1] = objDVOPREmployeePositionHistoryInyemppd.empl_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPREmployeePositionHistoryInyemppd()).DELETE_SPNAME);
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
