using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLSegment:DVOBase
    {
        private int _strucid;
        private int _flexsegid;
        private int _position;
        private int _length;        
        private string _required;
        private string _subtotal;        
       
        #region Constructor

        public DVOGLSegment()
        {
            _strucid=0;
            _flexsegid=0;
            _position=0;
            _length=0;        
            _required=string.Empty;
            _subtotal=string.Empty;        
        }

        #endregion Constructor

        #region Public Properties

      
        public int strucid
        {
            get { return _strucid; }
            set {  _strucid= value; }
        }
      
        public int flexsegid
        {
            get { return _flexsegid; }
            set { _flexsegid = value; }
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


        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_GlSegmentIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_GlSegmentUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_GlSegmentDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_GlSegmentGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspglsegmentall"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  Flex_struct_Details.position Position, Flex_struct_Details.flexsegid,Flex_struct_Header.desc , Flex_struct_Details.length Length, Flex_struct_Details.subtotal Subtotal,");
            sql.Append("Flex_struct_Details.required FROM  Flex_struct_Header,Flex_struct_Details where Flex_struct_Details.flexsegid = Flex_struct_Header.id ");          
            if (Convert.ToInt32(parameters[0]) > 0)//Id
                sql.Append(" and Flex_struct_Details.strucid = " + parameters[0].ToString());          
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
