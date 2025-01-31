using UnityEngine;

public class Resources
{
    private long woodAmount;
    private long stoneAmount;
    private long gemsAmount;

    //-1 means NO maximum //Gems has no limit because the carry capacity is so low + valuable
    private long singleMaxLoad;

    public Resources() {
        
    }

    public void LoadResources(long wood, long stone, long gems, long singleMaxLoad = -1) {
        this.woodAmount = wood;
        this.stoneAmount = stone;
        this.gemsAmount = gems;
        this.singleMaxLoad = singleMaxLoad;
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

    private long GetTotalResources() {
        return woodAmount + stoneAmount;
    }

    public void AddWood(long wood) {
        if(singleMaxLoad != -1) {
            woodAmount += wood;
        }else {
            if(GetTotalResources() + wood <= singleMaxLoad) {
                woodAmount += wood;
            }else {
                woodAmount += singleMaxLoad - GetTotalResources();
            }
        }
    }

    public void AddStone(long stone) {
        if(singleMaxLoad != -1) {
            stoneAmount += stone;
        }else {
            if(GetTotalResources() + stone <= singleMaxLoad) {
                stoneAmount += stone;
            }else {
                stoneAmount += singleMaxLoad - GetTotalResources();
            }
        }
    }

    public void AddGems(long gems) {
        if(singleMaxLoad != -1) {
            gemsAmount += gems;
        }else {
            if(GetTotalResources() + gems <= singleMaxLoad) {
                gemsAmount += gems;
            }else {
                gemsAmount += singleMaxLoad - GetTotalResources();
            }
        }
    }

    public void RemoveWood(long wood) {
        //No negative check yet... if needed, add.
        woodAmount -= wood;
    }

    public void RemoveStone(long stone) {
        //No negative check yet... if needed, add.
        stoneAmount -= stone;
    }

    public void RemoveGems(long gems) {
        //No negative check yet... if needed, add.
        gemsAmount -= gems;
    }

    public void AttackerWinResources(Resources resources) {
        AddWood(resources.GetWood());
        AddStone(resources.GetStone());
        AddGems(resources.GetGems());
    }

    public void DefenderDefeated() {
        LoadResources(0, 0, 0);
    }

    public void NewMaxResources(long singleMaxLoad) {
        this.singleMaxLoad = singleMaxLoad;
        if(GetTotalResources() > singleMaxLoad) {
            var keepRatio = (long)singleMaxLoad / GetTotalResources();
            woodAmount *= keepRatio;
            stoneAmount *= keepRatio;
        }
    }
}
