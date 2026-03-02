using System;
using System.Collections.Generic;
using System.Text;

using ExceptionManagement;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLSecUserModule
    {
        public static List<DVOSecUserModule> GetAllUserModules()
        {
            List<DVOSecUserModule> objSecUserModuleList = new List<DVOSecUserModule>();
            try
            {
                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecUserModule));
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOSecUserModule objSecUserModule = new DVOSecUserModule();
                            objSecUserModule.UserModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_usermoduleid"
                            objSecUserModule.UserId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_userid"
                            objSecUserModule.UserLoginId = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//v_loginid
                            objSecUserModule.UserFirstName = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//p_firstname
                            objSecUserModule.UserLastName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//p_lastname
                            objSecUserModule.UserEmpId = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//p_empid
                            objSecUserModule.RoleId = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"p_roleid"
                            objSecUserModule.ModuleId = (dr[8] != DBNull.Value) ? Convert.ToInt32(dr[8]) : 0;//"p_moduleid"
                            objSecUserModule.ModuleName = (dr[9] != DBNull.Value) ? dr[9].ToString().Trim() : string.Empty;//"v_module"
                            objSecUserModule.ModuleParentId = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;//"v_parentid"
                            objSecUserModule.AddPermission = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;//"v_add"
                            objSecUserModule.UpdatePermission = (dr[12] != DBNull.Value) ? Convert.ToInt32(dr[12]) : 0;//"v_update"
                            objSecUserModule.DeletePermission = (dr[13] != DBNull.Value) ? Convert.ToInt32(dr[13]) : 0;//"v_delete"
                            objSecUserModule.FindPermission = (dr[14] != DBNull.Value) ? Convert.ToInt32(dr[14]) : 0;//"v_find"
                            objSecUserModule.BrowsePermission = (dr[15] != DBNull.Value) ? Convert.ToInt32(dr[15]) : 0;//"v_browse"
                            objSecUserModule.NextPermission = (dr[16] != DBNull.Value) ? Convert.ToInt32(dr[16]) : 0;//"v_next"
                            objSecUserModule.PreviousPermission = (dr[17] != DBNull.Value) ? Convert.ToInt32(dr[17]) : 0;//"v_previous"
                            objSecUserModule.TabPermission = (dr[18] != DBNull.Value) ? Convert.ToInt32(dr[18]) : 0;//"v_tab"
                            objSecUserModule.OptionsPermission = (dr[19] != DBNull.Value) ? Convert.ToInt32(dr[19]) : 0;//"v_options"
                            objSecUserModule.InitPermission = (dr[20] != DBNull.Value) ? Convert.ToInt32(dr[20]) : 0;//"v_init"
                            objSecUserModule.Option = (dr[22] != DBNull.Value) ? dr[22].ToString().Trim() : string.Empty;//"v_moduleoption"
                            objSecUserModule.FormName = (dr[23] != DBNull.Value) ? dr[23].ToString().Trim() : string.Empty;//"v_moduleform"
                            objSecUserModule.ModuleTypeId = (dr[24] != DBNull.Value) ? Convert.ToInt32(dr[24]) : 0;//v_moduletypeid
                            objSecUserModule.ModuleType = (dr[25] != DBNull.Value) ? dr[25].ToString().Trim() : string.Empty;//v_moduletype
                            objSecUserModule.ModuleImage = (dr[26] != DBNull.Value) ? dr[26].ToString().Trim() : string.Empty;//v_moduleimage

                            objSecUserModuleList.Add(objSecUserModule);
                            //sort list by moduleid
                            objSecUserModuleList.Sort(delegate(DVOSecUserModule objSecUserModule1, DVOSecUserModule objSecUserModule2)
                            {
                                return Comparer<int>.Default.Compare(objSecUserModule1.ModuleId, objSecUserModule2.ModuleId);
                            });
                            ////////find list with moduleid=1
                            //////objSecUserModuleList.FindAll(delegate(DVOSecUserModule objSecUserModule1)
                            //////{
                            //////    return objSecUserModule1.ModuleId == 1;
                            //////});
                            ////////to show list
                            //////objSecUserModuleList.ForEach(delegate(DVOSecUserModule objSecUserModule1)
                            //////{
                            //////    Console.WriteLine(string.Format("{0},{1}", objSecUserModule1.ModuleId, objSecUserModule1.ModuleName));
                            //////});
                        }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objSecUserModuleList;
        }

        public static List<DVOSecUserModule> GetUserModules(ref DVOSecUserModule pobjSecUserModule)
        {
            List<DVOSecUserModule> objSecUserModuleList = new List<DVOSecUserModule>();
            try
            {
                object[] parameters = new object[8];
                parameters[0] = pobjSecUserModule.UserModuleId;
                parameters[1] = pobjSecUserModule.UserId;
                parameters[2] = pobjSecUserModule.RoleId;
                parameters[3] = pobjSecUserModule.ModuleId;
                parameters[4] = "";
                parameters[5] = "";
                parameters[6] = "";
                parameters[7] = "";

                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecUserModule));
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOSecUserModule objSecUserModule = new DVOSecUserModule();
                            objSecUserModule.UserModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_usermoduleid"
                            objSecUserModule.UserId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_userid"
                            objSecUserModule.UserLoginId = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//v_loginid
                            objSecUserModule.UserFirstName = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//p_firstname
                            objSecUserModule.UserLastName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//p_lastname
                            objSecUserModule.UserEmpId = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//p_empid
                            objSecUserModule.RoleId = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"p_roleid"
                            objSecUserModule.ModuleId = (dr[8] != DBNull.Value) ? Convert.ToInt32(dr[8]) : 0;//"p_moduleid"
                            objSecUserModule.ModuleName = (dr[9] != DBNull.Value) ? dr[9].ToString().Trim() : string.Empty;//"v_module"
                            objSecUserModule.ModuleParentId = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;//"v_parentid"
                            objSecUserModule.AddPermission = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;//"v_add"
                            objSecUserModule.UpdatePermission = (dr[12] != DBNull.Value) ? Convert.ToInt32(dr[12]) : 0;//"v_update"
                            objSecUserModule.DeletePermission = (dr[13] != DBNull.Value) ? Convert.ToInt32(dr[13]) : 0;//"v_delete"
                            objSecUserModule.FindPermission = (dr[14] != DBNull.Value) ? Convert.ToInt32(dr[14]) : 0;//"v_find"
                            objSecUserModule.BrowsePermission = (dr[15] != DBNull.Value) ? Convert.ToInt32(dr[15]) : 0;//"v_browse"
                            objSecUserModule.NextPermission = (dr[16] != DBNull.Value) ? Convert.ToInt32(dr[16]) : 0;//"v_next"
                            objSecUserModule.PreviousPermission = (dr[17] != DBNull.Value) ? Convert.ToInt32(dr[17]) : 0;//"v_previous"
                            objSecUserModule.TabPermission = (dr[18] != DBNull.Value) ? Convert.ToInt32(dr[18]) : 0;//"v_tab"
                            objSecUserModule.OptionsPermission = (dr[19] != DBNull.Value) ? Convert.ToInt32(dr[19]) : 0;//"v_options"
                            objSecUserModule.InitPermission = (dr[20] != DBNull.Value) ? Convert.ToInt32(dr[20]) : 0;//"v_init"
                            objSecUserModule.Option = (dr[22] != DBNull.Value) ? dr[22].ToString().Trim() : string.Empty;//"v_moduleoption"
                            objSecUserModule.FormName = (dr[23] != DBNull.Value) ? dr[23].ToString().Trim() : string.Empty;//"v_moduleform"
                            objSecUserModule.ModuleTypeId = (dr[24] != DBNull.Value) ? Convert.ToInt32(dr[24]) : 0;//v_moduletypeid
                            objSecUserModule.ModuleType = (dr[25] != DBNull.Value) ? dr[25].ToString().Trim() : string.Empty;//v_moduletype
                            objSecUserModule.ModuleImage = (dr[26] != DBNull.Value) ? dr[26].ToString().Trim() : string.Empty;//v_moduleimage

                            objSecUserModuleList.Add(objSecUserModule);
                        }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objSecUserModuleList;
        }

        public static int InsertModulesOfUser(ref List<DVOSecUserModule> objNewSecUserModuleList,
            ref List<DVOSecUserModule> objModifiedSecUserModuleList,
            ref List<DVOSecUserModule> objDeletedSecUserModuleList)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
            try
            {
                if (objNewSecUserModuleList.Count > 0)
                    foreach (DVOSecUserModule obj in objNewSecUserModuleList)
                        InsertModuleOfUser(ref objTransaction, obj);
                        

                if (objModifiedSecUserModuleList.Count > 0)
                    foreach (DVOSecUserModule obj in objModifiedSecUserModuleList)
                        UpdateModuleOfUser(ref objTransaction, obj);

                if (objDeletedSecUserModuleList.Count > 0)
                    foreach (DVOSecUserModule obj in objDeletedSecUserModuleList)
                        DeleteModuleOfUser(ref objTransaction, obj);

                if (objTransaction != null)
                    objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }

        public static int InsertModuleOfUser(ref object objTransaction, DVOSecUserModule objSecUserModule)
        {
             object V=null;
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
                parameters[0] = objSecUserModule.UserId;
                parameters[1] = objSecUserModule.RoleId;
                parameters[2] = objSecUserModule.ModuleId;
                parameters[3] = objSecUserModule.AddPermission;
                parameters[4] = objSecUserModule.UpdatePermission;
                parameters[5] = objSecUserModule.DeletePermission;
                parameters[6] = objSecUserModule.FindPermission;
                parameters[7] = objSecUserModule.BrowsePermission;
                parameters[8] = objSecUserModule.NextPermission;
                parameters[9] = objSecUserModule.PreviousPermission;
                parameters[10] = objSecUserModule.TabPermission;
                parameters[11] = objSecUserModule.OptionsPermission;
                parameters[12] = objSecUserModule.InitPermission;
                parameters[13] = DVOApplicationUserInfo.UserId;
                parameters[14] = DVOApplicationUserInfo.MachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecUserModule.INSERT_USERMODULE);
                if (o == null || o==DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

             //   int _Roleid = objSecUserModule.RoleId;
             //   int _moduleID = objSecUserModule.ModuleId;
             //   List<DVOSecRoleModule> listDVOSecRoleModul = new List<DVOSecRoleModule>();
             //   listDVOSecRoleModul=InsertModuleOFUser(ref objTransaction, _Roleid, _moduleID);
             //   if (listDVOSecRoleModul.Count > 0)
             //    V=objDALBaseClass.InsertData_ByTransaction(ref objTransaction,ref parameters, typeof(DVOsecusrmdlwsirol),true);
             //   else
             //    V=objDALBaseClass.InsertData_ByTransaction(ref objTransaction,ref parameters, typeof(DVOsecusrmdlothrol),true);
             //if (V == null)
             //    throw new Exception();
             //else if (Convert.ToInt32(V) < 1)
             //    throw new Exception();

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
        //Added by Sunil Pahwa
        private static List<DVOSecRoleModule> InsertModuleOFUser(ref object objTransaction, int _Roleid, int _moduleID)
        {
            List<DVOSecRoleModule> lstDVOSecRoleModule = new List<DVOSecRoleModule>();
            try
            {
                object[] Parameter = new object[2];
                Parameter[0] = _Roleid;
                Parameter[1] = _moduleID;
                DVOSecRoleModule objDVOSecRoleMod = new DVOSecRoleModule();
                DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOSecRoleModule), objDVOSecRoleMod.COUNT_USER_MODULE_IN_ROLE))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOSecRoleModule obj = new DVOSecRoleModule();
                        obj.RoleModuleId  = (dr[0]!=DBNull.Value)?Convert.ToInt32 (dr[0]):0;
                        obj.RoleId = (dr[1]!=DBNull.Value) ? Convert.ToInt32 (dr[1]):0;
                        obj.ModuleId = (dr[2] != DBNull.Value) ? Convert.ToInt32(dr[2]) : 0;
                        obj.AddPermission = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;
                        obj.UpdatePermission = (dr[4] != DBNull.Value) ? Convert.ToInt32(dr[4]) : 0;
                        obj.DeletePermission = (dr[5] != DBNull.Value) ? Convert.ToInt32(dr[5]) : 0;
                        obj.FindPermission = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;
                        obj.BrowsePermission = (dr[7] != DBNull.Value) ? Convert.ToInt32(dr[7]) : 0;
                        obj.NextPermission = (dr[8] != DBNull.Value) ? Convert.ToInt32(dr[8]) : 0;
                        obj.PreviousPermission = (dr[9] != DBNull.Value) ? Convert.ToInt32(dr[9]) : 0;
                        obj.TabPermission = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;
                        obj.OptionsPermission = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;
                        obj.InitPermission = (dr[12] != DBNull.Value) ? Convert.ToInt32(dr[12]) : 0;

                        lstDVOSecRoleModule.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstDVOSecRoleModule;

        }

        public static int UpdateModuleOfUser(ref object objTransaction, DVOSecUserModule objSecUserModule)
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
                object[] parameters = new object[16];
                parameters[0] = objSecUserModule.UserModuleId;
                parameters[1] = objSecUserModule.UserId;
                parameters[2] = objSecUserModule.RoleId;
                parameters[3] = objSecUserModule.ModuleId;
                parameters[4] = objSecUserModule.AddPermission;
                parameters[5] = objSecUserModule.UpdatePermission;
                parameters[6] = objSecUserModule.DeletePermission;
                parameters[7] = objSecUserModule.FindPermission;
                parameters[8] = objSecUserModule.BrowsePermission;
                parameters[9] = objSecUserModule.NextPermission;
                parameters[10] = objSecUserModule.PreviousPermission;
                parameters[11] = objSecUserModule.TabPermission;
                parameters[12] = objSecUserModule.OptionsPermission;
                parameters[13] = objSecUserModule.InitPermission;
                parameters[14] = DVOApplicationUserInfo.UserId;
                parameters[15] = DVOApplicationUserInfo.MachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecUserModule.UPDATE_USERMODULE);
                if (o == null || o == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

             //   //int _userID=objSecUserModule.UserId;
             //   int _Roleid = objSecUserModule.RoleId;
             //   int _Moduleid = objSecUserModule.ModuleId;
             //   List<DVOSecRoleModule> listDVOSecRoleModulUPD = new List<DVOSecRoleModule>();
             //   object[] UPDparameters = new object[15];
             //   UPDparameters[0] = objSecUserModule.UserId;
             //   UPDparameters[1] = objSecUserModule.RoleId;
             //   UPDparameters[2] = objSecUserModule.ModuleId;
             //   UPDparameters[3] = objSecUserModule.AddPermission;
             //   UPDparameters[4] = objSecUserModule.UpdatePermission;
             //   UPDparameters[5] = objSecUserModule.DeletePermission;
             //   UPDparameters[6] = objSecUserModule.FindPermission;
             //   UPDparameters[7] = objSecUserModule.BrowsePermission;
             //   UPDparameters[8] = objSecUserModule.NextPermission;
             //   UPDparameters[9] = objSecUserModule.PreviousPermission;
             //   UPDparameters[10] = objSecUserModule.TabPermission;
             //   UPDparameters[11] = objSecUserModule.OptionsPermission;
             //   UPDparameters[12] = objSecUserModule.InitPermission;
             //   UPDparameters[13] = DVOApplicationUserInfo.UserId;
             //   UPDparameters[14] = DVOApplicationUserInfo.MachineInfo;

             //   listDVOSecRoleModulUPD = InsertModuleOFUser(ref objTransaction, _Roleid, _Moduleid);

             //   if (listDVOSecRoleModulUPD.Count > 0)
             //       U = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UPDparameters, typeof(DVOsecusrmdlwsirol), true);
             //   else
             //       U = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UPDparameters, typeof(DVOsecusrmdlothrol), true);
             //if (U == null)
             //    throw new Exception();
             //else if (Convert.ToInt32(U) < 1)
             //    throw new Exception();

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

        public static int DeleteModuleOfUser(ref object objTransaction, DVOSecUserModule objSecUserModule)
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
                object[] parameters = new object[6];
                parameters[0] = objSecUserModule.UserModuleId;
                parameters[1] = objSecUserModule.UserId;
                parameters[2] = objSecUserModule.RoleId;
                parameters[3] = objSecUserModule.ModuleId;
                parameters[4] = DVOApplicationUserInfo.UserId;
                parameters[5] = DVOApplicationUserInfo.MachineInfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objSecUserModule.DELETE_USERMODULE);
                if (o == null || o == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();
             //   int _Roleid = objSecUserModule.RoleId;
             //   int _Moduleid = objSecUserModule.ModuleId;
             //   List<DVOSecRoleModule> listDVOSecRoleModulDEL = new List<DVOSecRoleModule>();
             //   listDVOSecRoleModulDEL = InsertModuleOFUser(ref objTransaction, _Roleid, _Moduleid);
             //   object[] DELparameters = new object[5];
             //   DELparameters[0] = objSecUserModule.UserId;
             //   DELparameters[1] = objSecUserModule.RoleId;
             //   DELparameters[2] = objSecUserModule.ModuleId;
             //   DELparameters[3] = DVOApplicationUserInfo.UserId;
             //   DELparameters[4] = DVOApplicationUserInfo.MachineInfo;

             //   if (listDVOSecRoleModulDEL.Count > 0)
             //    D= objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref DELparameters, typeof(DVOsecusrmdlwsirol),true);
             //   else
             //    D=objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref DELparameters, typeof(DVOsecusrmdlothrol),true);
             //if (D == null)
             //    throw new Exception();
             //else if (Convert.ToInt32(D) < 1)
             //    throw new Exception();

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



        //public static int InsertModulesOfUser(ref List<DVOSecUserModule> objSecUserModuleList)
        //{
        //    if (objSecUserModuleList.Count > 0)
        //    {
        //        DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
        //        DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
        //        object objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();

        //        try
        //        {
        //            foreach (DVOSecUserModule objSecUserModule in objSecUserModuleList)
        //            {
        //                if (objSecUserModule.UserModuleId > 0)
        //                {
        //                    DVOSecUserModule tobjSecUserModule = objSecUserModule;
        //                    UpdateModulesOfUser_UsingTransaction(ref objTransaction, ref tobjSecUserModule);
        //                    //Added by Sunil Pahwa
        //                    DVOSecRoleModule objDVOSecRoleModule = new DVOSecRoleModule();
        //                    objDVOSecRoleModule.RoleId = tobjSecUserModule.RoleId;
        //                    objDVOSecRoleModule.ModuleId = tobjSecUserModule.ModuleId;
        //                    //Below Function is Checking the Module for this Particular user in the SecRoleModule 
        //                    int _moduleInRole= BLLSecRoleModule.ChkModuleInRole(ref objDVOSecRoleModule);
        //                    if (_moduleInRole > 0)
        //                    {
        //                        //Insert in wasinrole table
        //                        DVOsecusrmdlwsirol objsecusrmdlwsirol = new DVOsecusrmdlwsirol();
        //                        objsecusrmdlwsirol.userid = objSecUserModule.UserId;
        //                        objsecusrmdlwsirol.roleid = objSecUserModule.RoleId;
        //                        objsecusrmdlwsirol.moduleid= objSecUserModule.ModuleId;
        //                        objsecusrmdlwsirol.addpermission= objSecUserModule.AddPermission;
        //                        objsecusrmdlwsirol.updatepermission=objSecUserModule.UpdatePermission;
        //                        objsecusrmdlwsirol.deletepermission=objSecUserModule.DeletePermission;
        //                        objsecusrmdlwsirol.findpermission=objSecUserModule.FindPermission;
        //                        objsecusrmdlwsirol.browsepermission=objSecUserModule.BrowsePermission;
        //                        objsecusrmdlwsirol.nextpermission=objSecUserModule.NextPermission;
        //                        objsecusrmdlwsirol.previouspermission=objSecUserModule.PreviousPermission;
        //                        objsecusrmdlwsirol.tabpermission=objSecUserModule.TabPermission;
        //                        objsecusrmdlwsirol.optionspermission=objSecUserModule.OptionsPermission;
        //                        objsecusrmdlwsirol.initpermission=objSecUserModule.InitPermission;
        //                        objsecusrmdlwsirol.insertby=objSecUserModule.InsertBy;
        //                        objsecusrmdlwsirol.insertmachineinfo=objSecUserModule.InsertMachineInfo;

        //                        int i=BLLsecusrmdlwsirol.InsertFromAddModuletoUser(ref objTransaction, ref objsecusrmdlwsirol);
                                


        //                    }
        //                    else
        //                    {
        //                        DVOsecusrmdlothrol objDVOsecusrmdlothrol = new DVOsecusrmdlothrol();
        //                        objDVOsecusrmdlothrol.userid = objSecUserModule.UserId; 
        //                        objDVOsecusrmdlothrol.roleid=objSecUserModule.RoleId;
        //                        objDVOsecusrmdlothrol.moduleid = objSecUserModule.ModuleId; 
        //                        objDVOsecusrmdlothrol.addpermission = objSecUserModule.AddPermission; ;
        //                        objDVOsecusrmdlothrol.updatepermission = objSecUserModule.UpdatePermission;                       
        //                        objDVOsecusrmdlothrol.deletepermission = objSecUserModule.DeletePermission;
        //                        objDVOsecusrmdlothrol.findpermission = objSecUserModule.FindPermission; ;
        //                        objDVOsecusrmdlothrol.browsepermission = objSecUserModule.BrowsePermission;
        //                        objDVOsecusrmdlothrol.nextpermission = objSecUserModule.NextPermission; 
        //                        objDVOsecusrmdlothrol.previouspermission = objSecUserModule.PreviousPermission; 
        //                        objDVOsecusrmdlothrol.tabpermission = objSecUserModule.TabPermission; ;
        //                        objDVOsecusrmdlothrol.optionspermission = objSecUserModule.OptionsPermission; 
        //                        objDVOsecusrmdlothrol.initpermission = objSecUserModule.InitPermission;
        //                        //objDVOsecusrmdlothrol.status=objSecUserModule.s;
        //                        objDVOsecusrmdlothrol.insertby = objSecUserModule.InsertBy;
        //                        objDVOsecusrmdlothrol.insertmachineinfo = objSecUserModule.InsertMachineInfo;
        //                        int R = BLLsecusrmdlothrol.InsertModuleOFUserWhichNotInRole(ref objTransaction, ref objDVOsecusrmdlothrol);
                               

        //                    }
        //                }
        //                else
        //                {
        //                    object[] parameters = new object[15];
        //                    parameters[0] = objSecUserModule.UserId;
        //                    parameters[1] = objSecUserModule.RoleId;
        //                    parameters[2] = objSecUserModule.ModuleId;
        //                    parameters[3] = objSecUserModule.AddPermission;
        //                    parameters[4] = objSecUserModule.UpdatePermission;
        //                    parameters[5] = objSecUserModule.DeletePermission;
        //                    parameters[6] = objSecUserModule.FindPermission;
        //                    parameters[7] = objSecUserModule.BrowsePermission;
        //                    parameters[8] = objSecUserModule.NextPermission;
        //                    parameters[9] = objSecUserModule.PreviousPermission;
        //                    parameters[10] = objSecUserModule.TabPermission;
        //                    parameters[11] = objSecUserModule.OptionsPermission;
        //                    parameters[12] = objSecUserModule.InitPermission;
        //                    parameters[13] = objSecUserModule.InsertBy;
        //                    parameters[14] = objSecUserModule.InsertMachineInfo;

        //                    objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecUserModule));

        //                    //Added by Sunil Pahwa
        //                    DVOSecRoleModule objDVOSecRoleModule = new DVOSecRoleModule();
        //                    objDVOSecRoleModule.RoleId = objSecUserModule.RoleId;
        //                    objDVOSecRoleModule.ModuleId = objSecUserModule.ModuleId;
        //                    //Below Function is Checking the Module for this Particular user in the SecRoleModule 
        //                    int _moduleInRole = BLLSecRoleModule.ChkModuleInRole(ref objDVOSecRoleModule);
        //                    if (_moduleInRole > 0)
        //                    {
        //                        //Insert in wasinrole table
        //                        DVOsecusrmdlwsirol objsecusrmdlwsirol = new DVOsecusrmdlwsirol();
        //                        objsecusrmdlwsirol.userid = objSecUserModule.UserId;
        //                        objsecusrmdlwsirol.roleid = objSecUserModule.RoleId;
        //                        objsecusrmdlwsirol.moduleid = objSecUserModule.ModuleId;
        //                        objsecusrmdlwsirol.addpermission = objSecUserModule.AddPermission;
        //                        objsecusrmdlwsirol.updatepermission = objSecUserModule.UpdatePermission;
        //                        objsecusrmdlwsirol.deletepermission = objSecUserModule.DeletePermission;
        //                        objsecusrmdlwsirol.findpermission = objSecUserModule.FindPermission;
        //                        objsecusrmdlwsirol.browsepermission = objSecUserModule.BrowsePermission;
        //                        objsecusrmdlwsirol.nextpermission = objSecUserModule.NextPermission;
        //                        objsecusrmdlwsirol.previouspermission = objSecUserModule.PreviousPermission;
        //                        objsecusrmdlwsirol.tabpermission = objSecUserModule.TabPermission;
        //                        objsecusrmdlwsirol.optionspermission = objSecUserModule.OptionsPermission;
        //                        objsecusrmdlwsirol.initpermission = objSecUserModule.InitPermission;
        //                        objsecusrmdlwsirol.insertby = objSecUserModule.InsertBy;
        //                        objsecusrmdlwsirol.insertmachineinfo = objSecUserModule.InsertMachineInfo;

        //                        int i = BLLsecusrmdlwsirol.InsertFromAddModuletoUser(ref objTransaction, ref objsecusrmdlwsirol);
        //                        //objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);


        //                    }
        //                    else
        //                    {
        //                        DVOsecusrmdlothrol objDVOsecusrmdlothrol = new DVOsecusrmdlothrol();
        //                        objDVOsecusrmdlothrol.userid = objSecUserModule.UserId;
        //                        objDVOsecusrmdlothrol.roleid = objSecUserModule.RoleId;
        //                        objDVOsecusrmdlothrol.moduleid = objSecUserModule.ModuleId;
        //                        objDVOsecusrmdlothrol.addpermission = objSecUserModule.AddPermission; ;
        //                        objDVOsecusrmdlothrol.updatepermission = objSecUserModule.UpdatePermission;
        //                        objDVOsecusrmdlothrol.deletepermission = objSecUserModule.DeletePermission;
        //                        objDVOsecusrmdlothrol.findpermission = objSecUserModule.FindPermission; ;
        //                        objDVOsecusrmdlothrol.browsepermission = objSecUserModule.BrowsePermission;
        //                        objDVOsecusrmdlothrol.nextpermission = objSecUserModule.NextPermission;
        //                        objDVOsecusrmdlothrol.previouspermission = objSecUserModule.PreviousPermission;
        //                        objDVOsecusrmdlothrol.tabpermission = objSecUserModule.TabPermission; ;
        //                        objDVOsecusrmdlothrol.optionspermission = objSecUserModule.OptionsPermission;
        //                        objDVOsecusrmdlothrol.initpermission = objSecUserModule.InitPermission;
        //                        //objDVOsecusrmdlothrol.status=objSecUserModule.s;
        //                        objDVOsecusrmdlothrol.insertby = objSecUserModule.InsertBy;
        //                        objDVOsecusrmdlothrol.insertmachineinfo = objSecUserModule.InsertMachineInfo;
        //                        int R = BLLsecusrmdlothrol.InsertModuleOFUserWhichNotInRole(ref objTransaction, ref objDVOsecusrmdlothrol);
                              

        //                    }




        //                }
        //            }
        //            objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
        //            objTransaction = null;
        //            return 1;
        //        }
        //        catch (Exception ex)
        //        {
        //            ExceptionManager.Publish(ex);
        //            objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
        //            objTransaction = null;
        //        }
        //    }
        //    return 0;
        //}

        private static int UpdateModulesOfUser_UsingTransaction(ref Object objTransaction, ref DVOSecUserModule objSecUserModule)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                object[] parameters = new object[15];
                //parameters[0] = objSecUserModule.UserModuleId;
                parameters[0] = objSecUserModule.UserId;
                parameters[1] = objSecUserModule.RoleId;
                parameters[2] = objSecUserModule.ModuleId;
                parameters[3] = objSecUserModule.AddPermission;
                parameters[4] = objSecUserModule.UpdatePermission;
                parameters[5] = objSecUserModule.DeletePermission;
                parameters[6] = objSecUserModule.FindPermission;
                parameters[7] = objSecUserModule.BrowsePermission;
                parameters[8] = objSecUserModule.NextPermission;
                parameters[9] = objSecUserModule.PreviousPermission;
                parameters[10] = objSecUserModule.TabPermission;
                parameters[11] = objSecUserModule.OptionsPermission;
                parameters[12] = objSecUserModule.InitPermission;
                parameters[13] = objSecUserModule.UpdateBy;
                parameters[14] = objSecUserModule.UpdateMachineInfo;

                objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecUserModule));
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

        public static int UpdateModulesOfUser(ref DVOSecUserModule objSecUserModule)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();

            try
            {
                object[] parameters = new object[16];
                //parameters[0] = objSecUserModule.UserModuleId;
                parameters[0] = objSecUserModule.UserId;
                parameters[1] = objSecUserModule.RoleId;
                parameters[2] = objSecUserModule.ModuleId;
                parameters[3] = objSecUserModule.AddPermission;
                parameters[4] = objSecUserModule.UpdatePermission;
                parameters[5] = objSecUserModule.DeletePermission;
                parameters[6] = objSecUserModule.FindPermission;
                parameters[7] = objSecUserModule.BrowsePermission;
                parameters[8] = objSecUserModule.NextPermission;
                parameters[9] = objSecUserModule.PreviousPermission;
                parameters[10] = objSecUserModule.TabPermission;
                parameters[11] = objSecUserModule.OptionsPermission;
                parameters[12] = objSecUserModule.InitPermission;
                parameters[13] = objSecUserModule.UpdateBy;
                parameters[14] = objSecUserModule.UpdateMachineInfo;

                objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecUserModule));

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
    }
}
