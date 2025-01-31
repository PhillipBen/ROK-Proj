using UnityEngine;
using System.Collections.Generic;

public class MapUnit : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private ArmyManager AM;
    private UIManager UIM;
    private KingdomMap KM;

    //Commander commander

    public List<UnitComplete> numberOfHealthyUnits = new List<UnitComplete>();
    public List<UnitComplete> numberOfSlightlyWoundedUnits = new List<UnitComplete>();
    public List<UnitComplete> numberOfSeverelyWoundedUnits = new List<UnitComplete>();
    public Vector2 destination;
    public GameObject objDestination; //An army target. //If friendly, attack first enemy who attacks you. If enemy, attack them once in range.
    public GameObject tempTarget; //A temporary target. Whatever was in range this frame, priority to objDestination
    public Player player; //Who owns this march.
    public bool neutralUnitTF; //If a neutral unit, the owned units will be barbarian. This means their corresponding stats will be different.
    public float moveSpeed;
    public float attackRange = 0.5f; //In tiles

    //Army Resources:
    public Resources resources = new Resources();
    
    public class Combat {
        public GameObject attacker;
        public GameObject defender;

        public Combat(GameObject attacker, GameObject defender) {
            this.attacker = attacker;
            this.defender = defender;
        }
    }
    private List<Combat> combatantsList;


    public int tempIndex = 0;
    public float lastAttack = 0f;
    //Neutral units have thier units saved on infantry.
    //##### End of Variables #####


    //##### Beg of Main Functions #####

    //Sprite Size: We want a 115 x 115 sprite for the commander. 

    void Awake() {
        AM = GM.GetComponent<ArmyManager>();
        UIM = GM.GetComponent<UIManager>();
        KM = GM.GetComponent<KingdomMap>();

        objDestination = null;
        //SetTeamColor();

        //LoadTestBarb();
        //if(neutralUnitTF)
        //    LoadTestBarb();

        //UpdateMovementSpeed();
    }

    void Update() {

        // if(lastAttack == 0) {
        //     lastAttack = Time.time;
        //     Debug.Log("Attack Index: " + tempIndex);
        //     AttackEnemy();
        //     tempIndex += 1;
        // }
        // if(Time.time - lastAttack >= 0.25) {
        //     lastAttack = 0f;
        // }
    }

    public bool AbleToAttackTF(float timePassed) {
        if(Time.time - lastAttack >= 1) {
            //lastAttack = 0f;
            return true;
        }
        return false;
    }

    public void DeployMapUnit(List<UnitComplete> numberOfNewUnits) {
        var spriteObj = this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        spriteObj.GetComponent<SpriteRenderer>().sprite = AM.armyCommanderPreset;
        for(int i = 0; i < numberOfNewUnits.Count; i++) {
            numberOfHealthyUnits.Add(new UnitComplete(numberOfNewUnits[i].unit, numberOfNewUnits[i].number)); //Stupid Deep copy needed so attackers didn't refer to the same unit list.
        }

        var spriteVector2 = new Vector2(spriteObj.GetComponent<SpriteRenderer>().sprite.texture.width, spriteObj.GetComponent<SpriteRenderer>().sprite.texture.height);
        var smallerSide = 0f; 
        if(spriteVector2.x < spriteVector2.y)
            smallerSide = spriteVector2.x;
        else 
            smallerSide = spriteVector2.y;
        var spriteScale = 115 / smallerSide; //Ratio Calculator
        spriteObj.transform.localScale = new Vector3((float)spriteScale, (float)spriteScale, 1);

        resources.LoadResources(0, 0, 0, AM.GetArmyTotalLoad(numberOfHealthyUnits));

        SetTeamColor();
        UpdateMovementSpeed();
    }

    public void MapUnitSelected() {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        viewportPoint = new Vector2(viewportPoint.x * Screen.width, (viewportPoint.y * Screen.height) - 180);
        UIM.MapUnitSelectedOpenGUI(viewportPoint);
    }

    public void MapUnitReturnedToBase() {
        AM.UnitReturnedToBase(numberOfHealthyUnits, numberOfSlightlyWoundedUnits, numberOfSeverelyWoundedUnits);
    }

    private void LoadTestBarb() {
        // numberOfHealthyUnits = new int[4,5,1] { //Not slightly wounded, not severely wounded. These are your fighting force.
        // //Number of Units
        //     {{1000},{0},{0},{0},{0}}, //Infantry, {T1 - T5}
        //     {{0},{0},{0},{0},{1}}, //Artillery, {T1 - T5}
        //     {{0},{0},{0},{0},{0}}, //Armored Vehicles, {T1 - T5}
        //     {{0},{0},{0},{0},{0}} //Transport, {T1 - T5}
        // };
    }

    private void SetTeamColor() {
        if(neutralUnitTF)
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        else 
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color((255 / 255f), (200 / 255f), (94 / 255f));
    }

    public void SetNewDestination(Vector2 destination) {
        this.destination = destination;
    }

    public void SetNewObjDestination(GameObject destination) {
        this.objDestination = destination;
    }

    private void UpdateMovementSpeed() { //Trigger after every unit change (battle).
        //Technically ignores slight and sev wounded units, but whatever.
        var slowestSpeed = 999;
        for(int i = 0; i < numberOfHealthyUnits.Count; i++) {
            if(numberOfHealthyUnits[i].unit.marchSpeed < slowestSpeed) {
                slowestSpeed = numberOfHealthyUnits[i].unit.marchSpeed;
            }
        }
        moveSpeed = slowestSpeed;
    }

    public void MoveUnit(float timePassed) {
        var curPos = this.gameObject.transform.position;
        var target = new Vector2(0f, 0f);
        if(objDestination != null)
            target = new Vector2(objDestination.transform.position.x, objDestination.transform.position.y);
        else
            target = destination;

        var xDis = Mathf.Abs(curPos.x - target.x);
        var yDis = Mathf.Abs(curPos.y - target.y);
        if(xDis > 0.1f || yDis > 0.1f) {
            var newX = 0f;
            var newY = 0f;
            var speedMulti = 0.003f; //This is the multi to determine how fast the stat speed actually is. Modify this if you want a global unit speed inc/dec.
            var xMoveDistancePerc = moveSpeed * timePassed * speedMulti * (xDis / (xDis + yDis));
            var yMoveDistancePerc = moveSpeed * timePassed * speedMulti * (yDis / (xDis + yDis));
            //Debug.Log("%: " + xMoveDistancePerc + " " + yMoveDistancePerc);
            var newAngleY = 0f;
            var newAngleZ = 0f;

            //X
            if(curPos.x > target.x) {
                newX = curPos.x - xMoveDistancePerc;
            }else {
                newX = curPos.x + xMoveDistancePerc;
            }

            //Y
            if(curPos.y > target.y) {
                newY = curPos.y - yMoveDistancePerc;
            }else {
                newY = curPos.y + yMoveDistancePerc;
            }

            //Set Unit Angle
            //Y
            if(curPos.x > target.x) {
                //Moving Right
                newAngleY = 180f;

            }else {
                newAngleY = 0f;
            }

            //Z
            var baseAngle = Mathf.Atan(yDis / xDis) * Mathf.Rad2Deg;
            if(curPos.y > target.y) {
                baseAngle *= -1;
                newAngleZ = Mathf.Max(baseAngle, -45f);
            }else {
                newAngleZ = Mathf.Min(baseAngle, 45f);
            }

            /*
            Left:
            - Y = 0
            - Z = 0

            Left Up:
            - Y = 0
            - Z = 0 - 90, but 45 - 90 = 45.

            Left Down:
            - Y = 0
            - Z = 0 - -90, but -45 - -90 = -45.


            Right:
            - Y = 180
            - Z = 0;

            Right Up:
            - Y = 180
            - Z = 0 - 90, but 45 - 90 = 45.

            Right Down:
            - Y = 180
            - Z = 0 - -90, but -45 - -90 = -45.
            */

            this.gameObject.transform.position = new Vector2(newX, newY);
            this.gameObject.transform.GetChild(2).eulerAngles = new Vector3(0f, newAngleY, newAngleZ);
        }
    }

        public bool HasTargetAvailable() {
            if(IsInRangeOfTarget()) {
                tempTarget = objDestination;
                return true;
            }
            else {
                tempTarget = AltEnemySearch();
                if(tempTarget != null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsInRangeOfTarget() {
            if(objDestination != null && CalcDistance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), new Vector2(objDestination.transform.position.x, objDestination.transform.position.y)) <= attackRange)
                return true;
            else
                return false;
        }

        public GameObject AltEnemySearch() {
            var unitList = KM.mapUnitList;
            for(int i = 0; i < unitList.Count; i++) {
                if(unitList[i] != this.gameObject)
                    if(CalcDistance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), new Vector2(unitList[i].transform.position.x, unitList[i].transform.position.y)) <= attackRange) {
                        //tempTarget = unitList[i]; //A one frame store for the expensive operation to get closest target.
                        return unitList[i];
                    }
            }
            return null;
        }

        public float CalcDistance(Vector2 obj1, Vector2 obj2) {
            return Mathf.Sqrt(Mathf.Pow((obj1.x - obj2.x), 2) + Mathf.Pow((obj1.y - obj2.y), 2));
        }


        /*  Attack Rules:
        - Normal Attack: 1 v 1, both deal normal attack to each other at the same time. 
        - Counter Attack: Any unit not targeted for Normal Attack, should be counter attacked.
        -- 75% Damage of normal attack.

        Attack Order:
        - First Army Attacks You (Your Target): They will hit you, you respond with the same amount of damage as your army before that hit.
        - Second + Army Attacks You: They will hit you, you respond with counter attack damage (75%).

        Targeting: 
        - If you are in range of your target, deal normal attack. All else is counter.
        - If you are intercepted on the way to your target, deal normal attack to that first attacker.
        - If you are intercepted, then reach your target, switch normal attack to target, and counter to interceptor.

        Attack Speed:
        - What if the enemy army has a faster attack speed than you?
        -- Not deal with this?
        --- YES. Don't deal with this.

        In Combat:
        - Once a unit is out of combat, any resources lost due to unit loss are not removed until the unit is no longer is in combat
        -- This is so the attacker, ONLY if a full defeat has succeeded, can gain 50% of the resources.

        To get IN combat:
        - The first hit must connect from the attacker.

        To get OUT of combat:
        - The attacker is defeated, the attacker stops targeting that army, the defender reaches a city (theirs, ally) or allied building, or the attacker gets out of 5 unit range.

        */
        public void AttackEnemy() { 
            //Normal Attack Initiation
            lastAttack = Time.time;

            var combatExistsTF = false;
            for(int i = 0; i < combatantsList.Count; i++) {
                if(combatantsList[i].defender == tempTarget) {
                    combatExistsTF = true;
                }
            }
            if(!combatExistsTF) {
                combatantsList.Add(new Combat(this.gameObject, tempTarget));
                tempTarget.GetComponent<MapUnit>().DefenderBeginCombat(this.gameObject);
            }
            var target = tempTarget;
            tempTarget = null; //Assign to null for next frame

            var damageCalc = CalculateAttackDamage();
            target.GetComponent<MapUnit>().AttackedByEnemy(damageCalc.x, damageCalc.y, this.gameObject);
        }

        public void AttackedByEnemy(float avgEnemyAtt, float totalDMGTook, GameObject origAttackerGO) {
            //Normal Attack Damage Taken
            
            //PrintCurrentArmyUnitStats();
            //Calc damage response before taking damage, so 1v1s are fair, (counterattack will also be more fair).
            var damageCalc = CalculateAttackDamage();
            damageCalc = new Vector2(damageCalc.x, damageCalc.y * 0.75f); //Calculate Counter Attack Damage

            AttackTakeDamage(avgEnemyAtt, totalDMGTook, origAttackerGO);

            origAttackerGO.GetComponent<MapUnit>().CounterAttackedByEnemy(damageCalc.x, damageCalc.y, this.gameObject);
        }

        public void CounterAttackedByEnemy(float avgEnemyAtt, float totalDMGTook, GameObject origDefenderGO) {
            AttackTakeDamage(avgEnemyAtt, totalDMGTook, origDefenderGO);
        }

        private Vector2 CalculateAttackDamage() {
            //var combatEnvironment = 0; //0 = field, 1 = city

            var totalEnemyHealthyUnits = 0;
            for(int a = 0; a < numberOfHealthyUnits.Count; a++) {
                totalEnemyHealthyUnits += numberOfHealthyUnits[a].number;
            }

            var avgEnemyAtt = 0f;
            for(int a = 0; a < numberOfHealthyUnits.Count; a++) {
                avgEnemyAtt += numberOfHealthyUnits[a].unit.attack * (numberOfHealthyUnits[a].number / totalEnemyHealthyUnits);
            }

            var damagePercent = 0.2f;
            var totalDMGTook = damagePercent * avgEnemyAtt * totalEnemyHealthyUnits;
            return new Vector2(avgEnemyAtt, totalDMGTook);
        }

        private void AttackTakeDamage(float avgEnemyAtt, float totalDMGTook, GameObject attackerGO) {
            //var combatEnvironment = 0; //0 = field, 1 = city

            var totalHealthyUnits = 0;
            for(int a = 0; a < numberOfHealthyUnits.Count; a++) {
                totalHealthyUnits += numberOfHealthyUnits[a].number;
            }

            var ratioUnitsToTotal = 0f;
            var ratioOfDefenseToEnemyAttack = 0f;

            var unitsSurvived = false;
            for(int a = 0; a < numberOfHealthyUnits.Count; a++) {
                if(numberOfHealthyUnits[a].number > 0) {
                    var unit = numberOfHealthyUnits[a].unit;
                    ratioUnitsToTotal = numberOfHealthyUnits[a].number / totalHealthyUnits;
                    ratioOfDefenseToEnemyAttack = avgEnemyAtt / numberOfHealthyUnits[a].unit.defense;
                    //Debug.Log("Hit: DMG Took: " + totalDMGTook + " R Units: " + ratioUnitsToTotal + " R Def: " + ratioOfDefenseToEnemyAttack + " UHealth: " + unit.health);
                    var totalHitUnits = Mathf.Ceil((totalDMGTook * ratioUnitsToTotal * ratioOfDefenseToEnemyAttack) / unit.health);
                    //Debug.Log("Hit: Type: " + unit.type + " Tier: " + unit.tier + " Num: " + totalHitUnits);
                    //Debug.Log("B4: " + numberOfHealthyUnits[a].number);
                    numberOfHealthyUnits[a].number -= (int)totalHitUnits;
                    //Debug.Log("After: " + numberOfHealthyUnits[a].number);
                    if(numberOfHealthyUnits[a].number > 0)
                        unitsSurvived = true;
                }
            }

            if(!unitsSurvived) {
                attackerGO.GetComponent<MapUnit>().EnemyUnitDefeated(resources, this.gameObject); //Before this unit's resources are removed
                DefeatUnit();
            }
        }

        private void PrintCurrentArmyUnitStats() {
            Debug.Log("Friendly Units:");
            Debug.Log("Healthy:");
            for(int a = 0; a < numberOfHealthyUnits.Count; a++) {
                Debug.Log("Type: " + numberOfHealthyUnits[a].unit.type + "Tier: " + numberOfHealthyUnits[a].unit.tier + "Num: " + numberOfHealthyUnits[a].number);
            }
            Debug.Log("Slightly Wounded:");
            for(int a = 0; a < numberOfSlightlyWoundedUnits.Count; a++) {
                Debug.Log("Type: " + numberOfSlightlyWoundedUnits[a].unit.type + "Tier: " + numberOfSlightlyWoundedUnits[a].unit.tier + "Num: " + numberOfSlightlyWoundedUnits[a].number);
            }
            Debug.Log("Severely Wounded:");
            for(int a = 0; a < numberOfSeverelyWoundedUnits.Count; a++) {
                Debug.Log("Type: " + numberOfSlightlyWoundedUnits[a].unit.type + "Tier: " + numberOfSlightlyWoundedUnits[a].unit.tier + "Num: " + numberOfSlightlyWoundedUnits[a].number);
            }
        }

        private void DefeatUnit() {
            resources.DefenderDefeated();
            KM.mapUnitList.Remove(this.gameObject);
            resources.DefenderDefeated();
            //Run back to home base.
        }

        public void EnemyUnitDefeated(Resources resources, GameObject enemy) {
            this.resources.AttackerWinResources(resources);
            EndCombat(enemy);
        }

        public bool UnitInCombatTF() {
            if(combatantsList.Count > 0)
                return true;
            else
                return false;
        }

        public void DefenderBeginCombat(GameObject attacker) {
            //Activated due to an enemy attack.
            combatantsList.Add(new Combat(attacker, this.gameObject));
        }

        public void AttackerUnableToPursue(GameObject attacker) {
            //Called when the defender stops targetting you or is out of range.
            //NOTE: The attacker might have targetted you but not actually attacked & started combat yet, so that combat still might not exist.
            EndCombat(attacker);
        }

        private void EndCombat(GameObject enemy) {
            //End combat, but you might be the overall combat attacker or defender
            for(int i = 0; i < combatantsList.Count; i++) {
                if(combatantsList[i].attacker == enemy && combatantsList[i].defender == this.gameObject || combatantsList[i].attacker == this.gameObject && combatantsList[i].defender == enemy) {
                    combatantsList.RemoveAt(i);
                    resources.NewMaxResources(AM.GetArmyTotalLoad(numberOfHealthyUnits));
                }
            }
        }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
