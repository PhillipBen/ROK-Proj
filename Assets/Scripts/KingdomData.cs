using UnityEngine;
using System.Collections.Generic;

public class KingdomData : MonoBehaviour
{
    //##### Beg of Variables #####
    List<Kingdom> kingdomList = new List<Kingdom>();
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    public void LoadKingdoms(List<Kingdom> kingdomList) {
        this.kingdomList = kingdomList;
        for(int i = 0; i < this.kingdomList.Count; i++) {
            Debug.Log("K: " + kingdomList[i]);
        }
    }

    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public List<Kingdom> GetKingdomList() {
        return kingdomList;
    }
    //##### End of Getters/Setters #####
}
