using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
namespace JKPS.BLL
{
    public class BLLPRUpdateTaxTables
    {/// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="pobjDVODeductionTaxTables">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVODeductionTaxTables> GetData(ref DVODeductionTaxTables pobjDVODeductionTaxTables)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVODeductionTaxTables> listDVODeductionTaxTables = new List<DVODeductionTaxTables>();
            try
            {
                object[] parameters = new object[11];
                parameters[0] = pobjDVODeductionTaxTables.tax_year;
                parameters[1] = pobjDVODeductionTaxTables.ded_code;
                parameters[2] = pobjDVODeductionTaxTables.week_allow;
                parameters[3] = pobjDVODeductionTaxTables.biweek_allow;
                parameters[4] = pobjDVODeductionTaxTables.smonth_allow;
                parameters[5] = pobjDVODeductionTaxTables.month_allow;
                parameters[6] = pobjDVODeductionTaxTables.quarter_allow;
                parameters[7] = pobjDVODeductionTaxTables.syear_allow;
                parameters[8] = pobjDVODeductionTaxTables.year_allow;
                parameters[9] = pobjDVODeductionTaxTables.misc_allow;
                parameters[10] = pobjDVODeductionTaxTables.RowID;
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionTaxTables)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVODeductionTaxTables objDVODeductionTaxTables = new DVODeductionTaxTables();
                        objDVODeductionTaxTables.tax_year = (dr[0] != DBNull.Value ? (dr[0].ToString().Trim()) : string.Empty);
                        objDVODeductionTaxTables.ded_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVODeductionTaxTables.week_allow = (dr[2] != DBNull.Value ? (decimal?)dr[2] : null);
                        objDVODeductionTaxTables.biweek_allow = (dr[3] != DBNull.Value ? (decimal?)dr[3] : null);
                        objDVODeductionTaxTables.smonth_allow = (dr[4] != DBNull.Value ? (decimal?)dr[4] : null);
                        objDVODeductionTaxTables.month_allow = (dr[5] != DBNull.Value ? (decimal?)dr[5] : null);
                        objDVODeductionTaxTables.quarter_allow = (dr[6] != DBNull.Value ? (decimal?)dr[6] : null);
                        objDVODeductionTaxTables.syear_allow = (dr[7] != DBNull.Value ? (decimal?)dr[7] : null);
                        objDVODeductionTaxTables.year_allow = (dr[8] != DBNull.Value ? (decimal?)dr[8] : null);
                        objDVODeductionTaxTables.misc_allow = (dr[9] != DBNull.Value ? (decimal?)dr[9] : null);
                        objDVODeductionTaxTables.RowID = Convert.ToInt32(dr[11].ToString());
                        listDVODeductionTaxTables.Add(objDVODeductionTaxTables);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVODeductionTaxTables;
            }
            return listDVODeductionTaxTables;
        }

        /// <summary>
        /// Get Tax Table detail for a particular Tax-Year and Tax-Code
        /// </summary>
        /// <param name="objGLAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>dataset with information of segments of particular Account-Type</returns>
        public static DataSet GetPRTaxDetail(ref DVODeductionTaxTables objPRUpdateTaxTables)
        {
            object[] parameters = new object[2];
            parameters[0] = objPRUpdateTaxTables.tax_year;
            parameters[1] = objPRUpdateTaxTables.ded_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.GET_PR_TAX_DETAIL);//
            parameters = null;
            objDalBaseClass = null;
            return ds;
        }
        /// <summary>
        /// This method is use to insert new Tax Code and detail into database
        /// </summary>
        /// <param name="objPRUpdateTaxTables">reference of DVODeductionTaxTables type object as a collection of parameters of search criteria.</param>
        ///  /// <param name="lstDVODeductionTaxTables">reference of lstDVODeductionTaxTables type object as a collection of detail information.</param>
        /// <returns>return object  for confirmation, either data is inserted or not</returns>       
        public static object InsertData(ref DVODeductionTaxTables objPRUpdateTaxTables, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] parameters = new object[10];
                parameters[0] = objPRUpdateTaxTables.tax_year;
                parameters[1] = objPRUpdateTaxTables.ded_code;
                parameters[2] = objPRUpdateTaxTables.week_allow;
                parameters[3] = objPRUpdateTaxTables.biweek_allow;
                parameters[4] = objPRUpdateTaxTables.smonth_allow;
                parameters[5] = objPRUpdateTaxTables.month_allow;
                parameters[6] = objPRUpdateTaxTables.quarter_allow;
                parameters[7] = objPRUpdateTaxTables.syear_allow;
                parameters[8] = objPRUpdateTaxTables.year_allow;
                parameters[9] = objPRUpdateTaxTables.misc_allow;
                //Insert master information into database
                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), true);
                if (obj.ToString() == "1")
                {
                    //after successfully insertion of master information insert detail information
                    //record by record
                    // foreach (DVODeductionTaxTables objUpdateTaxTables in lstDVODeductionTaxTables)                       

                    BLLPRUpdateTaxTables.InsertDetail(ref objTransaction, ref lstDVODeductionTaxTables);
                }

                parameters = null;
                objDALBaseClass = null;

                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;
        }
        /// <summary>
        /// This method is use to insert new Tax code detail into database
        /// </summary>
        /// <param name="objTransaction">reference of transaction object.</param>        
        ///  /// <param name="lstDVODeductionTaxTables">reference of lstDVODeductionTaxTables type object as a collection of detail information.</param>
        /// <returns>return object  for confirmation, either data is inserted or not</returns>  
        public static object InsertDetail(ref object objTransaction, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
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
                foreach (DVODeductionTaxTables objPRUpdateTaxTables in lstDVODeductionTaxTables)
                {
                    object[] parameters = new object[8];
                    parameters[0] = objPRUpdateTaxTables.tax_year;
                    parameters[1] = objPRUpdateTaxTables.ded_code;
                    parameters[2] = objPRUpdateTaxTables.pay_period;
                    parameters[3] = objPRUpdateTaxTables.marital_stat;
                    parameters[4] = objPRUpdateTaxTables.over_amt;
                    parameters[5] = objPRUpdateTaxTables.base_amt;
                    parameters[6] = objPRUpdateTaxTables.tax_rate;
                    //Set order number on the basis of pay_period

                    if (objPRUpdateTaxTables.pay_period != string.Empty)
                    {
                        string pay_period = objPRUpdateTaxTables.pay_period.ToString();
                        objPRUpdateTaxTables.order_no = GetOrderNo(pay_period);
                        parameters[7] = objPRUpdateTaxTables.order_no;
                    }
                    else
                    {
                        parameters[7] = objPRUpdateTaxTables.order_no;
                    }

                    obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.Ins_Tax_Code_Detail);
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


        //commented by Sunil pahwa

        /// <summary>
        /// This method is use to update Tax Code and detail into database
        /// </summary>
        /// <param name="objPRUpdateTaxTables">reference of DVODeductionTaxTables type object as a collection of parameters of search criteria.</param>
        ///  /// <param name="lstDVODeductionTaxTables">reference of lstDVODeductionTaxTables type object as a collection of detail information.</param>
        /// <returns>return object  for confirmation, either data is updated or not</returns>       
        //public static object UpdateData(ref DVODeductionTaxTables objPRUpdateTaxTables, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
        //{
        //    DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    object obj = null;
        //    try
        //    {
        //        object[] parameters = new object[10];
        //        parameters[0] = objPRUpdateTaxTables.tax_year;
        //        parameters[1] = objPRUpdateTaxTables.ded_code;
        //        parameters[2] = objPRUpdateTaxTables.week_allow;
        //        parameters[3] = objPRUpdateTaxTables.biweek_allow;
        //        parameters[4] = objPRUpdateTaxTables.smonth_allow;
        //        parameters[5] = objPRUpdateTaxTables.month_allow;
        //        parameters[6] = objPRUpdateTaxTables.quarter_allow;
        //        parameters[7] = objPRUpdateTaxTables.syear_allow;
        //        parameters[8] = objPRUpdateTaxTables.year_allow;
        //        parameters[9] = objPRUpdateTaxTables.misc_allow;

        //        obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), true);
        //        if (obj.ToString() == "1")
        //        {
        //            //First delete the data from database
        //            BLLPRUpdateTaxTables.DeleteDetail(ref objPRUpdateTaxTables);
        //            //then Insert again record by record
        //            BLLPRUpdateTaxTables.InsertDetail(ref objTransaction, ref lstDVODeductionTaxTables);
        //        }

        //        parameters = null;
        //        objDALBaseClass = null;

        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManager.Publish(ex);
        //        return obj;
        //    }
        //    return obj;
        //}
        //public static object UpdateDetail(ref object objTransaction, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object obj = null;

        //    try
        //    {
        //        foreach (DVODeductionTaxTables objPRUpdateTaxTables in lstDVODeductionTaxTables)
        //        {
        //            object[] parameters = new object[8];
        //            parameters[0] = objPRUpdateTaxTables.tax_year;
        //            parameters[1] = objPRUpdateTaxTables.ded_code;
        //            parameters[2] = objPRUpdateTaxTables.pay_period;
        //            parameters[3] = objPRUpdateTaxTables.marital_stat;
        //            parameters[4] = objPRUpdateTaxTables.over_amt;
        //            parameters[5] = objPRUpdateTaxTables.base_amt;
        //            parameters[6] = objPRUpdateTaxTables.tax_rate;
        //            //Set order number on the basis of pay_period
        //            int order_no;
        //            if (objPRUpdateTaxTables.pay_period != string.Empty)
        //            {
        //                string pay_period = objPRUpdateTaxTables.pay_period.ToString();
        //                objPRUpdateTaxTables.order_no = GetOrderNo(pay_period);
        //                parameters[7] = objPRUpdateTaxTables.order_no;
        //            }                    

        //            obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.Ins_Tax_Code_Detail);
        //            parameters = null;
        //        }
        //        objDALBaseClass = null;
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        else
        //            throw ex;

        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return obj;
        //    }
        //    return obj;
        //}

        /// <summary>
        /// To Delete Record
        /// </summary>
        /// <param name="objDVODeductionTaxTables">DVO object with all information to delete</param>
        /// <returns>object</returns>
        public static Object DeleteData(ref DVODeductionTaxTables objDVODeductionTaxTables)
        {
            object obj = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVODeductionTaxTables.tax_year;
                parameters[1] = objDVODeductionTaxTables.ded_code;
                obj = objDALBaseClass.DeleteData(ref parameters, typeof(DVODeductionTaxTables), true);
                if (obj.ToString() == "1")
                {
                    BLLPRUpdateTaxTables.DeleteDetail(ref objDVODeductionTaxTables);
                }
                parameters = null;
                objDALBaseClass = null;
                return obj;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;
        }
        /// <summary>
        /// To Delete Record
        /// </summary>
        /// <param name="objDVODeductionTaxTables">reference of DVODeductionTaxTables type object as a collection of parameters</param>
        /// <returns>object</returns>
        public static Object DeleteDetail(ref DVODeductionTaxTables objDVODeductionTaxTables)
        {
            object obj = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVODeductionTaxTables.tax_year;
                parameters[1] = objDVODeductionTaxTables.ded_code;
                obj = objDALBaseClass.DeleteData(ref parameters, typeof(DVODeductionTaxTables), objDVODeductionTaxTables.DEL_PR_TAX_DETAIL);
                parameters = null;
                objDALBaseClass = null;
                return obj;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;
        }

        /// <summary>
        /// This method is use to get deduction code from database 
        /// </summary>
        /// <param name="objDeductionCodes">reference of DVODeductionTaxTables type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having deduction code information</returns>
        public static List<DVODeductionTaxTables> GetDeductionCodes(ref DVODeductionTaxTables objDeductionCodes)
        {

            List<DVODeductionTaxTables> objDeductionCodeslist = new List<DVODeductionTaxTables>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionTaxTables)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVODeductionTaxTables obj_DeductionCodes = new DVODeductionTaxTables();

                    //obj_DeductionCodes.ded_code = dr[0].ToString().Trim();//ded_code
                    //obj_DeductionCodes.description = dr[1].ToString().Trim();//description
                    //obj_DeductionCodes.dfltkeyvalue = dr[2].ToString().Trim();//keyvalue

                    objDeductionCodeslist.Add(obj_DeductionCodes);
                }
            }
            return objDeductionCodeslist;
        }
        public static int GetOrderNo(string pay_period)
        {
            DVODeductionTaxTables objPRUpdateTaxTables = new DVODeductionTaxTables();
            if (pay_period != string.Empty)
            {
                if (pay_period == "W")
                {
                    objPRUpdateTaxTables.order_no = 1;
                }
                else if (pay_period == "B")
                {
                    objPRUpdateTaxTables.order_no = 2;
                }
                else if (pay_period == "B")
                {
                    objPRUpdateTaxTables.order_no = 3;
                }
                else if (pay_period == "M")
                {
                    objPRUpdateTaxTables.order_no = 4;
                }
                else if (pay_period == "Q")
                {
                    objPRUpdateTaxTables.order_no = 5;
                }
                else if (pay_period == "H")
                {
                    objPRUpdateTaxTables.order_no = 6;
                }
                else if (pay_period == "A")
                {
                    objPRUpdateTaxTables.order_no = 7;
                }
                else if (pay_period == "D")
                {
                    objPRUpdateTaxTables.order_no = 8;
                }
                else
                {
                    objPRUpdateTaxTables.order_no = 9;
                }

            }
            return objPRUpdateTaxTables.order_no;
            objPRUpdateTaxTables = null;
        }


        public static int UpdateDataInfo(ref object objTransaction, ref DVODeductionTaxTables objPRUpdateTaxTables, ref List<DVODeductionTaxTables> listNewDVODeductionTaxTables,
                                           ref List<DVODeductionTaxTables> listUpdateDVODeductionTaxTables, ref List<DVODeductionTaxTables> listDeleteDVODeductionTaxTables)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objPRUpdateTaxTables.tax_year;
                parameters[1] = objPRUpdateTaxTables.ded_code;
                parameters[2] = objPRUpdateTaxTables.week_allow;
                parameters[3] = objPRUpdateTaxTables.biweek_allow;
                parameters[4] = objPRUpdateTaxTables.smonth_allow;
                parameters[5] = objPRUpdateTaxTables.month_allow;
                parameters[6] = objPRUpdateTaxTables.quarter_allow;
                parameters[7] = objPRUpdateTaxTables.syear_allow;
                parameters[8] = objPRUpdateTaxTables.year_allow;
                parameters[9] = objPRUpdateTaxTables.misc_allow;
                parameters[10] = objPRUpdateTaxTables.RowID;
                object obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), true);
                if (obj.ToString() == "1")
                {
                    //First delete the data from database
                    //BLLPRUpdateTaxTables.DeleteDetail(ref objPRUpdateTaxTables);
                    //then Insert again record by record
                    if (listNewDVODeductionTaxTables.Count > 0)
                        BLLPRUpdateTaxTables.InsertDetail(ref objTransaction, ref listNewDVODeductionTaxTables);
                    if (listDeleteDVODeductionTaxTables.Count > 0)
                        BLLPRUpdateTaxTables.DetailDetailByRowId(ref objTransaction, ref listDeleteDVODeductionTaxTables);
                    if (listUpdateDVODeductionTaxTables.Count > 0)
                        BLLPRUpdateTaxTables.UpdateDetail(ref objTransaction, ref listUpdateDVODeductionTaxTables);
                }



                parameters = null;

                if (statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return 0;
            }

            return 1;
        }
        /// <summary>
        /// This method is use to insert new Tax code detail into database
        /// </summary>
        /// <param name="objTransaction">reference of transaction object.</param>        
        ///  /// <param name="lstDVODeductionTaxTables">reference of lstDVODeductionTaxTables type object as a collection of detail information.</param>
        /// <returns>return object  for confirmation, either data is inserted or not</returns>  
        public static object UpdateDetail(ref object objTransaction, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
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
                foreach (DVODeductionTaxTables objPRUpdateTaxTables in lstDVODeductionTaxTables)
                {
                    object[] parameters = new object[9];
                    parameters[0] = objPRUpdateTaxTables.tax_year;
                    parameters[1] = objPRUpdateTaxTables.ded_code;
                    parameters[2] = objPRUpdateTaxTables.pay_period;
                    parameters[3] = objPRUpdateTaxTables.marital_stat;
                    parameters[4] = objPRUpdateTaxTables.over_amt;
                    parameters[5] = objPRUpdateTaxTables.base_amt;
                    parameters[6] = objPRUpdateTaxTables.tax_rate;
                    parameters[7] = objPRUpdateTaxTables.RowID;
                    //Set order number on the basis of pay_period
                    if (objPRUpdateTaxTables.pay_period != string.Empty)
                    {
                        string pay_period = objPRUpdateTaxTables.pay_period.ToString();
                        objPRUpdateTaxTables.order_no = GetOrderNo(pay_period);
                        parameters[8] = objPRUpdateTaxTables.order_no;
                    }
                    else
                    {
                        parameters[8] = objPRUpdateTaxTables.order_no;
                    }

                    obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.Upd_Tax_Code_Detail);
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
        public static object DetailDetailByRowId(ref object objTransaction, ref List<DVODeductionTaxTables> lstDVODeductionTaxTables)
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
                foreach (DVODeductionTaxTables objPRUpdateTaxTables in lstDVODeductionTaxTables)
                {
                    object[] parameters = new object[3];
                    parameters[0] = objPRUpdateTaxTables.tax_year;
                    parameters[1] = objPRUpdateTaxTables.ded_code;
                    parameters[2] = objPRUpdateTaxTables.RowID;
                    //Set order number on the basis of pay_period

                    obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.Del_Tax_Code_Detail_By_RowId);
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

        // Added by Sarvjeet Verma On 30/04/2009 
        public static DataSet GetDataPrintTaxTableHeader(ref DVODeductionTaxTables pobjDVODeductionTaxTables)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[11];
            parameters[0] = pobjDVODeductionTaxTables.tax_year;
            parameters[1] = pobjDVODeductionTaxTables.ded_code;
            parameters[2] = pobjDVODeductionTaxTables.week_allow;
            parameters[3] = pobjDVODeductionTaxTables.biweek_allow;
            parameters[4] = pobjDVODeductionTaxTables.smonth_allow;
            parameters[5] = pobjDVODeductionTaxTables.month_allow;
            parameters[6] = pobjDVODeductionTaxTables.quarter_allow;
            parameters[7] = pobjDVODeductionTaxTables.syear_allow;
            parameters[8] = pobjDVODeductionTaxTables.year_allow;
            parameters[9] = pobjDVODeductionTaxTables.misc_allow;
            parameters[10] = pobjDVODeductionTaxTables.RowID;
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionTaxTables));
            return ds;
        }
        public static DataSet GetDataPrintTaxTableDetail(ref DVODeductionTaxTables objPRUpdateTaxTables)
        {
            object[] parameters = new object[2];
            parameters[0] = objPRUpdateTaxTables.tax_year;
            parameters[1] = objPRUpdateTaxTables.ded_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVODeductionTaxTables), objPRUpdateTaxTables.GET_PR_TAX_DETAIL_RPT);//
            parameters = null;
            objDalBaseClass = null;
            return ds;
        }
    }
}

