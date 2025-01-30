using UnityEngine;
using System.Collections.Generic;

public class DeployLocalCounter : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private UIManager UIM;
    private PlayerMap PM;
    private ArmyManager AM;

    public List<UnitComplete> numberOfSelectedUnits = new List<UnitComplete>();
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start() {
        UIM = GM.GetComponent<UIManager>();
        PM = GM.GetComponent<PlayerMap>();
        AM = GM.GetComponent<ArmyManager>();
    }

    public void UpdateDeployCounts(int type, int tier, int num) {
        var unitFound = false;
        for(int i = 0; i < numberOfSelectedUnits.Count; i++) {
            if(numberOfSelectedUnits[i].SameUnit(type, tier)) {
                numberOfSelectedUnits[i].number = num;
                unitFound = true;
                break;
            }
        }
        if(!unitFound) {
            var unit = AM.FindUnit(type, tier);
            numberOfSelectedUnits.Add(new UnitComplete(unit, num));
        }

        var maxMarchSize = PM.returnMaxMarchSize();
        var numUnits = 0;
        var totalPower = 0f;
        var totalLoad = 0;

        for(int a = 0; a < numberOfSelectedUnits.Count; a++) {
            numUnits += numberOfSelectedUnits[a].number;
            totalPower += numberOfSelectedUnits[a].unit.power * num;
            totalLoad += numberOfSelectedUnits[a].unit.load * num;
        }
        UIM.DeployUnitNumberUpdate(this.transform.parent.parent.GetChild(2).gameObject, numUnits, totalPower, totalLoad, maxMarchSize);
    }

    public void ClearSliderNumbers() {
        for(int i = 0; i < this.transform.childCount; i++) {
            if(this.transform.GetChild(i).childCount > 1) {
                //Slider
                this.transform.GetChild(i).GetChild(2).GetComponent<DeploySlider>().ClearSlider();
            }
        }
    }
    //##### End of Main Functions #####
}
