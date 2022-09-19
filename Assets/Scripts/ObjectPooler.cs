using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _runtimeObjectHolder;

    [SerializeField] private ParticleSystem _explosion;
    public ObjectPool<ParticleSystem> _explosionPool;

    public static ObjectPooler SharedInstance;

    public List<GameObject> pooledObjects;

    public List<ObjectPoolItem> itemsToPool;

    private void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.transform.SetParent(_runtimeObjectHolder.transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }

        }

        _explosionPool = new ObjectPool<ParticleSystem>(
            () => Instantiate(_explosion),
            exp => exp.gameObject.SetActive(true),
            exp => exp.gameObject.SetActive(false),
            exp => Destroy(exp.gameObject),
            false, 10, 20
            );
    }

    IEnumerator releaseExp(ParticleSystem exp)
    {
        yield return new WaitForSeconds(2.0f);
        _explosionPool.Release(exp);
    }

    public void GetExplosion(Vector2 position)
    {
        var exp = _explosionPool.Get();
        exp.gameObject.transform.position = position;
        StartCoroutine(releaseExp(exp));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.CompareTag(tag))
            {

                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.transform.SetParent(_runtimeObjectHolder.transform);

                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }

        }

        return null;

    }
}
