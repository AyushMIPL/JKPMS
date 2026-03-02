using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    public class BLLAPVenTerms
    {
        /// <summary>
        /// This method is use to get Vendor terms Terms from database 
        /// </summary>
        /// <param name="objVendorTerms">reference of DVOAPVendorTerms type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having approval classes Terms</returns>
        public static List<DVOAPVendorTerms> GetVendorTerms(ref DVOAPVendorTerms objVendorTerms)
        {
            object[] parameters = new object[6];
            parameters[0] = objVendorTerms.terms_code;
            parameters[1] = objVendorTerms.terms_desc;
            parameters[2] = objVendorTerms.due_days;
            parameters[3] = objVendorTerms.disc_days;
            parameters[4] = objVendorTerms.disc_pct;
            parameters[5] = objVendorTerms.rowid;

            List<DVOAPVendorTerms> objVendorTermslist = new List<DVOAPVendorTerms>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, (typeof(DVOAPVendorTerms))))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOAPVendorTerms objAPVendorTerms = new DVOAPVendorTerms();
                    objAPVendorTerms.terms_code = dr[0].ToString().Trim();//terms_code
                    objAPVendorTerms.terms_desc = dr[1].ToString().Trim();//terms_desc
                    objAPVendorTerms.due_days = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);//due_days
                    objAPVendorTerms.disc_days = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0); ;//disc_days
                    objAPVendorTerms.disc_pct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0); ;//disc_pct
                    objAPVendorTerms.rowid = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0); ;//rowid
                    objVendorTermslist.Add(objAPVendorTerms);
                }
            }
            return objVendorTermslist;
        }
        /// <summary>
        /// This method is use to insert new vendor Terms into database
        /// </summary>
        /// <param name="objVendorTerms">reference of DVOAPVendorTerms type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static int InsertVendorTerms(ref DVOAPVendorTerms objVendorTerms)
        {
            object[] parameters = new object[5];
            parameters[0] = objVendorTerms.terms_code.Trim();
            parameters[1] = objVendorTerms.terms_desc.Trim();
            parameters[2] = objVendorTerms.due_days;
            parameters[3] = objVendorTerms.disc_days;
            parameters[4] = objVendorTerms.disc_pct;
            List<DVOAPVendorTerms> objAPVendorTerms = new List<DVOAPVendorTerms>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOAPVendorTerms));
            return c;

        }
        //Commented By Sunil Pahwa**************[28-01-09]************************************
        /// <summary>
        /// This method is use to update vendor Terms into database
        /// </summary>
        /// <param name="objVendorTerms">reference of DVOAPVendorTerms type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        //public static int UpdateVendorTerms(ref DVOAPVendorTerms objVendorTerms)
        //{
        //    object[] parameters = new object[6];
        //    parameters[0] = objVendorTerms.terms_code;
        //    parameters[1] = objVendorTerms.terms_desc;
        //    parameters[2] = objVendorTerms.due_days;
        //    parameters[3] = objVendorTerms.disc_days;
        //    parameters[4] = objVendorTerms.disc_pct;
        //    parameters[5] = objVendorTerms.rowid;
        //    List<DVOAPVendorTerms> objSecAccountTypeList = new List<DVOAPVendorTerms>();
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOAPVendorTerms));
        //    return c;
        //}

        /// <summary>
        /// This method is use to delete vendor Terms from database
        /// </summary>
        /// <param name="objVendorTerms">reference of DVOAPVendorTerms type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is deleted or not</returns>
        //***************************************************************************************





        public static int DeleteVendorTerms(ref DVOAPVendorTerms objVendorTerms)
        {
            object[] parameters = new object[1];
            parameters[0] = objVendorTerms.rowid;

            List<DVOAPVendorTerms> objAPVendorTermsList = new List<DVOAPVendorTerms>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOAPVendorTerms));
            return c;
        }
        //********************************************************************************
        //********Added by Sunil Pahwa**************[28-01-09]****************************
        //********************************************************************************
        public static object UpdateVendorTermsInfo(ref object objTransaction, ref DVOAPVendorTerms objVendorTerms)
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
                object[] parameters = new object[6];
                parameters[0] = objVendorTerms.terms_code;
                parameters[1] = objVendorTerms.terms_desc;
                parameters[2] = objVendorTerms.due_days;
                parameters[3] = objVendorTerms.disc_days;
                parameters[4] = objVendorTerms.disc_pct;
                parameters[5] = objVendorTerms.rowid;
             

                int c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAPVendorTerms));
               
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;

        }
        //***************************************************************************************************
    }
}
