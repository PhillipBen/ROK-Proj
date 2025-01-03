using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private MapManager MM;
    private KingdomMap KM;
    private PlayerMap PM;
    private InputManager IM;
    private UniverseMap UM;


    //Universe UI
    public GameObject universeListObject;

    //World GUI
    public bool tileSelectedTF;
    public GameObject MapZoomingObject;
    public GameObject UniverseZoomingObject;
    public GameObject PositionSearchObject;
    public GameObject CurrentPositionObject;
    public GameObject playerBaseSelectedObject;

    //Player GUI
    public GameObject playerGUIFolder;
    public GameObject powerObj;
    public GameObject VIPObj;
    public GameObject woodObj;
    public GameObject stoneObj;
    public GameObject chatObj;
    public GameObject tabBGObj;
    public GameObject commanderObj;
    public GameObject allianceObj;
    public GameObject inventoryObj;
    public bool tabOpenTF;
    public GameObject buildingOptionsObj;

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    private void Start() {
        //Manager Lists
        MM = GM.GetComponent<MapManager>();
        KM = GM.GetComponent<KingdomMap>();
        IM = GM.GetComponent<InputManager>();
        PM = GM.GetComponent<PlayerMap>();
        UM = GM.GetComponent<UniverseMap>();

        //UI Start Mode
        tileSelectedTF = false;
        playerBaseSelectedObject.SetActive(false);
        buildingOptionsObj.SetActive(false);
        tabOpenTF = false;
        TabGUIToggle();
        UpdateMapGUI();
    }

    public void UpdateMapGUI() {
        //0 = Kingdom List, 1 = Kingdom, 2 = Home
        if(MM.mapZoomingID == 2) {
            MapZoomingObject.SetActive(true);
            UniverseZoomingObject.SetActive(false);
            playerBaseSelectedObject.SetActive(false);

            PM.ShowTiles();
            KM.HideTiles();

            HideUniverseGUI();
            HideKingdomGUI();
            ShowPlayerGUI();

        }else if(MM.mapZoomingID == 1) {
            MapZoomingObject.SetActive(true);
            UniverseZoomingObject.SetActive(true);
            buildingOptionsObj.SetActive(false);

            PM.HideTiles();
            KM.ShowTiles();

            HideUniverseGUI();
            ShowKingdomGUI();
            ShowPlayerGUI();
        }else if(MM.mapZoomingID == 0) {
            MapZoomingObject.SetActive(false);
            UniverseZoomingObject.SetActive(true);
            playerBaseSelectedObject.SetActive(false);
            buildingOptionsObj.SetActive(false);

            PM.HideTiles();
            KM.HideTiles();

            UM.ViewingUniverseMapMode(); //functionality

            ShowUniverseGUI();
            HideKingdomGUI();
            HidePlayerGUI();
        }
    }

    public void ShowUniverseGUI() {
        universeListObject.SetActive(true);
    }

    public void HideUniverseGUI() {
        universeListObject.SetActive(false);
    }

    public void ShowKingdomGUI() {
        PositionSearchObject.SetActive(true);
        CurrentPositionObject.SetActive(true);
    }

    public void HideKingdomGUI() {
        PositionSearchObject.SetActive(false);
        CurrentPositionObject.SetActive(false);
    }

    public void ShowPlayerGUI() {
        playerGUIFolder.SetActive(true);
    }

    public void HidePlayerGUI() {
        playerGUIFolder.SetActive(false);
    }

    public void SortKingdomList(int ID) {
        //0 = Beginner Kingdoms, 1 = KVK Kingdoms
        UM.SortKingdomList(ID);
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

    public void ToggleTabSelected() {
        tabOpenTF = !tabOpenTF;
    }

    public void TabGUIToggle() {
        if(tabOpenTF) {
            tabBGObj.SetActive(true);
            commanderObj.SetActive(true);
            allianceObj.SetActive(true);
            inventoryObj.SetActive(true);
        }else {
            tabBGObj.SetActive(false);
            commanderObj.SetActive(false);
            allianceObj.SetActive(false);
            inventoryObj.SetActive(false);
        }
    }

    public void ShowBuildingOptions(Vector2 newPos) {
        buildingOptionsObj.SetActive(true);
        buildingOptionsObj.transform.position = newPos;  
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

    public void ToggleUniverseZooming() {
        MM.ToggleUniverseMapZooming();
    }

    public void ToggleTileSelected() {
        tileSelectedTF = !tileSelectedTF;
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

    public void DailyTasksSelected() {
        
    }

    public void PlayerProfileSelected() {
        
    }

    public void VIPSelected() {
        
    }

    public void BoostsSelected() {
        
    }

    public void WoodSelected() {
        
    }

    public void StoneSelected() {
        
    }

    public void GemsSelected() {
        
    }

    public void GemsBuySelected() {
        
    }

    public void DealsSelected() {
        
    }

    public void EventTasksSelected() {
        
    }

    public void InboxSelected() {
        
    }

    public void TabSelected() {
        ToggleTabSelected();
        TabGUIToggle();
    }

    public void CommanderSelected() {
        
    }

    public void AllianceSelected() {
        
    }

    public void InventorySelected() {
        
    }

    public void ChatSelected() {
        
    }

    public void TileEntitySearchSelected() {
        
    }

    //##### End of Button Clicked Events #####
}
