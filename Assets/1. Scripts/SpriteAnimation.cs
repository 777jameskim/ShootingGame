using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> sprites;

    private int index;
    private float delay;
    private float delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites == null || sprites.Count == 0)
            return;

        delayTimer += Time.deltaTime;

        if(delayTimer >= delay)
        {
            delayTimer = 0;

            sr.sprite = sprites[index];
            index++;

            if (index >= sprites.Count)
                index = 0;
        }
    }

    public void SetSprite(List<Sprite> sprites, float delay)
    {
        index = 0;
        delayTimer = delay;
        this.delay = delay;
        this.sprites = sprites.ToList();
    }

    public void OnHIt()
    {

    }
}
