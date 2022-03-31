using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using System.Net;

namespace handmadebot
{
    public class story_story_view_main
    {
        public story_story_view_main()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();

            try 
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
                    //client.Headers.Add(HttpRequestHeader.Host, "https://yandex.ru/news/region/tyumen?no_turbo=1");
                    string htmlCode = client.DownloadString("https://yandex.ru/news/region/tyumen?no_turbo=1");
                    //string htmlCode = client.DownloadString("https://yandex.ru/news/region/tyumen");
                    var document = parser.ParseDocument(htmlCode);

                    Console.WriteLine(123);

                    if (document.QuerySelector("[class = \"story story_view_main\"] h2.story__title a") != null)
                    {
                        foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_main\"] h2.story__title a"))
                        {
                            Story.Add(new Data() { Title_list_new = i.Text() });
                            Console.WriteLine(i.Text());
                        }

                        foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_main\"] > a"))
                        {
                            Story[0].Type_list_news = i.Text();
                        }

                        foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_main\"] div.story__image-container img.image"))
                        {
                            Story[0].Picture_list_news = i.GetAttribute("src");
                        }

                        foreach (IElement i in document.QuerySelectorAll("[class = \"story story_view_main\"] h2.story__title a"))
                        {
                            Story[0].Href_yandex_news = i.GetAttribute("href").Contains("https://yandex.ru") == true ? i.GetAttribute("href") + "&no_turbo=1" : "https://yandex.ru" + i.GetAttribute("href") + "&no_turbo=1";
                        }

                        client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
                        htmlCode = client.DownloadString(Story[0].Href_yandex_news);
                        document = parser.ParseDocument(htmlCode);

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group a [class = \"doc__agency\"]") != null)
                        {
                            Story[0].Agency_yandex_news = document.QuerySelector("[class = \"story__main\"] div.story__group a [class = \"doc__agency\"]").Text();
                        }

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group div.doc__text") != null)
                        {
                            Story[0].Text_yandex_news = document.QuerySelector("[class = \"story__main\"] div.story__group div.doc__text").Text();
                            //Story[0].Text_yandex_news = document.QuerySelector("div.story__main div.story__group div.doc.doc_for_story div.doc__content div.doc__text").Text();
                            //Console.WriteLine(document.QuerySelector("div.story__main div.story__group div.doc.doc_for_story div.doc__content div.doc__text").Text());
                        }

                        if (document.QuerySelector("[class = \"story__main\"] div.story__group a") != null)
                        {
                            Story[0].Href_sourse_news = document.QuerySelector("[class = \"story__main\"] div.story__group a").GetAttribute("href");
                        }

                        Story[0].Text_sourse_news = Content.Get_News(Story[0].Agency_yandex_news, Story[0].Href_sourse_news);

                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Произошел сбой в конструкторе story_story_view_main");
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
    }
}
