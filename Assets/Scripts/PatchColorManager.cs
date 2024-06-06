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
    private float downTime = 0f;
    private float stayTime = 1f;

    public Material defaultMaterial;
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
            // 记录鼠标按下时间
            downTime = Time.time;
            // 获取鼠标坐标
            Vector3 mousePos = Input.mousePosition;
            // 射线检测
            ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.transform.tag == "Patch")
                {
                    // 赋值，等待鼠标松开时判断
                    patch = raycastHit.transform.gameObject;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (showColorPanel) return;
            if (Time.time - downTime < stayTime) return;
            // 获取鼠标坐标
            Vector3 mousePos = Input.mousePosition;
            // 射线检测
            ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.transform.tag == "Patch")
                {
                    if (patch != raycastHit.transform.gameObject) return;
                    // 颜色面板移动到点击位置并打开
                    colorPanel.transform.localPosition = new Vector3(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2, 0f); ;
                    colorPanel.transform.localScale = Vector3.zero;
                    colorPanel.transform.DOScale(1f, 0f);
                    // 更改状态
                    showColorPanel = true;
                    // 暂时变为默认材质
                    if (patch != null)
                    {
                        patch.GetComponent<MeshRenderer>().material = defaultMaterial;
                    }
                }
            }
        }
    }

    private void TapRaycast()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("touch began");
            if (showColorPanel) return;
            // 记录点击按下时间
            downTime = Time.time;
            // 获取点击坐标
            Vector3 tapPos = Input.GetTouch(0).position;
            // 射线检测
            ray = Camera.main.ScreenPointToRay(tapPos);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.transform.tag == "Patch")
                {
                    // 赋值，等待点击松开时判断
                    patch = raycastHit.transform.gameObject;
                }
            }
        }

        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Debug.Log("touch ended");
            if (showColorPanel) return;
            if (Time.time - downTime < stayTime) return;
            // 获取点击坐标
            Vector3 tapPos = Input.GetTouch(0).position;
            // 射线检测
            ray = Camera.main.ScreenPointToRay(tapPos);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.transform.tag == "Patch")
                {
                    if (patch != raycastHit.transform.gameObject) return;
                    // 颜色面板移动到点击位置并打开
                    colorPanel.transform.localPosition = new Vector3(tapPos.x - Screen.width / 2, tapPos.y - Screen.height / 2, 0f); ;
                    colorPanel.transform.localScale = Vector3.zero;
                    colorPanel.transform.DOScale(1f, 0f);
                    // 更改状态
                    showColorPanel = true;
                    // 暂时变为默认材质
                    if (patch != null)
                    {
                        patch.GetComponent<MeshRenderer>().material = defaultMaterial;
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
