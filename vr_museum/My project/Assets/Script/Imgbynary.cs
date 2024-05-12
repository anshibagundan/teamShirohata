using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Imgbynary : MonoBehaviour
{
    public string apiUrl = "https://example.com/api/image";
    public Renderer imageRenderer; // �摜��\�����邽�߂�Renderer

    void Start()
    {
        StartCoroutine(LoadImageFromAPI(apiUrl));
    }

    IEnumerator LoadImageFromAPI(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // �o�C�i���[�f�[�^�̎擾
            byte[] imageData = www.downloadHandler.data;

            // �o�C�i���[�f�[�^���e�N�X�`���ɕϊ�
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // �e�N�X�`���������_���[�ɐݒ�
            imageRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.Log("API����̃o�C�i���[�f�[�^�̎擾�Ɏ��s���܂���: " + www.error);
        }
    }
}
