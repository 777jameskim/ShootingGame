using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    [SerializeField] private PlayerScript player;

    [SerializeField] private PBulletScript pBullet;
    [SerializeField] private Transform pParent;
    private Queue<PBulletScript> pBulletQueue = new Queue<PBulletScript>();

    [SerializeField] private EBulletScript eBullet;
    [SerializeField] private Transform eParent;
    private Queue<EBulletScript> eBulletQueue = new Queue<EBulletScript>();

    [SerializeField] private EnemyA enemyA;
    [SerializeField] private Transform parentA;
    private Queue<EnemyA> enemyAQueue = new Queue<EnemyA>();

    [SerializeField] private EnemyB enemyB;
    [SerializeField] private Transform parentB;
    private Queue<EnemyB> enemyBQueue = new Queue<EnemyB>();

    public void Awake()
    {
        Instance = this;
    }

    public PBulletScript PBullet
    {
        get
        {
            if (pBulletQueue.Count == 0)
            {
                PBulletScript newbullet = Instantiate(pBullet);
                newbullet.gameObject.SetActive(false);
                newbullet.transform.SetParent(pParent);
                pBulletQueue.Enqueue(newbullet);
            }

            PBulletScript bullet = pBulletQueue.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        set
        {
            value.gameObject.SetActive(false);
            pBulletQueue.Enqueue(value);
        }
    }

    //int ebcount = 1;

    public EBulletScript EBullet
    {
        get
        {
            if (eBulletQueue.Count == 0)
            {
                EBulletScript newbullet = Instantiate(eBullet);
                newbullet.gameObject.SetActive(false);
                newbullet.transform.SetParent(eParent);
                //newbullet.name = $"EB{ebcount}";
                //ebcount++;
                eBulletQueue.Enqueue(newbullet);
            }

            EBulletScript bullet = eBulletQueue.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        set
        {
            value.gameObject.SetActive(false);
            eBulletQueue.Enqueue(value);
        }
    }

    //int eacount = 1;

    public EnemyA EnemyA
    {
        get
        {
            if (enemyAQueue.Count == 0)
            {
                EnemyA newenemy = Instantiate(enemyA);
                newenemy.gameObject.SetActive(false);
                newenemy.transform.SetParent(parentA);
                //newenemy.name = $"Enemy A{eacount}";
                //eacount++;
                enemyAQueue.Enqueue(newenemy);
            }

            EnemyA enemy = enemyAQueue.Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            return enemy;
        }
        set
        {
            value.gameObject.SetActive(false);
            enemyAQueue.Enqueue(value);
        }
    }

    public EnemyB EnemyB
    {
        get
        {
            if (enemyBQueue.Count == 0)
            {
                EnemyB newenemy = Instantiate(enemyB);
                newenemy.gameObject.SetActive(false);
                newenemy.transform.SetParent(parentB);
                enemyBQueue.Enqueue(newenemy);
            }

            EnemyB enemy = enemyBQueue.Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            return enemy;
        }
        set
        {
            value.gameObject.SetActive(false);
            enemyBQueue.Enqueue(value);
        }
    }
}
