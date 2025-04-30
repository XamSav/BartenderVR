using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class ContentUI : MonoBehaviour
{
    private Transform _parent;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _card;
    private List<IngredientData> _ingredients = new List<IngredientData>();

    private void Awake()
    {
        _parent = transform.parent;
        try
        {
            _parent.GetComponent<GlassContent>().SetContentUI(this);
        }
        catch
        {
            Debug.LogError("No se ha podido encontrar el componente GlassContent en el padre.");
        }
        if(_ingredients.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateUI(LiquidSource liquid)
    {
        if (_ingredients.Count == 0)
        {
            AddNewCard(liquid.ingredientName, liquid.flowRate);
        }
        else
        {
            if(_ingredients.Exists(x => x.ingredientName == liquid.ingredientName))//Existe
            {
                IngredientData data = _ingredients.Find(x => x.ingredientName == liquid.ingredientName);
                data.amount += liquid.flowRate;
                foreach (Transform child in _content)
                {
                    if(child.GetChild(0).GetComponent<TMP_Text>().text == liquid.ingredientName){
                        child.GetChild(1).GetComponent<TMP_Text>().text = data.amount + "ml";
                    }
                }
            }
            else//No existe
            {
                AddNewCard(liquid.ingredientName, liquid.flowRate);
            }
        }
    }
    private void AddNewCard(string name, float ml)
    {
        IngredientData data = new IngredientData { ingredientName = name, amount = ml };
        _ingredients.Add(data);
        GameObject card = Instantiate(_card, _content);
        card.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        card.transform.GetChild(1).GetComponent<TMP_Text>().text = ml+"ml";
    }
    public void Clear()
    {
        foreach(Transform child in _content)
        {
            Destroy(child);
        }
        gameObject.SetActive(false);
    }
}
