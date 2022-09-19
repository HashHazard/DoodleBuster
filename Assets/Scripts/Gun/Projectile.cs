using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Projectile : MonoBehaviour
{
    public float speed = 9f;
    [SerializeField] private bool enemyBullet = false;

    //private TrailRenderer trail;

    private Action<Projectile> _killAction;

    public void Init(Action<Projectile> killAction)
    {
        _killAction = killAction;
    }
    private void Awake()
    {
        //if (!enemyBullet)
        //    trail = GetComponent<TrailRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        //Invoke("DisableProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.magnitude > 22f)
        {
            DisableProjectile();
        }
    }

    //public void ClearTrail()
    //{
    //    trail.Clear();
    //}

    void DisableProjectile()
    {
        //if (!enemyBullet)
        //{
        //    trail.Clear();
        //}
        _killAction(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool e = collision.CompareTag("Enemy1") || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3") || collision.CompareTag("Enemy4") || collision.CompareTag("Enemy5");
        if (e && !enemyBullet)
        {

            DisableProjectile();

        }

        if (collision.CompareTag("EnemyBullet") && !enemyBullet)
        {
            DisableProjectile();
        }
        else if (collision.CompareTag("Bullet") && enemyBullet)
        {
            DisableProjectile();
        }

        if (enemyBullet && collision.CompareTag("Player"))
        {
            DisableProjectile();
        }

    }
}
