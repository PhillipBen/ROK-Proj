using UnityEngine;
using TMPro;

public class DeploySlider: MonoBehaviour
{
    public int type;
    public int tier;
    public int num;
    public void UpdateNumber(int num) {
        this.num = num;
        this.transform.parent.parent.GetComponent<DeployLocalCounter>().UpdateDeployCounts(type, tier, num);
    }

    public void ClearSlider() {
        num = 0;
        this.transform.parent.GetChild(3).GetComponent<TMP_InputField>().text = "0";
    }
}
