using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 17 August 2009
    /// Description :Business Logic Layer for Treasury Bill Schemes
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class BLLTBSchemes
    {
        /// <summary>
        /// This method is use to get Treasury Bill Schemes information from database based on class code
        /// </summary>
        /// <param name="objTreasSchemes">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having Treasury Schemes information</returns>
        public static List<DVOTBSchemes> GetSchemes(ref DVOTBSchemes objTreasSchemes)
        {
            object[] parameters = new object[17];
            parameters[0] = objTreasSchemes.Rowid;
            parameters[1] = objTreasSchemes.tbschname;
            parameters[2] = objTreasSchemes.tbschdesc;
            parameters[3] = objTreasSchemes.startdate;
            parameters[4] = objTreasSchemes.enddate;
            parameters[5] = objTreasSchemes.schdays;
            parameters[6] = objTreasSchemes.minimum_amt;
            parameters[7] = objTreasSchemes.maximum_amt;
            parameters[8] = objTreasSchemes.processstart;
            parameters[9] = objTreasSchemes.amt_per_100;
            parameters[10] = objTreasSchemes.maxmembers;
            parameters[11] = objTreasSchemes.status;
            parameters[12] = objTreasSchemes.tbschid;
            parameters[13] = objTreasSchemes.bank_acct_no;
            parameters[14] = objTreasSchemes.deposit_acct_no;
            parameters[15] = objTreasSchemes.interest_acct_no;
            parameters[16] = objTreasSchemes.pay_acct_no;

            List<DVOTBSchemes> objBillSchemesList = new List<DVOTBSchemes>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOTBSchemes)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOTBSchemes objBillSchemes = new DVOTBSchemes();
                    objBillSchemes.Rowid = (dr["rowid"] != DBNull.Value ? Convert.ToInt32(dr["rowid"]) : 0);
                    objBillSchemes.tbschid = (dr["tbschid"] != DBNull.Value ? Convert.ToInt32(dr["tbschid"]) : 0);
                    objBillSchemes.tbschname = (dr["tbschname"] != DBNull.Value ? dr["tbschname"].ToString().Trim() : string.Empty);//tbschname
                    objBillSchemes.tbschdesc = (dr["tbschdesc"] != DBNull.Value ? dr["tbschdesc"].ToString().Trim() : string.Empty);//tbschdesc
                    objBillSchemes.startdate = (dr["startdate"] != DBNull.Value ? dr["startdate"].ToString().Trim() : string.Empty);//startdate
                    objBillSchemes.enddate = (dr["schemeendson"] != DBNull.Value ? dr["schemeendson"].ToString().Trim() : string.Empty);//enddate
                    objBillSchemes.schdays = (dr["schdays"] != DBNull.Value ? Convert.ToInt32(dr["schdays"]) : 0);//schdays
                    objBillSchemes.minimum_amt = (dr["minimumamount"] != DBNull.Value ? Convert.ToDecimal(dr["minimumamount"]) : 0);//minimumamount
                    objBillSchemes.maximum_amt = (dr["maximumamount"] != DBNull.Value ? Convert.ToDecimal(dr["maximumamount"]) : 0);//maximum_amt
                    objBillSchemes.processstart = (dr["processstart"] != DBNull.Value ? dr["processstart"].ToString().Trim() : string.Empty);//processstart
                    objBillSchemes.amt_per_100 = (dr["amt_per_100"] != DBNull.Value ? Convert.ToDecimal(dr["amt_per_100"]) : 0);//amt_per_100
                    objBillSchemes.maxmembers = (dr["maxmembers"] != DBNull.Value ? Convert.ToInt32(dr["maxmembers"]) : 0);//maxmembers
                    objBillSchemes.status = (dr["status"] != DBNull.Value ? dr["status"].ToString().Trim() : string.Empty);//status
                    //account No's
                    objBillSchemes.bank_acct_no = (dr["bank_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["bank_acct_no"]) : 0);//bank_acct_no
                    objBillSchemes.deposit_acct_no = (dr["deposit_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["deposit_acct_no"]) : 0);//deposit_acct_no
                    objBillSchemes.interest_acct_no = (dr["interest_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["interest_acct_no"]) : 0);//interest_acct_no
                    objBillSchemes.pay_acct_no = (dr["pay_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["pay_acct_no"]) : 0);//pay_acct_no
                    //AcctTypes
                    objBillSchemes.bank_acct_no_accounttype = (dr["BankAcctType"] != DBNull.Value ? dr["BankAcctType"].ToString().Trim() : string.Empty);//BankAcctType
                    objBillSchemes.deposit_acct_no_accounttype = (dr["DepoAcctType"] != DBNull.Value ? dr["DepoAcctType"].ToString().Trim() : string.Empty);//DepoAcctType
                    objBillSchemes.interest_acct_no_accounttype = (dr["IntAcctType"] != DBNull.Value ? dr["IntAcctType"].ToString().Trim() : string.Empty);//IntAcctType
                    objBillSchemes.pay_acct_no_accounttype = (dr["PayAcctType"] != DBNull.Value ? dr["PayAcctType"].ToString().Trim() : string.Empty);//PayAcctType
                    //Keyvalues
                    objBillSchemes.bank_acct_no_keyvalue = (dr["Bankkeyvalue"] != DBNull.Value ? dr["Bankkeyvalue"].ToString().Trim() : string.Empty);//Bankkeyvalue
                    objBillSchemes.deposit_acct_no_keyvalue = (dr["Depokeyvalue"] != DBNull.Value ? dr["Depokeyvalue"].ToString().Trim() : string.Empty);//Depokeyvalue
                    objBillSchemes.interest_acct_no_keyvalue = (dr["Intkeyvalue"] != DBNull.Value ? dr["Intkeyvalue"].ToString().Trim() : string.Empty);//Intkeyvalue
                    objBillSchemes.pay_acct_no_keyvalue = (dr["Paykeyvalue"] != DBNull.Value ? dr["Paykeyvalue"].ToString().Trim() : string.Empty);//Paykeyvalue

                    objBillSchemesList.Add(objBillSchemes);
                }
            }
            return objBillSchemesList;
        }
        /// <summary>
        /// This method is use to insert new Treasury Bill Scheme into database
        /// </summary>
        /// <param name="objTreasuryBillTenders">A class object passed as parameter having parameter data</param>
        /// <returns>return an object for confirmation, either data is inserted or not</returns>
        public static int InsertTreasurySchemes(ref object objTransaction, ref DVOTBSchemes objTreasuryBillSchemes)
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
                object[] parameters = new object[17];
                parameters[0] = objTreasuryBillSchemes.tbschname;
                parameters[1] = objTreasuryBillSchemes.tbschdesc;
                parameters[2] = objTreasuryBillSchemes.startdate;
                parameters[3] = objTreasuryBillSchemes.enddate;
                parameters[4] = objTreasuryBillSchemes.schdays;
                parameters[5] = objTreasuryBillSchemes.minimum_amt;
                parameters[6] = objTreasuryBillSchemes.maximum_amt;
                parameters[7] = objTreasuryBillSchemes.amt_per_100;
                parameters[8] = objTreasuryBillSchemes.processstart;
                parameters[9] = objTreasuryBillSchemes.maxmembers;
                parameters[10] = objTreasuryBillSchemes.status;
                parameters[11] = objTreasuryBillSchemes.Insertby;
                parameters[12] = objTreasuryBillSchemes.InsertMachineInfo;
                parameters[13] = objTreasuryBillSchemes.bank_acct_no;
                parameters[14] = objTreasuryBillSchemes.deposit_acct_no;
                parameters[15] = objTreasuryBillSchemes.interest_acct_no;
                parameters[16] = objTreasuryBillSchemes.pay_acct_no;

                List<DVOTBSchemes> objDVOTreasuryBillSchemesList = new List<DVOTBSchemes>();

                object obj = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objTreasuryBillSchemes.INSERT_SPNAME);
                if (obj == null)
                    throw new Exception("Error occured insert data in tbschemes ");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured insert data in tbschemes ");

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

        /// <summary>
        /// This method is use to update Treasury Schemes information into database
        /// </summary>
        /// <param name="objTreasuryBillTenders">A class object passed as parameter having parameter data</param>
        /// <returns>return an object for confirmation, either data is updated or not</returns>      
        public static object UpdateTreasurySchemes(ref object objTransaction, ref DVOTBSchemes objTreasuryBillSchemes)
        {
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelperSecurity.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object obj = null;
            try
            {
                object[] parameters = new object[18];
                parameters[0] = objTreasuryBillSchemes.tbschid;
                parameters[1] = objTreasuryBillSchemes.tbschname;
                parameters[2] = objTreasuryBillSchemes.tbschdesc;
                parameters[3] = objTreasuryBillSchemes.startdate;
                parameters[4] = objTreasuryBillSchemes.enddate;
                parameters[5] = objTreasuryBillSchemes.schdays;
                parameters[6] = objTreasuryBillSchemes.minimum_amt;
                parameters[7] = objTreasuryBillSchemes.maximum_amt;
                parameters[8] = objTreasuryBillSchemes.amt_per_100;
                parameters[9] = objTreasuryBillSchemes.processstart;
                parameters[10] = objTreasuryBillSchemes.maxmembers;
                parameters[11] = objTreasuryBillSchemes.status;
                parameters[12] = objTreasuryBillSchemes.UpdateBy;
                parameters[13] = objTreasuryBillSchemes.UpdateMachineInfo;
                parameters[14] = objTreasuryBillSchemes.bank_acct_no;
                parameters[15] = objTreasuryBillSchemes.deposit_acct_no;
                parameters[16] = objTreasuryBillSchemes.interest_acct_no;
                parameters[17] = objTreasuryBillSchemes.pay_acct_no;

                List<DVOTBSchemes> objDVOTreasuryBillClassesList = new List<DVOTBSchemes>();
                DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
                obj = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOTBSchemes), true);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelperSecurity.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return obj;

        }

        public static List<DVOTBSchemes> GetOnLoad()
        {

            List<DVOTBSchemes> oblList = new List<DVOTBSchemes>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOTBSchemes)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //if (!Convert.IsDBNull(dr[0])) ob.empl_code = dr[0].ToString().Trim();

                    DVOTBSchemes obj = new DVOTBSchemes();
                    if (!Convert.IsDBNull(dr[0])) obj.tbschid = Convert.ToInt32(dr[0]);
                    if (!Convert.IsDBNull(dr[1])) obj.tbschname = dr[1].ToString().Trim();

                    oblList.Add(obj);

                }
            }

            return oblList;


        }

        public static int GetIssueBySchemes(ref DVOTBSchemes objTreasSchemes)
        {
            int count = 0;
            object[] parameters = new object[1];
            parameters[0] = objTreasSchemes.tbschid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(objTreasSchemes.FIND_ISSUE_BY_SCHEME(ref parameters));
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return count;
        }

        public static int DeleteScheme(ref object objTransaction, ref DVOTBSchemes objDVOTBSchemes)
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
                parameters[0] = objDVOTBSchemes.Rowid;
                object obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOTBSchemes), true);
                if (obj == null)
                    throw new Exception("Error occured in delete data from tbschemes");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured in delete data from tbschemes");

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
    }
}
