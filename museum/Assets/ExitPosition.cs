using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPosition : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(0, 0, 50); // 各インスタンスの位置オフセット
    public StreetMaker streetMaker;  // StreetMakerの参照を保持する変数

    public int streetNum = 5;
    void Start()
    {
        //int streetNum = streetMaker.StreetNum;
        transform.position += positionOffset*streetNum;
    }
}