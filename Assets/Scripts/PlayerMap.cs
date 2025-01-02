using UnityEngine;
using System.Collections.Generic;

public class PlayerMap : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    public List<GameObject> buildingTileList = new List<GameObject>();
    public GameObject playerBaseTilesFolder;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = GM.GetComponent<KingdomMap>();
    }

    public void TileClicked(Vector3 clickPos) {
        for(int i = 0; i < buildingTileList.Count; i++) {
            buildingTileList[i].GetComponent<BuildingSlot>().IsTileClicked(clickPos);
        }
    }

    public void ShowTiles() {
        playerBaseTilesFolder.SetActive(true);
    }

    public void HideTiles() {
        playerBaseTilesFolder.SetActive(false);
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
