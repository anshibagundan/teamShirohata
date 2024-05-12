using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Imgbynary : MonoBehaviour
{
    public string apiUrl = "https://example.com/api/image";
    public Renderer imageRenderer; // 画像を表示するためのRenderer

    void Start()
    {
        StartCoroutine(LoadImageFromAPI(apiUrl));
    }

    IEnumerator LoadImageFromAPI(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // バイナリーデータの取得
            byte[] imageData = www.downloadHandler.data;

            // バイナリーデータをテクスチャに変換
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // テクスチャをレンダラーに設定
            imageRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.Log("APIからのバイナリーデータの取得に失敗しました: " + www.error);
        }
    }
}
