using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // Buscar la cámara principal (que normalmente representa al jugador en VR o FPS)
        if (Camera.main != null)
        {
            playerTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No se encontró una cámara principal. Asegúrate de que la cámara tenga la etiqueta 'MainCamera'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direction = playerTransform.position - transform.position;

            // Ignorar la componente Y para que solo rote en el eje Y
            direction.y = 0;

            // Si la dirección no es cero, ajustar la rotación
            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}