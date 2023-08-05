using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public Sprite heartFullSprite;
    public Sprite heartEmptySprite;
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderers[i].sprite = heartEmptySprite;
        }

        for (int i = 0; i < health; i++)
        {
            spriteRenderers[i].sprite = heartFullSprite;
        }
    }
}
