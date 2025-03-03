using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class GlassDispenser : MonoBehaviour
{
    public GameObject glassPrefab;  // Prefab de la copa
    public Transform handPosition;  // Posición en la mano del jugador
    private bool playerInRange = false;
    private InputDevice targetDevice;

    private void Start()
    {
        List<InputDevice> device = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, device);

        if(device.Count > 0)
        {
            targetDevice = device[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Detectar si la mano del jugador lo esta tocando
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            playerInRange = false;
        }
    }
    void Update()
    {
        if (playerInRange && targetDevice.isValid)
        {
            bool triggerPressed;
            if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                GiveGlassToPlayer();
            }
        }
    }
    void GiveGlassToPlayer()
    {
        if (handPosition.childCount > 0) return; // Evita que tenga varias copas al mismo tiempo

        // Instancia la copa en la mano
        GameObject newGlass = Instantiate(glassPrefab, handPosition.position, handPosition.rotation);
        newGlass.transform.SetParent(handPosition); // Adjunta la copa a la mano

        // Configura el XR Grab Interactable
        XRGrabInteractable grabInteractable = newGlass.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.trackPosition = true;
            grabInteractable.trackRotation = true;

            // Configurar Attach Transform si existe
            Transform attachTransform = newGlass.transform.Find("Attach Transform");
            if (attachTransform != null)
            {
                grabInteractable.attachTransform = attachTransform;
            }
        }
    }
}
