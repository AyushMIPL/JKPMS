using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;

namespace JKPS.BLL
{
    public class BLLCompanyView
    {

        public static List<DVOCompanyView> GetCompanyView(ref DVOCompanyView  obj)
        {
            object[] parameter = new object[1];
            parameter[0] = obj.rowid;
          
            List<DVOCompanyView> objDvoCompList = new List<DVOCompanyView>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameter ,typeof(DVOCompanyView)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOCompanyView objCmpView = new DVOCompanyView();
                    objCmpView.src_key = dr["src_key"].ToString().Trim();
                    objCmpView.src_desc = dr["src_desc"].ToString().Trim();
                    objDvoCompList.Add(objCmpView);

                }
            }
            return objDvoCompList;

        }

    }
}
