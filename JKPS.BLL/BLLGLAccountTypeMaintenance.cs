using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
namespace JKPS.BLL
{
    public class BLLGLAccountTypeMaintenance
    {
        /// <summary>
        /// This method is use to get Account type information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static List<DVOGLAccountTypeMaintenance> GetAccountTypeMaintenance(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {
            object[] parameters = new object[7];
            parameters[0] = objAccountTypeMaintenance.accounttype;
            parameters[1] = objAccountTypeMaintenance.desc;
            parameters[2] = objAccountTypeMaintenance.dfltacctcat;
            parameters[3] = objAccountTypeMaintenance.dfltincrwcrdt;
            parameters[4] = objAccountTypeMaintenance.printsafter;
            parameters[5] = objAccountTypeMaintenance.AccountCategory;
            parameters[6] = objAccountTypeMaintenance.rowid;

            List<DVOGLAccountTypeMaintenance> objSecAccountTypeMaintenancelist = new List<DVOGLAccountTypeMaintenance>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOGLAccountTypeMaintenance obj_AccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
                    if (dr[0] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.id = Convert.ToInt32(dr[0]);//id
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.accounttype = dr[1].ToString().Trim();//accounttype
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.desc = dr[2].ToString().Trim();//desc
                    }
                    

                    if (dr[3] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.keylength = Convert.ToInt32(dr[3]);//keylength
                    }
                    else
                    {
                        obj_AccountTypeMaintenance.keylength = 0;//keylength
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.segmentcnt = Convert.ToInt32(dr[4]);//segmentcnt
                    }
                    else
                    {
                        obj_AccountTypeMaintenance.segmentcnt = 0;
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.printsafter = dr[5].ToString().Trim();//printsafter
                    }
                    obj_AccountTypeMaintenance.dfltacctcat = dr[6].ToString().Trim();//dfltacctcat
                    obj_AccountTypeMaintenance.dfltincrwcrdt = dr[7].ToString().Trim();//dfltincrwcrdt
                    obj_AccountTypeMaintenance.rowid = Convert.ToInt32(dr[8].ToString().Trim());//rowid
                    objSecAccountTypeMaintenancelist.Add(obj_AccountTypeMaintenance);
                }
            }
            return objSecAccountTypeMaintenancelist;
        }

        /// <summary>
        /// This method is use to insert new account type into database
        /// </summary>
        /// <param name="objGLAccountType">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static object InsertAccountType(ref DVOGLAccountTypeMaintenance objGLAccountType, ref List<DVOGLSegment> listDVOGLSegment)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objGLAccountType.accounttype;
                parameters[1] = objGLAccountType.desc;
                parameters[2] = objGLAccountType.keylength;
                parameters[3] = objGLAccountType.printsafter;
                parameters[4] = objGLAccountType.dfltacctcat;
                parameters[5] = objGLAccountType.dfltincrwcrdt;
                

                using (DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance)))
                {
                    int newacd_id = 0;
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            newacd_id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"id"
                            if (newacd_id != 0)
                            {
                                foreach (DVOGLSegment objGLSegment in listDVOGLSegment)
                                    if (listDVOGLSegment.Count > 0)
                                    {
                                        objGLSegment.strucid = newacd_id;
                                    }
                                obj = BLLGLAccountTypeMaintenance.InsertData(ref objTransaction, ref listDVOGLSegment);

                                if (listDVOGLSegment.Count <= 0)
                                {
                                    obj = newacd_id;
                                }

                                //else  
                                //  {
                                //      obj = newacd_id;
                                //  }
                            }
                            else
                            {
                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                objDALBaseClass = null;
                                return obj;
                            }
                        }
                }
                parameters = null;
                objDALBaseClass = null;

                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;
            //List<DVOGLAccountTypeMaintenance> objGLAccountTypeList = new List<DVOGLAccountTypeMaintenance>();
            //DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
            //object obj = objDalBaseClass.InsertData(ref parameters, typeof(DVOGLAccountTypeMaintenance), true);
            //int c = 0;
            //if (obj != DBNull.Value)
            //{
            //    c = Convert.ToInt32(obj);
            //}
            //return c;

        }
        /// <summary>
        /// This method is use to update account type information into database
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        //public static object UpdateAccountType(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance, ref List<DVOGLSegment> listDVOGLSegment)
        //{
        //    DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    object obj = null;
        //    try
        //    {
        //        object[] parameters = new object[6];
        //        parameters[0] = objAccountTypeMaintenance.id;
        //        parameters[1] = objAccountTypeMaintenance.accounttype;
        //        parameters[2] = objAccountTypeMaintenance.desc;
        //        parameters[3] = objAccountTypeMaintenance.dfltacctcat;
        //        parameters[4] = objAccountTypeMaintenance.dfltincrwcrdt;
        //        parameters[5] = objAccountTypeMaintenance.printsafter;
        //        obj=objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance),true);
        //        if (obj.ToString() != "0")
        //        {
        //            DVOGLSegment objGLSegment = new DVOGLSegment();
        //            objGLSegment.strucid = objAccountTypeMaintenance.id;
        //            object objDel=BLLGLAccountTypeMaintenance.DeleteData(ref objTransaction, ref objGLSegment);
        //            if (objDel.ToString() == "1")
        //            {
        //                object objIns=BLLGLAccountTypeMaintenance.InsertData(ref objTransaction, ref listDVOGLSegment);
        //                if (objIns.ToString() == "1")
        //                {
        //                    objGLSegment = null;
        //                    parameters = null;
        //                    objDALBaseClass = null;

        //                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //                    return obj;
        //                }
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return obj;
        //    }
        //    return obj;


        //    //List<DVOGLAccountTypeMaintenance> objSecAccountTypeList = new List<DVOGLAccountTypeMaintenance>();
        //    //DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    //int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOGLAccountTypeMaintenance));
        //    //return c;
        //}

        /// <summary>
        /// This method is use to delete Account Type information from database
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
        public static object DeleteAccountType(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {
            object[] parameters = new object[3];
            parameters[0] = objAccountTypeMaintenance.id;
            parameters[1] = objAccountTypeMaintenance.accounttype;
            parameters[2] = "";
            List<DVOGLAccountTypeMaintenance> objSecAccountTypeList = new List<DVOGLAccountTypeMaintenance>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOGLAccountTypeMaintenance), true);
            return c;

        }
        /// <summary>
        /// Get segments assigned to a particular Account-Type
        /// </summary>
        /// <param name="objGLAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>dataset with information of segments of particular Account-Type</returns>
        public static DataSet GetGLAccountSegments(ref DVOGLAccountTypeMaintenance objGLAccountTypeMaintenance)
        {
            object[] parameters = new object[1];
            parameters[0] = objGLAccountTypeMaintenance.id;
            // parameters[1] = objGLAccountTypeMaintenance.rowid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance), objGLAccountTypeMaintenance.GET_GL_ACCOUNT_SEGMENTS);//
            parameters = null;
            objDalBaseClass = null;
            return ds;
        }

        /// <summary>
        /// Check number of account existing corressponding to accounttype, before delete to any account type
        /// If exist you can't delete account type
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
        public static object GetexistingAccount(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {
            object[] parameters = new object[3];
            parameters[0] = objAccountTypeMaintenance.id;
            parameters[1] = objAccountTypeMaintenance.accounttype;
            parameters[2] = "check";//To check if account exist corresponding to account type
            List<DVOGLAccountTypeMaintenance> objSecAccountTypeList = new List<DVOGLAccountTypeMaintenance>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOGLAccountTypeMaintenance), true);
            return c;
        }

        public static void BulkCopy(DataTable dtTobeInserted_updated, string srcTableName, string ForeignKeyName, object ForeignKeyValue)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            objDalBaseClass.BulkCopy(dtTobeInserted_updated, srcTableName, ForeignKeyName, ForeignKeyValue);

        }

        public static object InsertData(ref object objTransaction, ref List<DVOGLSegment> listDVOGLSegment)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objIns = null;
            try
            {
                foreach (DVOGLSegment obj in listDVOGLSegment)
                {
                    object[] parameters = new object[6];
                    parameters[0] = obj.strucid;
                    parameters[1] = obj.flexsegid;
                    parameters[2] = obj.position;
                    parameters[3] = obj.length;
                    parameters[4] = obj.required;
                    parameters[5] = obj.subtotal;
                    objIns = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLSegment), true);
                    if (objIns.ToString() != "1")
                    {
                        objDALBaseClass = null;
                        parameters = null;
                        return objIns;
                    }

                }
                objDALBaseClass = null;
                return objIns;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return objIns;
            }
            return objIns;
        }

        public static object DeleteData(ref object objTransaction, ref DVOGLSegment objGLSegment)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                if (objGLSegment != null)
                {
                    object[] parameters = new object[1];
                    parameters[0] = objGLSegment.strucid;

                    obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLSegment), true);
                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                else
                    throw ex;

                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;
        }
        /// <summary>
        /// This method is use to get Account type information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static DataSet GetAccountTypeMaintenance_DataSet(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {
            object[] parameters = new object[7];
            parameters[0] = objAccountTypeMaintenance.accounttype;
            parameters[1] = objAccountTypeMaintenance.desc;
            parameters[2] = objAccountTypeMaintenance.dfltacctcat;
            parameters[3] = objAccountTypeMaintenance.dfltincrwcrdt;
            parameters[4] = objAccountTypeMaintenance.printsafter;
            parameters[5] = objAccountTypeMaintenance.AccountCategory;
            parameters[6] = objAccountTypeMaintenance.rowid;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance));
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].Columns[0].ColumnName = "id";
                ds.Tables[0].Columns[1].ColumnName = "accounttype";
                ds.Tables[0].Columns[2].ColumnName = "desc";
                ds.Tables[0].Columns[3].ColumnName = "keylength";
                ds.Tables[0].Columns[4].ColumnName = "segmentcnt";
                ds.Tables[0].Columns[5].ColumnName = "printsafter";
                ds.Tables[0].Columns[6].ColumnName = "dfltacctcat";
                ds.Tables[0].Columns[7].ColumnName = "dfltincrwcrdt";
                ds.Tables[0].Columns[8].ColumnName = "rowid";
            }

            return ds;
        }

        /// <summary>
        /// This method is use to get Account type information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static List<DVOGLAccountTypeMaintenance> GetSegmentDesc(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {

            object[] parameters = new object[1];
            parameters[0] = objAccountTypeMaintenance.accounttype;

            List<DVOGLAccountTypeMaintenance> objSecAccountTypeMaintenancelist = new List<DVOGLAccountTypeMaintenance>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance), objAccountTypeMaintenance.GET_SEGMENTS_DESC))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOGLAccountTypeMaintenance obj_AccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
                    obj_AccountTypeMaintenance.SegmentDesc = dr[0].ToString().Trim();//desc
                    if (dr[1] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.SId = Convert.ToInt32(dr[1]);//subdivides
                    }

                    obj_AccountTypeMaintenance.Subdivides = dr[2].ToString().Trim();//desc                   
                    objSecAccountTypeMaintenancelist.Add(obj_AccountTypeMaintenance);
                }
            }
            return objSecAccountTypeMaintenancelist;
        }


        //******************************* Added by Bharat ************************************************
        /// <summary>
        /// Get segments assigned to a particular Account-Type
        /// </summary>
        /// <param name="objGLAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>dataset with information of segments of particular Account-Type</returns>
        public static DataSet GetAccountSegments(ref DVOGLAccountTypeMaintenance objGLAccountTypeMaintenance)
        {
            object[] parameters = new object[1];
            parameters[0] = objGLAccountTypeMaintenance.id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance), objGLAccountTypeMaintenance.GET_ACCOUNT_SEGMENTS);
            /*  Column 1 = v_strucid
             *  Column 2 = v_flexsegid
             *  Column 3 = v_length
             *  Column 4 = v_position
             *  Column 5 = v_desc     */
            parameters = null;
            objDalBaseClass = null;
            return ds;
        }

        /// <summary>
        /// This method is use to get Account type information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static List<DVOGLAccountTypeMaintenance> GetAccountTypes(ref DVOGLAccountTypeMaintenance objAccountTypeMaintenance)
        {
            object[] parameters = new object[1];
            objAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
            parameters[0] = objAccountTypeMaintenance.AccountCategory;

            List<DVOGLAccountTypeMaintenance> objSecAccountTypeMaintenancelist = new List<DVOGLAccountTypeMaintenance>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance), objAccountTypeMaintenance.GET_ACCOUNT_TYPES))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOGLAccountTypeMaintenance obj_AccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
                    if (dr[0] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.id = Convert.ToInt32(dr[0]);//id
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.accounttype = dr[1].ToString().Trim();//accounttype
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.desc = dr[2].ToString().Trim();//desc
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.keylength = Convert.ToInt32(dr[3]);//keylength
                    }
                    else
                    {
                        obj_AccountTypeMaintenance.keylength = 0;//keylength
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.segmentcnt = Convert.ToInt32(dr[4]);//segmentcnt
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.printsafter = dr[5].ToString().Trim();//printsafter
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.dfltacctcat = dr[6].ToString().Trim();//dfltacctcat
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        obj_AccountTypeMaintenance.dfltincrwcrdt = dr[7].ToString().Trim();//dfltincrwcrdt
                    }
                        objSecAccountTypeMaintenancelist.Add(obj_AccountTypeMaintenance);
                }
            }
            return objSecAccountTypeMaintenancelist;
        }
        //**************************************************************************************************

        public static object UpdateAccountTypeInfo(ref object objTransaction, ref DVOGLAccountTypeMaintenance objGLAccountType, ref List<DVOGLSegment> lstDVODVOGLSegment)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //int success = 0;
            //int success = 0;
            object obj = null;
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objGLAccountType.id;
                parameters[1] = objGLAccountType.accounttype;
                parameters[2] = objGLAccountType.desc;
                parameters[3] = objGLAccountType.printsafter;
                parameters[4] = objGLAccountType.dfltacctcat;
                parameters[5] = objGLAccountType.dfltincrwcrdt;
                
                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance), true);
                if (obj.ToString() != "0")
                {
                    DVOGLSegment objGLSegment = new DVOGLSegment();
                    if (lstDVODVOGLSegment.Count > 0)
                    {
                        objGLSegment.strucid = objGLAccountType.id;
                        object objDel = BLLGLAccountTypeMaintenance.DeleteData(ref objTransaction, ref objGLSegment);
                        if (objDel.ToString() == "1")
                        {
                            object objIns = BLLGLAccountTypeMaintenance.InsertData(ref objTransaction, ref lstDVODVOGLSegment);
                            if (objIns.ToString() == "1")
                            {
                                objGLSegment = null;
                                parameters = null;
                                objDALBaseClass = null;
                            }
                        }
                    }
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                   
                    return 1;
                }
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            return 1;
        }

        //Added By Rajeev
        //Aim : To Get All Account Type (For Budget Estimate)
        //Date : 08/07/2009
        public static List<DVOGLAccountTypeMaintenance> GetAllAccountType()
        {
            DVOGLAccountTypeMaintenance objGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
            List<DVOGLAccountTypeMaintenance> objSecAccountTypeMaintenancelist = new List<DVOGLAccountTypeMaintenance>();
            object[] parameters = new object[0];            
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLAccountTypeMaintenance), objGLAccountTypeMaintenance.ALL_ACCOUNTTYPE);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DVOGLAccountTypeMaintenance obj_AccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
                obj_AccountTypeMaintenance.id = Convert.ToInt32(dr[0]);//id
                obj_AccountTypeMaintenance.accounttype = dr[1].ToString().Trim();//accounttype
                obj_AccountTypeMaintenance.desc = dr[2].ToString().Trim();//desc

                if (dr[3] != DBNull.Value)
                {
                    obj_AccountTypeMaintenance.keylength = Convert.ToInt32(dr[3]);//keylength
                }
                else
                {
                    obj_AccountTypeMaintenance.keylength = 0;//keylength
                }

                objSecAccountTypeMaintenancelist.Add(obj_AccountTypeMaintenance);
            }
            parameters = null;
            objDalBaseClass = null;
            return objSecAccountTypeMaintenancelist;
        }


    }
}
