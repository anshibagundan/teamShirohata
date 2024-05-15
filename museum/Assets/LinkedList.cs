using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//画像データを保存する場所

public class LinkedPhoto{
    string title_{get; set;}
    string detailedTitle_{get; set;}
    string time_{get; set;}
    GameObject picture_{get; set;}
    float height_{get; set;}
    float width_{get; set;}
    string tag_{get; set;}
    int photoNum_{get; set;}

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

    //削除する
    public void Delete(LinkedPhoto photo){
        if(first_ == null)
            return;
        if(first_ == photo){
            if (first_ != null)
            {
                first_ = first_.NextPhoto;
                if (first_ != null)
                    first_.PreviousPhoto = null;
            }
        }
        else{
            LinkedPhoto current = first_;
            while(current.NextPhoto != null){
                if(current.NextPhoto == photo){
                    photo.NextPhoto.PreviousPhoto = current;
                    current.NextPhoto = photo.NextPhoto;
                    break;
                }
                else{
                    current = current.NextPhoto;
                }
            }
        }
    }

    //特定の場所に追加する
    public void InsertAfter(LinkedPhoto photo, string title, string detailedTitle, string time, GameObject picture, float height, float width, string tag, int photoNum){
        if(photo == null){
            Append(title, detailedTitle, time, picture, height, width, tag, photoNum);
            return;
        }

        LinkedPhoto current = first_;
        while(current != null){
            if(current == photo){
                LinkedPhoto newPhoto = new LinkedPhoto(title, detailedTitle, time, picture, height, width, tag, photoNum);
                newPhoto.NextPhoto = current.NextPhoto;
                newPhoto.PreviousPhoto = current;
                current.NextPhoto = newPhoto;
                break;
            }
            else{
                current = current.NextPhoto;
            }
        }
    }
}
