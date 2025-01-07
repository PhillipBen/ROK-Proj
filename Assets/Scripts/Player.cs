using UnityEngine;

public class Player
{
    public User user;
    public PlayerEXP playerEXP;
    public Resources playerResources = new Resources();

    public Player(User user, PlayerEXP playerEXP) {
        this.user = user;
        this.playerEXP = playerEXP;
    }
}
