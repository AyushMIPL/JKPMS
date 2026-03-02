using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public class DVOAppHistory:DVOBase
    {
        private int  _doc_no;
        private  string _dateFrom;
        private  string _dateto;
        private string _user_id;


        public DVOAppHistory()
        {
            _doc_no = 0;
            _dateFrom = "01/01/1900";
            _dateto = "01/01/1900";
            _user_id = string.Empty;
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
           
        }
        public string dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
      public string dateto
      {
          get { return _dateto; }
          set { _dateto = value; }
      }
        public string user_id
        {get { return _user_id; }
         set { _user_id = value; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspAppHistoryIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspAppHistoryUpd "; }
        }
        public override string DELETE_SPNAME
        {
            get { return "uspAppHistoryDel"; }
        }
        public override string FIND_SPNAME
        {
            get { return "uspAppHistoryGet"; }
        }
        public override string ALL_SPNAME
        {
            get { return "uspAppHistoryGetAll"; }
        }


        public override string TABLE_NAME
        {
            get { return "inxapphst"; }
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
       

        public override string FIND_QUERY(ref Object[] parameters)
      {
          StringBuilder sql = new StringBuilder();
          sql.Append("SELECT  i.doc_no p_doc_no, i.user_id p_user_id, i.date p_date,i.time p_time,");
          sql.Append(" i.pre_app_level p_pre_app_level,i.post_app_level p_post_app_level");
          sql.Append(" FROM inxapphst i WHERE 1=1 ");

         
          if (Convert.ToInt32(parameters[0]) > 0)//Doc_No
              sql.Append("AND doc_no ='" + parameters[0].ToString() + "'");
             if (parameters[1] != null)
              if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                  sql.Append("AND date >= '" + parameters[1].ToString().Replace("'", "''") + "'");//dateFrom
          if (parameters[2] != null)
              if (parameters[2].ToString() != string.Empty && !parameters[2].ToString().Trim().Contains("1900"))
                  sql.Append(" AND date <= '" + parameters[2].ToString().Replace("'", "''") + "'");//dateTo

          if (parameters[3] != null)
              if (parameters[3].ToString() != string.Empty )
                  sql.Append(" AND Rtrim(user_id) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
          //if (Convert.ToInt32(parameters[1]) > 0 || !parameters[1].Contains("1900"))
          //    sql.Append(" AND date >= '" + parameters[1].ToString().Replace("'", "''") + "'");//dateFrom
          //else
          //    sql.Append(" date>= '" + parameters[2].ToString().Replace("'", "''") + "'");
          //sql.Append(" AND date <= '" + parameters[3].ToString().Replace("'", "''") + "'");//DateTo
          //if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//UserID
          //    sql.Append(" AND Rtrim(user_id) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
          return sql.ToString();
         
      }
    
    }
}
