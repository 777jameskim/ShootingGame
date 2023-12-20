using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        speed = 1;
        HP = 10;
        fireDelay = 1;
        fireStartDelay = 1;
        bulletspeed = 5;
        kamikaze = true;
        InvokeRepeating("Fire", fireStartDelay, fireDelay);
    }

    protected override void Move()
    {
        base.Move();
        float travelX = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(travelX) > Time.deltaTime * speed)
        {
            if (travelX > 0)
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            else
                transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else
            transform.Translate(Vector2.right * travelX);
    }

    protected override void Pool()
    {
        base.Pool();
        Pooling.Instance.EnemyB = this;
    }

    protected override void SetColliderEnabled(bool value)
    {
        GetComponent<PolygonCollider2D>().enabled = value;
    }
}
