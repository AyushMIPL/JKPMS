using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;

namespace JKPS.BLL
{
    public class BLLPRBankMediaInybankd
    {
        public static List<DVOMasterBankDetails> GetData(ref DVOMasterBankDetails objDVOMasterBankDetails, bool OnlyMediaString, string BankCodes)
        {
            List<DVOMasterBankDetails> ListDVOMasterBankDetails = new List<DVOMasterBankDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            object[] parameters = new object[4];
            parameters[0] = objDVOMasterBankDetails.rowid;
            parameters[1] = objDVOMasterBankDetails.bank_code;
            if (OnlyMediaString)
            {
                parameters[2] = "FOR_MULTIPLE_BANK";
                parameters[3] = BankCodes;
            }
            else
            {
                parameters[2] = string.Empty;
                parameters[3] = string.Empty;
            }

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterBankDetails)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMasterBankDetails tempDVOMasterBankDetails = new DVOMasterBankDetails();

                    tempDVOMasterBankDetails.rowid = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                    tempDVOMasterBankDetails.bank_code = dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.media_str = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.insertby = dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0;
                    tempDVOMasterBankDetails.insertdate = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.insertmachineinfo = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.updateby = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
                    tempDVOMasterBankDetails.updatedate = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.updatemachineinfo = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;

                    ListDVOMasterBankDetails.Add(tempDVOMasterBankDetails);
                }
            }
            return ListDVOMasterBankDetails;
        }
        public static List<DVOMasterBankDetails> GetDataforExcelsheet(ref DVOMasterBankDetails objDVOMasterBankDetails, bool OnlyMediaString, string BankCodes)
        {
            List<DVOMasterBankDetails> ListDVOMasterBankDetails = new List<DVOMasterBankDetails>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            object[] parameters = new object[4];
            parameters[0] = objDVOMasterBankDetails.rowid;
            parameters[1] = objDVOMasterBankDetails.bank_code;
            if (OnlyMediaString)
            {
                parameters[2] = "FOR_MULTIPLE_BANK";
                parameters[3] = BankCodes;
            }
            else
            {
                parameters[2] = string.Empty;
                parameters[3] = string.Empty;
            }

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterBankDetails)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMasterBankDetails tempDVOMasterBankDetails = new DVOMasterBankDetails();

                    tempDVOMasterBankDetails.rowid = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                    tempDVOMasterBankDetails.bank_code = dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.media_str = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.insertby = dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0;
                    tempDVOMasterBankDetails.insertdate = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.insertmachineinfo = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.updateby = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
                    tempDVOMasterBankDetails.updatedate = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    tempDVOMasterBankDetails.updatemachineinfo = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;

                    ListDVOMasterBankDetails.Add(tempDVOMasterBankDetails);
                }
            }
            return ListDVOMasterBankDetails;
        }

        public static int InsertData(ref object objTransaction, ref DVOMasterBankDetails objDVOMasterBankDetails)
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
                object[] parameters = new object[5];
                parameters[0] = objDVOMasterBankDetails.bank_code;
                parameters[1] = objDVOMasterBankDetails.media_str;
                parameters[2] = objDVOMasterBankDetails.insertby;
                if (objDVOMasterBankDetails.insertdate == string.Empty) objDVOMasterBankDetails.insertdate = "01/01/1900";
                parameters[3] = objDVOMasterBankDetails.insertdate;
                parameters[4] = objDVOMasterBankDetails.insertmachineinfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterBankDetails.INSERT_SPNAME);
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

        public static int UpdateData(ref object objTransaction, ref DVOMasterBankDetails objDVOMasterBankDetails)
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
                object[] parameters = new object[5];
                parameters[0] = objDVOMasterBankDetails.bank_code;
                parameters[1] = objDVOMasterBankDetails.media_str;
                parameters[2] = objDVOMasterBankDetails.updateby;
                if (objDVOMasterBankDetails.updatedate == string.Empty) objDVOMasterBankDetails.updatedate = "01/01/1900";
                parameters[3] = objDVOMasterBankDetails.updatedate;
                parameters[4] = objDVOMasterBankDetails.updatemachineinfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterBankDetails.UPDATE_SPNAME);
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

        public static int DeleteData(ref object objTransaction, ref DVOMasterBankDetails objDVOMasterBankDetails)
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
                parameters[0] = objDVOMasterBankDetails.bank_code;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOMasterBankDetails.DELETE_SPNAME);
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
