using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
   public class BLLSalaryCode
    {
       public static List<DVOSalaryCode> GetSalaryCode(ref DVOSalaryCode objDVOSalarySearchCriteria)
       {
           Object[] parameters = new object[3];
           parameters[0] = objDVOSalarySearchCriteria.Code;
           parameters[1] = objDVOSalarySearchCriteria.Per_anum;
           parameters[2] = objDVOSalarySearchCriteria.RowID;
           List<DVOSalaryCode> objDVODetailsList = new List<DVOSalaryCode>();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryCode)))
           {
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   DVOSalaryCode tempobjDVO = new DVOSalaryCode();
                   tempobjDVO.RowID = Convert.ToInt32(dr["p_rowid"]);
                   tempobjDVO.Code = dr["p_code"].ToString().Trim();
                   tempobjDVO.Per_anum =(dr["p_per_annum"]!=DBNull.Value ?(decimal?)(dr["p_per_annum"]):null);
                   objDVODetailsList.Add(tempobjDVO);
               }
           }
           return objDVODetailsList;
       }
       public static int InsertCode(ref DVOSalaryCode objSalaryCode, ref object objTransaction)
       {
          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            int status = 0;
            try
            {
               
                object[] parameters = new object[2];
                parameters[0] = objSalaryCode.Code;
                parameters[1] = objSalaryCode.Per_anum;
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object c = objDalBaseClass.ExecuteScalar_ByTransaction(ref  objTransaction, ref parameters, objSalaryCode.INSERT_SPNAME);
                if (c == null)
                    throw new Exception();
                else if (Convert.ToInt16(c) < 1)
                    throw new Exception();
                if (Convert.ToInt16(c) == 1)
                {
                    if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    status = 1;
                }
                if (Convert.ToInt16(c) == 2)
                {
                    if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    status = 2;
                }
                //if (status == 1)
                //{
                //    if (!statusObjTransaction)
                //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //    // objTransaction = objDALBaseClassHelper.GetTransactionObject();
                //}
                //else
                //{
                //    if (!statusObjTransaction)
                //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //    //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                //}

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
           //else
           //{ status = 0; }
            return status;
       }
       public static bool UpdateCode(ref DVOSalaryCode objSalarycode, ref object objTransaction)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           bool statusObjTransaction = true;
           if (objTransaction == null)
           {
               objTransaction = objDALBaseClassHelper.GetTransactionObject();
               statusObjTransaction = false;
           }
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           bool status = false;

           try
           {
               object[] parameters = new object[2];
               //parameters[0] = objSalarycode.RowID;
               parameters[0] = objSalarycode.Code;
               parameters[1] = objSalarycode.Per_anum;

               object c = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalarycode.UPDATE_SPNAME, true);
               //success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOBCBudgetSets));
               //UpdateData(ref parameters, typeof(DVOBCBudgetSets));
               // return c;

               // success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref UpdParameters, typeof(DVOUpdateBudgetControlDefaults));
               if (c == null)
                   throw new Exception();
               else if (Convert.ToInt16(c) < 1)
                   throw new Exception();
               if (Convert.ToInt16(c) == 1)
               {

                   if (!statusObjTransaction)
                       objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                   status = true;

               }
           }
           catch (Exception ex)
           {
               if (!statusObjTransaction)
                   objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;
           }
           return status;
           
           
           
           
           
           //**************
           //bool status = false;
           //object[] parameters = new object[2];
           ////parameters[0] = objSalarycode.RowID;
           //parameters[0] = objSalarycode.Code;
           //parameters[1] = objSalarycode.Per_anum;
           //DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
           //object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalarycode.UPDATE_SPNAME, true);
           //if (Convert.ToInt16(c) == 1)
           //{
           //    status = true;
           //}
           //if (status)
           //{
           //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
           //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
           //}
           //else
           //{
           //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
           //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
           //}
           //return status;
       }
       public static bool DeleteCode(ref DVOSalaryCode objSalaryCode, ref object objTransaction)
       {
           bool status = false;
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           bool statusObjTransaction = true;
           if (objTransaction == null)
           {
               objTransaction = objDALBaseClassHelper.GetTransactionObject();
               statusObjTransaction = false;
           }
           try
           {
               object[] parameters = new object[1];
               parameters[0] = objSalaryCode.Code;
               object c = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objSalaryCode.DELETE_SPNAME, true);
               if (c == null)
                   throw new Exception();
               else if (Convert.ToInt16(c) < 1)
                   throw new Exception();

               if(Convert.ToInt16 (c)==1)
               {
               if (!statusObjTransaction)
                   objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                   status=true;
               }
              // return 
               //if (c != null)
               //{
               //    if (Convert.ToInt16(c) == 1)
               //    {
               //        status = true;
               //    }
               //    else { status = false; }
               //}
               //else
               //{ status = false; }
               //if (status)
               //{
               //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
               //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
               //}
               //else
               //{
               //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
               //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
               //}
               //return status;
           }
           catch (Exception ex)
           {
               if (!statusObjTransaction)
                   objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
               ExceptionManagement.ExceptionManager.Publish(ex);
               status = false;
               throw ex;
           }
           return status;

       }
       public static DataSet CheckSalaryCode(ref DVOSalaryCode objDVOSalaryCode)
       {
           Object[] parameters = new object[1];
           parameters[0] = objDVOSalaryCode.Code;

           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSalaryCode), objDVOSalaryCode.FIND_DETAIL);
           return ds;
       }
    }
}
