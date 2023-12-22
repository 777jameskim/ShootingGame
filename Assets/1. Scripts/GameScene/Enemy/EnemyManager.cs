using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private ItemScript[] items;
    [SerializeField] private EnemyScript[] enemies;
    [SerializeField] private Transform eBullets;
    [SerializeField] private float spawnDelay;
    private float spawnTimer;

    public GameObject rangeObject;
    BoxCollider2D rangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            int rand = Random.Range(0, enemies.Length);
            EnemyScript newEnemy = null;
            if (enemies[rand].GetComponent<EnemyA>())
                newEnemy = Pooling.Instance.EnemyA;
            if (enemies[rand].GetComponent<EnemyB>())
                newEnemy = Pooling.Instance.EnemyB;
            newEnemy.transform.position = Return_RandomPosition();
            newEnemy.SetPlayer(player)
                .SetItems(items);
        }
    }
    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        float range_X = rangeCollider.bounds.size.x;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0f, 0f);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }
}
