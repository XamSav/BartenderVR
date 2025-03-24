using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    //UNO DE TEST -> HACER ARRAY
    public GameObject customerPrefab; // Prefabs del cliente
    public Transform spawnPoint; // Posición donde aparece el cliente
    public Transform[] barPosition; // Punto donde el cliente se detiene
    public List<CocktailRecipe> cocktailMenu; // Lista de cócteles posibles
    public float spawnInterval = 10f; // Tiempo entre clientes

    private List<GameObject> activeCustomers = new List<GameObject>();
    private byte[] row = new byte[3];//Orden de las filas
    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (activeCustomers.Count < 3) // Máximo 3 clientes al mismo tiempo
            {
                SpawnCustomer();
            }
        }
    }

    void SpawnCustomer()
    {
        //HACER RANDOM PREFAB
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        activeCustomers.Add(newCustomer);
        Customer customerScript = newCustomer.GetComponent<Customer>();

        if (customerScript != null)
        {
            byte nRow = 0;
            byte minValue = row[0];
            for (byte i = 0; i < row.Length; i++)
            {
                if (row[i] < minValue)
                {
                    minValue = row[i];
                    nRow = i;
                }
            }
            row[nRow]++;
            //PONER MAS TRANSFORMS PARA HACER COLA
            customerScript.targetPosition = barPosition[nRow];
            customerScript.SetIndexPos(nRow);
            CocktailRecipe randomCocktail = cocktailMenu[Random.Range(0, cocktailMenu.Count)];
            customerScript.SetOrder(randomCocktail);

        }
    }
    public void CustomerLeave(byte indexPos)
    {
        row[indexPos]--;
        activeCustomers.RemoveAt(indexPos);
    }
}
