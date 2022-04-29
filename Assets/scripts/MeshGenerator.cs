using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes sure, that the object on which this class is attached to, also has a meshfilter
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    public int heightValue = 2;
    public int xSize, zSize;
    public float xScale = .3f;
    public float zScale = .3f;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    GameObject cutoffSlider;

    void OnValidate()
    {
        xSize = 100;
        zSize = 100;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        createShape();
        updateMesh();
    }


    void createShape()
    {
        // float cutoffValue = cutoffSlider.transform.GetChild(0).transform.localPosition.y;
        // create array with a given size
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        // generate vertices 
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // calculate height value
                // TODO map this to a slider value 
                float y = Mathf.PerlinNoise(x * xScale, z * zScale) * heightValue;

                // float y = Mathf.PerlinNoise(
                //     Mathf.PerlinNoise(x * xScale, z * zScale) * heightValue * 2f,
                //     Mathf.PerlinNoise(x * xScale, z * zScale) * heightValue * 5f
                // );

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // generate triangles
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void Update()
    {
        // TODO put this in a OnSliderChanged method
        updateMesh();
    }

    /// <summary>
    /// clear the mesh data and recalculate the positions and normals of all vertices
    /// </summary>
    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
