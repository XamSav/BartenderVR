using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteractorSimple : MonoBehaviour
{
    public float rayLength = 1f;
    public LayerMask interactableLayer;
    public InputActionProperty gripAction;

    private IInteractuable lastHovered;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayer))
        {
            IInteractuable currentInteractuable = hit.collider.GetComponent<IInteractuable>();

            if (currentInteractuable != null)
            {
                // Nuevo objeto apuntado
                if (currentInteractuable != lastHovered)
                {
                    if (lastHovered != null)
                        lastHovered.OnUnhover();

                    currentInteractuable.OnHover();
                    lastHovered = currentInteractuable;
                }

                // Si se presiona el botón Grip
                if (gripAction.action.WasPressedThisFrame())
                {
                    currentInteractuable.Action();
                }
            }
            else
            {
                ClearHover();
            }
        }
        else
        {
            ClearHover();
        }
    }

    private void ClearHover()
    {
        if (lastHovered != null)
        {
            lastHovered.OnUnhover();
            lastHovered = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }

}
