using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace blog.Models
{
    public class news
    {

      
       
        public int Id { get; set; }
        public string title { get; set; }
        public string pref { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }       
        public string photo { get; set; }
        public string category { get; set; }
        public string publisher { get; set; }
        public int? userID { get; set; }

        public int customerID { get; set; }

        [ForeignKey("customerID")]
        public customer customer { get; set; }

        
        [ForeignKey("catalogID")]
        public catalog? catalog { set; get; }

    }
}
