using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using App.Web.Filters;
using App.Web.Models;
using ClosedXML.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class TransactionController : BaseController
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public TransactionController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Transaction
        public ActionResult Index()
        {
            var model = new TransactionIndexViewModel
            {
                Departments = db.TxnDetails
                    .Where(d => d.Department != null && d.Department != "")
                    .Select(d => d.Department)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToList()
                    .Select(d => new SelectListItem { Text = d, Value = d })
                    .ToList(),

                Statuses = db.TxnDetails
                    .Where(d => d.Status != null && d.Status != "")
                    .Select(d => d.Status)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList()
                    .Select(s => new SelectListItem { Text = s, Value = s })
                    .ToList(),

                Schemes = db.TxnDetails
                    .Where(d => d.Scheme != null && d.Scheme != "")
                    .Select(d => d.Scheme)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList()
                    .Select(s => new SelectListItem { Text = s, Value = s })
                    .ToList()
            };

            return View(model);
        }

        // Server-side DataTable handler
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {   
            try
            {
                // Read filter parameters from query string
                string txnDateFrom = Request["txnDateFrom"];
                string txnDateTo = Request["txnDateTo"];
                string department = Request["department"];
                string status = Request["status"];
                string scheme = Request["scheme"];
                string searchAppRef = Request["searchAppRef"];
                string searchAccNo = Request["searchAccNo"];
                string searchTxnRef = Request["searchTxnRef"];

                // Base query: join txnHeader + txnDetail
                var query = from d in db.TxnDetails.AsNoTracking()
                            join h in db.TxnHeaders.AsNoTracking() on d.HeaderId equals h.HeaderId
                            select new TransactionDetailViewModel
                            {
                                DetailId = d.DetailId,
                                HeaderId = d.HeaderId,
                                SourceTable = h.SourceTable,
                                TxnDate = h.TxnDate,
                                ImportedOn = h.ImportedOn,
                                ApplicationReferenceNo = d.ApplicationReferenceNo,
                                ApplicationReferenceNo1 = d.ApplicationReferenceNo1,
                                Department = d.Department,
                                DepartmentAccountNo = d.DepartmentAccountNo,
                                Amount = d.Amount,
                                DateText = d.DateText,
                                DepartmentBankName = d.DepartmentBankName,
                                DepartmentBankIFSC = d.DepartmentBankIFSC,
                                Name = d.Name,
                                IFSC = d.IFSC,
                                AccountNo = d.AccountNo,
                                Scheme = d.Scheme,
                                Status = d.Status,
                                TransactionReference = d.TransactionReference,
                                TransactionDate = d.TransactionDate,
                                Remarks = d.Remarks
                            };

                // Apply filters
                if (!string.IsNullOrEmpty(txnDateFrom))
                {
                    DateTime fromDate;
                    if (DateTime.TryParseExact(txnDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
                    {
                        query = query.Where(x => x.TxnDate >= fromDate);
                    }
                }

                if (!string.IsNullOrEmpty(txnDateTo))
                {
                    DateTime toDate;
                    if (DateTime.TryParseExact(txnDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                    {
                        toDate = toDate.AddDays(1); // Include the entire day
                        query = query.Where(x => x.TxnDate < toDate);
                    }
                }

                if (!string.IsNullOrEmpty(department))
                {
                    query = query.Where(x => x.Department == department);
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(x => x.Status == status);
                }

                if (!string.IsNullOrEmpty(scheme))
                {
                    query = query.Where(x => x.Scheme == scheme);
                }

                if (!string.IsNullOrEmpty(searchAppRef))
                {
                    query = query.Where(x => x.ApplicationReferenceNo.Contains(searchAppRef) || x.ApplicationReferenceNo1.Contains(searchAppRef));
                }

                if (!string.IsNullOrEmpty(searchAccNo))
                {
                    query = query.Where(x => x.AccountNo.Contains(searchAccNo));
                }

                if (!string.IsNullOrEmpty(searchTxnRef))
                {
                    query = query.Where(x => x.TransactionReference.Contains(searchTxnRef));
                }

                // Total records (before global search)
                int totalRecords = query.Count();

                // Global search
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    string search = param.sSearch.ToLower();
                    query = query.Where(x =>
                        (x.Name != null && x.Name.ToLower().Contains(search)) ||
                        (x.Department != null && x.Department.ToLower().Contains(search)) ||
                        (x.AccountNo != null && x.AccountNo.ToLower().Contains(search)) ||
                        (x.ApplicationReferenceNo != null && x.ApplicationReferenceNo.ToLower().Contains(search)) ||
                        (x.TransactionReference != null && x.TransactionReference.ToLower().Contains(search)) ||
                        (x.Scheme != null && x.Scheme.ToLower().Contains(search)) ||
                        (x.Status != null && x.Status.ToLower().Contains(search)) ||
                        (x.SourceTable != null && x.SourceTable.ToLower().Contains(search)) ||
                        (x.Remarks != null && x.Remarks.ToLower().Contains(search))
                    );
                }

                int filteredRecords = query.Count();

                // Sorting
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                var sortDirection = Request["sSortDir_0"];

                switch (sortColumnIndex)
                {
                    case 0: query = sortDirection == "asc" ? query.OrderBy(x => x.DetailId) : query.OrderByDescending(x => x.DetailId); break;
                    case 1: query = sortDirection == "asc" ? query.OrderBy(x => x.SourceTable) : query.OrderByDescending(x => x.SourceTable); break;
                    case 2: query = sortDirection == "asc" ? query.OrderBy(x => x.TxnDate) : query.OrderByDescending(x => x.TxnDate); break;
                    case 3: query = sortDirection == "asc" ? query.OrderBy(x => x.Department) : query.OrderByDescending(x => x.Department); break;
                    case 4: query = sortDirection == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name); break;
                    case 5: query = sortDirection == "asc" ? query.OrderBy(x => x.AccountNo) : query.OrderByDescending(x => x.AccountNo); break;
                    case 6: query = sortDirection == "asc" ? query.OrderBy(x => x.Amount) : query.OrderByDescending(x => x.Amount); break;
                    case 7: query = sortDirection == "asc" ? query.OrderBy(x => x.Scheme) : query.OrderByDescending(x => x.Scheme); break;
                    case 8: query = sortDirection == "asc" ? query.OrderBy(x => x.Status) : query.OrderByDescending(x => x.Status); break;
                    case 9: query = sortDirection == "asc" ? query.OrderBy(x => x.TransactionReference) : query.OrderByDescending(x => x.TransactionReference); break;
                    case 10: query = sortDirection == "asc" ? query.OrderBy(x => x.TransactionDate) : query.OrderByDescending(x => x.TransactionDate); break;
                    default: query = query.OrderByDescending(x => x.TxnDate); break;
                }

                // Pagination
                var displayedData = query.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

                // Format for DataTable (array of arrays)
                var result = displayedData.Select(c => new[]
                {
                    c.DetailId.ToString(),
                    //c.SourceTable ?? "",
                    c.TxnDate.HasValue ? c.TxnDate.Value.ToString("dd/MM/yyyy") : "",
                    c.Department ?? "",
                    c.Name ?? "",
                    c.AccountNo ?? "",
                    c.Amount.HasValue ? c.Amount.Value.ToString("N2") : "0.00",
                    c.Scheme ?? "",
                    c.Status ?? "",
                    c.TransactionReference ?? "",
                    c.TransactionDate ?? "",
                    c.Remarks ?? "",
                    c.HeaderId.ToString()
                });

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = filteredRecords,
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string[]>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            var header = db.TxnHeaders.Find(id);
            if (header == null)
            {
                return HttpNotFound();
            }

            var details = db.TxnDetails.Where(d => d.HeaderId == id).ToList();

            ViewBag.Header = header;
            ViewBag.Details = details;

            return View();
        }

        // POST: Transaction/ExportToExcel
        [HttpPost]
        public ActionResult ExportToExcel(string txnDateFrom, string txnDateTo, string department, string status, string scheme, string searchAppRef, string searchAccNo, string searchTxnRef, string downloadToken)
        {
            try
            {
                var query = from d in db.TxnDetails.AsNoTracking()
                            join h in db.TxnHeaders.AsNoTracking() on d.HeaderId equals h.HeaderId
                            select new TransactionDetailViewModel
                            {
                                DetailId = d.DetailId,
                                HeaderId = d.HeaderId,
                                SourceTable = h.SourceTable,
                                TxnDate = h.TxnDate,
                                ImportedOn = h.ImportedOn,
                                ApplicationReferenceNo = d.ApplicationReferenceNo,
                                ApplicationReferenceNo1 = d.ApplicationReferenceNo1,
                                Department = d.Department,
                                DepartmentAccountNo = d.DepartmentAccountNo,
                                Amount = d.Amount,
                                DateText = d.DateText,
                                DepartmentBankName = d.DepartmentBankName,
                                DepartmentBankIFSC = d.DepartmentBankIFSC,
                                Name = d.Name,
                                IFSC = d.IFSC,
                                AccountNo = d.AccountNo,
                                Scheme = d.Scheme,
                                Status = d.Status,
                                TransactionReference = d.TransactionReference,
                                TransactionDate = d.TransactionDate,
                                Remarks = d.Remarks
                            };

                // Apply same filters as AjaxHandler
                if (!string.IsNullOrEmpty(txnDateFrom))
                {
                    DateTime fromDate;
                    if (DateTime.TryParseExact(txnDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
                    {
                        query = query.Where(x => x.TxnDate >= fromDate);
                    }
                }
                if (!string.IsNullOrEmpty(txnDateTo))
                {
                    DateTime toDate;
                    if (DateTime.TryParseExact(txnDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                    {
                        toDate = toDate.AddDays(1);
                        query = query.Where(x => x.TxnDate < toDate);
                    }
                }
                if (!string.IsNullOrEmpty(department))
                    query = query.Where(x => x.Department == department);
                if (!string.IsNullOrEmpty(status))
                    query = query.Where(x => x.Status == status);
                if (!string.IsNullOrEmpty(scheme))
                    query = query.Where(x => x.Scheme == scheme);
                if (!string.IsNullOrEmpty(searchAppRef))
                    query = query.Where(x => x.ApplicationReferenceNo.Contains(searchAppRef) || x.ApplicationReferenceNo1.Contains(searchAppRef));
                if (!string.IsNullOrEmpty(searchAccNo))
                    query = query.Where(x => x.AccountNo.Contains(searchAccNo));
                if (!string.IsNullOrEmpty(searchTxnRef))
                    query = query.Where(x => x.TransactionReference.Contains(searchTxnRef));

                var data = query.OrderByDescending(x => x.TxnDate).ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Transactions");

                    // Headers
                    var headers = new[] { "Detail ID", "Source Table", "Txn Date", "Imported On", "App Ref No", "Department", "Dept Account No", "Amount", "Name", "Account No", "IFSC", "Scheme", "Status", "Transaction Ref", "Transaction Date", "Department Bank", "Dept Bank IFSC", "Remarks" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = headers[i];
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // Data rows
                    int row = 2;
                    foreach (var item in data)
                    {
                        worksheet.Cell(row, 1).Value = item.DetailId;
                        worksheet.Cell(row, 2).Value = item.SourceTable ?? "";
                        worksheet.Cell(row, 3).Value = item.TxnDate.HasValue ? item.TxnDate.Value.ToString("dd/MM/yyyy") : "";
                        worksheet.Cell(row, 4).Value = item.ImportedOn.HasValue ? item.ImportedOn.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        worksheet.Cell(row, 5).Value = item.ApplicationReferenceNo ?? "";
                        worksheet.Cell(row, 6).Value = item.Department ?? "";
                        worksheet.Cell(row, 7).Value = item.DepartmentAccountNo ?? "";
                        worksheet.Cell(row, 8).Value = item.Amount ?? 0;
                        worksheet.Cell(row, 9).Value = item.Name ?? "";
                        worksheet.Cell(row, 10).Value = item.AccountNo ?? "";
                        worksheet.Cell(row, 11).Value = item.IFSC ?? "";
                        worksheet.Cell(row, 12).Value = item.Scheme ?? "";
                        worksheet.Cell(row, 13).Value = item.Status ?? "";
                        worksheet.Cell(row, 14).Value = item.TransactionReference ?? "";
                        worksheet.Cell(row, 15).Value = item.TransactionDate ?? "";
                        worksheet.Cell(row, 16).Value = item.DepartmentBankName ?? "";
                        worksheet.Cell(row, 17).Value = item.DepartmentBankIFSC ?? "";
                        worksheet.Cell(row, 18).Value = item.Remarks ?? "";
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        string fileName = "Transactions_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                        // Set cookie so client-side knows the download is complete
                        if (!string.IsNullOrEmpty(downloadToken))
                        {
                            Response.SetCookie(new System.Web.HttpCookie("downloadToken", downloadToken) { Path = "/" });
                        }

                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error exporting to Excel: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult PrintTransactionMonitoring(string txnDateFrom, string txnDateTo, string department, string status, string scheme, string searchAppRef, string searchAccNo, string searchTxnRef)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT d.DetailId, h.SourceTable, h.TxnDate, h.ImportedOn, d.[Application Reference No#] as ApplicationReferenceNo, ");
                sql.Append("d.Department, d.Name, d.[Account No#] as AccountNo, d.Amount, d.Scheme, d.Status, ");
                sql.Append("d.TransactionReference, d.TransactionDate, d.Remarks, d.IFSC, d.[Department Account No#] as DepartmentAccountNo, ");
                sql.Append("d.[Department Bank Name] as DepartmentBankName, d.[Department Bank IFSC] as DepartmentBankIFSC ");
                sql.Append("FROM txnDetail d ");
                sql.Append("JOIN txnHeader h ON d.HeaderId = h.HeaderId ");
                sql.Append("WHERE 1=1 ");

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(txnDateFrom))
                {
                    DateTime fromDate;
                    if (DateTime.TryParseExact(txnDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
                    {
                        sql.Append(" AND h.TxnDate >= @txnDateFrom");
                        parameters.Add(new SqlParameter("@txnDateFrom", fromDate));
                    }
                }

                if (!string.IsNullOrEmpty(txnDateTo))
                {
                    DateTime toDate;
                    if (DateTime.TryParseExact(txnDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                    {
                        toDate = toDate.AddDays(1);
                        sql.Append(" AND h.TxnDate < @txnDateTo");
                        parameters.Add(new SqlParameter("@txnDateTo", toDate));
                    }
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql.Append(" AND d.Department = @department");
                    parameters.Add(new SqlParameter("@department", department));
                }

                if (!string.IsNullOrEmpty(status))
                {
                    sql.Append(" AND d.Status = @status");
                    parameters.Add(new SqlParameter("@status", status));
                }

                if (!string.IsNullOrEmpty(scheme))
                {
                    sql.Append(" AND d.Scheme = @scheme");
                    parameters.Add(new SqlParameter("@scheme", scheme));
                }

                if (!string.IsNullOrEmpty(searchAppRef))
                {
                    sql.Append(" AND (d.[Application Reference No#] LIKE @searchAppRef OR d.[Application Reference No#1] LIKE @searchAppRef)");
                    parameters.Add(new SqlParameter("@searchAppRef", "%" + searchAppRef + "%"));
                }

                if (!string.IsNullOrEmpty(searchAccNo))
                {
                    sql.Append(" AND d.[Account No#] LIKE @searchAccNo");
                    parameters.Add(new SqlParameter("@searchAccNo", "%" + searchAccNo + "%"));
                }

                if (!string.IsNullOrEmpty(searchTxnRef))
                {
                    sql.Append(" AND d.TransactionReference LIKE @searchTxnRef");
                    parameters.Add(new SqlParameter("@searchTxnRef", "%" + searchTxnRef + "%"));
                }

                sql.Append(" ORDER BY h.TxnDate DESC");

                DataSet ds = new DataSet();
                string conString = ConnectionStringProvider.GetConnectionString();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        if (parameters.Count > 0)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds, "DataTable1");
                    }
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.HttpContext.Session["ReportName"] = "rptTransactionMonitoring.rpt";
                    this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptTransactionMonitoring.rpt"));
                    this.HttpContext.Session["rptSource"] = ds;
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "No records found for the selected filters." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error generating report: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
