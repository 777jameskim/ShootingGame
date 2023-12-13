using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private List<Sprite> sprites;
    private UnityAction action;

    private int index;
    private float delay;
    private float delayTimer;
    private bool loop;

    private float invincibleTimer;

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
                if(loop)
                    index = 0;
                else
                {
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                    else
                        sprites = null;
                }
        }

        if(invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer % (GameParams.invincibleBlink * 2) > GameParams.invincibleBlink)
                sr.color = new Color(1f, 1f, 1f, 0.5f);
            else
                sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void SetData(List<Sprite> sprites, float delay, bool loop = true)
    {
        index = 0;
        delayTimer = delay;
        this.delay = delay;
        this.sprites = sprites.ToList();
        this.loop = loop;
        this.action = null;
    }

    public void SetSprite(List<Sprite> sprites, float delay, bool loop = true)
    {
        SetData(sprites, delay, loop);
    }

    public void SetSprite(List<Sprite> sprites, float delay, UnityAction action, bool loop = true)
    {
        SetData(sprites, delay, loop);
        this.action = action;
    }

    public void Invincibility(float time)
    {
        this.invincibleTimer = time;
    }

    public bool GetInvincible()
    {
        return invincibleTimer > 0;
    }
}
