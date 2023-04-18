using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class RoleUpdateViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen rol adı giriniz.")]
        public string Name { get; set; }
    }
}
