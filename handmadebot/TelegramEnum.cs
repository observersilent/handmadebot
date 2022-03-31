using System;
using System.Collections.Generic;
using System.Text;

namespace handmadebot
{
    public class TelegramEnum
    {
        public enum UpdateType 
        {
            Message,
            InlineQuery,
            ChosenInlineResult,
            CallbackQuery,
            EditedMessage,
            ChannelPost,
            EditedChannelPost,
            ShippingQuery,
            PreCheckoutQuery,
            Poll,
            Unknown
        }
    }
}
