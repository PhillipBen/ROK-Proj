using UnityEngine;
using System.Collections.Generic;

public class Kingdom
{
    public int kingdomID;
    public List<Player> kingdomPlayerIDList;

    public Kingdom(int ID, List<Player> kingdomPlayerIDList) {
        this.kingdomID = ID;
        this.kingdomPlayerIDList = kingdomPlayerIDList;
    }
}
