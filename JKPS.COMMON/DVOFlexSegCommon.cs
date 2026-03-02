using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOFlexSegCommon : DVOBase
    {

        private string _keyvalue;
        private int _position;
        private int _length;
        private string _abbreviation;

        //Variable used as a parameter
        private string _EntityType;
        private string _AccountType;
        private string _Code;
        //Variable used to Get the Calculated Key value when Load Function Called
        private string _Ret_Keyvalue;

        //Variable used to insert the calculated the segvd_id  When Add Function Called
        //And Insert its value to Flex_Segment_Reference table
        private int _segvd_id;

        private int _strucid;
        private int _flexsegid;
        private string _required;
        private string _subtotal;
        private int _subdivides;

        public DVOFlexSegCommon()
        {
            //_keyvalue = "080880007003";
            _keyvalue = string.Empty;
            _position = 0;
            _length = 0;
            _abbreviation = string.Empty;


            _AccountType = string.Empty;
            _EntityType = string.Empty;
            // _EntityType = "styemplr";
            //_AccountType = "CAPREV";
            // _Code = "363";
            _Code = string.Empty;

            _Ret_Keyvalue = string.Empty;

            _segvd_id = 0;

            _strucid = 0;
            _flexsegid = 0;
            _required = string.Empty;
            _subtotal = string.Empty;
            _subdivides = 0;

        }

        #region Properties

        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public int position
        {
            get { return _position; }
            set { _position = value; }
        }
        public int length
        {
            get { return _length; }
            set { _length = value; }
        }
        public string abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }

        public string EntityType
        {
            get { return _EntityType; }
            set { _EntityType = value; }
        }
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Ret_Keyvalue
        {
            get { return _Ret_Keyvalue; }
            set { _Ret_Keyvalue = value; }
        }

        public int segvd_id
        {
            get { return _segvd_id; }
            set { _segvd_id = value; }
        }

        public int strucid
        {
            get { return _strucid; }
            set { _strucid = value; }
        }
        public int flexsegid
        {
            get { return _flexsegid; }
            set { _flexsegid = value; }
        }
        public string required
        {
            get { return _required; }
            set { _required = value; }
        }
        public string subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }
        public int subdivides
        {
            get { return _subdivides; }
            set { _subdivides = value; }
        }

        #endregion properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_FlexSegComIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_FlexSegComDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SegValComGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
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

        public string FIND_KEYLEN
        {
            get { return "USP_SegComKeyLen"; }
        }
        public string FIND_SegmentName
        {
            get { return "USP_SegmentName"; }
        }
        public string FIND_SegvdIDADD
        {
            get { return "USP_SegComAddGet"; }
        }
        public string FIND_SegvdIDGet
        {
            get { return "USP_SegComSegIdGet"; }
        }
        public string FIND_SegvdIDCheck
        {
            get { return "USP_SegComSegIdChk"; }
        }
        public string FIND_SEGDESC
        {
            get { return "USP_SegDescGet"; }
        }
        public string GETFIRSTID()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Flex_Segment_Value_Details.id FROM Master_Segment, Flex_Segment_Value_Details  WHERE segmenttype = 'ORGCMP' AND subdivides = 0 AND printsafter = 0  AND Master_Segment.id = Flex_Segment_Value_Details.segmentid ");
            return sql.ToString();
        }
        public string Find_allFlexVal(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Flex_Segment_Value_Details.keyvalue,Flex_struct_Details.position,Flex_struct_Details.length,Flex_Segment_Value_Details.abbreviation,Flex_Segment_Reference.entity_type,Flex_Segment_Reference.code,Flex_struct_Header.accounttype");
            sql.Append(" FROM Flex_Segment_Value_Details,Flex_Segment_Reference,Flex_struct_Header,Flex_struct_Details WHERE Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid AND Flex_Segment_Value_Details.id = Flex_Segment_Reference.segvd_id ");
            sql.Append(" AND (( Flex_Segment_Reference.entity_type='styemplr' ");

            if ((parameters[0] != null) || parameters[1] != null || parameters[2] != null || parameters[3] != null || parameters[4] != null)
            {
                sql.Append("    AND Flex_Segment_Reference.code IN (select empl_code from MasterEmployee where terminated IS NULL  ");
                if (parameters[0] != null)
                {
                    if (parameters[0].ToString() != String.Empty)
                    {
                        sql.Append(" AND empl_code =" + parameters[0].ToString().Trim());
                    }
                }
                if (parameters[1] != null)
                {
                    if (parameters[1].ToString() != String.Empty)
                    {
                        sql.Append(" AND soc_sec_num =" + parameters[1].ToString().Trim());
                    }
                }
                if (parameters[2] != null)
                {
                    if (parameters[2].ToString() != String.Empty)
                    {
                        sql.Append(" AND first_name =" + parameters[2].ToString().Trim());
                    }
                }
                if (parameters[3] != null)
                {
                    if (parameters[3].ToString() != String.Empty)
                    {
                        sql.Append(" AND last_name =" + parameters[3].ToString().Trim());
                    }
                }
                if (parameters[4] != null)
                {
                    if (parameters[4].ToString() != String.Empty)
                    {
                        sql.Append(" AND type_code  LIKE '%" + parameters[4].ToString().Trim() + "%' ");
                    }
                }
                sql.Append(" )");
            }
            sql.Append(" ) OR Flex_Segment_Reference.entity_type='styinccr' OR Flex_Segment_Reference.entity_type='styoblcr')");

            return sql.ToString();

        }
        public string Find_allAcctTypelength(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT accounttype,keylength FROM Flex_struct_Header ");
            return sql.ToString();
        }
        public string Find_AllFlexDeptKeyvalue(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Flex_Segment_Value_Details.keyvalue,Flex_struct_Details.position,Flex_struct_Details.length,");
            sql.Append(" Flex_Segment_Value_Details.abbreviation,Flex_struct_Header.accounttype,");
            sql.Append(" Flex_Segment_Reference.entity_type,Flex_Segment_Reference.code,Flex_struct_Header.keylength,styemplr.flexdeptaccttype");
            sql.Append(" FROM Flex_Segment_Value_Details,Flex_Segment_Reference,Flex_struct_Header,Flex_struct_Details,MasterEmployee");
            sql.Append(" WHERE Flex_struct_Header.id = Flex_struct_Details.strucid");
            sql.Append(" AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
            sql.Append(" AND Flex_Segment_Value_Details.id = Flex_Segment_Reference.segvd_id");
            sql.Append(" and Flex_struct_Header.accounttype=styemplr.flexdeptaccttype");
            sql.Append(" and Flex_Segment_Reference.code = styemplr.empl_code");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and trim(Flex_Segment_Reference.entity_type) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
