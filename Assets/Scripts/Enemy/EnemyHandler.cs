using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private float _EnemyHealth = 10f;

    private int localHealth;

    public event EventHandler<OnEnemyDamagedEventArgs> OnEnemyKilled;
    public event EventHandler<OnEnemyDamagedEventArgs> OnEnemyDamaged;

    private void OnEnable()
    {
        localHealth = (int)_EnemyHealth;
    }

    //public class OnEnemyKilledEventArgs : EventArgs
    //{
    //    public int spaceCount;
    //}

    public class OnEnemyDamagedEventArgs: EventArgs
    {
        public int health;
    }

    void Start()
    {
    }

    //int spaceCount = 0;

    // Update is called once per frame
    void Update()
    {
        //if (Keyboard.current.spaceKey.isPressed)
        //{
        //    spaceCount++;
        //    //OnEnemyKilled?.Invoke(this, new OnEnemyKilledEventArgs { spaceCount = spaceCount });
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Bullet"))
                localHealth -= 5;
            // if the enemy is hit by the player itself
            else localHealth -= 10;

            if (localHealth <= 0)
            {
                OnEnemyKilled?.Invoke(this, new OnEnemyDamagedEventArgs { health = (int)_EnemyHealth - localHealth });
            }
            else
            {
                OnEnemyDamaged?.Invoke(this, new OnEnemyDamagedEventArgs { health = (int)_EnemyHealth - localHealth});
            }
        }
        
    }
}
