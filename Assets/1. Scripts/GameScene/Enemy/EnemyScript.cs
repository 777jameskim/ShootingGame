using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    [SerializeField] private EBulletScript bullet;
    
    protected SpriteRenderer sr;
    private SpriteAnimation sa;
    private PlayerScript player;

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
}
