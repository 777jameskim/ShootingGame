using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite[] sprites;
    private UnityAction action;

    private int index;
    private float delay;
    private float delayTimer;
    private bool loop;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites == null || sprites.Length == 0)
            return;

        delayTimer += Time.deltaTime;

        if(delayTimer >= delay)
        {
            delayTimer = 0;

            sr.sprite = sprites[index];
            index++;

            if (index >= sprites.Length)
            {
                if(loop)
                    index = 0;
                else
                {
                    sprites = null;
                    sr.sprite = null;
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                }
            }
        }
    }

    private void SetData(Sprite[] sprites, float delay, bool loop = true)
    {
        index = 0;
        delayTimer = delay;
        this.delay = delay;
        this.sprites = sprites;
        this.loop = loop;
        this.action = null;
    }

    public void SetSprite(Sprite[] sprites, float delay, bool loop = true)
    {
        SetData(sprites, delay, loop);
    }

    public void SetSprite(Sprite[] sprites, float delay, UnityAction action, bool loop = true)
    {
        SetData(sprites, delay, loop);
        this.action = action;
    }
}
