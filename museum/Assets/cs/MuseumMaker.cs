using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotoProcessor : MonoBehaviour
{
    public List<string> v = new List<string> { "S", "S", "S", "S", "S", "R", "R", "R", "R1", "R1", "S", "S", "R2" };
        
    public GameObject streetPrefab; 
    public GameObject roomPrefab; 
    private Vector3 startPosition = new Vector3(0, 0, 50); // 開始位置
    private Vector3 positionOffset = new Vector3(0, 0, 50); // 各インスタンスの位置オフセット
    
    //写真関連
    public GameObject exhibitPrefab;  // スペルミス修正
    
    private Vector3 exhibitStart = new Vector3(10, 5, -15); // 開始位置
    private Vector3 exhibitOffset = new Vector3(0, 0, 10); // 各インスタンスの位置オフセット
    private Vector3 leftsidePosition = new Vector3(-10, 5, -12);
    private Vector3 leftsideRotation = new Vector3(0, 180, 0);

    void Start()
    {
        int streetNum = 0;
        int exhibitNum = 0;
        string roomName;

        for (int i = 0; i < v.Count;)
        {
            if (v[i] == "S")
            {
                //通路
                Vector3 position = startPosition + i * positionOffset;
                GameObject parentInstance = Instantiate(streetPrefab, position, Quaternion.identity);
                //写真
                Instantiate(exhibitPrefab, exhibitStart, Quaternion.identity, parentInstance.transform);
                while (i < v.Count && v[i] == "S")
                {
                    i++;
                    exhibitNum++;
                    //写真
                    Vector3 exhibitPosition = exhibitStart + exhibitNum * exhibitOffset;
                    Instantiate(exhibitPrefab, exhibitPosition, Quaternion.identity, parentInstance.transform);
                    
                    if (exhibitNum > 3)
                    {
                        exhibitNum = 0;
                        break;
                    }
                }
                exhibitNum = 0;
            }
            else
            {
                i++;
            }
        }
    }
}