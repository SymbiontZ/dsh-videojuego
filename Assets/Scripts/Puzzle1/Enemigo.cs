using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public GameObject enemy, enemyProyectil, enemyProyectilClon, explosionPrefab;
    public AudioClip explosionSound;
    private float timer = 0f, timeToMove = 0.5f, speed = -0.35f;
    private int numOfMovement = 0;

    void Update()
    {
        if (GameManager2.Instance.gameOver) return;

        if (numOfMovement == 18)
        {
            transform.Translate(new Vector3(0, 1f, 0));
            numOfMovement = 0;
            speed = -speed;
            timer = 0f;
        }

        timer += Time.deltaTime;
        if (timer > timeToMove && numOfMovement < 18)
        {
            transform.Translate(new Vector3(speed, 0, 0));
            timer = 0f;
            numOfMovement++;
        }

        FireEnemyProyectile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager2.Instance.GameOver();
            Debug.Log("Game Over");
        }
    }

    public void Morir()
    {
        // 1. Desactiva render y colliders
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // 2. Reproduce sonido
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // 3. Instancia explosión
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab,
                new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                Quaternion.identity);

            // Asegúrate de que la explosión está activa
            explosion.SetActive(true);

            // Destruye la explosión después de 2 segundos
            Destroy(explosion, 2f);
        }

        // 4. Destruye el enemigo después de un pequeño delay
        Destroy(gameObject, 0.1f); // Pequeño delay para asegurar que todo se ejecuta
    }

    void FireEnemyProyectile()
    {
        if (GameManager2.Instance.gameOver) return;

        if (Random.Range(0f, 10000f) < 1f)
        {
            enemyProyectilClon = Instantiate(enemyProyectil, new Vector3(enemy.transform.position.x, enemy.transform.position.y - 1f, enemy.transform.position.z), enemy.transform.rotation) as GameObject;
        }
    }
}