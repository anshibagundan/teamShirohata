using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

public class DatabaseConnector : MonoBehaviour
{
    [SerializeField] Renderer imageRenderer;

    async void Start()
    {
        string url = "https://vr-museum-6034ae04d19d.herokuapp.com/api/photo_model/";
        string rootUrl = "https://vr-museum-6034ae04d19d.herokuapp.com";

        MyData myData = await FetchData(url);
        if (myData != null)
        {
            string imageUrl = rootUrl + myData.content;
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            var asyncOperation = www.SendWebRequest();
            asyncOperation.completed += (op) =>
            {
                OnDownloadCompleted(www);
            };
        }
    }

    async Task<MyData> FetchData(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                string jsonData = responseData.TrimStart('[').TrimEnd(']');
                return JsonUtility.FromJson<MyData>(jsonData);
            }
            else
            {
                Debug.LogError("Error: " + response.StatusCode);
                return null;
            }
        }
    }

    void OnDownloadCompleted(UnityWebRequest www)
    {
        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            
imageRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.Log("画像の読み込みに失敗しました: " + www.error);
        }
    }

    [Serializable]
    public class MyData
    {
        public string content;
    }
}
