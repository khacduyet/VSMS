using NPOI.POIFS.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using VSMS.Models;
using VSMS.Models.DataModels;

namespace VSMS.Controllers
{
    public class RegisterController : BaseController
    {
        VSMS_Entities db = new VSMS_Entities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName, PassWord")] Member mem)
        {
            var md5 = Encryptor(mem.PassWord);
            var cus = db.Members.SingleOrDefault(x => x.UserName == mem.UserName && x.PassWord == md5);
            if (cus != null)
            {
                TempData["cus"] = cus.UserName;
                Session["customer"] = cus;
                return RedirectToAction("Index", "Home");
            }
            TempData["loginFail"] = "Wrong account name or password!";
            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            Session["customer"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SaveMember(Member mem)
        {
            var chk = db.Members.Where(x=> x.UserName == mem.UserName).SingleOrDefault();
            if (chk == null)
            {
                mem.Address = mem.BirthDay = mem.FullName = mem.Phone = "";
                mem.CreatedAt = DateTime.Now;
                mem.Status = 1;
                mem.EmailConfirmed = false;
                mem.PassWord = Common.CommonConstants.ParseMD5(mem.PassWord);
                db.Members.Add(mem);
                db.SaveChanges();
                BuildEmailTemplate(mem.Id);
                TempData["saveSc"] = "Signup successfully!";
                return RedirectToAction("About", "Home");
            }
            TempData["SaveEr"] = "User name has already!";
            return RedirectToAction("About", "Home");
        }

        public ActionResult Confirm(int id)
        {
            var data = db.Members.Where(x => x.Id == id).FirstOrDefault();
            data.EmailConfirmed = true;
            db.SaveChanges();
            TempData["info"] = "Hello " + data.UserName + "!";
            return RedirectToAction("Index","Home");
        }

        public void BuildEmailTemplate(int id)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Register/Template.cshtml"));
            var regInfo = db.Members.Where(x => x.Id == id).FirstOrDefault();
            var url = "http://localhost:63392" + Url.Action("Confirm", "Register") +"/"+ id;
            body = body.Replace("@ViewBag.ConfirmLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your account is successfully created!", body, regInfo.Email);
        }

        public ActionResult ResendEmail(int id)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Register/Template.cshtml"));
            var regInfo = db.Members.Where(x => x.Id == id).FirstOrDefault();
            var url = "http://localhost:63392" + Url.Action("Confirm", "Register") + "/" + id;
            body = body.Replace("@ViewBag.ConfirmLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your account is successfully created!", body, regInfo.Email);
            getAlert("Check registered mailboxes to confirm email!", "warning");
            return RedirectToAction("Index","ManagerMember");
        }

        public static void BuildEmailTemplate(string subText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "vsms.kd.ht@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subText;
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

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("vsms.kd.ht@gmail.com", "123123Aa");
            try
            {
                client.Send(mail);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}