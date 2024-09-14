using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using _Assets.Scripts.Gameplay;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Services.Web
{
    public class WebRequestsService
    {
        
        private readonly string _baseUrl = "http://213.178.155.172/api";
        public Task<List<List<TurnService.Team>>> MakeTurn(int x, int y)
        {
            var req = SendPostRequest<MakeATurnRequest, List<List<TurnService.Team>>>($"{_baseUrl}/maketurn{x},{y}", new MakeATurnRequest(x, y), new Dictionary<string, string>());
            return req;
        }
        
        public Task<List<List<TurnService.Team>>> GetBoard()
        {
            var req = SendGetRequest<List<List<TurnService.Team>>>($"{_baseUrl}/getboard", new Dictionary<string, string>());
            return req;
        }
        
        public Task<List<List<TurnService.Team>>> ResetBoard()
        {
            var req = SendGetRequest<List<List<TurnService.Team>>>($"{_baseUrl}/resetboard", new Dictionary<string, string>());
            return req;
        }

        public Task<TurnService.Team> GetLastTeam()
        {
            var req = SendGetRequest<TurnService.Team>($"{_baseUrl}/getlastteam", new Dictionary<string, string>());
            return req;
        }
        
        public class MakeATurnRequest
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Team { get; set; }

            public MakeATurnRequest(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class MakeATurnResponse
        {
            
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