using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Importa el espacio de nombres para la gestión de escenas

public class Jumpscare : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    private AnimatorStateInfo stateInfo;
    public Animator animator; // Referencia al Animator del objeto que contiene el script
    bool sonido = false; // Variable para controlar la recarga de la escena
    public GameObject Camara; // Referencia al objeto que contiene el jumpscare

    void Start()
    {
        anim = GetComponent<Animator>();
        stateInfo = anim.GetCurrentAnimatorStateInfo(0); // 0 = capa base
    }
    

    // Update is called once per frame
    void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Jumpscare") && !sonido)
        {
            // Si el estado actual es "Jumpscare" y el sonido no ha sido reproducido
            sonido = true; // Cambia la variable a true para evitar que se ejecute de nuevo
            // Entonces ejecuta los audios
            AudioSource[] audioSources = Camara.GetComponents<AudioSource>();
            audioSources[0].Play(); // Reproduce el primer audio
            audioSources[1].Play(); // Reproduce el segundo audio
        }
    }
}
