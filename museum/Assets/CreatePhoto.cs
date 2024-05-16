using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Threading.Tasks;

public class CreatePhoto : MonoBehaviour
{
    // Start is called before the first frame update
    //このスクリプトはデータベースから画像を抽出し、画像を板に貼り付けフレームをつける。
    
    public LinkedList photoList = new LinkedList();//双方向リストの用意
    public int[] sORr;//廊下用の画像か部屋用の画像かを判断する

    //Prefab用
    [SerializeField]
    GameObject board;
    
    //Prefabテスト用
    //Vector3 position = Vector3.zero;
    [SerializeField]
    float padding = 2;


    void Start(){
        extractionDB();   
    }

    void Update(){
    }

    async Task extractionDB(){
        //DBからデータを取得する
        string url = "https://vr-museum-6034ae04d19d.herokuapp.com/api/photo_model/";
        string rootUrl = "https://vr-museum-6034ae04d19d.herokuapp.com";//画像貼り付け用のurl

        List<MyData> myData = await FetchData(url);//DBから取得する

        sORr = new int[myData.Count];

        Vector3 position = Vector3.zero;//Prefabテスト用

        //boardに画像を貼り付け、双方向リストに挿入する。
        if(myData != null){
            Debug.Log("ok");

            LogIn login = new LogIn();
            string userFromU = login.userFromU;

            foreach(MyData data in myData){
                if(userFromU == data.user){

                }
                float width = (float)data.width;
                float height = (float)data.height;

                //Prefabによるインスタンス生成
                GameObject boardInstance = Instantiate(board, position, Quaternion.identity);
                boardInstance.transform.localScale = new Vector3(width/(width+height), height/(width+height), (float)0.05);
                position.x += padding;//Prefabテスト用

                //画像をテクスチャとして生成する
                string imageUrl = rootUrl + data.content;
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);//テクスチャを取得するためのオブジェクト
                var asyncOperation = www.SendWebRequest();//HTTPリクエストの送信
                asyncOperation.completed += (op) =>{//HTTPリクエストの完了したとき、テクスチャを取得する
        
                    if (www.result == UnityWebRequest.Result.Success){//リクエストが成功した時
                        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;       
                        boardInstance.GetComponent<Renderer>().material.mainTexture = texture;//取得したテクスチャをboardのテクスチャとする
                    }
                    else{
                        Debug.Log("画像の読み込みに失敗しました: " + www.error);
                    }
                };

                photoList.Append(data.title, data.detailed_title, data.time, boardInstance, height, width, data.tag, data.photo_num);
            }
            
            
        }

    }

    //DBからデータ取得する
    async Task<List<MyData>> FetchData(string url){
        
        using (HttpClient client = new HttpClient()){//HTTPリクエストを送信し、受信する
            HttpResponseMessage response = await client.GetAsync(url);//レスポンス結果

            if(response.IsSuccessStatusCode){//レスポンスが正常に取得できた時、データを取得する
                string responseData = await response.Content.ReadAsStringAsync();
                string jsonData = responseData.TrimStart('[').TrimEnd(']');
                return JsonUtility.FromJson<List<MyData>>(jsonData);
            }
            else{//エラー処理
                Debug.LogError("Error: " + response.StatusCode);
                return null;
            }
        }
    }

    [Serializable]
    public class MyData{
    public int id;
    public string title;
    public string detailed_title;
    public string user;
    public string time;
    public int photo_num;
    public string content;
    public int height;
    public int width;
    public string tag;
        
    }
}
