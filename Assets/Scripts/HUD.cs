using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [Header("Tiempo")]
    [SerializeField] private TMP_Text _clock;
    [Header("Money")]
    [SerializeField] private TMP_Text _money;
    [Header("Glass UI")]
    [SerializeField] private GameObject _parentList;
    [SerializeField] private GameObject _prefabCard;
    [Header("Cocteles")]
    [SerializeField] private CocktailRecipe[] _cocktails;//Imagen de los cocteles
    [SerializeField] private GameObject _cocktailsPanel;//Panel de los cocteles
    [SerializeField] private GameObject _cocktailsContent;//Panel de los cocteles
    [SerializeField] private GameObject _prefabRecipeIMG;//Prefab de la imagen de la receta
    [Header("Receta de Coctel")]
    [SerializeField] private GameObject _recipePanel;//Panel de la receta
    [SerializeField] private GameObject _recipeContent;//Contenido de la receta
    [SerializeField] private GameObject _prefabRecipe;//Prefab de la receta
    [SerializeField] private TMP_Text _titleRecipe;//Titulo de la receta
    
    public static HUD instance;
    private void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        SetUpUI();
    }
    private void SetUpUI()
    {
        byte i = 0;
        foreach (CocktailRecipe cocktail in _cocktails)
        {
            Transform button = Instantiate(_prefabRecipeIMG, _cocktailsContent.transform).transform.GetChild(0);
            Image image = button.GetComponent<Button>().targetGraphic.GetComponent<Image>();
            image.sprite = cocktail.cocktailImage;
            byte index = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { ShowRecipe(index); });
            i++;
        }
    }
    private void ShowRecipe(int id_cocktail)
    {
        _cocktailsPanel.SetActive(false);
        //Limpiamos la receta anterior
        foreach (Transform child in _recipeContent.transform)
        {
            Destroy(child.gameObject);
        }
        _titleRecipe.text = _cocktails[id_cocktail].cocktailName;
        foreach(IngredientData ingredient in _cocktails[id_cocktail].ingredients)
        {
            GameObject list = Instantiate(_prefabRecipe, _recipeContent.transform);
            list.transform.GetChild(0).GetComponent<TMP_Text>().text = ingredient.ingredientName;
            list.transform.GetChild(1).GetComponent<TMP_Text>().text = ingredient.amount + "ml";
        }
        _recipePanel.SetActive(true);
    }
    public void BuyUpgrade(int type)
    {
        UpgradeManager.instance.Upgrade((byte)type);
    }
    public void UpdateMoney(int money)
    {
        _money.text = money+"€";
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
