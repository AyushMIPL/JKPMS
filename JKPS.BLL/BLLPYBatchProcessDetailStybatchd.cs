using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLPYBatchProcessDetailStybatchd
    {
        public static List<DVOPYBatchProcessDetailStybatchd> GetData(ref DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd)
        {
            List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = new List<DVOPYBatchProcessDetailStybatchd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objDVOPYBatchProcessDetailStybatchd.pybatchid;
                parameters[1] = objDVOPYBatchProcessDetailStybatchd.batchprocessid;
                parameters[2] = objDVOPYBatchProcessDetailStybatchd.processname;
                if (objDVOPYBatchProcessDetailStybatchd.processstartedon == string.Empty || objDVOPYBatchProcessDetailStybatchd.processstartedon.Trim() == "01/01/1900")
                    objDVOPYBatchProcessDetailStybatchd.processstartedon = null;
                parameters[3] = objDVOPYBatchProcessDetailStybatchd.processstartedon;
                if (objDVOPYBatchProcessDetailStybatchd.processendedon == string.Empty || objDVOPYBatchProcessDetailStybatchd.processendedon.Trim() == "01/01/1900")
                    objDVOPYBatchProcessDetailStybatchd.processendedon = null;
                parameters[4] = objDVOPYBatchProcessDetailStybatchd.processendedon;
                parameters[5] = objDVOPYBatchProcessDetailStybatchd.recordssearched;
                parameters[6] = objDVOPYBatchProcessDetailStybatchd.recordsprocessed;
                parameters[7] = objDVOPYBatchProcessDetailStybatchd.status;
                parameters[8] = objDVOPYBatchProcessDetailStybatchd.errormessage;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPYBatchProcessDetailStybatchd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOPYBatchProcessDetailStybatchd tobjDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
                                tobjDVOPYBatchProcessDetailStybatchd.pybatchid = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.batchprocessid = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.processname = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
                                tobjDVOPYBatchProcessDetailStybatchd.processstartedon = (dr[3] != DBNull.Value ? Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                                tobjDVOPYBatchProcessDetailStybatchd.processendedon = (dr[4] != DBNull.Value ? Convert.ToDateTime(dr[4]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                                tobjDVOPYBatchProcessDetailStybatchd.recordssearched = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.recordsprocessed = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.status = dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.errormessage = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                                tobjDVOPYBatchProcessDetailStybatchd.insertby = dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.insertdate = (dr[10] != DBNull.Value ? Convert.ToDateTime(dr[10]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                                tobjDVOPYBatchProcessDetailStybatchd.insertmachineinfo = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                                tobjDVOPYBatchProcessDetailStybatchd.updateby = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
                                tobjDVOPYBatchProcessDetailStybatchd.updatedate = (dr[13] != DBNull.Value ? Convert.ToDateTime(dr[13]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                                tobjDVOPYBatchProcessDetailStybatchd.updatemachineinfo = dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty;

                                TimeSpan tsStratTime = dr[3] != DBNull.Value ? Convert.ToDateTime(dr[3]).TimeOfDay : TimeSpan.Zero;
                                TimeSpan tsEndTime = dr[4] != DBNull.Value ? Convert.ToDateTime(dr[4]).TimeOfDay : TimeSpan.Zero;

                                tobjDVOPYBatchProcessDetailStybatchd.StartTine = tsStratTime.ToString();
                                tobjDVOPYBatchProcessDetailStybatchd.EndTine = tsEndTime.ToString();
                                tobjDVOPYBatchProcessDetailStybatchd.processstartedon += " " + tobjDVOPYBatchProcessDetailStybatchd.StartTine.Split('.')[0];
                                tobjDVOPYBatchProcessDetailStybatchd.processendedon += " " + tobjDVOPYBatchProcessDetailStybatchd.EndTine.Split('.')[0];
                                listDVOPYBatchProcessDetailStybatchd.Add(tobjDVOPYBatchProcessDetailStybatchd);
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOPYBatchProcessDetailStybatchd = new List<DVOPYBatchProcessDetailStybatchd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOPYBatchProcessDetailStybatchd;
        }

        public static int InsertData(ref object objTransaction, ref List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd)
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
                if (listDVOPYBatchProcessDetailStybatchd.Count > 0)
                {
                    object[] parameters = new object[11];
                    foreach (DVOPYBatchProcessDetailStybatchd obj in listDVOPYBatchProcessDetailStybatchd)
                    {
                        parameters[0] = obj.pybatchid;
                        parameters[1] = obj.processname;
                        parameters[2] = obj.processstartedon;
                        parameters[3] = obj.processendedon;
                        parameters[4] = obj.recordssearched;
                        parameters[5] = obj.recordsprocessed;
                        parameters[6] = obj.status;
                        if (obj.searchcriteria.Trim().Length > 2000)
                            parameters[7] = obj.searchcriteria.Substring(0, 2000);
                        else
                            parameters[7] = obj.searchcriteria.Trim();

                        if (obj.errormessage.Trim().Length > 8000)
                            parameters[8] = obj.errormessage.Substring(0, 8000);
                        else
                            parameters[8] = obj.errormessage;
                        parameters[9] = obj.insertby;
                        parameters[10] = obj.insertmachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SPNAME);
                        if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0)
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

        public static int UpdateData(ref object objTransaction, ref List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd)
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
                if (listDVOPYBatchProcessDetailStybatchd.Count > 0)
                {
                    object[] parameters = new object[10];
                    foreach (DVOPYBatchProcessDetailStybatchd obj in listDVOPYBatchProcessDetailStybatchd)
                    {
                        parameters[0] = obj.pybatchid;
                        parameters[1] = obj.batchprocessid;
                        parameters[2] = obj.processname;
                        //if (!(obj.processendedon != null && !obj.processendedon.Trim().Contains("1900") && !obj.processendedon.Trim().Contains("0001")))
                        //    obj.processendedon = null;
                        parameters[3] = obj.processendedon;
                        parameters[4] = obj.recordssearched;
                        parameters[5] = obj.recordsprocessed;
                        parameters[6] = obj.status;
                        if (obj.errormessage.Trim().Length > 2000)
                            parameters[7] = obj.errormessage.Substring(0, 2000);
                        else
                            parameters[7] = obj.errormessage;

                        parameters[8] = obj.updateby;
                        parameters[9] = obj.updatemachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.UPDATE_SPNAME);
                        if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0)
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

        public static int DeleteData(ref object objTransaction, ref List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd)
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
                if (listDVOPYBatchProcessDetailStybatchd.Count > 0)
                {
                    object[] parameters = new object[4];
                    foreach (DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd in listDVOPYBatchProcessDetailStybatchd)
                    {
                        parameters[0] = objDVOPYBatchProcessDetailStybatchd.pybatchid;
                        parameters[1] = objDVOPYBatchProcessDetailStybatchd.batchprocessid;
                        parameters[2] = objDVOPYBatchProcessDetailStybatchd.updateby;
                        parameters[3] = objDVOPYBatchProcessDetailStybatchd.updatemachineinfo;

                        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPYBatchProcessDetailStybatchd.DELETE_SPNAME);
                        if (o == null)
                            throw new Exception();
                        else if (Convert.ToInt32(o) < 1)
                            throw new Exception();
                    }
                    parameters = null;
                    objDALBaseClass = null;
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
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

        public static int DeleteAllDetailLines(ref object objTransaction, int PayrollBatchId)
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
                parameters[0] = PayrollBatchId;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPYBatchProcessDetailStybatchd()).DELETE_ALL_DETAILS);
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

        public static int InsertProcessInfo(ref object objTransaction, ref DVOPYBatchProcessDetailStybatchd obj)
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
                parameters[0] = obj.pybatchid;
                parameters[1] = obj.processname;
                if (obj.searchcriteria.Trim().Length > 2000)
                    parameters[2] = obj.searchcriteria.Substring(0, 2000);
                else
                    parameters[2] = obj.searchcriteria.Trim();
                parameters[3] = obj.insertby;
                parameters[4] = obj.insertmachineinfo;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.ProcessIns);
                if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                obj.batchprocessid = Convert.ToInt32(o);
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

        public static (List<DVOPayrollProcess_PayEmployee>, List<DVOPayrollautopay>) ExecuteAutoPay(object[] parameters)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                DataSet dspayroll = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, "usp_CreateAutoPayroll_V2");

                var lst = new List<DVOPayrollProcess_PayEmployee>();
                var lstEmployees = new List<DVOPayrollautopay>();

                if ((dspayroll?.Tables?.Count ?? 0) > 0 && dspayroll.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dspayroll.Tables[0].Rows)
                    {
                        DVOPayrollProcess_PayEmployee obj = new DVOPayrollProcess_PayEmployee
                        {
                            Doc_no = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0,
                            EmplCode = dr["empl_code"] != DBNull.Value ? Convert.ToString(dr["empl_code"]) : "",
                            doc_date = dr["doc_date"] != DBNull.Value ? Convert.ToDateTime(dr["doc_date"]) : Convert.ToDateTime(null),
                            pay_date = dr["pay_date"] != DBNull.Value ? Convert.ToDateTime(dr["pay_date"]) : Convert.ToDateTime(null),
                            eop_date = dr["eop_date"] != DBNull.Value ? Convert.ToDateTime(dr["eop_date"]) : Convert.ToDateTime(null),
                            print_check = dr["print_check"] != DBNull.Value ? Convert.ToString(dr["print_check"]) : "",
                            Cash_acct_no = dr["cash_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["cash_acct_no"]) : 0,
                            Department = dr["department"] != DBNull.Value ? Convert.ToString(dr["department"]) : "",
                            cash_amount = dr["cash_amount"] != DBNull.Value ? Convert.ToDecimal(dr["cash_amount"]) : 0,
                            check_no = dr["check_no"] != DBNull.Value ? Convert.ToInt32(dr["check_no"]) : 0,
                            inc_gross = dr["inc_gross"] != DBNull.Value ? Convert.ToDecimal(dr["inc_gross"]) : 0,
                            ded_fica = dr["ded_fica"] != DBNull.Value ? Convert.ToDecimal(dr["ded_fica"]) : 0,
                            inc_taxable = dr["inc_taxable"] != DBNull.Value ? Convert.ToDecimal(dr["inc_taxable"]) : 0,
                            ded_medicare = dr["ded_medicare"] != DBNull.Value ? Convert.ToDecimal(dr["ded_medicare"]) : 0,
                            ded_fedtax = dr["ded_fedtax"] != DBNull.Value ? Convert.ToDecimal(dr["ded_fedtax"]) : 0,
                            ded_statax = dr["ded_statax"] != DBNull.Value ? Convert.ToDecimal(dr["ded_statax"]) : 0,
                            ded_loctax = dr["ded_loctax"] != DBNull.Value ? Convert.ToDecimal(dr["ded_loctax"]) : 0,
                            ded_other = dr["ded_other"] != DBNull.Value ? Convert.ToDecimal(dr["ded_other"]) : 0,
                            obl_futa = dr["obl_futa"] != DBNull.Value ? Convert.ToDecimal(dr["obl_futa"]) : 0,
                            obl_fica = dr["obl_fica"] != DBNull.Value ? Convert.ToDecimal(dr["obl_fica"]) : 0,
                            obl_medicare = dr["obl_medicare"] != DBNull.Value ? Convert.ToDecimal(dr["obl_medicare"]) : 0,
                            obl_other = dr["obl_other"] != DBNull.Value ? Convert.ToDecimal(dr["obl_other"]) : 0,
                            obl_total = dr["obl_total"] != DBNull.Value ? Convert.ToDecimal(dr["obl_total"]) : 0,
                            inc_net = dr["inc_net"] != DBNull.Value ? Convert.ToDecimal(dr["inc_net"]) : 0,
                            inc_expense = dr["inc_expense"] != DBNull.Value ? Convert.ToDecimal(dr["inc_expense"]) : 0,
                            total_hours = dr["total_hours"] != DBNull.Value ? Convert.ToDecimal(dr["total_hours"]) : 0,
                            ok_to_post = dr["ok_to_post"] != DBNull.Value ? Convert.ToString(dr["ok_to_post"]) : "",
                            accrue_sick = dr["accrue_sick"] != DBNull.Value ? Convert.ToString(dr["accrue_sick"]) : "",
                            accrue_vac = dr["accrue_vac"] != DBNull.Value ? Convert.ToString(dr["accrue_vac"]) : "",
                            bonus = dr["bonus"] != DBNull.Value ? Convert.ToString(dr["bonus"]) : "",
                            deposit = dr["deposit"] != DBNull.Value ? Convert.ToString(dr["deposit"]) : "",
                            start_date = dr["pay_start_date"] != DBNull.Value ? Convert.ToDateTime(dr["pay_start_date"]) : Convert.ToDateTime(null)
                        };
                        lst.Add(obj);


                        DVOPayrollautopay dvo = new DVOPayrollautopay
                        {
                            EmplCode = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString() : "",
                            SocSecNum = dr["Soc_Sec_Num"] != DBNull.Value ? dr["Soc_Sec_Num"].ToString() : "",
                            FirstName = dr["first_name"] != DBNull.Value ? dr["first_name"].ToString() : "",
                            LastName = dr["last_name"] != DBNull.Value ? dr["last_name"].ToString() : "",
                            CashAcct = dr["cash_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["cash_acct_no"]) : 0,
                            Department = dr["department"] != DBNull.Value ? dr["department"].ToString() : "",
                            Terminated = dr["terminated"] != DBNull.Value ? Convert.ToDateTime(dr["terminated"]).ToString("yyyy/MM/dd") : "",
                            PayPeriod = dr["pay_period"] != DBNull.Value ? dr["pay_period"].ToString() : "",
                            Allowances = dr["allowances"] != DBNull.Value ? Convert.ToInt32(dr["allowances"]) : 0,
                            StateAllow = dr["state_allow"] != DBNull.Value ? Convert.ToInt32(dr["state_allow"]) : 0,
                            MaritalStat = dr["marital_stat"] != DBNull.Value ? dr["marital_stat"].ToString() : "",
                            VacCode = dr["vac_code"] != DBNull.Value ? dr["vac_code"].ToString() : "",
                            VacAllowed = dr["vac_allowed"] != DBNull.Value ? Convert.ToDecimal(dr["vac_allowed"]) : 0,
                            VacUsed = dr["vac_used"] != DBNull.Value ? Convert.ToDecimal(dr["vac_used"]) : 0,
                            SickCode = dr["sick_code"] != DBNull.Value ? dr["sick_code"].ToString() : "",
                            SickAllowed = dr["sick_allowed"] != DBNull.Value ? Convert.ToDecimal(dr["sick_allowed"]) : 0,
                            SickUsed = dr["sick_used"] != DBNull.Value ? Convert.ToDecimal(dr["sick_used"]) : 0,
                            LastPay = dr["last_pay"] != DBNull.Value ? Convert.ToDateTime(dr["last_pay"]) : DateTime.MinValue,
                            HoldPayment = dr["hold_pymnt"] != DBNull.Value ? dr["hold_pymnt"].ToString() : "",
                            StaTaxCode = dr["statax_code"] != DBNull.Value ? dr["statax_code"].ToString() : "",
                            LocTaxCode = dr["loctax_code"] != DBNull.Value ? dr["loctax_code"].ToString() : "",
                            DirDept = dr["dir_dept"] != DBNull.Value ? dr["dir_dept"].ToString() : "",
                            FlexDeptAcctType = dr["flexdeptaccttype"] != DBNull.Value ? dr["flexdeptaccttype"].ToString() : "",
                            LastIncDate = dr["last_inc_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_inc_date"]).ToString("yyyy/MM/dd") : "",
                            RowID = dr["EmployeeID"] != DBNull.Value ? Convert.ToInt32(dr["EmployeeID"]) : 0,
                            Flexdeptkeyvalue = dr["Flexdeptkeyvalue"] != DBNull.Value ? dr["Flexdeptkeyvalue"].ToString() : "",
                            Job_Code = dr["job_code"] != DBNull.Value ? dr["job_code"].ToString() : "",
                            District = dr["SelectDistrict"] != DBNull.Value ? dr["SelectDistrict"].ToString() : "",
                            //pay_lapse int,
                            Employee_Type = dr["Type_Code"] != DBNull.Value ? dr["Type_Code"].ToString() : ""
                        };
                        lstEmployees.Add(dvo);
                    }
                }

                objDALBaseClass = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return (lst, lstEmployees);
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        public static DataSet ExecuteAutoPay_V6(object[] parameters)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                DataSet dspayroll = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, "usp_CreateAutoPayroll_V6");

                if ((dspayroll?.Tables?.Count ?? 0) <= 0 || ((dspayroll?.Tables?.Count ?? 0) <= 0 && dspayroll.Tables[0].Rows.Count <= 0))
                {
                    dspayroll = new DataSet();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("");
                    dt.Columns.Add("bank_desc");
                    dt.Columns.Add("amount");
                    dt.Columns.Add("bank_acct_no");
                    dt.Columns.Add("empl_code");
                    dt.Columns.Add("empl_name");
                    dt.Columns.Add("bank_code");
                    dt.Columns.Add("batch_date");
                    dt.Columns.Add("Account_Type");
                    dt.Columns.Add("ApplicationReferenceNo");

                    dspayroll.Tables.Add(dt);
                }
                else
                {
                    dspayroll.Tables[0].Columns[0].ColumnName = "bank_desc";
                    dspayroll.Tables[0].Columns[1].ColumnName = "amount";
                    dspayroll.Tables[0].Columns[2].ColumnName = "bank_acct_no";
                    //dspayroll.Tables[0].Columns[3].ColumnName = "empl_code";
                    dspayroll.Tables[0].Columns[4].ColumnName = "empl_name";
                    dspayroll.Tables[0].Columns[5].ColumnName = "bank_code";
                    dspayroll.Tables[0].Columns[6].ColumnName = "batch_date";
                    dspayroll.Tables[0].Columns[7].ColumnName = "Account_Type";
                    dspayroll.Tables[0].Columns[8].ColumnName = "ApplicationReferenceNo";
                }

                //if ((dspayroll?.Tables?.Count ?? 0) <= 0 || ((dspayroll?.Tables?.Count ?? 0) <= 0 && dspayroll.Tables[0].Rows.Count <= 0))
                //{
                //    dspayroll = new DataSet();

                //    DataTable dt = new DataTable();
                //    dt.Columns.Add("pybatchid");
                //    dt.Columns.Add("batchprocessid");
                //    dt.Columns.Add("processname");
                //    dt.Columns.Add("processstartedon");
                //    dt.Columns.Add("processendedon");
                //    dt.Columns.Add("recordssearched");
                //    dt.Columns.Add("recordsprocessed");
                //    dt.Columns.Add("status");
                //    dt.Columns.Add("searchcriteria");
                //    dt.Columns.Add("insertby");
                //    dt.Columns.Add("insertdate");
                //    dt.Columns.Add("insertmachineinfo");

                //    dspayroll.Tables.Add(dt);
                //}
                //else
                //{
                //    dspayroll.Tables[0].Columns[0].ColumnName = "pybatchid";
                //    dspayroll.Tables[0].Columns[1].ColumnName = "batchprocessid";
                //    dspayroll.Tables[0].Columns[2].ColumnName = "processname";
                //    dspayroll.Tables[0].Columns[3].ColumnName = "processstartedon";
                //    dspayroll.Tables[0].Columns[4].ColumnName = "processendedon";
                //    dspayroll.Tables[0].Columns[5].ColumnName = "recordssearched";
                //    dspayroll.Tables[0].Columns[6].ColumnName = "recordsprocessed";
                //    dspayroll.Tables[0].Columns[7].ColumnName = "status";
                //    dspayroll.Tables[0].Columns[8].ColumnName = "searchcriteria";
                //    dspayroll.Tables[0].Columns[8].ColumnName = "insertby";
                //    dspayroll.Tables[0].Columns[8].ColumnName = "insertdate";
                //    dspayroll.Tables[0].Columns[8].ColumnName = "insertmachineinfo";
                //}

                objDALBaseClass = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return dspayroll;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }
    }
}
