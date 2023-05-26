using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public List<GameObject> heartImages = new List<GameObject>();

    public Sprite fullSprite, halfSprite, emptySprite;

    public int currentLife;
    public int maxLife;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].GetComponent<SpriteRenderer>().sprite = fullSprite;
            currentLife += 2;
        }
        maxLife = currentLife;
    }
    public void DealDamage(int damage)
    {
        AudioManager.instance.Play("Damage");
        currentLife = Mathf.Clamp(currentLife - damage, 0, maxLife);
        int tempIndex = currentLife / 2;
        print(tempIndex);

        if (currentLife % 2 == 0)
            heartImages[tempIndex].GetComponent<SpriteRenderer>().sprite = emptySprite;
        else if (currentLife % 2 == 1)
            heartImages[tempIndex].GetComponent<SpriteRenderer>().sprite = halfSprite;

        if (currentLife > 0)
            return;
        else
        {
            maxLife = 0;
            currentLife = 0;
            Destroy(gameObject);
        }
    }


}
