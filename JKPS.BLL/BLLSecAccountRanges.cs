using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;


namespace JKPS.BLL
{
    public class BLLSecAccountRanges
    {
        /// <summary>
        /// This method is use to get Account number Ranges information from database 
        /// </summary>
        /// <param name="objSecAccountRanges">reference of DVOSecAccountRanges type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having Account number ranges information</returns>
        public static List<DVOSecAccountRanges> GetAccountRanges(ref DVOSecAccountRanges objSecAccountRanges)//int RoleId, string RoleName
        {
            object[] parameters = new object[0];            

            List<DVOSecAccountRanges> objSecAccountRangesList = new List<DVOSecAccountRanges>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOSecAccountRanges)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOSecAccountRanges objAccountRanges = new DVOSecAccountRanges();
                    objAccountRanges.rowid =(dr[0]!=DBNull.Value ?  Convert.ToInt32(dr[0]):0);//rowid
                    objAccountRanges.curr_asset = (dr[9]!=DBNull.Value ? Convert.ToInt32(dr[9]):0);//curr_asset
                    objAccountRanges.d_curr_asset =(dr[10]!=DBNull.Value ?  dr[10].ToString().Trim():string.Empty);//d_curr_asset
                    objAccountRanges.fixed_asset = (dr[11]!=DBNull.Value ? Convert.ToInt32(dr[11]):0);//fixed_asset
                    objAccountRanges.d_fixed_asset =(dr[12]!=DBNull.Value ? dr[12].ToString().Trim():string.Empty);//d_fixed_asset
                    objAccountRanges.curr_liab = (dr[13]!=DBNull.Value ? Convert.ToInt32(dr[13]):0);//curr_liab
                    objAccountRanges.d_curr_liab =(dr[14]!=DBNull.Value ?  dr[14].ToString().Trim():string.Empty);//d_curr_liab
                    objAccountRanges.long_term_liab =(dr[15]!=DBNull.Value ? Convert.ToInt32(dr[15]):0);//long_term_liab
                    objAccountRanges.d_long_term_liab =(dr[16]!=DBNull.Value ?dr[16].ToString().Trim():string.Empty);//d_lng_trm_liab
                    objAccountRanges.capital = (dr[17]!=DBNull.Value ? Convert.ToInt32(dr[17]):0);//capital
                    objAccountRanges.d_capital = (dr[18]!=DBNull.Value ? dr[18].ToString().Trim():string.Empty);//d_capital
                    objAccountRanges.income = (dr[19]!=DBNull.Value ? Convert.ToInt32(dr[19]):0);//income
                    objAccountRanges.d_income =(dr[20]!=DBNull.Value ? dr[20].ToString().Trim():string.Empty);//d_income
                    objAccountRanges.cost_goods =(dr[21]!=DBNull.Value ? Convert.ToInt32(dr[21]):0);//cost_goods
                    objAccountRanges.d_cost_goods = (dr[22]!=DBNull.Value ? dr[22].ToString().Trim():string.Empty);//d_cost_goods
                    objAccountRanges.expense = (dr[23]!=DBNull.Value ?Convert.ToInt32(dr[23]):0);//expense
                    objAccountRanges.d_expense = (dr[24]!=DBNull.Value ? dr[24].ToString().Trim():string.Empty); //d_expense                   
                    objSecAccountRangesList.Add(objAccountRanges);
                }
            }
            return objSecAccountRangesList;
        }
       //Commented by Sunil Pahwa
        /// <summary>
        /// This method is use to update role information into database
        /// </summary>
        /// <param name="objSecRole">reference of DVOSecAccountRanges type object as a collection of parameters</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        //public static int UpdateAccountRanges(ref DVOSecAccountRanges objSecAccountRanges)
        //{
        //    try
        //    {
        //        object[] parameters = new object[17];
        //        parameters[0] = objSecAccountRanges.rowid;
        //        parameters[1] = objSecAccountRanges.curr_asset;
        //        parameters[2] = objSecAccountRanges.d_curr_asset;
        //        parameters[3] = objSecAccountRanges.fixed_asset;
        //        parameters[4] = objSecAccountRanges.d_fixed_asset;
        //        parameters[5] = objSecAccountRanges.curr_liab;
        //        parameters[6] = objSecAccountRanges.d_curr_liab;
        //        parameters[7] = objSecAccountRanges.long_term_liab;
        //        parameters[8] = objSecAccountRanges.d_long_term_liab;
        //        parameters[9] = objSecAccountRanges.capital;
        //        parameters[10] = objSecAccountRanges.d_capital;
        //        parameters[11] = objSecAccountRanges.income;
        //        parameters[12] = objSecAccountRanges.d_income;
        //        parameters[13] = objSecAccountRanges.cost_goods;
        //        parameters[14] = objSecAccountRanges.d_cost_goods;
        //        parameters[15] = objSecAccountRanges.expense;
        //        parameters[16] = objSecAccountRanges.d_expense;
        //        List<DVOSecAccountRanges> objSecAccountRangesList = new List<DVOSecAccountRanges>();
        //        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //        int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOSecAccountRanges));
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
 
        //    }
        //    return 0;
        //}


        public static object UpdateAccountRangesInfo(ref object objTransaction, ref DVOSecAccountRanges objDVOSecAccountRangesUPD)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object success = null;
            try
            {
                object[] parameters = new object[17];
                parameters[0] = objDVOSecAccountRangesUPD.rowid;
                parameters[1] = objDVOSecAccountRangesUPD.curr_asset;
                parameters[2] = objDVOSecAccountRangesUPD.d_curr_asset;
                parameters[3] = objDVOSecAccountRangesUPD.fixed_asset;
                parameters[4] = objDVOSecAccountRangesUPD.d_fixed_asset;
                parameters[5] = objDVOSecAccountRangesUPD.curr_liab;
                parameters[6] = objDVOSecAccountRangesUPD.d_curr_liab;
                parameters[7] = objDVOSecAccountRangesUPD.long_term_liab;
                parameters[8] = objDVOSecAccountRangesUPD.d_long_term_liab;
                parameters[9] = objDVOSecAccountRangesUPD.capital;
                parameters[10] = objDVOSecAccountRangesUPD.d_capital;
                parameters[11] = objDVOSecAccountRangesUPD.income;
                parameters[12] = objDVOSecAccountRangesUPD.d_income;
                parameters[13] = objDVOSecAccountRangesUPD.cost_goods;
                parameters[14] = objDVOSecAccountRangesUPD.d_cost_goods;
                parameters[15] = objDVOSecAccountRangesUPD.expense;
                parameters[16] = objDVOSecAccountRangesUPD.d_expense;
                List<DVOSecAccountRanges> objSecAccountRangesList = new List<DVOSecAccountRanges>();
              
                success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecAccountRanges), true);
                if (success == null)
                    throw new Exception("Error in Updation");
                else if (Convert.ToInt32(success) < 1)
                    throw new Exception("Error in Updation");
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                objDVOSecAccountRangesUPD = null;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }

        public static object InsertAccountRangesInfo(ref DVOSecAccountRanges objDVOSecAccountRangesINS)
        {
            object success = null;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                object objTransaction = null;
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                try
                {
                    object[] parameters = new object[16];
                    parameters[0] = objDVOSecAccountRangesINS.curr_asset;
                    parameters[1] = objDVOSecAccountRangesINS.d_curr_asset;
                    parameters[2] = objDVOSecAccountRangesINS.fixed_asset;
                    parameters[3] = objDVOSecAccountRangesINS.d_fixed_asset;
                    parameters[4] = objDVOSecAccountRangesINS.curr_liab;
                    parameters[5] = objDVOSecAccountRangesINS.d_curr_liab;
                    parameters[6] = objDVOSecAccountRangesINS.long_term_liab;
                    parameters[7] = objDVOSecAccountRangesINS.d_long_term_liab;
                    parameters[8] = objDVOSecAccountRangesINS.capital;
                    parameters[9] = objDVOSecAccountRangesINS.d_capital;
                    parameters[10] = objDVOSecAccountRangesINS.income;
                    parameters[11] = objDVOSecAccountRangesINS.d_income;
                    parameters[12] = objDVOSecAccountRangesINS.cost_goods;
                    parameters[13] = objDVOSecAccountRangesINS.d_cost_goods;
                    parameters[14] = objDVOSecAccountRangesINS.expense;
                    parameters[15] = objDVOSecAccountRangesINS.d_expense;
                    List<DVOSecAccountRanges> objSecAccountRangesList = new List<DVOSecAccountRanges>();

                    success = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSecAccountRanges), true);
                    if (success == null)
                        throw new Exception("Error in Updation");
                    else if (Convert.ToInt32(success) < 1)
                        throw new Exception("Error in Updation");
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    objDVOSecAccountRangesINS = null;
                }
                catch (Exception ex)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManagement.ExceptionManager.Publish(ex);

                    throw ex;
                }
            return success;

        }
    }
}
