using UnityEngine;
using System.Collections.Generic;

public class PlayerMap : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    private UIManager UIM;
    private BuffManager BFM;
    public List<GameObject> buildingTileList = new List<GameObject>();
    private Vector3 resourceIncome; //Wood, Stone, Gems.
    public GameObject playerBaseTilesFolder;
    public BuildingSlot selectedBuilding;
    public int tempTHLevel;
    public int numberOfBuildersAvailable;
    public int maxNumBuildings;
    public string civAge;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = GM.GetComponent<KingdomMap>();
        UIM = GM.GetComponent<UIManager>();
        BFM = GM.GetComponent<BuffManager>();

        CalculateResourceIncome();
        CalculateUnlockedBuildings();
    }

    public void CalculateUnlockedBuildings() {
        var numOfPossUnlockedSlots = -1;
        for(int i = 0; i < buildingTileList.Count; i++) {
            if(buildingTileList[i].GetComponent<BuildingSlot>().buildingType == 2) {//TownHall
                numOfPossUnlockedSlots = buildingTileList[i].GetComponent<BuildingSlot>().GetMaxNumBuildings();
                break;
            }
        }

        var currNumUnlockedSlots = 0;
        for(int i = 0; i < buildingTileList.Count; i++) {
            BuildingSlot b = buildingTileList[i].GetComponent<BuildingSlot>();
            if(b.buildingType != 7) {//Not Wall
                if(currNumUnlockedSlots < numOfPossUnlockedSlots) {
                    b.slotUnlockedTF = true;
                    currNumUnlockedSlots += 1;
                }
            }else if(b.buildingType == 1) {
                b.slotUnlockedTF = true;
            }else if(b.buildingType == 7) {
                b.buildingBuiltTF = true;
                b.slotUnlockedTF = true;
            }
            b.SetBuildingImage();
        }
    }

    public void TileClicked(Vector3 clickPos) {

        var anyTileClickedTF = false;
        if(!UIM.buildingOptionsObjSelectedTF) {
            for(int i = 0; i < buildingTileList.Count; i++) {
                if(buildingTileList[i].GetComponent<BuildingSlot>().IsTileClicked(clickPos)) {
                    anyTileClickedTF = true;
                    selectedBuilding = buildingTileList[i].GetComponent<BuildingSlot>();
                    break;
                }
            }
        }
        if(!anyTileClickedTF) {
            UIM.TurnOffBuildingOptionsObjSelected();
        }
    }

    public void BuildingInProgressUpdate(int timePassed) {
        for(int i = 0; i < buildingTileList.Count; i++) {
           buildingTileList[i].GetComponent<BuildingSlot>().BuildingInProgress(timePassed);
        }
    }

    public void ShowTiles() {
        playerBaseTilesFolder.SetActive(true);
    }

    public void HideTiles() {
        playerBaseTilesFolder.SetActive(false);
    }

    public void CalculateResourceIncome() {
        //Called on Start and on Building Upgrade
        resourceIncome = new Vector3(0f, 0f, 0f);
        for(int i = 0; i < buildingTileList.Count; i++) {
            BuildingSlot b = buildingTileList[i].GetComponent<BuildingSlot>();
            if(b.buildingType == 0) {//Wood
                resourceIncome += new Vector3(b.GetProdPerSec(), 0f, 0f);
            }else if(b.buildingType == 1) {//Stone
                resourceIncome += new Vector3(0f, b.GetProdPerSec(), 0f);
            }
        }
    }

    public List<float> GetBuildingResourceValues() {
        //var resourceList = new List<float> {20f, 20f, 20f}; //0 = wood, 1 = stone, 2 = gems //Testing
        var resourceList = new List<float> {resourceIncome.x * BFM.woodProdBonus, resourceIncome.y * BFM.stoneProdBonus, resourceIncome.z};
        return resourceList;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
