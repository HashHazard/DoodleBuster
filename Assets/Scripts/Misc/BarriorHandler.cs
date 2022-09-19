using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriorHandler : MonoBehaviour
{
    private void resetInvincibility()
    {
        PlayerController.Instance.Invincible = false;
        PlayerController.Instance.BarriorActiveState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool e = collision.CompareTag("Enemy1") || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3") || collision.CompareTag("Enemy4") || collision.CompareTag("Enemy5");
        if ((e || collision.CompareTag("EnemyBullet")))
        {
            Invoke("resetInvincibility", 2f);
            gameObject.SetActive(false);
        }
    }
}
