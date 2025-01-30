using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    private UIManager UIM;
    private PlayerResources PR;
    private PlayerMap PM;
    private KingdomMap KM;

    void Start() {
        UIM = this.GetComponent<UIManager>();
        PR = this.GetComponent<PlayerResources>();
        PM = this.GetComponent<PlayerMap>();
        KM = this.GetComponent<KingdomMap>();

        UIM.InitLoadGraphics();
    }

    public void SimUpdateOneSecond(int timePassed) {
        PR.IncPlayerResources();
        PM.BuildingInProgressUpdate(timePassed);
    }

    public void SimUpdatePartialSecond(float timePassed) {
        KM.MoveAllUnits(timePassed);
        KM.AllUnitsAttack(timePassed);
    }

}
