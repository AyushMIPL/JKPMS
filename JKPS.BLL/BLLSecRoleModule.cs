using System;
using System.Collections.Generic;
using System.Text;

using ExceptionManagement;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLSecRoleModule
    {
        public static List<DVOSecRoleModule> GetAllRoleModules()
        {
            List<DVOSecRoleModule> objSecRoleModuleList = new List<DVOSecRoleModule>();
            try
            {
                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecRoleModule));
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOSecRoleModule objSecRoleModule = new DVOSecRoleModule();
                            objSecRoleModule.RoleModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_rolemoduleid"
                            objSecRoleModule.RoleId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_roleid"
                            objSecRoleModule.RoleName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_role"
                            objSecRoleModule.ModuleId = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"p_moduleid"
                            objSecRoleModule.ModuleName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"v_module"
                            objSecRoleModule.ModuleParentId = (dr[5] != DBNull.Value) ? Convert.ToInt32(dr[5]) : 0;//"v_parentid"
                            objSecRoleModule.AddPermission = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"v_add"
                            objSecRoleModule.UpdatePermission = (dr[7] != DBNull.Value) ? Convert.ToInt32(dr[7]) : 0;//"v_update"
                            objSecRoleModule.DeletePermission = (dr[8] != DBNull.Value) ? Convert.ToInt32(dr[8]) : 0;//"v_delete"
                            objSecRoleModule.FindPermission = (dr[9] != DBNull.Value) ? Convert.ToInt32(dr[9]) : 0;//"v_find"
                            objSecRoleModule.BrowsePermission = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;//"v_browse"
                            objSecRoleModule.NextPermission = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;//"v_next"
                            objSecRoleModule.PreviousPermission = (dr[12] != DBNull.Value) ? Convert.ToInt32(dr[12]) : 0;//"v_previous"
                            objSecRoleModule.TabPermission = (dr[13] != DBNull.Value) ? Convert.ToInt32(dr[13]) : 0;//"v_tab"
                            objSecRoleModule.OptionsPermission = (dr[14] != DBNull.Value) ? Convert.ToInt32(dr[14]) : 0;//"v_options"
                            objSecRoleModule.InitPermission = (dr[15] != DBNull.Value) ? Convert.ToInt32(dr[15]) : 0;//"v_init"
                            objSecRoleModule.Option = (dr[16] != DBNull.Value) ? dr[16].ToString().Trim() : string.Empty;//v_moduleoption
                            objSecRoleModule.FormName = (dr[17] != DBNull.Value) ? dr[17].ToString().Trim() : string.Empty;//v_moduleform
                            objSecRoleModule.ModuleTypeId = (dr[18] != DBNull.Value) ? Convert.ToInt32(dr[18]) : 0;//v_moduletypeid
                            objSecRoleModule.ModuleType = (dr[19] != DBNull.Value) ? dr[19].ToString().Trim() : string.Empty;//v_moduletype
                            objSecRoleModule.ModuleImage = (dr[20] != DBNull.Value) ? dr[20].ToString().Trim() : string.Empty;//v_moduleimage

                            objSecRoleModuleList.Add(objSecRoleModule);
                        }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objSecRoleModuleList;
        }

        public static List<DVOSecRoleModule> GetRoleModules(ref DVOSecRoleModule pobjSecRoleModule)
        {
            List<DVOSecRoleModule> objSecRoleModuleList = new List<DVOSecRoleModule>();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = pobjSecRoleModule.RoleModuleId;
                parameters[1] = pobjSecRoleModule.RoleId;
                parameters[2] = pobjSecRoleModule.ModuleId;

                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecRoleModule));
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOSecRoleModule objSecRoleModule = new DVOSecRoleModule();
                            objSecRoleModule.RoleModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_rolemoduleid"
                            objSecRoleModule.RoleId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_roleid"
                            objSecRoleModule.RoleName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_role"
                            objSecRoleModule.ModuleId = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"p_moduleid"
                            objSecRoleModule.ModuleName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"v_module"
                            objSecRoleModule.ModuleParentId = (dr[5] != DBNull.Value) ? Convert.ToInt32(dr[5]) : 0;//"v_parentid"
                            objSecRoleModule.AddPermission = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"v_add"
                            objSecRoleModule.UpdatePermission = (dr[7] != DBNull.Value) ? Convert.ToInt32(dr[7]) : 0;//"v_update"
                            objSecRoleModule.DeletePermission = (dr[8] != DBNull.Value) ? Convert.ToInt32(dr[8]) : 0;//"v_delete"
                            objSecRoleModule.FindPermission = (dr[9] != DBNull.Value) ? Convert.ToInt32(dr[9]) : 0;//"v_find"
                            objSecRoleModule.BrowsePermission = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;//"v_browse"
                            objSecRoleModule.NextPermission = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;//"v_next"
                            objSecRoleModule.PreviousPermission = (dr[12] != DBNull.Value) ? Convert.ToInt32(dr[12]) : 0;//"v_previous"
                            objSecRoleModule.TabPermission = (dr[13] != DBNull.Value) ? Convert.ToInt32(dr[13]) : 0;//"v_tab"
                            objSecRoleModule.OptionsPermission = (dr[14] != DBNull.Value) ? Convert.ToInt32(dr[14]) : 0;//"v_options"
                            objSecRoleModule.InitPermission = (dr[15] != DBNull.Value) ? Convert.ToInt32(dr[15]) : 0;//"v_init"
                            objSecRoleModule.Option = (dr[16] != DBNull.Value) ? dr[16].ToString().Trim() : string.Empty;//v_moduleoption
                            objSecRoleModule.FormName = (dr[17] != DBNull.Value) ? dr[17].ToString().Trim() : string.Empty;//v_moduleform
                            objSecRoleModule.ModuleTypeId = (dr[18] != DBNull.Value) ? Convert.ToInt32(dr[18]) : 0;//v_moduletypeid
                            objSecRoleModule.ModuleType = (dr[19] != DBNull.Value) ? dr[19].ToString().Trim() : string.Empty;//v_moduletype
                            objSecRoleModule.ModuleImage = (dr[20] != DBNull.Value) ? dr[20].ToString().Trim() : string.Empty;//v_moduleimage

                            objSecRoleModuleList.Add(objSecRoleModule);
                        }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objSecRoleModuleList;
        }
        #region Old Function 
       // public static int InsertModulesInRole(ref List<DVOSecRoleModule> objSecRoleModuleList)
        public static int InsertModulesInRole(ref List<DVOSecRoleModule> objNewSecRoleModuleList,
        ref List<DVOSecRoleModule> objModifiedSecRoleModuleList,
        ref List<DVOSecRoleModule> objDeletedSecRoleModuleList,
            ref List<DVOSecRoleModule> objSecRoleModuleList)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            if (objSecRoleModuleList.Count > 0)
            {

                int roleId = objSecRoleModuleList[0].RoleId;
                try
                {
                   
                    #region Old Code---------------------------------
                    //foreach (DVOSecRoleModule objSecRoleModule in objSecRoleModuleList)
                    //{
                    //    if (objSecRoleModule.RoleModuleId > 0)
                    //    {
                    //        DVOSecRoleModule tobjSecRoleModule = objSecRoleModule;
                    //        //if (objSecRoleModule == objSecRoleModuleList[objSecRoleModuleList.Count - 1])
                    //        //  UpdateModulesInRole_UsingTransaction(ref objTransaction, ref tobjSecRoleModule, 1);
                    //        // else
                    //        UpdateModulesInRole_UsingTransaction(ref objTransaction, ref tobjSecRoleModule, 0);
                    //    }
                    //    else
                    //    {
                    //        object[] parameters = new object[15];
                    //        parameters[0] = objSecRoleModule.RoleId;
                    //        parameters[1] = objSecRoleModule.ModuleId;
                    //        parameters[2] = objSecRoleModule.AddPermission;
                    //        parameters[3] = objSecRoleModule.UpdatePermission;
                    //        parameters[4] = objSecRoleModule.DeletePermission;
                    //        parameters[5] = objSecRoleModule.FindPermission;
                    //        parameters[6] = objSecRoleModule.BrowsePermission;
                    //        parameters[7] = objSecRoleModule.NextPermission;
                    //        parameters[8] = objSecRoleModule.PreviousPermission;
                    //        parameters[9] = objSecRoleModule.TabPermission;
                    //        parameters[10] = objSecRoleModule.OptionsPermission;
                    //        parameters[11] = objSecRoleModule.InitPermission;
                    //        parameters[12] = objSecRoleModule.InsertBy;
                    //        parameters[13] = objSecRoleModule.InsertMachineInfo;
                    //        //if (objSecRoleModule == objSecRoleModuleList[objSecRoleModuleList.Count - 1])
                    //        //    parameters[14] = 1;
                    //        //else
                    //        parameters[14] = 0;

                    //        objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecRoleModule), true);
                    //        //objDalBaseClass.InsertData(ref parameters, typeof(DVOSecRoleModule));

                    //    }
                    //}
                    #endregion
                  
                    if (objNewSecRoleModuleList.Count > 0)
                        foreach (DVOSecRoleModule obj in objNewSecRoleModuleList)
                            InsertModuleOfRole(ref objTransaction, obj);

                    if (objModifiedSecRoleModuleList.Count > 0)
                        foreach (DVOSecRoleModule obj in objModifiedSecRoleModuleList)
                            UpdateModuleOfRole(ref objTransaction, obj);

                    if (objDeletedSecRoleModuleList.Count > 0)
                        foreach (DVOSecRoleModule obj in objDeletedSecRoleModuleList)
                            DeleteModuleOfRole(ref objTransaction, obj);
                    //code end on 25-11-2009-----------
                    //Added by sarvjeet on 23/11/2009..
                    //UpdateUsersModule(ref objTransaction, roleId);
                    if (objTransaction != null)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return 1;
                }
                catch (Exception ex)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManager.Publish(ex);
                    objTransaction = null;
                    throw ex;
                }
            }
            return 0;
        }

        public static int InsertModuleOfRole(ref object objTransaction, DVOSecRoleModule objSecRoleModule)
        {
            object V = null;
            DALBaseClassHelperSecurity objDALBaseClassHelper = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                object[] parameters = new object[14];
                parameters[0] = objSecRoleModule.RoleId;
                parameters[1] = objSecRoleModule.ModuleId;
                parameters[2] = objSecRoleModule.AddPermission;
                parameters[3] = objSecRoleModule.UpdatePermission;
                parameters[4] = objSecRoleModule.DeletePermission;
                parameters[5] = objSecRoleModule.FindPermission;
                parameters[6] = objSecRoleModule.BrowsePermission;
                parameters[7] = objSecRoleModule.NextPermission;
                parameters[8] = objSecRoleModule.PreviousPermission;
                parameters[9] = objSecRoleModule.TabPermission;
                parameters[10] = objSecRoleModule.OptionsPermission;
                parameters[11] = objSecRoleModule.InitPermission;
                parameters[12] = DVOApplicationUserInfo.UserId;
                parameters[13] = DVOApplicationUserInfo.MachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecRoleModule.INSERT_ROLEMODULE);
                if (o == null || o == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }
       
        private static DataSet GetUserOFRole(int _Roleid)
        {
            List<DVOSecUsers> lstDVOSecUsers = new List<DVOSecUsers>();
            DataSet ds = null;
            try
            {
                object[] Parameter = new object[1];
                Parameter[0] = _Roleid;
                DVOSecUsers objDVOSecUsers = new DVOSecUsers();
                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                ds = objDalBaseClass.GetData(objDVOSecUsers.GET_USERS_BY_ROLEID(ref Parameter));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;

        }
        //---------------------------------
        public static int UpdateModuleOfRole(ref object objTransaction, DVOSecRoleModule objSecRoleModule)
        {
            object U = null;
            DALBaseClassHelperSecurity objDALBaseClassHelper = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                object[] parameters = new object[15];
                parameters[0] = objSecRoleModule.RoleModuleId;
                parameters[1] = objSecRoleModule.RoleId;
                parameters[2] = objSecRoleModule.ModuleId;
                parameters[3] = objSecRoleModule.AddPermission;
                parameters[4] = objSecRoleModule.UpdatePermission;
                parameters[5] = objSecRoleModule.DeletePermission;
                parameters[6] = objSecRoleModule.FindPermission;
                parameters[7] = objSecRoleModule.BrowsePermission;
                parameters[8] = objSecRoleModule.NextPermission;
                parameters[9] = objSecRoleModule.PreviousPermission;
                parameters[10] = objSecRoleModule.TabPermission;
                parameters[11] = objSecRoleModule.OptionsPermission;
                parameters[12] = objSecRoleModule.InitPermission;
                parameters[13] = objSecRoleModule.UpdateBy;
                parameters[14] = objSecRoleModule.UpdateMachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecRoleModule.UPDATE_ROLEMODULE);
                if (o == null || o == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

        
                parameters = null;
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static int DeleteModuleOfRole(ref object objTransaction, DVOSecRoleModule objSecRoleModule)
        {
            object D = null;
            DALBaseClassHelperSecurity objDALBaseClassHelper = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                object[] parameters = new object[5];
                parameters[0] = objSecRoleModule.RoleModuleId;
                parameters[1] = objSecRoleModule.RoleId;
                parameters[2] = objSecRoleModule.ModuleId;
                parameters[3] = DVOApplicationUserInfo.UserId;
                parameters[4] = DVOApplicationUserInfo.MachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecRoleModule.DELETE_ROLEMODULE);
                if (o == null || o == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }
        #endregion
 
        private static int UpdateModulesInRole_UsingTransaction(ref Object objTransaction, ref DVOSecRoleModule objSecRoleModule, int ToUpdateRoleOfUser)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                object[] parameters = new object[15];
                //parameters[0] = objSecRoleModule.RoleModuleId;
                parameters[0] = objSecRoleModule.RoleId;
                parameters[1] = objSecRoleModule.ModuleId;
                parameters[2] = objSecRoleModule.AddPermission;
                parameters[3] = objSecRoleModule.UpdatePermission;
                parameters[4] = objSecRoleModule.DeletePermission;
                parameters[5] = objSecRoleModule.FindPermission;
                parameters[6] = objSecRoleModule.BrowsePermission;
                parameters[7] = objSecRoleModule.NextPermission;
                parameters[8] = objSecRoleModule.PreviousPermission;
                parameters[9] = objSecRoleModule.TabPermission;
                parameters[10] = objSecRoleModule.OptionsPermission;
                parameters[11] = objSecRoleModule.InitPermission;
                parameters[12] = objSecRoleModule.UpdateBy;
                parameters[13] = objSecRoleModule.UpdateMachineInfo;
                parameters[14] = ToUpdateRoleOfUser;

                objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecRoleModule));
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static int UpdateModulesInRole(ref DVOSecRoleModule objSecRoleModule, int ToUpdateRoleOfUser)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();

            try
            {
                object[] parameters = new object[14];
                //parameters[0] = objSecRoleModule.RoleModuleId;
                parameters[0] = objSecRoleModule.RoleId;
                parameters[1] = objSecRoleModule.ModuleId;
                parameters[2] = objSecRoleModule.AddPermission;
                parameters[3] = objSecRoleModule.UpdatePermission;
                parameters[4] = objSecRoleModule.DeletePermission;
                parameters[5] = objSecRoleModule.FindPermission;
                parameters[6] = objSecRoleModule.BrowsePermission;
                parameters[7] = objSecRoleModule.NextPermission;
                parameters[8] = objSecRoleModule.PreviousPermission;
                parameters[9] = objSecRoleModule.TabPermission;
                parameters[10] = objSecRoleModule.OptionsPermission;
                parameters[11] = objSecRoleModule.InitPermission;
                parameters[12] = objSecRoleModule.UpdateBy;
                parameters[13] = objSecRoleModule.UpdateMachineInfo;
                //parameters[15] = ToUpdateRoleOfUser;

                objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecRoleModule));

                objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
                objTransaction = null;
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                objTransaction = null;
            }
            return 0;
        }

        public static List<int> GetUserofRole(ref DVOSecRoleModule objDVOSecRoleModule)
        {

            List<int> userIdList = new List<int>();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOSecRoleModule.RoleId;

                DALBaseClassHelper objDALBaseClassHelperSecurity = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecRoleModule), objDVOSecRoleModule.CHK_URS_ROLE))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        userIdList.Add(Convert.ToInt32(dr[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return userIdList;

        }

        //public static List<DVOSecRoleModule> ChkModuleInRole(ref DVOSecRoleModule objDVOSecRoleModule)
        //{
        //    //object _modulesInRole = null;
        //    List<DVOSecRoleModule> lstDVOSecRoleModule = new List<DVOSecRoleModule>();
        //    try
        //    {
        //        object[] parameters = new object[2];
        //        parameters[0] = objDVOSecRoleModule.RoleId;
        //        parameters[1] = objDVOSecRoleModule.ModuleId;

        //        DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
        //        DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
        //        using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecRoleModule), objDVOSecRoleModule.COUNT_USER_MODULE_IN_ROLE))
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                DVOSecRoleModule obj = new DVOSecRoleModule();
        //              //  obj.
                        
                        
        //                //lstDVOSecRoleModule.Add(Convert.ToInt32(dr[0]));
        //            }
        //        }
        //        //lstDVOSecRoleModule = objDalBaseClass.ExecuteScalar(ref parameters, objDVOSecRoleModule.COUNT_USER_MODULE_IN_ROLE);
        //        //if (_modulesInRole == null)
        //        //    throw new Exception();
        //        //return Convert.ToInt16(_modulesInRole);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);

        //    }
        //    //return Convert.ToInt16(_modulesInRole);
        //    return lstDVOSecRoleModule;
        //}

        public static void UpdateUsersModule(ref Object objTransaction, int roleid)
        {
            DALBaseClassHelper objDALBaseClassHelperSecurity = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                Object[] parameters = new object[3];
                parameters[0] = roleid;
                parameters[1] = DVOApplicationUserInfo.UserId;
                parameters[2] = DVOApplicationUserInfo.MachineInfo;
                Object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, (new DVOSecRoleModule()).UPD_USRMDL,true);
                if (o == null || Convert.ToInt32(o) != 1)
                   throw new Exception("Error has occurred while updating user's modules");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }
    }
}
