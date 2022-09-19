using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightHandling : MonoBehaviour
{
    [SerializeField] UnityEngine.Rendering.Universal.Light2D _flashLight;
    [SerializeField] UnityEngine.Rendering.Universal.Light2D _globalLight;
    [SerializeField] UnityEngine.Rendering.Universal.Light2D _lightening;

    private float GBIntensity;
    private float localGBIntensity;
    WaitForSeconds wait;
    WaitForSeconds gbWait;
    WaitForSeconds backgroundWait;

    public static LightHandling instance;


    public static LightHandling Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<LightHandling>();
            return instance;
        }
    }

    private void Awake()
    {
        wait = new WaitForSeconds(0.02f);
        gbWait = new WaitForSeconds(0.12f);
        backgroundWait = new WaitForSeconds(0.3f);
        GBIntensity = _globalLight.intensity;
        localGBIntensity = GBIntensity * 2f;
    }

    public void backgroundLightening()
    {
        StartCoroutine(lightening());
    }

    public void FlickerGBLight()
    {
        _globalLight.intensity = localGBIntensity;
        StartCoroutine(GBReset());
    }

    public void FlickerOnGunFire()
    {
        _flashLight.intensity = 0.6f;
        StartCoroutine(flashReset());
    }

    public void FlickerGBonEnemyDamage()
    {
        _globalLight.intensity = localGBIntensity;
        StartCoroutine(damageGBReset());
    }

    IEnumerator damageGBReset()
    {
        yield return gbWait;
        _globalLight.intensity = GBIntensity;
    }

    IEnumerator flashReset()
    {
        yield return wait;
        _flashLight.intensity = 1f;
    }
    IEnumerator GBReset()
    {
        _flashLight.gameObject.SetActive(false);
        yield return gbWait;
        _globalLight.intensity = GBIntensity;
        _flashLight.gameObject.SetActive(true);
    }

    IEnumerator lightening()
    {
        _flashLight.gameObject.SetActive(false);

        _lightening.gameObject.SetActive(true);
        yield return backgroundWait;
        _lightening.gameObject.SetActive(false);
        yield return wait;
        _lightening.gameObject.SetActive(true);
        yield return backgroundWait;

        _flashLight.gameObject.SetActive(true);
        _lightening.gameObject.SetActive(false);

    }
}
