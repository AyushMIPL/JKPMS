using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
{
///<Development and modification Details>
/// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
///1.) Make Functions for Lock Monitoring                     Rahul Jain                                   18/02/2009(DD)
///2.) 
///<summery>
    public class BLLLockMonitoring
    {
        public static List<DVOLockTableStatus> GetLockedRecords(ref DVOLockTableStatus objlockrecords)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOLockTableStatus> listDVOLockTableStatus = new List<DVOLockTableStatus>();
            try
            {
                object[] parameters = null;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOLockTableStatus)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOLockTableStatus objDVOLockTableStatus = new DVOLockTableStatus();
                        objDVOLockTableStatus.RowId = (dr["v_rowid"] != DBNull.Value) ? Convert.ToInt32(dr["v_rowid"]) : 0;//"v_rowid"
                        objDVOLockTableStatus.LockedRowId = (dr["v_lockedrowid"] != DBNull.Value) ? Convert.ToInt32(dr["v_lockedrowid"]) : 0;//"v_lockedrowid"
                        objDVOLockTableStatus.TableName = (dr["v_tablename"] != DBNull.Value) ? dr["v_tablename"].ToString().Trim() : string.Empty;//"v_tablename"
                        objDVOLockTableStatus.LockByUserLoginId = (dr["v_loginid"] != DBNull.Value) ? dr["v_loginid"].ToString().Trim() : string.Empty;//"v_lockbyuserid"
                        objDVOLockTableStatus.LockDatetime = (dr["v_lockdatetime"] != DBNull.Value) ? dr["v_lockdatetime"].ToString().Trim() : string.Empty;//"v_lockdatetime"
                        objDVOLockTableStatus.LockByMachInfo = (dr["v_machineinfo"] != DBNull.Value) ? dr["v_machineinfo"].ToString().Trim() : string.Empty;//"v_lockbymachinfo"
                        objDVOLockTableStatus.Release = (dr["v_release"] != DBNull.Value) ? dr["v_release"].ToString().Trim() : string.Empty;//"v_loginid"
                        listDVOLockTableStatus.Add(objDVOLockTableStatus);
                    }
                }
                return listDVOLockTableStatus;
            }
            catch (Exception ex)
            {
                //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                listDVOLockTableStatus = new List<DVOLockTableStatus>();
                return listDVOLockTableStatus;
            }
            return listDVOLockTableStatus;
        }

        public static int UpdateLockStatus(ref DVOLockTableStatus objLockUpd)
        {
            object[] Parameter = new object[1];
            Parameter[0] = objLockUpd.RowId;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.UpdateData(ref Parameter, typeof(DVOLockTableStatus),true);

            return Convert.ToInt32(c);

        }
        public static int CleanLockStatus()
        {
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object[] param=new object[0];
                object o= objDalBaseClass.ExecuteScalar(ref param, (new DVOLockTableStatus()).CLEAN_LOCK);
                if (o != DBNull.Value && o != null && o.ToString().Trim().Length > 0 && Convert.ToInt32(o) >= 0) { }
                else
                    throw new Exception("Error occured while cleaning locktablestatus table.");
                return Convert.ToInt32(o);
            }
            catch(Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }



    }
}
