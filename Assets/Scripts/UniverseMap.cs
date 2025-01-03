using UnityEngine;

public class UniverseMap : MonoBehaviour
{
    //##### Beg of Variables #####
    public GameManager GM;
    private MapManager MM;
    private KingdomData KD;

    public GameObject KingdomIconPreset;
    public GameObject KingdomListFolder;
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    void Start() {
        MM = GM.GetComponent<MapManager>();
        KD = GM.GetComponent<KingdomData>();
    }

    public void ViewingUniverseMapMode() {
        var kingdomList = KD.GetKingdomList();
        for(int i = 0; i < kingdomList.Count; i++) {
            InstantiateKingdomIcon(kingdomList[i]);
        }
    }

    private void InstantiateKingdomIcon(Kingdom kingdom) {
        GameObject newKingdomIcon = (GameObject)Instantiate(KingdomIconPreset.transform.GetChild(0).gameObject);
        newKingdomIcon.transform.SetParent(KingdomListFolder.gameObject.transform);
        newKingdomIcon.GetComponent<KingdomObject>().kingdomID = kingdom.kingdomID;
        newKingdomIcon.SetActive(true);
    }

    public void SortKingdomList(int sortStyle) {
        //Can't do actual sorting until classes are properly ready
        //0 = Beginner Kingdoms, 1 = KVK Kingdoms
    }
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####
}
