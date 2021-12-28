using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class UserActionSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(
            ResourceKey.UserAction_GotAddedProducts,
            "ℹ Получил добавленные продукты"));

        resources.Add(new Resource(
            ResourceKey.UserAction_GotAvailableShops,
            "ℹ Получил доступные магазины"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_GotWebsiteLink,
            "ℹ Получил ссылку на сайт"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_UserRegistered,
            "🎉 Новый пользователь зарегистрирован"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_PassedWrongUrl,
            @"❌ Не смогло достать ссылку из сообщения
Сообщение: {0}
Ошибка: {1}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_TriedAddDuplicate,
            @"❌ Попытался добавить дубликат
Ссылка: {0}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_ParsingError,
            @"❌ Не смогло достать данные по ссылке 
Ссылка: {0}
Ошибка: {1}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_AddedItem,
            @"✅ Новый продукт добавлен в каталог 
Ссылка: {0}
Заголовок: {1}
Цена: {2}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_WroteToSupport,
            @"👨🏻‍ Написал в поддержку 
Сообщение: {0}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_WroteWrongCommand,
            @"❌ Ввёл неверную комманду 
Текст: {0}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_RedirectedToMenu,
            @"➡ Перешёл в другое меню 
Название: {0}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_UserInfo,
            "Имя: {0} (Id: {1})"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_UserLogin,
            "Логин: @{0}"));
            
        resources.Add(new Resource(
            ResourceKey.UserAction_CalledHelp,
            "🆘 Запросил помощь"));
    }
}