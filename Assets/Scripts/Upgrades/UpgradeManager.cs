using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
public class UpgradeManager : MonoBehaviour
{
    private byte MaxLevel;
    //[SerializeField] private byte actualLevel = 1; // Max 3
    [SerializeField] private Transform _buttonsParent; // Content -> (0)Buttont Text+ (1)ButtonParent -> Button
    [SerializeField] private GameObject _buttonPrefab; 
    public static UpgradeManager instance;
    [Header("Upgrade Data")]
    public Upgrades[] upgrades;
    void Awake() { 
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this; 
    }
    private void Start()
    {
        MaxLevel = (byte)(upgrades[0].upgradeData.upgradedPrefab.Length-1);
        foreach (Transform child in _buttonsParent) // Destruir los botones existentes
        {
            Destroy(child.gameObject);
        }
        for (byte i = 0; i < upgrades.Length; i++)
        {
            CreateUpgradeButton(upgrades[i].name, i, upgrades[i].upgradeData.cost);
        }
        CreateUpgradeButton("Nivel", 255, 500); // Botón para la siguiente escena
    }
    private void CreateUpgradeButton(string name, byte index, int cost)
    {
        GameObject inst = Instantiate(_buttonPrefab, _buttonsParent);


        var localizeEvent = inst.transform.GetChild(0).GetComponent<LocalizeStringEvent>();
        if (localizeEvent != null)
        {
            localizeEvent.StringReference = new LocalizedString("Translation", name);
        }

        Button button = inst.transform.GetChild(1).GetComponent<Button>();
        button.onClick.AddListener(() => Upgrade((byte)index));
        button.interactable = false; // Desactivar el botón al inicio
        inst.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = cost.ToString() + "€";
    }
    public void Upgrade(byte item)
    {
        if(item == 255) { //Next Level
            GameManager.instance.NextLevel();
        }
        if(item >= upgrades.Length) {
            Debug.LogError("Índice de mejora no válido");
            return;
        }
        if (upgrades[item].level >= MaxLevel) // Mayor o igual al nivel maximo
        {
            Debug.Log("Ya has alcanzado el nivel máximo");
            return;
        }
        if(GameManager.instance.GetPlayerMoney() < upgrades[item].upgradeData.cost)
        {
            Debug.Log("No tienes dinero");
            return;
        }
        
        ProcessPayment(upgrades[item].upgradeData.cost);
        upgrades[item].level++;
        UpgradeItems(item);
        // Actualizar el texto del botón
        Transform buttonParent = _buttonsParent.GetChild(item);
        if (buttonParent != null)
        {
            Button but = buttonParent.GetChild(1).GetComponent<Button>();
            if (but != null)
            {
                but.interactable = false; // Desactivar el botón
                        
            }
        }
    }
    public void UpdateMoney(int money)
    {
        Debug.Log("Money:" + money);
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (!upgrades[i].isUpgraded)
            {
                Transform buttonParent = _buttonsParent.GetChild(i);
                if (buttonParent != null)
                {
                    Button but = buttonParent.GetChild(1).GetComponent<Button>();
                    if (but != null)
                    {
                        but.interactable = money >= upgrades[i].upgradeData.cost; // Activar o desactivar el botón según el dinero
                    }
                }
            }
        }
    }
    private void ProcessPayment(int cost)
    {
        GameManager.instance.PlayerPay(cost);
    }
    private void UpgradeItems(byte type)
    {
        if (upgrades[type].parents == null)
        {
            Debug.LogError("No se ha encontrado el padre");
            return;
        }
        Transform parent = upgrades[type].parents;
        GameObject[] gameObjects = new GameObject[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject child = parent.GetChild(i).gameObject;
            Transform t = child.transform;
            Destroy(child.gameObject);
            gameObjects[i] = Instantiate(upgrades[type].upgradeData.upgradedPrefab[upgrades[type].level], t.position, t.rotation);
        }
        foreach (GameObject go in gameObjects)
        {
            go.transform.parent = parent;
        }
    }
}
[Serializable]
public class Upgrades
{
    public string name;  // Nombre de la mejora
    public Transform parents;  // Precio de la mejora
    public UpgradeData upgradeData;  // Prefab del objeto mejorado
    public bool isUpgraded = false;
    public byte level = 0;
}