using UnityEngine;

public class MapManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    private PlayerMap PM;
    private UIManager UIM;
    
    //0 = Kingdom List, 1 = Kingdom, 2 = Home
    public int mapZoomingID;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        KM = this.GetComponent<KingdomMap>();
        PM = this.GetComponent<PlayerMap>();
        UIM = this.GetComponent<UIManager>();
        mapZoomingID = 2;
    }

    public void ToggleMapZooming() {
        if(mapZoomingID == 1) {
            mapZoomingID = 2;
        }else {
            mapZoomingID = 1;
        }
    }

    public void SetMapZoomingToPlayerMap() {
        mapZoomingID = 2;
        UIM.EnterPlayerMapGUI();
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public void setMapZoomingID(int mapZoomingID) {
        this.mapZoomingID = mapZoomingID;
    }
    public int getMapZoomingID() {
        return this.mapZoomingID;
    }
    //##### End of Getters/Setters #####
}
