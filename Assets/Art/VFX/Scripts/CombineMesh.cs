using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineMesh : MonoBehaviour
{
    void Start()
    {
        List<MeshFilter> meshFilters = GetComponentsInChildren<MeshFilter>().ToList();
        CombineInstance[] combine = new CombineInstance[meshFilters.Count];

        int i = 0;

        foreach (var filter in meshFilters)
        {
            combine[i].mesh = filter.sharedMesh;
            //combine[i].transform = filter.transform.localToWorldMatrix;
            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);
    }
}
