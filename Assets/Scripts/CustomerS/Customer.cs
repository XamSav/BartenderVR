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
    private TimerIndicator timerIndicator;
    private NavMeshAgent navMeshAgent;
    private Animator _anim;
    private GlassContent glassContent; // Contenido del vaso
    void Start()
    {
        spawnPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        timerIndicator = GetComponent<TimerIndicator>();
        _anim = GetComponent<Animator>();
        timerIndicator.totalTime = patienceTime;
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
        cocktailImageUI.gameObject.SetActive(true); // Mostrar la imagen del cóctel
        cocktailImageUI.sprite = cocktail.cocktailImage; // Imagen del pedido
        //thoughtBubble.gameObject.SetActive(true); // Mostrar la burbuja de pensamiento
    }

    IEnumerator WaitForDrink()
    {
        isWaiting = true;
        targetPosition.GetComponentInChildren<DeliveryZone>().SetCustomer(this);
        timerIndicator.StartTimer();
        yield return new WaitForSeconds(patienceTime);
        GetComponent<TimerIndicator>().isWaiting = false;
        if (isWaiting && !isLeaving)
        {
            StartCoroutine(PlayAnimationAndLeave("Sad")); // Cliente se va enojado
        }
    }
    public void ReceiveDrink(GlassContent glass)
    {
        isWaiting = false;
        glassContent = glass;
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
            StartCoroutine(PlayAnimationAndLeave("Happy"));
        }
        else
        {
            Debug.Log("Cliente insatisfecho");
            StartCoroutine(PlayAnimationAndLeave("Sad"));
        }
    }
    
    public void SetIndexPos(byte index)
    {
        indexPos = index;
    }
    private IEnumerator PlayAnimationAndLeave(string animationTrigger)
    {
        cocktailImageUI.gameObject.SetActive(false); // Esconder imagen
        isWaiting = false;
        isLeaving = true;

        _anim.SetTrigger(animationTrigger);

        // Esperar hasta que la animación empiece
        yield return new WaitUntil(() =>
            _anim.GetCurrentAnimatorStateInfo(0).IsName(animationTrigger)
        );

        // Esperar a que termine la animación
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(RotateThenLeave());
        //Destroy Glass
        if(glassContent != null)
            glassContent.gameObject.SetActive(false); // Desactivar el cóctel
    }
    
    IEnumerator RotateThenLeave()
    {
        Vector3 direction = (spawnPosition - transform.position).normalized;
        direction.y = 0f;
        if (direction == Vector3.zero)
            yield break;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float t = 0f;
        float duration = 0.5f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            yield return null;
        }

        Leave();
    }


    void Leave()
    {
        navMeshAgent.destination = spawnPosition;
        GameManager.instance.CustomerLeave(indexPos);
        Destroy(gameObject, 5f); // Destruir el cliente después de 2 segundos
    }
    public bool IsWaiting()
    {
        return isWaiting;
    }
}
