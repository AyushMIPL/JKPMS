using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa
   public class DVOUpdateInventoryTransfersStitrane :DVOBase
   {

   #region [variable Declaration]
       private int    _rowid;
       private int     _doc_no;
       private string  _doc_date;
       private string  _tran_no;
       private string  _tran_desc;
       private string  _ok_post;

       private int _insertby;
       private DateTime _insertdate;
       private string _insertmachineinfo;
       private int _updateby;
       private DateTime _updatedate;
       private string _updatemachineinfo;
   #endregion [Variable Declaration]

   #region [Constructor]
       public DVOUpdateInventoryTransfersStitrane()
       {
           _rowid = 0;
       _doc_no=0;
       _doc_date=string.Empty;
       _tran_no=string.Empty;
       _tran_desc=string.Empty;
       _ok_post=string.Empty;


       _insertby = 0;
       _insertdate = DateTime.Now;
       _insertmachineinfo = string.Empty;
       _updateby = 0;
       _updatedate = DateTime.Now;
       _updatemachineinfo = string.Empty;

   }
  #endregion [Constructor]

   #region [Public Properties]
       public int rowid
       {
           get { return _rowid; }
           set { _rowid  = value; }
       }
       public int doc_no
       {
           get { return _doc_no;}
           set {_doc_no=value;}
       }
       public string doc_date
       {
           get { return _doc_date ;}
           set { _doc_date=value;}
       }
       public string tran_no
       {
           get { return _tran_no ;}
           set { _tran_no=value;}
       }
       public string tran_desc
       {
           get { return _tran_desc ;}
           set { _tran_desc=value;}
       }
       public string ok_post
       {
           get { return _ok_post ;}
           set { _ok_post=value;}
       }

       public int insertby
       {
           get { return _insertby; }
           set { _insertby = value; }
       }
       public DateTime insertdate
       {
           get { return _insertdate; }
           set { _insertdate = value; }
       }
       public string insertmachineinfo
       {
           get { return _insertmachineinfo; }
           set { _insertmachineinfo = value; }
       }
       public int updateby
       {
           get { return _updateby; }
           set { _updateby = value; }
       }
       public DateTime updatedate
       {
           get { return _updatedate; }
           set { _updatedate = value; }
       }
       public string updatemachineinfo
       {
           get { return _updatemachineinfo; }
           set { _updatemachineinfo = value; }
       }

   #endregion [Public properties]

   #region [Stored-Procedures]

       public override string INSERT_SPNAME
        {
            get { return "uspinvtransferins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvtransinfoupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvtransinfodel"; }
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
            get { return "stitrane"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append(" select doc_no ,doc_date ,tran_no,tran_desc ,ok_post,rowid ");
            sql.Append(" from stitrane ");
            sql.Append(" where 1=1  ");
            sql.Append(" ");
            sql.Append(" ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0].ToString());

            if (parameters[1].ToString() != string.Empty && Convert.ToDateTime(parameters[1].ToString()) != Convert.ToDateTime("01/01/1900"))
                //if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
                sql.Append(" and doc_date ='" + parameters[1].ToString().Trim()+ "'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(tran_no) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(tran_desc) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND ok_post ='" + parameters[4].ToString().Trim() + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND rowid = " + parameters[5].ToString());

            return sql.ToString();
        }

          #endregion [Stored-Procedures]
    


    }
}
