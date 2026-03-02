using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;

namespace JKPS.BLL
{
    public class BLLMailHistory
    {

        public static List<DVOMailHistory> GetMailHistory(ref DVOMailHistory objDVOMailHistory)
        {
            Object[] parameters = new object[1];
            List<DVOMailHistory> objDVODetailsList = new List<DVOMailHistory>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMailHistory)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMailHistory tempobjDVO = new DVOMailHistory();
                    tempobjDVO.MailStatusID = Convert.ToInt32(dr["MailStatusID"]);
                    tempobjDVO.empl_code = dr["empl_code"].ToString().Trim();
                    tempobjDVO.doc_no = (dr["doc_no"] != DBNull.Value ? (int)(dr["doc_no"]) : 0);
                    tempobjDVO.pay_date = (dr["pay_date"] != DBNull.Value ? (DateTime)(dr["pay_date"]) : Convert.ToDateTime("01/01/1900"));
                    tempobjDVO.eop_date = (dr["eop_date"] != DBNull.Value ? (DateTime)(dr["eop_date"]) : Convert.ToDateTime("01/01/1900"));
                    tempobjDVO.keyvalue = dr["keyvalue"].ToString().Trim();
                    tempobjDVO.bank1 = dr["bank1"].ToString().Trim();
                    tempobjDVO.deposit1 = (dr["deposit1"] != DBNull.Value ? (decimal)(dr["deposit1"]) : 0);
                    tempobjDVO.bank2 = dr["bank2"].ToString().Trim();
                    tempobjDVO.deposit2 = (dr["deposit2"] != DBNull.Value ? (decimal)(dr["deposit2"]) : 0);
                    tempobjDVO.bank3 = dr["bank3"].ToString().Trim();
                    tempobjDVO.deposit3 = (dr["deposit3"] != DBNull.Value ? (decimal)(dr["deposit3"]) : 0);
                    tempobjDVO.bank4 = dr["bank4"].ToString().Trim();
                    tempobjDVO.deposit4 = (dr["deposit4"] != DBNull.Value ? (decimal)(dr["deposit4"]) : 0);
                    tempobjDVO.bank5 = dr["bank5"].ToString().Trim();
                    tempobjDVO.deposit5 = (dr["deposit5"] != DBNull.Value ? (decimal)(dr["deposit5"]) : 0);
                    tempobjDVO.check_no = dr["check_no"].ToString().Trim();
                    tempobjDVO.Status = (dr["Status"] != DBNull.Value ? (int)(dr["Status"]) : 0);
                    tempobjDVO.ErrorDesc = dr["ErrorDesc"].ToString().Trim();
                    tempobjDVO.insertby = (dr["insertby"] != DBNull.Value ? (int)(dr["insertby"]) : 0);
                    tempobjDVO.insertdate = (dr["insertdate"] != DBNull.Value ? (DateTime)(dr["insertdate"]) : Convert.ToDateTime("01/01/1900"));
                    tempobjDVO.insertmachineinfo = dr["insertmachineinfo"].ToString().Trim();
                    tempobjDVO.email = dr["mailid"].ToString().Trim();
                    tempobjDVO.first_name = dr["first_name"].ToString().Trim();
                    tempobjDVO.last_name = dr["last_name"].ToString().Trim();
                    objDVODetailsList.Add(tempobjDVO);
                }
            }
            return objDVODetailsList;
        }

        public static int InsertMailHistory(ref List<DVOMailHistory> listDVOMailHistory)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject(); 
            bool statusObjTransaction = true;
            try
            {
                foreach (DVOMailHistory objMailHistory in listDVOMailHistory)
                {
                    objDALBaseClassHelper = new DALBaseClassHelper();
                    objDALBaseClass = objDALBaseClassHelper.GetDAL();
                    objTransaction = objDALBaseClassHelper.GetTransactionObject(); 

                    object[] parameters = new object[23];
                    parameters[0] = objMailHistory.MailStatusID;
                    parameters[1] = objMailHistory.empl_code;
                    parameters[2] = objMailHistory.doc_no;
                    parameters[3] = objMailHistory.eop_date;
                    parameters[4] = objMailHistory.pay_date;
                    parameters[5] = objMailHistory.keyvalue;
                    parameters[6] = objMailHistory.bank1;
                    parameters[7] = objMailHistory.deposit1;
                    parameters[8] = objMailHistory.bank2;
                    parameters[9] = objMailHistory.deposit2;
                    parameters[10] = objMailHistory.bank3;
                    parameters[11] = objMailHistory.deposit3;
                    parameters[12] = objMailHistory.bank4;
                    parameters[13] = objMailHistory.deposit4;
                    parameters[14] = objMailHistory.bank5;
                    parameters[15] = objMailHistory.deposit5;
                    parameters[16] = objMailHistory.check_no;
                    parameters[17] = objMailHistory.Status;
                    parameters[18] = objMailHistory.ErrorDesc;
                    parameters[19] = objMailHistory.insertby;
                    parameters[20] = objMailHistory.insertdate;
                    parameters[21] = objMailHistory.insertmachineinfo;
                    parameters[22] = objMailHistory.email;

                    object obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMailHistory), objMailHistory.INSERT_SPNAME);
                    if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) > 0)
                        statusObjTransaction = Convert.ToBoolean(obj);
                    else
                        throw new Exception("Error occured while inserting time-card detail.");
                    if (statusObjTransaction && objTransaction != null)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                return 1;

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

    }
}
