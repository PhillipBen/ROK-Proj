using UnityEngine;

[CreateAssetMenu(fileName = "newUnit", menuName = "unit")]
public class Unit : ScriptableObject
{
    public int type; //Infantry, Artillery, ArmoredVehicles, Transport, Barbarian
    public int tier;
    //Attack, Defense, Health, March Speed, Load, Power, Training Time, Wood, Stone
    public int attack;
    public int defense;
    public int health;
    public int marchSpeed;
    public int load;
    public float power;
    public int trainingTime;
    public int woodCost;
    public int stoneCost;

    public bool SameUnit(int type, int tier) {
        if(this.type == type && this.tier == tier ) {
            return true;
        }else {
            return false;
        }
    }
}
