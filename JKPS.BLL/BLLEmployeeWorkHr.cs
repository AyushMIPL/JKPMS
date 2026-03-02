using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
{
    public class BLLEmployeeWorkHr
    {
        public static int InsertEmployeeWorkHr(ref DVOEmployeeWorkHr objEmployeeWorkHr)
        {

            object[] parameters = new object[7];
            parameters[0] = objEmployeeWorkHr.EmpWorkingHrId;
            parameters[1] = objEmployeeWorkHr.StartDate;
            parameters[2] = objEmployeeWorkHr.EndDate;
            //parameters[3] = objEmployeeWorkHr.InsertMachineInfo;
            //parameters[4] = objEmployeeWorkHr.InsertBy;
            //parameters[5] = objEmployeeWorkHr.UNIQUE_ID;


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOActivityLog));
            return c;
        }
        public static List<DVOEmployeeWorkHr> GetEmployeeWorkHr(ref DVOEmployeeWorkHr objEmployeeWorkHr)
        {
            object[] Parameter = new object[2];
            Parameter[0] = objEmployeeWorkHr.EmpWorkingHrId;
            List<DVOEmployeeWorkHr> lstEmployeeWorkHr = new List<DVOEmployeeWorkHr>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOEmployeeWorkHr)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmployeeWorkHr obj = new DVOEmployeeWorkHr();
                    obj.EmpWorkingHrId = Convert.ToInt32(dr[0]);//""
                    obj.StartDate = Convert.ToDateTime(dr[1].ToString().Trim());
                    obj.EndDate = Convert.ToDateTime(dr[2].ToString().Trim());
                    //obj.InsertMachineInfo = dr[4].ToString().Trim();
                    //obj.InsertDate = Convert.ToDateTime(dr[5]);

                    lstEmployeeWorkHr.Add(obj);
                }
                return lstEmployeeWorkHr;
            }
        }


        public static int DeleteEmployeeWorkHr(ref DVOEmployeeWorkHr objEmployeeWorkHr)
        {
            object[] Parameter = new object[1];
            Parameter[0] = objEmployeeWorkHr.EmpWorkingHrId;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOActivityLog));
            return c;
        }
    }
}
