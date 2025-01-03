using UnityEngine;
using System.Collections.Generic;

public class KingdomData : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private DataManager DM;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        DM = GM.GetComponent<DataManager>();
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public List<Kingdom> GetKingdomList() {
        return DM.GetKingdomList();
    }
    //##### End of Getters/Setters #####
}
