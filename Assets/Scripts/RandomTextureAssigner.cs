using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class RandomTextureAssigner : MonoBehaviour
{
    [SerializeField] private List<Texture> textures = new List<Texture>();
    [SerializeField] private Material baseMaterial;
    
    private void Start()
    {
        ApplyRandomTexture();
    }

    public void ApplyRandomTexture()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        // Crear nueva instancia del material
        Material materialInstance;
        if (baseMaterial != null)
        {
            materialInstance = new Material(baseMaterial);
        }
        else
        {
            materialInstance = new Material(Shader.Find("Standard"));
        }

        // Asignar textura aleatoria si hay disponibles
        if (textures.Count > 0)
        {
            int randomIndex = Random.Range(0, textures.Count);
            if (textures[randomIndex] != null)
            {
                materialInstance.mainTexture = textures[randomIndex];
                materialInstance.name = "Mat_" + textures[randomIndex].name;
            }
        }

        renderer.sharedMaterial = materialInstance;
    }

    public void AddTexture(Texture newTexture)
    {
        if (textures == null) textures = new List<Texture>();
        if (newTexture != null) textures.Add(newTexture);
    }
}