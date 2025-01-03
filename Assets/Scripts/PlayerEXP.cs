using UnityEngine;

public class PlayerEXP
{    
    //##### Beg of Variables #####
    private int currentLevel;
    private int currentEXP;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    public PlayerEXP(int currentLevel, int currentEXP) {
        this.currentLevel = currentLevel;
        this.currentEXP = currentEXP;
    }

    public void AddEXP(int exp) {
        currentEXP += exp;
        var cost = GetEXPToNextLevel(); //Table Equation
        if(currentEXP >= cost) {
            currentEXP -= cost;
            currentLevel += 1;
        }
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    public int GetCurrentLevel() {
        return currentLevel;
    }

    public int GetCurrentEXP() {
        return currentEXP;
    }

    public int GetEXPToNextLevel() {
        return currentLevel ^ 4;
    }

    public float GetEXPToNextLevelPercentage() {
        return (float)currentEXP / Mathf.Pow(currentLevel, 4);
    }
    //##### End of Getters/Setters #####
}
