using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPREmpTypeObligationStytypod
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOMasterEmpTypeObligations">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmpTypeObligations> GetData(ref DVOMasterEmpTypeObligations pobjDVOMasterEmpTypeObligations)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmpTypeObligations> listDVOMasterEmpTypeObligations = new List<DVOMasterEmpTypeObligations>();

            try
            {
                object[] parameters = new object[3];
                parameters[0] = pobjDVOMasterEmpTypeObligations.Rowid;
                parameters[1] = pobjDVOMasterEmpTypeObligations.type_code;
                parameters[2] = pobjDVOMasterEmpTypeObligations.obl_code;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypeObligations)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmpTypeObligations objDVOMasterEmpTypeObligations = new DVOMasterEmpTypeObligations();
                        objDVOMasterEmpTypeObligations.Rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterEmpTypeObligations.type_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeObligations.obl_code = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeObligations.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOMasterEmpTypeObligations.obl_rate = (dr[4] != DBNull.Value ? (decimal?) dr[4] : null);
                        objDVOMasterEmpTypeObligations.obl_limit = (dr[5] != DBNull.Value ? (decimal?)dr[5] : null);
                        objDVOMasterEmpTypeObligations.acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterEmpTypeObligations.department = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeObligations.bal_acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                        objDVOMasterEmpTypeObligations.bal_dept = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeObligations.pay_limit = (dr[10] != DBNull.Value ? (decimal?) dr[10] : null);

                        objDVOMasterEmpTypeObligations.acct_type = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]) : string.Empty);
                        objDVOMasterEmpTypeObligations.acct_desc = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]) : string.Empty);
                        objDVOMasterEmpTypeObligations.keyvalue = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]) : string.Empty);
                        objDVOMasterEmpTypeObligations.acct_type1 = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]) : string.Empty);
                        objDVOMasterEmpTypeObligations.acct_desc1 = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]) : string.Empty);
                        objDVOMasterEmpTypeObligations.keyvalue1 = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]) : string.Empty);
                        listDVOMasterEmpTypeObligations.Add(objDVOMasterEmpTypeObligations);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmpTypeObligations;
            }
            return listDVOMasterEmpTypeObligations;
        }

        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
        public  static int InsertData(ref object objTransaction, ref List<DVOMasterEmpTypeObligations> listDVOMasterEmpTypeObligations)
        {
            //object o = null;
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
                if (listDVOMasterEmpTypeObligations.Count > 0)
                    foreach (DVOMasterEmpTypeObligations objDVOMasterEmpTypeObligations in listDVOMasterEmpTypeObligations)
                    {

                        DVOMasterEmpTypeObligations obj = objDVOMasterEmpTypeObligations;
                        object[] InsParameter = new object[10];
                        InsParameter[0] = obj.type_code;
                        InsParameter[1] = obj.obl_code;
                        InsParameter[2] = obj.line_no;
                        InsParameter[3] = obj.obl_rate;
                        InsParameter[4] = obj.obl_limit;
                        InsParameter[5] = obj.acct_no;
                        InsParameter[6] = obj.department;
                        InsParameter[7] = obj.bal_acct_no;
                        InsParameter[8] = obj.bal_dept;
                        InsParameter[9] = obj.pay_limit;

                        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref InsParameter,(new DVOMasterEmpTypeObligations()).INSERT_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();

                        InsParameter = null;

                    }
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
        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
        public  static object  updateData(ref object objTransaction, ref List<DVOMasterEmpTypeObligations> listDVOMasterEmpTypeObligations)
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
                if (listDVOMasterEmpTypeObligations.Count > 0)
                    foreach (DVOMasterEmpTypeObligations objDVOPREmpTypObligationStytypod in listDVOMasterEmpTypeObligations)
                    {
                        object[] parameters = new object[11];
                        parameters[0] = objDVOPREmpTypObligationStytypod.Rowid;
                        parameters[1] = objDVOPREmpTypObligationStytypod.type_code;
                        parameters[2] = objDVOPREmpTypObligationStytypod.obl_code;
                        parameters[3] = objDVOPREmpTypObligationStytypod.line_no;
                        parameters[4] = objDVOPREmpTypObligationStytypod.obl_rate;
                        parameters[5] = objDVOPREmpTypObligationStytypod.obl_limit;
                        parameters[6] = objDVOPREmpTypObligationStytypod.acct_no;
                        parameters[7] = objDVOPREmpTypObligationStytypod.department;
                        parameters[8] = objDVOPREmpTypObligationStytypod.bal_acct_no;
                        parameters[9] = objDVOPREmpTypObligationStytypod.bal_dept;
                        parameters[10] = objDVOPREmpTypObligationStytypod.pay_limit;



                        obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterEmpTypeObligations), objDVOPREmpTypObligationStytypod.UPDATE_SPNAME);
                        parameters = null;
                    }
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
        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
         /// <summary>
        /// To Delete list of Employee Obligation Code
        /// </summary>
        /// <param name="listDVOMasterEmployeeObligations">DVO object with all information to update</param>
        /// <returns></returns>

        public  static int DeleteData(ref object objTransaction, ref List<DVOMasterEmpTypeObligations> listDeleteDVOMasterEmpTypeObligations)
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
                if (listDeleteDVOMasterEmpTypeObligations.Count > 0)
                    foreach (DVOMasterEmpTypeObligations objDVODVOMasterEmpTypeObligations in listDeleteDVOMasterEmpTypeObligations)
                    {
                        DVOMasterEmpTypeObligations obj = objDVODVOMasterEmpTypeObligations;
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
        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmpTypeObligations objDVOPDVOMasterEmpTypeObligations)
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
                parameters[0] = objDVOPDVOMasterEmpTypeObligations.Rowid;
                parameters[1] = objDVOPDVOMasterEmpTypeObligations.type_code;

                //object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeObligations()).DELETE_SPNAME);
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPDVOMasterEmpTypeObligations.DELETE_SPNAME);
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
