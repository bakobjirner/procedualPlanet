using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    [SerializeField, HideInInspector]
    MeshFilter[] oceanMeshFilters;
    OceanFace[] oceanFaces;

    [Range(2,256)]
    public int resolution = 10;

    public PlanetSettings planetSettings;
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    private void OnValidate()
    {
        GeneratePlanet();
    }


    void Initialize()
    {
        shapeGenerator.UpdateSettings(planetSettings);
        colorGenerator.UpdateSettings(planetSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
            terrainFaces = new TerrainFace[6];
            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };


            for (int i = 0; i < 6; i++)
            {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = planetSettings.planetMaterial;

                terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i], shapeGenerator);
            }
        
    }

    public void OnPlanetSettingsUpdated()
    {
        GeneratePlanet();
    }

    void GenerateMesh()
    {
        foreach(TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
        colorGenerator.updateColors();
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        InitializeOcean();
        GenerateOceanMesh();
    }

    //ocean stuff
    void InitializeOcean()
    {
        if (oceanMeshFilters == null || oceanMeshFilters.Length == 0)
        {
            oceanMeshFilters = new MeshFilter[6];
        }
        oceanFaces = new OceanFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        for (int i = 0; i < 6; i++)
        {
            if (oceanMeshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("oceanMesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                oceanMeshFilters[i] = meshObj.AddComponent<MeshFilter>();
                oceanMeshFilters[i].sharedMesh = new Mesh();
            }
            oceanMeshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = planetSettings.oceanMaterial;

            oceanFaces[i] = new OceanFace(oceanMeshFilters[i].sharedMesh, resolution, directions[i], planetSettings.radius + planetSettings.oceanDepth);
        }

    }

    void GenerateOceanMesh()
    {
        foreach (OceanFace face in oceanFaces)
        {
            face.ConstructMesh();
        }
    }
}
