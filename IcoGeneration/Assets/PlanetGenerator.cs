using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator
{
   
    private static PlanetSettings settings;
    public static MinMax elevationMinMax;

    public static void CreateIco(GameObject gameObject, PlanetSettings settings)
    {
        elevationMinMax = new MinMax();
        PlanetGenerator.settings = settings;
        //get mesh of Gameobject
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        Mesh mesh;
        if (filter.sharedMesh != null)
        {
            mesh = filter.sharedMesh;
        }
        else
        {
            mesh = new Mesh();
            filter.sharedMesh = mesh;
        }
        //clear mesh if not empty
        mesh.Clear();
        //set indexformat to allow for meshes with more than 65536 Vertices
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        SphereMeshData data = SphereGenerator.GetSphere(settings.detailLevel);

        Vector3[] vertices = data.vertices;
        vertices = ApplyNoiseToSphere(vertices);
        //list to array
        mesh.vertices = vertices;
        mesh.triangles = data.faces;
        mesh.normals = data.normals;

        //recalculate mesh
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();

            if (gameObject.GetComponent<MeshCollider>() != null)
            {
                gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            }
            else
            {
                gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
            }
        AddCollider(gameObject, mesh);

    }

    public static Vector3[] ApplyNoiseToSphere(Vector3[] vertices)
    {
        elevationMinMax = new MinMax();

        Vector3[] v = vertices;

        for (int i = 0; i < v.Length; i++)
        {
            v[i] = ApplyNoise(v[i], settings.noiseLayers);
        }

        return v;
    }

    private static void AddCollider(GameObject gameObject, Mesh mesh)
    {
        if (gameObject.GetComponent<MeshCollider>() != null)
        {
            gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
        else
        {
            gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        }
    }


    private static Vector3 ApplyNoise(Vector3 vertice, NoiseLayer[] noiseLayers)
    {
        float firstLayerValue = 0;
        float elevation = 0;
        Vector3 point = vertice.normalized;

        if (noiseLayers.Length > 0)
        {
            if (noiseLayers[0].enabled)
            {
                elevation = NoiseGenerator.ApplyNoise(point, noiseLayers[0].noiseSettings);
                firstLayerValue = elevation;
            }
        }

        for(int i = 1; i < noiseLayers.Length; i++)
        {
            if (noiseLayers[i].enabled)
            {
                NoiseSettings settings = noiseLayers[i].noiseSettings;
                float mask = (noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
               elevation += NoiseGenerator.ApplyNoise(point, settings)*mask;
            }
        }
        elevation = settings.radius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return point * elevation;
    }
}
