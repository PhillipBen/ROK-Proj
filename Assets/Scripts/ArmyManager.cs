using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;

    public int marchSize; //max number of units in a single march
    public int[,,] numberOfAvailableUnits = new int[4,5,1] {
        //Number of Units
        {{0},{0},{0},{0},{0}}, //Infantry, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Artillery, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Armored Vehicles, {T1 - T5}
        {{0},{0},{0},{0},{0}} //Transport, {T1 - T5}
    };

    //##### Beg of Unit Stats #####

    public float[,,] unitStats = new float[4,5,9] {
        //Attack, Defense, Health, March Speed, Load, Power, Training Time, Wood, Stone
        {{60, 130, 125, 90, 6, 1, 15, 50, 50},{90, 140, 130, 85, 8, 2, 30, 100, 100},{130, 145, 135, 80, 10, 3, 60, 200, 200},{160, 170, 170, 75, 11, 4, 90, 400, 400},{220, 230, 220, 70, 12, 10, 120, 800, 800}}, //Infantry, {T1 - T5}
        {{65, 120, 120, 95, 4, 1, 15, 60, 40},{100, 120, 125, 90, 6, 2, 30, 110, 90},{140, 125, 130, 85, 8, 3, 60, 180, 220},{170, 150, 165, 80, 9, 4, 90, 420, 380},{240, 220, 220, 75, 10, 10, 120, 820, 780}}, //Artillery, {T1 - T5}
        {{63, 125, 125, 100, 7, 1, 15, 40, 60},{95, 130, 130, 95, 9, 2, 30, 90, 110},{135, 135, 130, 90, 11, 3, 60, 220, 180},{165, 160, 165, 85, 12, 4, 90, 380, 420},{230, 225, 225, 80, 13, 10, 120, 780, 820}}, //Armored Vehicles, {T1 - T5}
        {{30, 60, 90, 125, 15, 0.5f, 10, 40, 40},{45, 75, 100, 120, 18, 1, 20, 90, 90},{75, 80, 105, 115, 21, 1.5f, 45, 180, 180},{90, 100, 120, 110, 24, 2, 75, 380, 380},{120, 130, 130, 105, 30, 4, 100, 780, 780}} //Transport, {T1 - T5}
    };

    public Sprite armyCommanderPreset;

    //##### Beg of Unit Stats #####

    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {

    }

    public void AddUnitsToArmy(int type, int tier, int num) {
        numberOfAvailableUnits[type - 1, tier - 1, 0] = numberOfAvailableUnits[type - 1, tier - 1, 0] + num;
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public void SetMarchSize(int newMarchSize) {
        marchSize = newMarchSize;
    }

    public Vector3 GetUnitsTotalCost(int type, int tier, int num) { //Returns Vector3 because we have two costs (wood, stone, time).
        return new Vector3(unitStats[type, tier, 7] * num, unitStats[type, tier, 8] * num, unitStats[type, tier, 6] * num);
    }
    //##### End of Getters/Setters #####
}
