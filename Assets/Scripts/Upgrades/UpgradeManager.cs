using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private byte[] lvls = new byte[3] { 0, 0, 0 };
    public static UpgradeManager instance;
    public UpgradeData[] upgradeDatas = new UpgradeData[3];
    public Transform _sillasParent;
    public Transform _mesasParent;
    public Transform _lamparasParent;
    private int playerMoney = 200;//150 de prueba

    void Awake() { 
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this; 
    }

    public void Upgrade(byte item)
    {
        if (lvls[item] < 2)
        {
            if (playerMoney >= upgradeDatas[item].cost)
            {
                playerMoney -= upgradeDatas[item].cost;
                lvls[item]++;
                Debug.Log("Level Var: " + lvls[item]);
                UpgradeItems(_sillasParent, item);
            }
            else
            {
                Debug.Log("No tienes dinero");
            }
        }
    }

    private void UpgradeItems(Transform parent, byte type)
    {
        Debug.Log("Upgrade: " + upgradeDatas[type].name);
        Debug.Log("Level Par: " + lvls[type]);
        Debug.Log("Type: " + type);
        GameObject[] gameObjects = new GameObject[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject child = parent.GetChild(i).gameObject;
            Transform t = child.transform;
            Destroy(child.gameObject);
            gameObjects[i] = Instantiate(upgradeDatas[type].upgradedPrefab[lvls[type]], t.position, t.rotation);

        }
        foreach(GameObject t in gameObjects)
        {
            t.transform.parent = parent;
        }
    }
}
