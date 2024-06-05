using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatchColorManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject patch;
    private GameObject colorPanel;

    private bool showColorPanel = false;

    public List<Material> patchMaterials;

    private void Start()
    {
        colorPanel = GameObject.Find("ColorPanel");
    }

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
            if (showColorPanel) return;
            Vector3 mousePos = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.transform.tag == "Patch")
                {
                    // 颜色面板移动到点击位置并打开
                    colorPanel.transform.localPosition = new Vector3(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2, 0f); ;
                    colorPanel.transform.localScale = Vector3.zero;
                    colorPanel.transform.DOScale(1f, 1f);
                    // 更改状态
                    showColorPanel = true;
                    
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
                            //SetPatchColor(1);
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
        showColorPanel = false;
    }
}
