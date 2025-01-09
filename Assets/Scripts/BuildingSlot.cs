using UnityEngine;
using System.Collections.Generic;

public class BuildingSlot : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private UIManager UIM;
    private PlayerData PD;
    private PlayerMap PM;
    private ArmyManager AM;
    private BuildingManager BM;
    public int cellSize;
    public int buildingGUIType; //0 = Regular Building, 1 = Wall, 2 = Non-buildable Building.
    public int buildingType; //0 = Lumberyard, 1 = Quarry, Townhall, Barracks, Hospital, Laboratory, Blacksmith, Wall, Watchtower, Trading Post, Resource Silo, Radar, Garrison, Alliance Center.
    public bool buildingBuiltTF;
    public int level;
    public bool upgradeInProgress;
    private long remainingTimeSeconds;
    public bool slotUnlockedTF; //If false, can't build on.
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    
    //##### Beg of Generic Functions #####
    void Start() {
        UIM = GM.GetComponent<UIManager>();
        PD = GM.GetComponent<PlayerData>();
        PM = GM.GetComponent<PlayerMap>();
        AM = GM.GetComponent<ArmyManager>();
        BM = GM.GetComponent<BuildingManager>();

        slotUnlockedTF = false;
        cellSize /= 100;
    }
    
    public void tempInit() {
        //Used for temp Instantiations
        UIM = GM.GetComponent<UIManager>();
        PD = GM.GetComponent<PlayerData>();
        PM = GM.GetComponent<PlayerMap>();
        AM = GM.GetComponent<ArmyManager>();
        BM = GM.GetComponent<BuildingManager>();

        cellSize /= 100;
    }

    

    public void SetBuildingImage() {
        if(buildingType != -1) {
            if(UIM.buildingSpriteList[buildingType] != null && buildingType != 7) {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = UIM.buildingSpriteList[buildingType];
            }
            else if(buildingType != 7) {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
            }
        }else {
            if(!slotUnlockedTF) {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = UIM.lockedSlot;
            }else {
                this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
            }
        }  
    }

    private void TurnOnBuildingIcon() {
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void TurnOffBuildingIcon() {
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    private bool hasMoreLevelsAvailable() {
        //Improve this for certain buildings
        if(level < 25) { 
            return true;
        }else {
            return false;
        }
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
                UIM.TurnOnBuildingOptionsObjSelected(viewportPoint, buildingType, hasMoreLevelsAvailable());
            }else if(slotUnlockedTF){
                //If the building is not built and the slot is unlocked for building, the UI should be for building that building initially.
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
            //On Upgrade Finished

            //Generic
            upgradeInProgress = false;
            level += 1;
            TurnOffBuildingIcon();
            PM.numberOfBuildersAvailable += 1;

            //Building Type Specific
            if(buildingType == 0 || buildingType == 1) {
                PM.CalculateResourceIncome(); //If a production building is built or upgraded, we need to calculate the new resource prod. output.
            }else if (buildingType == 2) {
                AM.SetMarchSize(GetMarchSize());
                PM.maxNumBuildings = GetMaxNumBuildings();
                PM.civAge = GetCivAge();
            }
            
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
            SetBuildingImage();
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
    //##### End of Generic Functions #####


    //##### Beg of Lumberyard & Quary Functions #####
    public float GetProdPerSec(int levelInc = 0) {
        if(!upgradeInProgress) 
            return Mathf.Pow((((float)level + levelInc) / 2), 1.05f);
        else
            return 0f;
    }
    //##### End of Lumberyard & Quary Functions #####


    //##### Beg of Town Hall Functions #####
    public int GetMarchSize() {
        return BM.marchSizeList[level - 1];
    }

    public int GetMaxNumBuildings() {
        return BM.maxNumBuildingsList[level - 1];
    }

    public string GetCivAge() {
        if(level < 4) {
            return "Stone";
        }else if(level < 10) {
            return "Bronze";
        }else if(level < 16) {
            return "Iron";
        }else if(level < 21) {
            return "Dark";
        }else {
            return "Feudal";
        }
    }
    //##### End of Town Hall Functions #####


    //##### Beg of NextBuilding Functions #####
    //##### End of NextBuilding Functions #####
    
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
            return (int)Mathf.Round(GetEstimatedResourcesCost(resourceType, levelInc) * BM.buildingsByResourcePercent[resourceType,0]);
        }else if(buildingTypeCheck == 1) {
            //Quarry
            return (int)Mathf.Round(GetEstimatedResourcesCost(resourceType, levelInc) * BM.buildingsByResourcePercent[resourceType,1]);
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
            return Mathf.Pow(GetTHLevelReq(levelInc), 2.5f) * BM.univPowerMulti;
        }else {
            Debug.Log("Error: Building Type not accounted for.");
            return -1;
        }
    }

    public void SetPD(PlayerData PD) {
        //Used in the case of Manually Creating BuildingSlot just for temporary use
        this.PD = PD;
    }
    //##### End of Getters/Setters #####
}
