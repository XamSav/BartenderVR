using UnityEngine;
using UnityEngine.UI;

public class HeightAdjuster : MonoBehaviour
{
    public Vector3 fixedPosition = Vector3.zero;
    public float fixedHeight = 1.75f; // altura deseada en metros

    void LateUpdate()
    {
        // Bloquea la posición horizontal
        transform.position = new Vector3(fixedPosition.x, fixedHeight, fixedPosition.z);
    }
}
