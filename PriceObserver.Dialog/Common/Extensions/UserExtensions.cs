using PriceObserver.Model.Data;

namespace PriceObserver.Dialog.Common.Extensions
{
    public static class UserExtensions
    {
        public static string GetFullName(this User user)
        {
            return string.IsNullOrEmpty(user.LastName)
                ? user.FirstName
                : $"{user.FirstName} {user.LastName}";
        }
    }
}