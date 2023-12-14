using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float delay;

    [SerializeField] private PBulletScript bullet;
    [SerializeField] private Transform bulletparent;
    [SerializeField] private float bullettimer;
    [SerializeField] private float bulletdelay;

    [SerializeField] private Transform lifebar;
    [SerializeField] private GameObject lifeicon;

    private PlayerAnimation pa;
    private int spritemode;
    private int HP = GameParams.playerHP;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        pa = GetComponent<PlayerAnimation>();
        pa.delay = delay;
        pa.MakeInvincible();
        transform.position = GameParams.startPosition;
        for(int i = 1; i < GameParams.lives; i++)
        {
            Instantiate(lifeicon, lifebar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
            return;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y + Time.deltaTime * speed, 0 - GameParams.playerY, GameParams.playerY),
                0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y - Time.deltaTime * speed, 0 - GameParams.playerY, GameParams.playerY),
                0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + Time.deltaTime * speed, 0 - GameParams.playerX, GameParams.playerX),
                transform.position.y,
                0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x - Time.deltaTime * speed, 0 - GameParams.playerX, GameParams.playerX),
                transform.position.y,
                0);
        }
        int newmode = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            newmode++;
        if (Input.GetKey(KeyCode.LeftArrow))
            newmode--;
        AnimationHandler(newmode);

        Shooting();
    }

    private void AnimationHandler(int newmode)
    {
        if(newmode != spritemode)
        {
            switch (newmode)
            {
                case -1:
                    pa.SetAnimation(PlayerDirection.Left);
                    break;
                case 1:
                    pa.SetAnimation(PlayerDirection.Right);
                    break;
                default:
                    pa.SetAnimation(PlayerDirection.Center);
                    break;
            }
            spritemode = newmode;
        }
    }

    private void Shooting()
    {
        bullettimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (bullettimer > bulletdelay)
            {
                bullettimer = 0;
                PBulletScript pb = Instantiate(bullet, transform.GetChild(0).position, Quaternion.identity);
                pb.transform.SetParent(bulletparent);
            }
        }
    }

    private bool GetInvincible()
    {
        return pa.invincibleTimer > 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EBulletScript>())
        {
            Destroy(collision.gameObject);
            if (GetInvincible())
                return;
            HP--;
        }
        if(HP <= 0)
        {
            alive = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            pa.PlayDeathAnimation();
        }
    }

    public void Revive()
    {
        if (lifebar.childCount <= 0)
            return;
        Destroy(lifebar.GetChild(0).gameObject);
        transform.position = GameParams.startPosition;
        pa.SetAnimation(PlayerDirection.Center);
        pa.MakeInvincible();
        GetComponent<CapsuleCollider2D>().enabled = true;
        HP = GameParams.playerHP;
        alive = true;
    }
}
