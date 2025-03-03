using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    private int playerMoney;//100 de prueba

    void Awake() { 
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this; 
    }

    public bool TryUpgrade(UpgradeableObject upgradeable)
    {
        if (!upgradeable.CanUpgrade()) return false;
        playerMoney = GameManager.instance.GetPlayerMoney();
        int cost = upgradeable.GetUpgradeCost();
        if (playerMoney >= cost)
        {
            playerMoney -= cost;
            upgradeable.Upgrade();
            return true;
        }
        return false;
    }
}
