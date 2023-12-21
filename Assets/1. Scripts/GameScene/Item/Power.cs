using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : ItemScript
{
    public override void Init()
    {
        base.Init();
    }

    public override void PickUp(PlayerScript player)
    {
        player.Support++;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
