using UnityEngine;
using UnityEngine.XR.Hands;
public class GlassDispenser : MonoBehaviour
{
    /*public GameObject glassPrefab; // Prefab de la copa
    public Transform handPosition; // Posición en la mano del jugador
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Detectar si la mano del jugador está cerca
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
        if (playerInRange && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Botón de acción (gatillo)
        {
            GiveGlassToPlayer();
        }
    }

    void GiveGlassToPlayer()
    {
        // Si el jugador ya tiene una copa, no generar otra
        if (handPosition.childCount > 0) return;

        // Crear la copa en la mano del jugador
        GameObject newGlass = Instantiate(glassPrefab, handPosition.position, handPosition.rotation);
        newGlass.transform.SetParent(handPosition); // Anclar a la mano
    }*/
}
