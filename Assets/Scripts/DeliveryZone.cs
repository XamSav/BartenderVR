using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private Customer waitingCustomer;
    [SerializeField] private GlassContent cocktail;
    private bool _canReceive = true;
    public void SetDrink(GlassContent glas)
    {
        _canReceive = false;
        if(glas.ingredients.Count > 0)
        {
            if (waitingCustomer != null && waitingCustomer.GetComponent<Customer>().IsWaiting())
            {
                waitingCustomer.ReceiveDrink(glas);
                //glas.gameObject.SetActive(false); // Desactivar el cóctel 
                ClearCocktail();
            }
        }
    }
    public void SetCustomer(Customer customer)
    {
        //_canReceive = false;
        waitingCustomer = customer;
        if(cocktail != null)
        {
            waitingCustomer.ReceiveDrink(cocktail);
            //cocktail.gameObject.SetActive(false); // Desactivar el cóctel 
            ClearCocktail();
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
    public void ClearCocktail()
    {
        //cocktail = null;
        _canReceive = true;
        waitingCustomer = null;
    }
}