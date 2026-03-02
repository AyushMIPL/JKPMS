using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOWarehousestiwhser: DVOBase
    {
        public int _RowId;
        public string _whse_code;
        public string _description;
        public string _department;
        public string _address1;
        public string _address2;
        public string _city;
        public string _state;
        public string _zip;
        public string _country;
        public string _phone;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        public DVOWarehousestiwhser()
        {
            _RowId = 0;
            _whse_code = "";
            _description = "";
            _department = "";
            _address1 = "";
            _address2 = "";
            _city = "";
            _state = "";
            _zip = "";
            _country = "";
            _phone = "";

            _InsertMachineInfo = "App";
            _InsertDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _UpdateBy = -1;
        }

        public int RowID
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string whse_code
        {
            get { return _whse_code; }
            set { _whse_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }


        public string city
        {
            get { return _city ; }
            set { _city = value; }
        }
        public string state
        {
            get { return _state ; }
            set { _state = value; }
        }
        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string country
        {
            get { return _country ; }
            set { _country = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
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
            get { return "uspwarehouseins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspwarehouseupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspwarehousedel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspwarehouseget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspstiwhsergetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "stiwhser"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        //Using in Print Purchase ordre report
        public string GETWarehouseaddress
        {
            get { return "uspwareaddrget"; }
        }

        public string GetAllWarehouseaddress
        {
            get { return "uspwareaddrgetall"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid,whse_code,description,department,address1,address2,city,state,zip,country,phone");
            sql.Append(" FROM  stiwhser where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND whse_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND description LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND address1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND address2 LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND city = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND state = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND zip = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND country = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND phone = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND rowid =" + parameters[9]);
            sql.Append(" order by whse_code");

            return sql.ToString();
        }
        public string FIND_DUPLICATE_WHSECODE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid,whse_code,description,department,address1,address2,city,state,zip,country,phone");
            sql.Append(" FROM  stiwhser where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND whse_code  ='" + parameters[0].ToString().Replace("'", "''") + "'");
            sql.Append(" order by whse_code");

            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
