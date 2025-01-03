using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomData KD;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KD = GM.GetComponent<KingdomData>();

        LoadAllData();
    }
    //##### End of Main Functions #####


    //##### Beg of Data Loaders #####
    private void LoadAllData() {
        LoadKingdoms();
    }

    private void LoadKingdoms() {

        //Holds the full list of all kingdoms.
        User user = new User(0, "Test Man");
        Player player = new Player(user);
        Kingdom kingdom = new Kingdom(0, new List<Player>() {player, player});
        List<Kingdom> kingdomList = new List<Kingdom>() {kingdom};

        KD.LoadKingdoms(kingdomList);
    }
    //##### End of Data Loaders #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
