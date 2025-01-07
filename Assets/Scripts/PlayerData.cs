using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private DataManager DM;

    //Player-Specific Data (Stored Here)
    private Player player;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        DM = GM.GetComponent<DataManager>();
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public void InitLoadPlayer(Player player) {
        this.player = player;
        //Debug.Log("Player: " + player.user.getUserInGameName());
    }

    public Player GetPlayer() {
        return player;
    }

    public PlayerEXP GetPlayerEXPData() {
        return player.playerEXP;
    }
    //##### End of Getters/Setters #####
}
