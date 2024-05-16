using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using Newtonsoft.Json;


public class LogIn : MonoBehaviour
{
    // Start is called before the first frame update

    //ボタンを押すとデータベースからUnityのユーザ名とデータベースのuserを一致するアカウントを見つける。
    //認証が成功したら次のシーンに移る。

    public Button button;

    //データベース用のパスワード    
    private string hashedPasswordDB;

    //Unity用のユーザ名とパスワード
    public TMP_InputField inputUserName;
    public TMP_InputField inputPassword;
    public string userFromU;
    private string hashedPasswordFromU;
    
    public async void OnClick(){

        //Unity用のユーザ名をテキストフィールドから代入する。
        userFromU = inputUserName.text;
        string salt;
        hashedPasswordFromU = Hash(inputPassword.text, out salt);

        //データベースからuserFromUと一致するuserを探しそのuserのパスワードをpasswordFromDBに代入する。
        string url = "https://vr-museum-6034ae04d19d.herokuapp.com/api/user_model/";

        List<MyData> myData = await FetchData(url);//DBから取得する
        
        if(myData != null){
           foreach(MyData data in myData){
                if(userFromU == data.username){
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
            SceneManager.LoadScene("MyMuseum");
        }
        else{
            UnityEngine.Debug.Log ("パスワードが間違っています。");
        }
        
        

    }

    //ハッシュ化
    private string Hash(string password, out string salt){
        // ソルトを生成
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            // PBKDF2を使ってパスワードをハッシュ化
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 720000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 256ビットのハッシュを生成
                return Convert.ToBase64String(hash);
            }
        }
    }

    //DBからデータ取得する
    async Task<List<MyData>> FetchData(string url){
    
        using (HttpClient client = new HttpClient()){//HTTPリクエストを送信し、受信する
            HttpResponseMessage response = await client.GetAsync(url);//レスポンス結果

            if(response.IsSuccessStatusCode){//レスポンスが正常に取得できた時、データを取得する
                string responseData = await response.Content.ReadAsStringAsync();
                //string jsonData = responseData.TrimStart('[').TrimEnd(']');
                return JsonConvert.DeserializeObject<List<MyData>>(responseData);
            }
            else{//エラー処理
                Debug.LogError("Error: " + response.StatusCode);
                return null;
            }
        }
    }

    public class MyData{
        public int id;
        public string username;
        public string password;
    }
}
