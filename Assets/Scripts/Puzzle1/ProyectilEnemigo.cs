using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    void Update()
    {
        if (GameManager2.Instance.gameOver)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(new Vector3(0, 5 * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Nave"))
        {
            NaveJugador jugador = collision.gameObject.GetComponent<NaveJugador>();
            if (jugador != null)
            {
                jugador.Morir();
                Debug.Log("Game Over");
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}