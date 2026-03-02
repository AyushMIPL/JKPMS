using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.DL;
using JKPS.COMMON;


namespace JKPS.BLL
{
    public  class BLLUpdatePayableDefaults
    {
        public static List<DVOUpdatePayableDefaults> GetAllData()
        {
            List<DVOUpdatePayableDefaults> lstPayDefaults = new List<DVOUpdatePayableDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOUpdatePayableDefaults)))
           {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayableDefaults objPayDefaults = new DVOUpdatePayableDefaults();
                    objPayDefaults.terms_code = Convert.ToString(dr[0]).Trim();//"p_terms_code"
                    objPayDefaults.terms_desc = Convert.ToString(dr[1]).Trim();//"p_terms_desc
                    objPayDefaults.due_days =Convert.ToInt32 ( dr[2]);//"p_due_days"
                    objPayDefaults.disc_days =Convert.ToInt32 ( dr[3]);//"p_disc_days"
                    objPayDefaults.disc_pct = Convert.ToDecimal(dr[4]);//"p_disc_pct"


                    lstPayDefaults.Add(objPayDefaults);

                 
               }
              return lstPayDefaults;
           }
        }

        public static List<DvoUpdatePayableDefDetails> GetAllInfo()
        {
            List<DvoUpdatePayableDefDetails> DVoUpdPayDeflst = new List<DvoUpdatePayableDefDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DvoUpdatePayableDefDetails )))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DvoUpdatePayableDefDetails obj = new DvoUpdatePayableDefDetails();
                    obj.term_code = dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty;
                    obj.ap_accounttypeid = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                    obj.ap_accounttype = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
                    obj.ap_accountdesc = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                    obj.ap_keyvalue = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;

                    obj.ap_acct_no = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
                   // obj.ap_account = Convert.ToInt32(dr[5]);
                    obj.cd_accounttypeid = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
                    obj.cd_accounttype = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    obj.cd_accountdesc = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                    obj.cd_keyvalue = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;

                    obj.cd_cash_acct_no = dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0;
                    obj.cd_disc_acctypeid = dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0;
                    obj.cd_disc_acctype = dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty;
                    obj.cd_disc_accdesc = dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty;
                    obj.cd_disc_keyvalue = dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty;

                    obj.cd_disc_acct_no = dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0;
                    obj.ap_doc_no = dr[16] != DBNull.Value ? Convert.ToInt32(dr[16]) : 0;
                    obj.ap_post_no = dr[17] != DBNull.Value ? Convert.ToInt32(dr[17]) : 0;
                    obj.ap_balanced = dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty;
                    obj.cd_doc_no = dr[19] != DBNull.Value ? Convert.ToInt32(dr[19]) : 0;

                    obj.cd_post_no = dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0;
                    obj.age_datetype = dr[21] != DBNull.Value ? dr[21].ToString().Trim() : string.Empty;
                    obj.age_per1 = dr[22] != DBNull.Value ? Convert.ToInt32(dr[22]) : 0;
                    obj.age_per2 = dr[23] != DBNull.Value ? Convert.ToInt32(dr[23]) : 0;
                    obj.age_per3 = dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0;

                    obj.age_dsc1 = dr[25] != DBNull.Value ? dr[25].ToString().Trim() : string.Empty;
                    obj.age_dsc2 = dr[26] != DBNull.Value ? dr[26].ToString().Trim() : string.Empty;
                    obj.age_dsc3 = dr[27] != DBNull.Value ? dr[27].ToString().Trim() : string.Empty;
                    obj.age_dsc4 = dr[28] != DBNull.Value ? dr[28].ToString().Trim() : string.Empty;
                    obj.federal_tax_id = dr[29] != DBNull.Value ? dr[29].ToString().Trim() : string.Empty;

                    obj.dflt_1099 = dr[30] != DBNull.Value ? dr[30].ToString().Trim() : string.Empty;
                    obj.def_mtaxcd = dr[31] != DBNull.Value ? dr[31].ToString().Trim() : string.Empty;
                    obj.gross_entry = dr[32] != DBNull.Value ? dr[32].ToString().Trim() : string.Empty;
                    obj.mtax_dsc = dr[33] != DBNull.Value ? dr[33].ToString().Trim() : string.Empty;
                    obj.entry_by_line = dr[34] != DBNull.Value ? dr[34].ToString().Trim() : string.Empty;

                    obj.disc_frght = dr[35] != DBNull.Value ? dr[35].ToString().Trim() : string.Empty;
                    obj.disc_tax = dr[36] != DBNull.Value ? dr[36].ToString().Trim() : string.Empty;
                    obj.use_batch_inv = dr[37] != DBNull.Value ? dr[37].ToString().Trim() : string.Empty;
                    obj.use_batch_pay = dr[38] != DBNull.Value ? dr[38].ToString().Trim() : string.Empty;
                    obj.use_approv_post = dr[39] != DBNull.Value ? dr[39].ToString().Trim() : string.Empty;

                    obj.approval_code = dr[40] != DBNull.Value ? dr[40].ToString().Trim() : string.Empty;
                    obj.auto_chkno = dr[41] != DBNull.Value ? dr[41].ToString().Trim() : string.Empty;
                    obj.last_chkno = dr[42] != DBNull.Value ? Convert.ToInt32(dr[42]) : 0;//"p_last_chkno"
                    obj.RowId = dr[44] != DBNull.Value ? Convert.ToInt32(dr[44]) : 0;

                  //  obj.cd_account = Convert.ToInt32 (dr[9]);
                     //   obj.ap_acct_no =Convert.ToInt32 (dr[1]);
                    DVoUpdPayDeflst.Add(obj);
                }
                return DVoUpdPayDeflst;
            }
        }
        //public static int UpdatePayableDefDtl(ref DvoUpdatePayableDefDetails objPayDefDtlUpd)
        //{



        //    object[] UpdateParameter = new object[25];

        //    UpdateParameter[0] = objPayDefDtlUpd.term_code;
        //    UpdateParameter[1] = objPayDefDtlUpd.ap_acct_no;
        //    UpdateParameter[2] = objPayDefDtlUpd.cd_cash_acct_no;
        //    UpdateParameter[3] = objPayDefDtlUpd.cd_disc_acct_no;
        //    UpdateParameter[4] = objPayDefDtlUpd.def_mtaxcd;
        //    UpdateParameter[5] = objPayDefDtlUpd.entry_by_line;
        //    UpdateParameter[6] = objPayDefDtlUpd.gross_entry;
        //    UpdateParameter[7] = objPayDefDtlUpd.mtax_dsc ;
        //    UpdateParameter[8] = objPayDefDtlUpd.ap_balanced;
        //    UpdateParameter[9] = objPayDefDtlUpd.age_datetype;
        //    UpdateParameter[10] = objPayDefDtlUpd.age_per1 ;
        //    UpdateParameter[11] = objPayDefDtlUpd.age_per2;
        //    UpdateParameter[12] = objPayDefDtlUpd.age_per3;
        //    UpdateParameter[13] = objPayDefDtlUpd.age_dsc1;
        //    UpdateParameter[14] = objPayDefDtlUpd.age_dsc2;
        //    UpdateParameter[15] = objPayDefDtlUpd.age_dsc3;
        //    UpdateParameter[16] = objPayDefDtlUpd.age_dsc4;
        //    UpdateParameter[17] = objPayDefDtlUpd.federal_tax_id;
        //    UpdateParameter[18] = objPayDefDtlUpd.dflt_1099;
        //    UpdateParameter[19] = objPayDefDtlUpd.use_batch_inv;
        //    UpdateParameter[20] = objPayDefDtlUpd.use_batch_pay;
        //    UpdateParameter[21] = objPayDefDtlUpd.use_approv_post;
        //    UpdateParameter[22] = objPayDefDtlUpd.approval_code ;
        //    UpdateParameter[23] = objPayDefDtlUpd.auto_chkno ;
        //    UpdateParameter[24] = objPayDefDtlUpd.last_chkno  ;
            
            

        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    int c=objDalBaseClass.UpdateData(ref UpdateParameter,typeof(DVOUpdatePayableDefaults));
        //    return c;

        //}



        public static List<DvoUpdatePayableDefDetails> GetData(ref DvoUpdatePayableDefDetails objDvoUpdatePay)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DvoUpdatePayableDefDetails> listDVOupdatePayDefln = new List<DvoUpdatePayableDefDetails>();
           try
            {
                object[] parameters = new object[1];
                parameters[0] = objDvoUpdatePay.RowId;
             

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DvoUpdatePayableDefDetails  )))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DvoUpdatePayableDefDetails obj=new DvoUpdatePayableDefDetails())
                                {

                                    obj.RowId = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//
                                    obj.term_code  = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//
                            
                                    listDVOupdatePayDefln.Add(obj);
                                }
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOupdatePayDefln;
           

        }

        public static int UpdatePayableDefDtlInfo(ref object objTransaction, ref DvoUpdatePayableDefDetails objPayDefDtlUpd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            } 
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object success = null;

            try
            {
                object[] UpdateParameter = new object[25];

                UpdateParameter[0] = objPayDefDtlUpd.term_code;
                UpdateParameter[1] = objPayDefDtlUpd.ap_acct_no;
                UpdateParameter[2] = objPayDefDtlUpd.cd_cash_acct_no;
                UpdateParameter[3] = objPayDefDtlUpd.cd_disc_acct_no;
                UpdateParameter[4] = objPayDefDtlUpd.def_mtaxcd;
                UpdateParameter[5] = objPayDefDtlUpd.entry_by_line;
                UpdateParameter[6] = objPayDefDtlUpd.gross_entry;
                UpdateParameter[7] = objPayDefDtlUpd.mtax_dsc;
                UpdateParameter[8] = objPayDefDtlUpd.ap_balanced;
                UpdateParameter[9] = objPayDefDtlUpd.age_datetype;
                UpdateParameter[10] = objPayDefDtlUpd.age_per1;
                UpdateParameter[11] = objPayDefDtlUpd.age_per2;
                UpdateParameter[12] = objPayDefDtlUpd.age_per3;
                UpdateParameter[13] = objPayDefDtlUpd.age_dsc1;
                UpdateParameter[14] = objPayDefDtlUpd.age_dsc2;
                UpdateParameter[15] = objPayDefDtlUpd.age_dsc3;
                UpdateParameter[16] = objPayDefDtlUpd.age_dsc4;
                UpdateParameter[17] = objPayDefDtlUpd.federal_tax_id;
                UpdateParameter[18] = objPayDefDtlUpd.dflt_1099;
                UpdateParameter[19] = objPayDefDtlUpd.use_batch_inv;
                UpdateParameter[20] = objPayDefDtlUpd.use_batch_pay;
                UpdateParameter[21] = objPayDefDtlUpd.use_approv_post;
                UpdateParameter[22] = objPayDefDtlUpd.approval_code;
                UpdateParameter[23] = objPayDefDtlUpd.auto_chkno;
                UpdateParameter[24] = objPayDefDtlUpd.last_chkno;

                success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref UpdateParameter, typeof(DVOUpdatePayableDefaults), true);
                if (success == null)
                    throw new Exception();
                else if (Convert.ToInt16(success) < 1)
                    throw new Exception();

                UpdateParameter = null;
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return Convert.ToInt32(success); ;

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                return 0;
            }
            return 0;

        }
        public static int InsertPayableDefDtlInfo(ref object objTransaction, ref DvoUpdatePayableDefDetails objPayDefDtlUpd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object success = null;

            try
            {
                object[] Parameter = new object[25];

                Parameter[0] = objPayDefDtlUpd.term_code;
                Parameter[1] = objPayDefDtlUpd.ap_acct_no;
                Parameter[2] = objPayDefDtlUpd.cd_cash_acct_no;
                Parameter[3] = objPayDefDtlUpd.cd_disc_acct_no;
                Parameter[4] = objPayDefDtlUpd.def_mtaxcd;
                Parameter[5] = objPayDefDtlUpd.entry_by_line;
                Parameter[6] = objPayDefDtlUpd.gross_entry;
                Parameter[7] = objPayDefDtlUpd.mtax_dsc;
                Parameter[8] = objPayDefDtlUpd.ap_balanced;
                Parameter[9] = objPayDefDtlUpd.age_datetype;
                Parameter[10] = objPayDefDtlUpd.age_per1;
                Parameter[11] = objPayDefDtlUpd.age_per2;
                Parameter[12] = objPayDefDtlUpd.age_per3;
                Parameter[13] = objPayDefDtlUpd.age_dsc1;
                Parameter[14] = objPayDefDtlUpd.age_dsc2;
                Parameter[15] = objPayDefDtlUpd.age_dsc3;
                Parameter[16] = objPayDefDtlUpd.age_dsc4;
                Parameter[17] = objPayDefDtlUpd.federal_tax_id;
                Parameter[18] = objPayDefDtlUpd.dflt_1099;
                Parameter[19] = objPayDefDtlUpd.use_batch_inv;
                Parameter[20] = objPayDefDtlUpd.use_batch_pay;
                Parameter[21] = objPayDefDtlUpd.use_approv_post;
                Parameter[22] = objPayDefDtlUpd.approval_code;
                Parameter[23] = objPayDefDtlUpd.auto_chkno;
                Parameter[24] = objPayDefDtlUpd.last_chkno;

                success = objDALBaseClass.InsertData_ByTransaction(ref  objTransaction, ref Parameter, typeof(DVOUpdatePayableDefaults), true);
                if (success == null)
                    throw new Exception();
                else if (Convert.ToInt16(success) < 1)
                    throw new Exception();

                Parameter = null;
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return Convert.ToInt32(success); ;

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                return 0;
            }
            return 0;

        }

        public static void UpdateAPDefaults_LastCheckNo(ref object objTransaction, ref DvoUpdatePayableDefDetails objDvoUpdatePayableDefDetails)
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
                object[] parameters = new object[1];
                parameters[0] = objDvoUpdatePayableDefDetails.last_chkno;

                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DvoUpdatePayableDefDetails), objDvoUpdatePayableDefDetails.UPDATE_LAST_CHECK_NO);
                if (obj == null || obj.ToString().Trim().Length <= 0 || Convert.ToInt32(obj) <= 0)
                    throw new Exception("Error occured during updation of last_checkno in 'stpcntrc'");

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            //return obj;
        }
    }
}
