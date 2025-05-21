using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    private List<AudioSource> audioPool;
    void Awake()
    {
        audioPool = new List<AudioSource>();
        for (int i = 0; i < 3; i++)
        {
            CrearAudioSource();
        }
    }
    private AudioSource CrearAudioSource()
    {
        GameObject gameObject = new GameObject("PooledAudioSourceSFX");
        gameObject.transform.SetParent(this.transform);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioPool.Add(audioSource);
        return audioSource;
    }
    private AudioSource ObtenerAudioSource()
    {
        foreach (AudioSource audioSource in audioPool)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        return CrearAudioSource();
    }

    public void ReproducirSFX(AudioClip clip, Transform transform)
    {
        AudioSource audioSource = ObtenerAudioSource();
        audioSource.transform.position = transform.position;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ReproducirSFX(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = ObtenerAudioSource();
        audioSource.transform.position = position;
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    public void DetenerSFX()
    {
        foreach (AudioSource audioSource in audioPool)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
