using UnityEngine;
using TMPro;
public class MoneyPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    public float floatSpeed = 0.5f;
    public float lifeTime = 1.5f;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int amount)
    {
        moneyText.text = "$" + amount.ToString();
    }

}
