using UnityEngine;

public class UnitComplete
{
    public Unit unit;
    public int number;

    public UnitComplete(Unit unit, int number) {
        this.unit = unit;
        this.number = number;
    }

    public bool SameUnit(int type, int tier) {
        if(unit.type == type && unit.tier == tier ) {
            return true;
        }else {
            return false;
        }
    }

    public bool SameUnit(Unit unit) {
        if(this.unit.type == unit.type && this.unit.tier == unit.tier ) {
            return true;
        }else {
            return false;
        }
    }
}
