using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;

namespace JKPS.BLL
{
    public class BLLRequistionDefaults
    {
        public static List<DVORequistionDefaults> GetRequistionDefaults(ref DVORequistionDefaults objDVORequistionDefaults)
        {
            object[] parameters = new object[0];
            List<DVORequistionDefaults> listDVORequistionDefaults = new List<DVORequistionDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVORequistionDefaults)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVORequistionDefaults objUpdReqDefaults = new DVORequistionDefaults();
                    objUpdReqDefaults.rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                    objUpdReqDefaults.min_days_req = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdReqDefaults.comm_level = dr[2].ToString().Trim();//comm_level
                    objUpdReqDefaults.min_dept = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdReqDefaults.add_nonstk_itm = dr[4].ToString().Trim();//fica_code
                    listDVORequistionDefaults.Add(objUpdReqDefaults);
                }
            }
            return listDVORequistionDefaults;
        }
        public static List<DVORequistionDefaults> GetAllRequistionDefaults()
        {
            object[] parameters = new object[0];
            List<DVORequistionDefaults> listDVORequistionDefaults = new List<DVORequistionDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVORequistionDefaults)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVORequistionDefaults objUpdReqDefaults = new DVORequistionDefaults();
                    objUpdReqDefaults.rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                    objUpdReqDefaults.min_days_req = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdReqDefaults.comm_level = dr[2].ToString().Trim();//comm_level
                    objUpdReqDefaults.min_dept = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdReqDefaults.add_nonstk_itm = dr[4].ToString().Trim();//fica_code
                    listDVORequistionDefaults.Add(objUpdReqDefaults);
                }
            }
            return listDVORequistionDefaults;
        }

        //public static int UpdateRequistionDefaults(ref DVORequistionDefaults objReqDefaults)
        //{
        //    object[] parameters = new object[7];
        //    parameters[0] = objReqDefaults.rowid;
        //    parameters[1] = objReqDefaults.min_days_req;
        //    parameters[2] = objReqDefaults.comm_level;
        //    parameters[3] = objReqDefaults.min_dept;
        //    parameters[4] = objReqDefaults.add_nonstk_itm;
        //    parameters[5] = objReqDefaults.updateby;
        //    parameters[6] = objReqDefaults.updatemachineinfo;

        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVORequistionDefaults));
        //    return c;
        //}

        public static int InsertRequistionDefaultInfo(ref DVORequistionDefaults objReqDefaultsIns)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objReqDefaultsIns.min_days_req;
                parameters[1] = objReqDefaultsIns.comm_level;
                parameters[2] = objReqDefaultsIns.min_dept;
                parameters[3] = objReqDefaultsIns.add_nonstk_itm;
                parameters[4] = objReqDefaultsIns.insertby;
                parameters[5] = objReqDefaultsIns.insertmachineinfo;
                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVORequistionDefaults), true);
                if (obj == null)
                    throw new Exception("Error occured to insert data in sturqcntrc.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured to insert data in sturqcntrc.");
                else
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    return 1;
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }
        public static int UpdateRequistionDefaults(ref object TransactionObject, ref DVORequistionDefaults objReqDefaults)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[7];
                parameters[0] = objReqDefaults.rowid;
                parameters[1] = objReqDefaults.min_days_req;
                parameters[2] = objReqDefaults.comm_level;
                parameters[3] = objReqDefaults.min_dept;
                parameters[4] = objReqDefaults.add_nonstk_itm;
                parameters[5] = objReqDefaults.updateby;
                parameters[6] = objReqDefaults.updatemachineinfo;

                object obj = objDalBaseClass.UpdateData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVORequistionDefaults), true);
                if (obj == null)
                    throw new Exception("Error occured to Update data in tbissuet.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured to Update data in tbissuet.");

                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;

        }
    }
}
