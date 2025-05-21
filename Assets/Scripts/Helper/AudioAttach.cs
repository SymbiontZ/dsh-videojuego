using UnityEngine;

[DisallowMultipleComponent]
public class AudioAttach : MonoBehaviour
{
    public AudioClip audioRecoger;
    public AudioClip audioSalir;

    public AudioClip ObtenerClip(string nombre)
    {
        switch (nombre.ToLower())
        {
            case "recoger":
                return audioRecoger;
            case "salir":
                return audioSalir;
            default:
                Debug.LogWarning($"AudioAttach: No se encontró un clip con el nombre '{nombre}' en {gameObject.name}");
                return null;
        }
    }

}
