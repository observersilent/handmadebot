using System;
using System.Threading;
using System.Data.SQLite;
using static handmadebot.TelegramClass;
using static handmadebot.TelegramRequestParameter;
using static handmadebot.TelegramAPI;

namespace handmadebot
{
    class Program
    {
        private static TelegramAPI API;
        
        static void Main(string[] args)
        {
            Create_Database.Create();
            API = new TelegramAPI();
            sendMessage(896172479, "Хуй");

            //InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
            //mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍", callback_data = "like_message_bot" },
            //                                           new InlineKeyboardButton { text = "👎", callback_data = "dislike_message_bot" } };
            //var keyboard = new InlineKeyboardMarkup(mas);
            //var payload = new sendMessageParameter { chat_id = -1001466398761, text = "Тест", reply_markup = keyboard };
            //sendMessage(payload);

            //API.sendMessage(-1001466398761, "ТЕСТ");
            API.onMessage += Display;
            API.onCallbackQuery += CallbackQuery;
            //API.onEditedChannelPost += EditedChannelPost;
            //API.onEditedMessage += DisplayEdit;
            API.onChannelPost += ChannelPost;

            Thread thread = new Thread(x => API.StartReceiving(2000));
            thread.Start();

            Thread thread1 = new Thread(x => populat_database.populate());
            thread1.Start();
        }

        public static async void Display(object sender, Update e) 
        {      
            if(e?.message?.reply_to_message?.text != null && (e?.message?.from?.id == 896172479 || e?.message?.from?.id == 114642706) &&(e?.message?.chat?.id == 896172479 || e?.message?.chat?.id == 114642706)) 
            {
                string text = e.message.reply_to_message.text;
                string comment = e?.message.text;
                string[] words = text.Split(new char[] { '\n' });

                if (words.Length == 6) 
                {
                    string type = words[0];
                    string title = words[1];
                    string short_text = words[2];
                    string picture = words[3];
                    string agency = words[4];
                    string href = words[5];

                    InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                    mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍", callback_data = "like_message_bot" },
                                 new InlineKeyboardButton { text = "👎", callback_data = "dislike_message_bot" } };
                    var keyboard = new InlineKeyboardMarkup(mas);

                    if (comment == "Нет") 
                    {
                        string format_text = "#" + type + "\n" + "<b>" + title + "</b>" + "\n" + "\n" + short_text + "\n" + $"<a href=\"{picture}\">⁠⁠⁠⁠⁠⁠⁠</a>" + "\n" + $"{agency}: <a href=\"{href}\">Открыть новость</a>";

                        var payloadchannel = new sendMessageParameter
                        {
                            text = format_text,
                            chat_id = -1001466398761,
                            parse_mode = "HTML",
                            disable_notification = true,
                            disable_web_page_preview = false,
                            reply_markup = keyboard
                        };

                        await TelegramAPI.sendMessage(payloadchannel);

                    }
                    if(comment != "Нет") 
                    {
                        string format_text = "#" + type + "\n" + "<b>" + title + "</b>" + "\n" + "\n" + short_text + "\n" + "\n" + "<b>" + "<i>" + comment + "</i>" + "</b>" + "\n" + $"<a href=\"{picture}\">⁠⁠⁠⁠⁠⁠⁠</a>" + "\n" + $"{agency}: <a href=\"{href}\">Открыть новость</a>";

                        var payloadchannel = new sendMessageParameter
                        {
                            text = format_text,
                            chat_id = -1001466398761,
                            parse_mode = "HTML",
                            disable_notification = true,
                            disable_web_page_preview = false,
                            reply_markup = keyboard
                        };
                        await TelegramAPI.sendMessage(payloadchannel);
                    }
                }
            }
            if(e.message.text == "Тест") 
            {
               await TelegramAPI.sendMessage(896172479, "Бот работает в штатном режиме...");
            }
        }

        public static void DisplayEdit(object sender, Update e)
        {
            Console.WriteLine($"Изменилось сообщение от {sender.GetType()} и его текст теперь {e.edited_message.text}");
        }

        public static async void ChannelPost(object sender, Update e)
        {
            long chat_id = e.channel_post.chat.id;
            long message_id = e.channel_post.message_id;
            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
            mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍", callback_data = "like_message_bot" },
                                                       new InlineKeyboardButton { text = "👎", callback_data = "dislike_message_bot" } };
            var keyboard = new InlineKeyboardMarkup(mas);
            var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
            await editMessageReplyMarkup(payload);

        }

        public static async void EditedChannelPost(object sender, Update e) 
        {
            if (e?.edited_channel_post?.reply_markup?.inline_keyboard[0][0]?.text == "Add a comment") 
            {
                string replay_markup_url = e?.edited_channel_post?.reply_markup?.inline_keyboard[0][0]?.url;
                string replay_markup_text = e?.edited_channel_post?.reply_markup?.inline_keyboard[0][0]?.text;
                long chat_id = e.edited_channel_post.chat.id;
                long message_id = e.edited_channel_post.message_id;

                Console.WriteLine(chat_id);
                Console.WriteLine(message_id);

                Thread.Sleep(10000);
                InlineKeyboardButton[][] mas = new InlineKeyboardButton[2][];
                mas[0] = new InlineKeyboardButton[1] { new InlineKeyboardButton { text = replay_markup_text, url = replay_markup_url } };
                mas[1] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍", callback_data = "like_message_bot" },
                                                       new InlineKeyboardButton { text = "👎", callback_data = "dislike_message_bot" } };
                var keyboard = new InlineKeyboardMarkup(mas);
                var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                await TelegramAPI.editMessageReplyMarkup(payload);

            }
        }
        public static async void CallbackQuery(object sender, Update e)
        {
            if(e.callback_query.data == "delete_message_bot") 
            {
                long chat_id = e.callback_query.message.chat.id;
                long message_id = e.callback_query.message.message_id;
                var payload = new deleteMessageParameter { chat_id = chat_id, message_id = message_id };
                await TelegramAPI.deleteMessage(payload);
                var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Удаляю", show_alert = false };
                await TelegramAPI.answerCallbackQuery(payload_answer);
            }

            using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
            {
                connection.Open();

                if (e.callback_query.data == "like_message_bot")
                {
                    long chat_id;
                    long message_id;
                    long from_id;
                    int count_like;
                    int count_dislike;

                    if (e.callback_query.message.chat.id == -1001466398761)
                    {
                        chat_id = -1001466398761;
                        message_id = e.callback_query.message.message_id;
                        from_id = e.callback_query.from.id;
                    }
                    else
                    {
                        chat_id = -1001466398761;
                        message_id = e.callback_query.message.forward_from_message_id;
                        from_id = e.callback_query.from.id;
                    }

                    string check_count_like = string.Format("SELECT Count_like, Count_dislike " +
                                                            "FROM Data_count_like " +
                                                            "WHERE Chat_id = @Chat_id " +
                                                            "AND Message_id = @Message_id");
                    SQLiteCommand check_count_command = new SQLiteCommand(check_count_like, connection);
                    check_count_command.Parameters.AddWithValue("Chat_id", chat_id);
                    check_count_command.Parameters.AddWithValue("Message_id", message_id);
                    bool HasRows;
                    string text_like;
                    string text_dislike;
                    using (var reader = check_count_command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            HasRows = true;
                            text_like = reader[0].ToString();
                            text_dislike = reader[1].ToString();
                        }
                        else
                        {
                            HasRows = false;
                            text_like = "0";
                            text_dislike = "0";
                        }
                        reader.Close();

                    }

                    if (!HasRows)
                    {
                        count_like = 0;
                        count_dislike = 0;
                        InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                        mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍 1", callback_data = "like_message_bot" },
                                               new InlineKeyboardButton { text = "👎", callback_data = "dislike_message_bot" } };
                        var keyboard = new InlineKeyboardMarkup(mas);

                        var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                        await TelegramAPI.editMessageReplyMarkup(payload);
                        var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                        await TelegramAPI.answerCallbackQuery(payload_answer);
                        Cleare_Like(chat_id, message_id, from_id);
                        Insert_Like(chat_id, message_id, from_id, "L");
                        Insert_Count(chat_id, message_id, 1, 0);
                    }
                    else
                    {
                        if (!int.TryParse(text_dislike, out count_dislike)) count_dislike = 0;
                        if (!int.TryParse(text_like, out count_like)) count_like = 0;
                        if (Check_Like(chat_id, message_id, from_id, "L") == "N" && Check_Like(chat_id, message_id, from_id, "D") == "N")
                        {
                            count_like += 1;
                            text_like = count_like == 0 ? ("👍") : ("👍 " + count_like.ToString());
                            text_dislike = count_dislike == 0 ? ("👎") : ("👎 " + count_dislike.ToString());
                            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                            mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = text_like, callback_data = "like_message_bot" },
                                                   new InlineKeyboardButton { text = text_dislike, callback_data = "dislike_message_bot" } };
                            var keyboard = new InlineKeyboardMarkup(mas);
                            var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                            await TelegramAPI.editMessageReplyMarkup(payload);
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);

                            Cleare_Like(chat_id, message_id, from_id);
                            Insert_Like(chat_id, message_id, from_id, "L");
                            Insert_Count(chat_id, message_id, count_like, count_dislike);
                        }
                        if (Check_Like(chat_id, message_id, from_id, "L") == "Y" && Check_Like(chat_id, message_id, from_id, "D") == "N")
                        {
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Вы уже проголосовали", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);
                        }
                        if (Check_Like(chat_id, message_id, from_id, "L") == "N" && Check_Like(chat_id, message_id, from_id, "D") == "Y")
                        {
                            count_like += 1;
                            text_like = count_like == 0 ? ("👍") : ("👍 " + count_like.ToString());
                            count_dislike -= 1;
                            text_dislike = count_dislike == 0 ? ("👎") : ("👎 " + count_dislike.ToString());

                            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                            mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = text_like, callback_data = "like_message_bot" },
                                                   new InlineKeyboardButton { text = text_dislike, callback_data = "dislike_message_bot" } };
                            var keyboard = new InlineKeyboardMarkup(mas);
                            var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                            await TelegramAPI.editMessageReplyMarkup(payload);
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);

                            Cleare_Like(chat_id, message_id, from_id);
                            Insert_Like(chat_id, message_id, from_id, "L");
                            Insert_Count(chat_id, message_id, count_like, count_dislike);

                        }
                    }
                    connection.Close();
                }
                if (e.callback_query.data == "dislike_message_bot")
                {
                    long chat_id;
                    long message_id;
                    long from_id;
                    int count_like;
                    int count_dislike;

                    if (e.callback_query.message.chat.id == -1001466398761)
                    {
                        chat_id = -1001466398761;
                        message_id = e.callback_query.message.message_id;
                        from_id = e.callback_query.from.id;
                    }
                    else
                    {
                        chat_id = -1001466398761;
                        message_id = e.callback_query.message.forward_from_message_id;
                        from_id = e.callback_query.from.id;
                    }

                    string check_count_like = string.Format("SELECT Count_like, Count_dislike " +
                                                            "FROM Data_count_like " +
                                                            "WHERE Chat_id = @Chat_id " +
                                                            "AND Message_id = @Message_id");
                    SQLiteCommand check_count_command = new SQLiteCommand(check_count_like, connection);
                    check_count_command.Parameters.AddWithValue("Chat_id", chat_id);
                    check_count_command.Parameters.AddWithValue("Message_id", message_id);
                    bool HasRows;
                    string text_like;
                    string text_dislike;
                    using (var reader = check_count_command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            HasRows = true;
                            text_like = reader[0].ToString();
                            text_dislike = reader[1].ToString();
                        }
                        else
                        {
                            HasRows = false;
                            text_like = "0";
                            text_dislike = "0";
                        }
                        reader.Close();
                    }
                    if (!HasRows)
                    {
                        count_like = 0;
                        count_dislike = 0;
                        InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                        mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = "👍", callback_data = "like_message_bot" },
                                               new InlineKeyboardButton { text = "👎 1", callback_data = "dislike_message_bot" } };
                        var keyboard = new InlineKeyboardMarkup(mas);

                        var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                        await TelegramAPI.editMessageReplyMarkup(payload);
                        var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                        await TelegramAPI.answerCallbackQuery(payload_answer);
                        Cleare_Like(chat_id, message_id, from_id);
                        Insert_Like(chat_id, message_id, from_id, "D");
                        Insert_Count(chat_id, message_id, 0, 1);
                    }
                    else
                    {
                        if (!int.TryParse(text_dislike, out count_dislike)) count_dislike = 0;
                        if (!int.TryParse(text_like, out count_like)) count_like = 0;
                        if (Check_Like(chat_id, message_id, from_id, "L") == "N" && Check_Like(chat_id, message_id, from_id, "D") == "N")
                        {
                            count_dislike += 1;
                            text_like = count_like == 0 ? ("👍") : ("👍 " + count_like.ToString());
                            text_dislike = count_dislike == 0 ? ("👎") : ("👎 " + count_dislike.ToString());
                            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                            mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = text_like, callback_data = "like_message_bot" },
                                               new InlineKeyboardButton { text = text_dislike, callback_data = "dislike_message_bot" } };
                            var keyboard = new InlineKeyboardMarkup(mas);
                            var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                            await TelegramAPI.editMessageReplyMarkup(payload);
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);

                            Cleare_Like(chat_id, message_id, from_id);
                            Insert_Like(chat_id, message_id, from_id, "D");
                            Insert_Count(chat_id, message_id, count_like, count_dislike);
                        }
                        if (Check_Like(chat_id, message_id, from_id, "L") == "N" && Check_Like(chat_id, message_id, from_id, "D") == "Y")
                        {
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Вы уже проголосовали", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);
                        }
                        if (Check_Like(chat_id, message_id, from_id, "L") == "Y" && Check_Like(chat_id, message_id, from_id, "D") == "N")
                        {
                            count_like -= 1;
                            text_like = count_like == 0 ? ("👍") : ("👍 " + count_like.ToString());
                            count_dislike += 1;
                            text_dislike = count_dislike == 0 ? ("👎") : ("👎 " + count_dislike.ToString());

                            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                            mas[0] = new InlineKeyboardButton[2] { new InlineKeyboardButton { text = text_like, callback_data = "like_message_bot" },
                                               new InlineKeyboardButton { text = text_dislike, callback_data = "dislike_message_bot" } };
                            var keyboard = new InlineKeyboardMarkup(mas);
                            var payload = new editMessageReplyMarkupParameter { chat_id = chat_id, message_id = message_id, reply_markup = keyboard };
                            await TelegramAPI.editMessageReplyMarkup(payload);
                            var payload_answer = new answerCallbackQueryParameter { callback_query_id = e.callback_query.id, text = "Ваш голос учтен", show_alert = false };
                            await TelegramAPI.answerCallbackQuery(payload_answer);

                            Cleare_Like(chat_id, message_id, from_id);
                            Insert_Like(chat_id, message_id, from_id, "D");
                            Insert_Count(chat_id, message_id, count_like, count_dislike);
                        }
                    }
                    connection.Close();
                }
            }
        }

        public static string Check_Like(long chat_id, long message_id, long from_id, string type)
        {
            using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
            {
                connection.Open();
                string check = string.Format("SELECT Type " +
                                               "FROM Data_like " +
                                              "WHERE Chat_id = @chat_id " +
                                              "AND Message_id = @message_id " +
                                              "AND From_id = @from_id " +
                                              "AND Type = @type");

                SQLiteCommand check_command = new SQLiteCommand(check, connection);
                check_command.Parameters.AddWithValue("chat_id", chat_id);
                check_command.Parameters.AddWithValue("message_id", message_id);
                check_command.Parameters.AddWithValue("from_id", from_id);
                check_command.Parameters.AddWithValue("type", type);
                using (var reader = check_command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        connection.Close();
                        return "N";

                    }
                    else
                    {
                        reader.Close();
                        connection.Close();
                        return "Y";
                    }                
                }               
            }
        }

        public static void Insert_Like(long chat_id, long message_id, long from_id, string type)
        {
            using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
            {
                connection.Open();
                string query = string.Format("INSERT INTO Data_like" + "(Chat_id, " +
                                                                                          "Message_id, " +
                                                                                          "From_id, " +
                                                                                          "Type, " +
                                                                                          "Date_create) " +

                                                                 "VALUES(@Chat_id, " +
                                                                        "@Message_id, " +
                                                                        "@From_id, " +
                                                                        "@Type, " +
                                                                        "datetime());");

                SQLiteCommand comand = new SQLiteCommand(query, connection);
                comand.Parameters.AddWithValue("Chat_id", chat_id);
                comand.Parameters.AddWithValue("Message_id", message_id);
                comand.Parameters.AddWithValue("From_id", from_id);
                comand.Parameters.AddWithValue("Type", type);
                comand.ExecuteNonQuery();
                connection.Close();

            }
        }

        public static void Cleare_Like(long chat_id, long message_id, long from_id)
        {
            using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
            {
                connection.Open();

                try 
                {
                    string query = string.Format("DELETE FROM Data_like " + "WHERE Chat_id = @Chat_id " +
                                                                   "AND Message_id = @Message_id " +
                                                                   "AND From_id = @From_id;");

                    using (var comand = new SQLiteCommand(query, connection))
                    {
                        comand.Parameters.AddWithValue("Chat_id", chat_id);
                        comand.Parameters.AddWithValue("Message_id", message_id);
                        comand.Parameters.AddWithValue("From_id", from_id);
                        comand.ExecuteNonQuery();
                        connection.Close();
                    }

                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Чет пошло не так в Cleare_like");
                    Console.WriteLine(ex.Message);
                    connection.Close();
                }
                                                            
            }
        }

        public static void Insert_Count(long chat_id, long message_id, int like, int dislake)
        {
            using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
            {
                connection.Open();
                string check = string.Format("SELECT id " +
                                                    "FROM Data_count_like " +
                                                    "WHERE Chat_id = @Chat_id " +
                                                    "AND Message_id = @Message_id");

                SQLiteCommand check_command = new SQLiteCommand(check, connection);
                check_command.Parameters.AddWithValue("Chat_id", chat_id);
                check_command.Parameters.AddWithValue("Message_id", message_id);
                using (var reader = check_command.ExecuteReader()) 
                {
                    reader.Read();
                    if (!reader.HasRows) 
                    {
                        string insert_query = string.Format("INSERT INTO Data_count_like(Chat_id, " +
                                                                                        "Message_id, " +
                                                                                        "Count_like, " +
                                                                                        "Count_dislike) " +
                                                            "VALUES(@Chat_id, " +
                                                                   "@Message_id, " +
                                                                   "@Count_like, " +
                                                                   "@Count_dislike)");
                        SQLiteCommand insert_command = new SQLiteCommand(insert_query, connection);
                        insert_command.Parameters.AddWithValue("Chat_id", chat_id);
                        insert_command.Parameters.AddWithValue("Message_id", message_id);
                        insert_command.Parameters.AddWithValue("Count_like", like);
                        insert_command.Parameters.AddWithValue("Count_dislike", dislake);
                        insert_command.ExecuteNonQuery();
                        reader.Close();
                        connection.Close();
                    }
                    else 
                    {
                        string id = reader[0].ToString();
                        string update_query = string.Format("UPDATE Data_count_like SET Count_like = @Count_like, Count_dislike = @Count_dislike WHERE id = @id");
                        SQLiteCommand update_command = new SQLiteCommand(update_query, connection);

                        update_command.Parameters.AddWithValue("Count_like", like);
                        update_command.Parameters.AddWithValue("Count_dislike", dislake);
                        update_command.Parameters.AddWithValue("id", id);
                        update_command.ExecuteNonQuery();
                        reader.Close();
                        connection.Close();
                    }
                }
            }
        }
    }
}



