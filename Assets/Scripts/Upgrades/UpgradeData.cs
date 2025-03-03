using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;  // Nombre de la mejora Ejemplo: Silla_1 , Silla_2 , Silla_3
    public int cost;  // Precio de la mejora
    public GameObject upgradedPrefab;  // Prefab del objeto mejorado
}
