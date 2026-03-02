using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPREmpTypeDeductionStytypdd
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOMasterEmpTypeDeductions">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmpTypeDeductions> GetData(ref DVOMasterEmpTypeDeductions pobjDVOMasterEmpTypeDeductions)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions = new List<DVOMasterEmpTypeDeductions>();

            try
            {
                object[] parameters = new object[3];
                parameters[0] = pobjDVOMasterEmpTypeDeductions.Rowid;
                parameters[1] = pobjDVOMasterEmpTypeDeductions.type_code;
                parameters[2] = pobjDVOMasterEmpTypeDeductions.ded_code;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypeDeductions)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmpTypeDeductions objDVOMasterEmpTypeDeductions = new DVOMasterEmpTypeDeductions();
                        objDVOMasterEmpTypeDeductions.Rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterEmpTypeDeductions.type_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeDeductions.ded_code = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeDeductions.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOMasterEmpTypeDeductions.ded_rate = (dr[4] != DBNull.Value ? (decimal?)dr[4] : null);
                        objDVOMasterEmpTypeDeductions.ded_limit = (dr[5] != DBNull.Value ? (decimal?) dr[5] : null);
                        objDVOMasterEmpTypeDeductions.ded_apply = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeDeductions.acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOMasterEmpTypeDeductions.department = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeDeductions.lo_ded_amt =(dr[9] != DBNull.Value ? (decimal?)dr[9] : null);
                        objDVOMasterEmpTypeDeductions.hi_ded_amt =(dr[10] != DBNull.Value ? (decimal?) dr[10] : null);
                        objDVOMasterEmpTypeDeductions.pay_limit = (dr[11] != DBNull.Value ? (decimal?) dr[11] : null);

                        //Added by Sunil for emptypes
                        objDVOMasterEmpTypeDeductions.acct_type = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]) : string.Empty);
                        objDVOMasterEmpTypeDeductions.acct_desc = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]) : string.Empty);
                        objDVOMasterEmpTypeDeductions.keyvalue = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]) : string.Empty);
                        listDVOMasterEmpTypeDeductions.Add(objDVOMasterEmpTypeDeductions);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmpTypeDeductions;
            }
            return listDVOMasterEmpTypeDeductions;
        }

        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
        public  static int InsertData(ref object objTransaction, ref List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions)
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
                if (listDVOMasterEmpTypeDeductions.Count > 0)
                    foreach (DVOMasterEmpTypeDeductions objDVOMasterEmpTypeDeductions in listDVOMasterEmpTypeDeductions)
                    {

                        DVOMasterEmpTypeDeductions obj = objDVOMasterEmpTypeDeductions;
                        object[] InsParameter = new object[11];
                        InsParameter[0] = obj.type_code;
                        InsParameter[1] = obj.ded_code;
                        InsParameter[2] = obj.line_no;
                        InsParameter[3] = obj.ded_rate;
                        InsParameter[4] = obj.ded_limit;
                        InsParameter[5] = obj.ded_apply;
                        InsParameter[6] = obj.acct_no;
                        InsParameter[7] = obj.department;
                        InsParameter[8] = obj.lo_ded_amt;
                        InsParameter[9] = obj.hi_ded_amt;
                        InsParameter[10] = obj.pay_limit;

                        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref InsParameter, (new DVOMasterEmpTypeDeductions()).INSERT_SPNAME);
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
        public  static object  updateData(ref object objTransaction, ref List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions)
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
                if (listDVOMasterEmpTypeDeductions.Count > 0)
                    foreach (DVOMasterEmpTypeDeductions objDDVOPREmpTyeDeductionStytypdd in listDVOMasterEmpTypeDeductions)
                    {
                        object[] parameters = new object[12];
                        parameters[0] = objDDVOPREmpTyeDeductionStytypdd.Rowid;
                        parameters[1] = objDDVOPREmpTyeDeductionStytypdd.type_code;
                        parameters[2] = objDDVOPREmpTyeDeductionStytypdd.ded_code;
                        parameters[3] = objDDVOPREmpTyeDeductionStytypdd.line_no;
                        parameters[4] = objDDVOPREmpTyeDeductionStytypdd.ded_rate;
                        parameters[5] = objDDVOPREmpTyeDeductionStytypdd.ded_limit;
                        parameters[6] = objDDVOPREmpTyeDeductionStytypdd.ded_apply;
                        parameters[7] = objDDVOPREmpTyeDeductionStytypdd.acct_no;
                        parameters[8] = objDDVOPREmpTyeDeductionStytypdd.department;
                        parameters[9] = objDDVOPREmpTyeDeductionStytypdd.lo_ded_amt;
                        parameters[10] = objDDVOPREmpTyeDeductionStytypdd.lo_ded_amt;
                        parameters[11] = objDDVOPREmpTyeDeductionStytypdd.pay_limit;


                        obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterEmpTypeDeductions), objDDVOPREmpTyeDeductionStytypdd.UPDATE_SPNAME);
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
        /// To Delete Employee Deduction Codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmpTypeDeductions objDVOPDVOMasterEmpTypeDeductions)
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
                parameters[0] = objDVOPDVOMasterEmpTypeDeductions.Rowid;
                parameters[1] = objDVOPDVOMasterEmpTypeDeductions.type_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmpTypeDeductions()).DELETE_SPNAME);
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



        //Added by -Sunil Pahwa* 
        //form -updateEmployeeTypes        
        /// <summary>
        /// To Delete list of Employee Deduction Codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeDeductions">DVO object with all information to update</param>
        /// <returns></returns>
        public static int  DeleteData(ref object objTransaction, ref List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions)
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
                if (listDVOMasterEmpTypeDeductions.Count > 0)
                    foreach (DVOMasterEmpTypeDeductions objDVODVOMasterEmployeeDeductions in listDVOMasterEmpTypeDeductions)
                    {
                        DVOMasterEmpTypeDeductions obj = objDVODVOMasterEmployeeDeductions;
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


    
        
    }
}
