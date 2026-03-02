using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)     DVO For Fiscal Sub Category            Rajeev(D)                                 02/05/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOFiscalSubCat : DVOBase
    {
         
        private int _subcatid;
        private int _catid;
        private int _mcatid;
        private string _scatdesc;
        private string _scatprintodr;
        private string _scatobjcode;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;


         //Constructor to Assign the initial value to the decleared variable
        public DVOFiscalSubCat()
        {
            _subcatid=0;
             _catid = 0;
            _mcatid = 0;
            _scatdesc = string.Empty;
            _scatprintodr = string.Empty;
            _scatobjcode=string.Empty;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;
        }

        #region StartProperties

        public int subcatid
        {
            get { return _subcatid; }
            set { _subcatid = value; }
        }
        public int catid
        {
            get { return _catid; }
            set { _catid = value; }
        }
        public int mcatid
        {
            get { return _mcatid; }
            set { _mcatid = value; }
        }

        public string scatdesc 
        {
            get { return _scatdesc; }
            set { _scatdesc = value; }
        }

        public string scatprintodr
        {
            get { return _scatprintodr; }
            set { _scatprintodr = value; }
        }

        public string scatobjcode
        {
            get { return _scatobjcode; }
            set { _scatobjcode = value; }
        }

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
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
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspfisscatins"; }
        }                 

        public override string UPDATE_SPNAME
        {
            get { return "uspfisscatupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspfisscatdel"; }
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
            get { return "fisscat"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("Select Rowid,subcatid,mcatid,catid,scatdesc,scatprintodr,scatobjcode,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy from fissubcat");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append("AND catid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append("AND mcatid=" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(scatdesc) LIKE '%" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(scatprintodr)  LIKE '%" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(scatobjcode)  LIKE '%" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (Convert.ToInt32(parameters[5]) != 0)
                    sql.Append("AND Rowid=" + Convert.ToInt32(parameters[5]));

            return sql.ToString();
            
        }

        public string Find_next_order
        {
            get { return "uspfiscatodr"; }
        }

        #endregion Stored-Procedures

        public string  IS_DUPLICATE_SUB_CAT(ref object[] Parameter)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select COUNT(*) from fissubcat where 1=1");
            if (Parameter[0] != null)
                if (Parameter[0].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(scatdesc) = '" + Parameter[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
    }
}
