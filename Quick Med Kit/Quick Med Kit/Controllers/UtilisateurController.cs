using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick_Med_Kit.Models;
using System.Net;
using System.Net.Mail;

namespace Quick_Med_Kit.Controllers
{
    public class UtilisateurController : Controller
    {
        QuickMedkitEntities db = new QuickMedkitEntities();
        // GET: Utilisateur
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Utilisateur Utilisateur)
        {
            /*if(ModelState.IsValid)
            {
                //string PSWRD = PasswordEncryption.Encode(Utilisateur.Pswrd_Utilisateur);
            }*/
            var utilisateur = db.Utilisateur.FirstOrDefault(u => u.Email_Utilisateur == Utilisateur.Email_Utilisateur && u.Pswrd_Utilisateur == Utilisateur.Pswrd_Utilisateur);
            if(utilisateur==null)
            {
                ViewBag.message = "Votre Email ou mot de Passe est Incorrect,Réessayer";
            }
            else
            {
                if(utilisateur.Email_Utilisateur=="admin@gmail.com" && utilisateur.Pswrd_Utilisateur== "adminadmin")
                {
                    Session["Admin"] = utilisateur.Email_Utilisateur;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["Nom"] = utilisateur.Email_Utilisateur;
                    Session["id_utilisateur"] = utilisateur.ID_Utilisateur;
                    Session["Nom_Utilisateur"] = utilisateur.Nom_Utiliateur;
                    return RedirectToAction("Index", "UtilisateurProfil");
                }
            }
            return View();
        }
        public ActionResult LoginLivreur()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginLivreur(Livreur Livreur)
        {
            /*if(ModelState.IsValid)
            {
                //string PSWRD = PasswordEncryption.Encode(Utilisateur.Pswrd_Utilisateur);
            }*/
            var livreur = db.Livreur.FirstOrDefault(u => u.Email_Livreur == Livreur.Email_Livreur && u.Pswrd_telephone_Livreur == Livreur.Pswrd_telephone_Livreur);
            if (livreur == null)
            {
                ViewBag.message = "Votre Email ou mot de Passe est Incorrect,Réessayer";
            }
            else
            {
                Session["Livreur_nom"] = livreur.Nom_Livreur+" "+livreur.Prenom_Livreur;
                Session["Livreur_email"] = livreur.Email_Livreur;
                Session["id_livreur"] = livreur.ID_Livreur;
                return RedirectToAction("Index", "Livreur");
            }
            return View();
        }

    }
}