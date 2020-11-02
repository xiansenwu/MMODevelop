using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerCheck : MonoBehaviour
{
    public string realSortingLayer = "";
    public int realOrderInLayer = -999;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Renderer mesh = GetComponent<Renderer>();
        //SkinnedMeshRenderer skinMesh = GetComponent<SkinnedMeshRenderer>();
        Canvas canva = GetComponent<Canvas>();
        if (mesh != null)
        {
            realSortingLayer = mesh.sortingLayerName;
            realOrderInLayer = mesh.sortingOrder;
        }
//         else if (skinMesh != null)
//         {
//             realSortingLayer = skinMesh.sortingLayerName;
//             realOrderInLayer = skinMesh.sortingOrder;
//         }
        else if (canva != null)
        {
            realSortingLayer = canva.sortingLayerName;
            realOrderInLayer = canva.sortingOrder;
        }
        else
        {
            realSortingLayer = "";
            realOrderInLayer = -999;
        }

        
    }
}
