using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerResources : MonoBehaviour
{
    public GameManager GM;
    private PlayerData PD;
    private PlayerMap PM;
    private TimeManager TM;
    private UIManager UIM;

    public long woodAmount;
    public long stoneAmount;
    public long gemsAmount;

    void Start()
    {
        PD = GM.GetComponent<PlayerData>();
        PM = GM.GetComponent<PlayerMap>();
        TM = GM.GetComponent<TimeManager>();
        UIM = GM.GetComponent<UIManager>();

        UpdatePlayerResorcesVariables();
    }

    public void UpdatePlayerResorcesVariables() {
        Resources res = PD.GetPlayer().playerResources;
        woodAmount = res.woodAmount;
        stoneAmount = res.stoneAmount;
        gemsAmount = res.gemsAmount;
    }

    public void IncPlayerResources() {
        //Called Every Sim Update

        Resources res = PD.GetPlayer().playerResources;
        float multi = TM.lastSimTimeMulti();
        var addedResourcesList = PM.GetBuildingResourceValues(); //This corresponds to 1 second worth of resources.
        
        for(int i = 0; i < addedResourcesList.Count; i++) {
            addedResourcesList[i] = addedResourcesList[i] * multi;
        }

        //Add Resources: 0 = wood, 1 = stone, 2 = gems
        res.woodAmount += Convert.ToInt64(addedResourcesList[0]);
        res.stoneAmount += Convert.ToInt64(addedResourcesList[1]);
        res.gemsAmount += Convert.ToInt64(addedResourcesList[2]);

        UIM.UpdateResourcesGUI();
        UpdatePlayerResorcesVariables();
    }
}
