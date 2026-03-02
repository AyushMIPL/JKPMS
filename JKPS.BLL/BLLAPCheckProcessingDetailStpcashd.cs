using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLAPCheckProcessingDetailStpcashd
    {
        public static List<DVOAPCheckProcessingDetailStpcashd> GetData(ref DVOAPCheckProcessingStpcashe pobjDVOAPCheckProcessingStpcashe, ref DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd)
        {
            List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[17];
                parameters[0] = pobjDVOAPCheckProcessingStpcashe.vend_code;
                parameters[1] = pobjDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[2] = pobjDVOAPCheckProcessingStpcashe.cash_acct;
                parameters[3] = pobjDVOAPCheckProcessingStpcashe.cash_department;

                parameters[4] = objDVOAPCheckProcessingDetailStpcashd.doc_no;
                parameters[5] = objDVOAPCheckProcessingDetailStpcashd.inv_doc_no;
                parameters[6] = objDVOAPCheckProcessingDetailStpcashd.inv_no;
                if (objDVOAPCheckProcessingDetailStpcashd.due_date == string.Empty)
                    objDVOAPCheckProcessingDetailStpcashd.due_date = null;
                parameters[7] = objDVOAPCheckProcessingDetailStpcashd.due_date;
                parameters[8] = objDVOAPCheckProcessingDetailStpcashd.dist_acct;
                parameters[9] = objDVOAPCheckProcessingDetailStpcashd.dist_amt;
                parameters[10] = objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred;
                parameters[11] = objDVOAPCheckProcessingDetailStpcashd.disc_acct;
                parameters[12] = objDVOAPCheckProcessingDetailStpcashd.disc_amt;
                parameters[13] = objDVOAPCheckProcessingDetailStpcashd.disc_deb_cred;
                parameters[14] = objDVOAPCheckProcessingDetailStpcashd.RowId;
                parameters[15] = objDVOAPCheckProcessingDetailStpcashd.ok_to_post;
                if (objDVOAPCheckProcessingDetailStpcashd.ForCheckRecon == "CheckReconciliation")
                {
                    parameters[16] = "CheckReconciliation";
                }
                else
                parameters[16] = string.Empty;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingDetailStpcashd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOAPCheckProcessingDetailStpcashd tobjDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                tobjDVOAPCheckProcessingDetailStpcashd.doc_no = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//p_doc_no
                                tobjDVOAPCheckProcessingDetailStpcashd.inv_doc_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//p_inv_doc_no
                                tobjDVOAPCheckProcessingDetailStpcashd.inv_no = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//p_inv_no
                                tobjDVOAPCheckProcessingDetailStpcashd.due_date = (dr[3] != DBNull.Value ? Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//p_due_date
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_acct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);//p_dist_acct
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_department = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//v_dist_department
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_amt = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);//p_dist_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_dist_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.mtax_code = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//v_mtax_code
                                tobjDVOAPCheckProcessingDetailStpcashd.goods_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);//v_goods_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_acct = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);//p_disc_acct
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_department = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//p_disc_department
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_amt = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);//p_disc_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_deb_cred = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);//"p_disc_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.RowId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);//v_RowId
                                tobjDVOAPCheckProcessingDetailStpcashd.pendingBalance = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0);//v_balance
                                tobjDVOAPCheckProcessingDetailStpcashd.paymethod = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);//"v_pay_method
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_date = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);//v_disc_date
                                tobjDVOAPCheckProcessingDetailStpcashd.open_doc_no = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);//v_open_doc_no

                                listDVOAPCheckProcessingDetailStpcashd.Add(tobjDVOAPCheckProcessingDetailStpcashd);
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingDetailStpcashd;
        }

        
        public static int GetCountVendorInvoice(ref DVOAPCheckProcessingStpcashe pobjDVOAPCheckProcessingStpcashe, ref DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd)
        {
            int _Count = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[17];
                parameters[0] = pobjDVOAPCheckProcessingStpcashe.vend_code;
                parameters[1] = pobjDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[2] = pobjDVOAPCheckProcessingStpcashe.cash_acct;
                parameters[3] = pobjDVOAPCheckProcessingStpcashe.cash_department;

                parameters[4] = objDVOAPCheckProcessingDetailStpcashd.doc_no;
                parameters[5] = objDVOAPCheckProcessingDetailStpcashd.inv_doc_no;
                parameters[6] = objDVOAPCheckProcessingDetailStpcashd.inv_no;
                if (objDVOAPCheckProcessingDetailStpcashd.due_date == string.Empty)
                    objDVOAPCheckProcessingDetailStpcashd.due_date = null;
                parameters[7] = objDVOAPCheckProcessingDetailStpcashd.due_date;
                parameters[8] = objDVOAPCheckProcessingDetailStpcashd.dist_acct;
                parameters[9] = objDVOAPCheckProcessingDetailStpcashd.dist_amt;
                parameters[10] = objDVOAPCheckProcessingDetailStpcashd.dist_deb_cred;
                parameters[11] = objDVOAPCheckProcessingDetailStpcashd.disc_acct;
                parameters[12] = objDVOAPCheckProcessingDetailStpcashd.disc_amt;
                parameters[13] = objDVOAPCheckProcessingDetailStpcashd.disc_deb_cred;
                parameters[14] = objDVOAPCheckProcessingDetailStpcashd.RowId;
                parameters[15] = objDVOAPCheckProcessingDetailStpcashd.ok_to_post;
                parameters[16] = "CheckVendorInvoice";

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingDetailStpcashd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                _Count = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {   
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _Count;
        }

        public static List<DVOAPCheckProcessingDetailStpcashd> GetAllData()
        {
            List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL(); 
            try
            {
                using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOAPCheckProcessingDetailStpcashd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOAPCheckProcessingDetailStpcashd tobjDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                tobjDVOAPCheckProcessingDetailStpcashd.doc_no = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//p_doc_no
                                tobjDVOAPCheckProcessingDetailStpcashd.inv_doc_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//p_inv_doc_no
                                tobjDVOAPCheckProcessingDetailStpcashd.inv_no = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);//p_inv_no
                                tobjDVOAPCheckProcessingDetailStpcashd.due_date = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//p_due_date
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_acct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);//p_dist_acct
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_department = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//v_dist_department
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_amt = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);//p_dist_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_dist_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.mtax_code = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//v_mtax_code
                                tobjDVOAPCheckProcessingDetailStpcashd.goods_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);//v_goods_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_acct = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);//p_disc_acct
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_department = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//p_disc_department
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_amt = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);//p_disc_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_deb_cred = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);//"p_disc_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.RowId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);//v_RowId
                                tobjDVOAPCheckProcessingDetailStpcashd.pendingBalance = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0);//v_balance
                                tobjDVOAPCheckProcessingDetailStpcashd.paymethod = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);//"v_pay_method
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_date = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);//v_disc_date
                                tobjDVOAPCheckProcessingDetailStpcashd.open_doc_no = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);//v_open_doc_no

                                listDVOAPCheckProcessingDetailStpcashd.Add(tobjDVOAPCheckProcessingDetailStpcashd);
                            }
                }
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingDetailStpcashd;
        }

        public static int InsertData(ref object objTransaction, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd, out decimal DebitAmountInserted, out decimal CreditAmountInserted)
        {
            DebitAmountInserted = 0; CreditAmountInserted = 0;
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
                if (listDVOAPCheckProcessingDetailStpcashd.Count > 0)
                {
                    object[] parameters = new object[15];
                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listDVOAPCheckProcessingDetailStpcashd)
                    {
                        parameters[0] = obj.doc_no;
                        parameters[1] = obj.inv_doc_no;
                        parameters[2] = obj.inv_no.Length > 10 ? obj.inv_no.Substring(0, 10) : obj.inv_no;
                        if (obj.due_date.Trim().Length > 0 && !obj.due_date.Contains("1900") && !obj.due_date.Contains("0001"))
                            obj.due_date = obj.due_date;// Convert.ToDateTime(obj.due_date).ToString("MM/dd/yyyy");
                        parameters[3] = obj.due_date;
                        parameters[4] = obj.dist_acct;
                        parameters[5] = obj.dist_department;
                        parameters[6] = obj.dist_amt;
                        parameters[7] = obj.dist_deb_cred;
                        parameters[8] = obj.disc_acct;
                        parameters[9] = obj.disc_department;
                        parameters[10] = obj.disc_amt;
                        parameters[11] = obj.disc_deb_cred;
                        parameters[12] = obj.insertby;
                        parameters[13] = obj.insertdate;
                        parameters[14] = obj.insertmachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();

                        if (obj.dist_deb_cred == "DB") DebitAmountInserted += obj.dist_amt;
                        else if (obj.dist_deb_cred == "CR") CreditAmountInserted += obj.dist_amt;

                        if (obj.disc_deb_cred == "DB") DebitAmountInserted += obj.disc_amt;
                        else if (obj.disc_deb_cred == "CR") CreditAmountInserted += obj.disc_amt;
                    }
                    parameters = null;
                }
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

        public static int UpdateData(ref object objTransaction, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
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
                if (listDVOAPCheckProcessingDetailStpcashd.Count > 0)
                {
                    object[] parameters = new object[16];
                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listDVOAPCheckProcessingDetailStpcashd)
                    {
                        parameters[0] = obj.RowId;
                        parameters[1] = obj.doc_no;
                        parameters[2] = obj.inv_doc_no;
                        parameters[3] = obj.inv_no;
                        if (obj.due_date.Trim().Length > 0 && !obj.due_date.Contains("1900") && !obj.due_date.Contains("0001"))
                            obj.due_date = obj.due_date;// Convert.ToDateTime(obj.due_date).ToString("MM/dd/yyyy");
                        parameters[4] = obj.due_date;
                        parameters[5] = obj.dist_acct;
                        parameters[6] = obj.dist_department;
                        parameters[7] = obj.dist_amt;
                        parameters[8] = obj.dist_deb_cred;
                        parameters[9] = obj.disc_acct;
                        parameters[10] = obj.disc_department;
                        parameters[11] = obj.disc_amt;
                        parameters[12] = obj.disc_deb_cred;
                        parameters[13] = obj.updateby;
                        parameters[14] = obj.updatedate;
                        parameters[15] = obj.updatemachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.UPDATE_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();
                    }
                    parameters = null;
                }
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

        public static int DeleteData(ref object objTransaction, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
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
                if (listDVOAPCheckProcessingDetailStpcashd.Count > 0)
                {
                    object[] parameters = new object[2];
                    foreach (DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd in listDVOAPCheckProcessingDetailStpcashd)
                    {
                        //if (objDVOAPCheckProcessingDetailStpcashd != null)
                        //{
                        parameters[0] = objDVOAPCheckProcessingDetailStpcashd.doc_no;
                        parameters[1] = objDVOAPCheckProcessingDetailStpcashd.RowId;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingDetailStpcashd.DELETE_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();
                        //}
                    }
                    parameters = null;
                    objDALBaseClass = null;
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
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

        public static int DeleteAllDetailLines(ref object objTransaction, int DocumentNo)
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
                object[] parameters = new object[1];
                parameters[0] = DocumentNo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOAPCheckProcessingDetailStpcashd()).DELETE_ALL_DETAILS);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();

                    parameters = null;
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

        public static bool CheckRelateToDoc(int CheckRowId, int DocNo)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = CheckRowId;
                parameters[1] = DocNo;

                object o = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOAPCheckProcessingStpcashe()).CHECK_RELATE_TO_DOC);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToBoolean(o))
                    return true;
                else
                    return false;

                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return true;
            }
            return true;
        }

        public static List<DVOAPCheckProcessingDetailStpcashd> GetDataForCheckStatus(ref DVOAPCheckProcessingDetailStpcashd objDVOAPChkProcessingDetailStpcashd)
        {
            List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOAPChkProcessingDetailStpcashd.doc_no;


                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingDetailStpcashd), objDVOAPChkProcessingDetailStpcashd.GET_DETAIL_FOR_CHECK_STATUS))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOAPCheckProcessingDetailStpcashd tobjDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                
                                tobjDVOAPCheckProcessingDetailStpcashd.inv_no = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//p_inv_no
                                tobjDVOAPCheckProcessingDetailStpcashd.due_date = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);//p_due_date
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_acct = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);//p_dist_acct
                               
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_amt = (dr[3] != DBNull.Value ? Convert.ToDecimal(dr[3]) : 0);//p_dist_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.dist_deb_cred = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//"p_dist_deb_cred

                                tobjDVOAPCheckProcessingDetailStpcashd.disc_acct = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);//p_disc_acct

                                tobjDVOAPCheckProcessingDetailStpcashd.disc_amt = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);//p_disc_amt
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_deb_cred = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);//"p_disc_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.keyvalue = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//"p_disc_deb_cred
                                tobjDVOAPCheckProcessingDetailStpcashd.disc_keyvalue = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//"p_disc_deb_cred
                                ////tobjDVOAPCheckProcessingDetailStpcashd.pendingBalance = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0);//v_balance
                                ////tobjDVOAPCheckProcessingDetailStpcashd.paymethod = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty);//"v_pay_method
                                ////tobjDVOAPCheckProcessingDetailStpcashd.disc_date = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);//v_disc_date
                                ////tobjDVOAPCheckProcessingDetailStpcashd.open_doc_no = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);//v_open_doc_no

                                listDVOAPCheckProcessingDetailStpcashd.Add(tobjDVOAPCheckProcessingDetailStpcashd);
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingDetailStpcashd;



        }

        public static int InsertIntoStpcashd(ref object objTransaction, ref DVOAPCheckProcessingDetailStpcashd objStpcashd)
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
                object[] parameters = new object[15];
                parameters[0] = objStpcashd.doc_no;
                parameters[1] = objStpcashd.inv_doc_no;
                parameters[2] = objStpcashd.inv_no;
                if (objStpcashd.due_date  == string.Empty) objStpcashd.due_date  = "01/01/1900";
                parameters[3] = objStpcashd.due_date;
                parameters[4] = objStpcashd.dist_acct;
                parameters[5] = objStpcashd.dist_department;
                parameters[6] = objStpcashd.dist_amt;
                parameters[7] = objStpcashd.dist_deb_cred;
                parameters[8] = objStpcashd.disc_acct;
                parameters[9] = objStpcashd.disc_department;
                parameters[10] = objStpcashd.disc_amt;
                parameters[11] = objStpcashd.disc_deb_cred;
                parameters[12] = objStpcashd.insertby;
                parameters[13] = objStpcashd.insertdate;
                parameters[14] = objStpcashd.insertmachineinfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objStpcashd.INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
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
    }
}
