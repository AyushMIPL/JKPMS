using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    public class MasterDependantDetails : BaseEntity
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.MidName + " " + this.LastName;
            }
        }
        public MasterContributor Contributor
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.MasterContributor.Where(x => x.PersonID == this.PersonID).FirstOrDefault();
            }
        }
        [Display(Name = "JKPS ID")]
        [StringLength(10)]
        public string PersonID { get; set; }


        [Required(ErrorMessage = "Enter First Name First")]
        [Display(Name = "First Name *")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Mid Name")]
        [StringLength(50)]
        public string MidName { get; set; }
        [Required(ErrorMessage = "Enter Last Name First")]
        [Display(Name = "Last Name *")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [StringLength(1)]
        [Column(TypeName = "char")]
        [UIHint("Gender")]
        public string Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth *")]
        [Column(TypeName = "datetime")]

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Select Relationship First")]
        [Display(Name = "Relationship Type *")]
        public int? RelationshipID { get; set; }
        public virtual MasterRelationshipType Relationship { get; set; }

        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Termination Date")]

        public DateTime? TerminationDate { get; set; }

        /*[Required(ErrorMessage = "Select Dependant Status First")]
        [Display(Name = "Dependant Status")]
        public int DependantStatusID { get; set; }*/
    }
}
