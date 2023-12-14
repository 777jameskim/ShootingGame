using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(sprites, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
