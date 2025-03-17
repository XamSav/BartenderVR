using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private Customer waitingCustomer;
    [SerializeField] private GlassReceiver cocktail;
    
    public void SetDrink(GlassReceiver glas)
    {
        cocktail = glas;
        if(cocktail.ingredients.Count > 0)
        {
            if (waitingCustomer != null)
            {
                waitingCustomer.ReceiveDrink(cocktail);
                glas.gameObject.SetActive(false); // Desactivar el cóctel 
            }
        }
    }
    public void SetCustomer(Customer customer)
    {
        Debug.Log("Customer: " + customer.requestedCocktail.cocktailName);
        waitingCustomer = customer;
        if(cocktail != null)
        {
            waitingCustomer.ReceiveDrink(cocktail);
            cocktail.gameObject.SetActive(false); // Desactivar el cóctel 
        }
    }
}
