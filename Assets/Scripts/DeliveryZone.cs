using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    Customer waitingCustomer;
    GlassReceiver cocktail;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Drink"))
        {
            cocktail = col.GetComponent<GlassReceiver>();
            Debug.Log(cocktail.ingredients);
            if (waitingCustomer != null)
            {
                waitingCustomer.ReceiveDrink(cocktail);
                col.gameObject.SetActive(false); // Desactivar el cóctel 
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
