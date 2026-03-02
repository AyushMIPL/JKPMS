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
    public class BLLDeductionAnalysis
    {
        public static int InsertData(ref object TransactionObject, ref DVOstydedanlyr objDVOstydedanlyr, ref  List<DVOstydedanlyd> listDVOstydedanlyd, ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd, out int NewDocNo)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                int newAPDocNo = 0;
                int _status = 0;
                //Insert INTO stpcashe
                //_status;// = BLLAPCheckProcessingStpcashe.InsertData(ref TransactionObject, ref objDVOAPCheckProcessingStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out newAPDocNo, false);
                if (_status > 0)
                {
                    objDVOstydedanlyr.apdoc_no = newAPDocNo;
                    //Insert into stydedanlyr 
                    NewDocNo = 0;
                    //int obj = BLLDeductionAnalysis.InsertstydedanlyR(ref TransactionObject, ref  objDVOstydedanlyr, out NewDocNo);
                    
                    object[] parameters = new object[5];
                    parameters[0] = objDVOstydedanlyr.apdoc_no;
                    parameters[1] = objDVOstydedanlyr.amount;
                    parameters[2] = objDVOstydedanlyr.chk_name;
                    parameters[3] = objDVOstydedanlyr.insertby;
                    parameters[4] = objDVOstydedanlyr.insertmachineinfo;

                    using (DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOstydedanlyr)))
                    {
                        if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                        {
                            if (ds.Tables[0].Rows[0][1] != DBNull.Value && ds.Tables[0].Rows[0][1].ToString().Trim().Length > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][1]) > 0)
                            {
                                NewDocNo = ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
                                if (NewDocNo <= 0)
                                    throw new Exception("Error occured to Insert data in stydedanlyr.");
                            }
                            else
                                throw new Exception("Error occured to Insert data in stydedanlyr.");
                        }
                    }
                    //if (obj == DBNull.Value)
                    //    throw new Exception("Error occured to Insert data in stydedanlyr.");
                    //else if (Convert.ToInt32(obj) < 1)
                    //    throw new Exception("Error occured to Insert data in stydedanlyr.");

                    if (NewDocNo > 0)
                    {
                        foreach (DVOstydedanlyd obj in listDVOstydedanlyd)
                            obj.doc_no = NewDocNo;
                        int obj2 = InsertstydedanlyD(ref TransactionObject, ref listDVOstydedanlyd);
                        if (obj2 <= 0)
                            throw new Exception("Error occured during inserting of new stydedanlyd.");

                    }
                }
                else
                    throw new Exception("Error occured while inserting Non-Ap Entry from Deduction Analysis screen.");
                if (!statusObjTransaction && TransactionObject != null)
                    //objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                if (TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        ////Insert header entry
        //public static int InsertstydedanlyR(ref object TransactionObject, ref DVOstydedanlyr objDVOstydedanlyr, out int NewDocNo)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    int success = 0;
        //    if (TransactionObject == null)
        //    {
        //        TransactionObject = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    int _NewDocNo = 0;
        //    try
        //    {
        //        object[] parameters = new object[7];
        //        parameters[0] = objDVOstydedanlyr.apdoc_no;
        //        parameters[1] = objDVOstydedanlyr.amount;
        //        parameters[2] = objDVOstydedanlyr.chk_name;
        //        parameters[3] = objDVOstydedanlyr.insertby;
        //        parameters[4] = objDVOstydedanlyr.insertmachineinfo;

        //        object obj = objDalBaseClass.InsertData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOstydedanlyr), true);
        //        if (obj == null)
        //            throw new Exception("Error occured to Insert data in stydedanlyr.");
        //        else if (Convert.ToInt32(obj) < 1)
        //            throw new Exception("Error occured to Insert data in stydedanlyr.");

        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        if (!statusObjTransaction && TransactionObject != null)
        //            objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
        //        throw ex;
        //    }
        //    return success;
        //}
        //insert Detail entry
        public static int InsertstydedanlyD(ref object objTransaction, ref List<DVOstydedanlyd> listlistDVOstydedanlyd)
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
                if (listlistDVOstydedanlyd.Count > 0)
                {
                    object[] parameters = new object[10];
                    foreach (DVOstydedanlyd obj in listlistDVOstydedanlyd)
                    {
                        parameters[0] = obj.doc_no;
                        parameters[1] = obj.payroll_doc_no;
                        parameters[2] = obj.empl_code;
                        parameters[3] = obj.last_name;
                        parameters[4] = obj.first_name;
                        parameters[5] = obj.ded_code;
                        parameters[6] = obj.ded_amount;
                        parameters[7] = obj.pay_date;
                        parameters[8] = obj.insertby;
                        parameters[9] = obj.insertmachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SPNAME);
                        if (o == DBNull.Value)
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
    }
}
