using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    Customer waitingCustomer;
    GlassReceiver cocktail;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Drink"))
        {
            try
            {
                cocktail = col.GetComponent<GlassReceiver>();
                if (cocktail.ingredients.Count > 0)
                {
                    if (waitingCustomer != null)
                    {
                        waitingCustomer.ReceiveDrink(cocktail);
                        col.gameObject.SetActive(false); // Desactivar el cóctel 
                    }
                }
            }
            catch {
                Debug.Log("Error GlassReceiver");
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
