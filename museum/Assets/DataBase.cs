using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//画像データを保存する場所
//photoNumを追加intで

public class LinkedPhoto{
    int id_{get; set;}//いるかどうかは不明
    string title_{get; set;}
    string user_{get; set;}//いるかどうかは不明
    DateTime time_{get; set;}
    Uri url_{get; set;}
    double height_{get; set;}
    double width_{get; set;}

    public LinkedPhoto PreviousPhoto {get; set;}
    public LinkedPhoto NextPhoto {get; set;}
    string tag_{get; set;}

    public LinkedPhoto(int id, string title, string user, DateTime time, Uri url, double height, double width, string tag){
        id_ = id;
        title_ = title;
        user_ = user;
        time_ = time;
        url_ = url;
        height_ = height;
        width_ = width;
        tag_ = tag;

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
    public void Append(int id, string title, string user, DateTime time, Uri url, double height, double width, string tag){
        LinkedPhoto photo = new LinkedPhoto(id, title, user, time, url, height, width, tag);
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
    public void InsertAfter(LinkedPhoto photo, int id, string title, string user, DateTime time, Uri url, double height, double width, string tag){
        if(photo == null){
            Append(id, title, user, time, url, height, width, tag);
            return;
        }

        LinkedPhoto current = first_;
        while(current != null){
            if(current == photo){
                LinkedPhoto newPhoto = new LinkedPhoto(id, title, user, time, url, height, width, tag);
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

    public int TotalPhoto(){
        int count = 1;
        LinkedPhoto current = first_;
        while(current.NextPhoto != null){
            count++;
            current = current.NextPhoto;
        }

        return count;
    }
}
