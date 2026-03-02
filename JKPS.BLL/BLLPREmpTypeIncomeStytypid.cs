using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPREmpTypeIncomeStytypid
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOMasterEmpTypeIncomes">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterEmpTypeIncomes> GetData(ref DVOMasterEmpTypeIncomes pobjDVOMasterEmpTypeIncomes)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterEmpTypeIncomes> listDVOMasterEmpTypeIncomes = new List<DVOMasterEmpTypeIncomes>();

            try
            {
                object[] parameters = new object[3];
                parameters[0] = pobjDVOMasterEmpTypeIncomes.Rowid;
                parameters[1] = pobjDVOMasterEmpTypeIncomes.type_code;
                parameters[2] = pobjDVOMasterEmpTypeIncomes.inc_code;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypeIncomes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterEmpTypeIncomes objDVOMasterEmpTypeIncomes = new DVOMasterEmpTypeIncomes();
                        objDVOMasterEmpTypeIncomes.Rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterEmpTypeIncomes.type_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeIncomes.inc_code = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeIncomes.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOMasterEmpTypeIncomes.inc_rate = (dr[4] != DBNull.Value ? (decimal?) dr[4] : null);
                        objDVOMasterEmpTypeIncomes.inc_number = (dr[5] != DBNull.Value ? (decimal?) dr[5] : null);
                        objDVOMasterEmpTypeIncomes.inc_hours = (dr[6] != DBNull.Value ? (decimal?) dr[6] : null);
                        objDVOMasterEmpTypeIncomes.acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOMasterEmpTypeIncomes.department = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOMasterEmpTypeIncomes.lo_inc_amt = (dr[9] != DBNull.Value ? (decimal?)dr[9] : null);
                        objDVOMasterEmpTypeIncomes.hi_inc_amt = (dr[10] != DBNull.Value ? (decimal?)dr[10] : null);
                        //Added by Sunil Pahwa using Update EmployeeType in Payroll 

                        objDVOMasterEmpTypeIncomes.acct_type = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]) : string.Empty);
                        objDVOMasterEmpTypeIncomes.acct_desc = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]) : string.Empty);
                        objDVOMasterEmpTypeIncomes.keyvalue = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]) : string.Empty);


                        listDVOMasterEmpTypeIncomes.Add(objDVOMasterEmpTypeIncomes);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterEmpTypeIncomes;
            }
            return listDVOMasterEmpTypeIncomes;
        }

        //Added by- Sunil Pahwa* 
        //form -updateEmployeeTypes        
        public static int InsertData(ref object objTransaction, ref List<DVOMasterEmpTypeIncomes> listDVOMasterEmpTypeIncomes)
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
                if (listDVOMasterEmpTypeIncomes.Count > 0)
                    foreach (DVOMasterEmpTypeIncomes objDVOMasterEmpTypeIncomes in listDVOMasterEmpTypeIncomes)
                    {
                        DVOMasterEmpTypeIncomes obj = objDVOMasterEmpTypeIncomes;
                        object[] InsParameter = new object[10];
                        InsParameter[0] = obj.type_code;
                        InsParameter[1] = obj.inc_code;
                        InsParameter[2] = obj.line_no;
                        InsParameter[3] = obj.inc_rate;
                        InsParameter[4] = obj.inc_number;
                        InsParameter[5] = obj.inc_hours;
                        InsParameter[6] = obj.acct_no;
                        InsParameter[7] = obj.department;
                        InsParameter[8] = obj.lo_inc_amt;
                        InsParameter[9] = obj.hi_inc_amt;


                        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref InsParameter, (new DVOMasterEmpTypeIncomes()).INSERT_SPNAME);
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
        public  static object   updateData(ref object objTransaction, ref List<DVOMasterEmpTypeIncomes> listDVOMasterEmpTypeIncomes)
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
                if(listDVOMasterEmpTypeIncomes.Count>0)
                foreach (DVOMasterEmpTypeIncomes objDVOMasterEmpTypeIncomes in listDVOMasterEmpTypeIncomes)
                {
                    object[] parameters = new object[11];
                    parameters[0] = objDVOMasterEmpTypeIncomes.Rowid;
                    parameters[1] = objDVOMasterEmpTypeIncomes.type_code;
                    parameters[2] = objDVOMasterEmpTypeIncomes.inc_code;
                    parameters[3] = objDVOMasterEmpTypeIncomes.line_no;
                    parameters[4] = objDVOMasterEmpTypeIncomes.inc_rate;
                    parameters[5] = objDVOMasterEmpTypeIncomes.inc_number;
                    parameters[6] = objDVOMasterEmpTypeIncomes.inc_hours;
                    parameters[7] = objDVOMasterEmpTypeIncomes.acct_no;
                    parameters[8] = objDVOMasterEmpTypeIncomes.department;
                    parameters[9] = objDVOMasterEmpTypeIncomes.lo_inc_amt;
                    parameters[10] = objDVOMasterEmpTypeIncomes.hi_inc_amt;



                    obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterEmpTypeIncomes), objDVOMasterEmpTypeIncomes.UPDATE_SPNAME);
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
        /// To Delete list of Employee Income codes
        /// </summary>
        /// <param name="listDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public  static int  DeleteData(ref object objTransaction, ref List<DVOMasterEmpTypeIncomes> listDeleteDVOMasterEmpTypeIncomes)
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
                if (listDeleteDVOMasterEmpTypeIncomes.Count > 0)
                    foreach (DVOMasterEmpTypeIncomes objDVOPRDVOMasterEmpTypeIncomes in listDeleteDVOMasterEmpTypeIncomes)
                    {
                        DVOMasterEmpTypeIncomes obj = objDVOPRDVOMasterEmpTypeIncomes;
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
        /// <summary>
        /// To Delete Employee Income codes
        /// </summary>
        /// <param name="objDVOMasterEmployeeIncomes">DVO object with all information to update</param>
        /// <returns></returns>
        public static int DeleteData(ref object objTransaction, ref DVOMasterEmpTypeIncomes obDVOPRDVOMasterEmpTypeIncomes)
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
                parameters[0] = obDVOPRDVOMasterEmpTypeIncomes.Rowid;
                parameters[1] = obDVOPRDVOMasterEmpTypeIncomes.type_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmpTypeIncomes()).DELETE_SPNAME);
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

    

