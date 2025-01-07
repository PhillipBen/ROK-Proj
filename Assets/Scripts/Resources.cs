using UnityEngine;

public class Resources
{
    public long woodAmount;
    public long stoneAmount;
    public long gemsAmount;

    public Resources() {
        
    }

    public void LoadResources(long wood, long stone, long gems) {
        this.woodAmount = wood;
        this.stoneAmount = stone;
        this.gemsAmount = gems;
    }

    public long GetWood() {
        return woodAmount;
    }

    public long GetStone() {
        return stoneAmount;
    }

    public long GetGems() {
        return gemsAmount;
    }
}
