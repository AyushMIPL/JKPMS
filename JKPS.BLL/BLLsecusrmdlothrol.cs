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
    public class BLLsecusrmdlothrol
    {

        public static int InsertModuleOFUserWhichNotInRole(ref Object objTransaction, ref DVOsecusrmdlothrol objDVOsecusrmdlothrol)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelper = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] Parameter = new object[15];
                Parameter[0] = objDVOsecusrmdlothrol.userid;
                Parameter[1] = objDVOsecusrmdlothrol.roleid;
                Parameter[2] = objDVOsecusrmdlothrol.moduleid;
                Parameter[3] = objDVOsecusrmdlothrol.addpermission;
                Parameter[4] = objDVOsecusrmdlothrol.updatepermission;
                Parameter[5] = objDVOsecusrmdlothrol.deletepermission;
                Parameter[6] = objDVOsecusrmdlothrol.findpermission;
                Parameter[7] = objDVOsecusrmdlothrol.browsepermission;
                Parameter[8] = objDVOsecusrmdlothrol.nextpermission;
                Parameter[9] = objDVOsecusrmdlothrol.previouspermission;
                Parameter[10] = objDVOsecusrmdlothrol.tabpermission;
                Parameter[11] = objDVOsecusrmdlothrol.optionspermission;
                Parameter[12] = objDVOsecusrmdlothrol.initpermission;
                Parameter[13] = objDVOsecusrmdlothrol.insertby;
                Parameter[14] = objDVOsecusrmdlothrol.insertmachineinfo;


               
                object C = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOsecusrmdlothrol), true);
                if (C == null)
                    throw new Exception();

                //if (!statusObjTransaction)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
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
