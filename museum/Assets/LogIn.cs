using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System.Threading.Tasks;

public class LogIn : MonoBehaviour
{
    // Start is called before the first frame update

    //ボタンを押すとデータベースからUnityのユーザ名とデータベースのuserを一致するアカウントを見つける。
    //認証が成功したら次のシーンに移る。

    //データベース用のパスワード    
    private string hashedPasswordDB;

    //Unity用のユーザ名とパスワード
    public string userFromU;
    private string hashedPasswordFromU;
    
    public async void OnClick(){
        //Unity用のユーザ名とパスワードをUserName.csとPassword.csから代入する。
        UserName userName = GetComponent<UserName>();
        Password password = GetComponent<Password>();
        userFromU = userName.user;
        hashedPasswordFromU = password.hashedPassword;

        //データベースからuserFromUと一致するuserを探しそのuserのパスワードをpasswordFromDBに代入する。
        string url = "https://vr-museum-6034ae04d19d.herokuapp.com/api/user_model/";

        List<MyData> myData = await FetchData(url);//DBから取得する
        if(myData != null){
           foreach(MyData data in myData){
                if(userFromU == data.user){
                    hashedPasswordDB = data.password;//パスワードはハッシュ化されている
                    break;
                }
           }
           if(hashedPasswordDB == null){//userが見つからなかった時
                UnityEngine.Debug.Log ("ユーザが存在しません。");
           }
        }

        //入力されたパスワードとデータベースからのパスワードを比較する。
        if(hashedPasswordFromU == hashedPasswordDB){
            //MyMuseumSceneに切り替え
            //MyMuseumSceneは美術館生成しているSceneである。
            SceneManager.LoadScene("MyMuseumScene");
        }
        else{
            UnityEngine.Debug.Log ("パスワードが間違っています。");
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

    public class MyData{
        public string id;
        public string user;
        public string password;
    }
}
