using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EnemyScript : MonoBehaviour
{
    protected SpriteAnimation sa;
    protected PlayerScript player;
    private ItemScript[] items;

    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private Sprite[] deadSprites;

    [SerializeField] private EBulletScript bullet;

    private Transform firePosT;
    protected float bulletspeed;

    protected float speed;
    protected int HP { get; set; }
    protected int score;

    protected float fireDelay;
    protected float fireTimer;

    public bool alive;
    protected bool kamikaze;

    public virtual void Initialize()
    {
        sa = GetComponent<SpriteAnimation>();
        firePosT = transform.GetChild(0);
        sa.SetSprite(normalSprites, 1);
        SetColliderEnabled(true);
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
            Move();
        if (player.alive)
            FirePosAutoRotate();
        if (GameParams.OutOfBounds(transform, 1))
            Pool();

        Fire();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    protected virtual void SetColliderEnabled(bool value)
    {
        GetComponent<Collider2D>().enabled = value;
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

    public void Fire()
    {
        fireTimer += Time.deltaTime;
        if (alive && fireTimer > fireDelay)
        {
            fireTimer = 0;
            EBulletScript b = Pooling.Instance.EBullet;
            b.transform.position = firePosT.position;
            b.transform.rotation = firePosT.rotation;
            b.speed = bulletspeed;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
        {
            if (!collision.GetComponent<PlayerScript>().GetInvincible())
            {
                collision.GetComponent<PlayerScript>().Death();
                if (kamikaze)
                    Death(true);
            }
        }
        else if (collision.GetComponent<PBulletScript>())
        {
            Pooling.Instance.PBullet = collision.GetComponent<PBulletScript>();
            Damage();
        }
    }

    void Damage()
    {
        sa.SetSprite(hitSprites, GameParams.hitBlink,
            () => {
                sa.SetSprite(normalSprites, 0.2f);
            }, false);
        HP--;
        if (HP <= 0)
            Death();
    }

    void Death(bool crash = false)
    {
        alive = false;
        SetColliderEnabled(false);
        UI.Instance.Score += score;
        sa.SetSprite(deadSprites, 0.1f,
            () => {
                if (!crash)
                    CreateItem();
                Pool();
            }, false);
    }

    protected abstract void Pool();

    void CreateItem()
    {
        int rand = Random.Range(0, 100);
        int index = (rand < 80) ? 0 : (rand < 95) ? 1 : 2;
        //int index = 1;

        Instantiate(items[index], transform.position, Quaternion.identity);
    }
}
