using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers
{
    public class admeen : Controller
    {
        context db = new context();


#region AllUsers
        public IActionResult AllUsers(string? search)
        {
            List<customer> li;
            if (search != null)
            {
                li = db.customer.Where(n => n.username == search).ToList();
            }
           
            else
            {
                li = db.customer.ToList();
            }
           
            return View(li);
        }
        #endregion



 #region ShowUserProfile
        public IActionResult ShowUserProfile(int Id)
        {
            customer c = db.customer.Where(n => n.Id == Id).FirstOrDefault();
            return View(c);
        } 
#endregion



#region deleteuser
        public IActionResult deleteuser(int Id)
        {
            customer c = db.customer.Where(n => n.Id == Id).FirstOrDefault();
            db.customer.Remove(c);
            db.SaveChanges();
            return RedirectToAction("AllUsers", "admeen");
        } 
#endregion

        public IActionResult searchd(string? s)
        {
            List<news> li= db.news.Where(n => n.publisher == s).ToList(); 
              
            return View(li);
        }

    }
}

