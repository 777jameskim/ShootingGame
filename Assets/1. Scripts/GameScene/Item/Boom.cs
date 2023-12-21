using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : ItemScript
{
    public override void Init()
    {
        base.Init();
    }

    public override void PickUp(PlayerScript player)
    {
        Debug.Log("Boom pickup");
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
