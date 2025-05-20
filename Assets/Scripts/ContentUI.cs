using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
public class ContentUI : MonoBehaviour
{
    private Transform _parent;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _card;
    [SerializeField] private Slider _slider;

    [SerializeField] private Image _sliderFillImage; // Referencia al componente Image del Slider
    [SerializeField] private Image _handleImage; // Referencia al componente Image del Slider


    private List<IngredientData> _ingredients = new List<IngredientData>();
    private GlassContent _glassContent;

    private void Awake()
    {
        _parent = transform.parent;
        try
        {
            _glassContent = _parent.GetComponent<GlassContent>();
            _glassContent.SetContentUI(this);
            _slider.maxValue = _glassContent.maxCapacity;
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
    private void Update()
    {
        _slider.value = _glassContent.GetCurrentVolume();
        //UpdateSliderColor();
        if (_glassContent.GetCurrentVolume() == 0)
        {
            Clear();
        }
        else
        {
            UpdateSliderColor();
        }
    }
    public void UpdateUI(LiquidSource liquid)
    {
        if (_ingredients.Count == 0)
        {
            AddNewCard(liquid.ingredientName, liquid.flowRate, liquid.color);
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
                AddNewCard(liquid.ingredientName, liquid.flowRate, liquid.color);
            }
        }
    }
    private void AddNewCard(string name, float ml, Color col)
    {
        IngredientData data = new IngredientData { ingredientName = name, amount = ml, color = col };
        _ingredients.Add(data);
        GameObject card = Instantiate(_card, _content);
        card.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        card.transform.GetChild(1).GetComponent<TMP_Text>().text = ml+"ml";
        //UpdateSliderColor();

    }
    private void UpdateSliderColor()
    {/*
        // Si hay más de un tipo de líquido, mezclamos los colores
        if (_ingredients.Count > 0)
        {
            // Creamos una lista de colores de los líquidos
            Color[] colors = new Color[_ingredients.Count];
            for (int i = 0; i < _ingredients.Count; i++)
            {
                colors[i] = _ingredients[i].color;
            }

            // Si hay múltiples líquidos, calculamos un color promedio
            Color blendedColor = BlendColors(colors);
            _sliderFillImage.color = blendedColor;
        }*/
        Color mixedColor = GetWeightedColorAverage(_ingredients);
        _sliderFillImage.color = mixedColor;
        _handleImage.color = mixedColor;
    }
    /*
    private Color BlendColors(Color[] colors)
    {
        // Mezclamos los colores de los líquidos
        Color blendedColor = Color.black;
        for (int i = 0; i < colors.Length; i++)
        {
            blendedColor += colors[i];
        }

        return blendedColor / colors.Length; // Promedio de los colores
    }*/
    private Color GetWeightedColorAverage(List<IngredientData> ingredients)
    {
        if (ingredients.Count == 0) return Color.clear;

        float totalAmount = 0f;
        float r = 0f, g = 0f, b = 0f;

        foreach (var ingredient in ingredients)
        {
            totalAmount += ingredient.amount;
            r += ingredient.color.r * ingredient.amount;
            g += ingredient.color.g * ingredient.amount;
            b += ingredient.color.b * ingredient.amount;
        }

        if (totalAmount == 0) return Color.black;

        return new Color(r / totalAmount, g / totalAmount, b / totalAmount);
    }

    public void Clear()
    {
        foreach(Transform child in _content)
        {
            Destroy(child.gameObject);
        }
        _ingredients.Clear();
        gameObject.SetActive(false);
    }
}
