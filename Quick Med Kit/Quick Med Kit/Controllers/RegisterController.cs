using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Quick_Med_Kit.Models;


namespace Quick_Med_Kit.Controllers
{
    public class RegisterController : Controller
    {
        QuickMedkitEntities ourdb = new QuickMedkitEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveData( Utilisateur utilisateur)
        {
            utilisateur.IsValid = false;
            ourdb.Utilisateur.Add(utilisateur);
            ourdb.SaveChanges();
            BuildEmailTemplate(utilisateur.ID_Utilisateur);
            return Json("registration Successfull", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            Utilisateur Data = ourdb.Utilisateur.Where(x => x.ID_Utilisateur == regId).FirstOrDefault();
            Data.IsValid = true;
            ourdb.SaveChanges();
            var msg = "Votre Email a été Vérifié!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/")+"Text"+".cshtml");
            var regInfo = ourdb.Utilisateur.Where(x => x.ID_Utilisateur == regID).FirstOrDefault();
            var url = "https://localhost:44390/" + "Register/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Votre Compte est bien Créer", body, regInfo.Email_Utilisateur);
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
            if(!string.IsNullOrEmpty(bcc))
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