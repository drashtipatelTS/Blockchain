using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestMVC.Models
{
    public class StudentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Student Name")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Standard { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
