using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    //Movimiento
    public Transform targetPosition; // Posición de la barra
    public float moveSpeed = 2f; // Velocidad de movimiento
    //Nube
    public SpriteRenderer thoughtBubble; // Burbuja de pensamiento
    public Image cocktailImageUI; // Imagen del cóctel

    public CocktailRecipe requestedCocktail; //Pedido del cliente
    public DeliveryZone delivery;
    [SerializeField]private float patienceTime = 20f; // Tiempo antes de irse
    private byte indexPos;
    private bool isWaiting = true;

    void Start()
    {
        StartCoroutine(MoveToBar());
    }
    IEnumerator MoveToBar()
    {
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
            yield return null; // Esperar al siguiente frame
        }

        // Llego a la barra, inicia la espera
        StartCoroutine(WaitForDrink());
    }
    public void SetOrder(CocktailRecipe cocktail)
    {
        requestedCocktail = cocktail;
        cocktailImageUI.sprite = cocktail.cocktailImage; // Imagen del pedido
        thoughtBubble.gameObject.SetActive(true); // Mostrar la burbuja de pensamiento
    }

    IEnumerator WaitForDrink()
    {
        isWaiting = true;
        targetPosition.GetComponentInChildren<DeliveryZone>().SetCustomer(this);
        yield return new WaitForSeconds(patienceTime);
        if (isWaiting)
        {
            Leave(false); // Cliente se va enojado
        }
    }

    public void ReceiveDrink(GlassReceiver glass)
    {
        //if (!isWaiting) return;

        int result = GameManager.instance.CheckCocktail(glass, requestedCocktail);
        switch (result)
        {
            case 0:
                Debug.Log("Faltan Ingredientes");
                break;
            case 1:
                Debug.Log("Cantidad Incorrecta");
                break;
            case 2: Debug.Log("Ingredientes Incorrectos");
                break;
            case 3:
                Debug.Log("Coctel Bien Hecho");
                break;
        }
        if(result == 3)
        {
            Debug.Log("Cliente satisfecho");
            Leave(true);
        }
        else
        {
            Debug.Log("Cliente insatisfecho");
            Leave(false);
        }
    }
    public void SetIndexPos(byte index)
    {
        indexPos = index;
    }
    void Leave(bool happy)
    {
        isWaiting = false;
        thoughtBubble.gameObject.SetActive(false);
        Debug.Log(happy ? "El cliente se va feliz." : "El cliente se va molesto.");
        GameManager.instance.CustomerLeave(indexPos);
        Destroy(gameObject, 2f); // Destruir el cliente después de 2 segundos
    }
}
