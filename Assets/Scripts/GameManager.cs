using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    private UIManager UIM;
    private PlayerData PD;

    void Start() {
        UIM = this.GetComponent<UIManager>();
        PD = this.GetComponent<PlayerData>();

        UIM.InitLoadGraphics(PD);
    }
}
