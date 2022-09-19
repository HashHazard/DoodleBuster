using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using TMPro;

public class animTest : MonoBehaviour
{
    SpriteRenderer _renderer;
    public TextMeshProUGUI text;
    
    private void Start()
    {
        //_renderer = GetComponent<SpriteRenderer>();
        //_renderer.DOColor(Color.red, 0.5f).SetLoops(-1, LoopType.Yoyo);
        //transform.DOShakeScale(1.8f, 3f, 20, 100, true).SetLoops(-1, LoopType.Restart);
        
    }

    Vector2 currentVelocity;
    float turnSpeed = 360;
    float angle;
    float cVelocity;
    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        //Debug.Log(mousePosition);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        text.SetText(mousePosition.ToString());

        //--Normally following the mouse
        //transform.position = mousePosition;

        //--Moving towards the mouse with a constant velocity
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, 10 * Time.deltaTime);

        //--Moving towards the mouse with a constant velocity and damping
        //transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, 0.3f, 10f);

        //--Moving towards the mouse with an offset of 2f
        //mousePosition += ((Vector2)transform.position - mousePosition).normalized * 2f;
        //transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, 0.3f, 20f);

        //--Rotating towards the mouse normally
        Vector2 direction = mousePosition - (Vector2)transform.position;
        //float angle = Vector2.SignedAngle(Vector2.right, direction);
        //transform.eulerAngles = new Vector3(0f, 0f, angle);

        //--Rotating smoothly with a contant speed
        //Vector3 targetRotation = new Vector3(0, 0, angle);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed * Time.deltaTime);


        //--Smoothdamped rotation
        float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref cVelocity, 0.3f, turnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);

    }
    //[SerializeField] private Transform _diamond;
    //[SerializeField] private float _cycleLength = 2f;

    //[SerializeField] private float duration = 1f;
    //[SerializeField] private float strength = 1f;
    //[SerializeField] private int vibrato = 10;
    //[SerializeField] private float randomness = 90f;
    //[SerializeField] private bool fadeOut = true;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //transform.DOMove(new Vector3(10f, transform.position.y, 0f), _cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    //    _diamond.DORotate(new Vector3(0, 360f, 0), _cycleLength * 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    //    //transform.DOShakeScale(duration, strength, vibrato,randomness,fadeOut).SetLoops(-1, LoopType.Restart);
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
