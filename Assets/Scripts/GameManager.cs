using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Puntuacion")]
    private byte _score = 0;
    private byte _reputation = 100;
    private int playerMoney = 100;
    [Header("Tiempo")]
    [SerializeField] private int _maxTime = 100;
    private float _currentTime = 0;
    [Header("Estado de la partida")]
    [SerializeField]private bool _isPlaying = true;
    [Header("Contenido Del Vaso")]
    [SerializeField] private Transform _parentCards;
    [SerializeField] private GameObject _cardMl;
    [Header("Evaluacion")]
    [SerializeField] private CocktailEvaluator cocktailEvaluator;
    [SerializeField] private CustomerSpawner customerSpawner;

    private List<Card> cards = new List<Card>();
    public static GameManager instance;

    

    private void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
    }
    public void AddLiquid(string liquid, float ml)
    {
        foreach(Card item in cards)
        {
            if(item.liquid == liquid)
            {
                item.ml += ml;
                item.mlText.text = ($"{item.ml:F2}ml");
                return;
            }
        }

        GameObject card = Instantiate(_cardMl, _parentCards);
        TMP_Text[] camps = card.GetComponentsInChildren<TMP_Text>();
        camps[0].text = ($"{liquid}: ");
        camps[1].text = ($"{ml:F2}ml");
        cards.Add(new Card { liquid = liquid,ml = ml, mlText = camps[1] });
    }
    public void CustomerLeave(byte indexPos)
    {
        customerSpawner.CustomerLeave(indexPos);
    }
    public int CheckCocktail(GlassReceiver glass, CocktailRecipe customer)
    {
        int result = cocktailEvaluator.EvaluateCocktail(glass, customer);
        return result;
    }

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
    }
    public void PlayerPay(int cost)
    {
        playerMoney -= cost;
    }
    [System.Serializable]
    public class Card
    {
        public string liquid;
        public float ml;
        public TMP_Text mlText;
    }
}
