using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchColorManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject patch;

    public List<Material> patchMaterials;

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            TapRaycast();
        }
        else
        {
            MouseRaycast();
        }
    }

    private void MouseRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.transform.tag == "Patch")
                {
                    SetPatchColor(1);
                    patch = raycastHit.transform.gameObject;
                }
            }
        }
    }

    private void TapRaycast()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if(Input.GetTouch(i).tapCount == 2)
                {
                    Debug.Log("双击检测");
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out raycastHit))
                    {
                        if (raycastHit.transform.tag == "Patch")
                        {
                            SetPatchColor(1);
                            patch = raycastHit.transform.gameObject;
                        }
                    }
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
