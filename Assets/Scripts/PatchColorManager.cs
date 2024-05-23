using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchColorManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject patch;

    [Header("Patch Materials")]
    public List<Material> patchMaterials;

    private void Update()
    {
        PatchRaycastCheck();
    }

    private void PatchRaycastCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.transform.tag == "Patch")
                {
                    patch = raycastHit.transform.gameObject;
                }
            }
        }
    }

    public void SetPatchColor(int materialIndex)
    {
        if (patch == null) return;

        patch.GetComponent<MeshRenderer>().material = patchMaterials[materialIndex];

        // 操作结束后置空 防止覆盖设置
        patch = null;
    }
}
