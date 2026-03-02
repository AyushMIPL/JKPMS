using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLWarrantEntryInbwarah : DVOBase
    {
        private int _DocNo;
        private string _Year;
        private string _Set;
        private string _Type;
        private string _EffectiveDate;
        private string _DateEntered;
        private string _Posted;
        private string _OkToPost;
        private string _EnteredBy;
        private string _RequestedBy;
        private string _Description;
        private string _StartPeriodForAllocation;
        private string _EndPeriodForAllocation;
        private string _AllocateFullAmount;
        private int _PostSequentialNo;
        private string _WarrantNumber;
        private int _count;

        #region Constructor

        public DVOGLWarrantEntryInbwarah()
        {
            _DocNo = 0;
            _Year = string.Empty;
            _Set = string.Empty;
            _Type = string.Empty;
            _EffectiveDate = "01/01/1900";
            _DateEntered = "01/01/1900";
            _Posted = string.Empty;
            _OkToPost = string.Empty;
            _EnteredBy = string.Empty;
            _RequestedBy = string.Empty;
            _Description = string.Empty;
            _StartPeriodForAllocation = string.Empty;
            _EndPeriodForAllocation = string.Empty;
            _AllocateFullAmount = string.Empty;
            _PostSequentialNo = 0;
            _WarrantNumber = string.Empty;
            _count = 0;
        }

        #endregion Constructor

        #region Properties

        public int DocNo
        {
            get { return _DocNo; }
            set { _DocNo = value; }
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string Set
        {
            get { return _Set; }
            set { _Set = value; }
        }
        
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string EffectiveDate
        {
            get { return _EffectiveDate; }
            set { _EffectiveDate = value; }
        }

        public string DateEntered
        {
            get { return _DateEntered; }
            set { _DateEntered = value; }
        }
        
        public string Posted
        {
            get { return _Posted; }
            set { _Posted = value; }
        }

        public string OkToPost
        {
            get { return _OkToPost; }
            set { _OkToPost = value; }
        }
        
        public string EnteredBy
        {
            get { return _EnteredBy; }
            set { _EnteredBy = value; }
        }
        
        public string RequestedBy
        {
            get { return _RequestedBy; }
            set { _RequestedBy = value; }
        }
        
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        
        public string StartPeriodForAllocation
        {
            get { return _StartPeriodForAllocation; }
            set { _StartPeriodForAllocation = value; }
        }
        
        public string EndPeriodForAllocation
        {
            get { return _EndPeriodForAllocation; }
            set { _EndPeriodForAllocation = value; }
        }
        
        public string AllocateFullAmount
        {
            get { return _AllocateFullAmount; }
            set { _AllocateFullAmount = value; }
        }
        
        public int PostSequentialNo
        {
            get { return _PostSequentialNo; }
            set { _PostSequentialNo = value; }
        }

        public string WarrantNumber
        {
            get { return _WarrantNumber; }
            set { _WarrantNumber = value; }
        }

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }


        #endregion Properties

        #region Stored-Procedures

        public string GET_CURRENT_ACCOUNTING_YEAR
        {
            get { return "SELECT curr_year FROM stgcntrc"; }
        }
        public string GET_CURRENT_BUDGET_SET
        {
            get { return "SELECT src_char_desc FROM stxinfor WHERE src_type = 's' AND src_char_desc = ( SELECT curr_year FROM stgcntrc )"; }
        }
        public string GET_CURRENT_ACCOUNTING_MONTH
        {
            get { return "SELECT curr_period FROM stgcntrc"; }
        }
        public string GET_WARRANT_ALLOCATE_PERCENT
        {
            get { return "uspWarntAlocPrcnt"; }//uspwarntalocprcnt
        }
        public string GET_WARRANT_ALLOCATE_AMOUNT
        {
            get { return "uspWarntAlocAmt"; }//uspwarntalocamt
        }
        public string GET_WARRANT_KEYVALUES
        {
            get { return "uspGetWarntKv"; }//uspgetwarntkv
        }

        public override string INSERT_SPNAME
        {
            get { return "uspGLWrntEntIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLWrntEntUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLWrntEntDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLWrntEntGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspGLWrntEntGelAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "inbwarah"; }
        }

        public override int UNIQUE_ID
        {
            get { return _DocNo; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //frm -WarrantType
        //Added by Sunil on 23/9/09
        public  string GET_TYPE_FOR_WARRANT_TYPE
        {
            get { return "uspwartypechkget"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT doc_no p_doc_no,Year p_year,set p_set,Type p_type,effectivedate p_effectivedate,dateentered p_dateentered,");
            sql.Append(" posted p_posted,oktopost p_oktopost,enteredby p_enteredby,requestedby p_requestedby,desc p_desc,");
            sql.Append(" startperiod4alloc p_startperiodalloc,endperiodforalloc p_endperiodalloc,allocfullamt p_allocfullamt,");
            sql.Append(" post_seq_no p_post_seq_no,warrant_num p_warrant_num");
            sql.Append(" FROM inbwarah");
            sql.Append(" WHERE 1=1");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND Year = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND set = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" AND Type = '" + parameters[3].ToString().Replace("'", "''") + "'");
                else
                    sql.Append(" AND Type <> 'MEMENT'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0 && !parameters[4].ToString().Trim().Contains("1900"))
                    sql.Append(" AND effectivedate = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0 && !parameters[5].ToString().Trim().Contains("1900"))
                    sql.Append(" AND dateentered = '" + parameters[5].ToString().Replace("'", "''") + "'");
            if (parameters[6] == null || parameters[6].ToString().Trim().Length <= 0)
                sql.Append(" AND posted NOT IN ('Y','C')");
            else if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)
                    sql.Append(" AND posted = '" + parameters[6].ToString().Replace("'", "''") + "'");
            if (parameters[7] == null || parameters[7].ToString().Trim().Length <= 0)
                sql.Append(" AND oktopost NOT IN ('Y','C')");
            else if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)
                    sql.Append(" AND oktopost = '" + parameters[7].ToString().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim().Length > 0)
                    sql.Append(" AND enteredby = '" + parameters[8].ToString().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString().Trim().Length > 0)
                    sql.Append(" AND requestedby = '" + parameters[9].ToString().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString().Trim().Length > 0)
                    sql.Append(" AND desc = '" + parameters[10].ToString().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString().Trim().Length > 0)
                    sql.Append(" AND startperiod4alloc = '" + parameters[11].ToString().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString().Trim().Length > 0)
                    sql.Append(" AND endperiodforalloc = '" + parameters[12].ToString().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString().Trim().Length > 0)
                    sql.Append(" AND allocfullamt = '" + parameters[13].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND post_seq_no = '" + parameters[14].ToString().Replace("'", "''") + "'");
            if (parameters[15] != null)
                if (parameters[15].ToString().Trim().Length > 0)
                    sql.Append(" AND warrant_num = '" + parameters[15].ToString().Replace("'", "''") + "'");


            return sql.ToString();
        }

        public string FIND_WARRANT_DETAIL(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select inbwarad.acct_type,inbwarad.amount,inbwarad.desc wddesc, inbwarad.keyvalue,inbwarad.lineno,");
            sql.Append(" inbwarah.dateentered,inbwarah.desc whdesc,inbwarah.doc_no, inbwarah.effectivedate,");
            sql.Append(" inbwarah.endperiodforalloc,inbwarah.enteredby, inbwarah.requestedby,inbwarah.startperiod4alloc,");
            sql.Append(" inbwarah.type,inbwarah.warrant_num, inbwarah.year,lr1.first_name fname1,lr1.last_name lname1, ");
            sql.Append(" lr2.first_name fname2 ,lr2.last_name lname2 ");
            sql.Append(" from inbwarah, inbwarad , outer MasterEmployee lr1 ,outer MasterEmployee lr2");
            sql.Append(" where inbwarah.doc_no = inbwarad.doc_no");
            sql.Append(" and inbwarah.enteredby = lr1.empl_code");
            sql.Append(" and inbwarah.requestedby=lr2.empl_code");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and inbwarah.type='" + parameters[0].ToString().Replace("'", "''") + "'");

            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0 && !parameters[1].ToString().Contains("1900"))
                    sql.Append(" and inbwarah.effectivedate='" + parameters[1].ToString() + "'");

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0 && !parameters[2].ToString().Contains("1900"))
                    sql.Append(" and inbwarah.dateentered='" + parameters[2].ToString() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" and inbwarah.enteredby='" + parameters[3].ToString().Replace("'", "''") + "'");

            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" and inbwarah.requestedby='" + parameters[4].ToString().Replace("'", "''") + "'");

            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    sql.Append(" and inbwarad.keyvalue  LIKE '" + parameters[5].ToString().Replace("'", "''") + "%'");

            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)
                    sql.Append(" and inbwarad.acct_type  = '" + parameters[6].ToString().Replace("'", "''") + "'");
           
            //sql.Append("order by inbwarah.dateentered, inbwarah.doc_no, inbwarad.lineno");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
