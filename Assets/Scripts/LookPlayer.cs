using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // Buscar la c�mara principal (que normalmente representa al jugador en VR o FPS)
        if (Camera.main != null)
        {
            playerTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No se encontr� una c�mara principal. Aseg�rate de que la c�mara tenga la etiqueta 'MainCamera'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcular la direcci�n hacia el jugador
            Vector3 direction = playerTransform.position - transform.position;

            // Ignorar la componente Y para que solo rote en el eje Y
            direction.y = 0;

            // Si la direcci�n no es cero, ajustar la rotaci�n
            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}