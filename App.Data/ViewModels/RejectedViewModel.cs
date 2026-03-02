using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entities;
namespace App.Data.ViewModels
{
    public class RejectedViewModel
    {
        //public int ApprovalProcessId { get; set; }
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public MasterContributor contributor
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return (from a in db.PensionApplications join b in db.MasterContributor on a.PersonID equals b.Id.ToString() where a.Id == this.ApplicationId select b).FirstOrDefault();
            }
        }
        public UserProfile user
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.UserProfiles.Where(x => x.UserId.ToString() == this.ApprovalUser).FirstOrDefault();
            }
        }
        public int ApplicationId { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalUser { get; set; }
        public DateTime? AppliedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public string Notes { get; set; }
        public string Print { get; set; }
        public int approvalProcessId { get; set; }

    }
    public class RRejectedViewModel
    {
        //public int ApprovalProcessId { get; set; }
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public MasterContributor contributor
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return (from a in db.RefundApplications join b in db.MasterContributor on a.PersonID equals b.Id.ToString() where a.Id == this.ApplicationId select b).FirstOrDefault();
            }
        }
        public UserProfile user
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.UserProfiles.Where(x => x.UserId.ToString() == this.ApprovalUser).FirstOrDefault();
            }
        }
        public int ApplicationId { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalUser { get; set; }
        public DateTime? AppliedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public string Notes { get; set; }
        public string Print { get; set; }
        public int approvalProcessId { get; set; }

    }
}
