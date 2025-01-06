using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private MapManager MM;
    private KingdomMap KM;
    private PlayerMap PM;
    private InputManager IM;
    private UniverseMap UM;
    private TimeManager TM;
    private PlayerData PD;


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
    public GameObject gemsObj;
    public GameObject chatObj;
    public GameObject tabBGObj;
    public GameObject commanderObj;
    public GameObject allianceObj;
    public GameObject inventoryObj;
    public bool tabOpenTF;
    public GameObject playerPopupGUI;
    public GameObject buildingOptionsObj;
    public bool buildingOptionsObjSelectedTF;
    public GameObject playerProfilePFP;
    public GameObject playerProfileLevel;
    public GameObject timeMultiTextObj;

    //##### End of Variables #####


    //##### Beg of Init Functions #####
    public void InitLoadGraphics() {
        UpdateResourcesGUI();
        UpdateTimeMutliGUI(TM.timeMulti);
    }
    //##### End of Init Functions #####


    //##### Beg of Main Functions #####
    private void Start() {
        //Manager Lists
        MM = GM.GetComponent<MapManager>();
        KM = GM.GetComponent<KingdomMap>();
        IM = GM.GetComponent<InputManager>();
        PM = GM.GetComponent<PlayerMap>();
        UM = GM.GetComponent<UniverseMap>();
        TM = GM.GetComponent<TimeManager>();
        PD = GM.GetComponent<PlayerData>();

        //UI Start Mode
        tileSelectedTF = false;
        buildingOptionsObjSelectedTF = false;
        playerBaseSelectedObject.SetActive(false);
        buildingOptionsObj.SetActive(false);
        tabOpenTF = false;
        playerPopupGUI.SetActive(false);
        
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

    public void ShowPlayerProfileGUI() {
        playerPopupGUI.SetActive(true);
    }

    public void HidePlayerProfileGUI() {
        playerPopupGUI.SetActive(false);
    }

    public void LoadEXPBar(PlayerEXP playerEXP) {
        playerProfileLevel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().fillAmount = playerEXP.GetEXPToNextLevelPercentage();
        playerProfileLevel.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = playerEXP.GetCurrentLevel().ToString();
    }

    public void ToggleTileSelected() {
        tileSelectedTF = !tileSelectedTF;
    }

    public void ToggleCitySelected(int playerID) {
        //TODO: playerID will later be used to generate the specific data of the player.
        //playerID -1 will appear if no tile was selected.

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

    public void ToggleBuildingOptionsObj() {
        buildingOptionsObjSelectedTF = !buildingOptionsObjSelectedTF;
    }

    public void ToggleBuildingOptionsObjSelected(Vector2 newPos, int buildingType) {
        //buildingType -1 will be input if no building was selected.

        if(!buildingOptionsObjSelectedTF) {
            ToggleBuildingOptionsObj();
        }else {
        if(!ClickGUILocation(buildingOptionsObj))
            ToggleBuildingOptionsObj();
        }

        //Generate GUI
        if(buildingOptionsObjSelectedTF) {
            //Load Custom Image based on selected building type.
            buildingOptionsObj.SetActive(true);
            buildingOptionsObj.transform.position = newPos;  
        }else {
            buildingOptionsObj.SetActive(false);
        }
    }

    public void SetWoodAmount(long amount) {
        woodObj.transform.GetChild(0).GetComponent<TMP_Text>().text = ConvertAmountToCharacters(amount);
    }

    public void SetStoneAmount(long amount) {
        stoneObj.transform.GetChild(0).GetComponent<TMP_Text>().text = ConvertAmountToCharacters(amount);
    }

    public void SetGemsAmount(long amount) {
        gemsObj.transform.GetChild(0).GetComponent<TMP_Text>().text = ConvertAmountToCharacters(amount);
    }

    public string ConvertAmountToCharacters(long amount) {
        if(amount < 1000000) {
            return amount.ToString();
        }else if (amount > 999999 && amount <1000000000) {
            var temp = Mathf.Round(amount / 10000);
            temp /= 100;
            return temp.ToString() + "M";
        }else if (amount > 999999999 && amount <1000000000000) {
            var temp = Mathf.Round(amount / 10000000);
            temp /= 100;
            return temp.ToString() + "B";
        }else if (amount > 999999999999 && amount <1000000000000000) {
            var temp = Mathf.Round(amount / 10000000000);
            temp /= 100;
            return temp.ToString() + "T";
        }else if (amount > 999999999999999 && amount <1000000000000000000) {
            var temp = Mathf.Round(amount / 10000000000000);
            temp /= 100;
            return temp.ToString() + "Q";
        }else {
            return "Too Big!";
        }
    }

    public void UpdateTimeMutliGUI(int newMulti) {
        timeMultiTextObj.GetComponent<TMP_Text>().text = "" + newMulti + "x";
    }

    public void UpdateResourcesGUI() {
        Player player = PD.GetPlayer();
        var PR = player.playerResources;

        SetWoodAmount(PR.woodAmount);
        SetStoneAmount(PR.stoneAmount);
        SetGemsAmount(PR.gemsAmount);
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

    public void DailyTasksSelected() {
        
    }

    public void PlayerProfileSelected(PlayerData playerData) {
        //Call load exp level based on selected user.

        LoadEXPBar(playerData.GetPlayerEXPData());
        ShowPlayerProfileGUI();
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

    public void TimeMultiUpdate(int updateType) {
        TM.UpdateTimeMulti(updateType); //updateType = 1 or -1
        UpdateTimeMutliGUI(TM.timeMulti);
    }

    //##### End of Button Clicked Events #####
}
