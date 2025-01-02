using UnityEngine;

public class MapManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    private PlayerMap PM;
    private UIManager UIM;
    private CameraMovement CM;
    
    //0 = Kingdom List, 1 = Kingdom, 2 = Home
    public int mapZoomingID;

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = this.GetComponent<KingdomMap>();
        PM = this.GetComponent<PlayerMap>();
        UIM = this.GetComponent<UIManager>();
        CM = GM.mainCamera.GetComponent<CameraMovement>();
        mapZoomingID = 2;
    }

    public void ToggleMapZooming() {
        if(mapZoomingID == 2) {
            mapZoomingID = 1;
        }else {
            mapZoomingID = 2;
        }
        UIM.UpdateMapGUI();
    }

    public void ToggleUniverseMapZooming() {
        if(mapZoomingID == 1) {
            mapZoomingID = 0;
        }else {
            mapZoomingID = 1;
        }
        UIM.UpdateMapGUI();
    }

    public void SetMapZoomingToPlayerMap() {
        mapZoomingID = 2;
        UIM.UpdateMapGUI();
        CM.MoveCamera(0, 0);
    }

    public void KingdomSelected() {
        Debug.Log("K Selected");
        //Needs to load new kingdom.
    }
    //##### End of Main Functions #####
}
