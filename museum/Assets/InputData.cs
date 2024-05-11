using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    //データベースから画像を抽出

    //データベースから画像を一時的に入力する用の配列
    public int photoList[];
    public int photoCorrider[];//廊下用の画像の配列
    public int photoRoom[];//部屋用の画像の配列

    public string userName;

    public void Input(){
        UserName userName = GetComponent<UserName>();
        userName = userName.user;

        /*データベースからuserが一致する画像のみを抽出し、photoList[]に画像のidを入力する。*/

        int corriderCount = 0;
        int roomCount = 0;
        for(int i; i < photoList.length; i++){
            if(photoList[i].tag.CompareTo(/*廊下用画像のtagを入力する*/) == 0){//tagが廊下用画像のtagと一致する時
                photoCorrider[corriderCount] = photoList[i];
                corriderCount++;
            }
            else{//tagが部屋用画像のtagと一致する時
                photoRoom[roomCount] = photoList[i];
                roomCount++;
            }
        }
    }
}
