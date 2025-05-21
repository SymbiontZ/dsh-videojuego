using System.Collections.Generic;
using UnityEngine;

public class Ordas : MonoBehaviour
{
    public GameObject[] olaClientes;
    private Queue<GameObject> poolClientes = new Queue<GameObject>();
    private GameObject c;
    void Start()
    {
        // Crea el pool de clientes
        for (int i = 0; i < 4; i++)
        {
            GameObject cliente = Instantiate(olaClientes[i], transform.position, transform.rotation);
            cliente.SetActive(false);
            poolClientes.Enqueue(cliente);
        }
    }

    public void NuevoCliente()
    {
        if (c != null)
        {
            Destroy(c);
        }
        if (GameFlow.aciertos >= 0)
        {
            c = poolClientes.Dequeue();
            c.SetActive(true);
        }
    }
}
