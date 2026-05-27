using App.Data;
using App.Data.ViewModels;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using App.Web.Models;

namespace App.Web.Helper
{
    public class PensionBackgroundJob
    {
        public static void ExecutePensionGeneration(DVOddmStypddreAndStypddrd objSearch, PensionProcessViewModel Paysearch, GeneratePensionProcessModel generatePensionProcessModel, string regionName, int userId)
        {
            try
            {
                // Set the region name in the logical call context so that ConnectionStringProvider.GetConnectionString() can access it in the background thread
                System.Runtime.Remoting.Messaging.CallContext.LogicalSetData("RegionName", regionName);

                // 1. Execute Heavy Stored Procedure
                DataSet ds = ReportingUtilities.GetDirectDepositeListing(ref objSearch, Paysearch, true);
                
                if (ds.Tables[0].Rows.Count != 0)
                {
                    // 2. Fetch distinct districts
                    var districtList = new HashSet<string>(generatePensionProcessModel.RegionNames.Split(',').Select(d => d.Trim().ToUpper()).ToList());
                    List<string> District;
                    
                    string connectionString = new ConnectionStringProvider().GetConnectionStringApi(regionName);
                    using (var db = new AppDbContext(connectionString))
                    {
                        District = db.MasterEmployees.AsNoTracking()
                            .Where(x => districtList.Contains(x.SelectDistrict.Trim().ToUpper()))
                            .Select(x => x.SelectDistrict.Trim().ToUpper())
                            .Distinct()
                            .ToList();
                    }

                    bool GenerateCheques_user = false;

                    // 3. Generate Bank Media Files sequentially (or can be Parallelized if SP supports it, keeping sequential for safety)
                    for (int i = 0; i < District.Count; i++)
                    {
                        if (i == District.Count - 1)
                        {
                            GenerateCheques_user = false;
                        }
                        DirectGenerateBankMediaBackground(Paysearch, generatePensionProcessModel, District[i], GenerateCheques_user, regionName, userId, connectionString);
                    }

                    // 4. Update Header status
                    using (var db = new AppDbContext(connectionString))
                    {
                        db.Database.ExecuteSqlCommand("UPDATE Process_DirectDeposit_Header SET used='Y' where pybatchid=" + Paysearch.pybatchid.ToString());
                    }

                    // 5. Update overall Batch Status to indicate completion
                    DVOPYBatchProcessStybatchr updBatch = new DVOPYBatchProcessStybatchr();
                    updBatch.pybatchid = Paysearch.pybatchid;
                    updBatch.updateby = userId;
                    updBatch.updatemachineinfo = Environment.MachineName;
                    
                    BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref updBatch);
                }
            }
            catch (Exception ex)
            {
                // Log the exception for diagnostics
                try
                {
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "background_error.txt");
                    System.IO.File.AppendAllText(logPath, DateTime.Now.ToString() + " Error in ExecutePensionGeneration: " + ex.ToString() + Environment.NewLine);
                }
                catch { }
                try
                {
                    DVOPYBatchProcessStybatchr cancelBatch = new DVOPYBatchProcessStybatchr();
                    cancelBatch.pybatchid = Paysearch.pybatchid;
                    cancelBatch.updateby = userId;
                    cancelBatch.updatemachineinfo = Environment.MachineName;
                    BLLPYBatchProcessStybatchr.CANCELBatchProcessStatus(ref cancelBatch); // or another SP to mark it as error
                }
                catch { }
            }
        }

        private static string DirectGenerateBankMediaBackground(PensionProcessViewModel Paysearch, GeneratePensionProcessModel generatePensionProcessModel, string District, bool GenerateCheques_user, string region, int userId, string connectionString)
        {
            try
            {
                string disData = string.Empty;
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }

                int apBatchID = -1;
                generatePensionProcessModel.GenerateCheques = "N";
                if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                {
                    apBatchID = -1;
                    bool IsMasterBatch = false;
                    string _APCurrentUser = string.Empty;
                    BLLBatchMaintenance.GetActiveBatchForReports(userId.ToString(), BatchTypes.APCashDeposit, out _APCurrentUser, out apBatchID, out IsMasterBatch);
                }

                bool sameMediaFormat = true;
                DVOMasterBankDetails objDVOMasterBankDetails = new DVOMasterBankDetails();
                List<DVOMasterBankDetails> listDVOMasterBankDetails = new List<DVOMasterBankDetails>();
                System.Collections.ArrayList _arrBankCodes = new System.Collections.ArrayList();
                
                string[] bankCodeArr = string.IsNullOrEmpty(generatePensionProcessModel.BanckCode) 
                    ? new string[0] 
                    : generatePensionProcessModel.BanckCode.Split(',');
                string _selectedBankCodes = string.Empty;
                foreach (var b in bankCodeArr)
                {
                    if (!string.IsNullOrWhiteSpace(b))
                    {
                        _arrBankCodes.Add(b.Trim());
                        _selectedBankCodes += "'" + b.Trim() + "',";
                    }
                }
                if (_selectedBankCodes.EndsWith(",")) _selectedBankCodes = _selectedBankCodes.TrimEnd(',');

                listDVOMasterBankDetails = BLLPRBankMediaInybankd.GetData(ref objDVOMasterBankDetails, true, _selectedBankCodes);
                if (listDVOMasterBankDetails.Count > 1)
                {
                    if (listDVOMasterBankDetails.Count == _arrBankCodes.Count)
                    {
                        for (int i = 0; i < listDVOMasterBankDetails.Count - 1; i++)
                        {
                            if (listDVOMasterBankDetails[i].media_str.Trim() != listDVOMasterBankDetails[i + 1].media_str.Trim())
                            {
                                sameMediaFormat = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        sameMediaFormat = false;
                    }
                }
                else
                {
                    sameMediaFormat = true;
                }

                if (!sameMediaFormat)
                {
                    return "All selected Banks have not same Media-Format.";
                }
                else
                {
                    var directoryName = region == "KASHMIR REGION" ? "K_Disbursement.csv" : "J_Disbursement.csv";
                    string formattedName = $"{District}_{Paysearch.pybatchid}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{directoryName}";

                    int TotalRowsCount = 0;
                    DVOddmStypddreAndStypddrd objDVOddmStypddreAndStypddrd = new DVOddmStypddreAndStypddrd();

                    foreach (string strBankCode in _arrBankCodes)
                    {
                        objDVOddmStypddreAndStypddrd.BanckCode = objDVOddmStypddreAndStypddrd.BanckCode + strBankCode + "^";
                    }
                    objDVOddmStypddreAndStypddrd.GenCheck = generatePensionProcessModel.GenerateCheques;
                    objDVOddmStypddreAndStypddrd.Date = generatePensionProcessModel.DepositDate != null ? Convert.ToDateTime(generatePensionProcessModel.DepositDate) : Convert.ToDateTime("01/01/1900");
                    
                    Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(generatePensionProcessModel.PayDate);
                    if (generatePensionProcessModel.PayDate == null || generatePensionProcessModel.PayDate == DateTime.Parse("01/01/1900"))
                    {
                        Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                    }
                    DateTime parsedDate = Paysearch.PayrollDate.Value;
                    objDVOddmStypddreAndStypddrd.District = disData;
                    string Paydate_value = parsedDate.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DataTable objDataTable;

                    var directorysName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
                    string filePath = Helper.DirectGenerateBankMediaPath(region, directorysName, formattedName);

                    using (var db = new AppDbContext(connectionString))
                    {
                        SqlParameter[] parameter = {
                            new SqlParameter("@DepositDate", Paydate_value),
                            new SqlParameter("@filePathWithName", filePath),
                            new SqlParameter("@UserId", userId),
                            new SqlParameter("@district", District),
                        };
                        
                        db.Database.CommandTimeout = 600; // Increase timeout for background processing
                        var result = db.Database.SqlQuery<string>("EXEC GenerateBankMediaFile @DepositDate,@filePathWithName, @UserId, @district", parameter).ToList();
                        
                        if (result.Any() && result[0] != "There is no record to create media.")
                        {
                            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                            object[] parameters = new object[20];
                            parameters[0] = Paysearch.pybatchid; 
                            parameters[1] = string.Empty;
                            parameters[2] = true; 
                            parameters[3] = DateTime.Now; 
                            parameters[4] = Environment.MachineName; 
                            parameters[5] = userId; 
                            parameters[6] = DateTime.Now; 
                            parameters[7] = true; 
                            parameters[8] = string.Empty; 
                            parameters[9] = true; 
                            parameters[10] = Convert.ToInt32(MediaType.TxnReport); 
                            parameters[11] = 0;
                            parameters[12] = 0;
                            parameters[13] = 0;
                            parameters[14] = formattedName; 
                            parameters[15] = District;
                            parameters[16] = false;   
                            parameters[17] = 0;  
                            parameters[18] = null;  
                            parameters[19] = 0; 

                            objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");
                        }
                    }

                    if (GenerateCheques_user)
                    {
                        if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.GenerateCheques) && generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                        {
                            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, true, apBatchID);
                        }
                        else
                        {
                            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
                        }
                        
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            TotalRowsCount += objDataTable.Rows.Count;
                        }
                        
                        if (TotalRowsCount <= 0) return "There is no record to create media.";
                    }
                    
                    return "File has been successfully saved.";
                }
            }
            catch (Exception ex)
            {
                try
                {
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "background_error.txt");
                    System.IO.File.AppendAllText(logPath, DateTime.Now.ToString() + " Error in DirectGenerateBankMediaBackground: " + ex.ToString() + Environment.NewLine);
                }
                catch { }
                return "Please Try Again..." + ex.Message;
            }
        }
    }
}
