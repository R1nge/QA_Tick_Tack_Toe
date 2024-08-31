using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Services.Web
{
    public class WebRequestsService
    {
        public async Task<List<Weather>> GetWeather()
        {
            var req = await SendGetRequest<List<Weather>>("https://localhost:44335/weatherforecast", null);
            Debug.Log(req[0].summary);
            return req;
        }

        public class Weather
        {
            public string date { get; set; }
            public string temperatureC { get; set; }
            public string summary { get; set; }
            public string temperatureF { get; set; }
        }

        private async Task<TResponse> SendPostRequest<TRequest, TResponse>(string url, TRequest request,
            Dictionary<string, string> body, string authHeaderName = "Auth")
        {
            string json = JsonConvert.SerializeObject(request);

            UnityWebRequest webRequest = body == null
                ? UnityWebRequest.PostWwwForm(url, json)
                : UnityWebRequest.Post(url, body);

            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            await webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"POST REQUEST {url} ERROR: {webRequest.error}  MESSAGE: {webRequest.downloadHandler.text}");
                return default;
            }

            string responseJson = webRequest.downloadHandler.text;
            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }

        private async Task<TResponse> SendGetRequest<TResponse>(string url, Dictionary<string, string> body,
            string authHeaderName = "Auth")
        {
            var webRequest = new UnityWebRequest(url);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            Debug.Log($"GET REQUEST {url} {webRequest.responseCode}");

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"GET REQUEST {url} ERROR: {webRequest.error}  MESSAGE: {webRequest.downloadHandler.text}");
                return default;
            }

            string responseJson = webRequest.downloadHandler.text;
            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }
    }
}