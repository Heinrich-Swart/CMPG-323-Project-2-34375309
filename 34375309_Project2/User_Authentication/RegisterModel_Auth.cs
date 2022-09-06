using System.ComponentModel.DataAnnotations;

namespace _34375309_Project2.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username
        {
            get;
            set;
        }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email")]
        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password
        {
            get;
            set;
        }
    }
}

