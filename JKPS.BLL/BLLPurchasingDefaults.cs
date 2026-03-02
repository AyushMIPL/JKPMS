using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************* DevelopmentDate(Modified Date)
    ///1.) BLL For Get and Update Defaults Data       Rahul Jain                                    05/05/2008(DD)
    ///2.) 
    ///<summery>
    public class BLLPurchasingDefaults
    {
        public static List<DVOPurchasingDefault> GetAllDetailInfo()
        {
            List<DVOPurchasingDefault> ListDVOPurchasingDefault = new List<DVOPurchasingDefault>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOPurchasingDefault)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOPurchasingDefault objDVOPurchasingDefault = new DVOPurchasingDefault();
                    objDVOPurchasingDefault.RowID = Convert.ToInt32(dr[0]);
                    objDVOPurchasingDefault.buyer_code = dr[1].ToString().Trim();
                    objDVOPurchasingDefault.price_tolerance = Convert.ToDecimal(dr[2]);
                    objDVOPurchasingDefault.po_type = dr[3].ToString().Trim();

                    objDVOPurchasingDefault.line_type = dr[4].ToString().Trim();
                    objDVOPurchasingDefault.whse_shipto = dr[5].ToString().Trim();
                    objDVOPurchasingDefault.ship_via = dr[6].ToString().Trim();
                    objDVOPurchasingDefault.fob_point = dr[7].ToString().Trim();
                    objDVOPurchasingDefault.print_notes = dr[8].ToString().Trim();
                    objDVOPurchasingDefault.mtaxg_code = dr[9].ToString().Trim();
                    objDVOPurchasingDefault.exempt_tax_code = dr[10].ToString().Trim();
                    objDVOPurchasingDefault.misc_tax_code = dr[11].ToString().Trim();
                    objDVOPurchasingDefault.frght_tax_code = dr[12].ToString().Trim();
                    objDVOPurchasingDefault.ap_acct_no = Convert.ToInt32(dr[13]);
                    objDVOPurchasingDefault.diff_acct_no = Convert.ToInt32(dr[14]);
                    objDVOPurchasingDefault.inv_acct_no = Convert.ToInt32(dr[15]);
                    objDVOPurchasingDefault.misc_acct_no = Convert.ToInt32(dr[16]);
                    objDVOPurchasingDefault.disc_acct_no = Convert.ToInt32(dr[17]);
                    objDVOPurchasingDefault.supp_acct_no = Convert.ToInt32(dr[18]);
                    objDVOPurchasingDefault.frght_acct_no = Convert.ToInt32(dr[19]);
                    objDVOPurchasingDefault.adj_acct_no = Convert.ToInt32(dr[20]);
                    objDVOPurchasingDefault.non_acct_no = Convert.ToInt32(dr[21]);
                    objDVOPurchasingDefault.cap_acct_no = Convert.ToInt32(dr[22]);
                    objDVOPurchasingDefault.cash_acct_no = Convert.ToInt32(dr[23]);
                    //Keyvalues
                    objDVOPurchasingDefault.ap_keyvalue = dr[24].ToString().Trim();
                    objDVOPurchasingDefault.diff_keyvalue = dr[25].ToString().Trim();
                    objDVOPurchasingDefault.inv_keyvalue = dr[26].ToString().Trim();
                    objDVOPurchasingDefault.misc_keyvalue = dr[27].ToString().Trim();
                    objDVOPurchasingDefault.disc_keyvalue  = dr[28].ToString().Trim();
                    objDVOPurchasingDefault.supp_keyvalue = dr[29].ToString().Trim();
                    objDVOPurchasingDefault.frght_keyvalue = dr[30].ToString().Trim();
                    objDVOPurchasingDefault.adj_keyvalue = dr[31].ToString().Trim();
                    objDVOPurchasingDefault.non_keyvalue = dr[32].ToString().Trim();
                    objDVOPurchasingDefault.cap_keyvalue = dr[33].ToString().Trim();
                    objDVOPurchasingDefault.cash_keyvalue = dr[34].ToString().Trim();
                    //account type
                    objDVOPurchasingDefault.ap_acct_type = dr[35].ToString().Trim();
                    objDVOPurchasingDefault.diff_acct_type = dr[36].ToString().Trim();
                    objDVOPurchasingDefault.inv_acct_type = dr[37].ToString().Trim();
                    objDVOPurchasingDefault.misc_acct_type = dr[38].ToString().Trim();
                    objDVOPurchasingDefault.disc_acct_type = dr[39].ToString().Trim();
                    objDVOPurchasingDefault.supp_acct_type = dr[40].ToString().Trim();
                    objDVOPurchasingDefault.frght_acct_type = dr[41].ToString().Trim();
                    objDVOPurchasingDefault.adj_acct_type = dr[42].ToString().Trim();
                    objDVOPurchasingDefault.non_acct_type = dr[43].ToString().Trim();
                    objDVOPurchasingDefault.cap_acct_type = dr[44].ToString().Trim();
                    objDVOPurchasingDefault.cash_acct_type = dr[45].ToString().Trim();

                    objDVOPurchasingDefault.req_doc_no = dr[46] != DBNull.Value ? Convert.ToInt32(dr[46]) : 0;
                    objDVOPurchasingDefault.req_post_no = dr[47] != DBNull.Value ? Convert.ToInt32(dr[47]) : 0;
                    objDVOPurchasingDefault.po_doc_no = dr[48] != DBNull.Value ? Convert.ToInt32(dr[48]) : 0;
                    objDVOPurchasingDefault.rec_doc_no = dr[49] != DBNull.Value ? Convert.ToInt32(dr[49]) : 0;
                    objDVOPurchasingDefault.rec_post_no = dr[50] != DBNull.Value ? Convert.ToInt32(dr[50]) : 0;
                    objDVOPurchasingDefault.use_batch_rec = dr[51].ToString().Trim();
                    objDVOPurchasingDefault.inv_post_no = dr[52] != DBNull.Value ? Convert.ToInt32(dr[52]) : 0;
                    objDVOPurchasingDefault.inv_doc_no = dr[53] != DBNull.Value ? Convert.ToInt32(dr[53]) : 0;
                    objDVOPurchasingDefault.use_batch_inv = dr[54].ToString().Trim();
                    objDVOPurchasingDefault.use_approv_post = dr[55].ToString().Trim();
                    objDVOPurchasingDefault.approval_code = dr[56].ToString().Trim();
                    //
                    objDVOPurchasingDefault.cpu_acct_no =dr[57]!= DBNull.Value ? Convert.ToInt32(dr[57]):0;
                    objDVOPurchasingDefault.cpu_keyvalue = dr[58]!=DBNull.Value ? Convert.ToString(dr[58]):string.Empty;
                    objDVOPurchasingDefault.cpu_acct_type = dr[59]!=DBNull.Value ? Convert.ToString(dr[59]):string.Empty;
                    //
                    objDVOPurchasingDefault.rcttogl_stk = dr[60] != DBNull.Value ? Convert.ToString(dr[60]) : string.Empty;
                    objDVOPurchasingDefault.rcttogl_nstk = dr[61] != DBNull.Value ? Convert.ToString(dr[61]) : string.Empty;

                    ListDVOPurchasingDefault.Add(objDVOPurchasingDefault);
                }
                return ListDVOPurchasingDefault;
            }
        }
        public static object UpdatePurchasingDefaltsInfo(ref object objTransaction, ref DVOPurchasingDefault objDVOPurchasingDefault)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object  success = null;

            try
            {
                object[] UpdateParameters = new object[42];
                UpdateParameters[0] = objDVOPurchasingDefault.RowID;
                UpdateParameters[1] = objDVOPurchasingDefault.buyer_code;
                UpdateParameters[2] = objDVOPurchasingDefault.price_tolerance;
                UpdateParameters[3] = objDVOPurchasingDefault.po_type;
                UpdateParameters[4] = objDVOPurchasingDefault.line_type;
                UpdateParameters[5] = objDVOPurchasingDefault.whse_shipto;
                UpdateParameters[6] = objDVOPurchasingDefault.ship_via;
                UpdateParameters[7] = objDVOPurchasingDefault.fob_point;
                UpdateParameters[8] = objDVOPurchasingDefault.print_notes;
                UpdateParameters[9] = objDVOPurchasingDefault.mtaxg_code;
                UpdateParameters[10] = objDVOPurchasingDefault.exempt_tax_code;
                UpdateParameters[11] = objDVOPurchasingDefault.misc_tax_code;
                UpdateParameters[12] = objDVOPurchasingDefault.frght_tax_code;
                UpdateParameters[13] = objDVOPurchasingDefault.ap_acct_no;
                UpdateParameters[14] = objDVOPurchasingDefault.diff_acct_no;
                UpdateParameters[15] = objDVOPurchasingDefault.inv_acct_no;
                UpdateParameters[16] = objDVOPurchasingDefault.misc_acct_no;
                UpdateParameters[17] = objDVOPurchasingDefault.disc_acct_no;
                UpdateParameters[18] = objDVOPurchasingDefault.supp_acct_no;
                UpdateParameters[19] = objDVOPurchasingDefault.frght_acct_no;
                UpdateParameters[20] = objDVOPurchasingDefault.adj_acct_no;
                UpdateParameters[21] = objDVOPurchasingDefault.non_acct_no;
                UpdateParameters[22] = objDVOPurchasingDefault.cap_acct_no;
                UpdateParameters[23] = objDVOPurchasingDefault.cash_acct_no;

                UpdateParameters[24] = objDVOPurchasingDefault.cpu_acct_no;
                UpdateParameters[25] = objDVOPurchasingDefault.req_doc_no;
                UpdateParameters[26] = objDVOPurchasingDefault.req_post_no;
                UpdateParameters[27] = objDVOPurchasingDefault.po_doc_no;
                UpdateParameters[28] = objDVOPurchasingDefault.rec_doc_no;
                UpdateParameters[29] = objDVOPurchasingDefault.rec_post_no;
                UpdateParameters[30] = objDVOPurchasingDefault.use_batch_rec;
                UpdateParameters[31] = objDVOPurchasingDefault.inv_post_no;
                UpdateParameters[32] = objDVOPurchasingDefault.inv_doc_no;
                UpdateParameters[33] = objDVOPurchasingDefault.use_batch_inv;
                UpdateParameters[34] = objDVOPurchasingDefault.use_approv_post;
                UpdateParameters[35] = objDVOPurchasingDefault.approval_code;
                UpdateParameters[36] = objDVOPurchasingDefault.InsertBy;
                UpdateParameters[37] = objDVOPurchasingDefault.InsertDate;
                UpdateParameters[38] = objDVOPurchasingDefault.InsertMachineInfo;
                UpdateParameters[39] = objDVOPurchasingDefault.UpdateBy;
                UpdateParameters[40] = objDVOPurchasingDefault.UpdateDate;
                UpdateParameters[41] = objDVOPurchasingDefault.UpdateMachineInfo;

                success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref UpdateParameters, typeof(DVOPurchasingDefault),true);

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;

        }
        public static int InsertPurchasingDefaltsInfo(ref DVOPurchasingDefault objDVOPurchasingDefault)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] InsertParameters = new object[30];
                InsertParameters[0] = objDVOPurchasingDefault.buyer_code;
                InsertParameters[1] = objDVOPurchasingDefault.price_tolerance;
                InsertParameters[2] = objDVOPurchasingDefault.po_type;
                InsertParameters[3] = objDVOPurchasingDefault.line_type;
                InsertParameters[4] = objDVOPurchasingDefault.whse_shipto;
                InsertParameters[5] = objDVOPurchasingDefault.ship_via;
                InsertParameters[6] = objDVOPurchasingDefault.fob_point;
                InsertParameters[7] = objDVOPurchasingDefault.print_notes;
                InsertParameters[8] = objDVOPurchasingDefault.mtaxg_code;
                InsertParameters[9] = objDVOPurchasingDefault.exempt_tax_code;
                InsertParameters[10] = objDVOPurchasingDefault.misc_tax_code;
                InsertParameters[11] = objDVOPurchasingDefault.frght_tax_code;
                InsertParameters[12] = objDVOPurchasingDefault.ap_acct_no;
                InsertParameters[13] = objDVOPurchasingDefault.diff_acct_no;
                InsertParameters[14] = objDVOPurchasingDefault.inv_acct_no;
                InsertParameters[15] = objDVOPurchasingDefault.misc_acct_no;
                InsertParameters[16] = objDVOPurchasingDefault.disc_acct_no;
                InsertParameters[17] = objDVOPurchasingDefault.supp_acct_no;
                InsertParameters[18] = objDVOPurchasingDefault.frght_acct_no;
                InsertParameters[19] = objDVOPurchasingDefault.adj_acct_no;
                InsertParameters[20] = objDVOPurchasingDefault.non_acct_no;
                InsertParameters[21] = objDVOPurchasingDefault.cap_acct_no;
                InsertParameters[22] = objDVOPurchasingDefault.cash_acct_no;

                InsertParameters[23] = objDVOPurchasingDefault.cpu_acct_no;
                InsertParameters[24] = objDVOPurchasingDefault.use_batch_rec;

                InsertParameters[25] = objDVOPurchasingDefault.use_batch_inv;
                InsertParameters[26] = objDVOPurchasingDefault.use_approv_post;
                InsertParameters[27] = objDVOPurchasingDefault.approval_code;
                InsertParameters[28] = objDVOPurchasingDefault.InsertBy;
                InsertParameters[29] = objDVOPurchasingDefault.InsertMachineInfo;

                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref InsertParameters, typeof(DVOPurchasingDefault), true);
                if (obj == null)
                    throw new Exception("Error occured to insert data in stucntrc.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured to insert data in stucntrc.");
                else
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    return 1;
                }
                InsertParameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }
    }
}
