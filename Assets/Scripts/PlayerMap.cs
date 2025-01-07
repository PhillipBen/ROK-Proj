using UnityEngine;
using System.Collections.Generic;

public class PlayerMap : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    private UIManager UIM;
    public List<GameObject> buildingTileList = new List<GameObject>();
    public GameObject playerBaseTilesFolder;
    public BuildingSlot selectedBuilding;

    public int tempTHLevel;
    public int numberOfBuildersAvailable;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = GM.GetComponent<KingdomMap>();
        UIM = GM.GetComponent<UIManager>();
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

    public List<float> GetBuildingResourceValues() {
        //For each building in list, if it produces resources, add to list.
        var resourceList = new List<float> {20f, 20f, 20f}; //0 = wood, 1 = stone, 2 = gems
        //Add resources
        return resourceList;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
