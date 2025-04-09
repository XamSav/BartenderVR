using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private Customer waitingCustomer;
    [SerializeField] private GlassContent cocktail;
    private bool _canReceive = true;
    public void SetDrink(GlassContent glas)
    {
        _canReceive = false;
        cocktail = glas;
        if(cocktail.ingredients.Count > 0)
        {
            if (waitingCustomer != null)
            {
                waitingCustomer.ReceiveDrink(cocktail);
                glas.gameObject.SetActive(false); // Desactivar el cóctel 
                _canReceive = true;
            }
        }
    }
    public void SetCustomer(Customer customer)
    {
        _canReceive = false;
        waitingCustomer = customer;
        if(cocktail != null)
        {
            waitingCustomer.ReceiveDrink(cocktail);
            cocktail.gameObject.SetActive(false); // Desactivar el cóctel 
            _canReceive = true;
        }
    }
    public bool CanReceive()
    {
        return _canReceive;
    }
    public bool HaveCoctail()
    {
        return cocktail != null;
    }
}