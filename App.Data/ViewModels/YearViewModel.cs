using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
    public class YearModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class tableViewModel
    {
        public string tableName { get; set; }
        public string ColumnName { get; set; }

    }
    public class ErrprInContributorDetailsViewModel
    {
        public string Personid { get; set; }
        public string Name { get; set; }
        public string Error { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int DependantId { get; set; }
        public int MarriageId { get; set; }
        public int JobDetailId { get; set; }
        public int Id { get; set; }
        public int JobStatusId { get; set; }
        public string EmployerName { get; set; }
        public int EmployerId { get; set; }
    }
}
