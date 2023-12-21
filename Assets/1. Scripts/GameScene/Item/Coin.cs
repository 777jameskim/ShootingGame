using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemScript
{
    public override void Init()
    {
        base.Init();
    }

    public override void PickUp(PlayerScript player)
    {
        UI.Instance.Score += GameParams.scoreCoin;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
