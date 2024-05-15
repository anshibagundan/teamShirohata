using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePhoto : MonoBehaviour
{
    // Start is called before the first frame update
    //このスクリプトはデータベースから画像を抽出し、画像を板に貼り付けフレームをつける。

    // //データベースから画像を一時的に入力する用の配列
    // public int photoList[];
    // public int photoCorrider[];//廊下用の画像の配列
    // public int photoRoom[];//部屋用の画像の配列

    // public string userName;


    void Start(){
        // public void Input(){
    //     UserName userName = GetComponent<UserName>();
    //     userName = userName.user;

    //     /*データベースからuserが一致する画像のみを抽出し、photoList[]に画像のidを入力する。*/

    //     int corriderCount = 0;
    //     int roomCount = 0;

        

        //for文にて画像に合わせて板とフレーム作成し、統合する。その後に廊下用と部屋用の画像と振り分ける。prehabで作成予定
        
        // for(int i; i < photoList.length; i++){
        //     int x = width;
        //     int y = height;

        //     if(photoList[i].tag.CompareTo(/*廊下用画像のtagを入力する*/) == 0){//tagが廊下用画像のtagと一致する時

        //         this.transform.localScale = new Vector3(x,y,0.1);//厚さは0.1で固定
        //         // photoCorrider[corriderCount] = photoList[i];
        //         // corriderCount++;
        //     }
        //     else{//tagが部屋用画像のtagと一致する時
        //         // photoRoom[roomCount] = photoList[i];
        //         // roomCount++;
        //     }
        // }
    }




    

    void Update(){

    }


}
