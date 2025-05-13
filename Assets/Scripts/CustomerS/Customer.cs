using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    //Movimiento
    public Transform targetPosition; // Posición de la barra
    public Vector3 spawnPosition; // Posición de la barra
    public float moveSpeed = 2f; // Velocidad de movimiento
    //Nube
    //public SpriteRenderer thoughtBubble; // Burbuja de pensamiento
    public Image cocktailImageUI; // Imagen del cóctel

    public CocktailRecipe requestedCocktail; //Pedido del cliente
    [SerializeField]private float patienceTime = 20f; // Tiempo antes de irse
    private byte indexPos;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private bool isLeaving = false;
    private NavMeshAgent navMeshAgent;
    private Animator _anim;
    void Start()
    {
        spawnPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        navMeshAgent.destination = targetPosition.position;
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPosition.position) < 0.3f && !isWaiting && !isLeaving)
        {
            _anim.SetTrigger("Salute");
            StartCoroutine(WaitForDrink());
        }
    }
    public void SetOrder(CocktailRecipe cocktail)
    {
        requestedCocktail = cocktail;
        cocktailImageUI.sprite = cocktail.cocktailImage; // Imagen del pedido
        //thoughtBubble.gameObject.SetActive(true); // Mostrar la burbuja de pensamiento
    }

    IEnumerator WaitForDrink()
    {
        isWaiting = true;
        targetPosition.GetComponentInChildren<DeliveryZone>().SetCustomer(this);
        yield return new WaitForSeconds(patienceTime);
        if (isWaiting && !isLeaving)
        {
            StartCoroutine(PlayAnimationAndLeave("Sad", false)); // Cliente se va enojado
        }
    }
    public void ReceiveDrink(GlassContent glass)
    {
        isWaiting = false;
        int result = GameManager.instance.CheckCocktail(glass, requestedCocktail);
        switch (result)
        {
            case 0:
                Debug.Log("Faltan Ingredientes");
                break;
            case 1:
                Debug.Log("Cantidad Incorrecta");
                GameManager.instance.ClientPay(Mathf.RoundToInt(requestedCocktail.price / 2));
                break;
            case 2: Debug.Log("Ingredientes Incorrectos");
                break;
            case 3:
                GameManager.instance.ClientPay(requestedCocktail.price);
                Debug.Log("Coctel Bien Hecho");
                break;
        }
        if(result == 3)
        {
            Debug.Log("Cliente satisfecho");
            StartCoroutine(PlayAnimationAndLeave("Happy", true));
        }
        else
        {
            Debug.Log("Cliente insatisfecho");
            StartCoroutine(PlayAnimationAndLeave("Sad", false));
        }
    }
    
    public void SetIndexPos(byte index)
    {
        indexPos = index;
    }
    private IEnumerator PlayAnimationAndLeave(string animationTrigger, bool happy)
    {
        _anim.SetTrigger(animationTrigger);//Happy - Sad
        isWaiting = false;
        isLeaving = true;
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length); // Esperar a que termine la animación
        Leave();
    }
    void Leave()
    {
        navMeshAgent.destination = spawnPosition;
        GameManager.instance.CustomerLeave(indexPos);
        Destroy(gameObject, 5f); // Destruir el cliente después de 2 segundos
    }
}
