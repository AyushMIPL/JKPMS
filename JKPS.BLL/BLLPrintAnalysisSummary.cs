using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using System.Collections;

namespace JKPS.BLL
{
    /// <summary>
    /// Function Added By Rahul
    /// Date : 29:07:2009
    /// Purpose: To Get the data for report: Print Analysis Summary  
    /// </summary>
    public class BLLPrintAnalysisSummary
    {
        #region START_END_DATE from stxmtxpr table
        public static DataSet GET_STARTEND_DATE(ref DVOMultiTaxstxmtxpr obj_stxmtxpr)
        {
            object[] parameters = new object[2];
            parameters[0] = obj_stxmtxpr.period;
            parameters[1] = obj_stxmtxpr.period_year;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMultiTaxstxmtxpr), obj_stxmtxpr.GET_STARTEND_DATE);
            return ds;
        }
        #endregion
        public static string GetMaxPeriod()
        {
            List<DVOMultiTaxstxmtxpr> objDistMList = new List<DVOMultiTaxstxmtxpr>();
            DVOMultiTaxstxmtxpr objDVOMultiTaxstxmtxpr = new DVOMultiTaxstxmtxpr();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(typeof(DVOMultiTaxstxmtxpr), objDVOMultiTaxstxmtxpr.GetMaxPeriod);
            string str = ds.Tables[0].Rows[0][0].ToString();
            return str;
        }
        public static string GetMaxYear()
        {
            List<DVOMultiTaxstxmtxpr> objDistMList = new List<DVOMultiTaxstxmtxpr>();
            DVOMultiTaxstxmtxpr objDVOMultiTaxstxmtxpr = new DVOMultiTaxstxmtxpr();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(typeof(DVOMultiTaxstxmtxpr), objDVOMultiTaxstxmtxpr.GetMaxYear);
            string str = ds.Tables[0].Rows[0][0].ToString();
            return str;
        }

        //Added By Rahul Jain On 07/29/2009 for getting Print Multilevel Tax Analysis Summary Report
        public static DataSet GetMultilevelTaxAnalysisSummary(ref DVOMultiTaxstxmtaxd objDVOMultiTaxstxmtaxd,string Period,string Year)
        {
            Object[] parameters = new object[3];
            parameters[0] = objDVOMultiTaxstxmtaxd.orig_journal;
            parameters[1] = objDVOMultiTaxstxmtaxd.FromDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[2] = objDVOMultiTaxstxmtaxd.ToDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(objDVOMultiTaxstxmtaxd.FINDQUERY_ANALYSIS_SUMMARY(ref parameters));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Added new column as per the requirement

                    #region Add New Column
                    ds.Tables[0].Columns.Add("period");
                    ds.Tables[0].Columns.Add("period_year");
                    ds.Tables[0].Columns.Add("start_date");
                    ds.Tables[0].Columns.Add("end_date");
                    ds.Tables[0].Columns.Add("ledger");
                    ds.Tables[0].Columns.Add("acct_desc");
                    ds.Tables[0].Columns.Add("db_mtax_amt", typeof(decimal));
                    ds.Tables[0].Columns.Add("cr_mtax_amt", typeof(decimal));
                    ds.Tables[0].Columns.Add("tot_db_mtax_amt", typeof(decimal));
                    ds.Tables[0].Columns.Add("tot_cr_mtax_amt", typeof(decimal));
                    #endregion

                    int Current_acct_no = 0;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        Current_acct_no = (dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0);
                        #region Before Group acct_no.....

                        bool _status = false;
                        if (i != 0)
                        {
                            if (ds.Tables[0].Rows[i]["acct_no"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["acct_no"].ToString().Trim())
                                _status = true;
                        }
                        else
                            _status = true;

                        if (_status)
                        {
                            string db_cr = string.Empty;
                            string acct_desc = string.Empty;
                            string[] acct_details = BLLAccountingLiberary.AcctChrTn(Current_acct_no);
                            acct_desc = Convert.ToString(acct_details[0]);
                            dr["acct_desc"] = acct_desc.Trim();
                            if (acct_details[1] != string.Empty)
                            {
                                db_cr = Convert.ToString(acct_details[1]);
                            }
                            dr["tot_db_mtax_amt"] = 0;
                            dr["tot_cr_mtax_amt"] = 0;
                        }
                        #endregion Before Group like_type

                        #region Before Group mtax_code.....

                        bool _statusdoc = false;
                        if (i != 0)
                        {
                            if (ds.Tables[0].Rows[i]["mtax_code"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["mtax_code"].ToString().Trim())
                                _statusdoc = true;
                        }
                        else
                            _statusdoc = true;

                        if (_statusdoc)
                        {
                            dr["db_mtax_amt"] = 0;
                            dr["cr_mtax_amt"] = 0;
                        }
                        #endregion Before Group like_type

                        #region on every row........
                        dr["db_mtax_amt"] = (dr["db_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["db_mtax_amt"]) : 0);
                        dr["tot_db_mtax_amt"] = (dr["tot_db_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["tot_db_mtax_amt"]) : 0);
                        dr["cr_mtax_amt"] = (dr["cr_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["cr_mtax_amt"]) : 0);
                        dr["tot_cr_mtax_amt"] = (dr["tot_cr_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["tot_cr_mtax_amt"]) : 0);
                        dr["period"] = Period;
                        dr["period_year"] = Year;
                        dr["start_date"] = objDVOMultiTaxstxmtaxd.FromDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        dr["end_date"] = objDVOMultiTaxstxmtaxd.ToDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        if (objDVOMultiTaxstxmtaxd.orig_journal == "R")
                        {
                            dr["ledger"] = " OE,AR ";
                        }
                        else if (objDVOMultiTaxstxmtaxd.orig_journal == "P")
                        {
                            dr["ledger"] = "AP,PU";
                        }
                        else
                        {
                            dr["ledger"] = "AR, OE, AP, PU ";
                        }
                        if (dr["debit_credit"].ToString().Trim() == "DB")
                        {
                            dr["db_mtax_amt"] = Convert.ToDecimal(dr["db_mtax_amt"]) + Convert.ToDecimal(dr["mtax_amt"]);
                            dr["tot_db_mtax_amt"] = Convert.ToDecimal(dr["tot_db_mtax_amt"]) + Convert.ToDecimal(dr["mtax_amt"]);
                        }
                        else
                        {
                            dr["cr_mtax_amt"] = Convert.ToDecimal(dr["cr_mtax_amt"]) + Convert.ToDecimal(dr["mtax_amt"]);
                            dr["tot_cr_mtax_amt"] = Convert.ToDecimal(dr["tot_cr_mtax_amt"]) + Convert.ToDecimal(dr["mtax_amt"]);
                        }

                        #endregion on every row

                    }//end for
                }//end ds.Tables[0].Rows.Count 
            }//end ds.Tables.Count
            return ds;
        }
        //*************************************************************

        //Added By Rahul Jain On 07/30/2009 for getting Print Multilevel Tax Analysis Detail Report
        public static DataSet GetMultilevelTaxAnalysisDetail(ref DVOMultiTaxstxmtaxd objDVOMultiTaxstxmtaxd, string Period, string Year)
        {
            Object[] parameters = new object[3];
            parameters[0] = objDVOMultiTaxstxmtaxd.orig_journal;
            parameters[1] = objDVOMultiTaxstxmtaxd.FromDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[2] = objDVOMultiTaxstxmtaxd.ToDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(objDVOMultiTaxstxmtaxd.FINDQUERY_ANALYSIS_DETAIL(ref parameters));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Added new column as per the requirement

                    #region Add New Column
                    ds.Tables[0].Columns.Add("period");
                    ds.Tables[0].Columns.Add("period_year");
                    ds.Tables[0].Columns.Add("start_date");
                    ds.Tables[0].Columns.Add("end_date");
                    ds.Tables[0].Columns.Add("ledger");
                    ds.Tables[0].Columns.Add("acct_desc");
                    ds.Tables[0].Columns.Add("db_mtax_amt", typeof(decimal));
                    ds.Tables[0].Columns.Add("cr_mtax_amt", typeof(decimal));
                    ds.Tables[0].Columns.Add("db_goods", typeof(decimal));
                    ds.Tables[0].Columns.Add("cr_goods", typeof(decimal));
                    #endregion

                    int Current_acct_no = 0;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        Current_acct_no = (dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0);
                        
                        #region on every row........

                        string db_cr = string.Empty;
                        string acct_desc = string.Empty;
                        string[] acct_details = BLLAccountingLiberary.AcctChrTn(Current_acct_no);
                        acct_desc = Convert.ToString(acct_details[0]);
                        dr["acct_desc"] = acct_desc.Trim();
                        if (acct_details[1] != string.Empty)
                        {
                            db_cr = Convert.ToString(acct_details[1]);
                        }
                        dr["db_mtax_amt"] = (dr["db_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["db_mtax_amt"]) : 0);
                        dr["cr_mtax_amt"] = (dr["cr_mtax_amt"] != DBNull.Value ? Convert.ToDecimal(dr["cr_mtax_amt"]) : 0);
                        dr["cr_goods"] = (dr["cr_goods"] != DBNull.Value ? Convert.ToDecimal(dr["cr_goods"]) : 0);
                        dr["db_goods"] = (dr["db_goods"] != DBNull.Value ? Convert.ToDecimal(dr["db_goods"]) : 0);
                        dr["period"] = Period;
                        dr["period_year"] = Year;
                        dr["start_date"] = objDVOMultiTaxstxmtaxd.FromDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        dr["end_date"] = objDVOMultiTaxstxmtaxd.ToDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        if (objDVOMultiTaxstxmtaxd.orig_journal == "R")
                        {
                            dr["ledger"] = " OE,AR ";
                        }
                        else if (objDVOMultiTaxstxmtaxd.orig_journal == "P")
                        {
                            dr["ledger"] = "AP,PU";
                        }
                        else
                        {
                            dr["ledger"] = "AR, OE, AP, PU ";
                        }
                        if (dr["debit_credit"].ToString().Trim() == "DB")
                        {
                            dr["db_mtax_amt"] = dr["mtax_amt"];
                            dr["db_goods"] = dr["goods"];
                            dr["cr_mtax_amt"] = 0;
                            dr["cr_goods"] = 0;
                        }
                        else
                        {
                            dr["cr_mtax_amt"] = dr["mtax_amt"];
                            dr["cr_goods"] = dr["goods"];
                            dr["db_mtax_amt"] = 0;
                            dr["db_goods"] = 0;
                        }

                        #endregion on every row

                    }//end for
                }//end ds.Tables[0].Rows.Count 
            }//end ds.Tables.Count
            return ds;
        }
        //*************************************************************
      
    }
}
