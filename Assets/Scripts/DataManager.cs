using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomData KD;
    private PlayerData PD;

    private List<Kingdom> kingdomList = new List<Kingdom>();
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Awake() {
        KD = GM.GetComponent<KingdomData>();
        PD = GM.GetComponent<PlayerData>();

        LoadAllData();
    }
    //##### End of Main Functions #####


    //##### Beg of Data Loaders #####
    private void LoadAllData() {
        LoadMainPlayer();
        LoadKingdoms();
    }

    private void LoadMainPlayer() {
        User user = new User(0, "SonicBlueTyphoon");
        PlayerEXP playerEXP = new PlayerEXP(2, 3);
        Player player = new Player(user, playerEXP);
        PD.InitLoadPlayer(player);
        player.LoadResourecs(12345000000, 9870000000000, 3456700000000000);
    }

    private void LoadKingdoms() {

        //Holds the full list of all kingdoms.
        User user = new User(1, "Test Man");
        PlayerEXP playerEXP = new PlayerEXP(1, 0);
        Player player = new Player(user, playerEXP);
        Kingdom kingdom = new Kingdom(0, new List<Player>() {player, player});
        kingdomList = new List<Kingdom>() {kingdom};
    }
    //##### End of Data Loaders #####


    //##### Beg of Getters/Setters #####
    public List<Kingdom> GetKingdomList() {
        return kingdomList;
    }
    //##### End of Getters/Setters #####
}
