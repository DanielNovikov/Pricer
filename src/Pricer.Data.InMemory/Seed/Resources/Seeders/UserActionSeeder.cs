using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class UserActionSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(
            ResourceKey.UserAction_GotAddedProducts,
            "ℹ Отримав додані товари",
            "ℹ Получил добавленные товары");

        resources.AddResource(
            ResourceKey.UserAction_GotAvailableShops,
            "ℹ Отримав доступні магазини",
            "ℹ Получил доступные магазины");
            
        resources.AddResource(
            ResourceKey.UserAction_GotWebsiteLink,
            "ℹ Отримав посилання на сайт",
            "ℹ Получил ссылку на сайт");
            
        resources.AddResource(
            ResourceKey.UserAction_UserRegistered,
            "🎉 Новий користувач зареєстровано",
            "🎉 Новый пользователь зарегистрирован");

        resources.AddResource(
            ResourceKey.UserAction_TriedAddDuplicate,
            @"❌ Спробував додати дублікат
Посилання: {0}",
            @"❌ Попытался добавить дубликат
Ссылка: {0}");
            
        resources.AddResource(
            ResourceKey.UserAction_ParsingError,
            @"❌ Не змогло дістати дані за посиланням 
Посилання: {0}
Помилка: {1}",
            @"❌ Не смогло достать данные по ссылке 
Ссылка: {0}
Ошибка: {1}");
            
        resources.AddResource(
            ResourceKey.UserAction_AddedItem,
            @"✅ Новий продукт додано до каталогу 
Посилання: {0}
Заголовок: {1}
Ціна: {2}",
            @"✅ Новый продукт добавлен в каталог 
Ссылка: {0}
Заголовок: {1}
Цена: {2}");
            
        resources.AddResource(
            ResourceKey.UserAction_WroteToSupport,
            @"👨🏻‍ Написав на підтримку 
Повідомлення: {0}",
            @"👨🏻‍ Написал в поддержку 
Сообщение: {0}");
            
        resources.AddResource(
            ResourceKey.UserAction_WroteWrongCommand,
            @"❌ Ввів невірну команду 
Текст: {0}",
            @"❌ Ввёл неверную комманду 
Текст: {0}");
            
        resources.AddResource(
            ResourceKey.UserAction_RedirectedToMenu,
            @"➡ Перейшов до іншого меню 
Назва: {0}",
            @"➡ Перешёл в другое меню 
Название: {0}");
            
        resources.AddResource(
            ResourceKey.UserAction_UserInfo,
            "Ім'я: {0} (External Id: {1})\nБот: {2}",
            "Имя: {0} (External Id: {1})\nБот: {2}");
            
        resources.AddResource(
            ResourceKey.UserAction_UserLogin,
            "Логін: @{0}",
            "Логин: @{0}");
            
        resources.AddResource(
            ResourceKey.UserAction_CalledHelp,
            "🆘 Запитав допомогу",
            "🆘 Запросил помощь");
        
        resources.AddResource(
            ResourceKey.UserAction_TriedToAddUnsupportedShop,
            @"❌ Не відомий магазин продукту
Посилання: {0}",
            @"❌ Не известный магазин продукта
Ссылка: {0}");
        
        resources.AddResource(
            ResourceKey.UserAction_GotAddItemInstruction, 
            "ℹ Отримав інструкцію щодо додавання нового товару",
            "ℹ Получил инструкцию по добавлению нового товара");
        
        resources.AddResource(
	        ResourceKey.UserAction_SelectedLanguage, 
	        "🌐 Змінив мову на {0}",
	        "🌐 Изменил язык на {0}");
        
        resources.AddResource(
	        ResourceKey.UserAction_RedirectedBackToMenu,
	        @"➡ Перейшов назад до меню 
Назва: {0}",
	        @"➡ Перешёл назад в меню 
Название: {0}");
        
        resources.AddResource(
	        ResourceKey.UserAction_ToggledPriceGrowthNotifications,
	        @"🔔 Встановив слідкування за зростанням цін на - {0}",
	        @"🔔 Установил слежение за возрастанием цен на - {0}");
            
        resources.AddResource(
	        ResourceKey.UserAction_AddedNotAvailableItem,
	        @"✅ Додано продукт із слідкуванням на нявність
Посилання: {0}
Заголовок: {1}
Ціна: {2}",
	        @"✅ Добавлен продукт с отслеживанием наличия
Ссылка: {0}
Заголовок: {1}
Цена: {2}");
        
        resources.AddResource(
	        ResourceKey.UserAction_DeletedItem,
	        @"🗑 Видалено товар
Посилання: {0}
Заголовок: {1}
Ідентифікатор: {2}",
	        @"🗑 Товар удален
Ссылка: {0}
Заголовок: {1}
Ідентифікатор: {2}");
        
        resources.AddResource(
	        ResourceKey.UserAction_RestoredItem,
	        @"✅ Товар відновлено
Посилання: {0}
Заголовок: {1}
Ціна: {2}",
	        @"✅ Товар восстановлен
Ссылка: {0}
Заголовок: {1}
Цена: {2}");

        resources.AddResource(
	        ResourceKey.UserAction_CalledTogglingPriceGrowingMenu,
	        "🔔 Запросив меню для зміни слідкування за зростанням",
	        "🔔 Запросил меню для смены слежения за возростанием");
        
        resources.AddResource(
	        ResourceKey.UserAction_CalledChangingLanguageMenu,
	        "🌐 Запросив меню для зміни мови",
	        "🌐 Запросил меню для изменения языка");
        
        resources.AddResource(
            ResourceKey.UserAction_CalledChangingMinimumDiscountThreshold,
            "💸 Запросив меню для зміни мінімальної знижки",
            "💸 Запросил меню для смени минимальной скидки");
        
        resources.AddResource(
            ResourceKey.UserAction_ChangedMinimumDiscountThreshold,
            "🏷 Змінив мінімальну знижку на {0}%",
            "🏷 Изменил минимальную скидку на {0}%");
    }
}