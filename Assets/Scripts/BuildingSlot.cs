using UnityEngine;
using System.Collections.Generic;

public class BuildingSlot : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private UIManager UIM;
    private PlayerData PD;
    private PlayerMap PM;
    public int cellSize;
    public int buildingGUIType; //0 = Regular Building, 1 = Wall, 2 = Non-buildable Building.
    public int buildingType; //0 = Lumberyard, 1 = Quarry, Townhall, Barracks, Hospital, Laboratory, Blacksmith, Wall, Watchtower, Trading Post, Resource Silo, Radar, Garrison, Alliance Center.
    public bool buildingBuiltTF;

    //Generic Variables
    public int level;
    //Buildings: Lumberyard, Quarry, Townhall, Barracks, Hospital, Laboratory, Blacksmith, Wall, Watchtower, Trading Post, Resource Silo, Radar, Garrison, Alliance Center.
    //Array Outer #: Wood, Stone, Time, EXP, Power Multi
    private float[,] buildingsByResourcePercent = new float[,] {
        {0.0025f, 0.005f, 0.33f, 0.05f, 0.075f, 0.075f, 0.06f, 0.125f, 0.02f, 0.07f, 0.04f, 0.03f, 0.075f, 0.07f}, //Wood
        {0.005f, 0.0025f, 0.33f, 0.075f, 0.05f, 0.05f, 0.085f, 0.2f, 0.05f, 0.035f, 0.01f, 0.04f, 0.1f, 0.05f}, //Stone
        {0.01f, 0.01f, 0.3f, 0.075f, 0.075f, 0.1f, 0.025f, 0.125f, 0.05f, 0.025f, 0.025f, 0.04f, 0.1f, 0.05f}, //Time
        {1f, 1f, 10f, 4f, 3f, 5f, 5f, 8f, 2f, 2f, 2f, 2f, 5f, 4f}, //EXP
        {1f, 1f, 10f, 4f, 3f, 5f, 5f, 8f, 2f, 2f, 2f, 2f, 5f, 4f}, //Power
    };

    public List<string> buildingNameList = new List<string>() {"Lumberyard", "Quarry", "Townhall", "Barracks", "Hospital", "Laboratory", "Blacksmith", "Wall", "Watchtower", "Trading Post", "Resource Silo", "Radar", "Garrison", "Alliance Center"};
    public List<string> buildingDetailList = new List<string>() {"Level", "TH Level Req", "Prod. Per Sec.", "Wood Cost", "Stone Cost", "Build Time", "Exp Gain", "Power"};

    private int univPowerMulti; //60

    public bool upgradeInProgress;
    private long remainingTimeSeconds;

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        UIM = GM.GetComponent<UIManager>();
        PD = GM.GetComponent<PlayerData>();
        PM = GM.GetComponent<PlayerMap>();

        cellSize /= 100;
        univPowerMulti = 60;

        SetBuildingImage();
    }

    private void SetBuildingImage() {
        if(buildingType != -1) {
            if(UIM.buildingSpriteList[buildingType] != null && buildingType != 7) {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = UIM.buildingSpriteList[buildingType];
            }
            else if(buildingType != 7) {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
            }
        }else {
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
        }  
    }

    private void TurnOnBuildingIcon() {
        this.transform.GetChild(2).gameObject.SetActive(true);
    }


    private void TurnOffBuildingIcon() {
        this.transform.GetChild(2).gameObject.SetActive(false);
    }
    public string buildingDetailsListGet(int ID, int level = 0) {
        if(ID == 0) {
            return level.ToString();
        }else if (ID == 1) {
            return GetTHLevelReq(level).ToString();
        }else if (ID == 2) {
            return (System.Math.Round(GetProdPerSec(level), 2) / 1).ToString();
        }else if (ID == 3) {
            return GetResourceCost(0, level).ToString();
        }else if (ID == 4) {
            return GetResourceCost(1, level).ToString();
        }else if (ID == 5) {
            return GetResourceCost(2, level).ToString();
        }else if (ID == 6) {
            return GetResourceCost(3, level).ToString();
        }else if (ID == 7) {
            return GetResourceCost(4, level).ToString();
        }
        return null;
    }

    public bool IsTileClicked(Vector3 clickPos) {
        var selectedTF = false;
        if(buildingGUIType == 0) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            if(pos.x - (cellSize / 2f) <= coords.x && pos.x + (cellSize / 2f) >= coords.x && pos.y - (cellSize / 2f) <= coords.y && pos.y + (cellSize / 2f) >= coords.y) {
                //Selected
                selectedTF = true;
            }
        }else if (buildingGUIType == 1) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            Vector2 scale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
            //0.2f is base (100 cell match).
            if((pos.x - (cellSize * (scale.x / 0.2f) / 2f)) <= coords.x && (pos.x + (cellSize * (scale.x / 0.2f) / 2f)) >= coords.x && (pos.y - (cellSize * (scale.y / 0.2f) / 2f)) <= coords.y && (pos.y + (cellSize * (scale.y / 0.2f) / 2f)) >= coords.y) {
                //Inside Outer Bounds Box
                var innerDist = 1.7;
                if((pos.x - (cellSize * (scale.x / 0.2f) / 2f) + innerDist) >= coords.x || (pos.x + (cellSize * (scale.x / 0.2f) / 2f) - innerDist) <= coords.x || (pos.y - (cellSize * (scale.y / 0.2f) / 2f) + innerDist) >= coords.y || (pos.y + (cellSize * (scale.y / 0.2f) / 2f) - innerDist) <= coords.y) {
                    //Inside Inner Bounds Box
                    selectedTF = true;
                }
            }
        }

        if(selectedTF) { 
            if(buildingBuiltTF) {
                //If building is built, then the UI should be for info, upgrading, and custom functions
                Vector2 thisPos = this.transform.position;
                Vector2 viewportPoint = Camera.main.WorldToViewportPoint(thisPos);
                viewportPoint = new Vector2(viewportPoint.x * Screen.width, (viewportPoint.y * Screen.height) - 180);
                UIM.TurnOnBuildingOptionsObjSelected(viewportPoint, buildingType);
            }else {
                //If the building is not built, the UI should be for building that building initially.
                UIM.BuildingBuildButtonPressed();
            }
            return true; //Return true or false based on if the tile was clicked.
        }else {
            return false;
        }
    }

    public void StartBuildingInProgress() {
        upgradeInProgress = true; 
        TurnOnBuildingIcon();
        PM.numberOfBuildersAvailable -= 1;
    }

    public void BuildingInProgress(int simTimePassed) {
        remainingTimeSeconds -= simTimePassed;
        if(remainingTimeSeconds <= 0 && upgradeInProgress) {
            upgradeInProgress = false;
            level += 1;
            TurnOffBuildingIcon();
            PM.numberOfBuildersAvailable += 1;
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####

    //Generic
    public int GetTHLevelReq(int levelInc = 0) {
        if(buildingType == 0 || buildingType == 1) {
            return level + levelInc - 1;
        }else {
            Debug.Log("Error: Building Type not accounted for.");
            return -1;
        }
    }

    public int GetResourceCost(int resourceType, int levelInc = 0, int impBuildingType = -1) {

        var buildingTypeCheck = buildingType;
        if(impBuildingType != -1)
            buildingTypeCheck = impBuildingType;

        if(buildingTypeCheck == 0) {
            //Lumberyard
            return (int)Mathf.Round(GetEstimatedResourcesCost(resourceType, levelInc) * buildingsByResourcePercent[resourceType,0]);
        }else if(buildingTypeCheck == 1) {
            //Quarry
            return (int)Mathf.Round(GetEstimatedResourcesCost(resourceType, levelInc) * buildingsByResourcePercent[resourceType,1]);
        }else {
            Debug.Log("Error: Building Type not accounted for.");
            return -1;
        }
    }

    public float GetEstimatedResourcesCost(int resourceType, int levelInc = 0) {
        //0 is wood, 1 is stone, 2 is time, 3 is exp, 4 is power, 5 is gems
        var YL = level + levelInc;
        if(resourceType == 0 || resourceType == 1) {
            return (int)Mathf.Round((Mathf.Pow((YL * 10000), 0.9f) + Mathf.Pow(((YL - 1) * 100000), 0.9f) + Mathf.Pow(YL, 6)) / 100) * 100;
        }else if(resourceType == 2) {
            return (int)Mathf.Round((Mathf.Pow((YL * 10000), 0.85f) + Mathf.Pow(((YL - 1) * 100000), 0.6f) + Mathf.Pow(YL, 5)) / 100) * 100;
        }else if(resourceType == 3) {
            return Mathf.Pow(GetTHLevelReq(levelInc), 2.5f);
        }else if(resourceType == 4) {
            return Mathf.Pow(GetTHLevelReq(levelInc), 2.5f) * univPowerMulti;
        }else {
            Debug.Log("Error: Building Type not accounted for.");
            return -1;
        }
    }

    public float convertTimeToGems(int sec) {
        //Decide this
        return Mathf.Max(sec / 30, 5); //See Bundles for how I got 30
    }

    public bool UpgradeBuilding(bool gemsSpentTF) {
        var pla = PD.GetPlayer().playerResources;
        if(gemsSpentTF) {
            var gemCost = convertTimeToGems(GetResourceCost(2));
            if(pla.woodAmount >= GetResourceCost(0) && pla.stoneAmount >= GetResourceCost(1) && pla.gemsAmount >= gemCost && PM.numberOfBuildersAvailable >= 1) {
                pla.woodAmount -= GetResourceCost(0);
                pla.stoneAmount -= GetResourceCost(1);
                pla.gemsAmount -= (long)gemCost;
                remainingTimeSeconds = 0; //Upgrades after '0' seconds. Do this so all upgrade rewards in one section.
                StartBuildingInProgress();
                return true; //Transaction Suceeded.
            }else {
                return false;
            }
        }else {
            if(pla.woodAmount >= GetResourceCost(0) && pla.stoneAmount >= GetResourceCost(1) && PM.numberOfBuildersAvailable >= 1) {
                pla.woodAmount -= GetResourceCost(0);
                pla.stoneAmount -= GetResourceCost(1);
                remainingTimeSeconds = GetResourceCost(2);
                StartBuildingInProgress();
                return true;
            }else {
                return false;
            }
        }
    }

    public bool BuildBuilding() { //Build at level 0
        var pla = PD.GetPlayer().playerResources;
        var tempLevelOffset = 1;
        if(pla.woodAmount >= GetResourceCost(0, tempLevelOffset) && pla.stoneAmount >= GetResourceCost(1, tempLevelOffset) && PM.numberOfBuildersAvailable >= 1) {
            pla.woodAmount -= GetResourceCost(0, tempLevelOffset);
            pla.stoneAmount -= GetResourceCost(1, tempLevelOffset);
            remainingTimeSeconds = GetResourceCost(2, tempLevelOffset);
            StartBuildingInProgress();//Building the first level still takes time
            buildingBuiltTF = true; //Not 'finished' building, but a building is selected there now.
            return true;
        }else {
            return false;
        }
    }

    public bool ResourceCostCheck(int resourceID, int levelInc = 0, int impBuildingType = 0) {
        var pla = PD.GetPlayer().playerResources;
        if(resourceID == 0) {
            if(pla.woodAmount >= GetResourceCost(0, levelInc, impBuildingType)) {
                return true;
            }else {
                return false;
            }
        }else if (resourceID == 1) {
            if(pla.stoneAmount >= GetResourceCost(1, levelInc, impBuildingType)) {
                return true;
            }else {
                return false;
            }
        }else if (resourceID == 5) { //Gems
            if(pla.stoneAmount >=  convertTimeToGems(GetResourceCost(2, levelInc, impBuildingType))) {
                return true;
            }else {
                return false;
            }
        }
        return false;
    }

    //Specific
    public float GetProdPerSec(int levelInc = 0) {
        if(!upgradeInProgress) 
            return Mathf.Pow((((float)level + levelInc) / 2), 1.05f);
        else
            return 0f;
    }

    public void SetPD(PlayerData PD) {
        //Used in the case of Manually Creating BuildingSlot just for temporary use
        this.PD = PD;
    }
    //##### End of Getters/Setters #####
}
