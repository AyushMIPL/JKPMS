using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLGLUpdateAccountSubGroups
    {
        /// <summary>
        /// This method is use to get Account sub groups information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLUpdateAccountSubGroups type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account sub groups information</returns>
        public static List<DVOGLUpdateAccountSubGroups> GetAccountSubGroup(ref DVOGLUpdateAccountSubGroups objAccountAccountSubGroups)
        {
            object[] parameters = new object[5];
            parameters[0] = objAccountAccountSubGroups.acct_type;
            parameters[1] = objAccountAccountSubGroups.keyvalue;
            parameters[2] = objAccountAccountSubGroups.acct_desc;
            parameters[3] = objAccountAccountSubGroups.subtotal_group;
            parameters[4] = objAccountAccountSubGroups.rowid;

            List<DVOGLUpdateAccountSubGroups> objAccountAccountSubGroupslist = new List<DVOGLUpdateAccountSubGroups>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLUpdateAccountSubGroups)))
            {
                for (int i=0;i<=ds.Tables[0].Rows.Count-1;i++)
                {
                    DVOGLUpdateAccountSubGroups obj_AccountSubGroups = new DVOGLUpdateAccountSubGroups();
                    obj_AccountSubGroups.acct_type = ds.Tables[0].Rows[i][0].ToString().Trim();//acct_type
                    obj_AccountSubGroups.keyvalue = ds.Tables[0].Rows[i][1].ToString().Trim();//keyvalue
                    obj_AccountSubGroups.acct_desc = ds.Tables[0].Rows[i][2].ToString().Trim();//acct_desc
                    obj_AccountSubGroups.subtotal_group = ds.Tables[0].Rows[i][3].ToString().Trim();//subtotal_group 
                    obj_AccountSubGroups.acct_no = Convert.ToInt64(ds.Tables[0].Rows[i][4]);//acct_no
                    obj_AccountSubGroups.rowid  = Convert.ToInt32(ds.Tables[0].Rows[i][5]);//rowid

                    objAccountAccountSubGroupslist.Add(obj_AccountSubGroups);
                }
            }
            return objAccountAccountSubGroupslist;
        }

        /// <summary>
        /// This method is use to update account type information into database
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        //public static int UpdateAccountSubGroup(ref DVOGLUpdateAccountSubGroups objAccountAccountSubGroups)
        //{//Earlier...................................................
        //    //try
        //    //{
        //    //    object[] parameters = new object[5];
        //    //    parameters[0] = objAccountAccountSubGroups.acct_type;
        //    //    parameters[1] = objAccountAccountSubGroups.keyvalue;
        //    //    parameters[2] = objAccountAccountSubGroups.acct_desc;
        //    //    parameters[3] = objAccountAccountSubGroups.subtotal_group;
        //    //    parameters[4] = objAccountAccountSubGroups.acct_no;

        //    //    List<DVOGLUpdateAccountSubGroups> objGLAccountSubGroupsList = new List<DVOGLUpdateAccountSubGroups>();
        //    //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    //    objDalBaseClass.UpdateData(ref parameters, typeof(DVOGLUpdateAccountSubGroups));
        //    //    return 1;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ExceptionManagement.ExceptionManager.Publish(ex);
        //    //    throw ex;
        //    //}
        //    //return 1;

        //   //changed for locking purpose


        //}


        public static int UpdateAccountSubGroupInfo(ref object objTransaction, ref DVOGLUpdateAccountSubGroups objAccountSubGroups)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
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
                parameters[0] = objAccountSubGroups.acct_type;
                parameters[1] = objAccountSubGroups.keyvalue;
                parameters[2] = objAccountSubGroups.acct_desc;
                parameters[3] = objAccountSubGroups.subtotal_group;
                parameters[4] = objAccountSubGroups.acct_no;

               object  success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOGLUpdateAccountSubGroups),true);
               if (success == null)
                   throw new Exception();
               else if (Convert.ToInt16(success) < 1)
                   throw new Exception();

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
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

        public static int ChkAccountExist(ref DVOGLUpdateAccountSubGroups objDVOGLUpdateAccountSubGroups)
        {
            object[] parameters = new object[2];
            parameters[0] = objDVOGLUpdateAccountSubGroups.acct_type;
            parameters[1] = objDVOGLUpdateAccountSubGroups.keyvalue;
          

            List<DVOGLUpdateAccountSubGroups> objAccountAccountSubGroupslist = new List<DVOGLUpdateAccountSubGroups>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object  Result = objDalBaseClass.ExecuteScalar(ref parameters, objDVOGLUpdateAccountSubGroups.CHK_ACCOUNT_BEFORE_INSERT);

            return Convert.ToInt32(Result);


           
        }
    }
}
