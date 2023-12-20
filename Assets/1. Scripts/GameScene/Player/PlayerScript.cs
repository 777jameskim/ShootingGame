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
                PBulletScript pb = Pooling.Instance.PBullet;
                pb.transform.position = transform.GetChild(0).position;
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
            //Destroy(collision.gameObject);
            Damage();
        }
        else if (collision.GetComponent<ItemScript>())
        {
            collision.GetComponent<ItemScript>().PickUp();
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
