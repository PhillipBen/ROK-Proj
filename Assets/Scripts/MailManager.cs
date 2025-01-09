using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MailManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    public User user;

    public List<Mail> mailList;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        user = GM.GetComponent<PlayerData>().GetPlayer().user;

        //Testing
        mailList = new List<Mail>() {
            //Year, Month, Day, Hour (mil), Min, Sec
            new Mail(new User(1, "sender"), new User(0, "SonicBlueTyphoon"), "Subject1", new DateTime(2025, 1, 4, 1, 1, 1), "This is the body of the text.", true),
            new Mail(new User(1, "sender"), new User(0, "SonicBlueTyphoon"), "Subject2", new DateTime(2024, 2, 3, 1, 1, 1), "This is the body of the text.", false),
            new Mail(new User(-1, "System"), new User(0, "SonicBlueTyphoon"), "Subject3", new DateTime(2024, 3, 4, 1, 1, 1), "This is the body of the text.", true),
            new Mail(new User(-2, "Your Alliance"), new User(0, "SonicBlueTyphoon"), "Subject4", new DateTime(2024, 4, 5, 1, 1, 1), "This is the body of the text.", false),
            new Mail(new User(0, "SonicBlueTyphoon"), new User(1, "sender"), "Subject5", new DateTime(2024, 1, 5, 6, 1, 1), "This is the body of the text.", false)
        };
    }

    public List<Mail> GetMailByTab(int tabType) {
        if(tabType == 0) {
            return GetAllPersonalMail();
        }else if(tabType == 1) {
            return GetAllReportMail();
        }else if(tabType == 2) {
            return GetAllAllianceMail();
        }else if(tabType == 3) {
            return GetAllSystemMail();
        }else if(tabType == 4) {
            return GetAllSentMail();
        }else if(tabType == 5) {
            return GetAllFavoritesMail();
        }else {
            return new List<Mail>();
        }
    }

    //Tab Sorting
    public List<Mail> GetAllPersonalMail() {
        //Sort mailList by personal. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            if(mailList[i].GetUserReciever().getUserID() == user.getUserID() && mailList[i].GetUserSender().getUserID() != -2 && mailList[i].GetUserSender().getUserID() != -1) {
                returnList.Add(mailList[i]);
            }
        }

        return SortByNewest(returnList);
    }

    public List<Mail> GetAllReportMail() {
        //Sort mailList by report. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            // if(mailList[i].GetBattleData() != null) { //Implement This
            //     returnList.Add(mailList[i]);
            // }
        }

        return SortByNewest(returnList);
    }

    public List<Mail> GetAllAllianceMail() {
        //Sort mailList by alliance. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            if(mailList[i].GetUserSender().getUserID() == -2) { //See userID for -2 = alliance
                returnList.Add(mailList[i]);
            }
        }

        return SortByNewest(returnList);
    }

    public List<Mail> GetAllSystemMail() {
        //Sort mailList by system. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            if(mailList[i].GetUserSender().getUserID() == -1) { //See userID for -1 = alliance
                returnList.Add(mailList[i]);
            }
        }

        return SortByNewest(returnList);
    }

    public List<Mail> GetAllSentMail() {
        //Sort mailList by sent. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            if(mailList[i].GetUserSender().getUserID() == user.getUserID()) {
                returnList.Add(mailList[i]);
            }
        }

        return SortByNewest(returnList);
    }

    public List<Mail> GetAllFavoritesMail() {
        //Sort mailList by favorites. Newest mail is at the top

        var returnList = new List<Mail>();
        for(int i = 0; i < mailList.Count; i++) {
            if(mailList[i].GetFavoriteTF() == true) {
                returnList.Add(mailList[i]);
            }
        }

        return SortByNewest(returnList);
    }

    public void RemoveSelectedMail(Mail mail) {
        mailList.Remove(mail);
    }   

    public List<Mail> SortByNewest(List<Mail> inputList) {
        return inputList.OrderBy(x => x.GetSentDateTime().Ticks).ToList();
    }

    public void SendMail(Mail mail) {
        //GUI will compile mail text into the Mail class then send it here.

        //Send to DB.

        //Add to current list. When you press Sent, it will organize it with this added to it.
        mailList.Add(mail);
    }

    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
