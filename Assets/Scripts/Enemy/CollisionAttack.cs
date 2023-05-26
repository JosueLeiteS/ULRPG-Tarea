using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    public string Tag;
    public int da�o;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entre");
        if (collision.gameObject.CompareTag(Tag) && Tag.Equals("Player")) {
            collision.gameObject.GetComponent<PlayerHealth>().DealDamage(da�o);
        }
        if (collision.gameObject.CompareTag(Tag) && Tag.Equals("Enemy")) {
            collision.gameObject.GetComponent<EnemyHealth>().DealDamage(da�o);
            AudioManager.instance.Play("Damage");
        }
    }
}
