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
    private float timeSinceMouseDown; //For mouse drag.
    private float mouseDragSecondsLeway = 0.125f; //How much time before click instead counts as a drag.
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start() {
        KM = GM.GetComponent<KingdomMap>();
        MM = GM.GetComponent<MapManager>();
        PM = GM.GetComponent<PlayerMap>();
        UIM = GM.GetComponent<UIManager>();
        timeSinceMouseDown = 0;
    }

    void Update() {
        //Mouse Down
        if(Input.GetMouseButton(0)) {
            //Trigger Delay
            if(timeSinceMouseDown == 0)
                timeSinceMouseDown = Time.time;

            //wait for mouse button up, then check delay time. If < 0.05, count as click. Else, count as drag.
        }

        //Mouse Up
        if(!Input.GetMouseButton(0)) {
            //Actions on Just Mouse Up

            //Delay Check (Drag vs Click)
            if(Time.time - timeSinceMouseDown < mouseDragSecondsLeway) {
                //Click
                //GUI Checks
                if(!UIM.GetAnyGUIActive()) {
                    //Map Zooming Checks
                    if(MM.mapZoomingID == 1) {
                        var unitSelected = KM.SelectMapUnit(GetMouseWorldPosition());
                        if(!unitSelected) {
                            //If no map unit was selected, check the tiles
                            KM.GridTileClicked(GetMouseWorldPosition());
                        }else {
                            KM.SelectedUnitSetDestination(GetMouseWorldPosition());
                        }
                    }else if(MM.mapZoomingID == 2) {
                        PM.TileClicked(GetMouseWorldPosition());
                    }
                }
            }else {
                //Drag

            }
            timeSinceMouseDown = 0;
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
