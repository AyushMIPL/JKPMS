using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;




namespace JKPS.BLL
{
    
    public class BLLSecCompany
    {
        public static int InsertCompanyInfo(ref DVOSecCompany objDvosecComp, ref List<DVOSecCompany> listDVOSecCompany)
        {
                object objTransaction = null;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] Parameters = new object[8];
                Parameters[0] = objDvosecComp.co_name;
                Parameters[1] = objDvosecComp.addr1;
                Parameters[2] = objDvosecComp.addr2;
                Parameters[3] = objDvosecComp.city;
                Parameters[4] = objDvosecComp.state;
                Parameters[5] = objDvosecComp.zip;
                Parameters[6] = objDvosecComp.county;
                Parameters[7] = objDvosecComp.country;


                object c = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref Parameters, typeof(DVOSecCompany));
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt32(c) < 1)
                    throw new Exception();
               // object Ddetail = BLLSecCompany.InsertDetail(ref objTransaction, ref listDVOSecCompany);
                if ((objTransaction != null) &&(Convert.ToInt32(c) >1))
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //return Convert.ToInt32(Ddetail);
                //int c = objDalBaseClass.InsertData(ref Parameters, typeof(DVOSecCompany));
                if (Convert.ToInt32(c) < 1)
                    c = 0;
                return Convert.ToInt32(c);
                    
              
            }
            catch(Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                throw ex;
            }
        }

        public static List<DVOSecCompany> GetCompanyInfo(ref DVOSecCompany objDvosecCompanyupper)
        {
            object[] Parameters = new object[9];
            Parameters[0] = objDvosecCompanyupper.Company_id;
            Parameters[1] = objDvosecCompanyupper.co_name;
            Parameters[2] = objDvosecCompanyupper.addr1;
            Parameters[3] = objDvosecCompanyupper.addr2;
            Parameters[4] = objDvosecCompanyupper.city;
            Parameters[5] = objDvosecCompanyupper.state;
            Parameters[6] = objDvosecCompanyupper.zip;
            Parameters[7] = objDvosecCompanyupper.county;
            Parameters[8] = objDvosecCompanyupper.country;
            

            List<DVOSecCompany> objseccompanylist = new List<DVOSecCompany>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOSecCompany)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objDvosecCompanyupper.Company_id = (dr["Company_ID"] != DBNull.Value ? Convert.ToInt32(dr["Company_ID"]) : 0);
                    objDvosecCompanyupper.co_name = (dr["company_name"] != DBNull.Value ? dr["company_name"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.addr1 = (dr["Address1"] != DBNull.Value ? dr["Address1"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.addr2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.city = (dr["City"] != DBNull.Value ? dr["City"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.state = (dr["State"] != DBNull.Value ? dr["State"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.zip = (dr["zip"] != DBNull.Value ? dr["Zip"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.county = (dr["County"] != DBNull.Value ? dr["County"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.country = (dr["Country"] != DBNull.Value ? dr["Country"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.mtax = (dr["Mtax"] != DBNull.Value ? dr["Mtax"].ToString().Trim() : string.Empty);
                    objDvosecCompanyupper.use_mtax_grps = (dr["use_mtax_grps"] != DBNull.Value ? dr["use_mtax_grps"].ToString().Trim() : string.Empty);
                    objseccompanylist.Add(objDvosecCompanyupper);
                }

            }
            return objseccompanylist;
        }

        public static List<DVOSecCompany> GetDetailInfo(ref  DVOSecCompany obj1)
        {
            object[] Parameters = new object[1];
            Parameters[0] = obj1.src_type;
            List<DVOSecCompany> objseccompanylist = new List<DVOSecCompany>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //using (DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOSecCompany), (new DVOSecCompany()).uspCompDetail))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOSecCompany obj = new DVOSecCompany();
            //        obj.src_key = (dr[0]!=DBNull.Value ? dr[0].ToString().Trim():string.Empty);
            //        obj.src_desc = (dr[1]!=DBNull.Value ?dr[1].ToString().Trim():string.Empty);
            //        obj.Company_id = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
            //        objseccompanylist.Add(obj);
            //    }
            //}
            return objseccompanylist;
        }
        //public static int UpdateCompanyInfo(ref DVOSecCompany objseccompany, ref List<DVOSecCompany> objcompinfolist)
        //{
        //    List<DVOSecCompany> lst = new List<DVOSecCompany>();
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    //DVOSecCompany objsecCompany = new DVOSecCompany();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    try
        //    {
        //        object[] Parameter = new object[10];
        //        Parameter[0] = objseccompany.co_name;
        //        Parameter[1] = objseccompany.addr1;
        //        Parameter[2] = objseccompany.addr2;
        //        Parameter[3] = objseccompany.city;
        //        Parameter[4] = objseccompany.state;
        //        Parameter[5] = objseccompany.zip;
        //        Parameter[6] = objseccompany.county;
        //        Parameter[7] = objseccompany.country;
        //        Parameter[8] = objseccompany.mtax;
        //        Parameter[9] = objseccompany.use_mtax_grps;

        //        int c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOSecCompany ));

        //        BLLSecCompany.UpdateInfoDetail(ref objTransaction, ref objcompinfolist);
        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManager.Publish(ex);
        //        return 0;
        //    }
        //    return 1;
        //}
        //private static int UpdateInfoDetail(ref object objTransaction, ref List<DVOSecCompany> listobjDetail)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    object success = null;
        //    try
        //    {
        //        foreach (DVOSecCompany obj in listobjDetail)
        //        {
        //            object[] parameters = new object[3];
        //            parameters[0] = obj.src_type;
        //            parameters[1] = obj.src_key;
        //            parameters[2] = obj.src_desc;

        //            success = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecCompany), (new DVOSecCompany().uspCompDetailupd));
        //            if (success == null)
        //                throw new Exception();
        //            else if (Convert.ToInt32(success) < 1)
        //                throw new Exception();
        //        }
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction)
        //        {
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            return 0;
        //        }
        //        else
        //        {
        //            ExceptionManager.Publish(ex);
        //            throw ex;
        //        }
        //    }
        //    return 1;
        //    bool transObjectStatus = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        transObjectStatus = false;
        //    }
        //    try
        //    {
        //        foreach (DVOSecCompany obj in listobjDetail)
        //        {
        //            object[] parameters = new object[3];
        //            parameters[0] = obj.src_type;
        //            parameters[1] = obj.src_key;
        //            parameters[2] = obj.src_desc;

        //            objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecCompany), (new DVOSecCompany().uspCompDetailupd));
        //        }
        //        if (!transObjectStatus)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!transObjectStatus)
        //        {
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            return 0;
        //        }
        //        else
        //        {
        //            ExceptionManager.Publish(ex);
        //            throw ex;
        //        }
        //    }
        //    return 1;
        //}
        //public static int UpdateCompanyInformation(ref object objTransaction, ref DVOSecCompany objseccompany, ref List<DVOSecCompany> listobjDetail)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object i = null;
        //    try
        //    {
        //        object[] Parameter = new object[11];
        //        Parameter[0] = objseccompany.co_name;
        //        Parameter[1] = objseccompany.addr1;
        //        Parameter[2] = objseccompany.addr2;
        //        Parameter[3] = objseccompany.city;
        //        Parameter[4] = objseccompany.state;
        //        Parameter[5] = objseccompany.zip;
        //        Parameter[6] = objseccompany.county;
        //        Parameter[7] = objseccompany.country;
        //        Parameter[8] = objseccompany.mtax;
        //        Parameter[9] = objseccompany.use_mtax_grps;
        //        Parameter[10] = objseccompany.rowid;

        //        i = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref Parameter, typeof(DVOSecCompany), true);
        //        if (i == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(i) < 1)
        //            throw new Exception();

        //        Parameter = null;
        //        UpdateInfoDetail(ref objTransaction, ref listobjDetail);
        //        objDALBaseClass = null;
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return Convert.ToInt32(i);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        throw ex;
        //    }
        //    return Convert.ToInt32(i);

        //}

        public static object UpdateDataInfo(ref object objTransaction, ref DVOSecCompany objseccompany,
            ref List<DVOSecCompany> listNewDVOSecCompany, ref List<DVOSecCompany> listUpdateDVOSecCompany, ref List<DVOSecCompany> listDeleteDVOSecCompany)
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
                object[] Parameter = new object[9];
                Parameter[1] = objseccompany.co_name;
                Parameter[2] = objseccompany.addr1;
                Parameter[3] = objseccompany.addr2;
                Parameter[4] = objseccompany.city;
                Parameter[5] = objseccompany.state;
                Parameter[6] = objseccompany.zip;
                Parameter[7] = objseccompany.county;
                Parameter[8] = objseccompany.country;
               
                Parameter[0] = objseccompany.Company_id;

                i = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref Parameter, typeof(DVOSecCompany));
                if (i == null)
                    throw new Exception();
                else if (Convert.ToInt32(i) < 1)
                    throw new Exception();
                else if (Convert.ToInt32(i) >= 1)
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    statusObjTransaction = true;
                }
                //    //First delete the data from database
                //    //BLLPRUpdateTaxTables.DeleteDetail(ref objPRUpdateTaxTables);
                //    //then Insert again record by record
                //    //if (listNewDVOSecCompany.Count > 0)
                //    //    BLLSecCompany.InsertDetail(ref objTransaction, ref listNewDVOSecCompany);
                //    //if (listDeleteDVOSecCompany.Count > 0)
                //    //    BLLSecCompany.DetailDetailByRowId(ref objTransaction, ref listDeleteDVOSecCompany);
                //    //if (listUpdateDVOSecCompany.Count > 0)
                //    //    BLLSecCompany.UpdateDetail(ref objTransaction, ref listUpdateDVOSecCompany);
                //}

                Parameter = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
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


        private static object  UpdateDetail(ref object objTransaction, ref List<DVOSecCompany> listUpdateDVOSecCompany)
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
                foreach (DVOSecCompany objUPDDVOSecCompany in listUpdateDVOSecCompany)
                {
                    object[] parameters = new object[4];
                   
                    parameters[0] = objUPDDVOSecCompany.src_key;
                    parameters[1] = objUPDDVOSecCompany.src_desc;
                    parameters[2] = objUPDDVOSecCompany.Company_id;
                    parameters[3] = objUPDDVOSecCompany.src_type;
                    obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecCompany), objUPDDVOSecCompany.UPDATE_COMPANY_DETAIL);
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

        private static object  DetailDetailByRowId(ref object objTransaction, ref List<DVOSecCompany> listDeleteDVOSecCompany)
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
                foreach (DVOSecCompany objDVOSecCompany in listDeleteDVOSecCompany)
                {
                    object[] parameters = new object[2];
                    parameters[0] = objDVOSecCompany.Company_id;
                    parameters[1] = objDVOSecCompany.src_type;
                  
                    //Set order number on the basis of pay_period

                    obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecCompany), objDVOSecCompany.DELETE_COMPANY_DETAIL);
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

        private static object  InsertDetail(ref object objTransaction, ref List<DVOSecCompany> listNewDVOSecCompany)
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
                foreach (DVOSecCompany objDVOSecCompany in listNewDVOSecCompany)
                {
                    object[] parameters = new object[3];
                    parameters[0] = objDVOSecCompany.src_type; 
                    parameters[1] = objDVOSecCompany.src_key;
                    parameters[2] = objDVOSecCompany.src_desc;
                    //Set order number on the basis of pay_period



                    obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecCompany), objDVOSecCompany.INSERT_COMPANY_DETAIL);
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
    }
}
