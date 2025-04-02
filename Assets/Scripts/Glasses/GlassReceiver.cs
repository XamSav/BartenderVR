using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlassReceiver : MonoBehaviour
{
    public List<IngredientData> ingredients = new List<IngredientData>(); // Lista de ingredientes en el vaso
    public float maxCapacity = 300f; // Capacidad maxima en ml
    private float currentVolume = 0f;
    private ContentUI _contentUI;
    [SerializeField] private Strainer strainer;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Liquid"))
        {
            LiquidSource liquid = other.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid);
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("MixingGlass")) // El vaso de mezcla debe tener este tag
        {
            LiquidSource liquid = col.gameObject.GetComponent<LiquidSource>();
            if (liquid != null)
            {
                AddIngredient(liquid);
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

    void AddIngredient(LiquidSource liquid)
    {
        if (currentVolume + liquid.flowRate > maxCapacity) return; //Vaso lleno
        //Derramar

        // Buscar si ya existe el ingrediente en la mezcla
        bool found = false;
        foreach (var item in ingredients)
        {
            if (item.ingredientName == liquid.ingredientName)
            {
                item.amount += liquid.flowRate;
                found = true;
                break;
            }
        }
        if (!found)
        {
            ingredients.Add(new IngredientData { ingredientName = liquid.ingredientName, amount = liquid.flowRate });
        }
        currentVolume += liquid.flowRate;
        if (!_contentUI.gameObject.activeInHierarchy)
            _contentUI.gameObject.SetActive(true);
        _contentUI.UpdateUI(liquid);
    }



    public void SetContentUI(ContentUI contentUI)
    {
        _contentUI = contentUI;
    }
}
