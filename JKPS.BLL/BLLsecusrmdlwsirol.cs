using System;
using System.Collections.Generic;
using System.Text;

using ExceptionManagement;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
namespace JKPS.BLL
{
    //Added by Sunil Pahwa 
    public class BLLsecusrmdlwsirol
    {

        public static int InsertFromAddModuletoUser(ref object objTransaction,ref DVOsecusrmdlwsirol objsecusrmdlwsirol)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }

            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object [] Parameter = new object[15];
                Parameter[0] = objsecusrmdlwsirol.userid;
                Parameter[1] = objsecusrmdlwsirol.roleid;
                Parameter[2] = objsecusrmdlwsirol.moduleid;
                Parameter[3] = objsecusrmdlwsirol.addpermission;
                Parameter[4] = objsecusrmdlwsirol.updatepermission;
                Parameter[5] = objsecusrmdlwsirol.deletepermission;
                Parameter[6] = objsecusrmdlwsirol.findpermission;
                Parameter[7] = objsecusrmdlwsirol.browsepermission;
                Parameter[8] = objsecusrmdlwsirol.nextpermission;
                Parameter[9] = objsecusrmdlwsirol.previouspermission;
                Parameter[10] = objsecusrmdlwsirol.tabpermission;
                Parameter[11] = objsecusrmdlwsirol.optionspermission;
                Parameter[12] = objsecusrmdlwsirol.initpermission;
               // Parameter[13] = objsecusrmdlwsirol.active;
               // Parameter[14] = objsecusrmdlwsirol.status;
                Parameter[13] = objsecusrmdlwsirol.insertby;
                Parameter[14] = objsecusrmdlwsirol.insertmachineinfo;

                object C=objDalBaseClass.InsertData_ByTransaction(ref  objTransaction ,ref Parameter, typeof(DVOsecusrmdlwsirol),true);
                if (C == null)
                    throw new Exception();
                //if(!statusObjTransaction)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;


            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                objTransaction = null;
            }

            return 0;
        }
    }
}
