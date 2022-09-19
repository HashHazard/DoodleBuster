using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreenObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem1") || collision.CompareTag("Gem2"))
        {
            collision.gameObject.SetActive(false);
        }
        else
            Destroy(collision.gameObject);
    }
}
