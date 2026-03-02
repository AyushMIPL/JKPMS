using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLGLSetupWarrantEntry 
    {
        /// <summary>
        /// This method is use to get Warrant Entry type information from database 
        /// </summary>
        /// <param name="objSetupWarrantEntry">reference of DVOGLSetupWarrantEntry type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static List<DVOGLSetupWarrantEntry> GetSetupWarrantEntry(ref DVOGLSetupWarrantEntry objGLSetupWarrantEntryGet)
        {
            object[] parameters = new object[8];
            parameters[0] = objGLSetupWarrantEntryGet.type;
            parameters[1] = objGLSetupWarrantEntryGet.desc;
            parameters[2] = objGLSetupWarrantEntryGet.mustbalance;
            parameters[3] = objGLSetupWarrantEntryGet.budapprov;
            parameters[4] = objGLSetupWarrantEntryGet.fullkeyreqd;
            parameters[5] = objGLSetupWarrantEntryGet.startendreqd;
            parameters[6] = objGLSetupWarrantEntryGet.dollarsorpercnt;
            parameters[7] = objGLSetupWarrantEntryGet.rowid;


            List<DVOGLSetupWarrantEntry> objGLSetupWarrantEntryList = new List<DVOGLSetupWarrantEntry>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLSetupWarrantEntry)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOGLSetupWarrantEntry obj_SetupWarrantEntry = new DVOGLSetupWarrantEntry();
                    obj_SetupWarrantEntry.rowid = Convert.ToInt32(dr[0]);//rowid
                    obj_SetupWarrantEntry.type = dr[1].ToString().Trim();//type
                    obj_SetupWarrantEntry.desc = dr[2].ToString().Trim();//desc
                    obj_SetupWarrantEntry.mustbalance = dr[3].ToString().Trim();//mustbalance
                    obj_SetupWarrantEntry.budapprov = dr[4].ToString();//budapprov
                    obj_SetupWarrantEntry.fullkeyreqd = dr[5].ToString().Trim();//fullkeyreqd
                    obj_SetupWarrantEntry.startendreqd = dr[6].ToString().Trim();//startendreqd
                    obj_SetupWarrantEntry.dollarsorpercnt = dr[7].ToString().Trim();//dollarsorpercnt

                    objGLSetupWarrantEntryList.Add(obj_SetupWarrantEntry);
                }
            }
            return objGLSetupWarrantEntryList;
        }

        /// <summary>
        /// This method is use to insert new warrant type into database
        /// </summary>
        /// <param name="objGLSetupWarrantEntry">reference of DVOGLSetupWarrantEntry type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static object InsertWarrantEntry(ref object objTransaction, ref DVOGLSetupWarrantEntry objGLSetupWarrantEntryins)
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
                object[] parameters = new object[7];
                parameters[0] = objGLSetupWarrantEntryins.type;
                parameters[1] = objGLSetupWarrantEntryins.desc;
                parameters[2] = objGLSetupWarrantEntryins.mustbalance;
                parameters[3] = objGLSetupWarrantEntryins.budapprov;
                parameters[4] = objGLSetupWarrantEntryins.fullkeyreqd;
                parameters[5] = objGLSetupWarrantEntryins.startendreqd;
                parameters[6] = objGLSetupWarrantEntryins.dollarsorpercnt;
                              
                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLSetupWarrantEntry),true);
                objGLSetupWarrantEntryins = null;
                if (obj != null)
                {
                    if (obj.ToString() == "1")
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return obj;

                    }
                    else
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return null;
                    }
                }
                else
                {
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return null;
                }
            }
            catch (Exception ex)
            {                
                ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
            return obj;
        }
      
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLSetupWarrantEntry type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
        public static int DeleteWarrantEntry(ref object objTransaction, ref DVOGLSetupWarrantEntry DVOGLSetupWarrantEntryDEL)
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
                object[] parameters = new object[1];
                parameters[0] = DVOGLSetupWarrantEntryDEL.rowid;
                obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLSetupWarrantEntry), true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
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
        }
        public static int  UpdateWarrantEntry(ref object objTransaction, ref DVOGLSetupWarrantEntry DVOGLSetupWarrantEntryUPD)
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
                object[] parameters = new object[8];
                parameters[0] = DVOGLSetupWarrantEntryUPD.rowid;//rowid
                parameters[1] = DVOGLSetupWarrantEntryUPD.type;//type
                parameters[2] = DVOGLSetupWarrantEntryUPD.desc;// desc;
                parameters[3] = DVOGLSetupWarrantEntryUPD.mustbalance;// mustbalance;
                parameters[4] = DVOGLSetupWarrantEntryUPD.budapprov;// budapprov;
                parameters[5] = DVOGLSetupWarrantEntryUPD.fullkeyreqd;// fullkeyreqd;
                parameters[6] = DVOGLSetupWarrantEntryUPD.startendreqd;// startendreqd;
                parameters[7] = DVOGLSetupWarrantEntryUPD.dollarsorpercnt;// dollarsorpercnt;
         
                obj = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOGLSetupWarrantEntry),true);
                DVOGLSetupWarrantEntryUPD = null;
                parameters = null;
                objDALBaseClass = null;
                if (obj == null)
                    throw new Exception();
                    else if (Convert.ToInt16(obj) <1)
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

