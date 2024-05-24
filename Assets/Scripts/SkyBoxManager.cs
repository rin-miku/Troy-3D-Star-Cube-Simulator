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

        Tween tween = transform.DORotate(new Vector3(0f, 359.9f, 0f), rotateTime)
            .SetEase(Ease.Linear) 
            .SetLoops(-1, LoopType.Restart);

        tween.Play();
    }
}
