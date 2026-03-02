using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //***********Implemented By -Sunil Pahwa*********************
    public class DVOUpdPurchaseDefDetail : DVOBase
    {
            private int _Purchase_class_id;
            private string _Item_class;
            private string _Src_type;
            private string _src_key;
            private string _Src_desc;
            private int _rowid;

        #region constructor

        public DVOUpdPurchaseDefDetail()
        {
            _Purchase_class_id = 0;
            _Item_class = string.Empty;
            _Src_type = string.Empty;
            _src_key = string.Empty;
            _Src_desc = string.Empty;
            _rowid = 0;
        }

        #endregion constructor

        #region Properties

        public int Purchase_class_id
        {
            get { return _Purchase_class_id; }
            set { _Purchase_class_id = value; }
        }
        public string Item_class
        {
            get { return _Item_class; }
            set { _Item_class = value; }
        }
        public string Src_type
        {
            get { return _Src_type; }
            set { _Src_type = value; }
        }
        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }
        public string Src_desc
        {
            get { return _Src_desc; }
            set { _Src_desc = value; }
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid  = value; }
        }
        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usppurdefdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUpdPurDefDtlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspUpdPurDefDtlDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspPurDefDtlGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspPurDefDtlGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "inpurcls"; }
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


        public string DELETE_DETAIL
        {
            get { return "uspupdpurdtldel"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  Item_Class item_class,Src_Desc p_src_desc, ls.rowid p_rowid ");
            sql.Append("  from  inpurcls ls ,stxinfor fo WHERE src_type='P' and  ");
            sql.Append(" ls.item_class=fo.src_key  ");

            //sql.Append("SELECT distinct purchase_class_id v_p_class_id,");
            //sql.Append(" Item_Class item_class,Src_Type p_src_type,Src_Desc p_src_desc");
            //sql.Append("  from  inpurcls ls ,stxinfor fo WHERE src_type='P' and  ");
            //sql.Append(" ls.item_class=fo.src_key  ");

            if (Convert.ToInt32(parameters[0]) > 0)//v_purchase_class_id
                sql.Append(" AND purchase_class_id = " + parameters[0].ToString());


            //if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//v_purchase_class_id
            //    sql.Append(" AND Rtrim(p_item_class) LIKE '" + parameters[1].ToString().Trim() + "%'");
            //if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//p_src_type
            //    sql.Append(" AND Rtrim(p_src_type) LIKE '" + parameters[2].ToString().Trim() + "%'");
            //if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//p_src_desc
            //    sql.Append(" AND Rtrim(p_src_desc) = '" + parameters[3].ToString().Trim() + "'");
         
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
