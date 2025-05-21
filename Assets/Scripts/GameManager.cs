using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private HUD hud;
    [Header("Puntuacion")]
    private int playerMoney = 499;
    [Header("Tiempo")]
    private float _currentTime = 0;
    [Header("Evaluacion")]
    [SerializeField] private CocktailEvaluator cocktailEvaluator;
    [SerializeField] private CustomerSpawner customerSpawner;
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
    }
    public void CustomerLeave(byte indexPos)
    {
        customerSpawner.CustomerLeave(indexPos);
    }
    public int CheckCocktail(GlassContent glass, CocktailRecipe customer)
    {
        int result = cocktailEvaluator.EvaluateCocktail(glass, customer);
        return result;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Level");
    }
    #region Encapsulaciones
    public float GetTime()
    {
        return _currentTime;
    }
    public int GetPlayerMoney()
    {
        return playerMoney;
    }
    public void ClientPay(int money)
    {
        playerMoney += money;
        UpdateUI();
    }
    public void PlayerPay(int cost)
    {
        playerMoney -= cost;
        UpdateUI();
    }
    #endregion
    private void UpdateUI()
    {
        hud.UpdateMoney(playerMoney);
        UpgradeManager.instance.UpdateMoney(playerMoney);
    }
    [System.Serializable]
    public class Card
    {
        public string liquid;
        public float ml;
        public TMP_Text mlText;
    }
}
