using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    //Implemented By: Sunil Pahwa on 12/5/09 for Requestor Information
    public class BLLRequestorInfoSturqsor
    {




        public static List<DVORequestorInfoSturqsor> GetRequestorList(ref DVORequestorInfoSturqsor objDvoRequestorInfoGet)
        {
            object[] Parameter = new object[5];
            Parameter[0] = objDvoRequestorInfoGet.requestor_code;
            Parameter[1] = objDvoRequestorInfoGet.request_desc;
            Parameter[2] = objDvoRequestorInfoGet.approval_level;
            Parameter[3] = objDvoRequestorInfoGet.whse_shipto;
            Parameter[4] = objDvoRequestorInfoGet.rowid;

            List<DVORequestorInfoSturqsor> lstDVORequestorInfoSturqsor = new List<DVORequestorInfoSturqsor>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVORequestorInfoSturqsor)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVORequestorInfoSturqsor objRequestorInfoSturqsor = new DVORequestorInfoSturqsor();
                    if (!Convert.IsDBNull(dr[0])) objRequestorInfoSturqsor.rowid = Convert.ToInt32(dr[0]);
                    if (!Convert.IsDBNull(dr[1])) objRequestorInfoSturqsor.requestor_code = Convert.ToString(dr[1]);
                    if (!Convert.IsDBNull(dr[2])) objRequestorInfoSturqsor.request_desc = dr[2].ToString().Trim();
                    if (!Convert.IsDBNull(dr[3])) objRequestorInfoSturqsor.whse_shipto = Convert.ToString(dr[3]);
                    if (!Convert.IsDBNull(dr[4])) objRequestorInfoSturqsor.approval_level = Convert.ToInt32(dr[4]);
                    if (!Convert.IsDBNull(dr[5])) objRequestorInfoSturqsor.whsedesc = dr[5].ToString().Trim();

                    lstDVORequestorInfoSturqsor.Add(objRequestorInfoSturqsor);
                }



            }
            return lstDVORequestorInfoSturqsor;
        }

        public static int InsertRequestorInfo(DVORequestorInfoSturqsor objDvoRequestorInfoIns)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = null;
            objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                
                object[] parameter = new object[4];
                parameter[0] = objDvoRequestorInfoIns.requestor_code;
                parameter[1] = objDvoRequestorInfoIns.request_desc;
                parameter[2] = objDvoRequestorInfoIns.approval_level;
                parameter[3] = objDvoRequestorInfoIns.whse_shipto;
                // parameter[4] = objDvoRequestorInfoIns.segmenttype;
          
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
               object  obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction ,ref parameter, typeof(DVORequestorInfoSturqsor),true);
               parameter = null;   
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
                    throw new Exception();
                else
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch(Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;

        }


        public static List<DVORequestorInfoSturqsor> GetAllWhseShipInfo()
        {
            List<DVORequestorInfoSturqsor> objDVORequestorInfoSturqsorlst = new List<DVORequestorInfoSturqsor>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVORequestorInfoSturqsor)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVORequestorInfoSturqsor objDVORequestorInfo = new DVORequestorInfoSturqsor();
                    if (!Convert.IsDBNull(dr[0])) objDVORequestorInfo.whse_code = Convert.ToString(dr[0]);//whse_code
                    if (!Convert.IsDBNull(dr[1])) objDVORequestorInfo.whsedesc = dr[1].ToString().Trim();//description
                    if (!Convert.IsDBNull(dr[2])) objDVORequestorInfo.department = Convert.ToString(dr[2]);//department


                    objDVORequestorInfoSturqsorlst.Add(objDVORequestorInfo);
                }
                return objDVORequestorInfoSturqsorlst;
            }
        }


        public static int UpdateRequestorInfo(ref object objTransaction, DVORequestorInfoSturqsor objDvoRequestorInfoUpd)
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
             object success = null;
             object[] parameter = new object[4];

             parameter[0] = objDvoRequestorInfoUpd.request_desc;
             parameter[1] = objDvoRequestorInfoUpd.approval_level;
             parameter[2] = objDvoRequestorInfoUpd.whse_shipto;
             parameter[3] = objDvoRequestorInfoUpd.requestor_code;
       

             success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameter, typeof(DVORequestorInfoSturqsor), true);
             parameter = null;
             if (success == null)
                 throw new Exception();
             else if (Convert.ToInt16(success) < 1)
                 throw new Exception();

             if (!statusObjTransaction)
                 objDALBaseClassHelper.CommitTransaction(ref objTransaction);
             objDvoRequestorInfoUpd = null;
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

        public static int  DeleteRequestorInfo(ref object objTransaction, ref DVORequestorInfoSturqsor objDvoReqStorinfoDel)
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
                object success = null;
                object[] parameter = new object[1];
                parameter[0] = objDvoReqStorinfoDel.rowid;

                success = objDalBaseClass.DeleteData_ByTransaction(ref objTransaction ,ref parameter, typeof(DVORequestorInfoSturqsor), true);
                parameter = null;
                if (success == null)
                    throw new Exception();
                else if (Convert.ToInt16(success) < 1)
                    throw new Exception();
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
