using UnityEngine;

public class MapUnit : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private ArmyManager AM;

    //Commander commander
    public int[,,] numberOfHealthUnits = new int[4,5,1] { //Not slightly wounded, not severely wounded. These are your fighting force.
        //Number of Units
        {{0},{0},{0},{0},{0}}, //Infantry, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Artillery, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Armored Vehicles, {T1 - T5}
        {{0},{0},{0},{0},{0}} //Transport, {T1 - T5}
    };
    public int[,,] numberOfSlightlyWoundedUnits = new int[4,5,1] { 
        //Number of Units
        {{0},{0},{0},{0},{0}}, //Infantry, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Artillery, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Armored Vehicles, {T1 - T5}
        {{0},{0},{0},{0},{0}} //Transport, {T1 - T5}
    };
    public int[,,] numberOfSeverelyWoundedUnits = new int[4,5,1] { 
        //Number of Units
        {{0},{0},{0},{0},{0}}, //Infantry, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Artillery, {T1 - T5}
        {{0},{0},{0},{0},{0}}, //Armored Vehicles, {T1 - T5}
        {{0},{0},{0},{0},{0}} //Transport, {T1 - T5}
    };
    public Vector2 destination;
    public Player player; //Who owns this march.
    public Resources ownedResources;
    public bool neutralUnitTF; //If a neutral unit, the owned units will be barbarian. This means their corresponding stats will be different.
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    //Sprite Size: We want a 115 x 115 sprite for the commander. 

    void Start() {
        AM = GM.GetComponent<ArmyManager>();

        DeployMapUnit();
    }

    public void DeployMapUnit() {
        var spriteObj = this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

        spriteObj.GetComponent<SpriteRenderer>().sprite = AM.armyCommanderPreset;

        var spriteVector2 = new Vector2(spriteObj.GetComponent<SpriteRenderer>().sprite.texture.width, spriteObj.GetComponent<SpriteRenderer>().sprite.texture.height);
        var smallerSide = 0f; 
        if(spriteVector2.x < spriteVector2.y)
            smallerSide = spriteVector2.x;
        else 
            smallerSide = spriteVector2.y;
        var spriteScale = 115 / smallerSide; //Ratio Calculator
        spriteObj.transform.localScale = new Vector3((float)spriteScale, (float)spriteScale, 1);
    }




    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
