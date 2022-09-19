using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawners;
    [SerializeField] private float _rateOfSpawn = 5f;

    [SerializeField] private EnemyVisualHandler e1Prefab, e2Prefab, e3Prefab, e4Prefab, e5Prefab;
    private ObjectPool<EnemyVisualHandler> e1Pool, e2Pool, e3Pool, e4Pool, e5Pool;

    [SerializeField] private GameObject _runtimeObjectHolder;

    //[SerializeField] private List<GameObject> _enemies;

    private float localRate = 0f;

    private void Awake()
    {
        e1Pool = new ObjectPool<EnemyVisualHandler>(
            () => { return Instantiate(e1Prefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false, 5, 30
            );

        e2Pool = new ObjectPool<EnemyVisualHandler>(
            () => { return Instantiate(e2Prefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false, 5, 30
            );
        e3Pool = new ObjectPool<EnemyVisualHandler>(
            () => { return Instantiate(e3Prefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false, 5, 30
            );
        e4Pool = new ObjectPool<EnemyVisualHandler>(
            () => { return Instantiate(e4Prefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false, 5, 30
            );
        e5Pool = new ObjectPool<EnemyVisualHandler>(
            () => { return Instantiate(e5Prefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false, 5, 30
            );
    }

    private void killEnemy1(EnemyVisualHandler enemy)
    {
        e1Pool.Release(enemy);
    }
    private void killEnemy2(EnemyVisualHandler enemy)
    {
        e2Pool.Release(enemy);
    }
    private void killEnemy3(EnemyVisualHandler enemy)
    {
        e3Pool.Release(enemy);
    }
    private void killEnemy4(EnemyVisualHandler enemy)
    {
        e4Pool.Release(enemy);
    }
    private void killEnemy5(EnemyVisualHandler enemy)
    {
        e5Pool.Release(enemy);
    }


    private void Update()
    {
        if (!_runtimeObjectHolder.gameObject.activeInHierarchy) return;
        if (localRate <= 0f)
        {
            localRate = _rateOfSpawn;
            SpawnEnemy();
            _rateOfSpawn -= 0.02f;
        }
        else
        {
            localRate -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _spawners.Count);
        Vector2 spawnPosition = _spawners[randomIndex].position;
        int randomEnemyRange = Random.Range(1, 101);
        //string tag = "Enemy";
        EnemyVisualHandler enemy = null;
        if (randomEnemyRange <= 5)
        {
            enemy = e5Pool.Get();
            enemy.transform.position = spawnPosition;
            enemy.Init(killEnemy5);
        }
        else if (randomEnemyRange <= 15)
        {
            enemy = e4Pool.Get();
            enemy.transform.position = spawnPosition;
            enemy.Init(killEnemy4);
        }
        else if (randomEnemyRange <= 35)
        {
            enemy = e3Pool.Get();
            enemy.transform.position = spawnPosition;
            enemy.Init(killEnemy3);
        }
        else if (randomEnemyRange <= 65)
        {
            enemy = e2Pool.Get();
            enemy.transform.position = spawnPosition;
            enemy.Init(killEnemy2);
        }
        else if (randomEnemyRange <= 100)
        {
            enemy = e1Pool.Get();
            enemy.transform.position = spawnPosition;
            Vector2 direction = PlayerController.Instance.transform.position - enemy.transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            enemy.transform.rotation = Quaternion.Euler(0, 0, angle);
            enemy.Init(killEnemy1);
        }

        if (enemy != null)
        {
            enemy.transform.SetParent(_runtimeObjectHolder.transform);
        }
        //GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(tag);
        //if (enemy != null)
        //{

        //    enemy.transform.position = spawnPosition;
        //    enemy.SetActive(true);
        //}

        //Instantiate(_enemyPrefab, _spawners[randomIndex].position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }

    //IEnumerator MyCoroutine()
    //{
    //    int i = 0;

    //    while (i < 10)
    //    {
    //        // Count to Ten
    //        Debug.Log(i);
    //        i++;
    //        yield return null;
    //    }

    //    while (i > 0)
    //    {
    //        // Count back to Zero
    //        Debug.Log(i);
    //        i--;
    //        yield return null;
    //    }

    //    // All done!
    //}



    //////////////////////////////////////////////////////////////////////
    //[SerializeField] private Shape _shapePrefab;
    //[SerializeField] private int _spawnCount = 20;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);
    //}

    //private void Spawn()
    //{
    //    for (var i = 0; i < _spawnCount; i++)
    //    {
    //        var shape = Instantiate(_shapePrefab);
    //        shape.transform.position = transform.position + Random.insideUnitSphere * 10;
    //        shape.Init(KillShape);
    //    }
    //}

    //private void KillShape(Shape shape)
    //{
    //    Destroy(shape.gameObject);
    //}

}
