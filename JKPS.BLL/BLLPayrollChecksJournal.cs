using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;
namespace JKPS.BLL
{
    public  class BLLPayrollChecksJournal
    {
        public static DataTable GetPayrollChecksJrnData(ref DVOGLActivityDetail objSearch)
        {
            object[] parameters = new object[2];
            parameters[0] = objSearch.DocDateFrom;
            parameters[1] = objSearch.DocDateTo;
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataTable objDataTable = new DataTable();
            try
            {
                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLActivityDetail), objSearch.GET_PAYROLLCHKJRN);
                if (ds.Tables.Count > 0)
                {  
                    ds.Tables[0].Columns[0].ColumnName = "v_src_desc";
                    ds.Tables[0].Columns[1].ColumnName = "v_acct_no";
                    ds.Tables[0].Columns[2].ColumnName = "v_act_code";
                    ds.Tables[0].Columns[3].ColumnName = "v_act_type";
                    ds.Tables[0].Columns[4].ColumnName = "v_amount";
                    ds.Tables[0].Columns[5].ColumnName = "v_dept_code";
                    ds.Tables[0].Columns[6].ColumnName = "v_check_no";
                    ds.Tables[0].Columns[7].ColumnName = "v_doc_no";
                    ds.Tables[0].Columns[8].ColumnName = "v_pay_date";
                    ds.Tables[0].Columns[9].ColumnName = "v_ref_code";
                    ds.Tables[0].Columns.Add("act_total");
                    ds.Tables[0].Columns.Add("empl_name");
                    ds.Tables[0].Columns.Add("act_desc");
                    ds.Tables[0].Columns.Add("keyvalue");
                    ds.Tables[0].Columns.Add("acct_desc");
                    ds.Tables[0].Columns.Add("SearchCriteria");
                    ds.Tables[0].Columns.Add("cancl_voided");
                    objDataTable = ds.Tables[0].Clone();
                    decimal act_total = 0;
                    int lastacct_no = 0;
                    string keyvalue=string.Empty;
                    string acct_desc=string.Empty;
                    string chk_line = string.Empty;
                    objDataTable = ds.Tables[0].Clone();
                    //Added by Sarvjeet Verma On 06/07/2009 to determine cancelled checks
                    //Get posted entries from styvoide
                    Object[] param = new object[0];
                    DataSet DSVoide = objDalBaseClass.GetData(ref param, typeof(DVOGLActivityDetail), objSearch.GET_VOIDE);
                    DSVoide.Tables[0].Columns[0].ColumnName = "pay_doc_no";
                    DSVoide.Tables[0].Columns[1].ColumnName = "doc_no";

                    DataRow[] SortedRowArray = ds.Tables[0].Select("", "v_act_type,v_acct_no,v_act_code");
                    for (int i = 0; i < SortedRowArray.Length; i++)
                    {
                        DataRow dr = SortedRowArray[i];
                        DataRow drn = objDataTable.NewRow(); 
                        decimal amount=(dr["v_amount"]!=DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) :0);
                        string act_type=(dr["v_act_type"]!=DBNull.Value ? Convert.ToString(dr["v_act_type"]) : string.Empty);
                        int doc_no=(dr["v_doc_no"]!=DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) :0);
                        string act_code = (dr["v_act_code"] != DBNull.Value ? Convert.ToString(dr["v_act_code"]) : string.Empty);
                        int CurrentAcct_no = (dr["v_acct_no"] != DBNull.Value ? Convert.ToInt32(dr["v_acct_no"]) : 0);
                        dr["SearchCriteria"] = " From " + objSearch.DocDateFrom + " to " + objSearch.DocDateTo;

                        if (act_type.Trim() == "B" || act_type.Trim() == "C" || act_type.Trim() == "D")
                        {
                            act_total = act_total + amount;
                            dr["act_total"] = act_total;
                            
                        }
                        if (act_type.Trim() == "A")
                        {
                            object[] parameter = new object[1];
                            parameter[0] = dr["v_ref_code"].ToString().Trim();
                           DataSet dsempname = objDalBaseClass.GetData(ref parameter,typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).GET_EMPL_NAME);
                           if (dsempname.Tables[0].Rows.Count > 0)
                           {
                               DataRow drempn = dsempname.Tables[0].Rows[0];
                               string last_name = drempn[2] != DBNull.Value ? drempn[2].ToString().Trim() : string.Empty;
                               string first_name = drempn[0] != DBNull.Value ? drempn[0].ToString().Trim() : string.Empty;
                               string midle_name = drempn[1] != DBNull.Value ? drempn[1].ToString().Trim() : string.Empty;
                               dr["empl_name"] = last_name + " , " + first_name;
                           }
                           //Determine this check is cancelled or not
                           DataRow[] dra=DSVoide.Tables[0].Select("pay_doc_no = "+doc_no);
                           if (dra.Length > 0)
                               dr["cancl_voided"] = "C";
                           //Check this is voided entry...
                           DataRow[] dra1 = DSVoide.Tables[0].Select("doc_no = " + doc_no);
                            if(dra1.Length>0)
                                dr["cancl_voided"] = "V";

                        }
                        else
                        {
                            dr["v_check_no"] = "";
                            dr["v_doc_no"] =0;
                            dr["v_pay_date"] =DBNull.Value;
                            dr["v_dept_code"] = "";
                            dr["v_src_desc"] = "";
                        }  
                        if (CurrentAcct_no != lastacct_no || i == 0)
                        {
                             keyvalue = string.Empty;
                             int id = 0;
                             string acct_type = string.Empty;
                             acct_desc = string.Empty;
                            BLLCommonUtilities.GetAccountInformation(CurrentAcct_no, out keyvalue, out acct_type, out id, out acct_desc);
                        }
                        dr["keyvalue"] = keyvalue;
                        dr["acct_desc"] = acct_desc;


                            bool status = false;
                            if (ds.Tables[0].Rows.Count != i + 1)
                            {
                                if (act_code.Trim() != SortedRowArray[i + 1]["v_act_code"].ToString().Trim())
                                {
                                    status = true;
                                }
                            }
                            else if (SortedRowArray.Length == i + 1)
                                status = true;

                            if (status)
                            {
                                if (act_type.Trim() == "B" || act_type.Trim() == "C" || act_type.Trim() == "D")
                                {   
                                    dr["act_total"] = act_total;
                                    act_total = 0;
                                    object[] actparam =new object[2];
                                    actparam[0]=act_type.Trim();
                                    actparam[1]=act_code.Trim();
                                    dr["act_desc"] = objDalBaseClass.ExecuteScalar(ref actparam, objSearch.GETACT_DESC);
                                }  
                                else
                                {
                                    dr["act_desc"] = "";
                                    dr["v_act_code"] = "";
                                    
                                }
                            }
                            lastacct_no = CurrentAcct_no;
                            drn.ItemArray = dr.ItemArray;
                            objDataTable.Rows.Add(drn);
                    }
                }
            }

            catch (Exception ex)
            {
                ds.Dispose();
                throw ex;
            }
            ds.Dispose();
            return objDataTable;
        }
    }
}
