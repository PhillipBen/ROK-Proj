using UnityEngine;

public class KingdomMap : MonoBehaviour
{
    //##### Beg of Variables #####

    //### Beg of Manager Variables ###
    public GameManager GM;
    private CameraMovement CM;
    private UIManager UIM;
    private MapManager MM;
    //### End of Manager Variables ###

    public int width;
    public int height;
    public int maxWidth; //Kingdom Tile Grid
    public int maxHeight; //Kingdom Tile Grid
    public int minWidth; //Kingdom Tile Grid
    public int minHeight; //Kingdom Tile Grid
    private int CL; //Current Length (So far)
    private int CH; //Current Height (So far)
    public float cellSize; //Tile size to pixels
    private Vector3 originPosition; //Not set up to use negative numbers
    public GameObject tilePreset;
    public GameObject tileFolder;
    public GameObject[,] TileArray;
    private GameObject mainCamera;
    private Vector3 cameraGridOffset = new Vector3(15.5f, 8.5f, -10);
    private Vector3 cameraGridCumulativeOffset;
    private GameObject selectedTile;
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start()
    {
        CM = GM.mainCamera.GetComponent<CameraMovement>();
        UIM = GM.GetComponent<UIManager>();
        MM = GM.GetComponent<MapManager>();

        CH = 0;
        CL = 0;
        mainCamera = GM.mainCamera;
        originPosition = mainCamera.transform.position - cameraGridOffset;
        CreateGrid(width, height, cellSize); //Creating the grid

        selectedTile = null;
    }

    public void ShowTiles() {
        tileFolder.SetActive(true);
    }

    public void HideTiles() {
        tileFolder.SetActive(false);
    }

    private void CreateGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        TileArray = new GameObject[width, height];

        for (int i = 0; i < width * height; i++)
        {
            InstantiateGridTile(CL, CH, originPosition.x, originPosition.y);

            if (CL == width - 1)
            {
                CH += 1;
                CL = 0;
            }
            else
            {
                CL += 1;
            }
        }

        UIM.SetCurrentPositionObject((int)(cameraGridCumulativeOffset.x), (int)(cameraGridCumulativeOffset.y));
    }

    
    public void CameraMovedGridFix(Vector3 posChange) {
        //Resets all grid tiles if grid gets off.
        foreach(GameObject tile in TileArray) {
            Destroy(tile);
            TileArray = new GameObject[width, height];
        }
        CH = 0;
        CL = 0;
        for(int i = 0; i < width * height; i++) 
        {
            InstantiateGridTile(CL, CH, originPosition.x + posChange.x, originPosition.y + posChange.y);

            if (CL == width - 1)
            {
                CH += 1;
                CL = 0;
            }
            else
            {
                CL += 1;
            }
        }

        UIM.SetCurrentPositionObject((int)(cameraGridCumulativeOffset.x), (int)(cameraGridCumulativeOffset.y));
    }

    private void InstantiateGridTile(int xPos, int yPos, float realWorldXPos, float realWorldYPos) {
        GameObject newTile = (GameObject)Instantiate(tilePreset.transform.GetChild(0).gameObject);
        newTile.transform.SetParent(tileFolder.gameObject.transform);
        TileArray[xPos, yPos] = newTile;
        newTile.GetComponent<TileProperties>().tileCoords = new Vector2(xPos, yPos);
        var x = (xPos * (cellSize / 100)) + realWorldXPos;
        var y = (yPos * (cellSize / 100)) + realWorldYPos;

        if(CheckInBounds(x, y)) {
            newTile.SetActive(true);
            newTile.transform.position = new Vector3(x, y, 0f);
            newTile.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }else {
           newTile.SetActive(false); 
        }
    }
    
    public void CameraMovedGridUpdate(bool xMoved, bool yMoved, int xMovePosNeg, int yMovePosNeg) {
        cameraGridCumulativeOffset += new Vector3(xMovePosNeg, yMovePosNeg, 0f);
        
        if(xMovePosNeg == 1) { //Pos
            for(int y = 0; y < height; y++) {
                //Remove Left-Bound Tile
                Destroy(TileArray[0, y]);

                //Update other tiles in same x pos in List by -1
                for(int x = 0; x < width - 1; x++) {
                    TileArray[x, y] = TileArray[x + 1, y];
                }

                InstantiateGridTile(width - 1, y, originPosition.x + cameraGridCumulativeOffset.x, originPosition.y + cameraGridCumulativeOffset.y);
            }
        }
        if(xMovePosNeg == -1) {
            for(int y = 0; y < height; y++) {
                //Remove Left-Bound Tile
                Destroy(TileArray[width - 1, y]);

                //Update other tiles in same x pos in List by -1
                for(int x = width - 1; x > 0; x--) {
                    TileArray[x, y] = TileArray[x - 1, y];
                }

                InstantiateGridTile(0, y, originPosition.x + cameraGridCumulativeOffset.x, originPosition.y + cameraGridCumulativeOffset.y);
            }
        }
        if(yMovePosNeg == 1) {
            for(int x = 0; x < width; x++) {
                //Remove Left-Bound Tile
                Destroy(TileArray[x, 0]);

                //Update other tiles in same x pos in List by -1
                for(int y = 0; y < height - 1; y++) {
                    TileArray[x, y] = TileArray[x, y + 1];
                }

                InstantiateGridTile(x, height - 1, originPosition.x + cameraGridCumulativeOffset.x, originPosition.y + cameraGridCumulativeOffset.y);
            }
        }
        if(yMovePosNeg == -1) {
            for(int x = 0; x < width; x++) {
                //Remove Left-Bound Tile
                Destroy(TileArray[x, height - 1]);

                //Update other tiles in same x pos in List by -1
                for(int y = height - 1; y > 0; y--) {
                    TileArray[x, y] = TileArray[x, y - 1];
                }

                InstantiateGridTile(x, 0, originPosition.x + cameraGridCumulativeOffset.x, originPosition.y + cameraGridCumulativeOffset.y);
            }
        } 

        UIM.SetCurrentPositionObject((int)(cameraGridCumulativeOffset.x), (int)(cameraGridCumulativeOffset.y));
    }

    public void MoveToPos(int x, int y) {
        var tempPos = new Vector3(x, y, 0f);
        CM.MoveCamera((int)tempPos.x, (int)tempPos.y);
    }

    public void GridTileClicked(Vector3 clickPos) {
        var tileClickCoords = new Vector2(Mathf.Floor(clickPos.x), Mathf.Floor(clickPos.y));
        if(tileClickCoords.x <= maxWidth && tileClickCoords.x >= minWidth && tileClickCoords.y <= maxHeight && tileClickCoords.y >= minHeight) {
            //Debug.Log("Selected: " + Mathf.Ceil(tileClickCoords.x + cameraGridOffset.x - cameraGridCumulativeOffset.x) + " " + Mathf.Ceil(tileClickCoords.y + cameraGridOffset.y - cameraGridCumulativeOffset.y));
            selectedTile = TileArray[(int)Mathf.Ceil(tileClickCoords.x + cameraGridOffset.x - cameraGridCumulativeOffset.x), (int)Mathf.Ceil(tileClickCoords.y + cameraGridOffset.y - cameraGridCumulativeOffset.y)];
            selectedTile.GetComponent<TileProperties>().TileSelected();
        }//Else: Out of Range!
    }

    private bool CheckInBounds(int x, int y) {
        return x <= maxWidth && x >= minWidth && y <= maxHeight && y >= minHeight ? true : false;
    }
    private bool CheckInBounds(float x, float y) {
        return x <= maxWidth && x >= minWidth && y <= maxHeight && y >= minHeight ? true : false;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
