using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Quick_Med_Kit.Models;
using System.Net;
using System.Data.Entity;

namespace Quick_Med_Kit.Controllers
{
    public class LivreurController : Controller
    {
        QuickMedkitEntities db = new QuickMedkitEntities();
        SqlConnection cn = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=QuickMedkit;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rd;
        // GET: Livreur
        public ActionResult Index()
        {
            if (Session["Livreur_email"] == null)
            {
                return RedirectToAction("LoginLivreur", "Utilisateur");
            }
            else
            {
                cmd = new SqlCommand("select Code_Medicament,Date_Commande,Quantity,Prix,total from Commande where ID_Livreur=@id",cn);
                cmd.Parameters.AddWithValue("@id", Session["id_livreur"]);
                cn.Open();
                rd = cmd.ExecuteReader();
                List<Commande> commandes = new List<Commande>();
                while (rd.Read())
                {
                    Commande comd = new Commande();
                    comd.Code_Medicament = int.Parse(rd[0].ToString());
                    comd.Date_Commande = Convert.ToDateTime(rd[1]);
                    comd.Quantity = int.Parse(rd[2].ToString());
                    comd.Prix = int.Parse(rd[3].ToString());
                    comd.total = int.Parse(rd[4].ToString());
                    commandes.Add(comd);
                }
                cn.Close();
                return View(commandes.ToList());

            }

        }
        public ActionResult Profil()
        {
            if (Session["Livreur_email"] == null)
            {
                return RedirectToAction("LoginLivreur", "Utilisateur");
            }
            else
            {
                cmd = new SqlCommand("SELEct Nom_Livreur,Prenom_Livreur,CIN,Email_Livreur,telephone_Livreur,age,Pswrd_telephone_Livreur from Livreur where ID_Livreur=@id", cn);
                cmd.Parameters.AddWithValue("@id", Session["id_livreur"]);
                cn.Open();
                rd = cmd.ExecuteReader();
                Livreur livreur = new Livreur();
                while (rd.Read())
                {
                    livreur.Nom_Livreur = rd[0].ToString();
                    livreur.Prenom_Livreur = rd[1].ToString();
                    livreur.CIN = rd[2].ToString();
                    livreur.Email_Livreur = rd[3].ToString();
                    livreur.telephone_Livreur = int.Parse(rd[4].ToString());
                    livreur.age = int.Parse(rd[5].ToString());
                    livreur.Pswrd_telephone_Livreur = rd[6].ToString();
                }
                cn.Close();
                return View(livreur);

            }
        }
        public ActionResult ModifierProfil()
        {
            if (Session["Livreur_email"] == null)
            {
                return RedirectToAction("LoginLivreur", "Utilisateur");
            }
            else
            {
                if (Session["id_livreur"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Livreur livreur = db.Livreur.Find(Session["id_livreur"]);
                if (livreur == null)
                {
                    return HttpNotFound();
                }
                return View(livreur);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierProfil(Livreur livreur)
        {
            if (ModelState.IsValid)
            {
                cmd = new SqlCommand("update Livreur set Nom_Livreur=@nom,Prenom_Livreur=@prn,CIN=@cin,Email_Livreur=@email,telephone_Livreur=@telephone,age=@age,Pswrd_telephone_Livreur=@pswrd where ID_Livreur=@id", cn);
                cmd.Parameters.AddWithValue("@id", Session["id_livreur"]);
                cmd.Parameters.AddWithValue("@nom",livreur.Nom_Livreur);
                cmd.Parameters.AddWithValue("@prn", livreur.Prenom_Livreur);
                cmd.Parameters.AddWithValue("@cin", livreur.CIN);
                cmd.Parameters.AddWithValue("@email", livreur.Email_Livreur);
                cmd.Parameters.AddWithValue("@telephone", livreur.telephone_Livreur);
                cmd.Parameters.AddWithValue("@age", livreur.age);
                cmd.Parameters.AddWithValue("@pswrd", livreur.Pswrd_telephone_Livreur);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction("Profil");
            }
            return View(livreur);
        }
        public ActionResult Supprimer(int? id)
        {
            if (Session["Livreur_email"] == null)
            {
                return RedirectToAction("LoginLivreur", "Utilisateur");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Livreur livreur = db.Livreur.Find(id);
                if (livreur == null)
                {
                    return HttpNotFound();
                }
                return View(livreur);
            }
        }
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livreur livreur = db.Livreur.Find(id);
            db.Livreur.Remove(livreur);
            db.SaveChanges();
            return RedirectToAction("LoginLivreur", "Utilisateur");
        }
        public ActionResult Deconnexion()
        {
            Status(int.Parse(Session["id_livreur"].ToString()));
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public JsonResult Status(int idliv)
        {
            Livreur liv = db.Livreur.Where(x => x.ID_Livreur == idliv).FirstOrDefault();
            var msg="";
            if(liv.Isenligne==false)
            {
                liv.Isenligne = true;
                Session["Status"] = liv.Isenligne;
                msg = "En Ligne";
            }
            else
            {
                liv.Isenligne = false;
                Session["Status"] = liv.Isenligne;
                msg = "Offline";
            }
            db.SaveChanges();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}