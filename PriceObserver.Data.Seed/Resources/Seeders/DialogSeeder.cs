using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class DialogSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_UserRegistered,
                @"Приветствую, {0}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить. Мы оповестим Вас как только цена снизится. ({1})

Так же можно перейти на сайт, где доступно больше возможностей для редактирования Вашего списка товаров в более удобном формате. ({2})

Если у вас есть какое-то пожелание или негодование, можете обратиться в поддержку. ({3})

{4}

{5}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_MessageDoesNotContainLink,
                "В сообщении нет ссылки на товар ❌");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_LinkInIncorrectFormat,
                "Ссылка в неверном формате ❌");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_SupportReply,
                "Спасибо за Ваше сообщение, мы с Вами скоро свяжемся! 🏃");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_ItemAdded,
                "Успешно добавлено! ✅");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_DuplicateItem,
                "Такой товар уже есть в Вашем списке ☑");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_IncorrectCommand,
                "Неверная комманда ❌");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_Website,
                "Нажмите <a href='{0}'>здесь</a> для перехода на сайт");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_EmptyCart,
                "В Вашем списке ещё нет никаких товаров 🗑");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_ItemInfo,
                "Цена на <a href='{0}'>товар</a> <b>{1}</b>");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_AvailableShops,
                @"Доступные магазины 📋
{0}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Dialog_ErrorOccured,
                @"Произошла ошибка ❌");
        }
    }
}