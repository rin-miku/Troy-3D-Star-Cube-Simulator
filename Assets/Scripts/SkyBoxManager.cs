using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyBoxManager : MonoBehaviour
{
    public float rotateTime = 60f;

    private void Start()
    {  
        transform.rotation = Quaternion.Euler(0, 0, 0);

        float newValue = 0f;
        DOTween.To(() => newValue, x => newValue = x, 360f, rotateTime)
            .OnUpdate(()=> 
            {
                transform.rotation = Quaternion.Euler(0f, newValue, 0f);
            })
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
