using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        speed = 3;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
