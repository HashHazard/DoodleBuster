using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using CameraShake;
using UnityEngine.Pool;

// bullet hit effect
// gun sound with different pitch
// screen shake when shooting gun
// more shake when enemy dying
// move base in sound effect
// shoot 3 bullets on powerups
// big explosion
// particle light emission permanence where enemy dies
public class Weapon : MonoBehaviour
{
    public float offset;

    //public GameObject projectile;
    public Transform muzzle;
    public float knockbackAmount;
    public GameObject muzzleFlash;

    [SerializeField] private float turnSpeed = 360;
    [SerializeField] private AudioClip _bulletClip;



    private float timeBtwShots;
    public float fireRate;

    private Vector2 gunSize;

    private float rotZ = 0f;
    private bool isFireButtonPressed = false;

    //Deadzone for onscreen joystick
    private float deadZone = 0.25f;

    private Vector3 RandomizeIntensity = new Vector3(0.4f, 0, 0);

    WaitForSeconds wait;


    [SerializeField] private Projectile _bulletPrefab;
    private ObjectPool<Projectile> _bulletPool;


    // Start is called before the first frame update
    void Start()
    {

        gunSize = transform.localScale;
        wait = new WaitForSeconds(0.06f);

        _bulletPool = new ObjectPool<Projectile>(
            () => { return Instantiate(_bulletPrefab); },
            bullet => { bullet.gameObject.SetActive(true);  },
            bullet => { bullet.gameObject.SetActive(false); },
            bullet => { Destroy(bullet.gameObject); },
            false, 15, 30
            );
    }

    public void OnRotate(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();
        //rotZ = Mathf.Atan2(inputVec.y, inputVec.x) * Mathf.Rad2Deg;
        //Vector2 direction = inputVec - (Vector2)transform.position;
        //float targetAngle = Vector2.SignedAngle(Vector2.left, direction);
        float targetAngle = Vector2.SignedAngle(Vector2.left, inputVec);
        //rotZ = Mathf.SmoothDamp(rotZ, targetAngle,ref currentVelocity, 0.2f, turnSpeed);
        rotZ = targetAngle;
        if (inputVec.magnitude > deadZone)
        {
            isFireButtonPressed = true;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotZ), turnSpeed * Time.deltaTime);
        }
        else
        {
            isFireButtonPressed = false;
        }

        //transform.eulerAngles = new Vector3(0, 0, rotZ);

    }

    //public void OnFire(InputValue button)
    //{
    //    //Debug.Log(button.Get<float>().ToString());
    //    if (button.Get<float>() > 0f) isFireButtonPressed = true;
    //    else isFireButtonPressed = false;


    //}

    private void killBullet(Projectile projectile)
    {
        _bulletPool.Release(projectile);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate gun in the direction of mouse
        //Vector3 difference = -mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + transform.position;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ * offset);

        //transform.localRotation = Quaternion.Euler(0f, 0f, rotZ * offset);

        // Instantiate bullets and set fireRate
        if (timeBtwShots <= 0)
        {
            if (isFireButtonPressed)
            {
                CameraShaker.Presets.ShortShake2D(0.04f, 0.1f, 15, 2);
                //CameraShake.Instance.OnBulletHit();
                muzzleFlash.SetActive(true);
                transform.localScale = Vector2.one * 1.2f;
                StartCoroutine(flash());

                //CameraShake.Instance.ShakeCamera(cameraShakeStrength, cameraShakeDuration);

                LightHandling.Instance.FlickerOnGunFire();

                SoundManager.Instance.PlaySound(_bulletClip);

                // new unity object pool system
                var bullet = _bulletPool.Get();
                bullet.transform.position = muzzle.position + new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                                                Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), 0f);
                //bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                bullet.transform.rotation = transform.rotation;
                bullet.Init(killBullet);
                //bullet.SetActive(true);

                /////////////////////////////////////////////////////////////////////////////////////
                //GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
                //if (bullet == null) Debug.LogError("NULL Bullets");
                //if (bullet != null)
                //{
                //    bullet.transform.position = muzzle.position + new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                //                                    Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), 0f);
                //    //bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                //    bullet.transform.rotation = transform.rotation;
                //    bullet.SetActive(true);
                //}
                /////////////////////////////////////////////////////////////////////////////////////

                //Instantiate(projectile, muzzle.position, Quaternion.Euler(0f, 0f, rotZ - 360f));
                PlayerController.Instance.Knockback(knockbackAmount, transform.rotation * Vector3.right);
                timeBtwShots = fireRate;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


    }

    IEnumerator flash()
    {
        yield return wait;
        muzzleFlash.SetActive(false);
        transform.localScale = gunSize;
    }
}
