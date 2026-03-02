using System;
using System.Collections.Generic;
using System.Text;

using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    ///<Development and modification Details>
   
    public class BLLChangePwd
    {
        public static int UpdatePassword(ref DVOChangePwd objSecUser)
        {

            object[] parameters = new object[6];
            parameters[0] = objSecUser.UserId;
            parameters[1] = objSecUser.NEWPassword;
            parameters[2] = objSecUser.LoginId;
            parameters[3] = objSecUser.UpdateBy;
            parameters[4] = objSecUser.UpdateMachineInfo;
            parameters[5] = objSecUser.LastPwdChnageDate;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOChangePwd));
            return c;
        }
    }
}
