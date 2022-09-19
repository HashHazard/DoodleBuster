using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CameraShake;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Heart _heart;

    [SerializeField] private int _playerHealthSize = 3;


    private List<Heart> _healthList;


    public static HealthBar instance;

    public static HealthBar Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<HealthBar>();
            return instance;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _healthList = new List<Heart>();
        for (int i = 0; i < _playerHealthSize; i++)
        {

            _healthList.Add(Instantiate(_heart, transform));
            _healthList[i].transform.position = new Vector3(transform.position.x + (1.5f * i), transform.position.y, transform.position.z);
        }
    }


    public void takeDamage(int amount = 1)
    {
        if (_healthList.Count <= 0 || _playerHealthSize < 0) return;


        for (int i = 0; i < amount; i++)
        {
            if (_playerHealthSize - 1 - i < 0) return;
            _healthList[_playerHealthSize - 1 - i].Disappear();
            _playerHealthSize--;
        }
        if (_playerHealthSize <= 0)
        {
            Invoke("RestartScene", 0f);
        }
    }

    private void RestartScene()
    {
        PauseHandler.Instance.OnDeath();
    }
}
