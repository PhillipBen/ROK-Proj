using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*
    Controls:
    Mouse Left Button: Press up and down for UI, Camera Movement. Hold also for camera movement.

    */


    //##### Beg of Variables #####
    public GameManager GM;
    private KingdomMap KM;
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start() {
        KM = GM.GetComponent<KingdomMap>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            KM.GridTileClicked(GetMouseWorldPosition());
        }
    }

    //Get Mouse Position in world with Z = 0f (2d)
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
