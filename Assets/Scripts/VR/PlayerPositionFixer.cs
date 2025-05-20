using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerPositionFixer : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; // Dónde querés que esté el jugador (piso)
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

        // Obtener la diferencia entre la cámara (cabeza real) y el origin
        Vector3 cameraInOriginSpace = xrOrigin.Camera.transform.localPosition;

        // Ajustar el Origin para que la cámara quede centrada en el target deseado
        Vector3 offset = new Vector3(cameraInOriginSpace.x, 0, cameraInOriginSpace.z);
        xrOrigin.MoveCameraToWorldLocation(targetTransform.position + offset * -1f);
    }
}
