using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
namespace JKPS.BLL
{
    public class BLLGeneralLibrary
    {
        /// <summary>
        /// Implemented by :Sanjay chawla
        /// Date : 07/08/2008
        /// Description : This class basically used for interacting with Data Access Layer and gets the data requested from Form
        /// Modified by:
        /// Modified Date :
        /// Description :
        /// </summary>
        
        /// <summary>
        /// This method is use to get current period information from database 
        /// </summary>
        /// <param name="objAccountTypeMaintenance">reference of DVOGLBeginPeriod type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        /// 
        //To hold the budget for new period
        public float budget;
        public static DateTime rev_date;
        public static string err_desc = string.Empty;
        public static DVOUpdateLedgerDefaults objDVOUpdateLedgerDefaults = new DVOUpdateLedgerDefaults();
        public static bool IsNewYear = false;
        
        public static DataSet GetCurrentPeriod()
        {
            Object[] parameters = new object[0];

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet dsExpExp = objDalBaseClass.GetData(ref parameters, typeof(DVOGLBeginPeriod),(new DVOGLBeginPeriod()).Get_Current_Period);            
            return dsExpExp;
        }
        public static DataSet Get_stxprdr(DateTime date)
        {
            Object[] parameters = new object[1];
            parameters[0] = date;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds= objDalBaseClass.GetData(ref parameters, typeof(DVOGLBeginPeriod), (new DVOGLBeginPeriod()).Get_stxprdr);
            return ds;
        }

        //public static bool CreateNewPeriod(ref DVOGLBeginPeriod objBeginPeriod)
        //{
        //    //Get current period account information to carry forward for new period
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    DataSet Current_Dataset = new DataSet();
        //    objDVOUpdateLedgerDefaults.part = "H";
        //    objDVOUpdateLedgerDefaults = BLLUpdateLedgerDefaults.GetLedgerDefaultInfo(ref objDVOUpdateLedgerDefaults);
        //    try
        //    {
        //        Current_Dataset = GetStxchrtd(objDVOUpdateLedgerDefaults.curr_period, objDVOUpdateLedgerDefaults.curr_year);

        //        if (Current_Dataset.Tables[0].Rows.Count > 0)
        //        {
        //            DVOGLstxchrtd objDVOGLstxchrtd = new DVOGLstxchrtd();
        //            DataRow dr = null;
        //            object[] Updparameters = new object[5];
        //            object upd_stat = null;
        //            object[] budparameter = new object[4];
        //            object newbudget = null;
        //            for (int i = 0; i < Current_Dataset.Tables[0].Rows.Count; i++)
        //            {
                        
        //                dr = Current_Dataset.Tables[0].Rows[i];
        //                objDVOGLstxchrtd.acct_no = (dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0);
        //                objDVOGLstxchrtd.activity = (dr["activity"] != DBNull.Value ? Convert.ToDecimal(dr["activity"]) : 0);
        //                objDVOGLstxchrtd.balance = (dr["balance"] != DBNull.Value ? Convert.ToDecimal(dr["balance"]) : 0);
        //                objDVOGLstxchrtd.department = dr["department"].ToString();
        //                objDVOGLstxchrtd.period_month = dr["period_month"].ToString();
        //                objDVOGLstxchrtd.period_year = dr["period_year"].ToString();
        //                objDVOGLstxchrtd.this_month = (dr["this_month"] != DBNull.Value ? Convert.ToDecimal(dr["this_month"]) : 0);
        //                //objDVOGLstxchrtd.budget = (dr["budget"] != DBNull.Value ? Convert.ToDecimal(dr["budget"]) : 0);

        //                if (objDVOGLstxchrtd.activity == 0)
                        
        //                    objDVOGLstxchrtd.activity = objDVOGLstxchrtd.this_month;           
        //                else
        //                   objDVOGLstxchrtd.activity = objDVOGLstxchrtd.activity + objDVOGLstxchrtd.this_month;
                        
        //                objDVOGLstxchrtd.this_month = 0;
        //                dr["activity"] = objDVOGLstxchrtd.activity;
        //                dr["this_month"] = objDVOGLstxchrtd.this_month;
        //                //update stxchrtd
                       
        //                Updparameters[0] = objDVOGLstxchrtd.acct_no;
        //                Updparameters[1] = objDVOGLstxchrtd.department;
        //                Updparameters[2] = objDVOGLstxchrtd.period_month;
        //                Updparameters[3] = objDVOGLstxchrtd.period_year;
        //                Updparameters[4] = objDVOGLstxchrtd.activity;
        //                upd_stat = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref Updparameters, objBeginPeriod.UPDATE_STXCHRTD_CURR, true);

        //                if (Convert.ToInt32(upd_stat) != 1)
        //                {
        //                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                    err_desc = "An SQL error has occured while updating stxchrtd";
        //                    return false;
        //                }
        //                upd_stat = null;

        //                //Get budget for new period if it is already in stxchrtd                      
        //                //budparameter[0] = objDVOGLstxchrtd.acct_no;
        //                //budparameter[1] = objDVOGLstxchrtd.department;
        //                //budparameter[2] = objBeginPeriod.period;//new period
        //                //budparameter[3] = objBeginPeriod.period_year; //new year
        //                //newbudget = objDALBaseClass.ExecuteScalar(ref budparameter, objBeginPeriod.GET_BUDGET);
        //                //if (newbudget == DBNull.Value || newbudget.ToString() == string.Empty)
        //                //{

        //                //    objDVOGLstxchrtd.budget = 0;
        //                //}
        //                //else
        //                //{   //retain the budget amount, and delete the row
        //                //    //and delete from stxchrtd
        //                //    objDVOGLstxchrtd.budget = Convert.ToDecimal(newbudget);
        //                //    object[] delparameter = new object[4];
        //                //    delparameter[0] = objDVOGLstxchrtd.acct_no;
        //                //    delparameter[1] = objDVOGLstxchrtd.department;
        //                //    delparameter[2] = objBeginPeriod.period;//new period
        //                //    delparameter[3] = objBeginPeriod.period_year; //new year
        //                //    object del_stat = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref delparameter, objBeginPeriod.DELETE_STXCHRTD, true);
        //                //}
        //                //dr["budget"] = objDVOGLstxchrtd.budget;

        //                //prepare for insert
        //                objDVOGLstxchrtd.period_month = objBeginPeriod.period;//new period
        //                objDVOGLstxchrtd.period_year = objBeginPeriod.period_year; //new year
        //                objDVOGLstxchrtd.activity = 0;
        //                objDVOGLstxchrtd.this_month = 0;
        //                if (!InsertIntoStxchrtd(ref objTransaction, ref objDVOGLstxchrtd))
        //                {
        //                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                    err_desc = "An SQL error has occured while creating new period into stxchrtd";
        //                    return false;
        //                }

        //                //update the control and period tables.
        //                //on_last_row
        //                if (Current_Dataset.Tables[0].Rows.Count == i + 1)
        //                {
        //                    //update stgcntrc
        //                    object[] stgparameter = new object[4];
        //                    stgparameter[0] = objBeginPeriod.period;//new period
        //                    stgparameter[1] = objBeginPeriod.period_year; //new year
        //                    stgparameter[2] = objDVOUpdateLedgerDefaults.curr_period;
        //                    stgparameter[3] = objDVOUpdateLedgerDefaults.curr_year;
        //                    object upd_stg = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref stgparameter, objDVOUpdateLedgerDefaults.UPDATE_STGCNTRC, true);
        //                    if (Convert.ToInt32(upd_stg) != 1)
        //                    {
        //                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                        err_desc = "An SQL error has occured while posting to stgchtrc";
        //                        return false;
        //                    }
        //                    // create the new stxperdr row (remove old one if it exists)
        //                    object[] stxperdr_para = new object[8];
        //                    stxperdr_para[0] = objBeginPeriod.period;//new period
        //                    stxperdr_para[1] = objBeginPeriod.period_year; //new year
        //                    stxperdr_para[2] = objDVOUpdateLedgerDefaults.curr_period;
        //                    stxperdr_para[3] = objDVOUpdateLedgerDefaults.curr_year;
        //                    stxperdr_para[4] = objBeginPeriod.start_date;//new date
        //                    stxperdr_para[5] = objBeginPeriod.end_date;
        //                    stxperdr_para[6] = objBeginPeriod.balanced;
        //                    stxperdr_para[7] = objBeginPeriod.period_closed;
        //                    object Ins_Stat = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref stxperdr_para, objBeginPeriod.INSERT_STPRDRNEW, true);
        //                    if (Convert.ToInt32(Ins_Stat)!=1)
        //                    {
        //                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                        err_desc = "An SQL error has occured while posting to stxperdr";
        //                        return false;
        //                    }
        //                    //if it's the first period, insert the 00 period row
        //                    if (objBeginPeriod.period.Trim() == "01")
        //                    {
        //                        IsNewYear = true;
        //                        object[] parameter = new object[1];
        //                        parameter[0] = objBeginPeriod.period_year; //new year
        //                        DataSet dsstxprdr = objDALBaseClass.GetData(ref parameter, typeof(DVOGLBeginPeriod), objBeginPeriod.FIND_STXPERDR);
        //                        if (dsstxprdr.Tables[0].Rows.Count == 0)
        //                        {
        //                            object[] stxperdr_parameter = new object[8];
        //                            stxperdr_para[0] = "00";//new period
        //                            stxperdr_para[1] = objBeginPeriod.period_year; //new year
        //                            stxperdr_para[2] = objDVOUpdateLedgerDefaults.curr_period;
        //                            stxperdr_para[3] = objDVOUpdateLedgerDefaults.curr_year;
        //                            stxperdr_para[4] = objBeginPeriod.start_date;//new date
        //                            stxperdr_para[5] = objBeginPeriod.start_date;
        //                            stxperdr_para[6] = objBeginPeriod.balanced.Trim();
        //                            stxperdr_para[7] = objBeginPeriod.period_closed.Trim();
        //                            object Ins_Status = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref stxperdr_para, objBeginPeriod.INSERT_STPRDRNEW, true);
        //                            if (Convert.ToInt32(Ins_Status) != 1)
        //                            {
        //                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                                err_desc = "An SQL error has occured while posting to stxperdr";
        //                                return false;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            err_desc = "No Element to Process in Current Period";
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            return false;
        //        }
        //        if (!Create_NewPeriod1(ref objTransaction, out err_desc))
        //        {
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //            return false;
        //        }
        //        if (IsNewYear)
        //        {
        //            if (!Create_NewYear(ref objTransaction, out err_desc))
        //            {
        //                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //                return false;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //    return true;
        //}
        ////public static bool Create_NewPeriod1(ref object objTransaction, out string err_desc)
        //{
        //    err_desc = string.Empty;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    DVOGLBeginPeriod objBeginPeriod = new DVOGLBeginPeriod();
        //    try
        //    {
        //        DataSet dsSTGJOURE = objDALBaseClass.GetData(typeof(DVOGLBeginPeriod), objBeginPeriod.FIND_STGJOURE);
        //        dsSTGJOURE.Tables[0].Columns[0].ColumnName = "auto_rev";
        //        dsSTGJOURE.Tables[0].Columns[1].ColumnName = "doc_date";
        //        dsSTGJOURE.Tables[0].Columns[2].ColumnName = "doc_desc";
        //        dsSTGJOURE.Tables[0].Columns[3].ColumnName = "doc_no";
        //        dsSTGJOURE.Tables[0].Columns[4].ColumnName = "doc_src";
        //        dsSTGJOURE.Tables[0].Columns[5].ColumnName = "file_type";
        //        dsSTGJOURE.Tables[0].Columns[6].ColumnName = "posted";
        //        objDVOUpdateLedgerDefaults = BLLUpdateLedgerDefaults.GetLedgerDefaultInfo(ref objDVOUpdateLedgerDefaults);
        //        object[] parameter = new object[2];
        //        parameter[0] = objDVOUpdateLedgerDefaults.curr_period;
        //        parameter[1] = objDVOUpdateLedgerDefaults.curr_year;
        //        object _rev_date = objDALBaseClass.ExecuteScalar(ref parameter, objBeginPeriod.Find_StxperdrStartDate);
        //        if (_rev_date == DBNull.Value || _rev_date.ToString().Trim() == string.Empty)
        //        {
        //            rev_date = DVOApplicationUserInfo.CurrentDate;
        //        }
        //        else
        //        {
        //            rev_date = Convert.ToDateTime(_rev_date);
        //        }
        //        DataRow dr = null;
        //        DVOStgjoure objDVOStgjoure = new DVOStgjoure();
        //        for (int i = 0; i < dsSTGJOURE.Tables[0].Rows.Count; i++)
        //        {
        //            dr = dsSTGJOURE.Tables[0].Rows[i];
        //            objDVOStgjoure.doc_no = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0;
        //            objDVOStgjoure.auto_rev = dr["auto_rev"].ToString().Trim();
        //            objDVOStgjoure.doc_date = dr["doc_date"] != DBNull.Value ? Convert.ToDateTime(dr["doc_date"]) : Convert.ToDateTime(null);
        //            objDVOStgjoure.doc_desc = dr["doc_desc"].ToString().Trim();
        //            objDVOStgjoure.file_type = dr["file_type"].ToString().Trim();
        //            objDVOStgjoure.posted = dr["posted"].ToString().Trim();
        //            objDVOStgjoure.doc_src = dr["doc_src"].ToString().Trim();
        //            if (objDVOStgjoure.auto_rev.Trim() == "Y")
        //            {
        //                if (!reverse_doc(objDVOStgjoure, ref objTransaction))
        //                {
        //                    return false;
        //                }
        //            }
        //            object[] delparameter = new object[1];
        //            delparameter[0] = objDVOStgjoure.doc_no;
        //            object del_ststus = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref delparameter, typeof(DVOStgjoure), true);
        //            if (Convert.ToInt32(del_ststus) != 1)
        //            {
        //                err_desc = "An SQL error has occured while deleting from stgjoure";
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        err_desc = ex.Message;
        //        return false;
        //    }
        //    return true;

        //}
        ////public static bool Create_NewYear(ref object objTransaction, out string err_desc)
        //{
        //    err_desc = string.Empty;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    DVOGLBeginPeriod objBeginPeriod = new DVOGLBeginPeriod();
        //    try
        //    {
        //        DataSet dsnewyear = objDALBaseClass.GetAllData(typeof(DVOGLstxchrtd));
        //        dsnewyear.Tables[0].Columns[0].ColumnName = "acct_no";
        //        dsnewyear.Tables[0].Columns[1].ColumnName = "activity";
        //        dsnewyear.Tables[0].Columns[2].ColumnName = "balance";
        //        dsnewyear.Tables[0].Columns[3].ColumnName = "department";
        //        dsnewyear.Tables[0].Columns[4].ColumnName = "period_month";
        //        dsnewyear.Tables[0].Columns[5].ColumnName = "period_year";
        //        dsnewyear.Tables[0].Columns[6].ColumnName = "this_month";
        //        dsnewyear.Tables[0].Columns[7].ColumnName = "acct_cat";
        //        dsnewyear.Tables[0].Columns[8].ColumnName = "incr_with_crdt";

        //        #region before_firs_row...........
        //        objDVOUpdateLedgerDefaults.part = "H";
        //        objDVOUpdateLedgerDefaults = BLLUpdateLedgerDefaults.GetLedgerDefaultInfo(ref objDVOUpdateLedgerDefaults);
        //        if (objDVOUpdateLedgerDefaults.gl_balanced.Trim() != "Y")
        //        {
        //            err_desc = "GL has not been balanced";
        //            return false;
        //        }
        //        int stxtranr_doc_no = 1;
        //        int stxtranr_post_no = 1;
        //        decimal retained_earnings = 0;
               
        //        object[] STXPERDR_PARAMETER = new object[1];
        //        STXPERDR_PARAMETER[0] = objDVOUpdateLedgerDefaults.curr_year;
        //        DateTime stxperdr_start_date = Convert.ToDateTime(null);
        //        DataSet dsstxperdr = objDALBaseClass.GetData(ref STXPERDR_PARAMETER, typeof(DVOGLBeginPeriod), objBeginPeriod.FIND_STXPERDR);
        //        if (dsstxperdr.Tables[0].Rows.Count != 0)
        //        {
        //            if (dsstxperdr.Tables[0].Rows[0][2] != DBNull.Value)
        //                stxperdr_start_date = Convert.ToDateTime(dsstxperdr.Tables[0].Rows[0][2]);
        //        }
        //        DataSet dsstgtranr = objDALBaseClass.GetData(ref STXPERDR_PARAMETER, typeof(DVOGLBeginPeriod), objBeginPeriod.FIND_STGTRANR);
        //        if (dsstgtranr.Tables[0].Rows.Count > 0)
        //        {
        //            err_desc = "EOY process has been done before";
        //            return false;
        //        }
        //        //get next YE journal trx number & post number 

        //        DataSet dsstxtranr = objDALBaseClass.GetData(typeof(DVOGLBeginPeriod), objBeginPeriod.FIND_STXTRANR);
        //        if (dsstxtranr.Tables[0].Rows[0][0].ToString() != string.Empty)
        //            stxtranr_doc_no = Convert.ToInt32(dsstxtranr.Tables[0].Rows[0][0]);
        //        if (dsstxtranr.Tables[0].Rows[0][1].ToString() != string.Empty)
        //            stxtranr_post_no = Convert.ToInt32(dsstxtranr.Tables[0].Rows[0][1]);

        //        object[] Insstgtranr = new object[5];
        //        Insstgtranr[0] = "YE";
        //        Insstgtranr[1] = stxtranr_doc_no;
        //        Insstgtranr[2] = "00";
        //        Insstgtranr[3] = objDVOUpdateLedgerDefaults.curr_year;
        //        Insstgtranr[4] = "N";

        //        //post the stxtranr row...
        //        DVOPostTrx ObjPostTransactions = new DVOPostTrx();
        //        ObjPostTransactions.post_or_check = "POST";
        //        ObjPostTransactions.orig_journal = "YE";
        //        ObjPostTransactions.doc_no = stxtranr_doc_no;
        //        ObjPostTransactions.post_no = stxtranr_post_no;
        //        ObjPostTransactions.post_date = DVOApplicationUserInfo.CurrentDate;
        //        ObjPostTransactions.doc_date = stxperdr_start_date;
        //        ObjPostTransactions.ref_code = "AUTO";
        //        ObjPostTransactions.doc_desc = "YEAR END BALANCING DOCUMENT";
        //        //Modified by Sarvjeet On 14/05/2009 added a Transactions parameter into trx_post
        //        int status = BLLAccountingLiberary.trx_post(ref ObjPostTransactions, ref objTransaction);
        //        if (status == 1)
        //        {
        //            err_desc = "An SQL error has occured while posting to (stxtranr)";
        //            return false;
        //        }
        //        //post to the gtranr row...
        //        object Insresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref Insstgtranr, (new DVOGLTransH()).INSERT_STGTRANR, true);
        //        if (Convert.ToInt32(Insresult) != 1)
        //        {
        //            err_desc = "An SQL error has occured while posting to (stgtranr)";
        //            return false;
        //        }
        //        #endregion before_firs_row...........

        //        DVOGLstxchrtd objDVOGLstxchrtd = new DVOGLstxchrtd();
        //        DataRow dr = null;
        //        for (int i = 0; i < dsnewyear.Tables[0].Rows.Count; i++)
        //        {
        //            dr = dsnewyear.Tables[0].Rows[i];
        //            objDVOGLstxchrtd.acct_no = (dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0);
        //            objDVOGLstxchrtd.activity = (dr["activity"] != DBNull.Value ? Convert.ToDecimal(dr["activity"]) : 0);
        //            objDVOGLstxchrtd.balance = (dr["balance"] != DBNull.Value ? Convert.ToDecimal(dr["balance"]) : 0);
        //            objDVOGLstxchrtd.department = dr["department"].ToString();
        //            objDVOGLstxchrtd.period_month = dr["period_month"].ToString();
        //            objDVOGLstxchrtd.incr_with_crdt = dr["incr_with_crdt"].ToString().Trim();

        //            #region on_every_row..................

        //            if (objDVOGLstxchrtd.balance != 0)
        //            { 
        //                object[] stgactvdParameters = new object[6];
        //                stgactvdParameters[0] = "YE";
        //                stgactvdParameters[1] = stxtranr_doc_no;
        //                stgactvdParameters[2] = objDVOGLstxchrtd.acct_no;
        //                stgactvdParameters[3] = objDVOGLstxchrtd.department;
        //                stgactvdParameters[4] = objDVOGLstxchrtd.balance;
        //                if (objDVOGLstxchrtd.incr_with_crdt == "Y")
        //                {
        //                    stgactvdParameters[5] = "D";
        //                    retained_earnings = retained_earnings + objDVOGLstxchrtd.balance;
        //                }
        //                else
        //                {
        //                    stgactvdParameters[5] = "C";
        //                    retained_earnings = retained_earnings - objDVOGLstxchrtd.balance;
        //                }
        //                object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref stgactvdParameters, (new DVOGLTRanActVD()).INSERT_STGACTVD, true);
        //                if (Convert.ToInt32(result) != 1)
        //                {
        //                    err_desc = "An SQL error has occured while posting to (stgactvd)";
        //                    return false;
        //                }
        //            }
        //            #endregion on_every_row..................

        //            #region on_last_row...
        //            if (dsnewyear.Tables[0].Rows.Count == i + 1)
        //            {
        //                object[] stgactvdParameters1 = new object[6];
        //                stgactvdParameters1[0] = "YE";
        //                stgactvdParameters1[1] = stxtranr_doc_no;
        //                stgactvdParameters1[2] = objDVOUpdateLedgerDefaults.retain_earnings;
        //                stgactvdParameters1[3] = "000";
        //                stgactvdParameters1[4] = retained_earnings;
        //                stgactvdParameters1[5] = "C";
        //                object stgactvdresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref stgactvdParameters1, (new DVOGLTRanActVD()).INSERT_STGACTVD, true);

        //                if (Convert.ToInt32(stgactvdresult) != 1)
        //                {
        //                    err_desc = "An SQL error has occured while posting to (stgactvd)";
        //                    return false;
        //                }
        //            }
        //            #endregion on_last_row...
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        err_desc = ex.Message;
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool set_newPeriod(ref DVOGLBeginPeriod objBeginPeriod, ref object objTransaction)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    objDVOUpdateLedgerDefaults.part = "H";
        //    objDVOUpdateLedgerDefaults = BLLUpdateLedgerDefaults.GetLedgerDefaultInfo(ref objDVOUpdateLedgerDefaults);
        //    object[] parameters = new object[17];
        //    parameters[0] = string.Empty;   //@new_period char(2),
        //    parameters[1] = string.Empty;   //@new_period_year char(4),
        //    parameters[2] = "01/01/1900";   //@start_date datetime,
        //    parameters[3] = "01/01/1900";   //@end_date datetime,
        //    parameters[4] = string.Empty;   //@balanced char(1),
        //    parameters[5] = string.Empty;   //@period_closed char(1),
        //    parameters[6] = 0;              //@check int,
        //    parameters[7] = "01/01/1900";   //@curr_start_date datetime,
        //    parameters[8] = "01/01/1900";   //@curr_end_date datetime,
        //    parameters[9] = string.Empty;   //@curr_period char(2),
        //    parameters[10] = string.Empty;  //@curr_period_year char(4),
        //    parameters[11] = 0;             //@acct_no int,
        //    parameters[12] = string.Empty;  //@dept char(3),
        //    parameters[13]=0;               //@budget float(12,0),
        //    parameters[14]=0;               //@activity flaot(12,0),
        //    parameters[15]=0;               //@this_month float(12,0),
        //    parameters[16]=0;               //@balance float(12,0),
        //    try
        //    {
        //        //set values of parameters and pass to routines as per criteria
        //       //Insert into table(stxperdr) new period,year,startdate,enddate
        //        parameters[0] = objBeginPeriod.period;
        //        parameters[1] = objBeginPeriod.period_year;
        //        parameters[2] = objBeginPeriod.start_date;
        //        parameters[3] = objBeginPeriod.end_date;
        //        parameters[4] = objBeginPeriod.balanced;
        //        parameters[5] = objBeginPeriod.period_closed;                
        //        parameters[6] = objBeginPeriod.check;
        //        int result1 = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));
        //        if (result1 >= 0)
        //        {
        //            //set values of parameters and pass to routines as per criteria
        //            //update table(stxperdr) set current period to close(period_closed='C')
        //            parameters[6] = objBeginPeriod.check;
        //            parameters[7] = objBeginPeriod.curr_start_date;
        //            parameters[8] = objBeginPeriod.curr_end_date;
        //            int result2 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));
        //            if (result2 >= 0)
        //            {
                        
        //                //Get current period account information to carry forward for new period
        //                parameters[9] = objBeginPeriod.curr_period;
        //                parameters[10] = objBeginPeriod.curr_year;
        //                DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOGLBeginPeriod), objBeginPeriod.Get_Current_Period_Account);
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        //                    {
        //                        if (ds.Tables[0].Rows[i][1].ToString() != string.Empty)
        //                        {
        //                            int acct_no = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
        //                            string dept = ds.Tables[0].Rows[i][2].ToString();
        //                            decimal balance = (ds.Tables[0].Rows[i][6] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i][6]) : 0);//balance
        //                            decimal activity = (ds.Tables[0].Rows[i][5] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i][5]) : 0);//activity
        //                            decimal this_month = (ds.Tables[0].Rows[i][7] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i][7]) : 0);//this_month
        //                            objBeginPeriod.acct_no = acct_no;
        //                            objBeginPeriod.dept = dept;
        //                            objBeginPeriod.balance = balance;
        //                            objBeginPeriod.activity = activity;
        //                            objBeginPeriod.this_month = this_month;
        //                            parameters[0] = objBeginPeriod.period;
        //                            parameters[1] = objBeginPeriod.period_year;
        //                            parameters[11] = objBeginPeriod.acct_no;
        //                            parameters[12] = objBeginPeriod.dept;
        //                            DataSet dss = objDALBaseClass.GetData(ref parameters, typeof(DVOGLBeginPeriod), objBeginPeriod.Get_Budget);
        //                            if (dss.Tables[0].Rows.Count > 0)
        //                            {
        //                                parameters[0] = objBeginPeriod.period;
        //                                parameters[1] = objBeginPeriod.period_year;
        //                                parameters[11] = objBeginPeriod.acct_no;
        //                                parameters[12] = objBeginPeriod.dept;

        //                                //Delete the Budget record for new period, If previously defined and insert new record 
        //                                int result3 = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));
        //                                objBeginPeriod.budget = Convert.ToDecimal(dss.Tables[0].Rows[0][0]);
        //                                parameters[13] = objBeginPeriod.budget;
        //                                if (result3 >= 0)
        //                                {
        //                                    parameters[0] = objBeginPeriod.period;
        //                                    parameters[1] = objBeginPeriod.period_year;
        //                                    parameters[11] = objBeginPeriod.acct_no;
        //                                    parameters[12] = objBeginPeriod.dept;
        //                                    parameters[13] = objBeginPeriod.budget;
        //                                    parameters[14] = objBeginPeriod.activity;
        //                                    parameters[16] = objBeginPeriod.balance;
        //                                    parameters[15] = objBeginPeriod.this_month;
        //                                    objBeginPeriod.check = 2;
        //                                    parameters[6] = objBeginPeriod.check;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //budget = null;
        //                                parameters[0] = objBeginPeriod.period;
        //                                parameters[1] = objBeginPeriod.period_year;
        //                                parameters[11] = objBeginPeriod.acct_no;
        //                                parameters[12] = objBeginPeriod.dept;
        //                                parameters[13] = objBeginPeriod.budget;
        //                                parameters[14] = objBeginPeriod.activity;
        //                                parameters[16] = objBeginPeriod.balance;
        //                                parameters[15] = objBeginPeriod.this_month;
        //                                objBeginPeriod.check = 2;
        //                                parameters[6] = objBeginPeriod.check;

        //                            }
        //                            int result5 = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));
        //                            if (result5 >= 0)
        //                            {

        //                                decimal old_activity, new_activity;
        //                                old_activity = objBeginPeriod.activity;
        //                                if (old_activity.ToString() != "0")
        //                                {
        //                                    new_activity = old_activity + objBeginPeriod.this_month;
        //                                }
        //                                else
        //                                {
        //                                    new_activity = objBeginPeriod.this_month;
        //                                }
        //                                objBeginPeriod.activity = new_activity;
        //                                objBeginPeriod.check = 2;
        //                                parameters[6] = objBeginPeriod.check;
        //                                parameters[9] = objBeginPeriod.curr_period;
        //                                parameters[10] = objBeginPeriod.curr_year;
        //                                parameters[11] = objBeginPeriod.acct_no;
        //                                parameters[12] = objBeginPeriod.dept;

        //                                parameters[14] = objBeginPeriod.activity;
        //                                parameters[15] = objBeginPeriod.this_month;
        //                                int result6 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));
        //                            }
        //                            else
        //                            {
        //                                err_desc = "An SQL error has occured while posting to (stxchrtd)";
        //                                return false;
        //                            }
                                    
        //                        }
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                err_desc = "An SQL error has occured while posting to (stxperdr)";
        //                return false;
        //            }
        //            //Update the general ledger control table,Set the curre_period and curr_year 
        //            //with new_period
        //            objBeginPeriod.check = 3;
        //            parameters[6] = objBeginPeriod.check;
        //            parameters[0] = objBeginPeriod.period;
        //            parameters[1] = objBeginPeriod.period_year;
        //            parameters[9] = objBeginPeriod.curr_period;
        //            parameters[10] = objBeginPeriod.curr_year;
        //            int result7 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLBeginPeriod));

        //        }
        //        else
        //        {
        //            err_desc = "An SQL error has occured while posting to (stxperdr)";
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        err_desc = ex.ToString();
        //        return false;         
        //    }
        //    return true;
        //}

        public static bool reverse_doc(DVOStgjoure objDVOStgjoure,ref object ObjTrx )
        {
            try
            {
                //reverse the document, create a brand new one.
                DVOStgjourd objDVOStgjourd = new DVOStgjourd();
                DVOStgjoure newobjDVOStgjoure = new DVOStgjoure();
                //int new_doc_no = 0;

                //create the new header transaction...
                newobjDVOStgjoure.doc_desc=objDVOStgjoure.doc_desc;
                newobjDVOStgjoure.doc_date = rev_date;
                newobjDVOStgjoure.doc_src = objDVOStgjoure.doc_src;
                newobjDVOStgjoure.file_type = objDVOStgjoure.file_type;
                newobjDVOStgjoure.auto_rev = "A";
                newobjDVOStgjoure.posted = "N";
                newobjDVOStgjoure.ok_to_post = "N";
                newobjDVOStgjoure.doc_no = 0;
                int CurrBatchId = -1;
                if (objDVOUpdateLedgerDefaults.use_batch_gen.Trim() == "Y")
                {
                    // Create New Batch..........
                    DVOBatch objDVOBatch = new DVOBatch();
                    objDVOBatch.batch_id = 0;
                    objDVOBatch.batch_type = "GJ";
                    objDVOBatch.batch_status = "ACT";
                    objDVOBatch.owner = DVOApplicationUserInfo.LoginId;
                    objDVOBatch.created_by = DVOApplicationUserInfo.LoginId;
                    objDVOBatch.create_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    objDVOBatch.create_time = DVOApplicationUserInfo.CurrentDate.TimeOfDay.ToString();
                    if (objDVOBatch.create_time.Trim().Length > 8)
                        objDVOBatch.create_time = objDVOBatch.create_time.Substring(0, 8);
                    objDVOBatch.approved_by = "";
                    objDVOBatch.approve_date = "";
                    objDVOBatch.approve_time = "";
                    objDVOBatch.posted_by = "";
                    objDVOBatch.post_date = "";
                    objDVOBatch.post_time = "";
                    objDVOBatch.post_seq = 0;
                    objDVOBatch.total_trx = 0;
                    int _batch_id = BLLBatchMaintenance.CreateNewBatch(ref objDVOBatch, ref ObjTrx);
                    if (_batch_id < 0)
                    {
                        err_desc = "An SQL error has occured while creating a new Batch ";
                        return false;
                    }
                    objDVOBatch.batch_id = _batch_id;
                    objDVOBatch.batch_type = "GJ";
                    objDVOBatch.owner = DVOApplicationUserInfo.LoginId;
                    if (!BLLBatchMaintenance.SetAsCurrentBatch(ref objDVOBatch, ref ObjTrx))
                    {
                        err_desc = "An SQL error has occured while creating a new Batch ";
                        return false;
                    }
                    string CurrUser;
                    //bool _IsMasterBatch = false;
                    BLLBatchMaintenance.GetBatchInfo(DVOApplicationUserInfo.LoginId, "GJ", out CurrUser, out CurrBatchId);//, out _IsMasterBatch);
                    newobjDVOStgjoure.user_id = CurrUser;
                    newobjDVOStgjoure.batch_id = CurrBatchId;
                }
                // Insert into stgjoure 
                if (!InsertIntoStgjoure(ref newobjDVOStgjoure, ref ObjTrx))
                {
                    err_desc = "An SQL error has occured while posting to stgjoure";
                    return false;
                }
                //inverse & create the new dist lines
                object[] parameter = new object[1];
                parameter[0] = objDVOStgjoure.doc_no;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet dstgjourd = objDALBaseClass.GetData(ref parameter, typeof(DVOStgjourd), objDVOStgjourd.GET_STGJOURD);
                if (dstgjourd.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dstgjourd.Tables[0].Rows)
                    {
                        objDVOStgjourd.orig_journal = dr[0].ToString().Trim();
                        objDVOStgjourd.acct_no = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0 ;
                        objDVOStgjourd.department = dr[3].ToString().Trim();
                        objDVOStgjourd.amount = dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0;
                        objDVOStgjourd.debit_credit = dr[5].ToString().Trim();
                        if (objDVOStgjourd.debit_credit == "D")
                            objDVOStgjourd.debit_credit = "C";
                        else
                            objDVOStgjourd.debit_credit = "D";  
                        objDVOStgjourd.doc_no = newobjDVOStgjoure.doc_no;
                        if (!InsertIntoStgjourd(ref objDVOStgjourd, ref ObjTrx))
                        {
                            err_desc = "An SQL error has occured while posting to stgjourd";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;           
        }

        public static bool InsertIntoStgjoure(ref DVOStgjoure objDVOStgjoure,ref object objTrx)
        {  
           object[] parameters=new object[10];
           try
           {
               parameters[0] = objDVOStgjoure.doc_no;
               parameters[1] = objDVOStgjoure.doc_desc;
               parameters[2] = objDVOStgjoure.doc_date;
               parameters[3] = objDVOStgjoure.doc_src;
               parameters[4] = objDVOStgjoure.auto_rev;
               parameters[5] = objDVOStgjoure.file_type;
               parameters[6] = objDVOStgjoure.posted;
               parameters[7] = objDVOStgjoure.ok_to_post;
               parameters[8] = objDVOStgjoure.batch_id;
               parameters[9] = objDVOStgjoure.user_id;
                Object nullTransactionObject = null;
                //int NewDocumentNo = BLLAccountingLiberary.Auto_Next("stgcntrc", "genjrn_doc_no", ref objTrx);
                int NewDocumentNo = BLLAccountingLiberary.Auto_Next_GJDocNo();
                if (NewDocumentNo <= 0)
                {
                    throw new Exception("General Ledger Control table is locked or empty.");
                }
                parameters[0] = NewDocumentNo;
               DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
               DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
               Object o = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOStgjoure.INSERT_STGJOURE,true);
               if (o == DBNull.Value || o.ToString().Trim().Length<=0 || Convert.ToInt32(o)<=0)
                   return false;
               objDVOStgjoure.doc_no = NewDocumentNo;
  
           }
           catch (Exception ex)
           {
               return false;
           }
           return true;      
        }
        public static bool InsertIntoStgjourd(ref DVOStgjourd objDVOStgjourd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[7];
                parameters[0] = objDVOStgjourd.orig_journal;
                parameters[1] = objDVOStgjourd.doc_no;
                parameters[2] = objDVOStgjourd.line_no;
                parameters[3] = objDVOStgjourd.acct_no;
                parameters[4] = objDVOStgjourd.department;
                parameters[5] = objDVOStgjourd.amount;
                parameters[6] = objDVOStgjourd.debit_credit;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOStgjourd.INSERT_STGJOURD,true);

                if (result != null)
                    if (result.ToString().Length > 0)
                        if (Convert.ToInt32(result) == 1)
                            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;   
        }        
        public static bool InsertIntoStxchrtd(ref object objTransaction, ref DVOGLstxchrtd objDVOGLstxchrtd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] InsParameter = new object[6];
                InsParameter[0] = objDVOGLstxchrtd.acct_no;
                InsParameter[1] = objDVOGLstxchrtd.department;
                InsParameter[2] = objDVOGLstxchrtd.period_month;
                InsParameter[3] = objDVOGLstxchrtd.period_year;
                InsParameter[4] = objDVOGLstxchrtd.balance;
                InsParameter[5] = objDVOGLstxchrtd.budget;
                object result = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref InsParameter, typeof(DVOGLstxchrtd), true);
                if (result != DBNull.Value)
                    if (result.ToString().Length > 0)
                        if (Convert.ToInt32(result) == 1)
                            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public static DataSet GetStxchrtd(string period,string year)
        {   
            DataSet ds=new DataSet();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = period;
                parameters[1] = year;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLBeginPeriod), (new DVOGLBeginPeriod()).Get_Current_Period_Account);
                ds.Tables[0].Columns[0].ColumnName = "acct_no";
                ds.Tables[0].Columns[1].ColumnName = "department";
                ds.Tables[0].Columns[2].ColumnName = "period_month";
                ds.Tables[0].Columns[3].ColumnName = "period_year";
                ds.Tables[0].Columns[4].ColumnName = "activity";
                ds.Tables[0].Columns[5].ColumnName = "balance";
                ds.Tables[0].Columns[6].ColumnName = "this_month";
                ds.Tables[0].Columns[7].ColumnName = "budget";
                ds.Tables[0].Columns[8].ColumnName = "keyvalue";
                ds.Tables[0].Columns[9].ColumnName = "acct_desc";
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        
    }
}
