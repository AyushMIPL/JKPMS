using System.Web;

namespace App.Web.Helper
{
  public static class CheckMimeType
  {
    public static bool IsCsvFile(HttpPostedFileBase file)
    {
      if (file.ContentType != "text/csv")
      {
        return false;
      }

      // Optionally, perform additional content checks here
      return true;
    }

    public static bool IsExcelFile(HttpPostedFileBase file)
    {
      if (file.ContentType != "application/vnd.ms-excel" && file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
      {
        return false;
      }

      return true;
    }
  }
}
