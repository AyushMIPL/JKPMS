using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using Microsoft.Ajax.Utilities;
// using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Repository
{
    public class CommomRepo
    {
        //private AppDbContext db = new AppDbContext();
        //static AppDbContext db = new AppDbContext();
        //to Calculate Refund Estimate, if estimateStatus is Yes than Calculation on Estimation

        public static List<Region> GetUserAssignedDistrict(int RoleId, string RoleName, int UserId, string DistrictName, string regionname)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(regionname);
                //db = new AppDbContext();
                ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
                AppDbContext db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                List<int> DistrictIds = new List<int>();
                if (!string.IsNullOrEmpty(DistrictName))
                {
                    List<string> Names = DistrictName.ToLower().Split(',').ToList();
                    Names = Names.Select(x => x.Trim()).Distinct().ToList();
                    DistrictIds = db.MasterDistrict.Where(x => Names.Contains(x.Name.ToLower().Trim())).Select(x => x.Id).Distinct().ToList();
                }
                int RegionId = 0;
                if (RoleName == "Admin" || RoleName == "Director Finance (DFSW)" || RoleName == "Districts")
                    RegionId = 0;
                else if (RoleName == "Directorate Jammu (DJ)")
                    RegionId = 1;
                else if (RoleName == "Directorate Kashmir (DK)")
                    RegionId = 2;
                else
                    RegionId = 0;
                var RegId = db.SecRoleLocationModule.Where(lm => lm.IsActive && lm.RoleID == RoleId && lm.UserId == UserId).Select(x => x.RegionID).Distinct().ToList();
                var DistrictId = db.SecRoleLocationModule.Where(lm => lm.IsActive && lm.RoleID == RoleId && lm.UserId == UserId).Select(x => x.DistrictID).Distinct().ToList();
                var masterRegion = db.MasterRegion.Where(x => x.IsActive && x.Name.Contains(regionname) && (RegionId > 0 ? (x.Id == RegionId) : RegId.Contains(x.Id))).Select(x => new App.Data.ViewModels.Region
                {
                    id = x.Id,
                    title = x.Name,
                    Selected = db.SecRoleLocationModule.Any(lm => lm.IsActive && lm.RegionID == x.Id && lm.RoleID == RoleId && lm.UserId == UserId && ((!string.IsNullOrEmpty(DistrictName) && DistrictIds.Count() > 0) ? DistrictIds.Contains(lm.DistrictID) : true)),
                    subs = db.MasterDistrict.Where(d => d.IsActive && d.RegionId == x.Id && (DistrictId.Contains(d.Id))).Select(d => new App.Data.ViewModels.District
                    {
                        id = d.Id,
                        title = d.Name,
                        Selected = db.SecRoleLocationModule.Any(lm => lm.IsActive && lm.DistrictID == d.Id && lm.RoleID == RoleId && lm.UserId == UserId && ((!string.IsNullOrEmpty(DistrictName) && DistrictIds.Count() > 0) ? DistrictIds.Contains(lm.DistrictID) : true))
                    }).ToList()
                }).ToList();

                sb.AppendLine(masterRegion.Count.ToString());
                //string filePath = @"D:\J & K\App.Web\UserFiles\ProfileImages\test.txt";
                //System.IO.File.AppendAllLines(filePath, new string[] { sb.ToString() });
                return masterRegion;
            }
            catch (Exception ex)
            {
                return new List<Region>();
            }

        }
        public static List<Region> GetAssignedDistrictForEdit(int RId, int UId, string regionname)
        {
            ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
            AppDbContext db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            int Id = Convert.ToInt32(AppUserManager.GetUserId());
            var userRoles = db.Roles.Include(r => r.Users).ToList();
            var userRoleNames = (from r in userRoles
                                 from u in r.Users
                                 where u.UserId == Id
                                 select (r.Id, r.Name, u.UserId)).FirstOrDefault();
            int UserId = userRoleNames.UserId;
            int RoleId = userRoleNames.Id;
            string RoleName = userRoleNames.Name;
            int RegionId = 0;
            if (RoleName == "Admin" || RoleName == "Director Finance (DFSW)" || RoleName == "Districts")
            RegionId =/*regionname == "JAMMU REGION"? 1:*/ 0;
            else if (RoleName == "Directorate Jammu (DJ)")
                RegionId = 1;
            else if (RoleName == "Directorate Kashmir (DK)")
                RegionId = 2;
            else
                RegionId = 0;
            var RegId = db.SecRoleLocationModule.Where(lm => lm.IsActive && lm.RoleID == RoleId && lm.UserId == UserId).Select(x => x.RegionID).Distinct().ToList();
            var DistrictId = db.SecRoleLocationModule.Where(lm => lm.IsActive && lm.RoleID == RoleId && lm.UserId == UserId).Select(x => x.DistrictID).Distinct().ToList();
            var masterRegion = db.MasterRegion.Where(x => x.IsActive && x.Name.Contains(regionname) && (RegionId > 0 ? (x.Id == RegionId) : RegId.Contains(x.Id))).Select(x => new App.Data.ViewModels.Region
            {
                id = x.Id,
                title = x.Name,
                Selected = db.SecRoleLocationModule.Any(lm => lm.IsActive && lm.RegionID == x.Id && lm.RoleID == RId && lm.UserId == UId),
                subs = db.MasterDistrict.Where(d => d.IsActive && d.RegionId == x.Id && (DistrictId.Contains(d.Id))).Select(d => new App.Data.ViewModels.District
                {
                    id = d.Id,
                    title = d.Name,
                    Selected = db.SecRoleLocationModule.Any(lm => lm.IsActive && lm.DistrictID == d.Id && lm.RoleID == RId && lm.UserId == UId)
                }).ToList()
            }).ToList();
            return masterRegion;
        }
    }
}