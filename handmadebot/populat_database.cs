using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using System.Data.Common;

using static handmadebot.TelegramClass;
using static handmadebot.TelegramRequestParameter;

namespace handmadebot
{
    public class populat_database
    {
        public static void populate()
        {
            while (true)
            {
                try 
                {
                    story_story_view_main main_new = new story_story_view_main();
                    foreach (story_story_view_main.Data i in main_new.Story)
                    {
                        using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
                        {
                            connection.Open();
                            string check = string.Format("SELECT ID " +
                                                           "FROM Data_news " +
                                                          "WHERE Title_list_new = @Title_list_new " +
                                                          "AND Picture_list_news = @Picture_list_news");
                            SQLiteCommand check_command = new SQLiteCommand(check, connection);
                            check_command.Parameters.AddWithValue("Title_list_new", i.Title_list_new);
                            check_command.Parameters.AddWithValue("Picture_list_news", i.Picture_list_news);
                            using (var reader = check_command.ExecuteReader())
                            {
                                reader.Read();
                                if (!reader.HasRows)
                                {
                                    string query = string.Format("INSERT INTO Data_news" + "(Title_list_new, " +
                                                                                          "Type_list_news, " +
                                                                                          "Href_yandex_news, " +
                                                                                          "Picture_list_news, " +
                                                                                          "Text_yandex_news, " +
                                                                                          "Href_sourse_news, " +
                                                                                          "Agency_yandex_news, " +
                                                                                          "Text_sourse_news, " +
                                                                                          "Date_create) " +
                                                                 "VALUES(@Title_list_new, " +
                                                                        "@Type_list_news, " +
                                                                        "@Href_yandex_news, " +
                                                                        "@Picture_list_news, " +
                                                                        "@Text_yandex_news, " +
                                                                        "@Href_sourse_news, " +
                                                                        "@Agency_yandex_news, " +
                                                                        "@Text_sourse_news, " +
                                                                        "datetime());");

                                    SQLiteCommand comand = new SQLiteCommand(query, connection);
                                    comand.Parameters.AddWithValue("Title_list_new", i.Title_list_new);
                                    comand.Parameters.AddWithValue("Type_list_news", i.Type_list_news);
                                    comand.Parameters.AddWithValue("Href_yandex_news", i.Href_yandex_news);
                                    comand.Parameters.AddWithValue("Picture_list_news", i.Picture_list_news);
                                    comand.Parameters.AddWithValue("Text_yandex_news", i.Text_yandex_news);
                                    comand.Parameters.AddWithValue("Href_sourse_news", i.Href_sourse_news);
                                    comand.Parameters.AddWithValue("Agency_yandex_news", i.Agency_yandex_news);
                                    comand.Parameters.AddWithValue("Text_sourse_news", i.Text_sourse_news);

                                    comand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Произошел сбой в работе функции populate при заполнении БД главной новостью");
                    Console.WriteLine($"Исключение: {ex.Message}");
                }

                try
                {
                    story_view_normal news = new story_view_normal();
                    foreach (story_view_normal.Data i in news.Story)
                    {
                        using (var connection = new SQLiteConnection("Data Source=database.sqlite3"))
                        {
                            connection.Open();
                            string check = string.Format("SELECT ID " +
                                                           "FROM Data_news " +
                                                          "WHERE Title_list_new = @Title_list_new " +
                                                          "OR Picture_list_news = @Picture_list_news");
                            SQLiteCommand check_command = new SQLiteCommand(check, connection);
                            check_command.Parameters.AddWithValue("Title_list_new", i.Title_list_new);
                            check_command.Parameters.AddWithValue("Picture_list_news", i.Picture_list_news);
                            using (var reader = check_command.ExecuteReader())
                            {
                                reader.Read();
                                if (!reader.HasRows)
                                {
                                    string query = string.Format("INSERT INTO Data_news" + "(Title_list_new, " +
                                                                                              "Type_list_news, " +
                                                                                              "Href_yandex_news, " +
                                                                                              "Picture_list_news, " +
                                                                                              "Text_yandex_news, " +
                                                                                              "Href_sourse_news, " +
                                                                                              "Agency_yandex_news, " +
                                                                                              "Text_sourse_news, " +
                                                                                              "Date_create)" +
                                                                     "VALUES(@Title_list_new, " +
                                                                            "@Type_list_news, " +
                                                                            "@Href_yandex_news, " +
                                                                            "@Picture_list_news, " +
                                                                            "@Text_yandex_news, " +
                                                                            "@Href_sourse_news, " +
                                                                            "@Agency_yandex_news, " +
                                                                            "@Text_sourse_news, " +
                                                                            "datetime());");

                                    SQLiteCommand comand = new SQLiteCommand(query, connection);
                                    comand.Parameters.AddWithValue("Title_list_new", i.Title_list_new);
                                    comand.Parameters.AddWithValue("Type_list_news", i.Type_list_news);
                                    comand.Parameters.AddWithValue("Href_yandex_news", i.Href_yandex_news);
                                    comand.Parameters.AddWithValue("Picture_list_news", i.Picture_list_news);
                                    comand.Parameters.AddWithValue("Text_yandex_news", i.Text_yandex_news);
                                    comand.Parameters.AddWithValue("Href_sourse_news", i.Href_sourse_news);
                                    comand.Parameters.AddWithValue("Agency_yandex_news", i.Agency_yandex_news);
                                    comand.Parameters.AddWithValue("Text_sourse_news", i.Text_sourse_news);

                                    comand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошел сбой в работе функции populate при заполнении БД всеми остальными новостями");
                    Console.WriteLine($"Исключение: {ex.Message}");
                }

                Console.WriteLine("Один цикл обновления БД отработал");
                SendNews();
                Thread.Sleep(3000000);
            }
        }

        public async static void SendNews() 
        {
            using (var connection = new SQLiteConnection("Data Source=database.sqlite3")) 
            {
                connection.Open();
                string query = "SELECT id, Title_list_new, Type_list_news, Picture_list_news, Text_yandex_news, Href_sourse_news, Agency_yandex_news, Text_sourse_news FROM Data_news WHERE Status is null";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    string id = record["id"].ToString();
                    string title = record["Title_list_new"].ToString();
                    string type = record["Type_list_news"].ToString();
                    string picture = record["Picture_list_news"].ToString();
                    string short_text = record["Text_yandex_news"].ToString();
                    string long_text = record["Text_sourse_news"].ToString();
                    string href = record["Href_sourse_news"].ToString();
                    string agency = record["Agency_yandex_news"].ToString();

                    bool check = (title != "")
                              && (type != "")
                              && (picture != "")
                              && (short_text != "")
                              && (href != "")
                              && (agency != "");

                    if (check)
                    {
                        try 
                        {
                            string format_text = "\n" + type + "\n"  + title  + "\n" + short_text + "\n" + picture + "\n" + agency + "\n" + href + "\n";
                            InlineKeyboardButton[][] mas = new InlineKeyboardButton[1][];
                            mas[0] = new InlineKeyboardButton[1] { new InlineKeyboardButton { text = "Удалить❌", callback_data = "delete_message_bot" } };
                            var keyboard = new InlineKeyboardMarkup(mas);


                            var payloadbot = new sendMessageParameter
                            {
                                text = format_text,
                                chat_id = 896172479,
                                parse_mode = "HTML",
                                disable_notification = true,
                                disable_web_page_preview = true,
                                reply_markup = keyboard
                            };

                            await TelegramAPI.sendMessage(payloadbot);

                            payloadbot = new sendMessageParameter
                            {
                                text = format_text,
                                chat_id = 114642706,
                                parse_mode = "HTML",
                                disable_notification = true,
                                disable_web_page_preview = true,
                                reply_markup = keyboard
                            };

                            await TelegramAPI.sendMessage(payloadbot);

                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine("Произошел сбой во время отправки новости из БД");
                            Console.WriteLine($"Исключение: {ex.Message}");
                        }
                        finally 
                        {
                            string update_rec = string.Format("UPDATE Data_news SET Status = 'Y' WHERE id = @id");
                            SQLiteCommand comand = new SQLiteCommand(update_rec, connection);
                            comand.Parameters.AddWithValue("id", id);
                            comand.ExecuteNonQuery();
                        }                        
                    }
                }
            }
        }
    }

   

}
