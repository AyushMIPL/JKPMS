using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public  class DVOBatchInfo:DVOBase
    {
       private int _batch_id  ;
       private string  _batch_type ;
       private string _batch_status;
       private string _owner;
       private string _created_by;
       private string _create_datefrom ;
      private string _create_dateto;
       private string _approved_by;
      private string _approve_datefrom;
      private string _approve_dateto;
       private string  _posted_by;

      private string _post_datefrom;
      private string _post_dateto;
     



 
 



      public DVOBatchInfo()
      { 
          _batch_id=0; 
          _batch_type=string.Empty;
          _batch_status= string.Empty;
          _owner= string.Empty;
          _created_by =string.Empty;
          _create_datefrom ="1/2/1900";
          _create_dateto ="1/2/1900";
          _approved_by =string.Empty;
          _approve_datefrom ="1/2/1900";
          _approve_dateto ="1/2/1900";
          _posted_by=string.Empty; 
          _post_datefrom ="1/2/1900";
          _post_dateto = "1/2/1900";
         
       
      }

      public int batch_id
      {
          get { return _batch_id; }
          set { _batch_id = value; }
      }
      public string batchtype
      {
          get { return _batch_type; }
          set { _batch_type = value; }
      
      }
      public string status
      {
          get { return _batch_status; }
          set { _batch_status = value; }
      }
      public string Owner
      {
          get { return _owner; }
          set { _owner = value; }
      }
      public string createdby
      {
          get { return _created_by; }
          set { _created_by = value; }
      }
      public string CreateDateFrom
      {
          get { return _create_datefrom; }
          set { _create_datefrom = value; }
      }
      public string CreateDateTo
      {
          get { return _create_dateto; }
          set { _create_dateto = value; }

      }
      public string ApprovedBy
      {
          get { return _approved_by; }
          set { _approved_by = value; }
         

      }
      public string approvdate_from
      {
          get { return _approve_datefrom; }
          set { _approve_datefrom = value; }
      }
      public string approvdate_to
      {
          get { return _approve_dateto; }
          set { _approve_dateto = value; }
      }
      public string postedBy
      {
          get { return _posted_by; }
          set { _posted_by = value; }
      }
      public string postdate_from
      {
          get { return _post_datefrom; }
          set{_post_datefrom = value;}
          
        }
      public string postdate_to
      {
          get { return _post_dateto; }
          set { _post_dateto = value; }
      }
    
     
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspBatchInfoGet"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }



        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            
           sql.Append ("SELECT  batch_id, batch_type, batch_status, owner, created_by,");
           sql.Append ("create_date, create_time, approved_by, approve_date, approve_time,");
           sql.Append("posted_by, post_date, post_time, post_seq,total_trx");
           sql.Append (" FROM  stxbtchh");
           sql.Append (" where 1=1");
       if(Convert.ToInt32(parameters[0])>0)
           sql.Append(" and batch_id='" + parameters[0].ToString().Replace("'", "''") + "'");
       if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
           sql.Append(" AND Rtrim(batch_type)='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//
           sql.Append(" AND Rtrim(batch_status)='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//
           sql.Append(" AND Rtrim(owner)='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[4].ToString() != string.Empty && parameters[4].ToString().Trim() != null)//
           sql.Append(" AND Rtrim(created_by)='" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//
           sql.Append(" and create_date>='" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)//
           sql.Append(" and create_date<='" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//
           sql.Append(" AND Rtrim(approved_by)='" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
       //if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//
       //    sql.Append(" and approve_date>='" + parameters[8].ToString().Trim() + "'");
       //if (parameters[9].ToString() != string.Empty && parameters[9].ToString() != null)//
       //    sql.Append(" and approve_date<='" + parameters[9].ToString().Trim() + "'");
       if (parameters[10].ToString() != string.Empty && parameters[10].ToString() != null)//
           sql.Append(" AND Rtrim(posted_by)='" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[11].ToString() != string.Empty && parameters[11].ToString() != null)//
           sql.Append(" and post_date>='" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
       if (parameters[12].ToString() != string.Empty && parameters[12].ToString() != null)//
           sql.Append(" and post_date<='" + parameters[12].ToString().Trim().Replace("'", "''") + "'");

       return sql.ToString();

        }
    }
}
