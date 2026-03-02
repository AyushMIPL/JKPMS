using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By -Sunil Pahwa*
    public class DVOUpdPurchaseDef : DVOBase
    {
        private int _purchase_class_id;
        private string _description;

        private int _Rowid;

        public DVOUpdPurchaseDef()
        {
            _purchase_class_id = 0;
            _description = string.Empty;
            _Rowid = 0;
        }
        public int purchase_class_id
        {
            get { return _purchase_class_id; }
            set { _purchase_class_id = value; }
        }
        public string description
        {
            get {  return _description; }
            set { _description = value; }
        }

        public int rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspUpdPurDefIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUpdPurDefUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspUpdPurDefDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspUpdPurDefGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspUpdPurDefGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "inpcmast"; }//inpurcls
        }
        public string INSERT_STUPRCHE
        {
            get { return "uspstuprcheupd"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            //*********Changed*************
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct description  p_description,");
            sql.Append(" purchase_class_id  v_p_class_id,rowid v_rowid from  inpcmast ");
            sql.Append("  WHERE  1=1 ");


            //********Earlier
            //System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT distinct description  p_description,");
            //sql.Append(" st.purchase_class_id  v_p_class_id,ls.rowid v_rowid from  inpcmast st , inpurcls ls ,stxinfor fo");
            //sql.Append("  WHERE  src_type='P' and st.purchase_class_id=ls.purchase_Class_id ");
            //sql.Append(" and  ls.item_class=fo.src_key ");
            //***********************************
    
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(description) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND rowid = " + parameters[1].ToString());
           // sql.Append("AND Orderby rowid ");
            return sql.ToString();
        }

        #endregion Stored-Procedures

    }
}
