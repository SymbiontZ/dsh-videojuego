using UnityEngine;

public class OscilacionVertical : MonoBehaviour
{
    [Header("Configuración de Oscilación")]
    [Tooltip("Altura mínima del objeto")]
    public float alturaMinima = 1f;
    [Tooltip("Altura máxima del objeto")]
    public float alturaMaxima = 3f;
    [Tooltip("Velocidad de oscilación (ciclos por segundo)")]
    public float velocidad = 1f;
    
    [Header("Opciones")]
    [Tooltip("Si está activado, la oscilación comenzará automáticamente")]
    public bool iniciarAutomaticamente = true;
    [Tooltip("Si está activado, mostrará información de depuración")]
    public bool modoDebug = false;
    
    private Vector3 posicionInicial;
    private bool estaOscilando = false;
    private float tiempoInicio;

    void Start()
    {
        posicionInicial = transform.position;
        
        if (iniciarAutomaticamente)
        {
            IniciarOscilacion();
        }
    }

    void Update()
    {
        if (estaOscilando)
        {
            // Calcula la oscilación usando una función seno
            float rango = alturaMaxima - alturaMinima;
            float alturaActual = alturaMinima + (Mathf.Sin((Time.time - tiempoInicio) * velocidad * 2 * Mathf.PI) * rango / 2 + rango / 2);
            
            // Aplica la nueva posición
            transform.position = new Vector3(posicionInicial.x, posicionInicial.y + alturaActual, posicionInicial.z);
            
            if (modoDebug)
            {
                Debug.Log($"Altura actual: {alturaActual}");
            }
        }
    }

    public void IniciarOscilacion()
    {
        estaOscilando = true;
        tiempoInicio = Time.time;
        
        if (modoDebug)
        {
            Debug.Log("Oscilación iniciada");
        }
    }

    public void DetenerOscilacion()
    {
        estaOscilando = false;
        transform.position = posicionInicial;
        
        if (modoDebug)
        {
            Debug.Log("Oscilación detenida");
        }
    }

    public void AlternarOscilacion()
    {
        if (estaOscilando)
        {
            DetenerOscilacion();
        }
        else
        {
            IniciarOscilacion();
        }
    }

    // Método para cambiar las alturas dinámicamente
    public void ConfigurarAlturas(float min, float max)
    {
        alturaMinima = min;
        alturaMaxima = max;
        
        if (modoDebug)
        {
            Debug.Log($"Nuevas alturas configuradas: Min={min}, Max={max}");
        }
    }
}