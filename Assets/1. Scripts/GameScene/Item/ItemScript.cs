using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites;
    protected float speed;

    protected SpriteAnimation sa;
    public virtual void Init()
    {
        sa = GetComponent<SpriteAnimation>();
    }

    public abstract void PickUp();

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
