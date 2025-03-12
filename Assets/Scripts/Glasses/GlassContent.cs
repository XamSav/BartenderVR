using System.Collections.Generic;
using UnityEngine;

public class GlassContent : MonoBehaviour
{
    // Clase para saber los ingredientes
    [System.Serializable]
    public class IngredientData
    {
        public string ingredientName;
        public float amount; // ml
    }

    public List<IngredientData> ingredients = new List<IngredientData>(); // Lista de ingredientes en el vaso
    public float maxCapacity = 300f;
    private float currentVolume = 0f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Liquid")) //PONER LAS PARTICULAS DE LIQUIDO CON ESTE TAG
        {
            LiquidSource liquid = col.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid.ingredientName, liquid.flowRate);
            }
        }
    }

    void AddIngredient(string ingredient, float amount)
    {
        if (currentVolume + amount > maxCapacity)
        {
            Debug.Log("�El vaso est� lleno!");
            return;
        }

        // Buscar si ya existe el ingrediente en la mezcla
        bool found = false;
        foreach (var item in ingredients)
        {
            if (item.ingredientName == ingredient)
            {
                item.amount += amount;
                found = true;
                break;
            }
        }

        // Si no est� en la lista, agregarlo
        if (!found)
        {
            ingredients.Add(new IngredientData { ingredientName = ingredient, amount = amount });
        }

        currentVolume += amount;
        Debug.Log($"A�adido {amount:F2}ml de {ingredient}. Total: {currentVolume:F2}ml");
        //GameManager.instance.AddLiquid(ingredient, amount);
    }
    public float GetCurrentVolume()
    {
        return currentVolume;
    }

    public void ClearContents()
    {
        ingredients.Clear();
        currentVolume = 0f;
        Debug.Log("Vaso de mezcla vaciado.");
    }
}
