using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;

namespace JKPS.BLL
{
    public class BLLUpdateIncomeCodes
    {

        public static List<DVOUpdateIncCode> GetDetailedInfo(ref DVOUpdateIncCode obj_DVOUpdateIncCode)
        {  
             object[] parameters=new object[13];
              parameters[0] = obj_DVOUpdateIncCode.Rowid;
              parameters[1]= obj_DVOUpdateIncCode.inc_code;
              parameters[2] = obj_DVOUpdateIncCode.description;
              parameters[3] = obj_DVOUpdateIncCode.dflt_num;
              parameters[4] = obj_DVOUpdateIncCode.dflt_rate;
              parameters[5] = obj_DVOUpdateIncCode.dflt_hours;
              parameters[6] = obj_DVOUpdateIncCode.dflt_acct;
              parameters[7] = obj_DVOUpdateIncCode.dflt_dept;
              parameters[8] = obj_DVOUpdateIncCode.inc_type;
              parameters[9] = obj_DVOUpdateIncCode.dflt_lo_inc_amt;
              parameters[10] = obj_DVOUpdateIncCode.dflt_hi_inc_amt;
              parameters[11] = obj_DVOUpdateIncCode.non_qual;//Pass account number null
              parameters[12] = obj_DVOUpdateIncCode.dfltaccounttype;
              


            List<DVOUpdateIncCode> ListobjDVOUpdateIncCode = new List<DVOUpdateIncCode>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateIncCode)))
            {
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
                            objDVOUpdateIncCode.inc_code = dr[0].ToString().Trim();
                            if (dr[1] != DBNull.Value)
                            {
                                objDVOUpdateIncCode.description = dr[1].ToString().Trim();
                            }
                            objDVOUpdateIncCode.dflt_num = (dr[2] != DBNull.Value ? (decimal?)(dr[2]) : null  );
                            objDVOUpdateIncCode.dflt_rate = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                            objDVOUpdateIncCode.dflt_hours = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);

                            objDVOUpdateIncCode.dflt_acct = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);
                            objDVOUpdateIncCode.dflt_dept = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);
                            if (dr[7] != DBNull.Value)
                            {
                                objDVOUpdateIncCode.inc_type = dr[7].ToString().Trim();
                            }
                            objDVOUpdateIncCode.dflt_lo_inc_amt = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                            objDVOUpdateIncCode.dflt_hi_inc_amt = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                            if (dr[10] != DBNull.Value)
                            {
                                objDVOUpdateIncCode.non_qual = dr[10].ToString().Trim();
                            }
                            if (dr[11] != DBNull.Value)
                            {
                                objDVOUpdateIncCode.dfltaccounttype = dr[11].ToString();
                            }
                            objDVOUpdateIncCode.Rowid = Convert.ToInt32(dr[12].ToString());
                            ListobjDVOUpdateIncCode.Add(objDVOUpdateIncCode);

                        }
                return ListobjDVOUpdateIncCode;
            }
        }
        //public static object UpdateIcomeCode(ref DVOUpdateIncCode objDVOUpdateIncCode)
        //{
           
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    object obj = null;
        //    try
        //    {
        //        object[] UpdateParameters = new object[11];
        //        UpdateParameters[0] = objDVOUpdateIncCode.inc_code;
        //        UpdateParameters[1] = objDVOUpdateIncCode.description;
        //        UpdateParameters[2] = objDVOUpdateIncCode.dflt_num;
        //        UpdateParameters[3] = objDVOUpdateIncCode.dflt_rate;
        //        UpdateParameters[4] = objDVOUpdateIncCode.dflt_hours;
        //        UpdateParameters[5] = objDVOUpdateIncCode.dflt_lo_inc_am;
        //        UpdateParameters[6] = objDVOUpdateIncCode.dflt_hi_inc_amt;
        //        UpdateParameters[7] = objDVOUpdateIncCode.inc_type;
        //        UpdateParameters[8] = objDVOUpdateIncCode.non_qual;
        //        UpdateParameters[9] = 0;//Pass account number null //Updated by sanjay
        //        UpdateParameters[10] = objDVOUpdateIncCode.dfltaccounttype;

        //        obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref UpdateParameters, (new DVOUpdateIncCode()).UPDATE_SPNAME);
        //        if (obj == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(obj) < 1)
        //            throw new Exception();

        //        if (objDVOUpdateIncCode.dfltaccounttype != string.Empty && objDVOUpdateIncCode.dfltkeyvalue != string.Empty)
        //        {
        //            //Updated by sanjay                
        //            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        //            objDVOFlexSegCommon.EntityType = "MasterIncCodes";
        //            objDVOFlexSegCommon.Code = objDVOUpdateIncCode.inc_code;
        //            objDVOFlexSegCommon.AccountType = objDVOUpdateIncCode.dfltaccounttype;
        //            objDVOFlexSegCommon.keyvalue = objDVOUpdateIncCode.dfltkeyvalue;
        //            BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);
                   
        //        }
        //        UpdateParameters = null;
        //        objDalBaseClass = null;

        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (objTransaction != null)
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return obj;
        //    }
        //    return obj;
        
        //}
        public static object InsertIncomeData(ref DVOUpdateIncCode objDVOUpdateIncCode)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] InsertParameters = new object[14];
                InsertParameters[0] = objDVOUpdateIncCode.inc_code;
                InsertParameters[1] = objDVOUpdateIncCode.description;
                InsertParameters[2] = objDVOUpdateIncCode.dflt_num;
                InsertParameters[3] = objDVOUpdateIncCode.dflt_rate;
                InsertParameters[4] = objDVOUpdateIncCode.dflt_hours;
                InsertParameters[5] = objDVOUpdateIncCode.dflt_acct;
                InsertParameters[6] = objDVOUpdateIncCode.dflt_dept;
                InsertParameters[7] = objDVOUpdateIncCode.inc_type;
                InsertParameters[8] = objDVOUpdateIncCode.dflt_lo_inc_amt;
                InsertParameters[9] = objDVOUpdateIncCode.dflt_hi_inc_amt;
                InsertParameters[10] = objDVOUpdateIncCode.non_qual;//Pass account number null
                InsertParameters[11] = objDVOUpdateIncCode.dfltaccounttype;
                InsertParameters[12] = null;
                InsertParameters[13] = null;

                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref InsertParameters, (new DVOUpdateIncCode()).INSERT_SPNAME);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                if(objDVOUpdateIncCode.dfltaccounttype!=string.Empty && objDVOUpdateIncCode.dfltkeyvalue!=string.Empty)
                {
                //Updated by sanjay                
                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "styinccr";
                objDVOFlexSegCommon.Code = objDVOUpdateIncCode.inc_code;
                objDVOFlexSegCommon.AccountType = objDVOUpdateIncCode.dfltaccounttype;
                objDVOFlexSegCommon.keyvalue = objDVOUpdateIncCode.dfltkeyvalue;
                BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);
                //return c;
                }
                InsertParameters = null;
                objDalBaseClass = null;

                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;

        }
        public static object DeleteIncomeData(ref DVOUpdateIncCode objDVOUpdateIncCode)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            object obj = null;
            try
            {
                object[] DeleteParameters = new object[1];
                DeleteParameters[0] = objDVOUpdateIncCode.inc_code;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref DeleteParameters, (new DVOUpdateIncCode()).DELETE_SPNAME);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();

                DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                objDVOFlexSegCommon.EntityType = "MasterIncCodes";
                objDVOFlexSegCommon.Code = objDVOUpdateIncCode.inc_code;
                BLLFlexSegCommon.Flexseg_delete(ref objDVOFlexSegCommon);

                DeleteParameters = null;
                objDalBaseClass = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;
        }

        public static List<DVOUpdateIncCode> IncCodeGetAll()
        {
            object[] parameters = new object[0];
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOUpdateIncCode> listDVOUpdateIncCode = new List<DVOUpdateIncCode>();
            DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objDVOUpdateIncCode.ALL_SPNAME))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateIncCode obj = new DVOUpdateIncCode();//EmpTcardID v_rowid,card_no v_card_no,empl_code v_empl_code,empl_name v_empl_name,start_date v_start_date,end_date v_end_date,used_flag v_used_flag
                    obj.inc_code = dr[1].ToString();
                    obj.description = dr[2].ToString();
                    obj.dflt_num = DBNull.Value.Equals(dr[3]) ? 0 : Convert.ToDecimal(dr[3]);
                    obj.dflt_rate = DBNull.Value.Equals(dr[4]) ? 0 : Convert.ToDecimal(dr[4]);
                    obj.dflt_hours = DBNull.Value.Equals(dr[5]) ? 0 : Convert.ToInt32(dr[5]);
                    //obj.acct_no = dr[6].ToString();
                    obj.inc_type = DBNull.Value.Equals(dr[7]) ? "" : dr[7].ToString();
                    obj.dflt_lo_inc_amt = DBNull.Value.Equals(dr[8]) ? 0 : Convert.ToDecimal(dr[8]);
                    obj.dflt_hi_inc_amt = DBNull.Value.Equals(dr[9]) ? 0 : Convert.ToDecimal(dr[9]);

                    listDVOUpdateIncCode.Add(obj);
                }
                return listDVOUpdateIncCode;
            }
        }

        public static object UpdIcomeCode(ref object objTransaction, ref DVOUpdateIncCode objDVOUpdateIncCode)
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
                 object[] UpdateParameters = new object[11];
                 UpdateParameters[0] = objDVOUpdateIncCode.inc_code;
                 UpdateParameters[1] = objDVOUpdateIncCode.description;
                 UpdateParameters[2] = objDVOUpdateIncCode.dflt_num;
                 UpdateParameters[3] = objDVOUpdateIncCode.dflt_rate;
                 UpdateParameters[4] = objDVOUpdateIncCode.dflt_hours;
                 UpdateParameters[5] = objDVOUpdateIncCode.dflt_lo_inc_amt;
                 UpdateParameters[6] = objDVOUpdateIncCode.dflt_hi_inc_amt;
                 UpdateParameters[7] = objDVOUpdateIncCode.inc_type;
                 UpdateParameters[8] = objDVOUpdateIncCode.non_qual;
                 UpdateParameters[9] = 0;//Pass account number null //Updated by sanjay
                 UpdateParameters[10] = objDVOUpdateIncCode.dfltaccounttype;
                 
                 //obj = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref parameters, typeof(DVOSecRole));
                 obj = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref UpdateParameters, (new DVOUpdateIncCode()).UPDATE_SPNAME);
                
                 if (obj == null)
                     throw new Exception();
                 else if (Convert.ToInt32(obj) < 1)
                     throw new Exception();

                 if (objDVOUpdateIncCode.dfltaccounttype != string.Empty && objDVOUpdateIncCode.dfltkeyvalue != string.Empty)
                 {
                     //Updated by sanjay                
                     DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                     objDVOFlexSegCommon.EntityType = "styinccr";
                     objDVOFlexSegCommon.Code = objDVOUpdateIncCode.inc_code;
                     objDVOFlexSegCommon.AccountType = objDVOUpdateIncCode.dfltaccounttype;
                     objDVOFlexSegCommon.keyvalue = objDVOUpdateIncCode.dfltkeyvalue;
                     BLLFlexSegCommon.FunctionFlexSeg_Add(ref objDVOFlexSegCommon);

                 }
                 UpdateParameters = null;
                // objDalBaseClass = null;
                 if (!statusObjTransaction)
                     objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                 return obj;
             }
             catch (Exception ex)
             {
                 if (!statusObjTransaction)
                     objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                 ExceptionManagement.ExceptionManager.Publish(ex);
                 return obj;
             }
             return obj;







            //    if (!statusObjTransaction)
            //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    if (!statusObjTransaction)
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    throw ex;
            //}
            //return success;





        }
    }
}
