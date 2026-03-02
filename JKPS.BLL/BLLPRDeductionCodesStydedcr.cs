using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLPRDeductionCodesMasterDedcodes
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="objDVOEmployeeStyemplr">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOPRDeductionCodesMasterDedcodes> GetData(ref DVOPRDeductionCodesMasterDedcodes pobjDVOPRDeductionCodesMasterDedcodes)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = new List<DVOPRDeductionCodesMasterDedcodes>();

            try
            {
                object[] parameters = new object[18];
                parameters[0] = pobjDVOPRDeductionCodesMasterDedcodes.RowID;
                parameters[1] = pobjDVOPRDeductionCodesMasterDedcodes.ded_code;
                parameters[2] = pobjDVOPRDeductionCodesMasterDedcodes.description;
                parameters[3] = pobjDVOPRDeductionCodesMasterDedcodes.ded_type;
                parameters[4] = pobjDVOPRDeductionCodesMasterDedcodes.ded_taxred;
                parameters[5] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_rate;
                parameters[6] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_limit;
                parameters[7] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_acct;
                parameters[8] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_dept;
                parameters[9] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_apply;
                parameters[10] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_hi_ded_amt;
                parameters[11] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_lo_ded_amt;
                parameters[12] = pobjDVOPRDeductionCodesMasterDedcodes.state_ein;
                parameters[13] = pobjDVOPRDeductionCodesMasterDedcodes.tax_jur;
                parameters[14] = pobjDVOPRDeductionCodesMasterDedcodes.dfltaccounttype;
                parameters[15] = pobjDVOPRDeductionCodesMasterDedcodes.dfltkeyvalue;
                parameters[16] = pobjDVOPRDeductionCodesMasterDedcodes.dflt_pay_limit;
                parameters[17] = pobjDVOPRDeductionCodesMasterDedcodes.yearrollover;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPRDeductionCodesMasterDedcodes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
                        objDVOPRDeductionCodesMasterDedcodes.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOPRDeductionCodesMasterDedcodes.ded_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.description = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.ded_type = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.ded_taxred = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_rate = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_limit = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_acct = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_dept = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_apply = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_hi_ded_amt = (dr[10] != DBNull.Value ? (decimal?) (dr[10]) : null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_lo_ded_amt = (dr[11] != DBNull.Value ? (decimal?) (dr[11]) : null);
                        objDVOPRDeductionCodesMasterDedcodes.state_ein = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.tax_jur = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);
                        //Modify by sanjay
                        objDVOPRDeductionCodesMasterDedcodes.dfltaccounttype = (dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dfltaccountdesc = (dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dfltkeyvalue = (dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_pay_limit =(dr[16] != DBNull.Value ? (decimal?) (dr[16]) : null);
                        objDVOPRDeductionCodesMasterDedcodes.yearrollover = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);
                        
                        listDVOPRDeductionCodesMasterDedcodes.Add(objDVOPRDeductionCodesMasterDedcodes);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOPRDeductionCodesMasterDedcodes;
            }
            return listDVOPRDeductionCodesMasterDedcodes;
        }

        public static List<DVOPRDeductionCodesMasterDedcodes> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = new List<DVOPRDeductionCodesMasterDedcodes>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOPRDeductionCodesMasterDedcodes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
                        objDVOPRDeductionCodesMasterDedcodes.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOPRDeductionCodesMasterDedcodes.ded_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.description = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.ded_type = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.ded_taxred = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_rate = Convert.ToDecimal(dr[5] != DBNull.Value ? dr[5]: null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_limit = Convert.ToDecimal(dr[6] != DBNull.Value ? dr[6] : null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_acct = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_dept = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_apply = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_hi_ded_amt = Convert.ToDecimal(dr[10] != DBNull.Value ? dr[10] : null);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_lo_ded_amt = Convert.ToDecimal(dr[11] != DBNull.Value ? dr[11] : null);
                        objDVOPRDeductionCodesMasterDedcodes.state_ein = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.tax_jur = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dfltaccounttype = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dfltkeyvalue = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);
                        objDVOPRDeductionCodesMasterDedcodes.dflt_pay_limit = Convert.ToDecimal(dr[16] != DBNull.Value ? dr[16] : null);
                        objDVOPRDeductionCodesMasterDedcodes.yearrollover = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);

                        listDVOPRDeductionCodesMasterDedcodes.Add(objDVOPRDeductionCodesMasterDedcodes);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOPRDeductionCodesMasterDedcodes;
            }
            return listDVOPRDeductionCodesMasterDedcodes;
        }

        /// <summary>
        /// To Insert New Record
        /// </summary>
        /// <param name="objDVOPRDeductionCodesMasterDedcodes">DVO object with all information to insert</param>
        /// <returns></returns>
        public static object InsertData(ref object objTransaction, ref DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes)
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
                object[] parameters = new object[20];
                parameters[0] = objDVOPRDeductionCodesMasterDedcodes.ded_code;
                parameters[1] = objDVOPRDeductionCodesMasterDedcodes.description;
                parameters[2] = objDVOPRDeductionCodesMasterDedcodes.ded_type;
                parameters[3] = objDVOPRDeductionCodesMasterDedcodes.ded_taxred;
                parameters[4] = objDVOPRDeductionCodesMasterDedcodes.dflt_rate;
                parameters[5] = objDVOPRDeductionCodesMasterDedcodes.dflt_limit;
                parameters[6] = objDVOPRDeductionCodesMasterDedcodes.dflt_acct;
                parameters[7] = objDVOPRDeductionCodesMasterDedcodes.dflt_dept;
                parameters[8] = objDVOPRDeductionCodesMasterDedcodes.dflt_apply;
                parameters[9] = objDVOPRDeductionCodesMasterDedcodes.dflt_hi_ded_amt;
                parameters[10] = objDVOPRDeductionCodesMasterDedcodes.dflt_lo_ded_amt;
                parameters[11] = objDVOPRDeductionCodesMasterDedcodes.state_ein;
                parameters[12] = objDVOPRDeductionCodesMasterDedcodes.tax_jur;
                parameters[13] = objDVOPRDeductionCodesMasterDedcodes.dfltaccounttype;
                parameters[14] = objDVOPRDeductionCodesMasterDedcodes.dfltkeyvalue;
                parameters[15] = objDVOPRDeductionCodesMasterDedcodes.dflt_pay_limit;
                parameters[16] = objDVOPRDeductionCodesMasterDedcodes.yearrollover;
                parameters[17] = objDVOPRDeductionCodesMasterDedcodes.InsertMachineInfo;
                if (objDVOPRDeductionCodesMasterDedcodes.InsertDate == string.Empty) objDVOPRDeductionCodesMasterDedcodes.InsertDate = null;
                parameters[18] = objDVOPRDeductionCodesMasterDedcodes.InsertDate;
                parameters[19] = objDVOPRDeductionCodesMasterDedcodes.InsertBy;
                
                obj=objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPRDeductionCodesMasterDedcodes),true);

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return obj;
                }
                else
                    throw ex;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return obj;
        }

        /// <summary>
        /// To Update Record
        /// </summary>
        /// <param name="objDVOPRDeductionCodesMasterDedcodes">DVO object with all information to update</param>
        /// <returns>object</returns>
        public static Object UpdateData(ref object objTransaction, ref DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            object obj = null;
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[21];
                parameters[0] = objDVOPRDeductionCodesMasterDedcodes.RowID;
                parameters[1] = objDVOPRDeductionCodesMasterDedcodes.ded_code;
                parameters[2] = objDVOPRDeductionCodesMasterDedcodes.description;
                parameters[3] = objDVOPRDeductionCodesMasterDedcodes.ded_type;
                parameters[4] = objDVOPRDeductionCodesMasterDedcodes.ded_taxred;
                parameters[5] = objDVOPRDeductionCodesMasterDedcodes.dflt_rate;
                parameters[6] = objDVOPRDeductionCodesMasterDedcodes.dflt_limit;
                parameters[7] = objDVOPRDeductionCodesMasterDedcodes.dflt_acct;
                parameters[8] = objDVOPRDeductionCodesMasterDedcodes.dflt_dept;
                parameters[9] = objDVOPRDeductionCodesMasterDedcodes.dflt_apply;
                parameters[10] = objDVOPRDeductionCodesMasterDedcodes.dflt_hi_ded_amt;
                parameters[11] = objDVOPRDeductionCodesMasterDedcodes.dflt_lo_ded_amt;
                parameters[12] = objDVOPRDeductionCodesMasterDedcodes.state_ein;
                parameters[13] = objDVOPRDeductionCodesMasterDedcodes.tax_jur;
                parameters[14] = objDVOPRDeductionCodesMasterDedcodes.dfltaccounttype;
                parameters[15] = objDVOPRDeductionCodesMasterDedcodes.dfltkeyvalue;
                parameters[16] = objDVOPRDeductionCodesMasterDedcodes.dflt_pay_limit;
                parameters[17] = objDVOPRDeductionCodesMasterDedcodes.yearrollover;
                parameters[18] = objDVOPRDeductionCodesMasterDedcodes.UpdateMachineInfo;
                if (objDVOPRDeductionCodesMasterDedcodes.UpdateDate == string.Empty) objDVOPRDeductionCodesMasterDedcodes.UpdateDate = null;
                parameters[19] = objDVOPRDeductionCodesMasterDedcodes.UpdateDate;
                parameters[20] = objDVOPRDeductionCodesMasterDedcodes.UpdateBy;

                obj=objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPRDeductionCodesMasterDedcodes),true);

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction )
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return obj;
                }
                else
                    throw ex;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return obj;
        }

        /// <summary>
        /// To Delete Record
        /// </summary>
        /// <param name="objDVOPRDeductionCodesMasterDedcodes">DVO object with all information to delete</param>
        /// <returns>object</returns>
        public static Object DeleteData(ref DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes)
        {           
            object obj = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVOPRDeductionCodesMasterDedcodes.RowID;
                parameters[1] = objDVOPRDeductionCodesMasterDedcodes.ded_code;
                obj = objDALBaseClass.DeleteData(ref parameters, typeof(DVOPRDeductionCodesMasterDedcodes), true);
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
        /// <param name="objDeductionCodes">reference of DVOPRDeductionCodesMasterDedcodes type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having deduction code information</returns>
        public static List<DVOPRDeductionCodesMasterDedcodes> GetDeductionCodes(ref DVOPRDeductionCodesMasterDedcodes objDeductionCodes)
        {

            List<DVOPRDeductionCodesMasterDedcodes> objDeductionCodeslist = new List<DVOPRDeductionCodesMasterDedcodes>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPRDeductionCodesMasterDedcodes), objDeductionCodes.GetDeductionCodes))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOPRDeductionCodesMasterDedcodes obj_DeductionCodes = new DVOPRDeductionCodesMasterDedcodes();

                    obj_DeductionCodes.ded_code = dr[0].ToString().Trim();//ded_code
                    obj_DeductionCodes.description = dr[1].ToString().Trim();//description
                    obj_DeductionCodes.dfltkeyvalue = dr[2].ToString().Trim();//keyvalue

                    objDeductionCodeslist.Add(obj_DeductionCodes);
                }
            }
            return objDeductionCodeslist;
        }

    }
}
