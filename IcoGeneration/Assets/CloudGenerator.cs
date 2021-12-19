using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    //create list of vertices TODO: replace with array
    private static List<Vector3> vertList = new List<Vector3>();
    //TODO: check
    private static Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

    public static void CreateIco(GameObject gameObject, CloudSettings settings)
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
        //reset lists
        middlePointIndexCache = new Dictionary<long, int>();
        vertList = new List<Vector3>();

        // create 12 vertices of a icosahedron
        float t = (1f + Mathf.Sqrt(5f)) / 2f;

        vertList.Add(new Vector3(-1f, t, 0f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(1f, t, 0f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(-1f, -t, 0f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(1f, -t, 0f).normalized * settings.cloudSize);

        vertList.Add(new Vector3(0f, -1f, t).normalized * settings.cloudSize);
        vertList.Add(new Vector3(0f, 1f, t).normalized * settings.cloudSize);
        vertList.Add(new Vector3(0f, -1f, -t).normalized * settings.cloudSize);
        vertList.Add(new Vector3(0f, 1f, -t).normalized * settings.cloudSize);

        vertList.Add(new Vector3(t, 0f, -1f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(t, 0f, 1f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(-t, 0f, -1f).normalized * settings.cloudSize);
        vertList.Add(new Vector3(-t, 0f, 1f).normalized * settings.cloudSize);

        // create 20 triangles of the icosahedron
        List<Triangle> faces = new List<Triangle>();

        // 5 faces around point 0
        faces.Add(new Triangle(0, 11, 5));
        faces.Add(new Triangle(0, 5, 1));
        faces.Add(new Triangle(0, 1, 7));
        faces.Add(new Triangle(0, 7, 10));
        faces.Add(new Triangle(0, 10, 11));

        // 5 adjacent faces 
        faces.Add(new Triangle(1, 5, 9));
        faces.Add(new Triangle(5, 11, 4));
        faces.Add(new Triangle(11, 10, 2));
        faces.Add(new Triangle(10, 7, 6));
        faces.Add(new Triangle(7, 1, 8));

        // 5 faces around point 3
        faces.Add(new Triangle(3, 9, 4));
        faces.Add(new Triangle(3, 4, 2));
        faces.Add(new Triangle(3, 2, 6));
        faces.Add(new Triangle(3, 6, 8));
        faces.Add(new Triangle(3, 8, 9));

        // 5 adjacent faces 
        faces.Add(new Triangle(4, 9, 5));
        faces.Add(new Triangle(2, 4, 11));
        faces.Add(new Triangle(6, 2, 10));
        faces.Add(new Triangle(8, 6, 7));
        faces.Add(new Triangle(9, 8, 1));

        //subdivide to desired level
        faces = SubdivideFaces(faces, settings.detailLevel, settings.cloudSize);

        //list to array
        mesh.vertices = vertList.ToArray();

        //create the list of triangles (always 3 consecutive points in list are one triangle)
        List<int> triList = new List<int>();
        for (int i = 0; i < faces.Count; i++)
        {
            triList.Add(faces[i].v1);
            triList.Add(faces[i].v2);
            triList.Add(faces[i].v3);
        }
        mesh.triangles = triList.ToArray();


        //set normals, simply use position of the point so all normals point outward
        Vector3[] normales = new Vector3[vertList.Count];
        for (int i = 0; i < normales.Length; i++)
        {
            normales[i] = vertList[i].normalized;
        }
        mesh.normals = normales;

        //recalculate mesh
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
    }


    private static List<Triangle> SubdivideFaces(List<Triangle> faces, int numberOfSubdivisions, float radius)
    {

        //in case somebody enters a value that is to high. do not remove if you aren't absolutely sure what youre doing. Will 
        if (numberOfSubdivisions > 8)
        {
            numberOfSubdivisions = 2;
        }

        // refine triangles
        for (int i = 0; i < numberOfSubdivisions; i++)
        {
            List<Triangle> facesDivided = new List<Triangle>();
            foreach (var tri in faces)
            {
                // replace triangle by 4 triangles
                int a = MiddlePoint(tri.v1, tri.v2, radius);
                int b = MiddlePoint(tri.v2, tri.v3, radius);
                int c = MiddlePoint(tri.v3, tri.v1, radius);

                facesDivided.Add(new Triangle(tri.v1, a, c));
                facesDivided.Add(new Triangle(tri.v2, b, a));
                facesDivided.Add(new Triangle(tri.v3, c, b));
                facesDivided.Add(new Triangle(a, b, c));
            }
            faces = facesDivided;
        }
        return faces;
    }



    // return index of vertice in the middle of p1 and p2, creates new vertice if it doesn't exist yet
    private static int MiddlePoint(int p1, int p2, float radius)
    {
        // get key of searched point in dictionary by combining the indexes of the two points
        bool firstIsSmaller = p1 < p2;
        long smallerIndex = firstIsSmaller ? p1 : p2;
        long greaterIndex = firstIsSmaller ? p2 : p1;
        //key is smaller index followed by larger index
        long key = (smallerIndex << 32) + greaterIndex;

        //check if point exists
        int indexMiddle;
        if (middlePointIndexCache.TryGetValue(key, out indexMiddle))
        {
            //if the point already exists, return its index
            return indexMiddle;
        }

        // if it doesn't exist, calculate it
        Vector3 point1 = vertList[p1];
        Vector3 point2 = vertList[p2];
        Vector3 middle = new Vector3
        (
            (point1.x + point2.x) / 2f,
            (point1.y + point2.y) / 2f,
            (point1.z + point2.z) / 2f
        );

        //normalize point to make sure its on the sphere
        middle = middle.normalized * radius;

        // add vertex to list
        int i = vertList.Count;
        vertList.Add(middle);

        // store it to dictonary, return index
        middlePointIndexCache.Add(key, i);

        return i;
    }
}