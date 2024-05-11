using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    // Start is called before the first frame update

    //ボタンを押すとデータベースからUnityのユーザ名とデータベースのuserを一致するアカウントを見つける。
    //認証が成功したら次のシーンに移る。

    //データベース用のパスワード
    public string passwordFromDB;
    //Unity用のユーザ名とパスワード
    public string userFromU;
    public string passwordFromU;
    
    public void OnClick(){
        //Unity用のユーザ名とパスワードをUserName.csとPassword.csから代入する。
        UserName userName = GetComponent<UserName>();
        Password password = GetComponent<Password>();
        userFromU = userName.user;
        passwordFromU = password.password;

        /*データベースからuserFromUと一致するuserを探しそのuserのパスワードをpasswordFromDBに代入する。*/
       

        if(passwordFromU.CompareTo(passwordFromDB) == 0){
            //MyMuseumSceneに切り替え
            //MyMuseumSceneは美術館生成しているSceneである。
            SceneManager.LoadScene("MyMuseumScene");
        }
        else{
            UnityEngine.Debug.Log ("ユーザ名またはパスワードが間違っています。");
        }

    }
}
