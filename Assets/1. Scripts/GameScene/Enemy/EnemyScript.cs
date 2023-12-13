using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EnemyScript : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected SpriteAnimation sa;
    private PlayerScript player;

    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private Sprite[] deadSprites;

    [SerializeField] private EBulletScript bullet;

    private Transform firePosT;
    private Transform bulletparent;
    protected float bulletspeed;

    protected float speed;
    protected int HP { get; set; }

    protected float fireDelay;
    protected float fireStartDelay;
    private float fireTimer;

    protected virtual void Initialize()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();
        firePosT = transform.GetChild(0);
        sa.SetSprite(normalSprites.ToList(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (player != null)
        {
            FirePosAutoRotate();
        }
        if (transform.position.y < 0 - GameParams.boundaryY)
            Destroy(gameObject);
    }

    void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    void FirePosAutoRotate()
    {
        Vector2 vec = transform.position - player.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePosT.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public EnemyScript SetPlayer(PlayerScript player)
    {
        this.player = player;
        return this;
    }

    public EnemyScript SetEBullets(Transform eBullets)
    {
        this.bulletparent = eBullets;
        return this;
    }

    public void Fire()
    {
        EBulletScript b = Instantiate(bullet, firePosT);
        b.transform.SetParent(bulletparent);
        b.speed = bulletspeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PBulletScript>())
        {
            sa.SetSprite(hitSprites.ToList(), GameParams.hitBlink,
                () => {
                    sa.SetSprite(normalSprites.ToList(), 0.2f);
                }, false);
            HP--;
            Destroy(collision.gameObject);
        }
        if (HP == 0)
        {
            sa.SetSprite(deadSprites.ToList(), 0.1f,
                () => {
                    Destroy(gameObject);
                }, false);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
