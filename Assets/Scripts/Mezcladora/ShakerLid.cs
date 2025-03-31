using UnityEngine;

public class ShakerLid : MonoBehaviour
{
    public bool isClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShakerTop"))
        {
            isClosed = true;
            Debug.Log("Vaso de mezcla cerrado.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShakerTop"))
        {
            isClosed = false;
            Debug.Log("Vaso de mezcla abierto.");
        }
    }
}
