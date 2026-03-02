using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
using JKPS.CommonUtilities;
namespace JKPS.BLL
{
    public class BLLMisc
    {
        /// <summary>
        /// Calling data layer's get All Data
        /// Passing no Parameters since we are accessing all the data
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOGeneralLedger Defined in the common Layer</returns>
        public static List<DVOApprovalLevel> GetAllApprovalLevels()
        {
            List<DVOApprovalLevel> lstDVOApprovalLevel = new List<DVOApprovalLevel>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOApprovalLevel)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOApprovalLevel objDVOApprovalLevel = new DVOApprovalLevel();
                        if(!Convert.IsDBNull(dr[0]))  objDVOApprovalLevel.acd_id = Convert.ToInt32(dr[0]);//"acd_id"
                        if (!Convert.IsDBNull(dr[1])) objDVOApprovalLevel.approval_level = Convert.ToInt32(dr[1]);//"approval_level"
                        if (!Convert.IsDBNull(dr[2])) objDVOApprovalLevel.amount = Convert.ToDecimal(dr[2]);//"amount"
                        if (!Convert.IsDBNull(dr[3])) objDVOApprovalLevel.description = Convert.ToString(dr[3]);//"description"
                        lstDVOApprovalLevel.Add(objDVOApprovalLevel);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOApprovalLevel;
        }
        /// <summary>
        /// Calling data layer's get All Data
        /// Passing no Parameters since we are accessing all the data
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOGeneralLedger Defined in the common Layer</returns>
        public static List<DVOingappcls> GetAllingappcls()
        {
            List<DVOingappcls> lstDVOingappcls = new List<DVOingappcls>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOingappcls)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOingappcls objDVOingappcls = new DVOingappcls();
                        if(!Convert.IsDBNull(dr[0]))objDVOingappcls.acd_id = Convert.ToInt32(dr[0]);//"acd_id"
                        if (!Convert.IsDBNull(dr[1])) objDVOingappcls.module = Convert.ToString(dr[1]);//"module"
                        if (!Convert.IsDBNull(dr[2])) objDVOingappcls.dept = Convert.ToString(dr[2]);//"dept"
                        if (!Convert.IsDBNull(dr[3])) objDVOingappcls.line_amount = Convert.ToDecimal(dr[3]);//"line_amount"
                        if (!Convert.IsDBNull(dr[4])) objDVOingappcls.total_amount = Convert.ToDecimal(dr[4]);//"total_amount"
                        if (!Convert.IsDBNull(dr[5])) objDVOingappcls.global_skip = Convert.ToString(dr[5]);//"global_skip"
                        lstDVOingappcls.Add(objDVOingappcls);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOingappcls;
        }
        public static List<DVOSecUsers> GetAllSysUsers()
        {
            List<DVOSecUsers> objSecUserList = new List<DVOSecUsers>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSystemUserInfo)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecUsers objSecUser = new DVOSecUsers();
                    if(!Convert.IsDBNull(dr[0]))objSecUser.UserId = Convert.ToInt32(dr[0]);//"userid"
                    if (!Convert.IsDBNull(dr[1])) objSecUser.FirstName = Convert.ToString(dr[1]).Trim();//"firstname"
                    if (!Convert.IsDBNull(dr[2])) objSecUser.LastName = Convert.ToString(dr[2]).Trim();//"lastname"
                    if (!Convert.IsDBNull(dr[3])) objSecUser.LoginId = Convert.ToString(dr[3]).Trim();//"loginid"
                    if (!Convert.IsDBNull(dr[4])) objSecUser.isSysUser = Convert.ToBoolean(dr[4]);//"IsSysUser"
                    objSecUserList.Add(objSecUser);
                }
            }

            return objSecUserList;
        }
        public static List<DVOSource> GetSources(string strsrc_type)
        {
            List<DVOSource> lstDVOSource = new List<DVOSource>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] ZoomParameters=new object[1];
            ZoomParameters[0] = strsrc_type;
            using (DataSet ds = objDalBaseClass.GetData(ref ZoomParameters, typeof(DVOSource))) 
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSource objDVOSource = new DVOSource();
                    if (!Convert.IsDBNull(dr[0])) objDVOSource.src_type = Convert.ToString(dr[0]);//"src_type"
                    if (!Convert.IsDBNull(dr[1])) objDVOSource.src_key = Convert.ToString(dr[1]);//"src_key"
                    if (!Convert.IsDBNull(dr[2])) objDVOSource.src_desc = Convert.ToString(dr[2]);//"src_desc"
                    if (!Convert.IsDBNull(dr[3])) objDVOSource.src_num_desc = Convert.ToDecimal(dr[3]);//"src_num_desc"
                    if (!Convert.IsDBNull(dr[4])) objDVOSource.src_char_desc = Convert.ToString(dr[4]);//"src_char_desc"
                    if (!Convert.IsDBNull(dr[5])) objDVOSource.src_acct_no = Convert.ToInt32(dr[5]);//"src_acct_no"
                    lstDVOSource.Add(objDVOSource);
                }
            }

            return lstDVOSource;
        }
        public static List<DVOAccountGroupsZ> GetAccountGroupsForZoom(string pgrp_key,string pgrp_desc)
        {
            List<DVOAccountGroupsZ> lstDVOAccountGroupsZ = new List<DVOAccountGroupsZ>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] ZoomParameters = new object[2];
            ZoomParameters[0] = pgrp_key;
            ZoomParameters[1] = pgrp_desc;

            using (DataSet ds = objDalBaseClass.GetData(ref ZoomParameters, typeof(DVOAccountGroupsZ)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOAccountGroupsZ objDVOAccountGroupsZ = new DVOAccountGroupsZ();
                    if (!Convert.IsDBNull(dr[0])) objDVOAccountGroupsZ.grp_key = Convert.ToString(dr[0]);//"grp_key"
                    if (!Convert.IsDBNull(dr[1])) objDVOAccountGroupsZ.grp_desc = Convert.ToString(dr[1]);//"grp_desc"
                    lstDVOAccountGroupsZ.Add(objDVOAccountGroupsZ);
                }
            }

            return lstDVOAccountGroupsZ;
        }
        public static object TypeCastIfDBNull(ref object obj, ref Type typ)
        {
            if (obj != DBNull.Value)
            {
                obj = Convert.ChangeType(obj, typ);
            }
            else
            {
                if (typ == typeof(int))
                {
                    obj = 0;
                }
                else if (typ == typeof(string))
                {
                    obj = string.Empty;
                }
                else if (typ == typeof(System.DateTime))
                {
                    obj = System.DateTime.MinValue;
                }

            }
            return obj;
        }
        public static bool ForceLock()
        {
            bool isLocked = false;
            string ptablename = "inaccdef";
            string pkeyFieldName = "user_id";
            int pKeyFieldValue = 1;
            object[] parameters = new object[3];
            parameters[0] = ptablename;
            parameters[1] = pkeyFieldName;
            parameters[2] = pKeyFieldValue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                if (Convert.ToInt32(objDalBaseClass.ForceLock(ref objTransaction, ref parameters, typeof(DVOUserApprovalInfoOverAll))) > 0)
                {
                    isLocked = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return isLocked;
        }
  
        //Added By rahul Jain on 22/01/2010
        public static List<DVOApprovalLevel> GetAllApprovalLevelByModule(ref DVOApprovalLevel objDVOApprovalLevel)
        {
            List<DVOApprovalLevel> lstDVOApprovalLevel = new List<DVOApprovalLevel>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = objDVOApprovalLevel.acd_id;
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(objDVOApprovalLevel.FIND_APPROVAL_BY_MODULE(ref parameters)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOApprovalLevel objDVOApprovalLevel1 = new DVOApprovalLevel();
                        if (!Convert.IsDBNull(dr[0])) objDVOApprovalLevel1.acd_id = Convert.ToInt32(dr[0]);//"acd_id"
                        if (!Convert.IsDBNull(dr[1])) objDVOApprovalLevel1.approval_level = Convert.ToInt32(dr[1]);//"approval_level"
                        if (!Convert.IsDBNull(dr[2])) objDVOApprovalLevel1.amount = Convert.ToDecimal(dr[2]);//"amount"
                        if (!Convert.IsDBNull(dr[3])) objDVOApprovalLevel1.description = Convert.ToString(dr[3]);//"description"
                        lstDVOApprovalLevel.Add(objDVOApprovalLevel1);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOApprovalLevel;
        }
    }
}
