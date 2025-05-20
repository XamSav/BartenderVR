using UnityEngine;

public class LockCameraHeight : MonoBehaviour
{
    public float fixedCameraHeight = 1.75f;
    public Transform cameraTransform;

    void LateUpdate()
    {
        Vector3 camLocalPos = cameraTransform.localPosition;
        camLocalPos.y = fixedCameraHeight;
        cameraTransform.localPosition = camLocalPos;
    }
}
