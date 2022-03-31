using System;
using System.Collections.Generic;
using System.Text;


namespace handmadebot
{
    public class TelegramClass
    {
        public class User
        {
            public int id { get; set; }
            public bool is_bot { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string username { get; set; }
            public string language_code { get; set; }
        }
        public class Chat
        {
            public long id { get; set; }
            public string first_name { get; set; }
        }
        public class MessageEntity
        {
            public string type { get; set; }
            public int offset { get; set; }
            public int length { get; set; }
            public string url { get; set; }
            public User user { get; set; }
        }
        public class PhotoSize
        {
            public string file_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int file_size { get; set; }
        }
        public class Audio
        {
            public string file_id { get; set; }
            public int duration { get; set; }
            public string performer { get; set; }
            public string title { get; set; }
            public string mime_type { get; set; }
            public int file_size { get; set; }
            public PhotoSize thumb { get; set; }
        }
        public class Document
        {
            public string file_id { get; set; }
            public PhotoSize thumb { get; set; }
            public string file_name { get; set; }
            public string mime_type { get; set; }
            public int file_size { get; set; }
        }
        public class Animation
        {
            public string file_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int duration { get; set; }
            public PhotoSize thumb { get; set; }
            public string file_name { get; set; }
            public string mime_type { get; set; }
            public int file_size { get; set; }
        }
        public class Game
        {
            public string title { get; set; }
            public string description { get; set; }
            public PhotoSize[] photo { get; set; }
            public string text { get; set; }
            public MessageEntity[] text_entities { get; set; }
            public Animation animation { get; set; }
        }
        public class MaskPosition
        {
            public string point { get; set; }
            public double x_shift { get; set; }
            public double y_shift { get; set; }
            public double scale { get; set; }
        }
        public class Sticker
        {
            public string file_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool is_animated { get; set; }
            public PhotoSize thumb { get; set; }
            public string emoji { get; set; }
            public string set_name { get; set; }
            public MaskPosition mask_position { get; set; }
            public int file_size { get; set; }
        }
        public class Video
        {
            public string file_id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int duration { get; set; }
            public PhotoSize thumb { get; set; }
            public string mime_type { get; set; }
            public int file_size { get; set; }
        }
        public class Voice
        {
            public string file_id { get; set; }
            public int duration { get; set; }
            public string mime_type { get; set; }
            public int file_size { get; set; }
        }
        public class VideoNote
        {
            public string file_id { get; set; }
            public int length { get; set; }
            public int duration { get; set; }
            public PhotoSize thumb { get; set; }
            public int file_size { get; set; }
        }
        public class Contact
        {
            public string phone_number { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int user_id { get; set; }
            public string vcard { get; set; }
        }
        public class Location
        {
            public double longitude { get; set; }
            public double latitude { get; set; }
        }
        public class Venue
        {
            public Location location { get; set; }
            public string title { get; set; }
            public string address { get; set; }
            public string foursquare_id { get; set; }
            public string foursquare_type { get; set; }
        }
        public class PollOption
        {
            public string text { get; set; }
            public int voter_count { get; set; }
        }
        public class Poll
        {
            public string id { get; set; }
            public string question { get; set; }
            public PollOption[] options { get; set; }
            public bool is_closed { get; set; }
        }
        public class Invoice
        {
            public string title { get; set; }
            public string description { get; set; }
            public string start_parameter { get; set; }
            public string currency { get; set; }
            public int total_amount { get; set; }

        }
        public class ShippingAddress
        {
            public string country_code { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string street_line1 { get; set; }
            public string street_line2 { get; set; }
            public string post_code { get; set; }
        }
        public class OrderInfo
        {
            public string name { get; set; }
            public string phone_number { get; set; }
            public string email { get; set; }
            public ShippingAddress shipping_address { get; set; }

        }
        public class SuccessfulPayment
        {
            public string currency { get; set; }
            public int total_amount { get; set; }
            public string invoice_payload { get; set; }
            public string shipping_option_id { get; set; }
            public OrderInfo order_info { get; set; }
            public string telegram_payment_charge_id { get; set; }
            public string provider_payment_charge_id { get; set; }

        }
        public class PassportFile
        {
            public string file_id { get; set; }
            public int file_size { get; set; }
            public int file_date { get; set; }
        }
        public class EncryptedPassportElement
        {
            public string type { get; set; }
            public string data { get; set; }
            public string phone_number { get; set; }
            public string email { get; set; }
            public PassportFile[] files { get; set; }
            public PassportFile front_side { get; set; }
            public PassportFile reverse_side { get; set; }
            public PassportFile selfie { get; set; }
            public PassportFile[] translation { get; set; }
            public string hash { get; set; }
        }
        public class EncryptedCredentials
        {
            public string data { get; set; }
            public string hash { get; set; }
            public string secret { get; set; }

        }
        public class PassportData
        {
            public EncryptedPassportElement[] data { get; set; }
            public EncryptedCredentials credentials { get; set; }
        }
        public class LoginUrl
        {
            public string url { get; set; }
            public string forward_text { get; set; }
            public string bot_username { get; set; }
            public bool request_write_access { get; set; }
        }
        public class InlineKeyboardButton
        {
            public string text { get; set; }
            public string url { get; set; }
            public LoginUrl login_url { get; set; }
            public string callback_data { get; set; }
            public string switch_inline_query { get; set; }
            public string switch_inline_query_current_chat { get; set; }
            public string callback_game { get; set; }
            public bool pay { get; set; }

        } 
        public class InlineKeyboardMarkup
        {
            public InlineKeyboardButton[][] inline_keyboard { get; set; }

            public InlineKeyboardMarkup(InlineKeyboardButton[][] keyboard) 
            {
                this.inline_keyboard = keyboard;
            }
        }
        public class Message
        {
            public int message_id { get; set; }
            public User from { get; set; }
            public int date { get; set; }
            public Chat chat { get; set; }
            public User forward_from { get; set; }
            public Chat forward_from_chat { get; set; }
            public int forward_from_message_id { get; set; }
            public string forward_signature { get; set; }
            public string forward_sender_name { get; set; }
            public int forward_date { get; set; }
            public Message reply_to_message { get; set; }
            public int edit_date { get; set; }
            public string media_group_id { get; set; }
            public string author_signature { get; set; }
            public string text { get; set; }
            public MessageEntity[] entities { get; set; }
            public MessageEntity[] caption_entities { get; set; }
            public Audio audio { get; set; }
            public Document document { get; set; }
            public Animation animation { get; set; }
            public Game game { get; set; }
            public PhotoSize[] photo { get; set; }
            public Sticker sticker { get; set; }
            public Video video { get; set; }
            public Voice voice { get; set; }
            public VideoNote video_note { get; set; }
            public string caption { get; set; }
            public Contact contact { get; set; }
            public Location location { get; set; }
            public Venue venue { get; set; }
            public Poll poll { get; set; }
            public User[] new_chat_members { get; set; }
            public User left_chat_member { get; set; }
            public string new_chat_title { get; set; }
            public PhotoSize[] new_chat_photo { get; set; }
            public bool delete_chat_photo { get; set; }
            public bool group_chat_created { get; set; }
            public bool supergroup_chat_created { get; set; }
            public bool channel_chat_created { get; set; }
            public long migrate_to_chat_id { get; set; }
            public long migrate_from_chat_id { get; set; }
            public Message pinned_message { get; set; }
            public Invoice invoice { get; set; }
            public SuccessfulPayment successful_payment { get; set; }
            public string connected_website { get; set; }
            public PassportData passport_data { get; set; }
            public InlineKeyboardMarkup reply_markup { get; set; }

        }
        public class InlineQuery
        {
            public string id { get; set; }
            public User from { get; set; }
            public Location location { get; set; }
            public string query { get; set; }
            public string offset { get; set; }

        }
        public class ChosenInlineResult
        {
            public string result_id { get; set; }
            public User from { get; set; }
            public Location location { get; set; }
            public string inline_message_id { get; set; }
            public string query { get; set; }
        }
        public class CallbackQuery
        {
            public string id { get; set; }
            public User from { get; set; }
            public Message message { get; set; }
            public string inline_message_id { get; set; }
            public string chat_instance { get; set; }
            public string data { get; set; }
            public string game_short_name { get; set; }
        }
        public class ShippingQuery
        {
            public string id { get; set; }
            public User from { get; set; }
            public string invoice_payload { get; set; }
            public ShippingAddress shipping_address { get; set; }

        }
        public class PreCheckoutQuery
        {
            public string id { get; set; }
            public User from { get; set; }
            public string currency { get; set; }
            public int total_amount { get; set; }
            public string invoice_payload { get; set; }
            public string shipping_option_id { get; set; }
            public OrderInfo order_info { get; set; }
        }
        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
            public Message edited_message { get; set; }
            public Message channel_post { get; set; }
            public Message edited_channel_post { get; set; }
            public InlineQuery inline_query { get; set; }
            public ChosenInlineResult chosen_inline_result { get; set; }
            public CallbackQuery callback_query { get; set; }
            public ShippingQuery shipping_query { get; set; }
            public PreCheckoutQuery pre_checkout_query { get; set; }
            public Poll poll { get; set; }
            public TelegramEnum.UpdateType Type
            {
                get
                {
                    if (message != null) return TelegramEnum.UpdateType.Message;
                    if (inline_query != null) return TelegramEnum.UpdateType.InlineQuery;
                    if (chosen_inline_result != null) return TelegramEnum.UpdateType.ChosenInlineResult;
                    if (callback_query != null) return TelegramEnum.UpdateType.CallbackQuery;
                    if (edited_message != null) return TelegramEnum.UpdateType.EditedMessage;
                    if (channel_post != null) return TelegramEnum.UpdateType.ChannelPost;
                    if (edited_channel_post != null) return TelegramEnum.UpdateType.EditedChannelPost;
                    if (shipping_query != null) return TelegramEnum.UpdateType.ShippingQuery;
                    if (pre_checkout_query != null) return TelegramEnum.UpdateType.PreCheckoutQuery;
                    if (poll != null) return TelegramEnum.UpdateType.Poll;
                    return TelegramEnum.UpdateType.Unknown; 
                }
            }

        }
        public class InputMediaPhoto 
        {
            public string type { get; set; }
            public string media { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
        }
        public class InputMediaVideo
        {
            public string type { get; set; }
            public string media { get; set; }
            public string thumb { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int duration { get; set; }
            public bool supports_streaming { get; set; }
        }
        public class InputMediaGroup
        {
            public string type { get; set; }
            public string media { get; set; }
            public string thumb { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int duration { get; set; }
            public bool supports_streaming { get; set; }
        }
        public class InputMediaAnimation
        {
            public string type { get; set; }
            public string media { get; set; }
            public string thumb { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int duration { get; set; }
        }
        public class InputMediaAudio
        {
            public string type { get; set; }
            public string media { get; set; }
            public string thumb { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }
            public int duration { get; set; }
            public string performer { get; set; }
            public string title { get; set; }

        }
        public class InputMediaDocument
        {
            public string type { get; set; }
            public string media { get; set; }
            public string thumb { get; set; }
            public string caption { get; set; }
            public string parse_mode { get; set; }          
        }

    }
}
