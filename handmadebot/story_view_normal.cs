using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using System.Net;

namespace handmadebot
{
    public class story_view_normal
    {
        public story_view_normal()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();

            try 
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
                    string htmlCode = client.DownloadString("https://yandex.ru/news/region/tyumen?no_turbo=1");
                    var document = parser.ParseDocument(htmlCode);

                    foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_normal\"] h2.story__title a"))
                    {
                        Title_list_new.Add(i.Text());
                    }

                    foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_normal\"] > a"))
                    {
                        Type_list_news.Add(i.Text());
                    }

                    foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_normal\"] div.story__image-container img.image"))
                    {
                        Picture_list_news.Add(i.GetAttribute("src"));
                    }

                    foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_normal\"] h2.story__title a"))
                    {
                        Href_yandex_news.Add(i.GetAttribute("href").Contains("https://yandex.ru") == true ? i.GetAttribute("href") + "&no_turbo=1" : "https://yandex.ru" + i.GetAttribute("href") + "&no_turbo=1");
                    }

                    if (Title_list_new.Count == Type_list_news.Count && Type_list_news.Count == Picture_list_news.Count && Picture_list_news.Count == Href_yandex_news.Count)
                    {
                        for (int i = 0; i < Title_list_new.Count; i++)
                        {
                            Story.Add(new Data()
                            {
                                Title_list_new = Title_list_new[i],
                                Type_list_news = Type_list_news[i],
                                Picture_list_news = Picture_list_news[i],
                                Href_yandex_news = Href_yandex_news[i]
                            });
                        }
                    }
                    else
                    {
                        throw new Exception("Длины массивов с частями новости не совпадают по размеру!!!");
                    }

                    for (int i = 0; i < Story.Count; i++)
                    {
                        client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
                        htmlCode = client.DownloadString(Story[i].Href_yandex_news);
                        document = parser.ParseDocument(htmlCode);

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group a [class = \"doc__agency\"]") != null)
                        {
                            Story[i].Agency_yandex_news = document.QuerySelector("[class = \"story__main\"] div.story__group a [class = \"doc__agency\"]").Text();
                        }

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group div.doc__text") != null)
                        {
                            Story[i].Text_yandex_news = document.QuerySelector("[class = \"story__main\"] div.story__group div.doc__text").Text();
                        }

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group a") != null)
                        {
                            Story[i].Href_sourse_news = document.QuerySelector("[class = \"story__main\"] div.story__group a").GetAttribute("href");
                        }

                        Story[i].Text_sourse_news = Content.Get_News(Story[i].Agency_yandex_news, Story[i].Href_sourse_news);
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Произошел сбой в конструкторе story_view_normal");
                Console.WriteLine($"Исключение: {ex.Message}");
            }           
        }

        public class Data
        {
            public string Title_list_new { get; set; } = null; //Заголовок новости
            public string Type_list_news { get; set; } = null; //Категория новости
            public string Href_yandex_news { get; set; } = null; //Ссылка новости Яндекс
            public string Picture_list_news { get; set; } = null; //Картинка к заголовку новости Яндекс
            public string Text_yandex_news { get; set; } = null; //Текст новости Яндекса
            public string Href_sourse_news { get; set; } = null; //Ссылка на саму новость
            public string Agency_yandex_news { get; set; } = null; //Имя издателя
            public string Text_sourse_news { get; set; } = null; //Основной текст новости
        };
        public List<Data> Story = new List<Data>();

        public List<string> Title_list_new = new List<string>();
        public List<string> Type_list_news = new List<string>();
        public List<string> Href_yandex_news = new List<string>();
        public List<string> Picture_list_news = new List<string>();
        public List<string> Text_yandex_news = new List<string>();
        public List<string> Href_sourse_news = new List<string>();
        public List<string> Agency_yandex_news = new List<string>();
        public List<string> Text_sourse_news = new List<string>();

    }
}
