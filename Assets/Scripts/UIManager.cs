using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private MapManager MM;
    private KingdomMap KM;
    private InputManager IM;

    public bool tileSelectedTF;

    //Position Search UI
    public GameObject PositionSearchObject;
    public GameObject CurrentPositionObject;

    //Player Selected UI
    public GameObject playerBaseSelectedObject;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    private void Start() {
        //Manager Lists
        MM = GM.GetComponent<MapManager>();
        KM = GM.GetComponent<KingdomMap>();
        IM = GM.GetComponent<InputManager>();

        //UI Start Mode
        tileSelectedTF = false;
        playerBaseSelectedObject.SetActive(false);
    }


    //##### End of Main Functions #####


    //##### Beg of Button Clicked Events #####
    public void PositionSearchButtonClicked() {
        var inputX = PositionSearchObject.transform.GetChild(3).GetComponent<TMP_InputField>().text != "" ? int.Parse(PositionSearchObject.transform.GetChild(3).GetComponent<TMP_InputField>().text) : 0;
        var inputY = PositionSearchObject.transform.GetChild(4).GetComponent<TMP_InputField>().text != "" ? int.Parse(PositionSearchObject.transform.GetChild(4).GetComponent<TMP_InputField>().text) : 0;
        if(inputX > 999) {
            inputX = 999;
        }
        if(inputX < 0) {
            inputX = 999;
        }
        if(inputY > 999) {
            inputY = 999;
        }
        if(inputY < 0) {
            inputY = 0;
        }
        KM.MoveToPos(inputX, inputY);
    }

    public void SetCurrentPositionObject(int x, int y) {
        CurrentPositionObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "X: " + x;
        CurrentPositionObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "Y: " + y;
    }

    public void ToggleKingdomZooming() {
        MM.ToggleMapZooming();
    }

    public void ToggleTileSelected() {
        tileSelectedTF = !tileSelectedTF;
    }

    public bool ClickGUILocation(GameObject GO) {
        //True = Inside GUI, False = Outside GUI
        RectTransform rt = GO.transform.GetComponent<RectTransform>();
        Vector2 localMousePosition = rt.InverseTransformPoint(Input.mousePosition);
        if (rt.rect.Contains(localMousePosition))
            return true;
        else
            return false;
    }

    public void ToggleCitySelected(int playerID) {
        //TODO: playerID will later be used to generate the specific data of the player.

        if(!tileSelectedTF) {
            ToggleTileSelected();
        }else {
        if(!ClickGUILocation(playerBaseSelectedObject))
            ToggleTileSelected();
        }

        //Generate GUI
        if(tileSelectedTF) {
            playerBaseSelectedObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "User ID: " + playerID;
            playerBaseSelectedObject.SetActive(true);
        }else {
            playerBaseSelectedObject.SetActive(false);
        }
    }

    public void EnterPlayerMapGUI() {
        playerBaseSelectedObject.SetActive(false);
    }

    //##### End of Button Clicked Events #####
}
