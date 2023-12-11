using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float boundaryY = 5;
    [SerializeField] private float speed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        if (transform.position.y > boundaryY)
            Destroy(gameObject);
    }
}
