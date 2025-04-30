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
            GetFromPool();
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
                Debug.Log($"Objeto detectado al inicio: {collider.name}");
                lastPos = collider.transform.parent.position;
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
    private GameObject GetFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf) // Buscar un objeto inactivo en la piscina
            {
                obj.SetActive(true);
                obj.transform.position = lastPos;
                obj.transform.rotation = lastRotation;
                return obj;
            }
        }

        // Si no hay objetos disponibles, instanciar uno nuevo (opcional)
        GameObject newObj = Instantiate(objetoPrefab, lastPos, lastRotation);
        newObj.transform.SetParent(transform);
        pool.Add(newObj);
        return newObj;
    }
    #endregion
}
