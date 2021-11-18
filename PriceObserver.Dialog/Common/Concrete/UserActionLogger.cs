using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Model.Converters.Extensions;
using PriceObserver.Model.Data;

namespace PriceObserver.Dialog.Common.Concrete
{
    public class UserActionLogger : IUserActionLogger
    {
        private readonly ILogger<UserActionLogger> _logger;

        public UserActionLogger(ILogger<UserActionLogger> logger)
        {
            _logger = logger;
        }

        public void LogAllItemsCalled(User user)
        {
            LogInformation(user, "ℹ Получил добавленные продукты");
        }

        public void LogShopsCalled(User user)
        {
            LogInformation(user, "ℹ Получил доступные магазины");
        }

        public void LogWebsiteCalled(User user)
        {
            LogInformation(user, "ℹ Получил ссылку на сайт");
        }

        public void LogUserRegistered(User user)
        {
            LogInformation(user, "🎉 Новый пользователь зарегистрирован");
        }

        public void LogWrongUrlPassed(User user, string messageText, string error)
        {
            var message = $@"❌ Не смогло достать ссылку из сообщения
Сообщение: {messageText}
Ошибка: {error}";
            
            LogError(user, message);
        }

        public void LogDuplicateItem(User user, Uri url)
        {
            var message = $@"❌ Попытался добавить дубликат
Ссылка: {url}";
            
            LogError(user, message);
        }

        public void LogParsingError(User user, Uri url, string parseResultError)
        {
            var message = $@"❌ Не смогло достать данные по ссылке 
Ссылка: {url}
Ошибка: {parseResultError}";
            
            LogError(user, message);
        }

        public void LogItemAdded(User user, Item item)
        {
            var message = $@"✅ Новый продукт добавлен в каталог 
Ссылка: {item.Url}
Заголовок: {item.Title}
Цена: {item.Price}";
            
            LogInformation(user, message);
        }

        public void LogWriteToSupport(User user, string messageText)
        {
            var message = $@"👨🏻‍ Написал в поддержку 
Сообщение: {messageText}";
            
            LogInformation(user, message);
        }

        public void LogWrongCommand(User user, string messageText)
        {
            var message = $@"❌ Ввёл неверную комманду 
Текст: {messageText}";
            
            LogInformation(user, message);
        }

        public void LogRedirectToMenu(User user, Menu menuToRedirect)
        {
            var message = $@"➡ Перешёл в другое меню 
Название: {menuToRedirect.Type.ToString()}";
                    
            LogInformation(user, message);
        }

        private void LogInformation(User user, string message)
        {
            Log(user, LogLevel.Information, message);
        }

        private void LogError(User user, string message)
        {
            Task.Run(() => Log(user, LogLevel.Error, message));
        }

        private void Log(User user, LogLevel logLevel, string message)
        {
            var userInfo = GetUserInfo(user);

            _logger.Log(logLevel, $"{message}{Environment.NewLine}{userInfo}");
        }
        
        private string GetUserInfo(User user)
        {
            var info = $@"Имя: {user.GetFullName()} (Id: {user.Id})";

            if (!string.IsNullOrEmpty(user.Username))
                info += $"{Environment.NewLine}Логин: @{user.Username}";

            return info;
        }
    }
}