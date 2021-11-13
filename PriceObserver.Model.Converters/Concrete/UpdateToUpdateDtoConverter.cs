using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Telegram.Input;
using Telegram.Bot.Types;

namespace PriceObserver.Model.Converters.Concrete
{
    public class UpdateToUpdateDtoConverter : IUpdateToUpdateDtoConverter
    {
        public UpdateDto Convert(Update source)
        {
            var message = source.Message;
            var chat = message.Chat;
            
            return new UpdateDto
            {
                Text = message.Text,
                UserId = chat.Id,
                Username = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };
        }
    }
}