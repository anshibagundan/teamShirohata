using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//画像データを保存する場所

public class LinkedPhoto{
    public string title_{get; set;}
    public string detailedTitle_{get; set;}
    public string time_{get; set;}
    public GameObject picture_{get; set;}
    public float height_{get; set;}
    public float width_{get; set;}
    public string tag_{get; set;}
    public int photoNum_{get; set;}

    public LinkedPhoto PreviousPhoto {get; set;}
    public LinkedPhoto NextPhoto {get; set;}
    

    public LinkedPhoto(string title, string detailedTitle, string time, GameObject picture, float height, float width, string tag, int photoNum){
        title_ = title;
        detailedTitle_ = detailedTitle;
        time_ = time;
        picture_ = picture;
        height_ = height;
        width_ = width;
        tag_ = tag;
        photoNum_ = photoNum;

        PreviousPhoto = null;
        NextPhoto = null;
    }

    //pictureの配置
    public void SetUp(Vector3 position, Quaternion rot){
        picture_.transform.position = position;
        picture_.transform.rotation = rot;
        picture_.SetActive(true);//表示可能にする
    }
}

public class LinkedList{
    private LinkedPhoto first_;
    private LinkedPhoto last_;

    public LinkedPhoto Head {get{return first_;}}
    public LinkedPhoto Tail {get{return last_;}}

    public LinkedList(){
        first_ = null;
        last_ = null;
    }

    //最初のLinkedPhotoを引き渡す
    public LinkedPhoto First(){
        return first_;
    }
    //追加する
    public void Append(string title, string detailedTitle, string time, GameObject picture, float height, float width, string tag, int photoNum){
        LinkedPhoto photo = new LinkedPhoto(title, detailedTitle, time, picture, height, width, tag, photoNum);
        if(first_ == null){
            first_ = photo;
            last_ = photo;
        }   
        else{
            photo.PreviousPhoto = last_;
            last_.NextPhoto = photo;
            last_ = photo;
        }
    }

    public void Insert(LinkedPhoto photo){
        if(first_ == null){
            first_ = photo;
            last_ = photo;
        }   
        else{
            photo.PreviousPhoto = last_;
            last_.NextPhoto = photo;
            last_ = photo;
        }
    }

    public void Delete(LinkedPhoto photo)
    {
        if (first_ == null)
            return;

        if (first_ == photo)
        {
            LinkedPhoto temp = first_;
            if (first_ != null)
            {
                first_ = first_.NextPhoto;
                if (first_ != null){
                    first_.PreviousPhoto = null;
                }
                else{
                    last_ = null;
                }
                   
            }
        }
        else
        {
            LinkedPhoto current = first_;
            while (current.NextPhoto != null)
            {
                if (current.NextPhoto == photo)
                {
                    if(photo.NextPhoto == null){
                        current.NextPhoto = null;
                        last_ = current;
                        break;
                    }

                    photo.NextPhoto.PreviousPhoto = current;
                    current.NextPhoto = photo.NextPhoto;
                    break;
                }
                else
                {
                    current = current.NextPhoto;
                }
            }
        }
    }

   

    public void InsertAfter(LinkedPhoto prevPhoto, LinkedPhoto photo){
        if(prevPhoto == null){
            return;
        }
        LinkedPhoto current = first_;
        while (current != null)
        {
            if (current == prevPhoto)
            {
                photo.NextPhoto = current.NextPhoto;
                photo.PreviousPhoto = current;
                current.NextPhoto = photo;
                if(last_ == prevPhoto){
                    last_ = photo;
                }
                break;
            }
            else
            {
                current = current.NextPhoto;
            }
        }
        // if(current == null){
        //     Insert(photo);
        // }
        

    }

    public int Size(){
        int count = 0;

        LinkedPhoto current = first_;
        while(current != null){
            count++;
            current = current.NextPhoto;
        }
        return count;
    }

    //画像を入れ替える
    public void Change(LinkedPhoto photo1, LinkedPhoto photo2){
       if (photo1 == photo2) {
            return;
        }

       // photo1の前後の写真を保持
        LinkedPhoto photo1Prev = photo1.PreviousPhoto;
        LinkedPhoto photo1Next = photo1.NextPhoto;

        // photo2の前後の写真を保持
        LinkedPhoto photo2Prev = photo2.PreviousPhoto;
        LinkedPhoto photo2Next = photo2.NextPhoto;

        // photo1の前後の写真のポインタをphoto2につなぎ替える
        if (photo1Prev != null) {
            photo1Prev.NextPhoto = photo2;
        }
        if (photo1Next != null) {
            photo1Next.PreviousPhoto = photo2;
        }

        // photo2の前後の写真のポインタをphoto1につなぎ替える
        if (photo2Prev != null) {
            photo2Prev.NextPhoto = photo1;
        }
        if (photo2Next != null) {
            photo2Next.PreviousPhoto = photo1;
        }

        // photo1とphoto2の前後のポインタを入れ替える
        photo1.PreviousPhoto = photo2Prev;
        photo1.NextPhoto = photo2Next;
        photo2.PreviousPhoto = photo1Prev;
        photo2.NextPhoto = photo1Next;

        // リストの先頭と末尾の更新
        if (first_ == photo1) {
            first_ = photo2;
        } else if (first_ == photo2) {
            first_ = photo1;
        }

        if (last_ == photo1) {
            last_ = photo2;
        } else if (last_ == photo2) {
            last_ = photo1;
        }

    }

   
   
    //photoNumを基準に整列する
    public void Sort(){
        if (first_ == null) return;

        bool swapped;
        do
        {
            swapped = false;
            LinkedPhoto current = first_;

            while (current != null && current.NextPhoto != null)
            {
                if (current.photoNum_ > current.NextPhoto.photoNum_)
                {
                    Change(current, current.NextPhoto);
                    swapped = true;
                }
                current = current.NextPhoto;
            }
        } while (swapped);

        Debug.Log(first_.NextPhoto.NextPhoto == null);
        
    }

}
