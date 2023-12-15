using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EnemyScript : MonoBehaviour
{
    protected SpriteAnimation sa;
    private PlayerScript player;
    private ItemScript[] items;

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

    private bool alive = true;

    protected virtual void Initialize()
    {
        sa = GetComponent<SpriteAnimation>();
        firePosT = transform.GetChild(0);
        sa.SetSprite(normalSprites, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
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
    public EnemyScript SetItems(ItemScript[] items)
    {
        this.items = items;
        return this;
    }

    public EnemyScript SetEBullets(Transform eBullets)
    {
        this.bulletparent = eBullets;
        return this;
    }

    public void Fire()
    {
        if (alive)
        {
            EBulletScript b = Instantiate(bullet, firePosT);
            b.transform.SetParent(bulletparent);
            b.speed = bulletspeed;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PBulletScript>())
        {
            sa.SetSprite(hitSprites, GameParams.hitBlink,
                () => {
                    sa.SetSprite(normalSprites, 0.2f);
                }, false);
            HP--;
            Destroy(collision.gameObject);
        }
        if (HP <= 0)
        {
            alive = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            UI.Instance.Score += GameParams.scoreA;
            sa.SetSprite(deadSprites, 0.1f,
                () => {
                    CreateItem();
                    Destroy(gameObject);
                }, false);
        }
    }

    void CreateItem()
    {
        int rand = Random.Range(0, 100);
        int index = (rand < 80) ? 0 : (rand < 95) ? 1 : 2;

        Instantiate(items[index], transform.position, Quaternion.identity);
    }
}
