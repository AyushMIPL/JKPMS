using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPRObligationCodesMasterOblCodes
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="pobjDVOMasterOblCodes">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOMasterOblCodes> GetData(ref DVOMasterOblCodes pobjDVOMasterOblCodes)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterOblCodes> listDVOMasterOblCodes = new List<DVOMasterOblCodes>();

            try
            {
                object[] parameters = new object[14];
                parameters[0] = pobjDVOMasterOblCodes.RowID;
                parameters[1] = pobjDVOMasterOblCodes.obl_code;
                parameters[2] = pobjDVOMasterOblCodes.description;
                parameters[3] = pobjDVOMasterOblCodes.obl_type;
                parameters[4] = pobjDVOMasterOblCodes.dflt_rate;
                parameters[5] = pobjDVOMasterOblCodes.dflt_limit;
                parameters[6] = pobjDVOMasterOblCodes.dflt_acct;
                parameters[7] = pobjDVOMasterOblCodes.dflt_dept;
                parameters[8] = pobjDVOMasterOblCodes.dflt_bacct;
                parameters[9] = pobjDVOMasterOblCodes.dflt_bdept;
                parameters[10] = pobjDVOMasterOblCodes.dfltaccounttype;
                parameters[11] = pobjDVOMasterOblCodes.dfltbaccounttype;
                parameters[12] = pobjDVOMasterOblCodes.dfltbkeyvalue;
                parameters[13] = pobjDVOMasterOblCodes.dflt_pay_limit;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterOblCodes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterOblCodes objDVOMasterOblCodes = new DVOMasterOblCodes();
                        objDVOMasterOblCodes.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterOblCodes.obl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.description = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.obl_type = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                        objDVOMasterOblCodes.dflt_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                        objDVOMasterOblCodes.dflt_acct = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterOblCodes.dflt_dept = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_bacct = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                        objDVOMasterOblCodes.dflt_bdept = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltaccounttype = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
                        //objDVOMasterOblCodes.dfltbaccounttype = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);
                        //objDVOMasterOblCodes.dfltbkeyvalue = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_pay_limit = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                        objDVOMasterOblCodes.dfltbaccounttype = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltbkeyvalue = (dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltbacct_desc = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);
                        listDVOMasterOblCodes.Add(objDVOMasterOblCodes);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterOblCodes;
            }
            return listDVOMasterOblCodes;
        }

        public static List<DVOMasterOblCodes> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOMasterOblCodes> listDVOMasterOblCodes = new List<DVOMasterOblCodes>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterOblCodes)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOMasterOblCodes objDVOMasterOblCodes = new DVOMasterOblCodes();
                        objDVOMasterOblCodes.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOMasterOblCodes.obl_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.description = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.obl_type = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                        objDVOMasterOblCodes.dflt_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                        objDVOMasterOblCodes.dflt_acct = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        objDVOMasterOblCodes.dflt_dept = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_bacct = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                        objDVOMasterOblCodes.dflt_bdept = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltaccounttype = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltbaccounttype = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dfltbkeyvalue = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOMasterOblCodes.dflt_pay_limit = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);

                        listDVOMasterOblCodes.Add(objDVOMasterOblCodes);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOMasterOblCodes;
            }
            return listDVOMasterOblCodes;
        }

        /// <summary>
        /// To Insert New Record
        /// </summary>
        /// <param name="objDVOMasterOblCodes">DVO object with all information to insert</param>
        /// <returns>object</returns>
        public static object InsertData(ref object objTransaction, ref DVOMasterOblCodes objDVOMasterOblCodes)
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
                object[] parameters = new object[16];
                parameters[0] = objDVOMasterOblCodes.obl_code;
                parameters[1] = objDVOMasterOblCodes.description;
                parameters[2] = objDVOMasterOblCodes.obl_type;
                parameters[3] = objDVOMasterOblCodes.dflt_rate;
                parameters[4] = objDVOMasterOblCodes.dflt_limit;
                parameters[5] = objDVOMasterOblCodes.dflt_acct;
                parameters[6] = objDVOMasterOblCodes.dflt_dept;
                parameters[7] = objDVOMasterOblCodes.dflt_bacct;
                parameters[8] = objDVOMasterOblCodes.dflt_bdept;
                parameters[9] = objDVOMasterOblCodes.dfltaccounttype;
                parameters[10] = objDVOMasterOblCodes.dfltbaccounttype;
                parameters[11] = objDVOMasterOblCodes.dfltbkeyvalue;
                parameters[12] = objDVOMasterOblCodes.dflt_pay_limit;
                parameters[13] = objDVOMasterOblCodes.InsertMachineInfo;
                if (objDVOMasterOblCodes.InsertDate == string.Empty) objDVOMasterOblCodes.InsertDate = null;
                parameters[14] = objDVOMasterOblCodes.InsertDate;
                parameters[15] = objDVOMasterOblCodes.InsertBy;

                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterOblCodes), true);

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return obj;
                }
                else
                    throw ex;
            }
            return obj;
        }

        /// <summary>
        /// To Update Record
        /// </summary>
        /// <param name="objDVOMasterOblCodes">DVO object with all information to update</param>
        /// <returns>object</returns>
        public static object UpdateData(ref object objTransaction, ref DVOMasterOblCodes objDVOMasterOblCodes)
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
                object[] parameters = new object[17];
                parameters[0] = objDVOMasterOblCodes.RowID;
                parameters[1] = objDVOMasterOblCodes.obl_code;
                parameters[2] = objDVOMasterOblCodes.description;
                parameters[3] = objDVOMasterOblCodes.obl_type;
                parameters[4] = objDVOMasterOblCodes.dflt_rate;
                parameters[5] = objDVOMasterOblCodes.dflt_limit;
                parameters[6] = objDVOMasterOblCodes.dflt_acct;
                parameters[7] = objDVOMasterOblCodes.dflt_dept;
                parameters[8] = objDVOMasterOblCodes.dflt_bacct;
                parameters[9] = objDVOMasterOblCodes.dflt_bdept;
                parameters[10] = objDVOMasterOblCodes.dfltaccounttype;
                parameters[11] = objDVOMasterOblCodes.dfltbaccounttype;
                parameters[12] = objDVOMasterOblCodes.dfltbkeyvalue;
                parameters[13] = objDVOMasterOblCodes.dflt_pay_limit;
                parameters[14] = objDVOMasterOblCodes.UpdateMachineInfo;
                if (objDVOMasterOblCodes.UpdateDate == string.Empty) objDVOMasterOblCodes.UpdateDate = null;
                parameters[15] = objDVOMasterOblCodes.UpdateDate;
                parameters[16] = objDVOMasterOblCodes.UpdateBy;

                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterOblCodes), true);

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return obj;
                }
                else
                    throw ex;
            }
            return obj;
        }
        /// <summary>
        /// To Delete Record
        /// </summary>
        /// <param name="objDVOPRDeductionCodesMasterDedcodes">DVO object with all information to delete</param>
        /// <returns>object</returns>
        public static Object DeleteData(ref DVOMasterOblCodes objDVOMasterOblCodes)
        {
            object obj = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVOMasterOblCodes.RowID;
                parameters[1] = objDVOMasterOblCodes.obl_code;
                obj = objDALBaseClass.DeleteData(ref parameters, typeof(DVOMasterOblCodes), true);
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
        public static DataSet GetActivityCode(ref DVOMasterOblCodes oblObligationCodes)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = new DataSet();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = oblObligationCodes.obl_code;
                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterOblCodes), oblObligationCodes.GetActivityCode);
                return ds;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return ds;
        }
    }
}
