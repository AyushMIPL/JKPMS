using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON 
{
   public  class Form2Class : DVOBase
    {
        #region Private Varieables

            private int _id;
            private string _desc;
            private int _keylength;
            private int _subdividedby;
            private string _subdividedbydesc;
            private int _subdivides;
            private string _segmenttype;
            private int _IssubtoInSegmentItems;
           
        #endregion Private Varieables

        #region Constructor
            public Form2Class()
                {
                    _id = 0;
                    _desc = string.Empty;
                    _keylength = 0;
                    _subdividedby = 0;
                    _subdividedbydesc = string.Empty;
                    _subdivides = 0;
                    _segmenttype = string.Empty;
                }

            #endregion Constructor

        #region Properties

        //********************** Added by Rahul ********************************************
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string desc
        {
            get { return _desc; }
            set { _desc = value; }

        }
        public int keylength
        {
            get { return _keylength; }
            set { _keylength = value; }
        }
        public int subdividedby
        {
            get { return _subdividedby; }
            set { _subdividedby = value; }
        }
        public int subdivides
        {
            get { return _subdivides; }
            set { _subdivides = value; }
        }
        public string segmenttype
        {
            get { return _segmenttype; }
            set { _segmenttype = value; }
        }
        public string subdividedbydesc
        {
            get { return _subdividedbydesc; }
            set { _subdividedbydesc = value; }
        }

        //******* Added by Bharat *********
        public int IssubtoInSegmentItems
        {
            get { return _IssubtoInSegmentItems; }
            set { _IssubtoInSegmentItems = value; }
        }
        //public int PositionInAccountKeyValue
        //{
        //    get { return _PositionInAccountKeyValue; }
        //    set { _PositionInAccountKeyValue = value; }
        //}
        //public int LenghtInAccountKeyValue
        //{
        //    get { return _LenghtInAccountKeyValue; }
        //    set { _LenghtInAccountKeyValue = value; }
        //}
        //**********************************
        #endregion Properties

        #region Stored-Procedures

        //********************** Added by Bharat ********************************************
        public string GET_SEGMENT_ITEMS
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("SELECT vd.segmentid v_segmentid,vd.id v_id,vd.keyvalue v_keyvalue,vd.[desc] v_segitm_desc,vd.issubto v_issubto,dr.[desc] v_seg_desc");
                sqlquery.Append(" FROM Master_Segment vd,Master_Segment dr");
                sqlquery.Append(" WHERE vd.segmentid = dr.id");
                if (_id > 0)
                    sqlquery.Append(" AND vd.segmentid=" + _id.ToString());
                if (_IssubtoInSegmentItems > 0)
                    sqlquery.Append(" AND (vd.issubto = " + _IssubtoInSegmentItems.ToString() + " OR vd.issubto = 0)");

                return sqlquery.ToString();
            }
        }
        //***********************************************************************************

        public override string INSERT_SPNAME
        {
            get { return "uspFlxSegIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspflxsegUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspFlxSegDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspFlxSegGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspFlxSegGetAll"; }
        }

        //Modified by: Rajeev
        //Modification Date:06/11/08
        public string FINDSUBDIVIDE_SPNAME
        {
            get { return "uspFlxSegNotSubdividebyGet"; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
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
        //store procedure used for informix 
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT id p_id,desc p_desc,keylength p_keylength,subdividedby p_subdividedby,");
            sql.Append(" (select desc from Master_Segment where id=i.subdividedby)  ");
            sql.Append(" p_subdividedbydesc ,subdivides p_subdivides, segmenttype p_segmenttype from ");
            sql.Append(" Master_Segment i where 1=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND  id=" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND  keylength= " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND subdividedby = " + parameters[3].ToString());

            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND subdividedby = " + parameters[4].ToString());
            if (parameters[4] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(segmenttype) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            //if (Convert.ToInt32(parameters[6]) > 0)
            //    sql.Append(" AND subdivides= " + parameters[6].ToString());
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(subdivides) LIKE '" + parameters[4].ToString().Trim() + "%'");
            //return sql.ToString();
           
            return sql.ToString();
        }
       


        #endregion Stored-Procedures
    }
}
