using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
    public class MoveContributorContributionViewModel
    {
        public MoveContributorContributionViewModel()
        {
            this.Record = new List<OldAndNewContribution>();
        }
        public int success { get; set; }
        public string Error { get; set; }
        public List<OldAndNewContribution> Record { get; set; }
    }
    public class OldAndNewContribution
    {
        public string OldContributorName { get; set; }
        public string OldEmployerName { get; set; }
        public int OldDetailId { get; set; }
        public int OldDetailFinalizedId { get; set; }
        public decimal OldContributorContribution { get; set; }
        public decimal OldEmployerContribution { get; set; }
        public decimal OldSalaryAmount { get; set; }

        public string NewContributorName { get; set; }
        public string NewEmployerName { get; set; }
        public int NewDetailId { get; set; }
        public int NewDetailFinalizedId { get; set; }
        public decimal NewContributorContribution { get; set; }
        public decimal NewEmployerContribution { get; set; }
        public decimal NewSalaryAmount { get; set; }

        public string SourceName { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}
