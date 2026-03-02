using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using System.Data;
using JKPS.COMMON;
namespace JKPS.BLL
{
    class BLLAccountbalancesstxchrtd
    {

        public static bool CalculateBalances(ref object objTrx)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                ds = objDALBaseClass.GetData(typeof(DVOGLstxchrtd), (new DVOGLstxchrtd()).Get_Account_details);
                ds.Tables[0].Columns[0].ColumnName = "acct_no";
                ds.Tables[0].Columns[1].ColumnName = "department";
                ds.Tables[0].Columns[2].ColumnName = "period_month";
                ds.Tables[0].Columns[3].ColumnName = "period_year";
                ds.Tables[0].Columns[4].ColumnName = "activity";
                ds.Tables[0].Columns[5].ColumnName = "balance";
                ds.Tables[0].Columns[6].ColumnName = "this_month";
                decimal running_balance = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DVOGLstxchrtd objDVOGLstxchrtd = new DVOGLstxchrtd();
                    DataRow dr = ds.Tables[0].Rows[i];
                    
                    objDVOGLstxchrtd.acct_no = (dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"].ToString()) : 0);
                    objDVOGLstxchrtd.activity = (dr["activity"] != DBNull.Value ? Convert.ToDecimal(dr["activity"].ToString()) : 0);
                    objDVOGLstxchrtd.balance = (dr["balance"] != DBNull.Value ? Convert.ToDecimal(dr["balance"].ToString()) : 0);
                    objDVOGLstxchrtd.department = dr["department"].ToString();
                    objDVOGLstxchrtd.period_month = dr["period_month"].ToString();
                    objDVOGLstxchrtd.incr_with_crdt = dr["incr_with_crdt"].ToString().Trim();

                    running_balance = objDVOGLstxchrtd.balance;
                    if (objDVOGLstxchrtd.activity != 0)
                    {
                        running_balance = running_balance - objDVOGLstxchrtd.activity;
                    }
                    if (objDVOGLstxchrtd.this_month != 0)
                    {
                        running_balance = running_balance - objDVOGLstxchrtd.this_month;

                    }

                    if (objDVOGLstxchrtd.activity != 0)
                    {
                        running_balance = running_balance - objDVOGLstxchrtd.activity;
                    }
                    if (objDVOGLstxchrtd.this_month != 0)
                    {
                        running_balance = running_balance - objDVOGLstxchrtd.this_month;

                    }
                    objDVOGLstxchrtd.balance = running_balance;
                    // update stxchrtd
                    object[] parameters = new object[5];
                    parameters[0] = objDVOGLstxchrtd.acct_no;
                    parameters[1] = objDVOGLstxchrtd.department.Trim();
                    parameters[2] = objDVOGLstxchrtd.period_month;
                    parameters[3] = objDVOGLstxchrtd.period_year;
                    parameters[4] = objDVOGLstxchrtd.balance;
                    object upd_status = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTrx, ref parameters, objDVOGLstxchrtd.UpdateBalanceRecalculated);
                    if (upd_status.ToString().Trim() != "1")
                    {
                        // An SQL error has occured while updating stxchrtd
                        return false;
                    }
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
