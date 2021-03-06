using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace handmadebot
{
    public static class Create_Database
    {
        public static void Create()
        {
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                using(var connection = new SQLiteConnection("Data Source=database.sqlite3")) 
                {
                    connection.Open();
                    string create_database = string.Format("CREATE TABLE Data_news " + "(id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, " +
                                                                                       "Title_list_new     VARCHAR, " +
                                                                                       "Type_list_news     VARCHAR, " +
                                                                                       "Href_yandex_news   VARCHAR, " +
                                                                                       "Picture_list_news  VARCHAR, " +
                                                                                       "Text_yandex_news   VARCHAR, " +
                                                                                       "Href_sourse_news   VARCHAR, " +
                                                                                       "Agency_yandex_news VARCHAR, " +
                                                                                       "Text_sourse_news   VARCHAR, " +
                                                                                       "Status             VARCHAR (1), " +
                                                                                       "Date_create        DATE);");

                    string create_database_2 = string.Format("CREATE TABLE Data_like " + "(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                                                                                         "Chat_id     INTEGER, " +
                                                                                         "Message_id  INTEGER, " +
                                                                                         "From_id     INTEGER, " +
                                                                                         "Type        VARCHAR (1), " +
                                                                                         "Date_create DATE);");

                    string create_database_3 = string.Format("CREATE TABLE Data_count_like " + "(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                                                                                               "Chat_id       INTEGER, " +
                                                                                               "Message_id    INTEGER, " +
                                                                                               "Count_like    INTEGER, " +
                                                                                               "Count_dislike INTEGER);");





                    SQLiteCommand create_command = new SQLiteCommand(create_database, connection);
                    SQLiteCommand create_command_2 = new SQLiteCommand(create_database_2, connection);
                    SQLiteCommand create_command_3 = new SQLiteCommand(create_database_3, connection);
                    create_command.ExecuteNonQuery();
                    create_command_2.ExecuteNonQuery();
                    create_command_3.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

    }

    




}
