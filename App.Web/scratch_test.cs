using System;
using System.IO;
using System.Data;
using ExcelDataReader;

class Program
{
    static void Main()
    {
        string textFile = "test.xls";
        File.WriteAllText(textFile, "Col1\tCol2\tCol3\r\nVal1\tVal2\tVal3");

        try
        {
            using (var stream = File.Open(textFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });
                    Console.WriteLine("Columns: " + result.Tables[0].Columns.Count);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }
}
