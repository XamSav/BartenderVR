using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerPositionFixer : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; // D�nde quer�s que est� el jugador (piso)
    [SerializeField] private XROrigin xrOrigin;

    void Start()
    {
        RecenterPlayer();
    }

    public void RecenterPlayer()
    {
        if (xrOrigin == null)
            xrOrigin = FindFirstObjectByType<XROrigin>();

        if (xrOrigin == null || targetTransform == null)
            return;

        // Obtener la diferencia entre la c�mara (cabeza real) y el origin
        Vector3 cameraInOriginSpace = xrOrigin.Camera.transform.localPosition;

        // Ajustar el Origin para que la c�mara quede centrada en el target deseado
        Vector3 offset = new Vector3(cameraInOriginSpace.x, 0, cameraInOriginSpace.z);
        xrOrigin.MoveCameraToWorldLocation(targetTransform.position + offset * -1f);
    }
}
