using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    // Start is called before the first frame update

    //パスワードの入力
    public InputField inputField;
    public string password;

    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        password = inputField.text;
    }
}
