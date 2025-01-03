using UnityEngine;

public class User
{
    //##### Beg of Variables #####
    private int userID;
    private string userInGameName;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    public User(int userID, string userInGameName) {
        this.userID = userID;
        this.userInGameName = userInGameName;
    }
    //##### End of Main Functions #####
    

    //##### Beg of Getters/Setters #####
    public void setUserID(int userID) {
        this.userID = userID;
    }
    public int getUserID() {
        return this.userID;
    }
    public void setUserInGameName(string userInGameName) {
        this.userInGameName = userInGameName;
    }
    public string getUserInGameName() {
        return this.userInGameName;
    }
    //##### End of Getters/Setters #####
}
