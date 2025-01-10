using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;

    public int marchSize; //max number of units in a single march
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {

    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public void SetMarchSize(int newMarchSize) {
        marchSize = newMarchSize;
    }
    //##### End of Getters/Setters #####
}
