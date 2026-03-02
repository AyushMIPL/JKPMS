using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;

namespace JKPS.BLL
{
    public class BLLFlxsegView
    {

        public static List<DVOFlxview> GetAllSegViewList()
        {
            List<DVOFlxview> objFlxseglst = new List<DVOFlxview>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOFlxview)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlxview objflxview = new DVOFlxview();
                   if(!Convert.IsDBNull(dr[0]))objflxview.rowid = Convert.ToInt32(dr[0]);//"p_rowid"
                    if (!Convert.IsDBNull(dr[1])) objflxview.src_key = dr[1].ToString().Trim();//"p_key"

                  if(!Convert.IsDBNull (dr[2]))   objflxview.src_desc = dr[2].ToString().Trim();//"v_desc"

                    objFlxseglst.Add(objflxview);
                }
                return objFlxseglst;
            }

        }

        public static List<DVOFlxview> GetSegViewList(ref DVOFlxview objFlxView)
        {
            try
            {
                object[] parameters = new object[7];
                parameters[0] = objFlxView.rowid;
                parameters[1] = objFlxView.src_type;
                parameters[2] = objFlxView.src_key;
                parameters[3] = objFlxView.src_desc;
                parameters[4] = objFlxView.src_num_desc;
                parameters[5] = objFlxView.src_char_desc;
                parameters[6] = objFlxView.src_acct_no;

                List<DVOFlxview> objFlxseglst = new List<DVOFlxview>();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                using (DataSet ds = objDalBaseClass.GetData(ref parameters,typeof(DVOFlxview)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOFlxview objflxview = new DVOFlxview();
                        if (!Convert.IsDBNull(dr[0])) objflxview.rowid = Convert.ToInt32(dr[0]);//p_rowid
                        if (!Convert.IsDBNull(dr[1])) objflxview.src_type = dr[1].ToString().Trim();//p_src_type
                        if (!Convert.IsDBNull(dr[2])) objflxview.src_key = dr[2].ToString().Trim();//p_src_key
                        if (!Convert.IsDBNull(dr[3])) objflxview.src_desc = dr[3].ToString().Trim();//p_src_desc
                        if (!Convert.IsDBNull(dr[4])) objflxview .src_num_desc  = Convert.ToInt32(dr[4]);//p_src_num_desc
                       if(!Convert.IsDBNull(dr[5]))objflxview.src_char_desc = dr[5].ToString().Trim();//p_src_char_desc
                       if(!Convert.IsDBNull(dr[6]))objflxview.src_acct_no = Convert.ToInt32(dr[6]);//p_src_acct_no

                        objFlxseglst.Add(objflxview);
                    }
                    return objFlxseglst;
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }
    }
}
