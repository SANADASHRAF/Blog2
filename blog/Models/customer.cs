using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models
{
    
    public class customer
    {
        public int Id { get; set; }
        public string username { get; set; }
        [Range(21,100,ErrorMessage ="you must greater than 20 years old")]
        public int age { get; set; }
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Not Vaild Email")]
        public string email { get; set; }
        public string password { get; set; }
        [NotMapped]
        [Compare("password", ErrorMessage = "password not match")]
        public string ConfirmPassword { set; get; }


        public string adress { get; set; }
        public string photo { get; set; }
        public ICollection<news> news { get; set; } 
        public ICollection<contactt> contactt { get; set; }
    }
}
