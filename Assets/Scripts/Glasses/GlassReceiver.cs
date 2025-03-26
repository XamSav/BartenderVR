using System.Collections.Generic;
using UnityEngine;

public class GlassReceiver : MonoBehaviour
{
    public List<IngredientData> ingredients = new List<IngredientData>(); // Lista de ingredientes en el vaso
    public float maxCapacity = 300f; // Capacidad maxima en ml
    private float currentVolume = 0f;
    private ContentUI _contentUI;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MixingGlass")) // El vaso de mezcla debe tener este tag
        {
            GlassContent mixingGlass = other.gameObject.GetComponent<GlassContent>(); // Obtener contenido del vaso de mezcla
            if (mixingGlass != null)
            {
                TransferContents(mixingGlass);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeliveryZone")){
            DeliveryZone deliveryZone = other.GetComponent<DeliveryZone>();
            if (deliveryZone != null)
            {
                deliveryZone.SetDrink(this);
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
            Debug.Log("El vaso receptor esta lleno o no hay liquido que verter.");
            return;
        }

        foreach (var ingredient in mixingGlass.ingredients)
        {
            AddIngredient(new IngredientData { ingredientName = ingredient.ingredientName, amount = ingredient.amount });
        }

        mixingGlass.ClearContents();
        Debug.Log("Contenido transferido al vaso normal.");
        PrintContents();
    }

    void AddIngredient(IngredientData ingredient)
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
        ingredients.Add(new IngredientData { ingredientName = ingredient.ingredientName, amount = ingredient.amount });
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
    public void SetContentUI(ContentUI contentUI)
    {
        _contentUI = contentUI;
    }
}
