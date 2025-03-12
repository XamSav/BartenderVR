using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "Bartender/Cocktail Recipe")]
public class CocktailRecipe : ScriptableObject
{
    [System.Serializable]
    public class Ingredient
    {
        public string ingredientName;
        public float amount; // Cantidad en ml
    }
    [System.Serializable]
    public class Decorations
    {
        public string decorationName;
        public byte amount;
    }

    public string cocktailName;
    public Sprite cocktailImage;
    public int price;
    public List<Ingredient> ingredients = new List<Ingredient>();
    public List<Decorations> decorations = new List<Decorations>();
}
