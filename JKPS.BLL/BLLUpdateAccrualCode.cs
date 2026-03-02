using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;

namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.) BLL For UpdateAccrualCode                    Rajeev(D)                                    18/11/2008(DD)
    ///2.) 
    ///<summery>
   public class BLLUpdateAccrualCode
    {
        // Function Execute to insert new Accrual Code and Check For the Existing Code before Insertion.
        public static int InsertAccrualCode(ref object objTransaction,ref DVOUpdAccrualCode objDVOAccrCode)
        {
            //int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            } 
            try
            {
                object[] parameters = new object[12];
                parameters[0] = objDVOAccrCode.accr_code;
                parameters[1] = objDVOAccrCode.accr_desc;
                parameters[2] = objDVOAccrCode.accr_method;
                parameters[3] = objDVOAccrCode.accr_rate;
                parameters[4] = objDVOAccrCode.accr_freq;
                parameters[5] = objDVOAccrCode.accr_lapse;

                //Parameters used For Only SQL Server
                parameters[6] = objDVOAccrCode.InsertMachineInfo;
                parameters[7] = objDVOAccrCode.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[8] = objDVOAccrCode.InsertBy;
                parameters[9] = objDVOAccrCode.UpdateMachineInfo;
                parameters[10] = objDVOAccrCode.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[11] = objDVOAccrCode.UpdateBy;

                object obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOUpdAccrualCode()).INSERT_SPNAME);
                //DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdAccrualCode));
                //if(Ds.Tables.Count>0)
                //    if (Ds.Tables[0].Rows.Count > 0)
                //    {
                //        success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
                //        objDALBaseClassHelper.CommitTransaction(ref objTransection);
                //    }
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                // Return when Code Inserted the value
                if (Convert.ToInt16 (obj) == 1)
                {
                    if (!statusObjTransaction)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return 1;
                    }
                }
                 // Return when matching code found
                else if (Convert.ToInt16 (obj) == 2)
                {
                    if (!statusObjTransaction)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return 2;
                    }
                }
                // Return when Exception generated 
                else
                {

                    if (!statusObjTransaction)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManager.Publish(ex);
                    return 0;
                }
                
            }
            return 0;

        }

        //Function Execute to Get the all AccrualCode List.
        public static List<DVOUpdAccrualCode> GetALLAccrualCode()
        {
            List<DVOUpdAccrualCode> objDVOUpdAccrualCodeList = new List<DVOUpdAccrualCode>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOUpdAccrualCode)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdAccrualCode objDVOAccrCode = new DVOUpdAccrualCode();
                    objDVOAccrCode.RowID = Convert.ToInt32(dr[0]);
                    objDVOAccrCode.accr_code = dr[1].ToString().Trim();
                    objDVOAccrCode.accr_desc = dr[2].ToString().Trim();
                    objDVOAccrCode.accr_method = dr[3].ToString().Trim();
                    objDVOAccrCode.accr_rate = (dr[4] != DBNull.Value ? (decimal?) dr[4] : null);
                    objDVOAccrCode.accr_freq = Convert.ToInt32(dr[5]);
                    objDVOAccrCode.accr_lapse = Convert.ToInt32(dr[6]);
                    objDVOUpdAccrualCodeList.Add(objDVOAccrCode);
                }
                return objDVOUpdAccrualCodeList;
            }
        }

        //Function Execute to Get Individual AccrualCode Details According to *AccrualCode*
        public static List<DVOUpdAccrualCode> GetAccrualCode(ref DVOUpdAccrualCode objDVOAccrCode)
        {
            List<DVOUpdAccrualCode> objDVOUpdAccrualCodeList = new List<DVOUpdAccrualCode>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            
            try
            {
                object[] parameter = new object[1];
                parameter[0] = objDVOAccrCode.accr_code;
                DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdAccrualCode));
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DVOUpdAccrualCode tempobjDVOAccrCode = new DVOUpdAccrualCode();
                        tempobjDVOAccrCode.RowID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                        tempobjDVOAccrCode.accr_code = ds.Tables[0].Rows[0].ItemArray[1].ToString().Trim();
                        tempobjDVOAccrCode.accr_desc = ds.Tables[0].Rows[0].ItemArray[2].ToString().Trim();
                        tempobjDVOAccrCode.accr_method = ds.Tables[0].Rows[0].ItemArray[3].ToString().Trim();
                        tempobjDVOAccrCode.accr_rate = Convert.ToDecimal(ds.Tables[0].Rows[0].ItemArray[4]!=DBNull.Value ? ds.Tables[0].Rows[0].ItemArray[4].ToString():null);
                        tempobjDVOAccrCode.accr_freq = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[5].ToString());
                        tempobjDVOAccrCode.accr_lapse = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[6].ToString());
                        objDVOUpdAccrualCodeList.Add(tempobjDVOAccrCode);
                        return objDVOUpdAccrualCodeList;
                    }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
            return null;
        }

       //Get All Accural Code Also done the Search  according to the parameter value. Used For Indformix
       public static List<DVOUpdAccrualCode> GetAccrualCodeAllSearch(ref DVOUpdAccrualCode objDVOAccrCode)
       {
           object[] parameters = new object[7];
           parameters[0] = objDVOAccrCode.accr_code;
           parameters[1] = objDVOAccrCode.accr_desc;
           parameters[2] = objDVOAccrCode.accr_method;
           parameters[3] = objDVOAccrCode.accr_rate;
           parameters[4] = objDVOAccrCode.accr_freq;
           parameters[5] = objDVOAccrCode.accr_lapse;
           parameters[6] = objDVOAccrCode.RowID;

           List<DVOUpdAccrualCode> objDVOUpdAccrualCodeList = new List<DVOUpdAccrualCode>();
           DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
           DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

           using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdAccrualCode)))
           {
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   DVOUpdAccrualCode tempobjDVOAccrCode = new DVOUpdAccrualCode();
                   tempobjDVOAccrCode.RowID = Convert.ToInt32(dr[0]);
                   tempobjDVOAccrCode.accr_code = dr[1].ToString().Trim();
                   tempobjDVOAccrCode.accr_desc = dr[2].ToString().Trim();
                   tempobjDVOAccrCode.accr_method = dr[3].ToString().Trim();
                   tempobjDVOAccrCode.accr_rate = (dr[4]!=DBNull.Value? (decimal?)dr[4]:null);
                   tempobjDVOAccrCode.accr_freq = Convert.ToInt32(dr[5]);
                   tempobjDVOAccrCode.accr_lapse = Convert.ToInt32(dr[6]);
                   objDVOUpdAccrualCodeList.Add(tempobjDVOAccrCode);
               }
           }

           return objDVOUpdAccrualCodeList;
       }




        //Function Execute to Update AccrualCode According to the *RowID*
        //public static int UpdateAccrualCode(ref DVOUpdAccrualCode objDVOAccrCode)
        //{
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        object[] parameters = new object[13];
        //        parameters[0] = objDVOAccrCode.RowID;
        //        parameters[1] = objDVOAccrCode.accr_code;
        //        parameters[2] = objDVOAccrCode.accr_desc;
        //        parameters[3] = objDVOAccrCode.accr_method;
        //        parameters[4] = objDVOAccrCode.accr_rate;
        //        parameters[5] = objDVOAccrCode.accr_freq;

        //        //Parameters used For Only SQL Server
        //        parameters[6] = objDVOAccrCode.accr_lapse;
        //        parameters[7] = objDVOAccrCode.InsertMachineInfo;
        //        parameters[8] = objDVOAccrCode.InsertDate;
        //        parameters[9] = objDVOAccrCode.InsertBy;
        //        parameters[10] = objDVOAccrCode.UpdateMachineInfo;
        //        parameters[11] = objDVOAccrCode.UpdateDate;
        //        parameters[12] = objDVOAccrCode.UpdateBy;

        //        object[] RetValue = new object[1];
        //        RetValue[0] = objDalBaseClass.UpdateData(ref parameters, typeof(DVOUpdAccrualCode), true);
        //        // success = objDalBaseClass.UpdateData(ref parameters, typeof(DVOUpdAccrualCode));
        //        // Return when Code updated Successfully
        //        if (RetValue[0] != null)
        //        {
        //            string ret = RetValue[0].ToString();
        //            if (Convert.ToInt32(RetValue[0].ToString()) == 1)
        //            {
        //                return 1;
        //            }
        //            // Return when matching code found 
        //            else if (Convert.ToInt32(RetValue[0].ToString()) == 2)
        //            {
        //                return 2;
        //            }
        //            // Return when Exception generated 
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return 0;
        //    }
        //    //return 0;

        //}

        //Function Execute to Delete AccrualCode According to the *RowID*
        public static int DeleteAccrualCode(ref DVOUpdAccrualCode objDVOAccrCode)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
           try
           {
               int success = 0;
               object[] parameters = new object[1];
               parameters[0] = objDVOAccrCode.RowID;
               success = objDalBaseClass.DeleteData(ref parameters, typeof(DVOUpdAccrualCode));
               if (success > 0)
                   return success;
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               return 0;
           }
           return 0;
        }



       public static object  UpdateAccrualCode(ref object objTransaction, ref DVOUpdAccrualCode objDVoAccrualCode)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            } 
           DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
           bool status = false;

            try
            {
               //******************************

                  object[] parameters = new object[13];
                  parameters[0] = objDVoAccrualCode.RowID;
                  parameters[1] = objDVoAccrualCode.accr_code;
                  parameters[2] = objDVoAccrualCode.accr_desc;
                  parameters[3] = objDVoAccrualCode.accr_method;
                  parameters[4] = objDVoAccrualCode.accr_rate;
                  parameters[5] = objDVoAccrualCode.accr_freq;

                //Parameters used For Only SQL Server
                  parameters[6] = objDVoAccrualCode.accr_lapse;
                  parameters[7] = objDVoAccrualCode.InsertMachineInfo;
                  parameters[8] = objDVoAccrualCode.InsertDate;
                  parameters[9] = objDVoAccrualCode.InsertBy;
                  parameters[10] = objDVoAccrualCode.UpdateMachineInfo;
                  parameters[11] = objDVoAccrualCode.UpdateDate;
                  parameters[12] = objDVoAccrualCode.UpdateBy;

                //object[] RetValue = new object[1];
                //RetValue[0] = 
                  //success = objDALBaseClass.UpdateData_ByTransaction (ref  objTransaction , ref parameters, typeof(DVOUpdAccrualCode));
                  object c = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVoAccrualCode.UPDATE_SPNAME, true);
                  if (Convert.ToInt16(c) == 1)
                  {
                      status = true;
                  }
                  //success = objDALBaseClass.UpdateData(ref parameters, typeof(DVOUpdAccrualCode));
                // Return when Code updated Successfully
                //if (RetValue[0] != null)
                //{
                //    string ret = RetValue[0].ToString();
                //    if (Convert.ToInt32(RetValue[0].ToString()) == 1)
                //    {
                //        return 1;
                //    }
                //    // Return when matching code found 
                //    else if (Convert.ToInt32(RetValue[0].ToString()) == 2)
                //    {
                //        return 2;
                //    }
                //    // Return when Exception generated 
                //    else
                //    {
                //        return 0;
                //    }
                //}
                //else
                //{
                //    return 0;
                //}

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);

            }
          
               
            
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return status  ;

           
        }
    }
}
