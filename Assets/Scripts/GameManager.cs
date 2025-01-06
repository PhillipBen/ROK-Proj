using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    private UIManager UIM;
    private PlayerResources PR;

    void Start() {
        UIM = this.GetComponent<UIManager>();
        PR = this.GetComponent<PlayerResources>();

        UIM.InitLoadGraphics();
    }

    public void SimUpdate() {
        PR.IncPlayerResources();
    }
}
