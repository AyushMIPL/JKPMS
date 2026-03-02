using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;
using App.Web.Models;
using App.Web.Filters;
using App.Web.Repository;
using System.Threading.Tasks;
using App.Web.Helper;
using App.Data.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using JKPS.BLL;
using JKPS.COMMON;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class GenericPensionProcessController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public GenericPensionProcessController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }


        public ActionResult Index()
        {
            List<DynamicMenuItem> menus = new List<DynamicMenuItem>();
            List<App.Data.ViewModels.SessionViewModel> model;
            model = (List<App.Data.ViewModels.SessionViewModel>)Session["list"];
            if (model != null)
            {
                foreach (var item in model.Where(x => x.ViewPermission == true && x.ParentId == 14))
                {
                    int parentId = 0;
                    if (item.ParentId == 0)
                    {
                        parentId = 1;
                    }

                    menus.Add(new DynamicMenuItem { LinkText = @item.ModuleName, ActionName = @item.ActionName, ControllerName = @item.ControllerName, Class = item.ModuleClass, IsParent = parentId, MenuId = Convert.ToInt32(@item.ModuleID), ParentMenuId = @item.ParentId, DisplayOrder = @item.DisplayOrder });
                }
                var list = menus.OrderBy(x => x.DisplayOrder);
                return View(menus);
            }
            return View(model);

        }

        //close batch

        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBankMedia()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintCheques()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintBankListing()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePensionProcess()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePaySlipDetails()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBatchProcessAjax()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseBatchProcessAjax()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectDepositEntries()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectDepositIndex()
        {
            return null;
        }


        #endregion
    }
}
