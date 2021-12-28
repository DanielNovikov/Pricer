using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class DialogSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(
            ResourceKey.Dialog_UserRegistered,
            @"Приветствую, {0}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить. Мы оповестим Вас как только цена снизится 💰.

Нажмите <b>{1}</b> для получения дополнительной информации.

{2}

{3}"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_MessageDoesNotContainLink,
            "В сообщении нет ссылки на товар ❌"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_LinkInIncorrectFormat,
            "Ссылка в неверном формате ❌"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_SupportReply,
            "Спасибо за Ваше сообщение, мы с Вами скоро свяжемся! 🏃"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_ItemAdded,
            "Успешно добавлено! ✅"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_DuplicateItem,
            "Такой товар уже есть в Вашем списке ☑"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_IncorrectCommand,
            "Неверная комманда ❌"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_Website,
            "Нажмите <a href='{0}'>здесь</a> для перехода на сайт"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_EmptyCart,
            "В вашем списке ещё нет никаких товаров 🗑"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_ItemInfo,
            "Цена на <a href='{0}'>товар</a> <b>{1}</b>"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_AvailableShops,
            @"Доступные магазины 📋
{0}"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_ErrorOccured,
            @"Произошла ошибка ❌"));
            
        resources.Add(new Resource(
            ResourceKey.Dialog_Help,
            @"Инструкция о том как пользоваться ботом 🤖

<b>{0}</b> - переход в меню для добавления новых товаров за ценой которых бот будет следить.

<b>{1}</b> - даёт возможность увидеть список добавленных Вами товаров.

<b>{2}</b> - список доступных магазинов с которыми бот умеет работать.

<b>{3}</b> - предоставляет ссылку на сайт, где доступно редактирование Вашего списка товаров в более удобном формате. 

<b>{4}</b> - если у вас есть какой-то вопрос, пожелание или негодование, обращайтесь в поддержку."));
    }
}