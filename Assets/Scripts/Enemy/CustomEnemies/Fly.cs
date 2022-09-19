using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public class Fly : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    //[SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private float _positionChangeTime = 5f;
    [SerializeField] private Transform _fly;
    private float localChangeTime = 0f;

    private Vector2 _target = Vector2.zero;
    private Transform _player;

    [SerializeField] private Projectile _enemyBulletPrefab;
    private ObjectPool<Projectile> _bulletPool;
    // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _bulletPool = new ObjectPool<Projectile>(
            () => { return Instantiate(_enemyBulletPrefab); },
            bullet => { bullet.gameObject.SetActive(true); },
            bullet => { bullet.gameObject.SetActive(false); },
            bullet => { Destroy(bullet.gameObject); },
            false, 20, 100
            );
    }

    private void killBullet(Projectile projectile)
    {
        _bulletPool.Release(projectile);
    }

    private void OnEnable()
    {
        _fly.DOLocalMoveY(0.8f, 0.5f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }


    Vector2 currentVelocity;
    // Update is called once per frame
    void Update()
    {
        if (localChangeTime <= 0f)
        {
            localChangeTime = _positionChangeTime;
            _target = new Vector2(Random.Range(-18, 19), Random.Range(-9, 10));

        }
        else
        {
            localChangeTime -= Time.deltaTime;
        }

        if (Random.value > 0.85 && currentVelocity.magnitude < 0.3f)
        {
            var bullet = _bulletPool.Get();
            bullet.transform.position = transform.position;
            Vector2 direction = _player.position - transform.position;
            float angle = Vector2.SignedAngle(Vector2.left, direction);
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.Init(killBullet);

            //GameObject enemyBullet = ObjectPooler.SharedInstance.GetPooledObject("EnemyBullet");
            //if (enemyBullet != null)
            //{
            //    enemyBullet.transform.position = transform.position;
            //    Vector2 direction = _player.position - transform.position;
            //    float angle = Vector2.SignedAngle(Vector2.left, direction);
            //    enemyBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            //    enemyBullet.SetActive(true);
            //}
        }
        transform.position = Vector2.SmoothDamp(transform.position, _target, ref currentVelocity, 2f, _speed);
        //transform.up = Vector3.MoveTowards(transform.up, _target, _rotationSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, _speed * Time.deltaTime);
    }
}
