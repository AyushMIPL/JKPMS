using System;
using System.Collections.Generic;
using System.Text;
using JKPS.CommonUtilities;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;
using ExceptionManagement;
namespace JKPS.BLL
{
   public  class BLLUpdateVoidChecks
    {
        public static List<DVOUpdateVoidChecks> GetDetailInfo(ref DVOUpdateVoidChecks objDvoVoidChk)
        {
            List<DVOUpdateVoidChecks> lstDvoUpdVChk = new List<DVOUpdateVoidChecks>();
            object[] Parameter = new object[5];

            Parameter[0] = objDvoVoidChk.chk_doc_no;
            Parameter[1] = objDvoVoidChk.posted;
            Parameter[2] = objDvoVoidChk.doc_no;
            Parameter[3] = objDvoVoidChk.void_date;
            Parameter[4] = objDvoVoidChk.rowid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateSalaryPositions), (new DVOUpdateSalaryPositions()).uspDfltSalScgetall))
            //using(DataSet ds= objDalBaseClass.GetData(ref Parameter, typeof(DVOUpdateVoidChecks),(new DVOUpdateVoidChecks()).GET_VOID_CHK_HEAD ))
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOUpdateVoidChecks)))
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks obj = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) obj.chk_doc_no  = Convert.ToInt32(dr[0].ToString().Trim());
                  //  if (!Convert.IsDBNull(dr[1])) obj.chk_doc_no = Convert.ToInt32(dr[1].ToString());
                    if (!Convert.IsDBNull(dr[1])) obj.doc_no = Convert.ToInt32(dr[1].ToString());
                    if (!Convert.IsDBNull(dr[2])) obj.void_date  = Convert.ToString (dr[2].ToString());
                    if (!Convert.IsDBNull(dr[3])) obj.posted = dr[3].ToString();
                    if (!Convert.IsDBNull(dr[4])) obj.ok_to_posted = dr[4].ToString();
                    if (!Convert.IsDBNull(dr[5])) obj.rowid  =Convert.ToInt32 ( dr[5].ToString());
                    lstDvoUpdVChk.Add(obj);
                }
            }
            return lstDvoUpdVChk;
            
        }

        public static List<DVOUpdateVoidChecks> GetVoidChkInfo(ref DVOUpdateVoidChecks searcgDvoVoidChecks)
        {
            List<DVOUpdateVoidChecks> lstDvoUpdVCheck = new List<DVOUpdateVoidChecks>();
            object[] Parameter = new object[5];
            Parameter[0] = searcgDvoVoidChecks.chk_doc_no;
            Parameter[1] = searcgDvoVoidChecks.posted;
            Parameter[2] = searcgDvoVoidChecks.doc_no;
            Parameter[3] = searcgDvoVoidChecks.void_date;
            Parameter[4] = searcgDvoVoidChecks.rowid;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOUpdateVoidChecks)))
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks obj1 = new DVOUpdateVoidChecks();
                 
                    if (!Convert.IsDBNull(dr[0])) obj1.chk_doc_no = Convert.ToInt32(dr[0].ToString());
                    if (!Convert.IsDBNull(dr[1])) obj1.doc_no = Convert.ToInt32(dr[1].ToString());
                    if (!Convert.IsDBNull(dr[2])) obj1.void_date = Convert.ToString (dr[2].ToString());

                    if (!Convert.IsDBNull(dr[3])) obj1.posted = dr[3].ToString();
                    if (!Convert.IsDBNull(dr[4])) obj1.ok_to_posted = dr[4].ToString();
                    if (!Convert.IsDBNull(dr[5])) obj1.rowid =  Convert.ToInt32(dr[5].ToString());
                    lstDvoUpdVCheck.Add(obj1);
                }

            }
            return lstDvoUpdVCheck;
        }

        public static List<DVOUpdateVoidChecks> GetDetailInformation(ref DVOUpdateVoidChecks obj)
        {
            object[] parameter = new object[1];
            parameter[0] = obj.doc_no;
            List<DVOUpdateVoidChecks> lstDvoUpdVCk = new List<DVOUpdateVoidChecks>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          
            using (DataSet ds = objDalBaseClass.GetData(ref parameter , typeof(DVOUpdateVoidChecks), (new DVOUpdateVoidChecks()).GET_VOID_CHK_DETAIL ))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks ob = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) ob.doc_date  =dr[0].ToString().Trim();
                    if (!Convert.IsDBNull(dr[1])) ob.doc_desc  =dr[1].ToString();
                    if (!Convert.IsDBNull(dr[2])) ob.post_no   = Convert.ToInt32(dr[2].ToString());
                    if (!Convert.IsDBNull(dr[3])) ob.post_date  = dr[3].ToString();
                    if (!Convert.IsDBNull(dr[4])) ob.ref_code  = dr[4].ToString();
                    lstDvoUpdVCk.Add(ob);
                }

            }
            return lstDvoUpdVCk;
        }

        public static List<DVOUpdateVoidChecks> GetDetailInformation1(ref DVOUpdateVoidChecks obj)
        {
            object[] parameter = new object[1];
            parameter[0] = obj.doc_no;
            List<DVOUpdateVoidChecks> lstDvoUpdVCk = new List<DVOUpdateVoidChecks>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           
            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateVoidChecks), (new DVOUpdateVoidChecks()).GET_VOID_CHK_DETAIL1))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks ob = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) ob.check_no = dr[0].ToString().Trim();
                    if (!Convert.IsDBNull(dr[1])) ob.amt = Convert.ToDecimal(dr[1].ToString());

                    lstDvoUpdVCk.Add(ob);
                }
            }
            return lstDvoUpdVCk;
        }

       public static int InsVoidChkInfo(ref DVOUpdateVoidChecks objDvoUpdCHkIns)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           try
           {
               bool status = false;
               object[] InsParameter = new object[4];
               InsParameter[0] = objDvoUpdCHkIns.chk_doc_no;
               //InsParameter[1] = objDvoUpdCHkIns.doc_no;
               InsParameter[1] = objDvoUpdCHkIns.void_date;
               InsParameter[2] = objDvoUpdCHkIns.posted;
               InsParameter[3] = objDvoUpdCHkIns.ok_to_posted;
               object c = objDalBaseClass.ExecuteScalar(ref InsParameter, objDvoUpdCHkIns.INSERT_SPNAME);
               InsParameter = null;
               objDalBaseClass = null;
               if (Convert.ToInt16(c) == 1)
               {
                   return 1;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               System.Windows.Forms.MessageBox.Show(ex.Message);
           }
           return 0;
       }

       public static List<DVOUpdateVoidChecks> GetDetailInfo1(ref DVOUpdateVoidChecks objDvoVoidChk)
        {

            object[] parameter = new object[1];
            parameter[0] = objDvoVoidChk.chk_doc_no; 


            List<DVOUpdateVoidChecks> lstDvo = new List<DVOUpdateVoidChecks>();


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         
            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateVoidChecks), (new DVOUpdateVoidChecks()).FIND_VOID_CHK_HEAD))
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks ob = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) ob.doc_no  =Convert.ToInt32 ( dr[0].ToString().Trim());
                
                    lstDvo.Add(ob);
                }

            }
            return lstDvo;
           
        }

        public static List<DVOUpdateVoidChecks> GetDetailInfo2(ref DVOUpdateVoidChecks objDvoVoidChk)
        {

            object[] parameter = new object[1];
            parameter[0] = objDvoVoidChk.chk_doc_no;


            List<DVOUpdateVoidChecks> lstDvo = new List<DVOUpdateVoidChecks>();


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        
            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateVoidChecks), (new DVOUpdateVoidChecks()).FIND_VOID_CHK_HEAD1))
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks ob = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) ob.doc_no  =Convert.ToInt32 ( dr[0].ToString().Trim());
                
                    lstDvo.Add(ob);
                }

            }
            return lstDvo;
           
        }

        public static List<DVOUpdateVoidChecks> GetDetailVendorInfo(ref DVOUpdateVoidChecks obj)
        {

            object[] parameter = new object[1];
            parameter[0] = obj.doc_no;


            List<DVOUpdateVoidChecks> lstDvoUpdVCk = new List<DVOUpdateVoidChecks>();


            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         
            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateVoidChecks), (new DVOUpdateVoidChecks()).GET_VOID_CHK_VENDER_DETAIL))
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateVoidChecks ob = new DVOUpdateVoidChecks();
                    if (!Convert.IsDBNull(dr[0])) ob.vend_code  = dr[0].ToString().Trim();
                    if (!Convert.IsDBNull(dr[1])) ob.bus_name  = dr[1].ToString();
                 
                    lstDvoUpdVCk.Add(ob);
                }

            }
            return lstDvoUpdVCk;

           
        }

       public static int DeleteVoidChkInfo(ref object objTransaction, ref DVOUpdateVoidChecks objDvoUpdDel)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object k = null;
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] parameter = new object[1];
                parameter[0] = objDvoUpdDel.rowid;


                k = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameter, typeof(DVOUpdateVoidChecks), objDvoUpdDel.UPDATE_WHEN_DELETE);
                     //objDalBaseClass.UpdateData_ByTransaction(ref objTransaction,ref  parameter,objDvoUpdDel.UPDATE_WHEN_DELETE, true);
                if (k == null)
                    throw new Exception();
                else if (Convert.ToInt16(k) < 1)
                    throw new Exception();

                parameter  = null;
                objDalBaseClass = null;

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

       public static int  UpdateVoidChkInfo(ref object objTransaction, ref DVOUpdateVoidChecks objDvoVoidChkUpd)
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
                object[] Parameter = new object[4];
                Parameter[0] = objDvoVoidChkUpd.chk_doc_no;
                Parameter[1] = objDvoVoidChkUpd.void_date;
                Parameter[2] = objDvoVoidChkUpd.rowid;
                Parameter[3] = objDvoVoidChkUpd.doc_no;
                
                
                object c = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref Parameter, objDvoVoidChkUpd.UPDATE_SPNAME, true);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();
                Parameter = null;
                objDALBaseClass = null;

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

       public static int GetDocNo(int ckeckNo)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           DVOUpdateVoidChecks obj=new DVOUpdateVoidChecks();
           int doc_no = 0;
           try
           {
               object[] parameters = new object[1];
               parameters[0] = ckeckNo;
               object o = objDalBaseClass.ExecuteScalar(ref parameters, obj.GETDOCNO);
               if (o == null || o.ToString().Trim().Length <= 0)
               {
                   doc_no = 0;
               }
               else
               {
                   doc_no = Convert.ToInt32(o);
               }
           }
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;
           }
           return doc_no;
       }

       public static string GetVendName(string vendCode)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           DVOUpdateVoidChecks obj = new DVOUpdateVoidChecks();
           string VEND_NAME = string.Empty;
           try
           {
               object[] parameters = new object[1];
               parameters[0] = vendCode;
               object o = objDalBaseClass.ExecuteScalar(ref parameters, obj.GET_VEND_NAME);
               VEND_NAME = Convert.ToString(VEND_NAME);
           }
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;
           }
           return VEND_NAME;
       }
    }
}
