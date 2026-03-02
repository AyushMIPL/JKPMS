using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
///<Development and modification Details>

{
    public class BLLActivityLog
    {
        public static int InsertActivityLog(ref DVOActivityLog objActivity)
        {
           
            object[] parameters = new object[7];
            parameters[0] = objActivity.ActivityName;
            parameters[1] = objActivity.LoginId;
            parameters[2] = objActivity.FormName;
            parameters[3] = objActivity.Module;
            parameters[4] = objActivity.InsertMachineInfo;
            parameters[5] = objActivity.InsertBy;
            parameters[6] = objActivity.UNIQUE_ID;


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOActivityLog));
            return c;
        }
        public static List<DVOActivityLog> GetActivityLog(ref DVOActivityLog objActLog)
        {
            object[] Parameter = new object[2];
            Parameter[0] = objActLog.dateFrom;
            Parameter[1] = objActLog.dateto;
            List<DVOActivityLog> lstActLog = new List<DVOActivityLog>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOActivityLog)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOActivityLog obj = new DVOActivityLog();
                    obj.ActivityId = Convert.ToInt32(dr[0]);//""
                    obj.ActivityName = dr[1].ToString().Trim();//""
                    obj.LoginId = dr[2].ToString().Trim();//""
                    obj.FormName = dr[3].ToString().Trim();//""
                    obj.Module = dr[4].ToString().Trim();//""
                    obj.InsertDate =Convert.ToDateTime(dr[5]);//""
                    

                    lstActLog.Add(obj);

                }
                return lstActLog;
            }
        }
        public static int DeleteActivityLog(ref DVOActivityLog objActLogDel)
        {
            object[] Parameter = new object[1];
            Parameter[0] = objActLogDel.ActivityId;
            //Parameter[1] = objActLogDel.dateFrom;
            //Parameter[2] = objActLogDel.dateto;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOActivityLog));
            return c;

        }

    }
}
