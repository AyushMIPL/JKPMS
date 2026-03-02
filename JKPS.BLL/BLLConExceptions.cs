using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
///<Development and modification Details>

{
    public class BLLConExceptions
  {
        public static int InsertConExceptions(ref DVOConExceptions objConExceptions)
        {
           
            object[] parameters = new object[7];
            parameters[0] = objConExceptions.Type;
            parameters[1] = objConExceptions.Title;
            parameters[2] = objConExceptions.Description;
            parameters[3] = objConExceptions.IsActive;
            parameters[4] = objConExceptions.CreatedMachineInfo;
            parameters[5] = objConExceptions.CreatedBy;
            parameters[6] = objConExceptions.UNIQUE_ID;


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOConExceptions));
            return c;
        }
        public static List<DVOConExceptions> GetConExceptions(ref DVOConExceptions objConExceptions)
        {
            object[] Parameter = new object[2];
            Parameter[0] = objConExceptions.dateFrom;
            Parameter[1] = objConExceptions.dateto;
            List<DVOConExceptions> lstConExceptions = new List<DVOConExceptions>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOConExceptions)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOConExceptions obj = new DVOConExceptions();
                    obj.Id = Convert.ToInt32(dr[0]);//""
                    obj.Type = dr[1].ToString().Trim();//""
                    obj.Title = dr[2].ToString().Trim();//""
                    obj.Description = dr[3].ToString().Trim();//""
                    obj.IsActive = Convert.ToBoolean(dr[4].ToString().Trim());//""
                    obj.CreatedOn =Convert.ToDateTime(dr[5]);//""
                    

                    lstConExceptions.Add(obj);

                }
                return lstConExceptions;
            }
        }
        public static int DeleteConExceptions(ref DVOConExceptions objConExceptionsDel)
        {
            object[] Parameter = new object[1];
            Parameter[0] = objConExceptionsDel.Id;
            //Parameter[1] = objActLogDel.dateFrom;
            //Parameter[2] = objActLogDel.dateto;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOConExceptions));
            return c;

        }

    }
}
