using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class UserActionSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_GotAddedProducts,
                "ℹ Получил добавленные продукты");

            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_GotAvailableShops,
                "ℹ Получил доступные магазины");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_GotWebsiteLink,
                "ℹ Получил ссылку на сайт");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_UserRegistered,
                "🎉 Новый пользователь зарегистрирован");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_PassedWrongUrl,
                @"❌ Не смогло достать ссылку из сообщения
Сообщение: {0}
Ошибка: {1}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_TriedAddDuplicate,
                @"❌ Попытался добавить дубликат
Ссылка: {0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_ParsingError,
                @"❌ Не смогло достать данные по ссылке 
Ссылка: {0}
Ошибка: {1}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_AddedItem,
                @"✅ Новый продукт добавлен в каталог 
Ссылка: {0}
Заголовок: {1}
Цена: {2}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_WroteToSupport,
                @"👨🏻‍ Написал в поддержку 
Сообщение: {0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_WroteWrongCommand,
                @"❌ Ввёл неверную комманду 
Текст: {0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_RedirectedToMenu,
                @"➡ Перешёл в другое меню 
Название: {0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_UserInfo,
                "Имя: {0} (Id: {1})");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_UserLogin,
                "Логин: @{0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.UserAction_CalledHelp,
                "🆘 Запросил помощь");
        }
    }
}