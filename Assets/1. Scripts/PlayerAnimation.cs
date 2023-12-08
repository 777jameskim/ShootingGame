using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection
{
    Center,
    Left,
    Right
}


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> center;
    [SerializeField] private List<Sprite> left;
    [SerializeField] private List<Sprite> right;

    public float delay;

    Dictionary<PlayerDirection, List<Sprite>> Spritemap = new Dictionary<PlayerDirection, List<Sprite>>();

    // Start is called before the first frame update
    void Start()
    {
        Spritemap.Add(PlayerDirection.Center, center);
        Spritemap.Add(PlayerDirection.Left, left);
        Spritemap.Add(PlayerDirection.Right, right);
        GetComponent<SpriteAnimation>().SetSprite(center, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAnimation(PlayerDirection dir)
    {
        GetComponent<SpriteAnimation>().SetSprite(Spritemap[dir], delay);
    }
}