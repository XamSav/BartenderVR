using UnityEngine;

public class Pouring : MonoBehaviour
{
    [SerializeField] private GlassContent mixingGlass;
    private float maxPourRate = 50f; // M�xima cantidad de l�quido que puede derramarse por segundo
    private float minPourRate = 5f;  // M�nima cantidad de l�quido derramado

    private void Update()
    {
        float fillPercentage = mixingGlass.GetCurrentVolume() / mixingGlass.maxCapacity;
        float dynamicPourThreshold = Mathf.Lerp(70f, 90f, 1 - fillPercentage);
        float dynamicSpillThreshold = Mathf.Lerp(90f, 110f, 1 - fillPercentage);

        float angle = Vector3.Angle(Vector3.up, transform.up);

        if (angle > dynamicPourThreshold)
        {
            Pour(angle, fillPercentage);
        }
    }

    private void Pour(float angle, float fillPercentage)
    {
        if (mixingGlass.ingredients.Count == 0)
        {
            Debug.Log("No hay contenido para verter.");
            return;
        }

        float pourRate = Mathf.Lerp(minPourRate, maxPourRate, (angle - 70f) / 40f); // Cuanto m�s inclinado, m�s r�pido vierte
        pourRate *= Mathf.Lerp(0.5f, 1.5f, fillPercentage); // Cuanto m�s lleno, m�s r�pido se vierte

        mixingGlass.ReduceVolume(pourRate * Time.deltaTime);
        Debug.Log($"Vertiendo {pourRate * Time.deltaTime:F2} ml por segundo. �ngulo: {angle:F1}�");

        // TODO: Implementar part�culas de l�quido
    }
}

