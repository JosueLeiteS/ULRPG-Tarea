using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    public string Tag;
    public int daño;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entre");
        if (collision.gameObject.CompareTag(Tag) && Tag.Equals("Player")) {
            collision.gameObject.GetComponent<PlayerHealth>().DealDamage(daño);
        }
        if (collision.gameObject.CompareTag(Tag) && Tag.Equals("Enemy")) {
            collision.gameObject.GetComponent<EnemyHealth>().DealDamage(daño);
            AudioManager.instance.Play("Damage");
        }
    }
}
