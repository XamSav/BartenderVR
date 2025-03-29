using UnityEngine;

public class GhostPreview3d : MonoBehaviour
{
    [SerializeField] private Material _transparent; // Material transparente
    [SerializeField][Range(0, 10f)] private float dist; // Distancia del raycast
    [SerializeField] private GameObject _previewPrefab; // Prefab del objeto de vista previa
    private GameObject _lastInstance;
    private bool _instantiate = false;

    private void Update()
    {
        // Raycast desde el controlador hacia adelante
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dist))
        {
            if (hit.collider.CompareTag("DeliveryZone")) // Si apunta a una zona válida
            {
                if (!_instantiate)
                {
                    if (hit.collider.GetComponent<DeliveryZone>().CanReceive())
                    {
                        _instantiate = true;
                        GameObject preview = _previewPrefab.transform.GetChild(1).gameObject;
                        Quaternion rotation = preview.transform.rotation;
                        Vector3 newPosition = hit.transform.position;


                        float previewHeight = preview.GetComponent<Renderer>().bounds.size.y;
                        float hitHeight = hit.transform.GetComponent<Renderer>().bounds.size.y;
                        newPosition.y += (hitHeight / 2) + (previewHeight / 2);

                        _lastInstance = Instantiate(preview, newPosition, rotation);
                        Renderer renderer = _lastInstance.GetComponent<Collider>().GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material = _transparent;
                        }
                    }
                }
            }
        }
        else
        {
            // Restaurar el material original si no hay hits
            if (_instantiate)
            {
                Destroy(_lastInstance);
                _instantiate = false;
            }
        }
    }









    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * dist);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dist))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
}
