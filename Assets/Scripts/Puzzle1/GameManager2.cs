using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }
    public bool gameOver { get; private set; }
    public Button restartButton;

    void Awake()
    {/*
        Jugador.Instance.DesactivarJugador();
        GameManager.Instance.gameObject.SetActive(false);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        */
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        restartButton.gameObject.SetActive(false);

    }

    public void GameOver()
    {
        gameOver = true;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Puzzle1");
    }

    public void Win()
    {
        gameOver = true;
        GameManager.Instance.sceneController.CargarEscenaDelay("Nivel1_Alter");
    }
}