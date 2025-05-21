using UnityEngine;

public class OutlineResaltado : MonoBehaviour, IResaltable
{
    [SerializeField] private Material outlineMaterial;

    private Renderer rend;
    private Material[] originalMaterials;

    private bool resaltadoActivo = false;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void ActivarResaltado()
    {
        if (resaltadoActivo) return;
        resaltadoActivo = true;

        Debug.Log("Activando resaltado en: " + gameObject.name);

        if (rend == null || outlineMaterial == null) return;

        originalMaterials = rend.materials; // Guardamos los materiales actuales

        Material[] newMaterials = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = outlineMaterial;

        rend.materials = newMaterials;
    }

    public void DesactivarResaltado()
    {
        if (!resaltadoActivo) return;
        resaltadoActivo = false;

        if (rend != null && originalMaterials != null)
        {
            rend.materials = originalMaterials;
        }
    }
}


