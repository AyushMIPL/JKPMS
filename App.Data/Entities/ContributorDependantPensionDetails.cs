using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ContributorDependantPensionDetails : BaseEntity
  {
    public int ApplicationId { get; set; }
    public string PersonID { get; set; }
    public int DependantID { get; set; }
    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("RelationshipType")]
    public string RelationshipType { get; set; }
    public decimal PensionableAmount { get; set; }
  }
}

