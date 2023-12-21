using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites;
    protected PlayerScript player;
    protected float speed;

    protected SpriteAnimation sa;
    public virtual void Init()
    {
        sa = GetComponent<SpriteAnimation>();
        sa.SetSprite(sprites, 0.1f);
        speed = GameParams.itemSpeed;
    }

    public void SetPlayer(PlayerScript player)
    {
        this.player = player;
    }

    public abstract void PickUp(PlayerScript player);

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
