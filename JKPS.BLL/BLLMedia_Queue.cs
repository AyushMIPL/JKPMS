using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;

namespace JKPS.BLL
///<Development and modification Details>

{
  public class BLLMedia_Queue
  {
    public static int InsertMedia_Queue(ref DVOMedia_Queue objMedia_Queue)
    {

      object[] parameters = new object[7];
      parameters[0] = objMedia_Queue.RecordId;
      parameters[1] = objMedia_Queue.FilePath;
      parameters[2] = objMedia_Queue.IsUploaded;
      parameters[3] = objMedia_Queue.UploadedDate;
      parameters[4] = objMedia_Queue.IsUploaded;
      parameters[5] = objMedia_Queue.UploadErrors;
      parameters[6] = objMedia_Queue.IsReUploaded;
      parameters[7] = objMedia_Queue.MediaType;
      parameters[8] = objMedia_Queue.CreatedMachineInfo;
      parameters[9] = objMedia_Queue.CreatedBy;
      parameters[10] = objMedia_Queue.UNIQUE_ID;


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      int c = objDalBaseClass.InsertData(ref parameters, typeof(DVOMedia_Queue));
      return c;
    }
    public static List<DVOMedia_Queue> GetMedia_Queue(ref DVOMedia_Queue objMedia_Queue)
    {
      object[] Parameter = new object[2];
      Parameter[0] = objMedia_Queue.dateFrom;
      Parameter[1] = objMedia_Queue.dateto;
      List<DVOMedia_Queue> lstMedia_Queue = new List<DVOMedia_Queue>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOMedia_Queue)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOMedia_Queue obj = new DVOMedia_Queue();
          obj.Id = Convert.ToInt32(dr[0]);//""
          obj.RecordId = Convert.ToInt32(dr[1]);//""
          obj.FilePath = dr[2].ToString().Trim();//""
          obj.IsUploaded = Convert.ToBoolean(dr[5]);//""
          obj.UploadedDate = Convert.ToDateTime(dr[5]);//""
          obj.UploadErrors = dr[2].ToString().Trim();//""
          obj.MediaType = dr[3].ToString().Trim();//""
          obj.IsReUploaded = Convert.ToBoolean(dr[4].ToString().Trim());//""
          obj.CreatedOn = Convert.ToDateTime(dr[5]);//""


          lstMedia_Queue.Add(obj);

        }
        return lstMedia_Queue;
      }
    }
    public static int DeleteMedia_Queue(ref DVOMedia_Queue objMedia_QueueDel)
    {
      object[] Parameter = new object[1];
      Parameter[0] = objMedia_QueueDel.Id;
      //Parameter[1] = objActLogDel.dateFrom;
      //Parameter[2] = objActLogDel.dateto;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      int c = objDalBaseClass.DeleteData(ref Parameter, typeof(DVOMedia_Queue));
      return c;

    }

  }
}
