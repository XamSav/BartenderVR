using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public int cost;  // Precio de la mejora
    public GameObject[] upgradedPrefab;  // Prefab del objeto mejorado
}
