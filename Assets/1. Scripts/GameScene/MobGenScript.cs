using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenScript : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private EnemyScriptA enemyA;

    // Start is called before the first frame update
    void Start()
    {
        EnemyScriptA enemy = Instantiate(enemyA);
        enemy.SetPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
