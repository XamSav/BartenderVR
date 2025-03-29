using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GhostPreview : MonoBehaviour
{
    public GameObject previewPrefab; // Prefab del objeto de vista previa
    private GameObject previewInstance; // Instancia del objeto de vista previa
    private bool canPlace = false; // Indica si se puede colocar el objeto

    void Start()
    {
        // Instanciar el objeto de vista previa pero desactivado al inicio
        previewInstance = Instantiate(previewPrefab);
        previewInstance.SetActive(false);
    }

    void Update()
    {
        // Raycast desde el controlador hacia adelante
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("PlacementZone")) // Si apunta a una zona válida
            {
                previewInstance.SetActive(true);
                previewInstance.transform.position = hit.point; // Mueve el preview a la posición del raycast
                previewInstance.transform.rotation = Quaternion.identity; // Asegurar rotación correcta
                canPlace = true;
            }
            else
            {
                previewInstance.SetActive(false);
                canPlace = false;
            }
        }
        else
        {
            previewInstance.SetActive(false);
            canPlace = false;
        }
    }

    public void PlaceObject()
    {
        if (canPlace)
        {
            Instantiate(previewPrefab, previewInstance.transform.position, Quaternion.identity); // Crear el objeto real
            previewInstance.SetActive(false); // Ocultar vista previa después de colocar
        }
    }
}
