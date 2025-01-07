using UnityEngine;

public class TileProperties : MonoBehaviour
{
    //##### Beg of Variables #####

    //### Beg of Manager Variables ###
    public GameManager GM;
    private CameraMovement CM;
    private UIManager UIM;
    //### End of Manager Variables ###
    public Vector2 tileCoords;
    private GameObject tileImageGO;
    public Sprite thickTile;
    public Sprite thinTile;
    public int playerID;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        CM = GM.mainCamera.GetComponent<CameraMovement>();
        UIM = GM.GetComponent<UIManager>();

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

    public void TileSelected() {
        UIM.TurnOnCitySelected(playerID);
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
