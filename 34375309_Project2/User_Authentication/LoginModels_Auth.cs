using System.ComponentModel.DataAnnotations;
namespace _34375309_Project2.Authentication
{
    public class LoginModel
    {
        //If the user entered the wrong username
        [Required(ErrorMessage = "Please enter your username")]
        public string Username
        {
            get;
            set;
        }

        //If the user entered the wrong password
        [Required(ErrorMessage = "Please enter your password")]
        public string Password
        {
            get;
            set;
        }
    }
}
