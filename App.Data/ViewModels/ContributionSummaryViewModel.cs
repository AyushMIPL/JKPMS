using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Extentions;

namespace App.Data.ViewModels
{
    public class ContributionSummaryViewModel
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public string JKPSUniqueID { get; set; }

        public string SocialSecurityNo { get; set; }

        public int? SuffixId { get; set; }
        public string FirstName { get; set; }

        public string MidName { get; set; }

        public string LastName { get; set; }


        public ContributionSummaryViewModel TestCall()
        {
            var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            Data.Entities.MasterContributor entity = db.MasterContributor.Find(1);

            var model = new ContributionSummaryViewModel();
            entity.MapTo(model);


            return model;
        }
    }


}
