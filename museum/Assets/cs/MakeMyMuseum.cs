using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MakeMyMuseum : MonoBehaviour
{
    private List<string> v = new List<string> { "S", "S", "S"};//, "S", "S", "R", "R", "R", "R", "R", "R","R", "R", "R", "R1", "R1", "S", "S", "R2" };
    //↑最終的にはtagで判別するかも…
        
    public GameObject streetPrefab; 
    public GameObject roomPrefab; 
    private Vector3 startPosition = new Vector3(0, 0, 50); // 開始位置
    private Vector3 positionOffset = new Vector3(0, 0, 50); // 各インスタンスの位置オフセット
    
    //写真関連
    public LinkedList photoList = new LinkedList();//双方向リストの用意
    public GameObject exhibitPrefab;  // スペルミス修正
    Quaternion rot =Quaternion.Euler(0,90,180);//exhibitPrefabを回転すさせる
    float padding = 5;
    
    private Vector3 exhibitStart = new Vector3(10, 10, -15); // 開始位置
    private Vector3 exhibitOffset = new Vector3(0, 0, 10); // 各インスタンスの位置オフセット
    private Vector3 leftsidePosition = new Vector3(-10, 10, -12);
    private Vector3 leftsideRotation = new Vector3(0, 180, 0);
    
    public static int streetNum = 0;

    private List<Vector3> roomPhotoPos = new List<Vector3>
    {
        new Vector3(-58, 10 , 0),//1
        new Vector3(-40, 10 , -18),//2
        new Vector3(-40, 10 , 18),//3
        new Vector3(-58, 10 , 10),//4
        new Vector3(-58, 10 , -10),//5
        new Vector3(-30, 10 , -18),//6
        new Vector3(-30, 10 , 18),//7
        new Vector3(-50, 10 , -18),//8
        new Vector3(-50, 10 , 18)//9
    };
    
    private List<Vector3> roomPhotoRote = new List<Vector3>
    {
        new Vector3(0, 180 ,0),//1
        new Vector3(0, 90 ,0),//2
        new Vector3(0, -90 ,0),//3
        new Vector3(0, 180 ,0),//4
        new Vector3(0, 180 ,0),//5
        new Vector3(0, 90 ,0),//6
        new Vector3(0, -90 ,0),//7
        new Vector3(0, 90 ,0),//8
        new Vector3(0, -90 ,0)//9
    };

    async void Start()
    {
       await extractionDB();
       //photoList.Sort();
       MuseumMaker();
        
    }

    async Task extractionDB(){
        //DBからデータを取得する
        string url = "https://vr-museum-6034ae04d19d.herokuapp.com/api/photo_model/";
        string rootUrl = "https://vr-museum-6034ae04d19d.herokuapp.com";//画像貼り付け用のurl

        List<MyData> myData = await FetchData(url);//DBから取得する

        

        //exhibitPrefabに画像を貼り付け、双方向リストに挿入する。
        if(myData != null){

            //LogIn login = new LogIn();
            string userFromU = "RCC";//login.userFromU;

            Vector3 position = Vector3.zero;//Prefabテスト

            foreach(MyData data in myData){
                if(userFromU == data.user){
                    
                }
                float width = (float)data.width;
                float height = (float)data.height;

                //PrefabによるexhibitPrefabのインスタンス生成
                GameObject exhibitPrefabInstance = Instantiate(exhibitPrefab, position, Quaternion.identity);
                exhibitPrefabInstance.transform.localScale = new Vector3((width/(width+height))*5, (height/(width+height))*5, (float)0.05);
                
                position.x += padding;

                //画像をテクスチャとして生成する
                string imageUrl = rootUrl + data.content;
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);//テクスチャを取得するためのオブジェクト
                var asyncOperation = www.SendWebRequest();//HTTPリクエストの送信
                asyncOperation.completed += (op) =>{//HTTPリクエストの完了したとき、テクスチャを取得する
        
                    if (www.result == UnityWebRequest.Result.Success){//リクエストが成功した時
                        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;       
                        exhibitPrefabInstance.GetComponent<Renderer>().material.mainTexture = texture;//取得したテクスチャをexhibitPrefabのテクスチャとする
                    }
                    else{
                        Debug.Log("画像の読み込みに失敗しました: " + www.error);
                    }
                };

                exhibitPrefabInstance.SetActive(false);//オブジェクトの非表示

                photoList.Append(data.title, data.detailed_title, data.time, exhibitPrefabInstance, height, width, data.tag, data.photo_num);
            }
            
        }

    }

    //DBからデータ取得する
    async Task<List<MyData>> FetchData(string url){
    
        using (HttpClient client = new HttpClient()){//HTTPリクエストを送信し、受信する
            HttpResponseMessage response = await client.GetAsync(url);//レスポンス結果

            if(response.IsSuccessStatusCode){//レスポンスが正常に取得できた時、データを取得する
                string responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MyData>>(responseData);
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
    
    private void MuseumMaker()
    {
        int exhibitNum = 0;
        string roomName;
        LinkedPhoto current = photoList.First();
        
        for (int i = 0; i < v.Count;)
        {
            

            if (v[i] == "S")
            {
                // 通路
                Vector3 position = startPosition + streetNum * positionOffset;
                GameObject parentInstance = Instantiate(streetPrefab, position, Quaternion.identity);
                streetNum++;
                // 写真
                // Instantiate(exhibitPrefab, position + exhibitStart, Quaternion.identity, parentInstance.transform);
                current.SetUp(position + exhibitStart,rot );
                //current.gameObject.transform.parent = parentInstance.transform;
                
                i++;
                exhibitNum++;
                current = current.NextPhoto;
                
                while (i < v.Count && v[i] == "S")
                {

                    // 写真
                    Vector3 exhibitPosition = position + exhibitStart + exhibitNum * exhibitOffset;
                    current.SetUp(exhibitPosition, rot);
                    //Debug.Log(current.photoNum_);
                    //current.gameObject.transform.parent = parentInstance.transform;
                    //Instantiate(exhibitPrefab, exhibitPosition, Quaternion.identity, parentInstance.transform);
                    i++;
                    exhibitNum++;

                    if (exhibitNum > 3)
                    {
                        exhibitNum = 0;
                        break;
                    }else if(current.NextPhoto != null){
                        current = current.NextPhoto;
                    }
                }
                exhibitNum = 0;
            }
            else
            {
                roomName = v[i];
                // 通路
                Vector3 position = startPosition + streetNum * positionOffset;
                GameObject parentInstance = Instantiate(roomPrefab, position, Quaternion.identity);
                streetNum++;

                // 写真
                Quaternion rotation = Quaternion.Euler(roomPhotoRote[exhibitNum]);
                current.SetUp(roomPhotoPos[exhibitNum] + position, rotation);
                //Instantiate(exhibitPrefab, roomPhotoPos[exhibitNum] + position, rotation, parentInstance.transform);

                exhibitNum++;
                i++;
                current = current.NextPhoto;

                while (i < v.Count && v[i] == roomName)
                {
                    
                    if (exhibitNum < roomPhotoPos.Count && exhibitNum < roomPhotoRote.Count)
                    {
                        // 写真
                        rotation = Quaternion.Euler(roomPhotoRote[exhibitNum]);
                        current.SetUp(roomPhotoPos[exhibitNum] + position, rotation);
                        //Instantiate(exhibitPrefab, roomPhotoPos[exhibitNum]+position, rotation, parentInstance.transform);
                    }else if(current.NextPhoto != null){
                        current = current.NextPhoto;
                    }

                    i++;
                    exhibitNum++;
                    
                }
                exhibitNum = 0;
            }
            
            
        }
    }
    
    
}