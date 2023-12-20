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

    public override void Initialize()
    {
        base.Initialize();
        speed = 3;
        HP = 3;
        fireDelay = 1f;
        fireStartDelay = 1f;
        bulletspeed = 5;
        kamikaze = true;
        InvokeRepeating("Fire", fireStartDelay, fireDelay);
    }

    protected override void Pool()
    {
        base.Pool();
        Pooling.Instance.EnemyA = this;
    }

    protected override void SetColliderEnabled(bool value)
    {
        GetComponent<CapsuleCollider2D>().enabled = value;
    }
}
