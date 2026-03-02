using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    //Implemented By: Sunil Pahwa
    public class BLLAccountGroups
    {
        public static List<DVOAccountGroups> GetAccountgrps(ref DVOAccountGroups objgetaccgrp)
        {
            object[] Parameter = new object[3];
            Parameter[0] = objgetaccgrp.grp_key;
            Parameter[1] = objgetaccgrp.grp_desc;
            Parameter[2] = objgetaccgrp.Rowid;
            List<DVOAccountGroups> lstAccountGrps = new List<DVOAccountGroups>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAccountGroups)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOAccountGroups obj = new DVOAccountGroups();

                    obj.grp_key = (dr[0] != DBNull.Value) ? dr[0].ToString().Trim() : string.Empty;//"p_grpkey"
                    obj.grp_desc = (dr[1] != DBNull.Value) ? dr[1].ToString().Trim() : string.Empty;//"p_grpdesc"
                    obj.Rowid = (dr[2] != DBNull.Value) ? Convert.ToInt32(dr[2]) : 0;//"p_rowid"
                    lstAccountGrps.Add(obj);
                }
                return lstAccountGrps;
            }
        }

        public static int DeleteAccountGrp(ref object objTransaction, ref DVOAccountGroups objAccountGrpDel)
        {
            object obj = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] Parameter = new object[1];
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                Parameter[0] = objAccountGrpDel.grp_key;

                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref  objTransaction, ref Parameter, objAccountGrpDel.DELETE_SPNAME);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
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

        public static object InsertAccInfo(ref object objTransaction, ref DVOAccountGroups objInsertgrp, ref List<DVOAccountGroups> lstgetAccgrps)
        {
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
                object[] Parameter = new object[2];
                Parameter[0] = objInsertgrp.grp_key;
                Parameter[1] = objInsertgrp.grp_desc;
                DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOAccountGroups));
                int c = 0; int i = 0;
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        c = Convert.ToInt32(ds.Tables[0].Rows[0][0]);//""

                if (c > 0)
                {
                    //foreach (DVOAccountGroups obj in lstgetAccgrps)
                    //    obj.grp_key = grpkey.ToString();

                    i = BLLAccountGroups.InsertAccDtlInfo(ref objTransaction, ref lstgetAccgrps);
                }
                if (i == 1)
                {
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                    if (!statusObjTransaction)
                    {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
            return 1;
        }

        private static int InsertAccDtlInfo(ref object objTransaction, ref List<DVOAccountGroups> lstgetAccgrps)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool transObjectStatus = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                transObjectStatus = false;
            }
            try
            {

                foreach (DVOAccountGroups obj in lstgetAccgrps)
                {
                    object[] parameters = new object[2];
                    parameters[0] = obj.grp_key;
                    parameters[1] = obj.acct_no;

                    object i = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), obj.INSERT_ACCOUNT_GROUP_DETAIL);
                    if (i != null)
                    {
                        if (i.ToString() != "1")
                        {
                            return 0;
                        }
                    }
                    else { return 0; }
                }
                if (!transObjectStatus)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!transObjectStatus)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return 0;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                    return 0;
                }
            }
            return 1;
        }


        //public static int UpdateInfo(ref object objTransaction, ref DVOAccountGroups objAccgrpsUpd, ref List<DVOAccountGroups> lstgetAccgrps)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        object[] Parameter = new object[3];
        //        Parameter[0] = objAccgrpsUpd.grp_key;
        //        Parameter[1] = objAccgrpsUpd.grp_desc;
        //        Parameter[2] = objAccgrpsUpd.Rowid;
        //        object c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOAccountGroups), true);
        //        if (c != null)
        //        {
        //            if (c.ToString() == "1")
        //            {
        //                int i = BLLAccountGroups.UpdateInfoDetailAGroups(ref objTransaction, ref lstgetAccgrps);
        //                if (i == 1)
        //                {
        //                    if (!statusObjTransaction)
        //                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //                }
        //                else
        //                {
        //                    if (!statusObjTransaction)
        //                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                    return 0;
        //                }
        //            }
        //            else
        //            {
        //                if (!statusObjTransaction)
        //                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            if (!statusObjTransaction)
        //                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManager.Publish(ex);
        //        return 0;
        //    }
        //    return 1;
        //}

        //private static int UpdateInfoDetailAGroups(ref object objTransaction, ref List<DVOAccountGroups> lstgetAccgrps)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    bool transObjectStatus = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        transObjectStatus = false;
        //    }
        //    try
        //    {
        //        if (lstgetAccgrps.Count > 0)
        //        {
        //            object[] parameters = new object[1];
        //            parameters[0] = lstgetAccgrps[0].grp_key;
        //            //Deleteting
        //            object obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, lstgetAccgrps[0].DELETE_ACCOUNT_GROUP_DETAIL);
        //            if (obj != null)
        //            {
        //                if (Convert.ToInt16(obj) == 1)
        //                {
        //                    //Inserting
        //                    int i = InsertAccDtlInfo(ref objTransaction, ref lstgetAccgrps);
        //                    if (i == 1)
        //                    {
        //                        //objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), obj.uspAccGrpsDtlupd);

        //                        if (!transObjectStatus)
        //                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //                        return 1;
        //                    }
        //                    else
        //                    {
        //                        if (!transObjectStatus)
        //                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                        return 0;
        //                    }
        //                }
        //                else
        //                {
        //                    if (!transObjectStatus)
        //                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                    return 0;
        //                }
        //            }
        //            else
        //            {
        //                if (!transObjectStatus)
        //                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                return 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!transObjectStatus)
        //        {
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            ExceptionManager.Publish(ex);
        //            return 0;
        //        }
        //        else
        //        {
        //            ExceptionManager.Publish(ex);
        //            return 0;
        //        }
        //    }
        //    return 1;
        //}

        public static List<DVOAccountGroups> GetAccountGroupDetailInfo(ref DVOAccountGroups nobjDVOAccountGroups)
        {
            object[] parameter = new object[1];
            parameter[0] = nobjDVOAccountGroups.grp_key;
            List<DVOAccountGroups> lst = new List<DVOAccountGroups>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //using (DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOSecCompany), (new DVOSecCompany()).uspCompDetail))
            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOAccountGroups), nobjDVOAccountGroups.uspAccGrpsDtlget))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOAccountGroups obj = new DVOAccountGroups();
                    obj.grp_key = dr[0].ToString().Trim();//"p_grpkey"
                    obj.acct_type = dr[1].ToString().Trim();//"v_acct_type"
                    obj.keyvalue = dr[2].ToString().Trim();//"v_acct_desc"
                    obj.acct_desc = dr[3].ToString().Trim();//"v_keyvalue"
                    //dr[4]is accounttypeid
                    obj.acct_no = (dr[5] != DBNull.Value) ? Convert.ToInt32(dr[5]) : 0;
                    obj.Rowid = (dr[6] != DBNull.Value) ? Convert.ToInt32(dr[6]) : 0;

                    lst.Add(obj);
                }
                return lst;
            }
        }

        public static int UpdateData(ref object objTransaction, ref DVOAccountGroups objAccgrpsUpd,
            ref List<DVOAccountGroups> listNewDVOUpdateAccountGroups, ref List<DVOAccountGroups> listUpdateDVOUpdateAccountGroups,
            ref List<DVOAccountGroups> listDeleteDVOUpdateAccountGroups)
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
                object[] parameters = new object[3];
                parameters[0] = objAccgrpsUpd.grp_key;
                parameters[1] = objAccgrpsUpd.grp_desc;
                parameters[2] = objAccgrpsUpd.Rowid;
                //parameters[3] = objinvShippedStiselleUpd.sell_desc;
                //parameters[4] = objinvShippedStiselleUpd.cust_code;
                //parameters[5] = objinvShippedStiselleUpd.ok_post;

                //parameters[6] = objinvShippedStiselleUpd.UpdateBy;
                //parameters[7] = objinvShippedStiselleUpd.UpdateDate;
                //parameters[8] = objinvShippedStiselleUpd.UpdateMachineInfo;



                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objAccgrpsUpd.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception("Error occured during updation of Iventory-Shipped header.");
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception("Error occured during updation of Iventory-Shipped header.");


                if (listNewDVOUpdateAccountGroups.Count > 0)
                    BLLAccountGroups.InsertNewAcctGrpDtl(ref objTransaction, ref listNewDVOUpdateAccountGroups);
                if (listDeleteDVOUpdateAccountGroups.Count > 0)
                    BLLAccountGroups.DeleteAcctGrpDetail(ref objTransaction, ref listDeleteDVOUpdateAccountGroups);
                if (listUpdateDVOUpdateAccountGroups.Count > 0)
                    BLLAccountGroups.UpdateAcctGrpDetail(ref objTransaction, ref listUpdateDVOUpdateAccountGroups);

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

        public static object UpdateAcctGrpDetail(ref object objTransaction, ref List<DVOAccountGroups> listUpdateDVOUpdateAccountGroups)
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
                if (listUpdateDVOUpdateAccountGroups.Count > 0)
                    foreach (DVOAccountGroups objDVOAccountGroupsUPD in listUpdateDVOUpdateAccountGroups)
                    {
                        object[] parameters = new object[3];
                        parameters[0] = objDVOAccountGroupsUPD.grp_key;
                        parameters[1] = objDVOAccountGroupsUPD.acct_no;
                        parameters[2] = objDVOAccountGroupsUPD.Rowid;
                       

                        //Parameters used For Only SQL Server             
                        //parameters[7] = objDVOAdInventorystiadjmd.UpdateMachineInfo;
                        //parameters[8] = objDVOAdInventorystiadjmd.UpdateDate;
                        //parameters[9] = objDVOAdInventorystiadjmd.UpdateBy;

                        obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), objDVOAccountGroupsUPD.UPD_ACCOUNT_GROUP_DETAILS);//Chk
                        parameters = null;
                    }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return Convert.ToInt32(obj);
            }
            return Convert.ToInt32(obj);





        }

        public static object DeleteAcctGrpDetail(ref object objTransaction, ref List<DVOAccountGroups> listDeleteDVOUpdateAccountGroups)
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
                foreach (DVOAccountGroups obDVOAccountGroupsDEL in listDeleteDVOUpdateAccountGroups)
                {
                    object[] parameters = new object[1];
                    parameters[0] = obDVOAccountGroupsDEL.Rowid;
                    obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), obDVOAccountGroupsDEL.DEL_ACCOUNT_GROUP_DETAILS);//chek proc
                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                else
                    throw ex;
                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;


        }

        public static object InsertNewAcctGrpDtl(ref object objTransaction, ref List<DVOAccountGroups> listNewDVOUpdateAccountGroups)
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

                foreach (DVOAccountGroups obDVOAccountGroups in listNewDVOUpdateAccountGroups)
                {
                    object[] parameters = new object[2];
                    parameters[0] = obDVOAccountGroups.grp_key;
                    parameters[1] = obDVOAccountGroups.acct_no;

                    //Parameters used For Only SQL Server
                    //parameters[7] = objDVOAdjustInventorystiadjmd.UpdateMachineInfo;
                    //parameters[8] = objDVOAdjustInventorystiadjmd.UpdateDate;
                    //parameters[9] = objDVOAdjustInventorystiadjmd.UpdateBy;

                    obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAccountGroups), obDVOAccountGroups.INSERT_ACCOUNT_GROUP_DETAIL);

                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                else
                    throw ex;

                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;

        }


    }
}
