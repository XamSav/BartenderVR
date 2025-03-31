using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GlassContent : MonoBehaviour
{
    [SerializeField] private Transform _liquidCardParent;
    public List<IngredientData> ingredients = new List<IngredientData>(); // Lista de ingredientes en el vaso
    public float maxCapacity = 300f;
    private float currentVolume = 0f;
    private ContentUI _contentUI;
    [SerializeField] private bool hasStrainer = false; // Variable para indicar si el vaso tiene colador

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle Colision");
        if (other.CompareTag("Liquid")) //PONER LAS PARTICULAS DE LIQUIDO CON ESTE TAG
        {
            Debug.Log("Particle Colision con liquido");
            LiquidSource liquid = other.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid.ingredientName, liquid.flowRate);
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Liquid")) //PONER LAS PARTICULAS DE LIQUIDO CON ESTE TAG
        {
            Debug.Log("Trigger con liquido");
            LiquidSource liquid = col.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid.ingredientName, liquid.flowRate);
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Liquid")) //PONER LAS PARTICULAS DE LIQUIDO CON ESTE TAG
        {
            Debug.Log("Colision con liquido");
            LiquidSource liquid = col.gameObject.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid.ingredientName, liquid.flowRate);
            }
        }
    }

    void AddIngredient(string ingredient, float amount)
    {
        if (currentVolume + amount > maxCapacity) return; //Vaso lleno
        //Derramar

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
        if (!found)
        {
            ingredients.Add(new IngredientData { ingredientName = ingredient, amount = amount });
        }
        currentVolume += amount;
        _contentUI.UpdateUI(ingredients);
        Debug.Log($"Añadido {amount:F2}ml de {ingredient}. Total: {currentVolume:F2}ml");
    }











    public void ReduceVolume(float amount)
    {
        currentVolume -= amount;

        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].amount -= amount;
        }
        Debug.Log($"Derrame de {amount:F2}ml. Nuevo volumen: {currentVolume:F2}ml");
    }

    public void SetStrainer(bool state)
    {
        hasStrainer = state;
    }

    public bool HasStrainer()
    {
        return hasStrainer;
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
        _contentUI.Clear();
    }
    public void SetContentUI(ContentUI content)
    {
        _contentUI = content;
    }
}
