using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
  public class BLLUpdKitDefD
    {
        /// <summary>
        /// Insert Details
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int InsertNewKitDefinitionD(ref DVOstokitrd objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[5];
                parameters[0] = objDVO.kit_code;
                parameters[1] = objDVO.line_no;
                parameters[2] = objDVO.item_code;
                parameters[3] = objDVO.ordr_qty;
                parameters[4] = objDVO.include_price;

                DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitrd));
                if (Ds.Tables.Count > 0)
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    }
                // Return when Contact Details Inserted Successfully
                if (success == 1)
                {
                    return 1;
                }
                // Return when matching ContactCode or Account number Found
                else if (success == 2)
                {
                    return 2;
                }
                // Return when Exception Occured During the insert Statement
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;
            }

        }

        /// <summary>
        /// Update Details
        /// </summary>
        /// <param name="TransactionObject"></param>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static object UpdateKitDefinitionD(ref object TransactionObject, ref DVOstokitrd objDVO)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            object success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                object[] parameters = new object[5];
                parameters[0] = objDVO.kit_code;
                parameters[1] = objDVO.line_no;
                parameters[2] = objDVO.item_code;
                parameters[3] = objDVO.ordr_qty;
                parameters[4] = objDVO.include_price;

                object[] RetValue = new object[1];
                RetValue[0] = objDalBaseClass.UpdateData(ref parameters, typeof(DVOstokitrd), true);

                // Return when Code updated Successfully
                if (RetValue[0] != null)
                {
                    string ret = RetValue[0].ToString();
                    if (Convert.ToInt32(RetValue[0].ToString()) == 1)
                    {
                        return 1;
                    }
                    // Return when matching code found 
                    else if (Convert.ToInt32(RetValue[0].ToString()) == 2)
                    {
                        return 2;
                    }
                    // Return when Exception generated 
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;
        }
    }
}
