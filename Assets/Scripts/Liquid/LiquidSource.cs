using UnityEngine;

public class LiquidSource : MonoBehaviour
{
    public string ingredientName = "Vodka"; // Nombre del l�quido en la botella
    public float flowRate = 50f; // Cantidad de l�quido que cae por segundo
    public Color color;
    private void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = color;
    }
}
