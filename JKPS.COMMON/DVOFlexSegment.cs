using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{

    /// </summary>
    public class DVOFlexSegment : DVOBase
    {
        private int _id;
        private string _desc;
        private int _keylength;
        private int _subdividedby;
        private string _subdividedbydesc;
        private int _subdivides;
        private string _segmenttype;
        private int _rowid;
        //******* Added by Sanjay *********
        private string _FlexDesc;
        private string _keyvalue;
        //******* Added by Bharat *********
        private int _IssubtoInSegmentItems;
        //private int _PositionInAccountKeyValue;
        //private int _LenghtInAccountKeyValue;
        //*********************************


        public DVOFlexSegment()
        {
            _id = 0;
            _desc = string.Empty;
            _keylength = 0;
            _subdividedby = 0;
            _subdividedbydesc = string.Empty;
            _subdivides = 0;
            _segmenttype = string.Empty;
            _FlexDesc = string.Empty;
            _keyvalue = string.Empty;
            _rowid=0;
        }
        
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
        public string FlexDesc
        {
            get { return _FlexDesc; }
            set { _FlexDesc = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
         public int Rowid
        {
            get { return _rowid ; }
            set { _rowid  = value; }
        }


      
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


        #region Stored-Procedures

        //********************** Added by Bharat ********************************************
        public string GET_SEGMENT_ITEMS
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("SELECT vd.segmentid v_segmentid,vd.id v_id,vd.keyvalue v_keyvalue,vd.[desc] v_segitm_desc,vd.issubto v_issubto,dr.SegmentDesc v_seg_desc");
                sqlquery.Append(" FROM Flex_Segment_Value_Details vd,Master_Segment dr");
                sqlquery.Append(" WHERE vd.segmentid = dr.SegmentID");
                if (_id > 0)
                    sqlquery.Append(" AND vd.segmentid=" + _id.ToString());
                if (_IssubtoInSegmentItems > 0)
                    sqlquery.Append(" AND (vd.issubto = " + _IssubtoInSegmentItems.ToString() + " OR vd.issubto = 0)");

                return sqlquery.ToString();
            }
        }       
       
        public string GET_SEGMENT_VALUE
        {
            get { return "USP_GetSegDetail"; }
        }

        public string GET_SEGMENT_Data
        {
            get { return "USP_GetSegDetailAll"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "USP_FlxSegIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_FlxSegUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_FlxSegDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_FlxSegGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_FlxSegGetAll"; }
        }

        
        public string FINDSUBDIVIDE_SPNAME
        {
            get { return "USP_FlxSNotSdvbyGet"; }
        }
        public override string TABLE_NAME
        {
            get { return "Master_Segment"; }
        }

        //Modified by: Sunil
        //Modification Date:18/12/09
        //public string GET_SEGMENT_ID
        //{
        //    get { return "uspflexsegmaxget"; }
        //}


        public override int UNIQUE_ID
        {
            get { return _id ; }
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
            sql.Append("SELECT SegmentID p_id,SegmentDesc p_desc,keylength p_keylength,SubDividedBy p_subdividedby,");
            sql.Append(" (select SegmentDesc from Master_Segment where SegmentID=i.SubDividedBy)  ");
            sql.Append(" p_subdividedbydesc ,SubDivides p_subdivides, Segmenttype p_segmenttype,RowID p_rowid from ");
            sql.Append(" Master_Segment i where 1=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND  SegmentID=" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(SegmentDesc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND  keylength= " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND SubdividedBy = " + parameters[3].ToString());

            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND subdivides = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(segmenttype) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND  Rowid=" + parameters[6].ToString());

            //if (Convert.ToInt32(parameters[6]) > 0)
            //    sql.Append(" AND subdivides= " + parameters[6].ToString());
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(subdivides) LIKE '" + parameters[4].ToString().Trim() + "%'");
            //return sql.ToString();
           
            return sql.ToString();
        }

        public string FIND_SEGMENT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT SegmentID ,SegmentDesc ,keylength p_keylength,SubdividedBy,subdivides ,segmenttype");
            sql.Append(" FROM Master_Segment ");
            sql.Append(" Where 1=1" );
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND  SegmentID=" + parameters[0].ToString());
            sql.Append(" order by SegmentID ");
            return sql.ToString();
        }

        #endregion Stored-Procedures

    }
}
