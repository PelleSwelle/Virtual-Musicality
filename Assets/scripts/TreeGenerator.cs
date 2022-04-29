using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public int noOfTrees;
    List<GameObject> prefabs;
    List<GameObject> trees;
    // needed to get the area on which to instantiate trees.
    MeshGenerator meshGenerator;
    void OnValidate()
    {
        meshGenerator = GameObject.Find("TerrainGenerator").GetComponent<MeshGenerator>();
        prefabs = new List<GameObject>();
        prefabs.Add(GameObject.Find("PineTree1"));
        prefabs.Add(GameObject.Find("PineTree2"));
        prefabs.Add(GameObject.Find("PineTree3"));

        noOfTrees = 200;
        trees = new List<GameObject>(noOfTrees);
    }

    void Start()
    {
        instantiateTrees();
        showTrees(noOfTrees);
    }

    public void updateTrees(int _noOfTrees)
    {
        this.noOfTrees = _noOfTrees;
    }


    public void hideTrees()
    {
        foreach (GameObject tree in this.trees)
        {
            tree.SetActive(false);
        }
    }

    public void showTrees(int _noOfTrees)
    {
        for (int i = 0; i < trees.Count; i++)
        {
            trees[i].SetActive(true);
        }
    }

    /// <summary>
    /// instantiates random prefabs in random positions according to the values given
    /// </summary>
    void instantiateTrees()
    {
        for (int i = 0; i < noOfTrees; i++)
        {
            trees.Add(
                GameObject.Instantiate(
                    // choose prefab
                    prefabs[
                        Random.Range(
                            0, prefabs.Count
                        )
                    ],
                    // where to put it 
                    new Vector3(
                        Random.Range(
                            meshGenerator.transform.position.x, meshGenerator.xSize),
                            meshGenerator.transform.position.y,
                            Random.Range(meshGenerator.transform.position.z, meshGenerator.zSize)
                    ),
                    // how to rotate it
                    Quaternion.identity
                )

            );
        }
    }
}
