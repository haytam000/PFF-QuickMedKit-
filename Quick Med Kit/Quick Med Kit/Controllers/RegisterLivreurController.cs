using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick_Med_Kit.Models;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.Text;
using System.Net.Mail;

namespace Quick_Med_Kit.Controllers
{
    public class RegisterLivreurController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();
        SqlConnection cn = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=QuickMedkit;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        // GET: RegisterLivreur
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveDataLivreur(Livreur livreur)
        {
            livreur.IsValid = false;
            livreur.Isenligne = false;
            ourdb.Livreur.Add(livreur);
            ourdb.SaveChanges();
            BuildEmailTemplate(livreur.ID_Livreur);
            return Json("registration Successfull", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            Livreur Data = ourdb.Livreur.Where(x => x.ID_Livreur == regId).FirstOrDefault();
            Data.IsValid = true;
            ourdb.SaveChanges();
            var msg = "Votre Email a été Vérifié!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = ourdb.Livreur.Where(x => x.ID_Livreur == regID).FirstOrDefault();
            var url = "https://localhost:44390/" + "RegisterLivreur/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Votre Compte est bien Créer", body, regInfo.Email_Livreur);
        }
        public void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "quickmedkit@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);

        }
        public void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("quickmedkit@gmail.com", "Quick123@");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}