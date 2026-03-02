using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************* DevelopmentDate(Modified Date)
    ///1.) BLL For Get and Update Screen Information       Rahul Jain                                    15/07/2007(DD)
    ///2.) 
    ///<summery>
    public class BLLScreenInfo
    {
        public static int UpdateData(ref DVOtblscrninfo objDVOtblscrninfo)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[8];
                parameters[0] = objDVOtblscrninfo.screen_type;
                parameters[1] = objDVOtblscrninfo.screen_name;
                parameters[2] = objDVOtblscrninfo.primary_tblname;
                parameters[3] = objDVOtblscrninfo.record_key;
                parameters[4] = objDVOtblscrninfo.notes;

                parameters[5] = objDVOtblscrninfo.entered_by;
                parameters[6] = objDVOtblscrninfo.date_entered;
                parameters[7] = objDVOtblscrninfo.machine_info;

                object c = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOtblscrninfo), true);
                if (Convert.ToInt32(c) > 0)
                {
                    success = Convert.ToInt32(c);
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);
                }
            }
            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 1;

        }
        public static List<DVOtblscrninfo> GetDetails(ref DVOtblscrninfo objDVOtblscrninfo)
        {

            object[] parameters = new object[4];

            parameters[0] = objDVOtblscrninfo.screen_name;
            parameters[1] = objDVOtblscrninfo.primary_tblname;
            parameters[2] = objDVOtblscrninfo.record_key;
            parameters[3] = objDVOtblscrninfo.screen_type;

            List<DVOtblscrninfo> ListDVOtblscrninfo = new List<DVOtblscrninfo>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOtblscrninfo)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOtblscrninfo tempDVOtblscrninfo = new DVOtblscrninfo();

                    tempDVOtblscrninfo.screen_name = dr["screen_name"].ToString().Trim();
                    tempDVOtblscrninfo.primary_tblname = dr["primary_tblname"].ToString().Trim();
                    tempDVOtblscrninfo.record_key = dr["record_key"].ToString().Trim();
                    tempDVOtblscrninfo.notes = dr["notes"].ToString().Trim();

                    ListDVOtblscrninfo.Add(tempDVOtblscrninfo);
                }
            }
            return ListDVOtblscrninfo;
        }
    }
}
