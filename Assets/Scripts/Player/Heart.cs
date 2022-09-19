using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Heart : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float strength = 1f;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float randomness = 90f;


    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakeScale(1.8f, 3f, 20, 100, true).SetLoops(-1, LoopType.Restart);
        //transform.DOShakePosition(duration, strength, vibrato, randomness, false, true).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void Disappear()
    {
        transform.DOScale(17f, 0);
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
        transform.DOScale(0f, 1.4f).SetEase(Ease.InElastic).OnComplete(SetInactive);

    }
}
