using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using CameraShake;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PerlinShake.Params shakeParams;

    [SerializeField] private AudioClip _playerDamageClip;

    [SerializeField] private ParticleSystem _damageEffect;
    [SerializeField] private GameObject _barrior;


    //[SerializeField] private GameObject _shockWave;

    // Material
    private Material _matWhite;
    private Material _matDefault;
    private Color _colorDefault;

    private Rigidbody2D rb;
    private static PlayerController instance;
    private Animator animator;
    private SpriteRenderer _renderer;

    private bool flipped = false;

    public bool Invincible = false;

    public bool BarriorActiveState = false;

    public bool isPlayerDead = false;

    private Vector2 _defScale;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.flipX = flipped;
        _defScale = transform.localScale;

        _matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        _matDefault = _renderer.material;
        _colorDefault = _renderer.color;
    }

    // Singleton instantiation
    public static PlayerController Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PlayerController>();
            return instance;
        }
    }


    private void ResetMat()
    {
        _renderer.material = _matDefault;
        _renderer.color = _colorDefault;
    }



    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x < 0f)
        {
            _renderer.flipX = true;
            flipped = true;
        }
        else
        {
            _renderer.flipX = false;
            flipped = false;
        }


        if (Mathf.Abs(rb.velocity.x) < 4.5f)
        {
            animator.SetFloat("speed", 0f);
        }

        // Falling Animator
        if (rb.velocity.y < -4.5f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

    }
    public void Knockback(float amount, Vector3 rotZ)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(rotZ * amount, ForceMode2D.Force);
        animator.SetFloat("speed", 1f);
        //rb.AddRelativeForce(rotZ * amount, ForceMode2D.Impulse);
    }

    private void resetInvincibility()
    {
        Invincible = false;
    }

    private void resetFade()
    {
        _renderer.DOFade(1f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool e = collision.CompareTag("Enemy1") || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3") || collision.CompareTag("Enemy4") || collision.CompareTag("Enemy5");
        if ((e || collision.CompareTag("EnemyBullet")) && !Invincible && !BarriorActiveState)
        {
            Invincible = true;
            HealthBar.Instance.takeDamage();
            //_shockWave.SetActive(true);
            SoundManager.Instance.PlaySound(_playerDamageClip);
            LightHandling.Instance.backgroundLightening();

            // Flash
            _renderer.material = _matWhite;
            _renderer.color = Color.white;
            Invoke("ResetMat", 0.7f);


            _damageEffect.gameObject.SetActive(true);
            _damageEffect.Play();

            //CameraShake.Instance.ShakeCamera(3f, 0.05f);
            CameraShaker.Shake(new PerlinShake(shakeParams));
            if (transform != null && _renderer != null)
            {
                transform.DOShakePosition(0.8f, 0.4f, 20, 90, false, true);
                //_renderer.DOFade(1.0f, 0.15f).SetLoops(5, LoopType.Yoyo).OnComplete(resetFade);
                Invoke("resetInvincibility", 2f);
                //transform.DOScale(transform.localScale.x + 0.5f, 0.8f).SetEase(Ease.InOutFlash).OnComplete(resetScale);


            }

        }

        if (collision.CompareTag("Gem2"))
        {
            if (!BarriorActiveState)
            {
                _barrior.SetActive(true);
                Invincible = true;
            }

            BarriorActiveState = true;
        }
    }

    public void PlayerDeath()
    {
        gameObject.SetActive(false);
    }

    private void resetScale()
    {
        transform.DOScale(_defScale.x, 0.5f).SetEase(Ease.InOutBack);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
