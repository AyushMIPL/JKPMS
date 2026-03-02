using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLUpdPayDefault
    {
        /// <summary>
        /// This method is use to get Payroll Defaults from database 
        /// </summary>
        /// <param name="objPurchaseClasses">reference of DVOUpdatePayDefaults type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having payroll defaults information</returns>
        public static List<DVOUpdatePayDefaults> GetPayrollDefaults(ref DVOUpdatePayDefaults objPayDefaults)
        {
            object[] parameters = new object[0];
            List<DVOUpdatePayDefaults> objPayDefaultslist = new List<DVOUpdatePayDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdatePayDefaults)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayDefaults objUpdPayDefaults = new DVOUpdatePayDefaults();
                    if(dr[0]!=DBNull.Value) 
                    objUpdPayDefaults.post_gl = dr[0].ToString().Trim();//post_gl
                if (dr[1] != DBNull.Value)
                    objUpdPayDefaults.ein_number = dr[1].ToString().Trim();//ein_number
                if (dr[2] != DBNull.Value)
                    objUpdPayDefaults.state_number = dr[2].ToString().Trim();//state_number
                if (dr[3] != DBNull.Value)
                    objUpdPayDefaults.fedtax_code = dr[3].ToString().Trim();//fedtax_code
                if (dr[4] != DBNull.Value)
                    objUpdPayDefaults.fica_code = dr[4].ToString().Trim();//fica_code
                if (dr[5] != DBNull.Value)
                    objUpdPayDefaults.medicare_code = dr[5].ToString().Trim();//medicare_code
                if (dr[6] != DBNull.Value)
                    objUpdPayDefaults.statax_code = dr[6].ToString().Trim();//statax_code
                if (dr[7] != DBNull.Value)
                    objUpdPayDefaults.loctax_code = dr[7].ToString().Trim();//loctax_code
                if (dr[8] != DBNull.Value)
                    objUpdPayDefaults.futa_code = dr[8].ToString().Trim();//futa_code
                if (dr[9] != DBNull.Value)
                    objUpdPayDefaults.fica_ob_code = dr[9].ToString().Trim();//fica_ob_code
                if (dr[10] != DBNull.Value)
                    objUpdPayDefaults.medicare_ob_code = dr[10].ToString().Trim();//medicare_ob_code
                if (dr[11] != DBNull.Value)
                    objUpdPayDefaults.eic_code = dr[11].ToString().Trim();//eic_code
                
                    objUpdPayDefaults.exp_acct = (dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0);//exp_acct
                
                    objUpdPayDefaults.liab_acct = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//liab_acct
            
                    objUpdPayDefaults.cash_acct = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);//cash_acct
                    if (dr[15] != DBNull.Value)
                    objUpdPayDefaults.mmedia_file = dr[15].ToString().Trim();//mmedia_file
                if (dr[16] != DBNull.Value)
                    objUpdPayDefaults.mmedia_command = dr[16].ToString().Trim();//mmedia_command
               
                    objUpdPayDefaults.py_doc_no = (dr[17] != DBNull.Value ? Convert.ToInt32(dr[17]) : 0);//py_doc_no
                
                    objUpdPayDefaults.py_post_no = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);//py_post_no
               
                    objUpdPayDefaults.immed_dest_dfi = (dr[19] != DBNull.Value ? Convert.ToInt32(dr[19]) : 0);//immed_dest_dfi
               
                    objUpdPayDefaults.immed_chk_digit = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);//immed_chk_digit
                    if (dr[21] != DBNull.Value)
                    objUpdPayDefaults.immed_dest_name = dr[21].ToString().Trim();//immed_dest_name
                if (dr[22] != DBNull.Value)
                    objUpdPayDefaults.co_bank_acct_no = dr[22].ToString().Trim();//co_bank_acct_no
                if (dr[23] != DBNull.Value)
                    objUpdPayDefaults.offset_debit = dr[23].ToString().Trim();//offset_debit
                if (dr[24] != DBNull.Value)
                    objUpdPayDefaults.set_up = dr[24].ToString().Trim();//set_up
                if (dr[25] != DBNull.Value)
                    objUpdPayDefaults.suta_code = dr[25].ToString().Trim();//suta_code
                if (dr[26] != DBNull.Value)
                    objUpdPayDefaults.rowid = Convert.ToInt32(dr[26]);//rowid
                if (dr[27] != DBNull.Value)
                    objUpdPayDefaults.liab_keyvalue = dr[27].ToString().Trim();//liab_keyvalue
                    //Added By Rahul Jain on 31-03-2009 for Getting Account type 
                if (dr[28] != DBNull.Value)
                    objUpdPayDefaults.liab_acct_type = dr[28].ToString().Trim();//liab_acct_type;
                if (dr[29] != DBNull.Value)
                    objUpdPayDefaults.exp_acct_type = dr[29].ToString().Trim();//exp_acct_type;
                    //***********************************************************
                if (dr[30] != DBNull.Value)
                    objUpdPayDefaults.exp_keyvalue = dr[30].ToString().Trim();//exp_keyvalue
                if (dr[31] != DBNull.Value)
                    objUpdPayDefaults.cash_keyvalue = dr[31].ToString().Trim();//cash_keyvalue
                
                    objUpdPayDefaults.cash_acct_type = dr[32] != DBNull.Value ? dr[32].ToString().Trim() : string.Empty;
                    objUpdPayDefaults.liab_acct_desc = dr[33] != DBNull.Value ? dr[33].ToString().Trim() : string.Empty;
                    objUpdPayDefaults.exp_acct_desc = dr[34] != DBNull.Value ? dr[34].ToString().Trim() : string.Empty;
                    objUpdPayDefaults.cash_acct_desc = dr[35] != DBNull.Value ? dr[35].ToString().Trim() : string.Empty;
                   
                    objPayDefaultslist.Add(objUpdPayDefaults);

                }
            }
            return objPayDefaultslist;
        }

        public static List<DVOUpdatePayDefaults> GetAllPayrollDefaults()
        {
      try
      {

      
            object[] parameters = new object[0];
            List<DVOUpdatePayDefaults> objPayDefaultslist = new List<DVOUpdatePayDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOUpdatePayDefaults)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayDefaults objUpdPayDefaults = new DVOUpdatePayDefaults();
                    if (dr[0] != DBNull.Value)
                    {
                        objUpdPayDefaults.post_gl = dr[0].ToString().Trim();//post_gl
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        objUpdPayDefaults.ein_number = dr[1].ToString().Trim();//ein_number
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        objUpdPayDefaults.state_number = dr[2].ToString().Trim();//state_number
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        objUpdPayDefaults.fedtax_code = dr[3].ToString().Trim();//fedtax_code
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        objUpdPayDefaults.fica_code = dr[4].ToString().Trim();//fica_code
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        objUpdPayDefaults.medicare_code = dr[5].ToString().Trim();//medicare_code
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        objUpdPayDefaults.statax_code = dr[6].ToString().Trim();//statax_code
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        objUpdPayDefaults.loctax_code = dr[7].ToString().Trim();//loctax_code
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        objUpdPayDefaults.futa_code = dr[8].ToString().Trim();//futa_code
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        objUpdPayDefaults.fica_ob_code = dr[9].ToString().Trim();//fica_ob_code
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        objUpdPayDefaults.medicare_ob_code = dr[10].ToString().Trim();//medicare_ob_code
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        objUpdPayDefaults.eic_code = dr[11].ToString().Trim();//eic_code
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        objUpdPayDefaults.exp_acct = (dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0);//exp_acct
                    }
                    if (dr[13] != DBNull.Value)
                    {
                        objUpdPayDefaults.liab_acct = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//liab_acct
                    }
                    if (dr[14] != DBNull.Value)
                    {
                        objUpdPayDefaults.cash_acct = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);//cash_acct
                    }
                    if (dr[15] != DBNull.Value)
                    {
                        objUpdPayDefaults.mmedia_file = dr[15].ToString().Trim();//mmedia_file
                    }
                    if (dr[16] != DBNull.Value)
                    {
                        objUpdPayDefaults.mmedia_command = dr[16].ToString().Trim();//mmedia_command
                    }
                    objUpdPayDefaults.py_doc_no = (dr[17] != DBNull.Value ? Convert.ToInt32(dr[17]) : 0);//py_doc_no
                    objUpdPayDefaults.py_post_no = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);//py_post_no
                    objUpdPayDefaults.immed_dest_dfi = (dr[19] != DBNull.Value ? Convert.ToInt32(dr[19]) : 0);//immed_dest_dfi
                    objUpdPayDefaults.immed_chk_digit = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);//immed_chk_digit
                    if (dr[21] != DBNull.Value)
                    {
                        objUpdPayDefaults.immed_dest_name = dr[21].ToString().Trim();//immed_dest_name
                    }
                    if (dr[22] != DBNull.Value)
                    {
                        objUpdPayDefaults.co_bank_acct_no = dr[22].ToString().Trim();//co_bank_acct_no
                    }
                    if (dr[23] != DBNull.Value)
                    {
                        objUpdPayDefaults.offset_debit = dr[23].ToString().Trim();//offset_debit
                    }
                    if (dr[24] != DBNull.Value)
                    {
                        objUpdPayDefaults.set_up = dr[24].ToString().Trim();//set_up
                    }
                    if (dr[25] != DBNull.Value)
                    {
                        objUpdPayDefaults.suta_code = dr[25].ToString().Trim();//suta_code
                    }
                    if (dr[26] != DBNull.Value)
                    {
                        objUpdPayDefaults.rowid = Convert.ToInt32(dr[26]);//suta_code
                    }
                    if (dr[27] != DBNull.Value)
                    {
                        objUpdPayDefaults.liab_keyvalue = dr[27].ToString().Trim();//liab_keyvalue
                    }
                    ////Added By Rahul Jain on 31-03-2009 for Getting Account type 
                    //objUpdPayDefaults.liab_acct_type = dr[28].ToString().Trim();//liab_acct_type;
                    //objUpdPayDefaults.exp_acct_type = dr[29].ToString().Trim();//exp_acct_type;
                    ////***********************************************************
                    if (dr[28] != DBNull.Value)
                    {
                        objUpdPayDefaults.exp_keyvalue = dr[28].ToString().Trim();//liab_keyvalue
                    }
                    if (dr[29] != DBNull.Value)
                    {
                        objUpdPayDefaults.cash_keyvalue = dr[29].ToString().Trim();//cash_keyvalue
                    }
                    objPayDefaultslist.Add(objUpdPayDefaults);
                }
            }
            return objPayDefaultslist;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw;
      }
    }

        /// <summary>
        /// This method is use to update payroll defaults into database
        /// </summary>
        /// <param name="objPayDefaults">reference of DVOUpdatePayDefaults type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
        public static int UpdatePayDefaults(ref DVOUpdatePayDefaults objPayDefaults)
        {
            object[] parameters = new object[23];
            parameters[0] = objPayDefaults.rowid;
            parameters[1] = objPayDefaults.post_gl;
            parameters[2] = objPayDefaults.ein_number;
            parameters[3] = objPayDefaults.state_number;
            parameters[4] = objPayDefaults.fedtax_code;
            parameters[5] = objPayDefaults.fica_code;
            parameters[6] = objPayDefaults.medicare_code;
            parameters[7] = objPayDefaults.statax_code;
            parameters[8] = objPayDefaults.loctax_code;
            parameters[9] = objPayDefaults.futa_code;
            parameters[10] = objPayDefaults.fica_ob_code;
            parameters[11] = objPayDefaults.medicare_ob_code;
            parameters[12] = objPayDefaults.eic_code;
            parameters[13] = objPayDefaults.exp_acct;
            parameters[14] = objPayDefaults.liab_acct;
            parameters[15] = objPayDefaults.cash_keyvalue;
            parameters[16] = objPayDefaults.mmedia_file;
            parameters[17] = objPayDefaults.mmedia_command;
            parameters[18] = objPayDefaults.immed_dest_dfi;
            parameters[19] = objPayDefaults.immed_chk_digit;
            parameters[20] = objPayDefaults.immed_dest_name;
            parameters[21] = objPayDefaults.co_bank_acct_no;
            parameters[22] = objPayDefaults.offset_debit;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOUpdatePayDefaults));
            return c;           
        }
        public static List<DVOUpdatePayDefaults> GetPayrollDeduction(ref DVOUpdatePayDefaults objPayDefaults)        
        {
            object[] parameters = new object[0];
            List<DVOUpdatePayDefaults> objPayDefaultslist = new List<DVOUpdatePayDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdatePayDefaults),objPayDefaults.GET_Pay_Deduction))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayDefaults objUpdPayDefaults = new DVOUpdatePayDefaults();
                    objUpdPayDefaults.ded_code = dr[0].ToString().Trim();//ded_code
                    objUpdPayDefaults.description = dr[1].ToString().Trim();//description
                    objUpdPayDefaults.keyvalue = dr[2].ToString().Trim();//keyvalue
                    objPayDefaultslist.Add(objUpdPayDefaults);
                }
            }
            return objPayDefaultslist;
        }
        public static List<DVOUpdatePayDefaults> GetPayrollObligation(ref DVOUpdatePayDefaults objPayDefaults)
        {
            object[] parameters = new object[0];
            List<DVOUpdatePayDefaults> objPayDefaultslist = new List<DVOUpdatePayDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdatePayDefaults), objPayDefaults.GET_Pay_Obligation))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayDefaults objUpdPayDefaults = new DVOUpdatePayDefaults();
                    objUpdPayDefaults.obl_code = dr[0].ToString().Trim();//obl_code
                    objUpdPayDefaults.description = dr[1].ToString().Trim();//description 
                    objUpdPayDefaults.acct_type = dr[2].ToString().Trim();//dfltaccounttype 
                    objUpdPayDefaults.keyvalue = dr[3].ToString().Trim();//keyvalue 

                    DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                    objDVOFlexSegCommon.EntityType = "MasterOblCodes";
                    objDVOFlexSegCommon.Code = dr[0].ToString().Trim();//obl_code
                    objDVOFlexSegCommon.AccountType = dr[2].ToString().Trim();//dfltaccounttype
                    string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

                    objUpdPayDefaults.keyvalue = _keyvalue.Trim();
                    objPayDefaultslist.Add(objUpdPayDefaults);
                }
            }
            return objPayDefaultslist;
        }
        public static List<DVOUpdatePayDefaults> Get_EIC_Income(ref DVOUpdatePayDefaults objPayDefaults)
        {
            object[] parameters = new object[0];
            List<DVOUpdatePayDefaults> objPayDefaultslist = new List<DVOUpdatePayDefaults>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdatePayDefaults), objPayDefaults.GET_eic_Income))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdatePayDefaults objUpdPayDefaults = new DVOUpdatePayDefaults();
                    objUpdPayDefaults.eic_code = dr[0].ToString().Trim();//eic_code
                    objUpdPayDefaults.description = dr[1].ToString().Trim();//description 
                    objUpdPayDefaults.acct_type = dr[2].ToString().Trim();//dfltaccounttype 
                    //objUpdPayDefaults.keyvalue = dr[3].ToString().Trim();//keyvalue 

                    DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
                    objDVOFlexSegCommon.EntityType = "MasterIncCodes";
                    objDVOFlexSegCommon.Code = dr[0].ToString().Trim();//eic_code
                    objDVOFlexSegCommon.AccountType = dr[2].ToString().Trim();//dfltaccounttype 
                    string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
                    objUpdPayDefaults.keyvalue = _keyvalue.Trim();
                    objPayDefaultslist.Add(objUpdPayDefaults);
                }
            }
            return objPayDefaultslist;
        }

        public static object UpdatePayDefaultsInfo(ref object objTransaction, ref DVOUpdatePayDefaults objPayDefaults)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                object[] parameters = new object[23];
                parameters[0] = objPayDefaults.rowid;
                parameters[1] = objPayDefaults.post_gl;
                parameters[2] = objPayDefaults.ein_number;
                parameters[3] = objPayDefaults.state_number;
                parameters[4] = objPayDefaults.fedtax_code;
                parameters[5] = objPayDefaults.fica_code;
                parameters[6] = objPayDefaults.medicare_code;
                parameters[7] = objPayDefaults.statax_code;
                parameters[8] = objPayDefaults.loctax_code;
                parameters[9] = objPayDefaults.futa_code;
                parameters[10] = objPayDefaults.fica_ob_code;
                parameters[11] = objPayDefaults.medicare_ob_code;
                parameters[12] = objPayDefaults.eic_code;
                parameters[13] = objPayDefaults.exp_acct;
                parameters[14] = objPayDefaults.liab_acct;
                parameters[15] = objPayDefaults.cash_keyvalue;
                parameters[16] = objPayDefaults.mmedia_file;
                parameters[17] = objPayDefaults.mmedia_command;
                parameters[18] = objPayDefaults.immed_dest_dfi;
                parameters[19] = objPayDefaults.immed_chk_digit;
                parameters[20] = objPayDefaults.immed_dest_name;
                parameters[21] = objPayDefaults.co_bank_acct_no;
                parameters[22] = objPayDefaults.offset_debit;


                //int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOUpdatePayDefaults));

                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUpdatePayDefaults ), true);

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return obj;
                }
                else
                    throw ex;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return obj;

        }
        
        public static void UpdatePayDefaults_PyDocNo(ref object objTransaction, ref DVOUpdatePayDefaults objPayDefaults)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objPayDefaults.rowid;
                parameters[1] = objPayDefaults.py_doc_no;

                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUpdatePayDefaults), objPayDefaults.UPDATE_PY_DOC_NO);
                if (obj == null || obj.ToString().Trim().Length <= 0 || Convert.ToInt32(obj) <= 0)
                    throw new Exception("Error occured during updation of py_doc_no in 'stycntrc'");

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            //return obj;
        }
        
        /// <summary>
        /// to Get Account's information based on assigned Keyvalue not use LIKE in procedure
        /// </summary>
        /// <param name="KeyValue">Keyvalue for which you want to get information</param>
        /// <param name="AccountNumber">out parameter to get Account Number</param>
        /// <param name="AccountType">out parameter to get AccountType of account</param>
        /// <param name="AccountTypeId">out parameter to get AccountTypeId of account</param>
        /// <param name="AccountDescription">out parameter to get Description of account</param>
        public static void GetAccountInformation(string Keyvalue, out int AccountNumber, out string AccountType, out int AccountTypeId, out string AccountDescription)
        {
            //set default values for out parameters
            AccountNumber = 0;
            AccountType = string.Empty;
            AccountTypeId = 0;
            AccountDescription = string.Empty;

            if (Keyvalue.Trim() != string.Empty)
            {
                //make object to send as parameter 
                DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
                //set keyvalue of object for which you want to get information
                objDVOGeneralLedger.keyvalue = Keyvalue;
                //call BLL class function to get information
                objDVOGeneralLedger = ((List<DVOGeneralLedger>)BLLUpdPayDefault.GetLedgerAccounts(objDVOGeneralLedger)).Count > 0 ? ((List<DVOGeneralLedger>)BLLUpdPayDefault.GetLedgerAccounts(objDVOGeneralLedger))[0] : null;

                if (objDVOGeneralLedger != null)
                {
                    //set variables with information 
                    AccountNumber = objDVOGeneralLedger.acct_no;
                    AccountType = objDVOGeneralLedger.acct_type;
                    AccountTypeId = objDVOGeneralLedger.acct_type_id;
                    AccountDescription = objDVOGeneralLedger.acct_desc;

                    objDVOGeneralLedger = null;
                }
            }
        }

        public static List<DVOGeneralLedger> GetLedgerAccounts(DVOGeneralLedger pDVOGeneralLedger)
        {
            object[] parameters = new object[1];
            parameters[0] = pDVOGeneralLedger.keyvalue;
           
            List<DVOGeneralLedger> lstDVOGeneralLedger = new List<DVOGeneralLedger>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGeneralLedger),pDVOGeneralLedger.GetAccountInfo))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
                        objDVOGeneralLedger.acct_no = Convert.ToInt32(dr[0]);//"acct_no"
                        objDVOGeneralLedger.acct_desc = Convert.ToString(dr[1]);//"acct_desc"
                        lstDVOGeneralLedger.Add(objDVOGeneralLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Exception to be handled
            }
            return lstDVOGeneralLedger;
        }
        //********************************************************************************************

        public static object InsertPayrollDefaultInfo(ref DVOUpdatePayDefaults objPayDefaultsIns)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();           
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            try
            {
                object[] parameters = new object[22];
                parameters[0] = objPayDefaultsIns.post_gl;
                parameters[1] = objPayDefaultsIns.ein_number;
                parameters[2] = objPayDefaultsIns.state_number;
                parameters[3] = objPayDefaultsIns.fedtax_code;
                parameters[4] = objPayDefaultsIns.fica_code;
                parameters[5] = objPayDefaultsIns.medicare_code;
                parameters[6] = objPayDefaultsIns.statax_code;
                parameters[7] = objPayDefaultsIns.loctax_code;
                parameters[8] = objPayDefaultsIns.futa_code;
                parameters[9] = objPayDefaultsIns.fica_ob_code;
                parameters[10] = objPayDefaultsIns.medicare_ob_code;
                parameters[11] = objPayDefaultsIns.eic_code;
                parameters[12] = objPayDefaultsIns.exp_acct;
                parameters[13] = objPayDefaultsIns.liab_acct;
                parameters[14] = objPayDefaultsIns.cash_keyvalue;
                parameters[15] = objPayDefaultsIns.mmedia_file;
                parameters[16] = objPayDefaultsIns.mmedia_command;
                parameters[17] = objPayDefaultsIns.immed_dest_dfi;
                parameters[18] = objPayDefaultsIns.immed_chk_digit;
                parameters[19] = objPayDefaultsIns.immed_dest_name;
                parameters[20] = objPayDefaultsIns.co_bank_acct_no;
                parameters[21] = objPayDefaultsIns.offset_debit;


                //int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOUpdatePayDefaults));

                obj = objDALBaseClass.InsertData(ref parameters, typeof(DVOUpdatePayDefaults),true);//.InsertData(ref parameters, typeof(DVOUpdatePayDefaults), true);

                parameters = null;
                objDALBaseClass = null;
                return obj;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return obj;
            
        }
    }
}

