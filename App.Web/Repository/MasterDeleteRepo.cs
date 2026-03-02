using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using App.Data.ViewModels;
using App.Data;
using System.Web.Mvc;
namespace App.Web.Repository
{
    public class MasterDeleteRepo
    {
        //static AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        static AppDbContext db;

        public MasterDeleteRepo()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }
        public static bool AjaxDelete(int id, string colName)
        {
            int result = 0;
            var tblDetails = tables().Where(x => x.ColumnName.ToLower().Contains(colName.ToLower())).FirstOrDefault();
            if (tblDetails != null)
            {
                string[] columnName = tblDetails.ColumnName.Split(',');
                string[] tblName = tblDetails.tableName.Split(',');
                for (int i = 0; i < columnName.Length; i++)
                {
                    for (int j = 0; j < tblName.Length; j++)
                    {
                        try
                        {
                            result = db.Set("App.Data.Entities." + tblName[j]).AsQueryable().Where(columnName[i] + "==" + id + "&& IsActive==true").Count();
                        }
                        catch
                        {

                        }
                        if (result > 0)
                            return false;
                    }
                }
            }
            return true;
        }
        public static List<tableViewModel> tables()
        {
            List<tableViewModel> stemp = new List<tableViewModel>();
            stemp.Add(new tableViewModel { tableName = "SecRoleLocationModule", ColumnName = "RegionID" });
            stemp.Add(new tableViewModel { tableName = "RefundApplications", ColumnName = "InterestRateId" });
            stemp.Add(new tableViewModel { tableName = "PensionApplications", ColumnName = "DiscountForGratuityMainId,MainId" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributorMarriageDetails,MasterState,MasterContributor,MasterEmployer", ColumnName = "CountryID" });
            stemp.Add(new tableViewModel { tableName = "MasterCity", ColumnName = "StateId" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributor,MasterEmployer", ColumnName = "PermanentCityID,CityID" });
            stemp.Add(new tableViewModel { tableName = "PensionApplications,RefundApplications,MasterContributorJobDetails", ColumnName = "DesignationId" });
            stemp.Add(new tableViewModel { tableName = "MasterContributorJobDetails", ColumnName = "DepartmentId" });
            stemp.Add(new tableViewModel { tableName = "MasterContributorJobDetails", ColumnName = "GradeId" });
            stemp.Add(new tableViewModel { tableName = "MasterContributorMarriageDetails", ColumnName = "MaritalStatusId" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributorMarriageDetails,MasterContributor", ColumnName = "NationalityID" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner", ColumnName = "PensionerTypeId" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributor,MasterEmployer", ColumnName = "PFRateID" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributor", ColumnName = "PrefixId" });
            stemp.Add(new tableViewModel { tableName = "MasterPensioner,MasterContributor", ColumnName = "SuffixId" });
            stemp.Add(new tableViewModel { tableName = "MasterDependantDetails", ColumnName = "RelationshipID" });
            stemp.Add(new tableViewModel { tableName = "MasterInterestRate,MasterDiscountForGratuityMain,MasterContributorJobDetails,MasterDiscountForGratuity,MasterEmployer", ColumnName = "EmployerTypeID,EmployerType" });
            stemp.Add(new tableViewModel { tableName = "MasterContributorJobDetails", ColumnName = "JobTitleID" });
            return stemp;
        }
    }

}