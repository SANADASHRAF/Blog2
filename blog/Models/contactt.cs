using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models
{
    public class contactt
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Not Vaild Number")]
        public string phone { get; set; }
        public string message { get; set; }
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Not Vaild Email")]
        public string email { get; set; }

        public int customerId { get; set; }

        [ForeignKey("customerId")]
        public customer customer { get; set; }
    }
}
