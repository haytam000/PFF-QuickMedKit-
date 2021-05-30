using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quick_Med_Kit.Models;

namespace Quick_Med_Kit.Controllers
{
    public class MédicamentController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();
        private List<ShoppingCart> shoppings=new List<ShoppingCart>();
        // GET: Médicament
        public ActionResult Index()
        {
            return View(ourdb.Medicament.ToList());
        }
        [HttpPost]
        public JsonResult Index(int ItemId)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            Medicament objItem = ourdb.Medicament.Single(model => model.Code_Medicament == ItemId);
            if(Session["CartCounter"] != null)
            {
                shoppings = Session["CartItem"] as List<ShoppingCart>;
            }
            if(shoppings.Any(model => model.ItemId == ItemId))
            {
                shoppingCart = shoppings.Single(model => model.ItemId == ItemId);
                shoppingCart.Quantity = shoppingCart.Quantity + 1;
                shoppingCart.Total = Convert.ToDecimal(objItem.Prix) * shoppingCart.Quantity;
            }
            else
            {
                shoppingCart.ItemId = ItemId;
                shoppingCart.ImagePath = objItem.upload_images;
                shoppingCart.Description = objItem.Description;
                shoppingCart.Quantity = 1;
                shoppingCart.Prix = Convert.ToDecimal(objItem.Prix);
                shoppingCart.Total = Convert.ToDecimal(objItem.Prix);
                shoppings.Add(shoppingCart);

            }
            Session["CartCounter"] = shoppings.Count;
            Session["CartItem"] = shoppings;
            return Json(data:new { succes=true, Counter = shoppings.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart()
        {
            shoppings = Session["CartItem"] as List<ShoppingCart>;
            return View(shoppings);
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            shoppings = Session["CartItem"] as List<ShoppingCart>;
            if (Session["id_utilisateur"]==null)
            {
                return RedirectToAction("Login", "Utilisateur");
            }
            else
            {
                if (Convert.ToBoolean(Session["Status"]) == true)
                {
                    foreach (var item in shoppings)
                    {
                        Commande cmd = new Commande();
                        cmd.Code_Medicament = int.Parse(Session["codemdic"].ToString());
                        cmd.ID_Utilisateur = int.Parse(Session["id_utilisateur"].ToString());
                        cmd.ID_Livreur = int.Parse(Session["id_livreur"].ToString());
                        cmd.Date_Commande = DateTime.Now;
                        cmd.Quantity = item.Quantity;
                        cmd.Prix = item.Prix;
                        cmd.total = item.Total;
                        ourdb.Commande.Add(cmd);
                        ourdb.SaveChanges();
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            Session["CartItem"] = null;
            return RedirectToAction("Index");
        }
    }
}