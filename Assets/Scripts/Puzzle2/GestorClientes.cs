using UnityEngine;

public class GestorClientes : MonoBehaviour
{
    Ordas a;
    AudioSource cling;
    public GameObject primero, segundo, tercero, cuarto;

    void Start()
    {
        a = GameObject.Find("Clientes").GetComponent<Ordas>();
        cling = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!GameFlow.orderAcepted)
        {
            if (GameFlow.aciertos > 0)
            {
                cling.Play();
                a.NuevoCliente();
                primero.SetActive(false);
                segundo.SetActive(false);
                tercero.SetActive(false);
                cuarto.SetActive(false);
                GameFlow.orderAcepted = true;
            }
            else
            {
                Debug.Log("PUZZLE TERMINADO");
            }
        }
    }
}
