using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
    {
    public class EditApplicantFields  
        {
        public int Id { get; set; }
        public int RoleID { get; set; }
        //public string RoleID { get; set; }
        public string FieldName { get; set; }      
        public Nullable<bool> AllowEdit { get; set; }
        public string TableName { get; set; }
        public int CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        
        public DateTime? CreatedOn { get; set; }
        
        public int ModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
       
        public DateTime? ModifiedOn { get; set; }
     
    }
}