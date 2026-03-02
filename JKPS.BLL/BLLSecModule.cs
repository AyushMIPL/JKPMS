using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
namespace JKPS.BLL
{
        public delegate void ActionAgainstError(string Message);
        
    public class BLLSecModule
    {
        public static event ActionAgainstError ErrorOccurredinLayer;
        public static List<DVOSecModule> GetModules(ref DVOSecModule objSecModule)
        {
            object[] parameters = new object[6];
            parameters[0] = objSecModule.ModuleId;
            parameters[1] = objSecModule.Module;
            parameters[2] = objSecModule.Level;
            parameters[3] = objSecModule.ParentId;
            parameters[4] = objSecModule.ModuleTypeId;
            parameters[5] = objSecModule.Option;
            List<DVOSecModule> objSecModuleList = new List<DVOSecModule>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecModule)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOSecModule objModule = new DVOSecModule();
                        objModule.ModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_moduleid"
                        objModule.Module = (dr[1] != DBNull.Value) ? dr[1].ToString().Trim() : string.Empty;//"p_module"
                        objModule.Description = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_description"
                        objModule.Level = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"p_level"
                        objModule.ParentId = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"p_parentid"
                        objModule.FormName = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"v_formname"
                        objModule.ModuleTypeId = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"p_moduletypeid"
                        objModule.ModuleTypeName = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"v_moduletype"
                        objModule.Option = (dr[8] != DBNull.Value) ? dr[8].ToString().Trim() : string.Empty;//"p_option"
                        objModule.ImagePath = (dr[9] != DBNull.Value) ? dr[9].ToString().Trim() : string.Empty;//"v_imagepath"
                        objModule.ParentName = (dr[10] != DBNull.Value) ? dr[10].ToString().Trim() : string.Empty;//p_parentName
                        objSecModuleList.Add(objModule);
                        //objModule.ModuleId = Convert.ToInt32(dr["pmoduleid"]);

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return objSecModuleList;
        }

        public static List<DVOSecModule> GetAllModules()
        {
            List<DVOSecModule> objSecModuleList = new List<DVOSecModule>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecModule)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOSecModule objModule = new DVOSecModule();
                        objModule.ModuleId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_moduleid"
                        objModule.Module = (dr[1] != DBNull.Value) ? dr[1].ToString().Trim() : string.Empty;//"p_module"
                        objModule.Description = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_description"
                        objModule.Level = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"p_level"
                        objModule.ParentId = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"p_parentid"
                        objModule.FormName = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"v_formname"
                        objModule.ModuleTypeId = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;//"p_moduletypeid"
                        objModule.ModuleTypeName = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"v_moduletype"
                        objModule.Option = (dr[8] != DBNull.Value) ? dr[8].ToString().Trim() : string.Empty;//"p_option"
                        objModule.ImagePath = (dr[9] != DBNull.Value) ? dr[9].ToString().Trim() : string.Empty;//"v_imagepath"
                        objSecModuleList.Add(objModule);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return objSecModuleList;
        }
        public static void GetModuleTypes(out List<DVOSecModuleType> lstModuleTypes)
        {
            lstModuleTypes = new List<DVOSecModuleType>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object[] parameters = new object[2];
            parameters[0] = 0;
            parameters[1] = string.Empty;

            try
            {
                using (DataSet dsModuleList = objDALBaseClass.GetData(ref parameters, typeof(DVOSecModuleType)))
                {
                    foreach (DataRow dr in dsModuleList.Tables[0].Rows)
                    {
                        DVOSecModuleType objDVOSecModuleType = new DVOSecModuleType();
                        objDVOSecModuleType.ModuleTypeId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_moduletypeid"
                        objDVOSecModuleType.ModuleType = (dr[1] != DBNull.Value) ? dr[1].ToString().Trim() : string.Empty;//"p_moduletype"
                        objDVOSecModuleType.ImagePath = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_imagepath"
                        lstModuleTypes.Add(objDVOSecModuleType);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }
        public static int InsertModule(ref DVOSecModule objDVOSecModule)
        {
            int Success = 0;
            object[] parameters = new object[9];
            parameters[0] = objDVOSecModule.Module;
            parameters[1] = objDVOSecModule.Description;
            parameters[2] = objDVOSecModule.Level;

            parameters[3] = objDVOSecModule.ParentId;
            parameters[4] = objDVOSecModule.FormName;
            parameters[5] = objDVOSecModule.ModuleTypeId;
            parameters[6] = objDVOSecModule.Option;
            parameters[7] = objDVOSecModule.InsertBy;
            parameters[8] = objDVOSecModule.InsertMachineInfo;
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                Success = objDALBaseClass.InsertData(ref parameters, objDVOSecModule.GetType());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return Success;
        }
        public static int DeleteModule(ref DVOSecModule objDVOSecModule)
        {
            int Success = 0;
            object[] parameters = new object[3];
            parameters[0] = objDVOSecModule.ModuleId;
            parameters[1] = objDVOSecModule.UpdateBy;
            parameters[2] = objDVOSecModule.UpdateMachineInfo;
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                Success = objDALBaseClass.DeleteData(ref parameters, typeof(DVOSecModule));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return Success;
        }
        public static int UpdateModule(ref DVOSecModule objDVOSecModule)
        {
            int Success = 0;
            object[] parameters = new object[10];
            parameters[0] = objDVOSecModule.ModuleId;
            parameters[1] = objDVOSecModule.Module;
            parameters[2] = objDVOSecModule.Description;
            parameters[3] = objDVOSecModule.Level;
            parameters[4] = objDVOSecModule.ParentId;
            parameters[5] = objDVOSecModule.FormName;
            parameters[6] = objDVOSecModule.ModuleTypeId;
            parameters[7] = objDVOSecModule.Option;
            parameters[8] = objDVOSecModule.UpdateBy;
            parameters[9] = objDVOSecModule.UpdateMachineInfo;
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                Success = objDALBaseClass.UpdateData(ref parameters, objDVOSecModule.GetType());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return Success;
        }
    }
}
