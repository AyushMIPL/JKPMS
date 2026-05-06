using System;
using System.IO;
using OfficeOpenXml;

namespace ExcelHeaderReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ReadHeaders(@"d:\Ayush\SourceCode\Official\JKPMS_Git\App.Web\DataFile\J_Uploads\JAMMU_20260428_103942.xlsx");
            ReadHeaders(@"d:\Ayush\SourceCode\Official\JKPMS_Git\App.Web\DataFile\J_MasterEmployeeDownloads\ContributionCSV_20260428_115522.xlsx");
        }

        static void ReadHeaders(string path)
        {
            try {
                using (var package = new ExcelPackage(new FileInfo(path)))
                {
                    var ws = package.Workbook.Worksheets[0];
                    int cols = ws.Dimension.End.Column;
                    Console.WriteLine("File: " + Path.GetFileName(path));
                    for(int i=1; i<=cols; i++) {
                        Console.Write(ws.Cells[1, i].Text + ",");
                    }
                    Console.WriteLine();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
