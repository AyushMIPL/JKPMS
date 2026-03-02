using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
namespace JKPS.BLL
{
  public class BLLGLTrialBalance
    {
        public static List<DVOGLTrialBalance> GetAllTrialBalance()
        {
            List<DVOGLTrialBalance> objGLtrialBalanceList = new List<DVOGLTrialBalance>();
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet dsTrialBalance = objDalBaseClass.GetAllData(typeof(DVOGLTrialBalance));
                if (dsTrialBalance.Tables.Count > 0)
                    if (dsTrialBalance.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in dsTrialBalance.Tables[0].Rows)
                        {
                            DVOGLTrialBalance objGLTrialbalance = new DVOGLTrialBalance();
                            objGLTrialbalance.acct_cat = dr["acct_cat"].ToString().TrimEnd();
                            objGLTrialbalance.acct_desc = dr["acct_desc"].ToString().TrimEnd();
                            objGLTrialbalance.acct_no = Convert.ToInt32(dr["acct_no"]);
                            objGLTrialbalance.acct_type = dr["acct_type"].ToString().TrimEnd();
                            objGLTrialbalance.activity = Convert.ToDecimal(dr["activity"]);
                            objGLTrialbalance.balance = Convert.ToDecimal(dr["balance"]);
                            objGLTrialbalance.budget = Convert.ToDecimal(dr["budget"]);
                            objGLTrialbalance.department = dr["department"].ToString().TrimEnd();
                            objGLTrialbalance.incr_with_crdt = dr["incr_with_crdt"].ToString().TrimEnd();
                            objGLTrialbalance.keyvalue = dr["keyvalue"].ToString().TrimEnd();
                            objGLTrialbalance.period_month = dr["period_month"].ToString().TrimEnd();
                            objGLTrialbalance.period_year = dr["period_year"].ToString().TrimEnd();
                            objGLTrialbalance.processing_seq = dr["processing_seq"].ToString().TrimEnd();
                            objGLTrialbalance.subtotal_group = dr["subtotal_group"].ToString().TrimEnd();
                            objGLTrialbalance.this_month = Convert.ToDecimal(dr["this_month"]);

                            objGLtrialBalanceList.Add(objGLTrialbalance);
                        }
            }
            catch { }

            return objGLtrialBalanceList;
        }
        public static DataSet GetMonthEndTrialbalance(ref DVOGLTrialBalance pobjGLTrialBal)
        {
            List<DVOGLTrialBalance> objMonthTrialBalanceList = new List<DVOGLTrialBalance>();
       
           
                object[] parameters = new object[4];
                parameters[0] = pobjGLTrialBal.acct_type;
                parameters[1] = pobjGLTrialBal.period_month;
                parameters[2] = pobjGLTrialBal.period_year;
                parameters[3] = pobjGLTrialBal.keyvalue;


                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                 DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLTrialBalance));
                      
            return ds;
        }
      public static DataSet GetMonthEndSegmentDetails()
      {
                  object[] parameters = new object[0];
                  DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                  DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          DataSet ds = objDalBaseClass.GetAllData(typeof(DVOGLTrialBalance));
          ds.Tables[0].TableName = "Master_Segment";
          //Modified by sunil pahwa on 10/12/2008 
          ds.Tables[0].Columns[0].ColumnName = "segmentid";
          ds.Tables[0].Columns[1].ColumnName = "id";
          ds.Tables[0].Columns[2].ColumnName = "keyvalue"; 
          ds.Tables[0].Columns[3].ColumnName = "desc";
          ds.Tables[0].Columns[4].ColumnName = "printsafter";
          ds.Tables[0].Columns[5].ColumnName = "issubto";
          ds.Tables[0].Columns[6].ColumnName = "abbreviation";
          //--------------------------------------------------
          return ds;
      }
      public static DataSet GetTrialbalanceInfo(ref DVOGLTrialBalance pobjGLTrialBal)
      {
          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          DataSet ds = null;
          try
          {
              object[] parameters = new object[4];
              parameters[0] = pobjGLTrialBal.acct_type;
              parameters[1] = pobjGLTrialBal.period_month;
              parameters[2] = pobjGLTrialBal.period_year;
              parameters[3] = pobjGLTrialBal.keyvalue;

              ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLTrialBalance));
              ds.Tables[0].Columns.Add("asterisk");
              ds.Tables[0].Columns.Add("Level1");
              ds.Tables[0].Columns.Add("Level2");
              ds.Tables[0].Columns.Add("Level3");
              ds.Tables[0].Columns.Add("Level4");
              ds.Tables[0].Columns.Add("Level5");
              ds.Tables[0].Columns.Add("Level6");
              ds.Tables[0].Columns.Add("Level7");
              ds.Tables[0].Columns.Add("Level8");
              ds.Tables[0].Columns.Add("LevelDesc1");
              ds.Tables[0].Columns.Add("LevelDesc2");
              ds.Tables[0].Columns.Add("LevelDesc3");
              ds.Tables[0].Columns.Add("LevelDesc4");
              ds.Tables[0].Columns.Add("LevelDesc5");
              ds.Tables[0].Columns.Add("LevelDesc6");
              ds.Tables[0].Columns.Add("LevelDesc7");
              ds.Tables[0].Columns.Add("LevelDesc8");

              List<DVOFlexSegCommon> objListSegDesc = null;
              DataSet DSMaster_Segment = null;
              string oldAcctType = string.Empty;

              string CurrMonth, CurrYear;
              //CurrMonth = ReportingUtilities.GetCurr_periodstgcntrc();
              //CurrYear = ReportingUtilities.GetCurr_yearstgcntrc();
              CurrMonth = DVOApplicationUserInfo.CurPeriod;
              CurrYear = DVOApplicationUserInfo.CurYear;
              bool this_period = false;
              if (pobjGLTrialBal.period_month == CurrMonth && pobjGLTrialBal.period_year == CurrYear)
                  this_period = true;
              DataSet dsCnt = null;
              string dpt = "000";
              if (this_period)
              {
                  object[] cntparam = new object[3];
                  cntparam[0] = dpt;
                  cntparam[1] = pobjGLTrialBal.period_month;
                  cntparam[2] = pobjGLTrialBal.period_year;
                  dsCnt = objDalBaseClass.GetData(ref cntparam, typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).GET_COUNT);
                  dsCnt.Tables[0].Columns[0].ColumnName = "acct_no";
              }

              foreach (DataRow dr in ds.Tables[0].Rows)
              {
                  string keyvalue = Convert.ToString(dr["keyvalue"]).Trim();
                  string acctType = Convert.ToString(dr["acct_type"]).Trim();

                  DataTable dtlSegments = BLLCommonUtilities.MakeSegments(acctType, oldAcctType, keyvalue, ref objListSegDesc, ref DSMaster_Segment);
                  if (dtlSegments.Rows.Count > 0)
                  {
                      DataRow drSegments = dtlSegments.Rows[0];
                      dr["Level1"] = drSegments["Level1"];
                      dr["Level2"] = drSegments["Level2"];
                      dr["Level3"] = drSegments["Level3"];
                      dr["Level4"] = drSegments["Level4"];
                      dr["Level5"] = drSegments["Level5"];
                      dr["Level6"] = drSegments["Level6"];
                      dr["Level7"] = drSegments["Level7"];
                      dr["Level8"] = drSegments["Level8"];
                      dr["LevelDesc1"] = drSegments["LevelDesc1"];
                      dr["LevelDesc2"] = drSegments["LevelDesc2"];
                      dr["LevelDesc3"] = drSegments["LevelDesc3"];
                      dr["LevelDesc4"] = drSegments["LevelDesc4"];
                      dr["LevelDesc5"] = drSegments["LevelDesc5"];
                      dr["LevelDesc6"] = drSegments["LevelDesc6"];
                      dr["LevelDesc7"] = drSegments["LevelDesc7"];
                      dr["LevelDesc8"] = drSegments["LevelDesc8"];
                  }
                  oldAcctType = acctType;

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
                  }
                  else
                  {
                      decimal this_month = dr["this_month"] != DBNull.Value ? Convert.ToDecimal(dr["this_month"]) : 0;
                      if (this_month != 0)
                          dr["asterisk"] = "*";
                  }
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
