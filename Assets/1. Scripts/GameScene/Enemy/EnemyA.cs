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
        HP = 3;
        fireDelay = 1;
        fireStartDelay = 1;
        bulletspeed = 5;
        InvokeRepeating("Fire", fireStartDelay, fireDelay);
    }
}
