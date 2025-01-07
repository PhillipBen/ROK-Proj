using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    private UIManager UIM;
    private PlayerResources PR;
    public PlayerMap PM;

    void Start() {
        UIM = this.GetComponent<UIManager>();
        PR = this.GetComponent<PlayerResources>();
        PM = this.GetComponent<PlayerMap>();

        UIM.InitLoadGraphics();
    }

    public void SimUpdate(int timePassed) {
        PR.IncPlayerResources();
        PM.BuildingInProgressUpdate(timePassed);
    }
}
