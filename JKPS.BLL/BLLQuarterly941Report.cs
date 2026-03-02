using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;
namespace JKPS.BLL
{
    public  class BLLQuarterly941Report
    {
        public static DataTable GetQuarterly941Data(decimal fed_deposits, string qtr_date, decimal adjust_fit)
        {
            DataSet ds = null;
            DataTable objDataTable = new DataTable();
            try  
            {
                string[] qtrdates = QtrInput(qtr_date);
                string start_date = qtrdates[1];
                string end_date = qtrdates[2];
                string year_date = qtrdates[0];
                object[] parameters=new object[2];
                parameters[0] = start_date;
                parameters[1] = end_date;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOQuarterlyReport), (new DVOQuarterlyReport()).GET_QTR941RPT);
                #region Set the column name..and add some columns for ......
                ds.Tables[0].Columns[0].ColumnName = "ref_code";
                ds.Tables[0].Columns[1].ColumnName = "act_code";
                ds.Tables[0].Columns[2].ColumnName = "act_type";
                ds.Tables[0].Columns[3].ColumnName = "amount";
                ds.Tables[0].Columns[4].ColumnName = "pay_date";
                ds.Tables[0].Columns.Add("start_date");
                ds.Tables[0].Columns.Add("end_date");
                ds.Tables[0].Columns.Add("m_period");
                ds.Tables[0].Columns.Add("adjust_fit");
                ds.Tables[0].Columns.Add("wages_fica");
                ds.Tables[0].Columns.Add("wages_medicare");
                ds.Tables[0].Columns.Add("fit_withheld");
                ds.Tables[0].Columns.Add("withheld_fica");
                ds.Tables[0].Columns.Add("withheld_medicare");
                ds.Tables[0].Columns.Add("tax_liability");
                ds.Tables[0].Columns.Add("fica_code");
                ds.Tables[0].Columns.Add("fica_wages");
                ds.Tables[0].Columns.Add("fica_max");
                ds.Tables[0].Columns.Add("medicare_wages");
                ds.Tables[0].Columns.Add("medicare_max");
                ds.Tables[0].Columns.Add("fedtax");
                ds.Tables[0].Columns.Add("fedtax_code");
                ds.Tables[0].Columns.Add("socsec_total");
                ds.Tables[0].Columns.Add("medicare_code");
                ds.Tables[0].Columns.Add("medicare_ob_code");
                ds.Tables[0].Columns.Add("medicare_total");
                ds.Tables[0].Columns.Add("taxes_total");
                ds.Tables[0].Columns.Add("empl_count");
                ds.Tables[0].Columns.Add("all_wages");
                ds.Tables[0].Columns.Add("fica_rate");
                ds.Tables[0].Columns.Add("fica_due");
                ds.Tables[0].Columns.Add("medicare_rate");
                ds.Tables[0].Columns.Add("fica_ob_code");
                ds.Tables[0].Columns.Add("medicare_due");
                ds.Tables[0].Columns.Add("fica_total");
                ds.Tables[0].Columns.Add("fica_adjust");
                ds.Tables[0].Columns.Add("adjust_fica");
                ds.Tables[0].Columns.Add("eic_code");
                ds.Tables[0].Columns.Add("eic_total");
                ds.Tables[0].Columns.Add("taxes_net");
                ds.Tables[0].Columns.Add("fed_deposits");
                ds.Tables[0].Columns.Add("tax_balance");
                ds.Tables[0].Columns.Add("over_payed");
                ds.Tables[0].Columns.Add("day1_total");
                ds.Tables[0].Columns.Add("day2_total");
                ds.Tables[0].Columns.Add("day3_total");
                ds.Tables[0].Columns.Add("day4_total");
                ds.Tables[0].Columns.Add("day5_total");
                ds.Tables[0].Columns.Add("day6_total");
                ds.Tables[0].Columns.Add("day7_total");
                ds.Tables[0].Columns.Add("day8_total");
                ds.Tables[0].Columns.Add("day9_total");
                ds.Tables[0].Columns.Add("day10_total");
                ds.Tables[0].Columns.Add("day11_total");
                ds.Tables[0].Columns.Add("day12_total");
                ds.Tables[0].Columns.Add("day13_total");
                ds.Tables[0].Columns.Add("day14_total");
                ds.Tables[0].Columns.Add("day15_total");
                ds.Tables[0].Columns.Add("day16_total");
                ds.Tables[0].Columns.Add("day17_total");
                ds.Tables[0].Columns.Add("day18_total");
                ds.Tables[0].Columns.Add("day19_total");
                ds.Tables[0].Columns.Add("day20_total");
                ds.Tables[0].Columns.Add("day21_total");
                ds.Tables[0].Columns.Add("day22_total");
                ds.Tables[0].Columns.Add("day23_total");
                ds.Tables[0].Columns.Add("day24_total");
                ds.Tables[0].Columns.Add("day25_total");
                ds.Tables[0].Columns.Add("day26_total");
                ds.Tables[0].Columns.Add("day27_total");
                ds.Tables[0].Columns.Add("day28_total");
                ds.Tables[0].Columns.Add("day29_total");
                ds.Tables[0].Columns.Add("day30_total");
                ds.Tables[0].Columns.Add("day31_total");
                ds.Tables[0].Columns.Add("month1_total");
                ds.Tables[0].Columns.Add("month2_total");
                ds.Tables[0].Columns.Add("month3_total");
                ds.Tables[0].Columns.Add("from_date");
                ds.Tables[0].Columns.Add("to_date");
                #endregion
                //create temporary YTD employee table of taxable wages..day31_total
                DataTable Objemp_temp = new DataTable();
                Objemp_temp.Columns.Add("soc_sec_num");
                Objemp_temp.Columns.Add("amount");
                Objemp_temp.Columns.Add("qtr_amount",typeof(decimal));
                decimal zero_value = 0;
                decimal day_eic_total = 0;
                decimal day_fit_withheld = 0;
                decimal day_withheld_fica = 0;
                decimal day_withheld_medicare = 0;
                decimal eic_total = 0;
                objDataTable = ds.Tables[0].Clone();
                List<DVOUpdatePayDefaults> lstPayDefaults = new List<DVOUpdatePayDefaults>();

                for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    dr["ref_code"]=(dr["ref_code"] != DBNull.Value ? dr["ref_code"].ToString().Trim(): string.Empty);
                    dr["act_code"]=(dr["act_code"] != DBNull.Value ? dr["act_code"].ToString().Trim(): string.Empty);
                    dr["act_type"]=(dr["act_type"] != DBNull.Value ? dr["act_type"].ToString().Trim(): string.Empty);
                    dr["amount"] = (dr["amount"] != DBNull.Value ? dr["amount"].ToString().Trim() :"0");
                    dr["pay_date"] = (dr["pay_date"] != DBNull.Value ? dr["pay_date"].ToString().Trim() : "01/01/0001"); 
                    dr["fed_deposits"] = fed_deposits;
                    dr["adjust_fit"] = adjust_fit;
                    dr["start_date"] = start_date;
                    dr["end_date"] = end_date;   
 
                    #region before first row
                    if (i == 0)
                    {
                        #region initialize accumulations
                        if (dr["fica_wages"] == DBNull.Value)
                            dr["fica_wages"] = 0;
                        if (dr["medicare_wages"] == DBNull.Value)
                            dr["medicare_wages"] = 0;
                        if (dr["all_wages"] == DBNull.Value)
                            dr["all_wages"] = 0;
                        if (dr["socsec_total"] == DBNull.Value)
                            dr["socsec_total"] = 0;
                        if (dr["fica_total"] == DBNull.Value)
                            dr["fica_total"] = 0;
                        if (dr["medicare_total"] == DBNull.Value)
                            dr["medicare_total"] = 0;
                        if (dr["medicare_max"] == DBNull.Value)
                            dr["medicare_max"] = 0;
                        if (dr["fedtax"] == DBNull.Value)
                            dr["fedtax"] = 0;
                        if (dr["month1_total"] == DBNull.Value)
                            dr["month1_total"] = 0;
                        if (dr["month2_total"] == DBNull.Value)
                            dr["month2_total"] = 0;
                        if (dr["month3_total"] == DBNull.Value)
                            dr["month3_total"] = 0;
                        if (dr["fica_max"] == DBNull.Value)
                            dr["fica_max"] = 0;
                        if (dr["fica_rate"] == DBNull.Value)
                            dr["fica_rate"] = 0;
                        if (dr["medicare_rate"] == DBNull.Value)
                            dr["medicare_rate"] = 0;
                        if (dr["fedtax_code"] == DBNull.Value)
                            dr["fedtax_code"] = 0;
                        //reset on each period
                        if (dr["tax_liability"] == DBNull.Value)
                            dr["tax_liability"] = 0;
                        if (dr["eic_total"] == DBNull.Value)
                            dr["eic_total"] = 0;
                        if (dr["withheld_fica"] == DBNull.Value)
                            dr["withheld_fica"] = 0;
                        if (dr["withheld_medicare"] == DBNull.Value)
                            dr["withheld_medicare"] = 0;
                        if (dr["fit_withheld"] == DBNull.Value)
                            dr["fit_withheld"] = 0;
                        if (dr["wages_fica"] == DBNull.Value)
                            dr["wages_fica"] = 0;
                        if (dr["wages_medicare"] == DBNull.Value)
                            dr["wages_medicare"] = 0;
                        if (dr["day1_total"] == DBNull.Value)
                            dr["day1_total"] = 0;
                        if (dr["day2_total"] == DBNull.Value)
                            dr["day2_total"] = 0;
                        if (dr["day3_total"] == DBNull.Value)
                            dr["day3_total"] = 0;
                        if (dr["day4_total"] == DBNull.Value)
                            dr["day4_total"] = 0;
                        if (dr["day5_total"] == DBNull.Value)
                            dr["day5_total"] = 0;
                        if (dr["day6_total"] == DBNull.Value)
                            dr["day6_total"] = 0;
                        if (dr["day7_total"] == DBNull.Value)
                            dr["day7_total"] = 0;
                        if (dr["day8_total"] == DBNull.Value)
                            dr["day8_total"] = 0;
                        if (dr["day9_total"] == DBNull.Value)
                            dr["day9_total"] = 0;
                        if (dr["day10_total"] == DBNull.Value)
                            dr["day10_total"] = 0;
                        if (dr["day11_total"] == DBNull.Value)
                            dr["day11_total"] = 0;
                        if (dr["day12_total"] == DBNull.Value)
                            dr["day12_total"] = 0;
                        if (dr["day13_total"] == DBNull.Value)
                            dr["day13_total"] = 0;
                        if (dr["day14_total"] == DBNull.Value)
                            dr["day14_total"] = 0;
                        if (dr["day15_total"] == DBNull.Value)
                            dr["day15_total"] = 0;
                        if (dr["day16_total"] == DBNull.Value)
                            dr["day16_total"] = 0;
                        if (dr["day17_total"] == DBNull.Value)
                            dr["day17_total"] = 0;
                        if (dr["day18_total"] == DBNull.Value)
                            dr["day18_total"] = 0;
                        if (dr["day19_total"] == DBNull.Value)
                            dr["day19_total"] = 0;
                        if (dr["day20_total"] == DBNull.Value)
                            dr["day20_total"] = 0;
                        if (dr["day21_total"] == DBNull.Value)
                            dr["day21_total"] = 0;
                        if (dr["day22_total"] == DBNull.Value)
                            dr["day22_total"] = 0;
                        if (dr["day23_total"] == DBNull.Value)
                            dr["day23_total"] = 0;
                        if (dr["day24_total"] == DBNull.Value)
                            dr["day24_total"] = 0;
                        if (dr["day25_total"] == DBNull.Value)
                            dr["day25_total"] = 0;
                        if (dr["day26_total"] == DBNull.Value)
                            dr["day26_total"] = 0;
                        if (dr["day27_total"] == DBNull.Value)
                            dr["day27_total"] = 0;
                        if (dr["day28_total"] == DBNull.Value)
                            dr["day28_total"] = 0;
                        if (dr["day29_total"] == DBNull.Value)
                            dr["day29_total"] = 0;
                        if (dr["day30_total"] == DBNull.Value)
                            dr["day30_total"] = 0;
                        if (dr["day31_total"] == DBNull.Value)
                            dr["day31_total"] = 0;
                        #endregion
                        //This method is use to get Payroll Defaults from database 
                        DVOUpdatePayDefaults objPayDefaults = new DVOUpdatePayDefaults();
                        lstPayDefaults = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaults);
                        dr["fedtax_code"] = lstPayDefaults[0].fedtax_code;
                        dr["fica_code"] = lstPayDefaults[0].fica_code;
                        dr["medicare_code"] = lstPayDefaults[0].medicare_code;
                        dr["fica_ob_code"] = lstPayDefaults[0].fica_ob_code;
                        dr["medicare_ob_code"] = lstPayDefaults[0].medicare_ob_code;
                        dr["eic_code"] = lstPayDefaults[0].eic_code;
                        //set fica_max to the default deduction values of limit/rate
                        decimal tmp_limit = 0;
                        decimal tmp_ded_rate = 0;
                        string fica_sql = "select dflt_limit, dflt_rate from MasterDedcodes where MasterDedcodes.ded_code = '" + dr["fica_code"].ToString().Trim() + "'";
                        DataSet temp_ficads = objDALBaseClass.GetData(fica_sql);
                        if (temp_ficads.Tables[0].Rows.Count != 0)
                        {
                            tmp_limit = (temp_ficads.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(temp_ficads.Tables[0].Rows[0][0]) : 0);
                            tmp_ded_rate = (temp_ficads.Tables[0].Rows[0][1] != DBNull.Value ? Convert.ToDecimal(temp_ficads.Tables[0].Rows[0][1]) : 0);
                        }
                        if (tmp_ded_rate != 0)
                            dr["fica_max"] = tmp_limit / tmp_ded_rate;
                        if (Convert.ToDecimal(dr["fica_max"]) == 0)
                            dr["fica_max"] = 57600.00; //# default to 1993 maximum

                        // set medicare_max to the default deduction values of limit/rate
                        decimal tmp_limit1 =0;
                        decimal tmp_ded_rate1 =0;
                        string medicare_sql = "select dflt_limit, dflt_rate from MasterDedcodes where MasterDedcodes.ded_code = '" + dr["medicare_code"].ToString().Trim() + "'";
                        DataSet temp_medicare_ds = objDALBaseClass.GetData(medicare_sql);
                        if (temp_medicare_ds.Tables[0].Rows.Count!=0)
                        {
                         tmp_limit1 = (temp_medicare_ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(temp_medicare_ds.Tables[0].Rows[0][0]) : 0);
                         tmp_ded_rate1 = (temp_medicare_ds.Tables[0].Rows[0][1] != DBNull.Value ? Convert.ToDecimal(temp_medicare_ds.Tables[0].Rows[0][1]) : 0);
                        }
                        if (tmp_ded_rate1 != 0)
                            dr["medicare_max"] = tmp_limit1 / tmp_ded_rate1;
                        if (Convert.ToDecimal(dr["medicare_max"]) == 0)
                            dr["medicare_max"] = 135000000.00; //# default to 1993 maximum

                        //build employee YTD fica wages table...........
                        PrepareYtoDTable(ref Objemp_temp, year_date, start_date, zero_value);

                        //get the fica rate from the sum of default rates defined in
                        decimal tmp_dedrate = 0;
                        string tmp_ded_rate_sql = "select dflt_rate from MasterDedcodes where MasterDedcodes.ded_code ='" + dr["fica_code"].ToString().Trim() + "'";
                        DataSet tmp_ded_rate_ds = objDALBaseClass.GetData(tmp_ded_rate_sql);
                        if (tmp_ded_rate_ds.Tables[0].Rows.Count != 0)
                            tmp_dedrate = ((tmp_ded_rate_ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(tmp_ded_rate_ds.Tables[0].Rows[0][0]) : 0));
                        decimal tmp_ob_rate = 0;
                        string tmp_ob_rate_sql = "select dflt_rate from MasterOblCodes where MasterOblCodes.obl_code ='" + dr["fica_ob_code"].ToString().Trim() + "'";
                        DataSet tmp_ob_rate_ds = objDALBaseClass.GetData(tmp_ded_rate_sql);
                        if (tmp_ob_rate_ds.Tables[0].Rows.Count != 0)
                            tmp_ob_rate = ((tmp_ob_rate_ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(tmp_ob_rate_ds.Tables[0].Rows[0][0]) : 0));

                        dr["fica_rate"] = tmp_dedrate + tmp_ob_rate;
                        if (Convert.ToDecimal(dr["fica_rate"]) == 0)
                            dr["fica_rate"] = 0.124;// 1993 value 

                        //get medicare rate from the sum of default rates defined in
                        decimal tmp_ded_rate0 = 0;
                        string tmp_ded_rate_sql0 = "select dflt_rate from MasterDedcodes where MasterDedcodes.ded_code ='" + dr["medicare_code"].ToString().Trim() + "'";
                        DataSet tmp_ded_rate_ds0 = objDALBaseClass.GetData(tmp_ded_rate_sql0);
                        if (tmp_ded_rate_ds0.Tables[0].Rows.Count != 0)
                            tmp_ded_rate0 = ((tmp_ded_rate_ds0.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(tmp_ded_rate_ds0.Tables[0].Rows[0][0]) : 0));

                        decimal tmp_ob_rate0 = 0;
                        string tmp_ob_rate_sql0 = "select dflt_rate from MasterOblCodes where MasterOblCodes.obl_code ='" + dr["medicare_ob_code"].ToString().Trim() + "'";
                        DataSet tmp_ob_rate_ds0 = objDALBaseClass.GetData(tmp_ob_rate_sql0);
                        if (tmp_ob_rate_ds0.Tables[0].Rows.Count != 0)
                            tmp_ob_rate0 = ((tmp_ob_rate_ds0.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDecimal(tmp_ob_rate_ds0.Tables[0].Rows[0][0]) : 0));
                        dr["medicare_rate"] = tmp_ded_rate0 + tmp_ob_rate0;
                        if (Convert.ToDecimal(dr["fica_rate"]) == 0)
                            dr["medicare_rate"] = 0.029;// 1993 value 
                    }
                    else
                    {
                        #region initialize accumulations

                        if (dr["fica_wages"] == DBNull.Value)
                            dr["fica_wages"] = ds.Tables[0].Rows[i - 1]["fica_wages"];
                        if (dr["medicare_wages"] == DBNull.Value)
                            dr["medicare_wages"] = ds.Tables[0].Rows[i - 1]["medicare_wages"];
                        if (dr["all_wages"] == DBNull.Value)
                            dr["all_wages"] = ds.Tables[0].Rows[i - 1]["all_wages"];
                        if (dr["socsec_total"] == DBNull.Value)
                            dr["socsec_total"] = ds.Tables[0].Rows[i - 1]["socsec_total"];
                        if (dr["fica_total"] == DBNull.Value)
                            dr["fica_total"] = ds.Tables[0].Rows[i - 1]["fica_total"];
                        if (dr["medicare_total"] == DBNull.Value)
                            dr["medicare_total"] = ds.Tables[0].Rows[i - 1]["medicare_total"];
                        if (dr["medicare_max"] == DBNull.Value)
                            dr["medicare_max"] = ds.Tables[0].Rows[i - 1]["medicare_max"];
                        if (dr["fedtax"] == DBNull.Value)
                            dr["fedtax"] = ds.Tables[0].Rows[i - 1]["fedtax"];
                        if (dr["month1_total"] == DBNull.Value)
                            dr["month1_total"] = ds.Tables[0].Rows[i - 1]["month1_total"];
                        if (dr["month2_total"] == DBNull.Value)
                            dr["month2_total"] = ds.Tables[0].Rows[i - 1]["month2_total"];
                        if (dr["month3_total"] == DBNull.Value)
                            dr["month3_total"] = ds.Tables[0].Rows[i - 1]["month3_total"];
                        if (dr["fica_max"] == DBNull.Value)
                            dr["fica_max"] = ds.Tables[0].Rows[i - 1]["fica_max"];
                        if (dr["fica_rate"] == DBNull.Value)
                            dr["fica_rate"] = ds.Tables[0].Rows[i - 1]["fica_rate"];
                        if (dr["medicare_rate"] == DBNull.Value)
                            dr["medicare_rate"] = ds.Tables[0].Rows[i - 1]["medicare_rate"];
                        if (dr["fedtax_code"] == DBNull.Value)
                            dr["fedtax_code"] = ds.Tables[0].Rows[i - 1]["fedtax_code"];
                        //reset on each period
                        if (dr["tax_liability"] == DBNull.Value)
                            dr["tax_liability"] = ds.Tables[0].Rows[i - 1]["tax_liability"];
                        if (dr["eic_total"] == DBNull.Value)
                            dr["eic_total"] = ds.Tables[0].Rows[i - 1]["eic_total"];
                        if (dr["withheld_fica"] == DBNull.Value)
                            dr["withheld_fica"] = ds.Tables[0].Rows[i - 1]["withheld_fica"];
                        if (dr["withheld_medicare"] == DBNull.Value)
                            dr["withheld_medicare"] = ds.Tables[0].Rows[i - 1]["withheld_medicare"];
                        if (dr["fit_withheld"] == DBNull.Value)
                            dr["fit_withheld"] = ds.Tables[0].Rows[i - 1]["fit_withheld"];
                        if (dr["wages_fica"] == DBNull.Value)
                            dr["wages_fica"] = ds.Tables[0].Rows[i - 1]["wages_fica"];
                        if (dr["wages_medicare"] == DBNull.Value)
                            dr["wages_medicare"] = ds.Tables[0].Rows[i - 1]["wages_medicare"];
                        if (dr["day1_total"] == DBNull.Value)
                            dr["day1_total"] = ds.Tables[0].Rows[i - 1]["day1_total"];
                        if (dr["day2_total"] == DBNull.Value)
                            dr["day2_total"] = ds.Tables[0].Rows[i - 1]["day2_total"];
                        if (dr["day3_total"] == DBNull.Value)
                            dr["day3_total"] = ds.Tables[0].Rows[i - 1]["day3_total"];
                        if (dr["day4_total"] == DBNull.Value)
                            dr["day4_total"] = ds.Tables[0].Rows[i - 1]["day4_total"];
                        if (dr["day5_total"] == DBNull.Value)
                            dr["day5_total"] = ds.Tables[0].Rows[i - 1]["day5_total"];
                        if (dr["day6_total"] == DBNull.Value)
                            dr["day6_total"] = ds.Tables[0].Rows[i - 1]["day6_total"];
                        if (dr["day7_total"] == DBNull.Value)
                            dr["day7_total"] = ds.Tables[0].Rows[i - 1]["day7_total"];
                        if (dr["day8_total"] == DBNull.Value)
                            dr["day8_total"] = ds.Tables[0].Rows[i - 1]["day8_total"];
                        if (dr["day9_total"] == DBNull.Value)
                            dr["day9_total"] = ds.Tables[0].Rows[i - 1]["day9_total"];
                        if (dr["day10_total"] == DBNull.Value)
                            dr["day10_total"] = ds.Tables[0].Rows[i - 1]["day10_total"];
                        if (dr["day11_total"] == DBNull.Value)
                            dr["day11_total"] = ds.Tables[0].Rows[i - 1]["day11_total"];
                        if (dr["day12_total"] == DBNull.Value)
                            dr["day12_total"] = ds.Tables[0].Rows[i - 1]["day12_total"];
                        if (dr["day13_total"] == DBNull.Value)
                            dr["day13_total"] = ds.Tables[0].Rows[i - 1]["day13_total"];
                        if (dr["day14_total"] == DBNull.Value)
                            dr["day14_total"] = ds.Tables[0].Rows[i - 1]["day14_total"];
                        if (dr["day15_total"] == DBNull.Value)
                            dr["day15_total"] = ds.Tables[0].Rows[i - 1]["day15_total"];
                        if (dr["day16_total"] == DBNull.Value)
                            dr["day16_total"] = ds.Tables[0].Rows[i - 1]["day16_total"];
                        if (dr["day17_total"] == DBNull.Value)
                            dr["day17_total"] = ds.Tables[0].Rows[i - 1]["day17_total"];
                        if (dr["day18_total"] == DBNull.Value)
                            dr["day18_total"] = ds.Tables[0].Rows[i - 1]["day18_total"];
                        if (dr["day19_total"] == DBNull.Value)
                            dr["day19_total"] = ds.Tables[0].Rows[i - 1]["day19_total"];
                        if (dr["day20_total"] == DBNull.Value)
                            dr["day20_total"] = ds.Tables[0].Rows[i - 1]["day20_total"];
                        if (dr["day21_total"] == DBNull.Value)
                            dr["day21_total"] = ds.Tables[0].Rows[i - 1]["day21_total"];
                        if (dr["day22_total"] == DBNull.Value)
                            dr["day22_total"] = ds.Tables[0].Rows[i - 1]["day22_total"];
                        if (dr["day23_total"] == DBNull.Value)
                            dr["day23_total"] = ds.Tables[0].Rows[i - 1]["day23_total"];
                        if (dr["day24_total"] == DBNull.Value)
                            dr["day24_total"] = ds.Tables[0].Rows[i - 1]["day24_total"];
                        if (dr["day25_total"] == DBNull.Value)
                            dr["day25_total"] = ds.Tables[0].Rows[i - 1]["day25_total"];
                        if (dr["day26_total"] == DBNull.Value)
                            dr["day26_total"] = ds.Tables[0].Rows[i - 1]["day26_total"];
                        if (dr["day27_total"] == DBNull.Value)
                            dr["day27_total"] = ds.Tables[0].Rows[i - 1]["day27_total"];
                        if (dr["day28_total"] == DBNull.Value)
                            dr["day28_total"] = ds.Tables[0].Rows[i - 1]["day28_total"];
                        if (dr["day29_total"] == DBNull.Value)
                            dr["day29_total"] = ds.Tables[0].Rows[i - 1]["day29_total"];
                        if (dr["day30_total"] == DBNull.Value)
                            dr["day30_total"] = ds.Tables[0].Rows[i - 1]["day30_total"];
                        if (dr["day31_total"] == DBNull.Value)
                            dr["day31_total"] = ds.Tables[0].Rows[i - 1]["day31_total"];
                        #endregion
                    }
                    #endregion
   
                    #region on every row..
                    dr["fica_max"] = ds.Tables[0].Rows[0]["fica_max"];
                    dr["medicare_max"] = ds.Tables[0].Rows[0]["medicare_max"];
                    dr["fica_rate"] = ds.Tables[0].Rows[0]["fica_rate"];
                    dr["medicare_rate"] = ds.Tables[0].Rows[0]["medicare_rate"];
                    dr["fedtax_code"] = lstPayDefaults[0].fedtax_code;
                    dr["fica_code"] = lstPayDefaults[0].fica_code;
                    dr["medicare_code"] = lstPayDefaults[0].medicare_code;
                    dr["fica_ob_code"] = lstPayDefaults[0].fica_ob_code;
                    dr["medicare_ob_code"] = lstPayDefaults[0].medicare_ob_code;
                    dr["eic_code"] = lstPayDefaults[0].eic_code;

                    string soc_sec_num = string.Empty;
                    string soc_sec_sql = "select styemplr.soc_sec_num from styemplr where styemplr.empl_code ='" + dr["ref_code"].ToString().Trim() + "'";
                   
                    DataSet soc_secds = objDALBaseClass.GetData(soc_sec_sql);
                    if (soc_secds.Tables[0].Rows.Count!=0)
                    soc_sec_num = (soc_secds.Tables[0].Rows[0][0] != DBNull.Value ? soc_secds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty);
                    //do not process rows with no social security number
                    if (soc_sec_num!=string.Empty)
                    {
                     //process each row by activity type
                       if (dr["act_type"].ToString()=="B")
                       {
                       // get detailed income code information
                           string inc_type = string.Empty;
                           string inccr_sql = "select * from MasterIncCodes where MasterIncCodes.inc_code ='" + dr["act_code"].ToString() + "'";
                           DataSet inccr_ds = objDALBaseClass.GetData(inccr_sql);
                           if (inccr_ds.Tables[0].Rows.Count!=0)
                           inc_type = (inccr_ds.Tables[0].Rows[0][7] != DBNull.Value ? inccr_ds.Tables[0].Rows[0][7].ToString().Trim() : string.Empty);
                           //don't process expense or advance income
                           if (!(inc_type == "E" || inc_type == "A"))
                           {
                               dr["all_wages"] = Convert.ToDecimal(dr["all_wages"]) + Convert.ToDecimal(dr["amount"]);
                               //record employee quarter wages
                               DataRow[] empdearrr = Objemp_temp.Select("soc_sec_num='" + soc_sec_num+"'");
                               if (empdearrr.Length == 0)
                               {
                                   DataRow empdr = Objemp_temp.NewRow();
                                   empdr[0] = soc_sec_num;
                                   empdr[1] = 0;
                                   empdr[2] = dr["amount"];
                                   Objemp_temp.Rows.Add(empdr);
                               }
                               else
                               {  
                                   foreach(DataRow drtemp in Objemp_temp.Rows)
                                   {
                                       if (drtemp[0].ToString().Trim() == soc_sec_num)
                                       {
                                           drtemp[1] = Convert.ToDecimal(drtemp[1]) + zero_value;
                                           drtemp[2] = Convert.ToDecimal(drtemp[2]) + Convert.ToDecimal(dr["amount"]);
                                           break;
                                        }
                                  }          
                               }
                               //accumulate fica wages
                               if (!(inc_type == "F" || inc_type == "B"))
                               {//accumulate employee fica wages
                                   foreach (DataRow drtemp in Objemp_temp.Rows)
                                   {
                                       if (drtemp[0].ToString().Trim() == soc_sec_num)
                                       {
                                           drtemp[1] = Convert.ToDecimal(drtemp[1]) + Convert.ToDecimal(dr["amount"]);
                                           drtemp[2] = Convert.ToDecimal(drtemp[2]) + zero_value;
                                           break;
                                       }
                                   }
                                   // get current total
                                    //select amount from empl_temp where empl_temp.soc_sec_num
                                   empdearrr = Objemp_temp.Select("soc_sec_num='" + soc_sec_num + "'");
                                   decimal tmp_amount =Convert.ToDecimal( empdearrr[0][1]);
                                   //sum wages_medicare
                                   if (tmp_amount < Convert.ToDecimal(dr["medicare_max"]))
                                   {
                                     
                                       dr["wages_medicare"] = Convert.ToDecimal(dr["wages_medicare"]) + Convert.ToDecimal(dr["amount"]);
                                   }
                                   else
                                   {// add partial wages
                                       if (tmp_amount < Convert.ToDecimal(dr["wages_medicare"]) + Convert.ToDecimal(dr["amount"]))
                                       {
                                          
                                         dr["wages_medicare"] =Convert.ToDecimal(dr["wages_medicare"])+((Convert.ToDecimal(dr["medicare_max"])+Convert.ToDecimal(dr["amount"]))-tmp_amount);

                                       }
                                   }
                                   //sum wages_fica
                                  if(tmp_amount < Convert.ToDecimal(dr["fica_max"]))
                                  {
                             
                                   dr["wages_fica"]= Convert.ToDecimal(dr["wages_fica"]) + Convert.ToDecimal(dr["amount"]);
                                  }
                                   else
                                   {// add partial wages
                                      
                                       if (tmp_amount < Convert.ToDecimal(dr["wages_fica"]) + Convert.ToDecimal(dr["amount"]))
                                       {
                                         dr["wages_fica"] =Convert.ToDecimal(dr["wages_fica"])+((Convert.ToDecimal(dr["fica_max"])+Convert.ToDecimal(dr["amount"]))-tmp_amount);

                                       }
                                     }
                                   }
                                }
                               //accumulate eic income
                              if (dr["act_code"] == dr["eic_code"])
                              {
                                  eic_total = eic_total + Convert.ToDecimal(dr["amount"]);
                                  day_eic_total = day_eic_total + Convert.ToDecimal(dr["amount"]); 
                              }
                           }
                           else if (dr["act_type"].ToString() == "C")
                           {
                              //get detailed deduction code information
                               string ded_sql = "select * from MasterDedcodes where MasterDedcodes.ded_code ='" + dr["act_code"].ToString() + "'";
                               DataSet ded_code_ds = objDALBaseClass.GetData(ded_sql);
                               string ded_taxred = (ded_code_ds.Tables[0].Rows[0][3] != DBNull.Value ? ded_code_ds.Tables[0].Rows[0][3].ToString().Trim() : string.Empty);
                              //accumulate federal income tax withholdings
                               if (dr["act_code"].ToString() == dr["fedtax_code"].ToString())
                               {
                                   dr["fit_withheld"] = Convert.ToDecimal(dr["fit_withheld"]) + Convert.ToDecimal(dr["amount"]);
                                   day_fit_withheld = day_fit_withheld + Convert.ToDecimal(dr["amount"]);
                           
                               }
                              // accumulate fica withholding
                               if (dr["act_code"].ToString() == dr["fica_code"].ToString())
                               {
                                 
                                   dr["withheld_fica"] = Convert.ToDecimal(dr["withheld_fica"]) + Convert.ToDecimal(dr["amount"]);
                                   day_withheld_fica = day_withheld_fica + Convert.ToDecimal(dr["amount"]);
                               }
                               //accumulate medicare withholding
                               if (dr["act_code"].ToString() == dr["medicare_code"].ToString())
                               {
                                   dr["withheld_medicare"] = Convert.ToDecimal(dr["withheld_medicare"]) + Convert.ToDecimal(dr["amount"]);
                                   day_withheld_medicare = day_withheld_medicare + Convert.ToDecimal(dr["amount"]);
                               }
                               // adjust fica/taxable income accumulation
                               if (ded_taxred == "A" || ded_taxred == "B")
                               {
                               //reduce taxable income
                               
                                   dr["all_wages"] = Convert.ToDecimal(dr["all_wages"]) - Convert.ToDecimal(dr["amount"]);
                               // adjust the employee YTD amount
                               foreach (DataRow drtemp in Objemp_temp.Rows)
                               {
                                   if (drtemp[0].ToString().Trim() == soc_sec_num)
                                   {
                                       drtemp[1] = Convert.ToDecimal(drtemp[1]) - Convert.ToDecimal(dr["amount"]);
                                       drtemp[2] = Convert.ToDecimal(drtemp[2]) - zero_value;
                                       break;
                                   }
                               }
                               //get current total
                               DataRow[] empdearrr = Objemp_temp.Select("soc_sec_num='" + soc_sec_num + "'");
                               decimal emp_amount = 0;
                               if (empdearrr.Length == 0)
                               {
                                   emp_amount = Convert.ToDecimal(dr["amount"]) * -1;
                                   DataRow empdr = Objemp_temp.NewRow();
                                   empdr[0] = soc_sec_num;
                                   empdr[1] = emp_amount;
                                   empdr[2] = 0;
                                   Objemp_temp.Rows.Add(empdr);
                               }
                               else
                               {
                                   emp_amount = Convert.ToDecimal(empdearrr[0][1]);
                               }
                               //adjust the fica wages only by amount relative to max fica
                               if (emp_amount < Convert.ToDecimal(dr["fica_max"]) - Convert.ToDecimal(dr["amount"]))
                               {
                                  
                                   dr["wages_fica"] = Convert.ToDecimal(dr["wages_fica"]) - Convert.ToDecimal(dr["amount"]);
                               }
                               else
                               {// subtract partial wages
                                 if (emp_amount < Convert.ToDecimal(dr["fica_max"]))
                                 {
                                   
                                     dr["wages_fica"] = Convert.ToDecimal(dr["wages_fica"]) - (Convert.ToDecimal(dr["fica_max"]) - emp_amount);
                                 }
                               }
                               //adjust medicare wages only by amount relative to max mdcr
                               if (emp_amount < Convert.ToDecimal(dr["medicare_max"]) - Convert.ToDecimal(dr["amount"]))
                               {
                                   
                                   dr["wages_medicare"] = Convert.ToDecimal(dr["wages_medicare"]) - Convert.ToDecimal(dr["amount"]);
                               }
                               else
                               {// subtract partial wages
                                   if (emp_amount < Convert.ToDecimal(dr["medicare_max"]))
                                   {
                                     
                                       dr["wages_medicare"] = Convert.ToDecimal(dr["wages_medicare"]) - (Convert.ToDecimal(dr["medicare_max"]) - emp_amount);
                                   }
                               }
                               }
                               //adjust fica income accumulation..............
                               if (ded_taxred == "F" || ded_taxred == "D")
                               {
                                //adjust the employee YTD amount
                                   foreach (DataRow drtemp in Objemp_temp.Rows)
                                   {
                                       if (drtemp[0].ToString().Trim() == soc_sec_num)
                                       {
                                           drtemp[1] = Convert.ToDecimal(drtemp[1]) - Convert.ToDecimal(dr["amount"]);
                                           drtemp[2] = Convert.ToDecimal(drtemp[2])- zero_value;
                                           break;
                                       }
                                   }
                                   //get current total

                                   DataRow[] empdearrr = Objemp_temp.Select("soc_sec_num='" + soc_sec_num + "'");
                                   decimal emp_amount = 0;
                                   if (empdearrr.Length == 0)
                                   {
                                       emp_amount = Convert.ToDecimal(dr["amount"]) * -1;
                                       DataRow empdr = Objemp_temp.NewRow();
                                       empdr[0] = soc_sec_num;
                                       empdr[1] = emp_amount;
                                       empdr[2] = 0;
                                       Objemp_temp.Rows.Add(empdr);
                                   }
                                   else
                                   {
                                       emp_amount = Convert.ToDecimal(empdearrr[0][1]);
                                   }
                                   //adjust the fica wages only by amount relative to max fica
                                   if (emp_amount < Convert.ToDecimal(dr["fica_max"]) - Convert.ToDecimal(dr["amount"]))
                                   {
                                 
                                       dr["wages_fica"] = Convert.ToDecimal(dr["wages_fica"]) - Convert.ToDecimal(dr["amount"]);
                                   }
                                   else
                                   {// subtract partial wages
                                       if (emp_amount < Convert.ToDecimal(dr["fica_max"]))
                                       {
                                      
                                           dr["wages_fica"] = Convert.ToDecimal(dr["wages_fica"]) - (Convert.ToDecimal(dr["fica_max"]) - emp_amount);
                                       }
                                   }
                                   //adjust medicare wages only by amount relative to max mdcr
                                   if (emp_amount < Convert.ToDecimal(dr["medicare_max"]) - Convert.ToDecimal(dr["amount"]))
                                   {
                          
                                       dr["wages_medicare"] = Convert.ToDecimal(dr["wages_medicare"]) - Convert.ToDecimal(dr["amount"]);
                                   }
                                   else
                                   {// subtract partial wages
                                       if (emp_amount < Convert.ToDecimal(dr["medicare_max"]))
                                       {
                                          
                                           dr["wages_medicare"] = Convert.ToDecimal(dr["wages_medicare"]) - (Convert.ToDecimal(dr["medicare_max"]) - emp_amount);
                                       }
                                   }
                               }
                               //adjust taxable income accumulation
                               if(ded_taxred == "T" || ded_taxred == "C")
                               {
                                 //reduce taxable income
                                 
                                   dr["all_wages"] = Convert.ToDecimal(dr["all_wages"]) - Convert.ToDecimal(dr["amount"]);
                               }
                           }
                           else if (dr["act_type"].ToString() == "D")
                           {
                            //accumulate fica withholding
                               if (dr["act_code"].ToString() == dr["fica_ob_code"].ToString())
                               {
                         
                                   dr["withheld_fica"] = Convert.ToDecimal(dr["withheld_fica"]) + Convert.ToDecimal(dr["amount"]);
                                   day_withheld_fica = day_withheld_fica + Convert.ToDecimal(dr["amount"]);
                               }
                              //accumulate medicare withholding
                               if (dr["act_code"].ToString() == dr["medicare_ob_code"].ToString())
                               {
                                   dr["withheld_medicare"] = Convert.ToDecimal(dr["withheld_medicare"]) + Convert.ToDecimal(dr["amount"]);
                                   day_withheld_medicare = day_withheld_medicare + Convert.ToDecimal(dr["amount"]);
                               }
                           }
                           else
                           { 

                           }
                    }//end if  social security number
                    int tmp_day = Convert.ToDateTime(dr["pay_date"]).Day;
                    if (tmp_day == 1)
                    {
                        dr["day1_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 2)
                    {
                        dr["day2_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 3)
                    {
                        dr["day3_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 4)
                    {
                        dr["day4_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 5)
                    {
                        dr["day5_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 6)
                    {
                        dr["day6_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 7)
                    {
                        dr["day7_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 8)
                    {
                        dr["day8_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 9)
                    {
                        dr["day9_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 10)
                    {
                        dr["day10_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 11)
                    {
                        dr["day11_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 12)
                    {
                        dr["day12_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 13)
                    {
                        dr["day13_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 14)
                    {
                        dr["day14_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 15)
                    {
                        dr["day15_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 16)
                    {
                        dr["day16_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 17)
                    {
                        dr["day17_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 18)
                    {
                        dr["day18_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 19)
                    {
                        dr["day19_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 20)
                    {
                        dr["day20_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 21)
                    {
                        dr["day21_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 22)
                    {
                        dr["day22_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 23)
                    {
                        dr["day23_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 24)
                    {
                        dr["day24_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 25)
                    {
                        dr["day25_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 26)
                    {
                        dr["day26_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 27)
                    {
                        dr["day27_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 28)
                    {
                        dr["day28_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 29)
                    {
                        dr["day29_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    else if (tmp_day == 30)
                    {
                        dr["day30_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    } 
                    else if (tmp_day == 31)
                    {
                        dr["day31_total"] = day_fit_withheld + day_withheld_fica + day_withheld_medicare - day_eic_total;
                    }
                    #endregion

                    #region process on group m_period..
                    string[] period = m_period(Convert.ToDateTime(dr["pay_date"]));
                    dr["m_period"] = period[0].ToString();
                    dr["from_date"] = period[1].ToString();
                    dr["to_date"] = period[2].ToString();
                    DateTime next_paydate;
                    DataRow drn=null;
                    if (ds.Tables[0].Rows.Count != i + 1)
                    {
                        next_paydate = (ds.Tables[0].Rows[i+1]["pay_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i+1]["pay_date"]) : Convert.ToDateTime(null));
                    }
                    else
                    {
                        next_paydate = Convert.ToDateTime(null);
                    }

                    string[] next_period = m_period(Convert.ToDateTime(next_paydate));
                
                    if (period[0].Trim() != next_period[0].Trim())
                    { 

                       //calculate tax liability
                       dr["tax_liability"] =Convert.ToDecimal( dr["fit_withheld"]) +Convert.ToDecimal( dr["withheld_fica"]) +Convert.ToDecimal( dr["withheld_medicare"]) - eic_total;
                        // total accumulations
                       dr["fica_total"] = Convert.ToDecimal(dr["fica_total"]) + Convert.ToDecimal(dr["withheld_fica"]) + Convert.ToDecimal(dr["withheld_medicare"]);
                       dr["socsec_total"] = Convert.ToDecimal(dr["socsec_total"]) + Convert.ToDecimal(dr["withheld_fica"]) ;
                       dr["medicare_total"] = Convert.ToDecimal(dr["medicare_total"]) + Convert.ToDecimal(dr["withheld_medicare"]);
                       dr["fedtax"] = Convert.ToDecimal(dr["fedtax"]) + Convert.ToDecimal(dr["fit_withheld"]);
                       dr["eic_total"] = Convert.ToDecimal(dr["eic_total"]) + eic_total;
                       dr["fica_wages"] = Convert.ToDecimal(dr["fica_wages"]) + Convert.ToDecimal(dr["wages_fica"]);
                       dr["medicare_wages"] = Convert.ToDecimal(dr["medicare_wages"]) + Convert.ToDecimal(dr["wages_medicare"]);
                       if (Convert.ToInt32(period[0]) == 1 || Convert.ToInt32(period[0]) == 4 || Convert.ToInt32(period[0]) == 7 || Convert.ToInt32(period[0]) == 10)
                       {
                           dr["month1_total"] = dr["tax_liability"];
                       }
                       else if (Convert.ToInt32(period[0]) == 2 || Convert.ToInt32(period[0]) == 5 || Convert.ToInt32(period[0]) == 8 || Convert.ToInt32(period[0]) == 11)
                       {
                           dr["month2_total"] = dr["tax_liability"];
                       }
                       else if (Convert.ToInt32(period[0]) == 3 || Convert.ToInt32(period[0]) == 6 || Convert.ToInt32(period[0]) == 9 || Convert.ToInt32(period[0]) == 12)
                       {
                           dr["month3_total"] = dr["tax_liability"];
                       }
                       drn = objDataTable.NewRow();
                       drn.ItemArray = dr.ItemArray;
                       objDataTable.Rows.Add(drn);
                       #region reset variabels
                       
                       day_fit_withheld = 0;
                       day_withheld_fica = 0;
                       day_withheld_medicare = 0;
                       day_eic_total = 0;
                        eic_total = 0;
                        dr["tax_liability"] = 0;
                        dr["eic_total"] = 0;
                        dr["withheld_fica"] = 0;
                        dr["withheld_medicare"] = 0;
                        dr["fit_withheld"] = 0;
                        dr["wages_fica"] = 0;
                        dr["wages_medicare"] = 0;
                        dr["day1_total"] = 0;
                        dr["day2_total"] = 0;
                        dr["day3_total"] = 0;
                        dr["day4_total"] = 0;
                        dr["day5_total"] = 0;
                        dr["day6_total"] = 0;
                        dr["day7_total"] = 0;
                        dr["day8_total"] = 0;
                        dr["day9_total"] = 0;
                        dr["day10_total"] = 0;
                        dr["day11_total"] = 0;
                        dr["day12_total"] = 0;
                        dr["day13_total"] = 0;
                        dr["day14_total"] = 0;
                        dr["day15_total"] = 0;
                        dr["day16_total"] = 0;
                        dr["day17_total"] = 0;
                        dr["day18_total"] = 0;
                        dr["day19_total"] = 0;
                        dr["day20_total"] = 0;
                        dr["day21_total"] = 0;
                        dr["day22_total"] = 0;
                        dr["day23_total"] = 0;
                        dr["day24_total"] = 0;
                        dr["day25_total"] = 0;
                        dr["day26_total"] = 0;
                        dr["day27_total"] = 0;
                        dr["day28_total"] = 0;
                        dr["day29_total"] = 0;
                        dr["day30_total"] = 0;
                        dr["day31_total"] = 0;
                        #endregion

                    }

                    #endregion

                    #region on last row..
                    if(ds.Tables[0].Rows.Count==i+1)
                    {   
                       dr["fica_due"]=Convert.ToDecimal(dr["fica_wages"])*Convert.ToDecimal(dr["fica_rate"]);
                       dr["medicare_due"]=Convert.ToDecimal(dr["medicare_wages"])*Convert.ToDecimal(dr["medicare_rate"]);
                       decimal fica_diff =Convert.ToDecimal(dr["socsec_total"])*Convert.ToDecimal(dr["fica_due"]);
                       decimal  medicare_diff=Convert.ToDecimal(dr["medicare_total"])*Convert.ToDecimal(dr["medicare_due"]);
                       dr["fica_adjust"] = fica_diff + medicare_diff;
                       dr["adjust_fica"] = Convert.ToDecimal(dr["socsec_total"])+ Convert.ToDecimal(dr["medicare_total"]);
                       dr["taxes_total"] = Convert.ToDecimal(dr["fedtax"])+ Convert.ToDecimal(dr["medicare_total"]);
                       dr["taxes_net"] = Convert.ToDecimal(dr["taxes_total"])- Convert.ToDecimal(dr["eic_total"]);
                       dr["tax_balance"] = Convert.ToDecimal(dr["taxes_net"])- Convert.ToDecimal(dr["fed_deposits"]);
                       if (Convert.ToDecimal(dr["tax_balance"])<0)
                       {
                        dr["over_payed"] = Convert.ToDecimal(dr["tax_balance"])*-1;
                       }
                       int sMonth= DateTime.ParseExact(start_date, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Month;
                       if (sMonth == 1)
                        {
                            DataRow[] empcount  = Objemp_temp.Select("qtr_amount > 0");
                            dr["empl_count"] = empcount.Length;
                        }
                        drn.ItemArray = dr.ItemArray;  
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

        public static void PrepareYtoDTable(ref DataTable Objemp_temp, string  year_date, string start_date, decimal zero_value)
        {
            object[] parameters = new object[2];
            parameters[0] = year_date;
            parameters[1] = start_date;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                DataSet empds = objDALBaseClass.GetData(ref parameters, typeof(DVOQuarterlyReport), (new DVOQuarterlyReport()).GET_EMP);
                string old_soc_sec_num = string.Empty;
                DataRow empdr = null;
                foreach (DataRow dr in empds.Tables[0].Rows)
                {
                    if (old_soc_sec_num != dr[5].ToString().Trim())//soc_sec_num
                    {
                        empdr = Objemp_temp.NewRow();
                        empdr[0] = dr[5].ToString().Trim();
                        empdr[1] = 0;
                        empdr[2] = 0;
                        Objemp_temp.Rows.Add(empdr);
                        old_soc_sec_num = dr[5].ToString().Trim();
                    }
                    if (dr[2].ToString().Trim() == "B")//v_act_type
                    {
                        empdr[0] = 0;
                        //process the income
                        //get income detail information
                        string inc_type = string.Empty;
                        string inccr_sql = "select * from MasterIncCodes where MasterIncCodes.inc_code ='" + dr[1].ToString().Trim() + "'";
                        DataSet inccr_ds = objDALBaseClass.GetData(inccr_sql);
                        if(inccr_ds.Tables[0].Rows.Count!=0)
                        inc_type = (inccr_ds.Tables[0].Rows[0][7] != DBNull.Value ? inccr_ds.Tables[0].Rows[0][7].ToString().Trim() : string.Empty);

                        //accumulate fica wages
                        if (!(inc_type == "E" || inc_type == "A" || inc_type == "F" || inc_type == "B"))
                        {
                            empdr[1] = Convert.ToDecimal(empdr[1]) + Convert.ToDecimal(dr[3]);
                            empdr[2] = Convert.ToDecimal(empdr[2]) + zero_value;
                        }

                    }
                    else if (dr[2].ToString().Trim() == "C")//v_act_type
                    {
                        //adjust fica income accumulation
                        //get deduction detail information
                        string ded_taxred = string.Empty;
                        string dedcr_sql = "select * from MasterDedcodes where MasterDedcodes.ded_code ='" + dr[1].ToString().Trim() + "'";
                        DataSet dedcr_ds = objDALBaseClass.GetData(dedcr_sql);
                        if(dedcr_ds.Tables[0].Rows.Count!=0)
                        ded_taxred = (dedcr_ds.Tables[0].Rows[0][3] != DBNull.Value ? dedcr_ds.Tables[0].Rows[0][3].ToString().Trim() : string.Empty);
                        if (ded_taxred == "A" || ded_taxred == "F" || ded_taxred == "B" || ded_taxred == "D")
                        {
                            //adjust the employee YTD amount
                            empdr[1] = Convert.ToDecimal(empdr[1]) - Convert.ToDecimal(dr[3]);
                            empdr[2] = Convert.ToDecimal(empdr[2]) - zero_value;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string[] m_period(DateTime date)
        {
            string[] str = new string[3];
            string s_date =string.Empty;
            string e_date = string.Empty;
            int this_month = 0;
            if (date==Convert.ToDateTime(null))
            {
                str[0] = "-1";
                str[1] = s_date;
                str[2] = e_date;
                return str;
            }
            //initialize the month/day values
            this_month = date.Month;
            s_date =this_month.ToString()+"/"+ "01/" + date.Year.ToString();
            if (this_month == 2)
            {
                if (date.Year / 4 == 0)
                {
                    e_date = this_month.ToString() + "/" + "29/" + date.Year.ToString();
                }
                else
                {
                    e_date = this_month.ToString() + "/" + "28/" + date.Year.ToString();
                }
            }
            else if (this_month == 4 || this_month == 6 || this_month == 9 || this_month == 11)
            {
                e_date = this_month.ToString() + "/" + "30/" + date.Year.ToString();
            }
            else
            {
              e_date = this_month.ToString() + "/" + "28/" + date.Year.ToString();
            }
            str[0] = this_month.ToString();
            str[1] = s_date;
            str[2] = e_date;
            return str;
        }

        public static string[] QtrInput(string qtrdate)
        {
          string[] str = new string[3];
          string start_date =string.Empty;
          string end_date=string.Empty;
          if (qtrdate.Trim().Length<=0)
              qtrdate = DVOApplicationUserInfo.CurrentDate.AddDays(-80).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
          
          //set the year date
          int qtr_year =DateTime.ParseExact(qtrdate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Year;
          
          string year_date = "01/01/"+qtr_year.ToString();
         //set the quarter start and end dates
          int qtr_month = DateTime.ParseExact(qtrdate, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Month;
          if (qtr_month==1 || qtr_month==2 || qtr_month==3 )
           {
               start_date = "01/01/" + qtr_year.ToString();
               end_date = "03/31/" + qtr_year.ToString();
           }
          else if (qtr_month == 4 || qtr_month ==5 || qtr_month ==6)
          {
              start_date = "04/01/" + qtr_year.ToString();
              end_date = "06/30/" + qtr_year.ToString();
          }
          else if (qtr_month == 7 || qtr_month == 8 || qtr_month == 9)
          {
              start_date = "07/01/" + qtr_year.ToString();
              end_date = "09/30/" + qtr_year.ToString();
          }
          else if (qtr_month == 10 || qtr_month == 11 || qtr_month == 12)
          {
              start_date = "10/01/" + qtr_year.ToString();
              end_date = "12/31/" + qtr_year.ToString();
          }
          str[0] = year_date;
          str[1] = start_date;
          str[2] = end_date;
          return str;
        }
    }
}
