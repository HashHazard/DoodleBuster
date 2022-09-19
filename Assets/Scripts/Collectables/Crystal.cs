using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private GameObject _crystal;
    [SerializeField] private ParticleSystem _crystalParticle;
    [SerializeField] private GameObject _light;
    [SerializeField] private AudioClip _appearClip;
    [SerializeField] private AudioClip _collectedClip;


    private bool isCollected;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isCollected = false;
        _light.SetActive(true);
        _crystal.SetActive(true);
        if (Random.value > 0.5)
        {
            _rb.gravityScale = _rb.gravityScale * -1;
        }
        SoundManager.Instance.PlaySound(_appearClip);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (gameObject.CompareTag("Gem1")) ScoreManager.Instance.UpdateGemSore();
            isCollected = true;
            _crystal.SetActive(false);
            _crystalParticle.gameObject.SetActive(true);
            _crystalParticle.Play();
            _light.SetActive(false);
            SoundManager.Instance.PlaySound(_collectedClip);
            Invoke("DisableCollectable", 1.6f);
        }
    }

    private void DisableCollectable()
    {
        gameObject.SetActive(false);
    }
}
