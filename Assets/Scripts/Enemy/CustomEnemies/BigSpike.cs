using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public class BigSpike : MonoBehaviour
{
    [SerializeField] private Transform _t1, _t2, _t3, _t4;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float pauseFiringTime = 4f;
    private float timeBtwShots;

    private Vector2 target;
    private float turnSpeed = 200f;
    private float localTurnSpeed;
    private float localPauseFiringTime;
    private bool turning = false;

    [SerializeField] private Projectile _enemyBulletPrefab;
    private ObjectPool<Projectile> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Projectile>(
             () => { return Instantiate(_enemyBulletPrefab); },
             bullet => { bullet.gameObject.SetActive(true); },
             bullet => { bullet.gameObject.SetActive(false); },
             bullet => { Destroy(bullet.gameObject); },
             false, 30, 100
             );
    }

    private void killBullet(Projectile projectile)
    {
        _bulletPool.Release(projectile);
    }

    private void OnEnable()
    {
        target = 8 * Random.insideUnitCircle;
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        localTurnSpeed = turnSpeed;
        timeBtwShots = fireRate;
        localPauseFiringTime = pauseFiringTime;

        StartCoroutine(MoveEnemy());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void instantiateEnemyBullet(Transform t)
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = t.position;
        bullet.transform.rotation = t.rotation;
        bullet.Init(killBullet);

        //GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("EnemyBullet");
        //bullet.transform.position = t.position;
        //bullet.transform.rotation = t.rotation;
        //bullet.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _enemy.transform.Rotate(0, 0, localTurnSpeed * Time.deltaTime);
        if ((Vector2)transform.position == target && !turning)
        {
            Fire();
        }
        if ((Vector2)transform.position == target)
        {

            if (localPauseFiringTime <= 0)
            {
                localPauseFiringTime = pauseFiringTime;
                StartCoroutine(RotateEnemy());

            }
            else
            {
                localPauseFiringTime -= Time.deltaTime;
            }
        }
    }

    private void Fire()
    {
        if (timeBtwShots <= 0f)
        {
            timeBtwShots = fireRate;
            localTurnSpeed = 0f;
            instantiateEnemyBullet(_t1);
            instantiateEnemyBullet(_t2);
            instantiateEnemyBullet(_t3);
            instantiateEnemyBullet(_t4);

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            //localTurnSpeed = turnSpeed;
        }

    }

    IEnumerator RotateEnemy()
    {
        localTurnSpeed = turnSpeed;
        turning = true;
        yield return new WaitForSecondsRealtime(2.5f);
        turning = false;
        localTurnSpeed = 0f;
    }


    IEnumerator MoveEnemy()
    {
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }


    }
}
