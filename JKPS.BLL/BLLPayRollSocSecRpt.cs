using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
namespace JKPS.BLL
{
   public  class BLLPayRollSocSecRpt
   {
       //public static DataSet GetSocSecRptData(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode)
       //{
       //    DataSet ds;
       //    DataSet DSRETURN = new DataSet();
       //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
       //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
       //    try
       //    {
       //        object[] parameters = new object[6];

       //        parameters[0] = objSearch.type_code.Trim();
       //        parameters[1] = objSearch.ok_to_post.Trim();
       //        parameters[2] = objSearch.period.Trim();
       //        parameters[3] = objSearch.year.Trim();
       //        parameters[4] = objSearch.startdate;//(objSearch.startdate != string.Empty ? Convert.ToDateTime(objSearch.startdate) : Convert.ToDateTime(null));
       //        parameters[5] = objSearch.enddate;//(objSearch.enddate != string.Empty ? Convert.ToDateTime(objSearch.enddate) : Convert.ToDateTime(null));
       //        ds = objDALBaseClass.GetData(objSearch.FIND_SOC_SEC_RPT(ref parameters, ref GlobalCode));
       //        if (ds.Tables[0].Rows.Count != 0)
       //        {
       //            ds.Tables[0].Columns[0].ColumnName = "v_addr1";
       //            ds.Tables[0].Columns[1].ColumnName = "v_addr2";
       //            ds.Tables[0].Columns[2].ColumnName = "v_city";
       //            ds.Tables[0].Columns[3].ColumnName = "v_co_name";
       //            ds.Tables[0].Columns[4].ColumnName = "v_country";
       //            ds.Tables[0].Columns[5].ColumnName = "v_state";
       //            ds.Tables[0].Columns[6].ColumnName = "v_zip";
       //            ds.Tables[0].Columns[7].ColumnName = "v_end_date";
       //            ds.Tables[0].Columns[8].ColumnName = "v_start_date";
       //            ds.Tables[0].Columns[9].ColumnName = "v_ein_number";
       //            ds.Tables[0].Columns[10].ColumnName = "v_date_hired";
       //            ds.Tables[0].Columns[11].ColumnName = "v_empl_code";
       //            ds.Tables[0].Columns[12].ColumnName = "v_first_name";
       //            ds.Tables[0].Columns[13].ColumnName = "v_last_name";
       //            ds.Tables[0].Columns[14].ColumnName = "v_middle_name";
       //            ds.Tables[0].Columns[15].ColumnName = "v_pay_period";
       //            ds.Tables[0].Columns[16].ColumnName = "v_soc_sec_num";
       //            ds.Tables[0].Columns[17].ColumnName = "v_terminated";
       //            ds.Tables[0].Columns[18].ColumnName = "v_doc_no";
       //            ds.Tables[0].Columns[19].ColumnName = "v_pay_date";
       //            ds.Tables[0].Columns.Add("v_num_rows");
       //            ds.Tables[0].Columns.Add("v_month_of");
       //            ds.Tables[0].Columns.Add("v_totsocsec");
       //            ds.Tables[0].Columns.Add("v_tot_ded_ssl");
       //            ds.Tables[0].Columns.Add("v_week1_amt");
       //            ds.Tables[0].Columns.Add("v_week2_amt");
       //            ds.Tables[0].Columns.Add("v_week3_amt");
       //            ds.Tables[0].Columns.Add("v_week4_amt");
       //            ds.Tables[0].Columns.Add("v_week5_amt");
       //            ds.Tables[0].Columns.Add("v_total_wages");
       //            ds.Tables[0].Columns.Add("v_wages_levy");
       //            ds.Tables[0].Columns.Add("v_socsec_contr");
       //            ds.Tables[0].Columns.Add("v_wages_sevpay");
       //            ds.Tables[0].Columns.Add("v_weeks_worked");
       //            ds.Tables[0].Columns.Add("v_comm_term");
       //            ds.Tables[0].Columns.Add("v_comm_term_date");
       //            DataTable DtFinal = new DataTable();
       //            DtFinal = ds.Tables[0].Clone();
       //            int v_weekCount = 0;
       //            string v_Week1 = " ";
       //            string v_Week2 = " ";
       //            string v_Week3 = " ";
       //            string v_Week4 = " ";
       //            string v_Week5 = " ";

       //            decimal v_Week1_amt = 0;
       //            decimal v_Week2_amt = 0;
       //            decimal v_Week3_amt = 0;
       //            decimal v_Week4_amt = 0;
       //            decimal v_Week5_amt = 0;
       //            int num_emp = 0;
       //            #region before frst row

       //            if (objSearch.period.Trim() == string.Empty)
       //            {
       //                ds.Tables[0].Rows[0]["v_month_of"] = objSearch.startdate + " to" + objSearch.enddate;
       //            }
       //            else
       //            {
       //                ds.Tables[0].Rows[0]["v_month_of"] = objSearch.period.Trim() + "/" + objSearch.year;
       //            }
       //            // get the total amount remitted to the Director of Social Security 
       //            decimal Local_Amount = 0;

       //            // DataSet ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_OBL_AMT_SSR(ref parameters, ref GlobalCode));
       //            // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
       //            //     dr = ds_ss_amount.Tables[0].Rows[0];
       //            //Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
       //            //Decimal TotalSocPaid = Local_Amount;
       //            //ds_ss_amount = null;
       //            // ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_DED_AMT_SSR(ref parameters, ref GlobalCode));

       //            // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
       //            //     Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
       //            //dr = ds_ss_amount.Tables[0].Rows[0];

       //            //  // Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
       //            //  ds.Tables[0].Rows[0]["v_totsocsec"] = TotalSocPaid + Local_Amount;




       //            //  //get the total SSL deductions for the report (total amount remitted to the Accountant General)
       //            //  Local_Amount = 0;
       //            //  DataSet ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_DED_AMT_SSR(ref parameters, ref GlobalCode));
       //            //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
       //            //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
       //            //  // dr = ds_ag_amount.Tables[0].Rows[0];
       //            //  //Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

       //            //  ds_ag_amount = null;
       //            //  ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_OBL_AMT_SSR(ref parameters, ref GlobalCode));
       //            //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
       //            //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
       //            //  // dr = ds_ag_amount.Tables[0].Rows[0];
       //            //  //  Local_Amount = Local_Amount+ (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

       //            //  ds.Tables[0].Rows[0]["v_tot_ded_ssl"] = Local_Amount;
       //            //  Decimal Tot_SSL_deductions = Local_Amount;

       //            //  Local_Amount = 0;
       //            #endregion

       //            int rownum = 0;
       //            DataRow Drnew = DtFinal.NewRow();

       //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
       //            {


       //                DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
       //                // Get amount for Levyee,Contrib and total wages
       //                object[] parameter = new object[1];
       //                parameter[0] = (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["v_doc_no"]) : 0);
       //                DataSet ds_amt = objDALBaseClass.GetData(objSearch.FIND_AMT_SUM_SSR(ref parameter, ref GlobalCode));
       //                ds_amt.Tables[0].Columns[0].ColumnName = "amount";
       //                ds_amt.Tables[0].Columns[1].ColumnName = "const";
       //                DataRow[] dr_amt = ds_amt.Tables[0].Select("", "const");
       //                // calculate the "Total Wages" for the employee
       //                ds.Tables[0].Rows[i]["v_total_wages"] = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);
       //                Drnew["v_total_wages"] = (Drnew["v_total_wages"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_total_wages"]) : 0) + Convert.ToDecimal(dr_amt[3][0]);
       //                Local_Amount = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);

       //                if (objSearch.type_code.Trim() == "WAGES")
       //                {
       //                    //must round the week up
       //                    //figure out what calendar monday we are on, then go back one week
       //                    //the calendar weeks are always one week behind the payroll weeks
       //                    //Modified on 27/07/2009 
       //                    Int32 payday = v_pay_date.Day;
       //                    if (payday >= 1 && payday <= 7)
       //                    {
       //                        v_weekCount = 1;
       //                    }
       //                    else if (payday >= 8 && payday <= 14)
       //                    {
       //                        v_weekCount = 2;

       //                    }
       //                    else if (payday >= 15 && payday <= 21)
       //                    {
       //                        v_weekCount = 3;
       //                    }
       //                    else if (payday >= 22 && payday <= 28)
       //                    {
       //                        v_weekCount = 4;
       //                    }
       //                    else if (payday > 28)
       //                    {
       //                        v_weekCount = 5;
       //                    }
       //                    //DateTime v_tmpDate = v_pay_date.AddDays(-(Convert.ToInt32(v_pay_date.DayOfWeek) ));
       //                    //v_weekCount = 1;
       //                    //while (v_tmpDate.AddDays(-(7 * v_weekCount)).Month == v_pay_date.Month)
       //                    //{
       //                    //    v_weekCount = v_weekCount + 1;
       //                    //}
       //                    switch (v_weekCount)
       //                    {
       //                        case 1:
       //                            v_Week1_amt = v_Week1_amt + Local_Amount;
       //                            ds.Tables[0].Rows[i]["v_week1_amt"] = v_Week1_amt;
       //                            v_Week1 = "X";
       //                            break;
       //                        case 2:
       //                            v_Week2_amt = v_Week2_amt + Local_Amount;
       //                            ds.Tables[0].Rows[i]["v_week2_amt"] = v_Week2_amt;
       //                            v_Week2 = "X";
       //                            break;
       //                        case 3:
       //                            v_Week3_amt = v_Week3_amt + Local_Amount;
       //                            ds.Tables[0].Rows[i]["v_week3_amt"] = v_Week3_amt;
       //                            v_Week3 = "X";
       //                            break;
       //                        case 4:
       //                            v_Week4_amt = v_Week4_amt + Local_Amount;
       //                            ds.Tables[0].Rows[i]["v_week4_amt"] = v_Week4_amt;
       //                            v_Week4 = "X";
       //                            break;
       //                        case 5:
       //                            v_Week5_amt = v_Week5_amt + Local_Amount;
       //                            ds.Tables[0].Rows[i]["v_week5_amt"] = v_Week5_amt;
       //                            v_Week5 = "X";
       //                            break;
       //                    }
       //                }
       //                //calculate the "Levy" for the employee
       //                Local_Amount = 0;
       //                //v_wages_levy
       //                Drnew["v_wages_levy"] = (Drnew["v_wages_levy"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_levy"]) : 0) + (dr_amt[0][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[0][0]) : 0);

       //                //calculate the "Contrib" for the employee
       //                //v_socsec_contr
       //                Local_Amount = (dr_amt[1][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[1][0]) : 0);
       //                Local_Amount = Local_Amount + (dr_amt[2][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[2][0]) : 0);
       //                Drnew["v_socsec_contr"] = (Drnew["v_socsec_contr"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_socsec_contr"]) : 0) + Local_Amount;

       //                //v_wages_sevpay
       //                Drnew["v_wages_sevpay"] = (Drnew["v_wages_sevpay"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_sevpay"]) : 0) + (dr_amt[4][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[4][0]) : 0);

       //                #region  Process on soc_sec_num
       //                bool _Status = false;
       //                if (ds.Tables[0].Rows.Count != i + 1)
       //                {
       //                    if (ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString().Trim() != ds.Tables[0].Rows[i + 1]["v_soc_sec_num"].ToString().Trim())
       //                    {
       //                        _Status = true;
       //                    }
       //                }
       //                else if (ds.Tables[0].Rows.Count == i + 1)
       //                    _Status = true;

       //                if (_Status)
       //                {
       //                    //DataRowCollection drn;
       //                    //drn = ds.Tables[0].Select("v_soc_sec_num= " + ds.Tables[0].Rows[i]["v_soc_sec_num"]);
       //                    if (ds.Tables[0].Rows[i]["v_addr1"] != DBNull.Value)
       //                    {
       //                        Drnew["v_addr1"] = ds.Tables[0].Rows[i]["v_addr1"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_addr2"] != DBNull.Value)
       //                    {
       //                        Drnew["v_addr2"] = ds.Tables[0].Rows[i]["v_addr2"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_city"] != DBNull.Value)
       //                    {
       //                        Drnew["v_city"] = ds.Tables[0].Rows[i]["v_city"].ToString();

       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_co_name"] != DBNull.Value)
       //                    {
       //                        Drnew["v_co_name"] = ds.Tables[0].Rows[i]["v_co_name"].ToString();

       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_country"] != DBNull.Value)
       //                    {
       //                        Drnew["v_country"] = ds.Tables[0].Rows[i]["v_country"].ToString();

       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_zip"] != DBNull.Value)
       //                    {
       //                        Drnew["v_zip"] = ds.Tables[0].Rows[i]["v_zip"].ToString();

       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value)
       //                    {
       //                        Drnew["v_end_date"] = ds.Tables[0].Rows[i]["v_end_date"].ToString();

       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value)
       //                    {
       //                        Drnew["v_start_date"] = ds.Tables[0].Rows[i]["v_start_date"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_ein_number"] != DBNull.Value)
       //                    {
       //                        Drnew["v_ein_number"] = ds.Tables[0].Rows[i]["v_ein_number"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_first_name"] != DBNull.Value)
       //                    {
       //                        Drnew["v_first_name"] = ds.Tables[0].Rows[i]["v_first_name"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_last_name"] != DBNull.Value)
       //                    {
       //                        Drnew["v_last_name"] = ds.Tables[0].Rows[i]["v_last_name"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_middle_name"] != DBNull.Value)
       //                    {
       //                        Drnew["v_middle_name"] = ds.Tables[0].Rows[i]["v_middle_name"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_pay_period"] != DBNull.Value)
       //                    {
       //                        Drnew["v_pay_period"] = ds.Tables[0].Rows[i]["v_pay_period"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_soc_sec_num"] != DBNull.Value)
       //                    {
       //                        Drnew["v_soc_sec_num"] = ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString();
       //                    }
       //                    // Drnew["v_terminated"] = ds.Tables[0].Rows[i]["v_terminated"].ToString();
       //                    if (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value)
       //                    {
       //                        Drnew["v_doc_no"] = ds.Tables[0].Rows[i]["v_doc_no"].ToString();
       //                    }
       //                    if (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value)
       //                    {
       //                        Drnew["v_pay_date"] = ds.Tables[0].Rows[i]["v_pay_date"].ToString();
       //                    }

       //                    Drnew["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
       //                    Drnew["v_week1_amt"] = v_Week1_amt;
       //                    Drnew["v_week2_amt"] = v_Week2_amt;
       //                    Drnew["v_week3_amt"] = v_Week3_amt;
       //                    Drnew["v_week4_amt"] = v_Week4_amt;
       //                    Drnew["v_week5_amt"] = v_Week5_amt;



       //                    //ds.Tables[0].Rows[i]["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
       //                    //determine if this is a new employee ("C" for Commencement Date) or if
       //                    //the employee has been terminated ("T")
       //                    DateTime date_hired = (ds.Tables[0].Rows[i]["v_date_hired"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_date_hired"]) : Convert.ToDateTime(null));
       //                    DateTime terminated = (ds.Tables[0].Rows[i]["v_terminated"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_terminated"]) : Convert.ToDateTime(null));
       //                    DateTime start_date = (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_start_date"]) : Convert.ToDateTime(null));
       //                    DateTime end_date = (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_end_date"]) : Convert.ToDateTime(null));

       //                    if ((date_hired >= start_date) && (date_hired <= end_date))
       //                    {
       //                        Drnew["v_comm_term"] = "C";
       //                        Drnew["v_comm_term_date"] = date_hired;
       //                    }
       //                    if (terminated >= start_date && terminated <= end_date)
       //                    {
       //                        Drnew["v_comm_term"] = "T";
       //                        Drnew["v_comm_term_date"] = terminated;
       //                    }
       //                    if (objSearch.type_code.Trim() == "WAGES")
       //                    {
       //                        v_Week1_amt = 0;
       //                        v_Week2_amt = 0;
       //                        v_Week3_amt = 0;
       //                        v_Week4_amt = 0;
       //                        v_Week5_amt = 0;
       //                    }
       //                    v_Week1 = " ";
       //                    v_Week2 = " ";
       //                    v_Week3 = " ";
       //                    v_Week4 = " ";
       //                    v_Week5 = " ";
       //                    num_emp++;
       //                    rownum++;
       //                    //Drnew["v_num_rows"] = ds.Tables[0].Rows.Count;
       //                    //Drnew["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
       //                    //Drnew["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
       //                    if (ds.Tables[0].Rows[0]["v_month_of"] != DBNull.Value)
       //                    {
       //                        Drnew["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];
       //                    }
       //                    DtFinal.Rows.Add(Drnew);
       //                    Drnew = DtFinal.NewRow();
       //                }
       //                #endregion

       //                ds.Tables[0].Rows[i]["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
       //                ds.Tables[0].Rows[i]["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
       //                ds.Tables[0].Rows[i]["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];

       //            }

       //            DSRETURN.Tables.Add(DtFinal);
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        ExceptionManagement.ExceptionManager.Publish(ex);
       //        throw ex;
       //    }

       //    return DSRETURN;
       //}

       //public static List<DVODistinctMonth> GetDistinctMonth_stxperdr()
       //{
       //    List<DVODistinctMonth> objDistMList = new List<DVODistinctMonth>();
       //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
       //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
       //    using (DataSet ds = objDalBaseClass.GetData(typeof(DVODistinctMonth),(new DVODistinctMonth()).GET_DISTICT_MONTH_XPERDR))
       //    {
       //        foreach (DataRow dr in ds.Tables[0].Rows)
       //        {
       //            DVODistinctMonth objDistM = new DVODistinctMonth();
       //            if (dr[0].ToString() != "")
       //            {
       //                objDistM.month = dr[0].ToString();
       //                objDistMList.Add(objDistM);
       //            }


       //        }
       //        return objDistMList;
       //    }
       //}
       //public static List<DVODistinctYear> GetDistinctYear_stxperdr()
       //{
       //    List<DVODistinctYear> objDistYList = new List<DVODistinctYear>();
       //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
       //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
       //    using (DataSet ds = objDalBaseClass.GetData(typeof(DVODistinctYear),(new DVODistinctYear()).GET_DISTICT_YEAR_XPERDR))
       //    {
       //        foreach (DataRow dr in ds.Tables[0].Rows)
       //        {
       //            DVODistinctYear objDistY = new DVODistinctYear();
       //            if (dr[0].ToString() != "")
       //            {
       //                objDistY.year = dr[0].ToString();
       //                objDistYList.Add(objDistY);
       //            }
       //        }
       //        return objDistYList;
       //    }
       //}

       //public static List<string> PrepareDataForSocSecInTxtFile(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode, out string fileName)
       //{   
       //    List<string> FinallistStr = new List<string>();
       //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
       //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
       //    fileName = string.Empty;
       //    try
       //    {
       //        object[] parameters = new object[6];

       //        parameters[0] = objSearch.type_code;
       //        parameters[1] = string.Empty;
       //        parameters[2] = objSearch.period;
       //        parameters[3] = objSearch.year;
       //        parameters[4] = string.Empty;
       //        parameters[5] = string.Empty;

       //        DataSet ds = objDALBaseClass.GetData(objSearch.FIND_SOC_SEC_RPT(ref parameters, ref GlobalCode));

       //        if (ds.Tables[0].Rows.Count > 0)
       //        {
       //            #region Set Columns Names.
       //            ds.Tables[0].Columns[0].ColumnName = "v_addr1";
       //            ds.Tables[0].Columns[1].ColumnName = "v_addr2";
       //            ds.Tables[0].Columns[2].ColumnName = "v_city";
       //            ds.Tables[0].Columns[3].ColumnName = "v_co_name";
       //            ds.Tables[0].Columns[4].ColumnName = "v_country";
       //            ds.Tables[0].Columns[5].ColumnName = "v_state";
       //            ds.Tables[0].Columns[6].ColumnName = "v_zip";
       //            ds.Tables[0].Columns[7].ColumnName = "v_end_date";
       //            ds.Tables[0].Columns[8].ColumnName = "v_start_date";
       //            ds.Tables[0].Columns[9].ColumnName = "v_ein_number";
       //            ds.Tables[0].Columns[10].ColumnName = "v_date_hired";
       //            ds.Tables[0].Columns[11].ColumnName = "v_empl_code";
       //            ds.Tables[0].Columns[12].ColumnName = "v_first_name";
       //            ds.Tables[0].Columns[13].ColumnName = "v_last_name";
       //            ds.Tables[0].Columns[14].ColumnName = "v_middle_name";
       //            ds.Tables[0].Columns[15].ColumnName = "v_pay_period";
       //            ds.Tables[0].Columns[16].ColumnName = "v_soc_sec_num";
       //            ds.Tables[0].Columns[17].ColumnName = "v_terminated";
       //            ds.Tables[0].Columns[18].ColumnName = "v_doc_no";
       //            ds.Tables[0].Columns[19].ColumnName = "v_pay_date";
       //            #endregion

       //            #region Initialize Global Variables..
       //            string hDrLine = string.Empty;
       //            string fTrLine = string.Empty;
       //            string period = string.Empty;
       //            string comName = string.Empty;
                   
       //            string LINE = string.Empty;
       //            string IntLine = string.Empty;
       //            int numLineNo = 1;
                   
       //            int totRecords = ds.Tables[0].Rows.Count;
       //            int totRcordsLength = totRecords.ToString().Trim().Length;
       //            if (totRcordsLength > 3)
       //            {
       //                for (int j = 0; j < totRcordsLength; j++)
       //                    IntLine = IntLine + "0";
       //            }
       //            else
       //            {
       //                IntLine = "000";
       //            }

       //            decimal gloAmtCTRLTTL = 0;
       //            decimal gloAmtTTLSS = 0;
       //            decimal gloAmtTTTLV = 0;
       //            decimal gloAmtTTTPE = 0;

       //            string v_Week1 = "0";
       //            string v_Week2 = "0";
       //            string v_Week3 = "0";
       //            string v_Week4 = "0";
       //            string v_Week5 = "0";
       //            decimal v_Week1_amt = 0;
       //            decimal v_Week2_amt = 0;
       //            decimal v_Week3_amt = 0;
       //            decimal v_Week4_amt = 0;
       //            decimal v_Week5_amt = 0;
       //            #endregion

       //            #region Prepare Header Line..

       //            //Get Registeration No..
       //            string regNo = ds.Tables[0].Rows[0]["v_ein_number"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["v_ein_number"]).Trim() : string.Empty;
       //            if (regNo.Trim().Length <= 0)
       //            {
       //                throw new Exception("Error : Registeration No. Not Found");
       //            }
       //            //File Name..
       //            fileName = regNo.Trim() + objSearch.period.Trim() + objSearch.year.Trim() + ".C3";

       //            period = "01/" + objSearch.period.Trim() + "/" + objSearch.year.Trim();

       //            comName = ds.Tables[0].Rows[0]["v_co_name"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["v_co_name"]).Trim() : string.Empty;
       //            if (comName.Trim().Length <= 0)
       //            {
       //                throw new Exception("Error : Company Name Not Found");
       //            }
       //            //Prepare Header Line..
       //            hDrLine = "HDR, " + regNo.Trim() + ", " + period + ", " + "1.0.0, " + comName.Trim();
       //            FinallistStr.Add(hDrLine);

       //            #endregion

       //            #region Prepare Detail Lines..

       //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
       //            {
       //                #region Initialize Local Variables..
       //                string detailLine = string.Empty;
       //                string SSN = string.Empty;
       //                string LASTNAME = string.Empty;
       //                string FIRSTNAME = string.Empty;
       //                string MIDDLENAME = string.Empty;
       //                string CMNCEDATE = string.Empty;
       //                string TERMDATE = string.Empty;
       //                string PAYFREQ = string.Empty;
       //                string HOLPAY ="0";
       //                string BONUS = "0";
       //                string payperiod = string.Empty;
       //                int v_weekCount = 0;
                       

       //                decimal locAmount = 0;
       //                decimal  locAmtLEVY = 0;
       //                decimal locAmtSOCSEC = 0;

       //                decimal locAmtHOLPAY = 0;
       //                decimal locAmtBONUS = 0;
                       
       //                #endregion

       //                DataRow dr = ds.Tables[0].Rows[i];

       //                #region LINE..
       //                LINE = IntLine;
       //                LINE = LINE.Remove(totRcordsLength - numLineNo.ToString().Length, numLineNo.ToString().Length);
       //                LINE = LINE + numLineNo.ToString();
                       
       //                #endregion

       //                #region SSN...
       //                SSN = dr["v_soc_sec_num"] != DBNull.Value ? Convert.ToString(dr["v_soc_sec_num"]).Trim() : string.Empty;
       //                if (SSN.Trim().Length <= 0)
       //                    throw new Exception("Error : Soc_Sec_Num Not Found");
       //                #endregion

       //                #region EMP NAME...
       //                LASTNAME = dr["v_last_name"] != DBNull.Value ? Convert.ToString(dr["v_last_name"]).Trim() : string.Empty;
       //                FIRSTNAME = dr["v_first_name"] != DBNull.Value ? Convert.ToString(dr["v_first_name"]).Trim() : string.Empty;
       //                MIDDLENAME = dr["v_middle_name"] != DBNull.Value ? Convert.ToString(dr["v_middle_name"]).Trim() : string.Empty;
       //                #endregion

       //                #region CMNCEDATE AND TERMDATE...
       //                if (dr["v_date_hired"] != DBNull.Value)
       //                    if (Convert.ToString(dr["v_date_hired"]).Trim().Length > 0)
       //                        CMNCEDATE = Convert.ToDateTime(dr["v_date_hired"]).ToString("dd/MM/yyyy");

       //                if (dr["v_terminated"] != DBNull.Value)
       //                    if (Convert.ToString(dr["v_terminated"]).Trim().Length > 0)
       //                        TERMDATE = Convert.ToDateTime(dr["v_terminated"]).ToString("dd/MM/yyyy");
       //                #endregion

       //                #region PAYFREQ..
       //                payperiod = dr["v_pay_period"] != DBNull.Value ? Convert.ToString(dr["v_pay_period"]).Trim() : string.Empty;
       //                switch (payperiod)
       //                {
       //                    case "M":
       //                        PAYFREQ = "3";
       //                        break;
       //                    case "W":
       //                        PAYFREQ = "1";
       //                        break;
       //                }
       //                #endregion

       //                #region WK1 ....WK5 AND PAY1.....PAY5
       //                int docNo = dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0;

       //                List<decimal> listAmounts = GetAmounts(docNo, ref GlobalCode);
                       
       //                //TotalWages
       //                locAmount = listAmounts[3];
       //                DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
       //                if (objSearch.type_code.Trim() == "WAGES")
       //                {

       //                    Int32 payday = v_pay_date.Day;
       //                    if (payday >= 1 && payday <= 7)
       //                    {
       //                        v_weekCount = 1;
       //                    }
       //                    else if (payday >= 8 && payday <= 14)
       //                    {
       //                        v_weekCount = 2;

       //                    }
       //                    else if (payday >= 15 && payday <= 21)
       //                    {
       //                        v_weekCount = 3;
       //                    }
       //                    else if (payday >= 22 && payday <= 28)
       //                    {
       //                        v_weekCount = 4;
       //                    }
       //                    else if (payday > 28)
       //                    {
       //                        v_weekCount = 5;
       //                    }
       //                    switch (v_weekCount)
       //                    {
       //                        case 1:
       //                            v_Week1_amt = locAmount;
       //                            v_Week1 = "1";
       //                            break;
       //                        case 2:
       //                            v_Week2_amt = locAmount;
       //                            v_Week2 = "1";
       //                            break;
       //                        case 3:
       //                            v_Week3_amt =  locAmount;
       //                            v_Week3 = "1";
       //                            break;
       //                        case 4:
       //                            v_Week4_amt =  locAmount;
       //                            v_Week4 = "1";
       //                            break;
       //                        case 5:
       //                            v_Week5_amt =  locAmount;
       //                            v_Week5 = "1";
       //                            break;
       //                    }
       //                }
       //                #endregion

       //                #region HOLPAID and BPAID..
       //                //BONUS
       //                if (listAmounts[4] != 0)
       //                {
       //                    locAmtBONUS = listAmounts[4];
       //                    BONUS = "1";
       //                }
       //                //HOLPAY
       //                if (listAmounts[5] != 0)
       //                {
       //                    locAmtHOLPAY = listAmounts[5];
       //                    HOLPAY = "1";
       //                }
       //                #endregion

       //                #region LEVY ,SOCSEC ,CTRLTTL ,TTLSS,TTTLV,TTTPE..
       //                //LEVY
       //                locAmtLEVY = listAmounts[0];
       //                gloAmtTTTLV = gloAmtTTTLV + locAmtLEVY;
       //                //SOCSEC
       //                locAmtSOCSEC = listAmounts[1];
       //                gloAmtTTLSS = gloAmtTTLSS + locAmtSOCSEC;
       //                //CTRLTTL
       //                gloAmtCTRLTTL = gloAmtCTRLTTL + listAmounts[3];
       //                //TTTPE
       //                gloAmtTTTPE = gloAmtTTTPE + listAmounts[2];
       //                #endregion

       //                bool _status = false;
       //                if (ds.Tables[0].Rows.Count != i + 1)
       //                {
       //                    if (Convert.ToString(dr["v_soc_sec_num"]).Trim() != Convert.ToString(ds.Tables[0].Rows[i + 1]["v_soc_sec_num"]).Trim())
       //                    {
       //                        _status = true;
       //                    }
       //                }
       //                else if (ds.Tables[0].Rows.Count == i + 1)
       //                    _status = true;

       //                if (_status)
       //                {


       //                    #region Prepare Detail Line ..
       //                    detailLine = LINE + ", " + SSN + ", " + LASTNAME + ", " + FIRSTNAME + ", " + MIDDLENAME + ", " + CMNCEDATE + ", " + TERMDATE + ", " + PAYFREQ + ", ";
       //                    detailLine = detailLine + v_Week1 + ", " + v_Week2 + ", " + v_Week3 + ", " + v_Week4 + ", " + v_Week5 + ", ";
       //                    detailLine = detailLine + HOLPAY + ", " + BONUS + ", " + v_Week1_amt + ", " + v_Week2_amt + ", " + v_Week3_amt + ", ";
       //                    detailLine = detailLine + v_Week4_amt + ", " + v_Week5_amt + ", " + locAmtHOLPAY + ", " + locAmtBONUS + ", " + locAmtLEVY + ", " + locAmtSOCSEC;
       //                    FinallistStr.Add(detailLine);
       //                    #endregion

       //                    if (objSearch.type_code.Trim() == "WAGES")
       //                    {
       //                        v_Week1_amt = 0;
       //                        v_Week2_amt = 0;
       //                        v_Week3_amt = 0;
       //                        v_Week4_amt = 0;
       //                        v_Week5_amt = 0;
       //                    }
       //                    v_Week1 = "0";
       //                    v_Week2 = "0";
       //                    v_Week3 = "0";
       //                    v_Week4 = "0";
       //                    v_Week5 = "0";
       //                    if (ds.Tables[0].Rows.Count != i + 1)
       //                        numLineNo = numLineNo + 1;
       //                }

       //            }
       //            #endregion

       //            #region Prepare Footer Line..
       //            fTrLine = "FTR, " + regNo + ", " + period + ", " + gloAmtCTRLTTL + ", " + gloAmtTTLSS + ", " + gloAmtTTTLV + ", " + gloAmtTTTPE + ", " + numLineNo;
       //            FinallistStr.Add(fTrLine);
       //            #endregion

       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        ExceptionManagement.ExceptionManager.Publish(ex);
       //        throw ex;

       //    }
       //    return FinallistStr;
       //}
        
       ////Written by Sarvjeet On 26/11/2009 ..
       //public static List<decimal> GetAmounts(int docNo, ref string[] GlobalCode)
       //{
       //    // This function will return list of amounts for (LEVY ,SOCSEC,TTTPE,TotalWages,BONUS,HOLPAY).
       //    // These amounts will be set on index basis into List object..
       //    //At Index :
       //    //0- LEVY
       //    //1- SOCSEC1+ SOCSEC2
       //    //2- TTTPE
       //    //3- TotalWages
       //    //4- BONUS
       //    //5- HOLPAY
       //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
       //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
       //    List<decimal> amountList = new List<decimal>();
       //    try
       //    {
                
       //        object[] parameter = new object[1];
       //        parameter[0] = docNo;
       //        DataSet ds = objDALBaseClass.GetData((new DVOPaymentToEmployee()).FIND_AMT_FOR_SSTF(ref parameter, ref GlobalCode));
       //        ds.Tables[0].Columns[0].ColumnName = "amount";
       //        ds.Tables[0].Columns[1].ColumnName = "const";
       //        DataRow[] dra = ds.Tables[0].Select("","const");
       //        //0-LEVY
       //        amountList.Add(dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0);
       //        //1-SOCSEC=SOCSEC1+SOCSEC2
       //        amountList.Add((dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0) + (dra[2][0] != DBNull.Value ? Convert.ToDecimal(dra[2][0]) : 0));
       //        //2-TTTPE
       //        amountList.Add(dra[3][0] != DBNull.Value ? Convert.ToDecimal(dra[3][0]) : 0);
       //        //3-TotalWages
       //        amountList.Add(dra[4][0] != DBNull.Value ? Convert.ToDecimal(dra[4][0]) : 0);
       //        //4-BONUS
       //        amountList.Add(dra[5][0] != DBNull.Value ? Convert.ToDecimal(dra[5][0]) : 0);
       //        //5-HOLPAY
       //        amountList.Add(dra[6][0] != DBNull.Value ? Convert.ToDecimal(dra[6][0]) : 0);
       //    }
       //    catch(Exception ex)
       //    {
       //        ExceptionManagement.ExceptionManager.Publish(ex);
       //        throw ex;
       //    }
       //    return amountList;
       //}

       public static DataSet GetSocSecRptData(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode)
       {
           DataSet ds;
           DataSet DSRETURN = new DataSet();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           String EmployeeID = string.Empty;
           try
           {
               object[] parameters = new object[6];

               parameters[0] = objSearch.type_code.Trim();
               parameters[1] = objSearch.ok_to_post.Trim();
               parameters[2] = objSearch.period.Trim();
               parameters[3] = objSearch.year.Trim();
               parameters[4] = objSearch.startdate;//(objSearch.startdate != string.Empty ? Convert.ToDateTime(objSearch.startdate) : Convert.ToDateTime(null));
               parameters[5] = objSearch.enddate;//(objSearch.enddate != string.Empty ? Convert.ToDateTime(objSearch.enddate) : Convert.ToDateTime(null));
               ds = objDALBaseClass.GetData(objSearch.FIND_SOC_SEC_RPT(ref parameters, ref GlobalCode));
             
                   ds.Tables[0].Columns[0].ColumnName = "v_addr1";
                   ds.Tables[0].Columns[1].ColumnName = "v_addr2";
                   ds.Tables[0].Columns[2].ColumnName = "v_city";
                   ds.Tables[0].Columns[3].ColumnName = "v_co_name";
                   ds.Tables[0].Columns[4].ColumnName = "v_country";
                   ds.Tables[0].Columns[5].ColumnName = "v_state";
                   ds.Tables[0].Columns[6].ColumnName = "v_zip";
                   ds.Tables[0].Columns[7].ColumnName = "v_end_date";
                   ds.Tables[0].Columns[8].ColumnName = "v_start_date";
                   ds.Tables[0].Columns[9].ColumnName = "v_ein_number";
                   ds.Tables[0].Columns[10].ColumnName = "v_date_hired";
                   ds.Tables[0].Columns[11].ColumnName = "v_empl_code";
                   ds.Tables[0].Columns[12].ColumnName = "v_first_name";
                   ds.Tables[0].Columns[13].ColumnName = "v_last_name";
                   ds.Tables[0].Columns[14].ColumnName = "v_middle_name";
                   ds.Tables[0].Columns[15].ColumnName = "v_pay_period";
                   ds.Tables[0].Columns[16].ColumnName = "v_soc_sec_num";
                   ds.Tables[0].Columns[17].ColumnName = "v_terminated";
                   ds.Tables[0].Columns[18].ColumnName = "v_doc_no";
                   ds.Tables[0].Columns[19].ColumnName = "v_pay_date";
                   ds.Tables[0].Columns.Add("v_num_rows");
                   ds.Tables[0].Columns.Add("v_month_of");
                   ds.Tables[0].Columns.Add("v_totsocsec");
                   ds.Tables[0].Columns.Add("v_tot_ded_ssl");
                   ds.Tables[0].Columns.Add("v_week1_amt");
                   ds.Tables[0].Columns.Add("v_week2_amt");
                   ds.Tables[0].Columns.Add("v_week3_amt");
                   ds.Tables[0].Columns.Add("v_week4_amt");
                   ds.Tables[0].Columns.Add("v_week5_amt");
                   ds.Tables[0].Columns.Add("v_total_wages");
                   ds.Tables[0].Columns.Add("v_wages_levy");
                   ds.Tables[0].Columns.Add("v_socsec_contr");
                   ds.Tables[0].Columns.Add("v_wages_sevpay");
                   ds.Tables[0].Columns.Add("v_weeks_worked");
                   ds.Tables[0].Columns.Add("v_comm_term");
                   ds.Tables[0].Columns.Add("v_comm_term_date");
                   DataTable DtFinal = new DataTable();
                   DtFinal = ds.Tables[0].Clone();
                   int v_weekCount = 0;
                   string v_Week1 = " ";
                   string v_Week2 = " ";
                   string v_Week3 = " ";
                   string v_Week4 = " ";
                   string v_Week5 = " ";

                   decimal v_Week1_amt = 0;
                   decimal v_Week2_amt = 0;
                   decimal v_Week3_amt = 0;
                   decimal v_Week4_amt = 0;
                   decimal v_Week5_amt = 0;
                   int num_emp = 0;
                   #region before frst row

                   if (objSearch.period.Trim() == string.Empty)
                   {
                       ds.Tables[0].Rows[0]["v_month_of"] = objSearch.startdate + " to" + objSearch.enddate;
                   }
                   else
                   {
                       ds.Tables[0].Rows[0]["v_month_of"] = objSearch.period.Trim() + "/" + objSearch.year;
                   }
                   // get the total amount remitted to the Director of Social Security 
                   decimal Local_Amount = 0;

                   // DataSet ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_OBL_AMT_SSR(ref parameters, ref GlobalCode));
                   // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
                   //     dr = ds_ss_amount.Tables[0].Rows[0];
                   //Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
                   //Decimal TotalSocPaid = Local_Amount;
                   //ds_ss_amount = null;
                   // ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_DED_AMT_SSR(ref parameters, ref GlobalCode));

                   // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
                   //     Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
                   //dr = ds_ss_amount.Tables[0].Rows[0];

                   //  // Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
                   //  ds.Tables[0].Rows[0]["v_totsocsec"] = TotalSocPaid + Local_Amount;




                   //  //get the total SSL deductions for the report (total amount remitted to the Accountant General)
                   //  Local_Amount = 0;
                   //  DataSet ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_DED_AMT_SSR(ref parameters, ref GlobalCode));
                   //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
                   //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
                   //  // dr = ds_ag_amount.Tables[0].Rows[0];
                   //  //Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

                   //  ds_ag_amount = null;
                   //  ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_OBL_AMT_SSR(ref parameters, ref GlobalCode));
                   //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
                   //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
                   //  // dr = ds_ag_amount.Tables[0].Rows[0];
                   //  //  Local_Amount = Local_Amount+ (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

                   //  ds.Tables[0].Rows[0]["v_tot_ded_ssl"] = Local_Amount;
                   //  Decimal Tot_SSL_deductions = Local_Amount;

                   //  Local_Amount = 0;
                   #endregion

                   int rownum = 0;
                   DataRow Drnew = DtFinal.NewRow();

                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                   {
                       EmployeeID = ds.Tables[0].Rows[i]["v_empl_code"].ToString();
                       DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
                       // Get amount for Levyee,Contrib and total wages
                       object[] parameter = new object[1];
                       parameter[0] = (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["v_doc_no"]) : 0);
                       DataSet ds_amt =null;
                       ds_amt = objDALBaseClass.GetData(objSearch.FIND_AMT_SUM_SSR(ref parameter, ref GlobalCode));
                        if (ds_amt.Tables.Count > 0)
                        {
                             if (ds_amt.Tables[0].Rows.Count != 0)
                        {
                       
                       
                       ds_amt.Tables[0].Columns[0].ColumnName = "amount";
                       ds_amt.Tables[0].Columns[1].ColumnName = "const";
                       DataRow[] dr_amt = ds_amt.Tables[0].Select("", "const");
                       // calculate the "Total Wages" for the employee
                       ds.Tables[0].Rows[i]["v_total_wages"] = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);
                       Drnew["v_total_wages"] = (Drnew["v_total_wages"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_total_wages"]) : 0) + Convert.ToDecimal(dr_amt[3][0]);
                       Local_Amount = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);

                       if (objSearch.type_code.Trim() == "WAGES")
                       {
                           //must round the week up
                           //figure out what calendar monday we are on, then go back one week
                           //the calendar weeks are always one week behind the payroll weeks
                           //Modified on 27/07/2009 
                           Int32 payday = v_pay_date.Day;
                           if (payday >= 1 && payday <= 7)
                           {
                               v_weekCount = 1;
                           }
                           else if (payday >= 8 && payday <= 14)
                           {
                               v_weekCount = 2;

                           }
                           else if (payday >= 15 && payday <= 21)
                           {
                               v_weekCount = 3;
                           }
                           else if (payday >= 22 && payday <= 28)
                           {
                               v_weekCount = 4;
                           }
                           else if (payday > 28)
                           {
                               v_weekCount = 5;
                           }
                           //DateTime v_tmpDate = v_pay_date.AddDays(-(Convert.ToInt32(v_pay_date.DayOfWeek) ));
                           //v_weekCount = 1;
                           //while (v_tmpDate.AddDays(-(7 * v_weekCount)).Month == v_pay_date.Month)
                           //{
                           //    v_weekCount = v_weekCount + 1;
                           //}
                           switch (v_weekCount)
                           {
                               case 1:
                                   v_Week1_amt = v_Week1_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week1_amt"] = v_Week1_amt;
                                   v_Week1 = "X";
                                   break;
                               case 2:
                                   v_Week2_amt = v_Week2_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week2_amt"] = v_Week2_amt;
                                   v_Week2 = "X";
                                   break;
                               case 3:
                                   v_Week3_amt = v_Week3_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week3_amt"] = v_Week3_amt;
                                   v_Week3 = "X";
                                   break;
                               case 4:
                                   v_Week4_amt = v_Week4_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week4_amt"] = v_Week4_amt;
                                   v_Week4 = "X";
                                   break;
                               case 5:
                                   v_Week5_amt = v_Week5_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week5_amt"] = v_Week5_amt;
                                   v_Week5 = "X";
                                   break;
                           }
                       }
                       //calculate the "Levy" for the employee
                       Local_Amount = 0;
                       //v_wages_levy
                       Drnew["v_wages_levy"] = (Drnew["v_wages_levy"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_levy"]) : 0) + (dr_amt[0][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[0][0]) : 0);

                       //calculate the "Contrib" for the employee
                       //v_socsec_contr
                       Local_Amount = (dr_amt[1][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[1][0]) : 0);
                       Local_Amount = Local_Amount + (dr_amt[2][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[2][0]) : 0);
                       Drnew["v_socsec_contr"] = (Drnew["v_socsec_contr"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_socsec_contr"]) : 0) + Local_Amount;

                       //v_wages_sevpay
                       Drnew["v_wages_sevpay"] = (Drnew["v_wages_sevpay"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_sevpay"]) : 0) + (dr_amt[4][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[4][0]) : 0);

                       #region  Process on soc_sec_num
                       bool _Status = false;
                       if (ds.Tables[0].Rows.Count != i + 1)
                       {
                           if (ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString().Trim() != ds.Tables[0].Rows[i + 1]["v_soc_sec_num"].ToString().Trim())
                           {
                               _Status = true;
                           }
                       }
                       else if (ds.Tables[0].Rows.Count == i + 1)
                           _Status = true;

                       if (_Status)
                       {
                           //DataRowCollection drn;
                           //drn = ds.Tables[0].Select("v_soc_sec_num= " + ds.Tables[0].Rows[i]["v_soc_sec_num"]);
                           if (ds.Tables[0].Rows[i]["v_addr1"] != DBNull.Value)
                           {
                               Drnew["v_addr1"] = ds.Tables[0].Rows[i]["v_addr1"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_addr2"] != DBNull.Value)
                           {
                               Drnew["v_addr2"] = ds.Tables[0].Rows[i]["v_addr2"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_city"] != DBNull.Value)
                           {
                               Drnew["v_city"] = ds.Tables[0].Rows[i]["v_city"].ToString();

                           }
                           if (ds.Tables[0].Rows[i]["v_co_name"] != DBNull.Value)
                           {
                               Drnew["v_co_name"] = ds.Tables[0].Rows[i]["v_co_name"].ToString();

                           }
                           if (ds.Tables[0].Rows[i]["v_country"] != DBNull.Value)
                           {
                               Drnew["v_country"] = ds.Tables[0].Rows[i]["v_country"].ToString();

                           }
                           if (ds.Tables[0].Rows[i]["v_zip"] != DBNull.Value)
                           {
                               Drnew["v_zip"] = ds.Tables[0].Rows[i]["v_zip"].ToString();

                           }
                           if (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value)
                           {
                               Drnew["v_end_date"] = ds.Tables[0].Rows[i]["v_end_date"].ToString();

                           }
                           if (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value)
                           {
                               Drnew["v_start_date"] = ds.Tables[0].Rows[i]["v_start_date"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_ein_number"] != DBNull.Value)
                           {
                               Drnew["v_ein_number"] = ds.Tables[0].Rows[i]["v_ein_number"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_first_name"] != DBNull.Value)
                           {
                               Drnew["v_first_name"] = ds.Tables[0].Rows[i]["v_first_name"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_last_name"] != DBNull.Value)
                           {
                               Drnew["v_last_name"] = ds.Tables[0].Rows[i]["v_last_name"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_middle_name"] != DBNull.Value)
                           {
                               Drnew["v_middle_name"] = ds.Tables[0].Rows[i]["v_middle_name"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_pay_period"] != DBNull.Value)
                           {
                               Drnew["v_pay_period"] = ds.Tables[0].Rows[i]["v_pay_period"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_soc_sec_num"] != DBNull.Value)
                           {
                               Drnew["v_soc_sec_num"] = ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString();
                           }
                           // Drnew["v_terminated"] = ds.Tables[0].Rows[i]["v_terminated"].ToString();
                           if (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value)
                           {
                               Drnew["v_doc_no"] = ds.Tables[0].Rows[i]["v_doc_no"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value)
                           {
                               Drnew["v_pay_date"] = ds.Tables[0].Rows[i]["v_pay_date"].ToString();
                           }

                           Drnew["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                           Drnew["v_week1_amt"] = v_Week1_amt;
                           Drnew["v_week2_amt"] = v_Week2_amt;
                           Drnew["v_week3_amt"] = v_Week3_amt;
                           Drnew["v_week4_amt"] = v_Week4_amt;
                           Drnew["v_week5_amt"] = v_Week5_amt;



                           //ds.Tables[0].Rows[i]["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                           //determine if this is a new employee ("C" for Commencement Date) or if
                           //the employee has been terminated ("T")
                           DateTime date_hired = (ds.Tables[0].Rows[i]["v_date_hired"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_date_hired"]) : Convert.ToDateTime(null));
                           DateTime terminated = (ds.Tables[0].Rows[i]["v_terminated"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_terminated"]) : Convert.ToDateTime(null));
                           DateTime start_date = (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_start_date"]) : Convert.ToDateTime(null));
                           DateTime end_date = (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_end_date"]) : Convert.ToDateTime(null));

                           if ((date_hired >= start_date) && (date_hired <= end_date))
                           {
                               Drnew["v_comm_term"] = "C";
                               Drnew["v_comm_term_date"] = date_hired;
                           }
                           else
                           {
                               Drnew["v_comm_term_date"] = date_hired;
                           }
                           if (terminated >= start_date && terminated <= end_date)
                           {
                               Drnew["v_comm_term"] = "T";
                               Drnew["v_comm_term_date"] = terminated;
                           }
                           if (objSearch.type_code.Trim() == "WAGES")
                           {
                               v_Week1_amt = 0;
                               v_Week2_amt = 0;
                               v_Week3_amt = 0;
                               v_Week4_amt = 0;
                               v_Week5_amt = 0;
                           }
                           v_Week1 = " ";
                           v_Week2 = " ";
                           v_Week3 = " ";
                           v_Week4 = " ";
                           v_Week5 = " ";
                           num_emp++;
                           rownum++;
                           //Drnew["v_num_rows"] = ds.Tables[0].Rows.Count;
                           //Drnew["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                           //Drnew["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                           if (ds.Tables[0].Rows[0]["v_month_of"] != DBNull.Value)
                           {
                               Drnew["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];
                           }
                           DtFinal.Rows.Add(Drnew);
                           Drnew = DtFinal.NewRow();
                       }
                             }
                       }
                       #endregion

                       ds.Tables[0].Rows[i]["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                       ds.Tables[0].Rows[i]["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                       ds.Tables[0].Rows[i]["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];

                   }

                   DSRETURN.Tables.Add(DtFinal);
               }
           
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               String Test = EmployeeID;
               throw ex;
           }

           return DSRETURN;
       }

       public static DataSet GetSocSecRptDataWithDepartmentinfo(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode)
       {
           DataSet ds;
           DataSet DSRETURN = new DataSet();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           String EmployeeID = string.Empty;
           try
           {
               object[] parameters = new object[6];

               parameters[0] = objSearch.type_code.Trim();
               parameters[1] = objSearch.ok_to_post.Trim();
               parameters[2] = objSearch.period.Trim();
               parameters[3] = objSearch.year.Trim();
               parameters[4] = objSearch.startdate;//(objSearch.startdate != string.Empty ? Convert.ToDateTime(objSearch.startdate) : Convert.ToDateTime(null));
               parameters[5] = objSearch.enddate;//(objSearch.enddate != string.Empty ? Convert.ToDateTime(objSearch.enddate) : Convert.ToDateTime(null));
               ds = objDALBaseClass.GetData(objSearch.FIND_SOC_SEC_RPT(ref parameters, ref GlobalCode));

               ds.Tables[0].Columns[0].ColumnName = "v_addr1";
               ds.Tables[0].Columns[1].ColumnName = "v_addr2";
               ds.Tables[0].Columns[2].ColumnName = "v_city";
               ds.Tables[0].Columns[3].ColumnName = "v_co_name";
               ds.Tables[0].Columns[4].ColumnName = "v_country";
               ds.Tables[0].Columns[5].ColumnName = "v_state";
               ds.Tables[0].Columns[6].ColumnName = "v_zip";
               ds.Tables[0].Columns[7].ColumnName = "v_end_date";
               ds.Tables[0].Columns[8].ColumnName = "v_start_date";
               ds.Tables[0].Columns[9].ColumnName = "v_ein_number";
               ds.Tables[0].Columns[10].ColumnName = "v_date_hired";
               ds.Tables[0].Columns[11].ColumnName = "v_empl_code";
               ds.Tables[0].Columns[12].ColumnName = "v_first_name";
               ds.Tables[0].Columns[13].ColumnName = "v_last_name";
               ds.Tables[0].Columns[14].ColumnName = "v_middle_name";
               ds.Tables[0].Columns[15].ColumnName = "v_pay_period";
               ds.Tables[0].Columns[16].ColumnName = "v_soc_sec_num";
               ds.Tables[0].Columns[17].ColumnName = "v_terminated";
               ds.Tables[0].Columns[18].ColumnName = "v_doc_no";
               ds.Tables[0].Columns[19].ColumnName = "v_pay_date";
               ds.Tables[0].Columns.Add("v_num_rows");
               ds.Tables[0].Columns.Add("v_month_of");
               ds.Tables[0].Columns.Add("v_totsocsec");
               ds.Tables[0].Columns.Add("v_tot_ded_ssl");
               ds.Tables[0].Columns.Add("v_week1_amt");
               ds.Tables[0].Columns.Add("v_week2_amt");
               ds.Tables[0].Columns.Add("v_week3_amt");
               ds.Tables[0].Columns.Add("v_week4_amt");
               ds.Tables[0].Columns.Add("v_week5_amt");
               ds.Tables[0].Columns.Add("v_total_wages");
               ds.Tables[0].Columns.Add("v_wages_levy");
               ds.Tables[0].Columns.Add("v_socsec_contr");
               ds.Tables[0].Columns.Add("v_wages_sevpay");
               ds.Tables[0].Columns.Add("v_weeks_worked");
               ds.Tables[0].Columns.Add("v_comm_term");
               ds.Tables[0].Columns.Add("v_comm_term_date");
               DataTable DtFinal = new DataTable();
               DtFinal = ds.Tables[0].Clone();
               int v_weekCount = 0;
               string v_Week1 = " ";
               string v_Week2 = " ";
               string v_Week3 = " ";
               string v_Week4 = " ";
               string v_Week5 = " ";

               decimal v_Week1_amt = 0;
               decimal v_Week2_amt = 0;
               decimal v_Week3_amt = 0;
               decimal v_Week4_amt = 0;
               decimal v_Week5_amt = 0;
               int num_emp = 0;
               #region before frst row

               if (objSearch.period.Trim() == string.Empty)
               {
                   ds.Tables[0].Rows[0]["v_month_of"] = objSearch.startdate + " to" + objSearch.enddate;
               }
               else
               {
                   ds.Tables[0].Rows[0]["v_month_of"] = objSearch.period.Trim() + "/" + objSearch.year;
               }
               // get the total amount remitted to the Director of Social Security 
               decimal Local_Amount = 0;

               // DataSet ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_OBL_AMT_SSR(ref parameters, ref GlobalCode));
               // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
               //     dr = ds_ss_amount.Tables[0].Rows[0];
               //Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
               //Decimal TotalSocPaid = Local_Amount;
               //ds_ss_amount = null;
               // ds_ss_amount = objDALBaseClass.GetData(objSearch.FIND_DED_AMT_SSR(ref parameters, ref GlobalCode));

               // foreach (DataRow dr in ds_ss_amount.Tables[0].Rows)
               //     Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
               //dr = ds_ss_amount.Tables[0].Rows[0];

               //  // Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
               //  ds.Tables[0].Rows[0]["v_totsocsec"] = TotalSocPaid + Local_Amount;




               //  //get the total SSL deductions for the report (total amount remitted to the Accountant General)
               //  Local_Amount = 0;
               //  DataSet ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_DED_AMT_SSR(ref parameters, ref GlobalCode));
               //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
               //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
               //  // dr = ds_ag_amount.Tables[0].Rows[0];
               //  //Local_Amount = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

               //  ds_ag_amount = null;
               //  ds_ag_amount = objDALBaseClass.GetData(objSearch.FIND_AG_OBL_AMT_SSR(ref parameters, ref GlobalCode));
               //  foreach (DataRow dr in ds_ag_amount.Tables[0].Rows)
               //      Local_Amount = Local_Amount + (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);
               //  // dr = ds_ag_amount.Tables[0].Rows[0];
               //  //  Local_Amount = Local_Amount+ (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0);

               //  ds.Tables[0].Rows[0]["v_tot_ded_ssl"] = Local_Amount;
               //  Decimal Tot_SSL_deductions = Local_Amount;

               //  Local_Amount = 0;
               #endregion

               int rownum = 0;
               DataRow Drnew = DtFinal.NewRow();

               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   EmployeeID = ds.Tables[0].Rows[i]["v_empl_code"].ToString();
                   DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
                   // Get amount for Levyee,Contrib and total wages
                   object[] parameter = new object[1];
                   parameter[0] = (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["v_doc_no"]) : 0);
                   DataSet ds_amt = null;
                   ds_amt = objDALBaseClass.GetData(objSearch.FIND_AMT_SUM_SSR(ref parameter, ref GlobalCode));
                   if (ds_amt.Tables.Count > 0)
                   {
                       if (ds_amt.Tables[0].Rows.Count != 0)
                       {


                           ds_amt.Tables[0].Columns[0].ColumnName = "amount";
                           ds_amt.Tables[0].Columns[1].ColumnName = "const";
                           DataRow[] dr_amt = ds_amt.Tables[0].Select("", "const");
                           // calculate the "Total Wages" for the employee
                           ds.Tables[0].Rows[i]["v_total_wages"] = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);
                           Drnew["v_total_wages"] = (Drnew["v_total_wages"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_total_wages"]) : 0) + Convert.ToDecimal(dr_amt[3][0]);
                           Local_Amount = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);

                           if (objSearch.type_code.Trim() == "WAGES")
                           {
                               //must round the week up
                               //figure out what calendar monday we are on, then go back one week
                               //the calendar weeks are always one week behind the payroll weeks
                               //Modified on 27/07/2009 
                               Int32 payday = v_pay_date.Day;
                               if (payday >= 1 && payday <= 7)
                               {
                                   v_weekCount = 1;
                               }
                               else if (payday >= 8 && payday <= 14)
                               {
                                   v_weekCount = 2;

                               }
                               else if (payday >= 15 && payday <= 21)
                               {
                                   v_weekCount = 3;
                               }
                               else if (payday >= 22 && payday <= 28)
                               {
                                   v_weekCount = 4;
                               }
                               else if (payday > 28)
                               {
                                   v_weekCount = 5;
                               }
                               //DateTime v_tmpDate = v_pay_date.AddDays(-(Convert.ToInt32(v_pay_date.DayOfWeek) ));
                               //v_weekCount = 1;
                               //while (v_tmpDate.AddDays(-(7 * v_weekCount)).Month == v_pay_date.Month)
                               //{
                               //    v_weekCount = v_weekCount + 1;
                               //}
                               switch (v_weekCount)
                               {
                                   case 1:
                                       v_Week1_amt = v_Week1_amt + Local_Amount;
                                       ds.Tables[0].Rows[i]["v_week1_amt"] = v_Week1_amt;
                                       v_Week1 = "X";
                                       break;
                                   case 2:
                                       v_Week2_amt = v_Week2_amt + Local_Amount;
                                       ds.Tables[0].Rows[i]["v_week2_amt"] = v_Week2_amt;
                                       v_Week2 = "X";
                                       break;
                                   case 3:
                                       v_Week3_amt = v_Week3_amt + Local_Amount;
                                       ds.Tables[0].Rows[i]["v_week3_amt"] = v_Week3_amt;
                                       v_Week3 = "X";
                                       break;
                                   case 4:
                                       v_Week4_amt = v_Week4_amt + Local_Amount;
                                       ds.Tables[0].Rows[i]["v_week4_amt"] = v_Week4_amt;
                                       v_Week4 = "X";
                                       break;
                                   case 5:
                                       v_Week5_amt = v_Week5_amt + Local_Amount;
                                       ds.Tables[0].Rows[i]["v_week5_amt"] = v_Week5_amt;
                                       v_Week5 = "X";
                                       break;
                               }
                           }
                           //calculate the "Levy" for the employee
                           Local_Amount = 0;
                           //v_wages_levy
                           Drnew["v_wages_levy"] = (Drnew["v_wages_levy"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_levy"]) : 0) + (dr_amt[0][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[0][0]) : 0);

                           //calculate the "Contrib" for the employee
                           //v_socsec_contr
                           Local_Amount = (dr_amt[1][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[1][0]) : 0);
                           Local_Amount = Local_Amount + (dr_amt[2][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[2][0]) : 0);
                           Drnew["v_socsec_contr"] = (Drnew["v_socsec_contr"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_socsec_contr"]) : 0) + Local_Amount;

                           //v_wages_sevpay
                           Drnew["v_wages_sevpay"] = (Drnew["v_wages_sevpay"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_sevpay"]) : 0) + (dr_amt[4][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[4][0]) : 0);

                           #region  Process on soc_sec_num
                           bool _Status = false;
                           if (ds.Tables[0].Rows.Count != i + 1)
                           {
                               if (ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString().Trim() != ds.Tables[0].Rows[i + 1]["v_soc_sec_num"].ToString().Trim())
                               {
                                   _Status = true;
                               }
                           }
                           else if (ds.Tables[0].Rows.Count == i + 1)
                               _Status = true;

                           if (_Status)
                           {
                               //DataRowCollection drn;
                               //drn = ds.Tables[0].Select("v_soc_sec_num= " + ds.Tables[0].Rows[i]["v_soc_sec_num"]);
                               if (ds.Tables[0].Rows[i]["v_addr1"] != DBNull.Value)
                               {
                                   Drnew["v_addr1"] = ds.Tables[0].Rows[i]["v_addr1"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_addr2"] != DBNull.Value)
                               {
                                   Drnew["v_addr2"] = ds.Tables[0].Rows[i]["v_addr2"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_city"] != DBNull.Value)
                               {
                                   Drnew["v_city"] = ds.Tables[0].Rows[i]["v_city"].ToString();

                               }
                               if (ds.Tables[0].Rows[i]["v_co_name"] != DBNull.Value)
                               {
                                   Drnew["v_co_name"] = ds.Tables[0].Rows[i]["v_co_name"].ToString();

                               }
                               if (ds.Tables[0].Rows[i]["v_country"] != DBNull.Value)
                               {
                                   Drnew["v_country"] = ds.Tables[0].Rows[i]["v_country"].ToString();

                               }
                               if (ds.Tables[0].Rows[i]["v_zip"] != DBNull.Value)
                               {
                                   Drnew["v_zip"] = ds.Tables[0].Rows[i]["v_zip"].ToString();

                               }
                               if (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value)
                               {
                                   Drnew["v_end_date"] = ds.Tables[0].Rows[i]["v_end_date"].ToString();

                               }
                               if (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value)
                               {
                                   Drnew["v_start_date"] = ds.Tables[0].Rows[i]["v_start_date"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_ein_number"] != DBNull.Value)
                               {
                                   Drnew["v_ein_number"] = ds.Tables[0].Rows[i]["v_ein_number"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_first_name"] != DBNull.Value)
                               {
                                   Drnew["v_first_name"] = ds.Tables[0].Rows[i]["v_first_name"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_last_name"] != DBNull.Value)
                               {
                                   Drnew["v_last_name"] = ds.Tables[0].Rows[i]["v_last_name"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_middle_name"] != DBNull.Value)
                               {
                                   Drnew["v_middle_name"] = ds.Tables[0].Rows[i]["v_middle_name"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_pay_period"] != DBNull.Value)
                               {
                                   Drnew["v_pay_period"] = ds.Tables[0].Rows[i]["v_pay_period"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_soc_sec_num"] != DBNull.Value)
                               {
                                   Drnew["v_soc_sec_num"] = ds.Tables[0].Rows[i]["v_soc_sec_num"].ToString();
                               }
                               // Drnew["v_terminated"] = ds.Tables[0].Rows[i]["v_terminated"].ToString();
                               if (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value)
                               {
                                   Drnew["v_doc_no"] = ds.Tables[0].Rows[i]["v_doc_no"].ToString();
                               }
                               if (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value)
                               {
                                   Drnew["v_pay_date"] = ds.Tables[0].Rows[i]["v_pay_date"].ToString();
                               }

                               Drnew["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                               Drnew["v_week1_amt"] = v_Week1_amt;
                               Drnew["v_week2_amt"] = v_Week2_amt;
                               Drnew["v_week3_amt"] = v_Week3_amt;
                               Drnew["v_week4_amt"] = v_Week4_amt;
                               Drnew["v_week5_amt"] = v_Week5_amt;



                               //ds.Tables[0].Rows[i]["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                               //determine if this is a new employee ("C" for Commencement Date) or if
                               //the employee has been terminated ("T")
                               DateTime date_hired = (ds.Tables[0].Rows[i]["v_date_hired"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_date_hired"]) : Convert.ToDateTime(null));
                               DateTime terminated = (ds.Tables[0].Rows[i]["v_terminated"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_terminated"]) : Convert.ToDateTime(null));
                               DateTime start_date = (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_start_date"]) : Convert.ToDateTime(null));
                               DateTime end_date = (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_end_date"]) : Convert.ToDateTime(null));

                               if ((date_hired >= start_date) && (date_hired <= end_date))
                               {
                                   Drnew["v_comm_term"] = "C";
                                   Drnew["v_comm_term_date"] = date_hired;
                               }
                               else
                               {
                                   Drnew["v_comm_term_date"] = date_hired;
                               }
                               if (terminated >= start_date && terminated <= end_date)
                               {
                                   Drnew["v_comm_term"] = "T";
                                   Drnew["v_comm_term_date"] = terminated;
                               }
                               if (objSearch.type_code.Trim() == "WAGES")
                               {
                                   v_Week1_amt = 0;
                                   v_Week2_amt = 0;
                                   v_Week3_amt = 0;
                                   v_Week4_amt = 0;
                                   v_Week5_amt = 0;
                               }
                               v_Week1 = " ";
                               v_Week2 = " ";
                               v_Week3 = " ";
                               v_Week4 = " ";
                               v_Week5 = " ";
                               num_emp++;
                               rownum++;
                               //Drnew["v_num_rows"] = ds.Tables[0].Rows.Count;
                               //Drnew["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                               //Drnew["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                               if (ds.Tables[0].Rows[0]["v_month_of"] != DBNull.Value)
                               {
                                   Drnew["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];
                               }
                               DtFinal.Rows.Add(Drnew);
                               Drnew = DtFinal.NewRow();
                           }
                       }
                   }
                           #endregion

                   ds.Tables[0].Rows[i]["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                   ds.Tables[0].Rows[i]["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                   ds.Tables[0].Rows[i]["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];

               }

               DSRETURN.Tables.Add(DtFinal);
           }

           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               String Test = EmployeeID;
               throw ex;
           }

           return DSRETURN;
       }
       public static List<DVODistinctMonth> GetDistinctMonth_stxperdr()
       {
           List<DVODistinctMonth> objDistMList = new List<DVODistinctMonth>();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           using (DataSet ds = objDalBaseClass.GetData(typeof(DVODistinctMonth), (new DVODistinctMonth()).GET_DISTICT_MONTH_XPERDR))
           {
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   DVODistinctMonth objDistM = new DVODistinctMonth();
                   if (dr[0].ToString() != "")
                   {
                       objDistM.month = dr[0].ToString();
                       objDistMList.Add(objDistM);
                   }


               }
               return objDistMList;
           }
       }
       public static List<DVODistinctYear> GetDistinctYear_stxperdr()
       {
           List<DVODistinctYear> objDistYList = new List<DVODistinctYear>();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           using (DataSet ds = objDalBaseClass.GetData(typeof(DVODistinctYear), (new DVODistinctYear()).GET_DISTICT_YEAR_XPERDR))
           {
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   DVODistinctYear objDistY = new DVODistinctYear();
                   if (dr[0].ToString() != "")
                   {
                       objDistY.year = dr[0].ToString();
                       objDistYList.Add(objDistY);
                   }
               }
               return objDistYList;
           }
       }

       public static List<string> PrepareDataForSocSecInTxtFile(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode, out string fileName)
       {
           List<string> FinallistStr = new List<string>();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           fileName = string.Empty;
           try
           {
               object[] parameters = new object[6];

               parameters[0] = objSearch.type_code;
               parameters[1] = string.Empty;
               parameters[2] = objSearch.period;
               parameters[3] = objSearch.year;
               parameters[4] = string.Empty;
               parameters[5] = string.Empty;

               DataSet ds = objDALBaseClass.GetData(objSearch.FIND_SOC_SEC_RPT(ref parameters, ref GlobalCode));

               if (ds.Tables[0].Rows.Count > 0)
               {
                   #region Set Columns Names.
                   ds.Tables[0].Columns[0].ColumnName = "v_addr1";
                   ds.Tables[0].Columns[1].ColumnName = "v_addr2";
                   ds.Tables[0].Columns[2].ColumnName = "v_city";
                   ds.Tables[0].Columns[3].ColumnName = "v_co_name";
                   ds.Tables[0].Columns[4].ColumnName = "v_country";
                   ds.Tables[0].Columns[5].ColumnName = "v_state";
                   ds.Tables[0].Columns[6].ColumnName = "v_zip";
                   ds.Tables[0].Columns[7].ColumnName = "v_end_date";
                   ds.Tables[0].Columns[8].ColumnName = "v_start_date";
                   ds.Tables[0].Columns[9].ColumnName = "v_ein_number";
                   ds.Tables[0].Columns[10].ColumnName = "v_date_hired";
                   ds.Tables[0].Columns[11].ColumnName = "v_empl_code";
                   ds.Tables[0].Columns[12].ColumnName = "v_first_name";
                   ds.Tables[0].Columns[13].ColumnName = "v_last_name";
                   ds.Tables[0].Columns[14].ColumnName = "v_middle_name";
                   ds.Tables[0].Columns[15].ColumnName = "v_pay_period";
                   ds.Tables[0].Columns[16].ColumnName = "v_soc_sec_num";
                   ds.Tables[0].Columns[17].ColumnName = "v_terminated";
                   ds.Tables[0].Columns[18].ColumnName = "v_doc_no";
                   ds.Tables[0].Columns[19].ColumnName = "v_pay_date";
                   #endregion

                   #region Initialize Global Variables..
                   string hDrLine = string.Empty;
                   string fTrLine = string.Empty;
                   string period = string.Empty;
                   string comName = string.Empty;

                   string LINE = string.Empty;
                   string IntLine = string.Empty;
                   int numLineNo = 1;

                   int totRecords = ds.Tables[0].Rows.Count;
                   int totRcordsLength = totRecords.ToString().Trim().Length;
                   if (totRcordsLength > 3)
                   {
                       for (int j = 0; j < totRcordsLength; j++)
                           IntLine = IntLine + "0";
                   }
                   else
                   {
                       IntLine = "000";
                   }

                   decimal gloAmtCTRLTTL = 0;
                   decimal gloAmtTTLSS = 0;
                   decimal gloAmtTTTLV = 0;
                   decimal gloAmtTTTPE = 0;

                   string v_Week1 = "0";
                   string v_Week2 = "0";
                   string v_Week3 = "0";
                   string v_Week4 = "0";
                   string v_Week5 = "0";
                   decimal v_Week1_amt = 0;
                   decimal v_Week2_amt = 0;
                   decimal v_Week3_amt = 0;
                   decimal v_Week4_amt = 0;
                   decimal v_Week5_amt = 0;
                   #endregion

                   #region Prepare Header Line..

                   //Get Registeration No..
                   string regNo = ds.Tables[0].Rows[0]["v_ein_number"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["v_ein_number"]).Trim() : string.Empty;
                   if (regNo.Trim().Length <= 0)
                   {
                       throw new Exception("Error : Registeration No. Not Found");
                   }
                   //File Name..
                   fileName = regNo.Trim() + objSearch.period.Trim() + objSearch.year.Trim() + ".C3";

                   period = "01/" + objSearch.period.Trim() + "/" + objSearch.year.Trim();

                   comName = ds.Tables[0].Rows[0]["v_co_name"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["v_co_name"]).Trim() : string.Empty;
                   if (comName.Trim().Length <= 0)
                   {
                       throw new Exception("Error : Company Name Not Found");
                   }
                   //Prepare Header Line..
                   hDrLine = "HDR, " + regNo.Trim() + ", " + period + ", " + "1.0.0, " + comName.Trim();
                   FinallistStr.Add(hDrLine);

                   #endregion

                   #region Prepare Detail Lines..

                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                   {
                       #region Initialize Local Variables..
                       string detailLine = string.Empty;
                       string SSN = string.Empty;
                       string LASTNAME = string.Empty;
                       string FIRSTNAME = string.Empty;
                       string MIDDLENAME = string.Empty;
                       string CMNCEDATE = string.Empty;
                       string TERMDATE = string.Empty;
                       string PAYFREQ = string.Empty;
                       string HOLPAY = "0";
                       string BONUS = "0";
                       string payperiod = string.Empty;
                       int v_weekCount = 0;


                       decimal locAmount = 0;
                       decimal locAmtLEVY = 0;
                       decimal locAmtSOCSEC = 0;

                       decimal locAmtHOLPAY = 0;
                       decimal locAmtBONUS = 0;

                       #endregion

                       DataRow dr = ds.Tables[0].Rows[i];

                       #region LINE..
                       LINE = IntLine;
                       LINE = LINE.Remove(totRcordsLength - numLineNo.ToString().Length, numLineNo.ToString().Length);
                       LINE = LINE + numLineNo.ToString();

                       #endregion

                       #region SSN...
                       SSN = dr["v_soc_sec_num"] != DBNull.Value ? Convert.ToString(dr["v_soc_sec_num"]).Trim() : string.Empty;
                       if (SSN.Trim().Length <= 0)
                           throw new Exception("Error : Soc_Sec_Num Not Found");
                       #endregion

                       #region EMP NAME...
                       LASTNAME = dr["v_last_name"] != DBNull.Value ? Convert.ToString(dr["v_last_name"]).Trim() : string.Empty;
                       FIRSTNAME = dr["v_first_name"] != DBNull.Value ? Convert.ToString(dr["v_first_name"]).Trim() : string.Empty;
                       MIDDLENAME = dr["v_middle_name"] != DBNull.Value ? Convert.ToString(dr["v_middle_name"]).Trim() : string.Empty;
                       #endregion

                       #region CMNCEDATE AND TERMDATE...
                       if (dr["v_date_hired"] != DBNull.Value)
                           if (Convert.ToString(dr["v_date_hired"]).Trim().Length > 0)
                               CMNCEDATE = Convert.ToDateTime(dr["v_date_hired"]).ToString("dd/MM/yyyy");

                       if (dr["v_terminated"] != DBNull.Value)
                           if (Convert.ToString(dr["v_terminated"]).Trim().Length > 0)
                               TERMDATE = Convert.ToDateTime(dr["v_terminated"]).ToString("dd/MM/yyyy");
                       #endregion

                       #region PAYFREQ..
                       payperiod = dr["v_pay_period"] != DBNull.Value ? Convert.ToString(dr["v_pay_period"]).Trim() : string.Empty;
                       switch (payperiod)
                       {
                           case "M":
                               PAYFREQ = "3";
                               break;
                           case "W":
                               PAYFREQ = "1";
                               break;
                       }
                       #endregion

                       #region WK1 ....WK5 AND PAY1.....PAY5
                       int docNo = dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0;

                       List<decimal> listAmounts = GetAmounts(docNo, ref GlobalCode);

                       //TotalWages
                       locAmount = listAmounts[3];
                       DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
                       if (objSearch.type_code.Trim() == "WAGES")
                       {

                           Int32 payday = v_pay_date.Day;
                           if (payday >= 1 && payday <= 7)
                           {
                               v_weekCount = 1;
                           }
                           else if (payday >= 8 && payday <= 14)
                           {
                               v_weekCount = 2;

                           }
                           else if (payday >= 15 && payday <= 21)
                           {
                               v_weekCount = 3;
                           }
                           else if (payday >= 22 && payday <= 28)
                           {
                               v_weekCount = 4;
                           }
                           else if (payday > 28)
                           {
                               v_weekCount = 5;
                           }
                           switch (v_weekCount)
                           {
                               case 1:
                                   v_Week1_amt = locAmount;
                                   v_Week1 = "1";
                                   break;
                               case 2:
                                   v_Week2_amt = locAmount;
                                   v_Week2 = "1";
                                   break;
                               case 3:
                                   v_Week3_amt = locAmount;
                                   v_Week3 = "1";
                                   break;
                               case 4:
                                   v_Week4_amt = locAmount;
                                   v_Week4 = "1";
                                   break;
                               case 5:
                                   v_Week5_amt = locAmount;
                                   v_Week5 = "1";
                                   break;
                           }
                       }
                       #endregion

                       #region HOLPAID and BPAID..
                       //BONUS
                       if (listAmounts[4] != 0)
                       {
                           locAmtBONUS = listAmounts[4];
                           BONUS = "1";
                       }
                       //HOLPAY
                       if (listAmounts[5] != 0)
                       {
                           locAmtHOLPAY = listAmounts[5];
                           HOLPAY = "1";
                       }
                       #endregion

                       #region LEVY ,SOCSEC ,CTRLTTL ,TTLSS,TTTLV,TTTPE..
                       //LEVY
                       locAmtLEVY = listAmounts[0];
                       gloAmtTTTLV = gloAmtTTTLV + locAmtLEVY;
                       //SOCSEC
                       locAmtSOCSEC = listAmounts[1];
                       gloAmtTTLSS = gloAmtTTLSS + locAmtSOCSEC;
                       //CTRLTTL
                       gloAmtCTRLTTL = gloAmtCTRLTTL + listAmounts[3];
                       //TTTPE
                       gloAmtTTTPE = gloAmtTTTPE + listAmounts[2];
                       #endregion

                       bool _status = false;
                       if (ds.Tables[0].Rows.Count != i + 1)
                       {
                           if (Convert.ToString(dr["v_soc_sec_num"]).Trim() != Convert.ToString(ds.Tables[0].Rows[i + 1]["v_soc_sec_num"]).Trim())
                           {
                               _status = true;
                           }
                       }
                       else if (ds.Tables[0].Rows.Count == i + 1)
                           _status = true;

                       if (_status)
                       {


                           #region Prepare Detail Line ..
                           detailLine = LINE + ", " + SSN + ", " + LASTNAME + ", " + FIRSTNAME + ", " + MIDDLENAME + ", " + CMNCEDATE + ", " + TERMDATE + ", " + PAYFREQ + ", ";
                           detailLine = detailLine + v_Week1 + ", " + v_Week2 + ", " + v_Week3 + ", " + v_Week4 + ", " + v_Week5 + ", ";
                           detailLine = detailLine + HOLPAY + ", " + BONUS + ", " + v_Week1_amt + ", " + v_Week2_amt + ", " + v_Week3_amt + ", ";
                           detailLine = detailLine + v_Week4_amt + ", " + v_Week5_amt + ", " + locAmtHOLPAY + ", " + locAmtBONUS + ", " + locAmtLEVY + ", " + locAmtSOCSEC;
                           FinallistStr.Add(detailLine);
                           #endregion

                           if (objSearch.type_code.Trim() == "WAGES")
                           {
                               v_Week1_amt = 0;
                               v_Week2_amt = 0;
                               v_Week3_amt = 0;
                               v_Week4_amt = 0;
                               v_Week5_amt = 0;
                           }
                           v_Week1 = "0";
                           v_Week2 = "0";
                           v_Week3 = "0";
                           v_Week4 = "0";
                           v_Week5 = "0";
                           if (ds.Tables[0].Rows.Count != i + 1)
                               numLineNo = numLineNo + 1;
                       }

                   }
                   #endregion

                   #region Prepare Footer Line..
                   fTrLine = "FTR, " + regNo + ", " + period + ", " + gloAmtCTRLTTL + ", " + gloAmtTTLSS + ", " + gloAmtTTTLV + ", " + gloAmtTTTPE + ", " + numLineNo;
                   FinallistStr.Add(fTrLine);
                   #endregion

               }
           }
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;

           }
           return FinallistStr;
       }

       //Written by Sarvjeet On 26/11/2009 ..
       public static List<decimal> GetAmounts(int docNo, ref string[] GlobalCode)
       {
           // This function will return list of amounts for (LEVY ,SOCSEC,TTTPE,TotalWages,BONUS,HOLPAY).
           // These amounts will be set on index basis into List object..
           //At Index :
           //0- LEVY
           //1- SOCSEC1+ SOCSEC2
           //2- TTTPE
           //3- TotalWages
           //4- BONUS
           //5- HOLPAY
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           List<decimal> amountList = new List<decimal>();
           try
           {

               object[] parameter = new object[1];
               parameter[0] = docNo;
               DataSet ds = objDALBaseClass.GetData((new DVOPaymentToEmployee()).FIND_AMT_FOR_SSTF(ref parameter, ref GlobalCode));
               ds.Tables[0].Columns[0].ColumnName = "amount";
               ds.Tables[0].Columns[1].ColumnName = "const";
               DataRow[] dra = ds.Tables[0].Select("", "const");
               //0-LEVY
               amountList.Add(dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0);
               //1-SOCSEC=SOCSEC1+SOCSEC2
               amountList.Add((dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0) + (dra[2][0] != DBNull.Value ? Convert.ToDecimal(dra[2][0]) : 0));
               //2-TTTPE
               amountList.Add(dra[3][0] != DBNull.Value ? Convert.ToDecimal(dra[3][0]) : 0);
               //3-TotalWages
               amountList.Add(dra[4][0] != DBNull.Value ? Convert.ToDecimal(dra[4][0]) : 0);
               //4-BONUS
               amountList.Add(dra[5][0] != DBNull.Value ? Convert.ToDecimal(dra[5][0]) : 0);
               //5-HOLPAY
               amountList.Add(dra[6][0] != DBNull.Value ? Convert.ToDecimal(dra[6][0]) : 0);
           }
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;
           }
           return amountList;
       }

       public static DataSet GetMonthlyDeductionRptData(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode)
       {
           DataSet ds;
           DataSet DSRETURN = new DataSet();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           try
           {
               object[] parameters = new object[6];

               parameters[0] = objSearch.type_code.Trim();
               parameters[1] = objSearch.ok_to_post.Trim();
               parameters[2] = objSearch.period.Trim();
               parameters[3] = objSearch.year.Trim();
               parameters[4] = objSearch.startdate;//(objSearch.startdate != string.Empty ? Convert.ToDateTime(objSearch.startdate) : Convert.ToDateTime(null));
               parameters[5] = objSearch.enddate;//(objSearch.enddate != string.Empty ? Convert.ToDateTime(objSearch.enddate) : Convert.ToDateTime(null));
               ds = objDALBaseClass.GetData(objSearch.FIND_Monthly_Deductions_RPT(ref parameters, ref GlobalCode));
               if (ds.Tables[0].Rows.Count != 0)
               {
                   ds.Tables[0].Columns[0].ColumnName = "v_emplcode";
                   ds.Tables[0].Columns[1].ColumnName = "v_firstname";
                   ds.Tables[0].Columns[2].ColumnName = "v_lastname";
                   ds.Tables[0].Columns[3].ColumnName = "v_address1";
                   ds.Tables[0].Columns[4].ColumnName = "v_address2";
                   ds.Tables[0].Columns[5].ColumnName = "v_appointdate";
                   ds.Tables[0].Columns[6].ColumnName = "v_pay_date";
                   ds.Tables[0].Columns[7].ColumnName = "v_dedcode";
                   ds.Tables[0].Columns[8].ColumnName = "v_dedrate";
                   ds.Tables[0].Columns[9].ColumnName = "v_amount";
                   ds.Tables[0].Columns.Add("v_num_rows");
                   ds.Tables[0].Columns.Add("v_month_of");
                   ds.Tables[0].Columns.Add("v_week1_amt");
                   ds.Tables[0].Columns.Add("v_week2_amt");
                   ds.Tables[0].Columns.Add("v_week3_amt");
                   ds.Tables[0].Columns.Add("v_week4_amt");
                   ds.Tables[0].Columns.Add("v_week5_amt");
                   ds.Tables[0].Columns.Add("v_weeks_worked");
                 
                   DataTable DtFinal = new DataTable();
                   DtFinal = ds.Tables[0].Clone();
                   int v_weekCount = 0;
                   string v_Week1 = " ";
                   string v_Week2 = " ";
                   string v_Week3 = " ";
                   string v_Week4 = " ";
                   string v_Week5 = " ";

                   decimal v_Week1_amt = 0;
                   decimal v_Week2_amt = 0;
                   decimal v_Week3_amt = 0;
                   decimal v_Week4_amt = 0;
                   decimal v_Week5_amt = 0;
                   int num_emp = 0;
                   #region before frst row

                              
                       ds.Tables[0].Rows[0]["v_month_of"] = objSearch.period.Trim() + "/" + objSearch.year;
                   
                   // get the total amount remitted to the Director of Social Security 
                   decimal Local_Amount = 0;

                   #endregion

                   int rownum = 0;
                   DataRow Drnew = DtFinal.NewRow();

                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                   {


                       DateTime v_pay_date = (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_pay_date"]) : Convert.ToDateTime(null));
                       // Get amount for Levyee,Contrib and total wages
                       object[] parameter = new object[1];
                    //   parameter[0] = (ds.Tables[0].Rows[i]["v_doc_no"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["v_doc_no"]) : 0);
                    //   DataSet ds_amt = objDALBaseClass.GetData(objSearch.FIND_AMT_SUM_SSR(ref parameter, ref GlobalCode));
                 //     ds_amt.Tables[0].Columns[0].ColumnName = "amount";
                 //      ds_amt.Tables[0].Columns[1].ColumnName = "const";
                 //      DataRow[] dr_amt = ds_amt.Tables[0].Select("", "const");
                       // calculate the "Total Wages" for the employee
                //       ds.Tables[0].Rows[i]["v_total_wages"] = (dr_amt[3][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[3][0]) : 0);
                  //     Drnew["v_total_wages"] = (Drnew["v_total_wages"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_total_wages"]) : 0) + Convert.ToDecimal(dr_amt[3][0]);
                       Local_Amount = (ds.Tables[0].Rows[i]["v_amount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i]["v_amount"]) : 0);

                       //if (objSearch.type_code.Trim() == "WAGES")
                       //{
                           //must round the week up
                           //figure out what calendar monday we are on, then go back one week
                           //the calendar weeks are always one week behind the payroll weeks
                           //Modified on 27/07/2009 
                           Int32 payday = v_pay_date.Day;
                           if (payday >= 1 && payday <= 7)
                           {
                               v_weekCount = 1;
                           }
                           else if (payday >= 8 && payday <= 14)
                           {
                               v_weekCount = 2;

                           }
                           else if (payday >= 15 && payday <= 21)
                           {
                               v_weekCount = 3;
                           }
                           else if (payday >= 22 && payday <= 28)
                           {
                               v_weekCount = 4;
                           }
                           else if (payday > 28)
                           {
                               v_weekCount = 5;
                           }
                         
                           switch (v_weekCount)
                           {
                               case 1:
                                   v_Week1_amt = v_Week1_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week1_amt"] = v_Week1_amt;
                                   v_Week1 = "X";
                                   break;
                               case 2:
                                   v_Week2_amt = v_Week2_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week2_amt"] = v_Week2_amt;
                                   v_Week2 = "X";
                                   break;
                               case 3:
                                   v_Week3_amt = v_Week3_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week3_amt"] = v_Week3_amt;
                                   v_Week3 = "X";
                                   break;
                               case 4:
                                   v_Week4_amt = v_Week4_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week4_amt"] = v_Week4_amt;
                                   v_Week4 = "X";
                                   break;
                               case 5:
                                   v_Week5_amt = v_Week5_amt + Local_Amount;
                                   ds.Tables[0].Rows[i]["v_week5_amt"] = v_Week5_amt;
                                   v_Week5 = "X";
                                   break;
                           }
                    //   }
                       //calculate the "Levy" for the employee
                       Local_Amount = 0;
                       //v_wages_levy
                     //  Drnew["v_wages_levy"] = (Drnew["v_wages_levy"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_levy"]) : 0) + (dr_amt[0][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[0][0]) : 0);

                       //calculate the "Contrib" for the employee
                       //v_socsec_contr
                   //    Local_Amount = (dr_amt[1][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[1][0]) : 0);
                    //   Local_Amount = Local_Amount + (dr_amt[2][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[2][0]) : 0);
                    //   Drnew["v_socsec_contr"] = (Drnew["v_socsec_contr"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_socsec_contr"]) : 0) + Local_Amount;

                       //v_wages_sevpay
                    //   Drnew["v_wages_sevpay"] = (Drnew["v_wages_sevpay"] != DBNull.Value ? Convert.ToDecimal(Drnew["v_wages_sevpay"]) : 0) + (dr_amt[4][0] != DBNull.Value ? Convert.ToDecimal(dr_amt[4][0]) : 0);

                       #region  Process on soc_sec_num
                       bool _Status = false;
                       if (ds.Tables[0].Rows.Count != i + 1)
                       {
                           if (ds.Tables[0].Rows[i]["v_emplcode"].ToString().Trim() != ds.Tables[0].Rows[i + 1]["v_emplcode"].ToString().Trim())
                           {
                               _Status = true;
                           }
                       }
                       else if (ds.Tables[0].Rows.Count == i + 1)
                           _Status = true;

                       if (_Status)
                       {
                           //DataRowCollection drn;
                           //drn = ds.Tables[0].Select("v_soc_sec_num= " + ds.Tables[0].Rows[i]["v_soc_sec_num"]);


                           if (ds.Tables[0].Rows[i]["v_firstname"] != DBNull.Value)
                           {
                               Drnew["v_firstname"] = ds.Tables[0].Rows[i]["v_firstname"].ToString();
                           }
                           if (ds.Tables[0].Rows[i]["v_lastname"] != DBNull.Value)
                           {
                               Drnew["v_lastname"] = ds.Tables[0].Rows[i]["v_lastname"].ToString();
                           }


                           if (ds.Tables[0].Rows[i]["v_emplcode"] != DBNull.Value)
                           {
                               Drnew["v_emplcode"] = ds.Tables[0].Rows[i]["v_emplcode"].ToString();
                           }
                           // Drnew["v_terminated"] = ds.Tables[0].Rows[i]["v_terminated"].ToString();
                           Drnew["v_dedcode"] = ds.Tables[0].Rows[i]["v_dedcode"].ToString();
                           if (ds.Tables[0].Rows[i]["v_pay_date"] != DBNull.Value)
                           {
                               Drnew["v_pay_date"] = ds.Tables[0].Rows[i]["v_pay_date"].ToString();
                           }

                           Drnew["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                           Drnew["v_week1_amt"] = v_Week1_amt;
                           Drnew["v_week2_amt"] = v_Week2_amt;
                           Drnew["v_week3_amt"] = v_Week3_amt;
                           Drnew["v_week4_amt"] = v_Week4_amt;
                           Drnew["v_week5_amt"] = v_Week5_amt;



                           //ds.Tables[0].Rows[i]["v_weeks_worked"] = v_Week1 + "|" + v_Week2 + "|" + v_Week3 + "|" + v_Week4 + "|" + v_Week5;
                           //determine if this is a new employee ("C" for Commencement Date) or if
                           //the employee has been terminated ("T")
                           DateTime date_hired = (ds.Tables[0].Rows[i]["v_appointdate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_appointdate"]) : Convert.ToDateTime(null));
                           //DateTime terminated = (ds.Tables[0].Rows[i]["v_terminated"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_terminated"]) : Convert.ToDateTime(null));
                           //DateTime start_date = (ds.Tables[0].Rows[i]["v_start_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_start_date"]) : Convert.ToDateTime(null));
                           //DateTime end_date = (ds.Tables[0].Rows[i]["v_end_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i]["v_end_date"]) : Convert.ToDateTime(null));

                           //if ((date_hired >= start_date) && (date_hired <= end_date))
                           //{
                              // Drnew["v_comm_term"] = "C";
                           Drnew["v_appointdate"] = date_hired;
                           //}
                           //else
                           //{
                           //    Drnew["v_comm_term_date"] = date_hired;
                           //}
                           //if (terminated >= start_date && terminated <= end_date)
                           //{
                           //    Drnew["v_comm_term"] = "T";
                           //    Drnew["v_comm_term_date"] = terminated;
                           //}
                           //if (objSearch.type_code.Trim() == "WAGES")
                           //{
                               v_Week1_amt = 0;
                               v_Week2_amt = 0;
                               v_Week3_amt = 0;
                               v_Week4_amt = 0;
                               v_Week5_amt = 0;
                           //}
                           v_Week1 = " ";
                           v_Week2 = " ";
                           v_Week3 = " ";
                           v_Week4 = " ";
                           v_Week5 = " ";
                           num_emp++;
                           rownum++;
                           //Drnew["v_num_rows"] = ds.Tables[0].Rows.Count;
                           //Drnew["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                           //Drnew["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                           if (ds.Tables[0].Rows[0]["v_month_of"] != DBNull.Value)
                           {
                               Drnew["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];
                           }
                           DtFinal.Rows.Add(Drnew);
                           Drnew = DtFinal.NewRow();
                       }
                       #endregion

                       //ds.Tables[0].Rows[i]["v_totsocsec"] = ds.Tables[0].Rows[0]["v_totsocsec"];
                       //ds.Tables[0].Rows[i]["v_tot_ded_ssl"] = ds.Tables[0].Rows[0]["v_tot_ded_ssl"];
                       ds.Tables[0].Rows[i]["v_month_of"] = ds.Tables[0].Rows[0]["v_month_of"];

                   }

                   DSRETURN.Tables.Add(DtFinal);
               }
           }
           catch (Exception ex)
           {
               ExceptionManagement.ExceptionManager.Publish(ex);
               throw ex;
           }

           return ds;// DSRETURN;
       }

       public static DataSet GetDeductionRptData(ref DVOPaymentToEmployee objSearch, ref string[] GlobalCode)
       {
         DataSet ds;
         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
         try
         {
           object[] parameters = new object[4];

           parameters[0] = objSearch.type_code.Trim();
           parameters[1] = objSearch.empl_code.Trim();
           parameters[2] = objSearch.startdate;//(objSearch.startdate != string.Empty ? Convert.ToDateTime(objSearch.startdate) : Convert.ToDateTime(null));
           parameters[3] = objSearch.enddate;//(objSearch.enddate != string.Empty ? Convert.ToDateTime(objSearch.enddate) : Convert.ToDateTime(null));
           ds = objDALBaseClass.GetData(objSearch.FIND_Deductions_RPT(ref parameters, ref GlobalCode));
           if (ds.Tables[0].Rows.Count != 0)
           {
             ds.Tables[0].Columns[0].ColumnName = "v_emplcode";
             ds.Tables[0].Columns[1].ColumnName = "v_firstname";
             ds.Tables[0].Columns[2].ColumnName = "v_lastname";
             ds.Tables[0].Columns[3].ColumnName = "v_address1";
             ds.Tables[0].Columns[4].ColumnName = "v_address2";
             ds.Tables[0].Columns[5].ColumnName = "v_appointdate";
             ds.Tables[0].Columns[6].ColumnName = "v_pay_date";
             ds.Tables[0].Columns[7].ColumnName = "v_dedcode";
             ds.Tables[0].Columns[8].ColumnName = "v_dedrate";
             ds.Tables[0].Columns[9].ColumnName = "v_amount";
           }
         }
         catch (Exception ex)
         {
           ExceptionManagement.ExceptionManager.Publish(ex);
           throw ex;
         }

         return ds;
       }
         
    }
}
