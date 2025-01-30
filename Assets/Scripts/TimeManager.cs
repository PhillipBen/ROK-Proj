using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public GameManager GM;

    public float lastSimTime; //Saves time since last sim update. Once >= 1, call simUpdate.
    public float lastPartialSimTime; //For the 1/60 Update
    public int timeMulti; //Base is in the inspector (1.0).
    List<int> timeMultiList = new List<int> {1, 2, 5, 10, 15, 30, 60};
    

    void Update()
    {
        lastSimTime += Time.deltaTime;
        lastPartialSimTime += Time.deltaTime;
        if(lastPartialSimTime >= 0.01666f) {
            lastPartialSimTime -= 0.01666f;
            GM.SimUpdatePartialSecond(0.01666f);
        }
        if(lastSimTime >= 1) {
            lastSimTime -= 1;
            GM.SimUpdateOneSecond(1);
        }
    }

    //Returns the multi used in sim-based calculations to determine thier final values.
    public float lastSimTimeMulti() {
        return (lastSimTime + 1) * timeMulti; //lastSimTime + 1 because min sim time is 1 sec. See update block.
    }

    public void UpdateTimeMulti(int updateType) {
        var timeListIndex = timeMultiList.IndexOf(timeMulti);
        if(updateType == 1 && timeListIndex <= timeMultiList.Count - 2) {
            //Inc Multi
            timeListIndex += 1;
        }else if (updateType == -1 && timeListIndex >= 1) {
            //Dec Multi
            timeListIndex -= 1;
        }
        timeMulti = timeMultiList[timeListIndex];
    }
}
