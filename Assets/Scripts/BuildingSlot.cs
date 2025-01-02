using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    public int cellSize;
    public int buildingType; //0 = Regular Building, 1 = Wall, 2 = Non-buildable Building.
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        cellSize /= 100;
    }
    public void IsTileClicked(Vector3 clickPos) {
        if(buildingType == 0) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            if(pos.x - (cellSize / 2f) <= coords.x && pos.x + (cellSize / 2f) >= coords.x && pos.y - (cellSize / 2f) <= coords.y && pos.y + (cellSize / 2f) >= coords.y) {
                //Selected
            }
        }else if (buildingType == 1) {
            var coords = new Vector2(clickPos.x, clickPos.y);
            Vector3 pos = this.transform.position;
            Vector2 scale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
            //0.2f is base (100 cell match).
            if((pos.x - (cellSize * (scale.x / 0.2f) / 2f)) <= coords.x && (pos.x + (cellSize * (scale.x / 0.2f) / 2f)) >= coords.x && (pos.y - (cellSize * (scale.y / 0.2f) / 2f)) <= coords.y && (pos.y + (cellSize * (scale.y / 0.2f) / 2f)) >= coords.y) {
                //Inside Outer Bounds Box
                var innerDist = 1.7;
                if((pos.x - (cellSize * (scale.x / 0.2f) / 2f) + innerDist) >= coords.x || (pos.x + (cellSize * (scale.x / 0.2f) / 2f) - innerDist) <= coords.x || (pos.y - (cellSize * (scale.y / 0.2f) / 2f) + innerDist) >= coords.y || (pos.y + (cellSize * (scale.y / 0.2f) / 2f) - innerDist) <= coords.y) {
                    //Inside Inner Bounds Box
                    //Selected
                }
            }
        }

        
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
