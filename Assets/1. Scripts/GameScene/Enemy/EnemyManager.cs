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

    // 랜덤 스폰 
    // 위에서 언급한 Plane의 자식인 RespawnRange 오브젝트
    public GameObject rangeObject;
    BoxCollider2D rangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnDelay;
    }

    private int enemycount = 0;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            int rand = Random.Range(0, enemies.Length);
            EnemyScript newEnemy = Instantiate(enemies[rand], Return_RandomPosition(), Quaternion.identity);
            newEnemy.name = $"Enemy {enemycount}";
            newEnemy.SetPlayer(player)
                .SetEBullets(eBullets)
                .SetItems(items);
            enemycount++;
        }
    }
    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0f, 0f);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }
}
