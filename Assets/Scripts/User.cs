using UnityEngine;

public class User : MonoBehaviour
{
    //##### Beg of Variables #####
    private int userID;
    private string userInGameName;
    //##### End of Variables #####
    

    //##### Beg of Getters/Setters #####
    protected void setUserID(int userID) {
        this.userID = userID;
    }
    protected int getUserID() {
        return this.userID;
    }
    protected void setUserInGameName(string userInGameName) {
        this.userInGameName = userInGameName;
    }
    protected string getUserInGameName() {
        return this.userInGameName;
    }
    //##### End of Getters/Setters #####
}
