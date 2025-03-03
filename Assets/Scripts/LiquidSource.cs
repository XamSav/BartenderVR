using UnityEngine;

public class LiquidSource : MonoBehaviour
{
    public string ingredientName = "Vodka"; // Nombre del líquido en la botella
    public float flowRate = 50f; // Cantidad de líquido que cae por segundo

    private void Start()
    {
        Debug.Log($"Botella lista: {ingredientName} - Flujo {flowRate}ml/s");
    }
}
