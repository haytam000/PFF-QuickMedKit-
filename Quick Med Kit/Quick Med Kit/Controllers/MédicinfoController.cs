using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick_Med_Kit.Models;
using System.Data.SqlClient;
using System.Net;

namespace Quick_Med_Kit.Controllers
{
    public class MédicinfoController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();

        // GET: Médicinfo
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medic = ourdb.Medicament.Find(id);
            Session["codemdic"] = id;
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }
    }
}