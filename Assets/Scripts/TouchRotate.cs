using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TouchRotate : MonoBehaviour
{
    public GameObject cube; // 确保这个引用在Inspector中被设置  

    private Vector3 startMousePosition;
    private Vector3 previousMousePosition;
    private float rotationSpeed = 50F; // 控制旋转速度  

    // Start is called before the first frame update  
    void Start()
    {
        // 可以在这里初始化一些值，但目前我们不需要  
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            previousMousePosition = startMousePosition; // 也可以在这里设置  
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - previousMousePosition;

            // 根据delta的值计算旋转角度  
            float rotX = -delta.x * rotationSpeed * Time.deltaTime;
            float rotY = -delta.y * rotationSpeed * Time.deltaTime;   

            // 创建一个Quaternion来表示旋转  
            Quaternion rotation = Quaternion.Euler(0, rotX, rotY);

            // 应用旋转到cube  
            cube.transform.Rotate(rotation.eulerAngles);

            // 更新previousMousePosition为当前值以便下一帧使用  
            previousMousePosition = currentMousePosition;
        }
    }
}
