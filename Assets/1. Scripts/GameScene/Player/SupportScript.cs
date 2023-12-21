using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportScript : MonoBehaviour
{
    [SerializeField] private PlayerScript player;

    [SerializeField] private float speed;
    [SerializeField] private float delay;

    [SerializeField] private PBulletScript bullet;
    [SerializeField] private float bullettimer;
    [SerializeField] private float bulletdelay;

    public bool Active
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EBulletScript>())
        {
            Pooling.Instance.EBullet = collision.GetComponent<EBulletScript>();
            Debug.Log($"Support Death: {name}");
            player.SupportDeath(this);
        }
    }
}
