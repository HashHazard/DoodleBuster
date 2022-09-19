using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _rotationSpeed = 1.5f;

    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerController>().transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null) return;

        // Look direction
        Vector2 dir = _target.position - transform.position;

        transform.up = Vector3.MoveTowards(transform.up, dir, _rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, _speed * Time.deltaTime);
    }
}
