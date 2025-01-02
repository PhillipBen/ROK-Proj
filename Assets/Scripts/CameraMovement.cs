using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //##### Beg of Variables #####
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    [SerializeField]
    private float zoomStep, minCamSize, MaxCamSize;
    private bool MouseDownTF;
    public Vector3 lastFullTileUpdate;
    public GameManager GM;
    private KingdomMap KingdomMap;
    private bool cellFix = false;
    private float startMoveTime;

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    public void Start()
    {
        MouseDownTF = false;
        lastFullTileUpdate = this.transform.position;
        this.GM = GM.GetComponent<GameManager>();
        this.KingdomMap = GM.GetComponent<KingdomMap>();
        startMoveTime = Time.time;
    }

    private void Update()
    {
        PanCamera();
        CheckDistanceTraveled();
    }

    private void PanCamera()
    {
        //Save position of mouse in world space when dragging starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            if (!MouseDownTF)
                dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
            MouseDownTF = false;

        //calculate distance between drag origin and world point if mouse is still held down
        if (Input.GetMouseButton(0))
        {
            if (!MouseDownTF)
            {
                Vector3 distance = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

                cam.transform.position += distance;
            }
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, MaxCamSize);
    }
    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, MaxCamSize);
    }

    void OnGUI()
    {
        if (Input.mouseScrollDelta.y == 1)
        {
            ZoomIn();
        }
        else if (Input.mouseScrollDelta.y == -1)
        {
            ZoomOut();
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    private void CheckDistanceTraveled() {
        if(this.KingdomMap.getMapZoomingID() == 2) { //If in Kingdom map mode
            var thisPos = this.transform.position;
            var xMoved = thisPos.x - lastFullTileUpdate.x >= 1 || thisPos.x - lastFullTileUpdate.x <= -1 ? true : false;
            var yMoved = thisPos.y - lastFullTileUpdate.y >= 1 || thisPos.y - lastFullTileUpdate.y <= -1 ? true : false;
            var xMovePosNeg = 0; //True = pos, False = neg;
            var yMovePosNeg = 0;
            if(xMoved) {
                xMovePosNeg = thisPos.x - lastFullTileUpdate.x >= 1 ? 1 : -1;
            }
            if(yMoved) {
                yMovePosNeg = thisPos.y - lastFullTileUpdate.y >= 1 ? 1 : -1;
            }
            if(xMoved || yMoved) {
                lastFullTileUpdate += new Vector3(xMovePosNeg, yMovePosNeg, 0f);
                this.KingdomMap.CameraMovedGridUpdate(xMoved, yMoved, xMovePosNeg, yMovePosNeg);
            }

            if((xMoved || yMoved) && cellFix == false ) {
                startMoveTime = Time.time;
                cellFix = true;
            }else if(Time.time - startMoveTime >= 1 && cellFix){
                this.KingdomMap.CameraMovedGridFix(lastFullTileUpdate);
                cellFix = false;
            }
        }
    }

    public void MoveCamera(int x, int y) {
        this.transform.position = new Vector3(x, y, -10f);
    }
    //##### End of Getters/Setters #####
}
