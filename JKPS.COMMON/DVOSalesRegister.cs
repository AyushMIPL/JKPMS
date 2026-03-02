using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 13 July 2009
    /// Description :common class for Sales history Report
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOSalesRegister:DVOBase
    {
        private int _Rowid;
        private string _start_date;
        private string _end_date;
        private string _item_class;
        private string _item_code;
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;
        

        #region Constructor
        public DVOSalesRegister()
        {
            _Rowid = 0;
            _start_date = string.Empty;
            _end_date = string.Empty;
            _item_class = string.Empty;
            _item_code = string.Empty;
            _Insertby=0;
            _InsertDate=Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo=string.Empty;
            _UpdateBy=0;
            _Updatedate=Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo=string.Empty;
        }
        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public string item_class
        {
            get { return _item_class; }
            set { _item_class = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        } 
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        #endregion public properties
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
            get { return ""; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }


        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stoinvce.currency_code,stoinvce.currency_rate,stoinvce.frght_amount,stoinvce.inv_date,stoinvce.inv_doc_no,stoinvce.inv_no,");
            sql.Append("stoinvce.tax_amount, stoshipd.doc_no,stoshipd.gross_margin, stoshipd.item_cost,stoshipd.net_amount, stoshipd.sell_to_code,stoshipd.ship_qty from stoinvce, stoshipd  where 1=1");
            sql.Append(" and stoinvce.inv_doc_no = stoshipd.inv_doc_no and stoinvce.stage = 'PST'");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//start_date
                sql.Append(" and stoinvce.inv_date >= '" + parameters[0].ToString() + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//end_date
                sql.Append(" and stoinvce.inv_date <= '" + parameters[1].ToString() + "'");
            sql.Append(" order by stoinvce.inv_date, stoinvce.inv_doc_no");
            return sql.ToString();
        }
        //Query for Product Summary
        public string FIND_QUERY_2(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_class, stoinvce.currency_code, stoinvce.currency_rate,");
            sql.Append("stoshipd.doc_no, stoshipd.gross_margin, stoshipd.net_amount,stoshipd.tax_amount, stxinfor.src_desc,'"+parameters[0]+ "' as start_date,'"+parameters[1]+ "' as end_date from stoinvce,stoshipd,outer(stiinvtr, stxinfor) where 1=1 ");
            sql.Append(" and stiinvtr.item_code = stoshipd.item_code and stoinvce.inv_doc_no = stoshipd.inv_doc_no and stxinfor.src_key = stiinvtr.item_class and stoshipd.stage = 'PST' and stxinfor.src_type = 'P'");//            
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//start_date
                sql.Append(" and stoinvce.inv_date >= '" + parameters[0].ToString() + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//end_date
                sql.Append(" and stoinvce.inv_date <= '" + parameters[1].ToString() + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//item_class
                sql.Append(" and  stiinvtr.item_class ='" + parameters[2].ToString()+"'");
            sql.Append(" order by stiinvtr.item_class");
            return sql.ToString();
        }
        //Query for Product by Date Summary
        public string FIND_QUERY_3(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_class,stoinvce.currency_code,stoinvce.currency_rate,");
            sql.Append("stoshipd.doc_no,stoshipd.gross_margin,stoshipd.inv_date, stoshipd.inv_doc_no,");
            sql.Append("stoshipd.item_code,stoshipd.item_cost,stoshipd.net_amount,stoshipd.sell_to_code,stxinfor.src_desc,'" + parameters[0] + "' as start_date,'" + parameters[1] + "' as end_date ");//
            sql.Append(" from stoshipd, stoinvce, outer(stiinvtr,stxinfor) where 1=1 and stoinvce.inv_doc_no = stoshipd.inv_doc_no ");
            sql.Append(" and stiinvtr.item_code = stoshipd.item_code and stxinfor.src_key = stiinvtr.item_class");
            sql.Append(" and stoshipd.stage = 'PST' and stxinfor.src_type = 'P' ");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//start_date
                sql.Append(" and stoinvce.inv_date >= '" + parameters[0].ToString() + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//end_date
                sql.Append(" and stoinvce.inv_date <= '" + parameters[1].ToString() + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//item_class
                sql.Append(" and  stiinvtr.item_class ='" + parameters[2].ToString() + "'");
            sql.Append(" order by stiinvtr.item_class, stoshipd.inv_date");
            return sql.ToString();
        }
        //Query for Product Detail
        public string FIND_QUERY_4(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_class,stoinvce.currency_code,stoinvce.currency_rate,stoordrd.line_type,");
            sql.Append("stoshipd.doc_no,stoshipd.gross_margin,stoshipd.inv_date, stoshipd.inv_doc_no,");
            sql.Append("stoshipd.item_code,stoshipd.item_cost,stoshipd.net_amount,stoshipd.sell_to_code,'" + parameters[0] + "' as start_date,'" + parameters[1] + "' as end_date, ");//
            sql.Append("(select src_desc from stxinfor where src_type = 'P' and src_key = stiinvtr.item_class) as class_desc, ");
            sql.Append("(select description from stoltypr where line_type = stoordrd.line_type) as line_type_desc ");
            sql.Append(" from stoshipd, stoinvce, outer (stiinvtr), stoordrd where 1=1 and stiinvtr.item_code = stoshipd.item_code ");
            sql.Append(" and stoshipd.doc_no = stoordrd.doc_no and stoshipd.line_no = stoordrd.line_no and stoinvce.inv_doc_no = stoshipd.inv_doc_no");
            sql.Append(" and stoshipd.stage = 'PST'");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//start_date
                sql.Append(" and stoinvce.inv_date >= '" + parameters[0].ToString() + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//end_date
                sql.Append(" and stoinvce.inv_date <= '" + parameters[1].ToString() + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//item_class
                sql.Append(" and stiinvtr.item_class ='" + parameters[2].ToString() + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//item_code
                sql.Append(" and stoshipd.item_code ='" + parameters[3].ToString() + "'");
            sql.Append(" order by stoordrd.line_type, stiinvtr.item_class, stoshipd.inv_date,stoshipd.item_code");
            return sql.ToString();
        }
        #endregion store-procedures
    }     
}
