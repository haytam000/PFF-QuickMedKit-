using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quick_Med_Kit.Models;


namespace Quick_Med_Kit.Controllers
{
    public class AdminController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Utilisateur");
            }
            else
            {
                return View(ourdb.Medicament.ToList());
            }
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medic = ourdb.Medicament.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }
        public ActionResult Supprimer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medic = ourdb.Medicament.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicament medic = ourdb.Medicament.Find(id);
            ourdb.Medicament.Remove(medic);
            ourdb.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Modifier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medic = ourdb.Medicament.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifier([Bind(Include = "Code_Medicament,Nom,Description,Date_Production,Date_expiration,Droit_Usage,Prix,upload_images")] Medicament medic)
        {
            if (ModelState.IsValid)
            {
                ourdb.Entry(medic).State = EntityState.Modified;
                ourdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medic);
        }
        public ActionResult Ajouter()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajouter([Bind(Include = "Code_Medicament,Nom,Description,Date_Production,Date_expiration,Droit_Usage,Prix,upload_images")] Medicament medic)
        {
            if (ModelState.IsValid)
            {
                ourdb.Medicament.Add(medic);
                ourdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medic);
        }
        public ActionResult ListCommande()
        {
            return View(ourdb.Commande.ToList());
        }
        public ActionResult Deconnexion()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}