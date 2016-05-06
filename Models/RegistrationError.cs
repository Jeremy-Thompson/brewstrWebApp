using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace brewstrWebApp.Models
{
    public class RegistrationError
    {
        private bool usernameTooLong;
        private bool passwordTooLong;
        private bool passwordTooShort;
        private bool emailTooLong;
        private bool phoneNumberTooLong;
        private bool emailInvalid;
        private bool usernameExists;

        #region constructor
        public RegistrationError(int msgFlg, bool userExists)
        { 
            if((msgFlg & 0x01) > 0)
            {
                usernameTooLong = true;
            }
            if ((msgFlg & 0x02) > 0)
            {
                passwordTooLong = true;
            }
            if ((msgFlg & 0x04) > 0)
            {
                passwordTooShort = true;
            }
            if ((msgFlg & 0x10) > 0)
            {
                emailTooLong = true;
            }
            if ((msgFlg & 0x20) > 0)
            {
                phoneNumberTooLong = true;
            }
            if ((msgFlg & 0x40) > 0)
            {
                emailInvalid = true;
            }

            if (userExists)
            {
                usernameExists = true;
            }
        }
        #endregion

        #region Get/Set
        public bool getUsernameTooLong()
        {
            return usernameTooLong;
        }
        public bool getPasswordTooLong()
        {
            return passwordTooLong;
        }
        public bool getPasswordTooShort()
        {
            return passwordTooShort;
        }
        public bool getEmailTooLong()
        {
            return emailTooLong;
        }
        public bool getPhoneTooLong()
        {
            return phoneNumberTooLong;
        }
        public bool getEmailInvalid()
        {
            return emailInvalid;
        }
        public bool getUsernameExists()
        {
            return usernameExists;
        }
        #endregion
    }
}