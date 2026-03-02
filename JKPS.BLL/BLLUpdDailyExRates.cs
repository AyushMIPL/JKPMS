using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    /// <summary>
    /// BLL for Form "Update Daily Exchange Rates"
    /// Created by : Shrishanshu on 250709
    /// DVO : DVOUpdDailyExRates
    /// </summary>
    public class BLLUpdDailyExRates
    {
        /// <summary>
        /// Function To get the To_Currency_Code from Table stxdcrtr 
        /// </summary>
        /// <returns></returns>
        public static string getToCurrCode()
        {
            string strTocurrCode = string.Empty;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData("select home_currency from stmcntrc");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strTocurrCode = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            return strTocurrCode;
        }
       
        /// <summary>
        /// Insert New Record
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int InsertRecord(ref DVOUpdDailyExRates objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();

            try
            {
                object[] parameters = new object[5];
                parameters[0] = objDVO.from_currency_code;
                parameters[1] = objDVO.to_currency_code;
                parameters[2] = objDVO.rate_type;
                parameters[3] = objDVO.currency_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                parameters[4] = objDVO.rate;

                object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdDailyExRates), true);
                success = Convert.ToInt32(obj);
                if (success > 0)
                {
                  objDALBaseClassHelper.CommitTransaction(ref objTransection);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                }


            }
            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return success;
        }

        /// <summary>
        /// Update Record
        /// </summary>
        /// <param name="TransactionObject"></param>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static object UpdateRecord(ref DVOUpdDailyExRates objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objDVO.rowid;
                parameters[1] = objDVO.from_currency_code;
                parameters[2] = objDVO.to_currency_code;
                parameters[3] = objDVO.rate_type;
                parameters[4] = objDVO.currency_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                parameters[5] = objDVO.rate;

                object obj = new object();
                obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdDailyExRates), true);
                success = Convert.ToInt32(obj);

                if (success == 1)
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                }


            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;

            }
            return success;
        }

        /// <summary>
        /// Gets Search Data
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static List<DVOUpdDailyExRates> GetData(ref DVOUpdDailyExRates objDVO)
        {
            List<DVOUpdDailyExRates> lstDVO = new List<DVOUpdDailyExRates>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            object[] parameters = new object[5];
            parameters[0] = objDVO.from_currency_code;
            parameters[1] = objDVO.to_currency_code;
            parameters[2] = objDVO.rate_type;
            parameters[3] = objDVO.currency_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[4] = objDVO.rate;

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdDailyExRates)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdDailyExRates obj = new DVOUpdDailyExRates();
                    obj.rowid = Convert.ToInt32(dr[0]);
                    obj.from_currency_code = dr[1].ToString().Trim();
                    obj.to_currency_code = dr[2].ToString().Trim();
                    obj.rate_type = dr[3].ToString().Trim();
                    obj.currency_date = Convert.ToDateTime(dr[4].ToString());
                    obj.rate = Convert.ToDecimal(dr[5].ToString());
                    lstDVO.Add(obj);

                }
                return lstDVO;

            }

        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int DeleteRecord(ref DVOUpdDailyExRates objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            int i = 0;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVO.rowid;

                object obj = new object();
                obj = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdDailyExRates), true);
                success = Convert.ToInt32(obj);
                if (success == 1)
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                }

            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;

            }
            return success;

        }
    }
}
