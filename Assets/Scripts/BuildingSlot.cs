using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private UIManager UIM;
    public int cellSize;
    public int buildingGUIType; //0 = Regular Building, 1 = Wall, 2 = Non-buildable Building.
    public int buildingType; //Barracks, City Hall, etc
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        UIM = GM.GetComponent<UIManager>();

        cellSize /= 100;
    }
    public bool IsTileClicked(Vector3 clickPos) {
        var selectedTF = false;
        if(buildingGUIType == 0) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            if(pos.x - (cellSize / 2f) <= coords.x && pos.x + (cellSize / 2f) >= coords.x && pos.y - (cellSize / 2f) <= coords.y && pos.y + (cellSize / 2f) >= coords.y) {
                //Selected
                selectedTF = true;
            }
        }else if (buildingGUIType == 1) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            Vector2 scale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
            //0.2f is base (100 cell match).
            if((pos.x - (cellSize * (scale.x / 0.2f) / 2f)) <= coords.x && (pos.x + (cellSize * (scale.x / 0.2f) / 2f)) >= coords.x && (pos.y - (cellSize * (scale.y / 0.2f) / 2f)) <= coords.y && (pos.y + (cellSize * (scale.y / 0.2f) / 2f)) >= coords.y) {
                //Inside Outer Bounds Box
                var innerDist = 1.7;
                if((pos.x - (cellSize * (scale.x / 0.2f) / 2f) + innerDist) >= coords.x || (pos.x + (cellSize * (scale.x / 0.2f) / 2f) - innerDist) <= coords.x || (pos.y - (cellSize * (scale.y / 0.2f) / 2f) + innerDist) >= coords.y || (pos.y + (cellSize * (scale.y / 0.2f) / 2f) - innerDist) <= coords.y) {
                    //Inside Inner Bounds Box
                    selectedTF = true;
                }
            }
        }

        if(selectedTF) {
            Vector2 thisPos = this.transform.position;
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(thisPos);
            viewportPoint = new Vector2(viewportPoint.x * Screen.width, (viewportPoint.y * Screen.height) - 280);
            UIM.ToggleBuildingOptionsObjSelected(viewportPoint, buildingType);
            return true; //Return true or false based on if the tile was clicked.
        }else {
            return false;
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
