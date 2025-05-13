using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private int poolSize = 2;
    [Header("Prefab a instanciar")]
    [SerializeField] private GameObject objetoPrefab;
    private Vector3 lastPos;
    private Quaternion lastRotation;
    private BoxCollider boxCollider;
    private bool spawning = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        DetectObjectsInsideTrigger();
        InitializePool();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drink"))
        {
            Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
            if(rb != null)
            {
                rb.isKinematic = false; // Hacer que el objeto no sea cinemático
            }
            if (!spawning)
            {
                spawning = true;
                Invoke("GetFromPool", 2f);
            }
        }
    }
    private void DetectObjectsInsideTrigger()
    {
        // Obtener el centro y tamaño del BoxCollider en el espacio mundial
        Vector3 center = boxCollider.bounds.center;
        Vector3 halfExtents = boxCollider.bounds.extents;

        // Detectar todos los colliders dentro del área del BoxCollider
        Collider[] colliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Drink"))
            {
                lastPos = collider.transform.parent.position;
                lastPos.y += 0f; // Ajustar la posición Y para que esté por encima del objeto
                lastRotation = collider.transform.parent.rotation;
                pool.Add(collider.gameObject);
            }
        }
    }
    
    //POOL MANAGEMENT
    #region Pool
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objetoPrefab, transform);
            obj.SetActive(false); // Desactivar el objeto inicialmente
            pool.Add(obj);
        }
    }
    private void GetFromPool()
    {
        // Buscar un objeto inactivo
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true; // Para que no se mueva hasta ser agarrada
                }
                obj.transform.position = lastPos;
                obj.transform.rotation = lastRotation;
                obj.SetActive(true);
                spawning = false;
                return; // ¡Salimos después de activar uno!
            }
        }

        // Si no se encontró ninguno disponible, instanciar uno nuevo
        GameObject newObj = Instantiate(objetoPrefab, lastPos, lastRotation);
        newObj.transform.SetParent(transform);
        pool.Add(newObj);

        spawning = false;
    }

    #endregion
}
