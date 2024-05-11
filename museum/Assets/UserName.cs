using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    // Start is called before the first frame update

    //ユーザ名の入力

    public InputField inputField;
    public string user;

    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        user = inputField.text;
    }
}
