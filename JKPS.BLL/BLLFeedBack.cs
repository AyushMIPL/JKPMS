using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;

namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.) BLL For FeedBack                             Rajeev(D)                                    25/11/2008(DD)
    ///2.) 
    ///<summery>
    public class BLLFeedBack
    {
        public static int InsertFeedBack(ref DVOFeedBack objFeedBack)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                int success = 0;
                object[] parameter = new object[12];
                parameter[0] = objFeedBack.P_Date;
                parameter[1] = objFeedBack.P_Desc;
                parameter[2] = objFeedBack.PRec_Situation;
                parameter[3] = objFeedBack.P_Module;
                parameter[4] = objFeedBack.P_Form;
                parameter[5] = objFeedBack.LoginID;
                parameter[6] = objFeedBack.Email;
                parameter[7] = objFeedBack.Phone;
                parameter[8] = objFeedBack.P_File;

                parameter[9] = objFeedBack.InsertMachineInfo;
                parameter[10] = objFeedBack.InsertDate;
                parameter[11] = objFeedBack.InsertBy;



                DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameter, typeof(DVOFeedBack));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        success = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
                parameter = null;
                objFeedBack = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransection);
                if (success > 0)
                    return success;
                else
                    return 0;
            }

            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            //return 0;
        }


        public static int UpdateFeedBack(ref DVOFeedBack objFeedBack)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objFeedBack.RowID;
                parameters[1] = objFeedBack.P_File;    


                object[] RetValue = new object[1];
                int i = objDalBaseClass.UpdateData(ref parameters, typeof(DVOFeedBack));
                if(i>-1)
                    return i;            
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;

        }
    }
}
