using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
///<Development and modification Details>

{
  public class BLLMediaDownloads
  {
    public static int InsertMediaDownloads(ref DVOMediaDownloads objMediaDownloads)
    {

      object[] parameters = new object[7];
      parameters[0] = objMediaDownloads.FileName;
      parameters[1] = objMediaDownloads.HasDownoaded;
      parameters[2] = objMediaDownloads.IsProcessed;
      parameters[3] = objMediaDownloads.MediaType;
      parameters[4] = objMediaDownloads.CreatedMachineInfo;
      parameters[5] = objMediaDownloads.CreatedBy;
      parameters[6] = objMediaDownloads.UNIQUE_ID;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOMediaDownloads));
      return c;
    }
    public static List<DVOMediaDownloads> GetMediaDownloads(ref DVOMediaDownloads objMediaDownloads)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objMediaDownloads.dateFrom;
      Parameter[1] = objMediaDownloads.dateto;
      List<DVOMediaDownloads> lstMediaDownloads = new List<DVOMediaDownloads>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMediaDownloads)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOMediaDownloads obj = new DVOMediaDownloads();
          obj.Id = Convert.ToInt32(dr[0]);//""
          obj.FileName = dr[1].ToString().Trim();//""
          obj.HasDownoaded = Convert.ToBoolean(dr[2].ToString().Trim());//""
          obj.IsProcessed = Convert.ToBoolean(dr[3].ToString().Trim());//""
          obj.MediaType = dr[5].ToString().Trim();//""
          obj.IsActive = Convert.ToBoolean(dr[4].ToString().Trim());//""
          obj.CreatedOn = Convert.ToDateTime(dr[5]);//""
          obj.CreatedMachineInfo = dr[5].ToString().Trim();//""
          lstMediaDownloads.Add(obj);
        }
        return lstMediaDownloads;
      }
    }
    public static int DeleteMediaDownloads(ref DVOMediaDownloads objMediaDownloadsDel)
    {
      object[] Parameter = new object[1];
      Parameter[0] = objMediaDownloadsDel.Id;
      //Parameter[1] = objActLogDel.dateFrom;
      //Parameter[2] = objActLogDel.dateto;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      int c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOMediaDownloads));
      return c;

    }

  }
}
