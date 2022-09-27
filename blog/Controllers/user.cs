using blog.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace blog.Controllers
{
    public class user : Controller
    {

        context db = new context();
        private readonly ILogger<user> _logger;
        private readonly IHostingEnvironment host;

        //private readonly IWebHostEnvironment host;

        public user(ILogger<user> logger,IHostingEnvironment host)
        {
            _logger = logger;
            this.host = host;
        }



#region register
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult register(customer c, IFormFile photo)
        {
            var v = db.news.Where(b => b.photo == photo.FileName).FirstOrDefault();
            string fileName = "";
            if (photo != null && v == null)
            {
                string attach = Path.Combine(host.WebRootPath, "attaches");
                fileName = photo.FileName;
                string fullPath = Path.Combine(attach, fileName);
                photo.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            c.photo = photo.FileName;
            customer x = db.customer.Where(n => n.email == c.email).FirstOrDefault();
            if (x != null)
            {
                ViewBag.massege4 = $"the user of this email {c.email}is exist before!!";
                return View(c);
            }
            c.photo = photo.FileName;


            db.customer.Add(c);
            db.SaveChanges();
            return RedirectToAction("login");


        }
        #endregion


#region signin

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(customer z)
        {
            customer c = db.customer.Where(n => n.email == z.email && n.password == z.password).SingleOrDefault();
            if (c != null)
            {

                HttpContext.Session.SetInt32("customerId", c.Id);
                HttpContext.Session.SetString("customerName", c.username);
                HttpContext.Session.SetString("customeremail", c.email);

                return RedirectToAction("yournews", "newsController1");
            }

            {
                ViewBag.msg = "Incorrect Email or Password";
                return View(c);
            }

        }
        #endregion


#region  profile
        public IActionResult profile()
        {
            //session
            int? customerId = HttpContext.Session.GetInt32("customerId");
            if (customerId == null)
                return RedirectToAction("login");

            customer s = db.customer.Where(n => n.Id == customerId).SingleOrDefault();

            return View(s);
        }
        #endregion



 #region logout
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "user");
        } 
 #endregion


#region add contact

        public IActionResult contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult contact(contactt c)
        {
            c.customerId=(int)HttpContext.Session.GetInt32("customerId");
            db.contactts.Add(c);
            db.SaveChanges();

            return RedirectToAction("contact");
        }
#endregion


#region about
        public IActionResult about()
        {
            return View();
        }
        #endregion


 #region your complaints
        public IActionResult complaints(int? Idd)
        {
           string? custemail= HttpContext.Session.GetString("customeremail");
           int? custId = HttpContext.Session.GetInt32("customerId");
            List<contactt> li;
            //if you admeen and want to show the complaints for user بعينة
            if(Idd!=null)
            {
                li=db.contactts.Where(n => n.customerId==Idd).ToList();
            }
            //if you admeen to show all complaints
            else if (custemail == "admeen@gmail.com")
            {
                li = db.contactts.ToList();
            }
            //complaints of the user that has login
            else
            {
                li = db.contactts.Where(n => n.customerId == custId).ToList();
            }
               
            return View(li);
        } 
#endregion
     
        
#region delete complaints
        public IActionResult deletecomplaints(int Id)
        {
            contactt s = db.contactts.Where(n => n.Id == Id).SingleOrDefault();
            db.contactts.Remove(s);
            db.SaveChanges();
            return RedirectToAction("complaints");
        }
#endregion


        public IActionResult editprofile()
        {
            return View();
        }

    }
}
