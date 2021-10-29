using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VSMS.Controllers
{
    public class BaseController : Controller
    {
        protected void getAlert(string msg, string type)
        {
            TempData["messenger"] = msg;
            if (type == "success")
            {
                TempData["dataType"] = "alert-success";
            }
            else if (type == "danger")
            {
                TempData["dataType"] = "alert-danger";
            }
            else if (type == "warning")
            {
                TempData["dataType"] = "alert-warning";
            }
            else if (type == "primary")
            {
                TempData["dataType"] = "alert-primary";
            }
            else if (type == "info")
            {
                TempData["dataType"] = "alert-info";
            }
        }

        protected void getPassForm(string old, string pass, string rep)
        {
            ViewData["OldPass"] = old;
            ViewData["PassWord"] = pass;
            ViewData["Repass"] = rep;
        }
    }
}