using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLEmployeeInfoLogEmpupdlog
    {
        /// <summary>
        /// To Insert Log of updated Employee Information
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
        /// <returns></returns>
        public static int InsertEmployeeInformationLog(ref object objTransaction, ref List<DVOEmployeeInfoLogEmpUpdLog> listDVOEmployeeInfoLogEmpUpdLog)
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
                object[] parameters = new object[7];
                if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
                    foreach (DVOEmployeeInfoLogEmpUpdLog objDVOEmployeeInfoLogEmpUpdLog in listDVOEmployeeInfoLogEmpUpdLog)
                    {
                        parameters[0] = objDVOEmployeeInfoLogEmpUpdLog.empl_code;
                        parameters[1] = objDVOEmployeeInfoLogEmpUpdLog.field_name;
                        parameters[2] = objDVOEmployeeInfoLogEmpUpdLog.old_value;
                        parameters[3] = objDVOEmployeeInfoLogEmpUpdLog.new_value;
                        parameters[4] = objDVOEmployeeInfoLogEmpUpdLog.update_by;
                        parameters[5] = objDVOEmployeeInfoLogEmpUpdLog.update_machine;
                        if (objDVOEmployeeInfoLogEmpUpdLog.update_date != null)
                            if (objDVOEmployeeInfoLogEmpUpdLog.update_date.Trim().Length > 0)
                                objDVOEmployeeInfoLogEmpUpdLog.update_date = objDVOEmployeeInfoLogEmpUpdLog.update_date;// Convert.ToDateTime(objDVOEmployeeInfoLogEmpUpdLog.update_date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[6] = objDVOEmployeeInfoLogEmpUpdLog.update_date;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOEmployeeInfoLogEmpUpdLog.INSERT_SPNAME);
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

        public static DataSet GetEmployeeInfoLog(ref DVOEmployeeInfoLogEmpUpdLog objDVO, string SocialSecurityNo)
        {

            object[] parameters = new object[5];
            parameters[0] = objDVO.empl_code;
            parameters[1] = SocialSecurityNo;
            parameters[2] = objDVO.update_by;
            parameters[3] = objDVO.startdate;
            parameters[4] = objDVO.enddate;


            List<DVOEmployeeInfoLogEmpUpdLog> objDVOList = new List<DVOEmployeeInfoLogEmpUpdLog>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

           DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOEmployeeInfoLogEmpUpdLog));           
            return ds;
        }
    }
}
