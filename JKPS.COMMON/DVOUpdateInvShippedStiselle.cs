using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdateInvShippedStiselle : DVOBase
    {
        
      private string _order_no;
      private string _doc_date;
      private string _ok_post;
      private string _sell_desc;
      private int    _doc_no;
      private string _cust_code;

      private int _rowid;
      private decimal _extension;
      private decimal _qty_on_hand;

      private string _InsertMachineInfo;
      private DateTime _InsertDate;
      private int _InsertBy;

      private string _UpdateMachineInfo;
      private DateTime _UpdateDate;
      private int _UpdateBy;


      public DVOUpdateInvShippedStiselle()
      {
        _order_no=string.Empty;
        _doc_date="01/01/1900";
        _ok_post=string.Empty;
        _sell_desc=string.Empty;
        _doc_no=0;
        _cust_code=string.Empty;
        _rowid = 0;
        _extension = 0.0M;
        _qty_on_hand = 0.0M;

        _InsertMachineInfo = "App";
        _InsertDate = DateTime.Now;
        _InsertBy = -1;
        _UpdateMachineInfo = "App";
        _UpdateDate = DateTime.Now;
        _UpdateBy = -1;

      }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
      public string order_no
      {
          get { return _order_no; }
          set { _order_no = value; }
      }

      public string doc_date
      {
          get { return _doc_date; }
          set { _doc_date = value; }
      }
      public string ok_post
      {
          get { return _ok_post; }
          set { _ok_post = value; }
      }
      public string sell_desc
      {
          get { return _sell_desc; }
          set { _sell_desc = value; }
      }
      public int doc_no
      {
          get { return _doc_no; }
          set { _doc_no = value; }
      }
      public string cust_code
      {
          get { return _cust_code; }
          set { _cust_code = value; }
      }
      
        public decimal extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        public decimal qty_on_hand
        {
            get { return _qty_on_hand; }
            set { _qty_on_hand = value; }
        }


        //Properties used for only SQL Server

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
            get { return _UpdateDate; }
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
            get { return "uspinvshippedins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvStippedUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvShippeddel"; }
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
            get { return "stiselle"; }
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

        public string INSERT_INV_SHIPPED_DTL_INFO
        {
            get { return "uspinvshipdtlins"; }//uspinvshippeddtlins
        }

        public string GET_INV_SHIPPED_DTL_INFO
        {
            get { return "uspinvshippdtlget"; }
        }
        public string UPD_INV_SHIPPED_DTL_INFO
        {
            get { return "uspinvStipeddtlUpd"; }
        }
        public string DELETE_GROUP_DETAIL
        {
            get { return "uspinvShipeddtldel"; }
        }


        public string GET_INFO
        {
            get { return "uspstilocardtlget"; }
        }
      
        //
        public string GET_INV_SHIPPED_INFO
        {
            get { return "uspinvntryshipdget"; }
        }

        public string UPDATE_POST_NO_STICNTRC
        {
            get { return "uspupdsticntrc"; }
        }
        public string UPDATE_STISELLE_ROWID
        {
            get { return "uspupdstisellerid"; }
        }
        public string UPDATE_STISELLE_DOCNO
        {
            get { return "uspupdstiselledcno"; }
        }


        public string GET_WAREHOUSE_STIWHSER
        {
            get { return ""; }
        }

        public string UPD_ISELLE_OK_POST
        {
            get { return "uspstiselleupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select doc_no,doc_date,order_no,sell_desc,cust_code,ok_post,rowid from stiselle where 1=1 ");
           

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(order_no) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[1].ToString() != string.Empty && Convert.ToDateTime(parameters[1].ToString()) != Convert.ToDateTime("01/01/1900"))
            //if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
                sql.Append(" and doc_date >='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND ok_post '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
           
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sell_desc) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
           
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND doc_no = " + parameters[4].ToString());



            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(cust_code) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
           

            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND rowid = " + parameters[6].ToString());





            return sql.ToString();
        }

        #endregion Stored-Procedures

    }
}
