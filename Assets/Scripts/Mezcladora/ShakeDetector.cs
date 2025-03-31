using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public GlassContent mixingGlass;
    public ShakerLid shakerLid;
    private Vector3 lastPosition;
    private float shakeThreshold = 0.3f;
    private int shakeCount = 0;
    private int requiredShakes = 5; // Número de sacudidas necesarias

    void Update()
    {
        if (shakerLid.isClosed)  // Solo si está cerrado
        {
            float movement = (transform.position - lastPosition).magnitude;

            if (movement > shakeThreshold)
            {
                shakeCount++;
                if (shakeCount >= requiredShakes)
                {
                    MixIngredients();
                    shakeCount = 0;
                }
            }
            lastPosition = transform.position;
        }
    }

    void MixIngredients()
    {
        foreach (var ingredient in mixingGlass.ingredients)
        {
            ingredient.amount *= 0.95f; // Reducción de volumen por mezcla
        }

        Debug.Log("¡Coctel mezclado correctamente!");
    }
}
