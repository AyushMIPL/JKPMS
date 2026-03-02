using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;


namespace JKPS.BLL
{
    public class BLLSalaryCategory
    {

        public static List<DVOSalaryScale> GetSalaryCode()
        {
            List<DVOSalaryScale> objsalaryScaleList = new List<DVOSalaryScale>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOSalaryScale)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSalaryScale objsalary = new DVOSalaryScale();
                    objsalary.Code = dr[0].ToString().Trim();//"p_code"
                    objsalary.Per_anum = Convert.ToDecimal(dr[1]);//"p_per_anum"
                    objsalaryScaleList.Add(objsalary);
                }
            }
            return objsalaryScaleList;
        }

        //Commented by SUnil
        //public static bool InsertCategory(ref DVOSalaryCategory  ObjInsSalaryScale)
        //{
        //    bool status = false;
        //    object[] parameters = new object[2];
        //    parameters[0] = ObjInsSalaryScale.Code;
        //    parameters[1] = ObjInsSalaryScale.Desc;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    //***********************Commented by Sunil Pahwa***************
        //    //object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryScale.INSERT_SPNAME, true);
        //    //***********************Modifed by Sunil Pahwa***********************************
        //   // object c = objDalBaseClass.InsertData ( ref parameters, typeof(DVOSalaryCategory));
        //    object c = objDalBaseClass.ExecuteScalar(ref parameters, ObjInsSalaryScale.INSERT_SPNAME);
        //    //********************************************************************************
        //    if (Convert.ToInt16(c) == 1)
        //    {
        //        status = true;
        //    }
        //    return status;
        //}
        public static DataSet Get_Details(ref DVOSalaryScale objDVODetailsSearchCriteria)
        {

            object[] parameters = new object[1];
            parameters[0] = objDVODetailsSearchCriteria.Code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryScale), objDVODetailsSearchCriteria.FIND_DETAIL);
            return ds;

        }
        public static List<DVOSalaryScale> GetPer_AnnumByScaleCode(ref DVOSalaryScale objDVOSearchCriteria)
        {

            object[] parameters = new object[1];
            parameters[0] = objDVOSearchCriteria.Scale_code;

            List<DVOSalaryScale> objDVOlist = new List<DVOSalaryScale>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryScale), objDVOSearchCriteria.FIND_DETAIL_BY_SCALECODE))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSalaryScale tempobjDVO = new DVOSalaryScale();
                    tempobjDVO.RowID = Convert.ToInt32(dr[0]);
                    tempobjDVO.Scale_code = dr[1].ToString().Trim();
                    tempobjDVO.Per_anum = Convert.ToDecimal(dr[2]);
                    objDVOlist.Add(tempobjDVO);
                }
            }
            return objDVOlist;
        }
        public static bool UpdateCategory(ref DVOSalaryScale objSalaryScale, ref object objTransaction)
        {
            bool status = false;
            object[] parameters = new object[3];
            parameters[0] = objSalaryScale.RowID;
            parameters[1] = objSalaryScale.Code;
            parameters[2] = objSalaryScale.Desc;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryScale.UPDATE_SPNAME, true);
            if (Convert.ToInt16(c) == 1)
            {
                status = true;
            }
            return status;
        }
        public static bool DeleteCategory(ref DVOSalaryScale objSalaryScale, ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool status = false;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objSalaryScale.RowID;
                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryScale.DELETE_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);

                status = true;

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                status = false;
            }
            //return 1;

            return status;

        }
        public static bool DeleteCategoryDetails(ref object objTransaction, ref DVOSalaryScale objSalaryScale)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            bool status = false;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objSalaryScale.First_code;
                object c = objDalBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSalaryScale), objSalaryScale.DELETE_SALARY_DETAIL_BY_CODE);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                status = true;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;

            }
            return status;
        }
        public static bool InsertCategoryDetails(ref DVOSalaryScale objSalaryScale, ref object objTransaction)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool status = false;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objSalaryScale.Code;
                parameters[1] = objSalaryScale.Scale_code;

                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryScale.INSERT_SALARY_DETAIL_BY_CODE, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt32(c) < 1)
                    throw new Exception();

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                status = true;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }


            return status;
        }
        public static List<DVOSalaryCategory> GetSalaryCategory(ref DVOSalaryCategory objDVOSalarySearchCriteria)
        {
            Object[] parameters = new object[3];

            parameters[0] = objDVOSalarySearchCriteria.Code;
            parameters[1] = objDVOSalarySearchCriteria.Desc;

            //added by sunil pahwa

            parameters[2] = objDVOSalarySearchCriteria.RowID;

            List<DVOSalaryCategory> objDVODetailsList = new List<DVOSalaryCategory>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryCategory)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSalaryCategory tempobjDVO = new DVOSalaryCategory();
                    tempobjDVO.RowID = Convert.ToInt32(dr["p_rowid"]);
                    tempobjDVO.Code = dr["p_code"].ToString().Trim();
                    tempobjDVO.Desc = dr["p_desc"].ToString().Trim();

                    objDVODetailsList.Add(tempobjDVO);


                }
            }
            return objDVODetailsList;
        }
        public static DataSet GetSalaryCatDetails(ref DVOSalaryScale objDVOsalaryscale)
        {
            Object[] parameters = new object[1];

            parameters[0] = objDVOsalaryscale.Code;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryScale), objDVOsalaryscale.FIND_DETAIL_BY_CODE);
            return ds;
        }
        public static DataSet Get_SalryCode(ref DVOSalaryScale objDVODetailsSearchCriteria)
        {
            object[] parameters = new object[2];
            parameters[0] = objDVODetailsSearchCriteria.First_code;
            parameters[1] = objDVODetailsSearchCriteria.Last_code;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryScale), objDVODetailsSearchCriteria.FIND_SALARY_CODE);
            return ds;

        }

        //***************************  Added By Bharat Dhall [19 December, 2008] ************
        /// <summary>
        /// get minimim scale code of category code 
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <param name="PerAnnum"></param>
        /// <returns></returns>
        public static string GetMinScaleCode(string CategoryCode, out Single PerAnnum)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string minScaleCode = string.Empty;
            PerAnnum = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = 0;// '0' for minimum scale code and '1' for maximum scale code
                parameters[1] = CategoryCode;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryCategory), (new DVOSalaryCategory()).GET_MIN_MAX_SCALE_CODE))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                minScaleCode = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? ds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;
                                PerAnnum = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToSingle(ds.Tables[0].Rows[0][1]) : 0;
                            }
                }
                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            objDalBaseClass = null;
            return minScaleCode;
        }
        /// <summary>
        /// get maximum scale code of category code 
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <param name="PerAnnum"></param>
        /// <returns></returns>
        public static string GetMaxScaleCode(string CategoryCode, out Single PerAnnum)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string maxScaleCode = string.Empty;
            PerAnnum = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = 1;// '0' for minimum scale code and '1' for maximum scale code
                parameters[1] = CategoryCode;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryCategory), (new DVOSalaryCategory()).GET_MIN_MAX_SCALE_CODE))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                maxScaleCode = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? ds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;
                                PerAnnum = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToSingle(ds.Tables[0].Rows[0][1]) : 0;
                            }
                }
                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            objDalBaseClass = null;
            return maxScaleCode;
        }
        //***********************************************************************************


        //public static List<DVOSalaryScale> GetSalaryScaleByCode(ref DVOSalaryScale objDVOsalaryscale)
        //{
        //    Object[] parameters = new object[1];

        //    parameters[0] = objDVOsalaryscale.Cat_code;

        //    List<DVOSalaryScale> objsalaryScaleList = new List<DVOSalaryScale>();
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryScale), objDVOsalaryscale.FIND_LAST_CODE))
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            DVOSalaryScale objsalary = new DVOSalaryScale();
        //            objsalary.Code = dr[0].ToString().Trim();//"p_code"
        //            objsalary.Per_anum = Convert.ToDecimal(dr[1]);//"p_per_anum"
        //            objsalaryScaleList.Add(objsalary);
        //        }
        //    }
        //    return objsalaryScaleList;
        //}

        public static bool UpdateSalaryCategory(ref DVOSalaryScale objSalaryscale, ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool status = false;
            try
            {

                object[] parameters = new object[3];
                parameters[0] = objSalaryscale.RowID;
                parameters[1] = objSalaryscale.Code;
                parameters[2] = objSalaryscale.Desc;
                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryscale.UPDATE_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                status = true;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return status;


        }

        //public static bool InsertCategor(ref DVOSalaryCategory ObjInsSalaryScale)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public static int InsertCategoryInfo(ref object objTransaction, ref DVOSalaryCategory ObjInsSalaryScale)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = ObjInsSalaryScale.Code;
                parameters[1] = ObjInsSalaryScale.Desc;
                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, ObjInsSalaryScale.INSERT_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt32(c) < 1)
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

        public static int InsertCategoryInfo(ref object objTransaction, ref DVOSalaryCategory ObjInsSalaryScale, ref List<DVOSalaryScale> lst)
        {
            int i = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = ObjInsSalaryScale.Code;
                parameters[1] = ObjInsSalaryScale.Desc;
                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, ObjInsSalaryScale.INSERT_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt32(c) < 1)
                    throw new Exception();
                if (Convert.ToInt16(c) == 1)
                {
                    i = BLLSalaryCategory.InsertDetailInfo(ref objTransaction, ref lst);
                    if (i == 1)
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return 1;
                    }
                    else
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return 0;
                    }
                }
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

        private static int InsertDetailInfo(ref object objTransaction, ref List<DVOSalaryScale > lst)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {

                foreach (DVOSalaryScale  obj in lst)
                {
                    object[] parameters = new object[2];
                    parameters[0] = obj.Code;
                    parameters[1] = obj.Scale_code;

                    object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SALARY_DETAIL_BY_CODE, true);
                    if (c == null)
                        throw new Exception();
                    else if (Convert.ToInt32(c) < 1)
                        throw new Exception();
                }
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
                //status = true;
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

        public static int UpdSalaryCategoryInfo(ref DVOSalaryCategory  objSalaryscale, ref object objTransaction, ref List<DVOSalaryScale> lstDVOSalaryScale)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {

                object[] parameters = new object[3];
                parameters[0] = objSalaryscale.RowID;
                parameters[1] = objSalaryscale.Code;
                parameters[2] = objSalaryscale.Desc;
                object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryscale.UPDATE_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();
                if (Convert.ToInt16 (c) == 1)
                {
                    int i = BLLSalaryCategory.UpdateDetailInfo(ref objSalaryscale,  ref objTransaction, ref lstDVOSalaryScale);
                    if (i == 1)
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    }
                    else
                    {
                        if (!statusObjTransaction)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return 0;
                    }



                }





                //if (!statusObjTransaction)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //status = true;




                //object[] Parameter = new object[3];
                //Parameter[0] = objSalaryscale.grp_key;
                //Parameter[1] = objSalaryscale.grp_desc;
                //Parameter[2] = objSalaryscale.Rowid;
                //object c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOAccountGroups), true);
                //if (c != null)
                //{
                //    if (c.ToString() == "1")
                //    {
                //        int i = BLLAccountGroups.UpdateInfoDetailAGroups(ref objTransaction, ref lstgetAccgrps);
                //        if (i == 1)
                //        {
                //            if (!statusObjTransaction)
                //                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //        }
                //        else
                //        {
                //            if (!statusObjTransaction)
                //                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //            return 0;
                //        }
                //    }
                //    else
                //    {
                //        if (!statusObjTransaction)
                //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //        return 0;
                //    }
                //}
                //else
                //{
                //    if (!statusObjTransaction)
                //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //    return 0;
                //}
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 1;








        }

        private static int UpdateDetailInfo(ref DVOSalaryCategory  objDVOSalary, ref object objTransaction, ref List<DVOSalaryScale> lstDVOSalaryScale)
        {
            DVOSalaryScale objDVOSalaryScale = new DVOSalaryScale();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool transObjectStatus = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                transObjectStatus = false;
            }
            try
            {
                //if (lstDVOSalaryScale.Count > 0)
                //{
                    object[] parameters = new object[1];
                    parameters[0] = objDVOSalary.Code; //lstDVOSalaryScale[0].First_code;
                    //Deleteting

                    object c = objDalBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSalaryScale), objDVOSalaryScale.DELETE_SALARY_DETAIL_BY_CODE);
                   // if (c == null)
                    if (c != null)
                    {
                        if (c.ToString() == "1")
                        {
                            //Inserting
                           
                                int i = InsertDtlInfo(ref objTransaction, ref lstDVOSalaryScale);
                                if (i == 1)
                                {
                                    //objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), obj.uspAccGrpsDtlupd);

                                    if (!transObjectStatus)
                                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                    return 1;
                                }
                                else
                                {
                                    if (!transObjectStatus)
                                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    return 0;
                                }
                            
                            //else
                            //{
                            //    if (!transObjectStatus)
                            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            //    return 0;
                            //}
                    }
                    else
                    {
                        if (!transObjectStatus)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {

                //ExceptionManagement.ExceptionManager.Publish(ex);
                //if (!transObjectStatus)
                //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //throw ex;
                if (!transObjectStatus)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManager.Publish(ex);
                    return 0;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                    return 0;
                }
            }
            return 1;

            
        }

        private static int InsertDtlInfo(ref object objTransaction, ref List<DVOSalaryScale> lstDVOSalaryScale)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool transObjectStatus = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                transObjectStatus = false;
            }
            try
            {

                foreach (DVOSalaryScale  obj in lstDVOSalaryScale)
                {
                   
                    object[] parameters = new object[2];
                    parameters[0] = obj.Code;
                    parameters[1] = obj.Scale_code;

                    object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SALARY_DETAIL_BY_CODE, true);
                    if (c == null)
                        throw new Exception();
                    else if (Convert.ToInt32(c) < 1)
                        throw new Exception();
                    if (c != null)
                    {
                        if (c.ToString() != "1")
                        {
                            return 0;
                        }
                    }
                    else { return 0; }
                }
                if (!transObjectStatus)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!transObjectStatus)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return 0;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                    return 0;
                }
            }
            return 1;



        }
    }
}