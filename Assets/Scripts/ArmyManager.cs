using UnityEngine;
using System.Collections.Generic;

public class ArmyManager : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;

    public int marchSize; //max number of units in a single march
    public List<UnitComplete> numberOfAvailableUnits = new List<UnitComplete>();
    public List<UnitComplete> numberOfSeverelyWoundedUnits = new List<UnitComplete>(); //Units that are in the hospital
    public int totalNumberOfSeverelyWoundedUnits; //Total number of units that are hospitalized (total of 3D array)
    public int maxNumberOfSeverelyWoundedUnits; //Max number possible. Any extra are dead.
    public Sprite armyCommanderPreset;
    public List<Unit> scriptableUnitList;

    //##### End of Variables #####


    //##### Beg of Main Functions #####

    void Start() {
        var unitInfOne = FindUnit(1, 1);
        numberOfAvailableUnits.Add(new UnitComplete(unitInfOne, 2000));
    }

    public Unit FindUnit(int type, int tier) {
        for(int i = 0; i < scriptableUnitList.Count; i++) {
            if(scriptableUnitList[i].SameUnit(type, tier))
                return scriptableUnitList[i];
        }
        Debug.Log("Error: Unit not found");
        return null;
    }

    public void AddUnitsToArmy(int type, int tier, int num) {
        //numberOfAvailableUnits[type - 1, tier - 1, 0] = numberOfAvailableUnits[type - 1, tier - 1, 0] + num;
        for(int x = 0; x < numberOfAvailableUnits.Count; x++) {
            if(numberOfAvailableUnits[x].SameUnit(type, tier)) {
                numberOfAvailableUnits[x].number += num;
                break;
            }
        }
        numberOfAvailableUnits.Add(new UnitComplete(FindUnit(type, tier), num));
    }

    public void UnitDeployed(List<UnitComplete> numberOfNewUnits) {
        for(int a = 0; a < numberOfNewUnits.Count; a++) {
            var unitFound = false;
            for(int b = 0; b < numberOfAvailableUnits.Count; b++) {
                if(numberOfNewUnits[a].SameUnit(numberOfAvailableUnits[b].unit)) {
                    numberOfAvailableUnits[b].number -= numberOfNewUnits[a].number;
                    unitFound = true;
                    break;
                }
            }
            if(!unitFound) {
                Debug.Log("Unit Deployed without existing in army beforehand.");
            }
        }
    }

    //FIX
    public void UnitReturnedToBase(List<UnitComplete> numberOfHealthyUnits, List<UnitComplete> numberOfSlightlyWoundedUnits, List<UnitComplete> numberOfSeverelyWoundedUnits) {
        for(int a = 0; a < numberOfAvailableUnits.Count; a++) {
            //Return Healthy and slightly wounded
            numberOfAvailableUnits[a].number += numberOfHealthyUnits[a].number + numberOfSlightlyWoundedUnits[a].number;
            //Check sev wounded.
            if(numberOfSeverelyWoundedUnits[a].number + totalNumberOfSeverelyWoundedUnits < maxNumberOfSeverelyWoundedUnits) {
                totalNumberOfSeverelyWoundedUnits += numberOfSeverelyWoundedUnits[a].number;
                this.numberOfSeverelyWoundedUnits[a].number += numberOfSeverelyWoundedUnits[a].number;
            }else {
                //Not enough space for all, only part or none
                var spaceRemaining = maxNumberOfSeverelyWoundedUnits - totalNumberOfSeverelyWoundedUnits;
                this.numberOfSeverelyWoundedUnits[a].number += spaceRemaining;
                totalNumberOfSeverelyWoundedUnits = maxNumberOfSeverelyWoundedUnits;
            }
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public void SetMarchSize(int newMarchSize) {
        marchSize = newMarchSize;
    }

    public Vector3 GetUnitsTotalCost(int type, int tier, int num) { //Returns Vector3 because we have two costs (wood, stone, time).
        var unit = FindUnit(type, tier);
        return new Vector3(unit.woodCost * num, unit.stoneCost * num, unit.trainingTime * num);
    }

    public int GetArmyTotalLoad(List<UnitComplete> unitList) {
        int maxLoad = 0;
        for(int i = 0; i < unitList.Count; i++) {
            maxLoad += unitList[i].unit.load * unitList[i].number;
        }
        return maxLoad;
    }
    //##### End of Getters/Setters #####
}
