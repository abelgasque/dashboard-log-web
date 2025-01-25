using System;

namespace App.Entities
{
    public class UserWs
    {
        #region Constructor
        public UserWs() { }

        public UserWs(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        #endregion

        #region Atributtes
        public string UserName { get; set; }

        public string Password { get; set; }
        #endregion
    }

    public class UserWsAuthenticated
    {
        #region Constructor
        public UserWsAuthenticated() { }

        public UserWsAuthenticated(string token, DateTime dateExpiretedUtcNow)
        {
            Token = token;
            DateExpiretedUtcNow = dateExpiretedUtcNow;
        }
        #endregion

        #region Atributtes
        public string Token { get; set; }
        public DateTime DateExpiretedUtcNow { get; set; }
        #endregion
    }
}