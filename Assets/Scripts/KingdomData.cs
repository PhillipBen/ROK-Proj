using UnityEngine;
using System.Collections.Generic;

public class KingdomData : MonoBehaviour
{
    //##### Beg of Variables #####

    //Holds the full list of all kingdoms.
    private List<int> kingdomIDList = new List<int>() {0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0010, 0011, 0012, 0013, 0014, 0015};
    //Holds the full 2D array list of all player IDs in a kingdom. Kingdom 0 on list kingdomIDList is the same as kingdom 0 on kingdomPlayerIDList.
    private int[,] kingdomPlayerIDList = new int[3, 5] {
        {1, 2, 3, 4, 5},
        {6, 7, 8, 9, 10},
        {11, 12, 13, 14, 15}
    };
    //##### End of Variables #####


    //##### Beg of Classes #####
    public class Kingdom {
        public int kingdomID;
        public List<int> kingdomPlayerIDList;
    }

    public class Player {
        public User user;
        //Resources and stuff.
    }
    //##### End of Classes #####


    //##### Beg of Main Functions #####
    public void LoadKingdoms(List<Kingdom> kingdomIDList) {

    }

    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public List<int> GetKingdomIDList() {
        return kingdomIDList;
    }
    //##### End of Getters/Setters #####
}
