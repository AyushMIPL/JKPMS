using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLSecUsers
    {
        public static DVOSecUsers Authentication(string LoginId, string Password, string MachineInfo, string ApplicationVersion)
        {
            object[] parameters = new object[4];
            parameters[0] = LoginId;
            parameters[1] = Password;
            parameters[2] = MachineInfo;
            parameters[3] = ApplicationVersion;

            DVOSecUsers objSecUsers = new DVOSecUsers();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();

            using (DataSet ds = objDalBaseClass.Authentication(ref parameters, typeof(DVOSecUsers)))
            {
                try
                {
                    objSecUsers.UserId = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"V_USERID"
                    objSecUsers.FirstName = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString().Trim() : string.Empty;//"V_FIRSTNAME"
                    objSecUsers.LastName = (ds.Tables[0].Rows[0][2] != DBNull.Value) ? ds.Tables[0].Rows[0][2].ToString().Trim() : string.Empty;//"V_LASTNAME"
                    objSecUsers.LoginId = (ds.Tables[0].Rows[0][3] != DBNull.Value) ? ds.Tables[0].Rows[0][3].ToString().Trim() : string.Empty;//"V_LOGINID"
                    objSecUsers.Password = (ds.Tables[0].Rows[0][4] != DBNull.Value) ? ds.Tables[0].Rows[0][4].ToString().Trim() : string.Empty;//"V_PASSWORD"
                    objSecUsers.EmployeeId = (ds.Tables[0].Rows[0][5] != DBNull.Value) ? ds.Tables[0].Rows[0][5].ToString().Trim() : string.Empty;//"V_EMPID"
                    objSecUsers.Department = (ds.Tables[0].Rows[0][6] != DBNull.Value) ? ds.Tables[0].Rows[0][6].ToString().Trim() : string.Empty;//"V_DEPARTMENT"
                    objSecUsers.RoleId = (ds.Tables[0].Rows[0][7] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][7]) : 0;//"V_ROLEID"
                    objSecUsers.RoleName = (ds.Tables[0].Rows[0][8] != DBNull.Value) ? ds.Tables[0].Rows[0][8].ToString().Trim() : string.Empty;//"V_ROLE"
                    objSecUsers.loginlogid = (ds.Tables[0].Rows[0][9] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][9]) : 0;//"V_LOGINLOGID"
                    objSecUsers.LastPwdUpddate = (ds.Tables[0].Rows[0][12] != DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0][12]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty;//v_LastPwdUpddate
                    if (ds.Tables[0].Rows[0][11] != DBNull.Value && ds.Tables[0].Rows[0][11].ToString().Trim().Length > 0)
                        objSecUsers.UserExpiresDate = Convert.ToDateTime(ds.Tables[0].Rows[0][11]);//v_userExpiresOn
                    objSecUsers.MenuStyle = (ds.Tables[0].Rows[0][15] != DBNull.Value) ? ds.Tables[0].Rows[0][15].ToString().Trim() : string.Empty;//"p_menustyle"
                    objSecUsers.ServerDate = Convert.ToDateTime(ds.Tables[0].Rows[0][16]);
                    objSecUsers.CurPeriod = (ds.Tables[0].Rows[0][17] != DBNull.Value) ? ds.Tables[0].Rows[0][17].ToString().Trim() : string.Empty;//CurPeriod
                    objSecUsers.CurYear = (ds.Tables[0].Rows[0][18] != DBNull.Value) ? ds.Tables[0].Rows[0][18].ToString().Trim() : string.Empty;//CurYear    


                }
                catch (Exception ex)
                {
                
                }

            }

            return objSecUsers;
        }

        public static int InsertUser(ref DVOSecUsers objSecUser)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
            int c;
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objSecUser.FirstName;
                parameters[1] = objSecUser.LastName;
                parameters[2] = objSecUser.LoginId;
                parameters[3] = objSecUser.Password;
                parameters[4] = objSecUser.EmployeeId;
                parameters[5] = objSecUser.Department;
                parameters[6] = objSecUser.RoleId;
                parameters[7] = objSecUser.InsertBy;
                parameters[8] = objSecUser.InsertMachineInfo;
                //Added by Rahul jain on 14/11/2008 for insert a UserExpiresOn
                parameters[9] = objSecUser.UserExpiresDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //Added by Bharat Dhall [04/10/2009]
                parameters[10] = objSecUser.EmailId;
                //************************************
                c = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecUsers));
                if (Convert.ToInt32(c) < 1)
                    throw new Exception();
                if (objTransaction != null)
                {
                    if (Convert.ToInt32(c) >= 1)
                        objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
                }
                return c;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return c;
        }

        public static List<DVOSecUsers> GetUsers(ref DVOSecUsers objsecUser)
        {
            object[] parameters = new object[11];
            parameters[0] = objsecUser.UserId;
            parameters[1] = objsecUser.FirstName;
            parameters[2] = objsecUser.LastName;
            parameters[3] = objsecUser.LoginId;
            parameters[4] = objsecUser.EmployeeId;
            parameters[5] = objsecUser.Department;
            parameters[6] = objsecUser.RoleId;
            parameters[7] = objsecUser.Active;
            //Added By Rahul jain on 14/11/2008 for adding a new parameter UserexpiresOn
            if (objsecUser.UserExpiresOn == string.Empty)
                objsecUser.UserExpiresOn = null;
            parameters[8] = objsecUser.UserExpiresOn;
            if (objsecUser.LastPwdUpddate == string.Empty)
                objsecUser.LastPwdUpddate = null;
            parameters[9] = objsecUser.LastPwdUpddate; 
            //*****************************************************************
            //Added by Bharat Dhall [04/10/2009]
            parameters[10] = objsecUser.EmailId;
            //***************************************

            List<DVOSecUsers> objSecUserList = new List<DVOSecUsers>();

            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecUsers)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecUsers objSecUser = new DVOSecUsers();
                    objSecUser.UserId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_userid"
                    objSecUser.RoleId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_roleid"
                    objSecUser.RoleName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_role"
                    objSecUser.FirstName = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//"p_firstname"
                    objSecUser.LastName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"p_lastname"
                    objSecUser.LoginId = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"p_loginid"
                    objSecUser.EmployeeId = (dr[6] != DBNull.Value) ? dr[6].ToString().Trim() : string.Empty;//"p_empid"
                    objSecUser.Department = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"p_department"
                    objSecUser.Active = (dr[8] != DBNull.Value) ? dr[8].ToString().Trim() : string.Empty;//"p_active"
            

                    objSecUser.LastPwdUpddate = (dr[10] != DBNull.Value) ? Convert.ToDateTime(dr[10]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty;
                  
                    if(dr[9]!=DBNull.Value)
                        if (dr[9].ToString() != "")
                        {
                            objSecUser.UserExpiresDate = Convert.ToDateTime(dr[9]);
                        }
                   
                    objSecUser.EmailId = (dr[12] != DBNull.Value) ? dr[12].ToString().Trim() : string.Empty;//"p_emailid"
                 
                    objSecUser.MenuStyle = (dr[13] != DBNull.Value) ? dr[13].ToString().Trim() : string.Empty;//"p_menustyle"
                    //*
                    objSecUserList.Add(objSecUser);
                }
            }

            return objSecUserList;
        }

        public static List<DVOSecUsers> GetUsers(ref DVOSecUsers objsecUser,bool withoutSecRole)
        {
            object[] parameters = new object[11];
            parameters[0] = objsecUser.UserId;
            parameters[1] = objsecUser.FirstName;
            parameters[2] = objsecUser.LastName;
            parameters[3] = objsecUser.LoginId;
            parameters[4] = objsecUser.EmployeeId;
            parameters[5] = objsecUser.Department;
            parameters[6] = objsecUser.RoleId;
            parameters[7] = objsecUser.Active;
            //Added By Rahul jain on 14/11/2008 for adding a new parameter UserexpiresOn
            if (objsecUser.UserExpiresOn == string.Empty)
                objsecUser.UserExpiresOn = null;
            parameters[8] = objsecUser.UserExpiresOn;
            if (objsecUser.LastPwdUpddate == string.Empty)
                objsecUser.LastPwdUpddate = null;
            parameters[9] = objsecUser.LastPwdUpddate;
            //*****************************************************************
            //Added by Bharat Dhall [04/10/2009]
            parameters[10] = objsecUser.EmailId;
            //***************************************

            List<DVOSecUsers> objSecUserList = new List<DVOSecUsers>();

            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(objsecUser.GET_USER(ref parameters)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecUsers objSecUser = new DVOSecUsers();
                    objSecUser.UserId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_userid"
                    objSecUser.RoleId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_roleid"
                    objSecUser.RoleName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_role"
                    objSecUser.FirstName = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//"p_firstname"
                    objSecUser.LastName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"p_lastname"
                    objSecUser.LoginId = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"p_loginid"
                    objSecUser.EmployeeId = (dr[6] != DBNull.Value) ? dr[6].ToString().Trim() : string.Empty;//"p_empid"
                    objSecUser.Department = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"p_department"
                    objSecUser.Active = (dr[8] != DBNull.Value) ? dr[8].ToString().Trim() : string.Empty;//"p_active"
                    //**********************RAHUL********************

                    objSecUser.LastPwdUpddate = (dr[10] != DBNull.Value) ? Convert.ToDateTime(dr[10]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty;
                    //Added by rahul jain on 14/11/2008 
                    if (dr[9] != DBNull.Value)
                        if (dr[9].ToString() != "")
                        {
                            objSecUser.UserExpiresDate = Convert.ToDateTime(dr[9]);
                        }
                    //Added by Bharat Dhall [04/10/2009]
                    objSecUser.EmailId = (dr[12] != DBNull.Value) ? dr[12].ToString().Trim() : string.Empty;//"p_emailid"
                    //***************************************
                    //Added by Bharat Dhall [04/10/2009]
                    objSecUser.MenuStyle = (dr[13] != DBNull.Value) ? dr[13].ToString().Trim() : string.Empty;//"p_menustyle"
                    //*
                    objSecUserList.Add(objSecUser);
                }
            }

            return objSecUserList;
        }

        public static List<DVOSecUsers> GetAllUsers()
        {
            List<DVOSecUsers> objSecUserList = new List<DVOSecUsers>();

            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();

            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecUsers)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecUsers objSecUser = new DVOSecUsers();
                    objSecUser.UserId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"p_userid"
                    objSecUser.RoleId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"p_roleid"
                    objSecUser.RoleName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_role"
                    objSecUser.FirstName = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//"p_firstname"
                    objSecUser.LastName = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"p_lastname"
                    objSecUser.LoginId = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"p_loginid"
                    objSecUser.EmployeeId = (dr[6] != DBNull.Value) ? dr[6].ToString().Trim() : string.Empty;//"p_empid"
                    objSecUser.Department = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"p_department"
                    objSecUser.Active = (dr[8] != DBNull.Value) ? dr[8].ToString().Trim() : string.Empty;//"p_active"
                    //Added by rahul jain on 14/11/2008 
                    objSecUser.UserExpiresOn = (dr[9] != DBNull.Value) ? dr[9].ToString().Trim() : string.Empty;//"p_UserExpiresOn"
                    objSecUser.LastPwdUpddate = (dr[10] != DBNull.Value) ? dr[10].ToString().Trim() : string.Empty;//"p_LastPwdUpddate"
                    //***********************************************
                    //Added by Bharat Dhall [04/10/2009]
                    objSecUser.EmailId = (dr[12] != DBNull.Value) ? dr[12].ToString().Trim() : string.Empty;//"p_emailid"
                    //***************************************
                    objSecUserList.Add(objSecUser);
                }
            }

            return objSecUserList;
        }

        public static int DeleteUser(ref DVOSecUsers objsecUser)
        {
            object[] parameters = new object[3];
            parameters[0] = objsecUser.UserId;
            parameters[1] = objsecUser.UpdateBy;
            parameters[2] = objsecUser.UpdateMachineInfo;
           // List<DVOSecUsers> objSecUserList = new List<DVOSecUsers>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
           int c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOSecUsers));
           return c;
            
        }

        public static DataSet GetALLUserByDataset()
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSecUsers));
            return ds;
        }

        public static object UpdateUserInfo(ref object objTransaction, ref DVOSecUsers objsecuser)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            int success = 0;

            try
            {
                object[] parameters = new object[11];
                parameters[0] = objsecuser.UserId;
                parameters[1] = objsecuser.FirstName;
                parameters[2] = objsecuser.LastName;
                parameters[3] = objsecuser.LoginId;
                parameters[4] = objsecuser.EmployeeId;
                parameters[5] = objsecuser.Department;
                parameters[6] = objsecuser.RoleId;
                parameters[7] = objsecuser.UpdateBy;
                parameters[8] = objsecuser.UpdateMachineInfo;
                parameters[9] = objsecuser.UserExpiresDate;              
                parameters[10] = objsecuser.EmailId;
           
                success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOSecUsers));

                if (!statusObjTransaction)
                    objDALBaseClassHelperSecurity.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;

        }

        public static int UpdateMenuStyle(ref object objTransaction, ref DVOSecUsers objsecuser)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            int success = 0;
            try
            {
                object[] parameters = new object[4];
                parameters[0] = objsecuser.MenuStyle;
                parameters[1] = objsecuser.UserId;
                parameters[2] = objsecuser.UpdateBy;
                parameters[3] = objsecuser.UpdateMachineInfo;

                object obj = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOSecUsers), objsecuser.UPDATE_STYLE);
                if (obj == null)
                    throw new Exception("Error occured to Update menu style in secusers.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured to Update menu style in secusers.");

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
            return success;

        }
    }
}
