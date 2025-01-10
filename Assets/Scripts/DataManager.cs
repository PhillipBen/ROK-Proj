using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomData KD;
    private PlayerData PD;

    private List<Kingdom> kingdomList = new List<Kingdom>();

    public Sprite defaultIcon;
    public List<Sprite> playerIcon;
    public List<Player> playerList;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Awake() {
        KD = GM.GetComponent<KingdomData>();
        PD = GM.GetComponent<PlayerData>();

        LoadAllData();
    }

    void Start() {
        playerList = new List<Player>() {
            new Player(new User(-1, "System"), null, "SYST", playerIcon[1]), 
            new Player(new User(-2, "Your Alliance"), null, "", playerIcon[2]), 
            new Player(new User(0, "SonicBlueTyphoon"), null, "SONC", playerIcon[0]), 
            new Player(new User(1, "sender"), null, "ALL1", defaultIcon) 
        };
    }

    public Player GetPlayerFromGloablList(int userID) {
        for(int i = 0; i < playerList.Count; i++) {
            if(playerList[i].user.getUserID() == userID)
                return playerList[i];
        }
        return null;
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
        Player player = new Player(user, playerEXP, null, null);
        PD.InitLoadPlayer(player);
        player.playerResources.LoadResources(0, 0, 0);//12345000000, 9870000000000, 3456700000000000);
    }

    private void LoadKingdoms() {

        //Holds the full list of all kingdoms.
        User user = new User(1, "Test Man");
        PlayerEXP playerEXP = new PlayerEXP(1, 0);
        Player player = new Player(user, playerEXP, null, null);
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
