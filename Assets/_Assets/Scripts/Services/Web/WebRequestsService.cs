﻿using System.Collections.Generic;
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
        public Task<List<List<TurnService.Team>>> GetBoard(int x, int y, int team)
        {
            var req = SendPostRequest<MakeATurnRequest, List<List<TurnService.Team>>>($"https://localhost:44335/maketurn{x},{y},{team}", new MakeATurnRequest(x, y, team), new Dictionary<string, string>());
            return req;
        }
        
        public class MakeATurnRequest
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Team { get; set; }

            public MakeATurnRequest(int x, int y, int team)
            {
                X = x;
                Y = y;
                Team = team;
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