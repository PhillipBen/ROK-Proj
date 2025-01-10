using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private ArmyManager AM;
    private PlayerMap PM;


    //Array Outer #: Wood, Stone, Time, EXP, Power Multi
    public float[,] buildingsByResourcePercent = new float[,] {
        {0.0025f, 0.005f, 0.33f, 0.05f, 0.075f, 0.075f, 0.06f, 0.125f, 0.02f, 0.07f, 0.04f, 0.03f, 0.075f, 0.07f}, //Wood
        {0.005f, 0.0025f, 0.33f, 0.075f, 0.05f, 0.05f, 0.085f, 0.2f, 0.05f, 0.035f, 0.01f, 0.04f, 0.1f, 0.05f}, //Stone
        {0.01f, 0.01f, 0.3f, 0.075f, 0.075f, 0.1f, 0.025f, 0.125f, 0.05f, 0.025f, 0.025f, 0.04f, 0.1f, 0.05f}, //Time
        {1f, 1f, 10f, 4f, 3f, 5f, 5f, 8f, 2f, 2f, 2f, 2f, 5f, 4f}, //EXP
        {1f, 1f, 10f, 4f, 3f, 5f, 5f, 8f, 2f, 2f, 2f, 2f, 5f, 4f}, //Power
    };
    public List<string> buildingNameList = new List<string>() {"Lumberyard", "Quarry", "Townhall", "Barracks", "Hospital", "Laboratory", "Blacksmith", "Wall", "Watchtower", "Trading Post", "Resource Silo", "Radar", "Garrison", "Alliance Center"};
    public List<string> buildingDetailList = new List<string>() {"Level", "TH Level Req", "Prod. Per Sec.", "Wood Cost", "Stone Cost", "Build Time", "Exp Gain", "Power"};
    public int univPowerMulti; //60
    public List<int> marchSizeList = new List<int>() {2000, 3000, 4000, 5000, 7000, 9000, 12000, 15000, 19000, 23000, 28000, 33000, 38000, 44000, 50000, 57000, 64000, 72000, 80000, 90000, 100000, 110000, 120000, 130000, 150000};

    public List<int> maxNumBuildingsList = new List<int>() {4, 8, 12, 14, 16, 17, 18, 18, 18, 20, 20, 20, 20, 21, 21, 21, 22, 22, 22, 23, 23, 23, 23, 23, 25};
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        AM = GM.GetComponent<ArmyManager>();
        PM = GM.GetComponent<PlayerMap>();

        univPowerMulti = 60;

        VariableInit();
    }

    private void VariableInit() {
        for(int i = 0; i < PM.buildingTileList.Count; i++) {
            var tile = PM.buildingTileList[i].GetComponent<BuildingSlot>();
            if(tile.buildingType == 2) { //City Hall
                AM.SetMarchSize(tile.GetMarchSize());
                PM.maxNumBuildings = tile.GetMaxNumBuildings();
                PM.civAge = tile.GetCivAge();
                break; //Only one of these buildings.
            }
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
