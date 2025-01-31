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

        SetInitResourcesAmount();
        UpdatePlayerResorcesVariables();
    }

    public void SetInitResourcesAmount() {
        var yourStartingResources = new List<int>() {100000, 100000, 100000}; //Wood, Stone, Gems

        Resources res = PD.GetPlayer().playerResources;
        res.AddWood(Convert.ToInt64(yourStartingResources[0]));
        res.AddStone(Convert.ToInt64(yourStartingResources[1]));
        res.AddGems(Convert.ToInt64(yourStartingResources[2]));

        woodAmount = yourStartingResources[0];
        stoneAmount = yourStartingResources[1];
        gemsAmount = yourStartingResources[2];
    }

    public void UpdatePlayerResorcesVariables() {
        Resources res = PD.GetPlayer().playerResources;
        woodAmount = res.GetWood();
        stoneAmount = res.GetStone();
        gemsAmount = res.GetStone();
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
        res.AddWood(Convert.ToInt64(addedResourcesList[0]));
        res.AddStone(Convert.ToInt64(addedResourcesList[1]));
        res.AddGems(Convert.ToInt64(addedResourcesList[2]));

        UIM.UpdateResourcesGUI();
        UpdatePlayerResorcesVariables();
    }
}
