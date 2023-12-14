using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection
{
    Center,
    Left,
    Right
}


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] center;
    [SerializeField] private Sprite[] left;
    [SerializeField] private Sprite[] right;

    [SerializeField] private Sprite[] dead;

    private SpriteAnimation sa;
    private SpriteRenderer sr;

    public float invincibleTimer;
    public float delay;

    Dictionary<PlayerDirection, Sprite[]> Spritemap = new Dictionary<PlayerDirection, Sprite[]>();

    // Start is called before the first frame update
    void Start()
    {
        Spritemap.Add(PlayerDirection.Center, center);
        Spritemap.Add(PlayerDirection.Left, left);
        Spritemap.Add(PlayerDirection.Right, right);
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        sa.SetSprite(center, delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer % (GameParams.invincibleBlink * 2) > GameParams.invincibleBlink)
                sr.color = new Color(1f, 1f, 1f, 0.5f);
            else
                sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SetAnimation(PlayerDirection dir)
    {
        sa.SetSprite(Spritemap[dir], delay);
    }

    public void PlayDeathAnimation()
    {
        sa.SetSprite(dead, 0.1f, () => {
            GetComponent<PlayerScript>().Revive();
        }, false);
    }
    public void MakeInvincible()
    {
        this.invincibleTimer = GameParams.invincibleTime;
    }
}
