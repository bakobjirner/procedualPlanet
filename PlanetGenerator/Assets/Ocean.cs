using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    [SerializeField,HideInInspector]
    MeshFilter[] oceanMeshFilters;
    OceanFace[] oceanFaces;

    [Range(2,256)]
    public int resolution = 10;

    public OceanSettings oceanSettings;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }


    void Initialize()
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
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                oceanMeshFilters[i] = meshObj.AddComponent<MeshFilter>();
                oceanMeshFilters[i].sharedMesh = new Mesh();
            }
            oceanMeshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = oceanSettings.oceanMaterial;

                oceanFaces[i] = new OceanFace(oceanMeshFilters[i].sharedMesh, resolution, directions[i], oceanSettings.radius);
            }
        
    }

    void GenerateMesh()
    {
        foreach(OceanFace face in oceanFaces)
        {
            face.ConstructMesh();
        }
    }
}
