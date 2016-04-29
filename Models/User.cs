using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace brewstrWebApp.Models
{
    public class User
    {
        private int _id;
        private string _name;
        private string _username;
        private string _password;
        private string _phone_number;
        private string _email_address;
        public bool _valid = false;
            
        #region c'tor
        public User( int id, string username, string phoneNumber, string emailAddress,
            string password)
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
        #endregion

        #region Class Methods
        private Boolean ValidateLogin(User_Login lg)
        {
            return lg.isValid();
        }
        #endregion

    }   
}