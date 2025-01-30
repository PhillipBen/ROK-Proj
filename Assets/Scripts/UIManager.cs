using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
    private BuildingManager BM;
    private MailManager MailM;
    private DataManager DM;
    private ArmyManager AM;


    //Universe UI
    public GameObject universeListObject;
    public GameObject UniverseFolderObj;

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
    public GameObject buildingInfoGuiTab;
    public GameObject buildingInfoGUIPreset;
    public GameObject buildingInfoGUIDataPreset;
    public GameObject buildingUpgradeGuiTab;
    public GameObject buildingUpgradeGUIDataPreset;
    public GameObject buildingUpgradeGUIPreset;
    public GameObject buildingBuildGUIObj;
    public GameObject buildingBuildGUIPreset;
    public bool BuildButtonPressedTF;
    public List<Sprite> buildingSpriteList;
    public Sprite lockedSlot;
    public GameObject mailFolderObj;
    public List<Mail> recentlyLoadedMail;
    private int mailLastSelectedTab;
    private int selectedMail;
    public GameObject UnitTrainingObj;
    private int troopTypeSelected;//Inf, Art, AV, Trans.
    private int troopTierSelected;//1-5
    public GameObject mapUnitPreset;
    public GameObject mapUnitFolder;
    public GameObject mapUnitDeploymentGUI;
    public GameObject mapUnitSliderSelectorPreset;
    public GameObject mapUnitSliderSelectorFolder;
    public List<Sprite> tierBackgroundList;
    public GameObject mapUnitTypeTextPreset;
    public GameObject MapUnitActionsObj;

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
        BM = GM.GetComponent<BuildingManager>();
        MailM = GM.GetComponent<MailManager>();
        DM = GM.GetComponent<DataManager>();
        AM = GM.GetComponent<ArmyManager>();

        //UI Start Mode
        tileSelectedTF = false;
        buildingOptionsObjSelectedTF = false;
        playerBaseSelectedObject.SetActive(false);
        buildingOptionsObj.SetActive(false);
        tabOpenTF = false;
        playerPopupGUI.SetActive(false);
        UniverseFolderObj.SetActive(true);
        UnitTrainingObj.SetActive(false);
        
        
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
        mapUnitFolder.SetActive(true);
    }

    public void HideKingdomGUI() {
        PositionSearchObject.SetActive(false);
        CurrentPositionObject.SetActive(false);
        mapUnitDeploymentGUI.SetActive(false);
        mapUnitFolder.SetActive(false);
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

    public void TurnOnCitySelected(int playerID) {
        if(tileSelectedTF) {
            TurnOffCitySelected();
        }
        else {
            playerBaseSelectedObject.transform.GetChild(3).GetComponent<TMP_Text>().text = "User ID: " + playerID;
            playerBaseSelectedObject.SetActive(true);
            tileSelectedTF = true;
        }
    }

    public void TurnOffCitySelected() {
        var inGUIPos = ClickGUILocation(playerBaseSelectedObject);
        if(!inGUIPos) {
            playerBaseSelectedObject.SetActive(false);
            tileSelectedTF = false;
        }
    }

    public void TurnOnBuildingOptionsObjSelected(Vector2 newPos, int buildingType, bool upgradeAvailable) {
        if(!buildingOptionsObjSelectedTF) {
            buildingOptionsObj.transform.position = newPos;  
            buildingOptionsObj.SetActive(true);
            buildingOptionsObjSelectedTF = true;

            if(upgradeAvailable) {
                //Hide level button if building is already max level
                buildingOptionsObj.transform.GetChild(2).gameObject.SetActive(true);
            }else {
                buildingOptionsObj.transform.GetChild(2).gameObject.SetActive(false);
            }

            if(buildingType == 0 || buildingType == 1) {
                //Hide Custom functionality, because there is none
                buildingOptionsObj.transform.GetChild(3).gameObject.SetActive(false);
            }else if(false) {
                //Add custom image pertaining to specific functionality
            }else {
                buildingOptionsObj.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void TurnOffBuildingOptionsObjSelected() {
        var inGUIPos = ClickGUILocation(buildingOptionsObj);
        if(!inGUIPos) {
            buildingOptionsObj.SetActive(false);
            buildingOptionsObjSelectedTF = false;
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

    public bool GetAnyGUIActive() {
        if(buildingBuildGUIObj.activeSelf || buildingUpgradeGuiTab.activeSelf || buildingInfoGuiTab.activeSelf || mailFolderObj.activeSelf || UnitTrainingObj.activeSelf || mapUnitDeploymentGUI.activeSelf)
            return true;
        else
            return false;
    }

    private void GenerateSelectedMailList(int sortType) {
        mailLastSelectedTab = sortType;
        selectedMail = 0;
        recentlyLoadedMail = MailM.GetMailByTab(sortType);
        var mailList = recentlyLoadedMail;
        var mailFolder = mailFolderObj.transform.GetChild(1).GetChild(0).gameObject;
        var mailPreset = mailFolderObj.transform.GetChild(1).GetChild(1).gameObject;

        //Delete old Details and Data Instantiations
        for(int i = 0; i < mailFolder.transform.childCount; i++) {
            Destroy(mailFolder.transform.GetChild(i).gameObject);
        }
        //Hide New Mail Section
        mailFolderObj.transform.GetChild(3).gameObject.SetActive(false);
        mailFolderObj.transform.GetChild(2).gameObject.SetActive(true);

        for(int i = 0; i < mailList.Count; i++) {
            var player = DM.GetPlayerFromGloablList(mailList[i].GetUserSender().getUserID());

            GameObject newMessage = (GameObject)Instantiate(mailPreset);
            newMessage.transform.SetParent(mailFolder.transform);
            newMessage.transform.localScale = new Vector3(1f, 1f, 1f);
            newMessage.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = player.playerIcon;
            newMessage.transform.GetChild(1).GetComponent<TMP_Text>().text = "[" + player.tempAllianceName + "]" + " " + mailList[i].GetUserSender().getUserInGameName();
            newMessage.transform.GetChild(2).GetComponent<TMP_Text>().text = mailList[i].GetSubject();
            newMessage.transform.GetChild(3).GetComponent<TMP_Text>().text = ReturnDateAgoFormatted(new DateTime(DateTime.Now.Ticks - mailList[i].GetSentDateTime().Ticks));
            newMessage.SetActive(true);
        }
        if(mailList.Count > 0) 
            GenerateSelectedMail(mailList[0]);
    }

    private string ReturnDateAgoFormatted(DateTime dt) {
        //Sets 0 years as 1 for all dates
        if(dt.Year - 1 > 0) { 
            return "" + (dt.Year - 1) + " years ago.";
        }else if(dt.Month - 1 > 0) {
            return "" + (dt.Month - 1) + " months ago.";
        }else if(dt.Day - 1 > 0) {
            return "" + (dt.Day - 1) + " days ago.";
        }else if(dt.Hour - 1 > 0) {
            return "" + (dt.Hour - 1) + " hours ago.";
        }else if(dt.Minute - 1 > 0) {
            return "" + (dt.Minute - 1) + " minutes ago.";
        }else {
            //Happened Just Now
            return "Sent Just Now.";
        }
    }

    private void GenerateSelectedMail(Mail mail) {
        var mailFolder = mailFolderObj.transform.GetChild(2);
        var player = DM.GetPlayerFromGloablList(mail.GetUserSender().getUserID());

        mailFolder.GetChild(1).GetComponent<TMP_Text>().text = "[" + player.tempAllianceName + "]" + " " + mail.GetUserSender().getUserInGameName();
        mailFolder.GetChild(2).GetComponent<TMP_Text>().text = mail.GetSubject();
        mailFolder.GetChild(3).GetComponent<TMP_Text>().text = mail.GetSentDateTime().ToString();
        mailFolder.GetChild(5).GetComponent<TMP_Text>().text = mail.GetBody();
    }

    private void StartNewMail() {
        mailFolderObj.transform.GetChild(3).gameObject.SetActive(true);
        mailFolderObj.transform.GetChild(2).gameObject.SetActive(false);

        //Clear fields of previous data.
        mailFolderObj.transform.GetChild(3).GetChild(0).GetComponent<TMP_InputField>().text = "";
        mailFolderObj.transform.GetChild(3).GetChild(1).GetComponent<TMP_InputField>().text = "";
        mailFolderObj.transform.GetChild(3).GetChild(2).GetComponent<TMP_InputField>().text = "";
    }

    private void SendNewMail() {
        var sender = PD.GetPlayer().user;
        var player = DM.GetPlayerFromGloablList(int.Parse(mailFolderObj.transform.GetChild(3).GetChild(0).GetComponent<TMP_InputField>().text));
        var reciever = player.user;
        var subject = mailFolderObj.transform.GetChild(3).GetChild(1).GetComponent<TMP_InputField>().text;
        var sentDateTime = DateTime.Now;
        var body = mailFolderObj.transform.GetChild(3).GetChild(2).GetComponent<TMP_InputField>().text;
        var favoriteTF = false;
        var newMail = new Mail(sender, reciever, subject, sentDateTime, body, favoriteTF);
        MailM.SendMail(newMail);

        //Clear fields of previous data.
        mailFolderObj.transform.GetChild(3).GetChild(0).GetComponent<TMP_InputField>().text = "";
        mailFolderObj.transform.GetChild(3).GetChild(1).GetComponent<TMP_InputField>().text = "";
        mailFolderObj.transform.GetChild(3).GetChild(2).GetComponent<TMP_InputField>().text = "";
        GenerateSelectedMailList(0);
    }

    private void TrainingGUIOpen() {
        UnitTrainingObj.SetActive(true);
        troopTypeSelected = 1; //Infantry
        troopTierSelected = 1; //Tier 1

        var unitNumFolder = UnitTrainingObj.transform.GetChild(5).GetChild(0);
        unitNumFolder.GetChild(1).gameObject.GetComponent<TMP_InputField>().text = "0";
        unitNumFolder.GetChild(0).gameObject.GetComponent<Slider>().value = 0;

        TrainingUpdateCosts();
        TrainingSetStats();
    }

    private void TrainingSetStats() {
        var infoTab = UnitTrainingObj.transform.GetChild(8);
        var unit = AM.FindUnit(troopTypeSelected, troopTierSelected);

        for(int i = 0; i < 9; i++) { //9 Stats
            if(i == 0)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.attack.ToString();
            else if(i == 1)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.defense.ToString();
            else if(i == 2)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.health.ToString();
            else if(i == 3)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.marchSpeed.ToString();
            else if(i == 4)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.load.ToString();
            else if(i == 5)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.power.ToString();
            else if(i == 6)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.trainingTime.ToString();
            else if(i == 7)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.woodCost.ToString();
            else if(i == 8)
                infoTab.GetChild((i * 2) + 1).gameObject.GetComponent<TMP_Text>().text = unit.stoneCost.ToString();
        }
    }

    private void TrainingUpdateCosts() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        var PR = PD.GetPlayer().playerResources;
        var unitNumFolder = UnitTrainingObj.transform.GetChild(5).GetChild(0);

        var unitCost = AM.GetUnitsTotalCost(troopTypeSelected, troopTierSelected, int.Parse(unitNumFolder.GetChild(1).gameObject.GetComponent<TMP_InputField>().text));

        unitNumFolder.GetChild(2).GetChild(1).gameObject.GetComponent<TMP_Text>().text = ConvertAmountToCharacters(Convert.ToInt64(unitCost.x)); //Wood Cost
        if(PR.woodAmount >= unitCost.x) {
            unitNumFolder.GetChild(2).GetChild(1).gameObject.GetComponent<TMP_Text>().color = Color.green;
        }else {
            unitNumFolder.GetChild(2).GetChild(1).gameObject.GetComponent<TMP_Text>().color = Color.red;
        }

        unitNumFolder.GetChild(3).GetChild(1).gameObject.GetComponent<TMP_Text>().text = ConvertAmountToCharacters(Convert.ToInt64(unitCost.y)); //Stone Cost
        if(PR.stoneAmount >= unitCost.y) {
            unitNumFolder.GetChild(3).GetChild(1).gameObject.GetComponent<TMP_Text>().color = Color.green;
        }else {
            unitNumFolder.GetChild(3).GetChild(1).gameObject.GetComponent<TMP_Text>().color = Color.red;
        }

        UnitTrainingObj.transform.GetChild(6).GetChild(2).gameObject.GetComponent<TMP_Text>().text = selectedBuilding.convertTimeToGems((int)unitCost.z).ToString(); //Time Cost - Gems
        if(PR.gemsAmount >= selectedBuilding.convertTimeToGems((int)unitCost.z)) {
            UnitTrainingObj.transform.GetChild(6).GetChild(2).gameObject.GetComponent<TMP_Text>().color = Color.green;
        }else {
            UnitTrainingObj.transform.GetChild(6).GetChild(2).gameObject.GetComponent<TMP_Text>().color = Color.red;
        }

        UnitTrainingObj.transform.GetChild(7).GetChild(2).gameObject.GetComponent<TMP_Text>().text = FormatTimeAsString((int)unitCost.z); //Time Cost - Reg Time
    }

    private string FormatTimeAsString(int time) {
        //Time input in seconds
        var secs = time % 60;
        var mins = Mathf.Floor(time / 60) % 60;
        var hours = Mathf.Floor(time / (60 * 60)) % 60;

        var secsStr = secs.ToString();
        var minsStr = mins.ToString();
        var hoursStr = hours.ToString();
        if(secsStr.Length == 1) {
            secsStr = "0" + secsStr;
        }
        if(minsStr.Length == 1) {
            minsStr = "0" + minsStr;
        }
        if(hoursStr.Length == 1) {
            hoursStr = "0" + hoursStr;
        }
        return hoursStr + ":" + minsStr + ":" + secsStr;
    }

    public string numberToCommas(int num, float remainder = 0f) {
        var retText = "";
        var numText = num.ToString();
        var maxLen = 0;
        if(numText.Length > 3) {
            maxLen = numText.Length % 3;
            if(maxLen == 0)
                maxLen = 3;
        }else {
            maxLen = numText.Length;
        }
        retText += numText.Substring(0, maxLen);
        for(int i = 0; i < Mathf.Floor((numText.Length - maxLen) / 3); i++) {
            retText += "," + numText.Substring(maxLen + (i*3),3);
        }
        if(remainder != 0f) 
            retText += "." + System.Math.Round(remainder, 2);
        return retText;
    }

    public void DeployUnitNumberUpdate(GameObject folder, int numUnits, float totalPower, int totalLoad, int max) {
        folder.transform.GetChild(0).GetComponent<TMP_Text>().text = "Units: " + numberToCommas(numUnits) + "/" + numberToCommas(max);
        var powerDec = 0f;
        if(totalPower % 1 != 0)
            powerDec = Mathf.Floor(totalPower) - totalPower;
        folder.transform.GetChild(1).GetComponent<TMP_Text>().text = "Total Power: " + numberToCommas((int)totalPower, powerDec);
        folder.transform.GetChild(2).GetComponent<TMP_Text>().text = "Load: " + numberToCommas(totalLoad);
    }

    public void DeployUnitSetCommanderPower(GameObject folder, int commanderPower1, int commanderPower2) {
        folder.transform.GetChild(3).GetComponent<TMP_Text>().text = "Commander Power: " + numberToCommas(commanderPower1 + commanderPower2);
    }

    public void MapUnitSelectedOpenGUI(Vector2 newPos) {
        MapUnitActionsObj.transform.position = newPos;
        MapUnitActionsObj.SetActive(true);
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
        mailFolderObj.SetActive(true);
        GenerateSelectedMailList(0);
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

    public void BuildingInfoButtonPressed() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;

        //Delete old Details and Data Instantiations
        for(int i = 0; i < buildingInfoGuiTab.gameObject.transform.GetChild(2).transform.GetChild(0).transform.childCount; i++) {
            Destroy(buildingInfoGuiTab.transform.GetChild(2).transform.GetChild(0).transform.GetChild(i).gameObject);
        }
        
        var pickList = new List<int>() {0};
        if(selectedBuilding.buildingType == 0 || selectedBuilding.buildingType == 1) {
            //Lumberyard & Quarry
            pickList = new List<int>() {2, 7}; //Prod. Per Sec., Power
        }else if(selectedBuilding.buildingType == 3) {
            //Barracks
            pickList = new List<int>() {7, 8}; //Power, Max Training Size
        }

        for(int i = 0; i < pickList.Count; i++) { //I choose which details to show.
            //Title
            GameObject newTile = (GameObject)Instantiate(buildingInfoGUIPreset.gameObject);
            newTile.transform.SetParent(buildingInfoGuiTab.gameObject.transform.GetChild(2).transform.GetChild(0));
            //Debug.Log("P: " + BM.buildingDetailList.Count);
            newTile.transform.GetChild(0).GetComponent<TMP_Text>().text = BM.GetBuildingDetailList(pickList[i]);
            newTile.SetActive(true);

            //Data
            GameObject newTile2 = (GameObject)Instantiate(buildingInfoGUIDataPreset.gameObject);
            newTile2.transform.SetParent(buildingInfoGuiTab.gameObject.transform.GetChild(2).transform.GetChild(0));
            newTile2.transform.GetChild(0).GetComponent<TMP_Text>().text = selectedBuilding.buildingDetailsListGet(pickList[i]);
            newTile2.SetActive(true);
        }

        buildingInfoGuiTab.SetActive(true);
    }

    public void HideInfoGUI() {
        buildingInfoGuiTab.SetActive(false);
    }

    public void BuildingUpgradeButtonPressed() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;

        if(!selectedBuilding.upgradeInProgress) {
            //Delete old Details and Data Instantiations
            for(int i = 0; i < buildingUpgradeGuiTab.gameObject.transform.GetChild(3).transform.GetChild(0).transform.childCount; i++) {
                Destroy(buildingUpgradeGuiTab.transform.GetChild(3).transform.GetChild(0).transform.GetChild(i).gameObject);
            }

            var pickList = new List<int>() {0};
            if(selectedBuilding.buildingType == 0 || selectedBuilding.buildingType == 1) {
                //Lumberyard & Quarry
                pickList = new List<int>() {2, 7}; //Prod. Per Sec., Power
            }if(selectedBuilding.buildingType == 3) {
                //Barracks
                pickList = new List<int>() {7, 8}; //Power, Max Training Size
            }

            
            for(int i = 0; i < pickList.Count; i++) { //I choose which details to show.
                //Title
                GameObject newTile = (GameObject)Instantiate(buildingUpgradeGUIPreset.gameObject);
                newTile.transform.SetParent(buildingUpgradeGuiTab.gameObject.transform.GetChild(3).transform.GetChild(0));
                newTile.transform.GetChild(0).GetComponent<TMP_Text>().text = BM.GetBuildingDetailList(pickList[i]);
                newTile.SetActive(true);

                //Data
                GameObject newTile2 = (GameObject)Instantiate(buildingUpgradeGUIDataPreset.gameObject);
                newTile2.transform.SetParent(buildingUpgradeGuiTab.gameObject.transform.GetChild(3).transform.GetChild(0));
                var res = selectedBuilding.buildingDetailsListGet(pickList[i]);
                newTile2.transform.GetChild(0).GetComponent<TMP_Text>().text = res + " + ";
                var bonus = System.Math.Round(float.Parse(selectedBuilding.buildingDetailsListGet(pickList[i], 1)) - float.Parse(res), 2).ToString();
                newTile2.transform.GetChild(1).GetComponent<TMP_Text>().text = bonus;
                newTile2.transform.GetChild(0).transform.position -= new Vector3(20f * bonus.Length, 0f, 0f);
                newTile2.SetActive(true);
            }

            //Level
            buildingUpgradeGuiTab.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = "Level " + selectedBuilding.level.ToString();
            buildingUpgradeGuiTab.transform.GetChild(2).transform.GetChild(2).GetComponent<TMP_Text>().text = (selectedBuilding.level + 1).ToString();

            //Cost
            var basePath = buildingUpgradeGuiTab.gameObject.transform.GetChild(4);

            //Wood
            basePath.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().text = selectedBuilding.GetResourceCost(0, 1).ToString();
            if(selectedBuilding.ResourceCostCheck(0))
                basePath.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.white;
            else {
                basePath.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.red;
            }

            //Stone
            basePath.transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>().text = selectedBuilding.GetResourceCost(1, 1).ToString();
            if(selectedBuilding.ResourceCostCheck(1))
                basePath.transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.white;
            else {
                basePath.transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.red;
            }

            //Time
            buildingUpgradeGuiTab.gameObject.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = selectedBuilding.GetResourceCost(2, 1).ToString();
            if(selectedBuilding.ResourceCostCheck(5))
                buildingUpgradeGuiTab.gameObject.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            else {
                buildingUpgradeGuiTab.gameObject.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
            }

            //Gems
            buildingUpgradeGuiTab.gameObject.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = selectedBuilding.convertTimeToGems(selectedBuilding.GetResourceCost(2, 1)).ToString();
            if(selectedBuilding.ResourceCostCheck(5))
                buildingUpgradeGuiTab.gameObject.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            else {
                buildingUpgradeGuiTab.gameObject.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(0.5f, 0f, 0f);
            }

            buildingUpgradeGuiTab.SetActive(true);
        }
    }

    public void UpgradeBuildingButtonPressed(bool gemsSpentTF) {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        var transactionSuceeded = selectedBuilding.UpgradeBuilding(gemsSpentTF);
        if(transactionSuceeded)
            HideUpgradeGUI();
    }

    public void HideUpgradeGUI() {
        buildingUpgradeGuiTab.SetActive(false);
    }

    public void BuildingCustomButtonPressed() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        if(!selectedBuilding.upgradeInProgress) {
            if(selectedBuilding.buildingType == 0 || selectedBuilding.buildingType == 1) {
                //No functionality.
            }else if(selectedBuilding.buildingType == 3 && !selectedBuilding.trainingInProgressTF) {
                //Barracks
                TrainingGUIOpen();
            }else if(false) {
                //Add calls to show gui for specific buildings.
            }
        }
    }

    public void BuildingBuildButtonPressed() {
        if(!BuildButtonPressedTF) {
            BuildButtonPressedTF = true;
            BuildingSlot building = new BuildingSlot(); //TEMPORARY BuildingSlot to get data. This is the actual clicked building slot.
            building.GM = this.GetComponent<GameManager>();
            building.tempInit();
            building.SetPD(PD);

            //Delete old Instantiations.
            for(int i = 0; i < buildingBuildGUIObj.gameObject.transform.GetChild(1).transform.GetChild(0).transform.childCount; i++) {
                Destroy(buildingBuildGUIObj.transform.GetChild(1).transform.GetChild(0).transform.GetChild(i).gameObject);
            }

            //Create new Item List
            for(int i = 0; i < BM.buildingNameList.Count; i++) { //I choose which details to show.
                if(BM.buildingNameList[i] != "Townhall" && BM.buildingNameList[i] != "Wall") { //Can't ever build more than one of those
                    building.buildingType = i;
                    //Title
                    GameObject newTile = (GameObject)Instantiate(buildingBuildGUIPreset.gameObject);
                    newTile.transform.SetParent(buildingBuildGUIObj.gameObject.transform.GetChild(1).transform.GetChild(0));
                    newTile.transform.localScale = new Vector3(1f, 1f, 1f);
                    newTile.SetActive(true);

                    //Image
                    if(buildingSpriteList[i] != null)
                        newTile.transform.GetChild(1).GetComponent<Image>().sprite = buildingSpriteList[i];
                    else
                        newTile.transform.GetChild(1).GetComponent<Image>().sprite = null;

                    //Name
                    newTile.transform.GetChild(2).GetComponent<TMP_Text>().text = BM.buildingNameList[i];

                    //Wood Cost
                    newTile.transform.GetChild(3).transform.GetChild(2).GetComponent<TMP_Text>().text = building.GetResourceCost(0, 1).ToString(); 
                    //Gets the wood cost, and sets the cost relative to the building slot level. But if the building is not yet built, it SHOULD be level 0 yet. If there is an error with cost amount, it def might be here.
                    if(building.ResourceCostCheck(0, 1))
                        newTile.transform.GetChild(3).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
                    else
                        newTile.transform.GetChild(3).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;

                    //Stone Cost
                    newTile.transform.GetChild(3).transform.GetChild(4).GetComponent<TMP_Text>().text = building.GetResourceCost(1, 1).ToString(); 
                    if(building.ResourceCostCheck(1, 1))
                        newTile.transform.GetChild(3).transform.GetChild(4).GetComponent<TMP_Text>().color = Color.green;
                    else
                        newTile.transform.GetChild(3).transform.GetChild(4).GetComponent<TMP_Text>().color = Color.red;

                    //Button - Color
                    if(building.ResourceCostCheck(0, 1) && building.ResourceCostCheck(1, 1))
                        newTile.transform.GetChild(4).GetComponent<Image>().color = Color.green;
                    else
                        newTile.transform.GetChild(4).GetComponent<Image>().color = Color.red;
                }
            }
            buildingBuildGUIObj.SetActive(true);
        }
    }

    public void BuildingBuildTabClosed() {
        //Close
        buildingBuildGUIObj.SetActive(false);
        BuildButtonPressedTF = false;
    }

    public void BuildSelectedBuilding(GameObject buttonSelected) {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        var childNumber = buttonSelected.transform.parent.GetSiblingIndex();
        if(childNumber > 1) {//Based on buildings removed from BuildingBuildButtonPressed()
            childNumber += 1;
        }
        if(childNumber > 7) {
            childNumber += 1;
        }
        if(selectedBuilding.ResourceCostCheck(0, 1, childNumber) && selectedBuilding.ResourceCostCheck(1, 1, childNumber)) {
            //Recheck able to purchase, incase your resources changed since you opened the tab.
            selectedBuilding.buildingType = childNumber;
            selectedBuilding.BuildBuilding();

            BuildingBuildTabClosed();
        }
    }

    public void mailTabButtonClicked(int type) {
        //0 = personal, 1 = report, 2 = alliance, 3 = system, 4 = sent, 5 = favorites
        GenerateSelectedMailList(type);
    }

    public void mailNewMailButtonClicked() {
        StartNewMail();
    }

    public void mailTrashButtonClicked() {
        if(recentlyLoadedMail.Count > 0) {
            MailM.RemoveSelectedMail(recentlyLoadedMail[selectedMail]);
            GenerateSelectedMailList(mailLastSelectedTab);
        }
    }

    public void mailMessageButtonClicked(GameObject message) {
        var messageIndex = message.transform.GetSiblingIndex();
        selectedMail = messageIndex;
        if(recentlyLoadedMail.Count > 0) 
            GenerateSelectedMail(recentlyLoadedMail[messageIndex]);
    }

    public void mailXButtonClicked() {
        mailFolderObj.SetActive(false);
    }

    public void mailSubmitButtonClicked() {
        SendNewMail();
    }

    public void TrainingGUIClose() {
        UnitTrainingObj.SetActive(false);
    }

    public void TrainingNumberUpdateFromSlider(GameObject slider) {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        UnitTrainingObj.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<TMP_InputField>().text = Mathf.Round(selectedBuilding.GetBarracksTrainingTroopMax() * slider.GetComponent<Slider>().value).ToString();
        TrainingUpdateCosts();
    }

    public void TrainingSelectTroopType(int type) {
        //1 = Infantry, 2 = Artillery, 3 = Armored Vehicles, 4 = Transport
        troopTypeSelected = type;
        TrainingSetStats();
        TrainingUpdateCosts();
    }

    public void TrainingSelectTroopTier(int tier) {
        //1-5
        troopTierSelected = tier;
        TrainingSetStats();
        TrainingUpdateCosts();
    }

    public void TrainingInfoButtonToggle() {
        if(UnitTrainingObj.transform.GetChild(8).gameObject.activeSelf)
            UnitTrainingObj.transform.GetChild(8).gameObject.SetActive(false);
        else   
            UnitTrainingObj.transform.GetChild(8).gameObject.SetActive(true);
    }

    public void TrainingInstantButtonClicked() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        var unitNumFolder = UnitTrainingObj.transform.GetChild(5).GetChild(0);

        var successTF = selectedBuilding.AbleToTrainUnits(troopTypeSelected, troopTierSelected, int.Parse(unitNumFolder.GetChild(1).gameObject.GetComponent<TMP_InputField>().text), true);
        if(successTF)
            UnitTrainingObj.SetActive(false);
    }

    public void TrainingTimedButtonClicked() {
        BuildingSlot selectedBuilding = PM.selectedBuilding;
        var unitNumFolder = UnitTrainingObj.transform.GetChild(5).GetChild(0);

        var successTF = selectedBuilding.AbleToTrainUnits(troopTypeSelected, troopTierSelected, int.Parse(unitNumFolder.GetChild(1).gameObject.GetComponent<TMP_InputField>().text), false);
        if(successTF)
            UnitTrainingObj.SetActive(false);
    }

    public void DeployUnitsToKingdomMap() {
        GameObject newMapUnit = (GameObject)Instantiate(mapUnitPreset);
        newMapUnit.transform.SetParent(mapUnitFolder.transform);
        newMapUnit.SetActive(true);
        newMapUnit.GetComponent<MapUnit>().DeployMapUnit(mapUnitSliderSelectorFolder.GetComponent<DeployLocalCounter>().numberOfSelectedUnits);
        AM.UnitDeployed(mapUnitSliderSelectorFolder.GetComponent<DeployLocalCounter>().numberOfSelectedUnits);
        KM.mapUnitList.Add(newMapUnit);

        mapUnitDeploymentGUI.SetActive(false);
    }

    public void OpenMapUnitDeploymentGUI() {
        playerBaseSelectedObject.SetActive(false);

        //Delete old Details and Data Instantiations
        for(int i = 0; i < mapUnitSliderSelectorFolder.transform.childCount; i++) {
            Destroy(mapUnitSliderSelectorFolder.transform.GetChild(i).gameObject);
        }

        for(int a = 1; a < 5; a++) {
            //Unit Type
            InstantiateMapUnitText(a);
            for(int b = 1; b < 6; b++) {
                for(int c = 0; c < AM.numberOfAvailableUnits.Count; c++) {
                    if(AM.numberOfAvailableUnits[c].unit.type == a && AM.numberOfAvailableUnits[c].unit.type == b  && AM.numberOfAvailableUnits[c].number > 0) {
                        InstantiateMapUnitSelectorSlider(a, b, AM.numberOfAvailableUnits[c].number);
                    }
                }
            }
        }

        var maxMarchSize = PM.returnMaxMarchSize();
        DeployUnitNumberUpdate(mapUnitDeploymentGUI.transform.GetChild(2).gameObject, 0, 0, 0, maxMarchSize);
        DeployUnitSetCommanderPower(mapUnitDeploymentGUI.transform.GetChild(2).gameObject, 0, 0); //Temporary until commanders.
        mapUnitDeploymentGUI.SetActive(true);
    }

    private void InstantiateMapUnitText(int type) {
        GameObject newText = (GameObject)Instantiate(mapUnitTypeTextPreset);
        newText.transform.SetParent(mapUnitSliderSelectorFolder.transform);
        newText.SetActive(true);

        var typeText = "Infantry";
        if(type == 2) {
            typeText = "Artillery";
        }else if(type == 3) {
            typeText = "Armored Vehicle";
        }else if(type == 4) {
            typeText = "Transport";
        }
        newText.transform.GetChild(0).GetComponent<TMP_Text>().text = typeText;
    }

    private void InstantiateMapUnitSelectorSlider(int type, int tier, int num) {
        GameObject newSlider = (GameObject)Instantiate(mapUnitSliderSelectorPreset);
        newSlider.transform.SetParent(mapUnitSliderSelectorFolder.transform);
        newSlider.transform.GetChild(2).GetComponent<DeploySlider>().type = type;
        newSlider.transform.GetChild(2).GetComponent<DeploySlider>().tier = tier;
        newSlider.transform.GetChild(2).GetComponent<DeploySlider>().num = num;
        newSlider.SetActive(true);
        newSlider.transform.GetChild(2).GetComponent<Slider>().maxValue = num;
        newSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = tierBackgroundList[tier];
        newSlider.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = new String('I', (tier + 1));
    }

    public void TrainingSliderUpdateLocalText(GameObject thisSliderGO) {
        var num = thisSliderGO.GetComponent<Slider>().value;
        thisSliderGO.transform.parent.GetChild(3).GetComponent<TMP_InputField>().text = num.ToString();
        thisSliderGO.GetComponent<DeploySlider>().num = (int)num;
    }

    public void TrainingTextUpdateLocalSlider(GameObject thisTextGO) {
        var text = thisTextGO.GetComponent<TMP_InputField>().text;
        var maxMarchSize = PM.returnMaxMarchSize();
        var slider = thisTextGO.transform.parent.GetChild(2);
        if(text.All(char.IsDigit) && text != "") {
            var num = int.Parse(text);
            if(maxMarchSize >= num) {
                slider.GetComponent<Slider>().value = int.Parse(text);
                slider.GetComponent<DeploySlider>().UpdateNumber(int.Parse(text));
            }else {
                thisTextGO.GetComponent<TMP_InputField>().text = maxMarchSize.ToString();
                slider.GetComponent<DeploySlider>().UpdateNumber(maxMarchSize);
            }
        }
        else {
            thisTextGO.GetComponent<TMP_InputField>().text = maxMarchSize.ToString();
            slider.GetComponent<DeploySlider>().UpdateNumber(maxMarchSize);
        }
    }

    public void DeployClearUnitsSelected() {
        mapUnitSliderSelectorFolder.GetComponent<DeployLocalCounter>().ClearSliderNumbers();
    }

    public void DeployCloseGUI() {
        mapUnitDeploymentGUI.SetActive(false);
    }

    public void MapUnitReachedHome() {
        //Set to Button right now. Adjust to when travel time <= 0;
        KM.MapUnitReturnToBase();
        MapUnitActionsObj.SetActive(false);
    }
    //##### End of Button Clicked Events #####
}
