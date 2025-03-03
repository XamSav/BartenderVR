using System.Collections.Generic;
using UnityEngine;

public class CocktailEvaluator : MonoBehaviour
{
    // 0 - Faltan Ingredientes
    // 1 - Cantidad Incorrecta
    // 2 - Ingredientes Incorrectos
    // 3 - Coctel Bien Hecho
    public int EvaluateCocktail(GlassReceiver glass, CocktailRecipe requestedCocktail)
    {
        if(glass.ingredients.Count != requestedCocktail.ingredients.Count) { return 0; }
        foreach(var recipeIngredient in requestedCocktail.ingredients)
        {
            bool found = false;
            foreach(var glassIngredient in glass.ingredients)
            {
                if(glassIngredient.ingredientName == recipeIngredient.ingredientName)
                {
                    found = true;
                    float tolerance = 5f;
                    if(Mathf.Abs(glassIngredient.amount - recipeIngredient.amount) > tolerance)
                    {
                        return 1;//Cantidad Incorrecta
                    }
                    break;
                }
            }
            if (!found) { return 2; }//Ingredientes Incorrectos
        }
        return 3;//Coctel Correcto
    }
}
