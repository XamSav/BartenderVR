using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private const byte MaxLevel = 2;
    private byte[] lvls = new byte[3] { 0, 0, 0 };
    public static UpgradeManager instance;
    public UpgradeData[] upgradeDatas = new UpgradeData[3];
    public Transform[] _parents = new Transform[3];
    private int playerMoney;
    void Awake() { 
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this; 
    }

    public void Upgrade(byte item)
    {
        if (item >= lvls.Length || item >= upgradeDatas.Length)
        {
            Debug.LogError("Índice de mejora no válido");
            return;
        }
        playerMoney = GameManager.instance.GetPlayerMoney();
        if (lvls[item] < MaxLevel)
        {
            if (playerMoney >= upgradeDatas[item].cost)
            {
                ProcessPayment(upgradeDatas[item].cost);
                lvls[item]++;
                UpgradeItems(GetParentTransform(item), item);
            }
            else
            {
                Debug.Log("No tienes dinero");
            }
        }
    }
    private void ProcessPayment(int cost)
    {
        GameManager.instance.PlayerPay(cost);
    }
    private Transform GetParentTransform(byte item)
    {
        if(item >= _parents.Length)
        {
            Debug.LogError("Índice de mejora no válido");
            return null;
        }else return _parents[item];
    }
    private void UpgradeItems(Transform parent, byte type)
    {
        if(parent == null)
        {
            Debug.LogError("No se ha encontrado el padre");
            return;
        }
        GameObject[] gameObjects = new GameObject[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject child = parent.GetChild(i).gameObject;
            Transform t = child.transform;
            Destroy(child.gameObject);
            gameObjects[i] = Instantiate(upgradeDatas[type].upgradedPrefab[lvls[type]], t.position, t.rotation);
        }
        foreach (GameObject go in gameObjects)
        {
            go.transform.parent = parent;
        }
    }
}
