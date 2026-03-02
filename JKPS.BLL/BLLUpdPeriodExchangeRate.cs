using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    public class BLLUpdPeriodExchangeRate
    {
        /// <summary>
        /// Gets Search Data
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static List<DVOMCstxpcrtr> GetData(ref DVOMCstxpcrtr objDVO)
        {
            List<DVOMCstxpcrtr> lstDVO = new List<DVOMCstxpcrtr>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            object[] parameters = new object[6];
            parameters[0] = objDVO.from_currency_code;
            parameters[1] = objDVO.rate_type;
            parameters[2] = objDVO.period;
            parameters[3] = objDVO.period_year;
            parameters[4] = objDVO.rate;
            parameters[5] = objDVO.to_currency_code;

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMCstxpcrtr)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMCstxpcrtr obj = new DVOMCstxpcrtr();
                    obj.Rowid = Convert.ToInt32(dr[0]);
                    obj.from_currency_code = dr[1].ToString().Trim();
                    obj.to_currency_code = dr[2].ToString().Trim();
                    obj.rate_type = dr[3].ToString().Trim();
                    obj.period = dr[4].ToString().Trim();
                    obj.period_year = dr[5].ToString().Trim();
                    obj.rate = Convert.ToDecimal(dr[6]);
                    lstDVO.Add(obj);

                }
                return lstDVO;

            }

        }

        /// <summary>
        /// Insert New Record
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int InsertRecord(ref DVOMCstxpcrtr objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();

            try
            {
                object[] parameters = new object[6];
                parameters[0] = objDVO.from_currency_code;
                parameters[1] = objDVO.to_currency_code;
                parameters[2] = objDVO.rate_type;
                parameters[3] = objDVO.period;
                parameters[4] = objDVO.period_year;
                parameters[5] = objDVO.rate;

                object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransection, ref parameters, typeof(DVOMCstxpcrtr), true);
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
        public static object UpdateRecord(ref DVOMCstxpcrtr objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[7];
                parameters[0] = objDVO.Rowid;
                parameters[1] = objDVO.from_currency_code;
                parameters[2] = objDVO.to_currency_code;
                parameters[3] = objDVO.rate_type;
                parameters[4] = objDVO.period;
                parameters[5] = objDVO.period_year;
                parameters[6] = objDVO.rate;

                object obj = new object();
                obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOMCstxpcrtr), true);
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
        /// Delete Record
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int DeleteRecord(ref DVOMCstxpcrtr objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVO.Rowid;
                object obj = new object();
                obj = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOMCstxpcrtr), true);
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
