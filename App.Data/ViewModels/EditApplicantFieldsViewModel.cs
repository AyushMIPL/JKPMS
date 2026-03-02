using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
    public class EditApplicantFieldsViewModel
    {
        public int Id { get; set; }
        public string RoleID { get; set; }
        public string FieldName { get; set; }
        public bool AllowEdit { get; set; }
        public int CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]

        public DateTime? CreatedOn { get; set; }


        public int ModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]

        public DateTime? ModifiedOn { get; set; }
        [NotMapped]
        public string TableName { get; set; }
  }
}
