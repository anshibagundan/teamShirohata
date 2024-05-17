using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPosition : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(0, 0, 50); // 各インスタンスの位置オフセット

    //public int streetNum = 5;
    void Start()
    {
        // 3秒後にStartMuseumメソッドを呼び出す
        Invoke("MoveExit", 1f);
    }

    void MoveExit()
    {
        int streetNum = MuseumMaker.streetNum;
        //int streetNum = MakeMyMuseum.streetNum;
        
        transform.position += positionOffset*streetNum;
    }
}