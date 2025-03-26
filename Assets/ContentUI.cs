using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class ContentUI : MonoBehaviour
{
    private Transform _parent;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _card;
    private void Start()
    {
        _parent = transform.parent;
        if (_parent.GetComponent<GlassContent>() != null)
            _parent.GetComponent<GlassContent>().SetContentUI(this);
        else if (_parent.GetComponent<GlassReceiver>() != null)
            _parent.GetComponent<GlassReceiver>().SetContentUI(this);
    }

    public void UpdateUI(List<IngredientData> contents)
    {
        Clear();
        foreach(IngredientData data in contents)
        {
            GameObject card = Instantiate(_card, _content);
            card.transform.GetChild(0).GetComponent<TMP_Text>().text = data.ingredientName;
            card.transform.GetChild(1).GetComponent<TMP_Text>().text = data.amount + "ml";
        }
    }
    public void Clear()
    {
        foreach(Transform child in _content)
        {
            Destroy(child);
        }
    }
}
