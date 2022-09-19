using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spikey : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _enemy;

    private Transform _player;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _enemy.transform.DOLocalMoveY(0.8f, 0.25f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = _player.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        //_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), _speed * Time.deltaTime));
        _rb.SetRotation(angle);
        _rb.MovePosition(_rb.position + ((Vector2)transform.up * _speed * Time.deltaTime));
    }
}
