using CICDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICDataBase.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(User UserModel)
        {
            using (NAICDatabaseEntities db = new NAICDatabaseEntities())
            {
                var LoginDetail = db.Users.Where(x => x.UserName == UserModel.UserName && x.Password == UserModel.Password).FirstOrDefault();
                if (LoginDetail == null)
                {
                    UserModel.ErrorMessage = "invalid Login";
                    return View("Index", UserModel);
                }
                else
                {
                    Session["UserID"] = LoginDetail.UserID;
                    Session["UserName"] = LoginDetail.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
                
        }

        public ActionResult Log()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}