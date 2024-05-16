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
        hashedPasswordFromU = Hash(inputPassword.text);

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
    private string Hash(string password){
        using(SHA256 sha256 = SHA256.Create()){
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < bytes.Length; i++){
                builder.Append(bytes[i].ToString("x2"));//連結して16進数に変換
            }
            return builder.ToString();
        }
    }

    //DBからデータ取得する
    private async Task<List<MyData>> FetchData(string url){
    
        using (HttpClient client = new HttpClient()){//HTTPリクエストを送信し、受信する
            HttpResponseMessage response = await client.GetAsync(url);//レスポンス結果

            if(response.IsSuccessStatusCode){//レスポンスが正常に取得できた時、データを取得する
                string responseData = await response.Content.ReadAsStringAsync();
                string jsonData = responseData.TrimStart('[').TrimEnd(']');
                Debug.Log(jsonData);
                return JsonUtility.FromJson<List<MyData>>(jsonData);
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
