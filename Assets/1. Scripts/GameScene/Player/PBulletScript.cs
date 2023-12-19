using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletScript : MonoBehaviour
{
    public float speed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        if (GameParams.OutOfBounds(transform))
            Pooling.Instance.PBullet = this;
    }
}
