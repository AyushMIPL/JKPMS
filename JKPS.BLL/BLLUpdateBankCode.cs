using System;
using System.Collections.Generic;
using System.Text;

using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)     BLL For Bank Code                         Rajeev(D)                                    15/12/2008(DD)
    ///2.) 
    ///<summery>
    public class BLLUpdateBankCode
    {
        
        public static List<DVOUpdateBankCode> GetBankCodeDetails(ref DVOUpdateBankCode objDVOBankCodeSearchCrit)
        {

            object[] parameters = new object[8];
            parameters[0] = objDVOBankCodeSearchCrit.bank_code;
            parameters[1] = objDVOBankCodeSearchCrit.bank_desc;
            parameters[2] = objDVOBankCodeSearchCrit.dfi_dest;
            parameters[3] = objDVOBankCodeSearchCrit.co_bank_acct_no;
            parameters[4] = objDVOBankCodeSearchCrit.cash_acct_no;
            parameters[5] = objDVOBankCodeSearchCrit.suppliercode;
            parameters[6] = objDVOBankCodeSearchCrit.mag_media;

            //**********Added by Sunil Pahwa******************
            parameters[7] = objDVOBankCodeSearchCrit.RowID;
            //************************************************

            List<DVOUpdateBankCode> objDVOBankCodeList = new List<DVOUpdateBankCode>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateBankCode)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateBankCode tempobjDVOBankCode = new DVOUpdateBankCode();

                    tempobjDVOBankCode.RowID = Convert.ToInt32(dr[0]);
                    tempobjDVOBankCode.bank_code =(dr[1]!=DBNull.Value ? dr[1].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.bank_desc = (dr[2]!=DBNull.Value ? dr[2].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.dfi_dest = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    tempobjDVOBankCode.chk_digit = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4].ToString().Trim()) : 0);
                    tempobjDVOBankCode.dd_bank_code = (dr[5]!=DBNull.Value ? dr[5].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.co_bank_acct_no =(dr[6]!=DBNull.Value ? dr[6].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.cash_acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7].ToString().Trim()) : 0);
                    tempobjDVOBankCode.offset_debit = (dr[8]!=DBNull.Value ? dr[8].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.mag_media = (dr[9]!=DBNull.Value ? dr[9].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.dd_create = (dr[10]!=DBNull.Value ? dr[10].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.dd_format = (dr[11]!=DBNull.Value ? dr[11].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.dd_transfer = (dr[12]!=DBNull.Value ? dr[12].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.suppliercode = (dr[13]!=DBNull.Value ? dr[13].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.bus_name = (dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty);
                    tempobjDVOBankCode.Acct_Type=(dr[14]!=DBNull.Value ? dr[14].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.Acct_Desc=(dr[15]!=DBNull.Value ? dr[15].ToString().Trim() :string.Empty);
                    tempobjDVOBankCode.Acct_No = (dr[16] != DBNull.Value ? Convert.ToInt32(dr[16].ToString().Trim()) : 0);
                    tempobjDVOBankCode.KeyValue = (dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty);

                    objDVOBankCodeList.Add(tempobjDVOBankCode);
                }
            }
            return objDVOBankCodeList;

        }

        public static int InsertNewBankCodeDetails(ref DVOUpdateBankCode objDVOUpdateBankCodeInsert, ref DVOMasterBankDetails objDVOMasterBankDetails)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[19];
                parameters[0] = objDVOUpdateBankCodeInsert.bank_code;
                parameters[1] = objDVOUpdateBankCodeInsert.bank_desc;
                parameters[2] = objDVOUpdateBankCodeInsert.dfi_dest;
                parameters[3] = objDVOUpdateBankCodeInsert.chk_digit;
                parameters[4] = objDVOUpdateBankCodeInsert.dd_bank_code;
                parameters[5] = objDVOUpdateBankCodeInsert.co_bank_acct_no;
                parameters[6] = objDVOUpdateBankCodeInsert.cash_acct_no;
                parameters[7] = objDVOUpdateBankCodeInsert.offset_debit;
                parameters[8] = objDVOUpdateBankCodeInsert.mag_media;
                parameters[9] = objDVOUpdateBankCodeInsert.dd_create;
                parameters[10] = objDVOUpdateBankCodeInsert.dd_format;
                parameters[11] = objDVOUpdateBankCodeInsert.dd_transfer;
                parameters[12] = objDVOUpdateBankCodeInsert.suppliercode;                

                //Parameters used For Only SQL Server
                parameters[13] = objDVOUpdateBankCodeInsert.InsertMachineInfo;
                parameters[14] = objDVOUpdateBankCodeInsert.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[15] = objDVOUpdateBankCodeInsert.InsertBy;
                parameters[16] = objDVOUpdateBankCodeInsert.UpdateMachineInfo;
                parameters[17] = objDVOUpdateBankCodeInsert.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[18] = objDVOUpdateBankCodeInsert.UpdateBy;

                object obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, (new DVOUpdateBankCode()).INSERT_SPNAME);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                else
                    success = Convert.ToInt32(obj);
                //DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateBankCode));
                //if (Ds.Tables.Count > 0)
                //    if (Ds.Tables[0].Rows.Count > 0)
                //    {
                //        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
                //    }

                // Return when Contact Details Inserted Successfully
                if (success == 1)
                {
                    BLLPRBankMediaInybankd.InsertData(ref objTransection, ref objDVOMasterBankDetails);
                    if (objTransection != null)
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    return 1;
                }
                // Return when matching BankCode Found
                else if (success == 2)
                {
                    if (objTransection != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                    return 2;
                }
                // Return when Exception Occured During the insert Statement
                else
                {
                    if (objTransection != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public static int DeleteBankCodeDetails(ref DVOUpdateBankCode objDVOUpdateBankCodeDelete, ref DVOMasterBankDetails objDVOMasterBankDetails)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // object objTransection = objDALBaseClassHelper.GetTransactionObject();
            int success = 0;
            bool ExcScalar = true;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVOUpdateBankCodeDelete.RowID;
                parameters[1] = objDVOUpdateBankCodeDelete.bank_code;

                //object[] RetValue = new object[1];
                object RetValue = objDalBaseClass.DeleteData(ref parameters, typeof(DVOUpdateBankCode), ExcScalar);
                if (RetValue != null)
                {
                    object objtran = null;
                    BLLPRBankMediaInybankd.DeleteData(ref objtran, ref objDVOMasterBankDetails);
                    success = Convert.ToInt32(RetValue.ToString());
                    return success;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
            return success;
        }


        public static object UpdateBankCodeDetailsInfo(ref object TransactionObject, ref DVOUpdateBankCode objDVOUpdateBankCodeUpd, ref DVOMasterBankDetails objDVOMasterBankDetails)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }

            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {

                object[] parameters = new object[17];
                parameters[0] = objDVOUpdateBankCodeUpd.RowID;
                parameters[1] = objDVOUpdateBankCodeUpd.bank_code;
                parameters[2] = objDVOUpdateBankCodeUpd.bank_desc;
                parameters[3] = objDVOUpdateBankCodeUpd.dfi_dest;
                parameters[4] = objDVOUpdateBankCodeUpd.chk_digit;
                parameters[5] = objDVOUpdateBankCodeUpd.dd_bank_code;
                parameters[6] = objDVOUpdateBankCodeUpd.co_bank_acct_no;
                parameters[7] = objDVOUpdateBankCodeUpd.cash_acct_no;
                parameters[8] = objDVOUpdateBankCodeUpd.offset_debit;
                parameters[9] = objDVOUpdateBankCodeUpd.mag_media;
                parameters[10] = objDVOUpdateBankCodeUpd.dd_create;
                parameters[11] = objDVOUpdateBankCodeUpd.dd_format;
                parameters[12] = objDVOUpdateBankCodeUpd.dd_transfer;
                parameters[13] = objDVOUpdateBankCodeUpd.suppliercode;
                //Parameters used For Only SQL Server               
                parameters[14] = objDVOUpdateBankCodeUpd.UpdateMachineInfo;
                parameters[15] = objDVOUpdateBankCodeUpd.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[16] = objDVOUpdateBankCodeUpd.UpdateBy;

                object[] RetValue = new object[1];
                //RetValue[0] = objDALBaseClass.UpdateData(ref parameters, typeof(DVOUpdateBankCode), true);
                object obj = objDALBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameters, (new DVOUpdateBankCode()).UPDATE_SPNAME);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                else
                    // Return when Code updated Successfully
                    //All details with bank code updated successfully
                    if (Convert.ToInt32(obj) == 1)
                    {
                        objDVOMasterBankDetails.bank_code = objDVOUpdateBankCodeUpd.bank_code;
                        BLLPRBankMediaInybankd.UpdateData(ref TransactionObject, ref objDVOMasterBankDetails);
                        if (TransactionObject != null)
                            objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
            }
            catch (Exception ex)
            {
                if (TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;
        }
    }
}
