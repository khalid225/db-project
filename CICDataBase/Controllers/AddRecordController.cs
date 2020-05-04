using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICDataBase.Controllers
{
    public class AddRecordController : Controller
    {
        // GET: AddRecord
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(CICDataBase.Models.Record ModelRecord)
        {
            return View();
        }
    }
}