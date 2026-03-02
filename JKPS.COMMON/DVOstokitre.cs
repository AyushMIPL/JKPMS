using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Update Kit Definition
    /// Header :Table-stokitre
    /// Created by : Shrishanshu on 210709
    /// </summary>
    public class DVOstokitre : DVOBase
    {
        /* Table coulmns
         * kit_code             char(15) 
         * desc1                char(30) 
         * desc2                char(30)
         */
        private int _rowid;
        private string _kit_code;
        private string _desc1;
        private string _desc2;
        public DVOstokitre()
        {
            _rowid = 0;
            _kit_code = string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;

        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public string kit_code
        {
            get { return _kit_code; }
            set { _kit_code = value; }
        }
        public string desc1
        {
            get { return _desc1; }
            set { _desc1 = value; }
        }
        public string desc2
        {
            get { return _desc2; }
            set { _desc2 = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstokitreins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstokitreupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstokitredel"; }
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
            get { return "stokitre"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("Select rowid,kit_code,desc1,desc2 from stokitre");
            sql.Append(" where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(kit_code) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(desc1) LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            
            return sql.ToString();
        }

        /// <summary>
        /// Added By Rajeev
        /// date : 06/08/2009
        /// Aim : Used to get data on print kit definition
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public  string FIND_KITDEFINITION(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select stokitrd.include_price,stokitrd.item_code,stokitrd.ordr_qty,stokitre.desc1,");
            sql.Append("stokitre.desc2,stokitre.kit_code from stokitre,stokitrd");
            sql.Append(" where 1=1 and stokitre.kit_code = stokitrd.kit_code");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stokitre.kit_code) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(desc1) LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append("ORDER BY stokitre.kit_code");

            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
