using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Imgup : MonoBehaviour
{
    public string imageUrl = "https://th.bing.com/th/id/OIP.SPCOU0xr5I7bwfk1pn6R2AHaHa?rs=1&pid=ImgDetMain";
    public Renderer imageRenderer; // �摜��\�����邽�߂�Renderer

    void Start()
    {
        StartCoroutine(LoadImage(imageUrl));
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            // �e�N�X�`���������_���[�ɐݒ�
            imageRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.Log("�摜�̓ǂݍ��݂Ɏ��s���܂���: " + www.error);
        }
    }
}
