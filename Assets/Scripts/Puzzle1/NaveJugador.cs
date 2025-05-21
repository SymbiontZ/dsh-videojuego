using UnityEngine;

public class NaveJugador : MonoBehaviour
{
    public GameObject jugador, proyectil, explosionPrefab;
    public AudioSource disparo;
    public AudioClip explosionSound;
    public Camera camera;
    public int contador = 0;
    private GameObject proyectilClon;
    private float minX, maxX;
    private float naveHalfWidth = 0.5f; // Valor por defecto

    void Start()
    {
        // Inicialización del AudioSource
        disparo = GetComponent<AudioSource>();

        // Calculamos los límites de pantalla de forma segura
        CalcularLimitesPantalla();
    }

    void CalcularLimitesPantalla()
    {
        // Intentamos obtener el ancho de la nave de forma segura
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            naveHalfWidth = spriteRenderer.bounds.extents.x;
        }
        else
        {
            // Si no hay SpriteRenderer, usamos el valor por defecto
            Debug.LogWarning("No se encontró SpriteRenderer en la nave, usando valor por defecto para el ancho");
            naveHalfWidth = 0.5f;
        }

        // Calculamos los límites de pantalla
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(cameraHeight * screenAspect, cameraHeight);

        Vector3 cameraPos = camera.transform.position;
        minX = cameraPos.x - cameraSize.x / 2 + naveHalfWidth;
        maxX = cameraPos.x + cameraSize.x / 2 - naveHalfWidth;
    }

    void Update()
    {
        if (GameManager2.Instance.gameOver) return;

        Movimiento();
        FireProjectile();
    }

    void Movimiento()
    {
        float movimiento = 0f;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movimiento = 8 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movimiento = -8 * Time.deltaTime;
        }

        // Calculamos la nueva posición
        float nuevaPosX = transform.position.x + movimiento;

        // Aplicamos límites
        nuevaPosX = Mathf.Clamp(nuevaPosX, minX, maxX);

        // Movemos la nave
        transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);
    }

    void FireProjectile()
    {
        if (Input.GetMouseButtonDown(0) && proyectilClon == null)
        {
            proyectilClon = Instantiate(proyectil, new Vector3(jugador.transform.position.x, jugador.transform.position.y + 1f, jugador.transform.position.z), jugador.transform.rotation) as GameObject;
            Proyectil p = proyectilClon.GetComponent<Proyectil>();
            p.SetJugador(this);
            if (disparo != null) disparo.Play();
        }
    }

    public void Morir()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 3f);

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab,
                new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity);

            explosion.SetActive(true);
            Destroy(explosion, 2f);
        }

        gameObject.SetActive(false);
        GameManager2.Instance.GameOver();
    }
}