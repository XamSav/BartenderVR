using UnityEngine;

public class UpgradeableObject : MonoBehaviour
{
    public UpgradeData[] upgradeLevels;  //Lista de niveles de mejora (Scriptable Object)
    private int currentLevel = 0;

    public bool CanUpgrade() => currentLevel < upgradeLevels.Length - 1;

    public int GetUpgradeCost() => CanUpgrade() ? upgradeLevels[currentLevel + 1].cost : -1;

    public void Upgrade()
    {
        if (!CanUpgrade()) return;

        currentLevel++;
        GameObject newObject = Instantiate(upgradeLevels[currentLevel].upgradedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);  // Reemplazar el objeto actual con el mejorado
    }
}
