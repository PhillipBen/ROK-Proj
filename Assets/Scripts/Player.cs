using UnityEngine;

public class Player
{
    public User user;
    public PlayerEXP playerEXP;

    //Resources
    public long woodAmount;
    public long stoneAmount;
    public long gemsAmount;

    public Player(User user, PlayerEXP playerEXP) {
        this.user = user;
        this.playerEXP = playerEXP;
    }

    public void LoadResourecs(long wood, long stone, long gems) {
        this.woodAmount = wood;
        this.stoneAmount = stone;
        this.gemsAmount = gems;
    }
}
