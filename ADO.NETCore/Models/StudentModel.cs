using System.ComponentModel.DataAnnotations;

namespace ADO.NETCore.Models
{
    public class StudentModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Kindly enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Kindly enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kindly enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kindly enter mobile")]
        [RegularExpression("[789]\\d{9}$", ErrorMessage = "Please enter a valid mobile number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Kindly enter address")]
        public string Address { get; set; }

    }
}
