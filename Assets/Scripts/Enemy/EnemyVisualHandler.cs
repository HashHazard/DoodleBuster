using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CameraShake;
using System;

public class EnemyVisualHandler : MonoBehaviour
{
    [SerializeField] AudioClip _damageClip;
    [SerializeField] AudioClip _explosionClip;

    [SerializeField] private GameObject _enemy;

    //[SerializeField] private GameObject _gem1;
    //[SerializeField] private GameObject _gem2;

    //BulletHit Impact
    [SerializeField] private float duration = 0.8f;
    [SerializeField] private float strength = 0.4f;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private float randomness = 90f;

    //[SerializeField] private ParticleSystem explosion;


    // Material
    private Material _matWhite;
    private Material _matDefault;
    private Color _colorDefault;
    //private UnityEngine.Object explosionRef;
    private Vector2 _scaleDefault;


    private SpriteRenderer _renderer;

    private Action<EnemyVisualHandler> _killAction;

    public void Init(Action<EnemyVisualHandler> killAction)
    {
        _killAction = killAction;
    }


    private void OnEnable()
    {
        EnemyHandler handler = GetComponent<EnemyHandler>();

        handler.OnEnemyKilled -= Handler_OnEnemyKilled;
        handler.OnEnemyDamaged -= Handler_OnEnemyDamaged;
        handler.OnEnemyKilled += Handler_OnEnemyKilled;
        handler.OnEnemyDamaged += Handler_OnEnemyDamaged;

    }

    private void Awake()
    {
        _renderer = _enemy.GetComponent<SpriteRenderer>();

        _matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        _matDefault = _renderer.material;
        _colorDefault = _renderer.color;
        //explosionRef = Resources.Load("Explosion");
        _scaleDefault = _enemy.transform.localScale;
    }

    private void resetScale()
    {
        _enemy.transform.DOScale(_scaleDefault.x, duration * 0.5f).SetEase(Ease.InOutBack);
    }

    private void ResetMat()
    {
        _renderer.material = _matDefault;
        _renderer.color = _colorDefault;
    }

    private void DamagePopupSetup(int amount)
    {
        int score = (int)(amount * 0.1);
        ScoreManager.Instance.IncreaseScore(score);
        DamagePopup popup = ObjectPooler.SharedInstance.GetPooledObject("DamageText").GetComponent<DamagePopup>();
        if (popup != null)
        {
            popup.transform.position = transform.position;
            popup.transform.localRotation = Quaternion.identity;
            popup.gameObject.SetActive(true);
            popup.Setup(amount);
        }
    }

    private void Handler_OnEnemyDamaged(object sender, EnemyHandler.OnEnemyDamagedEventArgs e)
    {
        _renderer.material = _matWhite;
        _renderer.color = Color.white;
        _enemy.transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
        _enemy.transform.DOScale(_enemy.transform.localScale.x + 1f, duration * 0.4f).SetEase(Ease.InOutFlash).OnComplete(resetScale);
        Invoke("ResetMat", 0.06f);
        LightHandling.Instance.FlickerGBonEnemyDamage();
        DamagePopupSetup(e.health * 10);
        CameraShaker.Presets.ShortShake2D();

        SoundManager.Instance.PlaySound(_damageClip);
        //CameraShake.Instance.ShakeCamera(2f, 0.08f);
    }

    private void Handler_OnEnemyKilled(object sender, EnemyHandler.OnEnemyDamagedEventArgs e)
    {
        SoundManager.Instance.PlaySound(_explosionClip);
        CameraShaker.Presets.Explosion2D();
        //CameraShake.Instance.ShakeCamera(4f, 0.15f);
        DamagePopupSetup(e.health * 10);
        LightHandling.Instance.FlickerGBLight();

        EnemyHandler handler = GetComponent<EnemyHandler>();
        handler.OnEnemyDamaged -= Handler_OnEnemyDamaged;
        handler.OnEnemyKilled -= Handler_OnEnemyKilled;

        _enemy.transform.localScale = _enemy.transform.localScale * 1.5f;

        //GameObject explosion = (GameObject)Instantiate(explosionRef);
        //explosion.transform.position = transform.position;
        //if (explosion != null)
        //{

        //    explosion.gameObject.SetActive(true);
        //    explosion.Play();
        //}

        ObjectPooler.SharedInstance.GetExplosion(transform.position);

        _enemy.transform.DOKill();
        _enemy.transform.localScale = _scaleDefault;

        if (_damageClip != null)
        {
            GameObject gem;
            int randomValue = UnityEngine.Random.Range(1, 101);
            if (randomValue <= 100 && randomValue > 90)
            {

                gem = ObjectPooler.SharedInstance.GetPooledObject("Gem2");
                if (gem != null)
                {
                    gem.transform.position = transform.position;
                    gem.transform.rotation = Quaternion.identity;
                    gem.SetActive(true);

                }
            }

            else if (randomValue <= 50)
            {
                gem = ObjectPooler.SharedInstance.GetPooledObject("Gem1");
                if (gem != null)
                {
                    gem.transform.position = transform.position;
                    gem.transform.rotation = Quaternion.identity;
                    gem.SetActive(true);

                }
            }

        }

        if (_damageClip == null)
        {
            gameObject.SetActive(false);
            return;
        }

        _killAction(this);
        //gameObject.SetActive(false);
    }

}
