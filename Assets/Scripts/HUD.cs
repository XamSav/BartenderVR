using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [Header("Tiempo")]
    [SerializeField] private TMP_Text _clock;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private GameObject _parentList;
    [SerializeField] private GameObject _prefabCard;
    public static HUD instance;
    private void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
    public void BuyUpgrade(int type)
    {
        UpgradeManager.instance.Upgrade((byte)type);
    }
    public void UpdateMoney(int money)
    {
        _money.text = money+"$";
    }
    public void UpdateGlass(string title, string ml)
    {
        foreach (Transform child in _parentList.transform)
        {
            TMP_Text titleText = child.GetChild(0).GetComponent<TMP_Text>();

            if (titleText.text == title) // Si ya existe, actualizar la cantidad
            {
                TMP_Text mlText = child.GetChild(1).GetComponent<TMP_Text>();
                mlText.text = ml+"ml";
                return; // Salir de la función para evitar duplicados
            }
        }
        GameObject card = Instantiate(_prefabCard, _parentList.transform);
        card.transform.GetChild(0).GetComponent<TMP_Text>().text = title;
        card.transform.GetChild(1).GetComponent<TMP_Text>().text = ml+"ml";
    }
    public void ClearGlass()
    {
        foreach (Transform child in _parentList.transform)
        {
            Destroy(child);
        }
    }
}
