using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph.currentLife + 1 <= ph.maxLife + 0.5) {
                ph.currentLife += 1;
                ph.UpdateLife();
                AudioManager.instance.Play("Vida");
                Destroy(gameObject);
            }            
            
        }
    }

}
