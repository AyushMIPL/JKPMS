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
    ///1.) BLL For Get and Line Type from stultypr       Rahul Jain                                    06/05/2008(DD)
    ///2.) 
    ///<summery>
    public class BLLLineTypestultypr
    {
        //Get All Data From stultypr table 
        public static List<DVOLineTypestultypr> GetLineType()
        {
            List<DVOLineTypestultypr> listDVOstultypr = new List<DVOLineTypestultypr>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOLineTypestultypr)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOLineTypestultypr obj_DVOstultypr = new DVOLineTypestultypr();
                    obj_DVOstultypr.RowID = Convert.ToInt32(dr[0]);//rowid
                    obj_DVOstultypr.line_type = dr[1].ToString().Trim();
                    obj_DVOstultypr.line_desc = dr[2].ToString().Trim();
                    obj_DVOstultypr.gl_acct_no = Convert.ToInt32(dr[3]);
                    obj_DVOstultypr.line_item_type = dr[4].ToString().Trim();
                    obj_DVOstultypr.update_description = dr[5].ToString().Trim();
                    obj_DVOstultypr.update_price = dr[6].ToString().Trim();
                    listDVOstultypr.Add(obj_DVOstultypr);
                }
            }
            return listDVOstultypr;
        }

        public static int Insert_Line_Type_Info(DVOLineTypestultypr objDvoLintTypeIns)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = null;
            objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {

                object obj = null;
                object[] parameter = new object[6];
                parameter[0] = objDvoLintTypeIns.line_type;
                parameter[1] = objDvoLintTypeIns.line_desc;
                parameter[2] = objDvoLintTypeIns.gl_acct_no;

                parameter[3] = objDvoLintTypeIns.line_item_type;
                parameter[4] = objDvoLintTypeIns.update_description;
                parameter[5] = objDvoLintTypeIns.update_price;

                //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction,ref parameter, typeof(DVOLineTypestultypr),true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
                    throw new Exception();
                else
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        //

        //
        //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        // bool statusObjTransaction = true;
        // if (objTransaction == null)
        // {
        //     objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //     statusObjTransaction = false;
        // }
        // DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        // try
        // {
        //     int success = 0;
        //     object[] UpdParameters = new object[6];
        //     UpdParameters[0] = objDvoUpdateSalPosIns.code;
        //     UpdParameters[1] = objDvoUpdateSalPosIns.desc;
        //     UpdParameters[2] = objDvoUpdateSalPosIns.dflt_cat_code;
        //     UpdParameters[3] = objDvoUpdateSalPosIns.dflt_scale_code;
        //     UpdParameters[4] = objDvoUpdateSalPosIns.dflt_py_acct_type;
        //     UpdParameters[5] = objDvoUpdateSalPosIns.Rowid;
        //    // int c = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOUpdPurchaseDef));
        //     success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameters, typeof(DVOUpdateSalaryPositions));

        //     UpdParameters = null;
        //    // BLLUpdPurchaseDef.UpdateInfoDetail(ref objTransaction, ref lstDVOUpdPurchaseDetail);
        //     //objDALBaseClass = null;

        //     if (!statusObjTransaction)
        //         objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //     return 1;
        // }
        // catch (Exception ex)
        // {
        //     ExceptionManagement.ExceptionManager.Publish(ex);
        //     if (!statusObjTransaction)
        //         objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //     throw ex;
        // }
        // return 0;
        //
        //
        public static int Update_Line_Type_Info(ref object objTransaction,DVOLineTypestultypr objDvoLintTypeUpd)
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
                 object  success = null;
                 object[] UpdParameter = new object[6];

                 UpdParameter[0] = objDvoLintTypeUpd.line_desc;
                 UpdParameter[1] = objDvoLintTypeUpd.gl_acct_no;
                 UpdParameter[2] = objDvoLintTypeUpd.line_item_type;
                 UpdParameter[3] = objDvoLintTypeUpd.update_description;
                 UpdParameter[4] = objDvoLintTypeUpd.update_price;
                 UpdParameter[5] = objDvoLintTypeUpd.line_type;



                 success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameter, typeof(DVOLineTypestultypr),true);
                 if (success == null)
                     throw new Exception();
                 else if (Convert.ToInt16(success) < 1)
                     throw new Exception();
                 if (!statusObjTransaction)
                     objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                 UpdParameter = null;
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

        public static List<DVOLineTypestultypr> Get_Lint_Type_Info(ref DVOLineTypestultypr SearchLintTypeInfo)
        {
            object[] parameters = new object[6];
            parameters[0] = SearchLintTypeInfo.line_type;
            parameters[1] = SearchLintTypeInfo.line_desc;
            parameters[2] = SearchLintTypeInfo.line_item_type;

            parameters[3] = SearchLintTypeInfo.update_description;
            parameters[4] = SearchLintTypeInfo.update_price;
            parameters[5] = SearchLintTypeInfo.RowID;

            List<DVOLineTypestultypr> lstDVOLineTyptultypr = new List<DVOLineTypestultypr>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOLineTypestultypr)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOLineTypestultypr objDvoLintTStultypr = new DVOLineTypestultypr();
                    if (!Convert.IsDBNull(dr[0])) objDvoLintTStultypr.line_type = Convert.ToString(dr[0]);
                    if (!Convert.IsDBNull(dr[1])) objDvoLintTStultypr.line_desc = dr[1].ToString().Trim();
                    if (!Convert.IsDBNull(dr[2])) objDvoLintTStultypr.gl_acct_no = Convert.ToInt32(dr[2]);
                    if (!Convert.IsDBNull(dr[3])) objDvoLintTStultypr.line_item_type = Convert.ToString(dr[3]);
                    if (!Convert.IsDBNull(dr[4])) objDvoLintTStultypr.update_description = dr[4].ToString().Trim();
                    if (!Convert.IsDBNull(dr[5])) objDvoLintTStultypr.update_price = Convert.ToString (dr[5]);
                    if (!Convert.IsDBNull(dr[6])) objDvoLintTStultypr.account_type= dr[6].ToString().Trim();
                    if (!Convert.IsDBNull(dr[7])) objDvoLintTStultypr.keyvalue = Convert.ToString(dr[7].ToString().Trim());
                    if (!Convert.IsDBNull(dr[8])) objDvoLintTStultypr.acct_desc = Convert.ToString(dr[8].ToString().Trim());
                    if (!Convert.IsDBNull(dr[9])) objDvoLintTStultypr.RowID = Convert.ToInt32(dr[9]);
                    lstDVOLineTyptultypr.Add(objDvoLintTStultypr);
                }
            }

            return lstDVOLineTyptultypr;
        }

        

        public static int Delete_Line_Type_Info(ref object  objTransaction ,DVOLineTypestultypr objDvoLintTypeStultyprDel)
        {
            object Delobj = null;
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
                object[] Parameter = new object[1];
                Parameter[0] = objDvoLintTypeStultyprDel.RowID;
                //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                Delobj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOLineTypestultypr), true);
                if (Delobj == null)
                    throw new Exception();
                else if (Convert.ToInt16(Delobj) < 1)
                    throw new Exception();
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
