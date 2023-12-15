using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : ItemScript
{
    public override void Init()
    {
        base.Init();
        sa.SetSprite(sprites, 0.1f);
        speed = 3f;
    }

    public override void PickUp()
    {
        Debug.Log("Power pickup");
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
