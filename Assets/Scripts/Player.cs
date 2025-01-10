using UnityEngine;

public class Player
{
    public User user;
    public PlayerEXP playerEXP;
    public Resources playerResources = new Resources();
    public string tempAllianceName; //Replace with link to alliance class/name whenever that becomes available.
    public Sprite playerIcon;

    public Player(User user, PlayerEXP playerEXP, string tempAllianceName, Sprite playerIcon) {
        this.user = user;
        this.playerEXP = playerEXP;
        this.tempAllianceName = tempAllianceName;
        this.playerIcon = playerIcon;
    }
}
