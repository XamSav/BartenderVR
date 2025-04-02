using UnityEngine;

public class Botle : MonoBehaviour
{
    [SerializeField][Range(20, 180)] private float tiltThreshold = 60f;
    public bool isPouring = false;
    [SerializeField] private GameObject _liquidParticles;
    void Update()
    {

        float xTilt = transform.eulerAngles.x;
        float zTilt = transform.eulerAngles.z;
        if (zTilt > tiltThreshold && zTilt < 360 - tiltThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }
        }
        else if (xTilt > tiltThreshold && xTilt < 360 - tiltThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }
        }
        else
        {
            if (isPouring)
            {
                StopPouring();
            }
        }
    }

    void StartPouring()
    {
        isPouring = true;
        Debug.Log("Botella inclinada, comienza a verter.");
        _liquidParticles.SetActive(true);
    }

    void StopPouring()
    {
        isPouring = false;
        Debug.Log("Botella en posición normal, deteniendo vertido.");
        _liquidParticles.SetActive(false);
    }
}
