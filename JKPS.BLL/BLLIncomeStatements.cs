using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
namespace JKPS.BLL
{
    public class BLLIncomeStatements
    {
        //This function prepare data for Print Income statement detail and summary report.
        public static DataTable GetIncomeStm(string dpt, string period, string year)
        {
            DataTable objDataTable = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string CurrMonth, CurrYear;
            //CurrMonth = ReportingUtilities.GetCurr_periodstgcntrc();
            //CurrYear = ReportingUtilities.GetCurr_yearstgcntrc();
            CurrMonth = DVOApplicationUserInfo.CurPeriod;
            CurrYear = DVOApplicationUserInfo.CurYear;
            Object[] parameters = new object[3];
            if (period == string.Empty)
                parameters[0] = CurrMonth;
            else
                parameters[0] = period;
            if (year == string.Empty)
                parameters[1] = CurrYear;
            else
                parameters[1] = year;
            parameters[2] = dpt;
            try
            {
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).GET_INC_STM);
                ds.Tables[0].Columns[0].ColumnName = "activity";
                ds.Tables[0].Columns[1].ColumnName = "balance";
                ds.Tables[0].Columns[2].ColumnName = "department";
                ds.Tables[0].Columns[3].ColumnName = "period_month";
                ds.Tables[0].Columns[4].ColumnName = "period_year";
                ds.Tables[0].Columns[5].ColumnName = "this_month";
                ds.Tables[0].Columns[6].ColumnName = "acct_desc";
                ds.Tables[0].Columns[7].ColumnName = "acct_no";
                ds.Tables[0].Columns[8].ColumnName = "acct_type";
                ds.Tables[0].Columns[9].ColumnName = "incr_with_crdt";
                ds.Tables[0].Columns[10].ColumnName = "processing_seq";
                ds.Tables[0].Columns[11].ColumnName = "subtotal_group";
                ds.Tables[0].Columns[12].ColumnName = "cost_goods";
                ds.Tables[0].Columns[13].ColumnName = "income";
                ds.Tables[0].Columns.Add("asterisk");
                ds.Tables[0].Columns.Add("m_amt",typeof(decimal));
                ds.Tables[0].Columns.Add("y_amt",typeof(decimal));
                ds.Tables[0].Columns.Add("m_pct", typeof(decimal));
                ds.Tables[0].Columns.Add("y_pct", typeof(decimal));
                ds.Tables[0].Columns.Add("mtd_tot", typeof(decimal));
                ds.Tables[0].Columns.Add("ytd_tot",typeof(decimal));
                ds.Tables[0].Columns.Add("m_amt_sub", typeof(decimal));
                ds.Tables[0].Columns.Add("y_amt_sub", typeof(decimal));
                ds.Tables[0].Columns.Add("m_pct_sub", typeof(decimal));
                ds.Tables[0].Columns.Add("y_pct_sub", typeof(decimal));

                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "processing_seq,acct_no,department";
                objDataTable = dv.ToTable();
                bool this_period = false;
                if (period == CurrMonth && year == CurrYear)
                    this_period = true;
                DataSet dsCnt = null;
                if (this_period)
                {
                        object[] cntparam = new object[3];
                        cntparam[0] = dpt;
                        cntparam[1] = period;
                        cntparam[2] = year;
                        dsCnt = objDalBaseClass.GetData(ref cntparam,typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).GET_COUNT);
                        dsCnt.Tables[0].Columns[0].ColumnName="acct_no";
                }
                //get total income for percentage calculations
                decimal[] da =GetIncome(dpt,period,year);
                decimal  mtd_tot = da[0];
                decimal ytd_tot = da[1];
                decimal m_amt_sub = 0;
                decimal y_amt_sub = 0;
                for (int i = 0; i < objDataTable.Rows.Count;i++)
                {

                    DataRow dr = objDataTable.Rows[i];
                    dr["mtd_tot"] = mtd_tot;
                    dr["ytd_tot"] = ytd_tot;
                    int acct_no = dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0;
                    //asterisk or no asterisk
                    // an asterisk is printed next to accounts
                    // under the following conditions:
                    // if report is for the current period, an asterisk will print if amounts
                    // have been posted to prior periods from the current period.
                    // if report is for a prior period, as asterisk will print
                    // if amounts have been posted
                    // to that prior period from the current period.
                    if (this_period)
                    {   
                        DataRow[] dra = dsCnt.Tables[0].Select("acct_no = " + acct_no);
                        if (dra.Length != 0)
                            dr["asterisk"] = "*";

                        this_period = true;
                    }
                    else
                    {
                        decimal this_month = dr["this_month"] != DBNull.Value ? Convert.ToDecimal(dr["this_month"]) : 0;
                        if (this_month != 0)
                            dr["asterisk"] = "*";
                    }
                    if (dr["activity"] == DBNull.Value)
                        dr["activity"] = 0;
                    if (dr["this_month"] == DBNull.Value)
                        dr["this_month"] = 0;
                    if (dr["balance"] == DBNull.Value)
                        dr["balance"] = 0;
                    // processing_seq:  6 is revenue, 7 is cgs, 8 is expense
                    int processing_seq = (dr["processing_seq"] != DBNull.Value ? Convert.ToInt32(dr["processing_seq"]) : 0);
                    string incr_with_crdt = dr["incr_with_crdt"] != DBNull.Value ? dr["incr_with_crdt"].ToString().Trim() : string.Empty;
                    if ((processing_seq == 6 && incr_with_crdt == "Y") || (processing_seq > 6 && incr_with_crdt != "Y"))
                    {
                        dr["m_amt"] = Convert.ToDecimal(dr["activity"]) + Convert.ToDecimal(dr["this_month"]);
                        dr["y_amt"] = dr["balance"];
                    }
                    else
                    {
                        dr["m_amt"] = (Convert.ToDecimal(dr["activity"]) + Convert.ToDecimal(dr["this_month"])) * -1;
                        dr["y_amt"] = Convert.ToDecimal(dr["balance"]) * -1;
                    }
                    if (mtd_tot != 0)
                    {
                        //calculate % of mtd gross revenue for monthly activity
                        //for acct/dept
                        dr["m_pct"] = (Convert.ToDecimal(dr["m_amt"]) / mtd_tot) * 100;
                    }
                    if (ytd_tot != 0)
                    {
                        //calculate % of ytd gross revenue for for acct/dept
                        dr["y_pct"] = (Convert.ToDecimal(dr["y_amt"]) / ytd_tot) * 100;
                    }
                    //accumulate monthly & yearly totals for this subtotal group
                    m_amt_sub = m_amt_sub + Convert.ToDecimal(dr["m_amt"]);
                    y_amt_sub = y_amt_sub + Convert.ToDecimal(dr["y_amt"]);
                    #region Process on after group acct_no

                    bool _Status = false;
                    if (objDataTable.Rows.Count != i + 1)
                    {
                        if (acct_no != Convert.ToInt32(objDataTable.Rows[i + 1]["acct_no"]))
                        {
                            _Status = true;
                        }
                    }
                    else if (objDataTable.Rows.Count == i + 1)
                        _Status = true;

                    if (_Status)
                    {
                        if (dr["subtotal_group"] != DBNull.Value)
                        {
                            dr["m_amt_sub"] = m_amt_sub;
                            dr["y_amt_sub"] = y_amt_sub;
                            if (mtd_tot != 0)
                            {
                                //calculate % of mtd gross revenue for this subtotal group
                                //for acct/dept
                                dr["m_pct_sub"] = (m_amt_sub / mtd_tot) * 100;
                            }
                            if (ytd_tot != 0)
                            {
                                //calculate % of ytd gross revenue for this subtotal group
                                dr["y_pct_sub"] = (y_amt_sub / ytd_tot) * 100;
                            }

                        }
                        m_amt_sub = 0;
                        y_amt_sub = 0;
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objDataTable;
        }
        // this function calculates the total income for percentage calculations
        public static decimal[] GetIncome(string dpt, string period,string year)
        {
            
            decimal mtd_tot = 0;
            decimal ytd_tot = 0;
            decimal[] da = new decimal[2];
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string CurrMonth, CurrYear;
            //CurrMonth = ReportingUtilities.GetCurr_periodstgcntrc();
            //CurrYear = ReportingUtilities.GetCurr_yearstgcntrc();
            CurrMonth = DVOApplicationUserInfo.CurPeriod;
            CurrYear = DVOApplicationUserInfo.CurYear;
            Object[] parameters = new object[3];
            if (period == string.Empty)
                parameters[0] = CurrMonth;
            else
                parameters[0] = period;
            if (year == string.Empty)
                parameters[1] = CurrYear;
            else
                parameters[1] = year;
            parameters[2] = dpt;
            try
            {
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).GET_TD_PER);
                ds.Tables[0].Columns[0].ColumnName = "incr_with_crdt";
                ds.Tables[0].Columns[1].ColumnName = "activity";
                ds.Tables[0].Columns[2].ColumnName = "balance";
                ds.Tables[0].Columns[3].ColumnName = "this_month";

                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["activity"] == DBNull.Value)
                        dr["activity"] = 0;
                    if (dr["this_month"] == DBNull.Value)
                        dr["this_month"] = 0;
                    if(dr["balance"]==DBNull.Value)
                        dr["balance"]=0;
                    if (dr["incr_with_crdt"].ToString().Trim() == "Y")
                    {
                        mtd_tot = mtd_tot + (Convert.ToDecimal(dr["activity"]) + Convert.ToDecimal(dr["activity"]));
                        ytd_tot = ytd_tot + Convert.ToDecimal(dr["balance"]);
                    }
                    else
                    {
                        mtd_tot = mtd_tot - (Convert.ToDecimal(dr["activity"]) + Convert.ToDecimal(dr["activity"]));
                        ytd_tot = ytd_tot - Convert.ToDecimal(dr["balance"]);
                    }
                }
                da[0] = mtd_tot;
                da[1] = ytd_tot;
                ds.Dispose();
                return da;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Get Startdate and End date from stxperdr by sending period,year as parameter
        public static string[] GetXperdrDate(string period, string year)
        {   
            string[] str=new string[2];
            try
            {
                object[] parameters = new object[2];
                parameters[0] = period;
                parameters[1] = year;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).GET_XPERDR_DATE);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    str[0] = ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd/MM/yyyy") : "";
                    str[1] = ds.Tables[0].Rows[0][1] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0][1]).ToString("dd/MM/yyyy") : "";
                }
                else
                {
                    str[0] = "";
                    str[1] = "";
                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return str;

        }
    }
}
