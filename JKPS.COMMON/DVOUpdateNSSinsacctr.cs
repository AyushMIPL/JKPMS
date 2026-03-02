using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdateNSSinsacctr : DVOBase
    {
        private int _contract_no;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

       public DVOUpdateNSSinsacctr()
       {
           _contract_no = 0;
           _InsertMachineInfo = "App";
           _InsertDate = DateTime.Now;
           _InsertBy = -1;
           _UpdateMachineInfo = "App";
           _UpdateDate = DateTime.Now;
           _UpdateBy = -1;
       }
       //Properties used for only SQL Server
        public int contract_no
        {
            get { return _contract_no; }
            set { _contract_no = value; }
        }

       public string InsertMachineInfo
       {
           get { return _InsertMachineInfo; }
           set { _InsertMachineInfo = value; }
       }
       public DateTime InsertDate
       {
           get { return _InsertDate; }
           set { _InsertDate = value; }
       }
       public int InsertBy
       {
           get { return _InsertBy; }
           set { _InsertBy = value; }
       }
       public string UpdateMachineInfo
       {
           get { return _UpdateMachineInfo; }
           set { _UpdateMachineInfo = value; }
       }
       public DateTime UpdateDate
       {
           get
           {
               return _UpdateDate;
           }
           set { _UpdateDate = value; }
       }
       public int UpdateBy
       {
           get { return _UpdateBy; }
           set { _UpdateBy = value; }
       }

       #region Stored-Procedures

       public override string INSERT_SPNAME
       {
           get { return ""; }
       }

       public override string UPDATE_SPNAME
       {
           get { return ""; }
       }

       public override string DELETE_SPNAME
       {
           get { return ""; }
       }

       public override string FIND_SPNAME
       {
           get { return ""; }
       }

       public override string ALL_SPNAME
       {
           get { return ""; }
       }
       public override string TABLE_NAME
       {
           get { return "insacctr"; }
       }

       public override int UNIQUE_ID
       {
           get { return _contract_no; }
       }

       public override string NOTES_TABLE_RECORD_ID
       {
           get { return string.Empty; }
           set { throw new Exception("The method or operation is not implemented."); }
       }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
        }
       #endregion Stored-Procedures

   }
}
