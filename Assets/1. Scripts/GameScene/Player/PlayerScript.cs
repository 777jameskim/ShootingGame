using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float delay;

    [SerializeField] private PBulletScript bullet;
    [SerializeField] private float bullettimer;
    [SerializeField] private float bulletdelay;

    [SerializeField] private SupportScript supportL;
    [SerializeField] private SupportScript supportR;

    private PlayerAnimation pa;
    private int spritemode;
    private int HP = GameParams.playerHP;
    public bool alive = true;

    private int support = 0;
    private bool supportPriorityLeft = true;

    public int Support
    {
        get { return support; }
        set
        {
            if (value >= 0 && value <= 2)
            {
                support = value;
                SupportHandler(value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pa = GetComponent<PlayerAnimation>();
        pa.delay = delay;
        pa.MakeInvincible();
        transform.position = GameParams.startPosition;
        Support = 0;
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
        if (newmode != spritemode)
            AnimationHandler(newmode);

        Fire();

        if (Input.GetKeyDown(KeyCode.Tab))
            SupportPriorityToggle();
    }

    private void AnimationHandler(int newmode)
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

    private void SupportHandler (int n)
    {
        switch (n)
        {
            case 1:
                if (supportPriorityLeft)
                {
                    supportL.Active = true;
                    supportR.Active = false;
                }
                else
                {
                    supportL.Active = false;
                    supportR.Active = true;
                }
                break;
            case 2:
                supportL.Active = true;
                supportR.Active = true;
                break;
            default:
                supportL.Active = false;
                supportR.Active = false;
                break;
        }
    }

    private void SupportPriorityToggle()
    {
        supportPriorityLeft = !supportPriorityLeft;
        SupportHandler(support);
    }

    public void SupportDeath(SupportScript support)
    {
        if (support == supportL)
            supportPriorityLeft = false;
        if (support == supportR)
            supportPriorityLeft = true;
        Support--;
    }

    private void Fire()
    {
        bullettimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (bullettimer > bulletdelay)
            {
                bullettimer = 0;
                PBulletScript pb = Pooling.Instance.PBullet;
                pb.transform.position = transform.GetChild(0).position;
                if (supportL.Active)
                    supportL.Fire();
                if (supportR.Active)
                    supportR.Fire();
            }
        }
    }

    public bool GetInvincible()
    {
        return pa.invincibleTimer > 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EBulletScript>())
        {
            Pooling.Instance.EBullet = collision.GetComponent<EBulletScript>();
            Damage();
        }
        else if (collision.GetComponent<ItemScript>())
        {
            collision.GetComponent<ItemScript>().PickUp(this);
            Destroy(collision.gameObject);
        }
    }

    public void Damage()
    {
        if (GetInvincible())
            return;
        HP--;
        if (HP <= 0)
            Death();
    }

    public void Death()
    {
        alive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        pa.PlayDeathAnimation();
    }

    public void Revive()
    {
        UI.Instance.Lives--;
        if (UI.Instance.Lives <= 0)
        {
            GameOver();
            //return;
        }
        Support = 0;
        transform.position = GameParams.startPosition;
        pa.SetAnimation(PlayerDirection.Center);
        pa.MakeInvincible();
        GetComponent<CapsuleCollider2D>().enabled = true;
        HP = GameParams.playerHP;
        alive = true;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
