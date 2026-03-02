using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa 
    public class DVOEmpPayDates : DVOBase
    {

        private int _rowid;
        private string  _salary_type;
        private string _date1;
        private string _date2;
        private string _date3;
        private string _date4 ;
        private string _date5;
        private string _date6;
        private string _date7;
        private string _date8;
        private string _date9;
        private string _date10;
        private string _date11;
        private string _date12 ;
        private string _year;
        private int _active;


        private int _insertby;
        private  DateTime  _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private DateTime  _updatedate;
        private string _updatemachineinfo;


       public DVOEmpPayDates()
       {
           _rowid = 0;
          _salary_type=string.Empty;
          _date1 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date
          _date2 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date
          _date3 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date
          _date4 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date
          _date5 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date;
          _date6 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;// DateTime.Now.Year;// date;
          _date7 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;// DateTime.Now.Year;// date;
          _date8 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date;
          _date9 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date;
          _date10 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date;
          _date11 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date;
          _date12 = "01/01/" + DVOApplicationUserInfo.CurrentDate.Year;//DateTime.Now.Year;// date ;
          _year = string.Empty;// date;
          _active = 0;


           _insertby = -1;
           _insertdate = DateTime.ParseExact(DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//DateTime.Now;
           _insertmachineinfo = string.Empty;
           _updateby = -1;
           _updatedate = DateTime.ParseExact(DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//DateTime.Now;
           _updatemachineinfo = string.Empty;

       }


       # region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
       public string salary_type
       {
           get { return _salary_type; }
           set { _salary_type = value; }
       }
       public string date1
       {
           get { return _date1; }
           set { _date1 = value; }
       }
       public string date2
       {
           get { return _date2; }
           set { _date2 = value; }
       }
       public string date3
       {
           get { return _date3; }
           set { _date3 = value; }
       }
       public string date4
       {
           get { return _date4; }
           set { _date4 = value; }
       }

       public string date5
       {
           get { return _date5; }
           set { _date5 = value; }
       }
       public string date6
       {
           get { return _date6; }
           set { _date6 = value; }
       }
       public string date7
       {
           get { return _date7; }
           set { _date7 = value; }
       }
       public string date8
       {
           get { return _date8; }
           set { _date8 = value; }
       }
       public string date9
       {
           get { return _date9; }
           set { _date9 = value; }
       }
       public string date10
       {
           get { return _date10; }
           set { _date10 = value; }
       }
       public string date11
       {
           get { return _date11; }
           set { _date11 = value; }
       }
       public string date12
       {
           get { return _date12; }
           set { _date12 = value; }
       }
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
       public int insertby
       {
           get { return _insertby; }
           set { _insertby = value; }
       }
       public DateTime  insertdate
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
       public DateTime  updatedate
       {
           get { return _updatedate; }
           set { _updatedate = value; }
       }
       public string updatemachineinfo
       {
           get { return _updatemachineinfo; }
           set { _updatemachineinfo = value; }
       }

       #endregion public properties

       #region Stored-Procedures

       public override string INSERT_SPNAME
       {
           get { return "emppaydatesins"; }
       }

       public override string UPDATE_SPNAME
       {
           get { return "emppaydatesupd"; }
       }

       public override string DELETE_SPNAME
       {
           get { return "emppaydatesdel"; }
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
           get { return "emppaydates"; }
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


        public string GET_EMP_PAY_DATE_INFO
        {
            get { return "uspemppaydateget"; }
        }






       public override string FIND_QUERY(ref Object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append(" select date1 v_date1 ,date2 v_date2,date3 v_date3,date4 v_date4,date5 v_date5, ");
           sql.Append(" date6 v_date6,date7  v_date7 ,date8 v_date8,date9 v_date9,date10 v_date10,");
           sql.Append(" date11 v_date11,date12 v_date12 ,salary_type v_salary_type,year v_year ,active v_active,rowid v_rowid from emppaydates where active=1  ");

           if (parameters[0] != null)
               if (parameters[0].ToString() != string.Empty)
                   sql.Append(" AND salary_type='" + parameters[0].ToString().Trim() + "'");

           if (parameters[1] != null)
               if (parameters[1].ToString() != string.Empty)
                   sql.Append(" AND year='" + parameters[1].ToString().Trim() + "'");


           if (Convert.ToInt32(parameters[2]) > 0)
               sql.Append(" AND  rowid=" + parameters[2].ToString());


           return sql.ToString();
       }





       #endregion Stored-Procedures


    }
}
