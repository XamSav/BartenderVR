using System.Collections.Generic;
using UnityEngine;

public class GlassReceiver : MonoBehaviour
{
    [System.Serializable]
    public class IngredientsData
    {
        public string ingredientName;
        public float amount; // Cantidad en ml
    }

    public List<IngredientsData> ingredients = new List<IngredientsData>(); // Lista de ingredientes en el vaso
    public float maxCapacity = 300f; // Capacidad máxima en ml
    private float currentVolume = 0f;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collider: "+ other.gameObject.name);
        if (other.gameObject.CompareTag("MixingGlass")) // El vaso de mezcla debe tener este tag
        {
            GlassContent mixingGlass = other.gameObject.GetComponent<GlassContent>(); // Obtener contenido del vaso de mezcla
            if (mixingGlass != null)
            {
                TransferContents(mixingGlass);
            }
        }
    }

    void TransferContents(GlassContent mixingGlass)
    {
        float remainingCapacity = maxCapacity - currentVolume;
        float afterTransfer = remainingCapacity - mixingGlass.GetCurrentVolume();
        //float transferVolume = Mathf.Min(mixingGlass.GetCurrentVolume(), remainingCapacity);

        if (afterTransfer <= 0)
        {
            Debug.Log("El vaso receptor está lleno o no hay líquido que verter.");
            return;
        }

        foreach (var ingredient in mixingGlass.ingredients)
        {
            AddIngredient(new IngredientsData { ingredientName = ingredient.ingredientName, amount = ingredient.amount });
        }

        mixingGlass.ClearContents();
        Debug.Log("Contenido transferido al vaso normal.");
        PrintContents();
    }

    void AddIngredient(IngredientsData ingredient)
    {
        if (currentVolume + ingredient.amount > maxCapacity) return;//Si sobre pasa sal

        foreach (var item in ingredients)
        {
            if (item.ingredientName == ingredient.ingredientName)
            {
                item.amount += ingredient.amount;
                return;
            }
        }
        ingredients.Add(new IngredientsData { ingredientName = ingredient.ingredientName, amount = ingredient.amount });
        currentVolume += ingredient.amount;
    }

    public void PrintContents()
    {
        Debug.Log("Ingredientes en el vaso normal:");
        foreach (var item in ingredients)
        {
            Debug.Log($"{item.ingredientName}: {item.amount:F2}ml");
        }
    }
}
