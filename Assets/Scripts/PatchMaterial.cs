using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchMaterial : MonoBehaviour
{
    public Material patchMaterial;

    // 留存最初的材质 方便重置
    private void Start()
    {
        patchMaterial = GetComponent<MeshRenderer>().material;
    }
}
