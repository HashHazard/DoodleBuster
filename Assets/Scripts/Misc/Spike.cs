using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spike : MonoBehaviour
{
    [SerializeField] private float endValue = 1f;
    [SerializeField] private float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(endValue + transform.position.y, duration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
