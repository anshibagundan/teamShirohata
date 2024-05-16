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

    public int Size(){
        int count = 1;

        LinkedPhoto current = first_;
        while(current != null){
            count++;
            current = current.NextPhoto;
        }
        return count;
    }

    //画像を入れ替える
    public void Change(LinkedPhoto photo1, LinkedPhoto photo2){
        LinkedPhoto temp = photo1.NextPhoto;
        photo1.NextPhoto = photo2.NextPhoto;
        photo2.NextPhoto = temp;

        temp = photo1.PreviousPhoto;
        photo1.PreviousPhoto = photo2.PreviousPhoto;
        photo2.PreviousPhoto = photo1.PreviousPhoto;
    }

    public void Sort(){
        if(first_ == null){
            return;
        }

        LinkedPhoto current = first_;
        while(current.NextPhoto != null){
            LinkedPhoto comparison = current.NextPhoto;
            LinkedPhoto min = current;

            while(comparison != null){
                if(min.photoNum_ > comparison.photoNum_){
                    min = comparison;
                }
                comparison = comparison.NextPhoto;
            }
            Change(current, min);
            
            current = current.NextPhoto;
        }
    }

    public string[] SorR(){
        LinkedPhoto current = first_;
        string[] sORr = new string[this.Size()];
        int num = 0;
        int roomCount = 0;

        while(current != null){
            if(current.tag_ == "corridor"){
                sORr[num] = "S";
            }
            else{
                if(sORr[num-1] == "S" || num == 0){
                    roomCount++;  
                }
                sORr[num] = "R" + roomCount;

            }
            num++;
            current = current.NextPhoto;
        }
        return sORr;
    }
}
