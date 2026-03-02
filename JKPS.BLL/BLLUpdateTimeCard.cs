using System;
using System.Collections.Generic;
using System.Text;

using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    public class BLLUpdateTimeCard
    {
        ///This Function used to call when user select a Employee 
        ///This function return a list with Basic income code assign 
        ///For this employee
        ///Expect Parameter Empl_code 
        public static List<DVOUpdateTimeCard> IncLoadDataGet(ref DVOUpdateTimeCard objTimeCardLoad)
        {
            int minLineNumber = 0;
            int in_line_no = 0;
            int ret_acc_no, ret_acc_type_id;
            string ret_acc_type, ret_keyvalue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOUpdateTimeCard> listDVOUpdateTimeCardLoad = new List<DVOUpdateTimeCard>();

            try
            {
                BLLPayrollFunctions objBllPayFn = new BLLPayrollFunctions();
                object[] parameters = new object[1];
                parameters[0] = objTimeCardLoad.empl_code;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objTimeCardLoad.FIND_INC_LOAD))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOUpdateTimeCard tempObjDVOUpdateTimeCard = new DVOUpdateTimeCard();
                        tempObjDVOUpdateTimeCard.empl_code = (dr[0] != DBNull.Value ? dr[0].ToString() : string.Empty);
                        tempObjDVOUpdateTimeCard.inc_code_id = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        tempObjDVOUpdateTimeCard.description_cr = (dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty);
                        tempObjDVOUpdateTimeCard.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3].ToString()) : 0);

                        tempObjDVOUpdateTimeCard.inc_rate_id = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                        //Set or Assign Income Number
                        if (dr[5] != DBNull.Value)
                        {
                            tempObjDVOUpdateTimeCard.inc_number_id = Convert.ToDecimal(dr[5]);
                        }
                        else
                        {
                            tempObjDVOUpdateTimeCard.inc_number_id = Convert.ToDecimal(dr[9]);
                        }
                        //Set or Assign Income Hour
                        if (dr[14] != DBNull.Value)
                        {
                            if (dr[14].ToString().Trim() == "H")
                            {
                                tempObjDVOUpdateTimeCard.inc_hours_id = tempObjDVOUpdateTimeCard.inc_number_id;
                            }
                            else
                            {
                                tempObjDVOUpdateTimeCard.inc_hours_id = 0.0M;
                            }
                        }
                        //set or assign Income Amount
                        tempObjDVOUpdateTimeCard.inc_Amount = (tempObjDVOUpdateTimeCard.inc_number_id) * (tempObjDVOUpdateTimeCard.inc_rate_id);


                        tempObjDVOUpdateTimeCard.acct_no_id = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                        tempObjDVOUpdateTimeCard.department_id = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        //build the default expense account
                        //And getting the value of Account Type and keyvalue
                        objBllPayFn.flxMixInc(tempObjDVOUpdateTimeCard.empl_code, tempObjDVOUpdateTimeCard.inc_code_id, out ret_acc_no, out ret_acc_type, out ret_keyvalue, out ret_acc_type_id);
                        if (ret_acc_no > 0)
                        {
                            tempObjDVOUpdateTimeCard.ret_acc_no = ret_acc_no;
                        }
                        if (ret_acc_type != string.Empty)
                        {
                            tempObjDVOUpdateTimeCard.ret_acc_type = ret_acc_type;
                        }
                        if (ret_keyvalue != string.Empty)
                        {
                            tempObjDVOUpdateTimeCard.ret_keyvalue = ret_keyvalue;
                        }
                        listDVOUpdateTimeCardLoad.Add(tempObjDVOUpdateTimeCard);

                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOUpdateTimeCardLoad;
            }
            return listDVOUpdateTimeCardLoad;
        }



        public static int GetLineNumberForARow(string emp_code, string inc_code)
        {
            int RetMinValue = 0;
            DataSet ds = null;
            DVOUpdateTimeCard objGetMinLinevalue = new DVOUpdateTimeCard();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = emp_code;
                parameters[1] = inc_code;

                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objGetMinLinevalue.FIND_INC_LINENO);
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RetMinValue = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                        }

                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {

                objDALBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return RetMinValue;
        }


        // returns true/false
        // #######################################################################
        // This function checks to see if there is a duplicate time card
        // entered for the empl_code given...returning true or false
        public static Boolean py_TimeRecordCheck(ref DVOUpdateTimeCard objTimeCardExistCheck)
        {
            Boolean RetKeyval = false;
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objTimeCardExistCheck.empl_code;
                parameters[1] = objTimeCardExistCheck.start_date;
                parameters[2] = objTimeCardExistCheck.end_date;

                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objTimeCardExistCheck.FIND_INC_TIMECARD_Exist);
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RetKeyval = false;
                        }
                        else
                        {
                            RetKeyval = true;
                        }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {

                objDALBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return RetKeyval;

        }


        ///This Function used to call when user select a income code inside to grid
        ///to update the Time Card For a Employee
        ///Function take two parameter 
        ///empl_code and inc_code 
        public static List<DVOUpdateTimeCard> IncLoadDefaultRowsForGrid(ref DVOUpdateTimeCard objTimeCardLoad)
        {
            int ret_acc_no, ret_acc_type_id;
            string ret_acc_type, ret_keyvalue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOUpdateTimeCard> listDVOUpdateTimeCardLoad = new List<DVOUpdateTimeCard>();

            try
            {
                BLLPayrollFunctions objBllPayFn = new BLLPayrollFunctions();
                object[] parameters = new object[1];
                parameters[0] = objTimeCardLoad.inc_code_id;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objTimeCardLoad.FIND_DETAILS_BY_INCCODE))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOUpdateTimeCard tempObjDVOUpdateTimeCard = new DVOUpdateTimeCard();
                        tempObjDVOUpdateTimeCard.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        tempObjDVOUpdateTimeCard.inc_code_id = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        tempObjDVOUpdateTimeCard.description_cr = (dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty);
                        if (dr[4] == DBNull.Value)
                            tempObjDVOUpdateTimeCard.dflt_num_cr = null;// (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);
                        else
                            tempObjDVOUpdateTimeCard.dflt_num_cr = (dr[3] != DBNull.Value ? (decimal?)(dr[3]) : null);

                        tempObjDVOUpdateTimeCard.dflt_rate_cr = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                        tempObjDVOUpdateTimeCard.dflt_acct_cr = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
                        tempObjDVOUpdateTimeCard.dflt_dept_cr = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
                        tempObjDVOUpdateTimeCard.inc_type_cr = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        tempObjDVOUpdateTimeCard.dftAccountType = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);


                        //set or assign Income Amount
                        tempObjDVOUpdateTimeCard.inc_Amount = (tempObjDVOUpdateTimeCard.dflt_num_cr) * (tempObjDVOUpdateTimeCard.dflt_rate_cr);
                        //Set or Assign Income Hour
                        if (dr[8] != DBNull.Value)
                        {
                            if (dr[8].ToString().Trim() == "H")
                            {
                                tempObjDVOUpdateTimeCard.dflt_hours_cr = tempObjDVOUpdateTimeCard.dflt_num_cr;
                            }
                            else
                            {
                                tempObjDVOUpdateTimeCard.dflt_hours_cr = null;
                            }
                        }

                        //build the default expense account
                        //And getting the value of Account Type and keyvalue
                        objBllPayFn.flxMixInc(objTimeCardLoad.empl_code, tempObjDVOUpdateTimeCard.inc_code_id, out ret_acc_no, out ret_acc_type, out ret_keyvalue, out ret_acc_type_id);
                        if (ret_acc_no > 0)
                        {
                            tempObjDVOUpdateTimeCard.ret_acc_no = ret_acc_no;
                        }
                        if (ret_acc_type != string.Empty)
                        {
                            tempObjDVOUpdateTimeCard.ret_acc_type = ret_acc_type;
                        }
                        if (ret_keyvalue != string.Empty)
                        {
                            tempObjDVOUpdateTimeCard.ret_keyvalue = ret_keyvalue;
                        }
                        listDVOUpdateTimeCardLoad.Add(tempObjDVOUpdateTimeCard);

                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOUpdateTimeCardLoad;
            }
            return listDVOUpdateTimeCardLoad;
        }

        /// <summary>
        /// This Function is used to get the data on the basis of search criteria.
        /// </summary>
        /// <param name="objTimeCardSearchCriteria" - Startdate,End Date,EmployeeCode and used flag=N(By Default)></param>
        /// <returns>List<DVOUpdateTimeCard></returns>
        public static List<DVOUpdateTimeCard> GetRecordDataFromStyTimee(ref DVOUpdateTimeCard objTimeCardSearchCriteria)
        {
            object[] parameters = new object[4];
            parameters[0] = objTimeCardSearchCriteria.start_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[1] = objTimeCardSearchCriteria.end_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[2] = objTimeCardSearchCriteria.empl_code;

            //added by Sunil Pahwa on 17/02/09
            parameters[3] = objTimeCardSearchCriteria.RowID;


            List<DVOUpdateTimeCard> objDVOUpdateTimeCardDetailsList = new List<DVOUpdateTimeCard>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard tempobjDVOUpdateTimeCard = new DVOUpdateTimeCard();
                    tempobjDVOUpdateTimeCard.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                    tempobjDVOUpdateTimeCard.card_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    tempobjDVOUpdateTimeCard.empl_code = (dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty);
                    tempobjDVOUpdateTimeCard.empl_name = (dr[3] != DBNull.Value ? dr[3].ToString() : string.Empty);
                    tempobjDVOUpdateTimeCard.start_date = (dr[4] != DBNull.Value ? Convert.ToDateTime(dr[4]) : Convert.ToDateTime("01/01/1900"));
                    tempobjDVOUpdateTimeCard.end_date = (dr[5] != DBNull.Value ? Convert.ToDateTime(dr[5]) : Convert.ToDateTime("01/01/1900"));
                    tempobjDVOUpdateTimeCard.used_flag = (dr[6] != DBNull.Value ? dr[6].ToString() : string.Empty);

                    objDVOUpdateTimeCardDetailsList.Add(tempobjDVOUpdateTimeCard);
                }
            }
            return objDVOUpdateTimeCardDetailsList;

        }

        public static List<DVOUpdateTimeCard> GetDetailedDataFromStyTimeed(ref DVOUpdateTimeCard objTimeCardDetailed)
        {
            object[] parameters = new object[1];
            parameters[0] = objTimeCardDetailed.card_no;

            List<DVOUpdateTimeCard> objDVOUpdateTimeedCardDetailsList = new List<DVOUpdateTimeCard>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objTimeCardDetailed.FIND_DETILSEARCH_BY_CARDNO))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard tempobjDVOUpdateTimeCard = new DVOUpdateTimeCard();
                    tempobjDVOUpdateTimeCard.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                    tempobjDVOUpdateTimeCard.card_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    tempobjDVOUpdateTimeCard.line_no_id = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                    tempobjDVOUpdateTimeCard.inc_code_id = (dr[3] != DBNull.Value ? dr[3].ToString() : string.Empty);
                    tempobjDVOUpdateTimeCard.inc_rate_id = (dr[4] != DBNull.Value ? (decimal?)(dr[4]) : null);
                    tempobjDVOUpdateTimeCard.inc_number_id = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                    tempobjDVOUpdateTimeCard.inc_hours_id = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                    tempobjDVOUpdateTimeCard.ret_acc_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                    tempobjDVOUpdateTimeCard.ret_acc_type = (dr[8] != DBNull.Value ? dr[8].ToString() : string.Empty);
                    tempobjDVOUpdateTimeCard.ret_keyvalue = (dr[9] != DBNull.Value ? dr[9].ToString() : string.Empty);

                    tempobjDVOUpdateTimeCard.inc_Amount = (tempobjDVOUpdateTimeCard.inc_rate_id * tempobjDVOUpdateTimeCard.inc_number_id);

                    objDVOUpdateTimeedCardDetailsList.Add(tempobjDVOUpdateTimeCard);
                }
            }
            return objDVOUpdateTimeedCardDetailsList;
        }

        public static int InsertNewTimeCardRecord(ref DVOUpdateTimeCard objDVOUpdateTimeCardRecord, ref List<DVOUpdateTimeCard> listDVOUpdateTimeCard)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int i = 0;
            try
            {

                object[] parameters = new object[11];
                parameters[0] = objDVOUpdateTimeCardRecord.empl_code;
                parameters[1] = objDVOUpdateTimeCardRecord.empl_name;
                parameters[2] = objDVOUpdateTimeCardRecord.start_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objDVOUpdateTimeCardRecord.end_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objDVOUpdateTimeCardRecord.used_flag;

                //Parameters used For Only SQL Server
                parameters[5] = objDVOUpdateTimeCardRecord.InsertMachineInfo;
                parameters[6] = objDVOUpdateTimeCardRecord.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[7] = objDVOUpdateTimeCardRecord.InsertBy;
                parameters[8] = objDVOUpdateTimeCardRecord.UpdateMachineInfo;
                parameters[9] = objDVOUpdateTimeCardRecord.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[10] = objDVOUpdateTimeCardRecord.UpdateBy;

                DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateTimeCard));
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0][0] != DBNull.Value && Ds.Tables[0].Rows[0][0] != null && Ds.Tables[0].Rows[0][0].ToString().Trim().Length > 0)
                    {
                        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0]);
                        if (success > 0)
                        {
                            for (int j = 0; j < listDVOUpdateTimeCard.Count; j++)
                                listDVOUpdateTimeCard[j].card_no = success;

                            i = InsertNewTimeCardDetails(ref listDVOUpdateTimeCard, ref  objTransection);
                            if (i < 0)
                                throw new Exception("Error occured while inserting time-card detail.");

                            success = i;
                        }
                        else
                            throw new Exception("Error occured while inserting time-card header.");
                    }
                    else
                        throw new Exception("Error occured while inserting time-card header.");
                }
                else
                    throw new Exception("Error occured while inserting time-card header.");



                //    {
                //        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
                //    }
                //if (success > 0)
                //{
                //    for (int j = 0; j < listDVOUpdateTimeCard.Count;j++ )
                //    {
                //        listDVOUpdateTimeCard[j].card_no = success;
                //    }
                //    i = BLLUpdateTimeCard.InsertNewTimeCardDetails(ref listDVOUpdateTimeCard, ref  objTransection);
                //    if (i > 0)
                //    {
                //        success = i;
                //        if (objTransection != null)
                //            objDALBaseClassHelper.CommitTransaction(ref objTransection);
                //    }
                //    else
                //    {
                //        success = i;
                //        if (objTransection == null)
                //            objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                //    }
                //}
                //else
                //{
                //    if (objTransection == null)
                //        objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                //}

                if (objTransection != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);
            }
            catch (Exception ex)
            {
                if (objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }

        public static int InsertNewTimeCardDetails(ref List<DVOUpdateTimeCard> listDVOUpdateTimeCard, ref object objTransection)
        {
            int detStatus = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //object obj = null;
            try
            {
                foreach (DVOUpdateTimeCard objDVOUpdateTimeCardDetailed in listDVOUpdateTimeCard)
                {
                    object[] parameters = new object[13];
                    parameters[0] = objDVOUpdateTimeCardDetailed.card_no;
                    parameters[1] = objDVOUpdateTimeCardDetailed.inc_code_id;
                    parameters[2] = objDVOUpdateTimeCardDetailed.inc_rate_id;
                    parameters[3] = objDVOUpdateTimeCardDetailed.inc_number_id;
                    parameters[4] = objDVOUpdateTimeCardDetailed.inc_hours_id;
                    parameters[5] = objDVOUpdateTimeCardDetailed.acct_no_id;
                    parameters[6] = objDVOUpdateTimeCardDetailed.line_no_id;

                    //Parameters used For Only SQL Server
                    parameters[7] = objDVOUpdateTimeCardDetailed.InsertMachineInfo;
                    parameters[8] = objDVOUpdateTimeCardDetailed.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    parameters[9] = objDVOUpdateTimeCardDetailed.InsertBy;
                    parameters[10] = objDVOUpdateTimeCardDetailed.UpdateMachineInfo;
                    parameters[11] = objDVOUpdateTimeCardDetailed.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    parameters[12] = objDVOUpdateTimeCardDetailed.UpdateBy;


                    object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateTimeCard), objDVOUpdateTimeCardDetailed.INSERT_TIMEDETAIL);
                    if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) > 0)
                        detStatus = Convert.ToInt32(obj);
                    else
                        throw new Exception("Error occured while inserting time-card detail.");
                    if (!statusObjTransaction && objTransection != null)
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                }
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return detStatus;
        }

        public static int DeleteTimeCardRecordAndDetails(ref object objTransection, ref DVOUpdateTimeCard objDVOUpdateTimeCardDel)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }

            try
            {
                int success = 0;
                object[] parameters = new object[1];
                parameters[0] = objDVOUpdateTimeCardDel.RowID;
                object[] RetValue = new object[1];
                RetValue[0] = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateTimeCard), true);
                if (RetValue[0] != null)
                {
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;
        }

        public static int UpdateTimeCardRecord(ref object objTransection, ref DVOUpdateTimeCard objDVOUpdateTimeCardRecordUpdate, ref List<DVOUpdateTimeCard> listNewDVOUpdateTimeCard, ref List<DVOUpdateTimeCard> listUpdateDVOUpdateTimeCard, ref List<DVOUpdateTimeCard> listDeleteDVOUpdateTimeCard)
        {
            //*********************
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransection == null)
            {
                objTransection = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int success = 0;
            //object obj = null;
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objDVOUpdateTimeCardRecordUpdate.RowID;
                parameters[1] = objDVOUpdateTimeCardRecordUpdate.start_date;
                parameters[2] = objDVOUpdateTimeCardRecordUpdate.end_date;

                //Parameters used For Only SQL Server                               
                parameters[3] = objDVOUpdateTimeCardRecordUpdate.UpdateMachineInfo;
                parameters[4] = objDVOUpdateTimeCardRecordUpdate.UpdateDate;
                parameters[5] = objDVOUpdateTimeCardRecordUpdate.UpdateBy;
                //object[] RetValue = new object[1];

                //success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref Parameter, typeof(DVOFlexSegment));

                object RetValue = objDALBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateTimeCard), true);

                // Return when Code updated Successfully with card No
                if (RetValue != DBNull.Value && RetValue.ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt32(RetValue) > 0)
                    {
                        success = Convert.ToInt32(RetValue);
                    }
                    if (success > 0)
                    {
                        for (int j = 0; j < listNewDVOUpdateTimeCard.Count; j++)
                        {
                            listNewDVOUpdateTimeCard[j].card_no = success;
                        }
                        for (int k = 0; k < listUpdateDVOUpdateTimeCard.Count; k++)
                        {
                            listUpdateDVOUpdateTimeCard[k].card_no = success;
                        }
                        for (int l = 0; l < listDeleteDVOUpdateTimeCard.Count; l++)
                        {
                            listDeleteDVOUpdateTimeCard[l].card_no = success;
                        }
                        if (listNewDVOUpdateTimeCard.Count > 0)
                            InsertWithUpdateTimeCardDetails(ref objTransection, ref listNewDVOUpdateTimeCard);
                        if (listDeleteDVOUpdateTimeCard.Count > 0)
                            DeleteDetailByRowId(ref objTransection, ref listDeleteDVOUpdateTimeCard);
                        if (listUpdateDVOUpdateTimeCard.Count > 0)
                            UpdateTimeCardDetails(ref objTransection, ref listUpdateDVOUpdateTimeCard);
                    }
                    else
                    {
                        throw new Exception("Error occured while updating time-card header record.");
                    }
                }
                else
                {
                    throw new Exception("Error occured while updating time-card header record.");
                }
                if (!statusObjTransaction && objTransection != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransection);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransection != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }
        public static int UpdateTimeCardDetails(ref object objTransaction, ref List<DVOUpdateTimeCard> listUpdateDVOUpdateTimeCard)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //object obj = null;
            try
            {
                if (listUpdateDVOUpdateTimeCard.Count > 0)
                    foreach (DVOUpdateTimeCard objDVOUpdateTimeCard in listUpdateDVOUpdateTimeCard)
                    {
                        object[] parameters = new object[9];
                        parameters[0] = objDVOUpdateTimeCard.RowID;
                        parameters[1] = objDVOUpdateTimeCard.inc_code_id;
                        parameters[2] = objDVOUpdateTimeCard.inc_rate_id;
                        parameters[3] = objDVOUpdateTimeCard.inc_number_id;
                        parameters[4] = objDVOUpdateTimeCard.inc_hours_id;
                        parameters[5] = objDVOUpdateTimeCard.acct_no_id;

                        //Parameters used For Only SQL Server             
                        parameters[6] = objDVOUpdateTimeCard.UpdateMachineInfo;
                        parameters[7] = objDVOUpdateTimeCard.UpdateDate;
                        parameters[8] = objDVOUpdateTimeCard.UpdateBy;

                        object obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUpdateTimeCard), objDVOUpdateTimeCard.UPDATE_DETAILS);
                        if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                            throw new Exception("Error occured while updating time-card detail.");
                        parameters = null;
                    }

                //if (listNewDVOUpdateTimeCard.Count > 0)
                //    obj= BLLUpdateTimeCard.InsertWithUpdateTimeCardDetails(ref objTransaction, ref listNewDVOUpdateTimeCard);
                //if (listDeleteDVOUpdateTimeCard.Count > 0)
                //    obj= BLLUpdateTimeCard.DeleteDetailByRowId(ref objTransaction, ref listDeleteDVOUpdateTimeCard);
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }

        public static List<DVOUpdateTimeCard> FIND_QUERY(DateTime StartDate, DateTime EndDate, string empl_code)
        {
            object[] Parameter = new object[4];
            Parameter[0] = StartDate;
            Parameter[1] = EndDate;
            Parameter[2] = empl_code;
            Parameter[3] = null;
            List<DVOUpdateTimeCard> lstUpdateTimeCard = new List<DVOUpdateTimeCard>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData((new DVOUpdateTimeCard()).FIND_QUERY(ref Parameter)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard obj = new DVOUpdateTimeCard();//EmpTcardID v_rowid,card_no v_card_no,empl_code v_empl_code,empl_name v_empl_name,start_date v_start_date,end_date v_end_date,used_flag v_used_flag
                    obj.EmpTcardID = Convert.ToInt32(dr[0]);
                    obj.RowID = Convert.ToInt32(dr[0]);
                    obj.card_no = Convert.ToInt32(dr[1]);
                    obj.empl_code = dr[2].ToString();
                    //obj.v_empl_code = Convert.ToInt32(dr[1]);
                    obj.empl_name = dr[3].ToString();
                    obj.start_date = Convert.ToDateTime(dr[4]);
                    obj.start_date = Convert.ToDateTime(dr[5]);
                    obj.used_flag = dr[6].ToString();

                    lstUpdateTimeCard.Add(obj);
                }
                return lstUpdateTimeCard;
            }
        }

        public static object InsertWithUpdateTimeCardDetails(ref object objTransaction, ref List<DVOUpdateTimeCard> lstDVOPRUpdateTaxTables)
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
                foreach (DVOUpdateTimeCard objDVOUpdateTimeCard in lstDVOPRUpdateTaxTables)
                {
                    object[] parameters = new object[13];
                    parameters[0] = objDVOUpdateTimeCard.card_no;
                    parameters[1] = objDVOUpdateTimeCard.inc_code_id;
                    parameters[2] = objDVOUpdateTimeCard.inc_rate_id;
                    parameters[3] = objDVOUpdateTimeCard.inc_number_id;
                    parameters[4] = objDVOUpdateTimeCard.inc_hours_id;
                    parameters[5] = objDVOUpdateTimeCard.acct_no_id;
                    parameters[6] = objDVOUpdateTimeCard.line_no_id;

                    //Parameters used For Only SQL Server
                    parameters[7] = objDVOUpdateTimeCard.InsertMachineInfo;
                    parameters[8] = objDVOUpdateTimeCard.InsertDate;
                    parameters[9] = objDVOUpdateTimeCard.InsertBy;
                    parameters[10] = objDVOUpdateTimeCard.UpdateMachineInfo;
                    parameters[11] = objDVOUpdateTimeCard.UpdateDate;
                    parameters[12] = objDVOUpdateTimeCard.UpdateBy;

                    object obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUpdateTimeCard), objDVOUpdateTimeCard.INSERT_TIMEDETAIL);
                    if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                        throw new Exception("Error occured while inserting time-card detail.");
                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }

        public static List<DVOUpdateTimeCard> GetIncTypeForAddedNewRow(ref DVOUpdateTimeCard objTimeCardLoad)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOUpdateTimeCard> listDVOUpdateTimeCardLoad = new List<DVOUpdateTimeCard>();

            try
            {
                BLLPayrollFunctions objBllPayFn = new BLLPayrollFunctions();
                object[] parameters = new object[1];
                parameters[0] = objTimeCardLoad.inc_code_id;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), objTimeCardLoad.FIND_DETAILS_BY_INCCODE))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOUpdateTimeCard tempObjDVOUpdateTimeCard = new DVOUpdateTimeCard();

                        tempObjDVOUpdateTimeCard.inc_type_cr = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                        tempObjDVOUpdateTimeCard.dftAccountType = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
                        listDVOUpdateTimeCardLoad.Add(tempObjDVOUpdateTimeCard);

                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOUpdateTimeCardLoad;
            }
            return listDVOUpdateTimeCardLoad;
        }

        public static object DeleteDetailByRowId(ref object objTransaction, ref List<DVOUpdateTimeCard> lstDVOUpdateTimeCard)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //object obj = null;

            try
            {
                foreach (DVOUpdateTimeCard objDVOUpdateTimeCard in lstDVOUpdateTimeCard)
                {
                    object[] parameters = new object[1];
                    parameters[0] = objDVOUpdateTimeCard.RowID;
                    object obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUpdateTimeCard), objDVOUpdateTimeCard.Delete_TimeCard_Details);
                    if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                        throw new Exception("Error occured while deleting time-card detail.");
                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }
    }
}
