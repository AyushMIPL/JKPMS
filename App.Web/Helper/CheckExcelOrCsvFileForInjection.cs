using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Web;

namespace App.Web.Helper
{
    public static class CheckExcelOrCsvFileForInjection
    {
        private static readonly int MaxFileSizeToUploadInMB;
        static CheckExcelOrCsvFileForInjection()
        {
            MaxFileSizeToUploadInMB = int.TryParse(ConfigurationManager.AppSettings["MaxFileSizeToUploadInMB"], out var fileSize) ? fileSize : 50;
        }
        public static string VerifyFile(HttpPostedFileBase file)
        {
            string isValidExcelFile = "Valid";
            if (file != null && file.ContentLength > 0)
            {
                double fileSizeInMegaBytes = GetFileSizeInMB(file.ContentLength);
                if (fileSizeInMegaBytes > MaxFileSizeToUploadInMB)
                {
                    return "Uploaded file size should be smaller than " + MaxFileSizeToUploadInMB + " MB";
                }

                string fileExtension = System.IO.Path.GetExtension(file.FileName);

                if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                    || fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase)
                    || fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    // check for header verification and column data(formula injection)
                    try
                    {
                        // Reading Excel file using ClosedXML
                        using (var stream = file.InputStream)
                        {
                            DataTable dataTable = new DataTable();
                            using (var workbook = new XLWorkbook(stream))
                            {
                                int TotalWorksheetCounts = workbook.Worksheets.Count;
                                if (TotalWorksheetCounts > 1)
                                {
                                    isValidExcelFile = "Excel file has more than 1 worksheet. Please delete other worksheets to proceed.";
                                    return isValidExcelFile;
                                }
                                var worksheet = workbook.Worksheet(1);

                                bool firstRow = true;
                                foreach (var row in worksheet.RowsUsed())
                                {
                                    // Skip empty rows
                                    if (row.IsEmpty())
                                    {
                                        continue;
                                    }

                                    if (firstRow)
                                    {
                                        foreach (var cell in row.CellsUsed())
                                        {
                                            dataTable.Columns.Add(cell.GetString());
                                        }
                                        firstRow = false;
                                    }
                                    else
                                    {
                                        var dataRow = dataTable.NewRow();
                                        int i = 0;

                                        foreach (var cell in row.CellsUsed())
                                        {
                                            dataRow[i] = cell.Value.ToString() ?? string.Empty;
                                            i++;
                                        }

                                        dataTable.Rows.Add(dataRow);
                                    }
                                }

                                // verify data in excel
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    // verify headers
                                    DataRow dr = dataTable.Rows[0];
                                    string isHeaderVerified = isHeaderRowVerified(dataTable);
                                    if (isHeaderVerified != "Verified")
                                    {
                                        isValidExcelFile = isHeaderVerified;
                                        return isValidExcelFile;
                                    }

                                    // verify data for formula injection

                                    // Iterate through all rows and columns in the worksheet
                                    bool isFormula = false;
                                    foreach (var row in worksheet.RowsUsed())
                                    {
                                        foreach (var cell in row.CellsUsed())
                                        {
                                            // Check if the cell contains a formula
                                            if (!string.IsNullOrWhiteSpace(cell.FormulaA1) &&
                                                (cell.FormulaA1.Trim().ToLower().StartsWith("=cmd")
                                                || cell.FormulaA1.Trim().ToLower().StartsWith("cmd")))
                                            {
                                                isFormula = true;
                                                break;
                                            }
                                        }

                                        if (isFormula)
                                            break;
                                    }

                                    if (isFormula)
                                    {
                                        return "Invalid data in excel. Please check and reupload again.";
                                    }

                                    string isRowVerified = isRowVerifiedForFormulaInjection(dataTable);
                                    if (isRowVerified != "true")
                                    {
                                        isValidExcelFile = isRowVerified;
                                        return isValidExcelFile;
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isValidExcelFile = "Excel file is not valid. Please verify and reupload again";
                        return isValidExcelFile;
                    }
                }
                else
                {
                    isValidExcelFile = "Not a valid excel file. Please check and reupload again.";
                    return isValidExcelFile;
                }
            }

            return isValidExcelFile;
        }

        public static string isHeaderRowVerified(DataTable columnRow)
        {
            string HeaderRowVerificationMessage = "Verified";
            bool isHeaderRowVerified = true;
            try
            {
                int totalColumnCount = columnRow.Columns.Count;
                if (totalColumnCount == 47)
                {
                    for (int i = 0; i < 47; i++)
                    {
                        string columnValue = columnRow.Columns[i].ColumnName.Trim();
                        switch (i)
                        {
                            case 0:
                                if (columnValue != "S.No")
                                    isHeaderRowVerified = false;
                                break;
                            case 1:
                                if (columnValue != "Application Reference No")
                                    isHeaderRowVerified = false;
                                break;
                            case 2:
                                if (columnValue != "Submission Location")
                                    isHeaderRowVerified = false;
                                break;
                            case 3:
                                if (columnValue != "Submission Date")
                                    isHeaderRowVerified = false;
                                break;
                            case 4:
                                if (columnValue != "Applied By")
                                    isHeaderRowVerified = false;
                                break;
                            case 5:
                                if (columnValue != "Select Tehsil Social Welfare Office (TSWO)")
                                    isHeaderRowVerified = false;
                                break;
                            case 6:
                                if (columnValue != "Select District")
                                    isHeaderRowVerified = false;
                                break;
                            case 7:
                                if (columnValue != "Name of the Applicant")
                                    isHeaderRowVerified = false;
                                break;
                            case 8:
                                if (columnValue != "Date of Birth")
                                    isHeaderRowVerified = false;
                                break;
                            case 9:
                                if (columnValue != "Age (In Years)")
                                    isHeaderRowVerified = false;
                                break;
                            case 10:
                                if (columnValue != "Mobile Number")
                                    isHeaderRowVerified = false;
                                break;
                            case 11:
                                if (columnValue != "Do you have BPL card")
                                    isHeaderRowVerified = false;
                                break;
                            case 12:
                                if (columnValue != "Father / Husband / Guardian Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 13:
                                if (columnValue != "E-Mail")
                                    isHeaderRowVerified = false;
                                break;
                            case 14:
                                if (columnValue != "Category")
                                    isHeaderRowVerified = false;
                                break;
                            case 15:
                                if (columnValue != "Gender")
                                    isHeaderRowVerified = false;
                                break;
                            case 16:
                                if (columnValue != "Present Address")
                                    isHeaderRowVerified = false;
                                break;
                            case 17:
                                if (columnValue != "Present District")
                                    isHeaderRowVerified = false;
                                break;
                            case 18:
                                if (columnValue != "Present Village Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 19:
                                if (columnValue != "Pincode")
                                    isHeaderRowVerified = false;
                                break;
                            case 20:
                                if (columnValue != "Present Halqa Panchayat / Municipality Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 21:
                                if (columnValue != "Present Tehsil")
                                    isHeaderRowVerified = false;
                                break;
                            case 22:
                                if (columnValue != "Permanent Address")
                                    isHeaderRowVerified = false;
                                break;
                            case 23:
                                if (columnValue != "Permanent District")
                                    isHeaderRowVerified = false;
                                break;
                            case 24:
                                if (columnValue != "Permanent Tehsil")
                                    isHeaderRowVerified = false;
                                break;
                            case 25:
                                if (columnValue != "Permanent Halqa Panchayat / Municipality Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 26:
                                if (columnValue != "Permanent Village Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 27:
                                if (columnValue != "Branch Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 28:
                                if (columnValue != "IFSC Code")
                                    isHeaderRowVerified = false;
                                break;
                            case 29:
                                if (columnValue != "Account No. of the Applicant")
                                    isHeaderRowVerified = false;
                                break;
                            case 30:
                                if (columnValue != "Bank Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 31:
                                if (columnValue != "Select Pension Type")
                                    isHeaderRowVerified = false;
                                break;
                            case 32:
                                if (columnValue != "Percentage of Disability")
                                    isHeaderRowVerified = false;
                                break;
                            case 33:
                                if (columnValue != "Civil Condition")
                                    isHeaderRowVerified = false;
                                break;
                            case 34:
                                if (columnValue != "Are you previously taking Pension from JK-ISSS / GOI-NSAP")
                                    isHeaderRowVerified = false;
                                break;
                            case 35:
                                if (columnValue != "Bank Name.")
                                    isHeaderRowVerified = false;
                                break;
                            case 36:
                                if (columnValue != "Branch Name.")
                                    isHeaderRowVerified = false;
                                break;
                            case 37:
                                if (columnValue != "IFSC Code.")
                                    isHeaderRowVerified = false;
                                break;
                            case 38:
                                if (columnValue != "Account Number.")
                                    isHeaderRowVerified = false;
                                break;
                            case 39:
                                if (columnValue != "Application Sanctioned under Scheme Name")
                                    isHeaderRowVerified = false;
                                break;
                            case 40:
                                if (columnValue != "Current Task")
                                    isHeaderRowVerified = false;
                                break;
                            case 41:
                                if (columnValue != "Current Status")
                                    isHeaderRowVerified = false;
                                break;
                            case 42:
                                if (columnValue != "Last Task")
                                    isHeaderRowVerified = false;
                                break;
                            case 43:
                                if (columnValue != "Version No")
                                    isHeaderRowVerified = false;
                                break;
                            case 44:
                                if (columnValue != "Last_pay_date")
                                    isHeaderRowVerified = false;
                                break;
                            case 45:
                                if (columnValue != "Application_approve_on")
                                    isHeaderRowVerified = false;
                                break;
                            case 46:
                                if (columnValue != "ActionOnDate")
                                    isHeaderRowVerified = false;
                                break;
                        }

                        if (!isHeaderRowVerified)
                            break;
                    }
                }
                else
                {
                    isHeaderRowVerified = false;
                }
            }
            catch
            {
                isHeaderRowVerified = false;
            }

            if (!isHeaderRowVerified)
            {
                HeaderRowVerificationMessage = "Excel column names are not valid. Please verify the column names and reupload again";
            }

            return HeaderRowVerificationMessage;
        }

        public static string isRowVerifiedForFormulaInjection(DataTable dt)
        {
            string isRowVerified = "true";
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    int totalColumnCount = row.Table.Columns.Count;
                    if (totalColumnCount == 47)
                    {
                        for (int i = 0; i < 47; i++)
                        {
                            string columnValue = GetSafeValue(row, i, "");
                            if (columnValue.StartsWith("=") || columnValue.StartsWith("+") || columnValue.StartsWith("-") || columnValue.StartsWith("@"))
                            {
                                isRowVerified = "Excel file is not valid.";
                            }

                            if (isRowVerified == "Excel file is not valid.")
                                break;
                        }

                        if (isRowVerified == "Excel file is not valid.")
                            break;
                    }
                    else
                    {
                        isRowVerified = "Excel file is not valid.";
                    }
                }

                return isRowVerified;
            }
            catch (Exception ex)
            {
                isRowVerified = ex.Message;
            }

            return isRowVerified;
        }

        public static string GetSafeValue(DataRow row, int columnIndex, string defaultValue = default)
        {
            try
            {
                if (columnIndex >= 0 && columnIndex < row.Table.Columns.Count && row[columnIndex] != DBNull.Value)
                {
                    return row[columnIndex].ToString().Trim();
                }
            }
            catch
            {
                return "Error retrieving value from column";
            }
            return defaultValue;
        }

        public static double GetFileSizeInMB(int fileSizeInBytes)
        {
            double fileSizeInMegabytes = 0;
            try
            {
                // Convert the file size to kilobytes 
                double fileSizeInKilobytes = fileSizeInBytes / 1024.0;

                // Convert the file size to megabytes 
                fileSizeInMegabytes = fileSizeInKilobytes / 1024.0;
            }
            catch
            {

            }

            return fileSizeInMegabytes;
        }

        public static string VerifyUploadValidationFile(HttpPostedFileBase file)
        {
            string isValidExcelFile = "Valid";
            if (file != null && file.ContentLength > 0)
            {
                double fileSizeInMegaBytes = GetFileSizeInMB(file.ContentLength);
                if (fileSizeInMegaBytes > MaxFileSizeToUploadInMB)
                {
                    return "Uploaded file size should be smaller than " + MaxFileSizeToUploadInMB + " MB";
                }

                string fileExtension = System.IO.Path.GetExtension(file.FileName);

                if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                    || fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase)
                    || fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    // check for header verification and column data(formula injection)
                    try
                    {
                        // Reading Excel file using ClosedXML
                        using (var stream = file.InputStream)
                        {
                            DataTable dataTable = new DataTable();
                            using (var workbook = new XLWorkbook(stream))
                            {
                                int TotalWorksheetCounts = workbook.Worksheets.Count;
                                if (TotalWorksheetCounts > 1)
                                {
                                    isValidExcelFile = "Excel file has more than 1 worksheet. Please delete other worksheets to proceed.";
                                    return isValidExcelFile;
                                }
                                var worksheet = workbook.Worksheet(1);

                                // Iterate through all rows and columns in the worksheet
                                bool isFormula = false;
                                foreach (var row in worksheet.RowsUsed())
                                {
                                    foreach (var cell in row.CellsUsed())
                                    {
                                        // Check if the cell contains a formula
                                        if (!string.IsNullOrWhiteSpace(cell.FormulaA1) &&
                                                (cell.FormulaA1.Trim().ToLower().StartsWith("=cmd")
                                                || cell.FormulaA1.Trim().ToLower().StartsWith("cmd")))
                                        {
                                            isFormula = true;
                                            break;
                                        }
                                    }

                                    if (isFormula)
                                        break;
                                }

                                if (isFormula)
                                {
                                    return "Invalid data in excel. Please check and reupload again.";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isValidExcelFile = "Excel file is not valid. Please verify and reupload again";
                        return isValidExcelFile;
                    }
                }
                else
                {
                    isValidExcelFile = "Only excel files are allowed.";
                    return isValidExcelFile;
                }
            }

            return isValidExcelFile;
        }
    }
}
