using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private SpriteAnimation sa;
    protected float speed;
    protected int HP { get; set; }

    [SerializeField] private float boundaryY;

    protected virtual void Initialize()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y < 0 - boundaryY)
            Destroy(gameObject);
    }

    public void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
