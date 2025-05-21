using UnityEngine;

public class Proyectil : MonoBehaviour
{

    private NaveJugador jugador;

    public void SetJugador(NaveJugador jugadorRef)
    {
        jugador = jugadorRef;
    }
    void Update()
    {
        if (GameManager2.Instance != null && GameManager2.Instance.gameOver) return;

        transform.Translate(new Vector3(0, 7 * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.Morir();
            }
            jugador.contador++;
            if (jugador.contador == 32)
            {
                Debug.Log("Win");
                GameManager2.Instance.Win();
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}