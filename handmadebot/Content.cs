using System.Text;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using System.Net;


namespace handmadebot
{
    public class Content
    {
        static IConfiguration config = Configuration.Default;
        static IBrowsingContext context = BrowsingContext.New(config);
        static IHtmlParser parser = context.GetService<IHtmlParser>();
        static WebClient client = new WebClient();

        public static string Get_News(string name_service, string reference)
        {
            string news_rezult = null;
            switch (name_service)
            {
                case "Вслух.Ру":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        var news_text = document.QuerySelectorAll("[itemprop = \"articleBody\"]  p:not(:last-child)");
                        var news_text_2 = document.QuerySelectorAll("[itemprop = \"articleBody\"] h2");
                        foreach (IElement i in news_text)
                        {
                            news_rezult += i.Html();
                            news_rezult += '\n';
                        }
                        foreach (IElement i in news_text_2)
                        {
                            news_rezult += i.Html();
                            news_rezult += '\n';
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "Новости Тюмени 72.ru":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[itemprop = \"articleBody\"] p:not([itemscope = \"itemscope\"])"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "Интерфакс":
                    client.Encoding = Encoding.GetEncoding("Windows-1251");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[itemprop = \"articleBody\"]  p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "АиФ Тюмень":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);

                        foreach (IElement i in document.QuerySelectorAll("[class = \"article_text\"]  p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "NashGorod.ru":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);

                        foreach (IElement i in document.QuerySelectorAll("[class = \"js-mediator-article\"] h2"))
                        {
                            news_rezult += i.Text() + "\n";
                        }

                        foreach (IElement i in document.QuerySelectorAll("[class = \"js-mediator-article\"] div p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }

                        return news_rezult;

                    }
                    catch
                    {
                        return null;
                    }
                case "РИА Новости":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[class = \"article__text\"]"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "КП - Тюмень":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[class = \"text js-mediator-article\"] p:not(:last-child)"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "RT на русском":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[class = \"article__summary article__summary_article-page js-mediator-article\"]"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        foreach (IElement i in document.QuerySelectorAll("[class = \"article__text article__text_article-page js-mediator-article\"] p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "РБК":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[class = \"article__text article__text_free\"] p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }
                case "Megatyumen.ru":
                    client.Encoding = Encoding.GetEncoding("utf-8");
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25");
                    try
                    {
                        var HtmlCode = client.DownloadString(reference);
                        var document = parser.ParseDocument(HtmlCode);
                        foreach (IElement i in document.QuerySelectorAll("[class = \"post-body-text\"] p"))
                        {
                            news_rezult += i.Text() + "\n";
                        }
                        return news_rezult;
                    }
                    catch
                    {
                        return null;
                    }

                default:
                    return null;

            }
        }
    }
}
