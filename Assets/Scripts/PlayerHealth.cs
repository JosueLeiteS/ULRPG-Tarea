using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
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
            heartImages[i].GetComponent<Image>().sprite = fullSprite;
            currentLife += 2;
        }
        maxLife = currentLife;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            DealDamage(2);
    }

    public void DealDamage(int damage)
    {
        AudioManager.instance.Play("Dano2");
        currentLife = Mathf.Clamp(currentLife - damage, 0, maxLife);
        int tempIndex = currentLife / 2;
        print(tempIndex);

        if (currentLife % 2 == 0)
            heartImages[tempIndex].GetComponent<Image>().sprite = emptySprite;
        else if (currentLife % 2 == 1)
            heartImages[tempIndex].GetComponent<Image>().sprite = halfSprite;
        
        if (currentLife > 0)
            return;
        else
        {
            maxLife = 0;
            currentLife = 0;
            Init();        
        }
    }
    public void UpdateLife() {
        int tempIndex = Mathf.Clamp(currentLife, 0, maxLife) / 2;
        print(tempIndex);

        if (currentLife % 2 == 0)
            heartImages[tempIndex].GetComponent<Image>().sprite = emptySprite;
        else if (currentLife % 2 == 1)
            heartImages[tempIndex].GetComponent<Image>().sprite = halfSprite;
    }
    public void Respawn() {
        AudioManager.instance.Play("respawn");
    }
}
