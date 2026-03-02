using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
namespace JKPS.BLL
{
   public class BLLOSCheckingAccount
    {
        /// <summary>
        /// This method is use to get checking account information from database 
        /// </summary>
        /// <param name="objCheckingAccount">reference of DVOOSCheckingAccount type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having checking account information</returns>
       public static List<DVOOSCheckingAccount> GetCheckingAccount(ref DVOOSCheckingAccount objCheckingAccount)
       {
           ////make object to pass as parameter of search function
           //DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
           ////call getDate function of BLL
           //List<DVOOSCheckingAccount> listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount);
           //objDVOOSCheckingAccount = null;

           ////check list is null or not
           //if (listDVOOSCheckingAccount != null)
           //{
           //    //check the permission of user for cash accounts
           //    //foreach (DVOOSCheckingAccount obj in listDVOOSCheckingAccount)
           //    for (int i = 0; i < listDVOOSCheckingAccount.Count; i++)
           //    {
           //        DVOOSCheckingAccount obj = listDVOOSCheckingAccount[i];
           //        if (!BLLAccountingLiberary.HasAccountPermission(DVOApplicationUserInfo.LoginId, obj.keyvalue, obj.accounttypeId, "AR"))
           //        {
           //            //if user hasn't permission for account then remove from list
           //            listDVOOSCheckingAccount.Remove(obj);
           //            i--;
           //        }
           //    }
           //}
           //return listDVOOSCheckingAccount;



            object[] parameters = new object[2];            
            parameters[0] = objCheckingAccount.acct_no;
            parameters[1] = objCheckingAccount.rowid;
            List<DVOOSCheckingAccount> objCheckingAccountlist = new List<DVOOSCheckingAccount>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOOSCheckingAccount)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOOSCheckingAccount obj_CheckingAccount = new DVOOSCheckingAccount();

                    obj_CheckingAccount.accounttype = (dr[0]!=DBNull.Value ? dr[0].ToString().Trim():string.Empty);//accounttype
                    obj_CheckingAccount.desc = (dr[1]!=DBNull.Value ? dr[1].ToString().Trim():string.Empty);//desc
                    obj_CheckingAccount.keyvalue = (dr[2]!=DBNull.Value ? dr[2].ToString().Trim():string.Empty);//keyvalue
                    obj_CheckingAccount.acct_desc = (dr[3]!=DBNull.Value ? dr[3].ToString().Trim():string.Empty);//acct_desc
                    obj_CheckingAccount.acct_no = (dr[4]!=DBNull.Value ?Convert.ToInt32(dr[4]):0);//acct_no
                    obj_CheckingAccount.accounttypeId =(dr[5]!=DBNull.Value ? Convert.ToInt32(dr[5]):0);//accounttypeId
                    obj_CheckingAccount.rowid = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);//accounttypeId
                    objCheckingAccountlist.Add(obj_CheckingAccount);
                }
            }
            return objCheckingAccountlist;
        }

        /// <summary>
        /// This method is use to insert new checking account type into database
        /// </summary>
        /// <param name="objOSCheckingAccount">reference of DVOOSCheckingAccount type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static int InsertCheckingAccount(ref DVOOSCheckingAccount objOSCheckingAccount)
        {
            object c = null;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objOSCheckingAccount.acct_no;

                List<DVOOSCheckingAccount> objOSCheckingAccountList = new List<DVOOSCheckingAccount>();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                c = objDalBaseClass.InsertData(ref parameters, typeof(DVOOSCheckingAccount), true);
                if (c == null && Convert.ToInt32(c) < 1)
                    throw new Exception("Error in Insertion Checking Accounts");
                else
                    return Convert.ToInt32(c);
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }

        }

     
        /// <summary>
        /// This method is use to delete Account Type information from database
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
       public static int DeleteAccountType(ref DVOOSCheckingAccount objOSCheckingAccount)
        {
            object[] parameters = new object[1];
            parameters[0] = objOSCheckingAccount.acct_no;
            List<DVOOSCheckingAccount> objCheckingAccountList = new List<DVOOSCheckingAccount>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOOSCheckingAccount));
            return c;

        }

        /// <summary>
        /// This method is use to delete all transactions from check register table based on account number and department from database
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return object variable for confirmation, either data is deleted or not</returns>
        public static object DeleteCheckingAccount(ref DVOOSCheckingAccount objOSCheckingAccount)
        {
            object[] parameters = new object[2];
            parameters[0] = objOSCheckingAccount.acct_no;
            parameters[1] = objOSCheckingAccount.department;
            List<DVOOSCheckingAccount> objCheckingAccountList = new List<DVOOSCheckingAccount>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOOSCheckingAccount), objOSCheckingAccount.DELETE_CHECK_SPNAME);
            return c;

        }

       /// <summary>
       /// to get only permitted cash accounts for current login user
       /// </summary>
       /// <returns></returns>
       public static List<DVOOSCheckingAccount> GetCashCheckingAccountsForAR()
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           List<DVOOSCheckingAccount> listDVOOSCheckingAccount = new List<DVOOSCheckingAccount>();
           object[] parameters = new object[2];
           parameters[0] = DVOApplicationUserInfo.LoginId;
           parameters[1] = "AR";
           DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).ACCOUNT_PERMISSION);
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               ds.Tables[0].Columns[0].ColumnName = "AcctMask";
               ds.Tables[0].Columns[1].ColumnName = "AcctType";
               //make object to pass as parameter of search function
               DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
               //call getDate function of BLL
               listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount);
               objDVOOSCheckingAccount = null;

               //check list is null or not
               if (listDVOOSCheckingAccount != null)
               {
                   string _tempKV = string.Empty, _accMask = string.Empty;
                   //check the permission of user for cash accounts
                   //foreach (DVOOSCheckingAccount obj in listDVOOSCheckingAccount)
                   for (int i = 0; i < listDVOOSCheckingAccount.Count; i++)
                   {
                       DVOOSCheckingAccount obj = listDVOOSCheckingAccount[i];
                       DataRow[] drs = ds.Tables[0].Select("AcctType = " + obj.accounttypeId.ToString());
                       if (drs.Length > 0)
                       {
                           foreach (DataRow drtmp in drs)
                           {
                               _tempKV = obj.keyvalue.Trim();
                               _accMask = drtmp[0] != DBNull.Value ? drtmp[0].ToString().Trim() : string.Empty;
                               try
                               {
                                   for (int j = 0; j < _accMask.Length; j++)
                                   {
                                       if (_accMask[j] == '#')
                                       {
                                           _tempKV = _tempKV.Remove(j, 1);
                                           _tempKV = _tempKV.Insert(j, "#");
                                       }
                                   }
                               }
                               catch { }
                               if (_tempKV == _accMask)
                                   break;
                           }
                           if (_tempKV != _accMask)
                           {
                               //if user hasn't permission for account then remove from list
                               listDVOOSCheckingAccount.Remove(obj);
                               i--;
                           }
                           _tempKV = string.Empty; 
                           _accMask = string.Empty;
                       }
                       else
                       {
                           //if user hasn't permission for account then remove from list
                           listDVOOSCheckingAccount.Remove(obj);
                           i--;
                       }
                       //if (!BLLAccountingLiberary.HasAccountPermission(DVOApplicationUserInfo.LoginId, obj.keyvalue, obj.accounttypeId, "AR"))
                       //{
                       //    //if user hasn't permission for account then remove from list
                       //    listDVOOSCheckingAccount.Remove(obj);
                       //    i--;
                       //}
                   }
               }
           }
           return listDVOOSCheckingAccount;
       }

       /// <summary>
       /// to get only permitted cash accounts for current login user
       /// </summary>
       /// <returns></returns>
       public static List<DVOOSCheckingAccount> GetPermittedCashAccounts(string Module)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           List<DVOOSCheckingAccount> listDVOOSCheckingAccount = new List<DVOOSCheckingAccount>();
           object[] parameters = new object[2];
           parameters[0] = DVOApplicationUserInfo.LoginId;
           parameters[1] = Module;
           DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).ACCOUNT_PERMISSION);
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               ds.Tables[0].Columns[0].ColumnName = "AcctMask";
               ds.Tables[0].Columns[1].ColumnName = "AcctType";
               //make object to pass as parameter of search function
               DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
               //call getDate function of BLL
               listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount);
               objDVOOSCheckingAccount = null;

               //check list is null or not
               if (listDVOOSCheckingAccount != null)
               {
                   string _tempKV = string.Empty, _accMask = string.Empty;
                   //check the permission of user for cash accounts
                   for (int i = 0; i < listDVOOSCheckingAccount.Count; i++)
                   {
                       DVOOSCheckingAccount obj = listDVOOSCheckingAccount[i];
                       DataRow[] drs = ds.Tables[0].Select("AcctType = " + obj.accounttypeId.ToString());
                       if (drs.Length > 0)
                       {
                           foreach (DataRow drtmp in drs)
                           {
                               _tempKV = obj.keyvalue.Trim();
                               _accMask = drtmp[0] != DBNull.Value ? drtmp[0].ToString().Trim() : string.Empty;
                               try
                               {
                                   for (int j = 0; j < _accMask.Length; j++)
                                   {
                                       if (_accMask[j] == '#')
                                       {
                                           _tempKV = _tempKV.Remove(j, 1);
                                           _tempKV = _tempKV.Insert(j, "#");
                                       }
                                   }
                               }
                               catch { }
                               if (_tempKV == _accMask)
                                   break;
                           }
                           if (_tempKV != _accMask)
                           {
                               //if user hasn't permission for account then remove from list
                               listDVOOSCheckingAccount.Remove(obj);
                               i--;
                           }
                           _tempKV = string.Empty;
                           _accMask = string.Empty;
                       }
                       else
                       {
                           //if user hasn't permission for account then remove from list
                           listDVOOSCheckingAccount.Remove(obj);
                           i--;
                       }
                   }
               }
           }
           return listDVOOSCheckingAccount;


           ////make object to pass as parameter of search function
           //DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
           ////call getDate function of BLL
           //List<DVOOSCheckingAccount> listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount);
           //objDVOOSCheckingAccount = null;

           ////check list is null or not
           //if (listDVOOSCheckingAccount != null)
           //{
           //    //check the permission of user for cash accounts
           //    //foreach (DVOOSCheckingAccount obj in listDVOOSCheckingAccount)
           //    for (int i = 0; i < listDVOOSCheckingAccount.Count; i++)
           //    {
           //        DVOOSCheckingAccount obj = listDVOOSCheckingAccount[i];
           //        if (!BLLAccountingLiberary.HasAccountPermission(DVOApplicationUserInfo.LoginId, obj.keyvalue, obj.accounttypeId, Module))
           //        {
           //            //if user hasn't permission for account then remove from list
           //            listDVOOSCheckingAccount.Remove(obj);
           //            i--;
           //        }
           //    }
           //}
           //return listDVOOSCheckingAccount;
       }

    }
}
