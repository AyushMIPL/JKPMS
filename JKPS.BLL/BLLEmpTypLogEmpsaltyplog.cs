using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLEmpTypLogEmpsaltyplog
    {
        /// <summary>
        /// To Insert Log of updated Employee Salary Type Information
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
        /// <returns></returns>
        public static int InsertEmployeeSalaryTypeLog(ref object objTransaction, ref List<DVOEmpTypLogEmpsaltyplog> listDVOEmpTypLogEmpsaltyplog)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] parameters = new object[6];
                if (listDVOEmpTypLogEmpsaltyplog.Count > 0)
                    foreach (DVOEmpTypLogEmpsaltyplog objDVOEmpTypLogEmpsaltyplog in listDVOEmpTypLogEmpsaltyplog)
                    {
                        parameters[0] = objDVOEmpTypLogEmpsaltyplog.emp_code;
                        parameters[1] = objDVOEmpTypLogEmpsaltyplog.type_code;
                        if (objDVOEmpTypLogEmpsaltyplog.date_assigned != null)
                            if (objDVOEmpTypLogEmpsaltyplog.date_assigned.Trim().Length > 0)
                                objDVOEmpTypLogEmpsaltyplog.date_assigned = objDVOEmpTypLogEmpsaltyplog.date_assigned.Trim();// Convert.ToDateTime(objDVOEmpTypLogEmpsaltyplog.date_assigned).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[2] = objDVOEmpTypLogEmpsaltyplog.date_assigned;
                        parameters[3] = objDVOEmpTypLogEmpsaltyplog.update_by;
                        if (objDVOEmpTypLogEmpsaltyplog.update_date != null)
                            if (objDVOEmpTypLogEmpsaltyplog.update_date.Trim().Length > 0)
                                objDVOEmpTypLogEmpsaltyplog.update_date = objDVOEmpTypLogEmpsaltyplog.update_date.Trim();// Convert.ToDateTime(objDVOEmpTypLogEmpsaltyplog.update_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[4] = objDVOEmpTypLogEmpsaltyplog.update_date;
                        parameters[5] = objDVOEmpTypLogEmpsaltyplog.update_machine;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmpTypLogEmpsaltyplog.INSERT_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();
                    }
                parameters = null;
                objDALBaseClass = null;

                if (objTransaction != null)
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Insert Log of updated Employee Salary Type Information
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
        /// <returns></returns>
        public static int UpdateEmployeeSalaryTypeLog(ref object objTransaction, ref List<DVOEmpTypLogEmpsaltyplog> listDVOEmpTypLogEmpsaltyplog)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] parameters = new object[6];
                if (listDVOEmpTypLogEmpsaltyplog.Count > 0)
                    foreach (DVOEmpTypLogEmpsaltyplog objDVOEmpTypLogEmpsaltyplog in listDVOEmpTypLogEmpsaltyplog)
                    {
                        parameters[0] = objDVOEmpTypLogEmpsaltyplog.emp_code;
                        parameters[1] = objDVOEmpTypLogEmpsaltyplog.type_code;
                        if (objDVOEmpTypLogEmpsaltyplog.date_assigned != null)
                            if (objDVOEmpTypLogEmpsaltyplog.date_assigned.Trim().Length > 0)
                                objDVOEmpTypLogEmpsaltyplog.date_assigned = objDVOEmpTypLogEmpsaltyplog.date_assigned;// Convert.ToDateTime(objDVOEmpTypLogEmpsaltyplog.date_assigned).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[2] = objDVOEmpTypLogEmpsaltyplog.date_assigned;
                        parameters[3] = objDVOEmpTypLogEmpsaltyplog.update_by;
                        if (objDVOEmpTypLogEmpsaltyplog.update_date != null)
                            if (objDVOEmpTypLogEmpsaltyplog.update_date.Trim().Length > 0)
                                objDVOEmpTypLogEmpsaltyplog.update_date = objDVOEmpTypLogEmpsaltyplog.update_date;// Convert.ToDateTime(objDVOEmpTypLogEmpsaltyplog.update_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[4] = objDVOEmpTypLogEmpsaltyplog.update_date;
                        parameters[5] = objDVOEmpTypLogEmpsaltyplog.update_machine;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmpTypLogEmpsaltyplog.UPDATE_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();
                    }
                parameters = null;
                objDALBaseClass = null;

                if (objTransaction != null)
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static DataSet GetEmployeeSalTypeLog(ref DVOEmpTypLogEmpsaltyplog objDVO)
        {

            object[] parameters = new object[3];
            parameters[0] = objDVO.emp_code;
            parameters[1] = objDVO.update_by;
            parameters[2] = objDVO.update_date;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOEmpTypLogEmpsaltyplog));
            if (ds != null)
                if (!(ds.Tables[0].Rows.Count > 0))
                {
                    //do nothing
                }
            return ds;
        }
    }
}
