using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class RoleModuleViewModel
  {
    public string ModuleName { get; set; }
    public string ModuleDesc { get; set; }
    public int ParentId { get; set; }
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    public string RoleID { get; set; }
    public int ModuleID { get; set; }
    public bool ViewPermission { get; set; }
    public bool AddPermssion { get; set; }
    public bool EditPermission { get; set; }
    public bool DeletePermission { get; set; }
    public List<Region> Group { get; set; }
  }
  public class Region
  {
    public int id { get; set; }
    public string title { get; set; }
    public bool Selected { get; set; }
    public List<District> subs { get; set; }
  }
  public class District
  {
    public int id { get; set; }
    public string title { get; set;}
    public bool Selected { get; set;}
  }
  public class SessionViewModel
  {
    //public string ModuleName { get; set; }
    //public string ModuleDesc { get; set; }
    //public int ParentId { get; set; }
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    //public int RoleID { get; set; }
    //public int ModuleID { get; set; }
    public bool? ViewPermission { get; set; }
    public bool? AddPermssion { get; set; }
    public bool? EditPermission { get; set; }
    public bool? DeletePermission { get; set; }
    public string ModuleName { get; set; }
    public int ParentId { get; set; }
    public int RoleID { get; set; }
    public int ModuleID { get; set; }
    public string ModuleClass { get; set; }
    public int? DisplayOrder { get; set; }
    public string Region { get; set; }
  }
}
