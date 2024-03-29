﻿using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class DialogSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(
            ResourceKey.Dialog_UserRegistered,
            @"Вітаю, {0}! 🎉

Тут Ви зможете додати посилання на бажані товари, за якими ви хотіли б стежити. Ми сповістимо вас, як тільки ціна знизиться 💰.

Натисніть <b>{1}</b> для отримання додаткової інформації.

{2}

{3}",
            @"Приветствую, {0}! 🎉

Здесь Вы сможете добавить ссылки на желаемые товары за которыми Вы хотели бы следить. Мы оповестим Вас как только цена снизится 💰.

Нажмите <b>{1}</b> для получения дополнительной информации.

{2}

{3}");

        resources.AddResource(
            ResourceKey.Dialog_MessageDoesNotContainLink,
            "У повідомленні немає посилання на товар ❌",
            "В сообщении нет ссылки на товар ❌");

        resources.AddResource(
            ResourceKey.Dialog_LinkInIncorrectFormat,
            "Посилання у неправильному форматі ❌",
            "Ссылка в неверном формате ❌");

        resources.AddResource(
            ResourceKey.Dialog_SupportReply,
            "Дякуємо за ваше повідомлення, ми з вами скоро зв'яжемося! 🏃",
            "Спасибо за Ваше сообщение, мы с Вами скоро свяжемся! 🏃");

        resources.AddResource(
            ResourceKey.Dialog_ItemAdded,
            "Успішно додано! ✅",
            "Успешно добавлено! ✅");

        resources.AddResource(
            ResourceKey.Dialog_DuplicateItem,
            "Такий товар вже є у вашому списку ☑",
            "Такой товар уже есть в Вашем списке ☑");

        resources.AddResource(
            ResourceKey.Dialog_IncorrectCommand,
            "Невірна команда ❌",
            "Неверная комманда ❌");

#if DEBUG
        resources.AddResource(
            ResourceKey.Dialog_Website,
            "Посилання на сайт - {0}",
            "Ссылка на сайт - {0}");
#else
        resources.AddResource(
            ResourceKey.Dialog_Website,
            "Натисніть щоб перейти на сайт ⬇",
            "Нажмите что-бы перейти на сайт ⬇");
#endif

        resources.AddResource(
            ResourceKey.Dialog_EmptyCart,
            "У вашому списку ще немає жодних товарів 🗑",
            "В вашем списке ещё нет никаких товаров 🗑");

        resources.AddResource(
            ResourceKey.Dialog_ItemInfo,
            @"{0}. {1}
 ├ Ціна <b>{2}</b>{3}
 └ <a href='{4}'>Посилання</a> на товар",
            @"{0}. {1}
 ├ Цена <b>{2}</b>{3}
 └ <a href='{4}'>Ссылка</a> на товар");

        resources.AddResource(
            ResourceKey.Dialog_AvailableShops,
            @"Доступні магазини 📋
{0}",
            @"Доступные магазины 📋
{0}");

        resources.AddResource(
            ResourceKey.Dialog_ErrorOccured,
            "Виникла помилка ❌",
            "Произошла ошибка ❌");

        resources.AddResource(
            ResourceKey.Dialog_Help,
            @"Інструкція користування ботом 🤖

<b>{0}</b> - інструкція з додавання нових товарів за ціною яких ви хотіли б стежити.

<b>{1}</b> - показує список доданих вами товарів.

<b>{2}</b> - список доступних магазинів з якими бот вміє працювати.

<b>{3}</b> - надає посилання на сайт, де доступне редагування вашого списку товарів у зручнішому форматі. 

<b>{4}</b> - якщо у вас є якесь питання, побажання чи обурення, звертайтеся на підтримку.",
            @"Инструкция пользования ботом 🤖

<b>{0}</b> - инструкция по добавлению новых товаров за ценой которых Вы хотели бы следить.

<b>{1}</b> - показывает список добавленных Вами товаров.

<b>{2}</b> - список доступных магазинов с которыми бот умеет работать.

<b>{3}</b> - предоставляет ссылку на сайт, где доступно редактирование Вашего списка товаров в более удобном формате. 

<b>{4}</b> - если у вас есть какой-то вопрос, пожелание или негодование, обращайтесь в поддержку.");

        resources.AddResource(
            ResourceKey.Dialog_ShopIsNotAvailable,
            "Магазин недоступний ❌",
            "Магазин недоступен ❌");

        resources.AddResource(
            ResourceKey.Dialog_AddItemInformation,
            @"💬 Клацніть у браузері або додатку магазину ""Поділитися"", виберіть Telegram і надішліть посилання на товар боту, за ціною якого Ви хотіли б стежити.

💻 Якщо ви користуєтеся ботом на комп'ютері, то скопіюйте посилання з пошукового поля в браузері і надішліть її в чат.

ℹ️ Щоразу натискати цю кнопку для додавання товару зовсім не обов'язково, просто надсилайте посилання або ділитесь посиланням з програми.",
            @"💬 Кликните в браузере или приложении магазина ""Поделиться"", выберите Telegram и отправьте ссылку на товар боту, за ценой которого Вы хотели бы следить.

💻 Если же вы пользуетесь ботом на компьютере, то скопируйте ссылку с поискового поля в браузере и пришлите её в чат.

ℹ️ Каждый раз нажимать данную кнопку для добавления товара совсем не обязательно, просто присылайте ссылку или делитесь ссылкой из приложения.");

        resources.AddResource(
            ResourceKey.Dialog_MaximumOfItemsExceeded,
            "Перейдіть на <a href='{0}'>сайт</a> щоб подивитись усі товари 🌍",
            "Перейдите на <a href='{0}'>сайт</a> что-бы просмотреть все товары 🌍");
        
        resources.AddResource(ResourceKey.Dialog_RestoreItem, "Повернути ➕", "Вернуть ➕");
        resources.AddResource(ResourceKey.Dialog_GoByItemUrl, "Перейти", "Перейти");
        
        resources.AddResource(ResourceKey.Dialog_ItemDeleted, "Товар видалено! 🗑", "Товар удален! 🗑");
        
        resources.AddResource(
            ResourceKey.Dialog_ChangeLanguageToRussian, 
            "Чи бажаєте ви змінити мову користування на російську?",
            "Хотите ли вы сменить язык пользования на росийский?");
        
        resources.AddResource(
            ResourceKey.Dialog_ChangeLanguageToUkrainian, 
            "Чи бажаєте ви змінити мову користування на українську?",
            "Хотите ли вы сменить язык пользования на украинский?");
        
        resources.AddResource(ResourceKey.Dialog_ChangeLanguage, "Змінити мову 🌐", "Сменить язык 🌐");
        resources.AddResource(ResourceKey.Dialog_LanguageChanged, "🌐 Мову змінено!", " 🌐 Язык изменен!");
        
        resources.AddResource(
            ResourceKey.Dialog_TogglePriceGrowingNotificationsToEnabled, 
            "Чи бажаєте ви отримувати повідомлення про зростання цін?",
            "Хотите ли вы получать уведомления о возрастании цены на товары?");
        
        resources.AddResource(
            ResourceKey.Dialog_TogglePriceGrowingNotificationsToDisabled, 
            "Чи бажаєте ви вимкнути отримання повідомлень про зростання цін?",
            "Хотите ли вы выключить получение уведомлений про возростание цен?");
        
        resources.AddResource(ResourceKey.Dialog_EnablePriceGrowingNotifications, "Увімкнути 🔔", "Включить 🔔");
        resources.AddResource(ResourceKey.Dialog_DisablePriceGrowingNotifications, "Вимкнути 🔕", "Выключить 🔕");
        
        resources.AddResource(
            ResourceKey.Dialog_PriceGrowingNotificationsEnabled, 
            "🔔 Повідомлення про зростання цін увімкнені!",
            "🔔 Уведомления про возрастание цен включены!");
        
        resources.AddResource(
            ResourceKey.Dialog_PriceGrowingNotificationsDisabled, 
            "🔕 Повідомлення про зростання цін вимкнені!",
            "🔕 Уведомления про возростание цен выключены!");
        
        resources.AddResource(
            ResourceKey.Dialog_ChangeMinimumDiscountThreshold,
            @"🏷 З якою мінімальною знижкою ви хочете отримувати повідомлення?
ℹ️ Наразі встановлено <b>{0}</b>%.",
            @"🏷 С какой минимальной скидкой вы хотите получать уведомления?
ℹ️ Сейчас установлено <b>{0}</b>%.");
        
        resources.AddResource(
            ResourceKey.Dialog_MinimumDiscountThresholdChanged,
            @"🏷 Мінімальну знижку встановлено на <b>{0}</b>%!",
            @"🏷 Минимальную скидку установлено на <b>{0}</b>%!");
        
#if !DEBUG
        resources.AddResource(ResourceKey.Dialog_GoToWebsite, @"Перейти", @"Перейти");
#endif
    }
}