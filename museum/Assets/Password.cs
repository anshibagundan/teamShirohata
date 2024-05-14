using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class Password : MonoBehaviour
{
    // Start is called before the first frame update

    //パスワードの入力
    public InputField inputField;
    private string password;

    public string hashedPassword;


    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        password = inputField.text;
        if (!string.IsNullOrEmpty(password))
        {
            hashedPassword = Hash(password);
        }
        else
        {
            hashedPassword = string.Empty;
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
}
