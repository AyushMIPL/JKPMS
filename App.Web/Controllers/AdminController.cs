using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Data.Entities;
using System.Data.Entity;
using App.Web.Filters;
using System.Net;
using App.Data;

namespace App.Web.Controllers
{
    public class AdminController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public AdminController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile entity = db.UserProfiles.Where(x => x.UserId == id).FirstOrDefault();
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id;
            return View(entity);
        }

    }
}