using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Data.ViewModels
{
    public class MasterBeneficiariesVM
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [ScaffoldColumn(false)]
        public int ModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(false)]
        public DateTime? ModifiedOn { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public string FilePath { get; set; }
        public int Finalized { get; set; }
        public bool IsProcessed { get; set; }
    }
}
