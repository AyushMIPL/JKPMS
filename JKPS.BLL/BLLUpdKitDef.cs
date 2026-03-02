using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;
using ExceptionManagement;

namespace JKPS.BLL
{
    /// <summary>
    /// Update Kit Definition
    /// Created by : Shrishanshu on 210709
    /// </summary>
    public class BLLUpdKitDef
    {
        /// <summary>
        /// Insert Header with Transaction
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int InsertNewKitDefinitionH(ref DVOstokitre objDVO, ref List<DVOstokitrd> listobjDVOD)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            int i = 0;
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objDVO.kit_code;
                parameters[1] = objDVO.desc1;
                parameters[2] = objDVO.desc2;


                DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitre));
                if (Ds.Tables.Count > 0)
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());

                    }
                // Return when Contact Details Inserted Successfully
                if (success > 0)
                {
                    i = BLLUpdKitDef.InsertKitDetails(ref listobjDVOD, ref objTransection);
                    if (i > 0)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    }
                    else
                    {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                    }
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
            return success;
        }

        /// <summary>
        /// Update Header
        /// </summary>
        /// <param name="TransactionObject"></param>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static object UpdateKitDefinitionH(ref DVOstokitre objDVO, ref List<DVOstokitrd> listobjDVOD)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            int i = 0;
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objDVO.kit_code;
                parameters[1] = objDVO.desc1;
                parameters[2] = objDVO.desc2;

                object obj = new object();
                obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitre), true);
                success = Convert.ToInt32(obj);
                // Return when Code updated Successfully
                if (success == 1)
                {
                    i = BLLUpdKitDef.UpdateKitDefinitionD(ref listobjDVOD, ref objTransection);
                    if (i > 0)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    }
                    else
                    {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;

            }
            return i;
        }

        /// <summary>
        /// Gets Header Data
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static List<DVOstokitre> GetDataH(ref DVOstokitre objDVO)
        {
            List<DVOstokitre> lstDVO = new List<DVOstokitre>();

            object[] Parameter = new object[2];
            Parameter[0] = objDVO.kit_code;
            Parameter[1] = objDVO.desc1;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOstokitre)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOstokitre obj = new DVOstokitre();
                    obj.rowid = Convert.ToInt32(dr[0]);
                    obj.kit_code = dr[1].ToString().Trim();
                    obj.desc1 = dr[2].ToString().Trim();
                    obj.desc2 = dr[3].ToString().Trim();
                    lstDVO.Add(obj);

                }
                return lstDVO;

            }

        }

        /// <summary>
        /// Function to Add Details with Transaction
        /// </summary>
        /// <param name="listDVOD"></param>
        /// <param name="objTransection"></param>
        /// <returns></returns>
        public static int InsertKitDetails(ref List<DVOstokitrd> listDVOD, ref object objTransection)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                foreach (DVOstokitrd objDVOD in listDVOD)
                {
                    object[] parameters = new object[5];
                    parameters[0] = objDVOD.kit_code;
                    parameters[1] = objDVOD.line_no;
                    parameters[2] = objDVOD.item_code;
                    parameters[3] = objDVOD.ordr_qty;
                    parameters[4] = objDVOD.include_price;

                    obj = objDalBaseClass.InsertData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitrd), true);
                    parameters = null;
                }
                // Return when Details Added Successfully

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);

                return Convert.ToInt32(obj);

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                return Convert.ToInt32(obj);
            }
            return Convert.ToInt32(obj);
        }

        public static int UpdateKitDefinitionD(ref List<DVOstokitrd> listDVOD, ref object objTransection)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                foreach (DVOstokitrd objDVOD in listDVOD)
                {
                    object[] parameters = new object[4];
                    parameters[0] = objDVOD.rowid;
                    parameters[1] = objDVOD.item_code;
                    parameters[2] = objDVOD.ordr_qty;
                    parameters[3] = objDVOD.include_price;

                    obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitrd), true);
                    if (Convert.ToInt32(obj) <= 0)
                    {
                        break;
                    }
                    parameters = null;
                }
                // Return when Details Added Successfully

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);

                return Convert.ToInt32(obj);

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                return Convert.ToInt32(obj);
            }
            return Convert.ToInt32(obj);
        }

        public static List<DVOstokitre> GetData(ref DVOstokitre objDVOH)
        {
            Object[] parameters = new object[3];

            parameters[0] = objDVOH.kit_code;
            parameters[1] = objDVOH.desc1;
            parameters[2] = objDVOH.desc2;

            List<DVOstokitre> objDVOList = new List<DVOstokitre>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOstokitre)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOstokitre tempobjDVO = new DVOstokitre();

                    tempobjDVO.rowid = Convert.ToInt32(dr[0]);
                    tempobjDVO.kit_code = dr[1].ToString().Trim();
                    tempobjDVO.desc1 = dr[2].ToString().Trim();
                    tempobjDVO.desc2 = dr[3].ToString().Trim();

                    objDVOList.Add(tempobjDVO);
                }
            }
            return objDVOList;
        }

        public static DataSet GetDataDetails(ref DVOstokitrd objDVOD)
        {

            object[] parameters = new object[1];
            parameters[0] = objDVOD.kit_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOstokitrd));
            return ds;

        }

        public static int DeleteKitRecord(ref DVOstokitre objDVO)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            int i = 0;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVO.kit_code;

                object obj = new object();
                obj = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitre), true);
                success = Convert.ToInt32(obj);
                // Return when Code Deleted Successfully
                if (success == 1)
                {
                    i = BLLUpdKitDef.DeleteDetails(objDVO.kit_code, ref objTransection);
                    if (i > 0)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    }
                    else
                    {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;

            }
            return i;

        }

        public static int DeleteDetails(string strKitCode, ref object objTransection)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = strKitCode;
                obj = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOstokitrd), true);
                
                

                // Return when Details Added Successfully

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);

                return Convert.ToInt32(obj);

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                return Convert.ToInt32(obj);
            }
            return Convert.ToInt32(obj);

        }

    }
}
