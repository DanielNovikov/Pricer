using PriceObserver.Model.Data;

namespace PriceObserver.Model.Dialog.Common
{
    public class AuthorizationResult
    {
        public User User { get; private set; }
        
        public bool IsNew { get; private set; }

        private AuthorizationResult()
        {
        }

        public static AuthorizationResult LoggedIn(User user)
        {
            return new AuthorizationResult
            {
                User = user,
                IsNew = false
            };
        }
        
        public static AuthorizationResult Registered(User user)
        {
            return new AuthorizationResult
            {
                User = user,
                IsNew = true
            };
        }
    }
}