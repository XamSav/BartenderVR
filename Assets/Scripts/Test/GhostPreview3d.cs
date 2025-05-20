using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GhostPreview3d : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private XRBaseInteractor hand;
    [SerializeField] private Material transparentMaterial;

    [Header("Settings")]
    [SerializeField, Range(0, 10f)] private float rayDistance = 5f;

    [SerializeField]private GameObject heldObject;
    [SerializeField]private GameObject lastInstance;
    private bool isInstantiated = false;

    void OnEnable()
    {
        hand.selectExited.AddListener(OnObjectReleased);
    }

    void OnDisable()
    {
        hand.selectExited.RemoveListener(OnObjectReleased);
    }

    void Update()
    {
        if (!hand.hasSelection)
        {
            CleanupPreview();
            return;
        }

        if (heldObject == null)
            heldObject = hand.interactablesSelected[0].transform.GetChild(1).gameObject;

        if (!heldObject.CompareTag("CocktailGlass"))
        {
            CleanupPreview();
            return;
        }
        //No hace falta el try ya que antes comprobamos si es un CocktailGlass
        if (heldObject.transform.parent.GetComponent<GlassContent>().ingredients.Count > 0)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
            {
            
                if (hit.collider.CompareTag("DeliveryZone"))
                {
                    if (!isInstantiated)
                    {
                        ShowPreview(hit);
                    }
                    return;
                }
            }
        }
        CleanupPreview();
    }

    private void ShowPreview(RaycastHit hit)
    {

        isInstantiated = true;

        GameObject model = heldObject;
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);

        float modelHeight = model.GetComponent<Renderer>().bounds.size.y;
        float hitHeight = hit.transform.GetComponent<Renderer>().bounds.size.y;

        Vector3 spawnPosition = hit.transform.position;
        spawnPosition.y += (hitHeight + modelHeight) / 2f;

        lastInstance = Instantiate(model, spawnPosition, rotation);
        if (lastInstance.TryGetComponent(out Renderer renderer))
        {
            renderer.material = transparentMaterial;
        }
    }

    private void CleanupPreview()
    {
        if (lastInstance != null)
            Destroy(lastInstance);

        isInstantiated = false;
        lastInstance = null;
        heldObject = null;
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        if (heldObject == null) return;
        if (heldObject.transform.parent.GetComponent<GlassContent>().ingredients.Count > 0)
        {
            // Verificamos si sigue apuntando a una DeliveryZone al soltar
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.CompareTag("DeliveryZone"))
                {
                    GameObject parent = args.interactableObject.transform.gameObject;
                    GameObject releasedObject = parent.transform.GetChild(1).gameObject;

                    // Colocar el objeto en la posición del hit
                    float objectHeight = releasedObject.GetComponent<Renderer>().bounds.size.y;
                    float zoneHeight = hit.transform.GetComponent<Renderer>().bounds.size.y;

                    Vector3 targetPosition = hit.transform.position;
                    targetPosition.y += (zoneHeight + objectHeight) / 2f;

                    parent.transform.position = targetPosition;
                    parent.transform.rotation = Quaternion.Euler(0, 0, 0);

                    // Desactivar física si quieres que no se caiga
                    if (parent.TryGetComponent(out Rigidbody rb))
                    {
                        //Block constrainst rotation 
                        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        //Block Pos X and Z
                        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    }
                }
            }

            // Limpieza normal
            CleanupPreview();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
}
