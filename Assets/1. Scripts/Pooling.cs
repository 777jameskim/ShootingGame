using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    [SerializeField] private PlayerScript player;

    [SerializeField] private PBulletScript pBullet;
    [SerializeField] private Transform pParent;
    private List<PBulletScript> pBulletList = new List<PBulletScript>();

    [SerializeField] private EBulletScript eBullet;
    [SerializeField] private Transform eParent;
    private List<EBulletScript> eBulletList = new List<EBulletScript>();

    [SerializeField] private EnemyA enemyA;
    [SerializeField] private Transform parentA;
    private List<EnemyA> enemyAList = new List<EnemyA>();

    [SerializeField] private EnemyB enemyB;
    [SerializeField] private Transform parentB;
    private List<EnemyB> enemyBList = new List<EnemyB>();

    public void Awake()
    {
        Instance = this;
    }

    public PBulletScript PBullet
    {
        get
        {
            if (pBulletList.Count == 0)
            {
                PBulletScript newbullet = Instantiate(pBullet);
                newbullet.gameObject.SetActive(false);
                newbullet.transform.SetParent(pParent);
                pBulletList.Add(newbullet);
            }

            PBulletScript bullet = null;
            for (int i = pBulletList.Count - 1; i >= 0; i--)
            {
                if (!pBulletList[i].gameObject.activeSelf)
                {
                    bullet = pBulletList[i];
                    pBulletList.RemoveAt(i);
                    break;
                }
            }
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        set
        {
            value.gameObject.SetActive(false);
            pBulletList.Add(value);
        }
    }

    public EBulletScript EBullet
    {
        get
        {
            if (eBulletList.Count == 0)
            {
                EBulletScript newbullet = Instantiate(eBullet);
                newbullet.gameObject.SetActive(false);
                newbullet.transform.SetParent(eParent);
                eBulletList.Add(newbullet);
            }

            EBulletScript bullet = null;
            for (int i = eBulletList.Count - 1; i >= 0; i--)
            {
                if (!eBulletList[i].gameObject.activeSelf)
                {
                    bullet = eBulletList[i];
                    eBulletList.RemoveAt(i);
                    break;
                }
            }
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        set
        {
            value.gameObject.SetActive(false);
            eBulletList.Add(value);
        }
    }

    public EnemyA EnemyA
    {
        get
        {
            if (enemyAList.Count == 0)
            {
                EnemyA newenemy = Instantiate(enemyA);
                newenemy.gameObject.SetActive(false);
                newenemy.transform.SetParent(parentA);
                enemyAList.Add(newenemy);
            }

            EnemyA enemy = null;
            for (int i = enemyAList.Count - 1; i >= 0; i--)
            {
                if (!enemyAList[i].gameObject.activeSelf)
                {
                    enemy = enemyAList[i];
                    enemyAList.RemoveAt(i);
                    break;
                }
            }
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            return enemy;
        }
        set
        {
            value.gameObject.SetActive(false);
            enemyAList.Add(value);
        }
    }

    public EnemyB EnemyB
    {
        get
        {
            if (enemyBList.Count == 0)
            {
                EnemyB newenemy = Instantiate(enemyB);
                newenemy.gameObject.SetActive(false);
                newenemy.transform.SetParent(parentB);
                enemyBList.Add(newenemy);
            }

            EnemyB enemy = null;
            for (int i = enemyBList.Count - 1; i >= 0; i--)
            {
                if (!enemyBList[i].gameObject.activeSelf)
                {
                    enemy = enemyBList[i];
                    enemyBList.RemoveAt(i);
                    break;
                }
            }
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            return enemy;
        }
        set
        {
            value.gameObject.SetActive(false);
            enemyBList.Add(value);
        }
    }
}
