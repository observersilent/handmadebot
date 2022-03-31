using System;
using System.Collections.Generic;
using System.Text;

using static handmadebot.TelegramClass;


namespace handmadebot
{
    public class TelegramRequestParameter
    {
        public class sendMessageParameter
        {
            public long chat_id { get; set; }
            public string text { get; set; }
            public string parse_mode { get; set; }
            public bool disable_web_page_preview { get; set; }
            public bool disable_notification { get; set; }
            public int reply_to_message_id { get; set; }
            public InlineKeyboardMarkup reply_markup { get; set; }
        }

        public class sendPhotoParameterChannel 
        {
            public long chat_id { get; set; }
            public string photo { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
            public bool disable_notification { get; set; }
            public int reply_to_message_id { get; set; }
            public InlineKeyboardMarkup reply_markup { get; set; }

        }

        public class sendMediaGroupParameter 
        {
            public int chat_id { get; set; }
            public InputMediaGroup[] media { get; set; }
            public bool disable_notification { get; set; }
            public int reply_to_message_id { get; set; }
        }

        public class deleteMessageParameter 
        {
            public long chat_id { get; set; }
            public long message_id { get; set; }
        }

        public class answerCallbackQueryParameter 
        {
            public string callback_query_id { get; set; }
            public string text { get; set; }
            public bool show_alert { get; set; }
            public string url { get; set; }
            public int cache_time { get; set; }

        }

        public class editMessageReplyMarkupParameter 
        {
            public long chat_id { get; set; }
            public long message_id { get; set; }
            public string inline_message_id { get; set; }
            public InlineKeyboardMarkup reply_markup { get; set; }
        }

        public class editMessageTextParameter 
        {
            public long chat_id { get; set; }
            public long message_id { get; set; }
            public string inline_message_id { get; set; }
            public string text { get; set; }
            public string parse_mode { get; set; }
            public bool disable_web_page_preview { get; set; }
            public InlineKeyboardMarkup reply_markup { get; set; }


        }

    }
}
