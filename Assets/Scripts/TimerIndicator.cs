using UnityEngine;
using UnityEngine.UI;

public class TimerIndicator : MonoBehaviour
{
    public Image timerFillImage;
    public float totalTime; // Tiempo total del "reloj"
    private float currentTime;
    public bool isWaiting = false;

    void Update()
    {
        if (isWaiting)
        {
            currentTime -= Time.deltaTime;

            // Actualiza la UI
            float fillAmount = Mathf.Clamp01(currentTime / totalTime);
            timerFillImage.fillAmount = fillAmount;

            // Opcional: destruir, reiniciar, etc.
            if (currentTime <= 0)
            {
                Debug.Log("Tiempo terminado");
                // Puedes reiniciar o ejecutar otra acción
            }
        }
    }
    public void StartTimer()
    {
        isWaiting = true;
        currentTime = totalTime;
    }
    public void ResetTimer()
    {
        currentTime = totalTime;
    }
}
