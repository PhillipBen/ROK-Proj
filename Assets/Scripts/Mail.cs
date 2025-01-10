using UnityEngine;
using System;

public class Mail
{
    //##### Beg of Variables #####
    private User sender; //Who the mail was sent to. If it is you, you are the reciever. If it is not you, you are the sender.
    private User reciever; //Opposite rule of sender.
    //private Sprite userIcon; //sender's user icon, GET from DB of players instead. A lot of storage to save an icon anyways.
    private string subject;
    private DateTime sentDateTime;
    private string body;
    //private BattleData battleData;
    //private Inventory recievedItems;
    private bool favoriteTF; //True means favorited

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    public Mail(User sender, User reciever, string subject, DateTime sentDateTime, string body, bool favoriteTF) {
        this.sender = sender;
        this.reciever = reciever;
        this.subject = subject;
        this.sentDateTime = sentDateTime;
        this.body = body;
        this.favoriteTF = favoriteTF;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public User GetUserSender() {
        return sender;
    }
    public User GetUserReciever() {
        return reciever;
    }
    public string GetSubject() {
        return subject;
    }
    public DateTime GetSentDateTime() {
        return sentDateTime;
    }
    public string GetBody() {
        return body;
    }
    // public BattleData GetBattleData() {
    //     return battleData;
    // }
    // public Inventory GetInventory() {
    //     return inventory;
    // }
    public bool GetFavoriteTF() {
        return favoriteTF;
    }
    //##### End of Getters/Setters #####
}
