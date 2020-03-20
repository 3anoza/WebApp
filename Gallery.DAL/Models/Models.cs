using System.ComponentModel.DataAnnotations;

namespace Gallery.DAL.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Empty Email. Please field this column.")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Empty Password. Please field this column")]
        [StringLength(30,MinimumLength = 6,ErrorMessage = "password must be between 6 and 12 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}