using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private const byte MaxLevel = 2;
    private byte[] lvls = new byte[3] { 0, 0, 0 };
    public static UpgradeManager instance;
    public UpgradeData[] upgradeDatas = new UpgradeData[3];
    public Transform _sillasParent;
    public Transform _mesasParent;
    public Transform _lamparasParent;
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
        switch (item)
        {
            case 0: return _sillasParent;
            case 1: return _mesasParent;
            case 2: return _lamparasParent;
            default: throw new ArgumentOutOfRangeException(nameof(item), "Índice de mejora no válido");
        }
    }
    private void UpgradeItems(Transform parent, byte type)
    {
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
