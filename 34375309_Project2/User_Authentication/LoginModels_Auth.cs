using System.ComponentModel.DataAnnotations;
namespace _34375309_Project2.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter your username")]
        public string Password
        {
            get;
            set;
        }
    }
}
