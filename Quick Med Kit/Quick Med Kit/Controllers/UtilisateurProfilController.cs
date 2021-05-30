using Quick_Med_Kit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Quick_Med_Kit.Controllers
{
    public class UtilisateurProfilController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();
        SqlConnection cn = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=QuickMedkit;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rd;
        // GET: UtilisateurProfil
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profil()
        {
            if (Session["id_utilisateur"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = ourdb.Utilisateur.Find(Session["id_utilisateur"]);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        public ActionResult Modifier()
        {
            if (Session["id_utilisateur"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = ourdb.Utilisateur.Find(Session["id_utilisateur"]);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifier(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                cmd = new SqlCommand("update Utilisateur set Nom_Utiliateur=@nom,Email_Utilisateur=@email,telephone_Utilisateur=@telephone,Pswrd_Utilisateur=@pswrd where ID_Utilisateur=@id", cn);
                cmd.Parameters.AddWithValue("@id", Session["id_utilisateur"]);
                cmd.Parameters.AddWithValue("@nom",utilisateur.Nom_Utiliateur);
                cmd.Parameters.AddWithValue("@email", utilisateur.Email_Utilisateur);
                cmd.Parameters.AddWithValue("@telephone", utilisateur.telephone_Utilisateur);
                cmd.Parameters.AddWithValue("@pswrd", utilisateur.Pswrd_Utilisateur);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction("Profil");
            }
            return View(utilisateur);
        }
        public ActionResult Supprimer(int? id)
        {
            if (Session["id_utilisateur"] == null)
            {
                return RedirectToAction("Login", "Utilisateur");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = ourdb.Utilisateur.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
        }
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = ourdb.Utilisateur.Find(id);
            ourdb.Utilisateur.Remove(utilisateur);
            ourdb.SaveChanges();
            return RedirectToAction("Login", "Utilisateur");
        }
        public ActionResult Deconnexion()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}