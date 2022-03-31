using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using static handmadebot.TelegramClass;
using static handmadebot.TelegramRequestParameter;



namespace handmadebot
{
    public class TelegramAPI
    {
        const string API_URL = "https://api.telegram.org/bot" + SecretKey.API_KEY + "/"; 
        static  readonly HttpClient client = new HttpClient(); 
        private int lastUpdateId = 0; 
        public TelegramAPI() 
        {
           
        }
        public class ApiResult 
        {
            public Update[] result { get; set; }
        }
       
        async static private Task<string> sendApiRequest(string ApiMethod, string Params) 
        {
            string Url = API_URL + ApiMethod + "?" + Params;
            //Console.WriteLine(Url);
            var response = await client.GetAsync(Url).Result.Content.ReadAsStringAsync();
            return response;
        }
        async static private Task<HttpResponseMessage> sendApiRequest(string ApiMethod, object obj) 
        {
            Uri u = new Uri(API_URL + ApiMethod);
            try 
            {
                if (obj is sendMessageParameter) { obj = (sendMessageParameter)obj; }
                else if (obj is sendPhotoParameterChannel) { obj = (sendPhotoParameterChannel)obj; }
                else if (obj is sendMediaGroupParameter) { obj = (sendMediaGroupParameter)obj; }
                else if (obj is deleteMessageParameter) { obj = (deleteMessageParameter)obj; }
                else if (obj is answerCallbackQueryParameter) { obj = (answerCallbackQueryParameter)obj; }
                else if (obj is editMessageReplyMarkupParameter) { obj = (editMessageReplyMarkupParameter)obj; }
                else if (obj is editMessageTextParameter) { obj = (editMessageTextParameter)obj; }
                else { throw new Exception("Неизвестный класс для отправки"); };

            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            string Json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = u,
                Content = content
            };
            HttpResponseMessage result = await client.SendAsync(request);
            return result;
        }
        async static public Task<HttpResponseMessage> sendMessage(long chat_id, string text)
        {
            var payload = new sendMessageParameter { chat_id = chat_id, text = text};
            HttpResponseMessage result = await sendApiRequest("sendMessage", payload);
            return result;
        }
        async static public Task<HttpResponseMessage> sendMessage(sendMessageParameter Params)
        {            
            HttpResponseMessage result = await sendApiRequest("sendMessage", Params);
            return result;
        }
        async public Task<HttpResponseMessage> sendMediaGroup(sendMediaGroupParameter Params) 
        {
            HttpResponseMessage result = await sendApiRequest("sendMediaGroup", Params);
            return result;
        }
        async static public Task<HttpResponseMessage> sendPhoto(sendPhotoParameterChannel Params)
        {
            HttpResponseMessage result = await sendApiRequest("sendPhoto", Params);
            return result;
        }
        async static public Task<HttpResponseMessage> answerCallbackQuery(answerCallbackQueryParameter Params) 
        {
            HttpResponseMessage result = await sendApiRequest("answerCallbackQuery", Params);
            return result;
        }
        async static public Task<HttpResponseMessage> deleteMessage(deleteMessageParameter Params)
        {
            HttpResponseMessage result = await sendApiRequest("deleteMessage", Params);
            return result;
        }
        async static public Task<HttpResponseMessage> editMessageReplyMarkup(editMessageReplyMarkupParameter Params) 
        {
            HttpResponseMessage result = await sendApiRequest("editMessageReplyMarkup", Params);
            return result;
        }
        async static public Task<HttpResponseMessage> editMessageText(editMessageTextParameter Params)
        {
            HttpResponseMessage result = await sendApiRequest("editMessageText", Params);
            return result;
        }
        async public Task<Update[]> getUpdates() 
        {
            var json = await sendApiRequest("getUpdates", $"offset={lastUpdateId}");
            //Console.WriteLine(json);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            //var test = JsonConvert.SerializeObject(apiResult);
            //Console.WriteLine(test);
            foreach (var update in apiResult.result) 
            {
                lastUpdateId = update.update_id + 1; //смещаем чтоб читать только те апдейты которые еще не получали
            }
            return apiResult.result;
        }
        async public void StartReceiving(int timeout) 
        {
            while (true) 
            {
                var updates = await getUpdates();
                foreach (var update in updates)
                {
                    if (update.Type == TelegramEnum.UpdateType.Message) 
                    {
                        onMessage?.Invoke(this, update);
                    }

                    if (update.Type == TelegramEnum.UpdateType.EditedMessage)
                    {
                        onEditedMessage?.Invoke(this, update);
                    }

                    if (update.Type == TelegramEnum.UpdateType.ChannelPost)
                    {
                        onChannelPost?.Invoke(this, update);
                    }

                    if (update.Type == TelegramEnum.UpdateType.CallbackQuery)
                    {
                        onCallbackQuery?.Invoke(this, update);
                    }

                    if (update.Type == TelegramEnum.UpdateType.EditedChannelPost) 
                    {
                        onEditedChannelPost?.Invoke(this, update);
                    }
                }
                Thread.Sleep(timeout);
            }
        }

        public event Action<object, Update> onMessage;
        public event Action<object, Update> onEditedMessage;
        public event Action<object, Update> onChannelPost;
        public event Action<object, Update> onEditedChannelPost;
        public event Action<object, Update> onCallbackQuery;
    }
}
