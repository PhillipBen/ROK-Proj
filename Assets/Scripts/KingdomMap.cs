using UnityEngine;

public class KingdomMap : MonoBehaviour
{
    //##### Beg of Variables #####

    //0 = Kingdom List, 1 = Kingdom, 2 = Home
    public int mapZoomingID;
    public int width;
    public int height;
    public int maxWidth;
    public int maxHeight;
    public int minWidth;
    public int minHeight;
    private int CL; //Current Length (So far)
    private int CH; //Current Height (So far)
    public float cellSize;
    private Vector3 originPosition; //Not set up to use negative numbers
    public GameObject tilePreset;
    public GameObject tileFolder;
    public GameObject[,] TileArray;
    public GameObject camera;
    private Vector3 cameraGridOffset = new Vector3(15.5f, 8.5f, -10);
    private Vector3 cameraGridCumulativeOffset;
    public GameManager GM;
    private CameraMovement CM;
    private UIManager UIM;
    private GameObject selectedTile;
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start()
    {
        CM = GM.mainCamera.GetComponent<CameraMovement>();
        UIM = GM.GetComponent<UIManager>();

        CH = 0;
        CL = 0;
        originPosition = camera.transform.position - cameraGridOffset;
        CreateGrid(width, height, cellSize); //Creating the grid

        selectedTile = null;
    }

    void Update() {
        if(mapZoomingID == 2 && tileFolder.activeSelf == false) {
            tileFolder.SetActive(true);
        }else if(mapZoomingID != 2) {
            tileFolder.SetActive(false);
        }
    }

    private void CreateGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        TileArray = new GameObject[width, height];

        for (int i = 0; i < width * height; i++)
        {
            
            GameObject newTile = (GameObject)Instantiate(tilePreset.transform.GetChild(0).gameObject);
            newTile.SetActive(true);
            newTile.transform.SetParent(tileFolder.gameObject.transform);
            TileArray[CL, CH] = newTile;
            newTile.GetComponent<TileProperties>().tileCoords = new Vector2(CL, CH);
            var x = (CL * (cellSize / 100));
            var y = (CH * (cellSize / 100));

            if(CheckInBounds(x + originPosition.x, y + originPosition.y)) {
                newTile.transform.position = new Vector3(x, y, 0) + originPosition;
                newTile.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            }else {
                newTile.SetActive(false);
            }

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
        for(int i = 0; i < width * height; i++) {
            
            GameObject newTile = (GameObject)Instantiate(tilePreset.transform.GetChild(0).gameObject);
            newTile.SetActive(true);
            newTile.transform.SetParent(tileFolder.gameObject.transform);
            TileArray[CL, CH] = newTile;
            newTile.GetComponent<TileProperties>().tileCoords = new Vector2(CL, CH);
            var x = (CL * (cellSize / 100));
            var y = (CH * (cellSize / 100));

            if(CheckInBounds(x + originPosition.x + posChange.x, y + originPosition.y + posChange.y)) {
                newTile.transform.position = new Vector3(x, y, 0) + originPosition + posChange;
                newTile.transform.position = new Vector3(newTile.transform.position.x, newTile.transform.position.y, 0);
                newTile.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            }else {
                newTile.SetActive(false);
            }

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
    
    private void CreateReplacementTile(int xPos, int yPos, int xMovePosNeg, int yMovePosNeg) {
        
        GameObject newTile = (GameObject)Instantiate(tilePreset.transform.GetChild(0).gameObject);
        newTile.transform.SetParent(tileFolder.gameObject.transform);
        TileArray[xPos, yPos] = newTile;
        newTile.GetComponent<TileProperties>().tileCoords = new Vector2(xPos, yPos);
        var x = (xPos * (cellSize / 100));
        var y = (yPos * (cellSize / 100));

        if(CheckInBounds(x + originPosition.x + cameraGridCumulativeOffset.x, y + originPosition.y + cameraGridCumulativeOffset.y)) {
            newTile.SetActive(true);
            newTile.transform.position = new Vector3(x, y, 0) + originPosition + cameraGridCumulativeOffset;
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

                CreateReplacementTile(width - 1, y, xMovePosNeg, 0);
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

                CreateReplacementTile(0, y, xMovePosNeg, 0);
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

                CreateReplacementTile(x, height - 1, 0, yMovePosNeg);
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

                CreateReplacementTile(x, 0, 0, yMovePosNeg);
            }
        } 

        UIM.SetCurrentPositionObject((int)(cameraGridCumulativeOffset.x), (int)(cameraGridCumulativeOffset.y));
    }

    public void ToggleKingdomZooming() {
            if(mapZoomingID == 2) {
                mapZoomingID = 3;
            }else {
                mapZoomingID = 2;
            }
        }

    public void MoveToPos(int x, int y) {
        var tempPos = new Vector3(x, y, 0f);// + cameraGridOffset;
        CM.MoveCamera((int)tempPos.x, (int)tempPos.y);
    }

    public void GridTileClicked(Vector3 clickPos) {
        var tileClickCoords = new Vector2(Mathf.Floor(clickPos.x), Mathf.Floor(clickPos.y));
        if(tileClickCoords.x <= maxWidth && tileClickCoords.x >= minWidth && tileClickCoords.y <= maxHeight && tileClickCoords.y >= minHeight) {
            Debug.Log("Selected: " + Mathf.Ceil(tileClickCoords.x + cameraGridOffset.x - cameraGridCumulativeOffset.x) + " " + Mathf.Ceil(tileClickCoords.y + cameraGridOffset.y - cameraGridCumulativeOffset.y));
            selectedTile = TileArray[(int)Mathf.Ceil(tileClickCoords.x + cameraGridOffset.x - cameraGridCumulativeOffset.x), (int)Mathf.Ceil(tileClickCoords.y + cameraGridOffset.y - cameraGridCumulativeOffset.y)];
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
    public void setMapZoomingID(int mapZoomingID) {
        this.mapZoomingID = mapZoomingID;
    }
    public int getMapZoomingID() {
        return this.mapZoomingID;
    }
    //##### End of Getters/Setters #####
}
