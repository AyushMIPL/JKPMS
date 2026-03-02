using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLSecRoles
    {
        /// <summary>
        /// This method is use to get role information from database based on Role name
        /// </summary>
        /// <param name="objSecRole">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having role information</returns>
        public static List<DVOSecRole> GetRoles(ref DVOSecRole objSecRole)//int RoleId, string RoleName
        {
            object[] parameters = new object[3];
            parameters[0] = objSecRole.RoleId;
            parameters[1] = objSecRole.Role;
            parameters[2] = objSecRole.Description;
            //parameters[3] = objSecRole.Rowid;
            List<DVOSecRole> objSecRoleList = new List<DVOSecRole>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecRole)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecRole objRole = new DVOSecRole();
                    objRole.RoleId =(dr[0]!=DBNull.Value ?  Convert.ToInt32(dr[0]):0);//p_roleid
                    objRole.Role = (dr[1]!=DBNull.Value ? Convert.ToString(dr[1]):string.Empty);//p_role
                    objRole.Description = (dr[2]!=DBNull.Value ? Convert.ToString(dr[2]):string.Empty);//v_description
                    //objRole.Rowid =(dr[3]!=DBNull.Value ? Convert.ToInt32 ( dr[3]):0);//v_rowid
                    objSecRoleList.Add(objRole);
                }
            }
            return objSecRoleList;
        }
        /// <summary>
        /// This method is use to insert new role into database
        /// </summary>
        /// <param name="objSecRole">A class object passed as parameter having parameter data</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static int InsertRoles(ref DVOSecRole objDvoSecRoleIns)
        {
            object[] parameters = new object[4];
            parameters[0] = objDvoSecRoleIns.Role;
            parameters[1] = objDvoSecRoleIns.Description;
            parameters[2] = objDvoSecRoleIns.InsertBy;
            parameters[3] = objDvoSecRoleIns.InsertMachineInfo;

            List<DVOSecRole> objSecRoleList = new List<DVOSecRole>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object  c = objDalBaseClass.InsertData(ref parameters, typeof(DVOSecRole),true);
            return Convert.ToInt32(c);

        }
        
        /// <summary>
        /// This methosd is use to update role information into database
        /// </summary>
        /// <param name="objSecRole">A class object passed as parameter having parameter data</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        /// Commented by Sunil Pahwa******************on 30-01-09************************
        public static int UpdateRoles(ref DVOSecRole objSecRole)
        {
            object[] parameters = new object[5];
            parameters[0] = objSecRole.RoleId;
            parameters[1] = objSecRole.Role;// RoleName;
            parameters[2] = objSecRole.Description;// RoleDesc;
            parameters[3] = objSecRole.UpdateBy;// UpdateBy;
            parameters[4] = objSecRole.UpdateMachineInfo;// UpdateMachineInfo;
            List<DVOSecRole> objSecRoleList = new List<DVOSecRole>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOSecRole));
            return c;

        }
        //****************************************************************************
        /// <summary>
        /// This methosd is use to delete role information from database
        /// </summary>
        /// <param name="objSecRole">A class object passed as parameter having parameter data</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
        public static int DeleteRoles(ref object objTransaction, ref DVOSecRole objSecRole)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object i = null;
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objSecRole.RoleId;
                parameters[1] = objSecRole.UpdateBy;// UpdateBy;
                parameters[2] = objSecRole.UpdateMachineInfo;// UpdateMachineInfo;
                List<DVOSecRole> objSecRoleList = new List<DVOSecRole>();
                i = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecRole), true);
                if (i == null)
                    throw new Exception();
                else if (Convert.ToInt32(i) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;

            }
            catch(Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;


        }

        public static List<DVOSecRole> GetRoles()
        {
            List<DVOSecRole> objSecRoleList = new List<DVOSecRole>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecRole)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecRole objRole = new DVOSecRole();
                    objRole.RoleId = Convert.ToInt32(dr[0]);//"p_roleid"
                    objRole.Role = dr[1].ToString().Trim();//"p_role"
                    objRole.Description = dr[2].ToString().Trim();//"v_description"
                    objSecRoleList.Add(objRole);
                }
            }
            return objSecRoleList;
        }


        public static int  UpdateRolesInfo(ref object objTransaction, ref DVOSecRole objDvoSecUpd)
        {
          DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
          bool statusObjTransaction = true;
          if (objTransaction == null)
          {
              objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
              statusObjTransaction = false;
          }
          DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
          object success = null;

          try
          {
            object[] parameters = new object[5];
            parameters[0] = objDvoSecUpd.RoleId;
            parameters[1] = objDvoSecUpd.Role;// RoleName;
            parameters[2] = objDvoSecUpd.Description;// RoleDesc;
            parameters[3] = objDvoSecUpd.UpdateBy;// UpdateBy;
            parameters[4] = objDvoSecUpd.UpdateMachineInfo;// UpdateMachineInfo;

            success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOSecRole));
            if (success == null)
                throw new Exception();
            else if (Convert.ToInt32(success) < 1)
                throw new Exception();
            if (!statusObjTransaction)
                objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
            return 1;
        }
        catch (Exception ex)
        {
            if (!statusObjTransaction)
                objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
            ExceptionManagement.ExceptionManager.Publish(ex);
            throw ex;
        }
        return 0;

        
           

        }
    }
}
