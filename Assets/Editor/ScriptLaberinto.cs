using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ScriptLaberinto : EditorWindow
{
    public Texture2D mazeMap;
    public GameObject wallPrefab;
    public float wallHeight = 1f;
    public float pixelToUnitScale = 0.1f;
    public bool combineMeshes = true;
    public bool generateColliders = true;
    public int maxVerticesPerChunk = 60000;
    public float redThreshold = 0.5f;

    [MenuItem("Tools/Generar Laberinto desde Imagen")]
    public static void ShowWindow()
    {
        GetWindow<ScriptLaberinto>("Generador de Laberinto");
    }

    void OnGUI()
    {
        GUILayout.Label("Configuración del Laberinto", EditorStyles.boldLabel);
        
        mazeMap = (Texture2D)EditorGUILayout.ObjectField("Imagen del Laberinto", mazeMap, typeof(Texture2D), false);
        wallPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab de Pared", wallPrefab, typeof(GameObject), false);
        wallHeight = EditorGUILayout.FloatField("Altura de Pared", wallHeight);
        pixelToUnitScale = EditorGUILayout.FloatField("Escala (Píxel a Unidad)", pixelToUnitScale);
        redThreshold = EditorGUILayout.Slider("Umbral Rojo", redThreshold, 0f, 1f);
        combineMeshes = EditorGUILayout.Toggle("Combinar Mallas", combineMeshes);
        generateColliders = EditorGUILayout.Toggle("Generar Colliders", generateColliders);

        if (GUILayout.Button("Generar Laberinto"))
        {
            GenerarLaberinto();
        }
    }

    void GenerarLaberinto()
    {
        if (mazeMap == null || wallPrefab == null)
        {
            Debug.LogError("Falta asignar la imagen o el prefab de pared!");
            return;
        }

        if (!mazeMap.isReadable)
        {
            Debug.LogError("La textura no es legible. Habilita 'Read/Write Enabled' en las opciones de importación.");
            return;
        }

        // Crear una copia legible de la textura
        Texture2D readableTexture = new Texture2D(mazeMap.width, mazeMap.height);
        readableTexture.SetPixels(mazeMap.GetPixels());
        readableTexture.Apply();

        GameObject laberintoPadre = new GameObject("LaberintoGenerado");
        List<GameObject> paredesGeneradas = new List<GameObject>();
        
        Color[] pixels = readableTexture.GetPixels();
        int width = readableTexture.width;
        int height = readableTexture.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = pixels[y * width + x];
                if (pixel.r < redThreshold) // Usamos solo el canal rojo
                {
                    Vector3 position = new Vector3(
                        x * pixelToUnitScale,
                        wallHeight / 2,
                        y * pixelToUnitScale
                    );
                    GameObject pared = (GameObject)PrefabUtility.InstantiatePrefab(wallPrefab);
                    pared.transform.position = position;
                    pared.transform.localScale = new Vector3(
                        pixelToUnitScale, 
                        wallHeight, 
                        pixelToUnitScale
                    );
                    pared.transform.parent = laberintoPadre.transform;
                    paredesGeneradas.Add(pared);
                }
            }
        }

        if (combineMeshes)
        {
            OptimizarLaberinto(laberintoPadre, paredesGeneradas);
        }
        else
        {
            LimpiarObjetosNoDeseados(laberintoPadre);
        }

        Debug.Log($"Laberinto generado con {paredesGeneradas.Count} paredes (Umbral rojo: {redThreshold})");
    }

    void OptimizarLaberinto(GameObject parent, List<GameObject> paredes)
    {
        List<List<GameObject>> chunks = DividirEnChunks(paredes, maxVerticesPerChunk);

        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunkParent = new GameObject($"LaberintoChunk_{i}");
            chunkParent.transform.parent = parent.transform;
            
            CombineMeshes(chunkParent, chunks[i]);
        }

        foreach (var pared in paredes)
        {
            DestroyImmediate(pared);
        }

        LimpiarObjetosNoDeseados(parent);
        Resources.UnloadUnusedAssets();
    }

    void LimpiarObjetosNoDeseados(GameObject parent)
    {
        HashSet<string> objetosDeseados = new HashSet<string>();
        
        for (int i = 0; i < 100; i++)
        {
            objetosDeseados.Add($"LaberintoChunk_{i}");
        }

        List<GameObject> objetosAEliminar = new List<GameObject>();
        
        foreach (Transform child in parent.transform)
        {
            if (!objetosDeseados.Contains(child.gameObject.name))
            {
                objetosAEliminar.Add(child.gameObject);
            }
        }

        foreach (var obj in objetosAEliminar)
        {
            DestroyImmediate(obj);
        }
    }

    List<List<GameObject>> DividirEnChunks(List<GameObject> objetos, int maxVerticesPorChunk)
    {
        List<List<GameObject>> chunks = new List<List<GameObject>>();
        List<GameObject> currentChunk = new List<GameObject>();
        int currentVertexCount = 0;

        foreach (var obj in objetos)
        {
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            if (mf == null || mf.sharedMesh == null) continue;

            int meshVertexCount = mf.sharedMesh.vertexCount;
            
            if (currentVertexCount + meshVertexCount > maxVerticesPorChunk && currentChunk.Count > 0)
            {
                chunks.Add(currentChunk);
                currentChunk = new List<GameObject>();
                currentVertexCount = 0;
            }

            currentChunk.Add(obj);
            currentVertexCount += meshVertexCount;
        }

        if (currentChunk.Count > 0)
        {
            chunks.Add(currentChunk);
        }

        return chunks;
    }

    void CombineMeshes(GameObject parent, List<GameObject> objetos)
    {
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();

        foreach (var obj in objetos)
        {
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            if (mf != null && mf.sharedMesh != null)
            {
                meshFilters.Add(mf);
            }
        }

        foreach (var mf in meshFilters)
        {
            combineInstances.Add(new CombineInstance()
            {
                mesh = mf.sharedMesh,
                transform = mf.transform.localToWorldMatrix
            });
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true);

        MeshFilter combinedFilter = parent.AddComponent<MeshFilter>();
        combinedFilter.mesh = combinedMesh;

        MeshRenderer combinedRenderer = parent.AddComponent<MeshRenderer>();
        combinedRenderer.sharedMaterial = wallPrefab.GetComponent<MeshRenderer>().sharedMaterial;

        if (generateColliders)
        {
            MeshCollider collider = parent.AddComponent<MeshCollider>();
            collider.sharedMesh = combinedMesh;
            collider.convex = false;
        }

        combinedMesh.RecalculateNormals();
        combinedMesh.Optimize();
    }
}