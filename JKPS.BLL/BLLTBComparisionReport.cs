using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;


namespace JKPS.BLL
{
    //**********************Written By - Rohit Wadhwa**************
    //**********************24/10/2009*****************************
    //*****************Used FOR IMPLIMENTATION OF TB COMPARISION REPORT **************
    public class BLLTBComparisionReport
    {
        public static DataTable GetTBComparisionReport(ref DVOTreasuryBillMaintanance objTBComparisionSearch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];
            object[] parametersquery = new object[10];
            parametersquery[0] = null;
            parametersquery[1] = null;
            parametersquery[2] = null;
            parametersquery[3] = null;
            parametersquery[4] = null;
            parametersquery[5] = null;
            parametersquery[6] = null;
            parametersquery[7] = null;
            parametersquery[8] = null;
            parametersquery[9] = null;

            DataSet ds = null;
            DVOTreasuryBillTenders objDVOtenders = new DVOTreasuryBillTenders();
            IDataReader DRD = objDalBaseClass.GetDataByReader(ref parametersquery, typeof(DVOTreasuryBillTenders));
            DataTable DtComparision = new DataTable();
            DtComparision.Columns.Add("prev_issue_num", Type.GetType("System.Int32"));
            DtComparision.Columns.Add("new_issue_num", Type.GetType("System.Int32"));
            DtComparision.Columns.Add("new_amt_issued", Type.GetType("System.Decimal"));
            DtComparision.Columns.Add("prev_amt_issued", Type.GetType("System.Decimal"));
            DtComparision.Columns.Add("new_amt_per_100", Type.GetType("System.Decimal"));
            DtComparision.Columns.Add("prev_amt_per_100", Type.GetType("System.Decimal"));
            DtComparision.Columns.Add("Tend_code", Type.GetType("System.String"));
            DtComparision.Columns.Add("TenderName", Type.GetType("System.String"));

            //  DataRow Dr = new DataRow();
            parameters = new object[4];
            parameters[0] = objTBComparisionSearch.schemeId;
            parameters[1] = objTBComparisionSearch.issue_no;
            parameters[2] = null;
            parameters[3] = 1;
            DataSet DStempHolder = new DataSet();
            DataSet DsNewIssueData = objDalBaseClass.GetData(objDVOtenders.GET_TENDER_ISSUE_AMT(ref parameters));
            DsNewIssueData.Tables[0].TableName = "DtNewIssueData";
            object LowerIssueNum = objDalBaseClass.ExecuteScalar(objDVOtenders.GET_lOWER_ISSUE_NUM(ref parameters));
            if (LowerIssueNum != DBNull.Value)
            {
                parameters[1] = Convert.ToInt32(LowerIssueNum);
            }
            else
            {
                parameters[1] = 0;
            }
            parameters[3] = 0;
            DataSet DsoldIssueData = objDalBaseClass.GetData(objDVOtenders.GET_TENDER_ISSUE_AMT(ref parameters));
            DsoldIssueData.Tables[0].TableName = "DtoldIssueData";
            while (DRD.Read())
            {

                DVOTreasuryBillMaintanance objmaintain = new DVOTreasuryBillMaintanance();
                DataRow Dr = DtComparision.NewRow();
                if ((DsNewIssueData.Tables[0].Rows.Count > 0) || (DsoldIssueData.Tables[0].Rows.Count > 0))
                {
                    DataRow[] drnew = null;

                    DataRow[] drold = null;
                    if (DRD[1] != DBNull.Value)
                    {
                        drnew = DsNewIssueData.Tables[0].Select("tend_code='" + DRD[1].ToString() + "'");
                        drold = DsoldIssueData.Tables[0].Select("tend_code='" + DRD[1].ToString() + "'");
                        Dr["Tend_code"] = DRD[1].ToString();
                        Dr["TenderName"] = DRD[3].ToString();

                    }

                    if (drnew.Length > 0 || drold.Length > 0)
                    {
                        if (drnew.Length > 0)
                        {
                            Dr["new_issue_num"] = objTBComparisionSearch.issue_no;
                            Dr["new_amt_issued"] = drnew[0][1];
                            Dr["new_amt_per_100"] = drnew[0][2];
                        }
                        if (drold.Length > 0)
                        {
                            Dr["prev_issue_num"] = objTBComparisionSearch.issue_no - 1;
                            Dr["prev_amt_issued"] = drold[0][1];
                            Dr["prev_amt_per_100"] = drold[0][2];
                        }
                        DtComparision.Rows.Add(Dr);
                    }

                }






            }
            DRD.Dispose();


            return DtComparision;

        }
        public static Int32 GetSMaxIssueInScheme()
        {
            object[] parameters = new object[2];
            parameters[0] = 0;
            parameters[1] = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object SMaxIssueinScheme = objDalBaseClass.ExecuteScalar("");
            Int32 SMaxIssueinSchemeS = 0;
            if (SMaxIssueinScheme != DBNull.Value)
            {
                SMaxIssueinSchemeS = Convert.ToInt32(SMaxIssueinScheme);
            }
            return SMaxIssueinSchemeS;
        }

    }
}
