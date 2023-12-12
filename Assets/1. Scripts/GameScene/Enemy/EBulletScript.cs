using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (GameParams.OutOfBounds(transform))
            Destroy(gameObject);
    }
}
