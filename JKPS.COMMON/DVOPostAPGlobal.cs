using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOPostAPGlobal
    {

        private Int32 _status;
        private string _description;
        private string _incr_with_crdt;
        private decimal _ap_accum;
        private decimal _disc_accum;
        private Int32 _next_doc_no;
       private int _check_count;
        public DVOPostAPGlobal()
        {
            _status = 0;
            _description = string.Empty;
            _incr_with_crdt = string.Empty;
            _ap_accum = 0.0M;
            _disc_accum = 0.0M;
            _next_doc_no = 0;
            _check_count = 0;
        }
        public Int32 status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Int32 next_doc_no
        {
           get { return _next_doc_no; }
           set { _next_doc_no = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
       public string incr_with_crdt
       {
           get { return _incr_with_crdt; }
           set { _incr_with_crdt = value; }
       }
       public decimal ap_accum
       {
           get { return _ap_accum; }
           set { _ap_accum = value; }
       }
       public decimal disc_accum
       {
           get { return _disc_accum; }
           set { _disc_accum = value; }
       }
       public int check_count 
       {
           get { return _check_count; }
           set { _check_count = value; }
       }
    }
}
