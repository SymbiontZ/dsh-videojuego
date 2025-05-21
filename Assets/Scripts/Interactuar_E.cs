using UnityEngine;

public class InteractuarConPuerta : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    [SerializeField] private float distanciaInteraccion = 3f; // Distancia máxima para interactuar

    private Transform jugador;

    void Start()
    {
        // Se obtiene la referencia al jugador
        jugador = Camera.main.transform;
    }

    void Update()
    {
        // Obtener el Collider del cubo (puerta)
        Collider puertaCollider = GetComponent<Collider>();

        // Comprobar si el jugador está dentro del rango de interacción con el cubo
        float distancia = Vector3.Distance(jugador.position, puertaCollider.ClosestPoint(jugador.position));

        // Si el jugador está cerca del cubo y presiona la tecla E
        if (distancia <= distanciaInteraccion)
        {
            // Mostrar un mensaje en la consola
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("¡Interactuaste con la puerta!");
            }
        }
    }
}
