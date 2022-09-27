using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace blog.Controllers
{
    public class newsController1 : Controller
    {
       
        private readonly ILogger<newsController1> _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;

        public newsController1(ILogger<newsController1> logger, IHostingEnvironment host)
        {
            _logger = logger;
            this.hosting = host;
        }


        context db = new context();

 #region addNews
        public IActionResult addNews()
        {

            return View();
        }
        [HttpPost]
        public IActionResult addNews(news n, IFormFile photo)
        {
            var v = db.news.Where(b => b.photo == photo.FileName).FirstOrDefault();
            string fileName = "";
            if (photo != null  && v==null)
            {
                string attachnews = Path.Combine(hosting.WebRootPath, "attachnews");
                fileName = photo.FileName;
                string fullPath = Path.Combine(attachnews, fileName);
                photo.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            n.photo = photo.FileName;
            n.customerID = (int)HttpContext.Session.GetInt32("customerId");
            n.publisher= (string)HttpContext.Session.GetString("customerName");
            n.date = DateTime.Now;
           
                db.news.Add(n);
                db.SaveChanges();
                return RedirectToAction("allNews", "newsController1");
            
           
        }

        #endregion


 #region allnews
        public IActionResult allNews()
        {

            return View(db.news.ToList());
        }
        #endregion

#region details
        public IActionResult details(int Id)
        {
            news s = db.news.Where(n => n.Id == Id).SingleOrDefault();
            return View(s);
        }
        #endregion

        //search for news(by category , username ,address)
 #region user news && if you an admeen to show user post &&  admeen to show all users post 
        public IActionResult yournews(int? Idd, string? search)
        {


            int? customId = HttpContext.Session.GetInt32("customerId");
            string? custemail=  HttpContext.Session.GetString("customeremail");
          
            List<news> li;
            //if you an admeen to show user post
            if(Idd!=null)
            {
                li = db.news.Where(n => n.customerID == Idd).ToList();
                if (li == null)
                    ViewBag.data = "This client has no posts yet!!";
            }
           
            //if you an admeen to show all users post
            else if (custemail == "admeen@gmail.com")
            {
                li = db.news.ToList();
            }
            else
            //if you is customer
            {
                li = db.news.Where(n => n.customerID == customId).ToList();
                
            }
            return View(li);
        }
        #endregion


#region update
        public IActionResult update(int Id)
        {
            news s = db.news.Where(n => n.Id == Id).SingleOrDefault();
            return View(s);
        }
        [HttpPost]
        public IActionResult update(news n, IFormFile photo)
        {
            string fileName = "";

            var x = db.news.Where(b => b.photo == photo.FileName).FirstOrDefault();

            if (photo != null && x == null)
            {
                string attachnews = Path.Combine(hosting.WebRootPath, "attachnews");
                fileName = photo.FileName;
                string fullPath = Path.Combine(attachnews, fileName);
                photo.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            n.photo = photo.FileName;
            n.customerID = (int)HttpContext.Session.GetInt32("customerId");
            n.publisher = (string)HttpContext.Session.GetString("customerName");
            n.date = DateTime.Now;

            db.Entry(n).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("yournews");
        }
        #endregion


#region delete
        public IActionResult delete(int id)
        {
            news s = db.news.Where(n => n.Id == id).SingleOrDefault();
            db.news.Remove(s);
            db.SaveChanges();
            return RedirectToAction("yournews");
        } 
#endregion



        

    }
}
