using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace brewstrWebApp.Models
{
    public class User
    {
        public int _id {get;set;}
        [Display(Name="Name")]
        [Required(ErrorMessage = "Please Enter your Name")]
        public string _name { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please Enter a Username")]
        [StringLength(100)]
        public string _username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter a Password")]
        [StringLength(15)]
        public string _password { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Enter a phone number")]
        [StringLength(30)]
        public string _phone_number { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please Enter an email address")]
        [StringLength(30)]
        public string _email_address { get; set; }
        public bool _valid = false;
        private bool _isAdmin;
            
        #region c'tor
        public User()
        {
            _id = 0;
            _username = null;
            _phone_number = null;
            _email_address = null;
            _password = null;
            _isAdmin = false;
        }

        public User( int id, string username, string phoneNumber, string emailAddress,
            string password, bool isAdmin)
        {
            User_Login lg = new User_Login(emailAddress,password);
            if(ValidateLogin(lg))
            {
                _valid = true;
            }
            _id = id;
            _username = username;
            _phone_number = phoneNumber;
            _email_address = emailAddress;
            _password = password;
            _isAdmin = isAdmin;
        }
        #endregion

        #region Get/Set
        public int getId()
        {
            return _id;
        }
        public string getName()
        {
            return _name ;
        }
        public string getUsername()
        {
            return _username;
        }
        public string getPhoneNumber()
        {
            return _phone_number;
        }
        public string getEmailAddress()
        {
            return _email_address;
        }
        public string getPassword()
        {
            return _password;
        }
        public bool getIsAdmin()
        {
            return _isAdmin;
        }
        #endregion

        #region Class Methods
        private Boolean ValidateLogin(User_Login lg)
        {
            return lg.isValid();
        }
        public Boolean startsWithJ()
        {
            return _name.Substring(0, 1).Equals("J");
        }
        #endregion

    }   
}