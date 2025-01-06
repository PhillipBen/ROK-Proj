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
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = GM.GetComponent<KingdomMap>();
        UIM = GM.GetComponent<UIManager>();
    }

    public void TileClicked(Vector3 clickPos) {
        var anyTileClickedTF = false;
        for(int i = 0; i < buildingTileList.Count; i++) {
            if(buildingTileList[i].GetComponent<BuildingSlot>().IsTileClicked(clickPos))
                anyTileClickedTF = true;
        }
        if(!anyTileClickedTF) {
            UIM.ToggleBuildingOptionsObjSelected(clickPos, -1);
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
        var resourceList = new List<float> {2f, 1f, 0f}; //0 = wood, 1 = stone, 2 = gems
        //Add resources
        return resourceList;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
