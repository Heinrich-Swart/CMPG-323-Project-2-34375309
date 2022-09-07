using System.ComponentModel.DataAnnotations;

namespace _34375309_Project2.Authentication
{
    public class RegisterModel

    {
        //If the user entered the wrong username
        [Required(ErrorMessage = "Please enter your username")]

        //Retrieving username
        public string Username
        {
            get;
            set;
        }

        [EmailAddress]
        //If the user entered the wrong email
        [Required(ErrorMessage = "Please enter your email")]

        //Retrieving email
        public string Email
        {
            get;
            set;
        }

        //If the user entered the wrong password
        [Required(ErrorMessage = "Please enter your password")]

        //Retrieving password
        public string Password
        {
            get;
            set;
        }
    }
}

