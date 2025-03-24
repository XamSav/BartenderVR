using UnityEngine;
using UnityEngine.UI;

public class HeightAdjuster : MonoBehaviour
{
    public Slider heightSlider;
    public Transform cameraOffset;  // Asigna el GameObject "Camera Offset" del XR Rig
    private float minHeight = 1.2f; // Altura mínima permitida
    private float maxHeight = 2.2f; // Altura máxima permitida

    void Start()
    {
        heightSlider.minValue = minHeight;
        heightSlider.maxValue = maxHeight;
        heightSlider.value = cameraOffset.localPosition.y; // Valor inicial
        heightSlider.onValueChanged.AddListener(AdjustHeight);
    }

    void AdjustHeight(float newHeight)
    {
        cameraOffset.localPosition = new Vector3(0, newHeight, 0);
    }
}
