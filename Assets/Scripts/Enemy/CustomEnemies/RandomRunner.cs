using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomRunner : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;   
    }

    private void OnEnable()
    {
        //Vector2 direction = _player.position - transform.position;
        //float angle = Vector2.SignedAngle(Vector2.up, direction);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}
