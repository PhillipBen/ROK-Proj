using UnityEngine;

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
        //List<Kingdom> kingdomIDList = new List<Kingdom>() {};
        //KD.LoadKingdoms();
    }
    //##### End of Data Loaders #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
