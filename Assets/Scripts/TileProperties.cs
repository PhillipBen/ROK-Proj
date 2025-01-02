using UnityEngine;

public class TileProperties : MonoBehaviour
{
    //##### Beg of Variables #####
    public Vector2 tileCoords;
    private GameObject tileImageGO;
    public Sprite thickTile;
    public Sprite thinTile;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        tileImageGO = this.transform.GetChild(0).gameObject;
    }

    public void UpdateTileSpriteType(int ID) {
        //0 = Thick tiles, 1 = Thin tiles, 2 = No tiles (GFX)
        if(ID == 0) {
            tileImageGO.GetComponent<SpriteRenderer>().sprite = thickTile;
        }else if (ID == 1) {
            tileImageGO.GetComponent<SpriteRenderer>().sprite = thinTile;
        }else if (ID == 2) {
            tileImageGO.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
