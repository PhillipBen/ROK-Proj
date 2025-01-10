using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*
    Controls:
    Mouse Left Button: Press up and down for UI, Camera Movement. Hold also for camera movement.

    */


    //##### Beg of Variables #####
    public GameManager GM;
    private MapManager MM;
    private KingdomMap KM;
    private PlayerMap PM;
    private UIManager UIM;
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start() {
        KM = GM.GetComponent<KingdomMap>();
        MM = GM.GetComponent<MapManager>();
        PM = GM.GetComponent<PlayerMap>();
        UIM = GM.GetComponent<UIManager>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0) && !UIM.GetAnyGUIActive()) {
            if(MM.mapZoomingID == 1) {
                KM.GridTileClicked(GetMouseWorldPosition());
            }else if(MM.mapZoomingID == 2) {
                PM.TileClicked(GetMouseWorldPosition());
            }
        }
    }
    
    public static Vector3 GetMouseWorldPosition2D()
    {
        Vector3 vec3 = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        Vector2 vec = new Vector2(vec3.x, vec3.y);
        return vec;
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####

    

}
