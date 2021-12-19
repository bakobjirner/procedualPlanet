using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanGenerator
{
 

    public static void CreateIco(GameObject gameObject, OceanSettings settings)
    {
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
        vertices = ApplyRadius(settings.oceandepth, vertices);

        
        mesh.vertices = vertices;

        mesh.triangles = data.faces;

        mesh.normals = data.normals;

        //recalculate mesh
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
    }

    private static Vector3[] ApplyRadius(float radius, Vector3[] vertices)
    {
        Vector3[] v = vertices;
        for(int i = 0; i < v.Length; i++)
        {
            Vector3 vertice = v[i].normalized;
            vertice *= radius;
            v[i] = vertice;
        }
        return v;
    }

}
