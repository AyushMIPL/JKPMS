using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLBankCodesMasterBanks
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="pobjDVOBankCodesMasterBanks">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOBankCodesMasterBanks> GetData(ref DVOBankCodesMasterBanks pobjDVOBankCodesMasterBanks)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOBankCodesMasterBanks> listDVOBankCodesMasterBanks = new List<DVOBankCodesMasterBanks>();

            try
            {
                object[] parameters = new object[14];
                parameters[0] = pobjDVOBankCodesMasterBanks.RowID;
                parameters[1] = pobjDVOBankCodesMasterBanks.bank_code;
                parameters[2] = pobjDVOBankCodesMasterBanks.bank_desc;
                parameters[3] = pobjDVOBankCodesMasterBanks.dfi_dest;
                parameters[4] = pobjDVOBankCodesMasterBanks.chk_digit;
                parameters[5] = pobjDVOBankCodesMasterBanks.dd_bank_code;
                parameters[6] = pobjDVOBankCodesMasterBanks.co_bank_acct_no;
                parameters[7] = pobjDVOBankCodesMasterBanks.cash_acct_no;
                parameters[8] = pobjDVOBankCodesMasterBanks.offset_debit;
                parameters[9] = pobjDVOBankCodesMasterBanks.mag_media;
                parameters[10] = pobjDVOBankCodesMasterBanks.dd_create;
                parameters[11] = pobjDVOBankCodesMasterBanks.dd_format;
                parameters[12] = pobjDVOBankCodesMasterBanks.dd_transfer;
                parameters[13] = pobjDVOBankCodesMasterBanks.suppliercode;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBankCodesMasterBanks)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOBankCodesMasterBanks objDVOBankCodesMasterBanks = new DVOBankCodesMasterBanks();
                        objDVOBankCodesMasterBanks.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOBankCodesMasterBanks.bank_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.bank_desc = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dfi_dest = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOBankCodesMasterBanks.chk_digit = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                        objDVOBankCodesMasterBanks.dd_bank_code = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.co_bank_acct_no = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.cash_acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOBankCodesMasterBanks.offset_debit = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.mag_media = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_create = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_format = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_transfer = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.suppliercode = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);

                        listDVOBankCodesMasterBanks.Add(objDVOBankCodesMasterBanks);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOBankCodesMasterBanks;
            }
            return listDVOBankCodesMasterBanks;
        }

        public static List<DVOBankCodesMasterBanks> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOBankCodesMasterBanks> listDVOBankCodesMasterBanks = new List<DVOBankCodesMasterBanks>();

            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOBankCodesMasterBanks)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOBankCodesMasterBanks objDVOBankCodesMasterBanks = new DVOBankCodesMasterBanks();
                        objDVOBankCodesMasterBanks.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOBankCodesMasterBanks.bank_code = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.bank_desc = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dfi_dest = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOBankCodesMasterBanks.chk_digit = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                        objDVOBankCodesMasterBanks.dd_bank_code = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.co_bank_acct_no = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.cash_acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        objDVOBankCodesMasterBanks.offset_debit = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.mag_media = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_create = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_format = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.dd_transfer = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);
                        objDVOBankCodesMasterBanks.suppliercode = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);

                        listDVOBankCodesMasterBanks.Add(objDVOBankCodesMasterBanks);
                    }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOBankCodesMasterBanks;
            }
            return listDVOBankCodesMasterBanks;
        }

        /// <summary>
        /// To Insert New Record
        /// </summary>
        /// <param name="objDVOBankCodesMasterBanks">DVO object with all information to insert</param>
        /// <returns></returns>
        public static int InsertData(ref object objTransaction, ref DVOBankCodesMasterBanks objDVOBankCodesMasterBanks)
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
                object[] parameters = new object[16];
                parameters[0] = objDVOBankCodesMasterBanks.bank_code;
                parameters[1] = objDVOBankCodesMasterBanks.bank_desc;
                parameters[2] = objDVOBankCodesMasterBanks.dfi_dest;
                parameters[3] = objDVOBankCodesMasterBanks.chk_digit;
                parameters[4] = objDVOBankCodesMasterBanks.dd_bank_code;
                parameters[5] = objDVOBankCodesMasterBanks.co_bank_acct_no;
                parameters[6] = objDVOBankCodesMasterBanks.cash_acct_no;
                parameters[7] = objDVOBankCodesMasterBanks.offset_debit;
                parameters[8] = objDVOBankCodesMasterBanks.mag_media;
                parameters[9] = objDVOBankCodesMasterBanks.dd_create;
                parameters[10] = objDVOBankCodesMasterBanks.dd_format;
                parameters[11] = objDVOBankCodesMasterBanks.dd_transfer;
                parameters[12] = objDVOBankCodesMasterBanks.suppliercode;
                parameters[13] = objDVOBankCodesMasterBanks.InsertMachineInfo;
                if (objDVOBankCodesMasterBanks.InsertDate == string.Empty) objDVOBankCodesMasterBanks.InsertDate = null;
                parameters[14] = objDVOBankCodesMasterBanks.InsertDate;
                parameters[15] = objDVOBankCodesMasterBanks.InsertBy;

                objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOBankCodesMasterBanks));

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return 0;
                }
                else
                    throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Update Record
        /// </summary>
        /// <param name="objDVOBankCodesMasterBanks">DVO object with all information to update</param>
        /// <returns></returns>
        public static int UpdateData(ref object objTransaction, ref DVOBankCodesMasterBanks objDVOBankCodesMasterBanks)
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
                object[] parameters = new object[17];
                parameters[0] = objDVOBankCodesMasterBanks.RowID;
                parameters[1] = objDVOBankCodesMasterBanks.bank_code;
                parameters[2] = objDVOBankCodesMasterBanks.bank_desc;
                parameters[3] = objDVOBankCodesMasterBanks.dfi_dest;
                parameters[4] = objDVOBankCodesMasterBanks.chk_digit;
                parameters[5] = objDVOBankCodesMasterBanks.dd_bank_code;
                parameters[6] = objDVOBankCodesMasterBanks.co_bank_acct_no;
                parameters[7] = objDVOBankCodesMasterBanks.cash_acct_no;
                parameters[8] = objDVOBankCodesMasterBanks.offset_debit;
                parameters[9] = objDVOBankCodesMasterBanks.mag_media;
                parameters[10] = objDVOBankCodesMasterBanks.dd_create;
                parameters[11] = objDVOBankCodesMasterBanks.dd_format;
                parameters[12] = objDVOBankCodesMasterBanks.dd_transfer;
                parameters[13] = objDVOBankCodesMasterBanks.suppliercode;
                parameters[14] = objDVOBankCodesMasterBanks.UpdateMachineInfo;
                if (objDVOBankCodesMasterBanks.UpdateDate == string.Empty) objDVOBankCodesMasterBanks.UpdateDate = null;
                parameters[15] = objDVOBankCodesMasterBanks.UpdateDate;
                parameters[16] = objDVOBankCodesMasterBanks.UpdateBy;

                objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOBankCodesMasterBanks));

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return 0;
                }
                else
                    throw ex;
            }
            return 0;
        }

    }
}
