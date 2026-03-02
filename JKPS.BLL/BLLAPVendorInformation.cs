using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
   public class BLLAPVendorInformation
    {
        /// <summary>
        /// This method is use to get Vendorinformation from database 
        /// </summary>
        /// <param name="objPurchaseClasses">reference of DVOASUpdPurchaseClass type object as a collection of parameters of search criteria.</param>
        /// <returns>return a list having approval classes information</returns>
       public static List<DVOAPVendorInformation> GetVendorInformation(ref DVOAPVendorInformation SearchObjDvoAPVendorInformation)
        {
            object[] parameters = new object[35];
            parameters[0] = SearchObjDvoAPVendorInformation.vend_code;
            parameters[1] = SearchObjDvoAPVendorInformation.bus_name;
            parameters[2] = SearchObjDvoAPVendorInformation.contact;
            parameters[3] = SearchObjDvoAPVendorInformation.phone;
            parameters[4] = SearchObjDvoAPVendorInformation.address1;
            parameters[5] = SearchObjDvoAPVendorInformation.address2;
            parameters[6] = SearchObjDvoAPVendorInformation.city;
            parameters[7] = SearchObjDvoAPVendorInformation.state;
            parameters[8] = SearchObjDvoAPVendorInformation.zip;
            parameters[9] = SearchObjDvoAPVendorInformation.country;
            parameters[10] = Convert.ToString(SearchObjDvoAPVendorInformation.credit_limit);
            parameters[11] = SearchObjDvoAPVendorInformation.terms_code;
            parameters[12] = SearchObjDvoAPVendorInformation.act_grp;
            parameters[13] = SearchObjDvoAPVendorInformation.spec_billing; ;
            parameters[14] = SearchObjDvoAPVendorInformation.ap_acct_dflt.ToString();
            //parameters[15] =SearchObjDvoAPVendorInformation.ap_department_dflt ;
            parameters[15] = SearchObjDvoAPVendorInformation.last_pay_date;
            parameters[16] = SearchObjDvoAPVendorInformation.hold_pymnt;
            parameters[17] = SearchObjDvoAPVendorInformation.take_dscnt;
            parameters[18] = SearchObjDvoAPVendorInformation.acct_bal.ToString();
            parameters[19] = SearchObjDvoAPVendorInformation.on_acct_amt.ToString();
            //parameters[21] =SearchObjDvoAPVendorInformation.arch_bal.ToString();
            parameters[20] = SearchObjDvoAPVendorInformation.spec_shipping;
            parameters[21] = SearchObjDvoAPVendorInformation.taxable;
            parameters[22] = SearchObjDvoAPVendorInformation.bo_allowed;
            parameters[23] = SearchObjDvoAPVendorInformation.pay_method;
            parameters[24] = SearchObjDvoAPVendorInformation.buyer_code;
            parameters[25] = SearchObjDvoAPVendorInformation.trd_ds_code;
            parameters[26] = SearchObjDvoAPVendorInformation.eta_days.ToString();
            //parameters[29] =SearchObjDvoAPVendorInformation.st_tx_code ;
            //parameters[30] =SearchObjDvoAPVendorInformation.co_tx_code;
            //parameters[31] =SearchObjDvoAPVendorInformation.ci_tx_code;
            parameters[27] = SearchObjDvoAPVendorInformation.cash_acct_no.ToString();
            //parameters[33] =SearchObjDvoAPVendorInformation.cash_department;
            parameters[28] = SearchObjDvoAPVendorInformation.exp_acct_no.ToString();
            //parameters[35] =SearchObjDvoAPVendorInformation.exp_department;
            parameters[29] = SearchObjDvoAPVendorInformation.print_1099;
            parameters[30] = SearchObjDvoAPVendorInformation.federal_tax_id;
            parameters[31] = SearchObjDvoAPVendorInformation.currency_code;
            //parameters[39] =SearchObjDvoAPVendorInformation.acct_bal_date;
            //parameters[40] =SearchObjDvoAPVendorInformation.on_acct_date;
            //parameters[41] =SearchObjDvoAPVendorInformation.sdb_code;
            //parameters[42] =SearchObjDvoAPVendorInformation.vendor_rating.ToString();
            //parameters[43] =SearchObjDvoAPVendorInformation.fax_phone;
            //parameters[44] =SearchObjDvoAPVendorInformation.telex_no ;
            parameters[32] = SearchObjDvoAPVendorInformation.mtax_frght;
            parameters [33] = SearchObjDvoAPVendorInformation.mtax_misc;
            parameters[34] = SearchObjDvoAPVendorInformation.Rowid;
            List<DVOAPVendorInformation> objVendorInformationlist = new List<DVOAPVendorInformation>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters,(typeof(DVOAPVendorInformation))))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOAPVendorInformation objAPVendorInformation = new DVOAPVendorInformation();
                    objAPVendorInformation.vend_code = dr[0].ToString().Trim();//vend_code
                    objAPVendorInformation.bus_name = dr[1].ToString().Trim();//bus_name
                    objAPVendorInformation.contact = dr[2].ToString().Trim();//contact
                    objAPVendorInformation.phone = dr[3].ToString().Trim();//phone
                    objAPVendorInformation.address1 = dr[4].ToString().Trim();//address1
                    objAPVendorInformation.address2 = dr[5].ToString().Trim();//address2
                    objAPVendorInformation.city = dr[6].ToString().Trim();//city
                    objAPVendorInformation.state = dr[7].ToString().Trim();//state
                    objAPVendorInformation.zip = dr[8].ToString().Trim();//zip
                    objAPVendorInformation.country = dr[9].ToString().Trim();//country
                    if (dr[10].ToString()!=string.Empty)
                    {
                        objAPVendorInformation.credit_limit = Convert.ToDecimal(dr[10]);//credit_limit;
                    }
                    objAPVendorInformation.terms_code = dr[11].ToString().Trim();//terms_code
                    objAPVendorInformation.act_grp = dr[12].ToString().Trim();//act_grp
                    if (dr[13].ToString() != string.Empty)
                    {
                        objAPVendorInformation.spec_billing = dr[13].ToString().Trim();//spec_billing
                    }
                    if (dr[14].ToString() != string.Empty)
                    {
                        objAPVendorInformation.ap_acct_dflt = Convert.ToInt32(dr[14]);//ap_acct_dflt;
                    }
                    objAPVendorInformation.ap_department_dflt = dr[15].ToString().Trim();//ap_department_dflt
                    objAPVendorInformation.last_pay_date = dr[16].ToString().Trim();//last_pay_date
                    objAPVendorInformation.hold_pymnt = dr[17].ToString().Trim();//hold_pymnt
                    objAPVendorInformation.take_dscnt = dr[18].ToString().Trim();//take_dscnt
                    if (dr[19].ToString() != string.Empty)
                    {
                        objAPVendorInformation.acct_bal = Convert.ToDecimal(dr[19]);//acct_bal
                    }
                    if (dr[20].ToString() != string.Empty)
                    {
                        objAPVendorInformation.on_acct_amt = Convert.ToDecimal(dr[20]);//on_acct_amt
                    }
                    //objAPVendorInformation.arch_bal = dr[21].ToString();//Convert.ToSingle(arch_bal.Text);
                    objAPVendorInformation.spec_shipping = dr[22].ToString().Trim();//spec_shipping
                    objAPVendorInformation.taxable = dr[23].ToString().Trim();//taxable
                    objAPVendorInformation.bo_allowed = dr[24].ToString().Trim();//bo_allowed
                    objAPVendorInformation.pay_method = dr[25].ToString().Trim();//pay_method
                    objAPVendorInformation.buyer_code = dr[26].ToString().Trim();//buyer_code
                    objAPVendorInformation.trd_ds_code = dr[27].ToString().Trim();//trd_ds_code
                    if (dr[28].ToString() != string.Empty)
                    {
                        objAPVendorInformation.eta_days = Convert.ToInt32(dr[28]);//eta_days
                    }
                    //objAPVendorInformation.st_tx_code = dr[29].ToString();//st_tx_code
                    //objAPVendorInformation.co_tx_code = dr[30].ToString();//co_tx_code
                    //objAPVendorInformation.ci_tx_code = dr[31].ToString();//ci_tx_code
                    if (dr[32].ToString() != string.Empty)
                    {
                        objAPVendorInformation.cash_acct_no = dr[32].ToString();//cash_acct_no
                    }
                    objAPVendorInformation.cash_department = dr[33].ToString().Trim();//cash_department
                    if (dr[34].ToString() != string.Empty)
                    {
                        //objAPVendorInformation.exp_acct_no = Convert.ToInt32(dr[34]);//exp_acct_no
                    }
                    //objAPVendorInformation.exp_department = dr[35].ToString();//exp_department
                    objAPVendorInformation.print_1099 = dr[36].ToString().Trim();//print_1099
                    objAPVendorInformation.federal_tax_id = dr[37].ToString().Trim();//federal_tax_id
                    objAPVendorInformation.currency_code = dr[38].ToString().Trim();//currency_code
                    //objAPVendorInformation.acct_bal_date = dr[39].ToString();//acct_bal_date
                    //objAPVendorInformation.on_acct_date = dr[40].ToString();//on_acct_date
                    //objAPVendorInformation.sdb_code = dr[41].ToString();//sdb_code
                    //objAPVendorInformation.vendor_rating = dr[42].ToString();//Convert.ToInt32(vendor_rating.Text);
                    //objAPVendorInformation.fax_phone = dr[43].ToString();//fax_phone
                    //objAPVendorInformation.telex_no = dr[44].ToString();//telex_no
                    objAPVendorInformation.mtax_frght = dr[45].ToString().Trim();//mtax_frght
                    objAPVendorInformation.mtax_misc = dr[46].ToString().Trim();//mtax_misc
                    if (dr[47].ToString() != string.Empty)
                    {
                        objAPVendorInformation.ap_acct_keyvalue = dr[47].ToString().Trim();//ap_acct_dflt;
                        objAPVendorInformation.ap_acct_type = dr[50].ToString().Trim();//ap_acct_type; //Updated By Rahul Jain 6/2/2009 dr[50]
                    }
                    objAPVendorInformation.Rowid  = Convert.ToInt32(dr[48].ToString().Trim());//rowid
                    if (dr[49].ToString() != string.Empty)
                    {
                        objAPVendorInformation.cash_acct_keyvalue = dr[49].ToString().Trim();//cash_acct_keyvalue
                    }    
                
                    if (dr[51].ToString() != string.Empty)
                    {
                        objAPVendorInformation.exp_acct_no = dr[51].ToString().Trim();//exp_acct_no
                        objAPVendorInformation.exp_acct_type = dr[52].ToString().Trim();//exp_acct_type
                    }
                    
                    objVendorInformationlist.Add(objAPVendorInformation);
                }
            }
            return objVendorInformationlist;
        }

        /// <summary>
        /// This method is use to insert new vendor information into database
        /// </summary>
        /// <param name="objAPVendorInformation">reference of DVOAPVendorInformation type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
       public static int InsertVendorInformation(ref DVOAPVendorInformation objVendorInformation)
        {
            object[] parameters = new object[34];           
            parameters[0] = objVendorInformation.vend_code;
            parameters[1] = objVendorInformation.bus_name;
            parameters[2] = objVendorInformation.contact;
            parameters[3] = objVendorInformation.phone;
            parameters[4] = objVendorInformation.address1;
            parameters[5] = objVendorInformation.address2;
            parameters[6] = objVendorInformation.city;
            parameters[7] = objVendorInformation.state;
            parameters[8] = objVendorInformation.zip;
            parameters[9] = objVendorInformation.country;
            parameters[10] =objVendorInformation.credit_limit;
            parameters[11] = objVendorInformation.terms_code;
            parameters[12] = objVendorInformation.act_grp;
            parameters[13] = objVendorInformation.spec_billing; ;
            parameters[14] = objVendorInformation.ap_acct_dflt;
            //parameters[15] =objVendorInformation.ap_department_dflt ;
            parameters[15] = objVendorInformation.last_pay_date;
            parameters[16] = objVendorInformation.hold_pymnt;
            parameters[17] = objVendorInformation.take_dscnt;
            parameters[18] = objVendorInformation.acct_bal;
            parameters[19] = objVendorInformation.on_acct_amt;
            //parameters[21] =objVendorInformation.arch_bal.ToString();
            parameters[20] = objVendorInformation.spec_shipping;
            parameters[21] = objVendorInformation.taxable;
            parameters[22] = objVendorInformation.bo_allowed;
            parameters[23] = objVendorInformation.pay_method;
            parameters[24] = objVendorInformation.buyer_code;
            parameters[25] = objVendorInformation.trd_ds_code;
            parameters[26] = objVendorInformation.eta_days;
            //parameters[29] =objVendorInformation.st_tx_code ;
            //parameters[30] =objVendorInformation.co_tx_code;
            //parameters[31] =objVendorInformation.ci_tx_code;
            parameters[27] = objVendorInformation.cash_acct_no;
            //parameters[33] =objVendorInformation.cash_department;
            parameters[28] = objVendorInformation.exp_acct_no;
            //parameters[35] =objVendorInformation.exp_department;
            parameters[29] = objVendorInformation.print_1099;
            parameters[30] = objVendorInformation.federal_tax_id;
            parameters[31] = objVendorInformation.currency_code;
            //parameters[39] =objVendorInformation.acct_bal_date;
            //parameters[40] =objVendorInformation.on_acct_date;
            //parameters[41] =objVendorInformation.sdb_code;
            //parameters[42] =objVendorInformation.vendor_rating.ToString();
            //parameters[43] =objVendorInformation.fax_phone;
            //parameters[44] =objVendorInformation.telex_no ;
            parameters[32] = objVendorInformation.mtax_frght;
            parameters[33] = objVendorInformation.mtax_misc;
            List<DVOAPVendorInformation> objAPVendorInformation = new List<DVOAPVendorInformation>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOAPVendorInformation));           
            return c;

        }

       //************************Commented BY Sunil Pahwa*******************************
        /// <summary>
        /// This method is use to update vendor information into database
        /// </summary>
        /// <param name="objAPVendorInformation">reference of DVOAPVendorInformation type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
       //public static int UpdateVendorInformation(ref DVOAPVendorInformation objVendorInformation)
       // {
       //     object[] parameters = new object[34];
       //     parameters[0] = objVendorInformation.vend_code;
       //     parameters[1] = objVendorInformation.bus_name;
       //     parameters[2] = objVendorInformation.contact;
       //     parameters[3] = objVendorInformation.phone;
       //     parameters[4] = objVendorInformation.address1;
       //     parameters[5] = objVendorInformation.address2;
       //     parameters[6] = objVendorInformation.city;
       //     parameters[7] = objVendorInformation.state;
       //     parameters[8] = objVendorInformation.zip;
       //     parameters[9] = objVendorInformation.country;
       //     parameters[10] = objVendorInformation.credit_limit;
       //     parameters[11] = objVendorInformation.terms_code;
       //     parameters[12] = objVendorInformation.act_grp;
       //     parameters[13] = objVendorInformation.spec_billing; ;
       //     parameters[14] = objVendorInformation.ap_acct_dflt;
       //     //parameters[15] =objVendorInformation.ap_department_dflt ;
       //     parameters[15] = objVendorInformation.last_pay_date;
       //     parameters[16] = objVendorInformation.hold_pymnt;
       //     parameters[17] = objVendorInformation.take_dscnt;
       //     parameters[18] = objVendorInformation.acct_bal;
       //     parameters[19] = objVendorInformation.on_acct_amt;
       //     //parameters[21] =objVendorInformation.arch_bal.ToString();
       //     parameters[20] = objVendorInformation.spec_shipping;
       //     parameters[21] = objVendorInformation.taxable;
       //     parameters[22] = objVendorInformation.bo_allowed;
       //     parameters[23] = objVendorInformation.pay_method;
       //     parameters[24] = objVendorInformation.buyer_code;
       //     parameters[25] = objVendorInformation.trd_ds_code;
       //     parameters[26] = objVendorInformation.eta_days;
       //     //parameters[29] =objVendorInformation.st_tx_code ;
       //     //parameters[30] =objVendorInformation.co_tx_code;
       //     //parameters[31] =objVendorInformation.ci_tx_code;
       //     parameters[27] = objVendorInformation.cash_acct_no;
       //     //parameters[33] =objVendorInformation.cash_department;
       //     parameters[28] = objVendorInformation.exp_acct_no;
       //     //parameters[35] =objVendorInformation.exp_department;
       //     parameters[29] = objVendorInformation.print_1099;
       //     parameters[30] = objVendorInformation.federal_tax_id;
       //     parameters[31] = objVendorInformation.currency_code;
       //     //parameters[39] =objVendorInformation.acct_bal_date;
       //     //parameters[40] =objVendorInformation.on_acct_date;
       //     //parameters[41] =objVendorInformation.sdb_code;
       //     //parameters[42] =objVendorInformation.vendor_rating.ToString();
       //     //parameters[43] =objVendorInformation.fax_phone;
       //     //parameters[44] =objVendorInformation.telex_no ;
       //     parameters[32] = objVendorInformation.mtax_frght;
       //     parameters[33] = objVendorInformation.mtax_misc;


       //     List<DVOAPVendorInformation> objSecAccountTypeList = new List<DVOAPVendorInformation>();
       //     DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
       //     int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOAPVendorInformation));
       //     return c;
       // }
       //****************************************************************************************

        /// <summary>
        /// This method is use to delete vendor information into database
        /// </summary>
        /// <param name="objAPVendorInformation">reference of DVOAPVendorInformation type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is updated or not</returns>
       public static int DeleteVendorInformation(ref DVOAPVendorInformation objVendorInformation)
        {
            object[] parameters = new object[1];
            parameters[0] = objVendorInformation.vend_code;          

            List<DVOAPVendorInformation> objSecAccountTypeList = new List<DVOAPVendorInformation>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            int c = objDalBaseClass.DeleteData(ref parameters, typeof(DVOAPVendorInformation));
            return c;
        }




       public static int  UpdateVendorInfo(ref object objTransaction, ref DVOAPVendorInformation objAPVendorInformation)
       {
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
             DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[34];
                parameters[0] = objAPVendorInformation.vend_code;
                parameters[1] = objAPVendorInformation.bus_name;
                parameters[2] = objAPVendorInformation.contact;
                parameters[3] = objAPVendorInformation.phone;
                parameters[4] = objAPVendorInformation.address1;
                parameters[5] = objAPVendorInformation.address2;
                parameters[6] = objAPVendorInformation.city;
                parameters[7] = objAPVendorInformation.state;
                parameters[8] = objAPVendorInformation.zip;
                parameters[9] = objAPVendorInformation.country;
                parameters[10] = objAPVendorInformation.credit_limit;
                parameters[11] = objAPVendorInformation.terms_code;
                parameters[12] = objAPVendorInformation.act_grp;
                parameters[13] = objAPVendorInformation.spec_billing; ;
                parameters[14] = objAPVendorInformation.ap_acct_dflt;
                //parameters[15] =objAPVendorInformation.ap_department_dflt ;
                parameters[15] = objAPVendorInformation.last_pay_date;
                parameters[16] = objAPVendorInformation.hold_pymnt;
                parameters[17] = objAPVendorInformation.take_dscnt;
                parameters[18] = objAPVendorInformation.acct_bal;
                parameters[19] = objAPVendorInformation.on_acct_amt;
                //parameters[21] =objAPVendorInformation.arch_bal.ToString();
                parameters[20] = objAPVendorInformation.spec_shipping;
                parameters[21] = objAPVendorInformation.taxable;
                parameters[22] = objAPVendorInformation.bo_allowed;
                parameters[23] = objAPVendorInformation.pay_method;
                parameters[24] = objAPVendorInformation.buyer_code;
                parameters[25] = objAPVendorInformation.trd_ds_code;
                parameters[26] = objAPVendorInformation.eta_days;
                //parameters[29] =objAPVendorInformation.st_tx_code ;
                //parameters[30] =objAPVendorInformation.co_tx_code;
                //parameters[31] =objAPVendorInformation.ci_tx_code;
                parameters[27] = objAPVendorInformation.cash_acct_no;
                //parameters[33] =objAPVendorInformation.cash_department;
                parameters[28] = objAPVendorInformation.exp_acct_no;
                //parameters[35] =objAPVendorInformation.exp_department;
                parameters[29] = objAPVendorInformation.print_1099;
                parameters[30] = objAPVendorInformation.federal_tax_id;
                parameters[31] = objAPVendorInformation.currency_code;
                //parameters[39] =objAPVendorInformation.acct_bal_date;
                //parameters[40] =objAPVendorInformation.on_acct_date;
                //parameters[41] =objAPVendorInformation.sdb_code;
                //parameters[42] =objAPVendorInformation.vendor_rating.ToString();
                //parameters[43] =objAPVendorInformation.fax_phone;
                //parameters[44] =objAPVendorInformation.telex_no ;
                parameters[32] = objAPVendorInformation.mtax_frght;
                parameters[33] = objAPVendorInformation.mtax_misc;


                int c = objDalBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAPVendorInformation));

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }

            return 1;

        }
    }
}
