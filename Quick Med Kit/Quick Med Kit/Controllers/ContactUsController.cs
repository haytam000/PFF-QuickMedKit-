using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Quick_Med_Kit.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(SendMailDto sendMail)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                MailMessage mail = new  MailMessage();
                mail.From = new MailAddress("quickmedkit@gmail.com");
                mail.To.Add("haytamjbilouu@gmail.com");
                mail.Subject = sendMail.Email;
                mail.IsBodyHtml = true;
                string content = "Name : " + sendMail.Name;
                content +="<br/> Message :"+ sendMail.Email;

                mail.Body = content;

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                NetworkCredential networkCredential = new NetworkCredential("quickmedkit@gmail.com", "Quick123@");
                client.UseDefaultCredentials = false;
                client.Credentials = networkCredential;
                client.Port = 25;
                client.EnableSsl = false;
                client.Send(mail);
                ViewBag.Message = "Votre Message est bien envoyé";
                ModelState.Clear();

            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }

            return View();
        }
    }
}